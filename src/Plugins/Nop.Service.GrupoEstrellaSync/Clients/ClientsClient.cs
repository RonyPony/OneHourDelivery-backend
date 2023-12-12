using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Nop.Service.GrupoEstrellaSync.Entities;
using Nop.Service.GrupoEstrellaSync.Helper;
using Nop.Service.GrupoEstrellaSync.Models;
using Nop.Service.GrupoEstrellaSync.Models.Parameters;
using Nop.Service.GrupoEstrellaSync.Models.RootObjets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Nop.Service.GrupoEstrellaSync.Clients
{
    /// <summary>
    /// This class contains all the methods to be able to consume the client controllers of the Nop.Plugin.API  
    /// </summary>
    public class ClientsClient
    {
        private readonly string UrlApi;
        private readonly HttpClient HttpClientHandler;
        private readonly Service1 _service1;
        private static Service1 _serviceStatic;
        private readonly IConfiguration ClientsConfiguration;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="service1">An instance of <see cref="Service1"/>.</param>
        /// <param name="serviceStatic">An instance of <see cref="Service1"/>.</param>
        public ClientsClient(Service1 service1, Service1 serviceStatic)
        {
            _service1 = service1;
            _serviceStatic = serviceStatic;
            ClientsConfiguration = service1._configuration.GetSection("ClientSyncSettings");
            UrlApi = service1._configuration.GetSection("settings").GetSection("NopCommerceApiURL").Value;
            TokenHelper tokenHelper = new TokenHelper(_service1);
            HttpClientHandler = new HttpClient { BaseAddress = new Uri(UrlApi), DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue(tokenHelper.AuthenticationScheme, tokenHelper.ApiRequestToken) } };
        }

        private static string GetQueryString(object obj)
        {
            IEnumerable<string> properties = from p in obj.GetType().GetProperties()
                                             where p.GetValue(obj, null) != null
                                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return String.Join("&", properties.ToArray());
        }

        /// <summary>
        /// Method for sync Products
        /// </summary>
        public async Task SyncClients()
        {
            CustomersRootObject nopCommerceCustomers = await GetCustomersAsync();
            List<Cliente> stardenClients = GetStardenClients();

            foreach (CustomerDto nopClient in nopCommerceCustomers.Customers)
            {
                if (HasNit(nopClient))
                {
                    string nit = ToNit(nopClient.CustomerInfo.CustomerAttributes.First(x => x.Name == "NIT")
                        .DefaultValue.Trim());
                    if (!ClientExist(ref stardenClients, nopClient))
                    {
                        try
                        {
                            var uploadClient = ToUploadClient(nopClient, nit);

                            CreateClients(uploadClient);

                            int erpClientCode = int.Parse(GetErpCodeByNit(nit));
                            
                            SaveClientVendor(erpClientCode);

                            if (uploadClient.FECHA_NACIMIENTO != null)
                            {
                                ClientePersona clientePersona = new ClientePersona
                                {
                                    CodigoDeCliente = erpClientCode,
                                    RegistroId = "X",
                                    NumeroId = "0",
                                    FechaNacimiento = uploadClient.FECHA_NACIMIENTO,
                                    EstadoCivil = "N",
                                    NumeroDeHijos = 0,
                                    DescripcionTarjeta = "X",
                                    NumeroTarjeta = "X",
                                    TipoDeVivienda = "N",
                                    CodigoProfesion = ""
                                };

                                CreateClientsPerson(clientePersona);
                            }

                            SaveErpCode(nopClient.Id.ToString(), erpClientCode.ToString());
                        }
                        catch (Exception e)
                        {

                            // Get stack trace for the exception with source file information
                            var st = new StackTrace(e, true);
                            // Get the top stack frame
                            var frame = st.GetFrame(0);
                            // Get the line number from the stack frame
                            var line = frame.GetFileLineNumber();
                            _service1.WriteToFile("Clients syncing ended at" + DateTime.Now, LogHelper.Logtype.Info);
                            _service1.WriteToFile("ERROR Clients syncing: " + e.Message + " StackTrace:" + e.StackTrace + "line:" + line.ToString() + " at" + DateTime.Now, LogHelper.Logtype.Error);
                        }
                    }
                    else
                    {
                        try
                        {
                            int clientCode = int.Parse(GetErpCodeByNit(nit));
                            Cliente erpClient = GetErpClientByCode(clientCode);

                            erpClient.TelefonoCliente = nopClient.CustomerInfo.Phone;
                            erpClient.DireccionCliente = $"{nopClient.CustomerInfo.StreetAddress} {nopClient.CustomerInfo.StreetAddress2}";
                            erpClient.CodigoDepartamento = nopClient.CustomerInfo.CountryId;
                            erpClient.CodigoMunicipio = nopClient.CustomerInfo.StateProvinceId;
                            erpClient.CodigoTerritorio = int.Parse(nopClient.CustomerInfo.StateProvinceId.ToString().Substring(0, 1));
                            erpClient.DireccionCobro = nopClient.CustomerInfo.StreetAddress2?? "";

                            UpdateClient(erpClient);

                            // TODO: validate if client has ecommerce vendor, if they don't, add it
                            if (!ClientHasEcommerceVendor(clientCode))
                            {
                                SaveClientVendor(clientCode);
                            }

                            ClientePersona erpClientPerson = GetErpClientPersonByCode(clientCode);

                            if (erpClientPerson != null)
                            {
                                erpClientPerson.FechaNacimiento = nopClient.CustomerInfo.ParseDateOfBirth();
                                UpdateClientsPerson(erpClientPerson);
                            }

                            SaveErpCode(nopClient.Id.ToString(), GetErpCodeByNit(nit));
                        }
                        catch (Exception e)
                        {
                            _service1.WriteToFile("Clients syncing ended at" + DateTime.Now, LogHelper.Logtype.Info);
                            _service1.WriteToFile("ERROR Clients syncing: " + e.Message + " StackTrace:" + e.StackTrace + " at" + DateTime.Now, LogHelper.Logtype.Error);
                        }
                    }
                }
            }
        }

        private Cliente GetErpClientByCode(int clientCode)
        {
            using GrupoEstrellaContext db = new GrupoEstrellaContext();

            // This is not a HAMMER, with the old version, the code execute a TOP, but is not known at the ERP system  

            // OLD Version: return db.Clientes.FirstOrDefault(c => c.CodigoDeCliente == clientCode);

            return db.Clientes.Where(c => c.CodigoDeCliente == clientCode)
                .ToList()
                .FirstOrDefault(c => c.CodigoDeCliente == clientCode);
        }

        private ClientePersona GetErpClientPersonByCode(int clientCode)
        {
            using GrupoEstrellaContext db = new GrupoEstrellaContext();

            // This is not a HAMMER, with the old version, the code execute a TOP, but is not known at the ERP system  

            // OLD Version: return db.ClientePersonas.FirstOrDefault(c => c.CodigoDeCliente == clientCode);

            return db.ClientePersonas.Where(c => c.CodigoDeCliente == clientCode)
                .ToList()
                .FirstOrDefault(c => c.CodigoDeCliente == clientCode);
        }

        private void UpdateClient(Cliente erpClient)
        {
            using var context = new GrupoEstrellaContext();

            string query = "exec sp_Cambio_CLIENTES " +
                           $"@CODIGO_DE_CLIENTE = {erpClient.CodigoDeCliente}" +
                           $", @NOMBRE_CLIENTE = '{erpClient.NombreCliente}'" +
                           $", @ESTADO_CLIENTE = '{erpClient.EstadoCliente}'" +
                           $", @CONTACTO_CLIENTE = '{erpClient.ContactoCliente}'" +
                           $", @DIRECCION_CLIENTE = '{erpClient.DireccionCliente}'" +
                           $", @DIRECCION_ENVIO = '{erpClient.DireccionEnvio}'" +
                           $", @TELEFONO_CLIENTE = '{erpClient.TelefonoCliente}'" +
                           $", @TELEX_O_FAX_CLIENTE = '{erpClient.TelexOFaxCliente}'" +
                           $", @FECHA_APERTURA = '{erpClient.FechaApertura.ToString("yyyy-MM-dd")}'" +
                           $", @NIT_CLIENTE = '{erpClient.NitCliente}'" +
                           $", @LIMITE_CRED_CLIENTE = {erpClient.LimiteCredCliente}" +
                           $", @SALDO_CLIENTE = {erpClient.SaldoCliente}" +
                           $", @EXENT_IMPTO_CLIENTE = '{erpClient.ExentImptoCliente}'" +
                           $", @TIPO_DE_CLIENTE = '{erpClient.TipoDeCliente}'" +
                           $", @TPO_DESPACHO_CLIENTE = '{erpClient.TpoDespachoCliente}'" +
                           $", @CODIGO_DEPARTAMENTO = {erpClient.CodigoDepartamento}" +
                           $", @CODIGO_DE_PAIS = {erpClient.CodigoDePais}" +
                           $", @CODIGO_MUNICIPIO = {erpClient.CodigoMunicipio}" +
                           $", @CODIGO_TERRITORIO = {erpClient.CodigoTerritorio}" +
                           $", @CODIGO_DE_CLASE = '{erpClient.CodigoDeClase}'" +
                           $", @NIVEL_PRECIO = '{erpClient.NivelPrecio}'" +
                           $", @CODIGO_DE_CONDICION = {erpClient.CodigoDeCondicion} " +
                           $", @CODIGO_TIPO_PAGO = '{erpClient.CodigoTipoPago}'" +
                           $", @TOTAL_CHEQUES_RECH = {erpClient.TotalChequesRech}" +
                           $", @GIRO_DE_NEGOCIO = '{erpClient.GiroDeNegocio}'" +
                           $", @NUMERO_REGISTRO = '{erpClient.NumeroRegistro}'" +
                           $", @CODIGO_IMPUESTO = '{erpClient.CodigoImpuesto}'" +
                           $", @CODIGO_DE_MONEDA = '{erpClient.CodigoDeMoneda}'" +
                           $", @CODIGO_COBRADOR = {erpClient.CodigoCobrador.GetValueOrDefault()}" +
                           $", @CODIGO_DE_GRUPO = '{erpClient.CodigoDeGrupo}'" +
                           $", @PORCENTAJE_DESCUENTO = {erpClient.PorcentajeDescuento.GetValueOrDefault()}" +
                           $", @SALDO_FINANCIAMIENTO = {erpClient.SaldoFinanciamiento.GetValueOrDefault()}" +
                           $", @CODIGO_ECC = '{erpClient.CodigoEcc}'" +
                           $", @CODIGO_RUTA_DESPACHO = {erpClient.CodigoRutaDespacho.GetValueOrDefault()} " +
                           $", @CATEGORIA_CLIENTE = '{erpClient.CategoriaCliente}'" +
                           $", @PORC_MORA = {erpClient.PorcMora.GetValueOrDefault()}" +
                           $", @NOMBRE_COMERCIAL = '{erpClient.NombreComercial}'" +
                           $", @PRECIOS_CON_IVA = '{erpClient.PreciosConIva}'" +
                           $", @REFERENCIA_CLIENTE = '{erpClient.ReferenciaCliente}'" +
                           $", @CTRL_IVA_PERCIBIDO = '{erpClient.CtrlIvaPercibido}'" +
                           $", @TPO_DESPACHO_PARCIAL = '{erpClient.TpoDespachoParcial}'" +
                           $", @CONSUMO_INTERNO = '{erpClient.ConsumoInterno}'" +
                           $", @CEDULA_CLIENTE = '{erpClient.CedulaCliente}'" +
                           $", @DIRECCION_COBRO = '{erpClient.DireccionCobro}'" +
                           $", @DIRECCION_EMAIL = '{erpClient.DireccionEmail}'" +
                           $", @CLAVE_CLIENTE = '{erpClient.ClaveCliente}'" +
                           $", @ACTIVO_EN_WEB = '{erpClient.ActivoEnWeb}'";

            //_service1.WriteToFile(query + " at" + DateTime.Now, LogHelper.Logtype.Info);
            context.Database.ExecuteSqlRaw(query);
        }

        /// <summary>
        /// Saves mapping between customer ERP Id and NopCommerce customer Id
        /// </summary>
        /// <param name="nopId">NopCommerce customer Id</param>
        /// <param name="erpId">ERP customer Id</param>
        public void SaveErpCode(string nopId, string erpId)
        {
            string path = $"erp/customers?CustomerId={nopId}&ErpCustomerId={erpId}";

            Task<HttpResponseMessage> response = HttpClientHandler.PostAsync(path, new StringContent(""));
            response.Wait();

            if (!response.Result.IsSuccessStatusCode)
            {
                throw new Exception($"ERROR saving client ERPcode in NopCommerce : {response.Result.RequestMessage} at {DateTime.Now}");
            }
        }

        /// <summary>
        /// Gets ERP customer code by NopCommerce customer Id.
        /// </summary>
        /// <param name="nopCommerceCustomerId">NopCommerce customer Id</param>
        public string GetCustomerErpCodeByNopCommerceId(int nopCommerceCustomerId)
        {
            string path = $"erp/customers?nopCommerceCustomerId={nopCommerceCustomerId}";

            HttpResponseMessage response = HttpClientHandler.GetAsync(path).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"ERROR saving client ERPcode in NopCommerce : {response.RequestMessage} at {DateTime.Now}");
            }

            string jsonResponse = response.Content.ReadAsStringAsync().Result;

            ErpCustomerCodeModel result = JsonConvert.DeserializeObject<ErpCustomerCodeModel>(jsonResponse);

            return result.ErpCustomerCode;
        }

        private static bool ClientExist(ref List<Cliente> list, CustomerDto clientDto)
        {
            var nopCommerceNIT = clientDto.CustomerInfo.CustomerAttributes.First(x => x.Name == "NIT").DefaultValue;

            bool result = list.Any(item => item.NitCliente.Trim() == ToNit(nopCommerceNIT.Trim()));

            return result;
        }

        /// <summary>
        /// Gets ERP customer Id by NIT
        /// </summary>
        /// <param name="nit">NIT of the customer to search for</param>
        /// <returns></returns>
        public static string GetErpCodeByNit(string nit)
        {
            using GrupoEstrellaContext db = new GrupoEstrellaContext();

            var clientsList = db.Clientes.ToList();

            return clientsList.FirstOrDefault(x => x.NitCliente.Trim() == nit.Trim())?.CodigoDeCliente.ToString();
        }

        /// <summary>
        /// Gets NIT from customer attributes
        /// </summary>
        /// <param name="text">NIT CustomerAttribute</param>
        public static string ToNit(string text)
        {
            string NIT = "";
            if (string.IsNullOrEmpty(text.Trim()))
            {
                return NIT;
            }

            NIT = text.Trim().Length > 14 ? text.Trim().Substring(0, 14) : text.Trim();

            return NIT;
        }

        private static bool HasNit(CustomerDto clientDto)
        {
            if (!ExistNitAttribute(clientDto))
            {
                return false;
            }

            return !string.IsNullOrEmpty(clientDto.CustomerInfo.CustomerAttributes.First(x => x.Name == "NIT").DefaultValue);
        }

        private static bool ExistNitAttribute(CustomerDto clientDto) => clientDto.CustomerInfo.CustomerAttributes.Count(x => x.Name == "NIT") > 0;

        private async Task<CustomersRootObject> GetCustomersAsync()
        {
            using (GrupoEstrellaContext db = new GrupoEstrellaContext())
            {
                string path = "api/customers";
                CustomersParametersModel parametersModel = new Models.Parameters.CustomersParametersModel();

                path += "?" + GetQueryString(parametersModel);

                Task<HttpResponseMessage> response = HttpClientHandler.GetAsync(path);
                response.Wait();

                if (!response.Result.IsSuccessStatusCode)
                {
                    throw new Exception(response.ToString() + "   " + path + " call with StatusCode not Success. ");
                }
                try
                {
                    Task<string> json = response.Result.Content.ReadAsStringAsync();
                    json.Wait();

                    CustomersRootObject result = JsonConvert.DeserializeObject<CustomersRootObject>(json.Result);
                    return result;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        private static List<Cliente> GetStardenClients()
        {
            using GrupoEstrellaContext db = new GrupoEstrellaContext();
            var resultListClientes = db.Clientes.ToList();

            return resultListClientes;
        }
        //public static string GetAllFootprints(Exception x)
        //{
        //    var st = new StackTrace(x, true);
        //    var frames = st.GetFrames();
        //    var traceString = new StringBuilder();

        //    foreach (var frame in frames)
        //    {
        //        if (frame.GetFileLineNumber() < 1)
        //            continue;

        //        traceString.Append("File: " + frame.GetFileName());
        //        traceString.Append(", Method:" + frame.GetMethod().Name);
        //        traceString.Append(", LineNumber: " + frame.GetFileLineNumber());
        //        traceString.Append("  -->  ");
        //    }

        //    return traceString.ToString();
        //}

        /// <summary>
        /// Maps <see cref="CustomerDto"/> class to <see cref="UploadClient"/> class
        /// </summary>
        /// <param name="nopClient">Instance of <see cref="CustomerDto"/> to map</param>
        /// <param name="NIT">Client NIT</param>
        public UploadClient ToUploadClient(CustomerDto nopClient, string NIT)
        {

            string nrc = nopClient.CustomerInfo.CustomerAttributes.FirstOrDefault(x => x.Name == "NRC")?.DefaultValue ??
                         string.Empty;

            UploadClient result = new UploadClient();

            try
            {
                result.NOMBRE_CLIENTE = nopClient.FirstName + " " + nopClient.LastName;
                result.ESTADO_CLIENTE = ClientsConfiguration["ESTADO_CLIENTE"];
                result.CONTACTO_CLIENTE = string.IsNullOrEmpty(nopClient.AdminComment) ? "" : nopClient.AdminComment;
                result.DIRECCION_CLIENTE = $"{nopClient.CustomerInfo.StreetAddress} {nopClient.CustomerInfo.StreetAddress2}";
                result.DIRECCION_ENVIO = $"{nopClient.ShippingAddress?.Address1} {nopClient.ShippingAddress?.Address2} {nopClient.ShippingAddress?.CountryName} {nopClient.ShippingAddress?.StateProvinceName}";
                result.TELEFONO_CLIENTE = nopClient.CustomerInfo.Phone ?? string.Empty;
                result.TELEX_O_FAX_CLIENTE = ClientsConfiguration["TELEX_O_FAX_CLIENTE"];
                result.FECHA_APERTURA = DateTime.Now;
                result.DIRECCION_COBRO = $"{nopClient.CustomerInfo.StreetAddress2}";

                result.NIT_CLIENTE = NIT;
                result.LIMITE_CRED_CLIENTE = int.Parse(ClientsConfiguration["LIMITE_CRED_CLIENTE"]);
                result.SALDO_CLIENTE = int.Parse(ClientsConfiguration["SALDO_CLIENTE"]);
                result.EXENT_IMPTO_CLIENTE = ClientsConfiguration["EXENT_IMPTO_CLIENTE"];
                result.TIPO_DE_CLIENTE = ClientsConfiguration["TIPO_DE_CLIENTE"];
                result.TPO_DESPACHO_CLIENTE = ClientsConfiguration["TPO_DESPACHO_CLIENTE"];
                result.CODIGO_DEPARTAMENTO = nopClient.CustomerInfo.CountryId;
                result.CODIGO_DE_PAIS = int.Parse(ClientsConfiguration["CODIGO_DE_PAIS"]);
                result.CODIGO_MUNICIPIO = nopClient.CustomerInfo.StateProvinceId;
                result.CODIGO_TERRITORIO = int.Parse(nopClient.CustomerInfo.StateProvinceId.ToString().Substring(0, 1));
                result.CODIGO_DE_CLASE = ClientsConfiguration["CODIGO_DE_CLASE"];
                result.NIVEL_PRECIO = ClientsConfiguration["NIVEL_PRECIO"];
                result.CODIGO_DE_CONDICION = int.Parse(ClientsConfiguration["CODIGO_DE_CONDICION"]);
                result.CODIGO_TIPO_PAGO = ClientsConfiguration["CODIGO_TIPO_PAGO"];
                result.TOTAL_CHEQUES_RECH = int.Parse(ClientsConfiguration["TOTAL_CHEQUES_RECH"]);
                result.GIRO_DE_NEGOCIO = nopClient.CustomerInfo.CustomerAttributes.FirstOrDefault(x => x.Name == "GIRO")?.DefaultValue ?? string.Empty;
                result.NUMERO_REGISTRO = nrc;
                result.CODIGO_IMPUESTO = ClientsConfiguration["CODIGO_IMPUESTO"];
                result.CODIGO_DE_MONEDA = ClientsConfiguration["CODIGO_DE_MONEDA"];

                result.CODIGO_COBRADOR = int.Parse(ClientsConfiguration["CODIGO_COBRADOR"]);

                result.CODIGO_DE_GRUPO = ClientsConfiguration["CODIGO_DE_GRUPO"];
                result.PORCENTAJE_DESCUENTO = int.Parse(ClientsConfiguration["PORCENTAJE_DESCUENTO"]);
                result.SALDO_FINANCIAMIENTO = int.Parse(ClientsConfiguration["SALDO_FINANCIAMIENTO"]);
                result.CODIGO_ECC = ClientsConfiguration["CODIGO_ECC"];

                result.CODIGO_RUTA_DESPACHO = int.Parse(ClientsConfiguration["CODIGO_RUTA_DESPACHO"]);
                result.CATEGORIA_CLIENTE = ClientsConfiguration["CATEGORIA_CLIENTE"];
                result.PORC_MORA = int.Parse(ClientsConfiguration["PORC_MORA"]);
                result.NOMBRE_COMERCIAL = nopClient.CustomerInfo.Company ?? string.Empty;
                result.FECHA_NACIMIENTO = nopClient.CustomerInfo.ParseDateOfBirth();
                result.PRECIOS_CON_IVA = GetIvaPrice(nrc);
                result.REFERENCIA_CLIENTE = ClientsConfiguration["REFERENCIA_CLIENTE"];
                result.CTRL_IVA_PERCIBIDO = ClientsConfiguration["CTRL_IVA_PERCIBIDO"];
                result.TPO_DESPACHO_PARCIAL = ClientsConfiguration["TPO_DESPACHO_PARCIAL"];
                result.CONSUMO_INTERNO = ClientsConfiguration["CONSUMO_INTERNO"];
                result.CEDULA_CLIENTE = ClientsConfiguration["CEDULA_CLIENTE"];
                
                result.DIRECCION_EMAIL = nopClient.Email;
                result.CLAVE_CLIENTE = ClientsConfiguration["CLAVE_CLIENTE"];
                result.ACTIVO_EN_WEB = ClientsConfiguration["ACTIVO_EN_WEB"];
            }
            catch (Exception ex)
            {

                // Get stack trace for the exception with source file information
                var st = new StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(0);
                // Get the line number from the stack frame
                var line = frame.GetFileLineNumber();
                _serviceStatic.WriteToFile(" Line:" + line + " StackTrace:" + ex.StackTrace + " InnerException:" + ex.InnerException?.Message + " Message:" + ex.Message + " at" + DateTime.Now, LogHelper.Logtype.Info);

            }


            return result;
            //return new UploadClient
            //{
            //    NOMBRE_CLIENTE = nopClient.FirstName + " " + nopClient.LastName,
            //    ESTADO_CLIENTE = ClientsConfiguration["ESTADO_CLIENTE"],
            //    CONTACTO_CLIENTE = string.IsNullOrEmpty(nopClient.AdminComment) ? "" : nopClient.AdminComment,
            //    DIRECCION_CLIENTE = $"{nopClient.CustomerInfo.StreetAddress} {nopClient.CustomerInfo.StreetAddress2}",
            //    DIRECCION_ENVIO = $"{nopClient.ShippingAddress.Address1} {nopClient.ShippingAddress.Address2} {nopClient.ShippingAddress.CountryName} {nopClient.ShippingAddress.StateProvinceName}",
            //    TELEFONO_CLIENTE = nopClient.CustomerInfo.Phone ?? string.Empty,
            //    TELEX_O_FAX_CLIENTE = ClientsConfiguration["TELEX_O_FAX_CLIENTE"],
            //    FECHA_APERTURA = DateTime.Now,
            //    NIT_CLIENTE = NIT,
            //    LIMITE_CRED_CLIENTE = int.Parse(ClientsConfiguration["LIMITE_CRED_CLIENTE"]),
            //    SALDO_CLIENTE = int.Parse(ClientsConfiguration["SALDO_CLIENTE"]),
            //    EXENT_IMPTO_CLIENTE = ClientsConfiguration["EXENT_IMPTO_CLIENTE"],
            //    TIPO_DE_CLIENTE = ClientsConfiguration["TIPO_DE_CLIENTE"],
            //    TPO_DESPACHO_CLIENTE = ClientsConfiguration["TPO_DESPACHO_CLIENTE"],
            //    CODIGO_DEPARTAMENTO = nopClient.CustomerInfo.CountryId,
            //    CODIGO_DE_PAIS = int.Parse(ClientsConfiguration["CODIGO_DE_PAIS"]),
            //    CODIGO_MUNICIPIO = nopClient.CustomerInfo.StateProvinceId,
            //    CODIGO_TERRITORIO = int.Parse(nopClient.CustomerInfo.StateProvinceId.ToString().Substring(0, 1)),
            //    CODIGO_DE_CLASE = ClientsConfiguration["CODIGO_DE_CLASE"],
            //    NIVEL_PRECIO = ClientsConfiguration["NIVEL_PRECIO"],
            //    CODIGO_DE_CONDICION = int.Parse(ClientsConfiguration["CODIGO_DE_CONDICION"]),
            //    CODIGO_TIPO_PAGO = ClientsConfiguration["CODIGO_TIPO_PAGO"],
            //    TOTAL_CHEQUES_RECH = int.Parse(ClientsConfiguration["TOTAL_CHEQUES_RECH"]),
            //    GIRO_DE_NEGOCIO = nopClient.CustomerInfo.CustomerAttributes.FirstOrDefault(x => x.Name == "GIRO")?.DefaultValue ?? string.Empty,
            //    NUMERO_REGISTRO = nrc,
            //    CODIGO_IMPUESTO = ClientsConfiguration["CODIGO_IMPUESTO"],
            //    CODIGO_DE_MONEDA = ClientsConfiguration["CODIGO_DE_MONEDA"],
            //    // Crear código cobrador en el ERP para la tienda en linea
            //    CODIGO_COBRADOR = int.Parse(ClientsConfiguration["CODIGO_COBRADOR"]),
            //    // Crear código de grupo en el ERP para la tienda en linea
            //    CODIGO_DE_GRUPO = ClientsConfiguration["CODIGO_DE_GRUPO"],
            //    PORCENTAJE_DESCUENTO = int.Parse(ClientsConfiguration["PORCENTAJE_DESCUENTO"]),
            //    SALDO_FINANCIAMIENTO = int.Parse(ClientsConfiguration["SALDO_FINANCIAMIENTO"]),
            //    CODIGO_ECC = ClientsConfiguration["CODIGO_ECC"],
            //    // Crear código ruta despacho en el ERP para la tienda en linea
            //    CODIGO_RUTA_DESPACHO = int.Parse(ClientsConfiguration["CODIGO_RUTA_DESPACHO"]),
            //    CATEGORIA_CLIENTE = ClientsConfiguration["CATEGORIA_CLIENTE"],
            //    PORC_MORA = int.Parse(ClientsConfiguration["PORC_MORA"]),
            //    NOMBRE_COMERCIAL = nopClient.CustomerInfo.Company ?? string.Empty,
            //    FECHA_NACIMIENTO = nopClient.CustomerInfo.ParseDateOfBirth(),
            //    PRECIOS_CON_IVA = GetIvaPrice(nrc),
            //    REFERENCIA_CLIENTE = ClientsConfiguration["REFERENCIA_CLIENTE"],
            //    CTRL_IVA_PERCIBIDO = ClientsConfiguration["CTRL_IVA_PERCIBIDO"],
            //    TPO_DESPACHO_PARCIAL = ClientsConfiguration["TPO_DESPACHO_PARCIAL"],
            //    CONSUMO_INTERNO = ClientsConfiguration["CONSUMO_INTERNO"],
            //    CEDULA_CLIENTE = ClientsConfiguration["CEDULA_CLIENTE"],
            //    DIRECCION_COBRO = ClientsConfiguration["DIRECCION_COBRO"],
            //    DIRECCION_EMAIL = nopClient.Email,
            //    CLAVE_CLIENTE = ClientsConfiguration["CLAVE_CLIENTE"],
            //    ACTIVO_EN_WEB = ClientsConfiguration["ACTIVO_EN_WEB"]
            //};
        }

        private static string GetIvaPrice(string nrc)
        {
            // Price with IVA #BusinesLogic
            //F = Price not include IVA , for Business bill
            //C = Price include IVA
            return string.IsNullOrEmpty(nrc) ? "C" : "F";

        }

        /// <summary>
        /// Saves a customer to the database
        /// </summary>
        /// <param name="uploadClient">Customer to save</param>
        public static void CreateClients(UploadClient uploadClient)
        {
            using var context = new GrupoEstrellaContext();

            string query = $"exec sp_Alta_CLIENTES @CODIGO_DE_CLIENTE = NULL" +
                           $", @NOMBRE_CLIENTE = '{uploadClient.NOMBRE_CLIENTE}' " +
                           $", @ESTADO_CLIENTE       = '{uploadClient.ESTADO_CLIENTE}' " +
                           $", @CONTACTO_CLIENTE      = '{uploadClient.CONTACTO_CLIENTE}' " +
                           $", @DIRECCION_CLIENTE     = '{uploadClient.DIRECCION_CLIENTE}' " +
                           $", @DIRECCION_ENVIO       = '{uploadClient.DIRECCION_ENVIO}' " +
                           $", @TELEFONO_CLIENTE      = '{uploadClient.TELEFONO_CLIENTE}' " +
                           $", @TELEX_O_FAX_CLIENTE   = '{uploadClient.TELEX_O_FAX_CLIENTE}' " +
                           $", @FECHA_APERTURA        = '{uploadClient.FECHA_APERTURA.ToString("yyyy-MM-dd")}' " +
                           $", @NIT_CLIENTE           = '{uploadClient.NIT_CLIENTE}' " +
                           $", @LIMITE_CRED_CLIENTE   = {uploadClient.LIMITE_CRED_CLIENTE.ToString()} " +
                           $", @SALDO_CLIENTE         = {uploadClient.SALDO_CLIENTE.ToString()} " +
                           $", @EXENT_IMPTO_CLIENTE   = '{uploadClient.EXENT_IMPTO_CLIENTE}' " +
                           $", @TIPO_DE_CLIENTE       = '{uploadClient.TIPO_DE_CLIENTE}' " +
                           $", @TPO_DESPACHO_CLIENTE  = '{uploadClient.TPO_DESPACHO_CLIENTE}' " +
                           $", @CODIGO_DEPARTAMENTO   = {uploadClient.CODIGO_DEPARTAMENTO.ToString()} " +
                           $", @CODIGO_DE_PAIS        = {uploadClient.CODIGO_DE_PAIS.ToString()} " +
                           $", @CODIGO_MUNICIPIO      = {uploadClient.CODIGO_MUNICIPIO.ToString()} " +
                           $", @CODIGO_TERRITORIO     = {uploadClient.CODIGO_TERRITORIO.ToString()} " +
                           $", @CODIGO_DE_CLASE       = '{uploadClient.CODIGO_DE_CLASE}' " +
                           $", @NIVEL_PRECIO          = '{uploadClient.NIVEL_PRECIO}' " +
                           $", @CODIGO_DE_CONDICION   = {uploadClient.CODIGO_DE_CONDICION.ToString()} " +
                           $", @CODIGO_TIPO_PAGO      = '{uploadClient.CODIGO_TIPO_PAGO}' " +
                           $", @TOTAL_CHEQUES_RECH    = {uploadClient.TOTAL_CHEQUES_RECH.ToString()} " +
                           $", @GIRO_DE_NEGOCIO       = '{uploadClient.GIRO_DE_NEGOCIO}' " +
                           $", @NUMERO_REGISTRO       = '{uploadClient.NUMERO_REGISTRO}' " +
                           $", @CODIGO_IMPUESTO       = '{uploadClient.CODIGO_IMPUESTO}' " +
                           $", @CODIGO_DE_MONEDA      = '{uploadClient.CODIGO_DE_MONEDA}' " +
                           $", @CODIGO_COBRADOR       = {uploadClient.CODIGO_COBRADOR.ToString()} " +
                           $", @CODIGO_DE_GRUPO       = '{uploadClient.CODIGO_DE_GRUPO}' " +
                           $", @PORCENTAJE_DESCUENTO  = {uploadClient.PORCENTAJE_DESCUENTO.ToString()} " +
                           $", @SALDO_FINANCIAMIENTO  = {uploadClient.SALDO_FINANCIAMIENTO.ToString()} " +
                           $", @CODIGO_ECC            = '{uploadClient.CODIGO_ECC}' " +
                           $", @CODIGO_RUTA_DESPACHO  = {uploadClient.CODIGO_RUTA_DESPACHO.ToString()} " +
                           $", @CATEGORIA_CLIENTE     = '{uploadClient.CATEGORIA_CLIENTE}' " +
                           $", @PORC_MORA             = {uploadClient.PORC_MORA.ToString()} " +
                           $", @NOMBRE_COMERCIAL      = '{uploadClient.NOMBRE_COMERCIAL}' " +
                           $", @PRECIOS_CON_IVA       = '{uploadClient.PRECIOS_CON_IVA}' " +
                           $", @REFERENCIA_CLIENTE    = '{uploadClient.REFERENCIA_CLIENTE}' " +
                           $", @CTRL_IVA_PERCIBIDO    = '{uploadClient.CTRL_IVA_PERCIBIDO}' " +
                           $", @TPO_DESPACHO_PARCIAL  = '{uploadClient.TPO_DESPACHO_PARCIAL}' " +
                           $", @CONSUMO_INTERNO       = '{uploadClient.CONSUMO_INTERNO}' " +
                           $", @CEDULA_CLIENTE        = '{uploadClient.CEDULA_CLIENTE}' " +
                           $", @DIRECCION_COBRO       = '{uploadClient.DIRECCION_COBRO}' " +
                           $", @DIRECCION_EMAIL       = '{uploadClient.DIRECCION_EMAIL}' " +
                           $", @CLAVE_CLIENTE         = '{uploadClient.CLAVE_CLIENTE}' " +
                           $", @ACTIVO_EN_WEB         = '{uploadClient.ACTIVO_EN_WEB}' ";

            //_serviceStatic.WriteToFile(query + " at" + DateTime.Now, LogHelper.Logtype.Info);
            context.Database.ExecuteSqlRaw(query);
        }

        private void SaveClientVendor(int erpClientId)
        {
            using var context = new GrupoEstrellaContext();

            var orderSettings = _service1._configuration.GetSection("OrderSyncSettings");

            string query = $"exec sp_Alta_CLIENTE_VENDEDOR  @CODIGO_DE_CLIENTE = {erpClientId}, " +
                           $"@CODIGO_VENDEDOR = {orderSettings["CodigoVendedor"]}";

            //_service1.WriteToFile(query + " at" + DateTime.Now, LogHelper.Logtype.Info);
            context.Database.ExecuteSqlRaw(query);
        }

        private bool ClientHasEcommerceVendor(int erpClientId)
        {
            using GrupoEstrellaContext db = new GrupoEstrellaContext();

            // This is not a HAMMER, with the old version, the code execute a TOP, but is not known at the ERP system

            var orderSettings = _service1._configuration.GetSection("OrderSyncSettings");

            int ecommerceVendorId = int.Parse(orderSettings["CodigoVendedor"]);

            var foundClientVendor = db.ClienteVendedores
                .Where(c => c.CodigoDeCliente == erpClientId && c.CodigoVendedor == ecommerceVendorId)
                .ToList()
                .FirstOrDefault();

            return foundClientVendor != null;
        }

        private void CreateClientsPerson(ClientePersona clientPerson)
        {
            using var context = new GrupoEstrellaContext();

            string query = $"exec sp_Alta_CLIENTE_PERSONA  @CODIGO_DE_CLIENTE = '{clientPerson.CodigoDeCliente}' " +
                           $", @REGISTRO_ID = '{clientPerson.RegistroId}' " +
                           $", @NUMERO_ID = '{clientPerson.RegistroId}' " +
                           $", @FECHA_NACIMIENTO = '{clientPerson.FechaNacimiento.GetValueOrDefault().ToString("yyyy-MM-dd")}' " +
                           $", @ESTADO_CIVIL = '{clientPerson.EstadoCivil}' " +
                           $", @NUMERO_DE_HIJOS = '{clientPerson.NumeroDeHijos}' " +
                           $", @DESCRIPCION_TARJETA = '{clientPerson.DescripcionTarjeta}' " +
                           $", @NUMERO_TARJETA = '{clientPerson.NumeroTarjeta}' " +
                           $", @TIPO_DE_VIVIENDA = '{clientPerson.TipoDeVivienda}' " +
                           $", @CODIGO_PROFESION = '{clientPerson.CodigoProfesion}' ";

            //_service1.WriteToFile(query + " at" + DateTime.Now, LogHelper.Logtype.Info);
            context.Database.ExecuteSqlRaw(query);
        }
        private void UpdateClientsPerson(ClientePersona clientPerson)
        {
            using var context = new GrupoEstrellaContext();

            string query = $"exec sp_Cambio_CLIENTE_PERSONA  @CODIGO_DE_CLIENTE = '{clientPerson.CodigoDeCliente}' " +
                           $", @REGISTRO_ID = '{clientPerson.RegistroId}' " +
                           $", @NUMERO_ID = '{clientPerson.RegistroId}' " +
                           $", @FECHA_NACIMIENTO = '{clientPerson.FechaNacimiento.GetValueOrDefault().ToString("yyyy-MM-dd")}' " +
                           $", @ESTADO_CIVIL = '{clientPerson.EstadoCivil}' " +
                           $", @NUMERO_DE_HIJOS = '{clientPerson.NumeroDeHijos}' " +
                           $", @DESCRIPCION_TARJETA = '{clientPerson.DescripcionTarjeta}' " +
                           $", @NUMERO_TARJETA = '{clientPerson.NumeroTarjeta}' " +
                           $", @TIPO_DE_VIVIENDA = '{clientPerson.TipoDeVivienda}' " +
                           $", @CODIGO_PROFESION = '{clientPerson.CodigoProfesion}' ";


            //_service1.WriteToFile(query + " at" + DateTime.Now, LogHelper.Logtype.Info);
            context.Database.ExecuteSqlRaw(query);
        }

        /// <summary>
        /// Gets NopCommerce customer by Id
        /// </summary>
        /// <param name="customerId">nopCommerce customer Id</param>
        public async Task<CustomerDto> GetCustomerById(int customerId)
        {
            HttpResponseMessage response = await HttpClientHandler.GetAsync($"api/customers/{customerId}");

            string json = await response.Content.ReadAsStringAsync();

            CustomersRootObject result = JsonConvert.DeserializeObject<CustomersRootObject>(json);

            return result.Customers.FirstOrDefault();
        }

        /// <summary>
        /// Gets the Id of the last client registered on Erp
        /// </summary>
        /// <returns></returns>
        public static int GetLastErpClientIdRegistered()
        {
            using GrupoEstrellaContext db = new GrupoEstrellaContext();

            return db.Clientes.OrderByDescending(erpClient => erpClient.CodigoDeCliente).FirstOrDefault().CodigoDeCliente;
        }
    }
}

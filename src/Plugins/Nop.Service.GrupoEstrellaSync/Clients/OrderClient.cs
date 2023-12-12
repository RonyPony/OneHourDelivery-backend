using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Nop.Plugin.Api.DTO.OrderItems;
using Nop.Plugin.Api.DTO.Orders;
using Nop.Plugin.Api.Models.OrdersParameters;
using Nop.Service.GrupoEstrellaSync.Entities;
using Nop.Service.GrupoEstrellaSync.Helper;
using Nop.Service.GrupoEstrellaSync.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Nop.Core.Domain.Payments;

namespace Nop.Service.GrupoEstrellaSync.Clients
{
    /// <summary>
    /// This class contains all the methods to be able to consume the Order controller of the Nop.Plugin.API 
    /// </summary>
    public class OrderClient
    {
        private readonly HttpClient _client;
        private readonly string urlApi;
        private readonly Service1 _service1;
        private readonly ClientsClient _clientsClient;



        public OrderClient(Service1 service1, ClientsClient clientsClient)
        {

            _service1 = service1;
            _clientsClient = clientsClient;
            urlApi = service1._configuration.GetSection("settings").GetSection("NopCommerceApiURL").Value;
            TokenHelper tokenHelper = new TokenHelper(_service1);

            _client = new HttpClient
            {
                BaseAddress = new Uri(urlApi),
                DefaultRequestHeaders =
                {
                    Authorization = new AuthenticationHeaderValue(tokenHelper.AuthenticationScheme, tokenHelper.ApiRequestToken)
                }
            };
        }

        private string GetQueryString(object obj)
        {
            IEnumerable<string> properties = from p in obj.GetType().GetProperties()
                                             where p.GetValue(obj, null) != null
                                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return string.Join("&", properties.ToArray());
        }

        private int CreateOrder(PedidosEnc order)
        {

            using var context = new GrupoEstrellaContext();

            string orderIdQuery = $@"EXEC [dbo].[sp_Cambio_FAC_CORR_PEDIDOS_ON_LINE]
                @ID_EMPRESA = '{order.IdEmpresa}'
		        ,@ID_SUCURSAL = '{order.IdSucursal}'
		        ,@ID_CENTRO_OPERATIVO = '{order.IdCentroOperativo}'
                ,@CORRELATIVO = @OrderId OUTPUT";

            var queryParams = new[]
            {
                new SqlParameter
                {
                    ParameterName = "@OrderId",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output,
                    Value = 0
                },
            };

            context.Database.ExecuteSqlRaw(orderIdQuery, queryParams);

            int.TryParse(queryParams.First().Value.ToString(), out int erpOrderId);

            // TODO: Agregar los siguientes campos en el ERP como valores por defecto para la tienda en linea
            // TipoEntrega

            string query = @$"EXEC [dbo].[sp_Alta_PEDIDOS_ENC]
		        @ID_EMPRESA = '{order.IdEmpresa}'
		        ,@ID_SUCURSAL = '{order.IdSucursal}'
		        ,@ID_CENTRO_OPERATIVO = '{order.IdCentroOperativo}'
		        ,@NUMERO_DE_PEDIDO = {erpOrderId}
		        ,@FECHA_PEDIDO = '{order.FechaPedido.ToString("yyyy-MM-dd")}'
		        ,@FECHA_OFRECIDO = '{order.FechaOfrecido.ToString("yyyy-MM-dd")}'
		        ,@USUARIO_INGRESO_PEDI = '{order.UsuarioIngresoPedi}'
		        ,@FECHA_INGRESO_PEDIDO = '{order.FechaIngresoPedido.ToString("yyyy-MM-dd")}'
		        ,@SUBTOTAL_PEDIDO = {order.SubtotalPedido}
		        ,@MONTO_FLETE = {order.MontoFlete}
		        ,@MONTO_SEGURO = {order.MontoSeguro}
		        ,@IVA_PEDIDO = {order.IvaPedido}
		        ,@TOTAL_GENERAL_PEDIDO = {order.TotalGeneralPedido}
		        ,@ESTADO_PEDIDO = '{order.EstadoPedido}'
		        ,@FLETE_A_CARGO_DE = '{order.FleteACargoDe}'
		        ,@TIPO_DE_DESCUENTO = '{order.TipoDeDescuento}'
		        ,@NOMBRE_A_FACTURAR = '{order.NombreAFacturar}'
		        ,@NIT_A_FACTURAR = '{order.NitAFacturar}'
		        ,@REGISTRO_A_FACTURAR = '{order.RegistroAFacturar}'
		        ,@PARTICIPA_PROMOCION = '{order.ParticipaPromocion}'
		        ,@DIRECCION_FACTURAR = '{order.DireccionFacturar}'
		        ,@DOCUMENTO_A_IMPRIMIR = '{order.DocumentoAImprimir}'
		        ,@CAMB_MONED_LOCAL_PED = {order.CambMonedLocalPed}
		        ,@CODIGO_DE_CLIENTE = {order.CodigoDeCliente}
		        ,@CODIGO_VENDEDOR = {order.CodigoVendedor}
		        ,@CODIGO_DE_CONDICION = {order.CodigoDeCondicion}
		        ,@CODIGO_TIPO_PAGO = '{order.CodigoTipoPago}'
		        ,@NO_DESPACHO = {order.NoDespacho}
		        ,@NIVEL_PRECIO = '{order.NivelPrecio}'
                ,@DESPACHO_AUTOMATICO = '{order.DespachoAutomatico}'
                ,@TIPO_ENTREGA = '{order.TipoEntrega}'
		        ,@PORCENTAJE_DE_IVA = {order.PorcentajeDeIva}
		        ,@PORC_DESCUENTO_GLOB = {order.PorcDescuentoGlob}
		        ,@MONTO_DESCUENTO_LINE = {order.MontoDescuentoLine}
		        ,@MONTO_EN_TARJETAS = {order.MontoEnTarjetas}
		        ,@NUMERO_PEDIDO_WEB = {order.NumeroPedidoWeb}";

            context.Database.ExecuteSqlRaw(query);

            return erpOrderId;
        }

        private void CreateOrderDetail(List<PedidosDet> orderItems)
        {
            using var context = new GrupoEstrellaContext();

            foreach (var orderItem in orderItems)
            {

                string query = @$"EXEC [dbo].[sp_Alta_PEDIDOS_DET]
		            @ID_EMPRESA = '{orderItem.IdEmpresa}'
		            ,@ID_SUCURSAL = '{orderItem.IdSucursal}'
		            ,@ID_CENTRO_OPERATIVO = '{orderItem.IdCentroOperativo}'
		            ,@NUMERO_DE_PEDIDO = {orderItem.NumeroDePedido}
		            ,@PRODUCT0 = '{orderItem.Product0}'
		            ,@CODIGO_UNIDAD_VENTA = '{orderItem.CodigoUnidadVenta}'
		            ,@CANTIDAD_PEDIDA = {orderItem.CantidadPedida}
		            ,@CANTIDAD_DESPACHADA = {orderItem.CantidadDespachada}
		            ,@PRECIO_UNIDAD_VENTA = {orderItem.PrecioUnidadVenta}
		            ,@MONTO_DESCUENTO_PDET = {orderItem.MontoDescuentoPdet}
		            ,@FACTOR_CONVERSION_UN = {orderItem.FactorConversionUn}
		            ,@MONTO_DESCUENTO_GLOB = {orderItem.MontoDescuentoGlob}
		            ,@PORC_DESCUENTO_LINEA = {orderItem.PorcDescuentoLinea}
		            ,@MONTO_IVA = {orderItem.MontoIva}
		            ,@SUBTOTAL_VENTAS = {orderItem.SubtotalVentas}
		            ,@PRECIO_AFECTADO = {orderItem.PrecioAfectado}";


                context.Database.ExecuteSqlRaw(query);
            }
        }

        private void SaveOrderIdErpOrderIdMapping(int nopId, int erpId)
        {
            string path = $"erp/orders?OrderId={nopId}&ErpOrderId={erpId}";

            HttpResponseMessage response = _client.PostAsync(path, new StringContent("")).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"ERROR saving client ERPcode in NopCommerce : {response.RequestMessage} at {DateTime.Now}");
            }
        }

        private async Task<OrdersRootObject> GetOrdersAsync()
        {
            var parametersModel = new OrdersParametersModel
            {
                Limit = int.MaxValue,
                PaymentStatus = PaymentStatus.Paid
            };

            string path = "api/orders";
            path += "?" + GetQueryString(parametersModel);

            HttpResponseMessage response = await _client.GetAsync(path);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"{response} {path} call with StatusCode not Success.");
            }

            string json = await response.Content.ReadAsStringAsync();

            var apiResult = JsonConvert.DeserializeObject<OrdersRootObject>(json);

            return apiResult;
        }

        private PedidosEnc ToPedidosEnc(OrderDto orderDto)
        {
            CustomerDto customer = _clientsClient.GetCustomerById(orderDto.Customer.Id).Result;
            string nit = string.Empty;
            string nrc = string.Empty;
            string erpCustomerCode = string.Empty;

            if (customer.CustomerInfo != null)
            {
                nit = ClientsClient.ToNit(customer.CustomerInfo.CustomerAttributes.FirstOrDefault(x => x.Name == "NIT")?
                    .DefaultValue ?? string.Empty);

                nrc = customer.CustomerInfo.CustomerAttributes.FirstOrDefault(x => x.Name == "NRC")?.DefaultValue ??
                    string.Empty;

                erpCustomerCode = _clientsClient.GetCustomerErpCodeByNopCommerceId(customer.Id);
            }

            if (string.IsNullOrWhiteSpace(erpCustomerCode))
            {
                UploadClient uploadClient = _clientsClient.ToUploadClient(customer, nit);

                ClientsClient.CreateClients(uploadClient);

                erpCustomerCode = ClientsClient.GetLastErpClientIdRegistered().ToString();

                _clientsClient.SaveErpCode(customer.Id.ToString(), erpCustomerCode);
            }

            int.TryParse(erpCustomerCode, out int parsedCustomerCode);

            IConfigurationSection configuration = _service1._configuration.GetSection("OrderSyncSettings");


            string fleteACargoDe = (orderDto.OrderShippingExclTax == 0) ? configuration.GetSection("FleteACargoDeCliente").Value : configuration.GetSection("FleteACargoDeEmpresa").Value;
            var result = new PedidosEnc
            {
                NumeroPedidoWeb = orderDto.Id,
                FechaPedido = orderDto.CreatedOnUtc.GetValueOrDefault(),
                CodigoDeCliente = parsedCustomerCode,
                SubtotalPedido = orderDto.OrderSubtotalInclTax,
                IvaPedido = orderDto.OrderTax,
                MontoFlete = orderDto.OrderShippingExclTax,
                TotalGeneralPedido = orderDto.OrderTotal,
                RegistroAFacturar = nrc,
                IdEmpresa = configuration.GetSection("IdEmpresa").Value,
                IdSucursal = configuration.GetSection("IdSucursal").Value,
                IdCentroOperativo = configuration.GetSection("IdCentroOperativo").Value,
                FechaOfrecido = orderDto.CreatedOnUtc.GetValueOrDefault(),
                UsuarioIngresoPedi = _service1._configuration.GetSection("ApiAuthCredentials")["Username"],
                FechaIngresoPedido = orderDto.CreatedOnUtc.GetValueOrDefault(),
                MontoSeguro = int.Parse(configuration.GetSection("MontoSeguro").Value),
                EstadoPedido = configuration.GetSection("EstadoPedido").Value,
                FleteACargoDe = fleteACargoDe,
                TipoDeDescuento = configuration.GetSection("TipoDeDescuento").Value,
                NombreAFacturar = $"{customer.FirstName} {customer.LastName}",
                NitAFacturar = string.IsNullOrWhiteSpace(nit) ? configuration.GetSection("NitPorDefecto").Value : nit.Trim(),
                ParticipaPromocion = configuration.GetSection("ParticipaPromocion").Value,
                DireccionFacturar = $"{orderDto.ShippingAddress.Address1} {orderDto.ShippingAddress.Address2} {orderDto.ShippingAddress.CountryName} {orderDto.ShippingAddress.StateProvinceName}",
                DocumentoAImprimir = string.IsNullOrWhiteSpace(nrc) ? "C" : "F",
                CambMonedLocalPed = int.Parse(configuration.GetSection("CambMonedLocalPed").Value),
                CodigoVendedor = int.Parse(configuration.GetSection("CodigoVendedor").Value),
                CodigoDeCondicion = int.Parse(configuration.GetSection("CodigoDeCondicion").Value),
                CodigoTipoPago = configuration.GetSection("CodigoTipoPago").Value,
                NoDespacho = int.Parse(configuration.GetSection("NoDespacho").Value),
                NivelPrecio = configuration.GetSection("NivelPrecio").Value,
                DespachoAutomatico = configuration.GetSection("DespachoAutomatico").Value,
                TipoEntrega = configuration.GetSection("TipoEntrega").Value,
                PorcentajeDeIva = int.Parse(configuration.GetSection("PorcentajeDeIva").Value),
                PorcDescuentoGlob = int.Parse(configuration.GetSection("PorcDescuentoGlob").Value),
                MontoDescuentoLine = int.Parse(configuration.GetSection("MontoDescuentoLine").Value),
                MontoEnTarjetas = orderDto.OrderTotal
            };

            return result;
        }

        private List<PedidosDet> ToPedidosDetList(ICollection<OrderItemDto> orderDto, int erpOrderId)
        {
            IConfigurationSection configuration = _service1._configuration.GetSection("OrderSyncSettings");

            return orderDto.Select(orderItem => new PedidosDet
            {
                NumeroDePedido = erpOrderId,
                Product0 = orderItem.Product.Sku,
                PrecioUnidadVenta = orderItem.UnitPriceExclTax.GetValueOrDefault(),
                MontoIva = (orderItem.UnitPriceInclTax - orderItem.UnitPriceExclTax).GetValueOrDefault(),
                CantidadPedida = orderItem.Quantity.GetValueOrDefault(),
                MontoDescuentoPdet = orderItem.DiscountAmountInclTax.GetValueOrDefault(),
                SubtotalVentas = orderItem.PriceInclTax.GetValueOrDefault(),
                IdEmpresa = configuration.GetSection("IdEmpresa").Value,
                IdSucursal = configuration.GetSection("IdSucursal").Value,
                IdCentroOperativo = configuration.GetSection("IdCentroOperativo").Value,
                CodigoUnidadVenta = configuration.GetSection("CodigoUnidadVenta").Value,
                CantidadDespachada = decimal.Parse(configuration.GetSection("CantidadDespachada").Value),
                FactorConversionUn = int.Parse(configuration.GetSection("FactorConversionUn").Value),
                MontoDescuentoGlob = int.Parse(configuration.GetSection("MontoDescuentoGlob").Value),
                PorcDescuentoLinea = int.Parse(configuration.GetSection("PorcDescuentoLinea").Value),
                PrecioAfectado = int.Parse(configuration.GetSection("PrecioAfectado").Value)
            }).ToList();
        }

        private List<PedidosEnc> GetStardentPedidosEnc()
        {
            using GrupoEstrellaContext db = new GrupoEstrellaContext();

            return db.PedidosEncs.ToList();
        }

        private bool OrderExist(ref List<PedidosEnc> list, string nopCommerceOrderNumber) => list.Any(item => item.NumeroPedidoWeb.ToString() == nopCommerceOrderNumber);

        /// <summary>
        /// Method for synching Orders
        /// </summary>
        public async Task SyncOrders()
        {
            OrdersRootObject nopCommerceOrders = await GetOrdersAsync();
            List<PedidosEnc> stardentOrders = GetStardentPedidosEnc();
            bool hasOccurredAnError = false;
            var stringBuilder = new StringBuilder();

            foreach (OrderDto nopCommerceOrder in nopCommerceOrders.Orders)
            {
                if (!OrderExist(ref stardentOrders, nopCommerceOrder.Id.ToString()))
                {
                    try
                    {
                        PedidosEnc erpOrder = ToPedidosEnc(nopCommerceOrder);
                        int erpOrderId = CreateOrder(erpOrder);
                        CreateOrderDetail(ToPedidosDetList(nopCommerceOrder.OrderItems, erpOrderId));
                        SaveOrderIdErpOrderIdMapping(nopCommerceOrder.Id, erpOrderId);
                    }
                    catch (Exception e)
                    {
                        hasOccurredAnError = true;
                        stringBuilder.AppendLine("\nCould not sync the following orders:\n\nORDER_ID | ERROR_MSG");
                        stringBuilder.AppendLine($"{nopCommerceOrder.Id} | {e.Message}");
                    }
                }
            }

            if (hasOccurredAnError) throw new Exception(stringBuilder.ToString());
        }
    }
}

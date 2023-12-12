using System;

#nullable disable

namespace Nop.Service.GrupoEstrellaSync.Entities
{
    public partial class Cliente
    {
        public int CodigoDeCliente { get; set; }
        public string NombreCliente { get; set; }
        public string EstadoCliente { get; set; }
        public string ContactoCliente { get; set; }
        public string DireccionCliente { get; set; }
        public string DireccionEnvio { get; set; }
        public string TelefonoCliente { get; set; }
        public string TelexOFaxCliente { get; set; }
        public DateTime FechaApertura { get; set; }
        public string NitCliente { get; set; }
        public decimal LimiteCredCliente { get; set; }
        public decimal SaldoCliente { get; set; }
        public string ExentImptoCliente { get; set; }
        public string TipoDeCliente { get; set; }
        public string TpoDespachoCliente { get; set; }
        public int CodigoDepartamento { get; set; }
        public int CodigoDePais { get; set; }
        public int CodigoMunicipio { get; set; }
        public int CodigoTerritorio { get; set; }
        public string CodigoDeClase { get; set; }
        public string NivelPrecio { get; set; }
        public int CodigoDeCondicion { get; set; }
        public string CodigoTipoPago { get; set; }
        public int TotalChequesRech { get; set; }
        public string GiroDeNegocio { get; set; }
        public string NumeroRegistro { get; set; }
        public string CodigoImpuesto { get; set; }
        public string CodigoDeMoneda { get; set; }
        public int? CodigoCobrador { get; set; }
        public string CodigoDeGrupo { get; set; }
        public double? PorcentajeDescuento { get; set; }
        public decimal? SaldoFinanciamiento { get; set; }
        public string CodigoEcc { get; set; }
        public byte[] Timestamp { get; set; }
        public int? CodigoRutaDespacho { get; set; }
        public string CategoriaCliente { get; set; }
        public double? PorcMora { get; set; }
        public string NombreComercial { get; set; }
        public string PreciosConIva { get; set; }
        public string ReferenciaCliente { get; set; }
        public string CtrlIvaPercibido { get; set; }
        public string TpoDespachoParcial { get; set; }
        public string ConsumoInterno { get; set; }
        public string CedulaCliente { get; set; }
        public string DireccionCobro { get; set; }
        public string DireccionEmail { get; set; }
        public string ClaveCliente { get; set; }
        public string ActivoEnWeb { get; set; }
        //public DateTime? FechaNacimiento { get; set; }
    }
}

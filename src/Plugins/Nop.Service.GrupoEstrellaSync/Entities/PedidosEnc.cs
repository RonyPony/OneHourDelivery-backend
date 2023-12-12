using System;

#nullable disable

namespace Nop.Service.GrupoEstrellaSync.Entities
{
    public partial class PedidosEnc
    {
        public string IdEmpresa { get; set; }
        public string IdSucursal { get; set; }
        public string IdCentroOperativo { get; set; }
        public int NumeroDePedido { get; set; }
        public DateTime FechaPedido { get; set; }
        public DateTime FechaOfrecido { get; set; }
        public string UsuarioIngresoPedi { get; set; }
        public DateTime FechaIngresoPedido { get; set; }
        public decimal? SubtotalPedido { get; set; }
        public decimal? MontoFlete { get; set; }
        public decimal? MontoSeguro { get; set; }
        public decimal? MontoDescuentoPedi { get; set; }
        public decimal? IvaPedido { get; set; }
        public decimal? TotalGeneralPedido { get; set; }
        public string EstadoPedido { get; set; }
        public string FleteACargoDe { get; set; }
        public string TipoDeDescuento { get; set; }
        public string NombreAFacturar { get; set; }
        public string NitAFacturar { get; set; }
        public string DireccionFacturar { get; set; }
        public string Observaciones { get; set; }
        public string DocumentoAImprimir { get; set; }
        public double CambMonedLocalPed { get; set; }
        public int CodigoDeCliente { get; set; }
        public int CodigoVendedor { get; set; }
        public int CodigoDeCondicion { get; set; }
        public string CodigoTipoPago { get; set; }
        public int NoDespacho { get; set; }
        public string NivelPrecio { get; set; }
        public double PorcentajeDeIva { get; set; }
        public double PorcDescuentoGlob { get; set; }
        public decimal MontoDescuentoLine { get; set; }
        public string CodigoDeBodega { get; set; }
        public string DespachoAutomatico { get; set; }
        public string Transporte { get; set; }
        public string Nota1 { get; set; }
        public string Nota2 { get; set; }
        public string Nota3 { get; set; }
        public string Nota4 { get; set; }
        public string GiroNegocioAFactu { get; set; }
        public string RegistroAFacturar { get; set; }
        public string ParticipaPromocion { get; set; }
        public string UsoClave { get; set; }
        public string TipoEntrega { get; set; }
        public string OrdenCompraRef { get; set; }
        public double? NumeroCuadre { get; set; }
        public decimal? MontoEfectivo { get; set; }
        public decimal? MontoCheques { get; set; }
        public decimal? MontoEnTarjetas { get; set; }
        public byte[] Timestamp { get; set; }
        public int? NumeroPedidoWeb { get; set; }
        public string CodigoRazon { get; set; }
        public string ObservacionAnula { get; set; }
        public double? TasaDirectaDolar { get; set; }
        public int? FactNumLetras { get; set; }
    }
}

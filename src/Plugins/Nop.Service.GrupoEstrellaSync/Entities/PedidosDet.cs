#nullable disable

namespace Nop.Service.GrupoEstrellaSync.Entities
{
    public partial class PedidosDet
    {
        public string IdEmpresa { get; set; }
        public string IdSucursal { get; set; }
        public string IdCentroOperativo { get; set; }
        public int NumeroDePedido { get; set; }
        public string Product0 { get; set; }
        public string CodigoUnidadVenta { get; set; }
        public decimal CantidadPedida { get; set; }
        public decimal CantidadDespachada { get; set; }
        public decimal PrecioUnidadVenta { get; set; }
        public decimal PrecioAfectado { get; set; }
        public decimal MontoDescuentoPdet { get; set; }
        public decimal FactorConversionUn { get; set; }
        public decimal MontoDescuentoGlob { get; set; }
        public double PorcDescuentoLinea { get; set; }
        public decimal MontoIva { get; set; }
        public decimal SubtotalVentas { get; set; }
        public string UsoClave { get; set; }
        public decimal? PrecioSugerido { get; set; }
        public short? CorrelativoIngreso { get; set; }
        public byte[] Timestamp { get; set; }
        public double? PrecioDeLista { get; set; }
    }
}

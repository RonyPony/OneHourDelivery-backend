using System;

#nullable disable

namespace Nop.Service.GrupoEstrellaSync.Entities
{
    public partial class Lote
    {
        public string Product0 { get; set; }
        public string CodigoBodega { get; set; }
        public string CodigoDeUbicacion { get; set; }
        public string CodigoDeLote { get; set; }
        public string DescripcionLote { get; set; }
        public DateTime FechaIngresoLote { get; set; }
        public DateTime? FecVencimientoLote { get; set; }
        public decimal CantidadReservada { get; set; }
        public decimal CantidadOrdenado { get; set; }
        public decimal CantidadDisponible { get; set; }
        public decimal? PesoDisponible { get; set; }
        public double CostoPepsUeps { get; set; }
        public string IdEmpresaComp { get; set; }
        public string IdSucursalComp { get; set; }
        public string IdCentroComp { get; set; }
        public int? CodigoTipoCompra { get; set; }
        public int? CodigoDeCompra { get; set; }
        public byte[] Timestamp { get; set; }
    }
}

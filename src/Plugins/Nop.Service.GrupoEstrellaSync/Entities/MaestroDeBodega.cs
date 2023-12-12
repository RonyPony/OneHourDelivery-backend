#nullable disable

namespace Nop.Service.GrupoEstrellaSync.Entities
{
    public partial class MaestroDeBodega
    {
        public string CodigoBodega { get; set; }
        public string DescripcionBodega { get; set; }
        public string TipoBodega { get; set; }
        public string NombreEncargado { get; set; }
        public string DireccionBodega { get; set; }
        public string TelefonoBodega { get; set; }
        public int CapacidadVolBodega { get; set; }
        public int CodigoTipoProducto { get; set; }
        public string Facturacion { get; set; }
        public string Planeacion { get; set; }
        public string IdEmpresa { get; set; }
        public string IdSucursal { get; set; }
        public byte[] Timestamp { get; set; }
        public string CodigoCentro { get; set; }
    }
}

#nullable disable

namespace Nop.Service.GrupoEstrellaSync.Entities
{
    public partial class Ubicacione
    {
        public string CodigoBodega { get; set; }
        public string CodigoDeUbicacion { get; set; }
        public string DescripUbicacion { get; set; }
        public int? CapacidadPesoUbica { get; set; }
        public int? CapacidadVolUbica { get; set; }
        public byte[] Timestamp { get; set; }
    }
}

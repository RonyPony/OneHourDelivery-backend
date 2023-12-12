#nullable disable

namespace Nop.Service.GrupoEstrellaSync.Entities
{
    public partial class Marca
    {
        public string CodigoMarca { get; set; }
        public string DescripcionMarca { get; set; }
        public string ValorOmisionMarca { get; set; }
        public byte[] Timestamp { get; set; }
    }
}

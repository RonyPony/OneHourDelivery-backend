#nullable disable

namespace Nop.Service.GrupoEstrellaSync.Entities
{
    public partial class TipoDeProducto
    {
        public int CodigoTipoProducto { get; set; }
        public string DescripcionTipo { get; set; }
        public string ClasificacionTipo { get; set; }
        public string ControlSaldos { get; set; }
        public byte[] Timestamp { get; set; }
        public string CtrlCodCompuesto { get; set; }
    }
}

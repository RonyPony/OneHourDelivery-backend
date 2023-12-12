#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class TableSectionDetail
    {
        public int Id { get; set; }
        public int IdtableSection { get; set; }
        public int Idobject { get; set; }
        public bool IsTable { get; set; }
        public string Name { get; set; }
        public int Xpos { get; set; }
        public int Ypos { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}

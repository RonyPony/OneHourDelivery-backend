#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class FloorPlanObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Xpos { get; set; }
        public int Ypos { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}

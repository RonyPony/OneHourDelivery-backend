#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class Series
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberFrom { get; set; }
        public int NumberTo { get; set; }
        public string Separator { get; set; }
        public bool ByRange { get; set; }
        public int CurrentNumber { get; set; }
        public string Description { get; set; }
        public int SeriesType { get; set; }
    }
}

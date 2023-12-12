#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class Supplyer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Comment { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool Active { get; set; }
        public decimal Credit { get; set; }
        public decimal? Balance { get; set; }
    }
}

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class Table
    {
        public int Id { get; set; }
        public decimal GamePrice { get; set; }
        public int? GameType { get; set; }
        public int TableType { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public bool Locked { get; set; }
        public int PersonQty { get; set; }
        public bool IsBar { get; set; }
        public bool IsGame { get; set; }
        public bool IsOffice { get; set; }
        public int Xpos { get; set; }
        public int Ypos { get; set; }
        public decimal Lenght { get; set; }
        public decimal Width { get; set; }
        public int TableSection { get; set; }
        public bool Active { get; set; }
        public bool Selected { get; set; }
        public decimal RentPrice { get; set; }
    }
}

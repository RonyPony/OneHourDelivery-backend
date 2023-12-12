using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class WarehouseTranfer
    {
        public int Id { get; set; }
        public int ToWareHouse { get; set; }
        public int FromWareHouse { get; set; }
        public DateTime Date { get; set; }
        public string Reference { get; set; }
        public string Comment { get; set; }
    }
}

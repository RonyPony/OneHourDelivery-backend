using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewWarehouseTranfer
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Reference { get; set; }
        public int ToWareHouse { get; set; }
        public string ToWarehouseName { get; set; }
        public int FromWareHouse { get; set; }
        public string FromWarehouseName { get; set; }
        public string Comment { get; set; }
    }
}

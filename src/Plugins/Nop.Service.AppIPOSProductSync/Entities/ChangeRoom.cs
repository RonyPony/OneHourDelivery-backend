using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ChangeRoom
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Order { get; set; }
        public int FromRoom { get; set; }
        public int ToRoom { get; set; }
        public string Remark { get; set; }
        public int RegisteredUser { get; set; }
    }
}

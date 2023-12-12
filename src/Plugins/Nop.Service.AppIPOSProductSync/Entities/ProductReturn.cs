using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ProductReturn
    {
        public int Id { get; set; }
        public int Client { get; set; }
        public int Bill { get; set; }
        public DateTime Date { get; set; }
    }
}

using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class OrderActivesList
    {
        public string Id { get; set; }
        public int OrderId { get; set; }
        public int? ClientId { get; set; }
        public string NameClient { get; set; }
        public string NumberRoom { get; set; }
        public DateTime OpeningDate { get; set; }
    }
}

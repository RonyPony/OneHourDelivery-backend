using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class OrderAnnulled
    {
        public DateTime Date { get; set; }
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int Id { get; set; }
    }
}

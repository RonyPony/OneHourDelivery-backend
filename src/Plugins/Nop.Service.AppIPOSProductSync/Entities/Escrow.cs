using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class Escrow
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public decimal Amount { get; set; }
        public DateTime EscrowDate { get; set; }
        public bool Active { get; set; }
        public int RegisteredUser { get; set; }
        public bool? Annulled { get; set; }
        public DateTime? AnnulledDate { get; set; }
        public int? AnnulledUser { get; set; }
    }
}

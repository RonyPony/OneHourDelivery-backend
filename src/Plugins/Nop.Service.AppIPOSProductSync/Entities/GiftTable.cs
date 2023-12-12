using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class GiftTable
    {
        public int IdgiftTable { get; set; }
        public string NameGiftTable { get; set; }
        public int ClientId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Applied { get; set; }
        public bool Active { get; set; }
    }
}

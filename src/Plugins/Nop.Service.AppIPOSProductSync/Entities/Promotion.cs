using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class Promotion
    {
        public int Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool Active { get; set; }
        public string Name { get; set; }
        public bool IsRecurrent { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

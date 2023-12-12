using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class Season
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool? Active { get; set; }
    }
}

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class Goal
    {
        public int Id { get; set; }
        public int YearGoal { get; set; }
        public int MonthGoal { get; set; }
        public int VendorId { get; set; }
        public int WorkAreaId { get; set; }
        public decimal ProductGoal { get; set; }
        public decimal ServiceGoal { get; set; }
        public decimal MenuGoal { get; set; }
        public decimal RoomGoal { get; set; }
        public bool? Active { get; set; }
    }
}

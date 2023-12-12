#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewGoal
    {
        public int GoalId { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public int WorkAreaId { get; set; }
        public string WorkAreaName { get; set; }
        public int YearGoal { get; set; }
        public int MonthGoal { get; set; }
        public decimal MenuGoal { get; set; }
        public decimal RoomGoal { get; set; }
        public decimal ProductGoal { get; set; }
        public decimal ServiceGoal { get; set; }
        public int MenuReal { get; set; }
        public int RoomReal { get; set; }
        public decimal ProductReal { get; set; }
        public decimal ServiceReal { get; set; }
        public decimal? PercentMenu { get; set; }
        public decimal? PercentRoom { get; set; }
        public decimal? PercentProduct { get; set; }
        public decimal? PercentService { get; set; }
        public decimal? TotalGoal { get; set; }
        public decimal? TotalReal { get; set; }
        public decimal? TotalPercent { get; set; }
    }
}

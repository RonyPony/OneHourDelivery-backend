namespace Nop.Plugin.Api.Compatibility
{
    public sealed class WarehouseScheduleMappingSnapshot
    {
        public int Id { get; set; }

        public int WarehouseId { get; set; }

        public int DayOfWeekId { get; set; }

        public TimeSpan OpeningTime { get; set; }

        public TimeSpan ClosingTime { get; set; }

        public bool IsClosed { get; set; }
    }
}
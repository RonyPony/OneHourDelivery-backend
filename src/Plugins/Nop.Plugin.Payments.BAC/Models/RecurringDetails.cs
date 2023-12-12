namespace Nop.Plugin.Payments.BAC.Models
{
    public sealed class RecurringDetails
    {
        public bool IsRecurring { get; set; }
        public string ExecutionDate { get; set; }
        public string Frequency { get; set; }
        public int NumberOfRecurrences { get; set; }
    }
}

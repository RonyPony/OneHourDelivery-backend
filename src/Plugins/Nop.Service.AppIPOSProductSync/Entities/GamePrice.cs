#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class GamePrice
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string Name { get; set; }
        public decimal PriceMinute { get; set; }
        public decimal PriceHour { get; set; }
        public bool AllowDiscount { get; set; }
        public bool Active { get; set; }
    }
}

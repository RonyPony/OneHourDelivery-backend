#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewTableReservation
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public string TableName { get; set; }
    }
}

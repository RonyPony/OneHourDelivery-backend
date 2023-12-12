#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewWaiterPoint
    {
        public string ParentName { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentCashRegister { get; set; }
    }
}

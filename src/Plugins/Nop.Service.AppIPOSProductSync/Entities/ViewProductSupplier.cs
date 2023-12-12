#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewProductSupplier
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public int ProductId { get; set; }
        public byte[] Picture { get; set; }
        public string Code { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string PrimaryUnit { get; set; }
        public string SecondaryUnit { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
    }
}

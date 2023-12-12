#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ProductReturnDetail
    {
        public int Id { get; set; }
        public int Refound { get; set; }
        public int Product { get; set; }
        public double Quantity { get; set; }
        public double Real { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewListProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public string StrMarca { get; set; }
        public int ProductCategory { get; set; }
        public string Unit { get; set; }
        public bool Locked { get; set; }
        public bool Active { get; set; }
        public string Code { get; set; }
        public string BarCode { get; set; }
        public decimal? Quantity { get; set; }
        public string NameCategoria { get; set; }
        public byte[] ImagenProd { get; set; }
    }
}

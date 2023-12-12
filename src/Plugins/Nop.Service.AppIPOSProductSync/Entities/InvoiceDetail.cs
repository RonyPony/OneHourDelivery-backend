#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class InvoiceDetail
    {
        public int Id { get; set; }
        public int Invoice { get; set; }
        public int Product { get; set; }
        public int Unit { get; set; }
        public decimal Quantity { get; set; }
        public decimal Cost { get; set; }
        public decimal CostCp { get; set; }
        public int TypeTax { get; set; }
        public decimal Tax { get; set; }
        public decimal TaxValue { get; set; }
        public decimal TaxValueCp { get; set; }
        public decimal SubTotal { get; set; }
        public decimal SubTotalCp { get; set; }
    }
}

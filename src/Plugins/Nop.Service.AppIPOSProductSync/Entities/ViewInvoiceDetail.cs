#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewInvoiceDetail
    {
        public int IdInvoiceDetail { get; set; }
        public int IdInvoice { get; set; }
        public int Product { get; set; }
        public string ProductName { get; set; }
        public int Unit { get; set; }
        public string UnitName { get; set; }
        public decimal Quantity { get; set; }
        public decimal Cost { get; set; }
        public decimal CostCp { get; set; }
        public int TipeTax { get; set; }
        public string NameTax { get; set; }
        public decimal TaxPercent { get; set; }
        public decimal TaxValue { get; set; }
        public decimal TaxValueCp { get; set; }
        public decimal SubTotal { get; set; }
        public decimal SubTotalCp { get; set; }
        public string Code { get; set; }
    }
}

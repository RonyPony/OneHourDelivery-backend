using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewRepInvoicedetail
    {
        public int IdInvoice { get; set; }
        public string Reference { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string SupplyerName { get; set; }
        public string WarehouseName { get; set; }
        public string Code { get; set; }
        public string ProductName { get; set; }
        public string UnitName { get; set; }
        public string NameTax { get; set; }
        public decimal Quantity { get; set; }
        public decimal Cost { get; set; }
        public decimal TaxValue { get; set; }
        public decimal SubTotal { get; set; }
    }
}

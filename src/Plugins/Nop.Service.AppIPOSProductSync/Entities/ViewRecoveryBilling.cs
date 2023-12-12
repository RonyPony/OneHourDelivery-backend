using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewRecoveryBilling
    {
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public int IdBill { get; set; }
        public string BillBumber { get; set; }
        public DateTime BillDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public int ReceiptId { get; set; }
        public string ReceiptNumber { get; set; }
        public DateTime ReceiptDate { get; set; }
        public int? Client { get; set; }
        public string NameClient { get; set; }
    }
}

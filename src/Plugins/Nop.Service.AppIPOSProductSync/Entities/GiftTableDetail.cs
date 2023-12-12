using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class GiftTableDetail
    {
        public int IdgiftTableDetail { get; set; }
        public int IdgiftTable { get; set; }
        public int Idproduct { get; set; }
        public int Idunit { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public decimal Quantity { get; set; }
    }
}

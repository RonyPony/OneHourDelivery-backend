using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class CorrectionWarehouse
    {
        public int Id { get; set; }
        public int Warehouse { get; set; }
        public DateTime Date { get; set; }
        public int CorrectionReason { get; set; }
        public string Reference { get; set; }
        public string Comment { get; set; }
    }
}

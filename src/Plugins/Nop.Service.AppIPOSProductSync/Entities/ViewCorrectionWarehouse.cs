using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewCorrectionWarehouse
    {
        public int IdCorrection { get; set; }
        public DateTime CorrectionDate { get; set; }
        public string Reference { get; set; }
        public int Warehouse { get; set; }
        public string WarehouseName { get; set; }
        public string Comment { get; set; }
    }
}

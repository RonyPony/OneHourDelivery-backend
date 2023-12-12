using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewKadex
    {
        public int Id { get; set; }
        public int Product { get; set; }
        public string CodeProduct { get; set; }
        public string NameProduct { get; set; }
        public int Unit { get; set; }
        public string NameUnit { get; set; }
        public int Operation { get; set; }
        public int ReferenceDocumentId { get; set; }
        public string ReferenceDocument { get; set; }
        public DateTime Date { get; set; }
        public int WarehouseHome { get; set; }
        public int WarehouseTarget { get; set; }
        public decimal Input { get; set; }
        public decimal Output { get; set; }
        public decimal QuantityInitial { get; set; }
        public decimal QuantityFinal { get; set; }
        public decimal CostInitial { get; set; }
        public decimal CostUnitOperation { get; set; }
        public decimal CostFinal { get; set; }
        public decimal? CostoUnitFinal { get; set; }
        public decimal CostUnitActual { get; set; }
    }
}

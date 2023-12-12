using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewInformePosVenta
    {
        public string NombreDist { get; set; }
        public string Cedula { get; set; }
        public string NameClient { get; set; }
        public string Address { get; set; }
        public string Departamento { get; set; }
        public string Ciudad { get; set; }
        public string CodPostal { get; set; }
        public string Pais { get; set; }
        public string ProductCode { get; set; }
        public string NameProduct { get; set; }
        public DateTime? Fecha { get; set; }
        public decimal Cantidad { get; set; }
        public string NameUnit { get; set; }
        public decimal Cost { get; set; }
        public string CantInv { get; set; }
        public string NameMoneda { get; set; }
        public string Comentarios { get; set; }
        public int? TrademarkId { get; set; }
        public string ProductMarcaName { get; set; }
    }
}

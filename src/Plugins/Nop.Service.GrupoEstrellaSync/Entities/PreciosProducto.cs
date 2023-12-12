using System;
using System.Collections.Generic;

#nullable disable

namespace Nop.Service.GrupoEstrellaSync.Entities
{
    public partial class PreciosProducto
    {
        public string Product0 { get; set; }
        public string CodigoUnidadVenta { get; set; }
        public string NivelPrecio { get; set; }
        public string CodigoDeMoneda { get; set; }
        public int SecuenciaPrecio { get; set; }
        public string PrecioEnFuncion { get; set; }
        public decimal? CantidadInicial { get; set; }
        public decimal? CantidadFinal { get; set; }
        public DateTime? FechaVigenciaI { get; set; }
        public DateTime? FechaVigenciaF { get; set; }
        public decimal PrecioMaximo { get; set; }
        public decimal PrecioMinimo { get; set; }
        public double? MargenPrecioMin { get; set; }
        public double? MargenPrecioMax { get; set; }
        public decimal? PrecioSugerido { get; set; }
        public string ValorOMargen { get; set; }
        public byte[] Timestamp { get; set; }
    }
}

using System;

#nullable disable

namespace Nop.Service.GrupoEstrellaSync.Entities
{
    public partial class MaestroProducto
    {
        public string Product0 { get; set; }
        public string DescripcionProd { get; set; }
        public string DescripLargaProd { get; set; }
        public string TipoProducto { get; set; }
        public double CostoUnitario { get; set; }
        public double CostoAlterno { get; set; }
        public decimal ExistenciaMaxima { get; set; }
        public decimal PuntoDeReorden { get; set; }
        public decimal ExistenciaMinima { get; set; }
        public string EstadoProducto { get; set; }
        public int? DiaConsumoPromedio { get; set; }
        public int DiaReabastecimiento { get; set; }
        public string PagaImpuestos { get; set; }
        public string SujetoADescuento { get; set; }
        public decimal CantMinimaPedido { get; set; }
        public decimal CantMultiploPedido { get; set; }
        public string CodigoAbc { get; set; }
        public int CodigoTipoProducto { get; set; }
        public string TipoCostoAValuar { get; set; }
        public string IndicaProcedencia { get; set; }
        public string CodigoReferencia { get; set; }
        public string CodigoArancelario { get; set; }
        public string ClaseProducto { get; set; }
        public string CodigoMarca { get; set; }
        public string Familia { get; set; }
        public string Subfamilia { get; set; }
        public string CalculoDeReorden { get; set; }
        public double FactorDeRotacion { get; set; }
        public string ManejaLotes { get; set; }
        public string GrupoConteo { get; set; }
        public DateTime? FechaUltimoConteo { get; set; }
        public string TipoCompetencia { get; set; }
        public double? PorcentajeDescuento { get; set; }
        public DateTime? FechaApertura { get; set; }
        public DateTime? FechaUltimaEntrada { get; set; }
        public DateTime? FechaUltimaSalida { get; set; }
        public decimal? MaximoAVender { get; set; }
        public string TipoDeVenta { get; set; }
        public string CodigoAnterior { get; set; }
        public string CodigoSucesor { get; set; }
        public string CalculaPrecioCosto { get; set; }
        public string CodigoEstructuraCi { get; set; }
        public string CtrlIvaPercibido { get; set; }
        public string CodigoCompuesto { get; set; }
        public byte[] Timestamp { get; set; }
        public double? PorcMaxPeso { get; set; }
        public double? PorcMinPeso { get; set; }
        public string ActivoEnWeb { get; set; }
        public string ParticipaPlaneacion { get; set; }
        public string OrigenProductoPlan { get; set; }
        public string TipoExistencia { get; set; }
        public string FormaDistribucion { get; set; }
        public string DemAntesBarrera { get; set; }
        public string DemDespuesBarrera { get; set; }
        public string TipoExistenciaMin { get; set; }
        public int? Barrera { get; set; }
        public string Subsubfamilia { get; set; }
    }
}

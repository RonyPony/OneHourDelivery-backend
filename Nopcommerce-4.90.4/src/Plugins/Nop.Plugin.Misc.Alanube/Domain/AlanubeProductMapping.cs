using Nop.Core;

namespace Nop.Plugin.Misc.Alanube.Domain;

public sealed class AlanubeProductMapping : BaseEntity
{
    public int ProductId { get; set; }
    public string GoodsServiceCode { get; set; }
    public string MeasurementUnitCode { get; set; }
    public string TaxCode { get; set; }
    public string DescriptionOverride { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime UpdatedOnUtc { get; set; }
}

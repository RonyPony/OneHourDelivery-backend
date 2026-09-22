using Nop.Core;

namespace Nop.Plugin.Misc.Alanube.Domain;

/// <summary>Stores an independent, never-reused fiscal numbering series.</summary>
public sealed class AlanubeFiscalSequence : BaseEntity
{
    public string CompanyId { get; set; }
    public string OfficeId { get; set; }
    public string BillingPoint { get; set; }
    public string DocumentType { get; set; }
    public long LastNumber { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime UpdatedOnUtc { get; set; }
}

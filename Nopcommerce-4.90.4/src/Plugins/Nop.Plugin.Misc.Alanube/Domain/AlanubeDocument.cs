using Nop.Core;

namespace Nop.Plugin.Misc.Alanube.Domain;

public sealed class AlanubeDocument : BaseEntity
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public int StoreId { get; set; }

    public string AlanubeId { get; set; }
    public string CompanyId { get; set; }
    public string OfficeId { get; set; }

    public ElectronicDocumentType DocumentType { get; set; }
    public ElectronicDocumentStatus DocumentStatus { get; set; }
    public int Version { get; set; }

    public string Cufe { get; set; }
    public string DocumentNumber { get; set; }
    public string CurrencyCode { get; set; }

    public decimal Subtotal { get; set; }
    public decimal Discount { get; set; }
    public decimal Tax { get; set; }
    public decimal Total { get; set; }

    public string ErrorCode { get; set; }
    public string ErrorMessage { get; set; }

    public DateTime? IssueDateUtc { get; set; }
    public DateTime? AuthorizedDateUtc { get; set; }
    public DateTime? CancelledDateUtc { get; set; }

    public int RetryCount { get; set; }
    public DateTime? LastRetryUtc { get; set; }

    public DateTime CreatedOnUtc { get; set; }
    public DateTime UpdatedOnUtc { get; set; }
}

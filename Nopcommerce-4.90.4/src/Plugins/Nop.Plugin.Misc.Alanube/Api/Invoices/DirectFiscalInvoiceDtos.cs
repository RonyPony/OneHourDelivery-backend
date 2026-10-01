using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nop.Plugin.Misc.Alanube.Api.Invoices;

public sealed class DirectFiscalInvoiceRequest
{
    public DirectFiscalInvoiceIdDoc IdDoc { get; set; }
    public DirectFiscalInvoiceSender Sender { get; set; }
    public DirectFiscalInvoiceBuyer Buyer { get; set; }
    public DirectFiscalInvoiceTotals Totals { get; set; }
    public IList<DirectFiscalInvoiceItem> ItemDetails { get; set; } = new List<DirectFiscalInvoiceItem>();
}

public sealed class DirectFiscalInvoiceIdDoc
{
    public string Encf { get; set; }
    public int IncomeType { get; set; } = 1;
    public int PaymentType { get; set; } = 1;
    public string SequenceDueDate { get; set; }
    public int TaxAmountIndicator { get; set; }
}

public sealed class DirectFiscalInvoiceSender
{
    public string Rnc { get; set; }
    public string CompanyName { get; set; }
    public string Tradename { get; set; }
    public string Address { get; set; }
    public string Province { get; set; }
    public string Municipality { get; set; }
    public string StampDate { get; set; }
}

public sealed class DirectFiscalInvoiceBuyer
{
    public string Rnc { get; set; }
    public string CompanyName { get; set; }
}

public sealed class DirectFiscalInvoiceTotals
{
    public decimal TotalTaxedAmount { get; set; }
    public decimal I1AmountTaxed { get; set; }
    public decimal ItbisS1 { get; set; }
    public decimal ItbisTotal { get; set; }
    public decimal Itbis1Total { get; set; }
    public decimal TotalAmount { get; set; }
}

public sealed class DirectFiscalInvoiceItem
{
    public int LineNumber { get; set; }
    public int BillingIndicator { get; set; } = 1;
    public string ItemName { get; set; }
    public string ItemDescription { get; set; }
    public int GoodServiceIndicator { get; set; } = 1;
    public int QuantityItem { get; set; }
    public int UnitMeasure { get; set; } = 62;
    public decimal UnitPriceItem { get; set; }
    public decimal ItemAmount { get; set; }
}

public sealed class DirectFiscalInvoiceResponse
{
    [JsonExtensionData]
    public Dictionary<string, JsonElement> Data { get; set; } = new();
}

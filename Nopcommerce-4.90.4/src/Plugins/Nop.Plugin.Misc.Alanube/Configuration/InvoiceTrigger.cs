namespace Nop.Plugin.Misc.Alanube.Configuration;

/// <summary>
/// Defines the event or action that triggers invoice processing.
/// </summary>
public enum InvoiceTrigger
{
    /// <summary>
    /// Invoice processing is started manually.
    /// </summary>
    Manual = 1,

    /// <summary>
    /// Invoice processing starts when an order is placed.
    /// </summary>
    OrderPlaced = 2,

    /// <summary>
    /// Invoice processing starts when payment is paid.
    /// </summary>
    PaymentPaid = 3,

    /// <summary>
    /// Invoice processing starts when an order enters processing.
    /// </summary>
    Processing = 4,

    /// <summary>
    /// Invoice processing starts when an order is complete.
    /// </summary>
    Complete = 5
}

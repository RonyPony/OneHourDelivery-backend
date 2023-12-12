namespace Nop.Plugin.Synchronizers.SAPCustomers.Models
{
    /// <summary>
    /// Represents the model class for mapping a SAP customer.
    /// </summary>
    public sealed class SapCustomerModel
    {
        /// <summary>
        /// Represents the card's code.
        /// </summary>
        public string CardCode { get; set; }

        /// <summary>
        /// Represents the card's name.
        /// </summary>
        public string CardName { get; set; }

        /// <summary>
        /// Represents the group's code.
        /// </summary>
        public int GroupCode { get; set; }

        /// <summary>
        /// Represents the federal's tax identification.
        /// </summary>
        public string FederalTaxID { get; set; }

        /// <summary>
        /// Represents the card's type.
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// Represents the balance amount.
        /// </summary>
        public decimal CurrentAccountBalance { get; set; }

        /// <summary>
        /// Represents open delivery account balance field.
        /// </summary>
        public decimal OpenDeliveryAccountBalance { get; set; }

        /// <summary>
        /// Represents the open order's balance
        /// </summary>
        public decimal OpenOrdersBalance { get; set; }

        /// <summary>
        /// Represents Currency field.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Represents the first phone field.
        /// </summary>
        public string Phone1 { get; set; }

        /// <summary>
        /// Represents the second phone field.
        /// </summary>
        public string Phone2 { get; set; }

        /// <summary>
        /// Represents Email field.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Represents website field.
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// Represents Currency field.
        /// </summary>
        public int? ShippingType { get; set; }

        /// <summary>
        /// Represents the business partner's type.
        /// </summary>
        public string CompanyPrivate { get; set; }

        /// <summary>
        /// Represents the payment's terms.
        /// </summary>
        public int PayTermsGrpCode { get; set; }

        /// <summary>
        /// Represents the price list field.
        /// </summary>
        public int PriceListNum { get; set; }

        /// <summary>
        /// Represents the credit limit field.
        /// </summary>
        public decimal CreditLimit { get; set; }

        /// <summary>
        /// Represents the total discount field.
        /// </summary>
        public decimal DiscountPercent { get; set; }

        /// <summary>
        /// Represents the effective discount's Groups field.
        /// </summary>
        public string EffectiveDiscount { get; set; }

        /// <summary>
        /// Represents the VatGroup field.
        /// </summary>
        public string VatGroup { get; set; }

        /// <summary>
        /// Represents the VatGroup field.
        /// </summary>
        public string FreeText { get; set; }

    }
}

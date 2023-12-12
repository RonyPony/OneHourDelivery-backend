using System.Collections.Generic;

namespace Nop.Plugin.Synchronizers.SAPOrders.Models
{
    /// <summary>
    /// Model used to save orders to the SAP API.
    /// </summary>
    public sealed class SapOrderRequestModel
    {
        /// <summary>
        /// Represents the Customer's CardCode for the order.
        /// </summary>
        public string CardCode { get; set; }

        /// <summary>
        /// Represents the Order's NumAtCard.
        /// </summary>
        public string NumAtCard { get; set; }

        /// <summary>
        /// Represents the Order's PostingDate.
        /// </summary>
        public string PostingDate { get; set; }

        /// <summary>
        /// Represents the Order's DeliveryDate.
        /// </summary>
        public string DeliveryDate { get; set; }

        /// <summary>
        /// Represents the Order's DocumentDate.
        /// </summary>
        public string DocumentDate { get; set; }

        /// <summary>
        /// Represents the Order's PaymentTerms.
        /// </summary>
        public int PaymentTerms { get; set; }

        /// <summary>
        /// Represents the Order's ShipType.
        /// </summary>
        public int ShipType { get; set; }

        /// <summary>
        /// Represents the Order's Remarks.
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// Represents the Order's StreetS.
        /// </summary>
        public string StreetS { get; set; }

        /// <summary>
        /// Represents the Order's BlockS.
        /// </summary>
        public string BlockS { get; set; }

        /// <summary>
        /// Represents the Order's CityS.
        /// </summary>
        public string CityS { get; set; }

        /// <summary>
        /// Represents the Order's ZipCodeS.
        /// </summary>
        public string ZipCodeS { get; set; }

        /// <summary>
        /// Represents the Order's CountyS.
        /// </summary>
        public string CountyS { get; set; }

        /// <summary>
        /// Represents the Order's StateS.
        /// </summary>
        public string StateS { get; set; }

        /// <summary>
        /// Represents the Order's CountryS.
        /// </summary>
        public string CountryS { get; set; }

        /// <summary>
        /// Represents the Order's Freight.
        /// </summary>
        public decimal Freight { get; set; }

        /// <summary>
        /// Represents a list of OrderItems for this order.
        /// </summary>
        public List<SapOrderProductModel> Rows { get; set; }
    }
}

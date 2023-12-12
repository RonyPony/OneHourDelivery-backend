using Nop.Plugin.Payments.CardNet.Helpers;
using Nop.Services.Directory;

namespace Nop.Plugin.Payments.CardNet.Extensions
{
    public static class CardNetExtensions
    {
        /// <summary>
        /// Formats decimal amount as CardNet amount (the last two digits are decimal digits).
        /// </summary>
        /// <param name="amount">The amount to format</param>
        /// <returns></returns>
        public static int FormatAsCardNetAmount(this decimal amount)
        {
            string total = $"{amount:0.00}".Replace(".", "");

            return int.Parse(total);
        }

        /// <summary>
        /// Converts a decimal amount to CardNet's default currency
        /// </summary>
        /// <param name="amount">The amount to convert</param>
        /// <param name="currencyService">Implementation of <see cref="ICurrencyService"/></param>
        /// <returns></returns>
        public static decimal ToDefaultCardNetCurrency(this decimal amount, ICurrencyService currencyService)
        {
            decimal result = currencyService.ConvertFromPrimaryStoreCurrency(amount, currencyService.GetCurrencyByCode(CardNetDefaults.DefaultCurrencyCode));

            return result;
        }
    }
}

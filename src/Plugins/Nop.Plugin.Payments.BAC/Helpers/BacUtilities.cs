using System;
using System.Security.Cryptography;
using System.Text;

namespace Nop.Plugin.Payments.BAC.Helpers
{
    /// <summary>
    /// Represents the class that provides some utilities needed for working with the BAC Payment method.
    /// </summary>
    public sealed class BacUtilities
    {
        /// <summary>
        /// Generates the signature with that will be used for requesting the token to the BAC.
        /// </summary>
        /// <param name="bacSettings">An instance of <see cref="BacSettings"/> class with all the configuration needed.</param>
        /// <param name="orderNumber">Represents the working order number.</param>
        /// <param name="amount">Represents the working order amount.</param>
        /// <returns>The corresponding signature.</returns>
        public static string GenerateSignature(BacSettings bacSettings, string orderNumber, decimal amount)
        {
            if (bacSettings.SignatureMethod != DefaultsInfo.DefaultSignatureEncryptionMethod)
            {
                throw new Exception($"The encryption method {bacSettings.SignatureMethod} is not supported at this moment.");
            }

            string source = $"{bacSettings.MerchantPassword}{bacSettings.MerchantId}{bacSettings.AcquirerId}{orderNumber}{FormatAmount(amount).Trim()}{bacSettings.Currency}";

            SHA1CryptoServiceProvider provider = new SHA1CryptoServiceProvider();

            byte[] bytearray = Encoding.UTF8.GetBytes(source);
            byte[] buffer = provider.ComputeHash(bytearray);

            string hashValue = Convert.ToBase64String(buffer);

            return hashValue;
        }

        /// <summary>
        /// Formats the total amount according to the BAC needs.
        /// </summary>
        /// <param name="amount">The amount to be formatted.</param>
        /// <returns></returns>
        public static string FormatAmount(decimal amount)
        {
            double  factor = Math.Pow(10, 2);

            return Math.Round(amount * (decimal)factor, 0).ToString().PadLeft(12, '0');
        }
    }
}

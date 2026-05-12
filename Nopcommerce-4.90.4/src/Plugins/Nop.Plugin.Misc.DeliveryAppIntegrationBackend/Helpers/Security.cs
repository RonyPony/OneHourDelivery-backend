using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers
{
    /// <summary>
    /// Encrypt signed_field_names data with a secret key for send to CyberSource Payment Page.
    /// </summary>
    public class Security
    {
        /// <summary>
        /// Signs the fields of the form that will be sent to Cybersource's payment page.
        /// </summary>
        /// <param name="paramsArray">Parameters to be signed.</param>
        /// <param name="secretKey">Secret key provided in <see cref="CyberSourcePaymentSettings"/></param>
        /// <returns>A string encripted with the secret key</returns>
        public static string Sign(IDictionary<string, string> paramsArray, string secretKey)
            => Sign(BuildDataToSign(paramsArray), secretKey);

        private static string Sign(string data, string secretKey)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(secretKey);

            HMACSHA256 hmacsha256 = new HMACSHA256(keyByte);
            byte[] messageBytes = encoding.GetBytes(data);
            return Convert.ToBase64String(hmacsha256.ComputeHash(messageBytes));
        }

        private static string BuildDataToSign(IDictionary<string, string> paramsArray)
        {
            string[] signedFieldNames = paramsArray["signed_field_names"].Split(',');
            IList<string> dataToSign = new List<string>();

            foreach (string signedFieldName in signedFieldNames)
            {
                dataToSign.Add(signedFieldName + "=" + paramsArray[signedFieldName]);
            }

            return CommaSeparate(dataToSign);
        }

        private static string CommaSeparate(IList<string> dataToSign)
        {
            return string.Join(",", dataToSign);
        }
    }
}

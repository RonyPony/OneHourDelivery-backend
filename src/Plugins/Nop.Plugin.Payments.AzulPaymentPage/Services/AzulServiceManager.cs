using Microsoft.AspNetCore.Http;
using Nop.Core;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Payments.AzulPaymentPage.Helpers;
using Nop.Services.Logging;
using System;
using Nop.Data;
using Nop.Plugin.Payments.AzulPaymentPage.Domains;

namespace Nop.Plugin.Payments.AzulPaymentPage.Services
{
    /// <summary>
    /// Represents the plugin service manager
    /// </summary>
    public sealed class AzulServiceManager
    {
        private readonly IRepository<AzulPaymentTransactionLog> _transactionLogRepository;

        /// <summary>
        /// Initializes a new instance of AzulServiceManager class.
        /// </summary>
        /// <param name="transactionLogRepository">an implementation of <see cref="IRepository{AzulPaymentTransactionLog}"/></param>
        public AzulServiceManager(IRepository<AzulPaymentTransactionLog> transactionLogRepository)
        {
            _transactionLogRepository = transactionLogRepository;
        }

        /// <summary>
        /// Check whether the plugin is configured
        /// </summary>
        /// <param name="settings">Plugin settings</param>
        /// <returns>Result</returns>
        public bool IsConfigured(AzulPaymentPageSettings settings)
        {
            //client id and url or alternative url are required to request services
            return !string.IsNullOrWhiteSpace(settings?.MerchantId) &&
                   (!string.IsNullOrWhiteSpace(settings?.Url) || !string.IsNullOrWhiteSpace(settings?.AlternativeUrl)) &&
                   !string.IsNullOrWhiteSpace(settings?.AuthKey) &&
                   !string.IsNullOrWhiteSpace(settings?.MerchantName) &&
                   !string.IsNullOrWhiteSpace(settings?.CurrencyCode) &&
                   !string.IsNullOrWhiteSpace(settings?.ApprovedUrl) &&
                   !string.IsNullOrWhiteSpace(settings?.CancelUrl) &&
                   !string.IsNullOrWhiteSpace(settings?.DeclinedUrl);
        }

        /// <summary>
        /// Logs an entry for AZUL transaction log
        /// </summary>
        /// <param name="logEntry">Transaction log entry</param>
        public void Log(AzulPaymentTransactionLog logEntry)
        {
            if (logEntry == null)
            {
                throw new ArgumentNullException(nameof(logEntry));
            }

            _transactionLogRepository.Insert(logEntry);
        }
    }
}

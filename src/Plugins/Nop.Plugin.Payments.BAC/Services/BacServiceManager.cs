using Nop.Data;
using Nop.Plugin.Payments.BAC.Domains;
using System;

namespace Nop.Plugin.Payments.BAC.Services
{
    /// <summary>
    /// Represents the plugin service manager
    /// </summary>
    public sealed class BacServiceManager
    {
        private readonly IRepository<BacTransactionLog> _transactionLogRepository;

        /// <summary>
        /// Initializes a new instance of BacServiceManager class with the values indicated as parameters.
        /// </summary>
        /// <param name="transactionLogRepository">An implementation of <see cref="IRepository{BacTransactionLog}"/></param>
        public BacServiceManager(IRepository<BacTransactionLog> transactionLogRepository) => _transactionLogRepository = transactionLogRepository;

        /// <summary>
        /// Check whether the plugin is configured
        /// </summary>
        /// <param name="settings">Plugin settings</param>
        /// <returns>Result</returns>
        public bool IsConfigured(BacSettings settings)
        {
            //client id and url or alternative url are required to request services
            return !string.IsNullOrWhiteSpace(settings?.MerchantId) &&
                   !string.IsNullOrWhiteSpace(settings?.AcquirerId) &&
                   !string.IsNullOrWhiteSpace(settings?.CardHolderResponseUrl) &&
                   !string.IsNullOrWhiteSpace(settings?.GatewayUrl) &&
                   !string.IsNullOrWhiteSpace(settings?.HostedPageUrl) &&
                   !string.IsNullOrWhiteSpace(settings?.MerchantId) &&
                   !string.IsNullOrWhiteSpace(settings?.MerchantPassword) &&
                   !string.IsNullOrWhiteSpace(settings?.SignatureMethod) &&
                   settings?.Currency >= 0 &&
                   settings?.CurrencyExponent >= 0;
        }

        /// <summary>
        /// Logs an entry for BAC transaction log
        /// </summary>
        /// <param name="logEntry">Transaction log entry</param>
        public void Log(BacTransactionLog logEntry)
        {
            if (logEntry == null)
            {
                throw new ArgumentNullException(nameof(logEntry));
            }

            _transactionLogRepository.Insert(logEntry);
        }
    }
}

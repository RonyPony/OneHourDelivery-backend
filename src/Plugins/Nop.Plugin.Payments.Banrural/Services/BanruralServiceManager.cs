using Nop.Core;
using Nop.Services.Logging;
using System;
using Nop.Data;
using Nop.Plugin.Payments.Banrural.Models;

namespace Nop.Plugin.Payments.Banrural.Services
{
    /// <summary>
    /// Represents the plugin service manager
    /// </summary>
    public sealed class BanruralServiceManager
    {
        #region Fields

        private readonly IRepository<BanruralTransactionLog> _transactionLogRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of BanruralServiceManager class.
        /// </summary>
        /// <param name="logger">An implementation of <see cref="ILogger"/></param>
        /// <param name="workContext">An implementation of <see cref="IWorkContext"/></param>
        /// <param name="transactionLogRepository">An implementation of <see cref="BanruralTransactionLog"/> repository.</param>
        public BanruralServiceManager(IRepository<BanruralTransactionLog> transactionLogRepository)
        {
            _transactionLogRepository = transactionLogRepository;
        }

        #endregion

        #region Methods
        
        /// <summary>
        /// Logs an entry to the Banrural transaction log.
        /// </summary>
        /// <param name="logEntry">Entry to be logged.</param>
        public void Log(BanruralTransactionLog logEntry)
        {
            if (logEntry == null)
                throw new ArgumentNullException(nameof(logEntry));

            _transactionLogRepository.Insert(logEntry);
        }

        #endregion
    }
}
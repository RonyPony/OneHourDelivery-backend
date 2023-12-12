using Nop.Data;
using Nop.Plugin.Synchronizers.CommonERPTables.Domains;
using System;
using System.Linq;

namespace Nop.Plugin.Synchronizers.CommonERPTables.Services
{
    /// <summary>
    /// Represents the plugin service manager responsible for handling the Order mapping request.
    /// </summary>
    public sealed class ErpOrderServiceManager
    {
        private readonly IRepository<ErpOrdersNopCommerceOrdersMapping> _erpOrdersNopCommerceOrdersRepository;

        /// <summary>
        /// Initializes a new instance of ErpCommonTablesServiceManager class with the values indicated as parameters.
        /// </summary>
        /// <param name="erpOrdersNopCommerceOrdersRepository">An implementation of <see cref="IRepository{ErpOrdersNopCommerceOrdersMapping}"/></param>
        public ErpOrderServiceManager(IRepository<ErpOrdersNopCommerceOrdersMapping> erpOrdersNopCommerceOrdersRepository)
            => _erpOrdersNopCommerceOrdersRepository = erpOrdersNopCommerceOrdersRepository;

        /// <summary>
        /// Saves an entry for the ERP-nopCommerce order mapping
        /// </summary>
        /// <param name="erpOrdersNopCommerceOrdersMapping">ERP-nopCommerce order entry</param>
        public void SaveErpOrderReference(ErpOrdersNopCommerceOrdersMapping erpOrdersNopCommerceOrdersMapping)
        {
            if (_erpOrdersNopCommerceOrdersRepository == null)
            {
                throw new ArgumentNullException(nameof(_erpOrdersNopCommerceOrdersRepository));
            }

            if (erpOrdersNopCommerceOrdersMapping == null)
            {
                throw new ArgumentNullException(nameof(erpOrdersNopCommerceOrdersMapping));
            }

            _erpOrdersNopCommerceOrdersRepository.Insert(erpOrdersNopCommerceOrdersMapping);
        }

        /// <summary>
        /// Identifies whether there exists or not
        /// </summary>
        /// <param name="nopCommerceOrderId">NopCommerce Order Id</param>
        /// <returns></returns>
        public bool OrderExists(int nopCommerceOrderId)
        {
            if (nopCommerceOrderId == 0)
            {
                throw new ArgumentNullException("You must provide a valid NopCommerce order ID.");
            }

            return _erpOrdersNopCommerceOrdersRepository.Table.Any(orderMapping => orderMapping.OrderId == nopCommerceOrderId);
        }
    }
}

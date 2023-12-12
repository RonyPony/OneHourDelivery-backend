using Nop.Data;
using Nop.Plugin.Synchronizers.CommonERPTables.Domains;
using System;
using System.Linq;

namespace Nop.Plugin.Synchronizers.CommonERPTables.Services
{
    /// <summary>
    /// Represents the plugin service manager responsible for handling the Customer mapping request.
    /// </summary>
    public sealed class ErpCustomerServiceManager
    {
        private readonly IRepository<ErpCustomersNopCommerceCustomersMapping> _erpCustomersNopCommerceCustomersRepository;

        /// <summary>
        /// Initializes a new instance of ErpCommonTablesServiceManager class with the values indicated as parameters.
        /// </summary>
        /// <param name="erpCustomersNopCommerceCustomersRepository">An implementation of <see cref="IRepository{ErpCustomersNopCommerceCustomersMapping}"/></param>
        public ErpCustomerServiceManager(IRepository<ErpCustomersNopCommerceCustomersMapping> erpCustomersNopCommerceCustomersRepository)
            => _erpCustomersNopCommerceCustomersRepository = erpCustomersNopCommerceCustomersRepository;

        /// <summary>
        /// Saves an entry for the ERP-nopCommerce customer mapping
        /// </summary>
        /// <param name="erpCustomersNopCommerceCustomersMapping">ERP-nopCommerce customer entry</param>
        public void SaveErpCustomerReference(ErpCustomersNopCommerceCustomersMapping erpCustomersNopCommerceCustomersMapping)
        {
            if (_erpCustomersNopCommerceCustomersRepository == null)
            {
                throw new ArgumentNullException(nameof(_erpCustomersNopCommerceCustomersRepository));
            }

            if (erpCustomersNopCommerceCustomersMapping == null)
            {
                throw new ArgumentNullException(nameof(erpCustomersNopCommerceCustomersMapping));
            }

            _erpCustomersNopCommerceCustomersRepository.Insert(erpCustomersNopCommerceCustomersMapping);
        }

        /// <summary>
        /// Gets ERP customer code by NopCommerce customer Id
        /// </summary>
        /// <param name="nopCommerceCustomerId">NopCommerce customer id to look for</param>
        public string GetCustomerErpCodeByNopCommerceId(int nopCommerceCustomerId)
        {
            if (nopCommerceCustomerId <= 0)
            {
                throw new ArgumentNullException(nameof(_erpCustomersNopCommerceCustomersRepository));
            }

            ErpCustomersNopCommerceCustomersMapping mapping =
                _erpCustomersNopCommerceCustomersRepository.Table.FirstOrDefault(customerMapping =>
                    customerMapping.CustomerId == nopCommerceCustomerId);

            return mapping?.ErpCustomerId;
        }
    }
}

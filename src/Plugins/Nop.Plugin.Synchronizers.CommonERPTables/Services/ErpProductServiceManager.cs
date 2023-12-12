using Nop.Data;
using Nop.Plugin.Synchronizers.CommonERPTables.Domains;
using System;
using System.Linq;

namespace Nop.Plugin.Synchronizers.CommonERPTables.Services
{
    /// <summary>
    /// Represents the plugin service manager responsible for handling the Products mapping request.
    /// </summary>
    public sealed class ErpProductServiceManager
    {
        private readonly IRepository<ErpProductsNopCommerceProductsMapping> _erpProductNopCommerceCustomersRepository;

        /// <summary>
        /// Initializes a new instance of ErpProductServiceManager class with the values indicated as parameters.
        /// </summary>
        /// <param name="erpProductNopCommerceCustomersRepository">An implementation of <see cref="IRepository{ErpProductsNopCommerceProductsMapping}"/></param>
        public ErpProductServiceManager(IRepository<ErpProductsNopCommerceProductsMapping> erpProductNopCommerceCustomersRepository) 
            => _erpProductNopCommerceCustomersRepository = erpProductNopCommerceCustomersRepository;

        /// <summary>
        /// Saves an entry for the ERP-nopCommerce product mapping
        /// </summary>
        /// <param name="erpProductsNopCommerceProductsMapping">ERP-nopCommerce customer entry</param>
        public void SaveErpCustomerReference(ErpProductsNopCommerceProductsMapping erpProductsNopCommerceProductsMapping)
        {
            if (_erpProductNopCommerceCustomersRepository == null)
            {
                throw new ArgumentNullException(nameof(_erpProductNopCommerceCustomersRepository));
            }

            if (erpProductsNopCommerceProductsMapping == null)
            {
                throw new ArgumentNullException(nameof(erpProductsNopCommerceProductsMapping));
            }

            _erpProductNopCommerceCustomersRepository.Insert(erpProductsNopCommerceProductsMapping);
        }

        /// <summary>
        /// Gets an entry for the ERP-nopCommerce products mapping by the NopCommerce product id.
        /// </summary>
        /// <param name="productId">The product id that you want the mapping information.</param>
        public ErpProductsNopCommerceProductsMapping GetByProductId(int productId)
        {
            if (productId <= 0)
            {
                throw new Exception("The product id you sent is invalid.");
            }

            ErpProductsNopCommerceProductsMapping erpProductsNopCommerceProductsMapping =
                _erpProductNopCommerceCustomersRepository.Table.FirstOrDefault(x => x.ProductId == productId);

            if (erpProductsNopCommerceProductsMapping == null)
            {
                throw new Exception("The product mapping you are looking for does not exits."); 
            }

            return erpProductsNopCommerceProductsMapping;
        }
    }
}

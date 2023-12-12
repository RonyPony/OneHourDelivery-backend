using Nop.Data;
using Nop.Plugin.Widgets.RegionsOnRegisterPage.Models;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Widgets.RegionsOnRegisterPage.Services
{
    /// <summary>
    /// Represents the plugin service manager
    /// </summary>
    public class RegionsOnRegisterPageService
    {
        #region Fields

        private readonly IRepository<Region> _regionRepository;
        private readonly IRepository<CustomerRegion> _customerRegionRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of RegionsOnRegisterPageService class.
        /// </summary>
        /// <param name="regionRepository">An implementation of <see cref="IRepository"/></param>
        /// <param name="customerRegionRepository">An implementation of <see cref="IRepository"/></param>
        public RegionsOnRegisterPageService(
            IRepository<Region> regionRepository,
            IRepository<CustomerRegion> customerRegionRepository)
        {
            _regionRepository = regionRepository;
            _customerRegionRepository = customerRegionRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get regions
        /// </summary>
        /// <returns>Returns a list of regions to display to the customer</returns>
        public List<Region> GetRegions()
        {
            var table = _regionRepository.Table.Distinct().OrderBy(a => a.Name);
           
            return table.ToList();
        }

        /// <summary>
        /// Insert in the database the region choose for the customer
        /// </summary>
        /// <param name="customerRegion">An implementation of <see cref="CustomerRegion"/></param>
        public void InsertCustomerRegion(CustomerRegion customerRegion)
        {
            _customerRegionRepository.Insert(customerRegion);
        }

        /// <summary>
        /// Get the customer's region from the database
        /// </summary>
        /// <param name="customerId">The Customer ID</param>
        /// <returns>Returns an entity of type CustomerRegion</returns>
        public CustomerRegion GetCustomerRegion(int customerId) => _customerRegionRepository.Table.FirstOrDefault(a => a.CustomerID == customerId);

        /// <summary>
        /// Update in the database the customer's region
        /// </summary>
        /// <param name="customerRegion">An implementation of <see cref="CustomerRegion"/></param>
        public void UpdateCustomerRegion(CustomerRegion customerRegion) => _customerRegionRepository.Update(customerRegion);

        #endregion
    }
}
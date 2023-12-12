using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Synchronizers.CommonERPTables.Domains;
using Nop.Plugin.Synchronizers.CommonERPTables.Services;
using Nop.Web.Framework.Controllers;
using System;

namespace Nop.Plugin.Synchronizers.CommonERPTables.Controllers
{
    /// <summary>
    /// Represents the controller used for saving the mapping information between the ERP and nopCommerce.
    /// </summary>
    public sealed class ErpCustomersController : BasePaymentController
    {
        private readonly ErpCustomerServiceManager _erpCustomerServiceManager;

        /// <summary>
        /// Initializes a new instance of ErpCustomersController class.
        /// </summary>
        /// <param name="erpCustomerServiceManager">An instance of <see cref="ErpCustomerServiceManager"/>.</param>
        public ErpCustomersController(ErpCustomerServiceManager erpCustomerServiceManager) 
            => _erpCustomerServiceManager = erpCustomerServiceManager;

        /// <summary>
        /// Saves the Customer mapping information.
        /// </summary>
        /// <returns>an <see cref="IActionResult"/> with the result for this operation.</returns>
        [HttpPost]
        public IActionResult Save(ErpCustomersNopCommerceCustomersMapping model)
        {
            if (model == null)
            {
                return BadRequest(new { operationResult = "Invalid operation", message = "We can process your request due to the model is null.", });
            }

            try
            {
                _erpCustomerServiceManager.SaveErpCustomerReference(model);

                return new JsonResult(new { operationResult = "Success", message = "", });
            }
            catch (Exception e)
            {
                return BadRequest(new { operationResult = "Error", message = $"There was an error while trying to save your information.\n {e.Message}", });
            }
        }

        /// <summary>
        /// Gets ERP customer code by NopCommerce customer Id.
        /// </summary>
        /// <returns>an <see cref="IActionResult"/> with the result for this operation.</returns>
        [HttpGet("erp/customers")]
        public IActionResult GetCustomerErpCodeByNopCommerceId(int nopCommerceCustomerId)
        {
            if (nopCommerceCustomerId <= 0)
            {
                return BadRequest(new { operationResult = "Invalid operation", message = "You must provide a valid customer Id.", });
            }

            try
            {
                string result = _erpCustomerServiceManager.GetCustomerErpCodeByNopCommerceId(nopCommerceCustomerId);

                return new JsonResult(new { ErpCustomerCode = result });
            }
            catch (Exception e)
            {
                return BadRequest(new { operationResult = "Error", message = $"There was an error while trying to save your information.\n {e.Message}", });
            }
        }
    }
}

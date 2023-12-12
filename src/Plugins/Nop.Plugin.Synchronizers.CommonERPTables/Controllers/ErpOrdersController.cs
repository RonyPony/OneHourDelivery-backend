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
    public sealed class ErpOrdersController : BasePaymentController
    {
        private readonly ErpOrderServiceManager _erpOrderServiceManager;

        /// <summary>
        /// Initializes a new instance of ErpOrdersController class.
        /// </summary>
        /// <param name="erpOrderServiceManager">An instance of <see cref="ErpOrderServiceManager"/>.</param>
        public ErpOrdersController(ErpOrderServiceManager erpOrderServiceManager) 
            => _erpOrderServiceManager = erpOrderServiceManager;

        /// <summary>
        /// Saves the Order mapping information.
        /// </summary>
        /// <returns>an <see cref="IActionResult"/> with the result for this operation.</returns>
        [HttpPost]
        public IActionResult Save(ErpOrdersNopCommerceOrdersMapping model)
        {
            if (model == null)
            {
                return BadRequest(new { operationResult = "Invalid operation", message = "We can process your request due to the model is null.", });
            }

            try
            {
                _erpOrderServiceManager.SaveErpOrderReference(model);

                return new JsonResult(new { operationResult = "Success", message = "", });
            }
            catch (Exception e)
            {
                return BadRequest(new { operationResult = "Error", message = $"There was an error while trying to save your information.\n {e.Message}", });
            }
        }

        /// <summary>
        /// Verify if the ERP order exist in the CommonERPtables plugin tables.
        /// </summary>
        /// <returns>an <see cref="IActionResult"/> with the result for this operation.</returns>
        [HttpGet("erp/Order/OrderExists")]
        public IActionResult OrderExists(int nopCommerceOrderId)
        {
            if (nopCommerceOrderId == 0)
            {
                return BadRequest(new { operationResult = "Invalid operation", message = "You must provide a valid NopCommerce order ID.", });
            }

            try
            {
                bool orderExists = _erpOrderServiceManager.OrderExists(nopCommerceOrderId);

                return new JsonResult(orderExists);                
            }
            catch (Exception e)
            {
                return BadRequest(new { operationResult = "Error", message = $"There was an error while trying to save your information.\n {e.Message}", });
            }
        }
    }
}
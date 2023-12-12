using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Synchronizers.CommonERPTables.Domains;
using Nop.Plugin.Synchronizers.CommonERPTables.Services;
using Nop.Web.Framework.Controllers;
using System;

namespace Nop.Plugin.Synchronizers.CommonERPTables.Controllers
{
    /// <summary>
    /// Represents the controller used for saving the Products mapping information between the ERP and nopCommerce.
    /// </summary>
    public sealed class ErpProductsController: BasePaymentController
    {
        private readonly ErpProductServiceManager _erpProductServiceManager;

        /// <summary>
        /// Initializes a new instance of ErpCustomersController class.
        /// </summary>
        /// <param name="erpProductServiceManager">An instance of <see cref="ErpProductServiceManager"/>.</param>
        public ErpProductsController(ErpProductServiceManager erpProductServiceManager) 
            => _erpProductServiceManager = erpProductServiceManager;

        /// <summary>
        /// Saves the Product mapping information.
        /// </summary>
        /// <returns>an <see cref="IActionResult"/> with the result for this operation.</returns>
        [HttpPost]
        public IActionResult Save(ErpProductsNopCommerceProductsMapping model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            try
            {
                _erpProductServiceManager.SaveErpCustomerReference(model);

                return new JsonResult(new { operationResult = "Success", message = "", });
            }
            catch (Exception e)
            {
                return BadRequest(new { operationResult = "Error", message = $"There was an error while trying to save your information.\n {e.Message}", });
            }
        }

        /// <summary>
        /// Gets an entry for the ERP-nopCommerce products mapping by the NopCommerce product id.
        /// </summary>
        /// <returns>an <see cref="IActionResult"/> with the result for this operation.</returns>
        [HttpGet("erp/products")]
        public IActionResult GetByProductId(int productId)
        {
            try
            {
                ErpProductsNopCommerceProductsMapping model = _erpProductServiceManager.GetByProductId(productId);

                return new JsonResult(model);
            }
            catch (Exception e)
            {
                return NotFound(new { operationResult = "Error", message = $"There was an error while trying to get your information.\n {e.Message}", });
            }
        }
    }
}

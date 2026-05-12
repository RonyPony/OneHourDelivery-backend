using Nop.Core.Domain.Discounts;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Models;
using Nop.Web.Areas.Admin.Models.Discounts;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Factories
{
    /// <summary>
    /// Represents a contract for delivery app discount model factory services.
    /// </summary>
    public interface IDeliveryAppDiscountModelFactory
    {
        /// <summary>
        /// Prepare delivery app discount search model
        /// </summary>
        /// <param name="searchModel">Discount search model</param>
        /// <returns>Discount search model</returns>
        DeliveryAppDiscountSearchModel PrepareDiscountSearchModel(DeliveryAppDiscountSearchModel searchModel);

        /// <summary>
        /// Prepare paged discount list model
        /// </summary>
        /// <param name="searchModel">Discount search model</param>
        /// <returns>Discount list model</returns>
        DeliveryAppDiscountListModel PrepareDiscountListModel(DeliveryAppDiscountSearchModel searchModel);

        /// <summary>
        /// Prepare discount model
        /// </summary>
        /// <param name="model">Discount model</param>
        /// <param name="discount">Discount</param>
        /// <param name="excludeProperties">Whether to exclude populating of some properties of model</param>
        /// <returns>Discount model</returns>
        DeliveryAppDiscountModel PrepareDiscountModel(DeliveryAppDiscountModel model, Discount discount, bool excludeProperties = false);

        /// <summary>
        /// Prepare product search model to add to the discount
        /// </summary>
        /// <param name="searchModel">Product search model to add to the discount</param>
        /// <returns>Product search model to add to the discount</returns>
        AddProductToDiscountSearchModel PrepareAddProductToDiscountSearchModel(AddProductToDiscountSearchModel searchModel);

        /// <summary>
        /// Prepare customer search model to assign the discount
        /// </summary>
        /// <param name="searchModel">Customer search model to assign the discount</param>
        /// <returns>Customer search model to add the discount</returns>
        DeliveryAppAddCustomerToListModel PrepareAddAssignCustomerToDiscountSearchModel(DeliveryAppAddCustomerToDiscountSearchModel searchModel);

        /// <summary>
        /// Prepare customer search model to assign the discount
        /// </summary>
        /// <param name="searchModel">Customer search model to assign the discount</param>
        /// <returns>Customer search model to add the discount</returns>
        DeliveryAppAddCustomerToDiscountSearchModel PrepareAssignDiscountToCustomer(DeliveryAppAddCustomerToDiscountSearchModel searchModel);

        /// <summary>
        /// Gets customers assigned to discounts
        /// </summary>
        /// <param name="discountId"></param>
        /// <returns>An instance of <see cref="DeliveryAppCustomerDiscountList"/></returns>
        DeliveryAppCustomerDiscountList GetCustomersAssignedToDiscountCoupon(int discountId);

    }
}

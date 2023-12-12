using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Models;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Factories
{
    /// <summary>
    /// Represents the delivery app base model factory that implements a most common admin model factories methods.
    /// </summary>
    public interface IDeliveryAppBaseAdminModelFactory
    {
        /// <summary>
        /// Prepare available drivers.
        /// </summary>
        /// <param name="items">Vendor items</param>
        /// <param name="withSpecialDefaultItem">Whether to insert the first special item for the default value</param>
        /// <param name="defaultItemText">Default item text; pass null to use default value of the default item text</param>
        void PrepareDrivers(IList<SelectListItem> items, bool withSpecialDefaultItem = true, string defaultItemText = null);

        /// <summary>
        /// Preparesthe order delivery information model.
        /// </summary>
        /// <param name="orderId">An order id.</param>
        /// <returns>An instance of <see cref="OrderDeliveryInfoModel"/>.</returns>
        OrderDeliveryInfoModel PrepareOrderDeliveryInfoModel(int orderId);

        /// <summary>
        /// Prepare available discount types
        /// </summary>
        /// <param name="items">Discount type items</param>
        /// <param name="withSpecialDefaultItem">Whether to insert the first special item for the default value</param>
        /// <param name="defaultItemText">Default item text; pass null to use default value of the default item text</param>
        void PrepareDiscountTypes(IList<SelectListItem> items, bool withSpecialDefaultItem = true, string defaultItemText = null);

        /// <summary>
        /// Prepare available payment methods.
        /// </summary>
        /// <param name="items">Peyment method items</param>
        /// <param name="withSpecialDefaultItem">Whether to insert the first special item for the default value</param>
        /// <param name="defaultItemText">Default item text; pass null to use default value of the default item text</param>
        void PreparePaymentMethods(IList<SelectListItem> items, bool withSpecialDefaultItem = true, string defaultItemText = null);


        /// <summary>
        /// Prepares the order payment collection model.
        /// </summary>
        /// <param name="orderId">An order id.</param>
        /// <returns>An instance of <see cref="OrderPaymentCollectionModel"/>.</returns>
        OrderPaymentCollectionModel PrepareOrderPaymentCollectionModel(int orderId);
    }
}

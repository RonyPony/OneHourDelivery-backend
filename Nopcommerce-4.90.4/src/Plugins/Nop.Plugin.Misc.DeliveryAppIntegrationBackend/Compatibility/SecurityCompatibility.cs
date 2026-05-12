using Nop.Core.Domain.Security;
using Nop.Services.Security;

namespace Nop.Plugin.Api.Compatibility
{
    public static class PermissionServiceCompatibilityExtensions
    {
        public static bool Authorize(this IPermissionService permissionService, string permissionRecordSystemName)
        {
            return permissionService.AuthorizeAsync(permissionRecordSystemName).GetAwaiter().GetResult();
        }

        public static bool Authorize(this IPermissionService permissionService, PermissionRecord permissionRecord)
        {
            return permissionService.AuthorizeAsync(permissionRecord).GetAwaiter().GetResult();
        }
    }
}

namespace Nop.Services.Security
{
    public static class StandardPermissionProvider
    {
        public const string ManageOrders = StandardPermission.Orders.ORDERS_VIEW;
        public const string ManageSettings = StandardPermission.Configuration.MANAGE_SETTINGS;
        public const string ManageMaintenance = StandardPermission.System.MANAGE_MAINTENANCE;
        public const string ManageDiscounts = StandardPermission.Promotions.DISCOUNTS_CREATE_EDIT_DELETE;
        public const string ManagePaymentMethods = StandardPermission.Configuration.MANAGE_PAYMENT_METHODS;
        public const string ManageShippingSettings = StandardPermission.Configuration.MANAGE_SHIPPING_SETTINGS;
    }
}

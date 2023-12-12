namespace Nop.Plugin.Synchronizers.SAPCustomers.Models
{
    /// <summary>
    /// Represents the model class for mapping the SAP customer's role.
    /// </summary>
    public sealed class SapCustomerRoleModel
    {
        /// <summary>
        /// Represents the group code a customer belongs to.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Represents the group name a customer belongs to.
        /// </summary>
        public string Name { get; set; }
    }
}

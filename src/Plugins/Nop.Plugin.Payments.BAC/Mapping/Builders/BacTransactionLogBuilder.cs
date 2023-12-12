using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Payments.BAC.Domains;

namespace Nop.Plugin.Payments.BAC.Mapping.Builders
{
    /// <summary>
    /// Entity builder for <see cref="BacTransactionLog"/>
    /// </summary>
    public class BacTransactionLogBuilder : NopEntityBuilder<BacTransactionLog>
    {
        #region Methods

        /// <summary>
        /// Apply entity configuration
        /// </summary>
        /// <param name="table">Create table expression builder</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table.WithColumn(nameof(BacTransactionLog.Id)).AsInt32().PrimaryKey().Identity();

            table.WithColumn(nameof(BacTransactionLog.OrderId)).AsInt32().Nullable();
            table.WithColumn(nameof(BacTransactionLog.ReasonCode)).AsInt32().Nullable();
            table.WithColumn(nameof(BacTransactionLog.ResponseCode)).AsInt32().Nullable();
            table.WithColumn(nameof(BacTransactionLog.FullException)).AsString(int.MaxValue).Nullable();
            table.WithColumn(nameof(BacTransactionLog.DateLogged)).AsDateTime();
        }

        #endregion
    }
}
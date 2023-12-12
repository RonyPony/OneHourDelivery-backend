using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Payments.CardNet.Domains;

namespace Nop.Plugin.Payments.CardNet.Mapping.Builders
{
    /// <summary>
    /// Entity builder for <see cref="CardNetTransactionLog"/>
    /// </summary>
    public class CardNetTransactionLogBuilder : NopEntityBuilder<CardNetTransactionLog>
    {
        /// <summary>
        /// Apply entity configuration
        /// </summary>
        /// <param name="table">Create table expression builder</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table.WithColumn(nameof(CardNetTransactionLog.Id)).AsInt32().PrimaryKey().Identity();

            table.WithColumn(nameof(CardNetTransactionLog.OrderId)).AsInt32().Nullable();
            table.WithColumn(nameof(CardNetTransactionLog.Amount)).AsDecimal(18,2).Nullable();
            table.WithColumn(nameof(CardNetTransactionLog.Currency)).AsString(3).Nullable();
            table.WithColumn(nameof(CardNetTransactionLog.ApprovalCode)).AsString(20).Nullable();
            table.WithColumn(nameof(CardNetTransactionLog.ResultType)).AsString(20).Nullable();
            table.WithColumn(nameof(CardNetTransactionLog.ErrorMessage)).AsString(3000).Nullable();
            table.WithColumn(nameof(CardNetTransactionLog.FullException)).AsString(int.MaxValue).Nullable();
            table.WithColumn(nameof(CardNetTransactionLog.DateLogged)).AsDateTime();
        }
    }
}

using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Payments.CyberSource.Models;

namespace Nop.Plugin.Payments.CyberSource.Mapping.Builders
{
    /// <summary>
    /// Entity builder for <see cref="CyberSourceTransactionLog"/>
    /// </summary>
    public class CyberSourceTransactionLogBuilder : NopEntityBuilder<CyberSourceTransactionLog>
    {
        /// <summary>
        /// Apply entity configuration
        /// </summary>
        /// <param name="table">Create table expression builder</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table.WithColumn(nameof(CyberSourceTransactionLog.Id)).AsInt32().PrimaryKey().Identity()
                .WithColumn(nameof(CyberSourceTransactionLog.OrderId)).AsInt32().Nullable()
                .WithColumn(nameof(CyberSourceTransactionLog.CustomerId)).AsInt32()
                .WithColumn(nameof(CyberSourceTransactionLog.CardType)).AsString(50).Nullable()
                .WithColumn(nameof(CyberSourceTransactionLog.Amount)).AsDecimal(18, 2).Nullable()
                .WithColumn(nameof(CyberSourceTransactionLog.Currency)).AsString(5).Nullable()
                .WithColumn(nameof(CyberSourceTransactionLog.TransactionId)).AsString(80).Nullable()
                .WithColumn(nameof(CyberSourceTransactionLog.TransactionUuid)).AsString(80).Nullable()
                .WithColumn(nameof(CyberSourceTransactionLog.ReasonCode)).AsString(5).Nullable()
                .WithColumn(nameof(CyberSourceTransactionLog.Status)).AsString(7).Nullable()
                .WithColumn(nameof(CyberSourceTransactionLog.Message)).AsString(3000).Nullable()
                .WithColumn(nameof(CyberSourceTransactionLog.FullException)).AsString(int.MaxValue).Nullable()
                .WithColumn(nameof(CyberSourceTransactionLog.DateLogged)).AsDateTime();
        }
    }
}

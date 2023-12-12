using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Payments.Banrural.Models;

namespace Nop.Plugin.Payments.Banrural.Mapping.Builders
{
    /// <summary>
    /// Represents the builder class for creating the database entity
    /// that will be use for storing the Banrural configuration.
    /// </summary>
    public class BanruralBuilder : NopEntityBuilder<BanruralTransactionLog>
    {
    /// <summary>
    /// Represents the builder method for creating the database entity
    /// that will be use for storing the Banrural configuration.
    /// </summary>
    /// <param name="table">An expression builder for a FluentMigrator.Expressions.CreateTableExpression</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table.WithColumn(nameof(BanruralTransactionLog.Id)).AsInt32().PrimaryKey().Identity()
                .WithColumn(nameof(BanruralTransactionLog.ReferenceNumber)).AsString(100).Nullable()
                .WithColumn(nameof(BanruralTransactionLog.Description)).AsString(500).Nullable()
                .WithColumn(nameof(BanruralTransactionLog.Uuid)).AsString(50).Nullable()
                .WithColumn(nameof(BanruralTransactionLog.Currency)).AsString(50)
                .WithColumn(nameof(BanruralTransactionLog.TaxAmount)).AsDecimal(18, 2).Nullable()
                .WithColumn(nameof(BanruralTransactionLog.Amount)).AsDecimal(18, 2)
                .WithColumn(nameof(BanruralTransactionLog.CustomerEmail)).AsString(150)
                .WithColumn(nameof(BanruralTransactionLog.Order)).AsString(50)
                .WithColumn(nameof(BanruralTransactionLog.TransactionId)).AsString(50).Nullable()
                .WithColumn(nameof(BanruralTransactionLog.IsSuccess)).AsBoolean()
                .WithColumn(nameof(BanruralTransactionLog.FullException)).AsString(int.MaxValue).Nullable();
        }
    }
}

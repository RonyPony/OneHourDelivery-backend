using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Payments.AzulPaymentPage.Domains;

namespace Nop.Plugin.Payments.AzulPaymentPage.Mapping.Builders
{
    /// <summary>
    /// Represents the builder class for creating the database entity
    /// that will be use for storing the AZUL configuration.
    /// </summary>
    public class AzulPaymentPageBuilder : NopEntityBuilder<AzulPaymentTransactionLog>
    {
        /// <inheritdoc />
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table.WithColumn(nameof(AzulPaymentTransactionLog.Id)).AsInt32().PrimaryKey().Identity()
                .WithColumn(nameof(AzulPaymentTransactionLog.OrderNumber)).AsString(80)
                .WithColumn(nameof(AzulPaymentTransactionLog.Amount)).AsDecimal(18, 2)
                .WithColumn(nameof(AzulPaymentTransactionLog.Itbis)).AsDecimal(18, 2)
                .WithColumn(nameof(AzulPaymentTransactionLog.AuthorizationCode)).AsString(80)
                .WithColumn(nameof(AzulPaymentTransactionLog.TransactionDate)).AsDateTime2()
                .WithColumn(nameof(AzulPaymentTransactionLog.ResponseCode)).AsString(80).Nullable()
                .WithColumn(nameof(AzulPaymentTransactionLog.ResponseMessage)).AsString(3000).Nullable()
                .WithColumn(nameof(AzulPaymentTransactionLog.ErrorDescription)).AsString(int.MaxValue).Nullable()
                .WithColumn(nameof(AzulPaymentTransactionLog.IsoCode)).AsString(50).Nullable()
                .WithColumn(nameof(AzulPaymentTransactionLog.Rrn)).AsString(50).Nullable()
                .WithColumn(nameof(AzulPaymentTransactionLog.DateLogged)).AsDateTime2();
        }
    }
}
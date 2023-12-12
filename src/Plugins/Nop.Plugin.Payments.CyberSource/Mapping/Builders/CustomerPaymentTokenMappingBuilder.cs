using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Payments.CyberSource.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Payments.CyberSource.Mapping.Builders
{
    class CustomerPaymentTokenMappingBuilder : NopEntityBuilder<CustomerPaymentTokenMapping>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table.WithColumn(nameof(CustomerPaymentTokenMapping.CustomerId)).AsInt32()
                 .WithColumn(nameof(CustomerPaymentTokenMapping.Token)).AsString().Nullable()
                 .WithColumn(nameof(CustomerPaymentTokenMapping.CardLastFourDigits)).AsString().Nullable()
                 .WithColumn(nameof(CustomerPaymentTokenMapping.CardType)).AsString().Nullable()
                 .WithColumn(nameof(CustomerPaymentTokenMapping.CardExpirationDate)).AsString().Nullable()
                 .WithColumn(nameof(CustomerPaymentTokenMapping.IsDefaultPaymentMethod)).AsBoolean();
        }
    }
}

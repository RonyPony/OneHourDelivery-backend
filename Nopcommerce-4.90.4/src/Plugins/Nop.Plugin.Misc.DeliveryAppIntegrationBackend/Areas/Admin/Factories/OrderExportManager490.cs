using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Factories
{
    public partial class OrderExportManager : IOrderExportManager
    {
        public string ExportOrdersToXml(IList<OrderPendingToClosePayment> orders)
        {
            return ExportToXml(orders, includeDeliveryColumns: true);
        }

        public byte[] ExportOrdersToXlsx(IList<OrderPendingToClosePayment> orders)
        {
            return ExportToXlsxInternal(orders, includeDeliveryColumns: true);
        }

        public string ExportVendorOrdersEarningToXml(IList<OrderPendingToClosePayment> orders)
        {
            return ExportToXml(orders, includeDeliveryColumns: false);
        }

        public byte[] ExportVendorOrdersEarningToXlsx(IList<OrderPendingToClosePayment> orders)
        {
            return ExportToXlsxInternal(orders, includeDeliveryColumns: false);
        }

        private static string ExportToXml(IList<OrderPendingToClosePayment> orders, bool includeDeliveryColumns)
        {
            using var stringWriter = new StringWriter();
            using var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = true });

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Orders");

            foreach (var order in orders)
            {
                xmlWriter.WriteStartElement("Order");
                WriteOrderXml(xmlWriter, order, includeDeliveryColumns);
                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            return stringWriter.ToString();
        }

        private static byte[] ExportToXlsxInternal(IList<OrderPendingToClosePayment> orders, bool includeDeliveryColumns)
        {
            using var stream = new MemoryStream();
            using var package = new ExcelPackage(stream);
            var worksheet = package.Workbook.Worksheets.Add("Orders");

            var headers = includeDeliveryColumns
                ? new[]
                {
                    "Order number", "Order id", "Vendor id", "Vendor payment status", "Driver payment status",
                    "Subtotal excl tax", "Delivery cost", "Order total", "Vendor profit", "Admin profit",
                    "Driver profit", "Created on UTC"
                }
                : new[]
                {
                    "Order number", "Order id", "Vendor id", "Order discount", "Subtotal excl tax",
                    "Order total without delivery", "Vendor profit", "Admin profit", "Created on UTC"
                };

            for (var index = 0; index < headers.Length; index++)
                worksheet.Cells[1, index + 1].Value = headers[index];

            var row = 2;
            foreach (var order in orders)
            {
                WriteOrderRow(worksheet, row++, order, includeDeliveryColumns);
            }

            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
            package.Save();
            return stream.ToArray();
        }

        private static void WriteOrderXml(XmlWriter xmlWriter, OrderPendingToClosePayment order, bool includeDeliveryColumns)
        {
            xmlWriter.WriteElementString("OrderNumber", order.CustomOrderNumber);
            xmlWriter.WriteElementString("OrderId", order.OrderId.ToString());
            xmlWriter.WriteElementString("VendorId", order.VendorId.ToString());
            xmlWriter.WriteElementString("VendorPaymentStatus", order.VendorPaymentStatus.ToString());
            xmlWriter.WriteElementString("OrderSubtotalExclTax", order.OrderSubtotalExclTax.ToString());
            xmlWriter.WriteElementString("OrderTotal", order.OrderTotal.ToString());
            xmlWriter.WriteElementString("OrderTotalVendorProfitAmount", order.OrderTotalVendorProfitAmount.ToString());
            xmlWriter.WriteElementString("OrderTotalAdministrativeProfitAmount", order.OrderTotalAdministrativeProfitAmount.ToString());
            xmlWriter.WriteElementString("CreatedOnUtc", order.CreatedOnUtc.ToString("O"));

            if (!includeDeliveryColumns)
                return;

            xmlWriter.WriteElementString("DriverPaymentStatus", order.DriverPaymentStatus.ToString());
            xmlWriter.WriteElementString("OrderShippingInclTax", order.OrderShippingInclTax.ToString());
            xmlWriter.WriteElementString("ShippingTaxMessengerProfitAmount", order.ShippingTaxMessengerProfitAmount.ToString());
            xmlWriter.WriteElementString("ShippingTaxAdministrativeProfitAmount", order.ShippingTaxAdministrativeProfitAmount.ToString());
        }

        private static void WriteOrderRow(ExcelWorksheet worksheet, int row, OrderPendingToClosePayment order, bool includeDeliveryColumns)
        {
            worksheet.Cells[row, 1].Value = order.CustomOrderNumber;
            worksheet.Cells[row, 2].Value = order.OrderId;
            worksheet.Cells[row, 3].Value = order.VendorId;
            worksheet.Cells[row, 4].Value = order.VendorPaymentStatus.ToString();

            if (includeDeliveryColumns)
            {
                worksheet.Cells[row, 5].Value = order.DriverPaymentStatus.ToString();
                worksheet.Cells[row, 6].Value = order.OrderSubtotalExclTax;
                worksheet.Cells[row, 7].Value = order.OrderShippingInclTax;
                worksheet.Cells[row, 8].Value = order.OrderTotal;
                worksheet.Cells[row, 9].Value = order.OrderTotalVendorProfitAmount;
                worksheet.Cells[row, 10].Value = order.OrderTotalAdministrativeProfitAmount;
                worksheet.Cells[row, 11].Value = order.ShippingTaxMessengerProfitAmount;
                worksheet.Cells[row, 12].Value = order.CreatedOnUtc;
                return;
            }

            worksheet.Cells[row, 4].Value = order.OrderDiscount;
            worksheet.Cells[row, 5].Value = order.OrderSubtotalExclTax;
            worksheet.Cells[row, 6].Value = order.OrderTotal - order.OrderShippingInclTax;
            worksheet.Cells[row, 7].Value = order.OrderTotalVendorProfitAmount;
            worksheet.Cells[row, 8].Value = order.OrderTotalAdministrativeProfitAmount;
            worksheet.Cells[row, 9].Value = order.CreatedOnUtc;
        }
    }
}

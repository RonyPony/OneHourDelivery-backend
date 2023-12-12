using Nop.Core.Domain.Catalog;
using Nop.Plugin.Api.Converters;
using Nop.Plugin.Api.DTO;
using Nop.Services.Catalog;
using Nop.Services.Media;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    public class ProductAttributeConverter : IProductAttributeConverter
    {
        private readonly IProductAttributeService _productAttributeService;
        private readonly IProductAttributeParser _productAttributeParser;
        private readonly IDownloadService _downloadService;

        public ProductAttributeConverter(IProductAttributeService productAttributeService,
            IProductAttributeParser productAttributeParser,
            IDownloadService downloadService,
            IApiTypeConverter apiTypeConverter)
        {
            _productAttributeService = productAttributeService;
            _productAttributeParser = productAttributeParser;
            _downloadService = downloadService;
        }

        public string ConvertToXml(List<ProductItemAttributeDto> attributeDtos, int productId)
        {
            var attributesXml = "";

            if (attributeDtos == null)
                return attributesXml;

            var productAttributes = _productAttributeService.GetProductAttributeMappingsByProductId(productId);
            foreach (var attribute in productAttributes)
            {
                ProductItemAttributeDto selectedAttribute = attributeDtos.Where(x => x.Id == attribute.Id).FirstOrDefault();

                switch (attribute.AttributeControlType)
                {
                    case AttributeControlType.DropdownList:
                    case AttributeControlType.RadioList:
                    case AttributeControlType.ColorSquares:
                    case AttributeControlType.ImageSquares:
                        {
                            if (selectedAttribute != null)
                            {
                                int selectedAttributeValue;
                                var isInt = int.TryParse(selectedAttribute.Value, out selectedAttributeValue);
                                if (isInt && selectedAttributeValue > 0)
                                {
                                    attributesXml = _productAttributeParser.AddProductAttribute(attributesXml,
                                        attribute, selectedAttributeValue.ToString());
                                }
                            }
                        }
                        break;
                    case AttributeControlType.Checkboxes:
                        {
                            if (selectedAttribute == null) continue;

                            List<string> attributeValues = selectedAttribute.Value.Split(",").ToList();

                            attributeValues.ForEach(x => x.Trim());

                            foreach (var value in attributeValues)
                            {
                                int selectedAttributeValue;
                                var isInt = int.TryParse(value, out selectedAttributeValue);
                                if (isInt && selectedAttributeValue > 0)
                                {
                                    // currently there is no support for attribute quantity
                                    var quantity = 1;

                                    attributesXml = _productAttributeParser.AddProductAttribute(attributesXml,
                                        attribute, selectedAttributeValue.ToString(), quantity);
                                }

                            }
                        }
                        break;
                    case AttributeControlType.ReadonlyCheckboxes:
                        {
                            //load read-only(already server - side selected) values
                            var attributeValues = _productAttributeService.GetProductAttributeValues(attribute.Id);
                            foreach (var selectedAttributeId in attributeValues
                                .Where(v => v.IsPreSelected)
                                .Select(v => v.Id)
                                .ToList())
                            {
                                attributesXml = _productAttributeParser.AddProductAttribute(attributesXml,
                                     attribute, selectedAttributeId.ToString());
                            }
                        }
                        break;
                    case AttributeControlType.TextBox:
                    case AttributeControlType.MultilineTextbox:
                        {
                            if (selectedAttribute != null)
                            {
                                attributesXml = _productAttributeParser.AddProductAttribute(attributesXml,
                                    attribute, selectedAttribute.Value);
                            }

                        }
                        break;
                    case AttributeControlType.Datepicker:
                        {
                            if (selectedAttribute != null)
                            {
                                DateTime selectedDate;

                                // Since nopCommerce uses this format to keep the date in the database to keep it consisten we will expect the same format to be passed
                                var validDate = DateTime.TryParseExact(selectedAttribute.Value, "D", CultureInfo.CurrentCulture,
                                                       DateTimeStyles.None, out selectedDate);

                                if (validDate)
                                {
                                    attributesXml = _productAttributeParser.AddProductAttribute(attributesXml,
                                        attribute, selectedDate.ToString("D"));
                                }
                            }
                        }
                        break;
                    case AttributeControlType.FileUpload:
                        {
                            if (selectedAttribute != null)
                            {
                                Guid downloadGuid;
                                bool isGuid = Guid.TryParse(selectedAttribute.Value, out downloadGuid);

                                if (!isGuid) continue;

                                var download = _downloadService.GetDownloadByGuid(downloadGuid);
                                if (download != null)
                                {
                                    attributesXml = _productAttributeParser.AddProductAttribute(attributesXml,
                                            attribute, download.DownloadGuid.ToString());
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            // No Gift Card attributes support yet

            return attributesXml;
        }

        public List<ProductItemAttributeDto> Parse(string attributesXml)
        {
            var attributeDtos = new List<ProductItemAttributeDto>();
            if (string.IsNullOrEmpty(attributesXml))
                return attributeDtos;

            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(attributesXml);

                foreach (XmlNode attributeNode in xmlDoc.SelectNodes(@"//Attributes/ProductAttribute"))
                {
                    if (attributeNode.Attributes != null && attributeNode.Attributes["ID"] != null)
                    {
                        int attributeId;
                        bool isInt = int.TryParse(attributeNode.Attributes["ID"].InnerText.Trim(), out attributeId);
                        if (isInt)
                        {
                            foreach (XmlNode attributeValue in attributeNode.SelectNodes("ProductAttributeValue"))
                            {
                                var value = attributeValue.SelectSingleNode("Value").InnerText.Trim();
                                // no support for quantity yet
                                //var quantityNode = attributeValue.SelectSingleNode("Quantity");
                                attributeDtos.Add(new ProductItemAttributeDto { Id = attributeId, Value = value });
                            }
                        }
                    }
                }
            }
            catch { } //TODO: Maybe a logger should be added here. In that case maybe the changes would also apply to the files with the same name in Nop.Plugin.Api and  in Nop.Plugin.DdpApi.

            return attributeDtos;
        }
    }

}

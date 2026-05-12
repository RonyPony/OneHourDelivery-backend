using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers
{
    /// <summary>
    /// Responsible of providing funcionality to interact with CustomerAttribute.
    /// </summary>
    public class CustomerAttributeHelper
    {

        /// <summary>
        /// Deserialize xml to Customer attributes
        /// </summary>
        /// <param name="xml">Valid xml with customer attributes.</param>
        /// <returns>A collection of <see cref="List{CustomerAttributeModel}"/></returns>
        public List<CustomerAttributeModel> DeserializeAttributes(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<CustomerAttributeModel>), new XmlRootAttribute("Attributes"));

            using (StringReader stringReader = new StringReader(xml))
            {
                return (List<CustomerAttributeModel>)serializer.Deserialize(stringReader);
            }
        }

        /// <summary>
        /// Gets a value of a vendor attribute.
        /// </summary>
        /// <param name="xml">Vendor attributes xml</param>
        /// <param name="id">Vendor attribute id</param>
        /// <returns>Customer attribute value for given customer</returns>
        public string GetCustomerAttributeValue(string xml, int id)
        {
            List<CustomerAttributeModel> deserializedAttributes = DeserializeAttributes(xml);

            var value = deserializedAttributes.FirstOrDefault(x => x.ID == id.ToString());

            return value?.CustomerAttributeValue.Value;
        }

        /// <summary>
        /// Builds vendor attributes valid to save on generic attributes.
        /// </summary>
        /// <param name="customerAttributeId">Indicates Vendor attribute id</param>
        /// <param name="value">Represent vendor attribute value.</param>
        /// <returns></returns>
        public CustomerAttributeList BuildCustomerAttribute(string customerAttributeId, string value) => new CustomerAttributeList
        {
            Attributes = new List<CustomerAttributeModel> {
                           new CustomerAttributeModel{ ID = customerAttributeId, CustomerAttributeValue = new CustomerAttributeValue{ Value = value } }
                        }
        };

    }

    /// <summary>
    /// Represents a collection of attributes for xml.
    /// </summary>
    public class CustomerAttributeList
    {
        /// <summary>
        /// Indicates customer attributes.
        /// </summary>
        public List<CustomerAttributeModel> Attributes { get; set; }

        /// <summary>
        /// Converts attributes property into xml string.
        /// </summary>
        /// <returns></returns>
        public string ToXML()
        {

            Encoding encoding = Encoding.UTF8;
            using (MemoryStream ms = new MemoryStream())
            {
                var settings = new XmlWriterSettings();
                settings.Indent = false;
                settings.OmitXmlDeclaration = true;
                settings.Encoding = encoding;

                using (var stringwriter = XmlWriter.Create(ms, settings))
                {
                    var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
                    var serializer = new XmlSerializer(this.Attributes.GetType(), new XmlRootAttribute("Attributes"));


                    serializer.Serialize(stringwriter, this.Attributes, emptyNamespaces);
                    return encoding.GetString(ms.ToArray()).Substring(1);
                }

            }

        }

    }

    /// <summary>
    /// Represents a customer attribute model.
    /// </summary>
    [XmlType("CustomerAttribute")]
    public class CustomerAttributeModel
    {
        /// <summary>
        /// Indices customer attributes id.
        /// </summary>
        [XmlAttribute("ID")]
        public string ID { get; set; }

        /// <summary>
        /// Indicates customer attribute value.
        /// </summary>
        public CustomerAttributeValue CustomerAttributeValue { get; set; }
    }
    /// <summary>
    /// Represents customer attribute value.
    /// </summary>
    public class CustomerAttributeValue
    {
        /// <summary>
        /// Indicates the value.
        /// </summary>
        public string Value { get; set; }
    }
}

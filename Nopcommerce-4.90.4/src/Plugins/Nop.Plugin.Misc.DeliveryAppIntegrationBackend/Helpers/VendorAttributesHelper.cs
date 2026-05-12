using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers
{
    /// <summary>
    /// Responsible of providing funcionality to interact with VendorAttributes.
    /// </summary>
    public class VendorAttributesHelper
    {
        /// <summary>
        /// Deserialize xml to Vendor attributes
        /// </summary>
        /// <param name="xml">Valid xml with vendor attributes.</param>
        /// <returns>A collection of <see cref="List{VendorAttributeModel}"/></returns>
        public List<VendorAttributeModel> DeserializeAttributes(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<VendorAttributeModel>), new XmlRootAttribute("Attributes"));

            using (StringReader stringReader = new StringReader(xml))
            {
                return (List<VendorAttributeModel>)serializer.Deserialize(stringReader);
            }
        }

        /// <summary>
        /// Gets a value of a vendor attribute.
        /// </summary>
        /// <param name="xml">Vendor attributes xml</param>
        /// <param name="id">Vendor attribute id</param>
        /// <returns>Vendor attribute value for given vendor</returns>
        public string GetVendorAttributeValue(string xml, int id)
        {
            List<VendorAttributeModel> deserializedAttributes = DeserializeAttributes(xml);

            var value = deserializedAttributes.FirstOrDefault(x => x.ID == id.ToString());

            return value?.VendorAttributeValue.Value;
        }

        /// <summary>
        /// Builds vendor attributes valid to save on generic attributes.
        /// </summary>
        /// <param name="vendorAttributeId">Indicates Vendor attribute id</param>
        /// <param name="value">Represent vendor attribute value.</param>
        /// <returns></returns>
        public VendorAttributeList BuildVendorAttribute(string vendorAttributeId, string value) => new VendorAttributeList
        {
            Attributes = new List<VendorAttributeModel> {
                           new VendorAttributeModel{ ID = vendorAttributeId, VendorAttributeValue = new VendorAttributeValueModel{ Value = value } }
                        }
        };
    }

    /// <summary>
    /// Represents a collection of attributes for xml.
    /// </summary>
    public class VendorAttributeList
    {
        /// <summary>
        /// Indicates vendor attributes.
        /// </summary>
        public List<VendorAttributeModel> Attributes { get; set; }

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
    /// Represents a vendor attribute model.
    /// </summary>
    [XmlType("VendorAttribute")]
    public class VendorAttributeModel
    {
        /// <summary>
        /// Indices vendor attributes id.
        /// </summary>
        [XmlAttribute("ID")]
        public string ID { get; set; }

        /// <summary>
        /// Indicates vendor attribute value.
        /// </summary>
        public VendorAttributeValueModel VendorAttributeValue { get; set; }

    }

    /// <summary>
    /// Represents vendor attribute value.
    /// </summary>
    public class VendorAttributeValueModel
    {
        /// <summary>
        /// Indicates the value.
        /// </summary>
        public string Value { get; set; }
    }
}

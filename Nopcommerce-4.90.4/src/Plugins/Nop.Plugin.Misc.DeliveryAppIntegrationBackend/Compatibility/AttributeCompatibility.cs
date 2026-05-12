using Microsoft.AspNetCore.Http;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Vendors;
using Nop.Services.Attributes;

namespace Nop.Services.Common
{
    public interface IAddressAttributeFormatter
    {
        string FormatAttributes(string attributesXml, string separator = "<br />", bool htmlEncode = true);
    }

    public interface IAddressAttributeParser
    {
        IList<string> ParseValues(string attributesXml, int attributeId);
        string ParseCustomAddressAttributes(IFormCollection form);
    }

    public interface IAddressAttributeService
    {
        IList<AddressAttribute> GetAllAddressAttributes();
        void InsertAddressAttribute(AddressAttribute addressAttribute);
        void InsertAddressAttributeValue(AddressAttributeValue addressAttributeValue);
    }

    public sealed class AddressAttributeParserAdapter : IAddressAttributeParser
    {
        private readonly IAttributeParser<AddressAttribute, AddressAttributeValue> _parser;

        public AddressAttributeParserAdapter(IAttributeParser<AddressAttribute, AddressAttributeValue> parser)
        {
            _parser = parser;
        }

        public IList<string> ParseValues(string attributesXml, int attributeId)
        {
            return _parser.ParseValues(attributesXml, attributeId);
        }

        public string ParseCustomAddressAttributes(IFormCollection form)
        {
            return _parser.ParseCustomAttributesAsync(form, NopCommonDefaults.AddressAttributeControlName).GetAwaiter().GetResult();
        }
    }

    public sealed class AddressAttributeServiceAdapter : IAddressAttributeService
    {
        private readonly IAttributeService<AddressAttribute, AddressAttributeValue> _service;

        public AddressAttributeServiceAdapter(IAttributeService<AddressAttribute, AddressAttributeValue> service)
        {
            _service = service;
        }

        public IList<AddressAttribute> GetAllAddressAttributes()
        {
            return _service.GetAllAttributesAsync().GetAwaiter().GetResult();
        }

        public void InsertAddressAttribute(AddressAttribute addressAttribute)
        {
            _service.InsertAttributeAsync(addressAttribute).GetAwaiter().GetResult();
        }

        public void InsertAddressAttributeValue(AddressAttributeValue addressAttributeValue)
        {
            _service.InsertAttributeValueAsync(addressAttributeValue).GetAwaiter().GetResult();
        }
    }

    public sealed class AddressAttributeFormatterAdapter : IAddressAttributeFormatter
    {
        private readonly IAttributeFormatter<AddressAttribute, AddressAttributeValue> _formatter;

        public AddressAttributeFormatterAdapter(IAttributeFormatter<AddressAttribute, AddressAttributeValue> formatter)
        {
            _formatter = formatter;
        }

        public string FormatAttributes(string attributesXml, string separator = "<br />", bool htmlEncode = true)
        {
            return _formatter.FormatAttributesAsync(attributesXml, separator, htmlEncode).GetAwaiter().GetResult();
        }
    }
}

namespace Nop.Services.Customers
{
    using Nop.Core.Domain.Customers;
    using Nop.Services.Attributes;

    public interface ICustomerAttributeParser
    {
        string ParseCustomerAttributes(IFormCollection form);
        IList<CustomerAttribute> ParseCustomerAttributes(string attributesXml);
        IList<CustomerAttributeValue> ParseCustomerAttributeValues(string attributesXml);
        IList<string> ParseValues(string attributesXml, int attributeId);
        string AddCustomerAttribute(string attributesXml, CustomerAttribute attribute, string value);
    }

    public interface ICustomerAttributeService
    {
        IList<CustomerAttribute> GetAllCustomerAttributes();
        IList<CustomerAttributeValue> GetCustomerAttributeValues(int customerAttributeId);
        void InsertCustomerAttribute(CustomerAttribute customerAttribute);
        void InsertCustomerAttributeValue(CustomerAttributeValue customerAttributeValue);
    }

    public sealed class CustomerAttributeParserAdapter : ICustomerAttributeParser
    {
        private readonly IAttributeParser<CustomerAttribute, CustomerAttributeValue> _parser;

        public CustomerAttributeParserAdapter(IAttributeParser<CustomerAttribute, CustomerAttributeValue> parser)
        {
            _parser = parser;
        }

        public IList<CustomerAttributeValue> ParseCustomerAttributeValues(string attributesXml)
        {
            return _parser.ParseAttributeValuesAsync(attributesXml).GetAwaiter().GetResult();
        }

        public string ParseCustomerAttributes(IFormCollection form)
        {
            return _parser.ParseCustomAttributesAsync(form, NopCustomerServicesDefaults.CustomerAttributePrefix).GetAwaiter().GetResult();
        }

        public IList<CustomerAttribute> ParseCustomerAttributes(string attributesXml)
        {
            return _parser.ParseAttributesAsync(attributesXml).GetAwaiter().GetResult();
        }

        public IList<string> ParseValues(string attributesXml, int attributeId)
        {
            return _parser.ParseValues(attributesXml, attributeId);
        }

        public string AddCustomerAttribute(string attributesXml, CustomerAttribute attribute, string value)
        {
            return _parser.AddAttribute(attributesXml, attribute, value);
        }
    }

    public sealed class CustomerAttributeServiceAdapter : ICustomerAttributeService
    {
        private readonly IAttributeService<CustomerAttribute, CustomerAttributeValue> _service;

        public CustomerAttributeServiceAdapter(IAttributeService<CustomerAttribute, CustomerAttributeValue> service)
        {
            _service = service;
        }

        public IList<CustomerAttribute> GetAllCustomerAttributes()
        {
            return _service.GetAllAttributesAsync().GetAwaiter().GetResult();
        }

        public IList<CustomerAttributeValue> GetCustomerAttributeValues(int customerAttributeId)
        {
            return _service.GetAttributeValuesAsync(customerAttributeId).GetAwaiter().GetResult();
        }

        public void InsertCustomerAttribute(CustomerAttribute customerAttribute)
        {
            _service.InsertAttributeAsync(customerAttribute).GetAwaiter().GetResult();
        }

        public void InsertCustomerAttributeValue(CustomerAttributeValue customerAttributeValue)
        {
            _service.InsertAttributeValueAsync(customerAttributeValue).GetAwaiter().GetResult();
        }
    }
}

namespace Nop.Services.Orders
{
    public interface ICheckoutAttributeParser
    {
        IList<CheckoutAttribute> ParseCheckoutAttributes(string attributesXml);
        IList<CheckoutAttributeValue> ParseCheckoutAttributeValues(string attributesXml);
        string AddCheckoutAttribute(string attributesXml, CheckoutAttribute attribute, string value);
        string RemoveCheckoutAttribute(string attributesXml, CheckoutAttribute attribute);
    }

    public interface ICheckoutAttributeService
    {
        IList<CheckoutAttribute> GetAllCheckoutAttributes();
        IList<CheckoutAttributeValue> GetCheckoutAttributeValues(int checkoutAttributeId);
        void InsertCheckoutAttribute(CheckoutAttribute checkoutAttribute);
        void InsertCheckoutAttributeValue(CheckoutAttributeValue checkoutAttributeValue);
    }

    public sealed class CheckoutAttributeParserAdapter : ICheckoutAttributeParser
    {
        private readonly IAttributeParser<CheckoutAttribute, CheckoutAttributeValue> _parser;

        public CheckoutAttributeParserAdapter(IAttributeParser<CheckoutAttribute, CheckoutAttributeValue> parser)
        {
            _parser = parser;
        }

        public IList<CheckoutAttribute> ParseCheckoutAttributes(string attributesXml)
        {
            return _parser.ParseAttributesAsync(attributesXml).GetAwaiter().GetResult();
        }

        public IList<CheckoutAttributeValue> ParseCheckoutAttributeValues(string attributesXml)
        {
            return _parser.ParseAttributeValuesAsync(attributesXml).GetAwaiter().GetResult();
        }

        public string AddCheckoutAttribute(string attributesXml, CheckoutAttribute attribute, string value)
        {
            return _parser.AddAttribute(attributesXml, attribute, value);
        }

        public string RemoveCheckoutAttribute(string attributesXml, CheckoutAttribute attribute)
        {
            return _parser.RemoveAttribute(attributesXml, attribute.Id);
        }
    }

    public sealed class CheckoutAttributeServiceAdapter : ICheckoutAttributeService
    {
        private readonly IAttributeService<CheckoutAttribute, CheckoutAttributeValue> _service;

        public CheckoutAttributeServiceAdapter(IAttributeService<CheckoutAttribute, CheckoutAttributeValue> service)
        {
            _service = service;
        }

        public IList<CheckoutAttribute> GetAllCheckoutAttributes()
        {
            return _service.GetAllAttributesAsync().GetAwaiter().GetResult();
        }

        public IList<CheckoutAttributeValue> GetCheckoutAttributeValues(int checkoutAttributeId)
        {
            return _service.GetAttributeValuesAsync(checkoutAttributeId).GetAwaiter().GetResult();
        }

        public void InsertCheckoutAttribute(CheckoutAttribute checkoutAttribute)
        {
            _service.InsertAttributeAsync(checkoutAttribute).GetAwaiter().GetResult();
        }

        public void InsertCheckoutAttributeValue(CheckoutAttributeValue checkoutAttributeValue)
        {
            _service.InsertAttributeValueAsync(checkoutAttributeValue).GetAwaiter().GetResult();
        }
    }
}

namespace Nop.Services.Vendors
{
    public interface IVendorAttributeService
    {
        IList<VendorAttribute> GetAllVendorAttributes();
        VendorAttributeValue GetVendorAttributeValueById(int vendorAttributeValueId);
        void InsertVendorAttribute(VendorAttribute vendorAttribute);
        void InsertVendorAttributeValue(VendorAttributeValue vendorAttributeValue);
    }

    public sealed class VendorAttributeServiceAdapter : IVendorAttributeService
    {
        private readonly IAttributeService<VendorAttribute, VendorAttributeValue> _service;

        public VendorAttributeServiceAdapter(IAttributeService<VendorAttribute, VendorAttributeValue> service)
        {
            _service = service;
        }

        public IList<VendorAttribute> GetAllVendorAttributes()
        {
            return _service.GetAllAttributesAsync().GetAwaiter().GetResult();
        }

        public VendorAttributeValue GetVendorAttributeValueById(int vendorAttributeValueId)
        {
            return _service.GetAttributeValueByIdAsync(vendorAttributeValueId).GetAwaiter().GetResult();
        }

        public void InsertVendorAttribute(VendorAttribute vendorAttribute)
        {
            _service.InsertAttributeAsync(vendorAttribute).GetAwaiter().GetResult();
        }

        public void InsertVendorAttributeValue(VendorAttributeValue vendorAttributeValue)
        {
            _service.InsertAttributeValueAsync(vendorAttributeValue).GetAwaiter().GetResult();
        }
    }
}

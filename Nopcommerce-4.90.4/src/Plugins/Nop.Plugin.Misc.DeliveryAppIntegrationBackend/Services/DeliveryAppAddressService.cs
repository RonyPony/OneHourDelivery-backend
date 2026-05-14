using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Data;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Represents a implementation for the delivery app order services.
    /// </summary>
    public sealed class DeliveryAppAddressService : IDeliveryAppAddressService
    {
        #region Fields

        private readonly IAddressAttributeParser _addressAttributeParser;
        private readonly IAddressService _addressService;
        private readonly IAddressAttributeService _addressAttributeService;
        private readonly IAddressGeoCoordinatesService _addressGeoCoordinatesService;
        private readonly ICustomerService _customerService;
        private readonly IRepository<AddressGeoCoordinatesMapping> _addressGeoCoordinatesMappingRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="DeliveryAppAddressService"/>.
        /// </summary>
        /// <param name="addressAttributeParser">An implementation of <see cref="IAddressAttributeParser"/>.</param>
        /// <param name="addressService">An implementation of <see cref="IAddressService"/>.</param>
        /// <param name="addressAttributeService">An implementation of <see cref="IAddressAttributeService"/>.</param>
        /// <param name="addressGeoCoordinatesService">An implementation of <see cref="IAddressGeoCoordinatesService"/>.</param>
        /// <param name="customerService">An implementation of <see cref="ICustomerService"/>.</param>
        /// <param name="addressGeoCoordinatesMappingRepository">An implementation of <see cref="IRepository{AddressGeoCoordinatesMapping}"/>.</param>
        public DeliveryAppAddressService(
            IAddressAttributeParser addressAttributeParser,
            IAddressService addressService,
            IAddressAttributeService addressAttributeService,
            IAddressGeoCoordinatesService addressGeoCoordinatesService,
            ICustomerService customerService,
            IRepository<AddressGeoCoordinatesMapping> addressGeoCoordinatesMappingRepository)
        {
            _addressAttributeParser = addressAttributeParser;
            _addressService = addressService;
            _addressAttributeService = addressAttributeService;
            _addressGeoCoordinatesService = addressGeoCoordinatesService;
            _customerService = customerService;
            _addressGeoCoordinatesMappingRepository = addressGeoCoordinatesMappingRepository;
        }

        #endregion

        #region Utilities

        private IList<string> GetAddressAttributeValueByVendorIdAndAttributeName(Address address, string addressAttributeName)
        {
            AddressAttribute addressAttribute = _addressAttributeService.GetAllAddressAttributes()
                .FirstOrDefault(x => x.Name.Equals(addressAttributeName));
            return _addressAttributeParser.ParseValues(address.CustomAttributes, addressAttribute.Id);
        }

        private void AddCustomAddressAttributesToFormCollectionDictionary(Dictionary<string, StringValues> formCollectionDictionary, string attributeName, string value)
        {
            var attribute = _addressAttributeService.GetAllAddressAttributes().FirstOrDefault(attr => attr.Name == attributeName);
            if (attribute is null) throw new ArgumentException($"Address attribute '{attributeName}' not found");
            formCollectionDictionary.Add(string.Format(NopCommonDefaults.AddressAttributeControlName, attribute.Id), value);
        }

        private string GetNewAddressCustomAttributes(DeliveryAppAddressModel model)
        {
            var addressCustomAttributes = new Dictionary<string, StringValues>();

            if (!string.IsNullOrWhiteSpace(model.Alias))
            {
                AddCustomAddressAttributesToFormCollectionDictionary(addressCustomAttributes, Defaults.AddressAliasAttribute.Name, model.Alias);
            }

            if (!string.IsNullOrWhiteSpace(model.ShippingSpecification))
            {
                AddCustomAddressAttributesToFormCollectionDictionary(addressCustomAttributes, Defaults.AddressShippingSpecificationAttribute.Name, model.ShippingSpecification);
            }

            return _addressAttributeParser.ParseCustomAddressAttributes(new FormCollection(addressCustomAttributes));
        }
        #endregion

        #region Methods

        ///<inheritdoc/>
        public double GetDistanceOnMeters(AddressGeoCoordinatesMapping address, decimal latitud, decimal longitud)
        {
            double calculation = (((Math.Acos(
                    Math.Sin((Convert.ToDouble(latitud) * Math.PI / 180)) *
                    Math.Sin((Convert.ToDouble(address.Latitude) * Math.PI / 180)) +
                    Math.Cos((Convert.ToDouble(latitud) * Math.PI / 180)) *
                    Math.Cos((Convert.ToDouble(address.Latitude)) * Math.PI / 180) *
                    Math.Cos(
                        ((Convert.ToDouble(longitud) - Convert.ToDouble(address.Longitude)) * Math.PI / 180)))
                ) * 180 / Math.PI) * 60 * 1.1515 * 1609.344);

            return calculation;
        }

        ///<inheritdoc/>
        public bool AreAddressesDuplicated(Address addressA, Address addressB)
        {
            if (addressA is null) throw new ArgumentNullException(nameof(addressA));
            if (addressB is null) throw new ArgumentNullException(nameof(addressB));

            return addressA.Email == addressB.Email
                && addressA.Address1 == addressB.Address1
                && addressA.Address2 == addressB.Address2
                && addressA.City == addressB.City
                && addressA.Company == addressB.Company
                && addressA.FirstName == addressB.FirstName
                && addressA.LastName == addressB.LastName
                && addressA.CountryId == addressB.CountryId
                && addressA.County == addressB.County
                && addressA.StateProvinceId == addressB.StateProvinceId
                && addressA.ZipPostalCode == addressB.ZipPostalCode
                && addressA.PhoneNumber == addressB.PhoneNumber
                && addressA.FaxNumber == addressB.FaxNumber;
        }

        ///<inheritdoc/>
        public IList<DeliveryAppAddressModel> GetAddressesByCustomerId(int id)
        {
            var customerAddresses = new List<DeliveryAppAddressModel>();
            IList<Address> customerAddessess = _customerService.GetAddressesByCustomerId(id);

            foreach (Address address in customerAddessess)
            {
                IList<string> addressAlias = GetAddressAttributeValueByVendorIdAndAttributeName(address, Defaults.AddressAliasAttribute.Name);
                IList<string> addressShippingSpecification = GetAddressAttributeValueByVendorIdAndAttributeName(address, Defaults.AddressShippingSpecificationAttribute.Name);
                AddressGeoCoordinatesMapping coordinates = _addressGeoCoordinatesService.GetAddressGeoCoordinates(address.Id);
                var deliveryAppAddress = address.ToModel<DeliveryAppAddressModel>();
                deliveryAppAddress.Latitude = coordinates?.Latitude;
                deliveryAppAddress.Longitude = coordinates?.Longitude;
                deliveryAppAddress.Alias = addressAlias.FirstOrDefault();
                deliveryAppAddress.ShippingSpecification = addressShippingSpecification.FirstOrDefault();
                customerAddresses.Add(deliveryAppAddress);
            }

            return customerAddresses;
        }

        ///<inheritdoc/>
        public int InsertNewCustomerAddress(int customerId, DeliveryAppAddressModel model)
        {
            Customer customer = _customerService.GetCustomerById(customerId);

            if (customer is null) throw new ArgumentException("Customer not found");

            var address = model.ToEntity<Address>();
            address.CustomAttributes = GetNewAddressCustomAttributes(model);
            _addressService.InsertAddress(address);
            _customerService.InsertCustomerAddress(customer, address);

            _addressGeoCoordinatesMappingRepository.Insert(new AddressGeoCoordinatesMapping
            {
                AddressId = address.Id,
                Latitude = model.Latitude.Value,
                Longitude = model.Longitude.Value
            });

            return address.Id;
        }

        ///<inheritdoc/>
        public int UpdateCustomerAddress(int customerId, DeliveryAppAddressModel addressModel)
        {         
            Customer customer = _customerService.GetCustomerById(customerId);

            if (customer is null)
                throw new ArgumentException("CustomerNotFound");

            Address address = _customerService.GetAddressesByCustomerId(customer.Id)
                .FirstOrDefault(address => address.Id == addressModel.Id);

            if (address is null)
                throw new ArgumentException("AddressNotFound");

            Address AddressMapper = addressModel.ToEntity<Address>();

            AddressMapper.CustomAttributes = GetNewAddressCustomAttributes(addressModel);
            AddressMapper.Id = address.Id;
            _addressService.UpdateAddress(AddressMapper);
      
            _addressGeoCoordinatesService.InsertAddressGeoCoordinates( new AddressGeoCoordinatesMapping { 
               AddressId = address.Id,
               Latitude = addressModel.Latitude.Value,
               Longitude = addressModel.Longitude.Value
            } , address.Id);

            return address.Id;
        }

        #endregion
    }
}

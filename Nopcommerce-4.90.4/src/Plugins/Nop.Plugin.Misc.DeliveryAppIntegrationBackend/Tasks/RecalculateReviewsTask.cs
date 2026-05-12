using Nop.Core;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Vendors;
using Nop.Data;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Logging;
using Nop.Services.ScheduleTasks;
using Nop.Services.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using VendorAttributeValue = Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers.VendorAttributeValueModel;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Tasks
{
    /// <summary>
    /// Represents the ScheduleTask that will be run to recalculate Vendor and Customer rating
    /// </summary>
    public sealed class RecalculateReviewsTask : IScheduleTask
    {
        #region Fields
        private readonly IVendorService _vendorService;
        private readonly IVendorReviewsService _vendorReviewsService;
        private readonly IVendorAttributeService _vendorAttributeService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ILogger _logger;
        private readonly VendorAttributesHelper _vendorAttributesHelper;
        private readonly ICustomerService _customerService;
        private readonly IRepository<CustomerRatingMapping> _customerMappingRepository;
        private readonly ICustomerAttributeService _customerAttributeService;
        private CustomerAttributeHelper _customerAttributeHelper;


        #endregion

        #region Ctor
        /// <summary>
        /// Creates an instance of <see cref="RecalculateVendorReviewTask"/>
        /// </summary>
        /// <param name="vendorService">An implementation of <see cref="IVendorService"/></param>
        /// <param name="vendorReviewsService">An implementation of <see cref="IVendorReviewsService"/></param>
        /// <param name="vendorAttributeService">An implementation of <see cref="IVendorAttributeService"/></param>
        /// <param name="genericAttributeService">An implementation of <see cref="IGenericAttributeService"/></param>
        /// <param name="logger">An implementation of <see cref="ILogger"/></param>
        /// <param name="customerService">An implementation of <see cref="ICustomerService"/></param>
        /// <param name="customerMappingRepository">An implementation of <see cref="IRepository{T}"/></param>
        /// <param name="customerAttributeService">An implementation of <see cref="ICustomerAttributeService"/></param>
        public RecalculateReviewsTask(IVendorService vendorService,
                                           IVendorReviewsService vendorReviewsService,
                                           IVendorAttributeService vendorAttributeService,
                                           IGenericAttributeService genericAttributeService,
                                           ILogger logger,
                                           ICustomerService customerService,
                                           IRepository<CustomerRatingMapping> customerMappingRepository,
                                           ICustomerAttributeService customerAttributeService)
        {
            _vendorService = vendorService;
            _vendorReviewsService = vendorReviewsService;
            _vendorAttributeService = vendorAttributeService;
            _genericAttributeService = genericAttributeService;
            _logger = logger;
            _vendorAttributesHelper = new VendorAttributesHelper();
            _customerService = customerService;
            _customerMappingRepository = customerMappingRepository;
            _customerAttributeService = customerAttributeService;
            _customerAttributeHelper = new CustomerAttributeHelper();
        }

        #endregion

        #region Methods

        ///<inheritdoc/>
        public Task ExecuteAsync()
        {  
            ExecuteRecalculateVendorReviewTask();
            ExecuteRecalculateCustomerReviewTask();
            return Task.CompletedTask;
        }

        private void ExecuteRecalculateVendorReviewTask()
        {
            try
            {
                IPagedList<Vendor> vendors = _vendorService.GetAllVendors();

                VendorAttribute vendorAttribute = _vendorAttributeService.GetAllVendorAttributes().FirstOrDefault(attr => attr.Name == Defaults.VendorRatingAttribute.Name);

                if (vendorAttribute == null)
                {
                    throw new ArgumentException($"{Defaults.VendorRatingAttribute} does not exists. Please verify");
                }

                foreach (Vendor vendor in vendors)
                {
                    decimal totalOfRatings = _vendorReviewsService.GetReviewsByVendor(vendor.Id).Count();

                    if (totalOfRatings == 0) continue;

                    decimal summarizedRating = _vendorReviewsService.GetReviewsByVendor(vendor.Id).Sum(x => x.Rating);
                    decimal rating = Math.Round(summarizedRating / totalOfRatings, 1);

                    var vendorGenericAttributes = _genericAttributeService.GetAttributesForEntity(vendor.Id, "Vendor")
                                                     .FirstOrDefault(x => x.Key == Defaults.GenericAttributeKeyForVendorAttributes);

                    if (vendorGenericAttributes == null)
                    {

                        VendorAttributeList vendorAttributeList = _vendorAttributesHelper.BuildVendorAttribute(vendorAttribute.Id.ToString(), rating.ToString());

                        _genericAttributeService.SaveAttribute(vendor, Defaults.GenericAttributeKeyForVendorAttributes, vendorAttributeList.ToXML());
                    }
                    else
                    {
                        UpdateGeneralRatingAttributeWhenExistsAttributes(vendorAttribute, rating, vendorGenericAttributes);
                    }

                }

            }
            catch(Exception e)
            {
                _logger.Error($"Error recalculating vendor reviews. {e.Message}", e);
            }
        }
        private void ExecuteRecalculateCustomerReviewTask()
        {
            try
            { 
                CustomerAttribute customerAttribute = _customerAttributeService.GetAllCustomerAttributes()
                .FirstOrDefault(atrr => atrr.Name == Defaults.CustomerRatingAttribute.Name);

                if (customerAttribute is null)
                {
                    throw new ArgumentException($"{Defaults.CustomerRatingAttribute.Name} does not exists. Please verify");
                }

                List<CustomerRatingModel> ratings = _customerMappingRepository.Table
                .GroupBy(rating => rating.RatedCustomerId)
                .Select(x => new CustomerRatingModel
                {
                    Id = x.Key,
                    Rating = x.Average(n => n.Rate)
                }).ToList();

                foreach (CustomerRatingModel ratingModel in ratings)
                {
                    var CustomerGenericAttribute = _genericAttributeService
                                                   .GetAttributesForEntity(ratingModel.Id, "Customer")
                                                   .FirstOrDefault(generic => generic.Key == "CustomCustomerAttributes");

                    if (CustomerGenericAttribute is null)
                    {
                        CustomerAttributeList customerAttributeList = _customerAttributeHelper
                            .BuildCustomerAttribute(customerAttribute.Id.ToString(), ratingModel.Rating.ToString());
                        var customer = _customerService.GetCustomerById(ratingModel.Id);
                        if (customer != null)
                        _genericAttributeService.SaveAttribute(customer, "CustomCustomerAttributes", customerAttributeList.ToXML());  
                    }
                    else
                    {
                        UpdateCustomerRatingAttributeWhenExists(customerAttribute, (decimal)ratingModel.Rating, CustomerGenericAttribute);
                    }
                }
            }
            catch(Exception e)
            {
                _logger.Error($"Error recalculating vendor reviews. {e.Message}", e);
            }
        }

        private void UpdateGeneralRatingAttributeWhenExistsAttributes(VendorAttribute vendorAttribute, decimal rating, Core.Domain.Common.GenericAttribute generalRatingAttribute)
        {
            List<VendorAttributeModel> deserializedAttributes = _vendorAttributesHelper.DeserializeAttributes(generalRatingAttribute.Value);

            foreach (var attribute in deserializedAttributes)
            {
                if (attribute.ID == vendorAttribute.Id.ToString())
                {
                    attribute.VendorAttributeValue.Value = rating.ToString();
                }
            }

            VendorAttributeList vendorAttributeList = new VendorAttributeList { Attributes = deserializedAttributes };

            generalRatingAttribute.Value = vendorAttributeList.ToXML();
            _genericAttributeService.UpdateAttribute(generalRatingAttribute);
        }

        private void UpdateCustomerRatingAttributeWhenExists(CustomerAttribute customerAttribute, decimal rating, GenericAttribute genericAttribute)
        {
            List<CustomerAttributeModel> deserializedAttributes = _customerAttributeHelper.DeserializeAttributes(genericAttribute.Value);

            foreach (var attribute in deserializedAttributes)
            {
                if (attribute.ID == customerAttribute.Id.ToString())
                {
                    attribute.CustomerAttributeValue.Value = rating.ToString();
                }
            }

            CustomerAttributeList customerAttributeList = new CustomerAttributeList { Attributes = deserializedAttributes };

            genericAttribute.Value = customerAttributeList.ToXML();
            _genericAttributeService.UpdateAttribute(genericAttribute);
        }
        #endregion
    }
}

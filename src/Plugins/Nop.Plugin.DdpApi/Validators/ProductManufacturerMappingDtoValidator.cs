
using Microsoft.AspNetCore.Http;
using Nop.Plugin.DdpApi.DTO.ProductManufacturerMappings;
using Nop.Plugin.DdpApi.Helpers;
using System.Collections.Generic;

namespace Nop.Plugin.DdpApi.Validators
{
    public class ProductManufacturerMappingDtoValidator : BaseDtoValidator<ProductManufacturerMappingsDto>
    {

        #region Constructors

        public ProductManufacturerMappingDtoValidator(IHttpContextAccessor httpContextAccessor, IJsonHelper jsonHelper, Dictionary<string, object> requestJsonDictionary) : base(httpContextAccessor, jsonHelper, requestJsonDictionary)
        {
            SetManufacturerIdRule();
            SetProductIdRule();
        }

        #endregion

        #region Private Methods

        private void SetManufacturerIdRule()
        {
            SetGreaterThanZeroCreateOrUpdateRule(p => p.ManufacturerId, "invalid manufacturer_id", "manufacturer_id");
        }

        private void SetProductIdRule()
        {
            SetGreaterThanZeroCreateOrUpdateRule(p => p.ProductId, "invalid product_id", "product_id");
        }

        #endregion

    }
}
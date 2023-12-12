using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Nop.Plugin.Api.DTO;

namespace Nop.Service.GrupoEstrellaSync.Models.RootObjets
{
    /// <summary>
    /// Represents ProductsRootObjectDto class from Nop.Api
    /// </summary>
    public class ProductsRootObjectDto : ISerializableObject
    {
        /// <summary>
        /// Constructor of this ProductsRootObjectDto
        /// </summary>
        public ProductsRootObjectDto()
        {
            Products = new List<ProductDto>();
        }

        /// <summary>
        /// Represents list of ProductsProducts in json object
        /// </summary>
        [JsonProperty("products")]
        public IList<ProductDto> Products { get; set; }

        /// <inheritdoc />
        public string GetPrimaryPropertyName() => "products";

        /// <inheritdoc />
        public Type GetPrimaryPropertyType() => typeof(ProductDto);
    }
}

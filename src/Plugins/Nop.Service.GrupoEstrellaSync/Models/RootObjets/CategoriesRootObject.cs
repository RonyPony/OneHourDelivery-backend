using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nop.Plugin.Api.DTO;

namespace Nop.Service.GrupoEstrellaSync.Models.RootObjets
{
    /// <summary>
    /// Represents CategoriesRootObject class from Nop.Api
    /// </summary>
    public class CategoriesRootObject : ISerializableObject
    {
        /// <summary>
        /// Constructor of this CategoriesRootObject
        /// </summary>
        public CategoriesRootObject()
        {
            Categories = new List<CategoryDto>();
        }
        /// <summary>
        /// Represents list of Categories in json object
        /// </summary>
        [JsonProperty("categories")]
        public IList<CategoryDto> Categories { get; set; }
        /// <summary>
        /// Get Primary Property Name
        /// </summary>
        /// <returns>categories</returns>
        public string GetPrimaryPropertyName() => "categories";

        /// <summary>
        /// Get Primary Property Type
        /// </summary>
        /// <returns> <see cref="Type"/> </returns>
        public Type GetPrimaryPropertyType() => typeof(CategoryDto);

        /// <summary>
        /// Is an explicit operator for  CategoriesRootObject
        /// </summary>
        /// <param name="v"> <see cref="CategoriesRootObject"/> </param>
        public static explicit operator CategoriesRootObject(Task<CategoriesRootObject> v)
        {
            throw new NotImplementedException();
        }
    }
}

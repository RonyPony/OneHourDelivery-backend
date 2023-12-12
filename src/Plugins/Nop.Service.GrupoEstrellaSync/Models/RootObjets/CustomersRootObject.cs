using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Nop.Plugin.Api.DTO;

namespace Nop.Service.GrupoEstrellaSync.Models.RootObjets
{
    /// <summary>
    /// Represents CustomersRootObjectDto class from Nop.Api
    /// </summary>
    public class CustomersRootObject : ISerializableObject
    {
        
        /// <summary>
        /// Constructor of this CustomersRootObjectDto
        /// </summary>
        public CustomersRootObject()
        {
            Customers = new List<CustomerDto>();
        }

        [JsonProperty("customers")]
        public IList<CustomerDto> Customers { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "customers";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(CustomerDto);
        }
    }
}

using Newtonsoft.Json;

namespace Nop.Service.GrupoEstrellaSync.Models
{
    /// <summary>
    /// Represents BaseDto class of nop.Api
    /// </summary>
    public abstract class BaseDto
    {
        
        /// Represents ID property of BaseDto class of nop.Api
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}

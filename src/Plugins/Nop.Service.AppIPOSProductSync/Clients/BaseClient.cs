using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Nop.Plugin.Api.DTO;
using Nop.Plugin.Api.DTO.Base;
using Nop.Service.AppIPOSSync.Entities;
using Nop.Service.AppIPOSSync.Helpers;

namespace Nop.Service.AppIPOSSync.Clients
{
    /// <summary>
    /// Base class used for clients, contains basic functionality that synching processes normally need
    /// </summary>
    /// <typeparam name="TEntity">The entity in the Erp database</typeparam>
    /// <typeparam name="TDto">The DTO sent to/received from Nop.Api</typeparam>
    /// <typeparam name="TRootObject">The Dto root object sent to/received from Nop.Api</typeparam>
    public abstract class BaseClient<TEntity, TDto, TRootObject> where TEntity : class where TDto : BaseDto where TRootObject : ISerializableObject
    {
        protected readonly AppIposContext Context;
        protected readonly string ApiControllerRoute;
        protected readonly HttpClient Client;

        protected BaseClient(AppIposContext context, string apiControllerRoute)
        {
            Context = context;
            ApiControllerRoute = apiControllerRoute;
            IConfiguration configuration = ConfigurationHelper.GetConfiguration();

            string apiUrl = configuration.GetSection("Settings")["NopCommerceApiURL"];

            Client = new HttpClient
            {
                BaseAddress = new Uri(apiUrl),
                DefaultRequestHeaders =
                {
                    Authorization = new AuthenticationHeaderValue(TokenHelper.AuthenticationScheme, TokenHelper.ApiRequestToken)
                }
            };
        }

        protected virtual string GetQueryString(object obj)
        {
            IEnumerable<string> properties = from p in obj.GetType().GetProperties()
                where p.GetValue(obj, null) != null
                select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return string.Join("&", properties.ToArray());
        }

        protected virtual async Task<TRootObject> CreateAsync(TDto model)
        {
            string json = JsonConvert.SerializeObject(model);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await Client.PostAsync(ApiControllerRoute, stringContent);

            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"{response}. Error creating {nameof(TEntity)} with StatusCode {response.StatusCode}");
            }

            string responseJson = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TRootObject>(responseJson);
        }

        protected virtual Task<TRootObject> GetAllFromNopCommerceAsync() => throw new NotImplementedException();

        protected virtual async Task<TRootObject> GetByNopCommerceIdAsync(int id)
        {
            string requestUri = $"/{ApiControllerRoute}/{id}";

            HttpResponseMessage response = await Client.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error getting {nameof(TRootObject)}. {requestUri} call with StatusCode {response.StatusCode}");
            }

            string json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TRootObject>(json);
        }

        protected virtual TDto ToDto(TEntity tableModel) => throw new NotImplementedException();

        protected virtual List<TEntity> GetAllFromErp() => Context.Set<TEntity>().ToList();

        protected virtual bool ExistsInNopCommerce(ref TRootObject nopCommerceObjectList, string erpObjectName) => throw new NotImplementedException();

        /// <summary>
        /// Method for synching
        /// </summary>
        public virtual Task Sync() => throw new NotImplementedException();

        protected virtual async Task<TDto> UpdateOnNopCommerceAsync(TDto model)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await Client.PutAsync($"{ApiControllerRoute}/{model.Id}", content);

            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            string jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TDto>(jsonResponse); ;
        }

        protected virtual async Task<HttpStatusCode> DeleteOnNopCommerceAsync(string id)
        {
            HttpResponseMessage response = await Client.DeleteAsync($"{ApiControllerRoute}/{id}");

            return response.StatusCode;
        }
    }
}

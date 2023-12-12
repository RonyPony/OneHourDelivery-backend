using System;
using System.Net.Http;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Nop.Service.AppIPOSSync.Models;

namespace Nop.Service.AppIPOSSync.Helpers
{
    /// <summary>
    /// Class used to get/set the token used to make requests to Nop.Api controllers
    /// </summary>
    public class TokenHelper
    {
        private static string _token;
        private static string _authScheme;

        /// <summary>
        /// Gets the token used to make requests to Nop.Api controllers
        /// </summary>
        public static string ApiRequestToken
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_token))
                    return _token;

                _token = GenerateToken();

                return _token;
            }
        }

        /// <summary>
        /// Gets the authentication scheme used to make requests to Nop.Api controllers
        /// </summary>
        public static string AuthenticationScheme
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_authScheme))
                    return _authScheme;

                GenerateToken();

                return _authScheme;
            }
        }

        private static string GenerateToken()
        {
            IConfiguration configuration = ConfigurationHelper.GetConfiguration();

            string apiUrl = configuration.GetSection("Settings")["NopCommerceApiURL"];

            var client = new HttpClient
            {
                BaseAddress = new Uri(apiUrl)
            };

            string userName = configuration.GetSection("ApiAuthCredentials")["Username"];
            string password = configuration.GetSection("ApiAuthCredentials")["Password"];

            var result = client.GetAsync($"token?Username={userName}&Password={password}").Result;

            result.EnsureSuccessStatusCode();

            string json = result.Content.ReadAsStringAsync().Result;

            TokenResponseModel tokenResponse = JsonSerializer.Deserialize<TokenResponseModel>(json);

            _authScheme = tokenResponse.token_type;

            return tokenResponse.access_token;
        }
    }
}

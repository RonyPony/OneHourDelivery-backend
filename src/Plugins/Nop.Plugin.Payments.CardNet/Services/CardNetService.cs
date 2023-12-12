using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Nop.Core;
using Nop.Data;
using Nop.Plugin.Payments.CardNet.Domains;
using Nop.Plugin.Payments.CardNet.Helpers;
using Nop.Plugin.Payments.CardNet.Models;
using Nop.Services.Localization;

namespace Nop.Plugin.Payments.CardNet.Services
{
    public class CardNetService
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly CardNetSettings _cardNetSettings;
        private readonly IRepository<CardNetTransactionLog> _transactionLogRepository;

        #endregion

        #region Ctor

        public CardNetService(ILocalizationService localizationService, CardNetSettings cardNetSettings,
            IRepository<CardNetTransactionLog> transactionLogRepository)
        {
            _localizationService = localizationService;
            _cardNetSettings = cardNetSettings;
            _transactionLogRepository = transactionLogRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Check whether the plugin is configured
        /// </summary>
        /// <param name="settings">Plugin settings</param>
        /// <returns>Boolean value representing whether the plugin is configured or not</returns>
        public bool IsPluginConfigured(CardNetSettings settings)
        {
            //client id and url or alternative url are required to request services
            return !string.IsNullOrWhiteSpace(settings.Url) &&
                   !string.IsNullOrWhiteSpace(settings.PwCheckoutScriptUrl) && 
                   !string.IsNullOrWhiteSpace(settings.PublicApiKey) && 
                   !string.IsNullOrWhiteSpace(settings.PrivateApiKey);
        }

        /// <summary>
        /// Sends purchase to CardNet API
        /// </summary>
        /// <param name="model">Purchase model sent to CardNet API</param>
        /// <returns>CardNet API result and errors (if any)</returns>
        public (CardNetPurchaseResult result, string errorMessage) ProcessPayment(CardNetPurchaseModel model)
        {
            if (!IsPluginConfigured(_cardNetSettings))
            {
                throw new NopException("Plugin not configured");
            }

            try
            {
                using HttpClient client = new HttpClient
                {
                    DefaultRequestHeaders =
                    {
                        Authorization = new AuthenticationHeaderValue("Basic", _cardNetSettings.PrivateApiKey)
                    }
                };

                string json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                Task<HttpResponseMessage> result = client.PostAsync(_cardNetSettings.Url, content);
                result.Wait();

                Task<string> jsonResult = result.Result.Content.ReadAsStringAsync();
                jsonResult.Wait();

                var purchaseResult = JsonConvert.DeserializeObject<CardNetPurchaseResult>(jsonResult.Result);

                if (!result.Result.IsSuccessStatusCode)
                {
                    var logEntry = new CardNetTransactionLog
                    {
                        ResultType = result.Result.ReasonPhrase,
                        ErrorMessage = $"Got status code {result.Result.StatusCode}",
                        FullException =
                            $"{string.Join(" ", purchaseResult.Errors.Select((err) => $"Error code: {err.ErrorCode}. Message: {err.Message}"))}.",
                        DateLogged = DateTime.Now
                    };

                    Log(logEntry);

                    var resultWithErrors = new CardNetPurchaseResult
                    {
                        Errors = purchaseResult.Errors
                    };

                    return (resultWithErrors, default);
                }

                return (purchaseResult, default);
            }
            catch (Exception e)
            {
                var logEntry = new CardNetTransactionLog
                {
                    ResultType = CardNetStatus.Error,
                    ErrorMessage = e.Message,
                    FullException = $"Source: {e.Source}. Message: {e.Message}. Stack trace: {e.StackTrace}. Inner exception: {e.InnerException?.Message}",
                    DateLogged = DateTime.Now
                };

                Log(logEntry);

                return (default, _localizationService.GetResource("Plugins.Payments.CardNet.GenericTransactionError"));
            }
        }

        /// <summary>
        /// Gets PWCheckout script that will be rendered
        /// </summary>
        /// <returns></returns>
        public string GetScript()
        {
            if (!IsPluginConfigured(_cardNetSettings))
            {
                throw new NopException("Plugin not configured");
            }

            var parameters = new Dictionary<string, string>
            {
                ["key"] = _cardNetSettings.PublicApiKey
            };

            var scriptUrl = QueryHelpers.AddQueryString(_cardNetSettings.PwCheckoutScriptUrl, parameters);

            return $@"<script src=""{scriptUrl}""></script>";
        }

        /// <summary>
        /// Logs an entry to CardNet transaction log
        /// </summary>
        /// <param name="logEntry">Transaction log entry</param>
        public void Log(CardNetTransactionLog logEntry)
        {
            if (logEntry == null)
                throw new ArgumentNullException(nameof(logEntry));

            _transactionLogRepository.Insert(logEntry);
        }

        #endregion
    }
}

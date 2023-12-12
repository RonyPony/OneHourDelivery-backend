using Newtonsoft.Json;
using Nop.Core;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Shipping;
using Nop.Plugin.Synchronizers.SAPOrders.Models;
using Nop.Services.Logging;
using Nop.Services.Orders;
using Nop.Services.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Nop.Plugin.Synchronizers.SAPOrders.Tasks
{
    /// <summary>
    /// Represents the ScheduleTask that will be run when synching orders status from SAP to NopCommerce
    /// </summary>
    public class SapOrdersSyncTask : IScheduleTask
    {
        #region Fields

        private readonly IOrderService _orderService;
        private readonly ILogger _logger;
        private readonly SapOrdersSyncSettings _sapOrdersSyncSettings;

        #endregion

        #region Ctor

        /// <summary>
        /// Represents the constructor for this class.
        /// </summary>
        /// <param name="orderService">Implementation of <see cref="IOrderService"/></param>
        /// <param name="logger">Implementation of <see cref="ILogger"/></param>
        /// <param name="sapOrdersSyncSettings">Implementation of <see cref="SapOrdersSyncSettings"/></param>
        public SapOrdersSyncTask(IOrderService orderService, ILogger logger,
            SapOrdersSyncSettings sapOrdersSyncSettings)
        {
            _orderService = orderService;
            _logger = logger;
            _sapOrdersSyncSettings = sapOrdersSyncSettings;
        }

        #endregion

        #region Utilities

        private async System.Threading.Tasks.Task ValidateSapOrderStatus(Order order)
        {
            try
            {
                using HttpClient client = new HttpClient
                {
                    DefaultRequestHeaders =
                    {
                        Authorization = new AuthenticationHeaderValue(_sapOrdersSyncSettings.AuthenticationScheme, _sapOrdersSyncSettings.ApiToken)
                    }
                };

                HttpResponseMessage
                    response = await client.GetAsync(string.Format(_sapOrdersSyncSettings.ApiGetUrl, order.Id));

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var sapInvoiceResponse = JsonConvert.DeserializeObject<SapInvoiceResponse>(jsonResponse);

                    if (sapInvoiceResponse.Extra.Any())
                    {
                        order.OrderStatusId = (int)OrderStatus.Complete;
                        order.ShippingStatusId = (int)ShippingStatus.Delivered;
                        _orderService.UpdateOrder(order);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error($"Error validating SAP order status. {e.Message}", e);
                throw new NopException($"Error validating SAP order status. {e.Message}");
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method that will be executed when the Task is run.
        /// </summary>
        public void Execute()
        {
            IPagedList<Order> orders = _orderService.SearchOrders(osIds: new List<int> { (int)OrderStatus.Processing });

            foreach (Order order in orders)
            {
                System.Threading.Tasks.Task orderStatusTask = ValidateSapOrderStatus(order);
                orderStatusTask.Wait();
            }
        }

        #endregion
    }
}

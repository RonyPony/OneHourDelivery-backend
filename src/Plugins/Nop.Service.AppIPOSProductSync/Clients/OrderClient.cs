using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Nop.Plugin.Api.DTO.Customers;
using Nop.Plugin.Api.DTO.Orders;
using Nop.Plugin.Api.Models.OrdersParameters;
using Nop.Service.AppIPOSSync.Entities;
using Nop.Service.AppIPOSSync.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nop.Service.AppIPOSSync.Clients
{
    /// <summary>
    /// This class contains all the methods to be able to sync orders using controllers of the Nop.Plugin.Api plugin 
    /// </summary>
    public class OrderClient : BaseClient<Order, OrderDto, OrdersRootObject>
    {
        private readonly CustomerClient _customerClient;
        private readonly IConfigurationSection _configurationSection = ConfigurationHelper.GetConfiguration().GetSection("Settings");
        private readonly IConfigurationSection _orderSyncSettings = ConfigurationHelper.GetConfiguration().GetSection("OrderSyncSettings");

        /// <summary>
        /// Constructor of the class
        /// </summary>
        public OrderClient(AppIposContext context, CustomerClient customerClient) : base(context, "api/orders") 
            => _customerClient = customerClient;

        protected override async Task<OrdersRootObject> GetAllFromNopCommerceAsync()
        {
            var parametersModel = new OrdersParametersModel
            {
                Limit = 1000
            };

            string requestUri = "api/orders";

            requestUri += "?" + GetQueryString(parametersModel);

            HttpResponseMessage response = await Client.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"{response}. {requestUri} call with StatusCode {response.StatusCode}");

            string json = await response.Content.ReadAsStringAsync();

            OrdersRootObject result = JsonConvert.DeserializeObject<OrdersRootObject>(json);
            return result;
        }

        private int GetSessionId()
        {
            string sessionId = Context.Customizes.FirstOrDefault(c => c.Field == _orderSyncSettings["CurrentSessionSearch"])
                ?.Value;

            int.TryParse(sessionId, out int result);

            return result;
        }

        private int GetOpeningCash()
        {
            string sessionId = Context.Customizes.FirstOrDefault(c => c.Field == _orderSyncSettings["CurrentSessionSearch"])?.Value;

            int.TryParse(sessionId, out int convertedSessionId);

            int? result = Context.OpeningCashes
                .Where(o => o.CashRegister == 1 && !o.Closed && o.Session == convertedSessionId)
                .OrderByDescending(o => o.Id)
                .FirstOrDefault()?
                .Id;

            return result.GetValueOrDefault();
        }

        private async Task<CustomerDto> GetCustomersAsync(int idCustomer)
        {
            string path = $"api/customers/{@idCustomer}";

            HttpResponseMessage response = await Client.GetAsync(path);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"{response}  {path} " + "   " + path + " call with StatusCode not Success.");
            }

            Task<string> json = response.Content.ReadAsStringAsync();

            CustomersRootObject result = JsonConvert.DeserializeObject<CustomersRootObject>(json.Result);

            return result.Customers.FirstOrDefault();
        }

        private async Task<Entities.Region> GetRegionIdAsync(int idCustomer)
        {
            CustomerDto customers = await GetCustomersAsync(idCustomer);

            string region = customers.CustomerInfo.CustomerAttributes
                .FirstOrDefault(x => x.Name == "REGION")?.Values
                .FirstOrDefault(x => x.IsPreSelected)?.Name;

            Entities.Region regionResult = Context.Regions.FirstOrDefault(x => x.Name == region);

            return regionResult;
        }

        private Order ToOrder(OrderDto orderDto)
        {
            int regionId = GetRegionIdAsync(orderDto.Customer.Id).Result == null
                ? Convert.ToInt32(_configurationSection["DefaultRegionId"])
                : GetRegionIdAsync(orderDto.Customer.Id).Result.Id;

            int.TryParse(_customerClient.GetErpClientIdByEmail(orderDto.Customer.Email), out int erpClientId);

            Order order = new Order
            {
                OpeningDate = (DateTime) orderDto.CreatedOnUtc,
                Client = erpClientId,
                ClientName = $"{orderDto.Customer.FirstName} {orderDto.Customer.LastName}",
                Name = orderDto.Customer.FirstName,
                SessionId = GetSessionId(),
                CashRegister = Convert.ToInt32(_orderSyncSettings["CashRegister"]),
                OpeningCashId = GetOpeningCash(),
                InProcess = Convert.ToBoolean(_orderSyncSettings["InProcess"]),
                RegisteredUser = Convert.ToInt32(_orderSyncSettings["RegisteredUser"]),
                Annulled = Convert.ToBoolean(_orderSyncSettings["Annulled"]),
                Applied = Convert.ToBoolean(_orderSyncSettings["Applied"]),
                Closed = Convert.ToBoolean(_orderSyncSettings["Closed"]),
                TableId = Convert.ToInt32(_orderSyncSettings["TableID"]),
                StationId = Convert.ToInt32(_orderSyncSettings["StationID"]),
                ClientBalance = Convert.ToDecimal(_orderSyncSettings["ClientBalance"]),
                TipsCharge = Convert.ToBoolean(_orderSyncSettings["TipsCharge"]),
                DiscountPercent = Convert.ToDecimal(_orderSyncSettings["DiscountPercent"]),
                ExtraCharge = Convert.ToBoolean(_orderSyncSettings["ExtraCharge"]),
                TaxCharge = Convert.ToBoolean(_orderSyncSettings["TaxCharge"]),
                DiscountOn = Convert.ToInt32(_orderSyncSettings["DiscountOn"]),
                PersonsQty = Convert.ToInt32(_orderSyncSettings["PersonsQty"]),
                SpecialDiscount = Convert.ToDecimal(_orderSyncSettings["SpecialDiscount"]),
                IsDetail = Convert.ToBoolean(_orderSyncSettings["IsDetail"]),
                IsCreditPayment = Convert.ToBoolean(_orderSyncSettings["IsCreditPayment"]),
                SendToBill = Convert.ToBoolean(_orderSyncSettings["SendToBill"]),
                IsRestaurant = Convert.ToBoolean(_orderSyncSettings["IsRestaurant"]),
                ToTheRoom = Convert.ToBoolean(_orderSyncSettings["ToTheRoom"]),
                IdorderRoom = Convert.ToInt32(_orderSyncSettings["IDOrderRoom"]),
                RegionId = regionId
            };

            return order;
        }

        private List<OrderDetail> ToOrderDetail(OrderDto orderDto, int erpOrderId)
        {
            List<OrderDetail> resultList = new List<OrderDetail>();
            foreach (var item in orderDto.OrderItems)
            {
                Product erpProduct = Context.Products.FirstOrDefault(p => p.Code == item.Product.Sku);
                decimal orderItemTax = (item.UnitPriceInclTax - item.UnitPriceExclTax).GetValueOrDefault();

                OrderDetail result = new OrderDetail
                {
                    OrderId = erpOrderId,
                    Code = item.Product.Sku,
                    Price = item.Product.Price.GetValueOrDefault(),
                    Tax1 = orderItemTax,
                    Quantity = (decimal)item.Quantity,
                    ProductDiscount = item.DiscountAmountInclTax.GetValueOrDefault(),
                    Name = item.Product.Name,
                    QuantityHours = Convert.ToDecimal(_orderSyncSettings.GetSection("QuantityHours").Value),
                    Cost = (decimal)erpProduct?.Cost,
                    CustomerDiscount = Convert.ToDecimal(_orderSyncSettings.GetSection("CustomerDiscount").Value),
                    AmountByService = Convert.ToDecimal(_orderSyncSettings.GetSection("AmountByService").Value),
                    Tax2 = Convert.ToDecimal(_orderSyncSettings.GetSection("Tax2").Value),
                    Tax3 = Convert.ToDecimal(_orderSyncSettings.GetSection("Tax3").Value),
                    IsExtra = Convert.ToBoolean(_orderSyncSettings.GetSection("IsExtra").Value),
                    HasSideOrders = Convert.ToBoolean(_orderSyncSettings.GetSection("HasSideOrders").Value),
                    PlayTimeId = Convert.ToInt32(_orderSyncSettings.GetSection("PlayTimeID").Value),
                    Promotion = Convert.ToBoolean(_orderSyncSettings.GetSection("Promotion").Value),
                    IsOpenBar = Convert.ToBoolean(_orderSyncSettings.GetSection("IsOpenBar").Value),
                    IsSpecialDrink = Convert.ToBoolean(_orderSyncSettings.GetSection("IsSpecialDrink").Value),
                    IsGame = Convert.ToBoolean(_orderSyncSettings.GetSection("IsGame").Value),
                    UnCommandedQty = Convert.ToDecimal(_orderSyncSettings.GetSection("UnCommandedQty").Value),
                    CommissionVendor = Convert.ToDecimal(_orderSyncSettings.GetSection("CommissionVendor").Value),
                    ProductId = erpProduct?.Id,
                    VendorId = Convert.ToInt32(_orderSyncSettings.GetSection("VendorID").Value),
                    ProductDiscountPercent = orderItemTax / item.PriceInclTax.GetValueOrDefault(),
                    CustomerDiscountPercent = Convert.ToInt32(_orderSyncSettings.GetSection("CustomerDiscountPercent").Value),
                    AmountByServicePercent = Convert.ToInt32(_orderSyncSettings.GetSection("AmountByServicePercent").Value),
                    Tax1Percent = Convert.ToInt32(_orderSyncSettings.GetSection("Tax1Percent").Value),
                    Tax2Percent = Convert.ToInt32(_orderSyncSettings.GetSection("Tax2Percent").Value),
                    Tax3Percent = Convert.ToInt32(_orderSyncSettings.GetSection("Tax3Percent").Value),
                    WorkAreaId = Convert.ToInt32(_orderSyncSettings.GetSection("WorkAreaId").Value),
                    IsOffice = Convert.ToBoolean(_orderSyncSettings.GetSection("IsOffice").Value),
                    GiftTableId = Convert.ToInt32(_orderSyncSettings.GetSection("GiftTableId").Value),
                    CalcRentType = Convert.ToInt32(_orderSyncSettings.GetSection("CalcRentType").Value),
                    RemissionId = Convert.ToInt32(_orderSyncSettings.GetSection("RemissionID").Value),
                    IsRestaurant = Convert.ToBoolean(_orderSyncSettings.GetSection("IsRestaurant").Value),
                    WarehouseId = Convert.ToInt32(_orderSyncSettings.GetSection("WarehouseID").Value),
                    LastUpdated = Convert.ToBoolean(_orderSyncSettings.GetSection("LastUpdated").Value),
                    Unit = erpProduct.Unit
                };

                resultList.Add(result);
            }

            return resultList;
        }

        private int CreateOrder(Order order)
        {
            Context.Orders.Add(order);
            Context.SaveChanges();

            return order.Id;
        }

        private void CreateOrderDetail(List<OrderDetail> listOrderDetails)
        {
            foreach (var orderItem in listOrderDetails)
            {
                Context.OrderDetails.Add(orderItem);
                Context.SaveChanges();
            }
        }

        private async Task<bool> OrderExistAsync(int nopCommerceOrderNumber)
        {
            string requestUri = "erp/Order/OrderExists?nopCommerceOrderId=" + nopCommerceOrderNumber;

            HttpResponseMessage response = await Client.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"{response}. {requestUri} call with StatusCode {response.StatusCode}");

            string json = await response.Content.ReadAsStringAsync();

            bool result = JsonConvert.DeserializeObject<bool>(json);

            return result;
        }

        private void SaveOrderIdErpOrderIdMapping(int nopId, int erpId)
        {
            string path = $"erp/orders?OrderId={nopId}&ErpOrderId={erpId}";

            HttpResponseMessage response = Client.PostAsync(path, new StringContent("")).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"ERROR saving client ERPcode in NopCommerce : {response.RequestMessage} at {DateTime.Now}");
            }
        }

        /// <summary>
        /// Method for sync Orders
        /// </summary>
        public override async Task Sync()
        {
            if (IsThereAnOpenClosing() && IsThereAnActiveCashOpening())
            {
                OrdersRootObject nopCommerceOrders = await GetAllFromNopCommerceAsync();

                foreach (OrderDto nopCommerceOrder in nopCommerceOrders.Orders)
                {
                    if (!OrderExistAsync(nopCommerceOrder.Id).Result)
                    {
                        Order order = ToOrder(nopCommerceOrder);

                        int erpOrderId = CreateOrder(order);

                        CreateOrderDetail(ToOrderDetail(nopCommerceOrder, erpOrderId));

                        SaveOrderIdErpOrderIdMapping(nopCommerceOrder.Id, erpOrderId);
                    }
                }
            }
        }

        private bool IsThereAnOpenClosing()
        {
            Customize result =
                Context.Customizes.FirstOrDefault(c => c.Field == _orderSyncSettings["OpenClosingSearch"]);

            return result?.Value == _orderSyncSettings["OpenClosingValidationValue1"] ||
                   result?.Value == _orderSyncSettings["OpenClosingValidationValue2"];
        }

        private bool IsThereAnActiveCashOpening()
        {
            string sessionId = Context.Customizes.FirstOrDefault(c => c.Field == _orderSyncSettings["CurrentSessionSearch"])?.Value;

            int.TryParse(sessionId, out int convertedSessionId);

            int? openingCashId = Context.OpeningCashes
                .Where(o => o.CashRegister == 1 && !o.Closed && o.Session == convertedSessionId)
                .OrderByDescending(o => o.Id)
                .FirstOrDefault()?
                .Id;

            return openingCashId != null;
        }
    }
}
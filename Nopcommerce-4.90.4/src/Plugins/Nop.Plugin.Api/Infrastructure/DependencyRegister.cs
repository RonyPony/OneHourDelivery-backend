using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Orders;
using Nop.Core.Infrastructure;
using Nop.Plugin.Api.Configuration;
using Nop.Plugin.Api.Converters;
using Nop.Plugin.Api.Factories;
using Nop.Plugin.Api.Helpers;
using Nop.Plugin.Api.JSON.Serializers;
using Nop.Plugin.Api.ModelBinders;
using Nop.Plugin.Api.Services;
using Nop.Plugin.Api.Validators;

namespace Nop.Plugin.Api.Infrastructure
{
    public class DependencyRegister : INopStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            RegisterPluginServices(services);
            RegisterModelBinders(services);
            var apiConfiguration = new ApiConfiguration();

            configuration.GetSection("Api").Bind(apiConfiguration);

            services.AddSingleton(apiConfiguration);
        }

        public void Configure(IApplicationBuilder application)
        {
        }

        private void RegisterModelBinders(IServiceCollection services)
        {
            services.AddScoped(typeof(ParametersModelBinder<>));
            services.AddScoped(typeof(JsonModelBinder<>));
        }

        private void RegisterPluginServices(IServiceCollection services)
        {
            services.AddScoped<ICustomerApiService, CustomerApiService>();
            services.AddScoped<ICategoryApiService, CategoryApiService>();
            services.AddScoped<IProductApiService, ProductApiService>();
            services.AddScoped<IProductCategoryMappingsApiService, ProductCategoryMappingsApiService>();
            services.AddScoped<IProductManufacturerMappingsApiService, ProductManufacturerMappingsApiService>();
            services.AddScoped<IOrderApiService, OrderApiService>();
            services.AddScoped<IShoppingCartItemApiService, ShoppingCartItemApiService>();
            services.AddScoped<IOrderItemApiService, OrderItemApiService>();
            services.AddScoped<IProductAttributesApiService, ProductAttributesApiService>();
            services.AddScoped<IProductPictureService, ProductPictureService>();
            services.AddScoped<IProductAttributeConverter, ProductAttributeConverter>();
            services.AddScoped<ISpecificationAttributeApiService, SpecificationAttributesApiService>();
            services.AddScoped<INewsLetterSubscriptionApiService, NewsLetterSubscriptionApiService>();
            services.AddScoped<IManufacturerApiService, ManufacturerApiService>();

            services.AddScoped<IMappingHelper, MappingHelper>();
            services.AddScoped<ICustomerRolesHelper, CustomerRolesHelper>();
            services.AddScoped<IJsonHelper, JsonHelper>();
            services.AddScoped<IDTOHelper, DTOHelper>();

            services.AddScoped<IJsonFieldsSerializer, JsonFieldsSerializer>();
            services.AddScoped<IFieldsValidator, FieldsValidator>();

            services.AddScoped<IObjectConverter, ObjectConverter>();
            services.AddScoped<IApiTypeConverter, ApiTypeConverter>();

            services.AddScoped<IFactory<Category>, CategoryFactory>();
            services.AddScoped<IFactory<Product>, ProductFactory>();
            services.AddScoped<IFactory<Customer>, CustomerFactory>();
            services.AddScoped<IFactory<Address>, AddressFactory>();
            services.AddScoped<IFactory<Order>, OrderFactory>();
            services.AddScoped<IFactory<ShoppingCartItem>, ShoppingCartItemFactory>();
            services.AddScoped<IFactory<Manufacturer>, ManufacturerFactory>();

            services.AddScoped<Maps.IJsonPropertyMapper, Maps.JsonPropertyMapper>();

            services.AddHttpContextAccessor();
            services.AddSingleton<Dictionary<string, object>>();

        }

        public int Order => int.MaxValue;
    }
}

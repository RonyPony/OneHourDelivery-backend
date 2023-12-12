using Autofac;
using Microsoft.AspNetCore.Http;
using Nop.Core.Configuration;
using Nop.Core.Domain.Catalog;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Plugin.DdpApi.Converters;
using Nop.Plugin.DdpApi.Factories;
using Nop.Plugin.DdpApi.Helpers;
using Nop.Plugin.DdpApi.JSON.Serializers;
using Nop.Plugin.DdpApi.ModelBinders;
using Nop.Plugin.DdpApi.Services;
using Nop.Plugin.DdpApi.Validators;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.DdpApi.Infrastructure
{

    public class DependencyRegister : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            RegisterPluginServices(builder);

            RegisterModelBinders(builder);
        }

        private void RegisterModelBinders(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(ParametersModelBinder<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(JsonModelBinder<>)).InstancePerLifetimeScope();
        }

        private void RegisterPluginServices(ContainerBuilder builder)
        {
            builder.RegisterType<ProductApiService>().As<IProductApiService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductCategoryMappingsApiService>().As<IProductCategoryMappingsApiService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductManufacturerMappingsApiService>().As<IProductManufacturerMappingsApiService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductAttributesApiService>().As<IProductAttributesApiService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductPictureService>().As<IProductPictureService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductAttributeConverter>().As<IProductAttributeConverter>().InstancePerLifetimeScope();

            builder.RegisterType<MappingHelper>().As<IMappingHelper>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerRolesHelper>().As<ICustomerRolesHelper>().InstancePerLifetimeScope();
            builder.RegisterType<JsonHelper>().As<IJsonHelper>().InstancePerLifetimeScope();
            builder.RegisterType<DTOHelper>().As<IDTOHelper>().InstancePerLifetimeScope();
            
            builder.RegisterType<JsonFieldsSerializer>().As<IJsonFieldsSerializer>().InstancePerLifetimeScope();

            builder.RegisterType<FieldsValidator>().As<IFieldsValidator>().InstancePerLifetimeScope();

            builder.RegisterType<ObjectConverter>().As<IObjectConverter>().InstancePerLifetimeScope();
            builder.RegisterType<ApiTypeConverter>().As<IApiTypeConverter>().InstancePerLifetimeScope();

            builder.RegisterType<ProductFactory>().As<IFactory<Product>>().InstancePerLifetimeScope();

            builder.RegisterType<Maps.JsonPropertyMapper>().As<Maps.IJsonPropertyMapper>().InstancePerLifetimeScope();

            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();

            builder.RegisterType<Dictionary<string, object>>().SingleInstance();
        }

        public virtual int Order
        {
            get { return Int16.MaxValue; }
        }
    }
}
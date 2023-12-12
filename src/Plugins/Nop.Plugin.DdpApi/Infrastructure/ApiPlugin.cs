using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Plugin.DdpApi.Domain;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Menu;

namespace Nop.Plugin.DdpApi.Infrastructure
{
    public class ApiPlugin : BasePlugin, IAdminMenuPlugin
    {
        private readonly ICustomerService _customerService;
        private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;
        private readonly IWorkContext _workContext;

        public ApiPlugin(
            ISettingService settingService,
            IWorkContext workContext,
            ICustomerService customerService,
            ILocalizationService localizationService,
            IWebHelper webHelper)
        {
            _settingService = settingService;
            _workContext = workContext;
            _customerService = customerService;
            _localizationService = localizationService;
            _webHelper = webHelper;
        }

        public override void Install()
        {
            //locales

            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.DdpApi", "DdpApi plugin");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.DdpApi.Admin.Menu.ManageClients", "Manage DdpApi Clients");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.DdpApi.Admin.Configure", "Configure Web DdpApi");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.DdpApi.Admin.GeneralSettings", "General Settings");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.DdpApi.Admin.EnableApi", "Enable DdpApi");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.DdpApi.Admin.EnableApi.Hint", "By checking this settings you can Enable/Disable the Web DdpApi");

            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.DdpApi.Admin.Menu.Title", "API");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.DdpApi.Admin.Menu.Settings.Title", "Settings");

            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.DdpApi.Admin.Page.Settings.Title", "DdpApi Settings");


            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.DdpApi.Admin.Settings.GeneralSettingsTitle", "General Settings");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.DdpApi.Admin.Edit", "Edit");

            _localizationService.AddOrUpdatePluginLocaleResource("DdpApi.Categories.Fields.Id.Invalid", "Id is invalid");
            _localizationService.AddOrUpdatePluginLocaleResource("DdpApi.InvalidPropertyType", "Invalid Property Type");
            _localizationService.AddOrUpdatePluginLocaleResource("DdpApi.InvalidType", "Invalid {0} type");
            _localizationService.AddOrUpdatePluginLocaleResource("DdpApi.InvalidRequest", "Invalid request");
            _localizationService.AddOrUpdatePluginLocaleResource("DdpApi.InvalidRootProperty", "Invalid root property");
            _localizationService.AddOrUpdatePluginLocaleResource("DdpApi.NoJsonProvided", "No Json provided");
            _localizationService.AddOrUpdatePluginLocaleResource("DdpApi.InvalidJsonFormat", "Json format is invalid");
            _localizationService.AddOrUpdatePluginLocaleResource("DdpApi.Category.InvalidImageAttachmentFormat", "Invalid image attachment base64 format");
            _localizationService.AddOrUpdatePluginLocaleResource("DdpApi.Category.InvalidImageSrc", "Invalid image source");
            _localizationService.AddOrUpdatePluginLocaleResource("DdpApi.Category.InvalidImageSrcType", "You have provided an invalid image source/attachment ");

            _settingService.SaveSetting(new ApiSettings());

            var apiRole = _customerService.GetCustomerRoleBySystemName(Constants.Roles.ApiRoleSystemName);

            if (apiRole == null)
            {
                apiRole = new CustomerRole
                {
                    Name = Constants.Roles.ApiRoleName,
                    Active = true,
                    SystemName = Constants.Roles.ApiRoleSystemName
                };

                _customerService.InsertCustomerRole(apiRole);
            }
            else if (apiRole.Active == false)
            {
                apiRole.Active = true;
                _customerService.UpdateCustomerRole(apiRole);
            }


            base.Install();

            // Changes to Web.Config trigger application restart.
            // This doesn't appear to affect the Install function, but just to be safe we will made web.config changes after the plugin was installed.
            //_webConfigMangerHelper.AddConfiguration();
        }

        public override void Uninstall()
        {
            //locales
            _localizationService.DeletePluginLocaleResources("Plugins.DdpApi");

            var apiRole = _customerService.GetCustomerRoleBySystemName(Constants.Roles.ApiRoleSystemName);
            if (apiRole != null)
            {
                apiRole.Active = false;
                _customerService.UpdateCustomerRole(apiRole);
            }


            base.Uninstall();
        }

        public void ManageSiteMap(SiteMapNode rootNode)
        {
            var pluginMenuName = _localizationService.GetResource("Plugins.DdpApi.Admin.Menu.Title", _workContext.WorkingLanguage.Id, defaultValue: "API");

            var settingsMenuName = _localizationService.GetResource("Plugins.DdpApi.Admin.Menu.Settings.Title", _workContext.WorkingLanguage.Id, defaultValue: "API");

            const string adminUrlPart = "Admin/";

            var pluginMainMenu = new SiteMapNode
            {
                Title = pluginMenuName,
                Visible = true,
                SystemName = "DdpApi-Main-Menu",
                IconClass = "fa-genderless"
            };

            pluginMainMenu.ChildNodes.Add(new SiteMapNode
            {
                Title = settingsMenuName,
                Url = _webHelper.GetStoreLocation() + adminUrlPart + "ApiAdmin/Settings",
                Visible = true,
                SystemName = "DdpApi-Settings-Menu",
                IconClass = "fa-genderless"
            });


            rootNode.ChildNodes.Add(pluginMainMenu);
        }
    }
}

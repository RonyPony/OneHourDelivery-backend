using System;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Plugin.Widgets.WhatsAppChat.Models;
using Nop.Services.Caching;
using Nop.Services.Configuration;
using Nop.Services.Media;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.WhatsAppChat.Components
{
    /// <summary>
    /// Represents the view component to display widget info in public store
    /// </summary>
    [ViewComponent(Name = "WidgetsWhatsAppChat")]
    public class WidgetsWhatsAppChatViewComponent : NopViewComponent
    {
        private readonly IStoreContext _storeContext;
        private readonly ISettingService _settingService;

        /// <summary>
        /// Initializes a new instance of WidgetsWhatsAppChatViewComponent class
        /// </summary>
        /// <param name="cacheKeyService"><see cref="ICacheKeyService"/></param>
        /// <param name="storeContext"><see cref="IStoreContext"/></param>
        /// <param name="settingService"><see cref="ISettingService"/></param>
        public WidgetsWhatsAppChatViewComponent(ICacheKeyService cacheKeyService,
            IStoreContext storeContext, 
            ISettingService settingService)
        {
            _storeContext = storeContext;
            _settingService = settingService;
        }

        /// <summary>
        /// Invoke view component
        /// </summary>
        /// <param name="widgetZone">Widget zone name</param>
        /// <param name="additionalData">Additional data</param>
        /// <returns>View component result</returns>
        public IViewComponentResult Invoke(string widgetZone, object additionalData)
        {
            WhatsAppChatSettings WhatsAppChatSettings = _settingService.LoadSetting<WhatsAppChatSettings>(_storeContext.CurrentStore.Id);
            PublicInfoModel model = new PublicInfoModel
            {
                Phone = WhatsAppChatSettings.Phone,
                HeaderTitle = WhatsAppChatSettings.HeaderTitle,
                PopupMessage = WhatsAppChatSettings.PopupMessage,
                Message = WhatsAppChatSettings.Message,
            };

            return View("~/Plugins/Widgets.WhatsAppChat/Views/PublicInfo.cshtml", model);
        }
    }
}

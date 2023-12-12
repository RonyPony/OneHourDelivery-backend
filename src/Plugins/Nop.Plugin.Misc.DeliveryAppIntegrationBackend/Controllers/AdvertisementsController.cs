using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Attributes;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Plugin.Widgets.NivoSlider;
using Nop.Services.Configuration;
using Nop.Services.Media;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Controllers
{
    /// <summary>
    /// Responsible for interactions with adversiments information entity.
    /// </summary>
    [Route("api/advertisements")]
    [ApiController]
    public sealed class AdvertisementsController : Controller
    {
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;
        private readonly IPictureService _pictureService;

        /// <summary>
        /// Creates an instance of <see cref="AdvertisementsController"/>
        /// </summary>
        /// <param name="settingService">An implementation of <see cref="ISettingService"/></param>
        /// <param name="storeContext">An implementation of <see cref="IStoreContext"/></param>
        /// <param name="pictureService">An implementation of <see cref="IPictureService"/></param>

        public AdvertisementsController(ISettingService settingService, IStoreContext storeContext, IPictureService pictureService)
        {
            _settingService = settingService;
            _storeContext = storeContext;
            _pictureService = pictureService;
        }

        [HttpGet, Authorize(Roles = "Registered, Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        //[SwaggerResponse((int)HttpStatusCode.OK, "Exposes the Gell all promotions functionality", typeof(List<Promotion>))]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest, "Exposes the Gell all promotions functionality", typeof(ErrorMessage))]
        public IActionResult GetAllAdvertisements()
        {
            //load settings for a chosen store scope
            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var nivoSliderSettings = _settingService.LoadSetting<NivoSliderSettings>(storeScope);

            ICollection<Advertisement> advertisements = new List<Advertisement>();

            if (nivoSliderSettings.Picture1Id > 0) advertisements.Add(new Advertisement { ImageUrl = _pictureService.GetPictureUrl(nivoSliderSettings.Picture1Id)  });
            if (nivoSliderSettings.Picture2Id > 0) advertisements.Add(new Advertisement { ImageUrl = _pictureService.GetPictureUrl(nivoSliderSettings.Picture2Id)  });
            if (nivoSliderSettings.Picture3Id > 0) advertisements.Add(new Advertisement { ImageUrl = _pictureService.GetPictureUrl(nivoSliderSettings.Picture3Id)  });
            if (nivoSliderSettings.Picture4Id > 0) advertisements.Add(new Advertisement { ImageUrl = _pictureService.GetPictureUrl(nivoSliderSettings.Picture4Id)  });
            if (nivoSliderSettings.Picture5Id > 0) advertisements.Add(new Advertisement { ImageUrl = _pictureService.GetPictureUrl(nivoSliderSettings.Picture5Id) });

            return Ok(advertisements);
        }
    }

}

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Attributes;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Services.Configuration;
using Nop.Services.Media;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Controllers
{
    [Route("api/advertisements")]
    [ApiController]
    public sealed class AdvertisementsController : Controller
    {
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;
        private readonly IPictureService _pictureService;

        public AdvertisementsController(
            ISettingService settingService,
            IStoreContext storeContext,
            IPictureService pictureService)
        {
            _settingService = settingService;
            _storeContext = storeContext;
            _pictureService = pictureService;
        }

        [HttpGet]
        [Authorize(Roles = "Registered, Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public async Task<IActionResult> GetAllAdvertisements()
        {
            var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var nivoSliderSettings = await LoadNivoSliderSettingsAsync(storeScope);

            ICollection<Advertisement> advertisements = new List<Advertisement>();

            if (nivoSliderSettings.Picture1Id > 0)
            {
                advertisements.Add(new Advertisement
                {
                    ImageUrl = await _pictureService.GetPictureUrlAsync(nivoSliderSettings.Picture1Id)
                });
            }

            if (nivoSliderSettings.Picture2Id > 0)
            {
                advertisements.Add(new Advertisement
                {
                    ImageUrl = await _pictureService.GetPictureUrlAsync(nivoSliderSettings.Picture2Id)
                });
            }

            if (nivoSliderSettings.Picture3Id > 0)
            {
                advertisements.Add(new Advertisement
                {
                    ImageUrl = await _pictureService.GetPictureUrlAsync(nivoSliderSettings.Picture3Id)
                });
            }

            if (nivoSliderSettings.Picture4Id > 0)
            {
                advertisements.Add(new Advertisement
                {
                    ImageUrl = await _pictureService.GetPictureUrlAsync(nivoSliderSettings.Picture4Id)
                });
            }

            if (nivoSliderSettings.Picture5Id > 0)
            {
                advertisements.Add(new Advertisement
                {
                    ImageUrl = await _pictureService.GetPictureUrlAsync(nivoSliderSettings.Picture5Id)
                });
            }

            return Ok(advertisements);
        }

        private async Task<NivoSliderSettingsSnapshot> LoadNivoSliderSettingsAsync(int storeScope)
        {
            return new NivoSliderSettingsSnapshot
            {
                Picture1Id = await _settingService.GetSettingByKeyAsync<int>("NivoSliderSettings.Picture1Id", storeId: storeScope),
                Picture2Id = await _settingService.GetSettingByKeyAsync<int>("NivoSliderSettings.Picture2Id", storeId: storeScope),
                Picture3Id = await _settingService.GetSettingByKeyAsync<int>("NivoSliderSettings.Picture3Id", storeId: storeScope),
                Picture4Id = await _settingService.GetSettingByKeyAsync<int>("NivoSliderSettings.Picture4Id", storeId: storeScope),
                Picture5Id = await _settingService.GetSettingByKeyAsync<int>("NivoSliderSettings.Picture5Id", storeId: storeScope)
            };
        }

        private sealed class NivoSliderSettingsSnapshot
        {
            public int Picture1Id { get; set; }

            public int Picture2Id { get; set; }

            public int Picture3Id { get; set; }

            public int Picture4Id { get; set; }

            public int Picture5Id { get; set; }
        }
    }
}
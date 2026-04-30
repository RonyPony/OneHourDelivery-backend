using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.NivoSlider.Models;
using Nop.Services.Configuration;
using Nop.Services.Media;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.NivoSlider.Components;

[ViewComponent(Name = "WidgetsNivoSlider")]
public class WidgetsNivoSliderViewComponent : NopViewComponent
{
    private readonly IStoreContext _storeContext;
    private readonly ISettingService _settingService;
    private readonly IPictureService _pictureService;

    public WidgetsNivoSliderViewComponent(
        IStoreContext storeContext,
        ISettingService settingService,
        IPictureService pictureService)
    {
        _storeContext = storeContext;
        _settingService = settingService;
        _pictureService = pictureService;
    }

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        var currentStore = await _storeContext.GetCurrentStoreAsync();
        var nivoSliderSettings = await _settingService.LoadSettingAsync<NivoSliderSettings>(currentStore.Id);

        var model = new PublicInfoModel
        {
            Picture1Url = await GetPictureUrlAsync(nivoSliderSettings.Picture1Id),
            Text1 = nivoSliderSettings.Text1,
            Link1 = nivoSliderSettings.Link1,
            AltText1 = nivoSliderSettings.AltText1,
            Picture2Url = await GetPictureUrlAsync(nivoSliderSettings.Picture2Id),
            Text2 = nivoSliderSettings.Text2,
            Link2 = nivoSliderSettings.Link2,
            AltText2 = nivoSliderSettings.AltText2,
            Picture3Url = await GetPictureUrlAsync(nivoSliderSettings.Picture3Id),
            Text3 = nivoSliderSettings.Text3,
            Link3 = nivoSliderSettings.Link3,
            AltText3 = nivoSliderSettings.AltText3,
            Picture4Url = await GetPictureUrlAsync(nivoSliderSettings.Picture4Id),
            Text4 = nivoSliderSettings.Text4,
            Link4 = nivoSliderSettings.Link4,
            AltText4 = nivoSliderSettings.AltText4,
            Picture5Url = await GetPictureUrlAsync(nivoSliderSettings.Picture5Id),
            Text5 = nivoSliderSettings.Text5,
            Link5 = nivoSliderSettings.Link5,
            AltText5 = nivoSliderSettings.AltText5
        };

        if (string.IsNullOrEmpty(model.Picture1Url) && string.IsNullOrEmpty(model.Picture2Url) &&
            string.IsNullOrEmpty(model.Picture3Url) && string.IsNullOrEmpty(model.Picture4Url) &&
            string.IsNullOrEmpty(model.Picture5Url))
        {
            return Content(string.Empty);
        }

        return View("~/Plugins/Widgets.NivoSlider/Views/PublicInfo.cshtml", model);
    }

    protected virtual async Task<string> GetPictureUrlAsync(int pictureId)
        => await _pictureService.GetPictureUrlAsync(pictureId, showDefaultPicture: false) ?? string.Empty;
}

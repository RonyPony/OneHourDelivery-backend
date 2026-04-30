using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Cms;
using Nop.Core.Infrastructure;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.NivoSlider;

/// <summary>
/// Plugin implementation for Nivo slider widget.
/// </summary>
public class NivoSliderPlugin : BasePlugin, IWidgetPlugin
{
    private readonly ILocalizationService _localizationService;
    private readonly IPictureService _pictureService;
    private readonly ISettingService _settingService;
    private readonly IWebHelper _webHelper;
    private readonly INopFileProvider _fileProvider;

    public NivoSliderPlugin(
        ILocalizationService localizationService,
        IPictureService pictureService,
        ISettingService settingService,
        IWebHelper webHelper,
        INopFileProvider fileProvider)
    {
        _localizationService = localizationService;
        _pictureService = pictureService;
        _settingService = settingService;
        _webHelper = webHelper;
        _fileProvider = fileProvider;
    }

    public bool HideInWidgetList => false;

    public override string GetConfigurationPageUrl()
        => $"{_webHelper.GetStoreLocation()}Admin/WidgetsNivoSlider/Configure";

    public Type GetWidgetViewComponent(string widgetZone)
        => typeof(Components.WidgetsNivoSliderViewComponent);

    public Task<IList<string>> GetWidgetZonesAsync()
        => Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.HomepageTop });

    public override async Task InstallAsync()
    {
        var sampleImagesPath = _fileProvider.MapPath("~/Plugins/Widgets.NivoSlider/Content/nivoslider/sample-images/");

        var banner1 = await _pictureService.InsertPictureAsync(
            await _fileProvider.ReadAllBytesAsync(_fileProvider.Combine(sampleImagesPath, "banner1.jpg")),
            MimeTypes.ImagePJpeg,
            "banner_1");

        var banner2 = await _pictureService.InsertPictureAsync(
            await _fileProvider.ReadAllBytesAsync(_fileProvider.Combine(sampleImagesPath, "banner2.jpg")),
            MimeTypes.ImagePJpeg,
            "banner_2");

        var settings = new NivoSliderSettings
        {
            Picture1Id = banner1.Id,
            Text1 = string.Empty,
            Link1 = _webHelper.GetStoreLocation(false),
            Picture2Id = banner2.Id,
            Text2 = string.Empty,
            Link2 = _webHelper.GetStoreLocation(false)
        };

        await _settingService.SaveSettingAsync(settings);

        await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
        {
            ["Plugins.Widgets.NivoSlider.Picture1"] = "Picture 1",
            ["Plugins.Widgets.NivoSlider.Picture2"] = "Picture 2",
            ["Plugins.Widgets.NivoSlider.Picture3"] = "Picture 3",
            ["Plugins.Widgets.NivoSlider.Picture4"] = "Picture 4",
            ["Plugins.Widgets.NivoSlider.Picture5"] = "Picture 5",
            ["Plugins.Widgets.NivoSlider.Picture"] = "Picture",
            ["Plugins.Widgets.NivoSlider.Picture.Hint"] = "Upload picture.",
            ["Plugins.Widgets.NivoSlider.Text"] = "Comment",
            ["Plugins.Widgets.NivoSlider.Text.Hint"] = "Enter comment for picture. Leave empty if you don't want to display any text.",
            ["Plugins.Widgets.NivoSlider.Link"] = "URL",
            ["Plugins.Widgets.NivoSlider.Link.Hint"] = "Enter URL. Leave empty if you don't want this picture to be clickable.",
            ["Plugins.Widgets.NivoSlider.AltText"] = "Image alternate text",
            ["Plugins.Widgets.NivoSlider.AltText.Hint"] = "Enter alternate text that will be added to image."
        });

        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        await _settingService.DeleteSettingAsync<NivoSliderSettings>();
        await _localizationService.DeleteLocaleResourcesAsync("Plugins.Widgets.NivoSlider");

        await base.UninstallAsync();
    }
}

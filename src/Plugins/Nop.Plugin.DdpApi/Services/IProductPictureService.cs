using Nop.Core.Domain.Catalog;

namespace Nop.Plugin.DdpApi.Services
{
    public interface IProductPictureService
    {
        ProductPicture GetProductPictureByPictureId(int pictureId);
    }
}

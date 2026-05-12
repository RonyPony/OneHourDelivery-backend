using Nop.Core;
using Nop.Data;
using System.Net;

namespace Nop.Core.Caching
{
    public class CachingSettings
    {
        public int ShortTermCacheTime { get; set; } = 3;
    }
}

namespace Nop.Services.Caching.Extensions
{
    public static class RepositoryCacheCompatibilityExtensions
    {
        public static TEntity ToCachedGetById<TEntity>(this IRepository<TEntity> repository, int id, int? cacheTime = null)
            where TEntity : BaseEntity
        {
            return repository.GetById(id);
        }
    }
}

namespace Nop.Core.Html
{
    public static class HtmlHelper
    {
        public static string FormatText(string text, bool stripTags, bool convertPlainTextToHtml, bool allowHtml, bool allowBBCode, bool resolveLinks, bool addNoFollowTag)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            var result = stripTags ? WebUtility.HtmlEncode(text) : text;
            return convertPlainTextToHtml ? result.Replace("\r\n", "<br />").Replace("\n", "<br />") : result;
        }
    }
}

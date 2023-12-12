using Nop.Core.Domain.Localization;
using Nop.Plugin.DdpApi.AutoMapper;
using Nop.Plugin.DdpApi.DTO.Languages;

namespace Nop.Plugin.DdpApi.MappingExtensions
{
    public static class LanguageDtoMappings
    {
        public static LanguageDto ToDto(this Language language)
        {
            return language.MapTo<Language, LanguageDto>();
        }
    }
}

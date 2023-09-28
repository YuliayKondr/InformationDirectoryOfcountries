using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ContriesDatabase.Constants;
using ContriesDatabase.Models;
using InformationDirectoryOfСountries.Models;
using RESTCountriesClient.Items;

namespace InformationDirectoryOfСountries.AutoMapper;

public sealed class AutoMappingProfile : Profile
{
    public AutoMappingProfile()
    {
        CreateMap<CountryDto, Country>()
            .ForMember(x => x.Name, y => y.MapFrom(x => x.Name.Official))
            .ForMember(x => x.Capital, y => y.MapFrom(x => x.Capital.FirstOrDefault()))
            .ForMember(x => x.Flag, y => y.MapFrom(x => x.Flags.Png))
            .ForMember(x => x.Id, y => y.Ignore())
            .ForMember(x => x.NameUa, y => y.Ignore())
            .ForMember(x => x.Languages, y => y.Ignore())
            .ForMember(x => x.Maps, y => y.Ignore());

        CreateMap<CountryDto, CountryViewItem>()
            .ForMember(x => x.Name, y => y.MapFrom(x => x.Name.Official))
            .ForMember(x => x.Capital, y => y.MapFrom(x => x.Capital.FirstOrDefault()))
            .ForMember(x => x.Flag, y => y.MapFrom(x => x.Flags.Png))
            .ForMember(x => x.FirstLanguage, y => y.MapFrom(x => x.Languages.FirstOrDefault().Value))
            .ForMember(x => x.Currency, y => y.MapFrom(x => GetCurrencyDto(x.Currencies)))
            .ForMember(x => x.Id, y => y.Ignore());

        CreateMap<Country, CountryViewItem>()
            .ForMember(x => x.FirstLanguage, y => y.MapFrom(x => GetLanguage(x.Languages)))
            .ForMember(x => x.Currency, y => y.MapFrom(x => GetCurrency(x.Currencies)))
            .ForMember(x => x.GoogleMapUrl, y => y.MapFrom(x => UrlMapByType(x.Maps, MapTypeConstant.GoogleTypeId)))
            .ForMember(x => x.OpenStreetMapUrl, y => y.MapFrom(x => UrlMapByType(x.Maps, MapTypeConstant.OpenStreetTypeId)));



    }

    private string GetCurrency(ICollection<Currency> currencies)
    {
        if (currencies?.Any() == true)
        {
            return currencies.First().ToString();
        }

        return string.Empty;
    }

    private string GetCurrencyDto(Dictionary<string, CurrencyItemDto> currencies)
    {
        if (currencies?.Any() == true)
        {
            return currencies.First().Value.ToString();
        }

        return string.Empty;
    }

    private string GetLanguage(ICollection<Language> languages)
    {
        if (languages?.Any() == true)
        {
            return languages.First().Name;
        }

        return string.Empty;
    }

    private string? UrlMapByType(ICollection<Map> maps, int typeId)
    {
        if (maps?.Any() != true)
        {
            return string.Empty;
        }

        return maps.FirstOrDefault(x => x.MapTypeId == typeId)?.Url;
    }
}
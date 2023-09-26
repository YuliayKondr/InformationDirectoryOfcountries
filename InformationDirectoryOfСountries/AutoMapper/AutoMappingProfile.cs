using System.Linq;
using AutoMapper;
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
            .ForMember(x => x.Id, y => y.Ignore());

    }
}
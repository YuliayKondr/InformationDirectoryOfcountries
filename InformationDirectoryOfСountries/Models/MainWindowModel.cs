using System;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using ContriesDatabase;
using ContriesDatabase.Constants;
using ContriesDatabase.Models;
using InformationDirectoryOfСountries.Arhitecture;
using Microsoft.EntityFrameworkCore;
using RESTCountriesClient;
using RESTCountriesClient.Items;

namespace InformationDirectoryOfСountries.Models
{
    public sealed class MainWindowModel : BaseNotifyPropertyChanged, IActivable
    {
        private readonly INavigationService _navigationService;
        private readonly ICountriesClient _countriesClient;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Country> _countryRepository;

        private ObservableCollection<CountryViewItem> _countries;

        public MainWindowModel(
            INavigationService navigationService,
            ICountriesClient countriesClient,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IRepository<Country> countryRepository,
            IRepository<Map> addressRepository,
            IRepository<Language> subscriptionRepository)
        {
            _navigationService = navigationService;
            _countriesClient = countriesClient;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _countryRepository = countryRepository;

            LoadCountriesCommand = new AsyncRelayCommand(LoadCountriesAsync);

            Countries = new ObservableCollection<CountryViewItem>();
        }

        public ObservableCollection<CountryViewItem> Countries
        {
            get => _countries;
            set => SetAndNotifieIfChanged(ref _countries, value);
        }

        public ICommand LoadCountriesCommand { get; set; }

        public Task ActivateAsync(object parameter)
        {
            return Task.CompletedTask;
        }

        private async Task LoadCountriesAsync()
        {
            Country[] countries = await _countryRepository.Queryable().ToArrayAsync();

            if (countries.Any())
            {
                Countries = new ObservableCollection<CountryViewItem>(countries.Select(x => _mapper.Map<CountryViewItem>(x)));

                return;
            }

            IReadOnlyCollection<CountryDto> countryDtos = await _countriesClient!.GetCounriesAsync();

            await SaveAsync(countryDtos);

            Countries = new ObservableCollection<CountryViewItem>(countryDtos.Select(x => _mapper.Map<CountryViewItem>(x)));
        }

        private async Task SaveAsync(IReadOnlyCollection<CountryDto> countries)
        {
            foreach (CountryDto countryDto in countries)
            {
                Country country = MapCountry(countryDto);

                _countryRepository.Insert(country);
            }

            await _unitOfWork.SaveChangesAsync(CancellationToken.None);
        }

        private Country MapCountry(CountryDto source)
        {
            string cc3 = source.Ccn3 == null ? "0" : source.Ccn3;

            Country country = new Country(cc3, source.Name.Official, string.Empty, source.Capital?.FirstOrDefault() ?? string.Empty, source.Flags.Png);

            country.SetCollections(
                MapLanguage(country, source),
                MapMap(country, source),
                MapCurrency(country, source));

            return country;
        }

        private Language[] MapLanguage(Country country, CountryDto sourceDto)
        {
            if (sourceDto.Languages?.Any() == true)
            {
                return sourceDto.Languages.Select(x => new Language( x.Value, x.Key, country)).ToArray() ;
            }

            return Array.Empty<Language>();
        }

        private Currency[] MapCurrency(Country country, CountryDto sourceDto)
        {
            if (sourceDto.Currencies?.Any() == true)
            {
                return sourceDto.Currencies.Select(x => new Currency(x.Value.Name, x.Key, x.Value.Symbol, country)).ToArray();
            }

            return Array.Empty<Currency>();
        }

        private Map[] MapMap(Country country, CountryDto sourceDto)
        {
            if(sourceDto.Maps?.Any() == true)
            {
                return sourceDto.Maps.Select(x => new Map(x.Key == MapTypeConstant.OpenStreetMaps ? 1 : 2, x.Value, country)).ToArray();
            }

            return Array.Empty<Map>();
        }
    }
}
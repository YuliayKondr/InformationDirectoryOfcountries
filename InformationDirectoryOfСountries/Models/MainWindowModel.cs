using System;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AutoMapper;
using CommunityToolkit.Mvvm.Messaging;
using ContriesDatabase;
using ContriesDatabase.Constants;
using ContriesDatabase.Models;
using InformationDirectoryOfСountries.Arhitecture;
using InformationDirectoryOfСountries.ContriesApplication;
using InformationDirectoryOfСountries.Views;
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

        private ObservableCollection<CountryViewItem> _countriesOrigin;
        private ObservableCollection<CountryViewItem> _countries;
        private CountryViewItem? _selectedCountry;
        private string _searchText;

        public MainWindowModel(
            INavigationService navigationService,
            ICountriesClient countriesClient,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IRepository<Country> countryRepository)
        {
            _navigationService = navigationService;
            _countriesClient = countriesClient;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _countryRepository = countryRepository;

            LoadCountriesCommand = new AsyncRelayCommand(LoadCountriesAsync);
            SearchCommand = new RelayCommand(Search);
            ShowInformCommand = new AsyncRelayCommand(ShowInform);
            ShowMapCommand = new RelayCommand<string>(ShowMapAsync);
            UpdateCommand = new AsyncRelayCommand(UpdateAsync);

            Countries = new ObservableCollection<CountryViewItem>();

            WeakReferenceMessenger.Default.Register<UpdateCountryMessage>(this, UpdateCountry);
        }

        public ObservableCollection<CountryViewItem> Countries
        {
            get => _countries;
            set => SetAndNotifieIfChanged(ref _countries, value);
        }

        public CountryViewItem? Country
        {
            get => _selectedCountry;
            set => SetAndNotifieIfChanged(ref _selectedCountry, value);
        }

        public string SearchText
        {
            get => _searchText;
            set => SetAndNotifieIfChanged(ref _searchText, value);
        }

        public ICommand LoadCountriesCommand { get; }

        public ICommand SearchCommand { get; }

        public ICommand ShowInformCommand { get; }

        public ICommand ShowMapCommand { get; }

        public ICommand UpdateCommand { get; }

        public Task ActivateAsync(object parameter)
        {
            return Task.CompletedTask;
        }

        private async Task LoadCountriesAsync()
        {
            Country[] countries = await _countryRepository.Queryable()
                .Include(x => x.Currencies)
                .Include(x => x.Languages)
                .Include(x => x.Maps)
                .ToArrayAsync();

            if (countries.Any())
            {
                _countriesOrigin = new ObservableCollection<CountryViewItem>(countries.Select(x => _mapper.Map<CountryViewItem>(x)).OrderBy(x => x.Name));

                Countries = _countriesOrigin;

                return;
            }

            IReadOnlyCollection<CountryDto> countryDtos = await _countriesClient.GetCounriesAsync();

            await SaveAsync(countryDtos);

            _countriesOrigin = new ObservableCollection<CountryViewItem>(countryDtos.Select(x => _mapper.Map<CountryViewItem>(x)).OrderBy(x => x.Name));

            Countries = _countriesOrigin;
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

        private static Country MapCountry(CountryDto source)
        {
            string cc3 = string.IsNullOrEmpty(source.Ccn3) ? "0" : source.Ccn3;

            Country country = new Country(cc3, source.Name.Official, string.Empty, source.Capital.FirstOrDefault() ?? string.Empty, source.Flags.Png);

            country.SetCollections(
                MapLanguage(country, source),
                MapMap(country, source),
                MapCurrency(country, source));

            return country;
        }

        private static Language[] MapLanguage(Country country, CountryDto sourceDto)
        {
            if (sourceDto.Languages?.Any() == true)
            {
                return sourceDto.Languages.Select(x => new Language( x.Value, x.Key, country)).ToArray() ;
            }

            return Array.Empty<Language>();
        }

        private static Currency[] MapCurrency(Country country, CountryDto sourceDto)
        {
            if (sourceDto.Currencies?.Any() == true)
            {
                return sourceDto.Currencies.Select(x => new Currency(x.Value.Name, x.Key, x.Value.Symbol, country)).ToArray();
            }

            return Array.Empty<Currency>();
        }

        private static Map[] MapMap(Country country, CountryDto sourceDto)
        {
            if(sourceDto.Maps?.Any() == true)
            {
                return sourceDto.Maps.Select(x => new Map(x.Key == MapTypeConstant.OpenStreetMaps ? 1 : 2, x.Value, country)).ToArray();
            }

            return Array.Empty<Map>();
        }

        private void Search()
        {
            if (string.IsNullOrEmpty(SearchText) && _countriesOrigin.Count != Countries.Count)
            {
                Countries = _countriesOrigin;
            }

            if (!string.IsNullOrEmpty(SearchText))
            {
                Countries = new ObservableCollection<CountryViewItem>(_countriesOrigin
                    .Where(x => x.Name?.StartsWith(SearchText) == true || x.NameUa?.StartsWith(SearchText) == true));
            }
        }

        private async Task ShowInform()
        {
            if (Country == null)
            {
                MessageBox.Show("Оберіть країну");
                return;
            }

            if (string.IsNullOrEmpty(Country.Name))
            {
                MessageBox.Show("У країни не заповнено ім'я");
                return;
            }

            CountryDto? countryDto = await _countriesClient.GetCounryByNameAsync(Country.Name);

            if (countryDto == null)
            {
                MessageBox.Show("Віддалений сервер нічого не знайшов цієї країни");
                return;
            }

            CountryParameter countryParameter = new CountryParameter(
                countryDto.Name.Common,
                countryDto.Region,
                countryDto.Area,
                countryDto.Population,
                countryDto.Flags?.Png ?? string.Empty,
                countryDto.CoatOfArms?.Png ?? string.Empty);

            await _navigationService.ShowDialogAsync(nameof(CountryView), countryParameter);
        }

        private async Task UpdateAsync()
        {
            if (Country != null)
            {
                await _navigationService.ShowDialogAsync(nameof(CountryUpdateView), new CountryParameter(Country.Id));
            }
            else
            {
                MessageBox.Show("Не вибраний об'єкт", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ShowMapAsync(string? mapType)
        {
            string selected = string.Empty;

            if (mapType == MapTypeConstant.OpenStreetMaps)
            {
                selected = Country?.OpenStreetMapUrl ?? string.Empty;
            }

            if (mapType == MapTypeConstant.GoogleMaps)
            {
                selected = Country?.GoogleMapUrl ?? string.Empty;
            }

            if (string.IsNullOrEmpty(selected))
            {
                MessageBox.Show("Пуста Url");
                return;
            }

            ProcessHelper.Start(selected);
        }

        private void UpdateCountry(object o, UpdateCountryMessage message)
        {
            CountryViewItem item = Countries.First(x => x.Id == message.Value.Id);

            item.NameUa = message.Value.NameUa;
        }
    }
}
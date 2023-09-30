using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ContriesDatabase;
using ContriesDatabase.Models;
using InformationDirectoryOfСountries.Arhitecture;
using InformationDirectoryOfСountries.Views;
using Microsoft.EntityFrameworkCore;

namespace InformationDirectoryOfСountries.Models;

public sealed class CountryUpdateModel : BaseNotifyPropertyChanged, IActivable
{
    private string _nameUa;
    private string _nameEn;
    private Country _countryForUpdate;
    private readonly IRepository<Country> _countryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly INavigationService _navigationService;

    public CountryUpdateModel(
        IRepository<Country> countryRepository,
        IUnitOfWork unitOfWork,
        INavigationService navigationService)
    {
        _countryRepository = countryRepository;
        _unitOfWork = unitOfWork;
        SaveCommand = new AsyncRelayCommand(SaveAsync);
        _navigationService = navigationService;
    }

    public ICommand SaveCommand { get; }

    public string NameUa
    {
        get => _nameUa;
        set => SetAndNotifieIfChanged(ref _nameUa, value);
    }

    public string NameEn
    {
        get => _nameEn;
        set => SetAndNotifieIfChanged(ref _nameEn, value);
    }

    public async Task ActivateAsync(object parameter)
    {
        CountryParameter countryParameter = (CountryParameter) parameter;

        int countryId = countryParameter.CountryId;

        Country? country = await _countryRepository.Queryable().Where(x => x.Id == countryId).FirstOrDefaultAsync(CancellationToken.None);

        if (country == null)
        {
            MessageBox.Show("Не знайдено країну у базі");
            return;
        }

        NameUa = string.Empty;

        NameEn = country.Name;
        _countryForUpdate = country;
    }

    private async Task SaveAsync()
    {
        if (string.IsNullOrEmpty(NameUa) || !Regex.IsMatch(NameUa, @"^[ІіЇїЄєҐґ0-9А-Яа-я]{1}[ІіЇїЄєҐґ0-9А-Яа-я\s&-.\/]*$"))
        {
            MessageBox.Show("Введіть назву українською мовою");
            return;
        }

        _countryForUpdate.SetNameUa(NameUa);

        _countryRepository.Update(_countryForUpdate);

        await _unitOfWork.SaveChangesAsync(CancellationToken.None);

        WeakReferenceMessenger.Default.Send(new UpdateCountryMessage(_countryForUpdate));

        _navigationService.CloseOpenedWindowDialog(nameof(CountryUpdateView));
    }
}
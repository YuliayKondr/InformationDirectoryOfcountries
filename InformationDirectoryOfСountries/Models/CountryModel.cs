using System.Threading.Tasks;
using InformationDirectoryOfСountries.Arhitecture;

namespace InformationDirectoryOfСountries.Models;

public sealed class CountryModel : BaseNotifyPropertyChanged, IActivable
{
    private string? _title;
    private string? _region;
    private decimal? _area;
    private int? _population;
    private string? _flag;
    private string? _coatOfArms;

    public string? Title
    {
        get => _title;
        set => SetAndNotifieIfChanged(ref _title, value);
    }

    public string? Region
    {
        get => _region;
        set => SetAndNotifieIfChanged(ref _region, value);
    }

    public decimal? Area
    {
        get => _area;
        set => SetAndNotifieIfChanged(ref _area, value);
    }

    public int? Population
    {
        get => _population;
        set => SetAndNotifieIfChanged(ref _population, value);
    }

    public string? Flag
    {
        get => _flag;
        set => SetAndNotifieIfChanged(ref _flag, value);
    }

    public string? CoatOfArms
    {
        get => _coatOfArms;
        set => SetAndNotifieIfChanged(ref _coatOfArms, value);
    }

    public Task ActivateAsync(object parameter)
    {
        CountryParameter p = (CountryParameter) parameter;

        Title = p.Name;
        Region = p.Region;
        Area = p.Area;
        Population = p.Population;
        Flag = p.Flag;
        CoatOfArms = p.CoatOfArms;

        return Task.CompletedTask;
    }
}
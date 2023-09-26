using InformationDirectoryOfСountries.Arhitecture;

namespace InformationDirectoryOfСountries.Models;

public sealed class CountryViewItem : BaseNotifyPropertyChanged
{
    private int _id;
    private string? _name;
    private string? _ccn3;
    private string? _flag;
    private string? _capitel;
    private string? _firstLanguage;

    public int Id
    {
        get => _id;
        set => SetAndNotifieIfChanged(ref _id, value);
    }

    public string? Name
    {
        get => _name;
        set => SetAndNotifieIfChanged(ref _name, value);
    }

    public string? Ccn3
    {
        get => _ccn3;
        set => SetAndNotifieIfChanged(ref _ccn3, value);
    }

    public string? Flag
    {
        get => _flag;
        set => SetAndNotifieIfChanged(ref _flag, value);
    }

    public string? Capital
    {
        get => _capitel;
        set => SetAndNotifieIfChanged(ref _capitel, value);
    }

    public string? FirstLanguage
    {
        get => _firstLanguage;
        set => SetAndNotifieIfChanged(ref _firstLanguage, value);
    }
}
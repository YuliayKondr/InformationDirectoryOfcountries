using InformationDirectoryOfСountries.Arhitecture;

namespace InformationDirectoryOfСountries.Models;

public sealed class CountryViewItem : BaseNotifyPropertyChanged
{
    private int _id;
    private string? _name;
    private string? _nameUa;
    private string? _ccn3;
    private string? _flag;
    private string? _capitel;
    private string? _firstLanguage;
    private string? _currency;
    private string? _google;
    private string? _openStreet;

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

    public string? NameUa
    {
        get => _nameUa;
        set => SetAndNotifieIfChanged(ref _nameUa, value);
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

    public string? Currency
    {
        get => _currency;
        set => SetAndNotifieIfChanged(ref _currency, value);
    }

    public string? GoogleMapUrl
    {
        get => _google;
        set => SetAndNotifieIfChanged(ref _google, value);
    }

    public string? OpenStreetMapUrl
    {
        get => _openStreet;
        set => SetAndNotifieIfChanged(ref _openStreet, value);
    }
}
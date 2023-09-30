namespace InformationDirectoryOfСountries.Models;

public sealed class CountryParameter
{
    public CountryParameter(int countryId)
    {
        CountryId = countryId;
    }

    public CountryParameter(string name, string region, decimal? area, int? population, string flag, string coatOfArms)
    {
        Name = name;
        Region = region;
        Area = area;
        Population = population;
        Flag = flag;
        CoatOfArms = coatOfArms;
    }

    public int CountryId { get; }

    public string Name { get; }

    public string Region { get; }


    public decimal? Area { get; }

    public int? Population { get; }

    public string Flag { get; }

    public string CoatOfArms { get; }
}
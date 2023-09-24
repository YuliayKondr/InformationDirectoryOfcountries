namespace ContriesDatabase.Models;

public class Language
{
    public Language(int countryId, string name, string smallName, string symbol)
    {
        CountryId = countryId;
        Name = name;
        SmallName = smallName;
        Symbol = symbol;
    }

    public Language()
    {
    }

    public int Id { get; protected set; }

    public int CountryId { get; protected set; }

    public string Name { get; protected set; }

    public string SmallName { get; protected set; }

    public string Symbol { get; protected set; }

    public virtual Country Country { get; protected set; }
}
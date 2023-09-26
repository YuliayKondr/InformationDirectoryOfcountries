namespace ContriesDatabase.Models;

public class Currency
{
    public Currency(string name, string smallName, string symbol, Country country)
    {
        Country = country;
        Name = name;
        SmallName = smallName;
        Symbol = symbol;
    }

    public Currency()
    {
    }

    public int Id { get; protected set; }

    public int CountryId { get; protected set; }

    public string Name { get; protected set; }

    public string SmallName { get; protected set; }

    public string Symbol { get; protected set; }

    public virtual Country Country { get; protected set; }
}
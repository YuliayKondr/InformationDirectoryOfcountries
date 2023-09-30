namespace ContriesDatabase.Models;

public class Country
{
    public Country(string ccn3, string name, string nameUa, string capital, string flag)
    {
        Ccn3 = ccn3;
        NameUa = nameUa;
        Name = name;
        Capital = capital;
        Flag = flag;
    }

    public Country()
    {
    }

    public int Id { get; protected set; }

    public string Ccn3 { get; protected set; }

    public string Name { get; protected set; }

    public string NameUa { get; protected set; }

    public string Capital { get; protected set; }

    public string Flag { get; protected set; }

    public virtual ICollection<Language> Languages { get; protected set; }

    public virtual ICollection<Map> Maps { get; protected set; }

    public virtual ICollection<Currency> Currencies { get; protected set; }

    public void SetCollections(Language[] languages, Map[] maps, Currency[] currencies)
    {
        if (languages?.Any() == true)
        {
            Languages = languages;
        }

        if (maps?.Any() == true)
        {
            Maps = maps;
        }

        if (currencies?.Any() == true)
        {
            Currencies = currencies;
        }
    }

    public void SetNameUa(string nameUa)
    {
        NameUa = nameUa;
    }
}
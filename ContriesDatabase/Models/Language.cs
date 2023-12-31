﻿namespace ContriesDatabase.Models;

public class Language
{
    public Language(string name, string smallName, Country country)
    {
        Name = name;
        SmallName = smallName;
        Country = country;
    }

    public Language()
    {
    }

    public int Id { get; protected set; }

    public int CountryId { get; protected set; }

    public string Name { get; protected set; }

    public string SmallName { get; protected set; }

    public virtual Country Country { get; protected set; }
}
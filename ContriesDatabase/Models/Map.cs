﻿namespace ContriesDatabase.Models;

public class Map
{
    public Map(int mapTypeId, string url, Country country)
    {
        Country = country;
        MapTypeId = mapTypeId;
        Url = url;
    }

    public Map()
    {
    }

    public int Id { get; protected set; }

    public int CountryId { get; protected set; }

    public int MapTypeId { get; protected set; }

    public string Url { get; protected set; }

    public virtual MapType MapType { get; protected set; }

    public virtual Country Country { get; protected set; }
}
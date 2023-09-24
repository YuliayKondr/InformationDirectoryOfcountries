using ContriesDatabase.EntityMappings;
using ContriesDatabase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContriesDatabase.DatabaseModel;

public class CountryMap : EntityMapBase<Country>
{
    public CountryMap()
        : base("countries")
    {
    }

    protected override void ConfigureMap(EntityTypeBuilder<Country> b)
    {
        b.HasKey(x => x.Id);
        b.Property(x => x.Id).HasColumnName("id_country");
        b.Property(x => x.Ccn3).HasColumnName("ccn3");
        b.Property(x => x.Name).HasColumnName("name");
        b.Property(x => x.NameUa).HasColumnName("name_ua");
        b.Property(x => x.Capital).HasColumnName("capital");
        b.Property(x => x.Flag).HasColumnName("flag");
    }
}
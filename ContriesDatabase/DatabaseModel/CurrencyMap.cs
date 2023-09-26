using ContriesDatabase.EntityMappings;
using ContriesDatabase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContriesDatabase.DatabaseModel;

public class CurrencyMap : EntityMapBase<Currency>
{
    public CurrencyMap() : base("currencies")
    {
    }

    protected override void ConfigureMap(EntityTypeBuilder<Currency> b)
    {
        b.HasKey(x => x.Id);
        b.Property(x => x.Id).HasColumnName("id");
        b.Property(x => x.CountryId).HasColumnName("id_country");
        b.Property(x => x.Name).HasColumnName("name");
        b.Property(x => x.SmallName).HasColumnName("small_name");
        b.Property(x => x.Symbol).HasColumnName("symbol");
        b.HasOne(x => x.Country).WithMany(x => x.Currencies).HasForeignKey(x => x.CountryId);
    }
}
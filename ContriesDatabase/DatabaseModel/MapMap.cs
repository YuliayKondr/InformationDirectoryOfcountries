using ContriesDatabase.EntityMappings;
using ContriesDatabase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContriesDatabase.DatabaseModel;

public class MapMap : EntityMapBase<Map>
{
    public MapMap() : base("maps")
    {
    }

    protected override void ConfigureMap(EntityTypeBuilder<Map> b)
    {
        b.HasKey(x => x.Id);
        b.Property(x => x.Id).HasColumnName("id");
        b.Property(x => x.CountryId).HasColumnName("id_country");
        b.Property(x => x.MapTypeId).HasColumnName("id_map_type");
        b.Property(x => x.Url).HasColumnName("url");

        b.HasOne(x => x.Country).WithMany(x => x.Maps).HasForeignKey(x => x.CountryId);
        b.HasOne(x => x.MapType).WithMany().HasForeignKey(x => x.MapTypeId);
    }
}
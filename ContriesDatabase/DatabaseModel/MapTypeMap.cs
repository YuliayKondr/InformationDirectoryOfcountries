using ContriesDatabase.EntityMappings;
using ContriesDatabase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContriesDatabase.DatabaseModel;

public class MapTypeMap : EntityMapBase<MapType>
{
    public MapTypeMap()
        : base("map_type")
    {
    }

    protected override void ConfigureMap(EntityTypeBuilder<MapType> b)
    {
        b.HasKey(x => x.Id);
        b.Property(x => x.Id).HasColumnName("id");
        b.Property(x => x.Name).HasColumnName("name");
    }
}
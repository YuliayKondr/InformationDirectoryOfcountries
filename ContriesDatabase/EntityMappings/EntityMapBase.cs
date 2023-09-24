using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContriesDatabase.EntityMappings;

public abstract class EntityMapBase<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class
{
    private readonly string _tableName;

    protected EntityMapBase(string tableName)
    {
        _tableName = tableName;
    }

    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.ToTable(_tableName);

        ConfigureMap(builder);
    }

    protected abstract void ConfigureMap(EntityTypeBuilder<TEntity> builder);
}
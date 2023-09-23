//using DatabaseModel.Models;
using Microsoft.EntityFrameworkCore;

namespace ContriesDatabase.Context
{
    public class MySqlContext : DbContext, IDataContext
    {
        public MySqlContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MySqlContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}

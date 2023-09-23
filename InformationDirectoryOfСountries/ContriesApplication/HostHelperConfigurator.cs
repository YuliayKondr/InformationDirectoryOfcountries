using System;
using ContriesDatabase.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ContriesDatabase.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

using ContriesDatabase;


namespace InformationDirectoryOfСountries.ContriesApplication
{
    internal static class HostHelperConfigurator
    {
        public static IServiceCollection ContriesHostConfiguration(this IServiceCollection services)
        {
            
            services.AddAutoMapper(typeof(IClientAssemblyMarker).Assembly);

            services.AddSingleton<MainWindow>();


            return services;
        }

        public static HostApplicationBuilder DatabaseDI(this HostApplicationBuilder builder)
        {
            builder.Services.AddDbContext<MySqlContext>(x => x.UseMySql(builder.Configuration.GetConnectionString("Main")!, new MySqlServerVersion(new Version(5, 7, 26))));
            builder.Services.AddSingleton<IDataContext, MySqlContext>();
            builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>();
            builder.Services.AddSingleton(typeof(IRepository<>), typeof(EfCoreRepository<>));

            return builder;
        }
    }
}

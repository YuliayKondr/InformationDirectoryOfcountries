using System;
using ContriesDatabase.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ContriesDatabase.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

using ContriesDatabase;
using InformationDirectoryOfСountries.Arhitecture;
using InformationDirectoryOfСountries.Models;
using InformationDirectoryOfСountries.Views;

namespace InformationDirectoryOfСountries.ContriesApplication
{
    internal static class HostHelperConfigurator
    {
        public static IServiceCollection ContriesHostConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(IClientAssemblyMarker).Assembly);
            services.NavigationDI();
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

        private static IServiceCollection NavigationDI(this IServiceCollection services)
        {
            services.AddSingleton<INavigationService, NavigationService>(
                x =>
                {
                    var navigationService = new NavigationService(x);
                    navigationService.Configure(nameof(MainWindow), typeof(MainWindow));
                    navigationService.Configure(nameof(CountryUpdateView), typeof(CountryUpdateView));
                    navigationService.Configure(nameof(CountryView), typeof(CountryView));

                    return navigationService;
                });

            services.AddScoped<MainWindowModel>();
            services.AddScoped<CountryUpdateModel>();
            services.AddScoped<CountryModel>();

            services.AddTransient(sp => new MainWindow() { DataContext = sp.GetRequiredService<MainWindowModel>() });
            services.AddTransient(sp => new CountryUpdateView() { DataContext = sp.GetRequiredService<CountryUpdateModel>() });
            services.AddTransient(sp => new CountryView() { DataContext = sp.GetRequiredService<CountryModel>() });
            return services;
        }
    }
}
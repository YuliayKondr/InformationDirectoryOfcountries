using InformationDirectoryOfСountries.ContriesApplication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RESTCountriesClient;
using System.Windows;

namespace InformationDirectoryOfСountries
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder();
            builder.Configuration.AddJsonFile("appsettings.json");
            builder.Services.ContriesHostConfiguration();
            builder.Services.AddCountriesClient("CountriesClient");

            _host = builder.Build();
        }        

        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            await _host.StartAsync();
            MainWindow mainWindow = _host.Services.GetService<MainWindow>();
            mainWindow!.Show();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            _host.Dispose();
        }
    }
}

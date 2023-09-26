using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace RESTCountriesClient
{
    public static class ContainerExtensions
    {
        public static IHttpClientBuilder AddCountriesClient(this IServiceCollection service)
        {
            service.AddScoped(serviceProvider => serviceProvider.GetService<IOptionsSnapshot<CountriesClientOptions>>()?.Value);

            return service.AddHttpClient<ICountriesClient, CountriesClient>((p, c) => ConfigureClient(p, c));
        }

        private static void ConfigureClient(IServiceProvider provider, HttpClient client)
        {
            IOptionsMonitor<CountriesClientOptions> options = provider.GetService<IOptionsMonitor<CountriesClientOptions>>();

            client.Timeout = TimeSpan.FromSeconds(options.CurrentValue.DefaultTimeoutSeconds);
            client.BaseAddress = new Uri(options.CurrentValue.BaseAddress);
        }

    }
}
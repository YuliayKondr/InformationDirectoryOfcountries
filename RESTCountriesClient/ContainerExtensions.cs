using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace RESTCountriesClient
{
    public static class ContainerExtensions
    {
        public static IHttpClientBuilder AddCountriesClient(this IServiceCollection service, string options)
        {
            if (!string.IsNullOrEmpty(options))
            {
                service.AddOptions<CountriesClientOptions>(options);
            }

            service.AddScoped(serviceProvider => serviceProvider.GetService<IOptionsSnapshot<CountriesClientOptions>>()?.Value);

            return service.AddHttpClient<ICountriesClient, CountriesClient>((p, c) => ConfigureClient(p, c));
        }

        private static void ConfigureClient(IServiceProvider provider, HttpClient client)
        {
            IOptions<CountriesClientOptions> options = provider.GetService<IOptions<CountriesClientOptions>>();

            client.Timeout = TimeSpan.FromSeconds(options.Value.DefaultTimeoutSeconds);
            client.BaseAddress = new Uri(options.Value.BaseAddress);
        }

    }
}
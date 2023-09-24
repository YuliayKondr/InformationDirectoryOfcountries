using Microsoft.Extensions.DependencyInjection;
using RESTCountriesClient.Items;
using Xunit;

namespace RESTCountriesClient.Test;

public sealed class CountriesClientTest
{
    private ICountriesClient? _client;

    public CountriesClientTest()
    {
        IServiceCollection serviceCollection = new ServiceCollection();

        serviceCollection.Configure<CountriesClientOptions>(x =>
        {
            x.BaseAddress = "https://restcountries.com";
            x.DefaultTimeoutSeconds = 60;
        });

        serviceCollection.AddCountriesClient("");
        var serviceProvider = serviceCollection.AddLogging().BuildServiceProvider();

        using IServiceScope scope = serviceProvider.CreateScope();

        _client = scope.ServiceProvider.GetService<ICountriesClient>();
    }

    [Fact]
    public async Task GetSessionAndEndSessionTests()
    {
        IReadOnlyCollection<CountryDto> countryDtos = await _client!.GetCounriesAsync();

        Assert.True(countryDtos?.Any() == true);
    }
}
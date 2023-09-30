using RESTCountriesClient.Items;

namespace RESTCountriesClient
{
    public interface ICountriesClient
    {
        Task<IReadOnlyCollection<CountryDto>> GetCounriesAsync();

        Task<CountryDto?> GetCounryByNameAsync(string name);
    }
}
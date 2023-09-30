using RESTCountriesClient.Items;
using RestEase;

namespace RESTCountriesClient
{
    [Header("Content-Type", "application/xml")]
    public interface ICountriesApi
    {
        [Get("/v3.1/all")]
        Task<Response<CountryDto[]>> GetContriesAsync([Query("fields")] params string[] properties);

        [Get("v3.1/name/{name_country}")]
        Task<Response<CountryDto[]>> GetCountryByName([Path("name_country")] string name);
    }
}
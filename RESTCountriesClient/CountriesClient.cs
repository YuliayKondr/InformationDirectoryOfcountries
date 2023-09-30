using RESTCountriesClient.Items;
using RestEase;

namespace RESTCountriesClient
{
    public sealed class CountriesClient : ICountriesClient
    {
        private readonly ICountriesApi _api;

        public CountriesClient(
            HttpClient httpClient)
        {
            _api = CreateContriesApi(httpClient);
        }

        public async Task<IReadOnlyCollection<CountryDto>> GetCounriesAsync()
        {
            Response<CountryDto[]> response = await _api.GetContriesAsync();

            if (response.ResponseMessage.IsSuccessStatusCode)
            {
                CountryDto[] countries = response.GetContent();

                return countries;
            }

            return Array.Empty<CountryDto>();
        }

        public async Task<CountryDto?> GetCounryByNameAsync(string name)
        {
            Response<CountryDto[]> response = await _api.GetCountryByName(name);

            if (response.ResponseMessage.IsSuccessStatusCode)
            {
                CountryDto[] country = response.GetContent();

                return country?.FirstOrDefault();
            }

            return null;
        }

        private ICountriesApi CreateContriesApi(HttpClient httpClient)
        {
            RestClient apiRestClient = new RestClient(httpClient)
            {
                RequestBodySerializer = new JsonRequestBodySerializer()
            };

            return apiRestClient.For<ICountriesApi>();
        }
    }
}
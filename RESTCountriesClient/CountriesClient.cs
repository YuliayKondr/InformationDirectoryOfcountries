using Microsoft.Extensions.Logging;
using RESTCountriesClient.Items;
using RestEase;

namespace RESTCountriesClient
{
    public sealed class CountriesClient : ICountriesClient
    {
        private readonly ILogger _logger;
        private readonly ICountriesApi _api;

        public CountriesClient(
            HttpClient httpClient,
            ILogger<CountriesClient> logger)
        {
            _api = CreateContriesApi(httpClient);
            _logger = logger;            
        }

        public async Task<IReadOnlyCollection<CountryDto>> GetCounriesAsync()
        {
            Response<CountryDto[]> response = await _api.GetContriesAsync();

            if (response != null && response.ResponseMessage.IsSuccessStatusCode)
            {
                CountryDto[] countries = response.GetContent();

                return countries;
            }

            return Array.Empty<CountryDto>();
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

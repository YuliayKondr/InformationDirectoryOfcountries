using Newtonsoft.Json;

namespace RESTCountriesClient.Items
{
    public sealed class CurrencyItemDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }
    }
}
using Newtonsoft.Json;

namespace RESTCountriesClient.Items
{
    public sealed record CurrencyItemDto
    {
        [JsonProperty("name")]
        public string Name { get; init; }

        [JsonProperty("symbol")]
        public string Symbol { get; init; }

        public override string ToString()
        {
            return $"{Name} ({Symbol})";
        }
    }
}
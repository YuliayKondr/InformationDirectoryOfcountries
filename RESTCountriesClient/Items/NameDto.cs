using Newtonsoft.Json;

namespace RESTCountriesClient.Items
{
    public sealed record NameDto
    {
        [JsonProperty("common")]
        public string Common { get; init; }

        [JsonProperty("official")]
        public string Official { get; init; }
    }
}
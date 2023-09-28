using Newtonsoft.Json;

namespace RESTCountriesClient.Items
{
    public sealed record CountryDto
    {
        [JsonProperty("flags")]
        public FlagDto Flags { get; init; }

        [JsonProperty("name")]
        public NameDto Name { get; init; }

        [JsonProperty("ccn3")]
        public string Ccn3 { get; init; }

        [JsonProperty("currencies")]
        public Dictionary<string, CurrencyItemDto> Currencies { get; init; }

        [JsonProperty("capital")]
        public string[] Capital { get; init; }

        [JsonProperty("languages")]
        public Dictionary<string, string> Languages { get; init; }

        [JsonProperty("maps")]
        public Dictionary<string, string> Maps { get; init; }
    }
}
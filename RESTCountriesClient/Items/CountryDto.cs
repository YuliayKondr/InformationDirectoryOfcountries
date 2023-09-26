using Newtonsoft.Json;

namespace RESTCountriesClient.Items
{
    public sealed class CountryDto
    {
        [JsonProperty("flags")]
        public FlagDto Flags { get; set; }

        [JsonProperty("name")]
        public NameDto Name { get; set; }

        [JsonProperty("ccn3")]
        public string Ccn3 { get; set; }

        [JsonProperty("currencies")]
        public Dictionary<string, CurrencyItemDto> Currencies { get; set; }

        [JsonProperty("capital")]
        public string[] Capital { get; set; }

        [JsonProperty("languages")]
        public Dictionary<string, string> Languages { get; set; }

        [JsonProperty("maps")]
        public Dictionary<string, string> Maps { get; set; }
    }
}
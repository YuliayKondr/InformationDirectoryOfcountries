using Newtonsoft.Json;

namespace RESTCountriesClient.Items
{
    public sealed class NameDto
    {
        [JsonProperty("common")]
        public string Common { get; set; }

        [JsonProperty("official")]
        public string Official { get; set; }
    }
}

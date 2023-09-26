using Newtonsoft.Json;

namespace RESTCountriesClient.Items
{
    public sealed class FlagDto
    {
        [JsonProperty("png")]
        public string Png { get; set; }
    }
}
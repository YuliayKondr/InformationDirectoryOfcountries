using Newtonsoft.Json;

namespace RESTCountriesClient.Items
{
    public sealed record FlagDto
    {
        [JsonProperty("png")]
        public string Png { get; init; }
    }
}
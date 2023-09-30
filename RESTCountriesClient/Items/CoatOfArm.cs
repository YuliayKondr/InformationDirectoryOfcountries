using Newtonsoft.Json;

namespace RESTCountriesClient.Items;

public sealed record CoatOfArm
{
    [JsonProperty("png")]
    public string Png { get; init; }
}
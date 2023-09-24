using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Dictionary<string, string> Language { get; set; }

        [JsonProperty("translations")]
        public Dictionary<string, string> Translations { get; set; }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTCountriesClient.Items
{
    public sealed class FlagDto
    {
        [JsonProperty("png")]
        public string Png { get; set; }
    }
}

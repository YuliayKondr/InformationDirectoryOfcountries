using System.ComponentModel.DataAnnotations;

namespace RESTCountriesClient
{
    public sealed class ContriesClientOptions
    {
        [Url]
        [Required]
        public string BaseAddress { get; set; }

        [Required]
        [Range(1, 300)]
        public int DefaultTimeoutSeconds { get; set; }
    }
}

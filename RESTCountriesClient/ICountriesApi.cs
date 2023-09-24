using RESTCountriesClient.Items;
using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTCountriesClient
{
    [Header("Content-Type", "application/xml")]
    public interface ICountriesApi
    {
        [Get("/v3.1/all")]
        Task<Response<CountryDto[]>> GetContriesAsync([Query("fields")] params string[] properties);
    }
}

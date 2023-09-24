using RESTCountriesClient.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTCountriesClient
{
    public interface ICountriesClient
    {
        Task<IReadOnlyCollection<CountryDto>> GetCounriesAsync();
    }
}

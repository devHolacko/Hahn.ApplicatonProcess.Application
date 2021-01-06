using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.Services
{
    public class CountryDomainService
    {
        private readonly IConfiguration _configuration;
        public CountryDomainService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task GetCountries()
        {
            string countriesUrl = _configuration.GetSection("CountriesBaseUrl").Value;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(countriesUrl);
            }
        }
    }
}

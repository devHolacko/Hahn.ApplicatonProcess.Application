using Hahn.ApplicatonProcess.December2020.Domain.Models.Country;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Data.Services.CountryService
{
    public class CountryDataService : ICountryDataService
    {
        private readonly string _apiUrl;
        public CountryDataService(string apiUrl)
        {
            _apiUrl = apiUrl;
        }
        public async Task<bool> CheckCountryExist(string country)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(_apiUrl);
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<List<KeyValuePair<string, string>>> GetCountries()
        {
            List<Country> countries = new List<Country>();
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(_apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    countries = JsonConvert.DeserializeObject<List<Country>>(jsonResponse);
                }
            }
            if (countries == null || !countries.Any())
                return new List<KeyValuePair<string, string>>();
            return countries.Select(c => new KeyValuePair<string, string>(c.numericCode, c.name)).ToList();
        }
    }
}

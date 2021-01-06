using Hahn.ApplicatonProcess.December2020.Domain.Models.Country;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public bool CheckCountryExist(string country)
        {
            throw new NotImplementedException();
        }

        public List<Country> GetCountries()
        {
            throw new NotImplementedException();
        }
    }
}

using Hahn.ApplicatonProcess.December2020.Domain.Models.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Data.Services.CountryService
{
    public interface ICountryDataService
    {
        public Task<List<Country>> GetCountries();
        public Task<bool> CheckCountryExist(string country);
    }
}

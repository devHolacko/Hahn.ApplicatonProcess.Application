using Hahn.ApplicatonProcess.December2020.Data.Services.CountryService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : BaseApiController
    {
        private readonly ICountryDataService _countryDataService;
        public CountriesController(ICountryDataService countryDataService)
        {
            _countryDataService = countryDataService;
        }
    }
}

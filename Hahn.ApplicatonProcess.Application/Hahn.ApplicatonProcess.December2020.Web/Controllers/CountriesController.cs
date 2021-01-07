using Hahn.ApplicatonProcess.December2020.Data.Services.CountryService;
using Hahn.ApplicatonProcess.December2020.Web.ViewModels.Base;
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


        [HttpGet]
        [ProducesResponseType(typeof(DataGenericResponse<List<KeyValuePair<string, string>>>), 200)]
        public async Task<IActionResult> GetCountriesLookup()
        {
            List<KeyValuePair<string, string>> countries = await _countryDataService.GetCountries();
            DataGenericResponse<List<KeyValuePair<string, string>>> response = new DataGenericResponse<List<KeyValuePair<string, string>>> { Success = true, Data = countries };
            return new OkObjectResult(response);
        }
    }

}

﻿using Hahn.ApplicatonProcess.December2020.Data.Services.CountryService;
using Hahn.ApplicatonProcess.December2020.Web.ViewModels.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class CountriesController : BaseApiController
    {
        private readonly ICountryDataService _countryDataService;
        public CountriesController(ICountryDataService countryDataService, IConfiguration configuration)
            : base(configuration)
        {
            _countryDataService = countryDataService;
        }

        /// <summary>
        /// An api that gets all countries as lookup
        /// </summary>
        /// <returns>List of key value pair for all countries</returns>
        /// <response code="201">Returns the list of the countries</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DataGenericResponse<List<KeyValuePair<string, string>>>))]
        public async Task<IActionResult> GetCountriesLookup()
        {
            Log.Information("Calling get countries lookup api");
            List <KeyValuePair<string, string>> countries = await _countryDataService.GetCountries();
            DataGenericResponse<List<KeyValuePair<string, string>>> response = new DataGenericResponse<List<KeyValuePair<string, string>>> { Success = true, Data = countries };
            if(response != null)
            {
                throw new NotImplementedException("Not implemented");
            }
            Log.Information("Request completed");
            return new OkObjectResult(response);
        }
    }

}

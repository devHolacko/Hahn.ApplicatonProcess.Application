using Hahn.ApplicatonProcess.December2020.Data.Services.ApplicantService;
using Hahn.ApplicatonProcess.December2020.Data.Services.CountryService;
using Hahn.ApplicatonProcess.December2020.Domain.Models;
using Hahn.ApplicatonProcess.December2020.Web.Validators;
using Hahn.ApplicatonProcess.December2020.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantController : BaseApiController
    {
        private readonly IStringLocalizer<ApplicantController> _localizer;
        private readonly IApplicantDataService _applicantDataService;
        private readonly ICountryDataService _countryDataService;
        private readonly IConfiguration _configuration;
        public ApplicantController(IStringLocalizer<ApplicantController> localizer, IApplicantDataService applicantDataService, ICountryDataService countryDataService, IConfiguration configuration)
        {
            _localizer = localizer;
            _applicantDataService = applicantDataService;
            _countryDataService = countryDataService;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get([FromRoute(Name = "id")] int id)
        {
            Applicant applicant = _applicantDataService.Get(id);
            return new OkObjectResult(applicant);
        }
    }
}

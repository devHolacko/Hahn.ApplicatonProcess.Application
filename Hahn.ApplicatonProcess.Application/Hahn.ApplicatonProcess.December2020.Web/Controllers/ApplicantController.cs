using AutoMapper;
using Hahn.ApplicatonProcess.December2020.Data.Services.ApplicantService;
using Hahn.ApplicatonProcess.December2020.Data.Services.CountryService;
using Hahn.ApplicatonProcess.December2020.Domain.Models;
using Hahn.ApplicatonProcess.December2020.Web.Validators;
using Hahn.ApplicatonProcess.December2020.Web.ViewModels.Applicant;
using Hahn.ApplicatonProcess.December2020.Web.ViewModels.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;

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
        private readonly IMapper _mapper;
        public ApplicantController(IStringLocalizer<ApplicantController> localizer, IApplicantDataService applicantDataService, ICountryDataService countryDataService, IConfiguration configuration, IMapper mapper)
        {
            _localizer = localizer;
            _applicantDataService = applicantDataService;
            _countryDataService = countryDataService;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(DataGenericResponse<ApplicantViewModel>), 200)]
        [ProducesResponseType(typeof(GenericResponse), 404)]
        [Route("{id}")]
        public IActionResult Get([FromRoute(Name = "id")] int id)
        {
            Applicant applicant = _applicantDataService.Get(id);
            if (applicant == null)
            {
                GenericResponse failureResponse = new GenericResponse { Success = false };
                return new NotFoundObjectResult(failureResponse);
            }
            ApplicantViewModel mappedApplicant = _mapper.Map<ApplicantViewModel>(applicant);
            DataGenericResponse<ApplicantViewModel> response = new DataGenericResponse<ApplicantViewModel> { Success = true, Data = mappedApplicant };
            return new OkObjectResult(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(DataGenericResponse<int>), 200)]
        [ProducesResponseType(typeof(DataGenericResponse<List<KeyValuePair<string, string>>>), 400)]
        public IActionResult Create([FromBody] CreateApplicantViewModel request)
        {
            CreateApplicantViewModelValidator validator = new CreateApplicantViewModelValidator(_countryDataService, _applicantDataService, _configuration);
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                DataGenericResponse<List<KeyValuePair<string, string>>> failureResponse = new DataGenericResponse<List<KeyValuePair<string, string>>>();
                failureResponse.Success = false;
                foreach (var error in validationResult.Errors)
                {
                    failureResponse.Data.Add(new KeyValuePair<string, string>(error.PropertyName, error.ErrorMessage));
                }
                return new BadRequestObjectResult(failureResponse);
            }
            Applicant mappedApplicant = _mapper.Map<Applicant>(request);
            int createdApplicantId = _applicantDataService.Create(mappedApplicant);
            DataGenericResponse<int> response = new DataGenericResponse<int>();
            if (createdApplicantId > 0)
            {
                response.Success = true;
                response.Data = createdApplicantId;
            }
            return new OkObjectResult(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(GenericResponse), 200)]
        [ProducesResponseType(typeof(DataGenericResponse<List<KeyValuePair<string, string>>>), 400)]
        public IActionResult Update([FromBody] UpdateApplicantViewModel request)
        {
            UpdateApplicantViewModelValidator validator = new UpdateApplicantViewModelValidator(_countryDataService, _applicantDataService, _configuration);
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                DataGenericResponse<List<KeyValuePair<string, string>>> failureResponse = new DataGenericResponse<List<KeyValuePair<string, string>>>();
                failureResponse.Success = false;
                foreach (var error in validationResult.Errors)
                {
                    failureResponse.Data.Add(new KeyValuePair<string, string>(error.PropertyName, error.ErrorMessage));
                }
                return new BadRequestObjectResult(failureResponse);
            }
            Applicant mappedApplicant = _mapper.Map<Applicant>(request);
            bool result = _applicantDataService.Update(mappedApplicant);
            GenericResponse response = new GenericResponse { Success = result };

            return new OkObjectResult(response);
        }
    }
}

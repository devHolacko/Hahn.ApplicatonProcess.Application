using AutoMapper;
using Hahn.ApplicatonProcess.December2020.Data.Services.ApplicantService;
using Hahn.ApplicatonProcess.December2020.Data.Services.CountryService;
using Hahn.ApplicatonProcess.December2020.Domain.Models;
using Hahn.ApplicatonProcess.December2020.Web.Validators;
using Hahn.ApplicatonProcess.December2020.Web.ViewModels.Applicant;
using Hahn.ApplicatonProcess.December2020.Web.ViewModels.Base;
using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// An api that gets all countries as lookup
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /1
        ///
        /// </remarks>
        /// <returns>List of key value pair for all countries</returns>
        /// <response code="200">Returns the selected applicant</response>
        /// /// <response code="404">If the applicant is not found</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataGenericResponse<ApplicantViewModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GenericResponse))]
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

        [HttpDelete]
        [ProducesResponseType(typeof(GenericResponse), 200)]
        [ProducesResponseType(typeof(GenericResponse), 404)]
        [Route("{id}")]
        public IActionResult Delete([FromRoute(Name = "id")] int id)
        {
            Applicant applicant = _applicantDataService.Get(id);
            if (applicant == null)
            {
                GenericResponse failureResponse = new GenericResponse { Success = false };
                return new NotFoundObjectResult(failureResponse);
            }
            bool result = _applicantDataService.Delete(id);
            GenericResponse response = new GenericResponse { Success = result };
            return new OkObjectResult(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(DataGenericResponse<List<ApplicantListItem>>), 200)]
        [ProducesResponseType(typeof(GenericResponse), 404)]
        [Route("size/{pageSize}/page/{pageNumber}")]
        public IActionResult Get([FromRoute(Name = "pageSize")] int pageSize, [FromRoute(Name = "pageNumber")] int pageNumber, string search = "")
        {
            List<Applicant> applicants = _applicantDataService.GetApplicantsWithPaging(search);
            if (applicants == null)
            {
                GenericResponse failureResponse = new GenericResponse { Success = false };
                return new NotFoundObjectResult(failureResponse);
            }
            List<ApplicantListItem> mappedApplicants = _mapper.Map<List<ApplicantListItem>>(applicants);
            DataGenericResponse<List<ApplicantListItem>> response = new DataGenericResponse<List<ApplicantListItem>> { Success = true, Data = mappedApplicants };
            return new OkObjectResult(response);
        }
    }
}

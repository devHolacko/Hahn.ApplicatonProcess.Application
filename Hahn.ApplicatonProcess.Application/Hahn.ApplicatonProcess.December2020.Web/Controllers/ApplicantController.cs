using AutoMapper;
using Hahn.ApplicatonProcess.December2020.Data.Services.ApplicantService;
using Hahn.ApplicatonProcess.December2020.Data.Services.CountryService;
using Hahn.ApplicatonProcess.December2020.Domain.Models;
using Hahn.ApplicatonProcess.December2020.Web.Validators;
using Hahn.ApplicatonProcess.December2020.Web.ViewModels.Applicant;
using Hahn.ApplicatonProcess.December2020.Web.ViewModels.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;

namespace Hahn.ApplicatonProcess.December2020.Web.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class ApplicantController : BaseApiController
    {
        //private readonly IStringLocalizer<ApplicantController> _localizer;
        private readonly IApplicantDataService _applicantDataService;
        private readonly ICountryDataService _countryDataService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public ApplicantController(IApplicantDataService applicantDataService, ICountryDataService countryDataService, IConfiguration configuration, IMapper mapper)
        {
            //_localizer = localizer;
            _applicantDataService = applicantDataService;
            _countryDataService = countryDataService;
            _configuration = configuration;
            _mapper = mapper;
        }

        /// <summary>
        /// An api that gets an applicant's details given the applicant's id
        /// </summary>
        /// <returns>Applicant's data</returns>
        /// <response code="200">Returns the selected applicant</response>
        /// <response code="404">If the applicant is not found</response>
        [HttpGet]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataGenericResponse<ApplicantViewModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GenericResponse))]
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

        /// <summary>
        /// An api that creates a new applicant
        /// </summary>
        /// <param name="request">
        /// </param>
        /// <returns>Returns created applicant url</returns>
        /// <response code="200">Returns the created applicant url</response>
        /// <response code="400">If the request data is invalid</response>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DataGenericResponse<int>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DataGenericResponse<List<KeyValuePair<string, string>>>))]
        public IActionResult Create([FromBody] CreateApplicantViewModel request)
        {
            CreateApplicantViewModelValidator validator = new CreateApplicantViewModelValidator(_countryDataService, _applicantDataService, _configuration);
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                DataGenericResponse<List<KeyValuePair<string, string>>> failureResponse = new DataGenericResponse<List<KeyValuePair<string, string>>>();
                failureResponse.Success = false;
                failureResponse.Data = new List<KeyValuePair<string, string>>();
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

        /// <summary>
        /// An api tha updates an existing applicant
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200">Returns the requests result</response>
        /// <response code="400">If the request data is invalid</response>
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
                failureResponse.Data = new List<KeyValuePair<string, string>>();
                foreach (var error in validationResult.Errors)
                {
                    failureResponse.Data.Add(new KeyValuePair<string, string>(error.PropertyName, error.ErrorMessage));
                }
                return new BadRequestObjectResult(failureResponse);
            }

            Applicant originalApplicant = _applicantDataService.Get(request.Id);
            if (originalApplicant == null)
            {
                GenericResponse notFoundResponse = new GenericResponse() { Success = false };
                return new NotFoundObjectResult(notFoundResponse);
            }

            Applicant mappedApplicant = _mapper.Map<Applicant>(request);
            mappedApplicant.CreatedOn = originalApplicant.CreatedOn;
            mappedApplicant.ModifiedOn = originalApplicant.ModifiedOn;
            bool result = _applicantDataService.Update(mappedApplicant);
            GenericResponse response = new GenericResponse { Success = result };

            return new OkObjectResult(response);
        }

        /// <summary>
        /// An api that deletes an applicant's details given the applicant's id
        /// </summary>
        /// <returns>Request's status</returns>
        /// <response code="200">Returns the request's status</response>
        /// <response code="404">If the applicant is not found</response>
        [HttpDelete]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GenericResponse))]
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

        /// <summary>
        /// An api that gets all the applicants' list with paging
        /// </summary>
        /// <returns>List of applicants</returns>
        /// <response code="200">Returns list of applicant</response>
        /// <response code="404">If no applicants found</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataGenericResponse<List<ApplicantListItem>>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GenericResponse))]
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

using FluentValidation;
using Hahn.ApplicatonProcess.December2020.Data.Services.ApplicantService;
using Hahn.ApplicatonProcess.December2020.Data.Services.CountryService;
using Hahn.ApplicatonProcess.December2020.Domain.Models;
using Hahn.ApplicatonProcess.December2020.Web.ViewModels.Applicant;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Hahn.ApplicatonProcess.December2020.Web.Validators
{
    public class UpdateApplicantViewModelValidator : AbstractValidator<UpdateApplicantViewModel>
    {
        private readonly ICountryDataService _countryDataService;
        private readonly IApplicantDataService _applicantDataService;
        private readonly IConfiguration _configuration;
        public UpdateApplicantViewModelValidator(ICountryDataService countryDataService, IApplicantDataService applicantDataService, IConfiguration configuration)
        {
            _countryDataService = countryDataService;
            _applicantDataService = applicantDataService;
            _configuration = configuration;

            RuleFor(a => a.Name).NotEmpty().MinimumLength(_configuration.GetValue<int>("Validations:Applicant:NameMinimumLength"));
            RuleFor(a => a.FamilyName).NotEmpty().MinimumLength(_configuration.GetValue<int>("Validations:Applicant:FamilyNameMinimumLength"));
            RuleFor(a => a.Address).NotEmpty().MinimumLength(_configuration.GetValue<int>("Validations:Applicant:AddressMinimumLength"));
            RuleFor(a => a.CountryOfOrigin).NotEmpty().Must(IsValidCountry);
            RuleFor(a => a.EmailAddress).NotEmpty().EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
            RuleFor(a => a).Must(x => IsUniqueEmail(x.Id, x.EmailAddress));
            RuleFor(a => a.Age).GreaterThanOrEqualTo(_configuration.GetValue<int>("Validations:Applicant:AgeMinimumValue")).LessThanOrEqualTo(_configuration.GetValue<int>("Validations:Applicant:AgeMaximumValue"));
        }

        private bool IsValidCountry(string countryName)
        {
            return _countryDataService.CheckCountryExist(countryName).Result;
        }

        private bool IsUniqueEmail(int applicantId, string email)
        {
            List<Applicant> applicantsWithEmail = _applicantDataService.Filter(c => string.Equals(c.EmailAddress, email, System.StringComparison.OrdinalIgnoreCase)).ToList();
            bool emailInUse = applicantsWithEmail.Count > 1 && applicantsWithEmail.Any(c => c.Id == applicantId);
            return !emailInUse;
        }
    }
}

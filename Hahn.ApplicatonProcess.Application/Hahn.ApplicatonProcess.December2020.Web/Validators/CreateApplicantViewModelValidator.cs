using FluentValidation;
using Hahn.ApplicatonProcess.December2020.Data.Services.ApplicantService;
using Hahn.ApplicatonProcess.December2020.Data.Services.CountryService;
using Hahn.ApplicatonProcess.December2020.Web.ViewModels.Applicant;
using Microsoft.Extensions.Configuration;

namespace Hahn.ApplicatonProcess.December2020.Web.Validators
{
    public class CreateApplicantViewModelValidator : AbstractValidator<CreateApplicantViewModel>
    {
        private readonly ICountryDataService _countryDataService;
        private readonly IApplicantDataService _applicantDataService;
        private readonly IConfiguration _configuration;
        public CreateApplicantViewModelValidator(ICountryDataService countryDataService,IApplicantDataService applicantDataService,IConfiguration configuration)
        {
            _countryDataService = countryDataService;
            _applicantDataService = applicantDataService;
            _configuration = configuration;

            RuleFor(a => a.Name).NotEmpty().MinimumLength(configuration.GetValue<int>("Validations:Applicant:NameMinimumLength"));
            RuleFor(a => a.FamilyName).NotEmpty().MinimumLength(configuration.GetValue<int>("Validations:Applicant:FamilyNameMinimumLength"));
            RuleFor(a => a.Address).NotEmpty().MinimumLength(configuration.GetValue<int>("Validations:Applicant:AddressMinimumLength"));
            RuleFor(a => a.CountryOfOrigin).NotEmpty().Must(IsValidCountry);
            RuleFor(a => a.EmailAddress).NotEmpty().EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible).Must(IsUniqueEmail);
            RuleFor(a => a.Age).LessThanOrEqualTo(configuration.GetValue<int>("Validations:Applicant:AgeMinimumValue")).GreaterThanOrEqualTo(configuration.GetValue<int>("Validations:Applicant:AgeMaximumValue"));
        }

        private bool IsValidCountry(string countryName)
        {
            return _countryDataService.CheckCountryExist(countryName).Result;
        }

        private bool IsUniqueEmail(string email)
        {
            return _applicantDataService.CheckEmailDuplicate(email);
        }
    }
}

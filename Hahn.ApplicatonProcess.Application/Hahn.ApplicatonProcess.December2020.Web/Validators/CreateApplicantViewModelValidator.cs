using FluentValidation;
using Hahn.ApplicatonProcess.December2020.Data.Services.ApplicantService;
using Hahn.ApplicatonProcess.December2020.Data.Services.CountryService;
using Hahn.ApplicatonProcess.December2020.Web.ViewModels;

namespace Hahn.ApplicatonProcess.December2020.Web.Validators
{
    public class CreateApplicantViewModelValidator : AbstractValidator<CreateApplicantViewModel>
    {
        private readonly ICountryDataService _countryDataService;
        private readonly IApplicantDataService _applicantDataService;
        public CreateApplicantViewModelValidator(ICountryDataService countryDataService,IApplicantDataService applicantDataService)
        {
            _countryDataService = countryDataService;
            _applicantDataService = applicantDataService;

            RuleFor(a => a.Name).NotEmpty().MinimumLength(5);
            RuleFor(a => a.FamilyName).NotEmpty().MinimumLength(5);
            RuleFor(a => a.Address).NotEmpty().MinimumLength(10);
            RuleFor(a => a.CountryOfOrigin).NotEmpty().Must(IsValidCountry);
            RuleFor(a => a.EmailAddress).NotEmpty().EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
        }

        private bool IsValidCountry(string countryName)
        {
            return _countryDataService.CheckCountryExist(countryName).Result;
        }
    }
}

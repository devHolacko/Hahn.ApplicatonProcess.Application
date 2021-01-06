using FluentValidation;
using Hahn.ApplicatonProcess.December2020.Data.ViewModels;

namespace Hahn.ApplicatonProcess.December2020.Web.Validators
{
    public class CreateApplicantViewModelValidator : AbstractValidator<CreateApplicantViewModel>
    {
        public CreateApplicantViewModelValidator()
        {
            RuleFor(a => a.Name).NotEmpty().MinimumLength(5);
            RuleFor(a => a.FamilyName).NotEmpty().MinimumLength(5);
            RuleFor(a => a.Address).NotEmpty().MinimumLength(10);
            RuleFor(a => a.CountryOfOrigin).NotEmpty().Must(IsValidCountry);
            RuleFor(a => a.EmailAddress).NotEmpty().EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
        }

        private bool IsValidCountry(string countryName)
        {
            return false;
        }
    }
}

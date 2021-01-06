using FluentValidation;
using Hahn.ApplicatonProcess.December2020.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.Validators
{
    public class ApplicantValidator : AbstractValidator<Applicant>
    {
        public ApplicantValidator()
        {
            RuleFor(a => a.Name).NotEmpty().MinimumLength(5);
            RuleFor(a => a.FamilyName).NotEmpty().MinimumLength(5);
            RuleFor(a => a.Address).NotEmpty().MinimumLength(10);
        }

        //private bool IsValidCountry(string country)
        //{

        //}
    }
}

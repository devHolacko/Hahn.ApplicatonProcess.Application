using AutoMapper;
using Hahn.ApplicatonProcess.December2020.Domain.Models;
using Hahn.ApplicatonProcess.December2020.Web.ViewModels.Applicant;

namespace Hahn.ApplicatonProcess.December2020.Web.Mappings
{
    public class ApplicantMapper : Profile
    {
        public ApplicantMapper()
        {
            CreateMap<CreateApplicantViewModel, Applicant>();
            CreateMap<Applicant, ApplicantViewModel>();
        }
    }
}

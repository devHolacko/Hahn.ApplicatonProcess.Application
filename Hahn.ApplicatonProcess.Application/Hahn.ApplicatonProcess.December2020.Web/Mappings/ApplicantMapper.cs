using AutoMapper;
using Hahn.ApplicatonProcess.December2020.Domain.Models;
using Hahn.ApplicatonProcess.December2020.Web.ViewModels.Applicant;
using System.Collections.Generic;

namespace Hahn.ApplicatonProcess.December2020.Web.Mappings
{
    public class ApplicantMapper : Profile
    {
        public ApplicantMapper()
        {
            CreateMap<CreateApplicantViewModel, Applicant>();
            CreateMap<Applicant, ApplicantViewModel>();
            CreateMap<UpdateApplicantViewModel, Applicant>();
            CreateMap<List<Applicant>, List<ApplicantListItem>>();
        }
    }
}

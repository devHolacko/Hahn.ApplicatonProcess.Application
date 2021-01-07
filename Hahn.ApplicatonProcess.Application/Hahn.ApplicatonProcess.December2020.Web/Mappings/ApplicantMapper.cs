using AutoMapper;
using Hahn.ApplicatonProcess.December2020.Domain.Models;
using Hahn.ApplicatonProcess.December2020.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Mappings
{
    public class ApplicantMapper : Profile
    {
        public ApplicantMapper()
        {
            CreateMap<CreateApplicantViewModel, Applicant>();
        }
    }
}

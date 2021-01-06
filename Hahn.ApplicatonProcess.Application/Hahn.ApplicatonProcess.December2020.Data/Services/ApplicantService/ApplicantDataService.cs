using Hahn.ApplicatonProcess.December2020.Domain.Interfaces;
using Hahn.ApplicatonProcess.December2020.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Data.Services.ApplicantService
{
    public class ApplicantDataService : IApplicantDataService
    {
        private readonly IRepository<Applicant> _applicantRepository;
        public ApplicantDataService(IRepository<Applicant> applicantRepository)
        {
            _applicantRepository = applicantRepository;
        }

        public int Create(Applicant applicant)
        {
            if (applicant == null)
                return -1;

            _applicantRepository.Create(applicant);

            return applicant.Id;
        }

        public bool CheckEmailDuplicate(string email)
        {
            return _applicantRepository.Filter(c => string.Equals(c.EmailAddress, email, StringComparison.OrdinalIgnoreCase)).Any();
        }

        public Applicant Get(int id)
        {
            return _applicantRepository.GetById(id);
        }

        public bool Update(Applicant applicant)
        {
            if (applicant == null)
                return false;

            return _applicantRepository.Edit(applicant);
        }

        public bool Delete(int id)
        {
            return _applicantRepository.Delete(id);
        }

        public List<Applicant> GetApplicantsWithPaging(string searchKeyword)
        {
            if (string.IsNullOrWhiteSpace(searchKeyword))
                return _applicantRepository.Filter(c => c.Active).ToList();

            return _applicantRepository.Filter(c =>
            c.Name.Contains(searchKeyword) || c.FamilyName.Contains(searchKeyword) || c.Address.Contains(searchKeyword)
            || Convert.ToString(c.Age).Contains(searchKeyword) || c.CountryOfOrigin.Contains(searchKeyword)
            || c.EmailAddress.Contains(searchKeyword) || Convert.ToString(c.Hired).Contains(searchKeyword))
                .ToList();
        }
    }
}

using Hahn.ApplicatonProcess.December2020.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Data.Services
{
    public interface IApplicantDataService
    {
        int Create(Applicant applicant);
        bool CheckEmailDuplicate(string email);
        Applicant Get(int id);
        bool Update(Applicant applicant);
        bool Delete(int id);
        List<Applicant> GetApplicantsWithPaging(string searchKeyword);
    }
}

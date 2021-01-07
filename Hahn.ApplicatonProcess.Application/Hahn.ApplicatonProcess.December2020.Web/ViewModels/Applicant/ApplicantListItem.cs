using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.ViewModels.Applicant
{
    public class ApplicantListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Country { get; set; }
        public bool Hired { get; set; }
    }
}

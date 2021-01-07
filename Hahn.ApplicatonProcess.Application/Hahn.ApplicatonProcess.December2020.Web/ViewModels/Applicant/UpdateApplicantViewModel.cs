using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.ViewModels.Applicant
{
    public class UpdateApplicantViewModel
    {
        /// <summary>
        /// Applicant's id
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// Applicant name
        /// </summary>
        /// <example>Abdullah</example>
        public string Name { get; set; }
        /// <summary>
        /// Applicant family name
        /// </summary>
        /// <example>El-Menawy</example>
        public string FamilyName { get; set; }
        /// <summary>
        /// Applicant address
        /// </summary>
        /// <example>101 Ain shams street - Cairo</example>
        public string Address { get; set; }
        /// <summary>
        /// Applicant's country of origin
        /// </summary>
        /// <example>Egypt</example>
        public string CountryOfOrigin { get; set; }
        /// <summary>
        /// Applicant's E-Mail
        /// </summary>
        /// <example>a.elmenawy@outlook.com</example>
        public string EmailAddress { get; set; }
        /// <summary>
        /// Applicant's age
        /// </summary>
        /// <example>27</example>
        public int Age { get; set; }
        /// <summary>
        /// Applicant's hiring status
        /// </summary>
        /// <example>true</example>
        public bool Hired { get; set; }
    }
}

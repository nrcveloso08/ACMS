using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Applicant
{
    public class ApplicantContactInfo
    {
        public Guid Applicant_Id { get; set; }
        public string FullName
        {
            get => this.FirstName?.Trim() + " " + this.LastName?.Trim();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public bool TextMessageOptIn { get; set; }
    }
}

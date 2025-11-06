using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Applicant
{
    public class ApplicantEmailAddress
    {
        public int Id { get; set; }
        public Guid ApplicantId { get; set; }
        [MaxLength(200)]
        public string EmailAddress { get; set; }
        public bool IsPrimary { get; set; }
    }
}

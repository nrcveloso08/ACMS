using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Applicant
{
    public class ApplicantPhoneNumber
    {
        public int Id { get; set; }
        public Guid Applicant_Id { get; set; }
        [MaxLength(50)]
        public string PhoneNumber { get; set; }
        public int Location_Id { get; set; }
        public bool IsPrimary { get; set; }
    }
}

using ATS.DTO.Applicant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Egypt
{
    public class EgyptAdditionalInfo
    {
        public int Id { get; set; }
        [ForeignKey("Applicant")]
        public Guid Applicant_Id { get; set; }
        public ApplicantBasicInfo Applicant { get; protected set; }
        public string National_Id { get; set; }
        public DateTimeOffset Created { get; set; } = DateTime.UtcNow;
        public string CreatedByUser { get; set; }
        public string CreatedByEmail { get; set; }
        public bool IsDeleted { get; set; } = false;
        public void SetUserData(string userName, string userEmail)
        {
            CreatedByUser = userName;
            CreatedByEmail = userEmail;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Guatemala
{
    public class GuatemalaAdditionalInfo
    {       
        public int Id { get; set; }
        public Guid Applicant_Id { get; set; }
        public string DPI { get; set; }       
        public string DPINo { get; set; }       
        public string Nit { get; set; }       
        public string Profession { get; set; }       
        public string PassportNo { get; set; }       
        public string WorkerCategoryCode { get; set; }       
        public string HomeDepartmentDescription { get; set; }       
        public string DaysOff { get; set; }       
        public string ScheduleHours { get; set; }
        public DateTime? HireDate { get; set; }
        public string Nationality { get; set; }
        public string BirthCountry { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string SecondaryEmail { get; set; }
        public string SecondaryMobile { get; set; }
        public int? EmploymentApplication_Id { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Guatemala
{
    public class GuatemalaAdditionalNames
    {
        public int id    { get; set; }
        public Guid Applicant_Id { get; set; }  
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string MiddleName { get; set; }
        public string SecondLastName { get; set; }
        public string MarriedLastName { get; set; }
        public int? EmploymentApplication_Id { get; set; }

    }
}

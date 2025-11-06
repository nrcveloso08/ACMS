using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Guatemala
{
    internal class GuatemalaEducationInfo
    {
        public int Id { get; set; }
        public Guid Applicant_Id { get; set; }
        public string Institution { get; set; }
        public string Title { get; set; }
        public string AcademicLevel { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}

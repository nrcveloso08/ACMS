using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Hyderabad
{
    public class HyderabadAdditionalInfo
    {
        public int Id { get; set; }
        public Guid Applicant_Id { get; set; }
        public string EmployeeRole { get; set; }
        public string WaveCategory { get; set; }
        public string GrossSalary { get; set; }
        public string HourlyRate { get; set; }
    }
}

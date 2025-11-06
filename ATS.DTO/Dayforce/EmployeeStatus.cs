using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Dayforce
{
    public class EmployeeStatus
    {
        public int Id { get; set; }
        public string EmployeeNumber { get; set; }
        public string EmploymentStatusGroupName { get; set; }
        public DateTime EmployeeEmploymentStatusEffectiveStart { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string StatusReasonName { get; set; }
    }
}

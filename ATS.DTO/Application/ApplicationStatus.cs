using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Application
{
    public class ApplicationStatus
    {
        public int Id { get; set; }
        public InternalApplicationStatus InternalStatus { get; set; } = InternalApplicationStatus.Unset;
        public string Name { get; set; }
        public int PrerequisiteId { get; set; } = -1;
        public bool IsDeleted { get; set; } = false;
    }

}

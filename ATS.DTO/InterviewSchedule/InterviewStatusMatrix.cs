using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.InterviewSchedule
{
    public class InterviewStatusMatrix
    {
        public int Id { get; set; }
        public InterviewStatus CurrentStatus { get; set; }
        public InterviewStatus NextStatus { get; set; }
    }
}

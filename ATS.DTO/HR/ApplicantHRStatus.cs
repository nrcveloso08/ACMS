using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.HR
{
    public class ApplicantHRStatus
    {
        public int Id { get; set; }
        public Guid ApplicantId { get; set; }
        public HRStatus HRStatus { get; set; }
    }

    public enum HRStatus
    {
        New = 1,
        In_Progress,
        Completed
    }
}

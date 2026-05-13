using ATS.DTO.InterviewSchedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.FCRA.EndpointParameters
{
    public class FCRAFilters
    {
        public FCRAStatus? Status { get; set; }
        public IList<int> LocationIds { get; set; }
        public int LineOfBusinessId { get; set; }
        public string ApplicantName { get; set; }
        public int Wave { get; set; }
        public PaginationFilters paginationFilters { get; set; }
        public InterviewStatus CurrentStatus { get; set; }
        public int Client_Id { get; set; }
        public Nullable<DateTime> StartDate { get; set; }
        public Nullable<DateTime> EndDate { get; set; }
    }
}

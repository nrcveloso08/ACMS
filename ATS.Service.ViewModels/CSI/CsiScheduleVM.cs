using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Service.ViewModels.CSI
{
    public class CsiScheduleVM
    {
        public int Id { get; set; }
        public int LineOfBusinessId { get; set; }
        public string LineOfBusiness { get; set; }
        public double Offset { get; set; }
        public int RequisitionId { get; set; }
        public int LocationId { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public int BookedApplicants { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public CSIBatchStatus Status { get; set; }
        public string StatusDisplay { get; set; }
        public DateTimeOffset Created { get; set; }
        public string CreatedBy { get; set; }
    }
    public enum CSIBatchStatus
    {
        Open = 1,
        In_Progress = 2,
        Completed = 3
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.RequisitionRequest
{
    public class Requisition
    {
        public int Id { get; set; }
        public DateTime TrainingStartDate { get; set; }
        public DateTime TrainingEndDate { get; set; }
        public TimeSpan? TrainingStartTimePerDay { get; set; }
        public TimeSpan? TrainingEndTimePerDay { get; set; }
        public string Wave { get; set; }
        public int LineOfBusiness_Id { get; set; }
        public string LineOfBusiness { get; set; }
        public int Client_Id { get; set; }
        public int Location_Id { get; set; }
        public bool IsFincare { get; set; }
        public string Location { get; set; }
        public string Country { get; set; }
        public HireType HireType { get; set; }
        public bool WorkingFromHome { get; set; }
        public DateTime RecruitmentCloseDate { get; set; }
        public IList<RequisitionWaveNote> RequisitionWaveNotes { get; set; }
        public IList<RequisitionRequestDetail> Details { get; set; }
        public int JobXrefCode_Id { get; set; }
        public int DepartmentXrefCode_Id { get; set; }
        public int OrganizationXrefCode_Id { get; set; }
        public DateTime? ABAYStartDate { get; set; }
        public DateTime? ABAYEndDate { get; set; }
        public string HiringAvailabilityGSheet { get; set; }
        public string AbayClassroomTimezone { get; set; }
        public string AbayClassroomWeekDays { get; set; }
        public int RequisitionType { get; set; }
        public bool TrainFromCampus { get; set; }
    }

    public enum HireType
    {
        Permanent = 1, Seasonal, Both
    }
}

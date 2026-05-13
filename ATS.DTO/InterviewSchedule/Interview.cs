using ATS.DTO.Applicant;
using ATS.DTO.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.InterviewSchedule
{
    public class Interview 
    {

        public int Id { get; set; }
        public Guid Applicant_Id { get; set; }
        public int RequisitionRequestId { get; set; }
        public int LocationId { get; set; }
        public int LineBusinessId { get; set; }
        public InterviewMode Mode { get; set; } = 0;
        public IList<SelectionList> ModeSelection { get; set; } = EnumHelper.ToSelectionList<InterviewMode>();
        public ICollection<ApplicantInterviewResponses> Responses { get; set; }

        public string ModeDisplayText
        {
            get
            {
                return this.Mode == 0 ? "" : this.Mode.GetDescription();
            }
        }
        public DateTimeOffset Created { get; set; } = DateTime.UtcNow;
        public string CreatedByUser { get; set; }
        public string CreatedByEmail { get; set; }
        public bool IsDeleted { get; set; } = false;
        public void SetUserData(string userName, string userEmail)
        {
            CreatedByUser = userName;
            CreatedByEmail = userEmail;
        }
    }

    public enum InterviewMode
    {
        On_Site = 1, Video, Phone
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Service.ViewModels.JobAdvertisement
{
    public class JobAdvertisementVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PublishedTitle { get; set; }
        public int CATSJobId { get; set; }
        public string Description { get; set; }
        public int LocationId { get; set; }
        public string LegacyRecruitmentLob { get; set; }
        public string AddressLine { get; set; }
        public string ZipCode { get; set; }
        public string InstructionHTML { get; set; }
        public string PhoneNumber { get; set; }
        public string GoogleMapIframeSrc { get; set; }
        public string GoogleMapShortURL { get; set; }
        public bool SelfSchedulerAppointmentEnabled { get; set; } = true;
        public bool PrescreeningQuestionsEnabled { get; set; } = true;
        public string Disclaimer { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsPublishedToLiveJobs { get; set; }
        public int? RequisitionRequestId { get; set; }
        public string HarverVacancyURL { get; set; } = null;
        public bool IsSMSEnabled { get; set; } = true;
        public bool IsReferralEnabled { get; set; } = true;
        public bool IsSMSOptEnabled { get; set; } = true;
        public int LanguageId { get; set; }
    }
}

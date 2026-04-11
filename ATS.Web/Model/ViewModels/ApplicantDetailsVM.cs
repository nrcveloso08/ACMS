using ATS.DTO.Applicant;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ATS.Web.Model.ViewModels
{
    public class ApplicantDetailsVM : ApplicantDetailsDTO
    {
        public ApplicantDetailsVM()
        {
            OnboardingPolicyXrefCodes = new List<SelectListItem>();
            JobXrefCodes = new List<SelectListItem>();
            DepartmentXrefCodes = new List<SelectListItem>();
            OrganizationXrefCodes = new List<SelectListItem>();
        }

        // =============================
        // UI DROPDOWNS
        // =============================

        public IList<SelectListItem> JobLocations { get; set; }
        public IList<SelectListItem> AvailableJobs { get; set; }

        public IList<SelectListItem> OnboardingPolicyXrefCodes { get; set; }
        public IList<SelectListItem> JobXrefCodes { get; set; }
        public IList<SelectListItem> DepartmentXrefCodes { get; set; }
        public IList<SelectListItem> OrganizationXrefCodes { get; set; }

        // =============================
        // FORM SELECTION FIELDS
        // =============================

        [Required]
        [Display(Name = "Onboarding Policy")]
        public int OnboardingPolicyXrefCode_Id { get; set; }

        [Required]
        [Display(Name = "Job")]
        public int JobXrefCode_Id { get; set; }

        [Required]
        [Display(Name = "Department")]
        public int DepartmentXrefCode_Id { get; set; }

        [Required]
        [Display(Name = "Organization")]
        public int OrganizationXrefCode_Id { get; set; }
    }
}

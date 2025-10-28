using ATS.Service.ViewModels.InterviewSchedule;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATS.Web.Controllers
{
    public class InterviewScheduleController : Controller
    {
        private static readonly List<InterviewScheduleVM> _mockSchedules = new()
    {
        new InterviewScheduleVM
        {
            TimeSlot = "08:30 AM",
            Applicants = new List<ApplicantViewModel>
            {
                new ApplicantViewModel { Name = "Kassandra Barrios", Phone = "+1 (480) 388-7748", Email = "kassandra.barrios@gmail.com", Status = "Not Right Now" },
                new ApplicantViewModel { Name = "Rey Singh", Phone = "+1 (760) 604-4834", Email = "reysingh@gmail.com", Status = "NCNS" }
            }
        },
        new InterviewScheduleVM
        {
            TimeSlot = "09:00 AM",
            Applicants = new List<ApplicantViewModel>
            {
                new ApplicantViewModel { Name = "Amy Okinwa", Phone = "+1 (404) 625-9029", Email = "amy.okinwa@gmail.com", Status = "Hired" }
            }
        }
    };

        public IActionResult Index(DateTime? date, string mode = "all")
        {
            return View(_mockSchedules);
        }

        [HttpGet]
        public IActionResult GetSchedules(DateTime? date, string mode = "all")
        {
            var schedules = _mockSchedules;

            if (mode != "all")
            {
                schedules = schedules
                    .Where(s => s.Applicants.Any(a => a.Status.Equals(mode, StringComparison.OrdinalIgnoreCase)))
                    .ToList();
            }

            return PartialView("Partials/_RenderSchedulePartial", schedules);
        }

        public IActionResult LoadInterviewFlyout(Guid applicantId)
        {
            try
            {
                var applicant = new ApplicantViewModel
                {
                    Id = applicantId,
                    Name = "Shamarie Prawl",
                    Email = "shamarie@example.com",
                    Phone = "(204) 987-6543"
                };

                return PartialView("~/Views/InterviewSchedule/Partials/_InterviewFlyout.cshtml", applicant);
            }
            catch (Exception ex)
            {
                return Content($"Flyout error: {ex.Message}\nStack: {ex.StackTrace}");
            }
        }


    }
}

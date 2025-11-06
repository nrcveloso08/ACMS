using ATS.DTO.InterviewSchedule;
using Microsoft.AspNetCore.Mvc;

namespace ATS.Web.Controllers
{
    public class InterviewScheduleController : Controller
    {
        private static readonly List<InterviewSchedule> _mockSchedules = new()
    {
        new InterviewSchedule
        {
            TimeSlot = "08:30 AM",
            Applicants = new List<ApplicantViewModel>
            {
                new ApplicantViewModel { Name = "Test Dummy1", Phone = "+9 (999) 999-9999", Email = "test@example.com", Status = "Not Right Now" },
                new ApplicantViewModel { Name = "Test Dummy2", Phone = "+9 (999) 999-9999", Email = "test@example.com", Status = "NCNS" }
            }
        },
        new InterviewSchedule
        {
            TimeSlot = "09:00 AM",
            Applicants = new List<ApplicantViewModel>
            {
                new ApplicantViewModel { Name = "Test Dummy3", Phone = "+9 (999) 999-999", Email = "test01@example.com", Status = "Hired" }
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
                    Name = "Test Dummy",
                    Email = "test@example.com",
                    Phone = "(999) 999-999"
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

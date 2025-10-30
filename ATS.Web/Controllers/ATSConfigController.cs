using Microsoft.AspNetCore.Mvc;

namespace ATS.Web.Controllers
{
    public class ATSConfigController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult AdvertisementSource()
            => PartialView("~/Views/ATSConfig/Partials/_AdvertisementSourcePartial.cshtml");

        public IActionResult AlternateSystemLocations()
            => PartialView("~/Views/ATSConfig/Partials/_AlternateSystemLocationsPartial.cshtml");

        public IActionResult HarverVacancies()
            => PartialView("~/Views/ATSConfig/Partials/_HarverVacanciesPartial.cshtml");

        public IActionResult Vacancies()
            => PartialView("~/Views/ATSConfig/Partials/_VacanciesPartial.cshtml");

        public IActionResult InterviewStatus()
            => PartialView("~/Views/ATSConfig/Partials/_InterviewStatusPartial.cshtml");

        public IActionResult PrescreeningQuestions()
            => PartialView("~/Views/ATSConfig/Partials/_PrescreeningQuestionsPartial.cshtml");

        public IActionResult ScheduleExceptions()
            => PartialView("~/Views/ATSConfig/Partials/_ScheduleExceptionsPartial.cshtml");

        public IActionResult EmailTemplates()
            => PartialView("~/Views/ATSConfig/Partials/_EmailTemplatesPartial.cshtml");

        public IActionResult SMSTemplate()
            => PartialView("~/Views/ATSConfig/Partials/_SMSTemplatePartial.cshtml");

        public IActionResult SMSNumbers()
            => PartialView("~/Views/ATSConfig/Partials/_SMSNumbersPartial.cshtml");

        public IActionResult MenuConfig()
            => PartialView("~/Views/ATSConfig/Partials/_MenuConfigPartial.cshtml");

        public IActionResult StatusMaintenance()
            => PartialView("~/Views/ATSConfig/Partials/_StatusMaintenancePartial.cshtml");

        public IActionResult TimeSlotConfiguration()
            => PartialView("~/Views/ATSConfig/Partials/_TimeSlotConfigurationPartial.cshtml");

        public IActionResult JobAdvertisement()
            => PartialView("~/Views/ATSConfig/Partials/_JobAdvertisementPartial.cshtml");
    }

}

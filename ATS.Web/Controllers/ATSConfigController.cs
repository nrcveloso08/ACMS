using ATS.DTO.AdvertisementSource;
using ATS.DTO.JobAdvertisement;
using ATS.DTO.Vacancies;
using ATS.Service;
using Microsoft.AspNetCore.Mvc;

namespace ATS.Web.Controllers
{
    public class ATSConfigController : Controller
    {
        private readonly IAdvertisementSourceService _advertisementSourceService;
        private readonly IHarverService _harverService;
        private readonly IJobAdvertisementService _jobAdvertisementService;

        public ATSConfigController(IAdvertisementSourceService advertisementSourceService,
            IHarverService harverService,
            IJobAdvertisementService jobAdvertisementService)
        {
            _advertisementSourceService = advertisementSourceService;
            _harverService = harverService;
            _jobAdvertisementService = jobAdvertisementService;
        }

        public IActionResult Index() => View();

        public async Task<IActionResult> GetAdvertisementSources()
        {
            try
            {
                var result = await _advertisementSourceService.GetAll();
                return Json(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddOrUpdate([FromBody] AdvertisementSource obj)
        {
            try
            {
                if (obj == null)
                    return Json(new { success = false, message = "Invalid request: No data provided." });

                var result = await _advertisementSourceService.AddOrUpdate(obj);
                return Json(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public async Task<IActionResult> GetHarverVacancies()
        {
            try
            {
                var result = await _harverService.GetAll();
                return Json(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> HarverVacancyAddOrUpdate([FromBody] Vacancy obj)
        {
            try
            {
                if (obj == null)
                    return Json(new { success = false, message = "Invalid request: No data provided." });

                var result = await _harverService.AddOrUpdate(obj);
                return Json(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public async Task<IActionResult> GetJobAdvertisements()
        {
            try
            {
                var result = await _jobAdvertisementService.GetAll();
                return Json(new { success = true, message = result });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> JobAdAddOrUpdate([FromBody] JobAdvertisement obj)
        {
            try
            {
                if (obj == null)
                    return Json(new { success = false, message = "Invalid request: No data provided." });

                var result = await _jobAdvertisementService.AddOrUpdate(obj);
                return Json(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

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

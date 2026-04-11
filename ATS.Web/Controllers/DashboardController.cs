using ATS.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATS.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ICandidateDetailService _candidateDetailService;

        public DashboardController(ICandidateDetailService candidateDetailService)
        {
            _candidateDetailService = candidateDetailService;
        }   


        // GET: DashboardController
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RenderApplicantChart()
        {
            return PartialView("~/Views/Dashboard/Partials/_ApplicantChart.cshtml");
        }

        public IActionResult NewApplicant()
        {
            return View();
        }

        // GET: DashboardController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DashboardController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DashboardController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DashboardController/Edit/5
        public ActionResult Edit()
        {
            return View();
        }

        public async Task<IActionResult> GetApplicantDetails(Guid applicantId)
        {           
            try
            {
                var result = await _candidateDetailService.GetApplicantDetailsAsync(applicantId);
                return Json(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }

        // POST: DashboardController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DashboardController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DashboardController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace ATS.Web.Controllers
{
    public class HrProcessingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult LoadFlyout(string name, string wave, string location, string wage, string hireType)
        {
            try
            {
                // Dummy model for now (will be displayed in partial)
                var model = new
                {
                    Name = name ?? "N/A",
                    Wave = wave ?? "N/A",
                    Location = location ?? "N/A",
                    Wage = wage ?? "N/A",
                    HireType = hireType ?? "N/A"
                };

                return PartialView("Partials/_FlyoutPartial", model);
            }
            catch (Exception ex)
            {
                // log ex if you have a logger
                return StatusCode(500, $"Flyout load failed: {ex.Message}");
            }
        }
    }
}

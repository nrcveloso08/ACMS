using Microsoft.AspNetCore.Mvc;

namespace ATS.Web.Controllers
{
    public class JobResponseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }
        [HttpGet]
        public IActionResult LoadStep(int step)
        {
            // Map step numbers to view filenames
            string viewName = step switch
            {
                1 => "_1PersonalInfo",
                2 => "_2EmploymentHistory",
                3 => "_3Questions",
                4 => "_4Education",
                5 => "_5Language",
                6 => "_6Documents",
                7 => "_7Reference",
                _ => null
            };

            if (string.IsNullOrEmpty(viewName))
                return BadRequest("Invalid step number");

            // ✅ Full and explicit partial view path (prevents Razor View Engine confusion)
            string fullViewPath = $"~/Views/JobResponse/Guatemala/{viewName}.cshtml";

            // ✅ Verify the file actually exists before returning it
            if (!System.IO.File.Exists(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Views", "JobResponse", "Guatemala", $"{viewName}.cshtml")))
            {
                return NotFound($"Partial view '{viewName}' not found under /Views/JobResponse/Guatemala/");
            }

            // ✅ Return the partial view
            return PartialView(fullViewPath);
        }

        [HttpGet]
        public IActionResult GetDocuments()
        {
            var docs = new List<object>
    {
        new { id = 1, description = "DPI Frontal", content = "Imagen Frontal" },
        new { id = 2, description = "DPI Trasera", content = "Imagen Trasera" },
        new { id = 3, description = "Diploma", content = "Diploma" }
    };

            return Json(docs);
        }

    }
}

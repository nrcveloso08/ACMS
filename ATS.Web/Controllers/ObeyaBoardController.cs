using Microsoft.AspNetCore.Mvc;

namespace ATS.Web.Controllers
{
    public class ObeyaBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoadTabular()
        {
            return PartialView("_Tabular");
        }
    }
}

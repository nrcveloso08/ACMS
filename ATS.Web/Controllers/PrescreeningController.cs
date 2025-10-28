using Microsoft.AspNetCore.Mvc;

namespace ATS.Web.Controllers
{
    public class PrescreeningController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search()
        {
            return View();
        }
    }
}

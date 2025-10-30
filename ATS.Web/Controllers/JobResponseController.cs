using Microsoft.AspNetCore.Mvc;

namespace ATS.Web.Controllers
{
    public class JobResponseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

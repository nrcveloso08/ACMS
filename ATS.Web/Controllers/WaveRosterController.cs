using Microsoft.AspNetCore.Mvc;

namespace ATS.Web.Controllers
{
    public class WaveRosterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

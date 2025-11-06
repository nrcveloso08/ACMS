using ATS.DTO.CSI;
using Microsoft.AspNetCore.Mvc;

namespace ATS.Web.Controllers
{
    public class CsiScheduleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create([FromBody] CsiSchedule model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            // TODO: Save to database here
            // _context.CsiSchedules.Add(model);
            // _context.SaveChanges();

            return Ok(new { success = true, message = "CSI Schedule created successfully!" });
        }

    }
}

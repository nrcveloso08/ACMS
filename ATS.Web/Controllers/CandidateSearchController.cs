using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATS.Web.Controllers
{
    public class CandidateSearchController : Controller
    {
        // GET: CandidateSearchController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CandidateSearchController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CandidateSearchController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CandidateSearchController/Create
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

        // GET: CandidateSearchController/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            return View();
        }

        // POST: CandidateSearchController/Edit/5
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

        // GET: CandidateSearchController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CandidateSearchController/Delete/5
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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATS.Web.Controllers
{
    public class PreScreenCandidateSearchController : Controller
    {
        // GET: PreScreenCandidateSearchController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PreScreenCandidateSearchController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PreScreenCandidateSearchController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PreScreenCandidateSearchController/Create
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

        // GET: PreScreenCandidateSearchController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PreScreenCandidateSearchController/Edit/5
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

        // GET: PreScreenCandidateSearchController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PreScreenCandidateSearchController/Delete/5
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

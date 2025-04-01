using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalDevices.Controllers
{
    public class ProductTypesController : Controller
    {
        // GET: ProductTypesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProductTypesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductTypesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductTypesController/Create
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

        // GET: ProductTypesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductTypesController/Edit/5
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

        // GET: ProductTypesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductTypesController/Delete/5
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

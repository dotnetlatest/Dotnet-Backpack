using System.Web.Mvc;

namespace Backpack.MVC.Site.Areas.NorthwindShop.Controllers
{
    public class PortalController : Controller
    {
        // GET: NorthwindShop/Home
        public ActionResult Index()
        {
            return View();
        }

        // GET: NorthwindShop/Home/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NorthwindShop/Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NorthwindShop/Home/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: NorthwindShop/Home/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NorthwindShop/Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: NorthwindShop/Home/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NorthwindShop/Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

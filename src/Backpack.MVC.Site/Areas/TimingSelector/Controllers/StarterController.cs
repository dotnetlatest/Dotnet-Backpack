using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Backpack.MVC.Site.Areas.TimingSelector.Controllers
{
    public class StarterController : Controller
    {
        // GET: TimingSelector/Starter
        public ActionResult Index()
        {
            return View();
        }

        // GET: TimingSelector/Starter/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TimingSelector/Starter/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TimingSelector/Starter/Create
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

        // GET: TimingSelector/Starter/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TimingSelector/Starter/Edit/5
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

        // GET: TimingSelector/Starter/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TimingSelector/Starter/Delete/5
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

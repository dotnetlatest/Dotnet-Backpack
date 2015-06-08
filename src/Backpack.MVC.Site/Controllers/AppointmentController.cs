using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Backpack.MVC.Site.Models;

namespace Backpack.MVC.Site.Controllers
{
    public class AppointmentController : Controller
    {
        // GET: Appointment
        public ActionResult Index()
        {
            return View();
        }


        public ViewResult MakeBooking()
        {
            List<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem {Text = "Action", Value = "0"},
                new SelectListItem {Text = "Drama", Value = "1"},
                new SelectListItem {Text = "Comedy", Value = "2", Selected = true},
                new SelectListItem {Text = "Science Fiction", Value = "3"}
            };





            ViewBag.MovieType = items;

            return View(new Appointment {Date = DateTime.Now});
        }

        [HttpPost]
        public ViewResult MakeBooking(Appointment appt)
        {
            return ModelState.IsValid ? View("Completed", appt) : View();
        }

        public ViewResult Completed()
        {
            return View();
        }
        // GET: Appointment/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Appointment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Appointment/Create
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

        // GET: Appointment/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Appointment/Edit/5
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

        // GET: Appointment/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Appointment/Delete/5
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

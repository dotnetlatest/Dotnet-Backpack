﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Backpack.MVC.Site.Controllers
{
    public class HelpersController : Controller
    {
        // GET: Helpers
        [Route("mvc-helpers")]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Helpers/Details/5
        [Route("helpers/{id}")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Helpers/Create
        public ActionResult Create()
        {       
            return View();
        }

        // POST: Helpers/Create
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

        // GET: Helpers/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Helpers/Edit/5
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

        // GET: Helpers/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Helpers/Delete/5
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Backpack.MVC.Site.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult SelectCategory()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Action", Value = "0" });

            items.Add(new SelectListItem { Text = "Drama", Value = "1" });

            items.Add(new SelectListItem { Text = "Comedy", Value = "2", Selected = true });

            items.Add(new SelectListItem { Text = "Science Fiction", Value = "3" });

            ViewBag.MovieType = items;

            return View();
        }

        public ActionResult CategoryChosen(string MovieType)
        {
            ViewBag.messageString = string.Format("Category Chosen is {0}", MovieType);

            return View();
        }

        public ActionResult SelectCategoryEnum()
        {

            SetViewBagMovieType(eMovieCategories.Drama);

            return View("SelectCategory");

        }

        public enum eMovieCategories { Action, Drama, Comedy, Science_Fiction };

        private void SetViewBagMovieType(eMovieCategories selectedMovie)
        {

            IEnumerable<eMovieCategories> values =

                              Enum.GetValues(typeof(eMovieCategories))

                              .Cast<eMovieCategories>();

            IEnumerable<SelectListItem> items =

                from value in values

                select new SelectListItem

                {

                    Text = value.ToString(),

                    Value = value.ToString(),

                    Selected = value == selectedMovie,

                };



            ViewBag.MovieType = items;

        }
    }
}
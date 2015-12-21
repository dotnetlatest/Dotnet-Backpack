using System.Web.Mvc;

namespace Backpack.Site.Areas.Northwind
{
    public class NorthwindAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Northwind";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Northwind_default",
                "Northwind/{controller}/{action}/{id}",
                new { controller="Portal", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
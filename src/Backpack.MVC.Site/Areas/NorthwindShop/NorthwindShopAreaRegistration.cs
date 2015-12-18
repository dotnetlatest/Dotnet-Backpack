using System.Web.Mvc;

namespace Backpack.MVC.Site.Areas.NorthwindShop
{
    public class NorthwindShopAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "northwind";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Northwind_default",
                "Northwind/{controller}/{action}/{id}",
                new { controlller ="Portal", action = "Index", id = UrlParameter.Optional },
                new[] { "Backpack.MVC.Site.Areas.NorthwindShop" }
            );
        }
    }
}
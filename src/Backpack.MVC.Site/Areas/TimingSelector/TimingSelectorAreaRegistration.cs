using System.Web.Mvc;

namespace Backpack.MVC.Site.Areas.TimingSelector
{
    public class TimingSelectorAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "TimingSelector";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "TimingSelector_default",
                "TimingSelector/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
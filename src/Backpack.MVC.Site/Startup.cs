using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Backpack.MVC.Site.Startup))]
namespace Backpack.MVC.Site
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

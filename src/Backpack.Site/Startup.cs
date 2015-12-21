using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Backpack.Site.Startup))]
namespace Backpack.Site
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

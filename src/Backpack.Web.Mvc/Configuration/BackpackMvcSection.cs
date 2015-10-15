using System.Configuration;

namespace Backpack.Web.Mvc.Configuration //BackpackMvcSection
{
    // <summary>
    /// Main configuration section for pop.web
    /// 
    public class BackpackMvcSection : ConfigurationSection
    {
        /// <summary>
        /// Default instance of configuration for pop.web section
        /// </summary>
        public static readonly BackpackMvcSection Default = ConfigurationManager.GetSection("backpack.web.mvc") as BackpackMvcSection;

        [ConfigurationProperty("static")]
        public StaticConfiguration Static
        {
            get { return (StaticConfiguration)this["static"]; }
            set { this["static"] = value; }
        }

    }
}
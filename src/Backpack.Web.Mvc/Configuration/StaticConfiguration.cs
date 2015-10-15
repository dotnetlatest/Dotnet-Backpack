using System.Configuration;

namespace Backpack.Web.Mvc.Configuration
{
    public class StaticConfiguration : ConfigurationElement
    {
        [ConfigurationProperty("root")]
        public string Root
        {
            get { return (string)this["root"]; }
            set { this["root"] = value; }
        }

    }
}
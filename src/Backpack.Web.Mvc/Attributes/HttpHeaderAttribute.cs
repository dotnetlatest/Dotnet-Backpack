using System;
using System.Web.Mvc;

namespace Backpack.Web.Mvc.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class HttpHeaderAttribute : ActionFilterAttribute
    {
        public string Name { get; private set; }
        public string Value { get; private set; }

        public HttpHeaderAttribute(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.AddHeader(Name, Value);

            base.OnResultExecuting(filterContext);
        }
    }
}
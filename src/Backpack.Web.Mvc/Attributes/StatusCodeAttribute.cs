using System;
using System.Net;
using System.Web.Mvc;

namespace Backpack.Web.Mvc.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class StatusCodeAttribute : ActionFilterAttribute
    {
        public HttpStatusCode StatusCode { get; set; }

        public StatusCodeAttribute() { }
        public StatusCodeAttribute(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.StatusCode = (int)StatusCode;

            base.OnResultExecuting(filterContext);
        }
    }
}
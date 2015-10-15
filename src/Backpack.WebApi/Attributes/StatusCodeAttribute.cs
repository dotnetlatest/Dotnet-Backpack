using System;
using System.Net;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Backpack.WebApi.Attributes
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

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            actionContext.Response.StatusCode = StatusCode;
            base.OnActionExecuting(actionContext);
        }
    }
}
using System;
using System.Web.Mvc;
using Backpack.Core.Logging;

namespace Backpack.Web.Mvc.Attributes
{
    public class LogErrorsAttribute : FilterAttribute, IExceptionFilter
    {
        public string Message { get; set; }

        public LogErrorsAttribute()
            : this("Unhandled exception")
        {
        }

        public LogErrorsAttribute(string message)
        {
            Message = message;
        }

        private static readonly Log Log = Log.Get();

        public void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;

            Log.Error(Message, ex);
        }
    }
}
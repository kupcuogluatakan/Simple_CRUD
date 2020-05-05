using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ODMSCommon.Exception;
using ODMSCommon.Logging;

namespace ODMSTerminal.Infrastructure.MvcActionFilters
{
    public class ExceptionFilter:HandleErrorAttribute
    {
        private readonly Loggable _logger;

        public ExceptionFilter(Loggable logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }
            var exception = filterContext.Exception;
            string message = exception.Message;
            if (exception is ODMSException)
            {
                message = exception.Message;
            }

            _logger.LogException(exception);

            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            if (filterContext.HttpContext.Request.HttpMethod == "POST" &&
               filterContext.HttpContext.Request.Headers.Get("X-Requested-With") == "XMLHttpRequest")
            {
                var result = new JsonResult();
                result.Data = new { Result = "Error", Message = message };//JsonConvert.SerializeObject(new { Result = "Error", Message = message });
                filterContext.Result = result;
            }
            else if (filterContext.HttpContext.Request.HttpMethod == "GET" &&
                filterContext.HttpContext.Request.Headers.Get("X-Requested-With") == "XMLHttpRequest")
            {
                var result = new ContentResult();
                result.Content = String.Format("{{\"Result\":\"Error\",\"Message\":\"{0}\"}}", message);
                filterContext.Result = result;
            }
            else if (filterContext.HttpContext.Request.HttpMethod == "GET")
            {
                if (filterContext.HttpContext.User.Identity.IsAuthenticated == false)
                {
                    filterContext.Result = new RedirectResult("~/login");
                }
                else
                {
                    var result = new RedirectResult("~/error");
                    filterContext.Controller.TempData["MessageToShow"] = message;
                    filterContext.Controller.TempData["IsError"] = true;
                    filterContext.Result = result;
                }
            }
            else
            {
                var result = new RedirectResult("~/error");
                filterContext.Controller.TempData["MessageToShow"] = message;
                filterContext.Controller.TempData["IsError"] = true;
                filterContext.Result = result;
            }
            if (filterContext.HttpContext.User.Identity.IsAuthenticated == false)
                filterContext.HttpContext.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            filterContext.ExceptionHandled = true;
        }
    }
}
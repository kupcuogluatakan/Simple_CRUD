using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ODMSCommon.Logging;
using ODMSCommon.Resources;

namespace ODMS.Filters
{
    //http://www.prideparrot.com/blog/archive/2012/5/exception_handling_in_asp_net_mvc

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ErrorHandlingFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled /*|| !filterCo   ntext.HttpContext.IsCustomErrorEnabled*/)
            {
                return;
            }

            if (new HttpException(null, filterContext.Exception).GetHttpCode() != 500)
            {
                return;
            }

            if (!ExceptionType.IsInstanceOfType(filterContext.Exception))
            {
                return;
            }

            // if the request is AJAX return JSON else view.
            if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                filterContext.Result = new JsonResult
                    {
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                        Data = new
                            {
                                error = true,
                                message =MessageResource.Err_Generic_Unexpected// filterContext.Exception.Message
                            }
                    };
            }
            else
            {
                var controllerName = (string) filterContext.RouteData.Values["controller"];
                var actionName = (string) filterContext.RouteData.Values["action"];
                var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);

                filterContext.Result = new ViewResult
                    {
                        ViewName = View,
                        MasterName = Master,
                        ViewData = new ViewDataDictionary(model),
                        TempData = filterContext.Controller.TempData
                    };
            }

            var logger = new Loggable();
            logger.ErrorAsync(filterContext.Exception.ToString());

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            //filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;

            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}
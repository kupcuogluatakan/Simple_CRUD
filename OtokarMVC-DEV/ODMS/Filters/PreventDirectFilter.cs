using System;
using System.Web;
using System.Web.Mvc;

namespace ODMS.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class PreventDirectFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Uri refUri = HttpContext.Current.Request.UrlReferrer;

            //if (refUri == null)
            //{
            //    try
            //    {
            //        filterContext.Result =
            //            ((ODMS.Controllers.ControllerBase)filterContext.Controller).RedirectToAction("Index", "Home");

            //    }
            //    catch (Exception)
            //    {

            //        //filterContext.Result =
            //        //    ((ODMS.Controllers.NewsController)filterContext.Controller).RedirectToAction("Index", "Home");
            //    }
            //}
        }

    }
}
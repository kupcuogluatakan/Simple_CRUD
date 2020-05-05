using System.Web.Mvc;

namespace ODMS.Core
{
    public class ODMSViewEngine : RazorViewEngine
    {
        public ODMSViewEngine()
            : base()
        {
            ViewLocationFormats = new string[] { "~/Views/{1}/{0}.cshtml", "~/Views/Shared/{0}.cshtml"/*multi language*/ };

            MasterLocationFormats = new string[] {"~/Views/{1}/{0}.cshtml"};

            PartialViewLocationFormats = new string[] {"~/Views/Shared/{0}.cshtml", "~/Views/{1}/{0}.cshtml"};

            FileExtensions = new string[] { "cshtml" };
            
        }

        //protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        //{
        //    return base.CreateView(controllerContext, viewPath.Replace("%1", "Pages"), masterPath.Replace("%1", "Pages"));
        //}

        //protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        //{
        //    return base.CreatePartialView(controllerContext, partialPath.Replace("%1", "PartialPages"));
        //}

        //protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        //{
        //    return (base.FileExists(controllerContext, virtualPath.Replace("%1", "Pages")) ||
        //        base.FileExists(controllerContext, virtualPath.Replace("%1", "PartialPages")));
        //}
    }
}
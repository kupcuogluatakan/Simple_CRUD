using System.Web.Mvc;

namespace ODMS.Controllers
{
    public class ErrorPageController : Controller
    {
        //
        // GET: /ErrorPage/

        public ActionResult Index()
        {
            return PartialView();
        }

    }
}

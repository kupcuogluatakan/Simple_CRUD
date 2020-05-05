using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ODMS.Filters;
using ODMSCommon;
using ODMSModel.DownloadFileActionResult;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class DownloadFileActionResultController : ControllerBase
    {
        public ActionResult DownloadFileActionResult(Guid id)
        {
            var list = (Dictionary<Guid, DownloadFileViewModel>)Session[CommonValues.DownloadFileFormatCookieKey];

            Session.Remove(CommonValues.DownloadFileFormatCookieKey);
            TempData.Remove(CommonValues.DownloadFileIdCookieKey);

            foreach (var item in list.Where(item => item.Key == id))
            {
                return  File(item.Value.MStream, item.Value.ContentType, item.Value.FileName);
            }

            return View();
        }
    }
}

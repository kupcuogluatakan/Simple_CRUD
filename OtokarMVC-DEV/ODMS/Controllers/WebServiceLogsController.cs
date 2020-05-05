using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSModel.WebServiceLogs;
using System.Web.Mvc;

namespace ODMS.Controllers
{
 
    public class WebServiceLogsController:ControllerBase
    {
        [AuthorizationFilter(CommonValues.PermissionCodes.WebServiceLogs.InvoiceList)]
        public ActionResult InvoiceListLogs()
        {
            return View();
        }
        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.WebServiceLogs.InvoiceList)]
        public JsonResult ListInvoiceWebServiceLogs([DataSourceRequest] DataSourceRequest request,InvoiceListFilter f)
        {
            var requestListItem = new InvoiceListLogItem(request);
            var items = new WebServiceLogsBL().ListInvoiceLogs(requestListItem, f).Data;
            return Json(new
            {
                Data = items,
                Total = requestListItem.TotalCount
            });
        }
        [AuthorizationFilter(CommonValues.PermissionCodes.WebServiceLogs.InvoiceList)]
        public ActionResult GetInvoiceLogResponse(long id)
        {
            return PartialView("_InvoiceLogReponse", new WebServiceLogsBL().GetInvoiceLogResponse(id).Model);
        }

    }
}
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon.Security;
using ODMSModel.WorkorderListInvoices;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class WorkorderListInvoicesController : ControllerBase
    {
        //Index
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.WorkOrderListInvoices.WorkOrderListInvoicesIndex)]
        [HttpGet]
        public ActionResult WorkorderListInvoicesIndex(int id = 0)
        {
            WorkorderListInvoicesListModel model = new WorkorderListInvoicesListModel();
            if (id > 0)
            {
                model.WorkOrderId = id;
            }
            return View(model);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.WorkOrderListInvoices.WorkOrderListInvoicesIndex)]
        public JsonResult ListWorkorderListInvoices([DataSourceRequest] DataSourceRequest request,
            WorkorderListInvoicesListModel workorderListModel)
        {
            WorkorderListInvoicesBL workorderListInvoicesBL = new WorkorderListInvoicesBL();
            WorkorderListInvoicesListModel workorderListInvoicesModel = new WorkorderListInvoicesListModel(request);
            int totalCount = 0;

            workorderListInvoicesModel.WorkOrderId = workorderListModel.WorkOrderId;

            var rValue = workorderListInvoicesBL.GetWorkorderInvoicesList(UserManager.UserInfo,workorderListInvoicesModel, out totalCount).Data;

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });
        }
    }
}

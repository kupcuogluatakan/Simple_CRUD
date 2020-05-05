using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.WorkOrderInvoiceList;
using System;
using System.Web.Mvc;

namespace ODMS.Controllers
{
    public class WorkOrderInvoiceListCancelledController : ControllerBase
    {
        #region WorkOrderInvoiceListCancelled Index

        [AuthorizationFilter(CommonValues.PermissionCodes.WorkOrderInvoiceList.WorkOrderInvoiceListIndex)]
        [HttpGet]
        public ActionResult WorkOrderInvoiceListCancelledIndex(Int64? idWorkOrder)
        {
            var model = new WorkOrderInvoiceListListModel();
            model.IdWorkOrder = idWorkOrder;
            if (UserManager.UserInfo.GetUserDealerId() != 0)
            {
                  model.IdDealer = UserManager.UserInfo.GetUserDealerId();
            }
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.WorkOrderInvoiceList.WorkOrderInvoiceListIndex, CommonValues.PermissionCodes.WorkOrderInvoiceList.WorkOrderInvoiceListIndex)]
        public ActionResult ListWorkOrderInvoiceListCancelled([DataSourceRequest] DataSourceRequest request, WorkOrderInvoiceListListModel model)
        {
            var WorkOrderInvoiceListCancelledBo = new WorkOrderInvoiceListBL();

            var v = new WorkOrderInvoiceListListModel(request);

            v.IdWorkOrder = model.IdWorkOrder;
            v.StartDate = model.StartDate;
            v.EndDate = model.EndDate;
            v.Plate = model.Plate;
            v.CustomerName = model.CustomerName;
            v.TaxNo = model.TaxNo;
            v.IdDealer = model.IdDealer;
            v.InvoiceNo = model.InvoiceNo;
            v.TCNo = model.TCNo;



            var totalCnt = 0;
            var returnValue = WorkOrderInvoiceListCancelledBo.ListWorkOrderInvoiceListCancelled(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion WorkOrderInvoiceListCancelled Index

        
    }
}
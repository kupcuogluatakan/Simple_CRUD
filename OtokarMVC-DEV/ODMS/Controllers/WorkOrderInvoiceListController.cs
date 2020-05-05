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
    public class WorkOrderInvoiceListController : ControllerBase
    {
        #region WorkOrderInvoiceList Index

        [AuthorizationFilter(CommonValues.PermissionCodes.WorkOrderInvoiceList.WorkOrderInvoiceListIndex)]
        [HttpGet]
        public ActionResult WorkOrderInvoiceListIndex(Int64? idWorkOrder)
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
        public ActionResult ListWorkOrderInvoiceList([DataSourceRequest] DataSourceRequest request, WorkOrderInvoiceListListModel model)
        {
            var workOrderInvoiceListBo = new WorkOrderInvoiceListBL();

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
            var returnValue = workOrderInvoiceListBo.ListWorkOrderInvoiceList(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion WorkOrderInvoiceList Index

        #region WorkOrderInvoiceList Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.WorkOrderInvoiceList.WorkOrderInvoiceListIndex, CommonValues.PermissionCodes.WorkOrderInvoiceList.WorkOrderInvoiceListDelete)]
        public ActionResult DeleteWorkOrderInvoiceList(Int64 idWorkOrderInv, Int64 idWorkOrder)
        {
            WorkOrderInvoiceListViewModel viewModel = new WorkOrderInvoiceListViewModel
            {
                IdWorkOrderInv = idWorkOrderInv,
                IdWorkOrder = idWorkOrder
            };

            var workOrderInvoiceListBo = new WorkOrderInvoiceListBL();

            viewModel.CommandType = CommonValues.DMLType.Delete;

            workOrderInvoiceListBo.DMLWorkOrderInvoiceList(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                viewModel.ErrorMessage);
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSBusiness.Reports;
using ODMSCommon;
using ODMSModel.WorkOrderPicking;
using System.Linq;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    public class WorkOrderPickingController : ControllerBase
    {
        [AuthorizationFilter(CommonValues.PermissionCodes.WorkOrderPicking.WorkOrderPickingIndex)]
        public ActionResult WorkOrderPickingIndex(int? id)
        {
            var model = new WorkOrderPickingViewModel();

            if (id.HasValue)
            {
                model.PartSaleId = id.GetValueOrDefault();

                int totalCount = 0;
                var listModel = new WorkOrderPickingListModel();
                listModel.PartSaleId = id;
                var bl = new WorkOrderPickingBL();
                var list = bl.ListWorkOrderPicking(UserManager.UserInfo,listModel, out totalCount).Data;
                if (totalCount != 0)
                {
                    model.WorkOrderPickingId = list[0].WorkOrderPickingId;
                    model.IsReturn = list[0].IsReturn;
                    model.StatusId = list[0].StatusId;
                }
            }
            SetComboBox();
            var status = CommonBL.ListLookup(UserManager.UserInfo,"PICK_STATUS").Data;
            foreach (var item in status.Where(x => x.Value == "0" || x.Value == "1"))
            {
                item.Selected = true;
            }
            ViewBag.SLWOPicking = status;
            return View(model);

        }

        public ActionResult ListWorkOrderPicking([DataSourceRequest]DataSourceRequest request, WorkOrderPickingListModel hModel)
        {
            var bl = new WorkOrderPickingBL();
            var model = new WorkOrderPickingListModel(request);
            int totalCount = 0;

            model.PartSaleId = hModel.PartSaleId;
            model.StatusIds = hModel.StatusIds.AddSingleQuote();
            model.WorkOrderPlate = hModel.WorkOrderPlate;
            model.IsReturn = hModel.IsReturn;
            model.StartDate = hModel.StartDate;
            model.EndDate = hModel.EndDate;
            model.No = hModel.No;
            model.CustomerId = hModel.CustomerId;
            model.PartCode = hModel.PartCode;
            model.PartName = hModel.PartName;

            var rValue = bl.ListWorkOrderPicking(UserManager.UserInfo,model, out totalCount).Data;

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });
        }

        public ActionResult PrintSparePartPickingReport(int woPickingId)
        {
            //TFS NO : 28512 statü yeni emir ise başlandıya çekilecek
            int totalCount = 0;
            WorkOrderPickingBL wopBo = new WorkOrderPickingBL();
            WorkOrderPickingListModel wopListModel = new WorkOrderPickingListModel();
            wopListModel.WorkOrderPickingId = woPickingId;
            List<WorkOrderPickingListModel> list = wopBo.ListWorkOrderPicking(UserManager.UserInfo,wopListModel, out totalCount).Data;
            if (totalCount > 0)
            {
                int statusId = list.ElementAt(0).StatusId.GetValue<int>();
                if (statusId == (int)CommonValues.WorkOrderPickingStatus.NewRecord)
                {
                    var model = new WorkOrderPickingBL().ChangeWorkOrderStat(UserManager.UserInfo,woPickingId,
                                                                             (int)
                                                                             CommonValues.WorkOrderPickingStatus.Started).Model;
                    if (model.ErrorNo > 0)
                    {
                        SetMessage(model.ErrorMessage, CommonValues.MessageSeverity.Fail);
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
                    }
                }
            }
            MemoryStream ms = null;

            try
            {
                ms = new MemoryStream(ReportManager.GetReport(ReportType.SparePartPickingReport, woPickingId));
            }
            catch (Exception ex)
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, ex.Message);
            }

            return File(ms, "application/pdf", "SparePartPickingReport.pdf");
        }

        public ActionResult PrintReturnSparePartPickingReport(int woPickingId)
        {
            //TFS NO : 28512 statü yeni emir ise başlandıya çekilecek
            int totalCount = 0;
            WorkOrderPickingBL wopBo = new WorkOrderPickingBL();
            WorkOrderPickingListModel wopListModel = new WorkOrderPickingListModel();
            wopListModel.WorkOrderPickingId = woPickingId;
            List<WorkOrderPickingListModel> list = wopBo.ListWorkOrderPicking(UserManager.UserInfo,wopListModel, out totalCount).Data;
            if (totalCount > 0)
            {
                int statusId = list.ElementAt(0).StatusId.GetValue<int>();
                if (statusId == (int)CommonValues.WorkOrderPickingStatus.NewRecord)
                {
                    var model = new WorkOrderPickingBL().ChangeWorkOrderStat(UserManager.UserInfo,woPickingId,
                                                                             (int)
                                                                             CommonValues.WorkOrderPickingStatus.Started).Model;
                    if (model.ErrorNo > 0)
                    {
                        SetMessage(model.ErrorMessage, CommonValues.MessageSeverity.Fail);
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
                    }
                }
            }
            MemoryStream ms = null;

            try
            {
                ms = new MemoryStream(ReportManager.GetReport(ReportType.ReturnPartPickingReport, woPickingId));
            }
            catch (Exception ex)
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, ex.Message);
            }

            return File(ms, "application/pdf", "ReturnSparePartPickingReport.pdf");
        }

        private void SetComboBox()
        {

            ViewBag.SLWOIsReturn = CommonBL.ListReturnPicking().Data;
        }


    }
}

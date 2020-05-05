using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSBusiness.Reports;
using ODMSBusiness.WorkOrder;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.WorkOrderInvoice;
using Permission = ODMSCommon.CommonValues.PermissionCodes.WorkorderInvoice;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class WorkOrderInvoiceController : ControllerBase
    {

        #region Work Order Invoice Index
        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderInvoiceIndex)]
        public ActionResult WorkOrderInvoiceIndex(int id = 0)
        {
            var dto = new WorkOrderInvoicesBL().GetWorkOrderInvoiceAmount(id, 0, 0, null).Model;
            string errorMessage = string.Empty;

            var model = new WorkOrderInvoicesViewModel
            {
                InvoiceAmount = dto.InvoiceAmountWithVat,
                ErrorMessage = dto.ErrorDesc,
                ErrorNo = dto.ErrorNo,
                Currrency = dto.CurrencyCode,
                InvoiceRatio = dto.InvoiceRatio,
                HideElements = id <= 0,
                WorkOrderId = id
            };
            //TODO: vat ratio gerekli mi?
            if (model.ErrorNo > 0)
                CheckErrorForMessage(model, true);
            return View(model);
        }
        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderInvoiceIndex)]
        public ActionResult ListWorkOrderInvoice([DataSourceRequest]DataSourceRequest request, WorkOrderInvoicesListModel viewModel)
        {
            var bus = new WorkOrderInvoicesBL();
            var model = new WorkOrderInvoicesListModel(request) { WorkOrderId = viewModel.WorkOrderId };
            var totalCnt = 0;
            var returnValue = bus.ListWorkOrderInvoices(UserManager.UserInfo, model, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }
        #endregion

        #region Work Order Invoice Create
        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderInvoiceIndex, Permission.WorkOrderInvoiceCreate)]
        public ActionResult WorkOrderInvoiceCreate(long id = 0, int customerId = 0, string workOrderIds = null, int invoiceTypeId = 0)
        {
            //id: WorkOrderId
            string currency = string.Empty;
            FillComboBoxes();
            var bl = new WorkOrderInvoicesBL();
            var dto = bl.GetWorkOrderInvoiceAmount(id, customerId, 0, workOrderIds).Model;
            var suggestedData = bl.GetSuggestedInvoiceData(id, workOrderIds).Model;
            ViewBag.HideElements = id <= 0 && workOrderIds == null;
            var model = new WorkOrderInvoicesViewModel
            {
                WorkOrderId = id,
                WitholdId = dto.WitholdId.ToString(),
                CustomerId = customerId,
                IsFromProposalWitholding=dto.IsFromProposalWitholding,
                InvoiceAmount = dto.InvoiceAmount,
                InvoiceSerialNo = suggestedData.Item1,
                InvoiceNo = suggestedData.Item2,
                SpecialInvoiceVatAmount = suggestedData.Item3,
                Currrency = currency,
                InvoiceRatio = 100,
                ErrorMessage = dto.ErrorDesc,
                ErrorNo = dto.ErrorNo,
                WorkOrderInvoiceId = 0,
                InvoiceDate = DateTime.Now,
                WorkOrderIds = workOrderIds,
                InvoiceTypeId = invoiceTypeId
            };
            if (model.ErrorNo > 0)
                CheckErrorForMessage(model, true);

            return View(model);
        }
        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderInvoiceIndex, Permission.WorkOrderInvoiceCreate)]
        [ValidateAntiForgeryToken]
        public ActionResult WorkOrderInvoiceCreate(WorkOrderInvoicesViewModel model)
        {
            ViewBag.HideElements = false;// model.WorkOrderId <= 0;
            FillComboBoxes();
            string currency = string.Empty;
            var bl = new WorkOrderInvoicesBL();

            var dto = bl.GetWorkOrderInvoiceAmount(model.WorkOrderId, 0, 0, model.WorkOrderIds).Model;

            model.InvoiceRatio = 100;
            model.VatRatio = dto.VatRatio;
            model.InvoiceAmount = dto.InvoiceAmount;
            model.InvoiceVatAmount = (dto.InvoiceAmount * model.VatRatio) / 100;
            if (!HttpContext.Request.UrlReferrer.ToString().ToLower().Contains("workorderbatchinvoice"))
            {
                if (!ModelState.IsValid)
                    return View(model);
            }
            model.CommandType = model.WorkOrderInvoiceId == 0 ? CommonValues.DMLType.Insert : CommonValues.DMLType.Update;
            bl.DMLWorkOrderInvoices(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);
            ModelState.Clear();
            return RedirectToAction("WorkOrderInvoiceCreate",
                new
                {
                    id = model.WorkOrderId,
                    customerId = model.CustomerId,
                    invoiceTypeId = model.InvoiceTypeId,
                    workOrderIds = model.WorkOrderIds,
                });
        }
        #region Invoice Edit

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderInvoiceIndex, Permission.WorkOrderInvoiceCreate)]
        public ActionResult EditWorkOrderInvoice(long id, long workOrderId = 0) /*invoiceId*/
        {
            FillComboBoxes();
            var model = new WorkOrderInvoicesBL().GetWorkOrderInvoices(UserManager.UserInfo, id).Model;
            model.WorkOrderId = workOrderId;
            ViewBag.HideElements = model == null;
            return View("WorkOrderInvoiceCreate", model);
        }


        #endregion


        #endregion

        #region Work Order Invoice  Update
        //no update here

        //[HttpGet]
        //[AuthorizationFilter(Permission.WorkOrderInvoiceIndex, Permission.WorkOrderInvoiceUpdate)]
        //public ActionResult WorkOrderInvoiceUpdate(int? id)
        //{
        //    FillComboBoxes();
        //    if (!(id.HasValue && id > 0))
        //    {
        //        SetMessage(MessageResource.Error_DB_NoRecordFound, CommonValues.MessageSeverity.Fail);
        //        return View();
        //    }
        //    return View(new WorkOrderInvoicesBL().GetWorkOrderInvoices(id.GetValueOrDefault()));
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[AuthorizationFilter(Permission.WorkOrderInvoiceIndex, Permission.WorkOrderInvoiceUpdate)]
        //public ActionResult WorkOrderInvoiceUpdate(WorkOrderInvoicesViewModel model)
        //{
        //    FillComboBoxes();
        //    if (ModelState.IsValid == false)
        //        return View(model);
        //    var bus = new WorkOrderInvoicesBL();
        //    model.CommandType = CommonValues.DMLType.Update;
        //    bus.DMLWorkOrderInvoices(model);
        //    CheckErrorForMessage(model, true);
        //    ModelState.Clear();
        //    return View(model);
        //}
        #endregion

        #region Work Order Invoice Delete
        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderInvoiceIndex, Permission.WorkOrderInvoiceDelete)]
        public ActionResult WorkOrderInvoiceDelete(int? id)
        {
            if (!(id.HasValue && id > 0))
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.Error_DB_NoRecordFound);
            }
            var bus = new WorkOrderInvoicesBL();
            var model = new WorkOrderInvoicesViewModel
            {
                WorkOrderInvoiceId = id ?? 0,
                CommandType = CommonValues.DMLType.Delete
            };
            bus.DMLWorkOrderInvoices(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);
            ModelState.Clear();
            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                model.ErrorMessage);
        }
        #endregion

        #region Work Order Invoice Details
        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderInvoiceIndex, Permission.WorkOrderInvoiceDetails)]
        public ActionResult WorkOrderInvoiceDetails(long? id)
        {
            if (!(id.HasValue && id > 0))
            {
                SetMessage(MessageResource.Error_DB_NoRecordFound, CommonValues.MessageSeverity.Fail);
                return View();
            }
            return View(new WorkOrderInvoicesBL().GetWorkOrderInvoices(UserManager.UserInfo, id.GetValueOrDefault()).Model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderInvoiceIndex, Permission.WorkOrderInvoiceUpdate)]
        public ActionResult SetBillingStatus(long workOrderDetailId, bool invoiceCancel)
        {
            var modelBase = new WorkOrderInvoicesBL().SetBillingStatus(UserManager.UserInfo, workOrderDetailId, invoiceCancel).Model;

            if (modelBase.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                modelBase.ErrorMessage);
        }


        #endregion

        #region List WorkOrder Indicators

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderInvoiceIndex)]
        public ActionResult ListWorkOrderIndicators(long id = 0, string workOrderDetailIds = null)
        {
            return PartialView("_ListWorkOrderIndicators", new WorkOrderInvoicesBL().ListWorkOrderInvoiceItems(UserManager.UserInfo, id, workOrderDetailIds).Data);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderInvoiceIndex)]
        public ActionResult ListWorkOrderInvoices(long id = 0, string workOrderDetailIds = null)
        {
            ViewBag.WorkOrderId = id;
            return PartialView("_ListWorkOrderInvoices", new WorkOrderInvoicesBL().ListInvoices(UserManager.UserInfo, id, workOrderDetailIds).Data);
        }

        #endregion
        #region Print Invoice
        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderInvoiceIndex)]
        public ActionResult PrintInvoice(string invoiceType, string worOrderDetailIds, long workOrderId, long invoiceId)
        {

            string invoType = null;
            worOrderDetailIds = String.IsNullOrEmpty(worOrderDetailIds) ? string.Empty : worOrderDetailIds.Substring(0, worOrderDetailIds.Length - 1);
            var model = new WorkOrderInvoicesBL().UpdateInvoiceIds(UserManager.UserInfo, workOrderId, invoiceId, worOrderDetailIds, CommonValues.DMLType.Update, out invoType).Model;
            CheckErrorForMessage(model, true);
            if (model.ErrorNo == 0)
            {

                var data = new WorkOrderInvoicesBL().GetWorkOrderInvoices(UserManager.UserInfo, invoiceId).Model;
                if (data == null)
                {
                    SetMessage(MessageResource.ErrorVehicleLeaveInvoice, CommonValues.MessageSeverity.Fail);
                    return WorkOrderInvoiceIndex(invoiceId.GetValue<int>());
                }

                var invType = (InvoiceType)0;
                if (data.InvoiceTypeId == 1) invType = InvoiceType.Dokumlu;
                if (data.InvoiceTypeId == 2) invType = InvoiceType.UcKirilimli;
                if (data.InvoiceTypeId == 3) invType = InvoiceType.Ozel;

                var stream = ReportManager.GetReport(ReportType.VehicleInvoiceReport, invType, invoiceId,
                    data.HasWitholding, invoiceType == "1" ? InvoicePrintType.Printed : invoiceType == "2" ? InvoicePrintType.Transcript : InvoicePrintType.Proforma);
                if (stream != null && stream.Length > 0)
                {
                    var filename = invoiceType == "1"
                                       ? string.Format(MessageResource.WorkOrderInvoice_Report_Invoice, workOrderId)
                                       : invoiceType == "2"
                                             ? string.Format(MessageResource.WorkOrderInvoice_Report_InvoiceTranscript,
                                                             workOrderId)
                                             : string.Format(MessageResource.WorkOrderInvoice_Report_InvoiceProforma,
                                                             workOrderId);
                    return File(stream, "application.pdf", filename);
                }
                SetMessage(MessageResource.ErrorVehicleLeaveInvoice, CommonValues.MessageSeverity.Fail);
                return WorkOrderInvoiceIndex(invoiceId.GetValue<int>());
            }

            return RedirectToAction("WorkOrderInvoiceCreate", new { id = workOrderId });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderInvoiceIndex, Permission.WorkOrderInvoiceDelete)]
        public ActionResult DeleteInvoice(long _workOrderId, long _invoiceId, int customerId)
        {
            string invType = null;
            var model = new WorkOrderInvoicesBL().UpdateInvoiceIds(UserManager.UserInfo, _workOrderId, _invoiceId, string.Empty, CommonValues.DMLType.Delete, out invType).Model;
            CheckErrorForMessage(model, true);
            return RedirectToAction("WorkOrderInvoiceCreate", new { id = _workOrderId, customerId = customerId });
        }

        #endregion


        #region Private Methods

        #region ComboBox List Json Methods
        [HttpPost]
        public JsonResult ListCustomerAddresses(int CustomerId = 0)
        {
            return
                Json(CustomerId == 0
                    ? new List<SelectListItem>()
                    : new WorkOrderInvoicesBL().ListCutomerAddresses(UserManager.UserInfo, CustomerId).Data);
        }
        [HttpPost]
        public JsonResult ListWitholdings()
        {
            return Json(new WorkOrderInvoicesBL().GetWitholdingListForDealer(ODMSCommon.Security.UserManager.UserInfo.GetUserDealerId()).Data);
        }

        [HttpPost]
        public JsonResult GetWorkOrderInvoiceDTO(int workOrderId, int customerId, long? invoiceId, string workOrderIds)
        {
            return Json(new WorkOrderInvoicesBL().GetWorkOrderInvoiceAmount(workOrderId, customerId, invoiceId.GetValueOrDefault(), workOrderIds).Model);
        }

        #endregion


        private void FillComboBoxes()
        {
            var bus = new WorkOrderInvoicesBL();
            ViewBag.DueDurationList = bus.ListDueDuration().Data;
            ViewBag.InvoiceTypeList = CommonBL.ListLookup(UserManager.UserInfo, WorkOrderInvoicesViewModel.InvoiceTypeLookKey).Data;

        }

        #endregion
    }
}

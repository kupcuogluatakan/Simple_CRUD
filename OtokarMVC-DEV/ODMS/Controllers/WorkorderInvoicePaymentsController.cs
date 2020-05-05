using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.PaymentType;
using ODMSModel.WorkorderInvoicePayments;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class WorkorderInvoicePaymentsController : ControllerBase
    {
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.WorkorderInvoicePayments.WorkorderInvoicePaymentsIndex)]
        public ActionResult WorkorderInvoicePaymentsIndex(int workorderInvoiceId, int workorderId)
        {
            var bo = new WorkorderInvoicePaymentsBL();
            var model = bo.GetWorkorderInvoicePaymentsIndexModel(UserManager.UserInfo, workorderInvoiceId, workorderId).Model;
            return View(model);
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.WorkorderInvoicePayments.WorkorderInvoicePaymentsIndex, CommonValues.PermissionCodes.WorkorderInvoicePayments.WorkorderInvoicePaymentsCreate)]
        public ActionResult WorkorderInvoicePaymentsCreate(int workorderInvoiceId, int workorderId)
        {
            var bo = new WorkorderInvoicePaymentsBL();
            ViewBag.PaymentTypeList = bo.GetPaymentTypeList(UserManager.UserInfo).Data;
            ViewBag.BankList = bo.GetBankList().Data;

            var model = new WorkorderInvoicePaymentsDetailModel { WorkorderInvoiceId = workorderInvoiceId, WorkorderId = workorderId };
            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.WorkorderInvoicePayments.WorkorderInvoicePaymentsIndex, CommonValues.PermissionCodes.WorkorderInvoicePayments.WorkorderInvoicePaymentsCreate)]
        public ActionResult WorkorderInvoicePaymentsCreate(WorkorderInvoicePaymentsDetailModel model)
        {
            var bo = new WorkorderInvoicePaymentsBL();
            ViewBag.PaymentTypeList = bo.GetPaymentTypeList(UserManager.UserInfo).Data;
            ViewBag.BankList = bo.GetBankList().Data;
            if (ModelState.IsValid)
            {
                model.CommandType = CommonValues.DMLType.Insert;
                model.PaymentDate = DateTime.Today;
                bo.DMLWorkorderInvoicePayments(UserManager.UserInfo, model);
                CheckErrorForMessage(model, true);
                ModelState.Clear();
                model = new WorkorderInvoicePaymentsDetailModel {WorkorderInvoiceId = model.WorkorderInvoiceId, WorkorderId = model.WorkorderId};
                return View(model);
            }
            return View(model);
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.WorkorderInvoicePayments.WorkorderInvoicePaymentsIndex, CommonValues.PermissionCodes.WorkorderInvoicePayments.WorkorderInvoicePaymentsUpdate)]
        public ActionResult WorkorderInvoicePaymentsUpdate(int id)
        {
            var bo = new WorkorderInvoicePaymentsBL();
            ViewBag.PaymentTypeList = bo.GetPaymentTypeList(UserManager.UserInfo).Data;
            ViewBag.BankList = bo.GetBankList().Data;

            var referenceModel = new WorkorderInvoicePaymentsDetailModel();
            if (id > 0)
            {
                referenceModel.Id = id;
                referenceModel = bo.GetWorkorderInvoicePayments(UserManager.UserInfo, referenceModel).Model;

                var paymentType =
                    (ViewBag.PaymentTypeList as List<PaymentTypeListModel>).Find(
                        x => x.Id == referenceModel.PaymentTypeId);

                referenceModel.BankRequired = paymentType.BankRequired;
                referenceModel.InstalmentNumberRequired = paymentType.InstalmentRequired;
                referenceModel.TransmitNumberRequired = paymentType.TransmitNoRequired;
            }
            return View(referenceModel);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.WorkorderInvoicePayments.WorkorderInvoicePaymentsIndex, CommonValues.PermissionCodes.WorkorderInvoicePayments.WorkorderInvoicePaymentsUpdate)]
        public ActionResult WorkorderInvoicePaymentsUpdate(WorkorderInvoicePaymentsDetailModel model)
        {
            var bo = new WorkorderInvoicePaymentsBL();
            ViewBag.PaymentTypeList = bo.GetPaymentTypeList(UserManager.UserInfo).Data;
            ViewBag.BankList = bo.GetBankList().Data;

            if (ModelState.IsValid)
            {
                model.CommandType = CommonValues.DMLType.Update;
                bo.DMLWorkorderInvoicePayments(UserManager.UserInfo, model);
                CheckErrorForMessage(model, true);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.WorkorderInvoicePayments.WorkorderInvoicePaymentsIndex, CommonValues.PermissionCodes.WorkorderInvoicePayments.WorkorderInvoicePaymentsDelete)]
        public ActionResult WorkorderInvoicePaymentsDelete(int id)
        {
            ViewBag.HideElements = false;

            var bo = new WorkorderInvoicePaymentsBL();
            var model = new WorkorderInvoicePaymentsDetailModel { Id = id, CommandType = CommonValues.DMLType.Delete };
            bo.DMLWorkorderInvoicePayments(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);
            ModelState.Clear();

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.WorkorderInvoicePayments.WorkorderInvoicePaymentsIndex, CommonValues.PermissionCodes.WorkorderInvoicePayments.WorkorderInvoicePaymentsDetails)]
        public ActionResult WorkorderInvoicePaymentsDetails(int id)
        {
            var referenceModel = new WorkorderInvoicePaymentsDetailModel { Id = id };
            var bo = new WorkorderInvoicePaymentsBL();

            var model = bo.GetWorkorderInvoicePayments(UserManager.UserInfo, referenceModel).Model;

            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.WorkorderInvoicePayments.WorkorderInvoicePaymentsIndex, CommonValues.PermissionCodes.WorkorderInvoicePayments.WorkorderInvoicePaymentsDetails)]
        public ActionResult ListWorkorderInvoicePayments([DataSourceRequest]DataSourceRequest request, WorkorderInvoicePaymentsListModel model)
        {
            var bo = new WorkorderInvoicePaymentsBL();
            var referenceModel = new WorkorderInvoicePaymentsListModel(request)
                {
                    BankId = model.BankId,
                    PaymentTypeId = model.PaymentTypeId,
                    PayAmount = model.PayAmount,
                    InstalmentNumber = model.InstalmentNumber,
                    WorkorderInvoiceId = model.WorkorderInvoiceId,
                    WorkorderId = model.WorkorderId
                };
            int totalCnt;
            var returnValue = bo.ListWorkorderInvoicePayments(UserManager.UserInfo, referenceModel, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

    }
}

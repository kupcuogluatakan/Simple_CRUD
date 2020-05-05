using System;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMSBusiness;
using ODMSModel.WorkOrderBatchInvoice;
using ODMSModel.WorkOrderInvoice;
using ODMS.Filters;
using ODMSCommon;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    public class WorkOrderBatchInvoiceController : ControllerBase
    {
        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.WorkOrderBatchInvoice.WorkOrderBatchInvoiceIndex)]
        public ViewResult Index()
        {
            ViewBag.DealerId = ODMSCommon.Security.UserManager.UserInfo.GetUserDealerId();

            return View();
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.WorkOrderBatchInvoice.WorkOrderBatchInvoiceIndex)]
        public JsonResult List([DataSourceRequest] DataSourceRequest request, WorkOrderBatchInvoiceList input)
        {
            var model = new WorkOrderBatchInvoiceList(request)
            {
                CustomerId = input.CustomerId,
                DealerId = ODMSCommon.Security.UserManager.UserInfo.GetUserDealerId()
            };

            var list = new WorkOrderBatchIvoiceBL().List(UserManager.UserInfo, model).Data;

            return Json(new DataSourceResult() { Data = list, Total = model.TotalCount });
        }

        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.WorkOrderBatchInvoice.WorkOrderBatchInvoiceIndex)]
        public ViewResult Invoices(string details, int customerId, long invoiceId = 0)
        {
            ViewBag.Details = details;
            ViewBag.CustomerId = customerId;
            string currency = string.Empty;
            FillComboBoxes();
            var model = new WorkOrderInvoicesViewModel
            {
                CustomerId = customerId,
                Currrency = currency,
                InvoiceRatio = 100,
                WorkOrderInvoiceId = invoiceId,
                InvoiceDate = DateTime.Now,
                WorkOrderIds = details,

            };
            if (model.ErrorNo > 0)
                CheckErrorForMessage(model, true);

            return View("_Invoices", model);
        }

        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.WorkOrderBatchInvoice.WorkOrderBatchInvoiceIndex)]
        public ActionResult ListWorkOrderDetails(string workOrderDetailIds)
        {
            var list = new WorkOrderInvoicesBL().ListWorkOrderInvoiceItems(UserManager.UserInfo, 0, workOrderDetailIds).Data;
            return View("_WorkOrderDetails", list);
        }

        private void FillComboBoxes()
        {
            var bus = new WorkOrderInvoicesBL();
            ViewBag.DueDurationList = bus.ListDueDuration().Data;
            ViewBag.InvoiceTypeList = CommonBL.ListLookup(UserManager.UserInfo, WorkOrderInvoicesViewModel.InvoiceTypeLookKey).Data;
        }

        [HttpPost]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.WorkOrderBatchInvoice.WorkOrderBatchInvoiceIndex)]
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
            model.InvoiceVatAmount = dto.InvoiceAmount * model.VatRatio;
            if (!HttpContext.Request.UrlReferrer.ToString().ToLower().Contains("workorderbatchinvoice"))
            {
                if (!ModelState.IsValid)
                    return View("_Invoices", model);
            }
            model.CommandType = model.WorkOrderInvoiceId == 0 ? CommonValues.DMLType.Insert : CommonValues.DMLType.Update;
            bl.DMLWorkOrderInvoices(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);
            ModelState.Clear();
            return RedirectToAction("Invoices",
                new
                {
                    customerId = model.CustomerId,
                    details = model.WorkOrderIds,
                    invoiceId = model.WorkOrderInvoiceId
                });
        }

        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.WorkOrderBatchInvoice.WorkOrderBatchInvoiceIndex)]
        public ActionResult ListWorkOrderInvoices(long id, string workOrderDetailIds)
        {
            ViewBag.WorkOrderId = 0;
            return View("_WorkOrderInvoices", new WorkOrderInvoicesBL().ListInvoices(UserManager.UserInfo, id, workOrderDetailIds).Data.Where(c => c.WorkOrderInvoiceId == id).ToList());
        }
    }
}

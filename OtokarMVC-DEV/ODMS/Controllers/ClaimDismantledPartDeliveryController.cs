using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.ClaimDismantledPartDelivery;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class ClaimDismantledPartDeliveryController : ControllerBase
    {
        private readonly ClaimDismantledPartDeliveryBL _service = new ClaimDismantledPartDeliveryBL();

        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimDismantledPartDelivery.ClaimDismantledPartDeliveryIndex)]
        public ActionResult ClaimDismantledPartDeliveryIndex()
        {
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimDismantledPartDelivery.ClaimDismantledPartDeliveryIndex)]
        public ActionResult Read([DataSourceRequest]DataSourceRequest request, ClaimDismantledPartDeliveryListModel model)
        {
            int totalCnt;

            var referenceModel = new ClaimDismantledPartDeliveryListModel(request);

            var list = _service.List(UserManager.UserInfo, referenceModel, out totalCnt).Data;

            return Json(new
            {
                Data = list,
                Total = totalCnt
            });
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimDismantledPartDelivery.ClaimDismantledPartDeliveryCreate)]
        public ActionResult ClaimDismantledPartDeliveryCreate()
        {
            return PartialView();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimDismantledPartDelivery.ClaimDismantledPartDeliveryCreate)]
        [HttpPost]
        public ActionResult ClaimDismantledPartDeliveryCreate(ClaimDismantledPartDeliveryViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.CommandType = CommonValues.DMLType.Insert;
                _service.Insert(UserManager.UserInfo, model);

                if (model.ErrorNo == 0)
                {
                    foreach (var item in ModelState.Keys.Where(item => item != "ClaimWayBillId"))
                        ModelState[item].Value = new ValueProviderResult(string.Empty, string.Empty, CultureInfo.CurrentCulture);
                }

                CheckErrorForMessage(model, true);
            }

            return PartialView(model);
        }


        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimDismantledPartDelivery.ClaimDismantledPartDeliveryUpdate)]
        public ActionResult ClaimDismantledPartDeliveryUpdate(int id)
        {
            return PartialView(_service.Get(UserManager.UserInfo, new ClaimDismantledPartDeliveryViewModel() { ClaimWayBillId = id }).Model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimDismantledPartDelivery.ClaimDismantledPartDeliveryUpdate)]
        [HttpPost]
        public ActionResult ClaimDismantledPartDeliveryUpdate(ClaimDismantledPartDeliveryViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.CommandType = CommonValues.DMLType.Update;
                _service.Update(UserManager.UserInfo, model);

                CheckErrorForMessage(model, true);
            }

            return PartialView(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimDismantledPartDelivery.ClaimDismantledPartDeliveryDetails)]
        public ActionResult ClaimDismantledPartDeliveryDetails(int id)
        {
            return PartialView(_service.Get(UserManager.UserInfo, new ClaimDismantledPartDeliveryViewModel() { ClaimWayBillId = id }).Model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimDismantledPartDelivery.ClaimDismantledPartDeliveryDelete)]
        [HttpPost]
        public ActionResult ClaimDismantledPartDeliveryDelete(int id)
        {
            var model = new ClaimDismantledPartDeliveryViewModel
            {
                ClaimWayBillId = id,
                CommandType = CommonValues.DMLType.Delete
            };

            _service.Delete(UserManager.UserInfo, model);

            ModelState.Clear();

            return model.ErrorNo == 0 ? GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success) :
                GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        public ActionResult PrintWayBill(int claimWayBillId)
        {
            MemoryStream ms = null;

            try
            {
                ms = new MemoryStream(_service.PrintWayBill(claimWayBillId));
            }
            catch
            {
                SetMessage(MessageResource.ErrorPrintWaybill, CommonValues.MessageSeverity.Fail);
                return ClaimDismantledPartDeliveryIndex();
            }

            return File(ms, "application/pdf", string.Format(MessageResource.ClaimDismantledPartDelivery_ReportName_Invoice, claimWayBillId));
        }

        public ActionResult PrintWayBillCopy(int claimWayBillId)
        {
            MemoryStream ms = null;

            try
            {
                ms = new MemoryStream(_service.PrintWayBillCopy(claimWayBillId));
            }
            catch
            {
                SetMessage(MessageResource.ErrorPrintWaybill, CommonValues.MessageSeverity.Fail);
                return ClaimDismantledPartDeliveryIndex();
            }

            return File(ms, "application/pdf", string.Format(MessageResource.ClaimDismantledPartDelivery_ReportName_InvoiceCopy, claimWayBillId));
        }

        public ActionResult PrintWayBillProforma(int claimWayBillId)
        {
            MemoryStream ms = null;

            try
            {
                ms = new MemoryStream(_service.PrintWayBillProforma(claimWayBillId));
            }
            catch
            {
                SetMessage(MessageResource.ErrorPrintWaybill, CommonValues.MessageSeverity.Fail);
                return ClaimDismantledPartDeliveryIndex();
            }

            return File(ms, "application/pdf", string.Format(MessageResource.ClaimDismantledPartDelivery_ReportName_InvoiceProforma, claimWayBillId));
        }
    }
}

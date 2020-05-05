using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.AppointmentDetails;
using Permission = ODMSCommon.CommonValues.PermissionCodes.AppointmentDetail;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class AppointmentDetailsController : ControllerBase
    {

        #region Appointment Details Index

        [HttpGet]
        [AuthorizationFilter(Permission.AppointmentDetailIndex)]
        public ActionResult AppointmentDetailsIndex(int id = 0)
        {
            var model = new AppointmentDetailsViewModel { HideElements = id <= 0, AppointmentId = id };
            return View(model);
        }
        [HttpPost]
        [AuthorizationFilter(Permission.AppointmentDetailIndex)]
        public ActionResult ListAppointmentDetails([DataSourceRequest]DataSourceRequest request, AppointmentDetailsListModel viewModel)
        {
            var bus = new AppointmentDetailsBL();
            var model = new AppointmentDetailsListModel(request) { AppointmentId = viewModel.AppointmentId };
            var totalCnt = 0;
            var returnValue = bus.ListAppointmentDetails(UserManager.UserInfo,model, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AppointmentDetailsGetTotalPrice(int appId)
        {
            var bl = new AppointmentDetailsBL();
            double totalPrice = bl.GetTotalPriceForAppointment(appId);
            return Json(new
            {
                Total = totalPrice
            });
        }

        #endregion

        #region Appointment Details Create
        [HttpGet]
        [AuthorizationFilter(Permission.AppointmentDetailIndex, Permission.AppointmentDetailCreate)]
        public ActionResult AppointmentDetailsCreate(int id = 0)
        {
            //TODO: vehicleid ile alakalı olarak burada periyodik bakım bulunamazsa
            //TODO: ekranda hata mesajı ile araç için periyordik bakım bulunamadı hatası görüntülenmeli
            int vehicleId = 0;
            ClearTempData();
            ViewBag.HideElements = id <= 0;
            var bl = new AppointmentDetailsBL();

            ViewBag.SLAppIndcType = bl.GetAppointmentIndicType(UserManager.UserInfo, id, out vehicleId).Data;

            ViewBag.SLCampaignCode = bl.GetCampaignCodeByVehicleId(UserManager.UserInfo, vehicleId).Data;
            ViewBag.SLMaint = bl.GetMaintCoupon(UserManager.UserInfo).Data;
            ViewBag.SLMaintByVehicle = bl.GetMaintByVehicle(UserManager.UserInfo, vehicleId).Data;
            var model = new AppointmentDetailsViewModel
            {
                AppointmentId = id,
                VehicleId = vehicleId
            };

            FillComboBoxes();
            return PartialView(model);
        }
        [HttpPost]
        [AuthorizationFilter(Permission.AppointmentDetailIndex)]
        public JsonResult ListCategories(int id, string typeCode)
        {
            return Json(AppointmentIndicatorCategoryBL.ListAppointmentIndicatorCategories(UserManager.UserInfo, id, typeCode).Data);
        }
        [HttpPost]
        [AuthorizationFilter(Permission.AppointmentDetailIndex)]
        public JsonResult ListSubCategories(int id, string typeCode)
        {
            return Json(AppointmentIndicatorSubCategoryBL.ListAppointmentIndicatorSubCategories(UserManager.UserInfo, id, typeCode).Data);
        }

        [HttpPost]
        public JsonResult ListMainCategories(string id)
        {
            return Json(AppointmentIndicatorMainCategoryBL.ListAppointmentIndicatorMainCategories(UserManager.UserInfo, id).Data);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.AppointmentDetailIndex, Permission.AppointmentDetailCreate)]
        [ValidateAntiForgeryToken]
        public ActionResult AppointmentDetailsCreate(AppointmentDetailsViewModel model)
        {
            int vehicleId = 0;
            ViewBag.HideElements = model.AppointmentId <= 0;
            var bl = new AppointmentDetailsBL();

            ViewBag.SLAppIndcType = bl.GetAppointmentIndicType(UserManager.UserInfo, model.AppointmentId, out vehicleId).Data;
            ViewBag.SLCampaignCode = bl.GetCampaignCodeByVehicleId(UserManager.UserInfo, vehicleId).Data;
            ViewBag.SLMaint = bl.GetMaintCoupon(UserManager.UserInfo).Data;
            ViewBag.SLMaintByVehicle = bl.GetMaintByVehicle(UserManager.UserInfo, vehicleId).Data;

            FillComboBoxes();
            //if (ModelState.IsValid == false)
            //    return View(model);
            var bus = new AppointmentDetailsBL();
            model.CommandType = CommonValues.DMLType.Insert;
            bus.DMLAppointmentDetails(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);
            ModelState.Clear();
            return View(new AppointmentDetailsViewModel() { AppointmentId = model.AppointmentId });
        }
       
        #endregion

        #region Appointment Details Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(Permission.AppointmentDetailIndex, Permission.AppointmentDetailDelete)]
        public ActionResult AppointmentDetailsDelete(int? id)
        {
            if (!(id.HasValue && id > 0))
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.Error_DB_NoRecordFound);
            }
            var bus = new AppointmentDetailsBL();
            var model = new AppointmentDetailsViewModel
                {
                    AppointmentIndicatorId = id ?? 0,
                    CommandType = CommonValues.DMLType.Delete
                };
            bus.DMLAppointmentDetails(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);
            ModelState.Clear();
            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                model.ErrorMessage);
        }
        #endregion

        #region Appointment Details Details
        [HttpGet]
        [AuthorizationFilter(Permission.AppointmentDetailIndex, Permission.AppointmentDetailDetails)]
        public ActionResult AppointmentDetailsDetails(int? id)
        {
            ClearTempData();
            if (!(id.HasValue && id > 0))
            {
                SetMessage(MessageResource.Error_DB_NoRecordFound, CommonValues.MessageSeverity.Fail);
                return View();
            }
            return View(new AppointmentDetailsBL().GetAppointmentDetails(UserManager.UserInfo, id.GetValueOrDefault()).Model);
        }
        #endregion

        #region Private Methods

        private void FillComboBoxes()
        {
            ViewBag.MainCategoryList = AppointmentIndicatorMainCategoryBL.ListAppointmentIndicatorMainCategories(UserManager.UserInfo, true).Data;
            ViewBag.FailureCodeList = new WorkOrderCardBL().ListFailureCodes(UserManager.UserInfo).Data;
        }

        #endregion
    }
}

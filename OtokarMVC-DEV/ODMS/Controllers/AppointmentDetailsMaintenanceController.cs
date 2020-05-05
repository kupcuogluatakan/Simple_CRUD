using System.Collections.Generic;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon.Resources;
using ODMSModel.AppointmentDetailsMaintenance;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class AppointmentDetailsMaintenanceController : ControllerBase
    {
        //Index
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.AppointmentIndicatorMaint.AppointmentIndicatorMaintIndex)]
        [HttpGet]
        public ActionResult AppointmentDetailsMaintenanceIndex(int id=0)
        {
            AppointmentDetailsMaintenanceViewModel model = new AppointmentDetailsMaintenanceViewModel();
            //model.AppIndicId = 1007;
            if (id>0)
            {
                model.AppIndicId = id;
                var bl = new AppointmentDetailsMaintenanceBL();
                bl.GetMaintIdByAppIndicId(UserManager.UserInfo, model);
            }
            SetComboBox(model.AppIndicId);

            return PartialView(model);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.AppointmentIndicatorMaint.AppointmentIndicatorMaintIndex)]
        public JsonResult ListAppointmentDetailsMaintenance([DataSourceRequest] DataSourceRequest request,
            AppointmentDetailsMaintenanceListModel modelAppDetMaint)
        {
            AppointmentDetailsMaintenanceBL appDetailMaintBL = new AppointmentDetailsMaintenanceBL();
            AppointmentDetailsMaintenanceListModel model = new AppointmentDetailsMaintenanceListModel(request);
            int totalCount = 0;

            model.AppIndicId = modelAppDetMaint.AppIndicId;
            model.ObjType = modelAppDetMaint.ObjType;


            var rValue = appDetailMaintBL.GetAppDetailMaintList(UserManager.UserInfo, model, out totalCount).Data;

            SetComboBox(model.AppIndicId);
            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });

        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.AppointmentIndicatorMaint.AppointmentIndicatorMaintIndex)]
        public JsonResult AddAppDetailLabourAndPart(AppointmentDetailsMaintenanceViewModel model)
        {
            AppointmentDetailsMaintenanceBL appDetailMaintBL = new AppointmentDetailsMaintenanceBL();
            model.CommandType = ODMSCommon.CommonValues.DMLType.Insert;

            appDetailMaintBL.DMLAppIndMainPartsLabours(UserManager.UserInfo, model);

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                model.ErrorMessage);

        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.AppointmentIndicatorMaint.AppointmentIndicatorMaintIndex)]
        public JsonResult DeleteAppDetailLabourAndPart(AppointmentDetailsMaintenanceViewModel model)
        {
            AppointmentDetailsMaintenanceBL appDetailMaintBL = new AppointmentDetailsMaintenanceBL();
            model.CommandType = ODMSCommon.CommonValues.DMLType.Delete;

            appDetailMaintBL.DMLAppIndMainPartsLabours(UserManager.UserInfo, model);

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                model.ErrorMessage);

        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.AppointmentIndicatorMaint.AppointmentIndicatorMaintIndex, ODMSCommon.CommonValues.PermissionCodes.AppointmentIndicatorMaint.AppointmentIndicatorMaintPartChange)]
        [HttpGet]
        public ActionResult ChangePart(int AppIndPartId)
        {
            ChangePartViewModel model = new ChangePartViewModel();
            AppointmentDetailsMaintenanceBL appDetailsMaintBL = new AppointmentDetailsMaintenanceBL();

            model.AppIndPartId = AppIndPartId;
            appDetailsMaintBL.GetSparePart(UserManager.UserInfo, model);

            return View(model);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.AppointmentIndicatorMaint.AppointmentIndicatorMaintIndex, ODMSCommon.CommonValues.PermissionCodes.AppointmentIndicatorMaint.AppointmentIndicatorMaintPartChange)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePart(ChangePartViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppointmentDetailsMaintenanceBL appDetailsMaintBL = new AppointmentDetailsMaintenanceBL();
                appDetailsMaintBL.ReplaceAppIndicatorPart(UserManager.UserInfo, model);

                CheckErrorForMessage(model, true);
                appDetailsMaintBL.GetSparePart(UserManager.UserInfo, model);
            }
            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.AppointmentIndicatorMaint.AppointmentIndicatorMaintIndex, ODMSCommon.CommonValues.PermissionCodes.AppointmentIndicatorMaint.AppointmentIndicatorMaintDelete)]
        public JsonResult DeleteAppointmentDetailsMaintenance(AppointmentDetailsMaintenanceViewModel model)
        {
            AppointmentDetailsMaintenanceBL appDetailsMaintBL = new AppointmentDetailsMaintenanceBL();
            model.CommandType = ODMSCommon.CommonValues.DMLType.Delete;

            appDetailsMaintBL.DMLAppoinmentIndicatorMaint(UserManager.UserInfo, model);

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                model.ErrorMessage);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.AppointmentIndicatorMaint.AppointmentIndicatorMaintIndex, ODMSCommon.CommonValues.PermissionCodes.AppointmentIndicatorMaint.AppointmentIndicatorMaintCreate)]
        [HttpGet]
        public ActionResult AppointmentDetailsMaintenanceCreate(int AppIndicId = 0)
        {
            AppointmentDetailsMaintenanceViewModel model = new AppointmentDetailsMaintenanceViewModel
                {
                    AppIndicId = AppIndicId,
                    MaintId = null
                };
            //Set 0 to get different sp condition
            SetComboBox(0);
            return View(model);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.AppointmentIndicatorMaint.AppointmentIndicatorMaintIndex, ODMSCommon.CommonValues.PermissionCodes.AppointmentIndicatorMaint.AppointmentIndicatorMaintCreate)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AppointmentDetailsMaintenanceCreate(AppointmentDetailsMaintenanceViewModel model)
        {
            AppointmentDetailsMaintenanceBL appDetailsMaintBL = new AppointmentDetailsMaintenanceBL();
            model.CommandType = ODMSCommon.CommonValues.DMLType.Insert;

            appDetailsMaintBL.DMLAppoinmentIndicatorMaint(UserManager.UserInfo, model);

            CheckErrorForMessage(model, true);
            //Set 0 to get different sp condition
            SetComboBox(0);
            return View(model);
        }

        private void SetComboBox(int AppIndicId)
        {
            List<SelectListItem> listMaintItems = AppointmentDetailsMaintenanceBL.ListAppMaintenanceAsSelectItem(UserManager.UserInfo, AppIndicId).Data;
            ViewBag.MSelectList = listMaintItems;
        }

    }
}

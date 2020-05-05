using System.Collections.Generic;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.Maintenance;
using ODMSModel.ViewModel;
using System.Web;
using System.IO;
using ODMSModel.DownloadFileActionResult;
using System;
using System.Linq;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class MaintenanceController : ControllerBase
    {
        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.Maintenance.MaintenanceIndex)]
        public JsonResult ListCategories(int id = 0)
        {
            return Json(AppointmentIndicatorCategoryBL.ListAppointmentIndicatorCategories(UserManager.UserInfo, id, true).Data);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.Maintenance.MaintenanceIndex)]
        public JsonResult ListSubCategories(int id = 0)
        {
            return Json(AppointmentIndicatorSubCategoryBL.ListAppointmentIndicatorSubCategories(UserManager.UserInfo, id, true).Data);
        }

        //Index
        [AuthorizationFilter(CommonValues.PermissionCodes.Maintenance.MaintenanceIndex)]
        [HttpGet]
        public ActionResult MaintenanceIndex()
        {
            SetComboBox();
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Maintenance.MaintenanceIndex)]
        [HttpPost]
        public ActionResult MaintenanceIndex(HttpPostedFileBase excelFile)
        {
            MaintenanceViewModel model = new MaintenanceViewModel();

            if (ModelState.IsValid)
            {
                if (excelFile != null)
                {
                    var bo = new MaintenanceBL();
                    Stream s = excelFile.InputStream;
                    List<MaintenanceViewModel> modelList = bo.ParseExcel(UserManager.UserInfo, model, s).Data;
                    if (model.ErrorNo > 0)
                    {
                        var ms = bo.SetExcelReport(modelList, model.ErrorMessage);

                        var fileViewModel = new DownloadFileViewModel
                        {
                            FileName = CommonValues.ErrorLog + DateTime.Now + CommonValues.ExcelExtOld,
                            ContentType = CommonValues.ExcelContentType,
                            MStream = ms,
                            Id = Guid.NewGuid()
                        };

                        Session[CommonValues.DownloadFileFormatCookieKey] = fileViewModel.Add(fileViewModel);
                        TempData[CommonValues.DownloadFileIdCookieKey] = fileViewModel.Id;

                        return base.Json(new { Success = false, Message = model.ErrorMessage, FileId = fileViewModel.Id });
                    }
                    else
                    {
                        foreach (var row in modelList)
                        {
                            if (row.MaintId > 0)
                                row.CommandType = CommonValues.DMLType.Update;
                            else
                                row.CommandType = CommonValues.DMLType.Insert;

                            bo.DMLMaintenance(UserManager.UserInfo, row);
                            if (row.ErrorNo > 0)
                            {
                                return base.Json(new { Success = false, Message = row.ErrorMessage });
                            }
                        }

                        return base.Json(new { Success = true, Message = MessageResource.Global_Display_Success });
                    }
                }
                else
                {
                    return base.Json(new { Success = false, Message = MessageResource.Global_Warning_ExcelNotSelected });
                }
            }
            return base.Json(new { Success = false, Message = MessageResource.Global_Warning_ExcelNotSelected });
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Maintenance.MaintenanceIndex, CommonValues.PermissionCodes.Maintenance.MaintenanceDetails)]
        public JsonResult ListMaintenance([DataSourceRequest] DataSourceRequest request, MaintenanceListModel maintModel)
        {
            MaintenanceBL maintBL = new MaintenanceBL();
            MaintenanceListModel model_Maint = new MaintenanceListModel(request);
            int totalCount = 0;

            model_Maint.IsActiveSearch = maintModel.IsActiveSearch;
            model_Maint.VehicleTypeId = maintModel.VehicleTypeId;
            model_Maint.MaintTypeId = maintModel.MaintTypeId;
            model_Maint.CategoryId = maintModel.CategoryId;
            model_Maint.MainCategoryId = maintModel.MainCategoryId;
            model_Maint.SubCategoryId = maintModel.SubCategoryId;
            model_Maint.MaintName = maintModel.MaintName;
            model_Maint.EngineType = maintModel.EngineType;
            model_Maint.FailureCodeId = maintModel.FailureCodeId;

            var rValue = maintBL.GetMaintenanceList(UserManager.UserInfo, model_Maint, out totalCount).Data;

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });

        }


        //Details
        [AuthorizationFilter(CommonValues.PermissionCodes.Maintenance.MaintenanceIndex, CommonValues.PermissionCodes.Maintenance.MaintenanceDetails)]
        [HttpGet]
        public ActionResult MaintenanceDetails(int id = 0)
        {
            MaintenanceBL maintBL = new MaintenanceBL();
            MaintenanceViewModel model_Maint = new MaintenanceViewModel();
            model_Maint.MaintId = id;

            maintBL.GetMaintenance(UserManager.UserInfo, model_Maint);

            return View(model_Maint);
        }


        //Create
        [AuthorizationFilter(CommonValues.PermissionCodes.Maintenance.MaintenanceIndex, CommonValues.PermissionCodes.Maintenance.MaintenanceCreate)]
        [HttpGet]
        public ActionResult MaintenanceCreate()
        {
            SetComboBox();
            MaintenanceViewModel model = new MaintenanceViewModel();
            model.IsActive = true;
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Maintenance.MaintenanceIndex, CommonValues.PermissionCodes.Maintenance.MaintenanceCreate)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MaintenanceCreate(MaintenanceViewModel maintModel)
        {
            MaintenanceBL maintBL = new MaintenanceBL();
            SetComboBox();
            if (ModelState.IsValid)
            {
                maintModel.CommandType = CommonValues.DMLType.Insert;
                maintBL.DMLMaintenance(UserManager.UserInfo, maintModel);

                CheckErrorForMessage(maintModel, true);

                ModelState.Clear();

                MaintenanceViewModel newModel = new MaintenanceViewModel();
                return View(newModel);

            }
            return View(maintModel);
        }


        //Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.Maintenance.MaintenanceIndex, CommonValues.PermissionCodes.Maintenance.MaintenanceDelete)]
        public ActionResult MaintenanceDelete(int maintId)
        {
            MaintenanceBL maintBL = new MaintenanceBL();
            MaintenanceViewModel maintModel = new MaintenanceViewModel { MaintId = maintId };
            maintModel.CommandType = maintModel.MaintId > 0 ? CommonValues.DMLType.Delete : "";

            maintBL.DMLMaintenance(UserManager.UserInfo, maintModel);

            if (maintModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, maintModel.ErrorMessage);
        }


        //Update
        [AuthorizationFilter(CommonValues.PermissionCodes.Maintenance.MaintenanceIndex, CommonValues.PermissionCodes.Maintenance.MaintenanceUpdate)]
        [HttpGet]
        public ActionResult MaintenanceUpdate(int id = 0)
        {
            MaintenanceBL maintBL = new MaintenanceBL();
            MaintenanceViewModel model_Maint = new MaintenanceViewModel { MaintId = id };

            maintBL.GetMaintenance(UserManager.UserInfo, model_Maint);
            SetComboBox();

            return View(model_Maint);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Maintenance.MaintenanceIndex, CommonValues.PermissionCodes.Maintenance.MaintenanceUpdate)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MaintenanceUpdate(MaintenanceViewModel maintModel)
        {
            MaintenanceBL maintBL = new MaintenanceBL();
            if (ModelState.IsValid)
            {
                maintModel.CommandType = maintModel.MaintId > 0 ? CommonValues.DMLType.Update : "";
                maintBL.DMLMaintenance(UserManager.UserInfo, maintModel);
                if (!CheckErrorForMessage(maintModel, true))
                {
                    maintModel.MaintName = (MultiLanguageModel)CommonUtility.DeepClone(maintModel.MaintName);
                    maintModel.MaintName.MultiLanguageContentAsText = maintModel.MultiLanguageContentAsText;
                }
            }
            SetComboBox();

            return View(maintModel);
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.LabourDuration.LabourDurationIndex)]
        public ActionResult ListVehicleEngineTypes(string vehicleTypeId)
        {
            return string.IsNullOrEmpty(vehicleTypeId)
                       ? Json(new List<SelectListItem>(), JsonRequestBehavior.AllowGet)
                       : Json(VehicleBL.ListVehicleEngineTypesAsSelectListItem(UserManager.UserInfo, vehicleTypeId.GetValue<int?>()).Data, JsonRequestBehavior.AllowGet);
        }

        //SetDefault
        private void SetComboBox()
        {
            List<SelectListItem> list_VehicleT = VehicleTypeBL.ListVehicleTypeAsSelectList(UserManager.UserInfo, null).Data;
            List<SelectListItem> list_MaintT = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.MaintenanceTypeLookup).Data;
            List<SelectListItem> list_SelectList = CommonBL.ListStatus().Data;
            ViewBag.IASelectList = list_SelectList;
            ViewBag.VTSelectList = list_VehicleT;
            ViewBag.MTSelectList = list_MaintT;
            ViewBag.MainCategoryList = AppointmentIndicatorMainCategoryBL.ListAppointmentIndicatorMainCategories(UserManager.UserInfo, true).Data;
            ViewBag.FailureCodeList = new WorkOrderCardBL().ListFailureCodes(UserManager.UserInfo).Data;

        }


        public ActionResult ExcelSample()
        {
            MaintenanceBL maintBL = new MaintenanceBL();
            var ms = maintBL.SetExcelReport(null, string.Empty);
            return File(ms, CommonValues.ExcelContentType, MessageResource.Maintenance_PageTitle_Index + CommonValues.ExcelExtOld);
        }

    }
}

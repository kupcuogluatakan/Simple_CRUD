using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.AppointmentIndicatorCategory;
using ODMSModel.DownloadFileActionResult;
using ODMSModel.ViewModel;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class AppointmentIndicatorCategoryController : ControllerBase
    {

        public ActionResult ExcelSample()
        {
            var bo = new AppointmentIndicatorCategoryBL();
            var ms = bo.SampleExcelFormat();
            return File(ms, CommonValues.ExcelContentType, MessageResource.AppointmentIndicatorCategory_PageTitle_Index + CommonValues.ExcelExtOld);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.AppointmentIndicatorCategory.AppointmentIndicatorCategoryDetails)]
        public JsonResult ListCategories(int? id)
        {
            return id.HasValue ? Json(AppointmentIndicatorCategoryBL.ListAppointmentIndicatorCategories(UserManager.UserInfo, id, true).Data, JsonRequestBehavior.AllowGet) : Json(null, JsonRequestBehavior.AllowGet);
        }

        private void SetDefaults()
        {
            ViewBag.StatusList = CommonBL.ListStatus().Data;
            ViewBag.AppointmentMainCategoryList = AppointmentIndicatorMainCategoryBL.ListAppointmentIndicatorMainCategories(UserManager.UserInfo, true).Data;
        }

        #region Index

        [AuthorizationFilter(CommonValues.PermissionCodes.AppointmentIndicatorCategory.AppointmentIndicatorCategoryIndex)]
        [HttpGet]
        public ActionResult AppointmentIndicatorCategoryIndex(int? appointmentIndicatorMainCategoryId)
        {
            SetDefaults();
            AppointmentIndicatorCategoryListModel model = new AppointmentIndicatorCategoryListModel();
            if (appointmentIndicatorMainCategoryId != null)
            {
                model.AppointmentIndicatorMainCategoryId = appointmentIndicatorMainCategoryId.GetValue<int>();
            }
            return View(model);
        }


        [AuthorizationFilter(CommonValues.PermissionCodes.AppointmentIndicatorCategory.AppointmentIndicatorCategoryIndex)]
        [HttpPost]
        public ActionResult AppointmentIndicatorCategoryIndex(AppointmentIndicatorCategoryListModel listModel, HttpPostedFileBase excelFile)
        {
            SetDefaults();

            var service = new AppointmentIndicatorCategoryBL();

            var model = new AppointmentIndicatorCategoryViewModel();

            var listModels = new List<AppointmentIndicatorCategoryViewModel>();

            model.CommandType = CommonValues.DMLType.Insert;
            using (Stream s = excelFile.InputStream)
            {
                listModels = service.ParseExcel(UserManager.UserInfo, model, s).Data;
            }

            if (listModels.Exists(q => q.ErrorNo >= 1))
            {
                var ms = service.SetExcelReport(listModels, model.ErrorMessage);

                var fileViewModel = new DownloadFileViewModel
                {
                    FileName = CommonValues.ErrorLog + DateTime.Now + CommonValues.ExcelExtOld,
                    ContentType = CommonValues.ExcelContentType,
                    MStream = ms,
                    Id = Guid.NewGuid()
                };

                Session[CommonValues.DownloadFileFormatCookieKey] = fileViewModel.Add(fileViewModel);
                TempData[CommonValues.DownloadFileIdCookieKey] = fileViewModel.Id;

                SetMessage(model.ErrorMessage, CommonValues.MessageSeverity.Fail);

                return View();
            }
            else
            {
                service.DMLAppointmentIndicatorCategory(UserManager.UserInfo, model, listModels);
                CheckErrorForMessage(model, true);
                return View();
            }
        }

        [AuthorizationFilter(
            CommonValues.PermissionCodes.AppointmentIndicatorCategory.AppointmentIndicatorCategoryIndex,
            CommonValues.PermissionCodes.AppointmentIndicatorCategory.AppointmentIndicatorCategoryDetails)]
        public JsonResult ListAppointmentIndicatorCategory([DataSourceRequest] DataSourceRequest request,
                                                               AppointmentIndicatorCategoryListModel
                                                                   appointmentIndicatorCategoryModel)
        {
            AppointmentIndicatorCategoryBL appointmentIndicatorCategoryBL = new AppointmentIndicatorCategoryBL();
            AppointmentIndicatorCategoryListModel model_AppointmentIndicatorCategory = new AppointmentIndicatorCategoryListModel(request);
            int totalCount = 0;

            model_AppointmentIndicatorCategory.IsActive = appointmentIndicatorCategoryModel.IsActive;
            model_AppointmentIndicatorCategory.AppointmentIndicatorMainCategoryId = appointmentIndicatorCategoryModel.AppointmentIndicatorMainCategoryId;
            model_AppointmentIndicatorCategory.AppointmentIndicatorCategoryId = appointmentIndicatorCategoryModel.AppointmentIndicatorCategoryId;
            model_AppointmentIndicatorCategory.AdminDesc = appointmentIndicatorCategoryModel.AdminDesc;

            var rValue = appointmentIndicatorCategoryBL.GetAppointmentIndicatorCategoryList(UserManager.UserInfo, model_AppointmentIndicatorCategory, out totalCount).Data;

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });

        }

        #endregion

        #region Details

        [AuthorizationFilter(
            CommonValues.PermissionCodes.AppointmentIndicatorCategory.AppointmentIndicatorCategoryIndex,
            CommonValues.PermissionCodes.AppointmentIndicatorCategory.AppointmentIndicatorCategoryDetails)]
        [HttpGet]
        public ActionResult AppointmentIndicatorCategoryDetails(int id = 0)
        {
            AppointmentIndicatorCategoryBL appointmentIndicatorCategoryBL =
                new AppointmentIndicatorCategoryBL();
            AppointmentIndicatorCategoryViewModel model_AppointmentIndicatorCategory =
                new AppointmentIndicatorCategoryViewModel { AppointmentIndicatorCategoryId = id };

            appointmentIndicatorCategoryBL.GetAppointmentIndicatorCategory(UserManager.UserInfo, model_AppointmentIndicatorCategory);

            return View(model_AppointmentIndicatorCategory);
        }

        #endregion

        #region Create

        [AuthorizationFilter(
            CommonValues.PermissionCodes.AppointmentIndicatorCategory.AppointmentIndicatorCategoryCreate,
            CommonValues.PermissionCodes.AppointmentIndicatorCategory.AppointmentIndicatorCategoryIndex)]
        [HttpGet]
        public ActionResult AppointmentIndicatorCategoryCreate()
        {
            SetDefaults();
            AppointmentIndicatorCategoryViewModel model = new AppointmentIndicatorCategoryViewModel();
            model.IsActive = true;
            return View(model);
        }

        [AuthorizationFilter(
            CommonValues.PermissionCodes.AppointmentIndicatorCategory.AppointmentIndicatorCategoryCreate,
            CommonValues.PermissionCodes.AppointmentIndicatorCategory.AppointmentIndicatorCategoryIndex)]
        [HttpPost]
        public ActionResult AppointmentIndicatorCategoryCreate(AppointmentIndicatorCategoryViewModel appointmentIndicatorCategoryModel)
        {
            SetDefaults();
            AppointmentIndicatorCategoryBL appointmentIndicatorCategoryBL = new AppointmentIndicatorCategoryBL();

            if (ModelState.IsValid)
            {
                AppointmentIndicatorCategoryViewModel existedModel = appointmentIndicatorCategoryBL.GetAppointmentIndicatorCategoryByCode(UserManager.UserInfo, appointmentIndicatorCategoryModel.Code).Model;
                if (string.IsNullOrEmpty(existedModel.Code))
                {
                    appointmentIndicatorCategoryModel.CommandType = CommonValues.DMLType.Insert;
                    appointmentIndicatorCategoryBL.DMLAppointmentIndicatorCategory(UserManager.UserInfo, appointmentIndicatorCategoryModel);
                    CheckErrorForMessage(appointmentIndicatorCategoryModel, true);

                    ModelState.Clear();
                    AppointmentIndicatorCategoryViewModel model = new AppointmentIndicatorCategoryViewModel();
                    return View(model);
                }
                else
                {
                    SetMessage(MessageResource.AppointmentIndicatorCategory_Warning_ExistedCode, CommonValues.MessageSeverity.Fail);
                }
            }
            return View(appointmentIndicatorCategoryModel);
        }

        #endregion

        #region Delete


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.AppointmentIndicatorCategory.AppointmentIndicatorCategoryDelete,
                             CommonValues.PermissionCodes.AppointmentIndicatorCategory.AppointmentIndicatorCategoryIndex)]
        public ActionResult AppointmentIndicatorCategoryDelete(int appointmentIndicatorCategoryId)
        {
            AppointmentIndicatorCategoryBL appointmentIndicatorCategoryBL = new AppointmentIndicatorCategoryBL();
            AppointmentIndicatorCategoryViewModel appointmentIndicatorCategoryModel = new AppointmentIndicatorCategoryViewModel
            {
                AppointmentIndicatorCategoryId = appointmentIndicatorCategoryId
            };

            appointmentIndicatorCategoryModel.CommandType = appointmentIndicatorCategoryModel.AppointmentIndicatorCategoryId > 0
                    ? CommonValues.DMLType.Delete
                    : "";

            appointmentIndicatorCategoryBL.DMLAppointmentIndicatorCategory(UserManager.UserInfo, appointmentIndicatorCategoryModel);

            if (appointmentIndicatorCategoryModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                                                      MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                                                  appointmentIndicatorCategoryModel.ErrorMessage);
        }

        #endregion

        #region Update

        [AuthorizationFilter(
            CommonValues.PermissionCodes.AppointmentIndicatorCategory.AppointmentIndicatorCategoryUpdate,
            CommonValues.PermissionCodes.AppointmentIndicatorCategory.AppointmentIndicatorCategoryIndex)]
        [HttpGet]
        public ActionResult AppointmentIndicatorCategoryUpdate(int id = 0)
        {
            AppointmentIndicatorCategoryBL appointmentIndicatorCategoryBL = new AppointmentIndicatorCategoryBL();
            AppointmentIndicatorCategoryViewModel model_AppointmentIndicatorCategory = new AppointmentIndicatorCategoryViewModel { AppointmentIndicatorCategoryId = id };

            appointmentIndicatorCategoryBL.GetAppointmentIndicatorCategory(UserManager.UserInfo, model_AppointmentIndicatorCategory);
            SetDefaults();

            return View(model_AppointmentIndicatorCategory);

        }

        [AuthorizationFilter(
            CommonValues.PermissionCodes.AppointmentIndicatorCategory.AppointmentIndicatorCategoryUpdate,
            CommonValues.PermissionCodes.AppointmentIndicatorCategory.AppointmentIndicatorCategoryIndex)]
        [HttpPost]
        public ActionResult AppointmentIndicatorCategoryUpdate(AppointmentIndicatorCategoryViewModel appointmentIndicatorCategoryModel)
        {
            SetDefaults();
            AppointmentIndicatorCategoryBL appointmentIndicatorCategoryBL = new AppointmentIndicatorCategoryBL();
            if (ModelState.IsValid)
            {
                appointmentIndicatorCategoryModel.CommandType = CommonValues.DMLType.Update;
                appointmentIndicatorCategoryBL.DMLAppointmentIndicatorCategory(UserManager.UserInfo, appointmentIndicatorCategoryModel);

                if (!CheckErrorForMessage(appointmentIndicatorCategoryModel, true))
                {
                    appointmentIndicatorCategoryModel.AppointmentIndicatorCategoryName =
                        (MultiLanguageModel)
                        CommonUtility.DeepClone(appointmentIndicatorCategoryModel.AppointmentIndicatorCategoryName);
                    appointmentIndicatorCategoryModel.AppointmentIndicatorCategoryName.MultiLanguageContentAsText = appointmentIndicatorCategoryModel.MultiLanguageContentAsText;
                }
            }

            return View(appointmentIndicatorCategoryModel);
        }

        #endregion
    }
}

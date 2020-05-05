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
using ODMSModel.AppointmentIndicatorMainCategory;
using ODMSModel.DownloadFileActionResult;
using ODMSModel.ViewModel;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class AppointmentIndicatorMainCategoryController : ControllerBase
    {
        private void SetDefaults()
        {
            //StatusList
            ViewBag.StatusList = CommonBL.ListStatus().Data;
        }

        #region Index

        public ActionResult ExcelSample()
        {
            var bo = new AppointmentIndicatorMainCategoryBL();
            var ms = bo.SetExcelReport(null, string.Empty);
            return File(ms, CommonValues.ExcelContentType, MessageResource.AppointmentIndicatorMainCategory_PageTitle_Index + CommonValues.ExcelExtOld);
        }
        [AuthorizationFilter(
            CommonValues.PermissionCodes.AppointmentIndicatorMainCategory.AppointmentIndicatorMainCategoryIndex)]
        [HttpGet]
        public ActionResult AppointmentIndicatorMainCategoryIndex()
        {
            SetDefaults();
            return View();
        }

        [AuthorizationFilter(
            CommonValues.PermissionCodes.AppointmentIndicatorMainCategory.AppointmentIndicatorMainCategoryIndex
            , CommonValues.PermissionCodes.AppointmentIndicatorMainCategory.AppointmentIndicatorMainCategoryDetails)]
        [HttpPost]
        public ActionResult AppointmentIndicatorMainCategoryIndex(AppointmentIndicatorMainCategoryListModel listModel,
                                                                  HttpPostedFileBase excelFile)
        {
            SetDefaults();

            AppointmentIndicatorMainCategoryViewModel model = new AppointmentIndicatorMainCategoryViewModel();
            if (excelFile != null)
            {
                AppointmentIndicatorMainCategoryBL bo = new AppointmentIndicatorMainCategoryBL();
                Stream s = excelFile.InputStream;
                List<AppointmentIndicatorMainCategoryViewModel> modelList = bo.ParseExcel(UserManager.UserInfo, model, s).Data;
                // excel dosyasındaki veriler kontrol edilir.
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

                    SetMessage(model.ErrorMessage, CommonValues.MessageSeverity.Fail);

                    return View();
                }
                else
                {

                    SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);
                }
            }
            else
            {
                SetMessage(MessageResource.Global_Warning_ExcelNotSelected, CommonValues.MessageSeverity.Fail);
            }
            ModelState.Clear();
            AppointmentIndicatorMainCategoryListModel vModel = new AppointmentIndicatorMainCategoryListModel();
            return View(vModel);
        }

        [AuthorizationFilter(
                CommonValues.PermissionCodes.AppointmentIndicatorMainCategory.AppointmentIndicatorMainCategoryIndex,
                CommonValues.PermissionCodes.AppointmentIndicatorMainCategory.AppointmentIndicatorMainCategoryDetails)]
        public JsonResult ListAppointmentIndicatorMainCategory([DataSourceRequest] DataSourceRequest request,
                                                               AppointmentIndicatorMainCategoryListModel
                                                                   appointmentIndicatorMainCategoryModel)
        {
            AppointmentIndicatorMainCategoryBL appointmentIndicatorMainCategoryBL =
                new AppointmentIndicatorMainCategoryBL();
            AppointmentIndicatorMainCategoryListModel model_AppointmentIndicatorMainCategory =
                new AppointmentIndicatorMainCategoryListModel(request);
            int totalCount = 0;

            model_AppointmentIndicatorMainCategory.IsActive = appointmentIndicatorMainCategoryModel.IsActive;

            var rValue = appointmentIndicatorMainCategoryBL.GetAppointmentIndicatorMainCategoryList(UserManager.UserInfo, model_AppointmentIndicatorMainCategory, out totalCount).Data;

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });

        }

        #endregion

        #region Details

        [AuthorizationFilter(
            CommonValues.PermissionCodes.AppointmentIndicatorMainCategory.AppointmentIndicatorMainCategoryIndex,
            CommonValues.PermissionCodes.AppointmentIndicatorMainCategory.AppointmentIndicatorMainCategoryDetails)]
        [HttpGet]
        public ActionResult AppointmentIndicatorMainCategoryDetails(int id = 0)
        {
            AppointmentIndicatorMainCategoryBL appointmentIndicatorMainCategoryBL = new AppointmentIndicatorMainCategoryBL();
            AppointmentIndicatorMainCategoryViewModel model_AppointmentIndicatorMainCategory = new AppointmentIndicatorMainCategoryViewModel { AppointmentIndicatorMainCategoryId = id };

            appointmentIndicatorMainCategoryBL.GetAppointmentIndicatorMainCategory(UserManager.UserInfo, model_AppointmentIndicatorMainCategory);

            return View(model_AppointmentIndicatorMainCategory);
        }

        #endregion

        #region Create

        [AuthorizationFilter(
            CommonValues.PermissionCodes.AppointmentIndicatorMainCategory.AppointmentIndicatorMainCategoryCreate,
            CommonValues.PermissionCodes.AppointmentIndicatorMainCategory.AppointmentIndicatorMainCategoryIndex)]
        [HttpGet]
        public ActionResult AppointmentIndicatorMainCategoryCreate()
        {
            SetDefaults();
            AppointmentIndicatorMainCategoryViewModel model = new AppointmentIndicatorMainCategoryViewModel();
            model.IsActive = true;
            model.CanBeUsedInAppointment = true;
            return View(model);
        }

        [AuthorizationFilter(
            CommonValues.PermissionCodes.AppointmentIndicatorMainCategory.AppointmentIndicatorMainCategoryCreate,
            CommonValues.PermissionCodes.AppointmentIndicatorMainCategory.AppointmentIndicatorMainCategoryIndex)]
        [HttpPost]
        public ActionResult AppointmentIndicatorMainCategoryCreate(
            AppointmentIndicatorMainCategoryViewModel appointmentIndicatorMainCategoryModel)
        {
            SetDefaults();
            AppointmentIndicatorMainCategoryBL appointmentIndicatorMainCategoryBL =
                new AppointmentIndicatorMainCategoryBL();

            if (ModelState.IsValid)
            {
                AppointmentIndicatorMainCategoryViewModel existedModel = appointmentIndicatorMainCategoryBL.GetAppointmentIndicatorMainCategoryByMainCode(UserManager.UserInfo, appointmentIndicatorMainCategoryModel.MainCode).Model;

                if (string.IsNullOrEmpty(existedModel.MainCode))
                {
                    appointmentIndicatorMainCategoryModel.CommandType = CommonValues.DMLType.Insert;
                    appointmentIndicatorMainCategoryBL.DMLAppointmentIndicatorMainCategory(UserManager.UserInfo, appointmentIndicatorMainCategoryModel);

                    CheckErrorForMessage(appointmentIndicatorMainCategoryModel, true);

                    ModelState.Clear();
                    AppointmentIndicatorMainCategoryViewModel model = new AppointmentIndicatorMainCategoryViewModel();
                    return View(model);
                }
                else
                {
                    SetMessage(MessageResource.AppointmentIndicatorMainCategory_Warning_ExistedMainCode, CommonValues.MessageSeverity.Fail);
                }
            }
            return View(appointmentIndicatorMainCategoryModel);
        }

        #endregion

        #region Delete


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.AppointmentIndicatorMainCategory.AppointmentIndicatorMainCategoryDelete, CommonValues.PermissionCodes.AppointmentIndicatorMainCategory.AppointmentIndicatorMainCategoryIndex)]
        public ActionResult AppointmentIndicatorMainCategoryDelete(int appointmentIndicatorMainCategoryId)
        {
            AppointmentIndicatorMainCategoryBL appointmentIndicatorMainCategoryBL = new AppointmentIndicatorMainCategoryBL();
            AppointmentIndicatorMainCategoryViewModel appointmentIndicatorMainCategoryModel = new AppointmentIndicatorMainCategoryViewModel
            {
                AppointmentIndicatorMainCategoryId = appointmentIndicatorMainCategoryId
            };

            appointmentIndicatorMainCategoryModel.CommandType = appointmentIndicatorMainCategoryModel.AppointmentIndicatorMainCategoryId > 0
                    ? CommonValues.DMLType.Delete
                    : "";

            appointmentIndicatorMainCategoryBL.DMLAppointmentIndicatorMainCategory(UserManager.UserInfo, appointmentIndicatorMainCategoryModel);

            if (appointmentIndicatorMainCategoryModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                                                      MessageResource.Global_Display_Success);

            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                                                  appointmentIndicatorMainCategoryModel.ErrorMessage);
        }

        #endregion

        #region Update

        [AuthorizationFilter(CommonValues.PermissionCodes.AppointmentIndicatorMainCategory.AppointmentIndicatorMainCategoryUpdate, CommonValues.PermissionCodes.AppointmentIndicatorMainCategory.AppointmentIndicatorMainCategoryIndex)]
        [HttpGet]
        public ActionResult AppointmentIndicatorMainCategoryUpdate(int id = 0)
        {
            AppointmentIndicatorMainCategoryBL appointmentIndicatorMainCategoryBL = new AppointmentIndicatorMainCategoryBL();
            AppointmentIndicatorMainCategoryViewModel model_AppointmentIndicatorMainCategory = new AppointmentIndicatorMainCategoryViewModel { AppointmentIndicatorMainCategoryId = id };

            appointmentIndicatorMainCategoryBL.GetAppointmentIndicatorMainCategory(UserManager.UserInfo, model_AppointmentIndicatorMainCategory);
            SetDefaults();

            return View(model_AppointmentIndicatorMainCategory);

        }

        [AuthorizationFilter(
            CommonValues.PermissionCodes.AppointmentIndicatorMainCategory.AppointmentIndicatorMainCategoryUpdate,
            CommonValues.PermissionCodes.AppointmentIndicatorMainCategory.AppointmentIndicatorMainCategoryIndex)]
        [HttpPost]
        public ActionResult AppointmentIndicatorMainCategoryUpdate(
            AppointmentIndicatorMainCategoryViewModel appointmentIndicatorMainCategoryModel)
        {
            SetDefaults();
            AppointmentIndicatorMainCategoryBL appointmentIndicatorMainCategoryBL = new AppointmentIndicatorMainCategoryBL();
            if (ModelState.IsValid)
            {

                appointmentIndicatorMainCategoryModel.CommandType = CommonValues.DMLType.Update;
                appointmentIndicatorMainCategoryBL.DMLAppointmentIndicatorMainCategory(UserManager.UserInfo, appointmentIndicatorMainCategoryModel);

                if (!CheckErrorForMessage(appointmentIndicatorMainCategoryModel, true))
                {
                    appointmentIndicatorMainCategoryModel.AppointmentIndicatorMainCategoryName =
                        (MultiLanguageModel)
                        CommonUtility.DeepClone(
                            appointmentIndicatorMainCategoryModel.AppointmentIndicatorMainCategoryName);
                    appointmentIndicatorMainCategoryModel.AppointmentIndicatorMainCategoryName
                                                         .MultiLanguageContentAsText =
                        appointmentIndicatorMainCategoryModel.MultiLanguageContentAsText;
                }

            }

            return View(appointmentIndicatorMainCategoryModel);
        }

        #endregion
    }
}

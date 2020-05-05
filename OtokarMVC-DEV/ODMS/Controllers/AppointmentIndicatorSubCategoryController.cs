using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.AppointmentIndicatorSubCategory;
using ODMSModel.DownloadFileActionResult;
using ODMSModel.ViewModel;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class AppointmentIndicatorSubCategoryController : ControllerBase
    {
        private void SetDefaults()
        {
            //StatusList
            ViewBag.StatusList = CommonBL.ListStatus().Data;
            //AppointmentMainCategoryList
            ViewBag.AppointmentMainCategoryList = AppointmentIndicatorMainCategoryBL.ListAppointmentIndicatorMainCategories(UserManager.UserInfo, true).Data;
            //YesNoList
            ViewBag.YesNoList = CommonBL.ListYesNo().Data;
            var bo = new AppointmentIndicatorSubCategoryBL();
            ViewBag.IndicatorTypeList = bo.ListOfIndicatorTypeCodeAsSelectListItem(UserManager.UserInfo).Data;
        }

        public ActionResult ExcelSample()
        {
            var bo = new AppointmentIndicatorSubCategoryBL();
            var ms = bo.SampleExcelFormat();
            return File(ms, CommonValues.ExcelContentType, MessageResource.AppointmentIndicatorSubCategory_PageTitle_Index + CommonValues.ExcelExtOld);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.AppointmentIndicatorCategory.AppointmentIndicatorCategoryDetails)]
        public JsonResult ListCategories(int? id)
        {
            return id.HasValue ? Json(AppointmentIndicatorCategoryBL.ListAppointmentIndicatorCategories(UserManager.UserInfo, id, true).Data, JsonRequestBehavior.AllowGet) : Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.AppointmentIndicatorCategory.AppointmentIndicatorCategoryDetails)]
        public JsonResult ListSubCategories(int? id)
        {
            return id.HasValue ? Json(AppointmentIndicatorSubCategoryBL.ListAppointmentIndicatorSubCategories(UserManager.UserInfo, id, true).Data, JsonRequestBehavior.AllowGet) : Json(null, JsonRequestBehavior.AllowGet);
        }

        #region Index

        [AuthorizationFilter(
            CommonValues.PermissionCodes.AppointmentIndicatorSubCategory.AppointmentIndicatorSubCategoryIndex)]
        [HttpGet]
        public ActionResult AppointmentIndicatorSubCategoryIndex(int? appointmentIndicatorCategoryId, int? appointmentIndicatorMainCategoryId)
        {
            SetDefaults();
            AppointmentIndicatorSubCategoryListModel model = new AppointmentIndicatorSubCategoryListModel();
            if (appointmentIndicatorCategoryId != null)
            {
                model.AppointmentIndicatorCategoryId = appointmentIndicatorCategoryId.GetValue<int>();
                model.AppointmentIndicatorMainCategoryId = appointmentIndicatorMainCategoryId.GetValue<int>();
            }
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.AppointmentIndicatorSubCategory.AppointmentIndicatorSubCategoryIndex)]
        [HttpPost]
        public ActionResult AppointmentIndicatorSubCategoryIndex(AppointmentIndicatorSubCategoryListModel listModel, HttpPostedFileBase excelFile)
        {
            SetDefaults();

            var service = new AppointmentIndicatorSubCategoryBL();

            var model = new AppointmentIndicatorSubCategoryViewModel();

            var listModels = new List<AppointmentIndicatorSubCategoryViewModel>();

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
                service.DMLAppointmentIndicatorSubCategory(UserManager.UserInfo, model, listModels);
                CheckErrorForMessage(model, true);
                return View();
            }
        }

        [AuthorizationFilter(
            CommonValues.PermissionCodes.AppointmentIndicatorSubCategory.AppointmentIndicatorSubCategoryIndex,
            CommonValues.PermissionCodes.AppointmentIndicatorSubCategory.AppointmentIndicatorSubCategoryDetails)]
        public JsonResult ListAppointmentIndicatorSubCategory([DataSourceRequest] DataSourceRequest request,
                                                               AppointmentIndicatorSubCategoryListModel
                                                                   appointmentIndicatorSubCategoryModel)
        {

            AppointmentIndicatorSubCategoryBL appointmentIndicatorSubCategoryBL = new AppointmentIndicatorSubCategoryBL();
            AppointmentIndicatorSubCategoryListModel model_AppointmentIndicatorSubCategory = new AppointmentIndicatorSubCategoryListModel(request);

            int totalCount = 0;

            model_AppointmentIndicatorSubCategory.IsActive = appointmentIndicatorSubCategoryModel.IsActive;
            model_AppointmentIndicatorSubCategory.AppointmentIndicatorMainCategoryId = appointmentIndicatorSubCategoryModel.AppointmentIndicatorMainCategoryId;
            model_AppointmentIndicatorSubCategory.AppointmentIndicatorCategoryId = appointmentIndicatorSubCategoryModel.AppointmentIndicatorCategoryId;
            model_AppointmentIndicatorSubCategory.AppointmentIndicatorSubCategoryId = appointmentIndicatorSubCategoryModel.AppointmentIndicatorSubCategoryId;
            model_AppointmentIndicatorSubCategory.AdminDesc = appointmentIndicatorSubCategoryModel.AdminDesc;
            model_AppointmentIndicatorSubCategory.IsAutoCreate = appointmentIndicatorSubCategoryModel.IsAutoCreate;

            var rValue = appointmentIndicatorSubCategoryBL.GetAppointmentIndicatorSubCategoryList(UserManager.UserInfo, model_AppointmentIndicatorSubCategory, out totalCount).Data;

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });

        }

        #endregion

        #region Details

        [AuthorizationFilter(
            CommonValues.PermissionCodes.AppointmentIndicatorSubCategory.AppointmentIndicatorSubCategoryIndex,
            CommonValues.PermissionCodes.AppointmentIndicatorSubCategory.AppointmentIndicatorSubCategoryDetails)]
        [HttpGet]
        public ActionResult AppointmentIndicatorSubCategoryDetails(int id = 0)
        {
            AppointmentIndicatorSubCategoryBL appointmentIndicatorSubCategoryBL = new AppointmentIndicatorSubCategoryBL();
            AppointmentIndicatorSubCategoryViewModel model_AppointmentIndicatorSubCategory = new AppointmentIndicatorSubCategoryViewModel { AppointmentIndicatorSubCategoryId = id };

            appointmentIndicatorSubCategoryBL.GetAppointmentIndicatorSubCategory(UserManager.UserInfo, model_AppointmentIndicatorSubCategory);

            return View(model_AppointmentIndicatorSubCategory);
        }

        #endregion

        #region Create

        [AuthorizationFilter(
            CommonValues.PermissionCodes.AppointmentIndicatorSubCategory.AppointmentIndicatorSubCategoryCreate,
            CommonValues.PermissionCodes.AppointmentIndicatorSubCategory.AppointmentIndicatorSubCategoryIndex)]
        [HttpGet]
        public ActionResult AppointmentIndicatorSubCategoryCreate()
        {
            SetDefaults();
            AppointmentIndicatorSubCategoryViewModel model = new AppointmentIndicatorSubCategoryViewModel();
            model.IsActive = true;
            return View(model);
        }

        [AuthorizationFilter(
            CommonValues.PermissionCodes.AppointmentIndicatorSubCategory.AppointmentIndicatorSubCategoryCreate,
            CommonValues.PermissionCodes.AppointmentIndicatorSubCategory.AppointmentIndicatorSubCategoryIndex)]
        [HttpPost]
        public ActionResult AppointmentIndicatorSubCategoryCreate(AppointmentIndicatorSubCategoryViewModel appointmentIndicatorSubCategoryModel)
        {
            SetDefaults();
            AppointmentIndicatorSubCategoryBL appointmentIndicatorSubCategoryBL = new AppointmentIndicatorSubCategoryBL();

            if (ModelState.IsValid)
            {
                AppointmentIndicatorSubCategoryViewModel existedModel = appointmentIndicatorSubCategoryBL.GetAppointmentIndicatorSubCategoryBySubCode(UserManager.UserInfo, appointmentIndicatorSubCategoryModel.SubCode).Model;
                if (string.IsNullOrEmpty(existedModel.SubCode))
                {
                    appointmentIndicatorSubCategoryModel.CommandType = CommonValues.DMLType.Insert;
                    appointmentIndicatorSubCategoryBL.DMLAppointmentIndicatorSubCategory(UserManager.UserInfo, appointmentIndicatorSubCategoryModel);
                    CheckErrorForMessage(appointmentIndicatorSubCategoryModel, true);

                    ModelState.Clear();
                    AppointmentIndicatorSubCategoryViewModel model = new AppointmentIndicatorSubCategoryViewModel();
                    return View(model);
                }
                else
                {
                    SetMessage(MessageResource.AppointmentIndicatorSubCategory_Warning_ExistedSubCode, CommonValues.MessageSeverity.Fail);
                }
            }
            return View(appointmentIndicatorSubCategoryModel);
        }

        #endregion

        #region Delete


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.AppointmentIndicatorSubCategory.AppointmentIndicatorSubCategoryDelete,
                             CommonValues.PermissionCodes.AppointmentIndicatorSubCategory.AppointmentIndicatorSubCategoryIndex)]
        public ActionResult AppointmentIndicatorSubCategoryDelete(int appointmentIndicatorSubCategoryId)
        {
            AppointmentIndicatorSubCategoryBL appointmentIndicatorSubCategoryBL = new AppointmentIndicatorSubCategoryBL();
            AppointmentIndicatorSubCategoryViewModel appointmentIndicatorSubCategoryModel = new AppointmentIndicatorSubCategoryViewModel
            {
                AppointmentIndicatorSubCategoryId = appointmentIndicatorSubCategoryId
            };

            appointmentIndicatorSubCategoryModel.CommandType = appointmentIndicatorSubCategoryModel.AppointmentIndicatorSubCategoryId > 0
                    ? CommonValues.DMLType.Delete
                    : "";

            appointmentIndicatorSubCategoryBL.DMLAppointmentIndicatorSubCategory(UserManager.UserInfo, appointmentIndicatorSubCategoryModel);

            if (appointmentIndicatorSubCategoryModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                                                      MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                                                  appointmentIndicatorSubCategoryModel.ErrorMessage);
        }

        #endregion

        #region Update

        [AuthorizationFilter(
            CommonValues.PermissionCodes.AppointmentIndicatorSubCategory.AppointmentIndicatorSubCategoryUpdate,
            CommonValues.PermissionCodes.AppointmentIndicatorSubCategory.AppointmentIndicatorSubCategoryIndex)]
        [HttpGet]
        public ActionResult AppointmentIndicatorSubCategoryUpdate(int id = 0)
        {
            AppointmentIndicatorSubCategoryBL appointmentIndicatorSubCategoryBL = new AppointmentIndicatorSubCategoryBL();
            AppointmentIndicatorSubCategoryViewModel model_AppointmentIndicatorSubCategory = new AppointmentIndicatorSubCategoryViewModel { AppointmentIndicatorSubCategoryId = id };

            appointmentIndicatorSubCategoryBL.GetAppointmentIndicatorSubCategory(UserManager.UserInfo, model_AppointmentIndicatorSubCategory);
            SetDefaults();

            return View(model_AppointmentIndicatorSubCategory);

        }

        [AuthorizationFilter(
            CommonValues.PermissionCodes.AppointmentIndicatorSubCategory.AppointmentIndicatorSubCategoryUpdate,
            CommonValues.PermissionCodes.AppointmentIndicatorSubCategory.AppointmentIndicatorSubCategoryIndex)]
        [HttpPost]
        public ActionResult AppointmentIndicatorSubCategoryUpdate(AppointmentIndicatorSubCategoryViewModel appointmentIndicatorSubCategoryModel)
        {
            SetDefaults();
            var appointmentIndicatorSubCategoryBL = new AppointmentIndicatorSubCategoryBL();
            if (ModelState.IsValid)
            {

                appointmentIndicatorSubCategoryModel.IndicatorTypeCodeList = appointmentIndicatorSubCategoryBL.ListOfIndicatorTypeCode(UserManager.UserInfo).Data;
                bool isExistsTYpeCode = appointmentIndicatorSubCategoryModel.IndicatorTypeCodeList.Any(x => x.ToString(CultureInfo.InvariantCulture) == appointmentIndicatorSubCategoryModel.IndicatorTypeCode);
                if (!isExistsTYpeCode)
                {
                    appointmentIndicatorSubCategoryModel.ErrorNo = 1;
                    appointmentIndicatorSubCategoryModel.ErrorMessage = MessageResource.AppointmnetIndicatorSubCategory_Warning_NotExistsIndicatorTypeCode;

                    CheckErrorForMessage(appointmentIndicatorSubCategoryModel, true);

                }
                else
                {
                    appointmentIndicatorSubCategoryModel.CommandType = CommonValues.DMLType.Update;
                    appointmentIndicatorSubCategoryBL.DMLAppointmentIndicatorSubCategory(UserManager.UserInfo, appointmentIndicatorSubCategoryModel);
                    if (!CheckErrorForMessage(appointmentIndicatorSubCategoryModel, true))
                    {
                        appointmentIndicatorSubCategoryModel.AppointmentIndicatorSubCategoryName = (MultiLanguageModel)CommonUtility.DeepClone(appointmentIndicatorSubCategoryModel.AppointmentIndicatorSubCategoryName);
                        appointmentIndicatorSubCategoryModel.AppointmentIndicatorSubCategoryName.MultiLanguageContentAsText = appointmentIndicatorSubCategoryModel.MultiLanguageContentAsText;
                    }
                }
            }

            return View(appointmentIndicatorSubCategoryModel);
        }

        #endregion
    }
}

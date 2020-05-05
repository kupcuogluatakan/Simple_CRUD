using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.AppointmentIndicatorFailureCode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using ODMSModel.DownloadFileActionResult;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class AppointmentIndicatorFailureCodeController : ControllerBase
    {
        private void SetDefaults()
        {
            ViewBag.CodeList = AppointmentIndicatorFailureCodeBL.ListAppointmentCodeAsSelectListItem().Data;
            ViewBag.StatusList = CommonBL.ListStatus().Data;
            ViewBag.ActiveYesNoList = CommonBL.ListYesNo().Data;
        }
        
        [HttpGet]
        public JsonResult ListCode()
        {
            List<SelectListItem> codeList = AppointmentIndicatorFailureCodeBL.ListAppointmentCodeAsSelectListItem().Data;
            codeList.Insert(0, new SelectListItem() { Text = MessageResource.Global_Display_Choose, Value = "0" });

            return Json(codeList, JsonRequestBehavior.AllowGet);
        }

        #region AppointmentIndicatorFailureCode Index

        [AuthorizationFilter(CommonValues.PermissionCodes.AppointmentIndicatorFailureCode.AppointmentIndicatorFailureCodeIndex)]
        [HttpGet]
        public ActionResult AppointmentIndicatorFailureCodeIndex()
        {
            SetDefaults();
            AppointmentIndicatorFailureCodeListModel model = new AppointmentIndicatorFailureCodeListModel();

            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.AppointmentIndicatorFailureCode.AppointmentIndicatorFailureCodeIndex, CommonValues.PermissionCodes.AppointmentIndicatorFailureCode.AppointmentIndicatorFailureCodeIndex)]
        public ActionResult ListAppointmentIndicatorFailureCode([DataSourceRequest] DataSourceRequest request, AppointmentIndicatorFailureCodeListModel model)
        {
            var appointmentIndicatorFailureCodeBo = new AppointmentIndicatorFailureCodeBL();

            var v = new AppointmentIndicatorFailureCodeListModel(request);
            //v.DealerName = model.DealerName;

            v.IdAppointmentIndicatorFailureCode = model.IdAppointmentIndicatorFailureCode;
            v.Code = model.Code;
            v.IsActive = model.IsActive;
            v.IsActiveName = model.IsActiveName;
            v.AdminDesc = model.AdminDesc;
            v.Description = model.Description;
            

            var totalCnt = 0;
            var returnValue = appointmentIndicatorFailureCodeBo.ListAppointmentIndicatorFailureCode(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region AppointmentIndicatorFailureCode Create

        [AuthorizationFilter(CommonValues.PermissionCodes.AppointmentIndicatorFailureCode.AppointmentIndicatorFailureCodeIndex, CommonValues.PermissionCodes.AppointmentIndicatorFailureCode.AppointmentIndicatorFailureCodeCreate)]
        public ActionResult AppointmentIndicatorFailureCodeCreate()
        {
            SetDefaults();

            var model = new AppointmentIndicatorFailureCodeViewModel();
            model.IsActive = true;

            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.AppointmentIndicatorFailureCode.AppointmentIndicatorFailureCodeIndex, CommonValues.PermissionCodes.AppointmentIndicatorFailureCode.AppointmentIndicatorFailureCodeCreate)]
        [HttpPost]
        public ActionResult AppointmentIndicatorFailureCodeCreate(AppointmentIndicatorFailureCodeViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            var appointmentIndicatorFailureCodeBo = new AppointmentIndicatorFailureCodeBL();

            AppointmentIndicatorFailureCodeViewModel viewControlModel = new AppointmentIndicatorFailureCodeViewModel
                {
                    Code = viewModel.Code
                };

            appointmentIndicatorFailureCodeBo.GetAppointmentIndicatorFailureCode(UserManager.UserInfo, viewControlModel);

            SetDefaults();

            if (viewControlModel.CreateDate == null)
            {
                if (ModelState.IsValid)
                {
                    viewModel.CommandType = CommonValues.DMLType.Insert;
  
                    appointmentIndicatorFailureCodeBo.DMLAppointmentIndicatorFailureCode(UserManager.UserInfo, viewModel);

                    CheckErrorForMessage(viewModel, true);

                    ModelState.Clear();
                }

                SetDefaults();
                return View();
            }
            else
            {
                SetMessage(MessageResource.CustomerDiscount_Error_DataHave, CommonValues.MessageSeverity.Fail);

                return View(viewModel);
            }
        }

        #endregion

        #region AppointmentIndicatorFailureCode Update
        [AuthorizationFilter(CommonValues.PermissionCodes.AppointmentIndicatorFailureCode.AppointmentIndicatorFailureCodeIndex, CommonValues.PermissionCodes.AppointmentIndicatorFailureCode.AppointmentIndicatorFailureCodeUpdate)]
        [HttpGet]
        public ActionResult AppointmentIndicatorFailureCodeUpdate(string id)
        {
            SetDefaults();
            var v = new AppointmentIndicatorFailureCodeViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                var appointmentIndicatorFailureCodeBo = new AppointmentIndicatorFailureCodeBL();
                v.Code = id;
                appointmentIndicatorFailureCodeBo.GetAppointmentIndicatorFailureCode(UserManager.UserInfo, v);
            }
            SetDefaults();
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.AppointmentIndicatorFailureCode.AppointmentIndicatorFailureCodeIndex, CommonValues.PermissionCodes.AppointmentIndicatorFailureCode.AppointmentIndicatorFailureCodeUpdate)]
        [HttpPost]
        public ActionResult AppointmentIndicatorFailureCodeUpdate(AppointmentIndicatorFailureCodeViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            var appointmentIndicatorFailureCodeBo = new AppointmentIndicatorFailureCodeBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Update;

                appointmentIndicatorFailureCodeBo.DMLAppointmentIndicatorFailureCode(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }

            SetDefaults();
            return View(viewModel);
        }

        #endregion

        #region AppointmentIndicatorFailureCode Delete
        //[AuthorizationFilter(CommonValues.PermissionCodes.AppointmentIndicatorFailureCode.AppointmentIndicatorFailureCodeIndex, CommonValues.PermissionCodes.AppointmentIndicatorFailureCode.AppointmentIndicatorFailureCodeDelete)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAppointmentIndicatorFailureCode(int idAppIndFulerCode)
        {
            AppointmentIndicatorFailureCodeViewModel viewModel = new AppointmentIndicatorFailureCodeViewModel
                {
                    IdAppointmentIndicatorFailureCode = idAppIndFulerCode
                };
            //viewModel.IdDealer = UserManager.UserInfo.GetUserDealerId();

            var appointmentIndicatorFailureCodeBo = new AppointmentIndicatorFailureCodeBL();
            appointmentIndicatorFailureCodeBo.GetAppointmentIndicatorFailureCode(UserManager.UserInfo, viewModel);

            viewModel.CommandType = CommonValues.DMLType.Delete;
            appointmentIndicatorFailureCodeBo.DMLAppointmentIndicatorFailureCode(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                viewModel.ErrorMessage);
        }
        #endregion

        #region Excel Upload

        public ActionResult ExcelSample()
        {
            var bo = new AppointmentIndicatorFailureCodeBL();
            var ms = bo.SetExcelReport(null, string.Empty);
            return File(ms, CommonValues.ExcelContentType, MessageResource.AppointmentIndicatorFailureCode_PageTitle_Index + CommonValues.ExcelExtOld);
        }
        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.AppointmentIndicatorFailureCode.AppointmentIndicatorFailureCodeIndex, CommonValues.PermissionCodes.AppointmentIndicatorFailureCode.AppointmentIndicatorFailureCodeIndex)]
        public ActionResult AppointmentIndicatorFailureCodeIndex(AppointmentIndicatorFailureCodeListModel listModel, HttpPostedFileBase excelFile)
        {
            AppointmentIndicatorFailureCodeViewModel model = new AppointmentIndicatorFailureCodeViewModel();
            SetDefaults();
            if (ModelState.IsValid)
            {
                if (excelFile != null)
                {
                    var bo = new AppointmentIndicatorFailureCodeBL();
                    Stream s = excelFile.InputStream;
                    List<AppointmentIndicatorFailureCodeViewModel> modelList = bo.ParseExcel(UserManager.UserInfo, model, s).Data;
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
                        foreach (var row in modelList)
                        {
                            bo.DMLAppointmentIndicatorFailureCode(UserManager.UserInfo, row);
                            if (row.ErrorNo > 0)
                            {
                                SetMessage(row.ErrorMessage, CommonValues.MessageSeverity.Fail);
                                return View(listModel);
                            }
                        }
                        SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);
                    }
                }
                else
                {
                    SetMessage(MessageResource.Global_Warning_ExcelNotSelected, CommonValues.MessageSeverity.Fail);
                }
                ModelState.Clear();
                AppointmentIndicatorFailureCodeListModel vModel = new AppointmentIndicatorFailureCodeListModel();
                return View(vModel);
            }
            return View(listModel);
        }

        #endregion
    }
}
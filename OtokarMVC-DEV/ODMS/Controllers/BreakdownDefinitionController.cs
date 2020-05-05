using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.BreakdownDefinition;
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
    public class BreakdownDefinitionController : ControllerBase
    {
        private void SetDefaults()
        {
            ViewBag.StatusList = CommonBL.ListStatus().Data;
        }

        [HttpGet]
        public JsonResult ListPdiBreakdownCode()
        {
            List<SelectListItem> breakdowCodeList = BreakdownDefinitionBL.ListPdiBreakdownCodeAsSelectListItem().Data;
            breakdowCodeList.Insert(0, new SelectListItem() { Text = MessageResource.Global_Display_Choose, Value = String.Empty });

            return Json(breakdowCodeList, JsonRequestBehavior.AllowGet);
        }

        #region BreakdownDefinition Index

        [AuthorizationFilter(CommonValues.PermissionCodes.BreakdownDefinition.BreakdownDefinitionIndex)]
        [HttpGet]
        public ActionResult BreakdownDefinitionIndex()
        {
            SetDefaults();
            BreakdownDefinitionListModel model = new BreakdownDefinitionListModel();

            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.BreakdownDefinition.BreakdownDefinitionIndex, CommonValues.PermissionCodes.BreakdownDefinition.BreakdownDefinitionIndex)]
        public ActionResult ListBreakdownDefinition([DataSourceRequest] DataSourceRequest request, BreakdownDefinitionListModel model)
        {
            var appointmentIndicatorFailureCodeBo = new BreakdownDefinitionBL();

            var v = new BreakdownDefinitionListModel(request);

            v.PdiBreakdownCode = model.PdiBreakdownCode;
            v.IsActive = model.IsActive;
            v.AdminDesc = model.AdminDesc;

            var totalCnt = 0;
            var returnValue = appointmentIndicatorFailureCodeBo.ListBreakdownDefinition(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion


        #region BreakdownDefinition

        [AuthorizationFilter(
            CommonValues.PermissionCodes.BreakdownDefinition.BreakdownDefinitionIndex,
            CommonValues.PermissionCodes.BreakdownDefinition.BreakdownDefinitionDetails)]
        [HttpGet]
        public ActionResult BreakdownDefinitionDetails(string id)
        {
            BreakdownDefinitionBL breakdownDefinitionBL = new BreakdownDefinitionBL();
            BreakdownDefinitionViewModel model_breakdownDefinition = new BreakdownDefinitionViewModel();
            model_breakdownDefinition.PdiBreakdownCode = id;

            breakdownDefinitionBL.GetBreakdownDefinition(UserManager.UserInfo, model_breakdownDefinition);

            return View(model_breakdownDefinition);
        }

        #endregion

        #region BreakdownDefinition Create

        [AuthorizationFilter(CommonValues.PermissionCodes.BreakdownDefinition.BreakdownDefinitionIndex, CommonValues.PermissionCodes.BreakdownDefinition.BreakdownDefinitionCreate)]
        public ActionResult BreakdownDefinitionCreate()
        {
            SetDefaults();

            var model = new BreakdownDefinitionViewModel();
            model.IsActive = true;

            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.BreakdownDefinition.BreakdownDefinitionIndex, CommonValues.PermissionCodes.BreakdownDefinition.BreakdownDefinitionCreate)]
        [HttpPost]
        public ActionResult BreakdownDefinitionCreate(BreakdownDefinitionViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            var appointmentIndicatorFailureCodeBo = new BreakdownDefinitionBL();

            BreakdownDefinitionViewModel viewControlModel = new BreakdownDefinitionViewModel
            {
                PdiBreakdownCode = viewModel.PdiBreakdownCode
            };

            appointmentIndicatorFailureCodeBo.GetBreakdownDefinition(UserManager.UserInfo, viewControlModel);

            SetDefaults();

            if (viewControlModel.AdminDesc == null)
            {
                if (ModelState.IsValid)
                {
                    viewModel.CommandType = CommonValues.DMLType.Insert;

                    appointmentIndicatorFailureCodeBo.DMLBreakdownDefinition(UserManager.UserInfo, viewModel);

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

        #region BreakdownDefinition Update
        [AuthorizationFilter(CommonValues.PermissionCodes.BreakdownDefinition.BreakdownDefinitionIndex, CommonValues.PermissionCodes.BreakdownDefinition.BreakdownDefinitionUpdate)]
        [HttpGet]
        public ActionResult BreakdownDefinitionUpdate(string id)
        {
            SetDefaults();
            var v = new BreakdownDefinitionViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                var appointmentIndicatorFailureCodeBo = new BreakdownDefinitionBL();
                v.PdiBreakdownCode = id;
                appointmentIndicatorFailureCodeBo.GetBreakdownDefinition(UserManager.UserInfo, v);
            }
            SetDefaults();
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.BreakdownDefinition.BreakdownDefinitionIndex, CommonValues.PermissionCodes.BreakdownDefinition.BreakdownDefinitionUpdate)]
        [HttpPost]
        public ActionResult BreakdownDefinitionUpdate(BreakdownDefinitionViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            var appointmentIndicatorFailureCodeBo = new BreakdownDefinitionBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Update;

                appointmentIndicatorFailureCodeBo.DMLBreakdownDefinition(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }

            SetDefaults();
            return View(viewModel);
        }

        #endregion

        #region BreakdownDefinition Delete
        //[AuthorizationFilter(CommonValues.PermissionCodes.BreakdownDefinition.BreakdownDefinitionIndex, CommonValues.PermissionCodes.BreakdownDefinition.BreakdownDefinitionDelete)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBreakdownDefinition(string pdiBreakdownCode)
        {
            BreakdownDefinitionViewModel viewModel = new BreakdownDefinitionViewModel
            {
                PdiBreakdownCode = pdiBreakdownCode
            };

            var appointmentIndicatorFailureCodeBo = new BreakdownDefinitionBL();
            appointmentIndicatorFailureCodeBo.GetBreakdownDefinition(UserManager.UserInfo, viewModel);

            viewModel.CommandType = CommonValues.DMLType.Delete;
            appointmentIndicatorFailureCodeBo.DMLBreakdownDefinition(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
        }
        #endregion

        #region Excel Upload

        public ActionResult ExcelSample()
        {
            var bo = new BreakdownDefinitionBL();
            var ms = bo.SetExcelReport(null, string.Empty);
            return File(ms, CommonValues.ExcelContentType, MessageResource.BreakdownDefinition_PageTitle_Index + CommonValues.ExcelExtOld);
        }
        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.BreakdownDefinition.BreakdownDefinitionIndex, CommonValues.PermissionCodes.BreakdownDefinition.BreakdownDefinitionIndex)]
        public ActionResult BreakdownDefinitionIndex(BreakdownDefinitionListModel listModel, HttpPostedFileBase excelFile)
        {
            BreakdownDefinitionViewModel model = new BreakdownDefinitionViewModel();
            SetDefaults();
            if (ModelState.IsValid)
            {
                if (excelFile != null)
                {
                    var bo = new BreakdownDefinitionBL();
                    Stream s = excelFile.InputStream;
                    List<BreakdownDefinitionViewModel> modelList = bo.ParseExcel(UserManager.UserInfo, model, s).Data;
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
                            bo.DMLBreakdownDefinition(UserManager.UserInfo, row);
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
                BreakdownDefinitionListModel vModel = new BreakdownDefinitionListModel();
                return View(vModel);
            }
            return View(listModel);
        }

        #endregion
    }
}
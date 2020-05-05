using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.DownloadFileActionResult;
using ODMSModel.PDIPartDefinition;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class PDIPartDefinitionController : ControllerBase
    {

        private void SetDefaults()
        {
            ViewBag.StatusList = CommonBL.ListStatusBool().Data;
        }

        [HttpGet]
        public JsonResult ListPartCode()
        {
            List<SelectListItem> partCodeList = PDIPartDefinitionBL.ListPartCodeAsSelectListItem().Data;
            partCodeList.Insert(0, new SelectListItem() { Text = MessageResource.Global_Display_Choose, Value = String.Empty });

            return Json(partCodeList, JsonRequestBehavior.AllowGet);
        }

        #region PDIPartDefinition Index

        [AuthorizationFilter(CommonValues.PermissionCodes.PDIPartDefinition.PDIPartDefinitionIndex)]
        [HttpGet]
        public ActionResult PDIPartDefinitionIndex()
        {
            SetDefaults();
            PDIPartDefinitionListModel model = new PDIPartDefinitionListModel();

            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PDIPartDefinition.PDIPartDefinitionIndex, CommonValues.PermissionCodes.PDIPartDefinition.PDIPartDefinitionIndex)]
        public ActionResult ListPDIPartDefinition([DataSourceRequest] DataSourceRequest request, PDIPartDefinitionListModel model)
        {
            var appointmentIndicatorFailureCodeBo = new PDIPartDefinitionBL();

            var v = new PDIPartDefinitionListModel(request)
            {
                PdiPartCode = model.PdiPartCode,
                IsActive = model.IsActive,
                AdminDesc = model.AdminDesc
            };

            var totalCnt = 0;
            var returnValue = appointmentIndicatorFailureCodeBo.ListPDIPartDefinition(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region PDIPartDefinition Create

        [AuthorizationFilter(CommonValues.PermissionCodes.PDIPartDefinition.PDIPartDefinitionIndex, CommonValues.PermissionCodes.PDIPartDefinition.PDIPartDefinitionCreate)]
        public ActionResult PDIPartDefinitionCreate()
        {
            SetDefaults();

            var model = new PDIPartDefinitionViewModel();
            model.IsActive = true;
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PDIPartDefinition.PDIPartDefinitionIndex, CommonValues.PermissionCodes.PDIPartDefinition.PDIPartDefinitionCreate)]
        [HttpPost]
        public ActionResult PDIPartDefinitionCreate(PDIPartDefinitionViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            var appointmentIndicatorFailureCodeBo = new PDIPartDefinitionBL();

            var viewControlModel = new PDIPartDefinitionViewModel
            {
                PdiPartCode = viewModel.PdiPartCode
            };

            appointmentIndicatorFailureCodeBo.GetPDIPartDefinition(UserManager.UserInfo, viewControlModel);

            SetDefaults();

            if (viewControlModel.AdminDesc == null)
            {
                if (ModelState.IsValid)
                {
                    viewModel.CommandType = CommonValues.DMLType.Insert;

                    appointmentIndicatorFailureCodeBo.DMLPDIPartDefinition(UserManager.UserInfo, viewModel);

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

        #region PDIPartDefinition Update

        [AuthorizationFilter(CommonValues.PermissionCodes.PDIPartDefinition.PDIPartDefinitionIndex, CommonValues.PermissionCodes.PDIPartDefinition.PDIPartDefinitionUpdate)]
        [HttpGet]
        public ActionResult PDIPartDefinitionUpdate(string id)
        {
            SetDefaults();
            var v = new PDIPartDefinitionViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                var appointmentIndicatorFailureCodeBo = new PDIPartDefinitionBL();
                v.PdiPartCode = id;
                appointmentIndicatorFailureCodeBo.GetPDIPartDefinition(UserManager.UserInfo, v);
            }
            SetDefaults();
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PDIPartDefinition.PDIPartDefinitionIndex, CommonValues.PermissionCodes.PDIPartDefinition.PDIPartDefinitionUpdate)]
        [HttpPost]
        public ActionResult PDIPartDefinitionUpdate(PDIPartDefinitionViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            var appointmentIndicatorFailureCodeBo = new PDIPartDefinitionBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Update;

                appointmentIndicatorFailureCodeBo.DMLPDIPartDefinition(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);

                if (viewModel.ErrorNo == 0)
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            }

            SetDefaults();
            return View(viewModel);
        }

        #endregion

        #region PDIPartDefinitionDetails

        [AuthorizationFilter(
            CommonValues.PermissionCodes.PDIPartDefinition.PDIPartDefinitionIndex,
            CommonValues.PermissionCodes.PDIPartDefinition.PDIPartDefinitionDetails)]
        [HttpGet]
        public ActionResult PDIPartDefinitionDetails(string pdiPartCode)
        {
            PDIPartDefinitionBL appointmentIndicatorMainCategoryBL = new PDIPartDefinitionBL();
            PDIPartDefinitionViewModel model_PDIPartDefinition = new PDIPartDefinitionViewModel { PdiPartCode = pdiPartCode };

            appointmentIndicatorMainCategoryBL.GetPDIPartDefinition(UserManager.UserInfo, model_PDIPartDefinition);

            return View(model_PDIPartDefinition);
        }

        #endregion

        #region PDIPartDefinition Delete
        [AuthorizationFilter(CommonValues.PermissionCodes.PDIPartDefinition.PDIPartDefinitionIndex, CommonValues.PermissionCodes.PDIPartDefinition.PDIPartDefinitionDelete)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePDIPartDefinition(string pdiPartCode)
        {
            PDIPartDefinitionViewModel viewModel = new PDIPartDefinitionViewModel { PdiPartCode = pdiPartCode };

            var appointmentIndicatorFailureCodeBo = new PDIPartDefinitionBL();

            viewModel.CommandType = CommonValues.DMLType.Delete;
            appointmentIndicatorFailureCodeBo.DMLPDIPartDefinition(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
        }
        #endregion

        #region Excel Upload

        public ActionResult ExcelSample()
        {
            var bo = new PDIPartDefinitionBL();
            var ms = bo.SetExcelReport(null, string.Empty);
            return File(ms, CommonValues.ExcelContentType, MessageResource.PDIPartDefinition_PageTitle_Index + CommonValues.ExcelExtOld);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.PDIPartDefinition.PDIPartDefinitionIndex, CommonValues.PermissionCodes.PDIPartDefinition.PDIPartDefinitionIndex)]
        public ActionResult PDIPartDefinitionIndex(PDIPartDefinitionListModel listModel, HttpPostedFileBase excelFile)
        {
            PDIPartDefinitionViewModel model = new PDIPartDefinitionViewModel();
            SetDefaults();
            if (ModelState.IsValid)
            {
                if (excelFile != null)
                {
                    var bo = new PDIPartDefinitionBL();
                    Stream s = excelFile.InputStream;
                    List<PDIPartDefinitionViewModel> modelList = bo.ParseExcel(UserManager.UserInfo, model, s).Data;
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

                        return View(listModel);
                    }
                    else
                    {
                        foreach (var row in modelList)
                        {
                            bo.DMLPDIPartDefinition(UserManager.UserInfo, row);
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
                PDIPartDefinitionListModel vModel = new PDIPartDefinitionListModel();
                return View(vModel);
            }
            return View(listModel);
        }

        #endregion
    }

}
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.DownloadFileActionResult;
using ODMSModel.PDIControlPartDefinition;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class PDIControlPartDefinitionController : ControllerBase
    {
        private void SetDefaults()
        {
            ViewBag.ControlModelCodeList = PDIControlDefinitionBL.ListPDIControlModelCodeAsSelectListItem().Data;
            ViewBag.PartList = PDIPartDefinitionBL.ListPartCodeAsSelectListItem().Data;
            ViewBag.ActiveYesNoList = CommonBL.ListStatusBool().Data;
        }

        #region PDIControlPartDefinition Index

        [AuthorizationFilter(CommonValues.PermissionCodes.PDIControlPartDefinition.PDIControlPartDefinitionIndex)]
        [HttpGet]
        public ActionResult PDIControlPartDefinitionIndex()
        {
            SetDefaults();
            PDIControlPartDefinitionListModel model = new PDIControlPartDefinitionListModel();

            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PDIControlPartDefinition.PDIControlPartDefinitionIndex, CommonValues.PermissionCodes.PDIControlPartDefinition.PDIControlPartDefinitionIndex)]
        public ActionResult ListPDIControlPartDefinition([DataSourceRequest] DataSourceRequest request, PDIControlPartDefinitionListModel model)
        {
            var pdiControlDefinitionBo = new PDIControlPartDefinitionBL();

            var v = new PDIControlPartDefinitionListModel(request)
            {
                IdPDIControlDefinition = model.IdPDIControlDefinition,
                IsActive = model.IsActive,
                PDIPartCode = model.PDIPartCode
            };

            var totalCnt = 0;
            var returnValue = pdiControlDefinitionBo.ListPDIControlPartDefinition(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region PDIControlPartDefinitionDetails

        [AuthorizationFilter(CommonValues.PermissionCodes.PDIControlPartDefinition.PDIControlPartDefinitionIndex, CommonValues.PermissionCodes.PDIControlPartDefinition.PDIControlPartDefinitionDetails)]
        [HttpGet]
        public ActionResult PDIControlPartDefinitionDetails(int id, string partCode)
        {
            PDIControlPartDefinitionBL appointmentIndicatorMainCategoryBL = new PDIControlPartDefinitionBL();
            PDIControlPartDefinitionViewModel model_PDIControlPartDefinition = new PDIControlPartDefinitionViewModel();
            model_PDIControlPartDefinition.IdPDIControlDefinition = id;
            model_PDIControlPartDefinition.PDIPartCode = partCode;

            appointmentIndicatorMainCategoryBL.GetPDIControlPartDefinition(UserManager.UserInfo, model_PDIControlPartDefinition);

            return View(model_PDIControlPartDefinition);
        }

        #endregion

        #region PDIControlPartDefinition Create

        [AuthorizationFilter(CommonValues.PermissionCodes.PDIControlPartDefinition.PDIControlPartDefinitionIndex, CommonValues.PermissionCodes.PDIControlPartDefinition.PDIControlPartDefinitionCreate)]
        public ActionResult PDIControlPartDefinitionCreate()
        {
            SetDefaults();

            var model = new PDIControlPartDefinitionViewModel();
            model.IsActive = true;
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PDIControlPartDefinition.PDIControlPartDefinitionIndex,
            CommonValues.PermissionCodes.PDIControlPartDefinition.PDIControlPartDefinitionCreate)]
        [HttpPost]
        public ActionResult PDIControlPartDefinitionCreate(PDIControlPartDefinitionViewModel viewModel,
                                                       IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();

            if (ModelState.IsValid)
            {
                var pdiControlDefinitionBo = new PDIControlPartDefinitionBL();

                PDIControlPartDefinitionViewModel viewControlModel = new PDIControlPartDefinitionViewModel
                {
                    IdPDIControlDefinition = viewModel.IdPDIControlDefinition,
                    PDIPartCode = viewModel.PDIPartCode
                };

                pdiControlDefinitionBo.GetPDIControlPartDefinition(UserManager.UserInfo, viewControlModel);

                if (viewControlModel.PDIModelCode == null)
                {
                    viewModel.CommandType = CommonValues.DMLType.Insert;

                    pdiControlDefinitionBo.DMLPDIControlPartDefinition(UserManager.UserInfo, viewModel);

                    CheckErrorForMessage(viewModel, true);

                    ModelState.Clear();
                    return View();
                }
                else
                {
                    SetMessage(MessageResource.CustomerDiscount_Error_DataHave, CommonValues.MessageSeverity.Fail);
                }
            }

            return View(viewModel);
        }

        #endregion

        #region PDIControlPartDefinition Update
        [
        AuthorizationFilter(CommonValues.PermissionCodes.PDIControlPartDefinition.PDIControlPartDefinitionIndex, CommonValues.PermissionCodes.PDIControlPartDefinition.PDIControlPartDefinitionUpdate)]
        [HttpGet]
        public ActionResult PDIControlPartDefinitionUpdate(int id, string partCode)
        {
            SetDefaults();
            var v = new PDIControlPartDefinitionViewModel();
            if (id != 0 && partCode != null)
            {
                var pdiControlDefinitionBo = new PDIControlPartDefinitionBL();
                v.IdPDIControlDefinition = id.GetValue<int>();
                v.PDIPartCode = partCode;
                pdiControlDefinitionBo.GetPDIControlPartDefinition(UserManager.UserInfo, v);
            }
            SetDefaults();
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PDIControlPartDefinition.PDIControlPartDefinitionIndex,
            CommonValues.PermissionCodes.PDIControlPartDefinition.PDIControlPartDefinitionUpdate)]
        [HttpPost]
        public ActionResult PDIControlPartDefinitionUpdate(PDIControlPartDefinitionViewModel viewModel,
                                                           IEnumerable<HttpPostedFileBase> attachments)
        {
            if (ModelState.IsValid)
            {
                var pdiControlDefinitionBo = new PDIControlPartDefinitionBL();
                viewModel.CommandType = CommonValues.DMLType.Update;

                pdiControlDefinitionBo.DMLPDIControlPartDefinition(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }

            SetDefaults();
            return View(viewModel);
        }

        #endregion

        #region PDIControlPartDefinition Delete

        [AuthorizationFilter(CommonValues.PermissionCodes.PDIControlPartDefinition.PDIControlPartDefinitionIndex, CommonValues.PermissionCodes.PDIControlPartDefinition.PDIControlPartDefinitionDelete)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePDIControlPartDefinition(int id, string partCode)
        {
            PDIControlPartDefinitionViewModel viewModel = new PDIControlPartDefinitionViewModel
            {
                IdPDIControlDefinition = id,
                PDIPartCode = partCode
            };

            var pdiControlDefinitionBo = new PDIControlPartDefinitionBL();
            pdiControlDefinitionBo.GetPDIControlPartDefinition(UserManager.UserInfo, viewModel);

            viewModel.CommandType = CommonValues.DMLType.Delete;
            pdiControlDefinitionBo.DMLPDIControlPartDefinition(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);

        }

        #endregion

        #region Excel Upload

        public ActionResult ExcelSample()
        {
            var bo = new PDIControlPartDefinitionBL();
            var ms = bo.SetExcelReport(null, string.Empty);
            return File(ms, CommonValues.ExcelContentType, MessageResource.PDIControlPartDefinition_PageTitle_Index + CommonValues.ExcelExtOld);
        }
        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.PDIControlPartDefinition.PDIControlPartDefinitionIndex, CommonValues.PermissionCodes.PDIControlPartDefinition.PDIControlPartDefinitionIndex)]
        public ActionResult PDIControlPartDefinitionIndex(PDIControlPartDefinitionListModel listModel, HttpPostedFileBase excelFile)
        {
            PDIControlPartDefinitionViewModel model = new PDIControlPartDefinitionViewModel();
            SetDefaults();
            if (ModelState.IsValid)
            {
                if (excelFile != null)
                {
                    var bo = new PDIControlPartDefinitionBL();
                    Stream s = excelFile.InputStream;
                    List<PDIControlPartDefinitionViewModel> modelList = bo.ParseExcel(UserManager.UserInfo, model, s).Data;
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
                            bo.DMLPDIControlPartDefinition(UserManager.UserInfo, row);
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
                PDIControlPartDefinitionListModel vModel = new PDIControlPartDefinitionListModel();
                return View(vModel);
            }
            return View(listModel);
        }

        #endregion
    }
}
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.DownloadFileActionResult;
using ODMSModel.PDIResultDefinition;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class PDIResultDefinitionController : ControllerBase
    {
        private void SetDefaults()
        {
            ViewBag.ActiveYesNoList = CommonBL.ListStatusBool().Data;
        }

        [HttpGet]
        public JsonResult ListCode()
        {
            List<SelectListItem> codeList = PDIResultDefinitionBL.ListPDIResultCodeAsSelectListItem().Data;
            codeList.Insert(0, new SelectListItem() { Text = MessageResource.Global_Display_Choose, Value = "0" });

            return Json(codeList, JsonRequestBehavior.AllowGet);
        }

        #region PDIResultDefinition Index

        [AuthorizationFilter(CommonValues.PermissionCodes.PDIResultDefinition.PDIResultDefinitionIndex)]
        [HttpGet]
        public ActionResult PDIResultDefinitionIndex()
        {
            SetDefaults();
            PDIResultDefinitionListModel model = new PDIResultDefinitionListModel();

            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PDIResultDefinition.PDIResultDefinitionIndex, CommonValues.PermissionCodes.PDIResultDefinition.PDIResultDefinitionIndex)]
        public ActionResult ListPDIResultDefinition([DataSourceRequest] DataSourceRequest request, PDIResultDefinitionListModel model)
        {
            var pdiResultDefinitionBo = new PDIResultDefinitionBL();

            var v = new PDIResultDefinitionListModel(request)
            {
                AdminDesc = model.AdminDesc,
                IsActive = model.IsActive,
                PDIResultCode = model.PDIResultCode
            };

            var totalCnt = 0;
            var returnValue = pdiResultDefinitionBo.ListPDIResultDefinition(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region PDIResultDefinitionDetails

        [AuthorizationFilter(
            CommonValues.PermissionCodes.PDIResultDefinition.PDIResultDefinitionIndex,
            CommonValues.PermissionCodes.PDIResultDefinition.PDIResultDefinitionDetails)]
        [HttpGet]
        public ActionResult PDIResultDefinitionDetails(string resultcode)
        {
            PDIResultDefinitionBL appointmentIndicatorMainCategoryBL = new PDIResultDefinitionBL();
            PDIResultDefinitionViewModel model_PDIResultDefinition = new PDIResultDefinitionViewModel();
            model_PDIResultDefinition.PDIResultCode = resultcode;

            appointmentIndicatorMainCategoryBL.GetPDIResultDefinition(UserManager.UserInfo, model_PDIResultDefinition);

            return View(model_PDIResultDefinition);
        }

        #endregion

        #region PDIResultDefinition Create

        [AuthorizationFilter(CommonValues.PermissionCodes.PDIResultDefinition.PDIResultDefinitionIndex, CommonValues.PermissionCodes.PDIResultDefinition.PDIResultDefinitionCreate)]
        public ActionResult PDIResultDefinitionCreate()
        {
            SetDefaults();

            var model = new PDIResultDefinitionViewModel();
            model.IsActive = true;
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PDIResultDefinition.PDIResultDefinitionIndex,
            CommonValues.PermissionCodes.PDIResultDefinition.PDIResultDefinitionCreate)]
        [HttpPost]
        public ActionResult PDIResultDefinitionCreate(PDIResultDefinitionViewModel viewModel,
                                                      IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();

            if (ModelState.IsValid)
            {
                var pdiResultDefinitionBo = new PDIResultDefinitionBL();

                PDIResultDefinitionViewModel viewResultModel = new PDIResultDefinitionViewModel
                {
                    PDIResultCode = viewModel.PDIResultCode
                };

                pdiResultDefinitionBo.GetPDIResultDefinition(UserManager.UserInfo, viewResultModel);


                if (viewResultModel.AdminDesc == null)
                {
                    viewModel.CommandType = CommonValues.DMLType.Insert;

                    pdiResultDefinitionBo.DMLPDIResultDefinition(UserManager.UserInfo, viewModel);

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

        #region PDIResultDefinition Update
        [AuthorizationFilter(CommonValues.PermissionCodes.PDIResultDefinition.PDIResultDefinitionIndex, CommonValues.PermissionCodes.PDIResultDefinition.PDIResultDefinitionUpdate)]
        [HttpGet]
        public ActionResult PDIResultDefinitionUpdate(string resultcode)
        {
            SetDefaults();
            var v = new PDIResultDefinitionViewModel();
            if (!string.IsNullOrEmpty(resultcode))
            {
                var pdiResultDefinitionBo = new PDIResultDefinitionBL();
                v.PDIResultCode = resultcode;
                pdiResultDefinitionBo.GetPDIResultDefinition(UserManager.UserInfo, v);
            }
            SetDefaults();
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PDIResultDefinition.PDIResultDefinitionIndex, CommonValues.PermissionCodes.PDIResultDefinition.PDIResultDefinitionUpdate)]
        [HttpPost]
        public ActionResult PDIResultDefinitionUpdate(PDIResultDefinitionViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            var pdiResultDefinitionBo = new PDIResultDefinitionBL();
            if (ModelState.IsValid)
            {
                PDIResultDefinitionViewModel viewResultModel = new PDIResultDefinitionViewModel
                {
                    PDIResultCode = viewModel.PDIResultCode
                };

                pdiResultDefinitionBo.GetPDIResultDefinition(UserManager.UserInfo, viewResultModel);


                if (viewResultModel.AdminDesc == null || viewModel.PDIResultCode == viewResultModel.PDIResultCode)
                {
                    viewModel.CommandType = CommonValues.DMLType.Update;

                    pdiResultDefinitionBo.DMLPDIResultDefinition(UserManager.UserInfo, viewModel);
                    CheckErrorForMessage(viewModel, true);
                }
                else
                {
                    SetMessage(MessageResource.CustomerDiscount_Error_DataHave, CommonValues.MessageSeverity.Fail);
                }
            }

            SetDefaults();
            return View(viewModel);
        }

        #endregion

        #region PDIResultDefinition Delete

        [AuthorizationFilter(CommonValues.PermissionCodes.PDIResultDefinition.PDIResultDefinitionIndex, CommonValues.PermissionCodes.PDIResultDefinition.PDIResultDefinitionDelete)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePDIResultDefinition(string resultcode)
        {
            var viewModel = new PDIResultDefinitionViewModel { PDIResultCode = resultcode };

            var pdiResultDefinitionBo = new PDIResultDefinitionBL();
            pdiResultDefinitionBo.GetPDIResultDefinition(UserManager.UserInfo, viewModel);

            viewModel.CommandType = CommonValues.DMLType.Delete;
            pdiResultDefinitionBo.DMLPDIResultDefinition(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
        }

        #endregion

        #region Excel Upload

        public ActionResult ExcelSample()
        {
            var bo = new PDIResultDefinitionBL();
            var ms = bo.SetExcelReport(null, string.Empty);
            return File(ms, CommonValues.ExcelContentType, MessageResource.PDIResultDefinition_PageTitle_Index + CommonValues.ExcelExtOld);
        }
        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.PDIResultDefinition.PDIResultDefinitionIndex, CommonValues.PermissionCodes.PDIResultDefinition.PDIResultDefinitionIndex)]
        public ActionResult PDIResultDefinitionIndex(PDIResultDefinitionListModel listModel, HttpPostedFileBase excelFile)
        {
            PDIResultDefinitionViewModel model = new PDIResultDefinitionViewModel();
            SetDefaults();
            if (ModelState.IsValid)
            {
                if (excelFile != null)
                {
                    var bo = new PDIResultDefinitionBL();
                    Stream s = excelFile.InputStream;
                    List<PDIResultDefinitionViewModel> modelList = bo.ParseExcel(UserManager.UserInfo, model, s).Data;
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
                            bo.DMLPDIResultDefinition(UserManager.UserInfo, row);
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
                PDIResultDefinitionListModel vModel = new PDIResultDefinitionListModel();
                return View(vModel);
            }
            return View(listModel);
        }

        #endregion
    }
}
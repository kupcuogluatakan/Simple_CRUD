using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.DownloadFileActionResult;
using ODMSModel.PDIControlDefinition;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class PDIControlDefinitionController : ControllerBase
    {
        private void SetDefaults()
        {
            ViewBag.ModelCodeList = VehicleModelBL.ListVehicleModelAsSelectList(UserManager.UserInfo).Data;
            ViewBag.ActiveList = CommonBL.ListStatusBool().Data;
            ViewBag.ActiveYesNoList = CommonBL.ListYesNo().Data;
        }

        [HttpGet]
        public JsonResult ListCode()
        {
            List<SelectListItem> codeList = PDIControlDefinitionBL.ListPDIControlCodeAsSelectListItem().Data;
            codeList.Insert(0, new SelectListItem() { Text = MessageResource.Global_Display_Choose, Value = "0" });

            return Json(codeList, JsonRequestBehavior.AllowGet);
        }

        #region PDIControlDefinition Index

        [AuthorizationFilter(CommonValues.PermissionCodes.PDIControlDefinition.PDIControlDefinitionIndex)]
        [HttpGet]
        public ActionResult PDIControlDefinitionIndex()
        {
            SetDefaults();
            PDIControlDefinitionListModel model = new PDIControlDefinitionListModel();

            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PDIControlDefinition.PDIControlDefinitionIndex, CommonValues.PermissionCodes.PDIControlDefinition.PDIControlDefinitionIndex)]
        public ActionResult ListPDIControlDefinition([DataSourceRequest] DataSourceRequest request, PDIControlDefinitionListModel model)
        {
            var pdiControlDefinitionBo = new PDIControlDefinitionBL();

            var v = new PDIControlDefinitionListModel(request);

            v.ModelKod = model.ModelKod;
            v.IsActive = model.IsActive;
            v.IsGroupCode = model.IsGroupCode;
            v.PDIControlCode = model.PDIControlCode;

            var totalCnt = 0;
            var returnValue = pdiControlDefinitionBo.ListPDIControlDefinition(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region PDIControlDefinitionDetails

        [AuthorizationFilter(
            CommonValues.PermissionCodes.PDIControlDefinition.PDIControlDefinitionIndex,
            CommonValues.PermissionCodes.PDIControlDefinition.PDIControlDefinitionDetails)]
        [HttpGet]
        public ActionResult PDIControlDefinitionDetails(int id = 0)
        {
            PDIControlDefinitionBL appointmentIndicatorMainCategoryBL = new PDIControlDefinitionBL();
            PDIControlDefinitionViewModel model_PDIControlDefinition = new PDIControlDefinitionViewModel();
            model_PDIControlDefinition.IdPDIControlDefinition = id;

            appointmentIndicatorMainCategoryBL.GetPDIControlDefinition(UserManager.UserInfo, model_PDIControlDefinition);

            return View(model_PDIControlDefinition);
        }

        #endregion

        #region PDIControlDefinition Create

        [AuthorizationFilter(CommonValues.PermissionCodes.PDIControlDefinition.PDIControlDefinitionIndex, CommonValues.PermissionCodes.PDIControlDefinition.PDIControlDefinitionCreate)]
        public ActionResult PDIControlDefinitionCreate()
        {
            SetDefaults();

            var model = new PDIControlDefinitionViewModel();
            model.IsActive = true;
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PDIControlDefinition.PDIControlDefinitionIndex, CommonValues.PermissionCodes.PDIControlDefinition.PDIControlDefinitionCreate)]
        [HttpPost]
        public ActionResult PDIControlDefinitionCreate(PDIControlDefinitionViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();

            if (ModelState.IsValid)
            {
                if (viewModel.RowNo < 0)
                {
                    SetMessage(MessageResource.Validation_GreaterThanOrEqualToZero, CommonValues.MessageSeverity.Fail);
                    return View(viewModel);
                }
                else
                {
                    var pdiControlDefinitionBo = new PDIControlDefinitionBL();
                    /*TFs No : 27744 OYA 26.12.2014
                     teslim öncesi kontrol tanımlama ekranında sıra noya unique kontrolü eklenmiş. bu kontrol sadece kodda var. 
                     DB ye iki adet unique kontrolü eklendi. o durumların handle edilip uyarılarının düzenlenmesi gerekli.*/
                    int totalCount = 0;
                    PDIControlDefinitionListModel listModel = new PDIControlDefinitionListModel();
                    listModel.ModelKod = viewModel.ModelKod;

                    List<PDIControlDefinitionListModel> list = pdiControlDefinitionBo.ListPDIControlDefinition(UserManager.UserInfo, listModel, out totalCount).Data;

                    var modelCodeControlCodeControl = (from e in list.AsEnumerable()
                                                       where
                                                           e.IdPDIControlDefinition !=
                                                           viewModel.IdPDIControlDefinition
                                                           && e.ModelKod == viewModel.ModelKod
                                                           && e.PDIControlCode == viewModel.PDIControlCode
                                                       select e);
                    if (modelCodeControlCodeControl.Any())
                    {
                        SetMessage(MessageResource.PDIControlDefitinition_Warning_SameModelSameControlExists, CommonValues.MessageSeverity.Fail);
                        return View(viewModel);
                    }
                    var modelCodeRowNoDuplicateControl = (from e in list.AsEnumerable()
                                                          where
                                                              e.IdPDIControlDefinition !=
                                                              viewModel.IdPDIControlDefinition
                                                              && e.ModelKod == viewModel.ModelKod
                                                              && e.RowNo == viewModel.RowNo
                                                          select e);
                    if (modelCodeRowNoDuplicateControl.Any())
                    {
                        SetMessage(MessageResource.PDIControlDefinition_Warning_SameModelSameRowNoExists, CommonValues.MessageSeverity.Fail);
                        return View(viewModel);
                    }

                    PDIControlDefinitionViewModel viewControlModel = new PDIControlDefinitionViewModel
                    {
                        PDIControlCode = viewModel.PDIControlCode,
                        ModelKod = viewModel.ModelKod
                    };

                    pdiControlDefinitionBo.GetPDIControlDefinition(UserManager.UserInfo, viewControlModel);

                    if (viewControlModel.IdPDIControlDefinition == 0)
                    {
                        viewModel.CommandType = CommonValues.DMLType.Insert;

                        pdiControlDefinitionBo.DMLPDIControlDefinition(UserManager.UserInfo, viewModel);

                        CheckErrorForMessage(viewModel, true);

                        var model = new PDIControlDefinitionViewModel();
                        model.IsActive = true;
                        return View(model);
                    }
                    else
                    {
                        SetMessage(MessageResource.PDIControl_Error_DataExists, CommonValues.MessageSeverity.Fail);
                    }
                }
            }

            return View(viewModel);
        }

        #endregion

        #region PDIControlDefinition Update
        [
        AuthorizationFilter(CommonValues.PermissionCodes.PDIControlDefinition.PDIControlDefinitionIndex, CommonValues.PermissionCodes.PDIControlDefinition.PDIControlDefinitionUpdate)]
        [HttpGet]
        public ActionResult PDIControlDefinitionUpdate(int? id)
        {
            SetDefaults();
            var v = new PDIControlDefinitionViewModel();
            if (id != null && id != 0)
            {
                var pdiControlDefinitionBo = new PDIControlDefinitionBL();
                v.IdPDIControlDefinition = id.GetValue<int>();
                pdiControlDefinitionBo.GetPDIControlDefinition(UserManager.UserInfo, v);
            }
            SetDefaults();
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PDIControlDefinition.PDIControlDefinitionIndex, CommonValues.PermissionCodes.PDIControlDefinition.PDIControlDefinitionUpdate)]
        [HttpPost]
        public ActionResult PDIControlDefinitionUpdate(PDIControlDefinitionViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            if (ModelState.IsValid)
            {
                if (viewModel.RowNo < 0)
                {
                    SetMessage(MessageResource.Validation_GreaterThanOrEqualToZero, CommonValues.MessageSeverity.Fail);
                    return View(viewModel);
                }
                else
                {

                    var pdiControlDefinitionBo = new PDIControlDefinitionBL();
                    /*TFs No : 27744 OYA 26.12.2014
                         teslim öncesi kontrol tanımlama ekranında sıra noya unique kontrolü eklenmiş. bu kontrol sadece kodda var. 
                         DB ye iki adet unique kontrolü eklendi. o durumların handle edilip uyarılarının düzenlenmesi gerekli.*/
                    int totalCount = 0;
                    PDIControlDefinitionListModel listModel = new PDIControlDefinitionListModel();
                    listModel.ModelKod = viewModel.ModelKod;

                    List<PDIControlDefinitionListModel> list = pdiControlDefinitionBo.ListPDIControlDefinition(UserManager.UserInfo, listModel, out totalCount).Data;

                    var modelCodeControlCodeControl = (from e in list.AsEnumerable()
                                                       where
                                                           e.IdPDIControlDefinition !=
                                                           viewModel.IdPDIControlDefinition
                                                           && e.ModelKod == viewModel.ModelKod
                                                           && e.PDIControlCode == viewModel.PDIControlCode
                                                       select e);
                    if (modelCodeControlCodeControl.Any())
                    {
                        SetMessage(MessageResource.PDIControlDefitinition_Warning_SameModelSameControlExists, CommonValues.MessageSeverity.Fail);
                        return View(viewModel);
                    }
                    var modelCodeRowNoDuplicateControl = (from e in list.AsEnumerable()
                                                          where
                                                              e.IdPDIControlDefinition !=
                                                              viewModel.IdPDIControlDefinition
                                                              && e.ModelKod == viewModel.ModelKod
                                                              && e.RowNo == viewModel.RowNo
                                                          select e);
                    if (modelCodeRowNoDuplicateControl.Any())
                    {
                        SetMessage(MessageResource.PDIControlDefinition_Warning_SameModelSameRowNoExists, CommonValues.MessageSeverity.Fail);
                        return View(viewModel);
                    }

                    PDIControlDefinitionViewModel viewControlModel = new PDIControlDefinitionViewModel
                    {
                        PDIControlCode = viewModel.PDIControlCode,
                        ModelKod = viewModel.ModelKod
                    };

                    pdiControlDefinitionBo.GetPDIControlDefinition(UserManager.UserInfo, viewControlModel);

                    if (viewControlModel.IdPDIControlDefinition == 0 ||
                        viewControlModel.IdPDIControlDefinition == viewModel.IdPDIControlDefinition)
                    {
                        viewModel.CommandType = CommonValues.DMLType.Update;

                        pdiControlDefinitionBo.DMLPDIControlDefinition(UserManager.UserInfo, viewModel);
                        CheckErrorForMessage(viewModel, true);

                        if (viewModel.ErrorNo == 0)
                            SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);
                    }
                    else
                    {
                        SetMessage(MessageResource.PDIControl_Error_DataExists, CommonValues.MessageSeverity.Fail);
                    }
                }
            }

            return View(viewModel);
        }

        #endregion

        #region PDIControlDefinition Delete
        [AuthorizationFilter(CommonValues.PermissionCodes.PDIControlDefinition.PDIControlDefinitionIndex, CommonValues.PermissionCodes.PDIControlDefinition.PDIControlDefinitionDelete)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePDIControlDefinition(int id)
        {
            PDIControlDefinitionViewModel viewModel = new PDIControlDefinitionViewModel { IdPDIControlDefinition = id };

            var pdiControlDefinitionBo = new PDIControlDefinitionBL();
            pdiControlDefinitionBo.GetPDIControlDefinition(UserManager.UserInfo, viewModel);

            viewModel.CommandType = CommonValues.DMLType.Delete;
            pdiControlDefinitionBo.DMLPDIControlDefinition(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
        }
        #endregion

        #region Excel Upload

        public ActionResult ExcelSample()
        {
            var bo = new PDIControlDefinitionBL();
            var ms = bo.SetExcelReport(null, string.Empty);
            return File(ms, CommonValues.ExcelContentType, MessageResource.PDIControlDefinition_PageTitle_Index + CommonValues.ExcelExtOld);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.PDIControlDefinition.PDIControlDefinitionIndex, CommonValues.PermissionCodes.PDIControlDefinition.PDIControlDefinitionIndex)]
        public ActionResult PDIControlDefinitionIndex(PDIControlDefinitionListModel listModel, HttpPostedFileBase excelFile)
        {
            PDIControlDefinitionViewModel model = new PDIControlDefinitionViewModel();
            SetDefaults();
            if (ModelState.IsValid)
            {
                if (excelFile != null)
                {
                    var bo = new PDIControlDefinitionBL();
                    Stream s = excelFile.InputStream;
                    List<PDIControlDefinitionViewModel> modelList = bo.ParseExcel(UserManager.UserInfo, model, s).Data;
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
                            bo.DMLPDIControlDefinition(UserManager.UserInfo, row);
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
                PDIControlDefinitionListModel vModel = new PDIControlDefinitionListModel();
                return View(vModel);
            }
            return View(listModel);
        }

        #endregion
    }
}
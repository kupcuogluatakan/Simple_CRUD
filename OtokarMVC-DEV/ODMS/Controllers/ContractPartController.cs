using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSBusiness.Business;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel;
using ODMSModel.Common;
using ODMSModel.ContractPart;
using ODMSModel.DownloadFileActionResult;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class ContractPartController : ControllerBase
    {
        private void SetDefaults()
        {
            //Status List
            List<SelectListItem> statusList = CommonBL.ListStatus().Data;
            ViewBag.StatusList = statusList;

            //List<SelectListItem> languageList = LanguageBL.ListLanguageAsSelectListItem(UserManager.UserInfo).Data;
            //ViewBag.LanguageList = languageList;

        }

        #region Contract Part Index

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Contract.ContractIndex)]
        [HttpGet]
        public ActionResult ContractPartIndex(int? idContract)
        {
            ContractPartListModel model = new ContractPartListModel { IdContract = idContract };
            SetDefaults();
            return PartialView(model);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Contract.ContractIndex, ODMSCommon.CommonValues.PermissionCodes.Contract.ContractDetails)]
        public ActionResult ListContractPart([DataSourceRequest] DataSourceRequest request, ContractPartListModel model)
        {
            var contractPartBo = new ContractPartBL();
            var v = new ContractPartListModel(request);
            var totalCnt = 0;
            v.IdContractPart = model.IdContractPart;
            v.IdContract = model.IdContract;
            v.IdPart = model.IdPart;
            var returnValue = contractPartBo.ListContractPart(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region Contract Part Create
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Contract.ContractIndex, ODMSCommon.CommonValues.PermissionCodes.Contract.ContractCreate)]
        [HttpGet]
        public ActionResult ContractPartCreate(int? idContract)
        {
            ContractPartViewModel model = new ContractPartViewModel();
            model.IdContract = idContract;
            SetDefaults();
            return View(model);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Contract.ContractIndex, ODMSCommon.CommonValues.PermissionCodes.Contract.ContractCreate)]
        [HttpPost]
        public ActionResult ContractPartCreate(ContractPartViewModel viewModel)
        {
            SetDefaults();
            var contractPartBo = new ContractPartBL();

            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Insert;
                contractPartBo.DMLContractPart(UserManager.UserInfo, viewModel);

                CheckErrorForMessage(viewModel, true);

                ModelState.Clear();

            }
            return View(viewModel);
        }

        #endregion

        #region Contract Part Update

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Contract.ContractIndex, ODMSCommon.CommonValues.PermissionCodes.ContractPart.ContractPartUpdate)]
        [HttpGet]
        public ActionResult ContractPartUpdate(int? idContractPart)
        {
            SetDefaults();
            var v = new ContractPartViewModel();
            if (idContractPart != null && idContractPart > 0)
            {
                var contractPartBo = new ContractPartBL();
                v.IdContractPart = idContractPart;
                contractPartBo.GetContractPart(UserManager.UserInfo, v);
            }
            return View(v);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Contract.ContractIndex, ODMSCommon.CommonValues.PermissionCodes.ContractPart.ContractPartUpdate)]
        [HttpPost]
        public ActionResult ContractPartUpdate(ContractPartViewModel viewModel)
        {
            SetDefaults();
            var campaignDocumentBo = new ContractPartBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Update;
                campaignDocumentBo.DMLContractPart(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }
            return View(viewModel);
        }

        #endregion

        #region Contract Part Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Contract.ContractIndex, ODMSCommon.CommonValues.PermissionCodes.Contract.ContractDelete)]
        public ActionResult DeleteContractPart(int? idContractPart)
        {
            ContractPartViewModel viewModel = new ContractPartViewModel() { IdContractPart = idContractPart };
            var campaignDocumentBo = new ContractPartBL();
            viewModel.CommandType = (viewModel.IdContractPart != null && viewModel.IdContractPart > 0) ? CommonValues.DMLType.Delete : string.Empty;
            campaignDocumentBo.DMLContractPart(UserManager.UserInfo, viewModel);

            ModelState.Clear();

            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
        }
        #endregion

        #region Contract Part Details
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Contract.ContractIndex, ODMSCommon.CommonValues.PermissionCodes.Contract.ContractDetails)]
        [HttpGet]
        public ActionResult ContractPartDetails(int? idContractPart)
        {
            var v = new ContractPartViewModel();
            var campaignDocumentBo = new ContractPartBL();

            v.IdContractPart = idContractPart;
            campaignDocumentBo.GetContractPart(UserManager.UserInfo, v);

            return View(v);
        }

        #endregion

        #region Excel Upload

        public ActionResult ExcelSample()
        {
            var bo = new ContractPartBL();
            var ms = bo.SetExcelReport(null, string.Empty);
            return File(ms, CommonValues.ExcelContentType, "Sözleşme Detay Girişleri" + CommonValues.ExcelExtOld);
            //return File(ms, CommonValues.ExcelContentType, MessageResource.LabourDuration_PageTitle_Index + CommonValues.ExcelExtOld);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.LabourDuration.LabourDurationIndex)]
        public ActionResult ContractPartIndex(ContractPartListModel listModel, HttpPostedFileBase contractPartExcelFile)
        {
            SetDefaults();
            ContractPartViewModel model = new ContractPartViewModel();

            if (ModelState.IsValid)
            {
                if (contractPartExcelFile != null)
                {
                    var bo = new ContractPartBL();
                    Stream s = contractPartExcelFile.InputStream;
                    List<ContractPartViewModel> modelList = bo.ParseExcel(UserManager.UserInfo, model, s).Data;
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

                        return RedirectToAction("ContractIndex", "Contract");
                    }
                    else
                    {
                        int totalCount = 0;
                        foreach (var row in modelList)
                        {
                            ContractPartListModel controlModel = new ContractPartListModel
                            {
                                PartCode = row.PartCode.GetValue<string>(),
                                IdContract = row.IdContract.GetValue<int>(),
                                IdPart = row.IdPart.GetValue<int>()
                            };
                            List<ContractPartListModel> list = bo.ListContractPart(UserManager.UserInfo, controlModel, out totalCount).Data;
                            if (totalCount > 0)
                            {
                                row.CommandType = CommonValues.DMLType.Update;
                            }
                            else
                            {
                                row.CommandType = CommonValues.DMLType.Insert;
                            }
                            bo.DMLContractPart(UserManager.UserInfo, row);
                            if (row.ErrorNo > 0)
                            {
                                SetMessage(row.ErrorMessage, CommonValues.MessageSeverity.Fail);
                                return RedirectToAction("ContractIndex", "Contract");
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
            }
            return RedirectToAction("ContractIndex", "Contract");
        }
        #endregion

        [HttpGet]
        public ActionResult ContractPartImage(int id)
        {
            DocumentBL docBl = new DocumentBL();
            DocumentInfo docInfo = docBl.GetDocumentById(id.GetValue<int>()).Model;
            return File(docInfo.DocBinary, docInfo.DocMimeType, docInfo.DocName);
        }
    }
}

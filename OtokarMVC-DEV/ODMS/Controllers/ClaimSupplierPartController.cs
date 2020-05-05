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
using ODMSModel.ClaimRecallPeriod;
using ODMSModel.ClaimSupplier;
using ODMSModel.ClaimSupplierPart;
using ODMSModel.DownloadFileActionResult;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class ClaimSupplierPartController : ControllerBase
    {
        private void SetDefaults()
        {
        }

        #region ClaimSupplierPart Index

        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimSupplierPart.ClaimSupplierPartIndex)]
        [System.Web.Http.HttpGet]
        public ActionResult ClaimSupplierPartIndex(string supplierCode)
        {
            var model = new ClaimSupplierPartListModel { SupplierCode = supplierCode };
            SetDefaults();
            return PartialView(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimSupplierPart.ClaimSupplierPartIndex)]
        public ActionResult ListClaimSupplierPart([DataSourceRequest] DataSourceRequest request,
                                                  ClaimSupplierPartListModel model)
        {
            var claimSupplierBo = new ClaimSupplierPartBL();
            var v = new ClaimSupplierPartListModel(request);
            var totalCnt = 0;
            v.SupplierCode = model.SupplierCode;
            var returnValue = claimSupplierBo.ListClaimSupplierParts(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
                {
                    Data = returnValue,
                    Total = totalCnt
                });
        }

        #endregion

        #region Create
        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimSupplierPart.ClaimSupplierPartIndex, CommonValues.PermissionCodes.ClaimSupplierPart.ClaimSupplierPartCreate)]
        [System.Web.Http.HttpGet]
        public ActionResult ClaimSupplierPartCreate(string supplierCode)
        {
            var model = new ClaimSupplierPartViewModel { SupplierCode = supplierCode };
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimSupplierPart.ClaimSupplierPartIndex, CommonValues.PermissionCodes.ClaimSupplierPart.ClaimSupplierPartCreate)]
        [System.Web.Http.HttpPost]
        public ActionResult CreateClaimSupplierPart(ClaimSupplierPartViewModel model)
        {
            if (ModelState.IsValid)
            {
                var bo = new ClaimSupplierPartBL();
                model.CommandType = CommonValues.DMLType.Insert;
                bo.DMLClaimSupplierPart(UserManager.UserInfo, model);
                CheckErrorForMessage(model, true);
                ModelState.Clear();
                return View("ClaimSupplierPartCreate", new ClaimSupplierPartViewModel());
            }
            return View("ClaimSupplierPartCreate", model);
        }

        #endregion


        #region Update

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimSupplierPart.ClaimSupplierPartIndex, CommonValues.PermissionCodes.ClaimSupplierPart.ClaimSupplierPartUpdate)]
        public ActionResult UpdateClaimSupplierPart(ClaimSupplierPartViewModel model)
        {
            if (ModelState.IsValid)
            {
                var bo = new ClaimSupplierPartBL();
                model.CommandType = CommonValues.DMLType.Insert;
                bo.DMLClaimSupplierPart(UserManager.UserInfo, model);
                CheckErrorForMessage(model, true);
                ModelState.Clear();
                return RedirectToAction("ClaimSupplierPartUpdate", new { partId = model.PartId, supplierCode = model.SupplierCode });
            }
            return View("ClaimSupplierPartUpdate", model);

        }

        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.ClaimRecallPeriodPart.ClaimRecallPeriodPartIndex, ODMSCommon.CommonValues.PermissionCodes.ClaimRecallPeriodPart.ClaimRecallPeriodPartUpdate)]
        public ActionResult ClaimSupplierPartUpdate(long partId, string supplierCode)
        {

            return View(new ClaimSupplierPartBL().GetClaimSupplierPart(UserManager.UserInfo, partId, supplierCode).Model);
        }

        #endregion
        #region Delete
        [HttpPost]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.ClaimRecallPeriodPart.ClaimRecallPeriodPartIndex, ODMSCommon.CommonValues.PermissionCodes.ClaimRecallPeriodPart.ClaimRecallPeriodPartDelete)]
        public ActionResult DeleteClaimSupplierPart(ClaimSupplierPartViewModel model)
        {
            var bo = new ClaimSupplierPartBL();
            model.CommandType = CommonValues.DMLType.Delete;
            bo.DMLClaimSupplierPart(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);
            ModelState.Clear();
            if (model.ErrorNo > 0)
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
            }
            return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
        }

        #endregion


        #region Excel Upload

        public ActionResult ExcelSample()
        {
            var bo = new ClaimSupplierPartBL();
            var ms = bo.SetExcelReport(null, string.Empty);
            return File(ms, CommonValues.ExcelContentType, MessageResource.ClaimSupplierPart_PageTitle_Index + CommonValues.ExcelExtOld);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimSupplierPart.ClaimSupplierPartIndex)]
        public ActionResult ClaimSupplierPartIndex(ClaimSupplierPartListModel listModel, HttpPostedFileBase supplierExcelFile)
        {
            SetDefaults();

            ClaimSupplierPartViewModel model = new ClaimSupplierPartViewModel();

            if (ModelState.IsValid)
            {
                if (supplierExcelFile != null)
                {
                    var bo = new ClaimSupplierPartBL();
                    Stream s = supplierExcelFile.InputStream;
                    List<ClaimSupplierPartViewModel> modelList = bo.ParseExcel(UserManager.UserInfo, model, s).Data;
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
                    }
                    else
                    {
                        bo.DeleteAllClaimSupplierPart(listModel.SupplierCode);
                        foreach (var row in modelList)
                        {
                            row.CommandType = CommonValues.DMLType.Insert;
                            row.ClaimRecallPeriodId = listModel.ClaimRecallPeriodId;
                            bo.DMLClaimSupplierPart(UserManager.UserInfo, row);
                            if (row.ErrorNo > 0)
                            {
                                SetMessage(row.ErrorMessage, CommonValues.MessageSeverity.Fail);
                                return RedirectToAction("ClaimSupplierIndex", "ClaimSupplier",
                                    new { supplierCode = listModel.SupplierCode });
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
            return RedirectToAction("ClaimSupplierIndex", "ClaimSupplier",
                                    new { supplierCode = listModel.SupplierCode });
        }

        #endregion
    }
}
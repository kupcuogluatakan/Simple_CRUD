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
using ODMSModel.ClaimRecallPeriodPart;
using ODMSModel.DownloadFileActionResult;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class ClaimRecallPeriodPartController : ControllerBase
    {
        private void SetDefaults()
        {
        }

        #region ClaimRecallPeriodPart Index

        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimRecallPeriodPart.ClaimRecallPeriodPartIndex)]
        [HttpGet]
        public ActionResult ClaimRecallPeriodPartIndex()
        {
            var periodBo = new ClaimRecallPeriodBL();
            var periodModel = new ClaimRecallPeriodViewModel();
            periodBo.GetClaimRecallPeriod(UserManager.UserInfo, periodModel);
            ViewBag.PeriodStatus = periodModel.IsActive;

            var model = new ClaimRecallPeriodPartListModel();

            SetDefaults();
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimRecallPeriodPart.ClaimRecallPeriodPartIndex)]
        public ActionResult ListClaimRecallPeriodPart([DataSourceRequest] DataSourceRequest request,
                                                      ClaimRecallPeriodPartListModel model)
        {
            var claimRecallPeriodBo = new ClaimRecallPeriodPartBL();
            var v = new ClaimRecallPeriodPartListModel(request)
            {
                SearchIsActive = model.SearchIsActive,
                PartCode = model.PartCode,
                PartName = model.PartName
            };
            var totalCnt = 0;
            var returnValue = claimRecallPeriodBo.ListClaimRecallPeriodParts(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
                {
                    Data = returnValue,
                    Total = totalCnt
                });
        }

        #endregion

        #region Create
        [HttpPost]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.ClaimRecallPeriodPart.ClaimRecallPeriodPartIndex, ODMSCommon.CommonValues.PermissionCodes.ClaimRecallPeriodPart.ClaimRecallPeriodPartCreate)]
        public ActionResult ClaimRecallPeriodPartCreate(ClaimRecallPeriodPartViewModel model)
        {
            if (ModelState.IsValid)
            {
                var bo = new ClaimRecallPeriodPartBL();
                model.CommandType = CommonValues.DMLType.Insert;
                bo.DMLClaimRecallPeriodPart(UserManager.UserInfo, model);
                CheckErrorForMessage(model, true);
                ModelState.Clear();
                return View(new ClaimRecallPeriodPartViewModel());
            }
            return View(model);
        }

        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.ClaimRecallPeriodPart.ClaimRecallPeriodPartIndex, ODMSCommon.CommonValues.PermissionCodes.ClaimRecallPeriodPart.ClaimRecallPeriodPartCreate)]
        public ActionResult ClaimRecallPeriodPartCreate()
        {
            return View(new ClaimRecallPeriodPartViewModel());
        }

        #endregion

        #region Update

        [HttpPost]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.ClaimRecallPeriodPart.ClaimRecallPeriodPartIndex, ODMSCommon.CommonValues.PermissionCodes.ClaimRecallPeriodPart.ClaimRecallPeriodPartUpdate)]
        public ActionResult ClaimRecallPeriodPartUpdate(ClaimRecallPeriodPartViewModel model)
        {
            if (ModelState.IsValid)
            {
                var bo = new ClaimRecallPeriodPartBL();
                model.CommandType = CommonValues.DMLType.Update;
                bo.DMLClaimRecallPeriodPart(UserManager.UserInfo, model);
                CheckErrorForMessage(model, true);
                ModelState.Clear();
                return RedirectToAction("ClaimRecallPeriodPartUpdate", new { id = model.PartId });
            }
            return View(model);
        }

        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.ClaimRecallPeriodPart.ClaimRecallPeriodPartIndex, ODMSCommon.CommonValues.PermissionCodes.ClaimRecallPeriodPart.ClaimRecallPeriodPartUpdate)]
        public ActionResult ClaimRecallPeriodPartUpdate(int id)
        {

            return View(new ClaimRecallPeriodPartBL().GetClaimRecallPeriodPart(UserManager.UserInfo, id).Model);
        }

        #endregion

        #region Delete
        [HttpPost]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.ClaimRecallPeriodPart.ClaimRecallPeriodPartIndex, ODMSCommon.CommonValues.PermissionCodes.ClaimRecallPeriodPart.ClaimRecallPeriodPartUpdate)]
        public ActionResult ClaimRecallPeriodPartDelete(int id)
        {

            var bo = new ClaimRecallPeriodPartBL();
            var model = new ClaimRecallPeriodPartViewModel() { PartId = id };
            model.CommandType = CommonValues.DMLType.Delete;
            bo.DMLClaimRecallPeriodPart(UserManager.UserInfo, model);
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
            var bo = new ClaimRecallPeriodPartBL();
            var ms = bo.SetExcelReport(null, string.Empty);
            return File(ms, CommonValues.ExcelContentType, MessageResource.ClaimRecallPeriodPart_PageTitle_Index + CommonValues.ExcelExtOld);
        }
        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimRecallPeriodPart.ClaimRecallPeriodPartIndex)]
        public ActionResult ClaimRecallPeriodPartIndex(ClaimRecallPeriodPartListModel listModel, HttpPostedFileBase partExcelFile)
        {
            SetDefaults();

            ClaimRecallPeriodPartViewModel model = new ClaimRecallPeriodPartViewModel();

            if (ModelState.IsValid)
            {
                if (partExcelFile != null)
                {
                    var bo = new ClaimRecallPeriodPartBL();
                    Stream s = partExcelFile.InputStream;
                    List<ClaimRecallPeriodPartViewModel> modelList = bo.ParseExcel(UserManager.UserInfo, model, s).Data;
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
                        //bo.DeleteAllClaimRecallPeriodPart(listModel.ClaimRecallPeriodId);
                        foreach (var row in modelList)
                        {
                            row.CommandType = CommonValues.DMLType.Insert;
                            bo.DMLClaimRecallPeriodPart(UserManager.UserInfo, row);
                            if (row.ErrorNo > 0)
                            {
                                SetMessage(row.ErrorMessage, CommonValues.MessageSeverity.Fail);
                                return RedirectToAction("ClaimRecallPeriodPartIndex", "ClaimRecallPeriodPart");
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
            return RedirectToAction("ClaimRecallPeriodPartIndex", "ClaimRecallPeriodPart");
        }

        #endregion
    }
}
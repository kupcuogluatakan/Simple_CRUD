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
using ODMSModel.DealerStartupInventoryLevel;
using ODMSModel.DownloadFileActionResult;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class DealerStartupInventoryLevelController : ControllerBase
    {
        readonly DealerBL _dealerBl = new DealerBL();

        private void SetDefaults()
        {
            ViewBag.DealerClassList = _dealerBl.ListDealerClassesAsSelectListItem().Data;
            ViewBag.StatusList = CommonBL.ListStatus().Data;
        }

        public ActionResult ExcelSample()
        {
            var bo = new DealerStartupInventoryLevelBL();
            var ms = bo.SetExcelReport(null, string.Empty);
            return File(ms, CommonValues.ExcelContentType, MessageResource.DealerStartupInventoryLevel_PageTitle_Index + CommonValues.ExcelExtOld);
        }

        #region DealerInventoryStartupLevel Index
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerStartupInventoryLevel.DealerStartupInventoryLevelIndex)]
        public ActionResult DealerStartupInventoryLevelIndex()
        {
            SetDefaults();
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.DealerStartupInventoryLevel.DealerStartupInventoryLevelIndex, CommonValues.PermissionCodes.DealerStartupInventoryLevel.DealerStartupInventoryLevelDetails)]
        public ActionResult ListDealerStartupInventoryLevels([DataSourceRequest]DataSourceRequest request, DealerStartupInventoryLevelListModel model)
        {
            SetDefaults();
            var bo = new DealerStartupInventoryLevelBL();
            var referenceModel = new DealerStartupInventoryLevelListModel(request)
            {
                DealerClassCode = model.DealerClassCode,
                PartCode = model.PartCode,
                PartName = model.PartName,
                IsActive = model.IsActive
            };
            int totalCnt;
            var returnValue = bo.ListDealerStartupInventoryLevels(UserManager.UserInfo, referenceModel, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }
        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerStartupInventoryLevel.DealerStartupInventoryLevelIndex, CommonValues.PermissionCodes.DealerStartupInventoryLevel.DealerStartupInventoryLevelCreate)]
        public ActionResult DealerStartupInventoryLevelIndex(DealerStartupInventoryLevelListModel listModel, HttpPostedFileBase excelFile)
        {
            DealerStartupInventoryLevelViewModel model = new DealerStartupInventoryLevelViewModel();
            SetDefaults();
            if (ModelState.IsValid)
            {
                if (excelFile != null)
                {
                    var bo = new DealerStartupInventoryLevelBL();
                    Stream s = excelFile.InputStream;
                    List<DealerStartupInventoryLevelViewModel> modelList = bo.ParseExcel(UserManager.UserInfo, model, s).Data;

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
                            DealerStartupInventoryLevelViewModel inserted = new DealerStartupInventoryLevelViewModel
                            {
                                DealerClassCode = row.DealerClassCode,
                                PartId = row.PartId
                            };
                            bo.GetDealerStartupInventoryLevel(inserted);
                            row.CommandType = inserted.DealerClassName == null ? CommonValues.DMLType.Insert : CommonValues.DMLType.Update;
                            bo.DMLDealerStartupInventoryLevel(UserManager.UserInfo, row);
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
                return View();
            }
            return View(listModel);
        }
        #endregion

        #region DealerInventoryStartupLevel Create
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerStartupInventoryLevel.DealerStartupInventoryLevelIndex, CommonValues.PermissionCodes.DealerStartupInventoryLevel.DealerStartupInventoryLevelCreate)]
        public ActionResult DealerStartupInventoryLevelCreate()
        {
            SetDefaults();
            DealerStartupInventoryLevelViewModel model = new DealerStartupInventoryLevelViewModel();
            model.IsActive = true;
            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerStartupInventoryLevel.DealerStartupInventoryLevelIndex, CommonValues.PermissionCodes.DealerStartupInventoryLevel.DealerStartupInventoryLevelCreate)]
        public ActionResult DealerStartupInventoryLevelCreate(DealerStartupInventoryLevelViewModel model)
        {
            SetDefaults();
            if (ModelState.IsValid)
            {
                decimal quantity = model.Quantity.GetValue<decimal>();
                var bo = new DealerStartupInventoryLevelBL();
                DealerStartupInventoryLevelViewModel existedModel = bo.GetDealerStartupInventoryLevel(model).Model;
                // mevcut kayıt varsa bilgileri güncellenip aktif yapılıyor.
                if (existedModel.PartName != null)
                {
                    existedModel.Quantity = quantity;
                    existedModel.IsActive = true;
                    existedModel.CommandType = CommonValues.DMLType.Update;
                    bo.DMLDealerStartupInventoryLevel(UserManager.UserInfo, existedModel);
                    CheckErrorForMessage(existedModel, true);
                }
                else
                {
                    model.CommandType = CommonValues.DMLType.Insert;
                    bo.DMLDealerStartupInventoryLevel(UserManager.UserInfo, model);
                    CheckErrorForMessage(model, true);
                }
                ModelState.Clear();
                return RedirectToAction("DealerStartupInventoryLevelCreate");
            }
            return View(model);
        }
        #endregion

        #region #region DealerInventoryStartupLevel Update
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerStartupInventoryLevel.DealerStartupInventoryLevelIndex, CommonValues.PermissionCodes.DealerStartupInventoryLevel.DealerStartupInventoryLevelUpdate)]
        public ActionResult DealerStartupInventoryLevelUpdate(string dealerClassCode, int partId = 0)
        {
            SetDefaults();
            var referenceModel = new DealerStartupInventoryLevelViewModel();
            if (!String.IsNullOrEmpty(dealerClassCode) && partId > 0)
            {
                var bo = new DealerStartupInventoryLevelBL();
                referenceModel.DealerClassCode = dealerClassCode;
                referenceModel.PartId = partId;
                referenceModel = bo.GetDealerStartupInventoryLevel(referenceModel).Model;
            }
            return View(referenceModel);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerStartupInventoryLevel.DealerStartupInventoryLevelIndex, CommonValues.PermissionCodes.DealerStartupInventoryLevel.DealerStartupInventoryLevelUpdate)]
        public ActionResult DealerStartupInventoryLevelUpdate(DealerStartupInventoryLevelViewModel viewModel)
        {
            SetDefaults();
            var bo = new DealerStartupInventoryLevelBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Update;
                bo.DMLDealerStartupInventoryLevel(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }
            return View(viewModel);
        }
        #endregion

        #region DealerInventoryStartupLevel Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerStartupInventoryLevel.DealerStartupInventoryLevelIndex, CommonValues.PermissionCodes.DealerStartupInventoryLevel.DealerStartupInventoryLevelDelete)]
        public ActionResult DealerStartupInventoryLevelDelete(DealerStartupInventoryLevelViewModel model)
        {
            ViewBag.HideElements = false;

            var bo = new DealerStartupInventoryLevelBL();
            model.CommandType = CommonValues.DMLType.Delete;
            bo.DMLDealerStartupInventoryLevel(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);
            ModelState.Clear();

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }
        #endregion

        #region DealerInventoryStartupLevel Details
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerStartupInventoryLevel.DealerStartupInventoryLevelIndex, CommonValues.PermissionCodes.DealerStartupInventoryLevel.DealerStartupInventoryLevelDetails)]
        public ActionResult DealerStartupInventoryLevelDetails(int dealerClassId = 0, int partId = 0)
        {
            var referenceModel = new DealerStartupInventoryLevelViewModel { DealerClassId = dealerClassId, PartId = partId };
            var bo = new DealerStartupInventoryLevelBL();

            var model = bo.GetDealerStartupInventoryLevel(referenceModel).Model;

            return View(model);
        }
        #endregion
    }
}

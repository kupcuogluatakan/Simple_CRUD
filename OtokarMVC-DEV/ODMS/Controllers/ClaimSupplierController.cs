using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.ClaimSupplier;
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
    public class ClaimSupplierController : ControllerBase
    {

        #region ClaimSupplier Index

        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimSupplier.ClaimSupplierIndex)]
        [HttpGet]
        public ActionResult ClaimSupplierIndex()
        {
            ClaimSupplierListModel model = new ClaimSupplierListModel();

            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimSupplier.ClaimSupplierIndex, CommonValues.PermissionCodes.ClaimSupplier.ClaimSupplierIndex)]
        public ActionResult ListClaimSupplier([DataSourceRequest] DataSourceRequest request, ClaimSupplierListModel model)
        {
            var claimSupplierBo = new ClaimSupplierBL();

            var v = new ClaimSupplierListModel(request)
            {
                SupplierCode = model.SupplierCode,
                SupplierName = model.SupplierName
            };

            var totalCnt = 0;
            var returnValue = claimSupplierBo.ListClaimSupplier(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region ClaimSupplier Create

        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimSupplier.ClaimSupplierIndex, CommonValues.PermissionCodes.ClaimSupplier.ClaimSupplierCreate)]
        public ActionResult ClaimSupplierCreate()
        {
            var model = new ClaimSupplierViewModel();
            model.IsActive = true;
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimSupplier.ClaimSupplierIndex, CommonValues.PermissionCodes.ClaimSupplier.ClaimSupplierCreate)]
        [HttpPost]
        public ActionResult ClaimSupplierCreate(ClaimSupplierViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            var claimSupplierBo = new ClaimSupplierBL();

            ClaimSupplierViewModel viewControlModel = new ClaimSupplierViewModel();
            viewControlModel.SupplierCode = viewModel.SupplierCode;

            claimSupplierBo.GetClaimSupplier(UserManager.UserInfo, viewControlModel);


            if (viewControlModel.SupplierName == null)
            {
                if (ModelState.IsValid)
                {
                    viewModel.CommandType = CommonValues.DMLType.Insert;

                    claimSupplierBo.DMLClaimSupplier(UserManager.UserInfo, viewModel);

                    CheckErrorForMessage(viewModel, true);

                    ModelState.Clear();
                }

                return View();
            }
            else
            {
                SetMessage(MessageResource.CustomerDiscount_Error_DataHave, CommonValues.MessageSeverity.Fail);

                return View(viewModel);
            }
        }

        #endregion

        #region ClaimSupplier Update
        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimSupplier.ClaimSupplierIndex, CommonValues.PermissionCodes.ClaimSupplier.ClaimSupplierUpdate)]
        [HttpGet]
        public ActionResult ClaimSupplierUpdate(string supplierCode)
        {
            var v = new ClaimSupplierViewModel();
            if (supplierCode != "")
            {
                var claimSupplierBo = new ClaimSupplierBL();
                v.SupplierCode = supplierCode;

                claimSupplierBo.GetClaimSupplier(UserManager.UserInfo, v);
            }
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimSupplier.ClaimSupplierIndex, CommonValues.PermissionCodes.ClaimSupplier.ClaimSupplierUpdate)]
        [HttpPost]
        public ActionResult ClaimSupplierUpdate(ClaimSupplierViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            var claimSupplierBo = new ClaimSupplierBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Update;

                claimSupplierBo.DMLClaimSupplier(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }

            return View(viewModel);
        }

        #endregion

        #region ClaimSupplier Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimSupplier.ClaimSupplierIndex, CommonValues.PermissionCodes.ClaimSupplier.ClaimSupplierDelete)]
        public ActionResult DeleteClaimSupplier(string SupplierCode)
        {
            ClaimSupplierViewModel viewModel = new ClaimSupplierViewModel { SupplierCode = SupplierCode };

            var claimSupplierBo = new ClaimSupplierBL();

            viewModel.CommandType = CommonValues.DMLType.Delete;
            claimSupplierBo.DMLClaimSupplier(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                viewModel.ErrorMessage);
        }
        #endregion

        #region Excel Upload

        public ActionResult ExcelSample()
        {
            var bo = new ClaimSupplierBL();
            var ms = bo.SetExcelReport(null, string.Empty);
            return File(ms, CommonValues.ExcelContentType, MessageResource.ClaimSupplier_PageTitle_Index + CommonValues.ExcelExtOld);
        }
        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimSupplier.ClaimSupplierIndex, CommonValues.PermissionCodes.ClaimSupplier.ClaimSupplierExcel)]
        public ActionResult ClaimSupplierIndex(ClaimSupplierListModel listModel, HttpPostedFileBase excelFile)
        {

            ClaimSupplierViewModel model = new ClaimSupplierViewModel();

            if (ModelState.IsValid)
            {
                if (excelFile != null)
                {
                    var bo = new ClaimSupplierBL();
                    Stream s = excelFile.InputStream;
                    List<ClaimSupplierViewModel> modelList = bo.ParseExcel(UserManager.UserInfo, model, s).Data;
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
                            ClaimSupplierViewModel inserted = new ClaimSupplierViewModel
                            {
                                SupplierCode = row.SupplierCode
                            };

                            bo.GetClaimSupplier(UserManager.UserInfo, inserted);
                            if (inserted.SupplierName != null)
                            {
                                row.CommandType = CommonValues.DMLType.Update;

                                bo.DMLClaimSupplier(UserManager.UserInfo, row);
                            }
                            else
                            {
                                row.CommandType = CommonValues.DMLType.Insert;

                                bo.DMLClaimSupplier(UserManager.UserInfo, row);
                            }
                            if (row.ErrorNo > 0)
                            {
                                SetMessage(row.ErrorMessage, CommonValues.MessageSeverity.Fail);
                                return View();
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
                ClaimSupplierListModel claimSupplierListModel = new ClaimSupplierListModel();
                return View(claimSupplierListModel);
            }
            return View(listModel);
        }

        #endregion
    }
}
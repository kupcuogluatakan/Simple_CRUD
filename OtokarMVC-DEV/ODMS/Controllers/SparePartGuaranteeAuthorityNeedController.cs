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
using ODMSCommon.Security;
using ODMSModel.DownloadFileActionResult;
using ODMSModel.SparePartGuaranteeAuthorityNeed;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class SparePartGuaranteeAuthorityNeedController : ControllerBase
    {
        private void SetDefaults()
        {
            ViewBag.SLStatus = CommonBL.ListYesNoValueIntWithAll(1).Data;//Setting selected 'yes'
        }

        #region Spare Part Guarantee Authority Need Index

        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartGuaranteeAuthorityNeed.SparePartGuaranteeAuthorityNeedIndex)]
        [HttpGet]
        public ActionResult SparePartGuaranteeAuthorityNeedIndex()
        {
            SetDefaults();
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartGuaranteeAuthorityNeed.SparePartGuaranteeAuthorityNeedIndex)]
        public ActionResult ListSparePartGuaranteeAuthorityNeed([DataSourceRequest] DataSourceRequest request, SparePartGuaranteeAuthorityNeedListModel model)
        {
            var sparePartGuaranteeAuthorityNeedBo = new SparePartGuaranteeAuthorityNeedBL();
            var v = new SparePartGuaranteeAuthorityNeedListModel(request);
            var totalCnt = 0;
            v.PartCode = model.PartCode;
            v.GuaranteeAuthorityNeedSearch = model.GuaranteeAuthorityNeedSearch;
            var returnValue = sparePartGuaranteeAuthorityNeedBo.ListSparePartGuaranteeAuthorityNeeds(UserManager.UserInfo,v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }
        #endregion
        
        #region Spare Part Guarantee Authority Need Delete
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartGuaranteeAuthorityNeed.SparePartGuaranteeAuthorityNeedIndex, CommonValues.PermissionCodes.SparePartGuaranteeAuthorityNeed.SparePartGuaranteeAuthorityNeedDelete)]
        public ActionResult DeleteSparePartGuaranteeAuthorityNeed(int partId)
        {
            SparePartGuaranteeAuthorityNeedViewModel viewModel = new SparePartGuaranteeAuthorityNeedViewModel() { PartId = partId};
            var sparePartGuaranteeAuthorityNeedBo = new SparePartGuaranteeAuthorityNeedBL();
            viewModel.GuaranteeAuthorityNeed = false;
            viewModel.CommandType = viewModel.PartId > 0 ? CommonValues.DMLType.Update : string.Empty;
            sparePartGuaranteeAuthorityNeedBo.DMLSparePartGuaranteeAuthorityNeed(UserManager.UserInfo,viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                viewModel.ErrorMessage);
        }
        #endregion

        #region Excel Upload

        public ActionResult ExcelSample()
        {
            var bo = new SparePartGuaranteeAuthorityNeedBL();
            var ms = bo.SetExcelReport(null, string.Empty);
            return File(ms, CommonValues.ExcelContentType, MessageResource.SparePartGuaranteeAuthorityNeed_PageTitle_Index + CommonValues.ExcelExtOld);
        }
        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartGuaranteeAuthorityNeed.SparePartGuaranteeAuthorityNeedIndex)]
        public ActionResult SparePartGuaranteeAuthorityNeedIndex(SparePartGuaranteeAuthorityNeedListModel listModel, HttpPostedFileBase excelFile)
        {
            SparePartGuaranteeAuthorityNeedViewModel model = new SparePartGuaranteeAuthorityNeedViewModel();
            SetDefaults();
            if (ModelState.IsValid)
            {
                if (excelFile != null)
                {
                    var bo = new SparePartGuaranteeAuthorityNeedBL();
                    Stream s = excelFile.InputStream;
                    List<SparePartGuaranteeAuthorityNeedViewModel> modelList = bo.ParseExcel(UserManager.UserInfo,model, s).Data;
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
                            bo.DMLSparePartGuaranteeAuthorityNeed(UserManager.UserInfo,row);
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
                SparePartGuaranteeAuthorityNeedListModel vModel = new SparePartGuaranteeAuthorityNeedListModel();
                return View(vModel);
            }
            return View(listModel);
        }

        #endregion
    }
}
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
using ODMSModel.DealerSaleChannelDiscountRatio;
using ODMSModel.DownloadFileActionResult;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class DealerSaleChannelDiscountRatioController : ControllerBase
    {
        private void SetDefaults()
        {
            ViewBag.DealerClassList = DealerClassBL.ListDealerClassesAsSelectListItem().Data;
            ViewBag.ChannelList = DealerSaleChannelBL.ListDealerSaleChannelsAsSelectListItem().Data;
        }

        public ActionResult ExcelSample()
        {
            var bo = new DealerSaleChannelDiscountRatioBL();
            var ms = bo.SetExcelReport(null, string.Empty);
            return File(ms, CommonValues.ExcelContentType, MessageResource.DealerSaleChannelDiscountRatios_PageTitle_Index + CommonValues.ExcelExtOld);
        }

        #region DealerSaleChannelDiscountRatio Index
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerSaleChannelDiscountRatio.DealerSaleChannelDiscountRatioIndex)]
        [HttpGet]
        public ActionResult DealerSaleChannelDiscountRatioIndex()
        {
            SetDefaults();
            return View(new DealerSaleChannelDiscountRatioViewModel());
        }
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerSaleChannelDiscountRatio.DealerSaleChannelDiscountRatioIndex
            , CommonValues.PermissionCodes.DealerSaleChannelDiscountRatio.DealerSaleChannelDiscountRatioDetails)]
        [HttpPost]
        public ActionResult DealerSaleChannelDiscountRatioIndex(DealerSaleChannelDiscountRatioViewModel model, HttpPostedFileBase excelFile)
        {
            SetDefaults();

            if (excelFile != null)
            {
                DealerSaleChannelDiscountRatioBL bo = new DealerSaleChannelDiscountRatioBL();
                Stream s = excelFile.InputStream;
                List<DealerSaleChannelDiscountRatioViewModel> modelList = bo.ParseExcel(UserManager.UserInfo, model, s).Data;
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
                    foreach (var row in modelList)
                    {
                        var inserted =
                            new DealerSaleChannelDiscountRatioViewModel
                            {
                                DealerClassCode = row.DealerClassCode,
                                ChannelCode = row.ChannelCode,
                                SparePartClassCode = row.SparePartClassCode
                            };
                        inserted = bo.GetDealerSaleChannelDiscountRatio(UserManager.UserInfo, inserted.DealerClassCode, inserted.ChannelCode, inserted.SparePartClassCode).Model;
                        row.CommandType = inserted.DealerClassName == null ? CommonValues.DMLType.Insert : CommonValues.DMLType.Update;
                        bo.DMLDealerSaleChannelDiscountRatio(UserManager.UserInfo, row);
                        if (inserted.ErrorNo > 0)
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
            return View();
        }
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerSaleChannelDiscountRatio.DealerSaleChannelDiscountRatioIndex, CommonValues.PermissionCodes.DealerSaleChannelDiscountRatio.DealerSaleChannelDiscountRatioDetails)]
        public ActionResult ListDealerSaleChannelDiscountRatio([DataSourceRequest]DataSourceRequest request, DealerSaleChannelDiscountRatioViewModel model)
        {
            var appointmentBo = new DealerSaleChannelDiscountRatioBL();
            var v = new DealerSaleChannelDiscountRatioListModel(request)
            {
                DealerClassCode = model.DealerClassCode,
                ChannelCode = model.ChannelCode,
                SparePartClassCode = model.SparePartClassCode
            };

            var totalCnt = 0;
            var returnValue = appointmentBo.ListDealerSaleChannelDiscountRatios(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }
        #endregion

        #region DealerSaleChannelDiscountRatio Create
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerSaleChannelDiscountRatio.DealerSaleChannelDiscountRatioIndex, CommonValues.PermissionCodes.DealerSaleChannelDiscountRatio.DealerSaleChannelDiscountRatioCreate)]
        public ActionResult DealerSaleChannelDiscountRatioCreate()
        {
            SetDefaults();
            return View(new DealerSaleChannelDiscountRatioViewModel());

        }
        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerSaleChannelDiscountRatio.DealerSaleChannelDiscountRatioIndex, CommonValues.PermissionCodes.DealerSaleChannelDiscountRatio.DealerSaleChannelDiscountRatioCreate)]
        public ActionResult DealerSaleChannelDiscountRatioCreate(DealerSaleChannelDiscountRatioViewModel model)
        {
            SetDefaults();

            if (!ModelState.IsValid)
                return View(model);

            var appointmentBo = new DealerSaleChannelDiscountRatioBL();
            model.CommandType = CommonValues.DMLType.Insert;
            appointmentBo.DMLDealerSaleChannelDiscountRatio(UserManager.UserInfo, model);

            if (CheckErrorForMessage(model, true))
                return View(model);

            ModelState.Clear();
            return View();
        }
        #endregion

        #region DealerSaleChannelDiscountRatio Update
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerSaleChannelDiscountRatio.DealerSaleChannelDiscountRatioIndex, CommonValues.PermissionCodes.DealerSaleChannelDiscountRatio.DealerSaleChannelDiscountRatioUpdate)]
        public ActionResult DealerSaleChannelDiscountRatioUpdate(string dealerClassCode, string channelCode, string sparePartClassCode)
        {
            SetDefaults();
            DealerSaleChannelDiscountRatioBL bo = new DealerSaleChannelDiscountRatioBL();
            DealerSaleChannelDiscountRatioViewModel model = bo.GetDealerSaleChannelDiscountRatio(UserManager.UserInfo, dealerClassCode, channelCode, sparePartClassCode).Model;
            return View(model);
        }
        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerSaleChannelDiscountRatio.DealerSaleChannelDiscountRatioIndex, CommonValues.PermissionCodes.DealerSaleChannelDiscountRatio.DealerSaleChannelDiscountRatioUpdate)]
        public ActionResult DealerSaleChannelDiscountRatioUpdate(DealerSaleChannelDiscountRatioViewModel model)
        {
            SetDefaults();
            if (!ModelState.IsValid)
                return View(model);
            var appointmentBo = new DealerSaleChannelDiscountRatioBL();
            model.CommandType = CommonValues.DMLType.Update;
            appointmentBo.DMLDealerSaleChannelDiscountRatio(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);
            return View(model);
        }
        #endregion

        #region DealerSaleChannelDiscountRatio Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Currency.CurrencyIndex, ODMSCommon.CommonValues.PermissionCodes.Currency.CurrencyDelete)]
        public ActionResult DeleteDealerSaleChannelDiscountRatio(string dealerClassCode, string channelCode, string sparePartClassCode)
        {
            DealerSaleChannelDiscountRatioViewModel viewModel = new DealerSaleChannelDiscountRatioViewModel
            {
                DealerClassCode = dealerClassCode,
                ChannelCode = channelCode,
                SparePartClassCode = sparePartClassCode
            };
            var currencyBo = new DealerSaleChannelDiscountRatioBL();
            viewModel.CommandType = CommonValues.DMLType.Delete;
            currencyBo.DMLDealerSaleChannelDiscountRatio(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
        }
        #endregion

        #region DealerSaleChannelDiscountRatio Details
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerSaleChannelDiscountRatio.DealerSaleChannelDiscountRatioIndex, CommonValues.PermissionCodes.DealerSaleChannelDiscountRatio.DealerSaleChannelDiscountRatioDetails)]
        public ActionResult DealerSaleChannelDiscountRatioDetails(string dealerClassCode, string channelCode, string sparePartClassCode)
        {
            SetDefaults();
            DealerSaleChannelDiscountRatioBL bo = new DealerSaleChannelDiscountRatioBL();
            DealerSaleChannelDiscountRatioViewModel model = bo.GetDealerSaleChannelDiscountRatio(UserManager.UserInfo, dealerClassCode,
                                                                                                 channelCode,
                                                                                                 sparePartClassCode).Model;
            return View(model);
        }
        #endregion
    }
}

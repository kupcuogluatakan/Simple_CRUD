using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon.Security;
using ODMSModel.DealerGuaranteeRatio;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Resources;
using System.IO;
using ODMSModel.DownloadFileActionResult;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class DealerGuaranteeRatioController : ControllerBase
    {
        private void SetDefaults()
        {
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
        }

        [HttpGet]
        public ActionResult GetGuaranteeRatio(int idDealer)
        {
            var vModel = new DealerGuaranteeRatioIndexViewModel {IdDealer = idDealer};

            if (idDealer != 0)
            {
                new DealerGuaranteeRatioBL().GetDealerGuaranteeRatio(UserManager.UserInfo, vModel);
                return Json(vModel.GuaranteeRatio, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
        }

        #region DealerGuaranteeRatio Index

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Role.RoleTypeIndex)]
        [HttpGet]
        public ActionResult DealerGuaranteeRatioIndex()
        {
            SetDefaults();

            DealerGuaranteeRatioListModel model = new DealerGuaranteeRatioListModel();
            if (UserManager.UserInfo.GetUserDealerId() != 0)
                model.IdDealer = UserManager.UserInfo.GetUserDealerId();
            //model.CustomerName = UserManager.UserInfo.UserName;
            //model.DealerName = UserManager.UserInfo.FirstName +CommonValues.EmptySpace+ UserManager.UserInfo.LastName;
            return View(model);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.DealerGuaranteeRatio.DealerGuaranteeRatioIndex, ODMSCommon.CommonValues.PermissionCodes.DealerGuaranteeRatio.DealerGuaranteeRatioUpdate)]
        public ActionResult ListDealerGuaranteeRatio([DataSourceRequest] DataSourceRequest request, DealerGuaranteeRatioListModel model)
        {
            var dealerGuaranteeRatioBo = new DealerGuaranteeRatioBL();

            var v = new DealerGuaranteeRatioListModel(request);
            v.DealerName = model.DealerName;
            v.IdDealer = model.IdDealer;
            v.GuaranteeRatio = model.GuaranteeRatio;
            v.DealerName = model.DealerSSID;

            var totalCnt = 0;
            var returnValue = dealerGuaranteeRatioBo.ListDealerGuaranteeRatio(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region DealerGuaranteeRatio Create

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.DealerGuaranteeRatio.DealerGuaranteeRatioIndex, ODMSCommon.CommonValues.PermissionCodes.DealerGuaranteeRatio.DealerGuaranteeRatioCreate)]
        public ActionResult DealerGuaranteeRatioCreate()
        {
            SetDefaults();

            var model = new DealerGuaranteeRatioIndexViewModel();
            //if (UserManager.UserInfo.GetUserDealerId() != 0)
            //    model.IdDealer = UserManager.UserInfo.GetUserDealerId();
            return View(model);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.DealerGuaranteeRatio.DealerGuaranteeRatioIndex, ODMSCommon.CommonValues.PermissionCodes.DealerGuaranteeRatio.DealerGuaranteeRatioCreate)]
        [HttpPost]
        public ActionResult DealerGuaranteeRatioCreate(DealerGuaranteeRatioIndexViewModel viewModel)
        {
            var DealerGuaranteeRatioBo = new DealerGuaranteeRatioBL();

            DealerGuaranteeRatioIndexViewModel viewControlModel = new DealerGuaranteeRatioIndexViewModel();
            viewModel.IdDealer = viewModel.IdDealer ?? UserManager.UserInfo.GetUserDealerId();
            viewControlModel.IdDealer = viewModel.IdDealer;

            //DealerGuaranteeRatioBo.GetDealerGuaranteeRatio(viewControlModel);

            SetDefaults();

            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Update;//Mantıksal Create

                DealerGuaranteeRatioBo.DMLDealerGuaranteeRatio(UserManager.UserInfo, viewModel);

                CheckErrorForMessage(viewModel, true);

                ModelState.Clear();
            }

            //var model = new DealerGuaranteeRatioIndexViewModel();
            //if (UserManager.UserInfo.GetUserDealerId() != 0)
            //    model.IdDealer = UserManager.UserInfo.GetUserDealerId();
            return View(new DealerGuaranteeRatioIndexViewModel() );
        
        }

        #endregion

        #region Excel Upload

        public ActionResult ExcelSample()
        {
            var bo = new DealerGuaranteeRatioBL();
            var ms = bo.SetExcelReport(null, string.Empty);
            return File(ms, CommonValues.ExcelContentType, MessageResource.DealerGuaranteeRatio_PageTitle_Index + CommonValues.ExcelExtOld);
        }
        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerGuaranteeRatio.DealerGuaranteeRatioIndex, CommonValues.PermissionCodes.DealerGuaranteeRatio.DealerGuaranteeRatioIndex)]
        public ActionResult DealerGuaranteeRatioIndex(DealerGuaranteeRatioListModel listModel, HttpPostedFileBase excelFile)
        {
            SetDefaults();

            DealerGuaranteeRatioIndexViewModel model = new DealerGuaranteeRatioIndexViewModel();
            
            if (ModelState.IsValid)
            {
                if (excelFile != null)
                {
                    var bo = new DealerGuaranteeRatioBL();
                    Stream s = excelFile.InputStream;
                    List<DealerGuaranteeRatioIndexViewModel> modelList = bo.ParseExcel(UserManager.UserInfo, model, s).Data;
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
                            DealerGuaranteeRatioIndexViewModel inserted = new DealerGuaranteeRatioIndexViewModel
                                {
                                    DealerSSID = row.DealerSSID,
                                    IdDealer = row.IdDealer
                                };

                            bo.GetDealerGuaranteeRatio(UserManager.UserInfo, inserted);
                            if (inserted.DealerSSID != null)
                            {
                                row.CommandType = CommonValues.DMLType.Update;

                                bo.DMLDealerGuaranteeRatio(UserManager.UserInfo, row);
                                if (row.ErrorNo > 0)
                                {
                                    SetMessage(row.ErrorMessage, CommonValues.MessageSeverity.Fail);
                                    return View(listModel);
                                }
                            }
                            else
                            {
                                SetMessage(MessageResource.Global_Warning_ExcelNotSelected, CommonValues.MessageSeverity.Fail);
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
                DealerGuaranteeRatioListModel dealerGuaranteeListModel = new DealerGuaranteeRatioListModel { IdDealer = (UserManager.UserInfo.GetUserDealerId() == 0 ? (int?)null : UserManager.UserInfo.GetUserDealerId()) };
                return View(dealerGuaranteeListModel);
            }
            return View(listModel);
        }

        #endregion

        #region DealerGuaranteeRatio Update
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.DealerGuaranteeRatio.DealerGuaranteeRatioIndex, ODMSCommon.CommonValues.PermissionCodes.DealerGuaranteeRatio.DealerGuaranteeRatioUpdate)]
        [HttpGet]
        public ActionResult DealerGuaranteeRatioUpdate(int id)
        {
            SetDefaults();
            var v = new DealerGuaranteeRatioIndexViewModel();
            if (id != 0)
            {
                var dealerGuaranteeRatioBo = new DealerGuaranteeRatioBL();

                v.IdDealer = id;

                dealerGuaranteeRatioBo.GetDealerGuaranteeRatio(UserManager.UserInfo, v);
            }
            return View(v);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.DealerGuaranteeRatio.DealerGuaranteeRatioIndex, ODMSCommon.CommonValues.PermissionCodes.DealerGuaranteeRatio.DealerGuaranteeRatioUpdate)]
        [HttpPost]
        public ActionResult DealerGuaranteeRatioUpdate(DealerGuaranteeRatioIndexViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            //if (UserManager.UserInfo.GetUserDealerId() > 0)
            //{
            //    viewModel.IdDealer = UserManager.UserInfo.GetUserDealerId();
            //}
            var dealerGuaranteeRatioBo = new DealerGuaranteeRatioBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Update;

                dealerGuaranteeRatioBo.DMLDealerGuaranteeRatio(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }

            SetDefaults();
            return View(viewModel);
        }

        #endregion

    }
}
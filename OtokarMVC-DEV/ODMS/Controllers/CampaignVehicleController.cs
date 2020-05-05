using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.CampaignVehicle;
using ODMSModel.DownloadFileActionResult;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class CampaignVehicleController : ControllerBase
    {
        private void SetDefaults()
        {
            List<SelectListItem> statusList = CommonBL.ListStatus().Data;
            ViewBag.StatusList = statusList;

            List<SelectListItem> yesNoList = CommonBL.ListYesNo().Data;
            ViewBag.YesNoList = yesNoList;
        }

        #region Campaign Vehicle Index

        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignVehicle.CampaignVehicleIndex)]
        [HttpGet]
        public ActionResult CampaignVehicleIndex(string campaignCode)
        {
            CampaignVehicleListModel model = new CampaignVehicleListModel { CampaignCode = campaignCode };
            SetDefaults();
            return PartialView(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignVehicle.CampaignVehicleIndex, CommonValues.PermissionCodes.CampaignVehicle.CampaignVehicleDetails)]
        public ActionResult ListCampaignVehicle([DataSourceRequest] DataSourceRequest request, CampaignVehicleListModel model)
        {
            var campaignVehicleBo = new CampaignVehicleBL();
            var v = new CampaignVehicleListModel(request);
            var totalCnt = 0;
            v.CampaignCode = model.CampaignCode;
            v.VinNo = model.VinNo;
            var returnValue = campaignVehicleBo.ListCampaignVehicles(UserManager.UserInfo,v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }


        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignVehicle.CampaignVehicleIndex, CommonValues.PermissionCodes.CampaignVehicle.CampaignVehicleDetails)]
        public ActionResult ListCampaignVehicleForDealer([DataSourceRequest] DataSourceRequest request, CampaignVehicleListModel model)
        {
            var campaignVehicleBo = new CampaignVehicleBL();
            var v = new CampaignVehicleListModel(request);
            var totalCnt = 0;
            v.CampaignCode = model.CampaignCode;
            v.VinNo = model.VinNo;
            var returnValue = campaignVehicleBo.ListCampaignVehiclesForDealer(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }


        #endregion

        #region Campaign Vehicle Create
        public ActionResult ExcelSample()
        {
            var bo = new CampaignVehicleBL();
            var ms = bo.SetExcelReport(null, string.Empty);
            return File(ms, CommonValues.ExcelContentType, MessageResource.CampaignVehicle_PageTitle_Index + CommonValues.ExcelExtOld);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignVehicle.CampaignVehicleIndex, CommonValues.PermissionCodes.CampaignVehicle.CampaignVehicleCreate)]
        [HttpGet]
        public ActionResult CampaignVehicleCreate(string campaignCode)
        {
            CampaignVehicleViewModel model = new CampaignVehicleViewModel();
            model.CampaignCode = campaignCode;
            model.IsActive = true;
            model.IsUtilized = false;
            SetDefaults();
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignVehicle.CampaignVehicleIndex, CommonValues.PermissionCodes.CampaignVehicle.CampaignVehicleCreate)]
        [HttpPost]
        public ActionResult CampaignVehicleCreate(CampaignVehicleViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            var campaignVehicleBo = new CampaignVehicleBL();

            if (ModelState.IsValid && attachments != null)
            {
                HttpPostedFileBase file = attachments.ElementAt(0);
                CampaignVehicleBL campaignVehicleBL = new CampaignVehicleBL();
                Stream s = attachments.ElementAt(0).InputStream;
                List<CampaignVehicleViewModel> campaignVehicleList = campaignVehicleBL.ParseExcel(UserManager.UserInfo, viewModel, s).Data;
                if (viewModel.ErrorNo > 0)
                {
                    var ms = campaignVehicleBo.SetExcelReport(campaignVehicleList, viewModel.ErrorMessage);

                    var fileViewModel = new DownloadFileViewModel
                    {
                        FileName = CommonValues.ErrorLog + DateTime.Now + CommonValues.ExcelExtOld,
                        ContentType = CommonValues.ExcelContentType,
                        MStream = ms,
                        Id = Guid.NewGuid()
                    };

                    Session[CommonValues.DownloadFileFormatCookieKey] = fileViewModel.Add(fileViewModel);
                    TempData[CommonValues.DownloadFileIdCookieKey] = fileViewModel.Id;

                    SetMessage(viewModel.ErrorMessage, CommonValues.MessageSeverity.Fail);

                    return View(viewModel);
                }
                else
                {
                    foreach (CampaignVehicleViewModel campaignVehicleViewModel in campaignVehicleList)
                    {
                        CampaignVehicleViewModel _orj = new CampaignVehicleViewModel() { CampaignCode = campaignVehicleViewModel.CampaignCode, VehicleId = campaignVehicleViewModel.VehicleId };
                        campaignVehicleBL.GetCampaignVehicle(UserManager.UserInfo, _orj);
                        campaignVehicleViewModel.CommandType = CommonValues.DMLType.Insert;

                        if (_orj.VinNo == campaignVehicleViewModel.VinNo.Trim())//update
                        {
                            campaignVehicleViewModel.CommandType = CommonValues.DMLType.Update;
                        }

                        campaignVehicleBo.DMLCampaignVehicle(UserManager.UserInfo, campaignVehicleViewModel);
                        if (campaignVehicleViewModel.ErrorNo > 0)
                        {
                            SetMessage(campaignVehicleViewModel.ErrorMessage, CommonValues.MessageSeverity.Fail);
                            return View(viewModel);
                        }
                    }

                    SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);

                    ModelState.Clear();
                }
            }
            return View(viewModel);
        }

        #endregion

        #region Campaign Vehicle Update
        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignVehicle.CampaignVehicleIndex, CommonValues.PermissionCodes.CampaignVehicle.CampaignVehicleUpdate)]
        [HttpGet]
        public ActionResult CampaignVehicleUpdate(string campaignCode, int vehicleId)
        {
            SetDefaults();
            var v = new CampaignVehicleViewModel();
            if (vehicleId > 0)
            {
                var campaignVehicleBo = new CampaignVehicleBL();
                v.VehicleId = vehicleId;
                v.CampaignCode = campaignCode;
                campaignVehicleBo.GetCampaignVehicle(UserManager.UserInfo, v);
            }
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignVehicle.CampaignVehicleIndex, CommonValues.PermissionCodes.CampaignVehicle.CampaignVehicleUpdate)]
        [HttpPost]
        public ActionResult CampaignVehicleUpdate(CampaignVehicleViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            var campaignVehicleBo = new CampaignVehicleBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Update;
                campaignVehicleBo.DMLCampaignVehicle(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
                campaignVehicleBo.GetCampaignVehicle(UserManager.UserInfo, viewModel);
            }
            return View(viewModel);
        }

        #endregion

        #region Campaign Vehicle Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignVehicle.CampaignVehicleIndex, CommonValues.PermissionCodes.CampaignVehicle.CampaignVehicleDelete)]
        public ActionResult DeleteCampaignVehicle(string campaignCode, int vehicleId)
        {
            CampaignVehicleViewModel viewModel = new CampaignVehicleViewModel() { CampaignCode = campaignCode, VehicleId = vehicleId };

            var campaignVehicleBo = new CampaignVehicleBL();
            campaignVehicleBo.GetCampaignVehicle(UserManager.UserInfo, viewModel);

            if (viewModel.IsUtilized.GetValue<bool>())
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.CampaignVehicle_Warning_CannotDeleteUtilized);
            }
            else
            {
                viewModel.CommandType = viewModel.VehicleId > 0 ? CommonValues.DMLType.Delete : string.Empty;
                campaignVehicleBo.DMLCampaignVehicle(UserManager.UserInfo, viewModel);

                ModelState.Clear();
                if (viewModel.ErrorNo == 0)
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
            }
        }
        #endregion

        #region Campaign Vehicle Details
        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignVehicle.CampaignVehicleIndex, CommonValues.PermissionCodes.CampaignVehicle.CampaignVehicleDetails)]
        [HttpGet]
        public ActionResult CampaignVehicleDetails(string campaignCode, int vehicleId)
        {
            var v = new CampaignVehicleViewModel();
            var campaignVehicleBo = new CampaignVehicleBL();

            v.VehicleId = vehicleId;
            v.CampaignCode = campaignCode;
            campaignVehicleBo.GetCampaignVehicle(UserManager.UserInfo, v);

            return View(v);
        }

        #endregion
    }
}
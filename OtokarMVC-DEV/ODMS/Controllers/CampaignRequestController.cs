using System;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.CampaignRequest;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using ODMSModel.Campaign;
using ODMSModel.CampaignVehicle;
using System.Linq;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class CampaignRequestController : ControllerBase
    {
        private void SetDefaults()
        {
            // PartList
            List<SelectListItem> dealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.DealerList = dealerList;
            // SupplyTypeList
            List<SelectListItem> requestStatusList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.CampaignRequestStatusLookup).Data;
            ViewBag.RequestStatusList = requestStatusList;

            ViewBag.ModelList = VehicleBL.ListVehicleCodeAsSelectListItem(UserManager.UserInfo, null).Data;

            ViewBag.CampaignCodeList = CampaignBL.ListCampaignAsSelectListItem(UserManager.UserInfo, null).Data;
        }

        #region CampaignRequest Index

        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignRequest.CampaignRequestIndex)]
        [HttpGet]
        public ActionResult CampaignRequestIndex()
        {
            CampaignRequestListModel model = new CampaignRequestListModel();
            if (UserManager.UserInfo.GetUserDealerId() > 0)
            {
                model.IdDealer = UserManager.UserInfo.GetUserDealerId();
            }

            SetDefaults();

            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignRequest.CampaignRequestIndex, CommonValues.PermissionCodes.CampaignRequest.CampaignRequestDetails)]
        public ActionResult ListCampaignRequest([DataSourceRequest] DataSourceRequest request, CampaignRequestListModel model)
        {
            var campaignRequestBo = new CampaignRequestBL();
            var v = new CampaignRequestListModel(request);
            var totalCnt = 0;
            v.CampaignCode = model.CampaignCode;
            v.VerihcleModelCode = model.VerihcleModelCode;
            v.IdCampaignRequest = model.IdCampaignRequest;
            v.RequestStatus = model.RequestStatus;
            v.RequestStatusName = model.RequestStatusName;
            v.IdDealer = model.IdDealer;
            v.CampaignName = model.CampaignName;

            var returnValue = campaignRequestBo.ListCampaignRequest(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region CampaignRequest Create

        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignRequest.CampaignRequestIndex, CommonValues.PermissionCodes.CampaignRequest.CampaignRequestCreate)]
        public ActionResult CampaignRequestCreate(int? status)
        {
            SetDefaults();

            var model = new CampaignRequestViewModel
            {
                RequestStatus = status ?? 0,
                PreferredOrderDate = DateTime.Now
            };

            if (UserManager.UserInfo.GetUserDealerId() > 0)
            {
                model.IdDealer = UserManager.UserInfo.GetUserDealerId();
            }

            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignRequest.CampaignRequestIndex, CommonValues.PermissionCodes.CampaignRequest.CampaignRequestCreate)]
        [HttpPost]
        public ActionResult CampaignRequestCreate(CampaignRequestViewModel viewModel)
        {
            var campaignRequestBo = new CampaignRequestBL();

            SetDefaults();

            var campaignModel = new CampaignViewModel();
            campaignModel.CampaignCode = viewModel.CampaignCode;

            var campaignBo = new CampaignBL();
            campaignBo.GetCampaign(UserManager.UserInfo, campaignModel);

            if (campaignModel.StartDate > viewModel.PreferredOrderDate || campaignModel.EndDate < viewModel.PreferredOrderDate)
            {
                SetMessage(MessageResource.CampaignRequest_Error_DateRange, CommonValues.MessageSeverity.Fail);

                return View(viewModel);
            }

            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Insert;

                campaignRequestBo.DMLCampaignRequest(UserManager.UserInfo, viewModel);

                CheckErrorForMessage(viewModel, true);

                ModelState.Clear();
            }

            CampaignRequestViewModel model = new CampaignRequestViewModel();
            if (UserManager.UserInfo.GetUserDealerId() > 0)
            {
                model.IdDealer = viewModel.IdDealer;
            }

            model.RequestStatus = viewModel.RequestStatus;//Aynı pencereden ikinci kez kayıt yapılamaz.
            model.PreferredOrderDate = DateTime.Now;
            return View(model);

        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignRequest.CampaignRequestIndex, CommonValues.PermissionCodes.CampaignRequest.CampaignRequestCreate)]
        [HttpPost]
        public ActionResult GetCampaignVinNumbers(string CampaignCode)
        {

            var _bll = new CampaignVehicleBL();
            var totalCnt = 0;
            var v = new CampaignVehicleListModel();
            v.CampaignCode = CampaignCode;

            var response = _bll.ListCampaignVehiclesMain(UserManager.UserInfo, v, out totalCnt);
            

            return Json(new
            {
                Data = response.Data.Where(w=>w.IsComplete == 0 ),
                Total = totalCnt
            });
            
        }


        #endregion

        #region CampaignRequest Update
        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignRequest.CampaignRequestIndex, CommonValues.PermissionCodes.CampaignRequest.CampaignRequestUpdate)]
        [HttpGet]
        public ActionResult CampaignRequestUpdate(decimal? idCampaignRequest)
        {
            SetDefaults();
            var v = new CampaignRequestViewModel();
            if (idCampaignRequest != 0)
            {
                var customerDiscountBo = new CampaignRequestBL();
                v.IdCampaignRequest = idCampaignRequest;

                customerDiscountBo.GetCampaignRequest(UserManager.UserInfo, v);
            }
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignRequest.CampaignRequestIndex, CommonValues.PermissionCodes.CampaignRequest.CampaignRequestUpdate)]
        [HttpPost]
        public ActionResult CampaignRequestUpdate(CampaignRequestViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            var campaignRequestBo = new CampaignRequestBL();
            if (ModelState.IsValid)
            {
                if (viewModel.RequestStatus == 0)//Sipariş vermek için
                {
                    //İptal işlemi silinip tekrar insert edilerek yapılmaktadır.
                    viewModel.CommandType = CommonValues.DMLType.Update;
                    campaignRequestBo.DMLCampaignRequest(UserManager.UserInfo, viewModel);
                }
                else
                {
                    viewModel.ErrorNo = 1;
                    viewModel.ErrorMessage = MessageResource.CampaignRequest_Display_Error;
                }
                CheckErrorForMessage(viewModel, true);
            }



            SetDefaults();
            return View(viewModel);
        }

        #endregion

        #region CampaignRequest Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignRequest.CampaignRequestIndex,
            CommonValues.PermissionCodes.CampaignRequest.CampaignRequestDelete)]
        public ActionResult DeleteCampaignRequest(decimal idCampaignRequest)
        {
            var viewModel = new CampaignRequestViewModel { IdCampaignRequest = idCampaignRequest };

            var campaignRequestBo = new CampaignRequestBL();
            campaignRequestBo.GetCampaignRequest(UserManager.UserInfo, viewModel);

            if (viewModel.WorkOrderId == 0)
            {
                if (viewModel.RequestStatus == 0)
                {
                    viewModel.CommandType = CommonValues.DMLType.Delete;
                    campaignRequestBo.DMLCampaignRequest(UserManager.UserInfo, viewModel);

                    ModelState.Clear();
                }
                else
                {
                    viewModel.ErrorNo = 1;
                    viewModel.ErrorMessage = MessageResource.CampaignRequest_Display_Error;
                }
            }
            else
            {
                viewModel.ErrorNo = 1;
                viewModel.ErrorMessage = MessageResource.CampaignRequest_Display_ErrorWorkOrder;
            }

            return viewModel.ErrorNo == 0 ? GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success)
                       : GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
        }

        #endregion

        #region CampaignRequest Details
        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignRequest.CampaignRequestIndex, CommonValues.PermissionCodes.CampaignRequest.CampaignRequestDetails)]
        [HttpGet]
        public ActionResult CampaignRequestDetails(decimal? idCampaignRequest)
        {
            SetDefaults();

            var v = new CampaignRequestViewModel();
            var campaignRequestBo = new CampaignRequestBL();

            v.IdCampaignRequest = idCampaignRequest;

            campaignRequestBo.GetCampaignRequest(UserManager.UserInfo, v);

            return View(v);
        }

        #endregion

        #region CampaignRequest Approved

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignRequest.CampaignRequestIndex, CommonValues.PermissionCodes.CampaignRequest.CampaignRequestCreate)]
        public ActionResult SendApproved(decimal campaignRequestId)
        {
            var campaignRequestBo = new CampaignRequestBL();

            var model = new CampaignRequestViewModel { IdCampaignRequest = campaignRequestId };
            campaignRequestBo.GetCampaignRequest(UserManager.UserInfo, model);

            model.RequestStatus = 1;
            model.CommandType = "A";
            model.PreferredOrderDate = DateTime.Now;

            campaignRequestBo.DMLCampaignRequest(UserManager.UserInfo, model);

            return model.ErrorNo == 0 ? GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success) :
                                            GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        #endregion
    }
}
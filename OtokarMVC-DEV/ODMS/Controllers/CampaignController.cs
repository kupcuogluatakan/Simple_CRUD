using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.Campaign;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class CampaignController : ControllerBase
    {
        private void SetDefaults()
        {
            List<SelectListItem> listSLCode = CommonBL.ListAppIndcFailureCode(UserManager.UserInfo).Data;
            ViewBag.SLFailureCode = listSLCode;
            //ModelKod List
            List<SelectListItem> modelKodList = VehicleModelBL.ListVehicleModelAsSelectList(UserManager.UserInfo).Data;
            ViewBag.ModelKodList = modelKodList;
            //YesNo List
            List<SelectListItem> yesNoList = CommonBL.ListYesNo().Data;
            ViewBag.YesNoList = yesNoList;
            //Status List
            List<SelectListItem> statusList = CommonBL.ListStatus().Data;
            ViewBag.StatusList = statusList;
            //SubCategryList
            List<SelectListItem> subCategoryList = AppointmentIndicatorSubCategoryBL.ListAppointmentIndicatorSubCategories(UserManager.UserInfo, null, true).Data;
            ViewBag.SubCategoryList = subCategoryList;
        }

        #region Campaign Index

        [AuthorizationFilter(CommonValues.PermissionCodes.Campaign.CampaignIndex)]
        [HttpGet]
        public ActionResult CampaignIndex(string campaignCode)
        {
            SetDefaults();
            CampaignListModel model = new CampaignListModel();
            if (!string.IsNullOrEmpty(campaignCode))
            {
                model.CampaignCode = campaignCode;
            }
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Campaign.CampaignIndex, CommonValues.PermissionCodes.Campaign.CampaignDetails)]
        public ActionResult ListCampaign([DataSourceRequest] DataSourceRequest request, CampaignListModel model)
        {
            var campaignBo = new CampaignBL();
            var v = new CampaignListModel(request);
            var totalCnt = 0;
            v.ModelKod = model.ModelKod;
            v.IsActive = model.IsActive;
            v.CampaignCode = model.CampaignCode;
            v.CampaignName = model.CampaignName;
            v.AdminDesc = model.AdminDesc;
            v.MainFailureCode = model.MainFailureCode;
            v.IndicatorCode = model.IndicatorCode;
            v.ModelKod = model.ModelKod;
            v.IsMust = model.IsMust;
            v.StartDate = model.StartDate;
            v.EndDate = model.EndDate;
            v.IndicatorCode = model.IndicatorCode;

            var returnValue = campaignBo.ListCampaigns(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region Campaign Create
        [AuthorizationFilter(CommonValues.PermissionCodes.Campaign.CampaignIndex, CommonValues.PermissionCodes.Campaign.CampaignCreate)]
        [HttpGet]
        public ActionResult CampaignCreate()
        {
            var model = new CampaignViewModel();
            model.StartDate = DateTime.Now;
            model.EndDate = DateTime.Now;
            model.IsActive = true;
            SetDefaults();
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Campaign.CampaignIndex, CommonValues.PermissionCodes.Campaign.CampaignCreate)]
        [HttpPost]
        public ActionResult CampaignCreate(CampaignViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            var campaignBo = new CampaignBL();

            if (ModelState.IsValid)
            {
                int totalCount = 0;
                CampaignListModel listModel = new CampaignListModel
                {
                    CampaignCode = viewModel.CampaignCode
                };

                campaignBo.ListCampaigns(UserManager.UserInfo, listModel, out totalCount);
                if (totalCount != 0)
                {
                    SetMessage(MessageResource.Campaign_Warning_CampaignCodeExists, CommonValues.MessageSeverity.Fail);
                    return View(viewModel);
                }
                else
                {
                    listModel.CampaignCode = null;
                    listModel.SubCategoryId = viewModel.SubCategoryId;
                    campaignBo.ListCampaigns(UserManager.UserInfo, listModel, out totalCount);
                }

                viewModel.CommandType = CommonValues.DMLType.Insert;
                campaignBo.DMLCampaign(UserManager.UserInfo, viewModel);

                CheckErrorForMessage(viewModel, true);

                ModelState.Clear();
            }
            return View(viewModel);
        }

        #endregion

        #region Campaign Update
        [AuthorizationFilter(CommonValues.PermissionCodes.Campaign.CampaignIndex, CommonValues.PermissionCodes.Campaign.CampaignUpdate)]
        [HttpGet]
        public ActionResult CampaignUpdate(string campaignCode)
        {
            SetDefaults();
            var v = new CampaignViewModel();
            if (!string.IsNullOrEmpty(campaignCode))
            {
                var campaignBo = new CampaignBL();
                v.CampaignCode = campaignCode;
                campaignBo.GetCampaign(UserManager.UserInfo, v);
            }
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Campaign.CampaignIndex, CommonValues.PermissionCodes.Campaign.CampaignUpdate)]
        [HttpPost]
        public ActionResult CampaignUpdate(CampaignViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            var campaignBo = new CampaignBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Update;
                campaignBo.DMLCampaign(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }
            return View(viewModel);
        }

        #endregion

        #region Campaign Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.Campaign.CampaignIndex, CommonValues.PermissionCodes.Campaign.CampaignDelete)]
        public ActionResult DeleteCampaign(string campaignCode)
        {
            CampaignViewModel viewModel = new CampaignViewModel() { CampaignCode = campaignCode };
            var campaignBo = new CampaignBL();
            viewModel.CommandType = !string.IsNullOrEmpty(campaignCode) ? CommonValues.DMLType.Delete : string.Empty;
            campaignBo.DMLCampaign(UserManager.UserInfo, viewModel);
            ModelState.Clear();

            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
        }
        #endregion

        #region Campaign Details
        [AuthorizationFilter(CommonValues.PermissionCodes.Campaign.CampaignIndex, CommonValues.PermissionCodes.Campaign.CampaignDetails)]
        [HttpGet]
        public ActionResult CampaignDetails(string campaignCode)
        {
            var v = new CampaignViewModel();
            var campaignBo = new CampaignBL();

            v.CampaignCode = campaignCode;
            campaignBo.GetCampaign(UserManager.UserInfo, v);

            return View(v);
        }

        #endregion
    }
}
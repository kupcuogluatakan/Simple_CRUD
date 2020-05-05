using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.CampaignDismantlePart;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class CampaignDismantlePartController : ControllerBase
    {
        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignDismantlePart.CampaignDismantlePartIndex)]
        public ActionResult CampaignDismantlePartIndex(string campaignCode)
        {
            var model = new CampaignDismantlePartViewModel();
            if (!string.IsNullOrEmpty(campaignCode))
                model.CampaignCode = campaignCode;

            return PartialView(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignDismantlePart.CampaignDismantlePartIndex)]
        public ActionResult ListCampaignDismantlePart([DataSourceRequest]DataSourceRequest request, CampaignDismantlePartListModel cModel)
        {
            var bl = new CampaignDismantlePartBL();
            var model = new CampaignDismantlePartListModel(request);
            int totalCount = 0;

            model.CampaignCode = cModel.CampaignCode;
            model.PartId = cModel.PartId;

            var rValue = bl.ListCampaignDismantlePart(UserManager.UserInfo, model, out totalCount).Data;

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignDismantlePart.CampaignDismantlePartIndex)]
        public ActionResult CampaignDismantlePartCreate(string campaignCode)
        {
            var model = new CampaignDismantlePartViewModel { CampaignCode = campaignCode };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignDismantlePart.CampaignDismantlePartIndex, CommonValues.PermissionCodes.CampaignDismantlePart.CampaignDismantlePartCreate)]
        public ActionResult CampaignDismantlePartCreate(CampaignDismantlePartViewModel model)
        {
            SetComboBox();

            if (!ModelState.IsValid)
                return View(model);

            string cCode = model.CampaignCode;
            var bl = new CampaignDismantlePartBL();
            model.CommandType = ODMSCommon.CommonValues.DMLType.Insert;

            bl.DMLCampaignDismantlePart(UserManager.UserInfo, model);

            if (!CheckErrorForMessage(model, true))
            {
                model = new CampaignDismantlePartViewModel() { CampaignCode = cCode, PartId = null };//TODO : ModelState Clear yemedi:S
                ModelState.Clear();
            }


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignDismantlePart.CampaignDismantlePartIndex, CommonValues.PermissionCodes.CampaignDismantlePart.CampaignDismantlePartDelete)]
        public ActionResult DeleteCampaignDismantlePart(string campaignCode, int partId)
        {
            var model = new CampaignDismantlePartViewModel();
            if (String.IsNullOrEmpty(campaignCode) || partId <= 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                model.ErrorMessage);

            var bl = new CampaignDismantlePartBL();
            model.CommandType = ODMSCommon.CommonValues.DMLType.Delete;
            model.CampaignCode = campaignCode;
            model.PartId = partId;

            bl.DMLCampaignDismantlePart(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        #region Campaign Document Details
        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignDismantlePart.CampaignDismantlePartIndex,
            CommonValues.PermissionCodes.CampaignDismantlePart.CampaignDismantlePartDetails)]
        [HttpGet]
        public ActionResult CampaignDismantlePartDetails(string campaignCode, int partId)
        {
            var v = new CampaignDismantlePartViewModel();
            var campaignPartBo = new CampaignDismantlePartBL();

            v.CampaignCode = campaignCode;
            v.PartId = partId;
            campaignPartBo.GetCampaignDismantlePart(UserManager.UserInfo, v);

            return View(v);
        }

        #endregion

        #region Campaign Document Update
        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignDismantlePart.CampaignDismantlePartIndex,
            CommonValues.PermissionCodes.CampaignDismantlePart.CampaignDismantlePartUpdate)]
        [HttpGet]
        public ActionResult CampaignDismantlePartUpdate(string campaignCode, int partId)
        {
            var v = new CampaignDismantlePartViewModel();
            if (partId > 0)
            {
                var campaignPartBo = new CampaignDismantlePartBL();
                v.CampaignCode = campaignCode;
                v.PartId = partId;
                campaignPartBo.GetCampaignDismantlePart(UserManager.UserInfo, v);
            }
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignDismantlePart.CampaignDismantlePartIndex,
            CommonValues.PermissionCodes.CampaignDismantlePart.CampaignDismantlePartUpdate)]
        [HttpPost]
        public ActionResult CampaignDismantlePartUpdate(CampaignDismantlePartViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            var campaignPartBo = new CampaignDismantlePartBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Update;
                campaignPartBo.DMLCampaignDismantlePart(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }
            return View(viewModel);
        }

        #endregion
        private void SetComboBox()
        {
        }
    }
}

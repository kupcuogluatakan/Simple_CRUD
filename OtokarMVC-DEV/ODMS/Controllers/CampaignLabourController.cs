using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.CampaignLabour;
using System;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class CampaignLabourController : ControllerBase
    {
        #region Member Variables

        private void SetDefaults()
        {
            List<SelectListItem> labourList = LabourBL.ListLabourAsSelectList(UserManager.UserInfo).Data;
            ViewBag.LabourList = labourList;
        }
        private string GetDurations(string campaignCode, Int64 idLabour)
        {
            StringBuilder durations = new StringBuilder();
            if (campaignCode != null && idLabour != 0)
            {

                List<SelectListItem> list = new CampaignLabourBL().ListLabourTimeAsSelectList(UserManager.UserInfo, campaignCode, idLabour).Data;
                foreach (var item in list)
                {
                    if (durations.Length != 0) durations.Append("<br>");
                    durations.Append(item.Value + " - " + item.Text);
                }
                return durations.ToString();
            }
            return string.Empty;
        }

        [HttpGet]
        public JsonResult GetLabourTime(string campaignCode, Int64 idLabour)
        {
            string durations = string.Empty;
            if (campaignCode != null && idLabour != 0)
            {
                durations = GetDurations(campaignCode, idLabour);
                return Json(durations, JsonRequestBehavior.AllowGet);
            }
            return Json(new List<CampaignLabourViewModel>(), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Campaign Labour Index

        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignLabour.CampaignLabourIndex)]
        [HttpGet]
        public ActionResult CampaignLabourIndex(string campaignCode)
        {
            CampaignLabourListModel model = new CampaignLabourListModel();
            if (campaignCode != null)
            {
                model.CampaignCode = campaignCode;
            }
            SetDefaults();
            return PartialView(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignLabour.CampaignLabourIndex, CommonValues.PermissionCodes.CampaignLabour.CampaignLabourDetails)]
        public ActionResult ListCampaignLabour([DataSourceRequest] DataSourceRequest request, CampaignLabourListModel model)
        {
            var campaignLabourBo = new CampaignLabourBL();
            var v = new CampaignLabourListModel(request);
            var totalCnt = 0;
            v.CampaignCode = model.CampaignCode;
            var returnValue = campaignLabourBo.ListCampaignLabours(UserManager.UserInfo, v, out totalCnt).Data;


            foreach (CampaignLabourListModel campaignLabourListModel in returnValue)
            {
                string durations = GetDurations(campaignLabourListModel.CampaignCode,
                                                campaignLabourListModel.LabourId);
                campaignLabourListModel.LabourDuration = durations;
            }

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region Campaign Labour Create
        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignLabour.CampaignLabourIndex, CommonValues.PermissionCodes.CampaignLabour.CampaignLabourCreate)]
        [HttpGet]
        public ActionResult CampaignLabourCreate(string campaignCode)
        {
            CampaignLabourViewModel model = new CampaignLabourViewModel();
            model.CampaignCode = campaignCode;
            SetDefaults();
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignLabour.CampaignLabourIndex, CommonValues.PermissionCodes.CampaignLabour.CampaignLabourCreate)]
        [HttpPost]
        public ActionResult CampaignLabourCreate(CampaignLabourViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            var campaignLabourBo = new CampaignLabourBL();

            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Insert;
                campaignLabourBo.DMLCampaignLabour(UserManager.UserInfo, viewModel);

                CheckErrorForMessage(viewModel, true);

                ModelState.Clear();
                var model = new CampaignLabourViewModel { CampaignCode = viewModel.CampaignCode };
                return View(model);
            }
            return View(viewModel);
        }

        #endregion

        #region Campaign Labour Update
        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignLabour.CampaignLabourIndex, CommonValues.PermissionCodes.CampaignLabour.CampaignLabourUpdate)]
        [HttpGet]
        public ActionResult CampaignLabourUpdate(string campaignCode, int labourId)
        {
            SetDefaults();
            var v = new CampaignLabourViewModel();
            if (labourId > 0)
            {
                var campaignLabourBo = new CampaignLabourBL();
                v.LabourId = labourId;
                v.CampaignCode = campaignCode;
                campaignLabourBo.GetCampaignLabour(UserManager.UserInfo, v);
            }

            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignLabour.CampaignLabourIndex, CommonValues.PermissionCodes.CampaignLabour.CampaignLabourUpdate)]
        [HttpPost]
        public ActionResult CampaignLabourUpdate(CampaignLabourViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            var campaignLabourBo = new CampaignLabourBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Update;
                campaignLabourBo.DMLCampaignLabour(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }
            return View(viewModel);
        }

        #endregion

        #region Campaign Labour Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignLabour.CampaignLabourIndex, CommonValues.PermissionCodes.CampaignLabour.CampaignLabourDelete)]
        public ActionResult DeleteCampaignLabour(string campaignCode, int labourId)
        {
            CampaignLabourViewModel viewModel = new CampaignLabourViewModel() { CampaignCode = campaignCode, LabourId = labourId };
            var campaignLabourBo = new CampaignLabourBL();
            viewModel.CommandType = viewModel.LabourId > 0 ? CommonValues.DMLType.Delete : string.Empty;
            campaignLabourBo.DMLCampaignLabour(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
        }
        #endregion

        #region Campaign Labour Details
        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignLabour.CampaignLabourIndex, CommonValues.PermissionCodes.CampaignLabour.CampaignLabourDetails)]
        [HttpGet]
        public ActionResult CampaignLabourDetails(string campaignCode, int labourId)
        {
            var v = new CampaignLabourViewModel();
            var campaignLabourBo = new CampaignLabourBL();

            v.LabourId = labourId;
            v.CampaignCode = campaignCode;
            campaignLabourBo.GetCampaignLabour(UserManager.UserInfo, v);

            return View(v);
        }

        #endregion
    }
}
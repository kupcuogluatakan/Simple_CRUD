using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using System.Linq;
using ODMSCommon.Resources;
using ODMSModel.CampaignPart;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class CampaignPartController : ControllerBase
    {
        private void SetDefaults()
        {
            // SupplyTypeList                                               
            List<SelectListItem> supplyTypeList = CommonBL.ListSupplyType().Data;//.ListLookup(CommonValues.LookupKeys.SupplyTypeLookup);
            ViewBag.SupplyTypeList = supplyTypeList;
        }

        #region Campaign Part Index

        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignPart.CampaignPartIndex)]
        [HttpGet]
        public ActionResult CampaignPartIndex(string campaignCode)
        {
            CampaignPartListModel model = new CampaignPartListModel();
            if (campaignCode != null)
            {
                model.CampaignCode = campaignCode;
            }
            SetDefaults();
            return PartialView(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignPart.CampaignPartIndex, CommonValues.PermissionCodes.CampaignPart.CampaignPartDetails)]
        public ActionResult ListCampaignPart([DataSourceRequest] DataSourceRequest request, CampaignPartListModel model)
        {
            var campaignPartBo = new CampaignPartBL();
            var v = new CampaignPartListModel(request);
            var totalCnt = 0;
            v.CampaignCode = model.CampaignCode;
            var returnValue = campaignPartBo.ListCampaignParts(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region Campaign Part Create
        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignPart.CampaignPartIndex, CommonValues.PermissionCodes.CampaignPart.CampaignPartCreate)]
        [HttpGet]
        public ActionResult CampaignPartCreate(string campaignCode)
        {
            CampaignPartViewModel model = new CampaignPartViewModel();
            model.CampaignCode = campaignCode;
            SetDefaults();
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignPart.CampaignPartIndex, CommonValues.PermissionCodes.CampaignPart.CampaignPartCreate)]
        [HttpPost]
        public ActionResult CampaignPartCreate(CampaignPartViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            var campaignPartBo = new CampaignPartBL();

            if (ModelState.IsValid)
            {
                int count = 0;
                var camPartBo = new CampaignPartBL();
                var listModel = new CampaignPartListModel { CampaignCode = viewModel.CampaignCode };
                List<CampaignPartListModel> list = camPartBo.ListCampaignParts(UserManager.UserInfo, listModel, out count).Data;
                if (count != 0)
                {
                    var control = (from r in list.AsEnumerable()
                                   where r.PartId == viewModel.PartId
                                   select r);
                    if (control.Count() != 0)
                    {
                        SetMessage(MessageResource.CampaignPart_Error_PartExists, CommonValues.MessageSeverity.Fail);
                        return View(viewModel);
                    }
                }


                viewModel.CommandType = CommonValues.DMLType.Insert;
                campaignPartBo.DMLCampaignPart(UserManager.UserInfo, viewModel);
                // gelentext hata kodunu barındırdığında patlıyordu. Regex koydum taner
                if (viewModel.ErrorMessage != "" && Regex.IsMatch(viewModel.ErrorMessage, "^[0-9]+$"))//(viewModel.PartId != long.Parse(viewModel.ErrorMessage))
                {
                    viewModel.PartId = long.Parse(viewModel.ErrorMessage);//ErrorMessage içinde yeni IdPart var
                    campaignPartBo.GetCampaignPart(UserManager.UserInfo, viewModel);

                    return View(viewModel);
                }

                CheckErrorForMessage(viewModel, true);

                ModelState.Clear();
                CampaignPartViewModel model = new CampaignPartViewModel { CampaignCode = viewModel.CampaignCode };

                return View(model);
            }
            return View(viewModel);
        }

        #endregion

        #region Campaign Part Update
        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignPart.CampaignPartIndex, CommonValues.PermissionCodes.CampaignPart.CampaignPartUpdate)]
        [HttpGet]
        public ActionResult CampaignPartUpdate(string campaignCode, int partId)
        {
            SetDefaults();
            var v = new CampaignPartViewModel();
            if (partId > 0)
            {
                var campaignPartBo = new CampaignPartBL();
                v.PartId = partId;
                v.CampaignCode = campaignCode;
                campaignPartBo.GetCampaignPart(UserManager.UserInfo, v);
            }
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignPart.CampaignPartIndex, CommonValues.PermissionCodes.CampaignPart.CampaignPartUpdate)]
        [HttpPost]
        public ActionResult CampaignPartUpdate(CampaignPartViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            var campaignPartBo = new CampaignPartBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Update;
                campaignPartBo.DMLCampaignPart(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);

                CampaignPartViewModel model = new CampaignPartViewModel
                {
                    CampaignCode = viewModel.CampaignCode,
                    PartId = viewModel.PartId
                };
                campaignPartBo.GetCampaignPart(UserManager.UserInfo, model);
                return View(model);
            }
            return View(viewModel);
        }

        #endregion

        #region Campaign Part Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignPart.CampaignPartIndex, CommonValues.PermissionCodes.CampaignPart.CampaignPartDelete)]
        public ActionResult DeleteCampaignPart(string campaignCode, int partId)
        {
            CampaignPartViewModel viewModel = new CampaignPartViewModel() { CampaignCode = campaignCode, PartId = partId };
            var campaignPartBo = new CampaignPartBL();
            viewModel.CommandType = viewModel.PartId > 0 ? CommonValues.DMLType.Delete : string.Empty;
            campaignPartBo.DMLCampaignPart(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
        }
        #endregion

        #region Campaign Part Details
        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignPart.CampaignPartIndex, CommonValues.PermissionCodes.CampaignPart.CampaignPartDetails)]
        [HttpGet]
        public ActionResult CampaignPartDetails(string campaignCode, int partId)
        {
            var v = new CampaignPartViewModel();
            var campaignPartBo = new CampaignPartBL();

            v.PartId = partId;
            v.CampaignCode = campaignCode;
            campaignPartBo.GetCampaignPart(UserManager.UserInfo, v);

            return View(v);
        }

        #endregion
    }
}
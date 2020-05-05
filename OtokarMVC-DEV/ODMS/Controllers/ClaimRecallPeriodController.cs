using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSModel.ClaimRecallPeriod;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class ClaimRecallPeriodController : ControllerBase
    {
        private void SetDefaults()
        {
            ViewBag.StatusList = CommonBL.ListStatus().Data;
        }

        #region ClaimRecallPeriod Index

        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimRecallPeriod.ClaimRecallPeriodIndex)]
        [HttpGet]
        public ActionResult ClaimRecallPeriodIndex(int? claimRecallPeriodId)
        {
            SetDefaults();
            ClaimRecallPeriodListModel model = new ClaimRecallPeriodListModel();
            if (claimRecallPeriodId != null)
            {
                model.ClaimRecallPeriodId = claimRecallPeriodId.GetValue<int>();
            }
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimRecallPeriod.ClaimRecallPeriodIndex, CommonValues.PermissionCodes.ClaimRecallPeriod.ClaimRecallPeriodDetails)]
        public ActionResult ListClaimRecallPeriod([DataSourceRequest] DataSourceRequest request, ClaimRecallPeriodListModel model)
        {
            var claimRecallPeriodBo = new ClaimRecallPeriodBL();
            var v = new ClaimRecallPeriodListModel(request);
            var totalCnt = 0;
            v.IsActive = model.IsActive;
            v.ClaimRecallPeriodId = model.ClaimRecallPeriodId;
            var returnValue = claimRecallPeriodBo.ListClaimRecallPeriods(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region ClaimRecallPeriod Create
        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimRecallPeriod.ClaimRecallPeriodIndex, CommonValues.PermissionCodes.ClaimRecallPeriod.ClaimRecallPeriodCreate)]
        [HttpGet]
        public ActionResult ClaimRecallPeriodCreate()
        {
            var model = new ClaimRecallPeriodViewModel();
            SetDefaults();
            model.IsActive = true;
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimRecallPeriod.ClaimRecallPeriodIndex, CommonValues.PermissionCodes.ClaimRecallPeriod.ClaimRecallPeriodCreate)]
        [HttpPost]
        public ActionResult ClaimRecallPeriodCreate(ClaimRecallPeriodViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            var claimRecallPeriodBo = new ClaimRecallPeriodBL();

            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Insert;
                claimRecallPeriodBo.DMLClaimRecallPeriod(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
                ModelState.Clear();

                var model = new ClaimRecallPeriodViewModel();
                model.IsActive = true;
                return View(model);
            }
            return View(viewModel);
        }

        #endregion

        #region ClaimRecallPeriod Update
        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimRecallPeriod.ClaimRecallPeriodIndex, CommonValues.PermissionCodes.ClaimRecallPeriod.ClaimRecallPeriodUpdate)]
        [HttpGet]
        public ActionResult ClaimRecallPeriodUpdate(int claimRecallPeriodId)
        {
            SetDefaults();
            var v = new ClaimRecallPeriodViewModel();
            if (claimRecallPeriodId != 0)
            {
                var claimRecallPeriodBo = new ClaimRecallPeriodBL();
                v.ClaimRecallPeriodId = claimRecallPeriodId;
                claimRecallPeriodBo.GetClaimRecallPeriod(UserManager.UserInfo, v);
            }
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimRecallPeriod.ClaimRecallPeriodIndex, CommonValues.PermissionCodes.ClaimRecallPeriod.ClaimRecallPeriodUpdate)]
        [HttpPost]
        public ActionResult ClaimRecallPeriodUpdate(ClaimRecallPeriodViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            var claimRecallPeriodBo = new ClaimRecallPeriodBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = ODMSCommon.CommonValues.DMLType.Update;
                claimRecallPeriodBo.DMLClaimRecallPeriod(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }
            return View(viewModel);
        }

        #endregion
        
        #region ClaimRecallPeriod Details
        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimRecallPeriod.ClaimRecallPeriodIndex, CommonValues.PermissionCodes.ClaimRecallPeriod.ClaimRecallPeriodDetails)]
        [HttpGet]
        public ActionResult ClaimRecallPeriodDetails(int claimRecallPeriodId)
        {
            var v = new ClaimRecallPeriodViewModel();
            var claimRecallPeriodBo = new ClaimRecallPeriodBL();

            v.ClaimRecallPeriodId = claimRecallPeriodId;
            claimRecallPeriodBo.GetClaimRecallPeriod(UserManager.UserInfo, v);

            return View(v);
        }

        #endregion
    }
}
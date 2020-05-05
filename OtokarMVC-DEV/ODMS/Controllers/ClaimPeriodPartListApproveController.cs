using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class ClaimPeriodPartListApproveController : ControllerBase
    {
        //
        // GET: /ClaimPeriodPartListApprove/
        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimPeriodPartListApprove.ClaimPeriodPartListApproveIndex)]
        [HttpGet]
        public ActionResult ClaimPeriodPartListApproveIndex()
        {
            ViewBag.ClaimPeriods = new ClaimPeriodPartListApproveBL().GetClaimPeriods().Data;
            return View();
        }
        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimPeriodPartListApprove.ClaimPeriodPartListApproveIndex)]
        [HttpPost]
        public ActionResult List(int id)
        {
            var list = new ClaimPeriodPartListApproveBL().ListClaimPeriodParts(UserManager.UserInfo, id).Data;
            return Json(new { Data = list });
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimPeriodPartListApprove.ClaimPeriodPartListApproveIndex)]
        public ActionResult ClaimDismantlePartInfo(long partId, int claimPeriodId)
        {
            var list = new ClaimPeriodPartListApproveBL().ListDismantledParts(UserManager.UserInfo, claimPeriodId, partId).Data;
            return PartialView(list);
        }
        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimPeriodPartListApprove.ClaimPeriodPartListApproveIndex, CommonValues.PermissionCodes.ClaimPeriodPartListApprove.ClaimPeriodPartListApprovement)]
        public ActionResult Save(int claimPeriodId, List<long> selectedParts, List<long> notSelectedParts)
        {
            var modelBase = new ClaimPeriodPartListApproveBL().Save(UserManager.UserInfo, claimPeriodId, selectedParts, notSelectedParts).Model;
            if (modelBase.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, modelBase.ErrorMessage);
        }
    }
}

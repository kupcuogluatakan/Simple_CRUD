using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSModel.ClaimWaybillList;
using System.Web.Mvc;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class ClaimWaybillListController : ControllerBase
    {
        public void SetDefaults()
        {
            ViewBag.AcceptStatusList = CommonBL.ListAcceptStatus().Data;
        }

        #region ClaimWaybillList Index

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.ClaimWaybill.ClaimWaybillIndex)]
        [HttpGet]
        public ActionResult ClaimWaybillListIndex()
        {
            SetDefaults();
            var model = new ClaimWaybillListListModel();

            return View(model);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.ClaimWaybill.ClaimWaybillIndex, ODMSCommon.CommonValues.PermissionCodes.ClaimWaybill.ClaimWaybillDetails)]
        public ActionResult ListClaimWaybillList([DataSourceRequest] DataSourceRequest request, ClaimWaybillListListModel model)
        {
            SetDefaults();
            var claimWaybillListBo = new ClaimWaybillListBL();

            var v = new ClaimWaybillListListModel(request)
                {
                    IsAccepted = model.IsAccepted,
                    WaybillNo = model.WaybillNo,
                    WaybillSerialNo = model.WaybillSerialNo,
                    WaybillText = model.WaybillText,
                    WaybillDate = model.WaybillDate,
                    AcceptUser = model.AcceptUser,
                    AcceptDate = model.AcceptDate
                };

            var totalCnt = 0;
            var returnValue = claimWaybillListBo.ListClaimWaybillList(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion ClaimWaybillList Index
    }
}
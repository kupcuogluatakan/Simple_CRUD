using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.PDIVehicleResult;
using System.Web.Mvc;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class PDIVehicleResultController : ControllerBase
    {
        private void SetDefaults()
        {
            ViewBag.StatusList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.VehicleResultStatus).Data;
            ViewBag.GroupList = PdiGifApproveGroupBL.ListPdiGifApproveGroupsAsSelectItem(UserManager.UserInfo).Data;
            ViewBag.TypeList = VehicleTypeBL.ListVehicleTypeAsSelectList(UserManager.UserInfo, null).Data;
            ViewBag.ModelKodList = VehicleModelBL.ListVehicleModelAsSelectList(UserManager.UserInfo).Data;
            ViewBag.UserList = CommonBL.ListUserByDealerId(null, null).Data; // Otokar kullanıcıları
        }

        #region PDIVehicleResult Index

        [AuthorizationFilter(CommonValues.PermissionCodes.PDIVehicleResult.PDIVehicleResultIndex)]
        [HttpGet]
        public ActionResult PDIVehicleResultIndex()
        {
            SetDefaults();
            PDIVehicleResultListModel model = new PDIVehicleResultListModel();
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.idDealer = UserManager.UserInfo.GetUserDealerId();
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PDIVehicleResult.PDIVehicleResultIndex)]
        public ActionResult ListPDIVehicleResult([DataSourceRequest] DataSourceRequest request, PDIVehicleResultListModel model)
        {
            var appointmentIndicatorFailureCodeBo = new PDIVehicleResultBL();
            //
            //ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem();
            //

            int tempDealer = 0;
            if (UserManager.UserInfo.GetUserDealerId() == 0)
                tempDealer = model.DealerId;
            else
                tempDealer = UserManager.UserInfo.GetUserDealerId();

            var v = new PDIVehicleResultListModel(request)
            {
                GroupId = model.GroupId,
                TypeId = model.TypeId,
                ModelKod = model.ModelKod,
                VinNo = model.VinNo,
                CustomerId = model.CustomerId,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                StatusId = model.StatusId,
                DealerId = tempDealer
            };

            var totalCnt = 0;
            var returnValue = appointmentIndicatorFailureCodeBo.ListPDIVehicleResult(UserManager.UserInfo,v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region PDIVehicleResultDetail Index

        [AuthorizationFilter(
            CommonValues.PermissionCodes.PDIVehicleResult.PDIVehicleResultIndex,
            CommonValues.PermissionCodes.PDIVehicleResult.PDIVehicleResultDetails)]
        [HttpGet]
        public ActionResult PdiVehicleResultDetailIndex(long id)
        {
            return View(id);
        }

        #endregion


        #region PDIVehicleResultConfirm Index

        [AuthorizationFilter(CommonValues.PermissionCodes.PDIVehicleResult.PDIVehicleResultConfirmIndex)]
        [HttpGet]
        public ActionResult PDIVehicleResultConfirmIndex()
        {
            SetDefaults();
            PDIVehicleResultListModel model = new PDIVehicleResultListModel();

            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PDIVehicleResult.PDIVehicleResultConfirmIndex)]
        public ActionResult ListPDIVehicleResultConfirm([DataSourceRequest] DataSourceRequest request, PDIVehicleResultListModel model)
        {
            var appointmentIndicatorFailureCodeBo = new PDIVehicleResultBL();

            var v = new PDIVehicleResultListModel(request);
            v.GroupId = model.GroupId;
            v.TypeId = model.TypeId;
            v.ModelKod = model.ModelKod;
            v.VinNo = model.VinNo;
            v.CustomerId = model.CustomerId;
            v.StartDate = model.StartDate;
            v.EndDate = model.EndDate;
            v.StatusId = model.StatusId;
            v.ApprovalUserId = model.ApprovalUserId;

            var totalCnt = 0;
            var returnValue = appointmentIndicatorFailureCodeBo.ListPDIVehicleResult(UserManager.UserInfo,v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region PDIVehicleResultDetailConfirm Index
        [AuthorizationFilter(
            CommonValues.PermissionCodes.PDIVehicleResult.PDIVehicleResultConfirmIndex,
            CommonValues.PermissionCodes.PDIVehicleResult.PDIVehicleResultConfirmDetails)]
        [HttpGet]
        public ActionResult PdiVehicleResultDetailConfirmIndex(int id)
        {
            PDIVehicleResultViewModel mo = new PDIVehicleResultViewModel();
            mo.PDIVehicleResultId = id;
            PDIVehicleResultBL bo = new PDIVehicleResultBL();
            bo.GetPDIVehicleResult(UserManager.UserInfo,mo);
            return View(mo);
        }

        [AuthorizationFilter(
            CommonValues.PermissionCodes.PDIVehicleResult.PDIVehicleResultConfirmIndex,
            CommonValues.PermissionCodes.PDIVehicleResult.PDIVehicleResultConfirmDetails,
            CommonValues.PermissionCodes.PDIVehicleResult.PDIVehicleResultConfirmDetailsApprove,
            CommonValues.PermissionCodes.PDIVehicleResult.PDIVehicleResultConfirmDetailsCancel)]
        [HttpPost]
        public ActionResult PdiVehicleResultDetailConfirmIndex(PDIVehicleResultViewModel model)
        {
            PDIVehicleResultBL bo = new PDIVehicleResultBL();
            model.CommandType = CommonValues.DMLType.Update;
            model.ApprovalUserId = UserManager.UserInfo.UserId;

            if (Request.Params["action:Approve"] != null)
            {
                model.StatusId = (int)CommonValues.PDIVehicleResultStatus.Approved;
            }
            if (Request.Params["action:Cancel"] != null)
            {
                model.StatusId = (int)CommonValues.PDIVehicleResultStatus.Cancelled;
            }
            bo.DMLPDIVehicleResult(UserManager.UserInfo,model);
            CheckErrorForMessage(model, true);

            return View(model);
        }

        #endregion

    }
}
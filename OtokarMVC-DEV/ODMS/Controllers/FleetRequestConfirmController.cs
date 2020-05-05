using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.FleetRequestConfirm;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class FleetRequestConfirmController : ControllerBase
    {
        private void SetDefaults()
        {
            List<SelectListItem> statusList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.FleetRequestStatus).Data;
            statusList.RemoveAt(0);
            ViewBag.StatusList = statusList;
        }

        #region FleetRequestConfirm Index
        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.FleetRequestConfirm.FleetRequestConfirmIndex)]
        public ActionResult FleetRequestConfirmIndex()
        {
            SetDefaults();
            return View();
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.FleetRequestConfirm.FleetRequestConfirmIndex, ODMSCommon.CommonValues.PermissionCodes.FleetRequestConfirm.FleetRequestConfirmIndex)]
        public ActionResult ListFleetRequestConfirm([DataSourceRequest]DataSourceRequest request, FleetRequestConfirmListModel model)
        {
            var bo = new FleetRequestConfirmBL();
            var referenceModel = new FleetRequestConfirmListModel(request) { StatusId = model.StatusId };
            int totalCnt;
            var returnValue = bo.ListFleetRequestConfirm(UserManager.UserInfo, referenceModel, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }
        #endregion

        #region FleetRequestConfirm Details
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.FleetRequestConfirm.FleetRequestConfirmIndex, ODMSCommon.CommonValues.PermissionCodes.FleetRequestConfirm.FleetRequestConfirmCreate)]
        public ActionResult FleetRequestConfirmDetails(int fleetrequestId, int status, string rejectDesc)
        {
            var model = new FleetRequestConfirmViewModel { FleetRequestId = fleetrequestId, StatusId = status, RejectDescription = rejectDesc };
            var bo = new FleetRequestConfirmBL();

            model.CommandType = CommonValues.DMLType.Update;
            bo.DMLFleetRequestConfirm(UserManager.UserInfo, model);

            CheckErrorForMessage(model, true);
            ModelState.Clear();

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }
        #endregion
    }
}
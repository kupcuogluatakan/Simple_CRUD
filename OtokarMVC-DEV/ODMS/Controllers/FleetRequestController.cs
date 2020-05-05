using System.Collections.Generic;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.FleetRequest;
using ODMSModel.FleetRequestVehicle;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class FleetRequestController : ControllerBase
    {
        private void SetDefaults()
        {
            ViewBag.StatusList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.FleetRequestStatus).Data;
        }

        #region Fleet Request Index
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.FleetRequest.FleetRequestIndex)]
        public ActionResult FleetRequestIndex()
        {
            SetDefaults();
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.FleetRequest.FleetRequestIndex, CommonValues.PermissionCodes.FleetRequest.FleetRequestDetails)]
        public ActionResult ListFleetRequests([DataSourceRequest]DataSourceRequest request, FleetRequestListModel model)
        {
            var bo = new FleetRequestBL();
            var referenceModel = new FleetRequestListModel(request) { StatusId = model.StatusId };
            int totalCnt;
            var returnValue = bo.ListFleetRequests(UserManager.UserInfo,referenceModel, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }
        #endregion

        #region Fleet Request Create
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.FleetRequest.FleetRequestIndex, CommonValues.PermissionCodes.FleetRequest.FleetRequestCreate)]
        public ActionResult FleetRequestCreate()
        {
            SetDefaults();
            var model = new FleetRequestViewModel
            {
                StatusId = (int)CommonValues.FleetRequestStatus.NewRecord
            };
            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.FleetRequest.FleetRequestIndex, CommonValues.PermissionCodes.FleetRequest.FleetRequestCreate)]
        public ActionResult FleetRequestCreate(FleetRequestViewModel model)
        {
            SetDefaults();
            if (ModelState.IsValid)
            {
                var bo = new FleetRequestBL();
                model.CommandType = CommonValues.DMLType.Insert;
                bo.DMLFleetRequest(UserManager.UserInfo,model);
                CheckErrorForMessage(model, true);
                ModelState.Clear();
                FleetRequestViewModel newModel = new FleetRequestViewModel();
                newModel.StatusId = (int)CommonValues.FleetRequestStatus.NewRecord;
                return View(newModel);
            }
            return View(model);
        }
        #endregion

        #region Fleet Request Update
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.FleetRequest.FleetRequestIndex, CommonValues.PermissionCodes.FleetRequest.FleetRequestUpdate)]
        public ActionResult FleetRequestUpdate(int fleetrequestId = 0)
        {
            SetDefaults();
            var referenceModel = new FleetRequestViewModel();
            if (fleetrequestId > 0)
            {
                var bo = new FleetRequestBL();
                referenceModel.FleetRequestId = fleetrequestId;
                referenceModel = bo.GetFleetRequest(UserManager.UserInfo, referenceModel).Model;
            }
            return View(referenceModel);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.FleetRequest.FleetRequestIndex, CommonValues.PermissionCodes.FleetRequest.FleetRequestUpdate)]
        public ActionResult FleetRequestUpdate(FleetRequestViewModel viewModel)
        {
            SetDefaults();
            var bo = new FleetRequestBL();
            if (ModelState.IsValid)
            {
                if (Request.Params["action:FleetRequestSendApproval"] != null)
                {
                    int totalCount = 0;
                    FleetRequestVehicleListModel vehicleListModel = new FleetRequestVehicleListModel
                    {
                        FleetRequestId = viewModel.FleetRequestId
                    };
                    FleetRequestVehicleBL vehicleBo = new FleetRequestVehicleBL();
                    List<FleetRequestVehicleListModel> vehicleList = vehicleBo.ListFleetRequestVehicle(vehicleListModel, out totalCount).Data;
                    if (totalCount == 0)
                    {
                        SetMessage(MessageResource.FleetRequest_Warning_VehicleNotExists,
                                   CommonValues.MessageSeverity.Fail);
                        return View(viewModel);
                    }
                    else
                    {
                        viewModel.StatusId = (int)CommonValues.FleetRequestStatus.FleetRequestWaitingForApproval;
                    }
                }
                if (Request.Params["action:FleetRequestApprove"] != null)
                {
                    viewModel.StatusId = (int)CommonValues.FleetRequestStatus.FleetRequestApproved;
                }

                viewModel.CommandType = CommonValues.DMLType.Update;
                bo.DMLFleetRequest(UserManager.UserInfo,viewModel);
                CheckErrorForMessage(viewModel, true);
                bo.GetFleetRequest(UserManager.UserInfo, viewModel);
            }
            return View(viewModel);
        }
        #endregion

        #region Fleet Request Delete
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.FleetRequest.FleetRequestIndex, CommonValues.PermissionCodes.FleetRequest.FleetRequestDelete)]
        public ActionResult FleetRequestDelete(int fleetrequestId)
        {
            ViewBag.HideElements = false;

            var bo = new FleetRequestBL();
            var model = new FleetRequestViewModel { FleetRequestId = fleetrequestId, CommandType = CommonValues.DMLType.Delete };
            bo.DMLFleetRequest(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);
            ModelState.Clear();

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }
        #endregion

        #region Fleet Request Send Approval
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.FleetRequest.FleetRequestIndex, CommonValues.PermissionCodes.FleetRequest.FleetRequestDelete)]
        public ActionResult FleetRequestSendApproval(int fleetrequestId)
        {
            ViewBag.HideElements = false;

            var bo = new FleetRequestBL();
            var model = new FleetRequestViewModel { FleetRequestId = fleetrequestId };
            bo.GetFleetRequest(UserManager.UserInfo, model);

            int totalCount = 0;
            FleetRequestVehicleListModel vehicleListModel = new FleetRequestVehicleListModel
            {
                FleetRequestId = fleetrequestId
            };
            FleetRequestVehicleBL vehicleBo = new FleetRequestVehicleBL();
            List<FleetRequestVehicleListModel> vehicleList = vehicleBo.ListFleetRequestVehicle(vehicleListModel, out totalCount).Data;
            if (totalCount == 0)
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.FleetRequest_Warning_VehicleNotExists);
            }
            else
            {
                model.StatusId = (int)CommonValues.FleetRequestStatus.FleetRequestWaitingForApproval;
                model.CommandType = CommonValues.DMLType.Update;
                bo.DMLFleetRequest(UserManager.UserInfo, model);
                CheckErrorForMessage(model, true);
                ModelState.Clear();

                if (model.ErrorNo == 0)
                {
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                                                          MessageResource.Global_Display_Success);
                }
                else
                {
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
                }
            }
        }
        #endregion

        #region Fleet Request Details
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.FleetRequest.FleetRequestIndex, CommonValues.PermissionCodes.FleetRequest.FleetRequestDetails)]
        public ActionResult FleetRequestDetails(int fleetrequestId)
        {
            var referenceModel = new FleetRequestViewModel { FleetRequestId = fleetrequestId };
            var bo = new FleetRequestBL();

            var model = bo.GetFleetRequest(UserManager.UserInfo, referenceModel).Model;

            return View(model);
        }
        #endregion
    }
}

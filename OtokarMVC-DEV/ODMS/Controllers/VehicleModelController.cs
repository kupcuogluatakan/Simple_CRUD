using System.Collections.Generic;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.VehicleModel;
using ODMSBusiness;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class VehicleModelController : ControllerBase
    {
        // Index
        [AuthorizationFilter(CommonValues.PermissionCodes.VehicleModel.VehicleModelIndex)]
        [HttpGet]
        public ActionResult VehicleModelIndex()
        {
            SetComboBox();
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.VehicleModel.VehicleModelIndex, CommonValues.PermissionCodes.VehicleModel.VehicleModelDetails)]
        public JsonResult ListVehicleModel([DataSourceRequest] DataSourceRequest request, VehicleModelListModel vehicleMModel)
        {
            VehicleModelBL vehicleMBL = new VehicleModelBL();
            VehicleModelListModel model_vehicleM = new VehicleModelListModel(request);
            int totalCount = 0;

            model_vehicleM.VehicleGroupId = vehicleMModel.VehicleGroupId;
            model_vehicleM.VehicleGroupName = vehicleMModel.VehicleGroupName;
            model_vehicleM.VehicleModelKod = vehicleMModel.VehicleModelKod;
            model_vehicleM.VehicleModelName = vehicleMModel.VehicleModelName;
            model_vehicleM.VehicleModelSSID = vehicleMModel.VehicleModelSSID;
            model_vehicleM.IsActiveSearch = vehicleMModel.IsActiveSearch;

            var rValue = vehicleMBL.GetVehicleModelList(UserManager.UserInfo,model_vehicleM,out totalCount).Data;

            return Json(new
            {
                Data  = rValue,
                Total = totalCount
            });
        }


        //Details
        [AuthorizationFilter(CommonValues.PermissionCodes.VehicleModel.VehicleModelIndex,CommonValues.PermissionCodes.VehicleModel.VehicleModelDetails)]
        [HttpGet]
        public ActionResult VehicleModelDetails(string id)
        {
            VehicleModelBL vehicleMBL = new VehicleModelBL();
            VehicleModelIndexViewModel model_VehicleM = new VehicleModelIndexViewModel();
            model_VehicleM.VehicleModelKod = id;

            vehicleMBL.GetVehicleModel(UserManager.UserInfo, model_VehicleM);

            return View(model_VehicleM);
        }


        //Delete
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.VehicleModel.VehicleModelIndex, CommonValues.PermissionCodes.VehicleModel.VehicleModelDelete)]
        public ActionResult VehicleModelDelete(string vehicleModelKod)
        {
            VehicleModelBL vehicleMBL = new VehicleModelBL();
            VehicleModelIndexViewModel vehicleMModel = new VehicleModelIndexViewModel();
            vehicleMModel.VehicleModelKod = vehicleModelKod;
            vehicleMModel.CommandType = string.IsNullOrEmpty(vehicleMModel.VehicleModelKod)
                ? ""
                : CommonValues.DMLType.Delete;

            vehicleMBL.DMLVehicleModel(UserManager.UserInfo, vehicleMModel);

            if (vehicleMModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, message: vehicleMModel.ErrorMessage);
        }

        
        //Update
        [AuthorizationFilter(CommonValues.PermissionCodes.VehicleModel.VehicleModelIndex, CommonValues.PermissionCodes.VehicleModel.VehicleModelUpdate)]
        [HttpGet]
        public ActionResult VehicleModelUpdate(string id)
        {
            VehicleModelBL vehicleMBL = new VehicleModelBL();
            VehicleModelIndexViewModel model_VehicleM = new VehicleModelIndexViewModel {VehicleModelKod = id};

            vehicleMBL.GetVehicleModel(UserManager.UserInfo, model_VehicleM);
            return View(model_VehicleM);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.VehicleModel.VehicleModelIndex, CommonValues.PermissionCodes.VehicleModel.VehicleModelUpdate)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VehicleModelUpdate(VehicleModelIndexViewModel vehicleMModel)
        {
            VehicleModelBL vehicleMBL = new VehicleModelBL();
            if (ModelState.IsValid)
            {
                vehicleMModel.CommandType = string.IsNullOrEmpty(vehicleMModel.VehicleModelKod)
                    ? ""
                    : CommonValues.DMLType.Update;
                vehicleMBL.DMLVehicleModel(UserManager.UserInfo, vehicleMModel);
                CheckErrorForMessage(vehicleMModel, true);
            }
            return View(vehicleMModel);
        }

        private void SetComboBox()
        {
            List<SelectListItem> list_VehicleGroup = VehicleGroupBL.ListVehicleGroupAsSelectList(UserManager.UserInfo).Data;
            List<SelectListItem> list_SelectList = CommonBL.ListStatus().Data;
            ViewBag.IASelectList = list_SelectList;
            ViewBag.VGSelectList = list_VehicleGroup;

        }

    }
}

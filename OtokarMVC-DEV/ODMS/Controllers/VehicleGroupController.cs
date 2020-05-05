using System.Collections.Generic;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSCommon.Resources;
using ODMSModel.VehicleGroup;
using ODMSBusiness;
using ODMSModel.ViewModel;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class VehicleGroupController : ControllerBase
    {
        // Index
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.VehicleGroup.VehicleGroupIndex)]
        [HttpGet]
        public ActionResult VehicleGroupIndex()
        {
            SetComboBox();
            return View();
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.VehicleGroup.VehicleGroupIndex, ODMSCommon.CommonValues.PermissionCodes.VehicleGroup.VehicleGroupDetails)]
        public JsonResult ListVehicleGroup([DataSourceRequest] DataSourceRequest request, VehicleGroupListModel vehicleModel)
        {
            VehicleGroupBL vehicleGBL = new VehicleGroupBL();
            VehicleGroupListModel model_vehicleG = new VehicleGroupListModel(request);
            int totalCount = 0;

            model_vehicleG.IsActiveSearch = vehicleModel.IsActiveSearch;
            model_vehicleG.VehicleGroupId = vehicleModel.VehicleGroupId;
            model_vehicleG.AdminDesc = vehicleModel.AdminDesc;
            model_vehicleG.VehicleGroupName = vehicleModel.GroupName;
            var rValue = vehicleGBL.GetVehicleGroupList(UserManager.UserInfo, model_vehicleG, out totalCount).Data;

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });
        }


        //Details
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.VehicleGroup.VehicleGroupIndex, ODMSCommon.CommonValues.PermissionCodes.VehicleGroup.VehicleGroupDetails)]
        [HttpGet]
        public ActionResult VehicleGroupDetails(int id = 0)
        {
            VehicleGroupBL vehicleGBL = new VehicleGroupBL();
            VehicleGroupIndexViewModel model_VehicleG = new VehicleGroupIndexViewModel();
            model_VehicleG.VehicleGroupId = id;
            vehicleGBL.GetVehicleGroup(UserManager.UserInfo, model_VehicleG);

            return View(model_VehicleG);
        }


        //Delete
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.VehicleGroup.VehicleGroupIndex, ODMSCommon.CommonValues.PermissionCodes.VehicleGroup.VehicleGroupDelete)]
        public ActionResult VehicleGroupDelete(int id)
        {
            VehicleGroupBL vehicleGBL = new VehicleGroupBL();
            VehicleGroupIndexViewModel vehicleModel = new VehicleGroupIndexViewModel();
            vehicleModel.CommandType = id > 0 ? ODMSCommon.CommonValues.DMLType.Delete : string.Empty;
            vehicleModel.VehicleGroupId = id;

            vehicleGBL.DMLVehicleGroup(UserManager.UserInfo, vehicleModel);

            if (vehicleModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                vehicleModel.ErrorMessage);

        }


        //Update
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.VehicleGroup.VehicleGroupIndex, ODMSCommon.CommonValues.PermissionCodes.VehicleGroup.VehicleGroupUpdate)]
        [HttpGet]
        public ActionResult VehicleGroupUpdate(int id = 0)
        {
            VehicleGroupBL vehicleGBL = new VehicleGroupBL();
            VehicleGroupIndexViewModel model_VehicleG = new VehicleGroupIndexViewModel {VehicleGroupId = id};

            vehicleGBL.GetVehicleGroup(UserManager.UserInfo, model_VehicleG);

            SetComboBox();
            return View(model_VehicleG);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.VehicleGroup.VehicleGroupIndex, ODMSCommon.CommonValues.PermissionCodes.VehicleGroup.VehicleGroupUpdate)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VehicleGroupUpdate(VehicleGroupIndexViewModel vehicleModel)
        {

            VehicleGroupBL vehicleGBL = new VehicleGroupBL();
            if (ModelState.IsValid)
            {
                vehicleModel.CommandType = vehicleModel.VehicleGroupId > 0 ? ODMSCommon.CommonValues.DMLType.Update : "";
                vehicleGBL.DMLVehicleGroup(UserManager.UserInfo, vehicleModel);
                if (!CheckErrorForMessage(vehicleModel, true))
                {
                    vehicleModel.VehicleGroupName = (MultiLanguageModel)ODMSCommon.CommonUtility.DeepClone(vehicleModel.VehicleGroupName);
                    vehicleModel.VehicleGroupName.MultiLanguageContentAsText = vehicleModel.MultiLanguageContentAsText;
                }
            }

            SetComboBox();
            return View(vehicleModel);
        }

        private void SetComboBox()
        {
            List<SelectListItem> listActive = CommonBL.ListStatus().Data;
            List<SelectListItem> listVatRatio = CommonBL.ListVatRatio().Data;
            ViewBag.VRSelectList = listVatRatio;
            ViewBag.IASelectList = listActive;
            ViewBag.VehicleGroupList = VehicleGroupBL.ListVehicleGroupAsSelectList(UserManager.UserInfo).Data;
        }

    }
}

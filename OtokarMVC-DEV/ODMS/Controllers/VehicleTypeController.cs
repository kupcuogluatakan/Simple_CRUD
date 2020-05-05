using System.Collections.Generic;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon.Resources;
using ODMSModel.VehicleType;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class VehicleTypeController : ControllerBase
    {
        //
        //Index
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.VehicleType.VehicleTypeIndex)]
        [HttpGet]
        public ActionResult VehicleTypeIndex()
        {
            SetComboBox();
            return View();
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.VehicleType.VehicleTypeIndex, ODMSCommon.CommonValues.PermissionCodes.VehicleType.VehicleTypeDetails)]
        public JsonResult ListVehicleType([DataSourceRequest] DataSourceRequest request, VehicleTypeListModel vehicleTModel)
        {
            VehicleTypeBL vehicleTBL = new VehicleTypeBL();
            VehicleTypeListModel model_Vehicle = new VehicleTypeListModel(request);
            int totalCount = 0;

            model_Vehicle.IsActiveSearch = vehicleTModel.IsActiveSearch;
            model_Vehicle.ModelKod = vehicleTModel.ModelKod;
            model_Vehicle.TypeSSID = vehicleTModel.TypeSSID;
            model_Vehicle.VehicleGroupId = vehicleTModel.VehicleGroupId;
            model_Vehicle.TypeId = vehicleTModel.TypeId;
            var rValue = vehicleTBL.GetVehicleTypeList(UserManager.UserInfo, model_Vehicle, out totalCount).Data;

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });
        }


        //Details
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.VehicleType.VehicleTypeIndex, ODMSCommon.CommonValues.PermissionCodes.VehicleType.VehicleTypeDetails)]
        [HttpGet]
        public ActionResult VehicleTypeDetails(int id = 0)
        {
            VehicleTypeIndexViewModel model_VehicleT = new VehicleTypeIndexViewModel();
            VehicleTypeBL vehicleTBL = new VehicleTypeBL();

            model_VehicleT.TypeId = id;
            vehicleTBL.GetVehicleType(UserManager.UserInfo, model_VehicleT);

            return View(model_VehicleT);
        }


        //Delete
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.VehicleType.VehicleTypeIndex, ODMSCommon.CommonValues.PermissionCodes.VehicleType.VehicleTypeDelete)]
        public ActionResult VehicleTypeDelete(int typeId)
        {
            VehicleTypeBL vehicleTBL = new VehicleTypeBL();
            VehicleTypeIndexViewModel vehicleTModel = new VehicleTypeIndexViewModel();
            vehicleTModel.TypeId = typeId;
            vehicleTModel.CommandType = vehicleTModel.TypeId > 0 ? ODMSCommon.CommonValues.DMLType.Delete : "";

            vehicleTBL.DMLVehicleType(UserManager.UserInfo, vehicleTModel);

            if (vehicleTModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                vehicleTModel.ErrorMessage);

        }


        //Update
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.VehicleType.VehicleTypeIndex, ODMSCommon.CommonValues.PermissionCodes.VehicleType.VehicleTypeUpdate)]
        [HttpGet]
        public ActionResult VehicleTypeUpdate(int id = 0)
        {
            VehicleTypeIndexViewModel model_VehicleT = new VehicleTypeIndexViewModel();
            VehicleTypeBL vehicleTBL = new VehicleTypeBL();

            model_VehicleT.TypeId = id;
            vehicleTBL.GetVehicleType(UserManager.UserInfo, model_VehicleT);

            return View(model_VehicleT);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.VehicleType.VehicleTypeIndex, ODMSCommon.CommonValues.PermissionCodes.VehicleType.VehicleTypeUpdate)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VehicleTypeUpdate(VehicleTypeIndexViewModel vehicleTModel)
        {
            VehicleTypeBL vehicleTBL = new VehicleTypeBL();
            vehicleTModel.CommandType = vehicleTModel.TypeId > 0 ? ODMSCommon.CommonValues.DMLType.Update : "";

            if (ModelState.IsValid)
            {
                vehicleTBL.DMLVehicleType(UserManager.UserInfo, vehicleTModel);
                CheckErrorForMessage(vehicleTModel, true);
            }

            return View(vehicleTModel);
        }

        private void SetComboBox()
        {
            List<SelectListItem> list_VehicleM = VehicleModelBL.ListVehicleModelAsSelectList(UserManager.UserInfo).Data;
            List<SelectListItem> list_SelectList = CommonBL.ListStatus().Data;
            ViewBag.VGSelectList = VehicleGroupBL.ListVehicleGroupAsSelectList(UserManager.UserInfo).Data;
            ViewBag.VTSelectList = VehicleTypeBL.ListVehicleTypeAsSelectList(UserManager.UserInfo, null).Data;
            ViewBag.IASelectList = list_SelectList;
            ViewBag.VMSelectList = list_VehicleM;
        }

    }
}

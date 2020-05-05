using System.Collections.Generic;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSCommon.Resources;
using ODMSModel.VehicleCode;
using ODMSBusiness;
using ODMSModel.ViewModel;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class VehicleCodeController : ControllerBase
    {
        //Index
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.VehicleCode.VehicleCodeIndex)]
        [HttpGet]
        public ActionResult VehicleCodeIndex()
        {
            SetComboBox();
            return View();
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.VehicleCode.VehicleCodeIndex, ODMSCommon.CommonValues.PermissionCodes.VehicleCode.VehicleCodeDetails)]
        public JsonResult ListVehicleCode([DataSourceRequest] DataSourceRequest request, VehicleCodeListModel vehicleCModel)
        {
            VehicleCodeBL vehicleCBL = new VehicleCodeBL();
            VehicleCodeListModel model_VehicleC = new VehicleCodeListModel(request);
            int totalCount = 0;

            model_VehicleC.IsActiveSearch = vehicleCModel.IsActiveSearch;
            model_VehicleC.VehicleCodeKod = vehicleCModel.VehicleCodeKod;
            model_VehicleC.VehicleTypeId = vehicleCModel.VehicleTypeId;
            model_VehicleC.VehicleCodeName = vehicleCModel.VehicleCodeName;
            model_VehicleC.VehicleCodeSSID = vehicleCModel.VehicleCodeSSID;
            model_VehicleC.VehicleTypeName = vehicleCModel.VehicleTypeName;
            model_VehicleC.ModelName = vehicleCModel.ModelName;
            model_VehicleC.EngineType = vehicleCModel.EngineType;
            model_VehicleC.VehicleGroupId = vehicleCModel.VehicleGroupId;
            model_VehicleC.VehicleCode = vehicleCModel.VehicleCode;
            var rValue = vehicleCBL.GetVehicleCodeList(UserManager.UserInfo, model_VehicleC, out totalCount).Data;

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });
        }


        //Details
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.VehicleCode.VehicleCodeIndex,ODMSCommon.CommonValues.PermissionCodes.VehicleCode.VehicleCodeDetails)]
        [HttpGet]
        public ActionResult VehicleCodeDetails(string id)
        {
            VehicleCodeBL vehicleCBL = new VehicleCodeBL();
            VehicleCodeIndexViewModel model_VehicleC = new VehicleCodeIndexViewModel();
            model_VehicleC.VehicleCodeKod = id;

            vehicleCBL.GetVehicleCode(UserManager.UserInfo, model_VehicleC);

            return View(model_VehicleC);
        }


        //Delete
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.VehicleCode.VehicleCodeIndex, ODMSCommon.CommonValues.PermissionCodes.VehicleCode.VehicleCodeDelete)]
        public ActionResult VehicleCodeDelete(string vehicleCodeKod)
        {
            VehicleCodeBL vehicleCBL = new VehicleCodeBL();
            VehicleCodeIndexViewModel vehicleCModel = new VehicleCodeIndexViewModel();
            vehicleCModel.VehicleCodeKod = vehicleCodeKod;
            vehicleCModel.CommandType = string.IsNullOrEmpty(vehicleCModel.VehicleCodeKod) ? string.Empty : ODMSCommon.CommonValues.DMLType.Delete;
            
            vehicleCBL.DMLVehicleCode(UserManager.UserInfo, vehicleCModel);

            if (vehicleCModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                vehicleCModel.ErrorMessage);

        }


        //Update
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.VehicleCode.VehicleCodeIndex, ODMSCommon.CommonValues.PermissionCodes.VehicleCode.VehicleCodeUpdate)]
        [HttpGet]
        public ActionResult VehicleCodeUpdate(string id)
        {
            VehicleCodeBL vehicleCBL = new VehicleCodeBL();
            VehicleCodeIndexViewModel model_VehicleC = new VehicleCodeIndexViewModel {VehicleCodeKod = id};

            vehicleCBL.GetVehicleCode(UserManager.UserInfo, model_VehicleC);

            return View(model_VehicleC);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.VehicleCode.VehicleCodeIndex, ODMSCommon.CommonValues.PermissionCodes.VehicleCode.VehicleCodeUpdate)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VehicleCodeUpdate(VehicleCodeIndexViewModel vehicleCModel)
        {
            VehicleCodeBL vehicleCBL = new VehicleCodeBL();
            if (ModelState.IsValid)
            {
                vehicleCModel.CommandType = string.IsNullOrEmpty(vehicleCModel.VehicleCodeKod) ? "" : ODMSCommon.CommonValues.DMLType.Update;
                vehicleCBL.DMLVehicleCode(UserManager.UserInfo, vehicleCModel);
                if (!CheckErrorForMessage(vehicleCModel, true))
                {
                    vehicleCModel.VehicleCodeName = (MultiLanguageModel)ODMSCommon.CommonUtility.DeepClone(vehicleCModel.VehicleCodeName);
                    vehicleCModel.VehicleCodeName.MultiLanguageContentAsText = vehicleCModel.MultiLanguageContentAsText;
                }
            }


            return View(vehicleCModel);
        }


        private void SetComboBox()
        {
            List<SelectListItem> list_SelectItem = VehicleTypeBL.ListVehicleTypeAsSelectList(UserManager.UserInfo, null).Data;
            List<SelectListItem> list_SelectList = CommonBL.ListStatus().Data;
            ViewBag.IASelectList = list_SelectList;
            ViewBag.VTSelectList = list_SelectItem;
        }


    }
}

using System.Collections.Generic;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using System.Linq;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.VehicleBodywork;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class VehicleBodyworkController : ControllerBase
    {
        private void SetDefaults()
        {
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.CountryList = CommonBL.ListCountries(UserManager.UserInfo).Data;
            ViewBag.BodyworkList = CommonBL.ListBodyWorks(UserManager.UserInfo).Data;
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
        }
        [HttpGet]
        public JsonResult ListWorkOrders(int? vehicleId, int? dealerId)
        {
            return vehicleId.HasValue ? Json(WorkOrderBL.ListWorkOrderAsSelectListItem(vehicleId, dealerId).Data, JsonRequestBehavior.AllowGet) : Json(null, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListCities(int? countryId)
        {
            return countryId.HasValue ? Json(CommonBL.ListCities(UserManager.UserInfo, countryId.Value).Data, JsonRequestBehavior.AllowGet) : Json(null, JsonRequestBehavior.AllowGet);
        }

        #region VehicleBodywork Index

        [AuthorizationFilter(CommonValues.PermissionCodes.VehicleBodywork.VehicleBodyworkIndex)]
        [HttpGet]
        public ActionResult VehicleBodyworkIndex()
        {
            SetDefaults();
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.VehicleBodywork.VehicleBodyworkIndex, CommonValues.PermissionCodes.VehicleBodywork.VehicleBodyworkDetails)]
        public ActionResult ListVehicleBodywork([DataSourceRequest] DataSourceRequest request, VehicleBodyworkListModel model)
        {
            var vehicleBodyworkBo = new VehicleBodyworkBL();

            var v = new VehicleBodyworkListModel(request);

            v.BodyworkCode = model.BodyworkCode;
            v.VehicleId = model.VehicleId;
            v.WorkOrderId = model.WorkOrderId;
            v.CityId = model.CityId;
            v.DealerId = model.DealerId;

            var totalCnt = 0;
            var returnValue = vehicleBodyworkBo.ListVehicleBodywork(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region VehicleBodywork Create

        [AuthorizationFilter(CommonValues.PermissionCodes.VehicleBodywork.VehicleBodyworkIndex, CommonValues.PermissionCodes.VehicleBodywork.VehicleBodyworkCreate)]
        public ActionResult VehicleBodyworkCreate()
        {
            SetDefaults();

            VehicleBodyworkViewModel model = new VehicleBodyworkViewModel();
            if (UserManager.UserInfo.GetUserDealerId() > 0)
            {
                model.DealerId = UserManager.UserInfo.GetUserDealerId();
            }
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.VehicleBodywork.VehicleBodyworkIndex, CommonValues.PermissionCodes.VehicleBodywork.VehicleBodyworkCreate)]
        [HttpPost]
        public ActionResult VehicleBodyworkCreate(VehicleBodyworkViewModel viewModel)
        {
            SetDefaults();
            var vehicleBodyworkBo = new VehicleBodyworkBL();

            VehicleBodyworkListModel viewControlModel = new VehicleBodyworkListModel();
            int totalCount = 0;
            viewControlModel.VehicleId = viewModel.VehicleId;
            vehicleBodyworkBo.ListVehicleBodywork(UserManager.UserInfo, viewControlModel, out totalCount);
            
            if (totalCount == 0)
            {
                if (ModelState.IsValid)
                {
                    viewModel.CommandType = CommonValues.DMLType.Insert;
                    vehicleBodyworkBo.DMLVehicleBodywork(UserManager.UserInfo, viewModel);
                    CheckErrorForMessage(viewModel, true);
                    ModelState.Clear();
                }
                VehicleBodyworkViewModel model = new VehicleBodyworkViewModel {DealerId = viewModel.DealerId};
                return View(model);
            }
            else
            {
                SetMessage(MessageResource.VehicleBodywork_Warning_VehicleExist, CommonValues.MessageSeverity.Fail);

                return View(viewModel);
            }
        }

        #endregion

        #region VehicleBodywork Update
        [AuthorizationFilter(CommonValues.PermissionCodes.VehicleBodywork.VehicleBodyworkIndex, CommonValues.PermissionCodes.VehicleBodywork.VehicleBodyworkUpdate)]
        [HttpGet]
        public ActionResult VehicleBodyworkUpdate(int vehicleBodyworkId)
        {
            SetDefaults();
            var v = new VehicleBodyworkViewModel();

            if (vehicleBodyworkId != 0)
            {
                var vehicleBodyworkBo = new VehicleBodyworkBL();
                v.VehicleBodyworkId = vehicleBodyworkId;

                vehicleBodyworkBo.GetVehicleBodywork(UserManager.UserInfo, v);
            }
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.VehicleBodywork.VehicleBodyworkIndex,
            CommonValues.PermissionCodes.VehicleBodywork.VehicleBodyworkUpdate)]
        [HttpPost]
        public ActionResult VehicleBodyworkUpdate(VehicleBodyworkViewModel viewModel)
        {
            SetDefaults();
            var vehicleBodyworkBo = new VehicleBodyworkBL();
            VehicleBodyworkListModel viewControlModel = new VehicleBodyworkListModel();
            int totalCount = 0;
            viewControlModel.VehicleId = viewModel.VehicleId;
            List<VehicleBodyworkListModel> list = vehicleBodyworkBo.ListVehicleBodywork(UserManager.UserInfo, viewControlModel, out totalCount).Data;
            list = (from r in list.AsEnumerable()
                    where r.VehicleBodyworkId != viewModel.VehicleBodyworkId
                    select r).ToList<VehicleBodyworkListModel>();

            if (!list.Any())
            {
                if (ModelState.IsValid)
                {
                    viewModel.CommandType = CommonValues.DMLType.Update;
                    viewModel.DealerId = UserManager.UserInfo.GetUserDealerId();
                    vehicleBodyworkBo.DMLVehicleBodywork(UserManager.UserInfo, viewModel);
                    CheckErrorForMessage(viewModel, true);

                    if (viewModel.ErrorNo == 0)
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                                                                   MessageResource.Global_Display_Success);
                }
            }
            else
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                                                           MessageResource.VehicleBodywork_Warning_VehicleExist);
            }

            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                                                       viewModel.ErrorMessage);
        }

        #endregion

        #region VehicleBodywork Delete

        [AuthorizationFilter(CommonValues.PermissionCodes.VehicleBodywork.VehicleBodyworkIndex,
            CommonValues.PermissionCodes.VehicleBodywork.VehicleBodyworkDelete)]
        [HttpPost]
        public ActionResult DeleteVehicleBodywork(int? vehicleBodyworkId)
        {
            VehicleBodyworkViewModel viewModel = new VehicleBodyworkViewModel
                {
                    VehicleBodyworkId = vehicleBodyworkId.GetValue<int>()
                };

            var vehicleBodyworkBo = new VehicleBodyworkBL();
            viewModel.CommandType = CommonValues.DMLType.Delete;
            vehicleBodyworkBo.DMLVehicleBodywork(UserManager.UserInfo, viewModel);
            ModelState.Clear();

            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                                                           MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                                                       viewModel.ErrorMessage);
        }

        #endregion

        #region VehicleBodywork Details
        [AuthorizationFilter(CommonValues.PermissionCodes.VehicleBodywork.VehicleBodyworkIndex, CommonValues.PermissionCodes.VehicleBodywork.VehicleBodyworkDetails)]
        [HttpGet]
        public ActionResult VehicleBodyworkDetails(int? vehicleBodyworkId)
        {
            var v = new VehicleBodyworkViewModel();
            var vehicleBodyworkBo = new VehicleBodyworkBL();

            v.VehicleBodyworkId = vehicleBodyworkId.GetValue<int>();

            SetDefaults();
            vehicleBodyworkBo.GetVehicleBodywork(UserManager.UserInfo, v);

            return View(v);
        }

        #endregion
    }
}

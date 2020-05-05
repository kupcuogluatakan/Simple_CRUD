using System;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.Dealer;
using ODMSModel.DealerVehicleGroup;
using Permission = ODMSCommon.CommonValues.PermissionCodes.Dealer;
using DealerVehicleGroupsPermission = ODMSCommon.CommonValues.PermissionCodes.DealerVehicleGroups;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class DealerController : ControllerBase
    {
        #region Get actions
        [HttpGet]
        [AuthorizationFilter(Permission.DealerIndex)]
        public ActionResult DealerIndex()
        {
            var bus = new DealerBL();
            ViewBag.DealerRegionList = bus.ListDealerRegions().Data;
            ViewBag.CountryList = bus.ListCountries(UserManager.UserInfo).Data;

            var purchaseOrderGroupService = new PurchaseOrderGroupBL();
            ViewBag.PurchaseOrderGroupList = purchaseOrderGroupService.PurchaseOrderGroupList().Data;

            return View();
        }
        [HttpGet]
        [AuthorizationFilter(Permission.DealerIndex, Permission.DealerCreate)]
        public ActionResult DealerCreate()
        {
            FillDropDownListData();
            
            DealerViewModel model = new DealerViewModel();
            model.IsActive = true;
            model.DealerRegionId = null;
            model.Country = null;
            model.Longitude = "0";
            model.Latitude = "0";

            return View(model);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.DealerIndex, Permission.DealerUpdate)]
        public ActionResult DealerUpdate(int id)
        {
            FillDropDownListData();
            return View(new DealerBL().GetDealer(UserManager.UserInfo, id).Model);
        }
        [HttpGet]
        [AuthorizationFilter(Permission.DealerIndex, Permission.DealerDetails)]
        public ActionResult DealerDetails(int id)
        {
            return View(new DealerBL().GetDealer(UserManager.UserInfo, id).Model);
        }
        [AuthorizationFilter(Permission.DealerIndex)]
        public JsonResult ListDealers([DataSourceRequest]DataSourceRequest request, DealerViewModel viewModel)
        {
            var bus = new DealerBL();
            var model = new DealerListModel(request);
            model.CityId = viewModel.City ?? 0;
            model.City = viewModel.City.HasValue ? viewModel.CityName : viewModel.ForeignCity;
            model.CountryId = viewModel.Country.GetValue<int>();
            model.DealerRegionId = viewModel.DealerRegionId.GetValue<int>();
            model.IsElectronicInvoiceEnabled = viewModel.IsElectronicInvoiceEnabled;
            model.AcceptOrderProposal = viewModel.AcceptOrderProposalSearch;
            if (!string.IsNullOrEmpty(viewModel.Status))
            {
                model.SearchIsActive = Convert.ToBoolean(Convert.ToInt16(viewModel.Status));
            }
            else
            {
                model.SearchIsActive = null;
            }
            model.PurchaseOrderGroupId = viewModel.PurchaseOrderGroupId;
            model.IsSaleDealer = viewModel.IsSaleDealer;
            model.SSID = viewModel.SSID;
            var totalCnt = 0;
            var returnValue = bus.ListDealersGrid(UserManager.UserInfo, model, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListCities(int? id)
        {
            return id.HasValue ? Json(new DealerBL().ListCities(UserManager.UserInfo, id.Value).Data, JsonRequestBehavior.AllowGet) : Json(null, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListTowns(int? id)
        {
            return id.HasValue ? Json(new DealerBL().ListTowns(UserManager.UserInfo, id.Value).Data, JsonRequestBehavior.AllowGet) : Json(null, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Post actions

        [HttpPost]
        [AuthorizationFilter(Permission.DealerIndex, CommonValues.PermissionCodes.Permission.PermissionCreate)]
        [ValidateAntiForgeryToken]
        public ActionResult DealerCreate(DealerViewModel model)
        {
            if (model.DealerRegionId == CommonBL.GetGeneralParameterValue(CommonValues.GeneralParameters.DealerRegionAbroad).Model.ToString().GetValue<int>())
            {
                ModelState.Remove("ContactNameSurname");
                ModelState.Remove("TaxNo");
                ModelState.Remove("CustomerGroup");
                ModelState.Remove("Phone");
                ModelState.Remove("ContactEmail");
                ModelState.Remove("CustomerGroupLookVal");
            }


            if (ModelState.IsValid == false)
            {
                FillDropDownListData();//bad practice
                return View(model);
            }
            var bus = new DealerBL();
            model.CommandType = CommonValues.DMLType.Insert;
            model.IsSaleDealer = model.PurchaseOrderGroupId.GetValue<int>() == (int)CommonValues.PurchaseOrderGroupType.VehicleSaleService;
            bus.DMLDealer(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);
            ModelState.Clear();
            return RedirectToAction("DealerCreate");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(Permission.DealerIndex, CommonValues.PermissionCodes.Permission.PermissionDelete)]
        public ActionResult DealerDelete(int id)
        {
            var bus = new DealerBL();
            var model = new DealerViewModel { DealerId = id, CommandType = CommonValues.DMLType.Delete };
            bus.DMLDealer(UserManager.UserInfo, model);
            ViewBag.HideFormElements = false;
            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,  model.ErrorMessage);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.DealerIndex, CommonValues.PermissionCodes.Permission.PermissionUpdate)]
        [ValidateAntiForgeryToken]
        public ActionResult DealerUpdate(DealerViewModel model)
        { 
            if (model.DealerRegionId == CommonBL.GetGeneralParameterValue(CommonValues.GeneralParameters.DealerRegionAbroad).Model.ToString().GetValue<int>())
            {
                ModelState.Remove("ContactNameSurname");
                ModelState.Remove("TaxNo");
                ModelState.Remove("CustomerGroup");
                ModelState.Remove("Phone");
                ModelState.Remove("ContactEmail");
                ModelState.Remove("CustomerGroupLookVal");
            }
            FillDropDownListData();
            if (ModelState.IsValid == false)
                return View(model);
            var bus = new DealerBL();
            model.CommandType = CommonValues.DMLType.Update;
            model.IsSaleDealer = model.IsSaleDealer ? model.IsSaleDealer :
                model.PurchaseOrderGroupId.GetValue<int>() == (int)CommonValues.PurchaseOrderGroupType.VehicleSaleService;
            bus.DMLDealer(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);
            return View(model);

        }

        #endregion

        #region DealerVehicleGroups

        [HttpGet]
        [AuthorizationFilter(DealerVehicleGroupsPermission.DealerVehicleGroupIndex)]
        public ActionResult DealerVehicleGroups(int id = 0)
        {
            var bus = new DealerBL();

            ViewBag.DealerId = id;
            var dealerList = DealerBL.ListDealerAsSelectListItem().Data;
            var item = dealerList.Find(c => c.Value == id.ToString());
            if (item == null)
                ViewBag.HideElements = true;
            else
            {
                ViewBag.HideElements = false;
                item.Selected = true;
                ViewBag.DealerList = dealerList;
                ViewBag.VehicleGroupsList = bus.ListVehicleGroupsAsSelectListItem(UserManager.UserInfo).Data;
                ViewBag.VehicleModelList = VehicleModelBL.ListVehicleModelAsSelectList(UserManager.UserInfo).Data;
            }
            DealerVehicleGroupViewModel model = new DealerVehicleGroupViewModel();
            model.DealerId = id;
            return PartialView(model);
        }
        [HttpGet]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(DealerVehicleGroupsPermission.DealerVehicleGroupIndex)]
        public ActionResult DeleteDealerVehicleGroups(int Id_Dealer, int Id_VehicleGroup)
        {
            var bus = new DealerBL();
            ViewBag.HideElements = false;
            var model = bus.GetDealerVehicleGroup(UserManager.UserInfo, Id_Dealer, Id_VehicleGroup).Model;
            return View(model);
        }

        [AuthorizationFilter(DealerVehicleGroupsPermission.DealerVehicleGroupIndex)]
        public JsonResult ListDealerVehicleGroups([DataSourceRequest]DataSourceRequest request, int dealerId)
        {
            int totalCnt = 0;
            var returnValue = new DealerBL().ListDealerVehicleGroups(UserManager.UserInfo, new DealerVehicleGroupsListModel(request) { DealerId = dealerId }, out totalCnt).Data;
            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [AuthorizationFilter(DealerVehicleGroupsPermission.DealerVehicleGroupIndex, DealerVehicleGroupsPermission.DealerVehicleGroupSave)]
        public ActionResult DealerVehicleGroups(DealerVehicleGroupViewModel model)
        {
            var bus = new DealerBL();
            ViewBag.DealerId = model.DealerId;
            var dealerList = DealerBL.ListDealerAsSelectListItem().Data;
            var item = dealerList.Find(c => c.Value == model.DealerId.ToString());
            ViewBag.HideElements = item != null;
            if (item != null) item.Selected = true;

            ViewBag.DealerList = dealerList;
            ViewBag.VehicleGroupsList = bus.ListVehicleGroupsAsSelectListItem(UserManager.UserInfo).Data;
            ViewBag.VehicleModelList = VehicleModelBL.ListVehicleModelAsSelectList(UserManager.UserInfo).Data;

            if (!ModelState.IsValid)
                return View(model);

            model.CommandType = CommonValues.DMLType.Insert;
            bus.DMLDealerVehicleGroups(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);
            ModelState.Clear();

            return Json(1);

        }
        [HttpPost]
        [AuthorizationFilter(DealerVehicleGroupsPermission.DealerVehicleGroupIndex, DealerVehicleGroupsPermission.DealerVehicleGroupDelete)]
        public ActionResult DeleteDealerVehicleGroups(DealerVehicleGroupViewModel model)
        {
            ViewBag.HideElements = false;
            var bus = new DealerBL();
            model.CommandType = CommonValues.DMLType.Delete;
            bus.DMLDealerVehicleGroups(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);
            ModelState.Clear();

            return model.ErrorNo == 0 ? GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success) :
                                        GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        [HttpGet]
        public JsonResult ListVehicleModel(int vehicleGroupId)
        {
            return vehicleGroupId != 0 ? Json(VehicleModelBL.ListVehicleModelAsSelectList(UserManager.UserInfo, vehicleGroupId).Data, JsonRequestBehavior.AllowGet) : Json(null, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Private Methods


        /// <summary>
        /// fills ViewBag data for DropdownLists
        /// </summary>
        private void FillDropDownListData()
        {
            var bus = new DealerBL();
            var purchaseOrderGroupService = new PurchaseOrderGroupBL();
            ViewBag.DealerRegionList = bus.ListDealerRegions().Data;
            ViewBag.CountryList = bus.ListCountries(UserManager.UserInfo).Data;
            ViewBag.CurrencyList = bus.ListCurrencies(UserManager.UserInfo).Data;
            ViewBag.CustomerGroupLookValList = bus.CustomerGroupLookVal(UserManager.UserInfo).Data;
            ViewBag.DealerClassList = bus.ListDealerClassesAsSelectListItem().Data;
            ViewBag.DealerSaleChannels = bus.ListDealerSaleChannels().Data;
            ViewBag.PurchaseOrderGroupList = purchaseOrderGroupService.PurchaseOrderGroupList().Data;
        }

        #endregion
    }
}


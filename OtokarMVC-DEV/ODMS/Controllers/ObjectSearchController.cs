using System.Collections.Generic;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSModel.ObjectSearch;
using ODMSModel.PurchaseOrderDetail;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    public class ObjectSearchController : ControllerBase
    {
        [HttpGet]
        public ActionResult ObjectSearch(ObjectSearchModel model)
        {
            return PartialView(model);
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.CustomerSearch.CustomerSearchIndex)]
        public ActionResult CustomerSearch(CustomerSearchViewModel referenceModel)
        {
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.CountryList = CommonBL.ListCountries(UserManager.UserInfo).Data;
            ViewBag.CustomerTypeList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.CustomerTypeLookup).Data;
            ViewBag.YesNoList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.YesNo).Data;
            if (UserManager.UserInfo.GetUserDealerId() != 0)
                referenceModel.DealerId = UserManager.UserInfo.GetUserDealerId();
            return View(referenceModel);
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.AppointmentIndicatorSubCategorySearch.AppointmentIndicatorSubCategorySearchIndex)]
        public ActionResult AppointmentIndicatorSubCategorySearch(AppointmentIndicatorSubCategorySearchViewModel referenceModel)
        {
            return PartialView(referenceModel);
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.VehicleSearch.VehicleSearchIndex)]
        public ActionResult VehicleSearch(VehicleSearchViewModel referenceModel)
        {
            List<SelectListItem> vehicleModelList = VehicleModelBL.ListVehicleModelAsSelectList(UserManager.UserInfo).Data;
            ViewBag.VehicleModelList = vehicleModelList;
            return View(referenceModel);
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.AppointmentSearch.AppointmentSearchIndex)]
        public ActionResult AppointmentSearch(AppointmentSearchViewModel referenceModel)
        {
            return View(referenceModel);
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.FleetSearch.FleetSearchIndex)]
        public ActionResult FleetSearch(FleetSearchViewModel referenceModel)
        {
            ViewBag.YesNoList = CommonBL.ListYesNo().Data;

            return View(referenceModel);
        }

        [HttpGet]
        public ActionResult PurchaseOrderSearch(PurchaseOrderSearchViewModel referenceModel)
        {
            ViewBag.StockTypeList = CommonBL.ListStockTypes(UserManager.UserInfo).Data;
            ViewBag.PoTypeList = CommonBL.ListLookup(UserManager.UserInfo, "PO_TYPE").Data;
            return View(referenceModel);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CustomerSearch.CustomerSearchIndex)]
        public ActionResult SearchCustomer([DataSourceRequest]DataSourceRequest request, CustomerSearchViewModel model)
        {
            var objectSearchBo = new ObjectSearchBL();
            var v = new CustomerSearchListModel(request);
            var totalCnt = 0;
            v.CustomerFullName = model.CustomerFullName;
            v.MobileNo = model.MobileNo;
            v.CountryId = model.CountryId;
            v.CustomerTypeId = model.CustomerTypeId;
            v.WitholdingStatus = model.WitholdingStatus;
            v.TCIdentityNo = model.TCIdentityNo;
            v.TaxNo = model.TaxNo;
            v.PassportNo = model.PassportNo;
            var returnValue = objectSearchBo.SearchCustomer(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }


        [AuthorizationFilter(CommonValues.PermissionCodes.CustomerSearch.CustomerSearchIndex)]
        public ActionResult SearchAppointmentIndicatorSubCategory([DataSourceRequest]DataSourceRequest request, AppointmentIndicatorSubCategorySearchViewModel model)
        {
            var objectSearchBo = new ObjectSearchBL();
            var v = new AppointmentIndicatorSubCategorySearchListModel(request);
            var totalCnt = 0;
            v.MainCategoryCode = model.MainCategoryCode;
            v.MainCategoryName = model.MainCategoryName;
            v.CategoryCode = model.CategoryCode;
            v.CategoryName = model.CategoryName;
            v.SubCategoryCode = model.SubCategoryCode;
            v.SubCategoryName = model.SubCategoryName;
            v.IndicatorTypeCode = model.IndicatorTypeCode;
            var returnValue = objectSearchBo.SearchAppointmentIndicatorSubCategory(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.VehicleSearch.VehicleSearchIndex)]
        public ActionResult SearchVehicle([DataSourceRequest]DataSourceRequest request, VehicleSearchViewModel model)
        {
            var objectSearchBo = new ObjectSearchBL();
            var v = new VehicleSearchListModel(request);
            var totalCnt = 0;
            v.CustomerFullName = model.CustomerFullName;
            v.VinNo = model.VinNo;
            v.WarrantyStartDate = model.WarrantyDate;
            v.EngineNo = model.EngineNo;
            v.Plate = model.Plate;
            v.ModelYear = model.ModelYear;
            v.VehicleModel = model.VehicleModel;
            v.VehicleType = model.VehicleType;
            if (model.FilterId == null)
                v.BodyworkDetailRequired = null;
            else
                v.BodyworkDetailRequired = true;

            var returnValue = objectSearchBo.SearchVehicle(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.AppointmentSearch.AppointmentSearchIndex)]
        public ActionResult SearchAppointment([DataSourceRequest]DataSourceRequest request, AppointmentSearchViewModel model)
        {
            var objectSearchBo = new ObjectSearchBL();
            var v = new AppointmentSearchListModel(request);
            var totalCnt = 0;
            v.CustomerFullName = model.CustomerName;
            v.ContactName = model.ContactName;
            v.StartDate = model.StartDate;
            v.EndDate = model.EndDate;
            v.VehiclePlate = model.Plate;
            var returnValue = objectSearchBo.SearchAppointment(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.AppointmentSearch.AppointmentSearchIndex)]
        public ActionResult SearchFleet([DataSourceRequest]DataSourceRequest request, FleetSearchListModel model)
        {
            var objectSearchBo = new ObjectSearchBL();
            var v = new FleetSearchListModel(request)
            {
                FleetCode = model.FleetCode,
                FleetName = model.FleetName,
                IsPartConstricted = model.IsPartConstricted
            };

            var returnValue = objectSearchBo.SearchFleet(UserManager.UserInfo, v).Data;

            return Json(new
            {
                Data = returnValue,
                Total = v.TotalCount
            });
        }

        //[AuthorizationFilter(CommonValues.PermissionCodes.AppointmentSearch.AppointmentSearchIndex)]
        public ActionResult SearchPurchaseOrder([DataSourceRequest]DataSourceRequest request, PurchaseOrderSearchListModel model)
        {
            var objectSearchBo = new ObjectSearchBL();
            var v = new PurchaseOrderSearchListModel(request)
            {
                PoNumber = model.PoNumber,
                IdStockType = model.IdStockType,
                DesiredShipDate = model.DesiredShipDate,
                Status = 1,
                IdDealer = ODMSCommon.Security.UserManager.UserInfo.GetUserDealerId(),
                PoType = model.PoType,
                SupplierId = model.SupplierId
            };

            var returnValue = objectSearchBo.SearchPurchaseOrder(UserManager.UserInfo, v).Data;

            return Json(new
            {
                Data = returnValue,
                Total = v.TotalCount
            });
        }

        public ActionResult ListPurchaseOrderDetail([DataSourceRequest]DataSourceRequest request, int PoNumber, int? PartId)
        {
            var objectSearchBo = new ObjectSearchBL();
            var v = new PurchaseOrderDetailListModel(request)
            {
                PurchaseOrderNumber = PoNumber,
                PartId = PartId
                //IdDealer = ODMSCommon.Security.UserManager.UserInfo.GetUserDealerId(),
            };

            var returnValue = objectSearchBo.SearchPurchaseOrderDetails(UserManager.UserInfo, v).Data;

            return Json(new
            {
                Data = returnValue,
                Total = v.TotalCount
            });
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.AppointmentIndicatorCategory.AppointmentIndicatorCategoryDetails)]
        public JsonResult ListVehicleTypes(string vehicleModel)
        {
            return !string.IsNullOrEmpty(vehicleModel) ?
                Json(VehicleTypeBL.ListVehicleTypeAsSelectList(UserManager.UserInfo, vehicleModel).Data, JsonRequestBehavior.AllowGet) :
                Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}
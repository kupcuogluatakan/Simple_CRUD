using System.Collections.Generic;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.MaintenanceAppointment;
using ODMSModel.Vehicle;
using ODMSModel.VehicleCode;
using ODMSModel.VehicleType;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class MaintenanceAppointmentController : ControllerBase
    {
        private void SetDefaults()
        {
            List<SelectListItem> vehicleTypeList = VehicleTypeBL.ListVehicleTypeAsSelectList(UserManager.UserInfo, null).Data;
            List<SelectListItem> vehicleModelList = VehicleModelBL.ListVehicleModelAsSelectList(UserManager.UserInfo).Data;
            List<SelectListItem> customerList = CustomerBL.ListCustomerAsSelectListItem(UserManager.UserInfo).Data;
            List<SelectListItem> dealerList = DealerBL.ListDealerAsSelectListItem().Data;
            List<SelectListItem> appointmentTypeList = AppointmentTypeBL.ListAppointmentTypeAsSelectListItem(UserManager.UserInfo).Data;

            ViewBag.VehicleTypeList = vehicleTypeList;
            ViewBag.VehicleModelList = vehicleModelList;
            ViewBag.CustomerList = customerList;
            ViewBag.DealerList = dealerList;
            ViewBag.AppointmentTypeList = appointmentTypeList;
        }

        private void SetDropDownList()
        {
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.AppointmentTypeList = AppointmentTypeBL.ListAppointmentTypeAsSelectListItem(UserManager.UserInfo).Data;
            ViewBag.VehicleModelList = VehicleModelBL.ListVehicleModelAsSelectList(UserManager.UserInfo).Data;
        }

        #region Maintenance Appointment Index

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.MaintenanceAppointment.MaintenanceAppointmentIndex)]
        [HttpGet]
        public ActionResult MaintenanceAppointmentIndex()
        {
            SetDefaults();
            return View();
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.MaintenanceAppointment.MaintenanceAppointmentIndex)]
        public JsonResult ListMaintenanceAppointment([DataSourceRequest] DataSourceRequest request, MaintenanceAppointmentListModel maintAppModel)
        {
            MaintenanceAppointmentBL maintAppBL = new MaintenanceAppointmentBL();
            MaintenanceAppointmentListModel model_MaintApp = new MaintenanceAppointmentListModel(request);
            int totalCount = 0;

            model_MaintApp.Plate = maintAppModel.Plate;
            model_MaintApp.VehicleTypeId = maintAppModel.VehicleTypeId;
            model_MaintApp.VehicleModelKod = maintAppModel.VehicleModelKod;
            model_MaintApp.VinNo = maintAppModel.VinNo;

            List<MaintenanceAppointmentListModel> resultList = new List<MaintenanceAppointmentListModel>();

            if (string.IsNullOrEmpty(model_MaintApp.Plate)
                && model_MaintApp.VehicleTypeId == 0
                && string.IsNullOrEmpty(model_MaintApp.VehicleModelKod)
                && string.IsNullOrEmpty(model_MaintApp.VinNo))
            {
                SetMessage(MessageResource.MaintenanceAppointment_WarningPlateModelCodeNotEntered, CommonValues.MessageSeverity.Fail);
            }
            else
            {
                resultList = maintAppBL.GetMaintenanceAppointmentList(UserManager.UserInfo, model_MaintApp, out totalCount).Data;
            }
            return Json(new
            {
                Data = resultList,
                Total = totalCount
            });

        }
        #endregion

        #region Maintenance Appointment Create
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.MaintenanceAppointment.MaintenanceAppointmentIndex, ODMSCommon.CommonValues.PermissionCodes.MaintenanceAppointment.MaintenanceAppointmentCreate)]
        [HttpGet]
        public ActionResult MaintenanceAppointmentCreate(int? maintenanceId, int? vehicleId)
        {
            SetDropDownList();
            var model = new MaintenanceAppointmentViewModel
            {
                AppointmentTypeId = 2,
                MaintenanceId = maintenanceId.GetValue<int>()
            };

            var vehicleModel = new VehicleIndexViewModel { VehicleId = vehicleId.GetValue<int>() };
            VehicleBL vehicleBL = new VehicleBL();
            vehicleBL.GetVehicle(UserManager.UserInfo, vehicleModel);

            if (vehicleModel.IsActive)
            {
                model.CustomerId = vehicleModel.CustomerId;

                if (UserManager.UserInfo.GetUserDealerId() != 0)
                {
                    model.DealerId = UserManager.UserInfo.GetUserDealerId();
                }
                model.VehicleColor = vehicleModel.Color;
                model.VehicleId = vehicleModel.VehicleId;
                model.VehiclePlate = vehicleModel.Plate;
                model.VehicleVin = vehicleModel.VinNo;
                model.VehicleModelName = vehicleModel.VehicleModel;
                model.VehicleTypeName = vehicleModel.VehicleType;

                VehicleCodeIndexViewModel vehicleCodeModel = new VehicleCodeIndexViewModel
                {
                    VehicleCodeKod = vehicleModel.VehicleCode
                };
                VehicleCodeBL vehicleCodeBL = new VehicleCodeBL();
                vehicleCodeBL.GetVehicleCode(UserManager.UserInfo, vehicleCodeModel);

                model.VehicleTypeId = vehicleCodeModel.VehicleTypeId.ToString();
                model.VehicleType = vehicleCodeModel.VehicleTypeName;

                VehicleTypeIndexViewModel vechileTypeModel = new VehicleTypeIndexViewModel
                {
                    TypeId = vehicleCodeModel.VehicleTypeId
                };
                VehicleTypeBL vechileTypeBL = new VehicleTypeBL();
                vechileTypeBL.GetVehicleType(UserManager.UserInfo, vechileTypeModel);

                model.VehicleModelCode = vechileTypeModel.ModelKod;
            }
            else
            {
                SetMessage(MessageResource.MaintenanceAppointment_Warning_PassiveVehicle, CommonValues.MessageSeverity.Fail);
            }
            return View(model);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.MaintenanceAppointment.MaintenanceAppointmentIndex, ODMSCommon.CommonValues.PermissionCodes.MaintenanceAppointment.MaintenanceAppointmentCreate)]
        [HttpPost]
        public ActionResult MaintenanceAppointmentCreate(MaintenanceAppointmentViewModel maintAppModel)
        {
            SetDropDownList();

            MaintenanceAppointmentBL maintAppBL = new MaintenanceAppointmentBL();
            if (ModelState.IsValid)
            {

                maintAppModel.CommandType = ODMSCommon.CommonValues.DMLType.Insert;

                maintAppBL.DMLMaintenanceAppointment(UserManager.UserInfo, maintAppModel);

                CheckErrorForMessage(maintAppModel, true);
                ModelState.Clear();


            }

            return View(maintAppModel);
        }
        #endregion

        public JsonResult GetVehicleData(int id)
        {
            if (id != 0)
            {
                VehicleIndexViewModel vehicleModel = new VehicleIndexViewModel { VehicleId = id };
                VehicleBL vehicleBo = new VehicleBL();
                vehicleBo.GetVehicle(UserManager.UserInfo, vehicleModel);


                VehicleCodeIndexViewModel vehicleCodeModel = new VehicleCodeIndexViewModel
                {
                    VehicleCodeKod = vehicleModel.VehicleCode
                };
                VehicleCodeBL vehicleCodeBL = new VehicleCodeBL();
                vehicleCodeBL.GetVehicleCode(UserManager.UserInfo, vehicleCodeModel);

                VehicleTypeIndexViewModel vechileTypeModel = new VehicleTypeIndexViewModel
                {
                    TypeId = vehicleCodeModel.VehicleTypeId
                };
                VehicleTypeBL vechileTypeBL = new VehicleTypeBL();
                vechileTypeBL.GetVehicleType(UserManager.UserInfo, vechileTypeModel);

                return Json(new { VehiclePlate = vehicleModel.Plate, VehicleModelKod = vechileTypeModel.ModelKod, VehicleColor = vehicleModel.Color, VehicleTypeId = vechileTypeModel.TypeId, VehicleType = vechileTypeModel.TypeName }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0);
            }
        }
    }
}

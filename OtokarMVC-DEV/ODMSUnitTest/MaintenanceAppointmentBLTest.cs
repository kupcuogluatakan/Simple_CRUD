using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.MaintenanceAppointment;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class MaintenanceAppointmentBLTest
    {

        MaintenanceAppointmentBL _MaintenanceAppointmentBL = new MaintenanceAppointmentBL();

        [TestMethod]
        public void MaintenanceAppointmentBL_DMLMaintenanceAppointment_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new MaintenanceAppointmentViewModel();
            model.MaintenanceId = 1;
            model.AppointmentId = 1;
            model.VehicleId = 29627;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.VehicleModelName = guid;
            model.VehicleTypeName = guid;
            model.CustomerName = guid;
            model.AppointmentTypeName = guid;
            model.DealerName = guid;
            model.VehicleIdVehiclePlate = guid;
            model.AppointmentDateFormatted = guid;
            model.AppointmentTimeFormatted = guid;
            model.ContactName = guid;
            model.ContactLastName = guid;
            model.ContactPhone = guid;
            model.ContactAddress = guid;
            model.VehiclePlate = guid;
            model.VehicleColor = guid;
            model.VehicleModelCode = guid;
            model.VehicleType = guid;
            model.VehicleTypeId = guid;
            model.ComplaintDescription = guid;
            model.VehicleVin = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _MaintenanceAppointmentBL.DMLMaintenanceAppointment(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void MaintenanceAppointmentBL_GetMaintenanceAppointmentList_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new MaintenanceAppointmentViewModel();
            model.MaintenanceId = 1;
            model.AppointmentId = 1;
            model.VehicleId = 29627;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.VehicleModelName = guid;
            model.VehicleTypeName = guid;
            model.CustomerName = guid;
            model.AppointmentTypeName = guid;
            model.DealerName = guid;
            model.VehicleIdVehiclePlate = guid;
            model.AppointmentDateFormatted = guid;
            model.AppointmentTimeFormatted = guid;
            model.ContactName = guid;
            model.ContactLastName = guid;
            model.ContactPhone = guid;
            model.ContactAddress = guid;
            model.VehiclePlate = guid;
            model.VehicleColor = guid;
            model.VehicleModelCode = guid;
            model.VehicleType = guid;
            model.VehicleTypeId = guid;
            model.ComplaintDescription = guid;
            model.VehicleVin = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _MaintenanceAppointmentBL.DMLMaintenanceAppointment(UserManager.UserInfo, model);

            int count = 0;
            var filter = new MaintenanceAppointmentListModel();
            filter.VehicleId = 29627;

            var resultGet = _MaintenanceAppointmentBL.GetMaintenanceAppointmentList(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void MaintenanceAppointmentBL_DMLMaintenanceAppointment_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new MaintenanceAppointmentViewModel();
            model.MaintenanceId = 1;
            model.AppointmentId = 1;
            model.VehicleId = 29627;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.VehicleModelName = guid;
            model.VehicleTypeName = guid;
            model.CustomerName = guid;
            model.AppointmentTypeName = guid;
            model.DealerName = guid;
            model.VehicleIdVehiclePlate = guid;
            model.AppointmentDateFormatted = guid;
            model.AppointmentTimeFormatted = guid;
            model.ContactName = guid;
            model.ContactLastName = guid;
            model.ContactPhone = guid;
            model.ContactAddress = guid;
            model.VehiclePlate = guid;
            model.VehicleColor = guid;
            model.VehicleModelCode = guid;
            model.VehicleType = guid;
            model.VehicleTypeId = guid;
            model.ComplaintDescription = guid;
            model.VehicleVin = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _MaintenanceAppointmentBL.DMLMaintenanceAppointment(UserManager.UserInfo, model);

            var filter = new MaintenanceAppointmentListModel();
            filter.VehicleId = 29627;

            int count = 0;
            var resultGet = _MaintenanceAppointmentBL.GetMaintenanceAppointmentList(UserManager.UserInfo, filter, out count);

            var modelUpdate = new MaintenanceAppointmentViewModel();
            modelUpdate.VehicleId = 29627;
            modelUpdate.DealerId = UserManager.UserInfo.DealerID;
            modelUpdate.VehicleModelName = guid;
            modelUpdate.VehicleTypeName = guid;
            modelUpdate.CustomerName = guid;
            modelUpdate.AppointmentTypeName = guid;
            modelUpdate.DealerName = guid;
            modelUpdate.VehicleIdVehiclePlate = guid;
            modelUpdate.AppointmentDateFormatted = guid;
            modelUpdate.AppointmentTimeFormatted = guid;
            modelUpdate.ContactName = guid;
            modelUpdate.ContactLastName = guid;
            modelUpdate.ContactPhone = guid;
            modelUpdate.ContactAddress = guid;
            modelUpdate.VehiclePlate = guid;
            modelUpdate.VehicleColor = guid;
            modelUpdate.VehicleModelCode = guid;
            modelUpdate.VehicleType = guid;
            modelUpdate.VehicleTypeId = guid;
            modelUpdate.ComplaintDescription = guid;
            modelUpdate.VehicleVin = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _MaintenanceAppointmentBL.DMLMaintenanceAppointment(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void MaintenanceAppointmentBL_DMLMaintenanceAppointment_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new MaintenanceAppointmentViewModel();
            model.MaintenanceId = 1;
            model.AppointmentId = 1;
            model.VehicleId = 29627;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.VehicleModelName = guid;
            model.VehicleTypeName = guid;
            model.CustomerName = guid;
            model.AppointmentTypeName = guid;
            model.DealerName = guid;
            model.VehicleIdVehiclePlate = guid;
            model.AppointmentDateFormatted = guid;
            model.AppointmentTimeFormatted = guid;
            model.ContactName = guid;
            model.ContactLastName = guid;
            model.ContactPhone = guid;
            model.ContactAddress = guid;
            model.VehiclePlate = guid;
            model.VehicleColor = guid;
            model.VehicleModelCode = guid;
            model.VehicleType = guid;
            model.VehicleTypeId = guid;
            model.ComplaintDescription = guid;
            model.VehicleVin = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _MaintenanceAppointmentBL.DMLMaintenanceAppointment(UserManager.UserInfo, model);

            var filter = new MaintenanceAppointmentListModel();
            filter.VehicleId = 29627;

            int count = 0;
            var resultGet = _MaintenanceAppointmentBL.GetMaintenanceAppointmentList(UserManager.UserInfo, filter, out count);

            var modelDelete = new MaintenanceAppointmentViewModel();
            modelDelete.VehicleId = 29627;
            modelDelete.DealerId = UserManager.UserInfo.DealerID;
            modelDelete.VehicleModelName = guid;
            modelDelete.VehicleTypeName = guid;
            modelDelete.CustomerName = guid;
            modelDelete.AppointmentTypeName = guid;
            modelDelete.DealerName = guid;
            modelDelete.VehicleIdVehiclePlate = guid;
            modelDelete.AppointmentDateFormatted = guid;
            modelDelete.AppointmentTimeFormatted = guid;
            modelDelete.ContactName = guid;
            modelDelete.ContactLastName = guid;
            modelDelete.ContactPhone = guid;
            modelDelete.ContactAddress = guid;
            modelDelete.VehiclePlate = guid;
            modelDelete.VehicleColor = guid;
            modelDelete.VehicleModelCode = guid;
            modelDelete.VehicleType = guid;
            modelDelete.VehicleTypeId = guid;
            modelDelete.ComplaintDescription = guid;
            modelDelete.VehicleVin = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _MaintenanceAppointmentBL.DMLMaintenanceAppointment(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}


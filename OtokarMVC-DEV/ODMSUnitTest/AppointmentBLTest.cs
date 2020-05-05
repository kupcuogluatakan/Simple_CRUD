using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.Appointment;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class AppointmentBLTest
    {

        AppointmentBL _AppointmentBL = new AppointmentBL();

        [TestMethod]
        public void AppointmentBL_DMLAppointment_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AppointmentViewModel();
            model.AppointmentId = 1;
            model.VehicleId = 29627;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.VehicleModelName = guid;
            model.VehicleTypeName = guid;
            model.CustomerName = guid;
            model.AppointmentTypeName = guid;
            model.DealerName = guid;
            model.VehicleIdVehiclePlate = guid;
            model.AppointmentDate = DateTime.Now;
            model.AppointmentTime = new TimeSpan(10, 0, 0);
            model.ContactName = guid;
            model.ContactLastName = guid;
            model.ContactPhone = guid;
            model.ContactAddress = guid;
            model.VehiclePlate = guid;
            model.VehicleColor = guid;
            model.VehicleModelCode = guid;
            model.VehicleType = guid;
            model.ComplaintDescription = guid;
            model.VehicleVin = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.AppointmentTypeId = 3;
            model.CommandType = "I";
            var result = _AppointmentBL.DMLAppointment(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void AppointmentBL_GetAppointment_GetModel()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AppointmentViewModel();
            model.AppointmentId = 1;
            model.VehicleId = 29627;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.VehicleModelName = guid;
            model.VehicleTypeName = guid;
            model.CustomerName = guid;
            model.AppointmentTypeName = guid;
            model.DealerName = guid;
            model.VehicleIdVehiclePlate = guid;
            model.AppointmentDate = DateTime.Now;
            model.AppointmentTime = new TimeSpan(10, 0, 0);
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
            model.ComplaintDescription = guid;
            model.VehicleVin = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.AppointmentTypeId = 3;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _AppointmentBL.DMLAppointment(UserManager.UserInfo, model);

            var resultGet = _AppointmentBL.GetAppointment(UserManager.UserInfo, result.Model.AppointmentId);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.DealerName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void AppointmentBL_GetAppointmentCustomer_GetModel()
        {
            var filter = new AppointmentCustomerViewModel();
            filter.CustomerId = 11833;

            var resultGet = _AppointmentBL.GetAppointmentCustomer(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.Name != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void AppointmentBL_GetAppointmentVehicleInfo_GetModel()
        {
            var resultGet = _AppointmentBL.GetAppointmentVehicleInfo(29627);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.VehicleModel != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void AppointmentBL_ListAppointments_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AppointmentViewModel();
            model.AppointmentId = 1;
            model.VehicleId = 29627;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.VehicleModelName = guid;
            model.VehicleTypeName = guid;
            model.CustomerName = guid;
            model.AppointmentTypeName = guid;
            model.DealerName = guid;
            model.VehicleIdVehiclePlate = guid;
            model.AppointmentDate = DateTime.Now;
            model.AppointmentTime = new TimeSpan(10, 0, 0);
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
            model.ComplaintDescription = guid;
            model.VehicleVin = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.AppointmentTypeId = 3;
            model.CommandType = "I";
            var result = _AppointmentBL.DMLAppointment(UserManager.UserInfo, model);

            int count = 0;
            var filter = new AppointmentListModel();
            filter.DealerId = UserManager.UserInfo.DealerID;
            filter.AppointmentTypeId = 3;
            var resultGet = _AppointmentBL.ListAppointments(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void AppointmentBL_DMLAppointment_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AppointmentViewModel();
            model.AppointmentId = 1;
            model.VehicleId = 29627;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.VehicleModelName = guid;
            model.VehicleTypeName = guid;
            model.CustomerName = guid;
            model.AppointmentTypeName = guid;
            model.DealerName = guid;
            model.VehicleIdVehiclePlate = guid;
            model.AppointmentDate = DateTime.Now;
            model.AppointmentTime = new TimeSpan(10, 0, 0);
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
            model.ComplaintDescription = guid;
            model.VehicleVin = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.AppointmentTypeId = 3;
            model.CommandType = "I";
            var result = _AppointmentBL.DMLAppointment(UserManager.UserInfo, model);

            var filter = new AppointmentListModel();
            filter.DealerId = UserManager.UserInfo.DealerID;

            int count = 0;
            var resultGet = _AppointmentBL.ListAppointments(UserManager.UserInfo, filter, out count);

            var modelUpdate = new AppointmentViewModel();
            modelUpdate.AppointmentId = resultGet.Data.First().AppointmentId;
            modelUpdate.VehicleId = 29627;
            modelUpdate.DealerId = UserManager.UserInfo.DealerID;
            modelUpdate.VehicleModelName = guid;
            modelUpdate.VehicleTypeName = guid;
            modelUpdate.CustomerName = guid;
            modelUpdate.AppointmentTypeName = guid;
            modelUpdate.DealerName = guid;
            modelUpdate.VehicleIdVehiclePlate = guid;
            modelUpdate.AppointmentDate = DateTime.Now;
            modelUpdate.AppointmentTime = new TimeSpan(10, 0, 0);
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
            modelUpdate.ComplaintDescription = guid;
            modelUpdate.VehicleVin = guid;
            modelUpdate.CommandType = "U";
            modelUpdate.AppointmentTypeId = 3;
            var resultUpdate = _AppointmentBL.DMLAppointment(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void AppointmentBL_DMLAppointment_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AppointmentViewModel();
            model.AppointmentId = 1;
            model.VehicleId = 29627;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.VehicleModelName = guid;
            model.VehicleTypeName = guid;
            model.CustomerName = guid;
            model.AppointmentTypeName = guid;
            model.DealerName = guid;
            model.VehicleIdVehiclePlate = guid;
            model.AppointmentDate = DateTime.Now;
            model.AppointmentTime = new TimeSpan(10, 0, 0);
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
            model.ComplaintDescription = guid;
            model.VehicleVin = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.AppointmentTypeId = 3;
            model.CommandType = "I";
            var result = _AppointmentBL.DMLAppointment(UserManager.UserInfo, model);

            var filter = new AppointmentListModel();
            filter.DealerId = UserManager.UserInfo.DealerID;

            int count = 0;
            var resultGet = _AppointmentBL.ListAppointments(UserManager.UserInfo, filter, out count);

            var modelDelete = new AppointmentViewModel();
            modelDelete.AppointmentId = resultGet.Data.First().AppointmentId;

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
            modelDelete.ComplaintDescription = guid;
            modelDelete.VehicleVin = guid;
            modelDelete.AppointmentTypeId = 3;


            modelDelete.CommandType = "D";
            var resultDelete = _AppointmentBL.DMLAppointment(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}


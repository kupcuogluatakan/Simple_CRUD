using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.FleetRequestVehicle;


namespace ODMSUnitTest
{

    [TestClass]
    public class FleetRequestVehicleBLTest
    {

        FleetRequestVehicleBL _FleetRequestVehicleBL = new FleetRequestVehicleBL();

        [TestMethod]
        public void FleetRequestVehicleBL_DMLFleetRequestVehicle_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FleetRequestVehicleViewModel();
            model.FleetRequestVehicleId = 1;
            model.FleetRequestId = 1;
            model.CustomerId = 1;
            model.VehicleId = 29627;
            model.CustomerName = guid;
            model.VehicleName = guid;
            model.DocumentName = guid;
            model.DocumentId = 1;
            model.HideElements = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FleetRequestVehicleBL.DMLFleetRequestVehicle(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void FleetRequestVehicleBL_GetFleetRequestVehicle_GetModel()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FleetRequestVehicleViewModel();
            model.FleetRequestVehicleId = 1;
            model.FleetRequestId = 1;
            model.CustomerId = 1;
            model.VehicleId = 29627;
            model.CustomerName = guid;
            model.VehicleName = guid;
            model.DocumentName = guid;
            model.DocumentId = 1;
            model.HideElements = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FleetRequestVehicleBL.DMLFleetRequestVehicle(UserManager.UserInfo, model);


            var resultGet = _FleetRequestVehicleBL.GetFleetRequestVehicle(result.Model.FleetRequestId);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.CustomerName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void FleetRequestVehicleBL_GetFleetRequestStatus_GetModel()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FleetRequestVehicleViewModel();
            model.FleetRequestVehicleId = 1;
            model.FleetRequestId = 1;
            model.CustomerId = 1;
            model.VehicleId = 29627;
            model.CustomerName = guid;
            model.VehicleName = guid;
            model.DocumentName = guid;
            model.DocumentId = 1;
            model.HideElements = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FleetRequestVehicleBL.DMLFleetRequestVehicle(UserManager.UserInfo, model);


            var resultGet = _FleetRequestVehicleBL.GetFleetRequestStatus(result.Model.FleetRequestId);

            Assert.IsTrue(resultGet.Model > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void FleetRequestVehicleBL_ListFleetRequestVehicle_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FleetRequestVehicleViewModel();
            model.FleetRequestVehicleId = 1;
            model.FleetRequestId = 1;
            model.CustomerId = 1;
            model.VehicleId = 29627;
            model.CustomerName = guid;
            model.VehicleName = guid;
            model.DocumentName = guid;
            model.DocumentId = 1;
            model.HideElements = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FleetRequestVehicleBL.DMLFleetRequestVehicle(UserManager.UserInfo, model);

            int count = 0;
            var filter = new FleetRequestVehicleListModel();
            filter.VehicleId = 29627;

            var resultGet = _FleetRequestVehicleBL.ListFleetRequestVehicle(filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void FleetRequestVehicleBL_DMLFleetRequestVehicle_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FleetRequestVehicleViewModel();
            model.FleetRequestVehicleId = 1;
            model.FleetRequestId = 1;
            model.CustomerId = 1;
            model.VehicleId = 29627;
            model.CustomerName = guid;
            model.VehicleName = guid;
            model.DocumentName = guid;
            model.DocumentId = 1;
            model.HideElements = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FleetRequestVehicleBL.DMLFleetRequestVehicle(UserManager.UserInfo, model);

            var filter = new FleetRequestVehicleListModel();
            filter.VehicleId = 29627;

            int count = 0;
            var resultGet = _FleetRequestVehicleBL.ListFleetRequestVehicle(filter, out count);

            var modelUpdate = new FleetRequestVehicleViewModel();
            modelUpdate.FleetRequestId = resultGet.Data.First().FleetRequestId;
            modelUpdate.VehicleId = 29627;
            modelUpdate.CustomerName = guid;
            modelUpdate.VehicleName = guid;
            modelUpdate.DocumentName = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _FleetRequestVehicleBL.DMLFleetRequestVehicle(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void FleetRequestVehicleBL_DMLFleetRequestVehicle_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FleetRequestVehicleViewModel();
            model.FleetRequestVehicleId = 1;
            model.FleetRequestId = 1;
            model.CustomerId = 1;
            model.VehicleId = 29627;
            model.CustomerName = guid;
            model.VehicleName = guid;
            model.DocumentName = guid;
            model.DocumentId = 1;
            model.HideElements = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FleetRequestVehicleBL.DMLFleetRequestVehicle(UserManager.UserInfo, model);

            var filter = new FleetRequestVehicleListModel();
            filter.VehicleId = 29627;

            int count = 0;
            var resultGet = _FleetRequestVehicleBL.ListFleetRequestVehicle(filter, out count);

            var modelDelete = new FleetRequestVehicleViewModel();
            modelDelete.FleetRequestVehicleId = resultGet.Data.First().FleetRequestVehicleId;
            modelDelete.VehicleId = 29627;
            modelDelete.CustomerName = guid;
            modelDelete.VehicleName = guid;
            modelDelete.DocumentName = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _FleetRequestVehicleBL.DMLFleetRequestVehicle(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}


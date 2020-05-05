using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.FleetVehicle;


namespace ODMSUnitTest
{

    [TestClass]
    public class FleetVehicleBLTest
    {

        FleetVehicleBL _FleetVehicleBL = new FleetVehicleBL();

        [TestMethod]
        public void FleetVehicleBL_DMLFleetVehicle_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FleetVehicleViewModel();
            model.FleetVehicleId = 1;
            model.FleetId = 1;
            model.CustomerId = 1;
            model.VehicleId = 29627;
            model.CustomerName = guid;
            model.VehicleName = guid;
            model.VehicleVinNo = guid;
            model.HideElements = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FleetVehicleBL.DMLFleetVehicle(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void FleetVehicleBL_DMLFleetVehicleWithList_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FleetVehicleViewModel();
            model.FleetVehicleId = 1;
            model.FleetId = 1;
            model.CustomerId = 1;
            model.VehicleId = 29627;
            model.CustomerName = guid;
            model.VehicleName = guid;
            model.VehicleVinNo = guid;
            model.HideElements = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FleetVehicleBL.DMLFleetVehicleWithList(UserManager.UserInfo, model, new System.Collections.Generic.List<FleetVehicleViewModel>());

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void FleetVehicleBL_GetFleetVehicle_GetModel()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FleetVehicleViewModel();
            model.FleetVehicleId = 1;
            model.FleetId = 1;
            model.CustomerId = 1;
            model.VehicleId = 29627;
            model.CustomerName = guid;
            model.VehicleName = guid;
            model.VehicleVinNo = guid;
            model.HideElements = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FleetVehicleBL.DMLFleetVehicle(UserManager.UserInfo, model);


            var resultGet = _FleetVehicleBL.GetFleetVehicle(result.Model.FleetVehicleId);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.VehicleName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void FleetVehicleBL_ListFleetVehicle_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FleetVehicleViewModel();
            model.FleetVehicleId = 1;
            model.FleetId = 1;
            model.CustomerId = 1;
            model.VehicleId = 29627;
            model.CustomerName = guid;
            model.VehicleName = guid;
            model.VehicleVinNo = guid;
            model.HideElements = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FleetVehicleBL.DMLFleetVehicleWithList(UserManager.UserInfo, model, new System.Collections.Generic.List<FleetVehicleViewModel>());

            int count = 0;
            var filter = new FleetVehicleListModel();
            filter.VehicleId = 29627;

            var resultGet = _FleetVehicleBL.ListFleetVehicle(filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void FleetVehicleBL_DMLFleetVehicleWithList_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FleetVehicleViewModel();
            model.FleetVehicleId = 1;
            model.FleetId = 1;
            model.CustomerId = 1;
            model.VehicleId = 29627;
            model.CustomerName = guid;
            model.VehicleName = guid;
            model.VehicleVinNo = guid;
            model.HideElements = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FleetVehicleBL.DMLFleetVehicleWithList(UserManager.UserInfo, model, new System.Collections.Generic.List<FleetVehicleViewModel>());

            var filter = new FleetVehicleListModel();
            filter.VehicleId = 29627;

            int count = 0;
            var resultGet = _FleetVehicleBL.ListFleetVehicle(filter, out count);

            var modelUpdate = new FleetVehicleViewModel();
            modelUpdate.VehicleId = 29627;
            modelUpdate.CustomerName = guid;
            modelUpdate.VehicleName = guid;
            modelUpdate.VehicleVinNo = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _FleetVehicleBL.DMLFleetVehicleWithList(UserManager.UserInfo, modelUpdate, new System.Collections.Generic.List<FleetVehicleViewModel>());
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void FleetVehicleBL_DMLFleetVehicleWithList_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FleetVehicleViewModel();
            model.FleetVehicleId = 1;
            model.FleetId = 1;
            model.CustomerId = 1;
            model.VehicleId = 29627;
            model.CustomerName = guid;
            model.VehicleName = guid;
            model.VehicleVinNo = guid;
            model.HideElements = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FleetVehicleBL.DMLFleetVehicleWithList(UserManager.UserInfo, model, new System.Collections.Generic.List<FleetVehicleViewModel>());

            var filter = new FleetVehicleListModel();
            filter.VehicleId = 29627;

            int count = 0;
            var resultGet = _FleetVehicleBL.ListFleetVehicle(filter, out count);

            var modelDelete = new FleetVehicleViewModel();
            modelDelete.VehicleId = 29627;
            modelDelete.CustomerName = guid;
            modelDelete.VehicleName = guid;
            modelDelete.VehicleVinNo = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _FleetVehicleBL.DMLFleetVehicleWithList(UserManager.UserInfo, modelDelete, new System.Collections.Generic.List<FleetVehicleViewModel>());
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}


using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.VehicleBodywork;


namespace ODMSUnitTest
{

    [TestClass]
    public class VehicleBodyworkBLTest
    {

        VehicleBodyworkBL _VehicleBodyworkBL = new VehicleBodyworkBL();

        [TestMethod]
        public void VehicleBodyworkBL_DMLVehicleBodywork_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleBodyworkViewModel();
            model.VehicleBodyworkId = 1;
            model.BodyworkCode = guid;
            model.BodyworkName = guid;
            model.VehicleId = 29627;
            model.VehiclePlate = guid;
            model.CountryId = 1;
            model.CountryName = guid;
            model.CityId = 1;
            model.CityName = guid;
            model.WorkOrderName = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.Manufacturer = guid;
            model.VehicleVinNo = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _VehicleBodyworkBL.DMLVehicleBodywork(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void VehicleBodyworkBL_GetVehicleBodywork_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleBodyworkViewModel();
            model.VehicleBodyworkId = 1;
            model.BodyworkCode = guid;
            model.BodyworkName = guid;
            model.VehicleId = 29627;
            model.VehiclePlate = guid;
            model.CountryId = 1;
            model.CountryName = guid;
            model.CityId = 1;
            model.CityName = guid;
            model.WorkOrderName = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.Manufacturer = guid;
            model.VehicleVinNo = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _VehicleBodyworkBL.DMLVehicleBodywork(UserManager.UserInfo, model);

            var filter = new VehicleBodyworkViewModel();
            filter.VehicleBodyworkId = result.Model.VehicleBodyworkId;
            filter.BodyworkCode = guid;
            filter.VehicleId = 29627;
            filter.CountryId = 1;
            filter.CityId = 1;
            filter.DealerId = UserManager.UserInfo.DealerID;

            var resultGet = _VehicleBodyworkBL.GetVehicleBodywork(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.DealerName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void VehicleBodyworkBL_ListVehicleBodywork_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleBodyworkViewModel();
            model.VehicleBodyworkId = 1;
            model.BodyworkCode = guid;
            model.BodyworkName = guid;
            model.VehicleId = 29627;
            model.VehiclePlate = guid;
            model.CountryId = 1;
            model.CountryName = guid;
            model.CityId = 1;
            model.CityName = guid;
            model.WorkOrderName = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.Manufacturer = guid;
            model.VehicleVinNo = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _VehicleBodyworkBL.DMLVehicleBodywork(UserManager.UserInfo, model);

            int count = 0;
            var filter = new VehicleBodyworkListModel();
            filter.BodyworkCode = guid;
            filter.VehicleId = 29627;
            filter.DealerId = UserManager.UserInfo.DealerID;

            var resultGet = _VehicleBodyworkBL.ListVehicleBodywork(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void VehicleBodyworkBL_DMLVehicleBodywork_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleBodyworkViewModel();
            model.VehicleBodyworkId = 1;
            model.BodyworkCode = guid;
            model.BodyworkName = guid;
            model.VehicleId = 29627;
            model.VehiclePlate = guid;
            model.CountryId = 1;
            model.CountryName = guid;
            model.CityId = 1;
            model.CityName = guid;
            model.WorkOrderName = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.Manufacturer = guid;
            model.VehicleVinNo = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _VehicleBodyworkBL.DMLVehicleBodywork(UserManager.UserInfo, model);

            var filter = new VehicleBodyworkListModel();
            filter.BodyworkCode = guid;
            filter.VehicleId = 29627;
            filter.DealerId = UserManager.UserInfo.DealerID;

            int count = 0;
            var resultGet = _VehicleBodyworkBL.ListVehicleBodywork(UserManager.UserInfo, filter, out count);

            var modelUpdate = new VehicleBodyworkViewModel();
            modelUpdate.VehicleBodyworkId = resultGet.Data.First().VehicleBodyworkId;

            modelUpdate.BodyworkCode = guid;
            modelUpdate.BodyworkName = guid;
            modelUpdate.VehicleId = 29627;
            modelUpdate.VehiclePlate = guid;
            modelUpdate.CountryId = 1;
            modelUpdate.CountryName = guid;
            modelUpdate.CityId = 1;
            modelUpdate.CityName = guid;
            modelUpdate.WorkOrderName = guid;
            modelUpdate.DealerId = UserManager.UserInfo.DealerID;
            modelUpdate.DealerName = guid;
            modelUpdate.Manufacturer = guid;
            modelUpdate.VehicleVinNo = guid;



            modelUpdate.CommandType = "U";
            var resultUpdate = _VehicleBodyworkBL.DMLVehicleBodywork(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void VehicleBodyworkBL_DMLVehicleBodywork_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleBodyworkViewModel();
            model.VehicleBodyworkId = 1;
            model.BodyworkCode = guid;
            model.BodyworkName = guid;
            model.VehicleId = 29627;
            model.VehiclePlate = guid;
            model.CountryId = 1;
            model.CountryName = guid;
            model.CityId = 1;
            model.CityName = guid;
            model.WorkOrderName = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.Manufacturer = guid;
            model.VehicleVinNo = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _VehicleBodyworkBL.DMLVehicleBodywork(UserManager.UserInfo, model);

            var filter = new VehicleBodyworkListModel();
            filter.BodyworkCode = guid;
            filter.VehicleId = 29627;
            filter.CountryId = 1;
            filter.CityId = 1;
            filter.DealerId = UserManager.UserInfo.DealerID;

            int count = 0;
            var resultGet = _VehicleBodyworkBL.ListVehicleBodywork(UserManager.UserInfo, filter, out count);

            var modelDelete = new VehicleBodyworkViewModel();
            modelDelete.VehicleBodyworkId = resultGet.Data.First().VehicleBodyworkId;

            modelDelete.BodyworkCode = guid;
            modelDelete.BodyworkName = guid;
            modelDelete.VehicleId = 29627;
            modelDelete.VehiclePlate = guid;
            modelDelete.CountryId = 1;
            modelDelete.CountryName = guid;
            modelDelete.CityId = 1;
            modelDelete.CityName = guid;
            modelDelete.WorkOrderName = guid;
            modelDelete.DealerId = UserManager.UserInfo.DealerID;
            modelDelete.DealerName = guid;
            modelDelete.Manufacturer = guid;
            modelDelete.VehicleVinNo = guid;



            modelDelete.CommandType = "D";
            var resultDelete = _VehicleBodyworkBL.DMLVehicleBodywork(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}


using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.LabourDuration;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class LabourDurationBLTest
    {

        LabourDurationBL _LabourDurationBL = new LabourDurationBL();

        [TestMethod]
        public void LabourDurationBL_DMLLabourDuration_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourDurationDetailModel();
            model.LabourId = 211;
            model.LabourCode = guid;
            model.LabourName = guid;
            model.LabourDesc = guid;
            model.VehicleModelId = guid;
            model.VehicleCode = guid;
            model.EngineType = guid;
            model.VehicleModelName = guid;
            model.VehicleTypeName = guid;
            model.Duration = 1;
            model.DurationString = guid;
            model.LabourIdString = guid;
            model.VehicleTypeIdString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _LabourDurationBL.DMLLabourDuration(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void LabourDurationBL_GetLabourDuration_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourDurationDetailModel();
            model.LabourId = 211;
            model.LabourCode = guid;
            model.LabourName = guid;
            model.LabourDesc = guid;
            model.VehicleModelId = guid;
            model.VehicleCode = guid;
            model.EngineType = guid;
            model.VehicleModelName = guid;
            model.VehicleTypeName = guid;
            model.Duration = 1;
            model.DurationString = guid;
            model.LabourIdString = guid;
            model.VehicleTypeIdString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _LabourDurationBL.DMLLabourDuration(UserManager.UserInfo, model);

            var filter = new LabourDurationDetailModel();
            filter.LabourId = 211;
            filter.LabourCode = guid;
            filter.VehicleCode = guid;

            var resultGet = _LabourDurationBL.GetLabourDuration(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.LabourName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void LabourDurationBL_GetLabourDurationIndexModel_GetModel()
        {
            var resultGet = _LabourDurationBL.GetLabourDurationIndexModel(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.LabourList.Count > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void LabourDurationBL_ListLabourDurations_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourDurationDetailModel();
            model.LabourId = 211;
            model.LabourCode = guid;
            model.LabourName = guid;
            model.LabourDesc = guid;
            model.VehicleModelId = guid;
            model.VehicleCode = guid;
            model.EngineType = guid;
            model.VehicleModelName = guid;
            model.VehicleTypeName = guid;
            model.Duration = 1;
            model.DurationString = guid;
            model.LabourIdString = guid;
            model.VehicleTypeIdString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _LabourDurationBL.DMLLabourDuration(UserManager.UserInfo, model);

            int count = 0;
            var filter = new LabourDurationListModel();
            filter.LabourId = 211;
            filter.LabourCode = guid;

            var resultGet = _LabourDurationBL.ListLabourDurations(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void LabourDurationBL_DMLLabourDuration_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourDurationDetailModel();
            model.LabourId = 211;
            model.LabourCode = guid;
            model.LabourName = guid;
            model.LabourDesc = guid;
            model.VehicleModelId = guid;
            model.VehicleCode = guid;
            model.EngineType = guid;
            model.VehicleModelName = guid;
            model.VehicleTypeName = guid;
            model.Duration = 1;
            model.DurationString = guid;
            model.LabourIdString = guid;
            model.VehicleTypeIdString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _LabourDurationBL.DMLLabourDuration(UserManager.UserInfo, model);

            var filter = new LabourDurationListModel();
            filter.LabourId = 211;
            filter.LabourCode = guid;

            int count = 0;
            var resultGet = _LabourDurationBL.ListLabourDurations(UserManager.UserInfo, filter, out count);

            var modelUpdate = new LabourDurationDetailModel();
            modelUpdate.LabourId = 211;
            modelUpdate.LabourCode = guid;
            modelUpdate.LabourName = guid;
            modelUpdate.LabourDesc = guid;
            modelUpdate.VehicleModelId = guid;
            modelUpdate.VehicleCode = guid;
            modelUpdate.EngineType = guid;
            modelUpdate.VehicleModelName = guid;
            modelUpdate.VehicleTypeName = guid;
            modelUpdate.DurationString = guid;
            modelUpdate.LabourIdString = guid;
            modelUpdate.VehicleTypeIdString = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _LabourDurationBL.DMLLabourDuration(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void LabourDurationBL_DMLLabourDuration_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourDurationDetailModel();
            model.LabourId = 211;
            model.LabourCode = guid;
            model.LabourName = guid;
            model.LabourDesc = guid;
            model.VehicleModelId = guid;
            model.VehicleCode = guid;
            model.EngineType = guid;
            model.VehicleModelName = guid;
            model.VehicleTypeName = guid;
            model.Duration = 1;
            model.DurationString = guid;
            model.LabourIdString = guid;
            model.VehicleTypeIdString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _LabourDurationBL.DMLLabourDuration(UserManager.UserInfo, model);

            var filter = new LabourDurationListModel();
            filter.LabourId = 211;
            filter.LabourCode = guid;

            int count = 0;
            var resultGet = _LabourDurationBL.ListLabourDurations(UserManager.UserInfo, filter, out count);

            var modelDelete = new LabourDurationDetailModel();
            modelDelete.LabourId = 211;
            modelDelete.LabourCode = guid;
            modelDelete.LabourName = guid;
            modelDelete.LabourDesc = guid;
            modelDelete.VehicleModelId = guid;
            modelDelete.VehicleCode = guid;
            modelDelete.EngineType = guid;
            modelDelete.VehicleModelName = guid;
            modelDelete.VehicleTypeName = guid;
            modelDelete.DurationString = guid;
            modelDelete.LabourIdString = guid;
            modelDelete.VehicleTypeIdString = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _LabourDurationBL.DMLLabourDuration(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }

        [TestMethod]
        public void LabourDurationBL_GetLabourList_GetAll()
        {
            var resultGet = _LabourDurationBL.GetLabourList(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void LabourDurationBL_GetVehicleModelList_GetAll()
        {
            var resultGet = _LabourDurationBL.GetVehicleModelList(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void LabourDurationBL_GetVehicleTypeList_GetAll()
        {
            var resultGet = LabourDurationBL.GetVehicleTypeList(UserManager.UserInfo, "4", "211");

            Assert.IsTrue(resultGet.Total > 0);
        }


        [TestMethod]
        public void LabourDurationBL_GetVehicleTypeEngineTypeList_GetAll()
        {
            var resultGet = LabourDurationBL.GetVehicleTypeEngineTypeList(UserManager.UserInfo, "4", "211");

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void LabourDurationBL_GetVehicleTypeEngineTypeListSearch_GetAll()
        {
            var resultGet = LabourDurationBL.GetVehicleTypeEngineTypeListSearch(UserManager.UserInfo, "211");

            Assert.IsTrue(resultGet.Total > 0);
        }

    }

}


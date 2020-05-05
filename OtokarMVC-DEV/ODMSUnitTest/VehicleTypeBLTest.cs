using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.VehicleType;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class VehicleTypeBLTest
    {

        VehicleTypeBL _VehicleTypeBL = new VehicleTypeBL();

        [TestMethod]
        public void VehicleTypeBL_DMLVehicleType_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleTypeIndexViewModel();
            model.VehicleGroupId = guid;
            model.TypeId = 1;
            model.ModelName = guid;
            model.ModelKod = "ATLAS";
            model.TypeName = guid;
            model.TypeSSID = guid;
            model.IsActive = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _VehicleTypeBL.DMLVehicleType(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void VehicleTypeBL_GetVehicleType_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleTypeIndexViewModel();
            model.VehicleGroupId = guid;
            model.TypeId = 1;
            model.ModelName = guid;
            model.ModelKod = "ATLAS";
            model.TypeName = guid;
            model.TypeSSID = guid;
            model.IsActive = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _VehicleTypeBL.DMLVehicleType(UserManager.UserInfo, model);

            var filter = new VehicleTypeIndexViewModel();
            filter.TypeId = result.Model.TypeId;
            filter.ModelKod = "ATLAS";

            var resultGet = _VehicleTypeBL.GetVehicleType(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.ModelName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void VehicleTypeBL_GetVehicleTypeList_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleTypeIndexViewModel();
            model.VehicleGroupId = guid;
            model.TypeId = 1;
            model.ModelName = guid;
            model.ModelKod = "ATLAS";
            model.TypeName = guid;
            model.TypeSSID = guid;
            model.IsActive = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _VehicleTypeBL.DMLVehicleType(UserManager.UserInfo, model);

            int count = 0;
            var filter = new VehicleTypeListModel();
            filter.ModelKod = "ATLAS";

            var resultGet = _VehicleTypeBL.GetVehicleTypeList(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void VehicleTypeBL_DMLVehicleType_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleTypeIndexViewModel();
            model.VehicleGroupId = guid;
            model.TypeId = 1;
            model.ModelName = guid;
            model.ModelKod = "ATLAS";
            model.TypeName = guid;
            model.TypeSSID = guid;
            model.IsActive = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _VehicleTypeBL.DMLVehicleType(UserManager.UserInfo, model);

            var filter = new VehicleTypeListModel();
            filter.ModelKod = "ATLAS";

            int count = 0;
            var resultGet = _VehicleTypeBL.GetVehicleTypeList(UserManager.UserInfo, filter, out count);

            var modelUpdate = new VehicleTypeIndexViewModel();
            modelUpdate.TypeId = resultGet.Data.First().TypeId;
            modelUpdate.VehicleGroupId = guid;

            modelUpdate.ModelName = guid;
            modelUpdate.ModelKod = "ATLAS";
            modelUpdate.TypeName = guid;
            modelUpdate.TypeSSID = guid;



            modelUpdate.CommandType = "U";
            var resultUpdate = _VehicleTypeBL.DMLVehicleType(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void VehicleTypeBL_DMLVehicleType_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleTypeIndexViewModel();
            model.VehicleGroupId = guid;
            model.TypeId = 1;
            model.ModelName = guid;
            model.ModelKod = "ATLAS";
            model.TypeName = guid;
            model.TypeSSID = guid;
            model.IsActive = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _VehicleTypeBL.DMLVehicleType(UserManager.UserInfo, model);

            var filter = new VehicleTypeListModel();
            filter.ModelKod = "ATLAS";

            int count = 0;
            var resultGet = _VehicleTypeBL.GetVehicleTypeList(UserManager.UserInfo, filter, out count);

            var modelDelete = new VehicleTypeIndexViewModel();
            modelDelete.TypeId = resultGet.Data.First().TypeId;
            modelDelete.VehicleGroupId = guid;
            modelDelete.ModelName = guid;
            modelDelete.ModelKod = "ATLAS";
            modelDelete.TypeName = guid;
            modelDelete.TypeSSID = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _VehicleTypeBL.DMLVehicleType(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }

        [TestMethod]
        public void VehicleTypeBL_ListVehicleTypeAsSelectList_GetAll()
        {
            var resultGet = VehicleTypeBL.ListVehicleTypeAsSelectList(UserManager.UserInfo, "ATLAS");

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}


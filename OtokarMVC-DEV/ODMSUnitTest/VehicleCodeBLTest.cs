using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.VehicleCode;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class VehicleCodeBLTest
    {

        VehicleCodeBL _VehicleCodeBL = new VehicleCodeBL();

        [TestMethod]
        public void VehicleCodeBL_DMLVehicleCode_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleCodeIndexViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.EngineType = guid;
            model.VehicleCodeKod = guid;
            model.AdminDesc = guid;
            model.VehicleTypeId = 1;
            model.VehicleTypeName = guid;
            model.VehicleCodeSSID = guid;
            model.IsActive = true;
            model.VehicleGroupId = guid;
            model.ModelName = guid;
            model.VehicleCode = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _VehicleCodeBL.DMLVehicleCode(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void VehicleCodeBL_GetVehicleCode_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleCodeIndexViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.EngineType = guid;
            model.VehicleCodeKod = guid;
            model.AdminDesc = guid;
            model.VehicleTypeId = 1;
            model.VehicleTypeName = guid;
            model.VehicleCodeSSID = guid;
            model.IsActive = true;
            model.VehicleGroupId = guid;
            model.ModelName = guid;
            model.VehicleCode = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _VehicleCodeBL.DMLVehicleCode(UserManager.UserInfo, model);

            var filter = new VehicleCodeIndexViewModel();
            filter.MultiLanguageContentAsText = "TR || TEST";
            filter.VehicleCodeKod = guid;
            filter.VehicleCodeSSID = guid;
            filter.VehicleCode = guid;

            var resultGet = _VehicleCodeBL.GetVehicleCode(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.AdminDesc != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void VehicleCodeBL_GetVehicleCodeList_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleCodeIndexViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.EngineType = guid;
            model.VehicleCodeKod = guid;
            model.AdminDesc = guid;
            model.VehicleTypeId = 1;
            model.VehicleTypeName = guid;
            model.VehicleCodeSSID = guid;
            model.IsActive = true;
            model.VehicleGroupId = guid;
            model.ModelName = guid;
            model.VehicleCode = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _VehicleCodeBL.DMLVehicleCode(UserManager.UserInfo, model);

            int count = 0;
            var filter = new VehicleCodeListModel();
            filter.VehicleCodeKod = guid;
            filter.VehicleCodeSSID = guid;
            filter.VehicleCodeName = guid;
            filter.VehicleCode = guid;

            var resultGet = _VehicleCodeBL.GetVehicleCodeList(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void VehicleCodeBL_DMLVehicleCode_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleCodeIndexViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.EngineType = guid;
            model.VehicleCodeKod = guid;
            model.AdminDesc = guid;
            model.VehicleTypeId = 1;
            model.VehicleTypeName = guid;
            model.VehicleCodeSSID = guid;
            model.IsActive = true;
            model.VehicleGroupId = guid;
            model.ModelName = guid;
            model.VehicleCode = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _VehicleCodeBL.DMLVehicleCode(UserManager.UserInfo, model);

            var filter = new VehicleCodeListModel();
            filter.VehicleCodeKod = guid;
            filter.VehicleCodeSSID = guid;
            filter.VehicleCodeName = guid;
            filter.VehicleCode = guid;

            int count = 0;
            var resultGet = _VehicleCodeBL.GetVehicleCodeList(UserManager.UserInfo, filter, out count);

            var modelUpdate = new VehicleCodeIndexViewModel();
            modelUpdate.MultiLanguageContentAsText = "TR || TEST";
            modelUpdate.EngineType = guid;
            modelUpdate.VehicleCodeKod = guid;
            modelUpdate.AdminDesc = guid;
            modelUpdate.VehicleTypeName = guid;
            modelUpdate.VehicleCodeSSID = guid;
            modelUpdate.VehicleGroupId = guid;
            modelUpdate.ModelName = guid;
            modelUpdate.VehicleCode = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _VehicleCodeBL.DMLVehicleCode(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void VehicleCodeBL_DMLVehicleCode_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleCodeIndexViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.EngineType = guid;
            model.VehicleCodeKod = guid;
            model.AdminDesc = guid;
            model.VehicleTypeId = 1;
            model.VehicleTypeName = guid;
            model.VehicleCodeSSID = guid;
            model.IsActive = true;
            model.VehicleGroupId = guid;
            model.ModelName = guid;
            model.VehicleCode = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _VehicleCodeBL.DMLVehicleCode(UserManager.UserInfo, model);

            var filter = new VehicleCodeListModel();
            filter.VehicleCodeKod = guid;
            filter.VehicleCodeSSID = guid;
            filter.VehicleCodeName = guid;
            filter.VehicleCode = guid;

            int count = 0;
            var resultGet = _VehicleCodeBL.GetVehicleCodeList(UserManager.UserInfo, filter, out count);

            var modelDelete = new VehicleCodeIndexViewModel();
            modelDelete.MultiLanguageContentAsText = "TR || TEST";
            modelDelete.EngineType = guid;
            modelDelete.VehicleCodeKod = guid;
            modelDelete.AdminDesc = guid;
            modelDelete.VehicleTypeName = guid;
            modelDelete.VehicleCodeSSID = guid;
            modelDelete.VehicleGroupId = guid;
            modelDelete.ModelName = guid;
            modelDelete.VehicleCode = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _VehicleCodeBL.DMLVehicleCode(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}


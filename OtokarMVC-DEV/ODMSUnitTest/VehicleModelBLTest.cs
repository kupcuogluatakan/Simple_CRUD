using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.VehicleModel;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class VehicleModelBLTest
    {

        VehicleModelBL _VehicleModelBL = new VehicleModelBL();

        [TestMethod]
        public void VehicleModelBL_DMLVehicleModel_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleModelIndexViewModel();
            model.VehicleModelKod = guid;
            model.VehicleModelName = guid;
            model.VehicleModelSSID = guid;
            model.IsActive = true;
            model.IsCouponCheck = true;
            model.IsPDICheck = true;
            model.VehicleGroupId = 1;
            model.VehicleGroupName = guid;
            model.IsBodyWorkDetailCheck = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _VehicleModelBL.DMLVehicleModel(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void VehicleModelBL_GetVehicleModel_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleModelIndexViewModel();
            model.VehicleModelKod = guid;
            model.VehicleModelName = guid;
            model.VehicleModelSSID = guid;
            model.IsActive = true;
            model.IsCouponCheck = true;
            model.IsPDICheck = true;
            model.VehicleGroupId = 1;
            model.VehicleGroupName = guid;
            model.IsBodyWorkDetailCheck = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _VehicleModelBL.DMLVehicleModel(UserManager.UserInfo, model);

            var filter = new VehicleModelIndexViewModel();

            var resultGet = _VehicleModelBL.GetVehicleModel(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.StatusId > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void VehicleModelBL_GetVehicleModelList_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleModelIndexViewModel();
            model.VehicleModelKod = guid;
            model.VehicleModelName = guid;
            model.VehicleModelSSID = guid;
            model.IsActive = true;
            model.IsCouponCheck = true;
            model.IsPDICheck = true;
            model.VehicleGroupId = 1;
            model.VehicleGroupName = guid;
            model.IsBodyWorkDetailCheck = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _VehicleModelBL.DMLVehicleModel(UserManager.UserInfo, model);

            int count = 0;
            var filter = new VehicleModelListModel();

            var resultGet = _VehicleModelBL.GetVehicleModelList(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void VehicleModelBL_DMLVehicleModel_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleModelIndexViewModel();
            model.VehicleModelKod = guid;
            model.VehicleModelName = guid;
            model.VehicleModelSSID = guid;
            model.IsActive = true;
            model.IsCouponCheck = true;
            model.IsPDICheck = true;
            model.VehicleGroupId = 1;
            model.VehicleGroupName = guid;
            model.IsBodyWorkDetailCheck = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _VehicleModelBL.DMLVehicleModel(UserManager.UserInfo, model);

            var filter = new VehicleModelListModel();

            int count = 0;
            var resultGet = _VehicleModelBL.GetVehicleModelList(UserManager.UserInfo, filter, out count);

            var modelUpdate = new VehicleModelIndexViewModel();
            modelUpdate.VehicleModelKod = guid;
            modelUpdate.VehicleModelName = guid;
            modelUpdate.VehicleModelSSID = guid;
            modelUpdate.VehicleGroupName = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _VehicleModelBL.DMLVehicleModel(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void VehicleModelBL_DMLVehicleModel_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleModelIndexViewModel();
            model.VehicleModelKod = guid;
            model.VehicleModelName = guid;
            model.VehicleModelSSID = guid;
            model.IsActive = true;
            model.IsCouponCheck = true;
            model.IsPDICheck = true;
            model.VehicleGroupId = 1;
            model.VehicleGroupName = guid;
            model.IsBodyWorkDetailCheck = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _VehicleModelBL.DMLVehicleModel(UserManager.UserInfo, model);

            var filter = new VehicleModelListModel();

            int count = 0;
            var resultGet = _VehicleModelBL.GetVehicleModelList(UserManager.UserInfo, filter, out count);

            var modelDelete = new VehicleModelIndexViewModel();
            modelDelete.VehicleModelKod = guid;
            modelDelete.VehicleModelName = guid;
            modelDelete.VehicleModelSSID = guid;
            modelDelete.VehicleGroupName = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _VehicleModelBL.DMLVehicleModel(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }

        [TestMethod]
        public void VehicleModelBL_ListVehicleModelAsSelectList_GetAll()
        {
            var resultGet = VehicleModelBL.ListVehicleModelAsSelectList(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }

    }

}


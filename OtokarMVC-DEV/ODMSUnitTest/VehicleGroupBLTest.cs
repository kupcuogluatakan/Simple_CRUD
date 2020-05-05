using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.VehicleGroup;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class VehicleGroupBLTest
    {

        VehicleGroupBL _VehicleGroupBL = new VehicleGroupBL();

        [TestMethod]
        public void VehicleGroupBL_DMLVehicleGroup_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleGroupIndexViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.VehicleGroupId = 1;
            model.AdminDesc = guid;
            model.IsActive = true;
            model.GroupName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _VehicleGroupBL.DMLVehicleGroup(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void VehicleGroupBL_GetVehicleGroup_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleGroupIndexViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.VehicleGroupId = 1;
            model.AdminDesc = guid;
            model.IsActive = true;
            model.GroupName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _VehicleGroupBL.DMLVehicleGroup(UserManager.UserInfo, model);

            var filter = new VehicleGroupIndexViewModel();
            filter.MultiLanguageContentAsText = "TR || TEST";

            var resultGet = _VehicleGroupBL.GetVehicleGroup(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.AdminDesc != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void VehicleGroupBL_GetVehicleGroupList_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleGroupIndexViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.VehicleGroupId = 1;
            model.AdminDesc = guid;
            model.IsActive = true;
            model.GroupName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _VehicleGroupBL.DMLVehicleGroup(UserManager.UserInfo, model);

            int count = 0;
            var filter = new VehicleGroupListModel();

            var resultGet = _VehicleGroupBL.GetVehicleGroupList(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void VehicleGroupBL_DMLVehicleGroup_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleGroupIndexViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.VehicleGroupId = 1;
            model.AdminDesc = guid;
            model.IsActive = true;
            model.GroupName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _VehicleGroupBL.DMLVehicleGroup(UserManager.UserInfo, model);

            var filter = new VehicleGroupListModel();

            int count = 0;
            var resultGet = _VehicleGroupBL.GetVehicleGroupList(UserManager.UserInfo, filter, out count);

            var modelUpdate = new VehicleGroupIndexViewModel();
            modelUpdate.MultiLanguageContentAsText = "TR || TEST";

            modelUpdate.AdminDesc = guid;

            modelUpdate.GroupName = guid;


            modelUpdate.CommandType = "U";
            var resultUpdate = _VehicleGroupBL.DMLVehicleGroup(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void VehicleGroupBL_DMLVehicleGroup_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleGroupIndexViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.VehicleGroupId = 1;
            model.AdminDesc = guid;
            model.IsActive = true;
            model.GroupName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _VehicleGroupBL.DMLVehicleGroup(UserManager.UserInfo, model);

            var filter = new VehicleGroupListModel();

            int count = 0;
            var resultGet = _VehicleGroupBL.GetVehicleGroupList(UserManager.UserInfo, filter, out count);

            var modelDelete = new VehicleGroupIndexViewModel();
            modelDelete.MultiLanguageContentAsText = "TR || TEST";
            modelDelete.AdminDesc = guid;
            modelDelete.GroupName = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _VehicleGroupBL.DMLVehicleGroup(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }

        [TestMethod]
        public void VehicleGroupBL_ListVehicleGroupAsSelectList_GetAll()
        {
            var resultGet = VehicleGroupBL.ListVehicleGroupAsSelectList(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}


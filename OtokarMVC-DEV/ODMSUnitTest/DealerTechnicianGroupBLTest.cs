using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.DealerTechnicianGroup;


namespace ODMSUnitTest
{

    [TestClass]
    public class DealerTechnicianGroupBLTest
    {

        DealerTechnicianGroupBL _DealerTechnicianGroupBL = new DealerTechnicianGroupBL();

        [TestMethod]
        public void DealerTechnicianGroupBL_DMLDealerTechnicianGroup_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerTechnicianGroupViewModel();
            model.DealerTechnicianGroupId = 1;
            model.TechnicianGroupName = guid;
            model.WorkshopTypeName = guid;
            model.Description = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.VehicleModelKod = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.VehicleModelKodOld = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _DealerTechnicianGroupBL.DMLDealerTechnicianGroup(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void DealerTechnicianGroupBL_GetDealerTechnicianGroup_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerTechnicianGroupViewModel();
            model.DealerTechnicianGroupId = 1;
            model.TechnicianGroupName = guid;
            model.WorkshopTypeName = guid;
            model.Description = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.VehicleModelKod = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.VehicleModelKodOld = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _DealerTechnicianGroupBL.DMLDealerTechnicianGroup(UserManager.UserInfo, model);

            var filter = new DealerTechnicianGroupViewModel();
            filter.DealerTechnicianGroupId = result.Model.DealerTechnicianGroupId;
            filter.DealerId = UserManager.UserInfo.DealerID;

            var resultGet = _DealerTechnicianGroupBL.GetDealerTechnicianGroup(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.DealerName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void DealerTechnicianGroupBL_GetDealerVehicleGroupRelationCount_GetModel()
        {
            var resultGet = _DealerTechnicianGroupBL.GetDealerVehicleGroupRelationCount(UserManager.UserInfo.GetUserDealerId(), 4);

            Assert.IsTrue(resultGet.Model > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void DealerTechnicianGroupBL_ListDealerTechnicianGroups_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerTechnicianGroupViewModel();
            model.DealerTechnicianGroupId = 1;
            model.TechnicianGroupName = guid;
            model.WorkshopTypeName = guid;
            model.Description = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.VehicleModelKod = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.VehicleModelKodOld = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _DealerTechnicianGroupBL.DMLDealerTechnicianGroup(UserManager.UserInfo, model);

            int count = 0;
            var filter = new DealerTechnicianGroupListModel();
            filter.DealerId = UserManager.UserInfo.DealerID;
            filter.DealerTechnicianGroupId = result.Model.DealerTechnicianGroupId;

            var resultGet = _DealerTechnicianGroupBL.ListDealerTechnicianGroups(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void DealerTechnicianGroupBL_DMLDealerTechnicianGroup_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerTechnicianGroupViewModel();
            model.DealerTechnicianGroupId = 1;
            model.TechnicianGroupName = guid;
            model.WorkshopTypeName = guid;
            model.Description = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.VehicleModelKod = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.VehicleModelKodOld = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _DealerTechnicianGroupBL.DMLDealerTechnicianGroup(UserManager.UserInfo, model);

            var filter = new DealerTechnicianGroupListModel();
            filter.DealerId = UserManager.UserInfo.DealerID;

            int count = 0;
            var resultGet = _DealerTechnicianGroupBL.ListDealerTechnicianGroups(UserManager.UserInfo, filter, out count);

            var modelUpdate = new DealerTechnicianGroupViewModel();
            modelUpdate.DealerTechnicianGroupId = resultGet.Data.First().DealerTechnicianGroupId;

            modelUpdate.TechnicianGroupName = guid;
            modelUpdate.WorkshopTypeName = guid;
            modelUpdate.Description = guid;
            modelUpdate.DealerId = UserManager.UserInfo.DealerID;
            modelUpdate.DealerName = guid;
            modelUpdate.VehicleModelKod = guid;

            modelUpdate.IsActiveName = guid;
            modelUpdate.VehicleModelKodOld = guid;


            modelUpdate.CommandType = "U";
            var resultUpdate = _DealerTechnicianGroupBL.DMLDealerTechnicianGroup(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void DealerTechnicianGroupBL_DMLDealerTechnicianGroup_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerTechnicianGroupViewModel();
            model.DealerTechnicianGroupId = 1;
            model.TechnicianGroupName = guid;
            model.WorkshopTypeName = guid;
            model.Description = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.VehicleModelKod = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.VehicleModelKodOld = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _DealerTechnicianGroupBL.DMLDealerTechnicianGroup(UserManager.UserInfo, model);

            var filter = new DealerTechnicianGroupListModel();
            filter.DealerId = UserManager.UserInfo.DealerID;

            int count = 0;
            var resultGet = _DealerTechnicianGroupBL.ListDealerTechnicianGroups(UserManager.UserInfo, filter, out count);

            var modelDelete = new DealerTechnicianGroupViewModel();
            modelDelete.DealerTechnicianGroupId = resultGet.Data.First().DealerTechnicianGroupId;
            modelDelete.TechnicianGroupName = guid;
            modelDelete.WorkshopTypeName = guid;
            modelDelete.Description = guid;
            modelDelete.DealerId = UserManager.UserInfo.DealerID;
            modelDelete.DealerName = guid;
            modelDelete.VehicleModelKod = guid;
            modelDelete.IsActiveName = guid;
            modelDelete.VehicleModelKodOld = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _DealerTechnicianGroupBL.DMLDealerTechnicianGroup(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}


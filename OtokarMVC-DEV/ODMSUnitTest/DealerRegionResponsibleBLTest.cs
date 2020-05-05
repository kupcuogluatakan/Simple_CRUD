using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.DealerRegionResponsible;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class DealerRegionResponsibleBLTest
    {

        DealerRegionResponsibleBL _DealerRegionResponsibleBL = new DealerRegionResponsibleBL();

        [TestMethod]
        public void DealerRegionResponsibleBL_DMLDealerRegionResponsible_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerRegionResponsibleDetailModel();
            model.DealerRegionId = 1;
            model.DealerRegionName = guid;
            model.Name = guid;
            model.Surname = guid;
            model.Phone = guid;
            model.Email = guid;
            model.DealerRegionIdString = guid;
            model.UserIdString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DealerRegionResponsibleBL.DMLDealerRegionResponsible(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void DealerRegionResponsibleBL_GetDealerRegionResponsible_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerRegionResponsibleDetailModel();
            model.DealerRegionId = UserManager.UserInfo.UserId;
            model.DealerRegionName = guid;
            model.Name = guid;
            model.Surname = guid;
            model.Phone = guid;
            model.Email = guid;
            model.DealerRegionIdString = guid;
            model.UserIdString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DealerRegionResponsibleBL.DMLDealerRegionResponsible(UserManager.UserInfo, model);

            var filter = new DealerRegionResponsibleDetailModel();
            filter.DealerRegionId = result.Model.DealerRegionId;

            var resultGet = _DealerRegionResponsibleBL.GetDealerRegionResponsible(filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.Name != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void DealerRegionResponsibleBL_ListDealerRegionResponsibles_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerRegionResponsibleDetailModel();
            model.DealerRegionName = guid;
            model.Name = guid;
            model.Surname = guid;
            model.Phone = guid;
            model.Email = guid;
            model.DealerRegionIdString = guid;
            model.UserIdString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DealerRegionResponsibleBL.DMLDealerRegionResponsible(UserManager.UserInfo, model);

            int count = 0;
            var filter = new DealerRegionResponsibleListModel();
            filter.DealerRegionId = result.Model.DealerRegionId;

            var resultGet = _DealerRegionResponsibleBL.ListDealerRegionResponsibles(filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void DealerRegionResponsibleBL_DMLDealerRegionResponsible_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerRegionResponsibleDetailModel();
            model.DealerRegionName = guid;
            model.Name = guid;
            model.Surname = guid;
            model.Phone = guid;
            model.Email = guid;
            model.DealerRegionIdString = guid;
            model.UserIdString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DealerRegionResponsibleBL.DMLDealerRegionResponsible(UserManager.UserInfo, model);

            var filter = new DealerRegionResponsibleListModel();
            filter.DealerRegionId = result.Model.DealerRegionId;

            int count = 0;
            var resultGet = _DealerRegionResponsibleBL.ListDealerRegionResponsibles(filter, out count);

            var modelUpdate = new DealerRegionResponsibleDetailModel();
            modelUpdate.DealerRegionId = resultGet.Data.First().DealerRegionId;
            modelUpdate.DealerRegionName = guid;
            modelUpdate.Name = guid;
            modelUpdate.Surname = guid;
            modelUpdate.Phone = guid;
            modelUpdate.Email = guid;
            modelUpdate.DealerRegionIdString = guid;
            modelUpdate.UserIdString = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _DealerRegionResponsibleBL.DMLDealerRegionResponsible(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void DealerRegionResponsibleBL_DMLDealerRegionResponsible_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerRegionResponsibleDetailModel();
            model.DealerRegionName = guid;
            model.Name = guid;
            model.Surname = guid;
            model.Phone = guid;
            model.Email = guid;
            model.DealerRegionIdString = guid;
            model.UserIdString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DealerRegionResponsibleBL.DMLDealerRegionResponsible(UserManager.UserInfo, model);

            var filter = new DealerRegionResponsibleListModel();
            filter.DealerRegionId = result.Model.DealerRegionId;

            int count = 0;
            var resultGet = _DealerRegionResponsibleBL.ListDealerRegionResponsibles(filter, out count);

            var modelDelete = new DealerRegionResponsibleDetailModel();
            modelDelete.DealerRegionId = resultGet.Data.First().DealerRegionId;
            modelDelete.DealerRegionName = guid;
            modelDelete.Name = guid;
            modelDelete.Surname = guid;
            modelDelete.Phone = guid;
            modelDelete.Email = guid;
            modelDelete.DealerRegionIdString = guid;
            modelDelete.UserIdString = guid;



            modelDelete.CommandType = "D";
            var resultDelete = _DealerRegionResponsibleBL.DMLDealerRegionResponsible(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}


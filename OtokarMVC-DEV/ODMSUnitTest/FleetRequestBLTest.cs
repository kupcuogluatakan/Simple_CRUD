using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.FleetRequest;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class FleetRequestBLTest
    {

        FleetRequestBL _FleetRequestBL = new FleetRequestBL();

        [TestMethod]
        public void FleetRequestBL_DMLFleetRequest_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FleetRequestViewModel();
            model.FleetRequestId = 1;
            model.Description = guid;
            model.StatusName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FleetRequestBL.DMLFleetRequest(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void FleetRequestBL_GetFleetRequest_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FleetRequestViewModel();
            model.FleetRequestId = 1;
            model.Description = guid;
            model.StatusName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FleetRequestBL.DMLFleetRequest(UserManager.UserInfo, model);

            var filter = new FleetRequestViewModel();
            filter.FleetRequestId = result.Model.FleetRequestId;
            var resultGet = _FleetRequestBL.GetFleetRequest(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.Description != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void FleetRequestBL_ListFleetRequests_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FleetRequestViewModel();
            model.FleetRequestId = 1;
            model.Description = guid;
            model.StatusName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FleetRequestBL.DMLFleetRequest(UserManager.UserInfo, model);

            int count = 0;
            var filter = new FleetRequestListModel();
            filter.DealerId = UserManager.UserInfo.DealerID;

            var resultGet = _FleetRequestBL.ListFleetRequests(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void FleetRequestBL_DMLFleetRequest_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FleetRequestViewModel();
            model.FleetRequestId = 1;
            model.Description = guid;
            model.StatusName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FleetRequestBL.DMLFleetRequest(UserManager.UserInfo, model);

            var filter = new FleetRequestListModel();
            filter.DealerId = UserManager.UserInfo.DealerID;

            int count = 0;
            var resultGet = _FleetRequestBL.ListFleetRequests(UserManager.UserInfo, filter, out count);

            var modelUpdate = new FleetRequestViewModel();
            modelUpdate.FleetRequestId = resultGet.Data.First().FleetRequestId;
            modelUpdate.Description = guid;
            modelUpdate.StatusName = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _FleetRequestBL.DMLFleetRequest(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void FleetRequestBL_DMLFleetRequest_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FleetRequestViewModel();
            model.FleetRequestId = 1;
            model.Description = guid;
            model.StatusName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FleetRequestBL.DMLFleetRequest(UserManager.UserInfo, model);

            var filter = new FleetRequestListModel();
            filter.DealerId = UserManager.UserInfo.DealerID;

            int count = 0;
            var resultGet = _FleetRequestBL.ListFleetRequests(UserManager.UserInfo, filter, out count);

            var modelDelete = new FleetRequestViewModel();
            modelDelete.FleetRequestId = resultGet.Data.First().FleetRequestId;
            modelDelete.Description = guid;
            modelDelete.StatusName = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _FleetRequestBL.DMLFleetRequest(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}


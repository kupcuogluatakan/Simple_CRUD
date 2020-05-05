using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.FleetRequestConfirm;


namespace ODMSUnitTest
{

    [TestClass]
    public class FleetRequestConfirmBLTest
    {

        FleetRequestConfirmBL _FleetRequestConfirmBL = new FleetRequestConfirmBL();

        [TestMethod]
        public void FleetRequestConfirmBL_DMLFleetRequestConfirm_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FleetRequestConfirmViewModel();
            model.FleetRequestId = 1;
            model.Description = guid;
            model.StatusName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FleetRequestConfirmBL.DMLFleetRequestConfirm(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void FleetRequestConfirmBL_ListFleetRequestConfirm_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FleetRequestConfirmViewModel();
            model.FleetRequestId = 1;
            model.Description = guid;
            model.StatusName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FleetRequestConfirmBL.DMLFleetRequestConfirm(UserManager.UserInfo, model);

            int count = 0;
            var filter = new FleetRequestConfirmListModel();

            var resultGet = _FleetRequestConfirmBL.ListFleetRequestConfirm(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void FleetRequestConfirmBL_DMLFleetRequestConfirm_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FleetRequestConfirmViewModel();
            model.FleetRequestId = 1;
            model.Description = guid;
            model.StatusName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FleetRequestConfirmBL.DMLFleetRequestConfirm(UserManager.UserInfo, model);

            var filter = new FleetRequestConfirmListModel();

            int count = 0;
            var resultGet = _FleetRequestConfirmBL.ListFleetRequestConfirm(UserManager.UserInfo, filter, out count);

            var modelUpdate = new FleetRequestConfirmViewModel();
            modelUpdate.FleetRequestId = resultGet.Data.First().FleetRequestId;
            modelUpdate.Description = guid;
            modelUpdate.StatusName = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _FleetRequestConfirmBL.DMLFleetRequestConfirm(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void FleetRequestConfirmBL_DMLFleetRequestConfirm_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FleetRequestConfirmViewModel();
            model.FleetRequestId = 1;
            model.Description = guid;
            model.StatusName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FleetRequestConfirmBL.DMLFleetRequestConfirm(UserManager.UserInfo, model);

            var filter = new FleetRequestConfirmListModel();

            int count = 0;
            var resultGet = _FleetRequestConfirmBL.ListFleetRequestConfirm(UserManager.UserInfo, filter, out count);

            var modelDelete = new FleetRequestConfirmViewModel();
            modelDelete.FleetRequestId = resultGet.Data.First().FleetRequestId;
            modelDelete.Description = guid;
            modelDelete.StatusName = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _FleetRequestConfirmBL.DMLFleetRequestConfirm(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}


using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.Fleet;


namespace ODMSUnitTest
{

    [TestClass]
    public class FleetBLTest
    {

        FleetBL _FleetBL = new FleetBL();

        [TestMethod]
        public void FleetBL_DMLFleet_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FleetViewModel();
            model.FleetName = guid;
            model.FleetCode = guid;
            model.IsConstrictedName = guid;
            model.IsVinControl = true;
            model.HasContent = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FleetBL.DMLFleet(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void FleetBL_GetFleet_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FleetViewModel();
            model.FleetName = guid;
            model.FleetCode = guid;
            model.IsConstrictedName = guid;
            model.IsVinControl = true;
            model.HasContent = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FleetBL.DMLFleet(UserManager.UserInfo, model);

            var filter = new FleetViewModel();
            filter.IdFleet = result.Model.IdFleet;
            filter.FleetCode = guid;

            var resultGet = _FleetBL.GetFleet(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.FleetName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void FleetBL_ListFleet_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FleetViewModel();
            model.FleetName = guid;
            model.FleetCode = guid;
            model.IsConstrictedName = guid;
            model.IsVinControl = true;
            model.HasContent = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FleetBL.DMLFleet(UserManager.UserInfo, model);

            int count = 0;
            var filter = new FleetListModel();
            filter.FleetCode = guid;

            var resultGet = _FleetBL.ListFleet(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void FleetBL_DMLFleet_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FleetViewModel();
            model.FleetName = guid;
            model.FleetCode = guid;
            model.IsConstrictedName = guid;
            model.IsVinControl = true;
            model.HasContent = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FleetBL.DMLFleet(UserManager.UserInfo, model);

            var filter = new FleetListModel();
            filter.FleetCode = guid;

            int count = 0;
            var resultGet = _FleetBL.ListFleet(UserManager.UserInfo, filter, out count);

            var modelUpdate = new FleetViewModel();
            modelUpdate.IdFleet = resultGet.Data.First().IdFleet;
            modelUpdate.FleetName = guid;
            modelUpdate.FleetCode = guid;
            modelUpdate.IsConstrictedName = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _FleetBL.DMLFleet(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void FleetBL_DMLFleet_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FleetViewModel();
            model.FleetName = guid;
            model.FleetCode = guid;
            model.IsConstrictedName = guid;
            model.IsVinControl = true;
            model.HasContent = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FleetBL.DMLFleet(UserManager.UserInfo, model);

            var filter = new FleetListModel();
            filter.FleetCode = guid;

            int count = 0;
            var resultGet = _FleetBL.ListFleet(UserManager.UserInfo, filter, out count);

            var modelDelete = new FleetViewModel();
            modelDelete.IdFleet = resultGet.Data.First().IdFleet;
            modelDelete.FleetName = guid;
            modelDelete.FleetCode = guid;
            modelDelete.IsConstrictedName = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _FleetBL.DMLFleet(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}


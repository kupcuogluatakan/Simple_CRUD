using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.WorkHour;

namespace ODMSUnitTest
{

    [TestClass]
    public class WorkHourBLTest
    {

        WorkHourBL _WorkHourBL = new WorkHourBL();

        [TestMethod]
        public void WorkHourBL_DMLWorkHour_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new WorkHourViewModel();
            model.WorkHourId = 1;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.Description = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _WorkHourBL.DMLWorkHour(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void WorkHourBL_GetWorkHour_GetModel()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new WorkHourViewModel();
            model.WorkHourId = 1;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.Description = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _WorkHourBL.DMLWorkHour(UserManager.UserInfo, model);

            var resultGet = _WorkHourBL.GetWorkHour(UserManager.UserInfo, result.Model.WorkHourId);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.Description != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkHourBL_ListWorkHours_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new WorkHourViewModel();
            model.WorkHourId = 1;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.Description = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _WorkHourBL.DMLWorkHour(UserManager.UserInfo, model);

            int count = 0;
            var filter = new WorkHourListModel();
            filter.DealerId = UserManager.UserInfo.DealerID;

            var resultGet = _WorkHourBL.ListWorkHours(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void WorkHourBL_DMLWorkHour_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new WorkHourViewModel();
            model.WorkHourId = 1;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.Description = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _WorkHourBL.DMLWorkHour(UserManager.UserInfo, model);

            var filter = new WorkHourListModel();
            filter.DealerId = UserManager.UserInfo.DealerID;

            int count = 0;
            var resultGet = _WorkHourBL.ListWorkHours(UserManager.UserInfo, filter, out count);

            var modelUpdate = new WorkHourViewModel();
            modelUpdate.WorkHourId = resultGet.Data.First().WorkHourId;
            modelUpdate.DealerId = UserManager.UserInfo.DealerID;
            modelUpdate.DealerName = guid;
            modelUpdate.Description = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _WorkHourBL.DMLWorkHour(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void WorkHourBL_DMLWorkHour_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new WorkHourViewModel();
            model.WorkHourId = 1;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.Description = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _WorkHourBL.DMLWorkHour(UserManager.UserInfo, model);

            var filter = new WorkHourListModel();
            filter.DealerId = UserManager.UserInfo.DealerID;

            int count = 0;
            var resultGet = _WorkHourBL.ListWorkHours(UserManager.UserInfo, filter, out count);

            var modelDelete = new WorkHourViewModel();
            modelDelete.WorkHourId = resultGet.Data.First().WorkHourId;
            modelDelete.DealerId = UserManager.UserInfo.DealerID;
            modelDelete.DealerName = guid;
            modelDelete.Description = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _WorkHourBL.DMLWorkHour(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }

        [TestMethod]
        public void WorkHourBL_GetTeaBreakList_GetAll()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new WorkHourViewModel();
            model.WorkHourId = 1;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.Description = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _WorkHourBL.DMLWorkHour(UserManager.UserInfo, model);

            var resultGet = _WorkHourBL.GetTeaBreakList(result.Model.WorkHourId);

            Assert.IsTrue(resultGet.Total > 0);
        }

    }

}


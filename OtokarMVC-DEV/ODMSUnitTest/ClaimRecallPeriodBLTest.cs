using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.ClaimRecallPeriod;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class ClaimRecallPeriodBLTest
    {

        ClaimRecallPeriodBL _ClaimRecallPeriodBL = new ClaimRecallPeriodBL();

        [TestMethod]
        public void ClaimRecallPeriodBL_DMLClaimRecallPeriod_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new ClaimRecallPeriodViewModel();
            model.ClaimRecallPeriodId = 1;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _ClaimRecallPeriodBL.DMLClaimRecallPeriod(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void ClaimRecallPeriodBL_GetClaimRecallPeriod_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new ClaimRecallPeriodViewModel();
            model.ClaimRecallPeriodId = 1;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _ClaimRecallPeriodBL.DMLClaimRecallPeriod(UserManager.UserInfo, model);

            var filter = new ClaimRecallPeriodViewModel();
            filter.ClaimRecallPeriodId = result.Model.ClaimRecallPeriodId;

            var resultGet = _ClaimRecallPeriodBL.GetClaimRecallPeriod(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.IsActive && resultGet.IsSuccess);
        }

        [TestMethod]
        public void ClaimRecallPeriodBL_ListClaimRecallPeriods_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new ClaimRecallPeriodViewModel();
            model.ClaimRecallPeriodId = 1;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _ClaimRecallPeriodBL.DMLClaimRecallPeriod(UserManager.UserInfo, model);

            int count = 0;
            var filter = new ClaimRecallPeriodListModel();
            filter.ClaimRecallPeriodId = result.Model.ClaimRecallPeriodId;

            var resultGet = _ClaimRecallPeriodBL.ListClaimRecallPeriods(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}


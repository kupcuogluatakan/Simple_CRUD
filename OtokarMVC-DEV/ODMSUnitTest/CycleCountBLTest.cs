using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.CycleCount;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class CycleCountBLTest
    {

        CycleCountBL _CycleCountBL = new CycleCountBL();

        [TestMethod]
        public void CycleCountBL_DMLCycleCount_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CycleCountViewModel();
            model.CycleCountId = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.CycleCountName = guid;
            model.DisplayCurrentAmount = true;
            model.DisplayCurrentAmountName = guid;
            model.CycleCountStatusName = guid;
            model.encryptedId = guid;
            model.IsClosedApproveTab = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _CycleCountBL.DMLCycleCount(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void CycleCountBL_GetCycleCount_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CycleCountViewModel();
            model.CycleCountId = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.CycleCountName = guid;
            model.DisplayCurrentAmount = true;
            model.DisplayCurrentAmountName = guid;
            model.CycleCountStatusName = guid;
            model.encryptedId = guid;
            model.IsClosedApproveTab = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _CycleCountBL.DMLCycleCount(UserManager.UserInfo, model);

            var filter = new CycleCountViewModel();
            filter.CycleCountId = result.Model.CycleCountId;
            filter.DealerId = UserManager.UserInfo.DealerID;

            var resultGet = _CycleCountBL.GetCycleCount(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.CycleCountName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void CycleCountBL_ListCycleCount_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CycleCountViewModel();
            model.CycleCountId = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.CycleCountName = guid;
            model.DisplayCurrentAmount = true;
            model.DisplayCurrentAmountName = guid;
            model.CycleCountStatusName = guid;
            model.encryptedId = guid;
            model.IsClosedApproveTab = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _CycleCountBL.DMLCycleCount(UserManager.UserInfo, model);

            int count = 0;
            var filter = new CycleCountListModel();
            filter.CycleCountId = Convert.ToInt32(result.Model.CycleCountId);
            var resultGet = _CycleCountBL.ListCycleCount(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}


using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.CycleCountPlan;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class CycleCountPlanBLTest
    {

        CycleCountPlanBL _CycleCountPlanBL = new CycleCountPlanBL();

        [TestMethod]
        public void CycleCountPlanBL_DMLCycleCountPlan_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CycleCountPlanViewModel();
            model.CycleCountPlanId = 1;
            model.CycleCountId = 1;
            model.WarehouseName = guid;
            model.RackName = guid;
            model.StockCardName = guid;
            model.PartId = 39399;
            model.StockTypeName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _CycleCountPlanBL.DMLCycleCountPlan(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void CycleCountPlanBL_GetCycleCountPlan_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CycleCountPlanViewModel();
            model.CycleCountPlanId = 1;
            model.CycleCountId = 1;
            model.WarehouseName = guid;
            model.RackName = guid;
            model.StockCardName = guid;
            model.PartId = 39399;
            model.StockTypeName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _CycleCountPlanBL.DMLCycleCountPlan(UserManager.UserInfo, model);

            var filter = new CycleCountPlanViewModel();
            filter.CycleCountId = result.Model.CycleCountId;
            filter.PartId = 39399;

            var resultGet = _CycleCountPlanBL.GetCycleCountPlan(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.RackId > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void CycleCountPlanBL_ListCycleCountPlans_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CycleCountPlanViewModel();
            model.CycleCountPlanId = 1;
            model.CycleCountId = 1;
            model.WarehouseName = guid;
            model.RackName = guid;
            model.StockCardName = guid;
            model.PartId = 39399;
            model.StockTypeName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _CycleCountPlanBL.DMLCycleCountPlan(UserManager.UserInfo, model);

            int count = 0;
            var filter = new CycleCountPlanListModel();

            var resultGet = _CycleCountPlanBL.ListCycleCountPlans(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void CycleCountPlanBL_ListById_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CycleCountPlanViewModel();
            model.CycleCountPlanId = 1;
            model.CycleCountId = 1;
            model.WarehouseName = guid;
            model.RackName = guid;
            model.StockCardName = guid;
            model.PartId = 39399;
            model.StockTypeName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _CycleCountPlanBL.DMLCycleCountPlan(UserManager.UserInfo, model);

            int count = 0;
            var filter = new CycleCountPlanViewModel();
            filter.PartId = 39399;

            var resultGet = _CycleCountPlanBL.ListById(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}


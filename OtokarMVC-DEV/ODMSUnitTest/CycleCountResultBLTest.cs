using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.CycleCountResult;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class CycleCountResultBLTest
    {

        CycleCountResultBL _CycleCountResultBL = new CycleCountResultBL();

        [TestMethod]
        public void CycleCountResultBL_DMLCycleCountResult_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CycleCountResultViewModel();
            model.CycleCountResultId = 1;
            model.CycleCountId = 1;
            model.WarehouseName = guid;
            model.RackName = guid;
            model.StockCardName = guid;
            model.BeforeCountQuantity = 1;
            model.AfterCountQuantity = 1;
            model.ApprovedCountQuantity = 1;
            model.RejectDescription = guid;
            model.CountUser = 1;
            model.CountUserName = guid;
            model.ApproveUser = 1;
            model.ApproveUserName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _CycleCountResultBL.DMLCycleCountResult(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void CycleCountResultBL_GetCycleCountResult_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CycleCountResultViewModel();
            model.CycleCountResultId = 1;
            model.CycleCountId = 1;
            model.WarehouseName = guid;
            model.RackName = guid;
            model.StockCardName = guid;
            model.BeforeCountQuantity = 1;
            model.AfterCountQuantity = 1;
            model.ApprovedCountQuantity = 1;
            model.RejectDescription = guid;
            model.CountUser = 1;
            model.CountUserName = guid;
            model.ApproveUser = 1;
            model.ApproveUserName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _CycleCountResultBL.DMLCycleCountResult(UserManager.UserInfo, model);

            var filter = new CycleCountResultViewModel();
            filter.CycleCountResultId = result.Model.CycleCountResultId;

            var resultGet = _CycleCountResultBL.GetCycleCountResult(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.RackId > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void CycleCountResultBL_ListCycleCountResults_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CycleCountResultViewModel();
            model.CycleCountResultId = 1;
            model.CycleCountId = 1;
            model.WarehouseName = guid;
            model.RackName = guid;
            model.StockCardName = guid;
            model.BeforeCountQuantity = 1;
            model.AfterCountQuantity = 1;
            model.ApprovedCountQuantity = 1;
            model.RejectDescription = guid;
            model.CountUser = 1;
            model.CountUserName = guid;
            model.ApproveUser = 1;
            model.ApproveUserName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _CycleCountResultBL.DMLCycleCountResult(UserManager.UserInfo, model);

            int count = 0;
            var filter = new CycleCountResultListModel();
            filter.PartCode = "M.162127";
            filter.CurrencyCode = guid;
            filter.CycleCountResultId = result.Model.CycleCountResultId;

            var resultGet = _CycleCountResultBL.ListCycleCountResults(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}


using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.CycleCountStockDiff;
using ODMSModel.CycleCountResult;


namespace ODMSUnitTest
{

    [TestClass]
    public class CycleCountStockDiffBLTest
    {

        CycleCountStockDiffBL _CycleCountStockDiffBL = new CycleCountStockDiffBL();

        [TestMethod]
        public void CycleCountStockDiffBL_DMLCycleCountStockDiff_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CycleCountStockDiffViewModel();
            model.CycleCountStockDiffId = 1;
            model.CycleCountId = 1;
            model.WarehouseName = guid;
            model.StockTypeName = guid;
            model.StockCardName = guid;
            model.BeforeCount = 1;
            model.AfterCount = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _CycleCountStockDiffBL.DMLCycleCountStockDiff(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void CycleCountStockDiffBL_DMLCycleCountStockDiff_Insert_1()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CycleCountStockDiffViewModel();
            model.CycleCountId = 1;
            model.StockCardId = 1;
            model.WarehouseId = 80;
            model.WarehouseName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _CycleCountStockDiffBL.DMLCycleCountStockDiff(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void CycleCountStockDiffBL_GetStockTypeDetailQuantity_GetModel()
        {
            var filter = new CycleCountStockDiffViewModel();
            filter.WarehouseId = 80;

            var resultGet = _CycleCountStockDiffBL.GetStockTypeDetailQuantity(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model > 0 && resultGet.IsSuccess);
        }

        /// <summary>
        /// TODO: Sonuncu parametreyi parametrik yap!!!
        /// </summary>
        [TestMethod]
        public void CycleCountStockDiffBL_GetCycleCountStockDiffTotalQuantity_GetModel()
        {
            var resultGet = _CycleCountStockDiffBL.GetCycleCountStockDiffTotalQuantity(80, 148148, 1);

            Assert.IsTrue(resultGet.Model > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void CycleCountStockDiffBL_ListCycleCountStockDiffs_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CycleCountResultAuditViewModel();
            model.CycleCountId = 1;
            model.StockCardId = 1;
            model.WarehouseId = 1;
            model.WarehouseName = guid;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.ExpectedQty = 1;
            model.CurrentQty = guid;
            model.DiffQty = guid;
            model.BfrQty = 1;
            model.PartId = 39399;
            model.NewQtyValues = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _CycleCountStockDiffBL.DMLCycleCountStockDiff(UserManager.UserInfo, model);

            int count = 0;
            var filter = new CycleCountStockDiffListModel();

            var resultGet = _CycleCountStockDiffBL.ListCycleCountStockDiffs(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void CycleCountStockDiffBL_ListStokTypeAudit_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CycleCountResultAuditViewModel();
            model.CycleCountId = 1;
            model.StockCardId = 1;
            model.WarehouseId = 1;
            model.WarehouseName = guid;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.ExpectedQty = 1;
            model.CurrentQty = guid;
            model.DiffQty = guid;
            model.BfrQty = 1;
            model.PartId = 39399;
            model.NewQtyValues = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _CycleCountStockDiffBL.DMLCycleCountStockDiff(UserManager.UserInfo, model);

            var filter = new CycleCountResultAuditViewModel();
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            filter.PartId = 39399;

            var resultGet = _CycleCountStockDiffBL.ListStokTypeAudit(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}


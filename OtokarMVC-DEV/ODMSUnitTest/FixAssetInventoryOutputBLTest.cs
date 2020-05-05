using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.FixAssetInventoryOutput;


namespace ODMSUnitTest
{

    [TestClass]
    public class FixAssetInventoryOutputBLTest
    {

        FixAssetInventoryOutputBL _FixAssetInventoryOutputBL = new FixAssetInventoryOutputBL();

        [TestMethod]
        public void FixAssetInventoryOutputBL_DMLFixAssetInventoryOutput_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FixAssetInventoryOutputViewModel();
            model.FixAssetName = guid;
            model.SerialNo = guid;
            model.ExitDesc = guid;
            model.StockTypeName = guid;
            model.FixAssetStatusName = guid;
            model.FixAssetCode = guid;
            model.Description = guid;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.PartCode = "M.162127";
            model.RestockReason = guid;
            model.SubmitFinished = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FixAssetInventoryOutputBL.DMLFixAssetInventoryOutput(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void FixAssetInventoryOutputBL_GetFixAssetInventoryOutput_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FixAssetInventoryOutputViewModel();
            model.FixAssetName = guid;
            model.SerialNo = guid;
            model.ExitDesc = guid;
            model.StockTypeName = guid;
            model.FixAssetStatusName = guid;
            model.FixAssetCode = guid;
            model.Description = guid;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.PartCode = "M.162127";
            model.RestockReason = guid;
            model.SubmitFinished = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FixAssetInventoryOutputBL.DMLFixAssetInventoryOutput(UserManager.UserInfo, model);

            var filter = new FixAssetInventoryOutputViewModel();
            filter.IdFixAssetInventory = result.Model.IdFixAssetInventory;
            filter.FixAssetCode = guid;
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            filter.PartCode = "M.162127";

            var resultGet = _FixAssetInventoryOutputBL.GetFixAssetInventoryOutput(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.Description != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void FixAssetInventoryOutputBL_ListFixAssetInventoryOutput_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FixAssetInventoryOutputViewModel();
            model.FixAssetName = guid;
            model.SerialNo = guid;
            model.ExitDesc = guid;
            model.StockTypeName = guid;
            model.FixAssetStatusName = guid;
            model.FixAssetCode = guid;
            model.Description = guid;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.PartCode = "M.162127";
            model.RestockReason = guid;
            model.SubmitFinished = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FixAssetInventoryOutputBL.DMLFixAssetInventoryOutput(UserManager.UserInfo, model);

            int count = 0;
            var filter = new FixAssetInventoryOutputListModel();

            var resultGet = _FixAssetInventoryOutputBL.ListFixAssetInventoryOutput(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void FixAssetInventoryOutputBL_DMLFixAssetInventoryOutput_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FixAssetInventoryOutputViewModel();
            model.FixAssetName = guid;
            model.SerialNo = guid;
            model.ExitDesc = guid;
            model.StockTypeName = guid;
            model.FixAssetStatusName = guid;
            model.FixAssetCode = guid;
            model.Description = guid;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.PartCode = "M.162127";
            model.RestockReason = guid;
            model.SubmitFinished = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FixAssetInventoryOutputBL.DMLFixAssetInventoryOutput(UserManager.UserInfo, model);

            var filter = new FixAssetInventoryOutputListModel();

            int count = 0;
            var resultGet = _FixAssetInventoryOutputBL.ListFixAssetInventoryOutput(UserManager.UserInfo, filter, out count);

            var modelUpdate = new FixAssetInventoryOutputViewModel();
            modelUpdate.IdFixAssetInventory = resultGet.Data.First().IdFixAssetInventory;
            modelUpdate.FixAssetName = guid;
            modelUpdate.SerialNo = guid;
            modelUpdate.ExitDesc = guid;
            modelUpdate.StockTypeName = guid;
            modelUpdate.FixAssetStatusName = guid;
            modelUpdate.FixAssetCode = guid;
            modelUpdate.Description = guid;
            modelUpdate.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            modelUpdate.PartCode = "M.162127";
            modelUpdate.RestockReason = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _FixAssetInventoryOutputBL.DMLFixAssetInventoryOutput(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void FixAssetInventoryOutputBL_DMLFixAssetInventoryOutput_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FixAssetInventoryOutputViewModel();
            model.FixAssetName = guid;
            model.SerialNo = guid;
            model.ExitDesc = guid;
            model.StockTypeName = guid;
            model.FixAssetStatusName = guid;
            model.FixAssetCode = guid;
            model.Description = guid;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.PartCode = "M.162127";
            model.RestockReason = guid;
            model.SubmitFinished = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FixAssetInventoryOutputBL.DMLFixAssetInventoryOutput(UserManager.UserInfo, model);

            var filter = new FixAssetInventoryOutputListModel();

            int count = 0;
            var resultGet = _FixAssetInventoryOutputBL.ListFixAssetInventoryOutput(UserManager.UserInfo, filter, out count);

            var modelDelete = new FixAssetInventoryOutputViewModel();
            modelDelete.IdFixAssetInventory = resultGet.Data.First().IdFixAssetInventory;
            modelDelete.FixAssetName = guid;
            modelDelete.SerialNo = guid;
            modelDelete.ExitDesc = guid;
            modelDelete.StockTypeName = guid;
            modelDelete.FixAssetStatusName = guid;
            modelDelete.FixAssetCode = guid;
            modelDelete.Description = guid;
            modelDelete.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            modelDelete.PartCode = "M.162127";
            modelDelete.RestockReason = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _FixAssetInventoryOutputBL.DMLFixAssetInventoryOutput(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}


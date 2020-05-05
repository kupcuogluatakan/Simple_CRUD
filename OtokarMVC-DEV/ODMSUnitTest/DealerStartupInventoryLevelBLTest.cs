using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.DealerStartupInventoryLevel;


namespace ODMSUnitTest
{

    [TestClass]
    public class DealerStartupInventoryLevelBLTest
    {

        DealerStartupInventoryLevelBL _DealerStartupInventoryLevelBL = new DealerStartupInventoryLevelBL();

        [TestMethod]
        public void DealerStartupInventoryLevelBL_DMLDealerStartupInventoryLevel_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerStartupInventoryLevelViewModel();
            model.DealerClassCode = guid;
            model.DealerClassName = guid;
            model.PartCode = "M.162127";
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.PartCodeName = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _DealerStartupInventoryLevelBL.DMLDealerStartupInventoryLevel(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void DealerStartupInventoryLevelBL_GetDealerStartupInventoryLevel_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerStartupInventoryLevelViewModel();
            model.DealerClassCode = guid;
            model.DealerClassName = guid;
            model.PartCode = "M.162127";
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.PartCodeName = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _DealerStartupInventoryLevelBL.DMLDealerStartupInventoryLevel(UserManager.UserInfo, model);

            var filter = new DealerStartupInventoryLevelViewModel();
            filter.DealerClassCode = guid;
            filter.PartId = 39399;
            filter.PartCode = "M.162127";
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            filter.PartCodeName = guid;

            var resultGet = _DealerStartupInventoryLevelBL.GetDealerStartupInventoryLevel(filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.DealerClassName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void DealerStartupInventoryLevelBL_ListDealerStartupInventoryLevels_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerStartupInventoryLevelViewModel();
            model.DealerClassCode = guid;
            model.DealerClassName = guid;
            model.PartCode = "M.162127";
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.PartCodeName = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _DealerStartupInventoryLevelBL.DMLDealerStartupInventoryLevel(UserManager.UserInfo, model);

            int count = 0;
            var filter = new DealerStartupInventoryLevelListModel();
            filter.DealerClassCode = guid;
            filter.PartId = 39399;
            filter.PartCode = "M.162127";
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";

            var resultGet = _DealerStartupInventoryLevelBL.ListDealerStartupInventoryLevels(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void DealerStartupInventoryLevelBL_DMLDealerStartupInventoryLevel_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerStartupInventoryLevelViewModel();
            model.DealerClassCode = guid;
            model.DealerClassName = guid;
            model.PartCode = "M.162127";
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.PartCodeName = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _DealerStartupInventoryLevelBL.DMLDealerStartupInventoryLevel(UserManager.UserInfo, model);

            var filter = new DealerStartupInventoryLevelListModel();
            filter.DealerClassCode = guid;
            filter.PartId = 39399;
            filter.PartCode = "M.162127";
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";

            int count = 0;
            var resultGet = _DealerStartupInventoryLevelBL.ListDealerStartupInventoryLevels(UserManager.UserInfo, filter, out count);

            var modelUpdate = new DealerStartupInventoryLevelViewModel();
            modelUpdate.DealerClassCode = guid;
            modelUpdate.DealerClassName = guid;
            modelUpdate.PartCode = "M.162127";
            modelUpdate.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            modelUpdate.PartCodeName = guid;

            modelUpdate.IsActiveName = guid;


            modelUpdate.CommandType = "U";
            var resultUpdate = _DealerStartupInventoryLevelBL.DMLDealerStartupInventoryLevel(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void DealerStartupInventoryLevelBL_DMLDealerStartupInventoryLevel_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerStartupInventoryLevelViewModel();
            model.DealerClassCode = guid;
            model.DealerClassName = guid;
            model.PartCode = "M.162127";
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.PartCodeName = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _DealerStartupInventoryLevelBL.DMLDealerStartupInventoryLevel(UserManager.UserInfo, model);

            var filter = new DealerStartupInventoryLevelListModel();
            filter.DealerClassCode = guid;
            filter.PartId = 39399;
            filter.PartCode = "M.162127";
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";

            int count = 0;
            var resultGet = _DealerStartupInventoryLevelBL.ListDealerStartupInventoryLevels(UserManager.UserInfo, filter, out count);

            var modelDelete = new DealerStartupInventoryLevelViewModel();
            modelDelete.DealerClassCode = guid;
            modelDelete.DealerClassName = guid;
            modelDelete.PartCode = "M.162127";
            modelDelete.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            modelDelete.PartCodeName = guid;

            modelDelete.IsActiveName = guid;


            modelDelete.CommandType = "D";
            var resultDelete = _DealerStartupInventoryLevelBL.DMLDealerStartupInventoryLevel(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}


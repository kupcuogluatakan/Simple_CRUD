using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.LabourPrice;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class LabourPriceBLTest
    {

        LabourPriceBL _LabourPriceBL = new LabourPriceBL();

        [TestMethod]
        public void LabourPriceBL_DMLLabourPrice_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourPriceViewModel();
            model.LabourPriceId = 1;
            model._LabourPriceId = guid;
            model.HasTSLabourPriceId = 1;
            model.HasNoTSLabourPriceId = 1;
            model.ModelCode = guid;
            model.VehicleGroupId = 1;
            model.ValidFromDate = DateTime.Now;
            model._ValidFromDate = guid;
            model.ValidEndDate = DateTime.Now;
            model._ValidEndDate = guid;
            model.DealerRegionId = 1;
            model.HasTsPaper = true;
            model.DealerClassName = guid;
            model.CurrencyCode = guid;
            model.LabourPriceTypeId = 1;
            model.UnitPrice = 1;
            model.IsActive = true;
            model.IsActiveString = guid;
            model.CurrencyName = guid;
            model.DealerClass = guid;
            model.HasTsPaperString = guid;
            model.LabourPriceType = guid;
            model.ModelName = guid;
            model.VehicleGroup = guid;
            model.DealerRegionName = guid;
            model.HasTSUnitPrice = 1;
            model._HasTSUnitPrice = guid;
            model.HasNoTSUnitPrice = 1;
            model._HasNoTSUnitPrice = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _LabourPriceBL.DMLLabourPrice(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void LabourPriceBL_GetLabourPrice_GetModel()
        {
            var resultGet = _LabourPriceBL.GetLabourPrice(UserManager.UserInfo, 211);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.UnitPrice > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void LabourPriceBL_ListLabourPrices_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourPriceViewModel();
            model.LabourPriceId = 1;
            model._LabourPriceId = guid;
            model.HasTSLabourPriceId = 1;
            model.HasNoTSLabourPriceId = 1;
            model.ModelCode = guid;
            model.VehicleGroupId = 1;
            model.ValidFromDate = DateTime.Now;
            model._ValidFromDate = guid;
            model.ValidEndDate = DateTime.Now;
            model._ValidEndDate = guid;
            model.DealerRegionId = 1;
            model.HasTsPaper = true;
            model.DealerClassName = guid;
            model.CurrencyCode = guid;
            model.LabourPriceTypeId = 1;
            model.UnitPrice = 1;
            model.IsActive = true;
            model.IsActiveString = guid;
            model.CurrencyName = guid;
            model.DealerClass = guid;
            model.HasTsPaperString = guid;
            model.LabourPriceType = guid;
            model.ModelName = guid;
            model.VehicleGroup = guid;
            model.DealerRegionName = guid;
            model.HasTSUnitPrice = 1;
            model._HasTSUnitPrice = guid;
            model.HasNoTSUnitPrice = 1;
            model._HasNoTSUnitPrice = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _LabourPriceBL.DMLLabourPrice(UserManager.UserInfo, model);

            int count = 0;
            var filter = new LabourPriceListModel();
            filter.ModelKod = "ATLAS";
            filter.CurrencyCode = guid;

            var resultGet = _LabourPriceBL.ListLabourPrices(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void LabourPriceBL_DMLLabourPrice_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourPriceViewModel();
            model.LabourPriceId = 1;
            model._LabourPriceId = guid;
            model.HasTSLabourPriceId = 1;
            model.HasNoTSLabourPriceId = 1;
            model.ModelCode = guid;
            model.VehicleGroupId = 1;
            model.ValidFromDate = DateTime.Now;
            model._ValidFromDate = guid;
            model.ValidEndDate = DateTime.Now;
            model._ValidEndDate = guid;
            model.DealerRegionId = 1;
            model.HasTsPaper = true;
            model.DealerClassName = guid;
            model.CurrencyCode = guid;
            model.LabourPriceTypeId = 1;
            model.UnitPrice = 1;
            model.IsActive = true;
            model.IsActiveString = guid;
            model.CurrencyName = guid;
            model.DealerClass = guid;
            model.HasTsPaperString = guid;
            model.LabourPriceType = guid;
            model.ModelName = guid;
            model.VehicleGroup = guid;
            model.DealerRegionName = guid;
            model.HasTSUnitPrice = 1;
            model._HasTSUnitPrice = guid;
            model.HasNoTSUnitPrice = 1;
            model._HasNoTSUnitPrice = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _LabourPriceBL.DMLLabourPrice(UserManager.UserInfo, model);

            var filter = new LabourPriceListModel();
            filter.ModelKod = "ATLAS";
            filter.CurrencyCode = guid;

            int count = 0;
            var resultGet = _LabourPriceBL.ListLabourPrices(UserManager.UserInfo, filter, out count);

            var modelUpdate = new LabourPriceViewModel();
            modelUpdate._LabourPriceId = guid;
            modelUpdate.ModelCode = guid;
            modelUpdate._ValidFromDate = guid;
            modelUpdate._ValidEndDate = guid;
            modelUpdate.DealerClassName = guid;
            modelUpdate.CurrencyCode = guid;
            modelUpdate.IsActiveString = guid;
            modelUpdate.CurrencyName = guid;
            modelUpdate.DealerClass = guid;
            modelUpdate.HasTsPaperString = guid;
            modelUpdate.LabourPriceType = guid;
            modelUpdate.ModelName = guid;
            modelUpdate.VehicleGroup = guid;
            modelUpdate.DealerRegionName = guid;
            modelUpdate._HasTSUnitPrice = guid;
            modelUpdate._HasNoTSUnitPrice = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _LabourPriceBL.DMLLabourPrice(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void LabourPriceBL_DMLLabourPrice_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourPriceViewModel();
            model.LabourPriceId = 1;
            model._LabourPriceId = guid;
            model.HasTSLabourPriceId = 1;
            model.HasNoTSLabourPriceId = 1;
            model.ModelCode = guid;
            model.VehicleGroupId = 1;
            model.ValidFromDate = DateTime.Now;
            model._ValidFromDate = guid;
            model.ValidEndDate = DateTime.Now;
            model._ValidEndDate = guid;
            model.DealerRegionId = 1;
            model.HasTsPaper = true;
            model.DealerClassName = guid;
            model.CurrencyCode = guid;
            model.LabourPriceTypeId = 1;
            model.UnitPrice = 1;
            model.IsActive = true;
            model.IsActiveString = guid;
            model.CurrencyName = guid;
            model.DealerClass = guid;
            model.HasTsPaperString = guid;
            model.LabourPriceType = guid;
            model.ModelName = guid;
            model.VehicleGroup = guid;
            model.DealerRegionName = guid;
            model.HasTSUnitPrice = 1;
            model._HasTSUnitPrice = guid;
            model.HasNoTSUnitPrice = 1;
            model._HasNoTSUnitPrice = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _LabourPriceBL.DMLLabourPrice(UserManager.UserInfo, model);

            var filter = new LabourPriceListModel();
            filter.ModelKod = "ATLAS";
            filter.CurrencyCode = guid;

            int count = 0;
            var resultGet = _LabourPriceBL.ListLabourPrices(UserManager.UserInfo, filter, out count);

            var modelDelete = new LabourPriceViewModel();
            modelDelete._LabourPriceId = guid;
            modelDelete.ModelCode = guid;
            modelDelete._ValidFromDate = guid;
            modelDelete._ValidEndDate = guid;
            modelDelete.DealerClassName = guid;
            modelDelete.CurrencyCode = guid;
            modelDelete.IsActiveString = guid;
            modelDelete.CurrencyName = guid;
            modelDelete.DealerClass = guid;
            modelDelete.HasTsPaperString = guid;
            modelDelete.LabourPriceType = guid;
            modelDelete.ModelName = guid;
            modelDelete.VehicleGroup = guid;
            modelDelete.DealerRegionName = guid;
            modelDelete._HasTSUnitPrice = guid;
            modelDelete._HasNoTSUnitPrice = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _LabourPriceBL.DMLLabourPrice(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }

        [TestMethod]
        public void LabourPriceBL_ListVehicleModels_GetAll()
        {
            var resultGet = _LabourPriceBL.ListVehicleModels(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void LabourPriceBL_ListLabourTypes_GetAll()
        {
            var resultGet = _LabourPriceBL.ListLabourTypes(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void LabourPriceBL_ListCurrencyCodes_GetAll()
        {
            var resultGet = _LabourPriceBL.ListCurrencyCodes(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}


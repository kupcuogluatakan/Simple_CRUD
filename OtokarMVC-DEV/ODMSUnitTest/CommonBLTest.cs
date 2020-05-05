using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.Common;
using System;
using ODMSCommon;

namespace ODMSUnitTest
{

    [TestClass]
    public class CommonBLTest
    {

        CommonBL CommonBL = new CommonBL();

        [TestMethod]
        public void CommonBL_DMLStockTransaction_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new StockTransactionViewModel();
            model.StockTransactionId = 1;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.TransactionTypeId = 1;
            model.PartId = 1;
            model.Quantity = 1;
            model.DealerPrice = 1;
            model.TransactionDesc = guid;
            model.ReserveQnty = 1;
            model.BlockQnty = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = CommonBL.DMLStockTransaction(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void CommonBL_ListGroupTypeValueInt_GetAll()
        {
            var resultGet = CommonBL.ListGroupTypeValueInt(false);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void CommonBL_ListLookup_GetAll()
        {
            var resultGet = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.DeliveryStatus);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void CommonBL_ListStatusAll_GetAll()
        {
            var resultGet = CommonBL.ListStatusAll(0);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void CommonBL_ListYesNoValueIntWithAll_GetAll()
        {
            var resultGet = CommonBL.ListYesNoValueIntWithAll(0);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void CommonBL_ListCountries_GetAll()
        {
            var resultGet = CommonBL.ListCountries(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void CommonBL_ListBodyWorks_GetAll()
        {
            var resultGet = CommonBL.ListBodyWorks(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void CommonBL_ListCities_GetAll()
        {
            var resultGet = CommonBL.ListCities(UserManager.UserInfo, 1);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void CommonBL_ListTowns_GetAll()
        {
            var resultGet = CommonBL.ListTowns(UserManager.UserInfo, 1);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void CommonBL_ListRacks_GetAll()
        {
            var resultGet = CommonBL.ListRacks(UserManager.UserInfo, 80);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void CommonBL_ListStockTypes_GetAll()
        {
            var resultGet = CommonBL.ListStockTypes(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void CommonBL_ListPo_GetAll()
        {
            var resultGet = CommonBL.ListPo(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void CommonBL_ListDealer_GetAll()
        {
            var resultGet = CommonBL.ListDealer(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void CommonBL_ListUserByDealerId_GetAll()
        {
            var resultGet = CommonBL.ListUserByDealerId(UserManager.UserInfo.GetUserDealerId(), false);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void CommonBL_ListCurrencies_GetAll()
        {
            var resultGet = CommonBL.ListCurrencies(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void CommonBL_ListAppIndcFailureCode_GetAll()
        {
            var resultGet = CommonBL.ListAppIndcFailureCode(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void CommonBL_ListSelectedStockTypes_GetAll()
        {
            var resultGet = CommonBL.ListSelectedStockTypes(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void CommonBL_ListStockTypes_GetAll_1()
        {
            var resultGet = CommonBL.ListStockTypes(UserManager.UserInfo, 39399, UserManager.UserInfo.GetUserDealerId());

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void CommonBL_ListSuppliersByDealerId_GetAll()
        {
            var resultGet = CommonBL.ListSuppliersByDealerId(UserManager.UserInfo.GetUserDealerId());

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void CommonBL_ListAllLabour_GetAll()
        {
            var resultGet = CommonBL.ListAllLabour(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void CommonBL_ListProcessType_GetAll()
        {
            var resultGet = CommonBL.ListProcessType(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void CommonBL_ListRacksByPartWareHouse_GetAll()
        {
            var resultGet = CommonBL.ListRacksByPartWareHouse(UserManager.UserInfo, 80, 22186);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void CommonBL_ListPeriodicMaintLang_GetAll()
        {
            var resultGet = CommonBL.ListPeriodicMaintLang(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}


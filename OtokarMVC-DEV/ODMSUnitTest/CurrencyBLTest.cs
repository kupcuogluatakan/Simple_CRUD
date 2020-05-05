using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.Currency;
using ODMSModel.ListModel;


namespace ODMSUnitTest
{

    [TestClass]
    public class CurrencyBLTest
    {

        CurrencyBL _CurrencyBL = new CurrencyBL();

        [TestMethod]
        public void CurrencyBL_DMLCurrency_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CurrencyIndexViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.CurrencyCode = guid;
            model.AdminName = guid;
            model.DecimalPartName = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _CurrencyBL.DMLCurrency(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void CurrencyBL_GetCurrency_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CurrencyIndexViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.CurrencyCode = guid;
            model.AdminName = guid;
            model.DecimalPartName = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _CurrencyBL.DMLCurrency(UserManager.UserInfo, model);

            var filter = new CurrencyIndexViewModel();
            filter.MultiLanguageContentAsText = "TR || TEST";
            filter.CurrencyCode = guid;

            var resultGet = _CurrencyBL.GetCurrency(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.AdminName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void CurrencyBL_ListCurrencyAsSelectList_GetAll()
        {
            var resultGet = CurrencyBL.ListCurrencyAsSelectList(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void CurrencyBL_ListCurrencys_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CurrencyIndexViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.CurrencyCode = guid;
            model.AdminName = guid;
            model.DecimalPartName = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _CurrencyBL.DMLCurrency(UserManager.UserInfo, model);

            int count = 0;
            var filter = new CurrencyListModel();
            filter.CurrencyCode = guid;

            var resultGet = _CurrencyBL.ListCurrencys(UserManager.UserInfo,filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}


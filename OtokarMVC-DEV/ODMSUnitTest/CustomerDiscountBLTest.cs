using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.CustomerDiscount;


namespace ODMSUnitTest
{

    [TestClass]
    public class CustomerDiscountBLTest
    {

        CustomerDiscountBL _CustomerDiscountBL = new CustomerDiscountBL();

        [TestMethod]
        public void CustomerDiscountBL_DMLCustomerDiscount_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CustomerDiscountIndexViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.CustomerName = guid;
            model.DealerName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _CustomerDiscountBL.DMLCustomerDiscount(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void CustomerDiscountBL_GetCustomerDiscount_GetModel()
        {
            var filter = new CustomerDiscountIndexViewModel();
            filter.IdDealer = UserManager.UserInfo.GetUserDealerId();
            filter.MultiLanguageContentAsText = "TR || TEST";

            var resultGet = _CustomerDiscountBL.GetCustomerDiscount(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.IdCustomer > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void CustomerDiscountBL_ListCustomerDiscount_GetAll()
        {
            int count = 0;
            var filter = new CustomerDiscountListModel();
            filter.IdDealer = UserManager.UserInfo.GetUserDealerId();

            var resultGet = _CustomerDiscountBL.ListCustomerDiscount(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}


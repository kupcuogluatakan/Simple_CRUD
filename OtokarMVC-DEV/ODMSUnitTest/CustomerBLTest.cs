using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.Customer;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class CustomerBLTest
    {

        CustomerBL _CustomerBL = new CustomerBL();

        [TestMethod]
        public void CustomerBL_DMLCustomer_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CustomerIndexViewModel();
            model.CustomerId = 1;
            model.TcIdentityNo = guid;
            model.SAPCustomerSSID = guid;
            model.BOSCustomerSSID = guid;
            model.CustomerName = guid;
            model.CustomerLastName = guid;
            model.CustomerTypeId = 2;
            model.CustomerTypeName = guid;
            model.IsDealerCustomer = true;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.GovernmentTypeName = guid;
            model.CompanyTypeId = 1;
            model.CompanyTypeName = guid;
            model.CountryId = 1;
            model.CountryName = guid;
            model.TaxOffice = guid;
            model.TaxNo = guid;
            model.PassportNo = guid;
            model.MobileNo = guid;
            model.WitholdingStatusName = guid;
            model.WitholdingId = guid;
            model.WitholdingName = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.IsElectronicInvoiceEnabled = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _CustomerBL.DMLCustomer(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void CustomerBL_GetCustomer_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CustomerIndexViewModel();
            model.CustomerId = 1;
            model.TcIdentityNo = guid;
            model.SAPCustomerSSID = guid;
            model.BOSCustomerSSID = guid;
            model.CustomerName = guid;
            model.CustomerLastName = guid;
            model.CustomerTypeId = 2;
            model.CustomerTypeName = guid;
            model.IsDealerCustomer = true;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.GovernmentTypeName = guid;
            model.CompanyTypeId = 1;
            model.CompanyTypeName = guid;
            model.CountryId = 1;
            model.TaxOffice = guid;
            model.TaxNo = guid;
            model.PassportNo = guid;
            model.MobileNo = guid;
            model.WitholdingStatusName = guid;
            model.WitholdingId = guid;
            model.WitholdingName = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.IsElectronicInvoiceEnabled = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _CustomerBL.DMLCustomer(UserManager.UserInfo, model);

            var filter = new CustomerIndexViewModel();
            filter.CustomerId = result.Model.CustomerId;
            filter.CustomerTypeId = 2;
            filter.DealerId = UserManager.UserInfo.DealerID;
            filter.CompanyTypeId = 1;
            filter.CountryId = 1;

            var resultGet = _CustomerBL.GetCustomer(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.CountryName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void CustomerBL_GetCustomersByDealer_GetModel()
        {
            var filter = new CustomerListModel();
            filter.DealerId = UserManager.UserInfo.DealerID;

            var resultGet = _CustomerBL.GetCustomersByDealer(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.CustomerId > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void CustomerBL_ListCustomerAsSelectListItem_GetAll()
        {
            var resultGet = CustomerBL.ListCustomerAsSelectListItem(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void CustomerBL_ListCustomers_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CustomerIndexViewModel();
            model.CustomerId = 1;
            model.TcIdentityNo = guid;
            model.SAPCustomerSSID = guid;
            model.BOSCustomerSSID = guid;
            model.CustomerName = guid;
            model.CustomerLastName = guid;
            model.CustomerTypeId = 2;
            model.CustomerTypeName = guid;
            model.IsDealerCustomer = true;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.GovernmentTypeName = guid;
            model.CompanyTypeId = 1;
            model.CompanyTypeName = guid;
            model.CountryId = 1;
            model.CountryName = guid;
            model.TaxOffice = guid;
            model.TaxNo = guid;
            model.PassportNo = guid;
            model.MobileNo = guid;
            model.WitholdingStatusName = guid;
            model.WitholdingId = guid;
            model.WitholdingName = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.IsElectronicInvoiceEnabled = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _CustomerBL.DMLCustomer(UserManager.UserInfo, model);

            int count = 0;
            var filter = new CustomerListModel();
            filter.CustomerName = guid;
            filter.DealerId = UserManager.UserInfo.DealerID;

            var resultGet = _CustomerBL.ListCustomers(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void CustomerBL_GetWitholdingList_GetAll()
        {
            var resultGet = _CustomerBL.GetWitholdingList(1);

            Assert.IsTrue(resultGet.Total > 0);
        }



    }

}


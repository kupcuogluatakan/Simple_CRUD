using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.CustomerContact;
using System;
using ODMSModel.Customer;

namespace ODMSUnitTest
{

    [TestClass]
    public class CustomerContactBLTest
    {

        CustomerContactBL _CustomerContactBL = new CustomerContactBL();
        CustomerBL _CustomerBL = new CustomerBL();

        [TestMethod]
        public void CustomerContactBL_DMLCustomerContact_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CustomerContactIndexViewModel();
            model.ContactId = 1;
            model.CustomerId = 1;
            model.CustomerName = guid;
            model.ContactTypeName = guid;
            model.Name = guid;
            model.Surname = guid;
            model.ContactTypeValue = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _CustomerContactBL.DMLCustomerContact(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void CustomerContactBL_GetCustomerContact_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CustomerContactIndexViewModel();
            model.ContactId = 1;
            model.CustomerId = 1;
            model.ContactTypeName = guid;
            model.Name = guid;
            model.Surname = guid;
            model.ContactTypeValue = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _CustomerContactBL.DMLCustomerContact(UserManager.UserInfo, model);

            var filter = new CustomerContactIndexViewModel();
            filter.ContactId = result.Model.ContactId;

            var resultGet = _CustomerContactBL.GetCustomerContact(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.CustomerName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void CustomerContactBL_ListCustomerContactes_GetAll()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);

            var model1 = new CustomerIndexViewModel();
            model1.TcIdentityNo = guid;
            model1.SAPCustomerSSID = guid;
            model1.BOSCustomerSSID = guid;
            model1.CustomerName = guid;
            model1.CustomerLastName = guid;
            model1.CustomerTypeId = 2;
            model1.CustomerTypeName = guid;
            model1.IsDealerCustomer = true;
            model1.DealerId = UserManager.UserInfo.DealerID;
            model1.DealerName = guid;
            model1.GovernmentTypeName = guid;
            model1.CompanyTypeId = 1;
            model1.CompanyTypeName = guid;
            model1.CountryId = 1;
            model1.CountryName = guid;
            model1.TaxOffice = guid;
            model1.TaxNo = guid;
            model1.PassportNo = guid;
            model1.MobileNo = guid;
            model1.WitholdingStatusName = guid;
            model1.WitholdingId = guid;
            model1.WitholdingName = guid;
            model1.IsActive = true;
            model1.IsActiveName = guid;
            model1.IsElectronicInvoiceEnabled = true;
            model1.UpdateUser = 1;
            model1.UpdateDate = DateTime.Now;
            model1.CommandType = "I";
            var resultCustomer = _CustomerBL.DMLCustomer(UserManager.UserInfo, model1);


            var model = new CustomerContactIndexViewModel();
            model.ContactId = resultCustomer.Model.CustomerId;
            model.CustomerId = 1;
            model.ContactTypeName = guid;
            model.Name = guid;
            model.Surname = guid;
            model.ContactTypeValue = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _CustomerContactBL.DMLCustomerContact(UserManager.UserInfo, model);

            int count = 0;
            var filter = new CustomerContactListModel();
            filter.CustomerId = result.Model.CustomerId;

            var resultGet = _CustomerContactBL.ListCustomerContactes(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

    }

}


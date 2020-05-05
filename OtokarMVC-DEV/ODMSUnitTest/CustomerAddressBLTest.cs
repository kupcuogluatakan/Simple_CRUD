using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.CustomerAddress;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class CustomerAddressBLTest
    {

        CustomerAddressBL _CustomerAddressBL = new CustomerAddressBL();

        [TestMethod]
        public void CustomerAddressBL_DMLCustomerAddress_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CustomerAddressIndexViewModel();
            model.AddressId = 1;
            model.CustomerId = 1;
            model.CustomerName = guid;
            model.AddressTypeName = guid;
            model.CountryId = 1;
            model.CountryName = guid;
            model.CityId = 1;
            model.CityName = guid;
            model.TownId = 1;
            model.TownName = guid;
            model.ZipCode = guid;
            model.Address1 = guid;
            model.Address2 = guid;
            model.Address3 = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _CustomerAddressBL.DMLCustomerAddress(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void CustomerAddressBL_GetCustomerAddress_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CustomerAddressIndexViewModel();
            model.AddressId = 1;
            model.CustomerId = 1;
            model.CustomerName = guid;
            model.AddressTypeName = guid;
            model.CountryId = 1;
            model.CityId = 1;
            model.CityName = guid;
            model.TownId = 1;
            model.TownName = guid;
            model.ZipCode = guid;
            model.Address1 = guid;
            model.Address2 = guid;
            model.Address3 = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _CustomerAddressBL.DMLCustomerAddress(UserManager.UserInfo, model);

            var filter = new CustomerAddressIndexViewModel();
            filter.AddressId = result.Model.AddressId;
            filter.CountryId = 1;
            filter.CityId = 1;
            filter.TownId = 1;
            filter.ZipCode = guid;

            var resultGet = _CustomerAddressBL.GetCustomerAddress(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.CountryName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void CustomerAddressBL_ListCustomerAddresses_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CustomerAddressIndexViewModel();
            model.AddressId = 1;
            model.CustomerId = 1;
            model.CustomerName = guid;
            model.AddressTypeName = guid;
            model.CountryId = 1;
            model.CountryName = guid;
            model.CityId = 1;
            model.CityName = guid;
            model.TownId = 1;
            model.TownName = guid;
            model.ZipCode = guid;
            model.Address1 = guid;
            model.Address2 = guid;
            model.Address3 = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _CustomerAddressBL.DMLCustomerAddress(UserManager.UserInfo, model);

            int count = 0;
            var filter = new CustomerAddressListModel();
            filter.ZipCode = guid;

            var resultGet = _CustomerAddressBL.ListCustomerAddresses(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

      

    }

}


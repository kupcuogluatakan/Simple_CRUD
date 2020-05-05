using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.Dealer;
using ODMSModel.DealerVehicleGroup;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class DealerBLTest
    {

        DealerBL _DealerBL = new DealerBL();

        [TestMethod]
        public void DealerBL_DMLDealer_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerViewModel();
            model.DealerId = UserManager.UserInfo.DealerID;
            model.SSID = guid;
            model.BranchSSID = guid;
            model.ShortName = guid;
            model.Name = guid;
            model.DealerRegionId = 1;
            model.Country = 1;
            model.ForeignCity = guid;
            model.AcceptOrderProposal = true;
            model.Town = guid;
            model.ForeignTown = guid;
            model.Address1 = guid;
            model.Address2 = guid;
            model.TaxOffice = guid;
            model.TaxNo = guid;
            model.Phone = guid;
            model.MobilePhone = guid;
            model.Fax = guid;
            model.ContactNameSurname = guid;
            model.DealerClassName = guid;
            model.DealerClassCode = guid;
            model.HasTs12047Certificate = true;
            model.ContactEmail = guid;
            model.HasServiceResponsibilityInsurance = true;
            model.CustomerGroupLookKey = 1;
            model.CustomerGroupLookVal = guid;
            model.SaleChannelCode = guid;
            model.SaleChannelName = guid;
            model.IsActive = true;
            model.ClaimRatio = 1;
            model.AutoMrp = true;
            model.LastMrpDate = guid;
            model.RegionName = guid;
            model.CountryName = guid;
            model.CityName = guid;
            model.TownName = guid;
            model.DealerClass = guid;
            model.CustomerGroup = guid;
            model.CurrencyCode = guid;
            model.CurrencyName = guid;
            model.PurchaseOrderGroupId = guid;
            model.PurchaseOrderGroupName = guid;
            model.IsElectronicInvoiceEnabled = true;
            model.Latitude = guid;
            model.Longitude = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _DealerBL.DMLDealer(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void DealerBL_DMLDealerVehicleGroups_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerVehicleGroupViewModel();
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.VehicleModelCode = guid;
            model.VehicleGroupName = guid;
            model.VehicleModelName = guid;
            model.IsActive = true;
            model.IsActiveString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _DealerBL.DMLDealerVehicleGroups(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void DealerBL_GetDealer_GetModel()
        {
            var resultGet = _DealerBL.GetDealer(UserManager.UserInfo, UserManager.UserInfo.GetUserDealerId());

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.Name != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void DealerBL_GetDealerBySSID_GetModel()
        {
            var resultGet = _DealerBL.GetDealerBySSID(UserManager.UserInfo, "0000200546");

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.Name != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void DealerBL_GetDealerVehicleGroup_GetModel()
        {
            var resultGet = _DealerBL.GetDealerVehicleGroup(UserManager.UserInfo, UserManager.UserInfo.GetUserDealerId(), 4);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.VehicleGroupName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void DealerBL_GetCountryCurrencyCode_GetModel()
        {
            var resultGet = _DealerBL.GetCountryCurrencyCode(1);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void DealerBL_GetDealerBranchSSID_GetModel()
        {
            var resultGet = DealerBL.GetDealerBranchSSID(UserManager.UserInfo.GetUserDealerId());

            Assert.IsTrue(resultGet.Model != null && resultGet.Model != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void DealerBL_GetDealerCountryId_GetModel()
        {
            var resultGet = DealerBL.GetDealerCountryId(UserManager.UserInfo.GetUserDealerId());

            Assert.IsTrue(resultGet.Model > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void DealerBL_GetDealerCustomerInfo_GetModel()
        {
            var resultGet = _DealerBL.GetDealerCustomerInfo(UserManager.UserInfo.GetUserDealerId());

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.CustomerId > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void DealerBL_ListDealersGrid_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerVehicleGroupViewModel();
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.VehicleModelCode = guid;
            model.VehicleGroupName = guid;
            model.VehicleModelName = guid;
            model.IsActive = true;
            model.IsActiveString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _DealerBL.DMLDealerVehicleGroups(UserManager.UserInfo, model);

            int count = 0;
            var filter = new DealerListModel();
            filter.DealerId = UserManager.UserInfo.DealerID;

            var resultGet = _DealerBL.ListDealersGrid(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void DealerBL_DMLDealerVehicleGroups_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerVehicleGroupViewModel();
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.VehicleModelCode = guid;
            model.VehicleGroupName = guid;
            model.VehicleModelName = guid;
            model.IsActive = true;
            model.IsActiveString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _DealerBL.DMLDealerVehicleGroups(UserManager.UserInfo, model);

            var filter = new DealerListModel();
            filter.DealerId = UserManager.UserInfo.DealerID;

            int count = 0;
            var resultGet = _DealerBL.ListDealersGrid(UserManager.UserInfo, filter, out count);

            var modelUpdate = new DealerVehicleGroupViewModel();
            modelUpdate.DealerId = UserManager.UserInfo.DealerID;
            modelUpdate.DealerName = guid;
            modelUpdate.VehicleModelCode = guid;
            modelUpdate.VehicleGroupName = guid;
            modelUpdate.VehicleModelName = guid;

            modelUpdate.IsActiveString = guid;


            modelUpdate.CommandType = "U";
            var resultUpdate = _DealerBL.DMLDealerVehicleGroups(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void DealerBL_DMLDealerVehicleGroups_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerVehicleGroupViewModel();
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.VehicleModelCode = guid;
            model.VehicleGroupName = guid;
            model.VehicleModelName = guid;
            model.IsActive = true;
            model.IsActiveString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _DealerBL.DMLDealerVehicleGroups(UserManager.UserInfo, model);

            var filter = new DealerListModel();
            filter.DealerId = UserManager.UserInfo.DealerID;
            filter.CityId = 1;
            filter.CountryId = 1;
            filter.TownId = 1;

            int count = 0;
            var resultGet = _DealerBL.ListDealersGrid(UserManager.UserInfo, filter, out count);

            var modelDelete = new DealerVehicleGroupViewModel();
            modelDelete.DealerId = UserManager.UserInfo.DealerID;
            modelDelete.DealerName = guid;
            modelDelete.VehicleModelCode = guid;
            modelDelete.VehicleGroupName = guid;
            modelDelete.VehicleModelName = guid;

            modelDelete.IsActiveString = guid;


            modelDelete.CommandType = "D";
            var resultDelete = _DealerBL.DMLDealerVehicleGroups(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }

        [TestMethod]
        public void DealerBL_GetDealerList_GetAll()
        {
            var resultGet = _DealerBL.GetDealerList(UserManager.UserInfo.LanguageCode);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void DealerBL_ListDealers_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerVehicleGroupViewModel();
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.VehicleModelCode = guid;
            model.VehicleGroupName = guid;
            model.VehicleModelName = guid;
            model.IsActive = true;
            model.IsActiveString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _DealerBL.DMLDealerVehicleGroups(UserManager.UserInfo, model);

            int count = 0;
            var filter = new DealerListModel();
            filter.DealerId = UserManager.UserInfo.DealerID;

            var resultGet = _DealerBL.ListDealers(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void DealerBL_ListCountries_GetAll()
        {
            var resultGet = _DealerBL.ListCountries(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }


        [TestMethod]
        public void DealerBL_ListCities_GetAll()
        {
            var resultGet = _DealerBL.ListCities(UserManager.UserInfo, 1);

            Assert.IsTrue(resultGet.Total > 0);
        }


        [TestMethod]
        public void DealerBL_ListCurrencies_GetAll()
        {
            var resultGet = _DealerBL.ListCurrencies(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void DealerBL_ListVehicleGroupsAsSelectListItem_GetAll()
        {
            var resultGet = _DealerBL.ListVehicleGroupsAsSelectListItem(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void DealerBL_ListDealerVehicleGroups_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerVehicleGroupViewModel();
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.VehicleModelCode = guid;
            model.VehicleGroupName = guid;
            model.VehicleModelName = guid;
            model.IsActive = true;
            model.IsActiveString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _DealerBL.DMLDealerVehicleGroups(UserManager.UserInfo, model);

            int count = 0;
            var filter = new DealerVehicleGroupsListModel();
            filter.DealerId = UserManager.UserInfo.DealerID;
            filter.VehicleModelCode = guid;

            var resultGet = _DealerBL.ListDealerVehicleGroups(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }


        [TestMethod]
        public void DealerBL_GetCountryDefaultPriceList_GetAll()
        {
            var resultGet = _DealerBL.GetCountryDefaultPriceList(1);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void DealerBL_ListTowns_GetAll()
        {
            var resultGet = _DealerBL.ListTowns(UserManager.UserInfo, 1);

            Assert.IsTrue(resultGet.Total > 0);
        }

    }

}


using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.Supplier;
using System;
using ODMSCommon.Security;


namespace ODMSUnitTest
{

	[TestClass]
	public class SupplierBLTest
	{

		SupplierBL _SupplierBL = new SupplierBL();

		[TestMethod]
		public void SupplierBL_DMLSupplier_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SupplierViewModel();
			model.SupplierId = 782; 
			model.Ssid= guid; 
			model.SupplierName= guid; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.TaxOffice= guid; 
			model.TaxNo= guid; 
			model.Url= guid; 
			model.ContactPerson= guid; 
			model.Email= guid; 
			model.Phone= guid; 
			model.MobilePhone= guid; 
			model.Fax= guid; 
			model.ChamberOfCommerce= guid; 
			model.TradeRegistryNo= guid; 
			model.CountryId = 1; 
			model.CountryName= guid; 
			model.CityId = 1; 
			model.CityName= guid; 
			model.TownId = 1; 
			model.TownName= guid; 
			model.ZipCode= guid; 
			model.Address= guid; 
			model.IsActive= true; 
			model.IsActiveString= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _SupplierBL.DMLSupplier(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void SupplierBL_GetSupplier_GetModel()
		{
			 var resultGet = _SupplierBL.GetSupplier(UserManager.UserInfo,519);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.SupplierId > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void SupplierBL_ListSuppliers_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SupplierViewModel();
			model.SupplierId = 782; 
			model.Ssid= guid; 
			model.SupplierName= guid; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.TaxOffice= guid; 
			model.TaxNo= guid; 
			model.Url= guid; 
			model.ContactPerson= guid; 
			model.Email= guid; 
			model.Phone= guid; 
			model.MobilePhone= guid; 
			model.Fax= guid; 
			model.ChamberOfCommerce= guid; 
			model.TradeRegistryNo= guid; 
			model.CountryId = 1; 
			model.CountryName= guid; 
			model.CityId = 1; 
			model.CityName= guid; 
			model.TownId = 1; 
			model.TownName= guid; 
			model.ZipCode= guid; 
			model.Address= guid; 
			model.IsActive= true; 
			model.IsActiveString= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _SupplierBL.DMLSupplier(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new SupplierListModel();
			filter.SupplierId = 782; 
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.ZipCode = guid; 
			
			 var resultGet = _SupplierBL.ListSuppliers(UserManager.UserInfo,filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void SupplierBL_DMLSupplier_Update()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SupplierViewModel();
			model.SupplierId = 782; 
			model.Ssid= guid; 
			model.SupplierName= guid; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.TaxOffice= guid; 
			model.TaxNo= guid; 
			model.Url= guid; 
			model.ContactPerson= guid; 
			model.Email= guid; 
			model.Phone= guid; 
			model.MobilePhone= guid; 
			model.Fax= guid; 
			model.ChamberOfCommerce= guid; 
			model.TradeRegistryNo= guid; 
			model.CountryId = 1; 
			model.CountryName= guid; 
			model.CityId = 1; 
			model.CityName= guid; 
			model.TownId = 1; 
			model.TownName= guid; 
			model.ZipCode= guid; 
			model.Address= guid; 
			model.IsActive= true; 
			model.IsActiveString= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _SupplierBL.DMLSupplier(UserManager.UserInfo, model);
			
			var filter = new SupplierListModel();
			filter.SupplierId = 782; 
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.ZipCode = guid; 
			
			int count = 0;
			 var resultGet = _SupplierBL.ListSuppliers(UserManager.UserInfo, filter, out count);
			
			var modelUpdate = new SupplierViewModel();
			modelUpdate.SupplierId = 782; 
			modelUpdate.Ssid= guid; 
			modelUpdate.SupplierName= guid; 
			modelUpdate.DealerId = UserManager.UserInfo.DealerID; 
			modelUpdate.DealerName= guid; 
			modelUpdate.TaxOffice= guid; 
			modelUpdate.TaxNo= guid; 
			modelUpdate.Url= guid; 
			modelUpdate.ContactPerson= guid; 
			modelUpdate.Email= guid; 
			modelUpdate.Phone= guid; 
			modelUpdate.MobilePhone= guid; 
			modelUpdate.Fax= guid; 
			modelUpdate.ChamberOfCommerce= guid; 
			modelUpdate.TradeRegistryNo= guid; 
			modelUpdate.CountryId = 1; 
			modelUpdate.CountryName= guid; 
			modelUpdate.CityId = 1; 
			modelUpdate.CityName= guid; 
			modelUpdate.TownId = 1; 
			modelUpdate.TownName= guid; 
			modelUpdate.ZipCode= guid; 
			modelUpdate.Address= guid; 
			
			modelUpdate.IsActiveString= guid; 
			
			
			modelUpdate.CommandType = "U";
			 var resultUpdate = _SupplierBL.DMLSupplier(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void SupplierBL_ListCities_GetAll()
		{
			 var resultGet = _SupplierBL.ListCities(UserManager.UserInfo, 1);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
        
		[TestMethod]
		public void SupplierBL_ListTowns_GetAll()
		{
			 var resultGet = _SupplierBL.ListTowns(UserManager.UserInfo, 1);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
        
		[TestMethod]
		public void SupplierBL_ListSupplierComboAsSelectListItem_GetAll()
		{
			 var resultGet = SupplierBL.ListSupplierComboAsSelectListItem(UserManager.UserInfo,true);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
        
		[TestMethod]
		public void SupplierBL_ListSupplierComboAsSelectListItemPO_GetAll()
		{
			 var resultGet = SupplierBL.ListSupplierComboAsSelectListItemPO(UserManager.UserInfo,true);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
        
		[TestMethod]
		public void SupplierBL_ListSupplierComboAsSelectListItemPONotInDealer_GetAll()
		{
			 var resultGet = SupplierBL.ListSupplierComboAsSelectListItemPONotInDealer(UserManager.UserInfo, true);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void SupplierBL_DMLSupplier_Delete()
		{
		    var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var modelDelete = new SupplierViewModel();
			modelDelete.SupplierId = 782; 
			modelDelete.Ssid= guid; 
			modelDelete.SupplierName= guid; 
			modelDelete.DealerId = UserManager.UserInfo.DealerID; 
			modelDelete.DealerName= guid; 
			modelDelete.TaxOffice= guid; 
			modelDelete.TaxNo= guid; 
			modelDelete.Url= guid; 
			modelDelete.ContactPerson= guid; 
			modelDelete.Email= guid; 
			modelDelete.Phone= guid; 
			modelDelete.MobilePhone= guid; 
			modelDelete.Fax= guid; 
			modelDelete.ChamberOfCommerce= guid; 
			modelDelete.TradeRegistryNo= guid; 
			modelDelete.CountryId = 1; 
			modelDelete.CountryName= guid; 
			modelDelete.CityId = 1; 
			modelDelete.CityName= guid; 
			modelDelete.TownId = 1; 
			modelDelete.TownName= guid; 
			modelDelete.ZipCode= guid; 
			modelDelete.Address= guid; 
			
			modelDelete.IsActiveString= guid; 
			
			
			modelDelete.CommandType = "D";
			 var resultDelete = _SupplierBL.DMLSupplier(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}


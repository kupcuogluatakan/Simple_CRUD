using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.SparePartSaleWaybill;
using System;
using ODMSBusiness.Business;


namespace ODMSUnitTest
{

	[TestClass]
	public class SparePartSaleWaybillBLTest
	{

		SparePartSaleWaybillBL _SparePartSaleWaybillBL = new SparePartSaleWaybillBL();

		[TestMethod]
		public void SparePartSaleWaybillBL_DMLSparePartSaleWaybill_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartSaleWaybillViewModel();
			model.SparePartSaleId= 1; 
			model.SparePartSaleWaybillId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.CustomerId= 1; 
			model.CustomerName= guid; 
			model.CustomerLastName= guid; 
			model.CustomerAddressCountryText= guid; 
			model.CustomerAddressCityText= guid; 
			model.CustomerAddressTownText= guid; 
			model.CustomerAddressZipCode= guid; 
			model.CustomerAddress1= guid; 
			model.CustomerAddress2= guid; 
			model.CustomerAddress3= guid; 
			model.CustomerTaxOffice= guid; 
			model.CustomeTaxNo= guid; 
			model.CustomerTCIdentity= guid; 
			model.CustomerPassportNo= guid; 
			model.SparePartSaleIdList= guid; 
			model.IsActive= true; 
			model.IsActiveString= guid; 
			model.IsWaybilled= true; 
			model.WaybillSerialNo= guid; 
			model.WaybillNo= guid; 
			model.WaybillReferenceNo= guid; 
			model.InvoiceSerialNo= guid; 
			model.InvoiceNo= guid; 
			model.BankRequired= true; 
			model.InstalmentRequired= true; 
			model.DefermentRequired= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _SparePartSaleWaybillBL.DMLSparePartSaleWaybill(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void SparePartSaleWaybillBL_GetSparePartSaleWaybill_GetModel()
		{
			 var resultGet = _SparePartSaleWaybillBL.GetSparePartSaleWaybill(UserManager.UserInfo, 63264);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.SparePartSaleWaybillId > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void SparePartSaleWaybillBL_ListSparePartSaleWaybills_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartSaleWaybillViewModel();
			model.SparePartSaleId= 1; 
			model.SparePartSaleWaybillId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.CustomerId= 1; 
			model.CustomerName= guid; 
			model.CustomerLastName= guid; 
			model.CustomerAddressCountryText= guid; 
			model.CustomerAddressCityText= guid; 
			model.CustomerAddressTownText= guid; 
			model.CustomerAddressZipCode= guid; 
			model.CustomerAddress1= guid; 
			model.CustomerAddress2= guid; 
			model.CustomerAddress3= guid; 
			model.CustomerTaxOffice= guid; 
			model.CustomeTaxNo= guid; 
			model.CustomerTCIdentity= guid; 
			model.CustomerPassportNo= guid; 
			model.SparePartSaleIdList= guid; 
			model.IsActive= true; 
			model.IsActiveString= guid; 
			model.IsWaybilled= true; 
			model.WaybillSerialNo= guid; 
			model.WaybillNo= guid; 
			model.WaybillReferenceNo= guid; 
			model.InvoiceSerialNo= guid; 
			model.InvoiceNo= guid; 
			model.BankRequired= true; 
			model.InstalmentRequired= true; 
			model.DefermentRequired= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _SparePartSaleWaybillBL.DMLSparePartSaleWaybill(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new SparePartSaleWaybillListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.CustomerAddressZipCode = guid; 
			
			 var resultGet = _SparePartSaleWaybillBL.ListSparePartSaleWaybills(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void SparePartSaleWaybillBL_DMLSparePartSaleWaybill_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartSaleWaybillViewModel();
			model.SparePartSaleId= 1; 
			model.SparePartSaleWaybillId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.CustomerId= 1; 
			model.CustomerName= guid; 
			model.CustomerLastName= guid; 
			model.CustomerAddressCountryText= guid; 
			model.CustomerAddressCityText= guid; 
			model.CustomerAddressTownText= guid; 
			model.CustomerAddressZipCode= guid; 
			model.CustomerAddress1= guid; 
			model.CustomerAddress2= guid; 
			model.CustomerAddress3= guid; 
			model.CustomerTaxOffice= guid; 
			model.CustomeTaxNo= guid; 
			model.CustomerTCIdentity= guid; 
			model.CustomerPassportNo= guid; 
			model.SparePartSaleIdList= guid; 
			model.IsActive= true; 
			model.IsActiveString= guid; 
			model.IsWaybilled= true; 
			model.WaybillSerialNo= guid; 
			model.WaybillNo= guid; 
			model.WaybillReferenceNo= guid; 
			model.InvoiceSerialNo= guid; 
			model.InvoiceNo= guid; 
			model.BankRequired= true; 
			model.InstalmentRequired= true; 
			model.DefermentRequired= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _SparePartSaleWaybillBL.DMLSparePartSaleWaybill(UserManager.UserInfo, model);
			
			var filter = new SparePartSaleWaybillListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.CustomerAddressZipCode = guid; 
			
			int count = 0;
			 var resultGet = _SparePartSaleWaybillBL.ListSparePartSaleWaybills(UserManager.UserInfo, filter, out count);
			
			var modelUpdate = new SparePartSaleWaybillViewModel();
			modelUpdate.SparePartSaleWaybillId = resultGet.Data.First().SparePartSaleWaybillId;
			modelUpdate.DealerId = UserManager.UserInfo.DealerID; 
			modelUpdate.CustomerName= guid; 
			modelUpdate.CustomerLastName= guid; 
			modelUpdate.CustomerAddressCountryText= guid; 
			modelUpdate.CustomerAddressCityText= guid; 
			modelUpdate.CustomerAddressTownText= guid; 
			modelUpdate.CustomerAddressZipCode= guid; 
			modelUpdate.CustomerAddress1= guid; 
			modelUpdate.CustomerAddress2= guid; 
			modelUpdate.CustomerAddress3= guid; 
			modelUpdate.CustomerTaxOffice= guid; 
			modelUpdate.CustomeTaxNo= guid; 
			modelUpdate.CustomerTCIdentity= guid; 
			modelUpdate.CustomerPassportNo= guid; 
			modelUpdate.SparePartSaleIdList= guid; 
			modelUpdate.IsActiveString= guid; 
			modelUpdate.WaybillSerialNo= guid; 
			modelUpdate.WaybillNo= guid; 
			modelUpdate.WaybillReferenceNo= guid; 
			modelUpdate.InvoiceSerialNo= guid; 
			modelUpdate.InvoiceNo= guid; 
			modelUpdate.CommandType = "U";
			 var resultUpdate = _SparePartSaleWaybillBL.DMLSparePartSaleWaybill(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void SparePartSaleWaybillBL_DMLSparePartSaleWaybill_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartSaleWaybillViewModel();
			model.SparePartSaleId= 1; 
			model.SparePartSaleWaybillId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.CustomerId= 1; 
			model.CustomerName= guid; 
			model.CustomerLastName= guid; 
			model.CustomerAddressCountryText= guid; 
			model.CustomerAddressCityText= guid; 
			model.CustomerAddressTownText= guid; 
			model.CustomerAddressZipCode= guid; 
			model.CustomerAddress1= guid; 
			model.CustomerAddress2= guid; 
			model.CustomerAddress3= guid; 
			model.CustomerTaxOffice= guid; 
			model.CustomeTaxNo= guid; 
			model.CustomerTCIdentity= guid; 
			model.CustomerPassportNo= guid; 
			model.SparePartSaleIdList= guid; 
			model.IsActive= true; 
			model.IsActiveString= guid; 
			model.IsWaybilled= true; 
			model.WaybillSerialNo= guid; 
			model.WaybillNo= guid; 
			model.WaybillReferenceNo= guid; 
			model.InvoiceSerialNo= guid; 
			model.InvoiceNo= guid; 
			model.BankRequired= true; 
			model.InstalmentRequired= true; 
			model.DefermentRequired= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _SparePartSaleWaybillBL.DMLSparePartSaleWaybill(UserManager.UserInfo, model);
			
			var filter = new SparePartSaleWaybillListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.CustomerAddressZipCode = guid; 
			
			int count = 0;
			 var resultGet = _SparePartSaleWaybillBL.ListSparePartSaleWaybills(UserManager.UserInfo, filter, out count);
			
			var modelDelete = new SparePartSaleWaybillViewModel();
			modelDelete.SparePartSaleWaybillId = resultGet.Data.First().SparePartSaleWaybillId;
			modelDelete.DealerId = UserManager.UserInfo.DealerID; 
			modelDelete.CustomerName= guid; 
			modelDelete.CustomerLastName= guid; 
			modelDelete.CustomerAddressCountryText= guid; 
			modelDelete.CustomerAddressCityText= guid; 
			modelDelete.CustomerAddressTownText= guid; 
			modelDelete.CustomerAddressZipCode= guid; 
			modelDelete.CustomerAddress1= guid; 
			modelDelete.CustomerAddress2= guid; 
			modelDelete.CustomerAddress3= guid; 
			modelDelete.CustomerTaxOffice= guid; 
			modelDelete.CustomeTaxNo= guid; 
			modelDelete.CustomerTCIdentity= guid; 
			modelDelete.CustomerPassportNo= guid; 
			modelDelete.SparePartSaleIdList= guid; 
			modelDelete.IsActiveString= guid; 
			modelDelete.WaybillSerialNo= guid; 
			modelDelete.WaybillNo= guid; 
			modelDelete.WaybillReferenceNo= guid; 
			modelDelete.InvoiceSerialNo= guid; 
			modelDelete.InvoiceNo= guid; 
			modelDelete.CommandType = "D";
			 var resultDelete = _SparePartSaleWaybillBL.DMLSparePartSaleWaybill(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}


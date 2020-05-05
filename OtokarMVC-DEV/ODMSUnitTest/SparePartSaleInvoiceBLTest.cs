using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.SparePartSaleInvoice;
using System;
using ODMSBusiness.Business;


namespace ODMSUnitTest
{

	[TestClass]
	public class SparePartSaleInvoiceBLTest
	{

		SparePartSaleInvoiceBL _SparePartSaleInvoiceBL = new SparePartSaleInvoiceBL();

		[TestMethod]
		public void SparePartSaleInvoiceBL_DMLSparePartSaleInvoice_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartSaleInvoiceViewModel();
			model.SparePartSaleId= 1; 
			model.SparePartSaleWaybillId= 1; 
			model.SparePartSaleInvoiceId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.CustomerId= 1; 
			model.InvoiceSerialNo= guid; 
			model.InvoiceNo= guid; 
			model.PayAmount= 1; 
			model.TransmitNo= guid; 
			model.CustomerName= guid; 
			model.CustomerLastName= guid; 
			model.SparePartSaleWaybillIdList= guid; 
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
			model.IsActive= true; 
			model.IsActiveString= guid; 
			model.BankRequired= true; 
			model.InstalmentRequired= true; 
			model.DefermentRequired= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _SparePartSaleInvoiceBL.DMLSparePartSaleInvoice(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void SparePartSaleInvoiceBL_GetSparePartSaleInvoice_GetModel()
		{
			 var resultGet = _SparePartSaleInvoiceBL.GetSparePartSaleInvoice(UserManager.UserInfo, 1);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.SparePartSaleId > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void SparePartSaleInvoiceBL_ListSparePartSaleInvoices_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartSaleInvoiceViewModel();
			model.SparePartSaleId= 1; 
			model.SparePartSaleWaybillId= 1; 
			model.SparePartSaleInvoiceId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.CustomerId= 1; 
			model.InvoiceSerialNo= guid; 
			model.InvoiceNo= guid; 
			model.PayAmount= 1; 
			model.TransmitNo= guid; 
			model.CustomerName= guid; 
			model.CustomerLastName= guid; 
			model.SparePartSaleWaybillIdList= guid; 
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
			model.IsActive= true; 
			model.IsActiveString= guid; 
			model.BankRequired= true; 
			model.InstalmentRequired= true; 
			model.DefermentRequired= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _SparePartSaleInvoiceBL.DMLSparePartSaleInvoice(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new SparePartSaleInvoiceListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.CustomerAddressZipCode = guid; 
			
			 var resultGet = _SparePartSaleInvoiceBL.ListSparePartSaleInvoices(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void SparePartSaleInvoiceBL_DMLSparePartSaleInvoice_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartSaleInvoiceViewModel();
			model.SparePartSaleId= 1; 
			model.SparePartSaleWaybillId= 1; 
			model.SparePartSaleInvoiceId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.CustomerId= 1; 
			model.InvoiceSerialNo= guid; 
			model.InvoiceNo= guid; 
			model.PayAmount= 1; 
			model.TransmitNo= guid; 
			model.CustomerName= guid; 
			model.CustomerLastName= guid; 
			model.SparePartSaleWaybillIdList= guid; 
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
			model.IsActive= true; 
			model.IsActiveString= guid; 
			model.BankRequired= true; 
			model.InstalmentRequired= true; 
			model.DefermentRequired= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _SparePartSaleInvoiceBL.DMLSparePartSaleInvoice(UserManager.UserInfo, model);
			
			var filter = new SparePartSaleInvoiceListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.CustomerAddressZipCode = guid; 
			
			int count = 0;
			 var resultGet = _SparePartSaleInvoiceBL.ListSparePartSaleInvoices(UserManager.UserInfo, filter, out count);
			
			var modelUpdate = new SparePartSaleInvoiceViewModel();
			modelUpdate.DealerId = UserManager.UserInfo.DealerID; 
			modelUpdate.InvoiceSerialNo= guid; 
			modelUpdate.InvoiceNo= guid; 
			modelUpdate.TransmitNo= guid; 
			modelUpdate.CustomerName= guid; 
			modelUpdate.CustomerLastName= guid; 
			modelUpdate.SparePartSaleWaybillIdList= guid; 
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
			modelUpdate.IsActiveString= guid; 
			modelUpdate.CommandType = "U";
			 var resultUpdate = _SparePartSaleInvoiceBL.DMLSparePartSaleInvoice(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void SparePartSaleInvoiceBL_DMLSparePartSaleInvoice_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartSaleInvoiceViewModel();
			model.SparePartSaleId= 1; 
			model.SparePartSaleWaybillId= 1; 
			model.SparePartSaleInvoiceId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.CustomerId= 1; 
			model.InvoiceSerialNo= guid; 
			model.InvoiceNo= guid; 
			model.PayAmount= 1; 
			model.TransmitNo= guid; 
			model.CustomerName= guid; 
			model.CustomerLastName= guid; 
			model.SparePartSaleWaybillIdList= guid; 
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
			model.IsActive= true; 
			model.IsActiveString= guid; 
			model.BankRequired= true; 
			model.InstalmentRequired= true; 
			model.DefermentRequired= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _SparePartSaleInvoiceBL.DMLSparePartSaleInvoice(UserManager.UserInfo, model);
			
			var filter = new SparePartSaleInvoiceListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.CustomerAddressZipCode = guid; 
			
			int count = 0;
			 var resultGet = _SparePartSaleInvoiceBL.ListSparePartSaleInvoices(UserManager.UserInfo, filter, out count);
			
			var modelDelete = new SparePartSaleInvoiceViewModel();
			modelDelete.DealerId = UserManager.UserInfo.DealerID; 
			modelDelete.InvoiceSerialNo= guid; 
			modelDelete.InvoiceNo= guid; 
			modelDelete.TransmitNo= guid; 
			modelDelete.CustomerName= guid; 
			modelDelete.CustomerLastName= guid; 
			modelDelete.SparePartSaleWaybillIdList= guid; 
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
			modelDelete.IsActiveString= guid; 
			modelDelete.CommandType = "D";
			 var resultDelete = _SparePartSaleInvoiceBL.DMLSparePartSaleInvoice(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}


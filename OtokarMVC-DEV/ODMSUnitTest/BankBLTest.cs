using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.Bank;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class BankBLTest
	{

		BankBL _BankBL = new BankBL();

		[TestMethod]
		public void BankBL_DMLBank_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new BankDetailModel();
			model.Code= guid; 
			model.Name= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _BankBL.DMLBank(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void BankBL_GetBank_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new BankDetailModel();
			model.Code= guid; 
			model.Name= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _BankBL.DMLBank(UserManager.UserInfo, model);
			
			var filter = new BankDetailModel();
			filter.Id = result.Model.Id;
			filter.Code = guid; 
			
			 var resultGet = _BankBL.GetBank(filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.Id > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void BankBL_ListBanks_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new BankDetailModel();
			model.Code= guid; 
			model.Name= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _BankBL.DMLBank(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new BankListModel();
			filter.Code = guid; 
			
			 var resultGet = _BankBL.ListBanks(filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void BankBL_DMLBank_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new BankDetailModel();
			model.Code= guid; 
			model.Name= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _BankBL.DMLBank(UserManager.UserInfo, model);
			
			var filter = new BankListModel();
			filter.Code = guid; 
			
			int count = 0;
			 var resultGet = _BankBL.ListBanks(filter, out count);
			
			var modelUpdate = new BankDetailModel();
			modelUpdate.Id = resultGet.Data.First().Id;
			modelUpdate.Code= guid; 
			modelUpdate.Name= guid; 
			
			
			
			modelUpdate.CommandType = "U";
			 var resultUpdate = _BankBL.DMLBank(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void BankBL_DMLBank_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new BankDetailModel();
			model.Code= guid; 
			model.Name= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _BankBL.DMLBank(UserManager.UserInfo, model);
			
			var filter = new BankListModel();
			filter.Code = guid; 
			
			int count = 0;
			 var resultGet = _BankBL.ListBanks(filter, out count);
			
			var modelDelete = new BankDetailModel();
			modelDelete.Id = resultGet.Data.First().Id;
			modelDelete.Code= guid; 
			modelDelete.Name= guid; 
			
			
			
			modelDelete.CommandType = "D";
			 var resultDelete = _BankBL.DMLBank(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}

		[TestMethod]
		public void BankBL_ListBanks_GetAll_1()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new BankDetailModel();
			model.Code= guid; 
			model.Name= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _BankBL.DMLBank(UserManager.UserInfo, model);

            var param = new ODMSModel.Filter.PropertyFilter<string>();
            param.Value = guid;

			var filter = new BankFilter();
			filter.Code = param; 
			
			 var resultGet = _BankBL.ListBanks(filter);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

	}

}


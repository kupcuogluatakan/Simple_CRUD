using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.PurchaseOrderGroup;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class PurchaseOrderGroupBLTest
	{

		PurchaseOrderGroupBL _PurchaseOrderGroupBL = new PurchaseOrderGroupBL();

		[TestMethod]
		public void PurchaseOrderGroupBL_Insert_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PurchaseOrderGroupViewModel();
			model.PurchaseOrderGroupId= 1; 
			model.GroupName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PurchaseOrderGroupBL.Insert(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void PurchaseOrderGroupBL_Update_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PurchaseOrderGroupViewModel();
			model.PurchaseOrderGroupId= 1; 
			model.GroupName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PurchaseOrderGroupBL.Update(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

        [TestMethod]
		public void PurchaseOrderGroupBL_Get_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PurchaseOrderGroupViewModel();
			model.PurchaseOrderGroupId= 1; 
			model.GroupName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PurchaseOrderGroupBL.Update(UserManager.UserInfo, model);
			
			var filter = new PurchaseOrderGroupViewModel();
			 var resultGet = _PurchaseOrderGroupBL.Get(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
		}
        
		[TestMethod]
		public void PurchaseOrderGroupBL_List_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PurchaseOrderGroupViewModel();
			model.PurchaseOrderGroupId= 1; 
			model.GroupName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PurchaseOrderGroupBL.Update(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new PurchaseOrderGroupViewModel();
			
			 var resultGet = _PurchaseOrderGroupBL.List(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void PurchaseOrderGroupBL_Update_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PurchaseOrderGroupViewModel();
			model.PurchaseOrderGroupId= 1; 
			model.GroupName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PurchaseOrderGroupBL.Update(UserManager.UserInfo, model);
			
			var filter = new PurchaseOrderGroupViewModel();
			
			int count = 0;
			 var resultGet = _PurchaseOrderGroupBL.List(UserManager.UserInfo, filter);
			
			var modelUpdate = new PurchaseOrderGroupViewModel();
			modelUpdate.GroupName= guid; 
			
			modelUpdate.CommandType = "U";
			 var resultUpdate = _PurchaseOrderGroupBL.Update(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void PurchaseOrderGroupBL_Update_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PurchaseOrderGroupViewModel();
			model.PurchaseOrderGroupId= 1; 
			model.GroupName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PurchaseOrderGroupBL.Update(UserManager.UserInfo, model);
			
			var filter = new PurchaseOrderGroupViewModel();
			
			int count = 0;
			 var resultGet = _PurchaseOrderGroupBL.List(UserManager.UserInfo, filter);
			
			var modelDelete = new PurchaseOrderGroupViewModel();
			modelDelete.GroupName= guid; 
			modelDelete.CommandType = "D";
			 var resultDelete = _PurchaseOrderGroupBL.Update(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}


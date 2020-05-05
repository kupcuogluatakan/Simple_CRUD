using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.PurchaseOrderMatch;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class PurchaseOrderMatchBLTest
	{

		PurchaseOrderMatchBL _PurchaseOrderMatchBL = new PurchaseOrderMatchBL();

		[TestMethod]
		public void PurchaseOrderMatchBL_Insert_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PurchaseOrderMatchViewModel();
			model.SalesOrganization= guid; 
			model.DistrChan= guid; 
			model.Division= guid; 
			model.PurhcaseOrderGroupName= guid; 
			model.PurhcaseOrderTypeName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PurchaseOrderMatchBL.Insert(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void PurchaseOrderMatchBL_Update_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PurchaseOrderMatchViewModel();
			model.SalesOrganization= guid; 
			model.DistrChan= guid; 
			model.Division= guid; 
			model.PurhcaseOrderGroupName= guid; 
			model.PurhcaseOrderTypeName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PurchaseOrderMatchBL.Update(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}
        
		[TestMethod]
		public void PurchaseOrderMatchBL_Get_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PurchaseOrderMatchViewModel();
			model.SalesOrganization= guid; 
			model.DistrChan= guid; 
			model.Division= guid; 
			model.PurhcaseOrderGroupName= guid; 
			model.PurhcaseOrderTypeName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PurchaseOrderMatchBL.Update(UserManager.UserInfo, model);
			
			var filter = new PurchaseOrderMatchViewModel();
			 var resultGet = _PurchaseOrderMatchBL.Get(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
		}
        
		[TestMethod]
		public void PurchaseOrderMatchBL_List_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PurchaseOrderMatchViewModel();
			model.SalesOrganization= guid; 
			model.DistrChan= guid; 
			model.Division= guid; 
			model.PurhcaseOrderGroupName= guid; 
			model.PurhcaseOrderTypeName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PurchaseOrderMatchBL.Update(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new PurchaseOrderMatchViewModel();
			
			 var resultGet = _PurchaseOrderMatchBL.List(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void PurchaseOrderMatchBL_Update_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PurchaseOrderMatchViewModel();
			model.SalesOrganization= guid; 
			model.DistrChan= guid; 
			model.Division= guid; 
			model.PurhcaseOrderGroupName= guid; 
			model.PurhcaseOrderTypeName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PurchaseOrderMatchBL.Update(UserManager.UserInfo, model);
			
			var filter = new PurchaseOrderMatchViewModel();
			
			int count = 0;
			 var resultGet = _PurchaseOrderMatchBL.List(UserManager.UserInfo, filter);
			
			var modelUpdate = new PurchaseOrderMatchViewModel();
			modelUpdate.SalesOrganization= guid; 
			modelUpdate.DistrChan= guid; 
			modelUpdate.Division= guid; 
			modelUpdate.PurhcaseOrderGroupName= guid; 
			modelUpdate.PurhcaseOrderTypeName= guid; 
			modelUpdate.CommandType = "U";
			 var resultUpdate = _PurchaseOrderMatchBL.Update(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void PurchaseOrderMatchBL_Update_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PurchaseOrderMatchViewModel();
			model.SalesOrganization= guid; 
			model.DistrChan= guid; 
			model.Division= guid; 
			model.PurhcaseOrderGroupName= guid; 
			model.PurhcaseOrderTypeName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PurchaseOrderMatchBL.Update(UserManager.UserInfo, model);
			
			var filter = new PurchaseOrderMatchViewModel();
			
			int count = 0;
			 var resultGet = _PurchaseOrderMatchBL.List(UserManager.UserInfo, filter);
			
			var modelDelete = new PurchaseOrderMatchViewModel();
			modelDelete.SalesOrganization= guid; 
			modelDelete.DistrChan= guid; 
			modelDelete.Division= guid; 
			modelDelete.PurhcaseOrderGroupName= guid; 
			modelDelete.PurhcaseOrderTypeName= guid; 
			modelDelete.CommandType = "D";
			 var resultDelete = _PurchaseOrderMatchBL.Update(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}


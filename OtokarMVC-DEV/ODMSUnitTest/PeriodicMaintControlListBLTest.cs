using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.PeriodicMaintControlList;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class PeriodicMaintControlListBLTest
	{

		PeriodicMaintControlListBL _PeriodicMaintControlListBL = new PeriodicMaintControlListBL();

		[TestMethod]
		public void PeriodicMaintControlListBL_DMLPeriodicMaintControlList_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PeriodicMaintControlListViewModel();
			model.PeriodicMaintCtrlListId= 1; 
			model.LanguageCustom= guid; 
			model.ModelKod = "ATLAS"; 
			model.TypeName= guid; 
			model.DocumentDesc= guid; 
			model.DocName= guid; 
			model.EngineType= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PeriodicMaintControlListBL.DMLPeriodicMaintControlList(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void PeriodicMaintControlListBL_ListPeriodicMaintControlList_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PeriodicMaintControlListViewModel();
			model.PeriodicMaintCtrlListId= 1; 
			model.LanguageCustom= guid; 
			model.ModelKod = "ATLAS"; 
			model.TypeName= guid; 
			model.DocumentDesc= guid; 
			model.DocName= guid; 
			model.EngineType= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PeriodicMaintControlListBL.DMLPeriodicMaintControlList(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new PeriodicMaintControlListListModel();
			filter.ModelKod = "ATLAS"; 
			
			 var resultGet = _PeriodicMaintControlListBL.ListPeriodicMaintControlList(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void PeriodicMaintControlListBL_DMLPeriodicMaintControlList_Update()
		{
		    var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PeriodicMaintControlListViewModel();
			model.PeriodicMaintCtrlListId= 1; 
			model.LanguageCustom= guid; 
			model.ModelKod = "ATLAS"; 
			model.TypeName= guid; 
			model.DocumentDesc= guid; 
			model.DocName= guid; 
			model.EngineType= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PeriodicMaintControlListBL.DMLPeriodicMaintControlList(UserManager.UserInfo, model);
			
			var filter = new PeriodicMaintControlListListModel();
			filter.ModelKod = "ATLAS"; 
			
			int count = 0;
			 var resultGet = _PeriodicMaintControlListBL.ListPeriodicMaintControlList(UserManager.UserInfo, filter, out count);
			
			var modelUpdate = new PeriodicMaintControlListViewModel();
			modelUpdate.PeriodicMaintCtrlListId = resultGet.Data.First().PeriodicMaintCtrlListId;
			modelUpdate.LanguageCustom= guid; 
			modelUpdate.ModelKod = "ATLAS"; 
			modelUpdate.TypeName= guid; 
			modelUpdate.DocumentDesc= guid; 
			modelUpdate.DocName= guid; 
			modelUpdate.EngineType= guid; 
			modelUpdate.CommandType = "U";
			 var resultUpdate = _PeriodicMaintControlListBL.DMLPeriodicMaintControlList(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void PeriodicMaintControlListBL_DMLPeriodicMaintControlList_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PeriodicMaintControlListViewModel();
			model.PeriodicMaintCtrlListId= 1; 
			model.LanguageCustom= guid; 
			model.ModelKod = "ATLAS"; 
			model.TypeName= guid; 
			model.DocumentDesc= guid; 
			model.DocName= guid; 
			model.EngineType= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PeriodicMaintControlListBL.DMLPeriodicMaintControlList(UserManager.UserInfo, model);
			
			var filter = new PeriodicMaintControlListListModel();
			filter.ModelKod = "ATLAS"; 
			
			int count = 0;
			 var resultGet = _PeriodicMaintControlListBL.ListPeriodicMaintControlList(UserManager.UserInfo, filter, out count);
			
			var modelDelete = new PeriodicMaintControlListViewModel();
			modelDelete.PeriodicMaintCtrlListId = resultGet.Data.First().PeriodicMaintCtrlListId;
			modelDelete.LanguageCustom= guid; 
			modelDelete.ModelKod = "ATLAS"; 
			modelDelete.TypeName= guid; 
			modelDelete.DocumentDesc= guid; 
			modelDelete.DocName= guid; 
			modelDelete.EngineType= guid; 
			
			
			
			modelDelete.CommandType = "D";
			 var resultDelete = _PeriodicMaintControlListBL.DMLPeriodicMaintControlList(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}

		[TestMethod]
		public void PeriodicMaintControlListBL_GetPeriodicMaintControlList_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PeriodicMaintControlListViewModel();
			model.PeriodicMaintCtrlListId= 1; 
			model.LanguageCustom= guid; 
			model.ModelKod = "ATLAS"; 
			model.TypeName= guid; 
			model.DocumentDesc= guid; 
			model.DocName= guid; 
			model.EngineType= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PeriodicMaintControlListBL.DMLPeriodicMaintControlList(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new PeriodicMaintControlListViewModel();
			filter.ModelKod = "ATLAS"; 
			
			 var resultGet = _PeriodicMaintControlListBL.GetPeriodicMaintControlList(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		
	}

}


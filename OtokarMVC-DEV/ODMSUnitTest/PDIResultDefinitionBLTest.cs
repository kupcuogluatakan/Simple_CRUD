using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.PDIResultDefinition;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class PDIResultDefinitionBLTest
	{

		PDIResultDefinitionBL _PDIResultDefinitionBL = new PDIResultDefinitionBL();

		[TestMethod]
		public void PDIResultDefinitionBL_DMLPDIResultDefinition_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PDIResultDefinitionViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.PDIResultCode= guid; 
			model.AdminDesc= guid; 
			model.IsActive= true; 
			model.IsActiveName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _PDIResultDefinitionBL.DMLPDIResultDefinition(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void PDIResultDefinitionBL_GetPDIResultDefinition_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PDIResultDefinitionViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.PDIResultCode= guid; 
			model.AdminDesc= guid; 
			model.IsActive= true; 
			model.IsActiveName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _PDIResultDefinitionBL.DMLPDIResultDefinition(UserManager.UserInfo, model);
			
			var filter = new PDIResultDefinitionViewModel();
			filter.PDIResultCode = result.Model.PDIResultCode;
			filter.MultiLanguageContentAsText = "TR || TEST"; 
			filter.PDIResultCode = guid; 
			
			 var resultGet = _PDIResultDefinitionBL.GetPDIResultDefinition(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.PDIResultCode !=String.Empty && resultGet.IsSuccess);
		}

		[TestMethod]
		public void PDIResultDefinitionBL_ListPDIResultDefinition_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PDIResultDefinitionViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.PDIResultCode= guid; 
			model.AdminDesc= guid; 
			model.IsActive= true; 
			model.IsActiveName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _PDIResultDefinitionBL.DMLPDIResultDefinition(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new PDIResultDefinitionListModel();
			filter.PDIResultCode = guid; 
			
			 var resultGet = _PDIResultDefinitionBL.ListPDIResultDefinition(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void PDIResultDefinitionBL_DMLPDIResultDefinition_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PDIResultDefinitionViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.PDIResultCode= guid; 
			model.AdminDesc= guid; 
			model.IsActive= true; 
			model.IsActiveName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _PDIResultDefinitionBL.DMLPDIResultDefinition(UserManager.UserInfo, model);
			
			var filter = new PDIResultDefinitionListModel();
			filter.PDIResultCode = guid; 
			
			int count = 0;
			 var resultGet = _PDIResultDefinitionBL.ListPDIResultDefinition(UserManager.UserInfo, filter, out count);
			
			var modelUpdate = new PDIResultDefinitionViewModel();
			modelUpdate.MultiLanguageContentAsText = "TR || TEST"; 
			modelUpdate.PDIResultCode= guid; 
			modelUpdate.AdminDesc= guid; 
			
			modelUpdate.IsActiveName= guid; 
			
			
			modelUpdate.CommandType = "U";
			 var resultUpdate = _PDIResultDefinitionBL.DMLPDIResultDefinition(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void PDIResultDefinitionBL_DMLPDIResultDefinition_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PDIResultDefinitionViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.PDIResultCode= guid; 
			model.AdminDesc= guid; 
			model.IsActive= true; 
			model.IsActiveName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _PDIResultDefinitionBL.DMLPDIResultDefinition(UserManager.UserInfo, model);
			
			var filter = new PDIResultDefinitionListModel();
			filter.PDIResultCode = guid; 
			
			int count = 0;
			 var resultGet = _PDIResultDefinitionBL.ListPDIResultDefinition(UserManager.UserInfo, filter, out count);
			
			var modelDelete = new PDIResultDefinitionViewModel();
			modelDelete.MultiLanguageContentAsText = "TR || TEST"; 
			modelDelete.PDIResultCode= guid; 
			modelDelete.AdminDesc= guid; 
			modelDelete.IsActiveName= guid; 
			modelDelete.CommandType = "D";
			 var resultDelete = _PDIResultDefinitionBL.DMLPDIResultDefinition(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}


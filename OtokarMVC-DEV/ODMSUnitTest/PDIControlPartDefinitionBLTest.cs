using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.PDIControlPartDefinition;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class PDIControlPartDefinitionBLTest
	{

		PDIControlPartDefinitionBL _PDIControlPartDefinitionBL = new PDIControlPartDefinitionBL();

		[TestMethod]
		public void PDIControlPartDefinitionBL_DMLPDIControlPartDefinition_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PDIControlPartDefinitionViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.IdPDIControlDefinition= 1; 
			model.PDIControlModelCode= guid; 
			model.PDIControlCode= guid; 
			model.PDIModelCode= guid; 
			model.PDIPartCode= guid; 
			model.IsActive= true; 
			model.IsActiveName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _PDIControlPartDefinitionBL.DMLPDIControlPartDefinition(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void PDIControlPartDefinitionBL_GetPDIControlPartDefinition_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PDIControlPartDefinitionViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.IdPDIControlDefinition= 1; 
			model.PDIControlModelCode= guid; 
			model.PDIControlCode= guid; 
			model.PDIModelCode= guid; 
			model.PDIPartCode= guid; 
			model.IsActive= true; 
			model.IsActiveName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _PDIControlPartDefinitionBL.DMLPDIControlPartDefinition(UserManager.UserInfo, model);
			
			var filter = new PDIControlPartDefinitionViewModel();
			filter.IdPDIControlDefinition = result.Model.IdPDIControlDefinition;
			filter.MultiLanguageContentAsText = "TR || TEST"; 
			filter.PDIControlModelCode = guid; 
			filter.PDIControlCode = guid; 
			filter.PDIModelCode = guid; 
			filter.PDIPartCode = guid; 
			
			 var resultGet = _PDIControlPartDefinitionBL.GetPDIControlPartDefinition(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.IdPDIControlDefinition > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void PDIControlPartDefinitionBL_ListPDIControlPartDefinition_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PDIControlPartDefinitionViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.IdPDIControlDefinition= 1; 
			model.PDIControlModelCode= guid; 
			model.PDIControlCode= guid; 
			model.PDIModelCode= guid; 
			model.PDIPartCode= guid; 
			model.IsActive= true; 
			model.IsActiveName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _PDIControlPartDefinitionBL.DMLPDIControlPartDefinition(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new PDIControlPartDefinitionListModel();
			filter.MultiLanguageContentAsText = "TR || TEST"; 
			filter.PDIControlModelCode = guid; 
			filter.PDIControlCode = guid; 
			filter.PDIModelCode = guid; 
			filter.PDIPartCode = guid; 
			
			 var resultGet = _PDIControlPartDefinitionBL.ListPDIControlPartDefinition(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void PDIControlPartDefinitionBL_DMLPDIControlPartDefinition_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PDIControlPartDefinitionViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.IdPDIControlDefinition= 1; 
			model.PDIControlModelCode= guid; 
			model.PDIControlCode= guid; 
			model.PDIModelCode= guid; 
			model.PDIPartCode= guid; 
			model.IsActive= true; 
			model.IsActiveName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _PDIControlPartDefinitionBL.DMLPDIControlPartDefinition(UserManager.UserInfo, model);
			
			var filter = new PDIControlPartDefinitionListModel();
			filter.MultiLanguageContentAsText = "TR || TEST"; 
			filter.PDIControlModelCode = guid; 
			filter.PDIControlCode = guid; 
			filter.PDIModelCode = guid; 
			filter.PDIPartCode = guid; 
			
			int count = 0;
			 var resultGet = _PDIControlPartDefinitionBL.ListPDIControlPartDefinition(UserManager.UserInfo, filter, out count);
			
			var modelUpdate = new PDIControlPartDefinitionViewModel();
			modelUpdate.IdPDIControlDefinition = resultGet.Data.First().IdPDIControlDefinition.Value;
			modelUpdate.MultiLanguageContentAsText = "TR || TEST"; 
			
			modelUpdate.PDIControlModelCode= guid; 
			modelUpdate.PDIControlCode= guid; 
			modelUpdate.PDIModelCode= guid; 
			modelUpdate.PDIPartCode= guid; 
			
			modelUpdate.IsActiveName= guid; 
			
			
			modelUpdate.CommandType = "U";
			 var resultUpdate = _PDIControlPartDefinitionBL.DMLPDIControlPartDefinition(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void PDIControlPartDefinitionBL_DMLPDIControlPartDefinition_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PDIControlPartDefinitionViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.IdPDIControlDefinition= 1; 
			model.PDIControlModelCode= guid; 
			model.PDIControlCode= guid; 
			model.PDIModelCode= guid; 
			model.PDIPartCode= guid; 
			model.IsActive= true; 
			model.IsActiveName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _PDIControlPartDefinitionBL.DMLPDIControlPartDefinition(UserManager.UserInfo, model);
			
			var filter = new PDIControlPartDefinitionListModel();
			filter.MultiLanguageContentAsText = "TR || TEST"; 
			filter.PDIControlModelCode = guid; 
			filter.PDIControlCode = guid; 
			filter.PDIModelCode = guid; 
			filter.PDIPartCode = guid; 
			
			int count = 0;
			 var resultGet = _PDIControlPartDefinitionBL.ListPDIControlPartDefinition(UserManager.UserInfo, filter, out count);
			
			var modelDelete = new PDIControlPartDefinitionViewModel();
			modelDelete.IdPDIControlDefinition = resultGet.Data.First().IdPDIControlDefinition.Value;
			modelDelete.MultiLanguageContentAsText = "TR || TEST"; 
			
			modelDelete.PDIControlModelCode= guid; 
			modelDelete.PDIControlCode= guid; 
			modelDelete.PDIModelCode= guid; 
			modelDelete.PDIPartCode= guid; 
			
			modelDelete.IsActiveName= guid; 
			
			
			modelDelete.CommandType = "D";
			 var resultDelete = _PDIControlPartDefinitionBL.DMLPDIControlPartDefinition(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}


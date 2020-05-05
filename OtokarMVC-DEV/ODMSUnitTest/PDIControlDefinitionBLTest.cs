using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.PDIControlDefinition;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class PDIControlDefinitionBLTest
	{

		PDIControlDefinitionBL _PDIControlDefinitionBL = new PDIControlDefinitionBL();

		[TestMethod]
		public void PDIControlDefinitionBL_DMLPDIControlDefinition_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PDIControlDefinitionViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.IdPDIControlDefinition= 1; 
			model.PDIControlCode= guid; 
			model.ModelKod = "ATLAS"; 
			model.IsGroupCodeName= guid; 
			model.IsActive= true; 
			model.IsActiveName= guid; 
			model.HasActiveControlPartMatch= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _PDIControlDefinitionBL.DMLPDIControlDefinition(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void PDIControlDefinitionBL_GetPDIControlDefinition_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PDIControlDefinitionViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.IdPDIControlDefinition= 1; 
			model.PDIControlCode= guid; 
			model.ModelKod = "ATLAS"; 
			model.IsGroupCodeName= guid; 
			model.IsActive= true; 
			model.IsActiveName= guid; 
			model.HasActiveControlPartMatch= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _PDIControlDefinitionBL.DMLPDIControlDefinition(UserManager.UserInfo, model);
			
			var filter = new PDIControlDefinitionViewModel();
			filter.IdPDIControlDefinition = result.Model.IdPDIControlDefinition;
			filter.MultiLanguageContentAsText = "TR || TEST"; 
			filter.PDIControlCode = guid; 
			filter.ModelKod = "ATLAS"; 
			filter.IsGroupCode = true; 
			filter.IsGroupCodeName = guid; 
			
			 var resultGet = _PDIControlDefinitionBL.GetPDIControlDefinition(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.IdPDIControlDefinition > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void PDIControlDefinitionBL_ListPDIControlDefinition_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PDIControlDefinitionViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.IdPDIControlDefinition= 1; 
			model.PDIControlCode= guid; 
			model.ModelKod = "ATLAS"; 
			model.IsGroupCodeName= guid; 
			model.IsActive= true; 
			model.IsActiveName= guid; 
			model.HasActiveControlPartMatch= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _PDIControlDefinitionBL.DMLPDIControlDefinition(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new PDIControlDefinitionListModel();
			filter.PDIControlCode = guid; 
			filter._PDIControlCode = guid; 
			filter.ModelKod = "ATLAS"; 
			filter.IsGroupCode = true; 
			filter.IsGroupCodeName = guid; 
			
			 var resultGet = _PDIControlDefinitionBL.ListPDIControlDefinition(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void PDIControlDefinitionBL_DMLPDIControlDefinition_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PDIControlDefinitionViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.IdPDIControlDefinition= 1; 
			model.PDIControlCode= guid; 
			model.ModelKod = "ATLAS"; 
			model.IsGroupCodeName= guid; 
			model.IsActive= true; 
			model.IsActiveName= guid; 
			model.HasActiveControlPartMatch= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _PDIControlDefinitionBL.DMLPDIControlDefinition(UserManager.UserInfo, model);
			
			var filter = new PDIControlDefinitionListModel();
			filter.PDIControlCode = guid; 
			filter._PDIControlCode = guid; 
			filter.ModelKod = "ATLAS"; 
			filter.IsGroupCode = true; 
			filter.IsGroupCodeName = guid; 
			
			int count = 0;
			 var resultGet = _PDIControlDefinitionBL.ListPDIControlDefinition(UserManager.UserInfo, filter, out count);
			
			var modelUpdate = new PDIControlDefinitionViewModel();
			modelUpdate.IdPDIControlDefinition = resultGet.Data.First().IdPDIControlDefinition;
			modelUpdate.MultiLanguageContentAsText = "TR || TEST"; 
			
			modelUpdate.PDIControlCode= guid; 
			modelUpdate.ModelKod = "ATLAS"; 
			modelUpdate.IsGroupCodeName= guid; 
			
			modelUpdate.IsActiveName= guid; 
			
			
			
			modelUpdate.CommandType = "U";
			 var resultUpdate = _PDIControlDefinitionBL.DMLPDIControlDefinition(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void PDIControlDefinitionBL_DMLPDIControlDefinition_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PDIControlDefinitionViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.IdPDIControlDefinition= 1; 
			model.PDIControlCode= guid; 
			model.ModelKod = "ATLAS"; 
			model.IsGroupCodeName= guid; 
			model.IsActive= true; 
			model.IsActiveName= guid; 
			model.HasActiveControlPartMatch= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _PDIControlDefinitionBL.DMLPDIControlDefinition(UserManager.UserInfo, model);
			
			var filter = new PDIControlDefinitionListModel();
			filter.PDIControlCode = guid; 
			filter._PDIControlCode = guid; 
			filter.ModelKod = "ATLAS"; 
			filter.IsGroupCode = true; 
			filter.IsGroupCodeName = guid; 
			
			int count = 0;
			 var resultGet = _PDIControlDefinitionBL.ListPDIControlDefinition(UserManager.UserInfo, filter, out count);
			
			var modelDelete = new PDIControlDefinitionViewModel();
			modelDelete.IdPDIControlDefinition = resultGet.Data.First().IdPDIControlDefinition;
			modelDelete.MultiLanguageContentAsText = "TR || TEST"; 
			
			modelDelete.PDIControlCode= guid; 
			modelDelete.ModelKod = "ATLAS"; 
			modelDelete.IsGroupCodeName= guid; 
			
			modelDelete.IsActiveName= guid; 
			
			
			
			modelDelete.CommandType = "D";
			 var resultDelete = _PDIControlDefinitionBL.DMLPDIControlDefinition(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}


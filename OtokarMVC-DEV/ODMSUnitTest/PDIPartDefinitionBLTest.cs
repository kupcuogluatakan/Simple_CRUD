using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.PDIPartDefinition;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class PDIPartDefinitionBLTest
	{

		PDIPartDefinitionBL _PDIPartDefinitionBL = new PDIPartDefinitionBL();

		[TestMethod]
		public void PDIPartDefinitionBL_DMLPDIPartDefinition_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PDIPartDefinitionViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.PdiPartCode= guid; 
			model.AdminDesc= guid; 
			model.IsActive= true; 
			model.IsActiveName= guid; 
			model.PartName.MultiLanguageContentAsText = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.PartDesc= guid; 
			model.HasActiveControlPartMatch= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _PDIPartDefinitionBL.DMLPDIPartDefinition(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void PDIPartDefinitionBL_GetPDIPartDefinition_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PDIPartDefinitionViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.PdiPartCode= guid; 
			model.AdminDesc= guid; 
			model.IsActive= true; 
			model.IsActiveName= guid; 
			model.PartName.MultiLanguageContentAsText = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.PartDesc= guid; 
			model.HasActiveControlPartMatch= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _PDIPartDefinitionBL.DMLPDIPartDefinition(UserManager.UserInfo, model);
			
			var filter = new PDIPartDefinitionViewModel();
			filter.PartDesc= result.Model.PartDesc;
			filter.MultiLanguageContentAsText = "TR || TEST"; 
			filter.PdiPartCode = guid; 
			filter.PartName.MultiLanguageContentAsText = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			
			 var resultGet = _PDIPartDefinitionBL.GetPDIPartDefinition(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.PartDesc!=String.Empty&& resultGet.IsSuccess);
		}

		[TestMethod]
		public void PDIPartDefinitionBL_ListPDIPartDefinition_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PDIPartDefinitionViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.PdiPartCode= guid; 
			model.AdminDesc= guid; 
			model.IsActive= true; 
			model.IsActiveName= guid; 
			model.PartName.MultiLanguageContentAsText = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.PartDesc= guid; 
			model.HasActiveControlPartMatch= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _PDIPartDefinitionBL.DMLPDIPartDefinition(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new PDIPartDefinitionListModel();
			filter.PdiPartCode = guid; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			
			 var resultGet = _PDIPartDefinitionBL.ListPDIPartDefinition(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void PDIPartDefinitionBL_DMLPDIPartDefinition_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PDIPartDefinitionViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.PdiPartCode= guid; 
			model.AdminDesc= guid; 
			model.IsActive= true; 
			model.IsActiveName= guid; 
			model.PartName.MultiLanguageContentAsText = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.PartDesc= guid; 
			model.HasActiveControlPartMatch= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _PDIPartDefinitionBL.DMLPDIPartDefinition(UserManager.UserInfo, model);
			
			var filter = new PDIPartDefinitionListModel();
			filter.PdiPartCode = guid; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			
			int count = 0;
			 var resultGet = _PDIPartDefinitionBL.ListPDIPartDefinition(UserManager.UserInfo, filter, out count);
			
			var modelUpdate = new PDIPartDefinitionViewModel();
			modelUpdate.MultiLanguageContentAsText = "TR || TEST"; 
			modelUpdate.PdiPartCode= guid; 
			modelUpdate.AdminDesc= guid; 
			
			modelUpdate.IsActiveName= guid; 
			modelUpdate.PartName.MultiLanguageContentAsText = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			modelUpdate.PartDesc= guid; 
			
			
			
			modelUpdate.CommandType = "U";
			 var resultUpdate = _PDIPartDefinitionBL.DMLPDIPartDefinition(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void PDIPartDefinitionBL_DMLPDIPartDefinition_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PDIPartDefinitionViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.PdiPartCode= guid; 
			model.AdminDesc= guid; 
			model.IsActive= true; 
			model.IsActiveName= guid; 
			model.PartName.MultiLanguageContentAsText = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.PartDesc= guid; 
			model.HasActiveControlPartMatch= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _PDIPartDefinitionBL.DMLPDIPartDefinition(UserManager.UserInfo, model);
			
			var filter = new PDIPartDefinitionListModel();
			filter.PdiPartCode = guid; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			
			int count = 0;
			 var resultGet = _PDIPartDefinitionBL.ListPDIPartDefinition(UserManager.UserInfo, filter, out count);
			
			var modelDelete = new PDIPartDefinitionViewModel();
			modelDelete.MultiLanguageContentAsText = "TR || TEST"; 
			modelDelete.PdiPartCode= guid; 
			modelDelete.AdminDesc= guid; 
			modelDelete.IsActiveName= guid; 
			modelDelete.PartName.MultiLanguageContentAsText = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			modelDelete.PartDesc= guid; 
			modelDelete.CommandType = "D";
			 var resultDelete = _PDIPartDefinitionBL.DMLPDIPartDefinition(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}


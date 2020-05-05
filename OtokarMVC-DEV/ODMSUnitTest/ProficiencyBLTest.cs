using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.Proficiency;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class ProficiencyBLTest
	{

		ProficiencyBL _ProficiencyBL = new ProficiencyBL();

		[TestMethod]
		public void ProficiencyBL_DMLProficiency_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ProficiencyDetailModel();
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.ProficiencyCode= guid; 
			model.ProficiencyName= guid; 
			model.Description= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ProficiencyBL.DMLProficiency(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void ProficiencyBL_DMLDealerProficiency_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ProficiencyDetailModel();
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.ProficiencyCode= guid; 
			model.ProficiencyName= guid; 
			model.Description= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ProficiencyBL.DMLDealerProficiency(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void ProficiencyBL_GetProficiency_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ProficiencyDetailModel();
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.ProficiencyCode= guid; 
			model.ProficiencyName= guid; 
			model.Description= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ProficiencyBL.DMLDealerProficiency(UserManager.UserInfo, model);
			
			var filter = new ProficiencyDetailModel();
			filter.ProficiencyCode = result.Model.ProficiencyCode;
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.MultiLanguageContentAsText = "TR || TEST"; 
			filter.ProficiencyCode = guid; 
			
			 var resultGet = _ProficiencyBL.GetProficiency(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.ProficiencyCode!=String.Empty && resultGet.IsSuccess);
		}

		[TestMethod]
		public void ProficiencyBL_ListProficiencies_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ProficiencyDetailModel();
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.ProficiencyCode= guid; 
			model.ProficiencyName= guid; 
			model.Description= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ProficiencyBL.DMLDealerProficiency(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new ProficiencyListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.ProficiencyCode = guid; 
			
			 var resultGet = _ProficiencyBL.ListProficiencies(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
        
		[TestMethod]
		public void ProficiencyBL_DMLDealerProficiency_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ProficiencyDetailModel();
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.ProficiencyCode= guid; 
			model.ProficiencyName= guid; 
			model.Description= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ProficiencyBL.DMLDealerProficiency(UserManager.UserInfo, model);
			
			var filter = new ProficiencyListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.ProficiencyCode = guid; 
			
			int count = 0;
			 var resultGet = _ProficiencyBL.ListProficiencies(UserManager.UserInfo, filter, out count);
			
			var modelDelete = new ProficiencyDetailModel();
			modelDelete.DealerId = UserManager.UserInfo.DealerID; 
			modelDelete.MultiLanguageContentAsText = "TR || TEST"; 
			modelDelete.ProficiencyCode= guid; 
			modelDelete.ProficiencyName= guid; 
			modelDelete.Description= guid; 
			
			modelDelete.CommandType = "D";
			 var resultDelete = _ProficiencyBL.DMLDealerProficiency(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}

		[TestMethod]
		public void ProficiencyBL_ListProficienciesOfDealer_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ProficiencyDetailModel();
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.ProficiencyCode= guid; 
			model.ProficiencyName= guid; 
			model.Description= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ProficiencyBL.DMLDealerProficiency(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new ProficiencyListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.ProficiencyCode = guid; 
			
			 var resultGet = _ProficiencyBL.ListProficienciesOfDealer(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
		
		[TestMethod]
		public void ProficiencyBL_ListProficiesAsSelectListItem_GetAll()
		{
			 var resultGet = ProficiencyBL.ListProficiesAsSelectListItem(UserManager.UserInfo);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void ProficiencyBL_DMLDealerProficiency_Update()
		{
		    var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var modelUpdate = new ProficiencyDetailModel();
			modelUpdate.DealerId = UserManager.UserInfo.DealerID; 
			modelUpdate.MultiLanguageContentAsText = "TR || TEST"; 
			modelUpdate.ProficiencyCode= guid; 
			modelUpdate.ProficiencyName= guid; 
			modelUpdate.Description= guid; 
			modelUpdate.CommandType = "U";
			 var resultUpdate = _ProficiencyBL.DMLDealerProficiency(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}
        

	}

}


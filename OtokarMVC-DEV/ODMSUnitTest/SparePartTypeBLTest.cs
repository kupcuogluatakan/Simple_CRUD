using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.SparePartType;
using ODMSModel.ListModel;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class SparePartTypeBLTest
	{

		SparePartTypeBL _SparePartTypeBL = new SparePartTypeBL();

		[TestMethod]
		public void SparePartTypeBL_DMLSparePartType_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartTypeIndexViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.PartTypeCode= guid; 
			model.AdminDesc= guid; 
			model.IsActive= true; 
			model.IsActiveName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _SparePartTypeBL.DMLSparePartType(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void SparePartTypeBL_GetSparePartType_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartTypeIndexViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.PartTypeCode= guid; 
			model.AdminDesc= guid; 
			model.IsActive= true; 
			model.IsActiveName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _SparePartTypeBL.DMLSparePartType(UserManager.UserInfo, model);
			
			var filter = new SparePartTypeIndexViewModel();
			filter.MultiLanguageContentAsText = "TR || TEST"; 
			filter.PartTypeCode = guid; 
			
			 var resultGet = _SparePartTypeBL.GetSparePartType(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.PartTypeCode !=String.Empty && resultGet.IsSuccess);
		}

		[TestMethod]
		public void SparePartTypeBL_ListSparePartTypeAsSelectListItem_GetAll()
		{
			 var resultGet = SparePartTypeBL.ListSparePartTypeAsSelectListItem(UserManager.UserInfo);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
		[TestMethod]
		public void SparePartTypeBL_ListSparePartTypes_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartTypeIndexViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.PartTypeCode= guid; 
			model.AdminDesc= guid; 
			model.IsActive= true; 
			model.IsActiveName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _SparePartTypeBL.DMLSparePartType(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new SparePartTypeListModel();
			filter.PartTypeCode = guid; 
			
			 var resultGet = _SparePartTypeBL.ListSparePartTypes(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void SparePartTypeBL_DMLSparePartType_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartTypeIndexViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.PartTypeCode= guid; 
			model.AdminDesc= guid; 
			model.IsActive= true; 
			model.IsActiveName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _SparePartTypeBL.DMLSparePartType(UserManager.UserInfo, model);
			
			var filter = new SparePartTypeListModel();
			filter.PartTypeCode = guid; 
			
			int count = 0;
			 var resultGet = _SparePartTypeBL.ListSparePartTypes(UserManager.UserInfo, filter, out count);
			
			var modelUpdate = new SparePartTypeIndexViewModel();
			modelUpdate.MultiLanguageContentAsText = "TR || TEST"; 
			modelUpdate.PartTypeCode= guid; 
			modelUpdate.AdminDesc= guid; 
			modelUpdate.IsActiveName= guid; 
			modelUpdate.CommandType = "U";
			 var resultUpdate = _SparePartTypeBL.DMLSparePartType(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void SparePartTypeBL_DMLSparePartType_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartTypeIndexViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.PartTypeCode= guid; 
			model.AdminDesc= guid; 
			model.IsActive= true; 
			model.IsActiveName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _SparePartTypeBL.DMLSparePartType(UserManager.UserInfo, model);
			
			var filter = new SparePartTypeListModel();
			filter.PartTypeCode = guid; 
			
			int count = 0;
			 var resultGet = _SparePartTypeBL.ListSparePartTypes(UserManager.UserInfo, filter, out count);
			
			var modelDelete = new SparePartTypeIndexViewModel();
			modelDelete.PartTypeCode = resultGet.Data.First().PartTypeCode;
			modelDelete.MultiLanguageContentAsText = "TR || TEST"; 
			modelDelete.PartTypeCode= guid; 
			modelDelete.AdminDesc= guid; 
			
			modelDelete.IsActiveName= guid; 
			
			
			modelDelete.CommandType = "D";
			 var resultDelete = _SparePartTypeBL.DMLSparePartType(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}


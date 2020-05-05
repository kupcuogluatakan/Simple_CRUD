using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.WorkshopType;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class WorkshopTypeBLTest
	{

		WorkshopTypeBL _WorkshopTypeBL = new WorkshopTypeBL();

		[TestMethod]
		public void WorkshopTypeBL_DMLWorkshopType_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new WorkshopTypeViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.WorkshopTypeId= 1; 
			model.Descripion= guid; 
			model.IsActive= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _WorkshopTypeBL.DMLWorkshopType(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void WorkshopTypeBL_GetWorkshopType_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new WorkshopTypeViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.WorkshopTypeId= 1; 
			model.Descripion= guid; 
			model.IsActive= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _WorkshopTypeBL.DMLWorkshopType(UserManager.UserInfo, model);
			
			var filter = new WorkshopTypeViewModel();
			filter.WorkshopTypeId = result.Model.WorkshopTypeId;
			filter.MultiLanguageContentAsText = "TR || TEST"; 
			
			 var resultGet = _WorkshopTypeBL.GetWorkshopType(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.WorkshopTypeId > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void WorkshopTypeBL_GetListWorkshopType_GetAll()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new WorkshopTypeViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.WorkshopTypeId= 1; 
			model.Descripion= guid; 
			model.IsActive= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _WorkshopTypeBL.DMLWorkshopType(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new WorkshopTypeListModel();
			
			 var resultGet = _WorkshopTypeBL.GetListWorkshopType(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
       
		[TestMethod]
		public void WorkshopTypeBL_ListWorkshopTypeAsSelectList_GetAll()
		{
			 var resultGet = WorkshopTypeBL.ListWorkshopTypeAsSelectList(UserManager.UserInfo);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void WorkshopTypeBL_DMLWorkshopType_Update()
		{
		    var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var modelUpdate = new WorkshopTypeViewModel();
			modelUpdate.WorkshopTypeId = 1;
			modelUpdate.MultiLanguageContentAsText = "TR || TEST"; 
			modelUpdate.Descripion= guid; 
			modelUpdate.CommandType = "U";
			 var resultUpdate = _WorkshopTypeBL.DMLWorkshopType(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void WorkshopTypeBL_DMLWorkshopType_Delete()
		{
		    var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var modelDelete = new WorkshopTypeViewModel();
			modelDelete.WorkshopTypeId = 1;
			modelDelete.MultiLanguageContentAsText = "TR || TEST"; 
			modelDelete.Descripion= guid; 
			modelDelete.CommandType = "D";
			 var resultDelete = _WorkshopTypeBL.DMLWorkshopType(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}


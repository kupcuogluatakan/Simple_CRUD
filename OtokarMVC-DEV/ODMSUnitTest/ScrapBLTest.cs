using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.Scrap;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class ScrapBLTest
	{

		ScrapBL _ScrapBL = new ScrapBL();

		[TestMethod]
		public void ScrapBL_DMLScrap_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ScrapViewModel();
			model.ScrapId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.DocName= guid; 
			model.ScrapReasonDesc= guid; 
			model.ScrapReasonName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ScrapBL.DMLScrap(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void ScrapBL_GetScrap_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ScrapViewModel();
			model.ScrapId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.DocName= guid; 
			model.ScrapReasonDesc= guid; 
			model.ScrapReasonName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ScrapBL.DMLScrap(UserManager.UserInfo, model);
			
			var filter = new ScrapViewModel();
			filter.ScrapId = result.Model.ScrapId;
			filter.DealerId = UserManager.UserInfo.DealerID; 
			
			 var resultGet = _ScrapBL.GetScrap(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.ScrapId > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void ScrapBL_ListScrap_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ScrapViewModel();
			model.ScrapId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.DocName= guid; 
			model.ScrapReasonDesc= guid; 
			model.ScrapReasonName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ScrapBL.DMLScrap(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new ScrapListModel();
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			filter.PartCode = "M.162127"; 
			
			 var resultGet = _ScrapBL.ListScrap(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void ScrapBL_DMLScrap_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ScrapViewModel();
			model.ScrapId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.DocName= guid; 
			model.ScrapReasonDesc= guid; 
			model.ScrapReasonName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ScrapBL.DMLScrap(UserManager.UserInfo, model);
			
			var filter = new ScrapListModel();
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			filter.PartCode = "M.162127"; 
			
			int count = 0;
			 var resultGet = _ScrapBL.ListScrap(UserManager.UserInfo, filter, out count);
			
			var modelUpdate = new ScrapViewModel();
			modelUpdate.ScrapId = resultGet.Data.First().ScrapId;
			
			modelUpdate.DealerId = UserManager.UserInfo.DealerID; 
			modelUpdate.DealerName= guid; 
			modelUpdate.DocName= guid; 
			modelUpdate.ScrapReasonDesc= guid; 
			modelUpdate.ScrapReasonName= guid; 
			
			
			
			modelUpdate.CommandType = "U";
			 var resultUpdate = _ScrapBL.DMLScrap(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void ScrapBL_DMLScrap_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ScrapViewModel();
			model.ScrapId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.DocName= guid; 
			model.ScrapReasonDesc= guid; 
			model.ScrapReasonName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ScrapBL.DMLScrap(UserManager.UserInfo, model);
			
			var filter = new ScrapListModel();
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			filter.PartCode = "M.162127"; 
			
			int count = 0;
			 var resultGet = _ScrapBL.ListScrap(UserManager.UserInfo, filter, out count);
			
			var modelDelete = new ScrapViewModel();
			modelDelete.ScrapId = resultGet.Data.First().ScrapId;
			
			modelDelete.DealerId = UserManager.UserInfo.DealerID; 
			modelDelete.DealerName= guid; 
			modelDelete.DocName= guid; 
			modelDelete.ScrapReasonDesc= guid; 
			modelDelete.ScrapReasonName= guid; 
			
			
			
			modelDelete.CommandType = "D";
			 var resultDelete = _ScrapBL.DMLScrap(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}


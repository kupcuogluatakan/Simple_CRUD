using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.PurchaseOrderGroupRelation;
using ODMSCommon.Security;


namespace ODMSUnitTest
{

	[TestClass]
	public class PurchaseOrderGroupRelationBLTest
	{

		PurchaseOrderGroupRelationBL _PurchaseOrderGroupRelationBL = new PurchaseOrderGroupRelationBL();

		[TestMethod]
		public void PurchaseOrderGroupRelationBL_Update_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PurchaseGroupRelationSaveModel();
			model.PurchaseOrderGroupId= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PurchaseOrderGroupRelationBL.Update(model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void PurchaseOrderGroupRelationBL_Insert_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PurchaseOrderGroupRelationListModel();
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.PurchaseOrderGroupId= 1; 
			model.DealerName= guid; 
			model.GroupName= guid; 
			model.IsActiveString= guid; 
			model.StartPage= 1; 
			model.PageSize= 1; 
			model.TotalCount= 1; 
			model.FilteredTotalCount= 1; 
			model.SortColumn= guid; 
			model.SortDirection= guid; 
			model.FirstCall= 1; 
			 var result = _PurchaseOrderGroupRelationBL.Insert(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void PurchaseOrderGroupRelationBL_Update_Insert_1()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PurchaseOrderGroupRelationListModel();
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.PurchaseOrderGroupId= 1; 
			model.DealerName= guid; 
			model.GroupName= guid; 
			model.IsActiveString= guid; 
			model.StartPage= 1; 
			model.PageSize= 1; 
			model.TotalCount= 1; 
			model.FilteredTotalCount= 1; 
			model.SortColumn= guid; 
			model.SortDirection= guid; 
			model.FirstCall= 1; 
			 var result = _PurchaseOrderGroupRelationBL.Update(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void PurchaseOrderGroupRelationBL_Get_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PurchaseOrderGroupRelationListModel();
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.PurchaseOrderGroupId= 1; 
			model.DealerName= guid; 
			model.GroupName= guid; 
			model.IsActiveString= guid; 
			model.StartPage= 1; 
			model.PageSize= 1; 
			model.TotalCount= 1; 
			model.FilteredTotalCount= 1; 
			model.SortColumn= guid; 
			model.SortDirection= guid; 
			model.FirstCall= 1; 
			 var result = _PurchaseOrderGroupRelationBL.Update(UserManager.UserInfo, model);
			
			var filter = new PurchaseOrderGroupRelationListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			
			 var resultGet = _PurchaseOrderGroupRelationBL.Get(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.DealerId > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void PurchaseOrderGroupRelationBL_ListOfIncluded_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PurchaseOrderGroupRelationListModel();
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.PurchaseOrderGroupId= 1; 
			model.DealerName= guid; 
			model.GroupName= guid; 
			model.IsActiveString= guid; 
			model.StartPage= 1; 
			model.PageSize= 1; 
			model.TotalCount= 1; 
			model.FilteredTotalCount= 1; 
			model.SortColumn= guid; 
			model.SortDirection= guid; 
			model.FirstCall= 1; 
			 var result = _PurchaseOrderGroupRelationBL.Update(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new PurchaseOrderGroupRelationListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			
			 var resultGet = _PurchaseOrderGroupRelationBL.ListOfIncluded(filter);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void PurchaseOrderGroupRelationBL_Update_Update_1()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PurchaseOrderGroupRelationListModel();
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.PurchaseOrderGroupId= 1; 
			model.DealerName= guid; 
			model.GroupName= guid; 
			model.IsActiveString= guid; 
			model.StartPage= 1; 
			model.PageSize= 1; 
			model.TotalCount= 1; 
			model.FilteredTotalCount= 1; 
			model.SortColumn= guid; 
			model.SortDirection= guid; 
			model.FirstCall= 1; 
			 var result = _PurchaseOrderGroupRelationBL.Update(UserManager.UserInfo, model);
			
			var filter = new PurchaseOrderGroupRelationListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			
			int count = 0;
			 var resultGet = _PurchaseOrderGroupRelationBL.ListOfIncluded(filter);
			
			var modelUpdate = new PurchaseOrderGroupRelationListModel();
			modelUpdate.DealerId = UserManager.UserInfo.DealerID; 
			modelUpdate.DealerName= guid; 
			modelUpdate.GroupName= guid; 
			modelUpdate.IsActiveString= guid; 
			modelUpdate.SortColumn= guid; 
			modelUpdate.SortDirection= guid; 
			 var resultUpdate = _PurchaseOrderGroupRelationBL.Update(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void PurchaseOrderGroupRelationBL_Update_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PurchaseOrderGroupRelationListModel();
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.PurchaseOrderGroupId= 1; 
			model.DealerName= guid; 
			model.GroupName= guid; 
			model.IsActiveString= guid; 
			model.StartPage= 1; 
			model.PageSize= 1; 
			model.TotalCount= 1; 
			model.FilteredTotalCount= 1; 
			model.SortColumn= guid; 
			model.SortDirection= guid; 
			model.FirstCall= 1; 
			 var result = _PurchaseOrderGroupRelationBL.Update(UserManager.UserInfo, model);
			
			var filter = new PurchaseOrderGroupRelationListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			
			int count = 0;
			 var resultGet = _PurchaseOrderGroupRelationBL.ListOfIncluded(filter);
			
			var modelDelete = new PurchaseOrderGroupRelationListModel();
			modelDelete.DealerId = UserManager.UserInfo.DealerID; 
			modelDelete.DealerName= guid; 
			modelDelete.GroupName= guid; 
			modelDelete.IsActiveString= guid; 
			modelDelete.SortColumn= guid; 
			modelDelete.SortDirection= guid; 
			 var resultDelete = _PurchaseOrderGroupRelationBL.Update(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}

		[TestMethod]
		public void PurchaseOrderGroupRelationBL_ListOfNotIncluded_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PurchaseOrderGroupRelationListModel();
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.PurchaseOrderGroupId= 1; 
			model.DealerName= guid; 
			model.GroupName= guid; 
			model.IsActiveString= guid; 
			model.StartPage= 1; 
			model.PageSize= 1; 
			model.TotalCount= 1; 
			model.FilteredTotalCount= 1; 
			model.SortColumn= guid; 
			model.SortDirection= guid; 
			model.FirstCall= 1; 
			 var result = _PurchaseOrderGroupRelationBL.Update(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new PurchaseOrderGroupRelationListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			
			 var resultGet = _PurchaseOrderGroupRelationBL.ListOfNotIncluded(filter);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
        
		[TestMethod]
		public void PurchaseOrderGroupRelationBL_Update_Delete_1()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PurchaseOrderGroupRelationListModel();
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.PurchaseOrderGroupId= 1; 
			model.DealerName= guid; 
			model.GroupName= guid; 
			model.IsActiveString= guid; 
			model.StartPage= 1; 
			model.PageSize= 1; 
			model.TotalCount= 1; 
			model.FilteredTotalCount= 1; 
			model.SortColumn= guid; 
			model.SortDirection= guid; 
			model.FirstCall= 1; 
			 var result = _PurchaseOrderGroupRelationBL.Update(UserManager.UserInfo, model);
			
			var filter = new PurchaseOrderGroupRelationListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			
			int count = 0;
			 var resultGet = _PurchaseOrderGroupRelationBL.ListOfNotIncluded(filter);
			
			var modelDelete = new PurchaseOrderGroupRelationListModel();
			modelDelete.DealerId = UserManager.UserInfo.DealerID; 
			
			modelDelete.DealerName= guid; 
			modelDelete.GroupName= guid; 
			modelDelete.IsActiveString= guid; 
			
			
			
			
			modelDelete.SortColumn= guid; 
			modelDelete.SortDirection= guid; 
			 var resultDelete = _PurchaseOrderGroupRelationBL.Update(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}

		[TestMethod]
		public void PurchaseOrderGroupRelationBL_List_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PurchaseOrderGroupRelationListModel();
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.PurchaseOrderGroupId= 1; 
			model.DealerName= guid; 
			model.GroupName= guid; 
			model.IsActiveString= guid; 
			model.StartPage= 1; 
			model.PageSize= 1; 
			model.TotalCount= 1; 
			model.FilteredTotalCount= 1; 
			model.SortColumn= guid; 
			model.SortDirection= guid; 
			model.FirstCall= 1; 
			 var result = _PurchaseOrderGroupRelationBL.Update(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new PurchaseOrderGroupRelationListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			
			 var resultGet = _PurchaseOrderGroupRelationBL.List(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void PurchaseOrderGroupRelationBL_Update_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PurchaseOrderGroupRelationListModel();
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.PurchaseOrderGroupId= 1; 
			model.DealerName= guid; 
			model.GroupName= guid; 
			model.IsActiveString= guid; 
			model.StartPage= 1; 
			model.PageSize= 1; 
			model.TotalCount= 1; 
			model.FilteredTotalCount= 1; 
			model.SortColumn= guid; 
			model.SortDirection= guid; 
			model.FirstCall= 1; 
			 var result = _PurchaseOrderGroupRelationBL.Update(UserManager.UserInfo, model);
			
			var filter = new PurchaseOrderGroupRelationListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			
			int count = 0;
			 var resultGet = _PurchaseOrderGroupRelationBL.List(UserManager.UserInfo, filter);
			
			var modelUpdate = new PurchaseOrderGroupRelationListModel();
			modelUpdate.DealerId = UserManager.UserInfo.DealerID; 
			
			modelUpdate.DealerName= guid; 
			modelUpdate.GroupName= guid; 
			modelUpdate.IsActiveString= guid; 
			
			
			
			
			modelUpdate.SortColumn= guid; 
			modelUpdate.SortDirection= guid; 
			 var resultUpdate = _PurchaseOrderGroupRelationBL.Update(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void PurchaseOrderGroupRelationBL_Update_Delete_2()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PurchaseOrderGroupRelationListModel();
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.PurchaseOrderGroupId= 1; 
			model.DealerName= guid; 
			model.GroupName= guid; 
			model.IsActiveString= guid; 
			model.StartPage= 1; 
			model.PageSize= 1; 
			model.TotalCount= 1; 
			model.FilteredTotalCount= 1; 
			model.SortColumn= guid; 
			model.SortDirection= guid; 
			model.FirstCall= 1; 
			 var result = _PurchaseOrderGroupRelationBL.Update(UserManager.UserInfo, model);
			
			var filter = new PurchaseOrderGroupRelationListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			
			int count = 0;
			 var resultGet = _PurchaseOrderGroupRelationBL.List(UserManager.UserInfo, filter);
			
			var modelDelete = new PurchaseOrderGroupRelationListModel();
			modelDelete.DealerId = UserManager.UserInfo.DealerID; 
			
			modelDelete.DealerName= guid; 
			modelDelete.GroupName= guid; 
			modelDelete.IsActiveString= guid; 
			
			
			
			
			modelDelete.SortColumn= guid; 
			modelDelete.SortDirection= guid; 
			var resultDelete = _PurchaseOrderGroupRelationBL.Update(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}


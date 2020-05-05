using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.StockCardPriceListModel;


namespace ODMSUnitTest
{

	[TestClass]
	public class StockCardPriceListBLTest
	{

		StockCardPriceListBL _StockCardPriceListBL = new StockCardPriceListBL();

		[TestMethod]
		public void StockCardPriceListBL_Insert_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new StockCardPriceListModel();
			model.StockCardId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.PartId= 1; 
			model.PriceId= 1; 
			model.CompanyPrice= 1; 
			model.CostPrice= 1; 
			model.ListPrice= 1; 
			model.IsActiveString= guid; 
			model.StartPage= 1; 
			model.PageSize= 1; 
			model.TotalCount= 1; 
			model.FilteredTotalCount= 1; 
			model.SortColumn= guid; 
			model.SortDirection= guid; 
			model.FirstCall= 1; 
			 var result = _StockCardPriceListBL.Insert(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void StockCardPriceListBL_Update_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new StockCardPriceListModel();
			model.StockCardId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.PartId= 1; 
			model.PriceId= 1; 
			model.CompanyPrice= 1; 
			model.CostPrice= 1; 
			model.ListPrice= 1; 
			model.IsActiveString= guid; 
			model.StartPage= 1; 
			model.PageSize= 1; 
			model.TotalCount= 1; 
			model.FilteredTotalCount= 1; 
			model.SortColumn= guid; 
			model.SortDirection= guid; 
			model.FirstCall= 1; 
			 var result = _StockCardPriceListBL.Update(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void StockCardPriceListBL_Get_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new StockCardPriceListModel();
			model.StockCardId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.PartId= 1; 
			model.PriceId= 1; 
			model.CompanyPrice= 1; 
			model.CostPrice= 1; 
			model.ListPrice= 1; 
			model.IsActiveString= guid; 
			model.StartPage= 1; 
			model.PageSize= 1; 
			model.TotalCount= 1; 
			model.FilteredTotalCount= 1; 
			model.SortColumn= guid; 
			model.SortDirection= guid; 
			model.FirstCall= 1; 
			var result = _StockCardPriceListBL.Update(UserManager.UserInfo, model);
			var filter = new StockCardPriceListModel();
			filter.StockCardId = result.Model.StockCardId;
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.PartId = 39399; 
			
			 var resultGet = _StockCardPriceListBL.Get(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.StockCardId > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void StockCardPriceListBL_Get_GetModel_1()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new StockCardPriceListModel();
			model.StockCardId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.PartId= 1; 
			model.PriceId= 1; 
			model.CompanyPrice= 1; 
			model.CostPrice= 1; 
			model.ListPrice= 1; 
			model.IsActiveString= guid; 
			model.StartPage= 1; 
			model.PageSize= 1; 
			model.TotalCount= 1; 
			model.FilteredTotalCount= 1; 
			model.SortColumn= guid; 
			model.SortDirection= guid; 
			model.FirstCall= 1; 
			 var result = _StockCardPriceListBL.Update(UserManager.UserInfo, model);
			
			var filter = new StockCardPriceListModel();
			filter.StockCardId = result.Model.StockCardId;
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.PartId = 39399; 
			
			 var resultGet = _StockCardPriceListBL.Get(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.StockCardId > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void StockCardPriceListBL_List_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new StockCardPriceListModel();
			model.StockCardId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.PartId= 1; 
			model.PriceId= 1; 
			model.CompanyPrice= 1; 
			model.CostPrice= 1; 
			model.ListPrice= 1; 
			model.IsActiveString= guid; 
			model.StartPage= 1; 
			model.PageSize= 1; 
			model.TotalCount= 1; 
			model.FilteredTotalCount= 1; 
			model.SortColumn= guid; 
			model.SortDirection= guid; 
			model.FirstCall= 1; 
			 var result = _StockCardPriceListBL.Update(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new StockCardPriceListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.PartId = 39399; 
			
			 var resultGet = _StockCardPriceListBL.List(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void StockCardPriceListBL_Update_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new StockCardPriceListModel();
			model.StockCardId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.PartId= 1; 
			model.PriceId= 1; 
			model.CompanyPrice= 1; 
			model.CostPrice= 1; 
			model.ListPrice= 1; 
			model.IsActiveString= guid; 
			model.StartPage= 1; 
			model.PageSize= 1; 
			model.TotalCount= 1; 
			model.FilteredTotalCount= 1; 
			model.SortColumn= guid; 
			model.SortDirection= guid; 
			model.FirstCall= 1; 
			 var result = _StockCardPriceListBL.Update(UserManager.UserInfo, model);
			
			var filter = new StockCardPriceListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.PartId = 39399; 
			
			int count = 0;
			 var resultGet = _StockCardPriceListBL.List(UserManager.UserInfo, filter);
			
			var modelUpdate = new StockCardPriceListModel();
			modelUpdate.StockCardId = resultGet.Data.First().StockCardId;
			modelUpdate.DealerId = UserManager.UserInfo.DealerID; 
			modelUpdate.IsActiveString= guid; 
			modelUpdate.SortColumn= guid; 
			modelUpdate.SortDirection= guid; 
			var resultUpdate = _StockCardPriceListBL.Update(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void StockCardPriceListBL_Update_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new StockCardPriceListModel();
			model.StockCardId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.PartId= 1; 
			model.PriceId= 1; 
			model.CompanyPrice= 1; 
			model.CostPrice= 1; 
			model.ListPrice= 1; 
			model.IsActiveString= guid; 
			model.StartPage= 1; 
			model.PageSize= 1; 
			model.TotalCount= 1; 
			model.FilteredTotalCount= 1; 
			model.SortColumn= guid; 
			model.SortDirection= guid; 
			model.FirstCall= 1; 
			 var result = _StockCardPriceListBL.Update(UserManager.UserInfo, model);
			
			var filter = new StockCardPriceListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.PartId = 39399; 
			
			int count = 0;
			 var resultGet = _StockCardPriceListBL.List(UserManager.UserInfo, filter);
			
			var modelDelete = new StockCardPriceListModel();
			modelDelete.StockCardId = resultGet.Data.First().StockCardId;
			modelDelete.DealerId = UserManager.UserInfo.DealerID; 
			modelDelete.IsActiveString= guid; 
			modelDelete.SortColumn= guid; 
			modelDelete.SortDirection= guid; 
			 var resultDelete = _StockCardPriceListBL.Update(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}


using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.StockCardYearly;


namespace ODMSUnitTest
{

	[TestClass]
	public class StockCardYearlyBLTest
	{

		StockCardYearlyBL _StockCardYearlyBL = new StockCardYearlyBL();

		[TestMethod]
		public void StockCardYearlyBL_StartUpdateMonthlyStock_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new StockCardYearlyListModel();
			model.IsActiveString= guid; 
			model.StartPage= 1; 
			model.PageSize= 1; 
			model.TotalCount= 1; 
			model.FilteredTotalCount= 1; 
			model.SortColumn= guid; 
			model.SortDirection= guid; 
			model.FirstCall= 1; 
			 var result = _StockCardYearlyBL.StartUpdateMonthlyStock(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void StockCardYearlyBL_ListStockCardYearly_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new StockCardYearlyListModel();
			model.IsActiveString= guid; 
			model.StartPage= 1; 
			model.PageSize= 1; 
			model.TotalCount= 1; 
			model.FilteredTotalCount= 1; 
			model.SortColumn= guid; 
			model.SortDirection= guid; 
			model.FirstCall= 1; 
			 var result = _StockCardYearlyBL.StartUpdateMonthlyStock(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new StockCardYearlyListModel();
			
			 var resultGet = _StockCardYearlyBL.ListStockCardYearly(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void StockCardYearlyBL_StartUpdateMonthlyStock_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new StockCardYearlyListModel();
			model.IsActiveString= guid; 
			model.StartPage= 1; 
			model.PageSize= 1; 
			model.TotalCount= 1; 
			model.FilteredTotalCount= 1; 
			model.SortColumn= guid; 
			model.SortDirection= guid; 
			model.FirstCall= 1; 
			 var result = _StockCardYearlyBL.StartUpdateMonthlyStock(UserManager.UserInfo, model);
			
			var filter = new StockCardYearlyListModel();
			
			int count = 0;
			 var resultGet = _StockCardYearlyBL.ListStockCardYearly(UserManager.UserInfo, filter);
			
			var modelUpdate = new StockCardYearlyListModel();
			modelUpdate.IdStockType = resultGet.Data.First().IdStockType;
			modelUpdate.IsActiveString= guid; 
			modelUpdate.SortColumn= guid; 
			modelUpdate.SortDirection= guid; 
			 var resultUpdate = _StockCardYearlyBL.StartUpdateMonthlyStock(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void StockCardYearlyBL_StartUpdateMonthlyStock_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new StockCardYearlyListModel();
			model.IsActiveString= guid; 
			model.StartPage= 1; 
			model.PageSize= 1; 
			model.TotalCount= 1; 
			model.FilteredTotalCount= 1; 
			model.SortColumn= guid; 
			model.SortDirection= guid; 
			model.FirstCall= 1; 
			 var result = _StockCardYearlyBL.StartUpdateMonthlyStock(UserManager.UserInfo, model);
			
			var filter = new StockCardYearlyListModel();
			
			int count = 0;
			 var resultGet = _StockCardYearlyBL.ListStockCardYearly(UserManager.UserInfo, filter);
			
			var modelDelete = new StockCardYearlyListModel();
			modelDelete.IdStockType = resultGet.Data.First().IdStockType;
			modelDelete.IsActiveString= guid; 
			modelDelete.SortColumn= guid; 
			modelDelete.SortDirection= guid; 
			 var resultDelete = _StockCardYearlyBL.StartUpdateMonthlyStock(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}


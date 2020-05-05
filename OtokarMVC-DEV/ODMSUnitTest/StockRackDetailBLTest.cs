using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.StockRackDetail;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class StockRackDetailBLTest
	{

		StockRackDetailBL _StockRackDetailBL = new StockRackDetailBL();

		[TestMethod]
		public void StockRackDetailBL_DMLStockExchange_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new StockExchangeViewModel();
			model.FromWarehouseId= 1; 
			model.ToWarehouseId= 1; 
			model.MaxQuantity= 1; 
			model.Description= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _StockRackDetailBL.DMLStockExchange(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void StockRackDetailBL_GetQuantity_GetModel()
		{
			 var resultGet = _StockRackDetailBL.GetQuantity(26,253005);
			
			Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void StockRackDetailBL_ListEmptyStockRackDetail_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new StockExchangeViewModel();
			model.FromWarehouseId= 1; 
			model.ToWarehouseId= 1; 
			model.MaxQuantity= 1; 
			model.Description= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _StockRackDetailBL.DMLStockExchange(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new StockRackDetailListModel();
			filter.PartId = 39399; 
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			
			 var resultGet = _StockRackDetailBL.ListEmptyStockRackDetail(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
        
		[TestMethod]
		public void StockRackDetailBL_ListStockRackDetail_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new StockExchangeViewModel();
			model.FromWarehouseId= 1; 
			model.ToWarehouseId= 1; 
			model.MaxQuantity= 1; 
			model.Description= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _StockRackDetailBL.DMLStockExchange(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new StockRackDetailListModel();
			filter.PartId = 39399; 
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			
			 var resultGet = _StockRackDetailBL.ListStockRackDetail(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void StockRackDetailBL_DMLStockExchange_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new StockExchangeViewModel();
			model.FromWarehouseId= 1; 
			model.ToWarehouseId= 1; 
			model.MaxQuantity= 1; 
			model.Description= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _StockRackDetailBL.DMLStockExchange(UserManager.UserInfo, model);
			
			var filter = new StockRackDetailListModel();
			filter.PartId = 39399; 
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			
			int count = 0;
			 var resultGet = _StockRackDetailBL.ListStockRackDetail(UserManager.UserInfo, filter, out count);
			
			var modelUpdate = new StockExchangeViewModel();
			modelUpdate.PartId = resultGet.Data.First().PartId;
			modelUpdate.Description= guid; 
			modelUpdate.CommandType = "U";
			 var resultUpdate = _StockRackDetailBL.DMLStockExchange(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void StockRackDetailBL_DMLStockExchange_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new StockExchangeViewModel();
			model.FromWarehouseId= 1; 
			model.ToWarehouseId= 1; 
			model.MaxQuantity= 1; 
			model.Description= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _StockRackDetailBL.DMLStockExchange(UserManager.UserInfo, model);
			
			var filter = new StockRackDetailListModel();
			filter.PartId = 39399; 
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			
			int count = 0;
			 var resultGet = _StockRackDetailBL.ListStockRackDetail(UserManager.UserInfo, filter, out count);
			
			var modelDelete = new StockExchangeViewModel();
			modelDelete.PartId = resultGet.Data.First().PartId;
			modelDelete.Description= guid; 
			modelDelete.CommandType = "D";
			 var resultDelete = _StockRackDetailBL.DMLStockExchange(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}


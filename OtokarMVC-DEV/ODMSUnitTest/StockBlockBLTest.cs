using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.StockBlock;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class StockBlockBLTest
	{

		StockBlockBL _StockBlockBL = new StockBlockBL();

		[TestMethod]
		public void StockBlockBL_DMLStockBlock_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new StockBlockViewModel();
			model.DealerName= guid; 
			model.BlockReasonDesc= guid; 
			model.BlockedStatusId= 1; 
			model.BlockedStatusName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _StockBlockBL.DMLStockBlock(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void StockBlockBL_GetStockBlock_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new StockBlockViewModel();
			model.DealerName= guid; 
			model.BlockReasonDesc= guid; 
			model.BlockedStatusId= 1; 
			model.BlockedStatusName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _StockBlockBL.DMLStockBlock(UserManager.UserInfo, model);
			
			var filter = new StockBlockViewModel();
			filter.IdStockBlock = result.Model.IdStockBlock;
			 var resultGet = _StockBlockBL.GetStockBlock(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.IdStockBlock > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void StockBlockBL_ListStockBlock_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new StockBlockViewModel();
			model.DealerName= guid; 
			model.BlockReasonDesc= guid; 
			model.BlockedStatusId= 1; 
			model.BlockedStatusName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _StockBlockBL.DMLStockBlock(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new StockBlockListModel();
			
			 var resultGet = _StockBlockBL.ListStockBlock(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void StockBlockBL_DMLStockBlock_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new StockBlockViewModel();
			model.DealerName= guid; 
			model.BlockReasonDesc= guid; 
			model.BlockedStatusId= 1; 
			model.BlockedStatusName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _StockBlockBL.DMLStockBlock(UserManager.UserInfo, model);
			
			var filter = new StockBlockListModel();
			
			int count = 0;
			 var resultGet = _StockBlockBL.ListStockBlock(UserManager.UserInfo, filter, out count);
			
			var modelUpdate = new StockBlockViewModel();
			modelUpdate.IdStockBlock = resultGet.Data.First().IdStockBlock;
			modelUpdate.DealerName= guid; 
			modelUpdate.BlockReasonDesc= guid; 
			
			modelUpdate.BlockedStatusName= guid; 
			
			
			
			modelUpdate.CommandType = "U";
			 var resultUpdate = _StockBlockBL.DMLStockBlock(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void StockBlockBL_DMLStockBlock_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new StockBlockViewModel();
			model.DealerName= guid; 
			model.BlockReasonDesc= guid; 
			model.BlockedStatusId= 1; 
			model.BlockedStatusName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _StockBlockBL.DMLStockBlock(UserManager.UserInfo, model);
			
			var filter = new StockBlockListModel();
			
			int count = 0;
			 var resultGet = _StockBlockBL.ListStockBlock(UserManager.UserInfo, filter, out count);
			
			var modelDelete = new StockBlockViewModel();
		    modelDelete.IdStockBlock = resultGet.Data.First().IdStockBlock;
            modelDelete.DealerName= guid; 
			modelDelete.BlockReasonDesc= guid; 
			
			modelDelete.BlockedStatusName= guid; 
			
			
			
			modelDelete.CommandType = "D";
			 var resultDelete = _StockBlockBL.DMLStockBlock(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}


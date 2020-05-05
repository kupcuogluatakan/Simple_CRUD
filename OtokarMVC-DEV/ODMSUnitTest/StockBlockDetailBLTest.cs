using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.StockBlockDetail;
using System.Collections.Generic;


namespace ODMSUnitTest
{

	[TestClass]
	public class StockBlockDetailBLTest
	{

		StockBlockDetailBL _StockBlockDetailBL = new StockBlockDetailBL();

		[TestMethod]
		public void StockBlockDetailBL_DMLStockBlockDetail_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new StockBlockDetailViewModel();
			model.DealerName= guid; 
			model.PartCode = "M.162127"; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.StockTypeName= guid; 
			model.BlockReasonDesc= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _StockBlockDetailBL.DMLStockBlockDetail(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void StockBlockDetailBL_DMLStockBlockDetailList_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new StockBlockDetailViewModel();
			List<StockBlockDetailViewModel> list = new List<StockBlockDetailViewModel>();
			model.DealerName= guid; 
			model.PartCode = "M.162127"; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.StockTypeName= guid; 
			model.BlockReasonDesc= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _StockBlockDetailBL.DMLStockBlockDetailList(UserManager.UserInfo,list,model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void StockBlockDetailBL_GetStockBlockDetail_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new StockBlockDetailViewModel();
            List<StockBlockDetailViewModel> list = new List<StockBlockDetailViewModel>();
            model.DealerName= guid; 
			model.PartCode = "M.162127"; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.StockTypeName= guid; 
			model.BlockReasonDesc= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _StockBlockDetailBL.DMLStockBlockDetailList(UserManager.UserInfo, list,model);
			
			var filter = new StockBlockDetailViewModel();
			filter.IdStockBlock = result.Model.IdStockBlock;
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			
			 var resultGet = _StockBlockDetailBL.GetStockBlockDetail(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.IdStockBlock > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void StockBlockDetailBL_GetStockBlockDetails_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            List<StockBlockDetailViewModel> list = new List<StockBlockDetailViewModel>();
            var model = new StockBlockDetailViewModel();
			model.DealerName= guid; 
			model.PartCode = "M.162127"; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.StockTypeName= guid; 
			model.BlockReasonDesc= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _StockBlockDetailBL.DMLStockBlockDetailList(UserManager.UserInfo, list,model);
			
			var filter = new StockBlockDetailViewModel();
			filter.IdStockBlock = result.Model.IdStockBlock;
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			
			 var resultGet = _StockBlockDetailBL.GetStockBlockDetails(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.IdStockBlock > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void StockBlockDetailBL_ListStockBlockDetail_GetAll()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
		    List<StockBlockDetailViewModel> list = new List<StockBlockDetailViewModel>();
            var model = new StockBlockDetailViewModel();
			model.DealerName= guid; 
			model.PartCode = "M.162127"; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.StockTypeName= guid; 
			model.BlockReasonDesc= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _StockBlockDetailBL.DMLStockBlockDetailList(UserManager.UserInfo, list,model);
			
			int count = 0;
			var filter = new StockBlockDetailListModel();
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			filter.PartCode = "M.162127"; 
			
			 var resultGet = _StockBlockDetailBL.ListStockBlockDetail(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
        
		[TestMethod]
		public void StockBlockDetailBL_GetStockBlockDetailList_GetAll()
		{
		    List<StockBlockDetailViewModel> list = new List<StockBlockDetailViewModel>();
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new StockBlockDetailViewModel();
			model.DealerName= guid; 
			model.PartCode = "M.162127"; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.StockTypeName= guid; 
			model.BlockReasonDesc= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _StockBlockDetailBL.DMLStockBlockDetailList(UserManager.UserInfo, list,model);
			int count = 0;
			var filter = new StockBlockDetailViewModel();
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			
			 var resultGet = _StockBlockDetailBL.GetStockBlockDetailList(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void StockBlockDetailBL_DMLStockBlockDetailList_Update()
		{
		    List<StockBlockDetailViewModel> list = new List<StockBlockDetailViewModel>();
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new StockBlockDetailViewModel();
			model.DealerName= guid; 
			model.PartCode = "M.162127"; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.StockTypeName= guid; 
			model.BlockReasonDesc= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _StockBlockDetailBL.DMLStockBlockDetailList(UserManager.UserInfo, list,model);
			
			var filter = new StockBlockDetailViewModel();
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			
			int count = 0;
			 var resultGet = _StockBlockDetailBL.GetStockBlockDetailList(UserManager.UserInfo, filter);
			
			var modelUpdate = new StockBlockDetailViewModel();
			modelUpdate.IdStockBlock = resultGet.Data.First().IdStockBlock;
			modelUpdate.DealerName= guid; 
			modelUpdate.PartCode = "M.162127"; 
			modelUpdate.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			modelUpdate.StockTypeName= guid; 
			modelUpdate.BlockReasonDesc= guid; 
			modelUpdate.CommandType = "U";
			 var resultUpdate = _StockBlockDetailBL.DMLStockBlockDetailList(UserManager.UserInfo, list,modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void StockBlockDetailBL_DMLStockBlockDetailList_Delete()
		{
            List<StockBlockDetailViewModel> list = new List<StockBlockDetailViewModel>();
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new StockBlockDetailViewModel();
			model.DealerName= guid; 
			model.PartCode = "M.162127"; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.StockTypeName= guid; 
			model.BlockReasonDesc= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _StockBlockDetailBL.DMLStockBlockDetailList(UserManager.UserInfo, list,model);
			
			var filter = new StockBlockDetailViewModel();
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			
			int count = 0;
			 var resultGet = _StockBlockDetailBL.GetStockBlockDetailList(UserManager.UserInfo, filter);
			
			var modelDelete = new StockBlockDetailViewModel();
			modelDelete.IdStockBlock = resultGet.Data.First().IdStockBlock;
			modelDelete.DealerName= guid; 
			modelDelete.PartCode = "M.162127"; 
			modelDelete.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			modelDelete.StockTypeName= guid; 
			modelDelete.BlockReasonDesc= guid; 
			modelDelete.CommandType = "D";
			 var resultDelete = _StockBlockDetailBL.DMLStockBlockDetailList(UserManager.UserInfo, list,modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}


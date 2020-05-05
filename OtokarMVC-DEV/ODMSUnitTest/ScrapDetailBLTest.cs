using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.ScrapDetail;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class ScrapDetailBLTest
	{

		ScrapDetailBL _ScrapDetailBL = new ScrapDetailBL();

		[TestMethod]
		public void ScrapDetailBL_DMLScrapDetail_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ScrapDetailViewModel();
			model.ScrapId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.DocName= guid; 
			model.ScrapReasonDesc= guid; 
			model.ScrapReasonName= guid; 
			model.Barcode= guid; 
			model.ScrapDetailId= 1; 
			model.PartCode = "M.162127"; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.StockTypeName= guid; 
			model.WarehouseName= guid; 
			model.RackName= guid; 
			model.StockQuantity= 1; 
			model.Quantity= 1; 
			model.ConfirmUserName= guid; 
			model.CancelUserName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ScrapDetailBL.DMLScrapDetail(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void ScrapDetailBL_GetScrapDetail_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ScrapDetailViewModel();
			model.ScrapId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.DocName= guid; 
			model.ScrapReasonDesc= guid; 
			model.ScrapReasonName= guid; 
			model.Barcode= guid; 
			model.ScrapDetailId= 1; 
			model.PartCode = "M.162127"; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.StockTypeName= guid; 
			model.WarehouseName= guid; 
			model.RackName= guid; 
			model.StockQuantity= 1; 
			model.Quantity= 1; 
			model.ConfirmUserName= guid; 
			model.CancelUserName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ScrapDetailBL.DMLScrapDetail(UserManager.UserInfo, model);
			
			var filter = new ScrapDetailViewModel();
			filter.ScrapId = result.Model.ScrapId;
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.PartId = 39399; 
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			
			 var resultGet = _ScrapDetailBL.GetScrapDetail(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.ScrapId > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void ScrapDetailBL_ListScrapDetail_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ScrapDetailViewModel();
			model.ScrapId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.DocName= guid; 
			model.ScrapReasonDesc= guid; 
			model.ScrapReasonName= guid; 
			model.Barcode= guid; 
			model.ScrapDetailId= 1; 
			model.PartCode = "M.162127"; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.StockTypeName= guid; 
			model.WarehouseName= guid; 
			model.RackName= guid; 
			model.StockQuantity= 1; 
			model.Quantity= 1; 
			model.ConfirmUserName= guid; 
			model.CancelUserName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ScrapDetailBL.DMLScrapDetail(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new ScrapDetailListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.PartId = 39399; 
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			
			 var resultGet = _ScrapDetailBL.ListScrapDetail(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void ScrapDetailBL_DMLScrapDetail_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ScrapDetailViewModel();
			model.ScrapId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.DocName= guid; 
			model.ScrapReasonDesc= guid; 
			model.ScrapReasonName= guid; 
			model.Barcode= guid; 
			model.ScrapDetailId= 1; 
			model.PartCode = "M.162127"; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.StockTypeName= guid; 
			model.WarehouseName= guid; 
			model.RackName= guid; 
			model.StockQuantity= 1; 
			model.Quantity= 1; 
			model.ConfirmUserName= guid; 
			model.CancelUserName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ScrapDetailBL.DMLScrapDetail(UserManager.UserInfo, model);
			
			var filter = new ScrapDetailListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.PartId = 39399; 
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			
			int count = 0;
			 var resultGet = _ScrapDetailBL.ListScrapDetail(UserManager.UserInfo, filter, out count);
			
			var modelUpdate = new ScrapDetailViewModel();
			modelUpdate.ScrapId = resultGet.Data.First().ScrapId;
			
			modelUpdate.DealerId = UserManager.UserInfo.DealerID; 
			modelUpdate.DealerName= guid; 
			modelUpdate.DocName= guid; 
			modelUpdate.ScrapReasonDesc= guid; 
			modelUpdate.ScrapReasonName= guid; 
			modelUpdate.Barcode= guid; 
			
			modelUpdate.PartCode = "M.162127"; 
			modelUpdate.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			modelUpdate.StockTypeName= guid; 
			modelUpdate.WarehouseName= guid; 
			modelUpdate.RackName= guid; 
			
			
			modelUpdate.ConfirmUserName= guid; 
			modelUpdate.CancelUserName= guid; 
			
			
			
			modelUpdate.CommandType = "U";
			 var resultUpdate = _ScrapDetailBL.DMLScrapDetail(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}
        [TestMethod]
		public void ScrapDetailBL_ListScrapDetailPart_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ScrapDetailViewModel();
			model.ScrapId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.DocName= guid; 
			model.ScrapReasonDesc= guid; 
			model.ScrapReasonName= guid; 
			model.Barcode= guid; 
			model.ScrapDetailId= 1; 
			model.PartCode = "M.162127"; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.StockTypeName= guid; 
			model.WarehouseName= guid; 
			model.RackName= guid; 
			model.StockQuantity= 1; 
			model.Quantity= 1; 
			model.ConfirmUserName= guid; 
			model.CancelUserName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ScrapDetailBL.DMLScrapDetail(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new ScrapDetailListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.PartId = 39399; 
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			
			 var resultGet = _ScrapDetailBL.ListScrapDetailPart(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
        
		[TestMethod]
		public void ScrapDetailBL_ListScrapDetailPartByBarcode_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ScrapDetailViewModel();
			model.ScrapId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.DocName= guid; 
			model.ScrapReasonDesc= guid; 
			model.ScrapReasonName= guid; 
			model.Barcode= guid; 
			model.ScrapDetailId= 1; 
			model.PartCode = "M.162127"; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.StockTypeName= guid; 
			model.WarehouseName= guid; 
			model.RackName= guid; 
			model.StockQuantity= 1; 
			model.Quantity= 1; 
			model.ConfirmUserName= guid; 
			model.CancelUserName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ScrapDetailBL.DMLScrapDetail(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new ScrapDetailListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.PartId = 39399; 
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			
			 var resultGet = _ScrapDetailBL.ListScrapDetailPartByBarcode(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void ScrapDetailBL_DMLScrapDetail_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ScrapDetailViewModel();
			model.ScrapId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.DocName= guid; 
			model.ScrapReasonDesc= guid; 
			model.ScrapReasonName= guid; 
			model.Barcode= guid; 
			model.ScrapDetailId= 1; 
			model.PartCode = "M.162127"; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.StockTypeName= guid; 
			model.WarehouseName= guid; 
			model.RackName= guid; 
			model.StockQuantity= 1; 
			model.Quantity= 1; 
			model.ConfirmUserName= guid; 
			model.CancelUserName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ScrapDetailBL.DMLScrapDetail(UserManager.UserInfo, model);
			
			var filter = new ScrapDetailListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.PartId = 39399; 
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			
			int count = 0;
			 var resultGet = _ScrapDetailBL.ListScrapDetailPartByBarcode(UserManager.UserInfo, filter, out count);
			
			var modelDelete = new ScrapDetailViewModel();
			modelDelete.ScrapId = resultGet.Data.First().ScrapId;
			
			modelDelete.DealerId = UserManager.UserInfo.DealerID; 
			modelDelete.DealerName= guid; 
			modelDelete.DocName= guid; 
			modelDelete.ScrapReasonDesc= guid; 
			modelDelete.ScrapReasonName= guid; 
			modelDelete.Barcode= guid; 
			
			modelDelete.PartCode = "M.162127"; 
			modelDelete.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			modelDelete.StockTypeName= guid; 
			modelDelete.WarehouseName= guid; 
			modelDelete.RackName= guid; 
			
			
			modelDelete.ConfirmUserName= guid; 
			modelDelete.CancelUserName= guid; 
			
			
			
			modelDelete.CommandType = "D";
			 var resultDelete = _ScrapDetailBL.DMLScrapDetail(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}


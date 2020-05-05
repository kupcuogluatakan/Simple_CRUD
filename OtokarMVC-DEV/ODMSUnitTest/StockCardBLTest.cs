using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.StockCard;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class StockCardBLTest
	{

		StockCardBL _StockCardBL = new StockCardBL();

		[TestMethod]
		public void StockCardBL_DMLStockCard_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new StockCardViewModel();
			model.StockCardId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.PartDealerName= guid; 
			model.PartCode = "M.162127"; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.IsOriginalPart= true; 
			model.IsOriginalPartName= guid; 
			model.WarehouseName= guid; 
			model.RackName= guid; 
			model.AutoOrder= true; 
			model.AutoOrderName= guid; 
			model.AvgDealerPrice= 1; 
			model.UnitName= guid; 
			model.StockServiceValue= guid; 
			model.AlternatePart= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _StockCardBL.DMLStockCard(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void StockCardBL_GetDealerPriceByDealerAndPart_GetModel()
		{
			 var resultGet = _StockCardBL.GetDealerPriceByDealerAndPart(86, 119156);
			
			Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void StockCardBL_GetStockCard_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new StockCardViewModel();
			model.StockCardId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.PartDealerName= guid; 
			model.PartCode = "M.162127"; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.IsOriginalPart= true; 
			model.IsOriginalPartName= guid; 
			model.WarehouseName= guid; 
			model.RackName= guid; 
			model.AutoOrder= true; 
			model.AutoOrderName= guid; 
			model.AvgDealerPrice= 1; 
			model.UnitName= guid; 
			model.StockServiceValue= guid; 
			model.AlternatePart= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _StockCardBL.DMLStockCard(UserManager.UserInfo, model);
			
			var filter = new StockCardViewModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.PartId = 39399; 
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			
			 var resultGet = _StockCardBL.GetStockCard(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.DealerId > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void StockCardBL_GetStockCardById_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new StockCardViewModel();
			model.StockCardId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.PartDealerName= guid; 
			model.PartCode = "M.162127"; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.IsOriginalPart= true; 
			model.IsOriginalPartName= guid; 
			model.WarehouseName= guid; 
			model.RackName= guid; 
			model.AutoOrder= true; 
			model.AutoOrderName= guid; 
			model.AvgDealerPrice= 1; 
			model.UnitName= guid; 
			model.StockServiceValue= guid; 
			model.AlternatePart= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _StockCardBL.DMLStockCard(UserManager.UserInfo, model);
			
			var filter = new StockCardViewModel();
			filter.StockCardId = result.Model.StockCardId;
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.PartId = 39399; 
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			
			 var resultGet = _StockCardBL.GetStockCardById(filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.StockCardId > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void StockCardBL_ListStockCards_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new StockCardViewModel();
			model.StockCardId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.PartDealerName= guid; 
			model.PartCode = "M.162127"; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.IsOriginalPart= true; 
			model.IsOriginalPartName= guid; 
			model.WarehouseName= guid; 
			model.RackName= guid; 
			model.AutoOrder= true; 
			model.AutoOrderName= guid; 
			model.AvgDealerPrice= 1; 
			model.UnitName= guid; 
			model.StockServiceValue= guid; 
			model.AlternatePart= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _StockCardBL.DMLStockCard(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new StockCardListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.PartId = 39399; 
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			
			 var resultGet = _StockCardBL.ListStockCards(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
        [TestMethod]
		public void StockCardBL_ListStockCardSearch_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new StockCardViewModel();
			model.StockCardId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.PartDealerName= guid; 
			model.PartCode = "M.162127"; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.IsOriginalPart= true; 
			model.IsOriginalPartName= guid; 
			model.WarehouseName= guid; 
			model.RackName= guid; 
			model.AutoOrder= true; 
			model.AutoOrderName= guid; 
			model.AvgDealerPrice= 1; 
			model.UnitName= guid; 
			model.StockServiceValue= guid; 
			model.AlternatePart= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _StockCardBL.DMLStockCard(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new StockCardSearchListModel();
			filter.PartId = 39399; 
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			filter.DealerId = UserManager.UserInfo.DealerID; 
			
			 var resultGet = _StockCardBL.ListStockCardSearch(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void StockCardBL_DMLStockCard_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new StockCardViewModel();
			model.StockCardId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.PartDealerName= guid; 
			model.PartCode = "M.162127"; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.IsOriginalPart= true; 
			model.IsOriginalPartName= guid; 
			model.WarehouseName= guid; 
			model.RackName= guid; 
			model.AutoOrder= true; 
			model.AutoOrderName= guid; 
			model.AvgDealerPrice= 1; 
			model.UnitName= guid; 
			model.StockServiceValue= guid; 
			model.AlternatePart= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _StockCardBL.DMLStockCard(UserManager.UserInfo, model);
			
			var filter = new StockCardSearchListModel();
			filter.PartId = 39399; 
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			filter.DealerId = UserManager.UserInfo.DealerID; 
			
			int count = 0;
			 var resultGet = _StockCardBL.ListStockCardSearch(UserManager.UserInfo, filter, out count);
			
			var modelUpdate = new StockCardViewModel();
			modelUpdate.DealerId = UserManager.UserInfo.DealerID; 
			modelUpdate.DealerName= guid; 
			modelUpdate.PartDealerName= guid; 
			modelUpdate.PartCode = "M.162127"; 
			modelUpdate.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			modelUpdate.IsOriginalPartName= guid; 
			modelUpdate.WarehouseName= guid; 
			modelUpdate.RackName= guid; 
			modelUpdate.AutoOrderName= guid; 
			modelUpdate.UnitName= guid; 
			modelUpdate.StockServiceValue= guid; 
			modelUpdate.AlternatePart= guid; 
			modelUpdate.CommandType = "U";
			 var resultUpdate = _StockCardBL.DMLStockCard(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void StockCardBL_DMLStockCard_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new StockCardViewModel();
			model.StockCardId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.PartDealerName= guid; 
			model.PartCode = "M.162127"; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.IsOriginalPart= true; 
			model.IsOriginalPartName= guid; 
			model.WarehouseName= guid; 
			model.RackName= guid; 
			model.AutoOrder= true; 
			model.AutoOrderName= guid; 
			model.AvgDealerPrice= 1; 
			model.UnitName= guid; 
			model.StockServiceValue= guid; 
			model.AlternatePart= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _StockCardBL.DMLStockCard(UserManager.UserInfo, model);
			
			var filter = new StockCardSearchListModel();
			filter.PartId = 39399; 
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			filter.DealerId = UserManager.UserInfo.DealerID; 
			int count = 0;
			 var resultGet = _StockCardBL.ListStockCardSearch(UserManager.UserInfo, filter, out count);
			var modelDelete = new StockCardViewModel();
			modelDelete.DealerId = UserManager.UserInfo.DealerID; 
			modelDelete.DealerName= guid; 
			modelDelete.PartDealerName= guid; 
			modelDelete.PartCode = "M.162127"; 
			modelDelete.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			modelDelete.IsOriginalPartName= guid; 
			modelDelete.WarehouseName= guid; 
			modelDelete.RackName= guid; 
			modelDelete.AutoOrderName= guid; 
			modelDelete.UnitName= guid; 
			modelDelete.StockServiceValue= guid; 
			modelDelete.AlternatePart= guid; 
			modelDelete.CommandType = "D";
			 var resultDelete = _StockCardBL.DMLStockCard(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}


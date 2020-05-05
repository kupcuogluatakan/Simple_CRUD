using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.PurchaseOrderDetail;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class PurchaseOrderDetailBLTest
	{

		PurchaseOrderDetailBL _PurchaseOrderDetailBL = new PurchaseOrderDetailBL();

		[TestMethod]
		public void PurchaseOrderDetailBL_DMLPurchaseOrderDetail_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PurchaseOrderDetailViewModel();
			model.PurchaseOrderDetailSeqNo= 1; 
			model.PurchaseOrderNumber= 1; 
			model.UnitName= guid; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.StatusName= guid; 
			model.SAPOfferNo= guid; 
			model.SAPRowNo= guid; 
			model.DenyReason= guid; 
			model.RowNumber= guid; 
			model.PartCode = "M.162127"; 
			model.OrderNumber= guid; 
			model.SupplierName= guid; 
			model.PoTypeName= guid; 
			model.StockTypeName= guid; 
			model.VehicleName= guid; 
			model.CurrencyCode= guid; 
			model.IsCampaignPO= true; 
			model.SAPShipIdPart= 1; 
			model.StockServiceValue= guid; 
			model.SpecialExplanation= guid; 
			model.MstSupplierIdDealer= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PurchaseOrderDetailBL.DMLPurchaseOrderDetail(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void PurchaseOrderDetailBL_GetPurchaseOrderDetail_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PurchaseOrderDetailViewModel();
			model.PurchaseOrderDetailSeqNo= 1; 
			model.PurchaseOrderNumber= 1; 
			model.UnitName= guid; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.StatusName= guid; 
			model.SAPOfferNo= guid; 
			model.SAPRowNo= guid; 
			model.DenyReason= guid; 
			model.RowNumber= guid; 
			model.PartCode = "M.162127"; 
			model.OrderNumber= guid; 
			model.SupplierName= guid; 
			model.PoTypeName= guid; 
			model.StockTypeName= guid; 
			model.VehicleName= guid; 
			model.CurrencyCode= guid; 
			model.IsCampaignPO= true; 
			model.SAPShipIdPart= 1; 
			model.StockServiceValue= guid; 
			model.SpecialExplanation= guid; 
			model.MstSupplierIdDealer= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			var result = _PurchaseOrderDetailBL.DMLPurchaseOrderDetail(UserManager.UserInfo, model);
			
			var filter = new PurchaseOrderDetailViewModel();
			filter.PartId = 39399; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			filter.PartCode = "M.162127"; 
			filter.CurrencyCode = guid; 
			
			 var resultGet = _PurchaseOrderDetailBL.GetPurchaseOrderDetail(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void PurchaseOrderDetailBL_GetPurchaseOrderDetailsBySapInfo_GetModel()
		{
		    var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);

            var resultGet = _PurchaseOrderDetailBL.GetPurchaseOrderDetailsBySapInfo(guid, guid, guid);
			
			Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void PurchaseOrderDetailBL_GetPurcaOrderSparePartDetailsModel_GetModel()
		{
			 var resultGet = _PurchaseOrderDetailBL.GetPurcaOrderSparePartDetailsModel(UserManager.UserInfo, 1);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.PoDetSeqNo > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void PurchaseOrderDetailBL_GetTotalPrice_GetModel()
		{
			 var resultGet = _PurchaseOrderDetailBL.GetTotalPrice("102");
			
			Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void PurchaseOrderDetailBL_ListPurchaseOrderDetails_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PurchaseOrderDetailViewModel();
			model.PurchaseOrderDetailSeqNo= 1; 
			model.PurchaseOrderNumber= 1; 
			model.UnitName= guid; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.StatusName= guid; 
			model.SAPOfferNo= guid; 
			model.SAPRowNo= guid; 
			model.DenyReason= guid; 
			model.RowNumber= guid; 
			model.PartCode = "M.162127"; 
			model.OrderNumber= guid; 
			model.SupplierName= guid; 
			model.PoTypeName= guid; 
			model.StockTypeName= guid; 
			model.VehicleName= guid; 
			model.CurrencyCode= guid; 
			model.IsCampaignPO= true; 
			model.SAPShipIdPart= 1; 
			model.StockServiceValue= guid; 
			model.SpecialExplanation= guid; 
			model.MstSupplierIdDealer= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PurchaseOrderDetailBL.DMLPurchaseOrderDetail(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new PurchaseOrderDetailListModel();
			filter.PartId = 39399; 
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			filter.SupplierDealerConfirm = 1; 
			filter.SupplierId = 782; 
			filter.CurrencyCode = guid; 
			
			 var resultGet = _PurchaseOrderDetailBL.ListPurchaseOrderDetails(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void PurchaseOrderDetailBL_DMLPurchaseOrderDetail_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PurchaseOrderDetailViewModel();
			model.PurchaseOrderDetailSeqNo= 1; 
			model.PurchaseOrderNumber= 1; 
			model.UnitName= guid; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.StatusName= guid; 
			model.SAPOfferNo= guid; 
			model.SAPRowNo= guid; 
			model.DenyReason= guid; 
			model.RowNumber= guid; 
			model.PartCode = "M.162127"; 
			model.OrderNumber= guid; 
			model.SupplierName= guid; 
			model.PoTypeName= guid; 
			model.StockTypeName= guid; 
			model.VehicleName= guid; 
			model.CurrencyCode= guid; 
			model.IsCampaignPO= true; 
			model.SAPShipIdPart= 1; 
			model.StockServiceValue= guid; 
			model.SpecialExplanation= guid; 
			model.MstSupplierIdDealer= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PurchaseOrderDetailBL.DMLPurchaseOrderDetail(UserManager.UserInfo, model);
			
			var filter = new PurchaseOrderDetailListModel();
			filter.PartId = 39399; 
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			filter.SupplierDealerConfirm = 1; 
			filter.SupplierId = 782; 
			filter.CurrencyCode = guid; 
			
			int count = 0;
			 var resultGet = _PurchaseOrderDetailBL.ListPurchaseOrderDetails(UserManager.UserInfo, filter, out count);
			
			var modelUpdate = new PurchaseOrderDetailViewModel();
			modelUpdate.UnitName= guid; 
			modelUpdate.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			modelUpdate.StatusName= guid; 
			modelUpdate.SAPOfferNo= guid; 
			modelUpdate.SAPRowNo= guid; 
			modelUpdate.DenyReason= guid; 
			modelUpdate.RowNumber= guid; 
			modelUpdate.PartCode = "M.162127"; 
			modelUpdate.OrderNumber= guid; 
			modelUpdate.SupplierName= guid; 
			modelUpdate.PoTypeName= guid; 
			modelUpdate.StockTypeName= guid; 
			modelUpdate.VehicleName= guid; 
			modelUpdate.CurrencyCode= guid; 
			
			
			modelUpdate.StockServiceValue= guid; 
			modelUpdate.SpecialExplanation= guid; 
			
			
			
			
			modelUpdate.CommandType = "U";
			 var resultUpdate = _PurchaseOrderDetailBL.DMLPurchaseOrderDetail(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void PurchaseOrderDetailBL_DMLPurchaseOrderDetail_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PurchaseOrderDetailViewModel();
			model.PurchaseOrderDetailSeqNo= 1; 
			model.PurchaseOrderNumber= 1; 
			model.UnitName= guid; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.StatusName= guid; 
			model.SAPOfferNo= guid; 
			model.SAPRowNo= guid; 
			model.DenyReason= guid; 
			model.RowNumber= guid; 
			model.PartCode = "M.162127"; 
			model.OrderNumber= guid; 
			model.SupplierName= guid; 
			model.PoTypeName= guid; 
			model.StockTypeName= guid; 
			model.VehicleName= guid; 
			model.CurrencyCode= guid; 
			model.IsCampaignPO= true; 
			model.SAPShipIdPart= 1; 
			model.StockServiceValue= guid; 
			model.SpecialExplanation= guid; 
			model.MstSupplierIdDealer= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PurchaseOrderDetailBL.DMLPurchaseOrderDetail(UserManager.UserInfo, model);
			
			var filter = new PurchaseOrderDetailListModel();
			filter.PartId = 39399; 
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			filter.SupplierDealerConfirm = 1; 
			filter.SupplierId = 782; 
			filter.CurrencyCode = guid; 
			
			int count = 0;
			 var resultGet = _PurchaseOrderDetailBL.ListPurchaseOrderDetails(UserManager.UserInfo, filter, out count);
			
			var modelDelete = new PurchaseOrderDetailViewModel();
			
			modelDelete.UnitName= guid; 
			modelDelete.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			modelDelete.StatusName= guid; 
			modelDelete.SAPOfferNo= guid; 
			modelDelete.SAPRowNo= guid; 
			modelDelete.DenyReason= guid; 
			modelDelete.RowNumber= guid; 
			modelDelete.PartCode = "M.162127"; 
			modelDelete.OrderNumber= guid; 
			modelDelete.SupplierName= guid; 
			modelDelete.PoTypeName= guid; 
			modelDelete.StockTypeName= guid; 
			modelDelete.VehicleName= guid; 
			modelDelete.CurrencyCode= guid; 
			
			
			modelDelete.StockServiceValue= guid; 
			modelDelete.SpecialExplanation= guid; 
			
			
			
			
			modelDelete.CommandType = "D";
			 var resultDelete = _PurchaseOrderDetailBL.DMLPurchaseOrderDetail(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}


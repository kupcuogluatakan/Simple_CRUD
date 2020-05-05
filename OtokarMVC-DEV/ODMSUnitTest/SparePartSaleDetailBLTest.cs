using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.SparePartSaleDetail;
using ODMSCommon.Security;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class SparePartSaleDetailBLTest
	{

		SparePartSaleDetailBL _SparePartSaleDetailBL = new SparePartSaleDetailBL();

		[TestMethod]
		public void SparePartSaleDetailBL_DMLSparePartSaleDetail_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartSaleDetailDetailModel();
			model.PartSaleId= 1; 
			model.SparePartSaleDetailId= 1; 
			model.SparePartCode= guid; 
			model.SparePartName= guid; 
			model.CurrencyCode= guid; 
			model.VatRatio= 1; 
			model.StatusName= guid; 
			model.ReturnReasonText= guid; 
			model.DeliverySeqNo= 1; 
			model.IsPriceFixed= true; 
			model.PickedQuantity= 1; 
			model.TotalListPrice= 1; 
			model.TotalDiscountPrice= 1; 
			model.TotalPriceWithoutVatRatio= 1; 
			model.TotalPriceWithVatRatio= 1; 
			model.DealerCurrencyCode= guid; 
			model.SoDetSeqNo= guid; 
			model.StockQuantity= 1; 
			model.PoOrderNo= 1; 
			model.PoOrderLine= 1; 
			model.VatExcluded= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SparePartSaleDetailBL.DMLSparePartSaleDetail(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void SparePartSaleDetailBL_DMLOtokarSparePartSaleDetail_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new OSparePartSaleDetailViewModel();
			model.SparePartSaleDetailId= 1; 
			model.SparePartSaleId= 1; 
			model.PartNameCode= guid; 
			model.PartId= 1; 
			model.ReturnReasonText= guid; 
			model.StockTypeId= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SparePartSaleDetailBL.DMLOtokarSparePartSaleDetail(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void SparePartSaleDetailBL_GetSparePartSaleDetail_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new OSparePartSaleDetailViewModel();
			model.SparePartSaleDetailId= 1; 
			model.SparePartSaleId= 1; 
			model.PartNameCode= guid; 
			model.PartId= 1; 
			model.ReturnReasonText= guid; 
			model.StockTypeId= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SparePartSaleDetailBL.DMLOtokarSparePartSaleDetail(UserManager.UserInfo, model);
			
			var filter = new SparePartSaleDetailDetailModel();
			filter.SparePartId = result.Model.PartId;
			filter.SparePartCode = guid; 
			filter.CurrencyCode = guid; 
			filter.DealerCurrencyCode = guid; 
			
			 var resultGet = _SparePartSaleDetailBL.GetSparePartSaleDetail(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.PartSaleId > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void SparePartSaleDetailBL_GetOtokarSparePartSaleDetail_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new OSparePartSaleDetailViewModel();
			model.SparePartSaleDetailId= 1; 
			model.SparePartSaleId= 1; 
			model.PartNameCode= guid; 
			model.PartId= 1; 
			model.ReturnReasonText= guid; 
			model.StockTypeId= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SparePartSaleDetailBL.DMLOtokarSparePartSaleDetail(UserManager.UserInfo, model);
			
			var filter = new OSparePartSaleDetailViewModel();
			filter.PartId = result.Model.PartId;
			filter.PartNameCode = guid; 
			filter.PartId = 39399; 
			
			 var resultGet = _SparePartSaleDetailBL.GetOtokarSparePartSaleDetail(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.PartId > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void SparePartSaleDetailBL_ListSparePartSaleDetails_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new OSparePartSaleDetailViewModel();
			model.SparePartSaleDetailId= 1; 
			model.SparePartSaleId= 1; 
			model.PartNameCode= guid; 
			model.PartId= 1; 
			model.ReturnReasonText= guid; 
			model.StockTypeId= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SparePartSaleDetailBL.DMLOtokarSparePartSaleDetail(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new SparePartSaleDetailListModel();
			filter.SparePartCode = guid; 
			filter.CurrencyCode = guid; 
			
			 var resultGet = _SparePartSaleDetailBL.ListSparePartSaleDetails(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void SparePartSaleDetailBL_ListOtokarSparePartSaleDetail_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new OSparePartSaleDetailViewModel();
			model.SparePartSaleDetailId= 1; 
			model.SparePartSaleId= 1; 
			model.PartNameCode= guid; 
			model.PartId= 1; 
			model.ReturnReasonText= guid; 
			model.StockTypeId= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SparePartSaleDetailBL.DMLOtokarSparePartSaleDetail(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new OSparePartSaleDetailListModel();
			filter.PartId = 39399; 
			filter.PartsNameCode = guid; 
			
			 var resultGet = _SparePartSaleDetailBL.ListOtokarSparePartSaleDetail(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
        
		[TestMethod]
		public void SparePartSaleDetailBL_ListOtokarSparePartDelivery_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new OSparePartSaleDetailViewModel();
			model.SparePartSaleDetailId= 1; 
			model.SparePartSaleId= 1; 
			model.PartNameCode= guid; 
			model.PartId= 1; 
			model.ReturnReasonText= guid; 
			model.StockTypeId= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SparePartSaleDetailBL.DMLOtokarSparePartSaleDetail(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new OSparePartSaleDevlieryListModel();
			filter.PartId = 39399; 
			
			 var resultGet = _SparePartSaleDetailBL.ListOtokarSparePartDelivery(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
        [TestMethod]
		public void SparePartSaleDetailBL_DMLOtokarSparePartSaleDetail_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new OSparePartSaleDetailViewModel();
			model.SparePartSaleDetailId= 1; 
			model.SparePartSaleId= 1; 
			model.PartNameCode= guid; 
			model.PartId= 1; 
			model.ReturnReasonText= guid; 
			model.StockTypeId= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SparePartSaleDetailBL.DMLOtokarSparePartSaleDetail(UserManager.UserInfo, model);
			
			var filter = new OSparePartSaleDevlieryListModel();
			filter.PartId = 39399; 
			
			int count = 0;
			 var resultGet = _SparePartSaleDetailBL.ListOtokarSparePartDelivery(UserManager.UserInfo, filter, out count);
			
			var modelDelete = new OSparePartSaleDetailViewModel();
			modelDelete.PartId = resultGet.Data.First().PartId;
			
			
			modelDelete.PartNameCode= guid; 
			
			modelDelete.ReturnReasonText= guid; 
			
			
			
			
			modelDelete.CommandType = "D";
			 var resultDelete = _SparePartSaleDetailBL.DMLOtokarSparePartSaleDetail(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}


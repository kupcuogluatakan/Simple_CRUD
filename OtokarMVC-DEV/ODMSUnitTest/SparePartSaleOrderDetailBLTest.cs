using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.SparePartSaleOrderDetail;
using ODMSCommon.Security;
using System;
using ODMSBusiness.Business;


namespace ODMSUnitTest
{

	[TestClass]
	public class SparePartSaleOrderDetailBLTest
	{

		SparePartSaleOrderDetailBL _SparePartSaleOrderDetailBL = new SparePartSaleOrderDetailBL();

		[TestMethod]
		public void SparePartSaleOrderDetailBL_DMLSparePartSaleOrderDetail_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartSaleOrderDetailDetailModel();
			model.SoNumber= guid; 
			model.SparePartSaleOrderDetailId= 1; 
			model.SparePartId= 1; 
			model.SparePartCode= guid; 
			model.SparePartName= guid; 
			model.PoDetailSequenceNo= guid; 
			model.StatusName= guid; 
			model.SpecialExplanation= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SparePartSaleOrderDetailBL.DMLSparePartSaleOrderDetail(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void SparePartSaleOrderDetailBL_GetSparePartSaleOrderDetail_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartSaleOrderDetailDetailModel();
			model.SoNumber= guid; 
			model.SparePartSaleOrderDetailId= 1; 
			model.SparePartId= 1; 
			model.SparePartCode= guid; 
			model.SparePartName= guid; 
			model.PoDetailSequenceNo= guid; 
			model.StatusName= guid; 
			model.SpecialExplanation= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SparePartSaleOrderDetailBL.DMLSparePartSaleOrderDetail(UserManager.UserInfo, model);
			
			var filter = new SparePartSaleOrderDetailDetailModel();
			filter.SparePartSaleOrderDetailId = result.Model.SparePartSaleOrderDetailId;
			filter.SparePartCode = guid; 
			
			 var resultGet = _SparePartSaleOrderDetailBL.GetSparePartSaleOrderDetail(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.SparePartSaleOrderDetailId > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void SparePartSaleOrderDetailBL_ListSparePartSaleOrderDetails_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartSaleOrderDetailDetailModel();
			model.SoNumber= guid; 
			model.SparePartSaleOrderDetailId= 1; 
			model.SparePartId= 1; 
			model.SparePartCode= guid; 
			model.SparePartName= guid; 
			model.PoDetailSequenceNo= guid; 
			model.StatusName= guid; 
			model.SpecialExplanation= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SparePartSaleOrderDetailBL.DMLSparePartSaleOrderDetail(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new SparePartSaleOrderDetailListModel();
			filter.SparePartCode = guid; 
			filter.CurrencyCode = guid; 
			
			 var resultGet = _SparePartSaleOrderDetailBL.ListSparePartSaleOrderDetails(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void SparePartSaleOrderDetailBL_DMLSparePartSaleOrderDetail_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartSaleOrderDetailDetailModel();
			model.SoNumber= guid; 
			model.SparePartSaleOrderDetailId= 1; 
			model.SparePartId= 1; 
			model.SparePartCode= guid; 
			model.SparePartName= guid; 
			model.PoDetailSequenceNo= guid; 
			model.StatusName= guid; 
			model.SpecialExplanation= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SparePartSaleOrderDetailBL.DMLSparePartSaleOrderDetail(UserManager.UserInfo, model);
			
			var filter = new SparePartSaleOrderDetailListModel();
			filter.SparePartCode = guid; 
			filter.CurrencyCode = guid; 
			
			int count = 0;
			 var resultGet = _SparePartSaleOrderDetailBL.ListSparePartSaleOrderDetails(UserManager.UserInfo, filter, out count);
			
			var modelUpdate = new SparePartSaleOrderDetailDetailModel();
			modelUpdate.SparePartSaleOrderDetailId = resultGet.Data.First().SparePartSaleOrderDetailId;
			modelUpdate.SoNumber= guid; 
			
			
			modelUpdate.SparePartCode= guid; 
			modelUpdate.SparePartName= guid; 
			modelUpdate.PoDetailSequenceNo= guid; 
			modelUpdate.StatusName= guid; 
			modelUpdate.SpecialExplanation= guid; 
			
			
			
			modelUpdate.CommandType = "U";
			 var resultUpdate = _SparePartSaleOrderDetailBL.DMLSparePartSaleOrderDetail(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}
        [TestMethod]
		public void SparePartSaleOrderDetailBL_ListSparePartSaleOrderDetailsForSaleOrderDetails_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartSaleOrderDetailDetailModel();
			model.SoNumber= guid; 
			model.SparePartSaleOrderDetailId= 1; 
			model.SparePartId= 1; 
			model.SparePartCode= guid; 
			model.SparePartName= guid; 
			model.PoDetailSequenceNo= guid; 
			model.StatusName= guid; 
			model.SpecialExplanation= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SparePartSaleOrderDetailBL.DMLSparePartSaleOrderDetail(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new SparePartSaleOrderDetailListModel();
			filter.SparePartCode = guid; 
			filter.CurrencyCode = guid; 
			
			 var resultGet = _SparePartSaleOrderDetailBL.ListSparePartSaleOrderDetailsForSaleOrderDetails(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
        
		[TestMethod]
		public void SparePartSaleOrderDetailBL_DMLSparePartSaleOrderDetail_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartSaleOrderDetailDetailModel();
			model.SoNumber= guid; 
			model.SparePartSaleOrderDetailId= 1; 
			model.SparePartId= 1; 
			model.SparePartCode= guid; 
			model.SparePartName= guid; 
			model.PoDetailSequenceNo= guid; 
			model.StatusName= guid; 
			model.SpecialExplanation= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SparePartSaleOrderDetailBL.DMLSparePartSaleOrderDetail(UserManager.UserInfo, model);
			
			var filter = new SparePartSaleOrderDetailListModel();
			filter.SparePartCode = guid; 
			filter.CurrencyCode = guid; 
			
			int count = 0;
			 var resultGet = _SparePartSaleOrderDetailBL.ListSparePartSaleOrderDetailsForSaleOrderDetails(UserManager.UserInfo, filter, out count);
			
			var modelDelete = new SparePartSaleOrderDetailDetailModel();
			modelDelete.SparePartSaleOrderDetailId = resultGet.Data.First().SparePartSaleOrderDetailId;
			modelDelete.SoNumber= guid; 
			modelDelete.SparePartCode= guid; 
			modelDelete.SparePartName= guid; 
			modelDelete.PoDetailSequenceNo= guid; 
			modelDelete.StatusName= guid; 
			modelDelete.SpecialExplanation= guid; 
			modelDelete.CommandType = "D";
			 var resultDelete = _SparePartSaleOrderDetailBL.DMLSparePartSaleOrderDetail(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}


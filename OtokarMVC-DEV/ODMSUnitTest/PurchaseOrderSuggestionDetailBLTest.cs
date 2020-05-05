using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using System;
using ODMSModel.PurchaseOrderSuggestionDetail;


namespace ODMSUnitTest
{

	[TestClass]
	public class PurchaseOrderSuggestionDetailBLTest
	{

		PurchaseOrderSuggestionDetailBL _PurchaseOrderSuggestionDetailBL = new PurchaseOrderSuggestionDetailBL();

		[TestMethod]
		public void PurchaseOrderSuggestionDetailBL_UpdatePurchaseOrderSuggestionDetail_Insert()
		{
            var result = _PurchaseOrderSuggestionDetailBL.UpdatePurchaseOrderSuggestionDetail(UserManager.UserInfo, 1);
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void PurchaseOrderSuggestionDetailBL_GetInitialInfoSuggestionDetail_GetModel()
        { 
			 var result = _PurchaseOrderSuggestionDetailBL.UpdatePurchaseOrderSuggestionDetail(UserManager.UserInfo, 1);
			
			var filter = new POSuggestionDetailViewModel();
			
			 var resultGet = _PurchaseOrderSuggestionDetailBL.GetInitialInfoSuggestionDetail(filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.MrpId > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void PurchaseOrderSuggestionDetailBL_ListPOSuggestionDetail_GetAll()
		{
			 var result = _PurchaseOrderSuggestionDetailBL.UpdatePurchaseOrderSuggestionDetail(UserManager.UserInfo, 1);
			
			int count = 0;
			var filter = new POSuggestionDetailListModel();
			filter.PartId = 39399; 
			
			 var resultGet = _PurchaseOrderSuggestionDetailBL.ListPOSuggestionDetail(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void PurchaseOrderSuggestionDetailBL_UpdatePurchaseOrderSuggestionDetail_Update()
		{
			 var result = _PurchaseOrderSuggestionDetailBL.UpdatePurchaseOrderSuggestionDetail(UserManager.UserInfo, 1);
			
			var filter = new POSuggestionDetailListModel();
			filter.PartId = 39399; 
			
			int count = 0;
			 var resultGet = _PurchaseOrderSuggestionDetailBL.ListPOSuggestionDetail(UserManager.UserInfo, filter, out count);
			 var resultUpdate = _PurchaseOrderSuggestionDetailBL.UpdatePurchaseOrderSuggestionDetail(UserManager.UserInfo, 1);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void PurchaseOrderSuggestionDetailBL_UpdatePurchaseOrderSuggestionDetail_Delete()
		{
			 var result = _PurchaseOrderSuggestionDetailBL.UpdatePurchaseOrderSuggestionDetail(UserManager.UserInfo, 1);
			
			var filter = new POSuggestionDetailListModel();
			filter.PartId = 39399; 
			
			int count = 0;
			 var resultGet = _PurchaseOrderSuggestionDetailBL.ListPOSuggestionDetail(UserManager.UserInfo, filter, out count);
			 var resultDelete = _PurchaseOrderSuggestionDetailBL.UpdatePurchaseOrderSuggestionDetail(UserManager.UserInfo, 1);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}


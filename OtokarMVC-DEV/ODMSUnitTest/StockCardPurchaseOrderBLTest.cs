using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.StockCardPurchaseOrder;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class StockCardPurchaseOrderBLTest
	{

		StockCardPurchaseOrderBL _StockCardPurchaseOrderBL = new StockCardPurchaseOrderBL();

		[TestMethod]
		public void StockCardPurchaseOrderBL_ListStockCardPurchaseOrder_GetAll()
		{
			
			int count = 0;
			string errorDesc = string.Empty; ;
			int errorCode = 0;
			var filter = new StockCardPurchaseOrderListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.PartId = 39399; 
			
			 var resultGet = _StockCardPurchaseOrderBL.ListStockCardPurchaseOrder(UserManager.UserInfo, filter, out count, out errorCode, out errorDesc);
			
			Assert.IsTrue(resultGet.Total > 0);
		}


	}

}


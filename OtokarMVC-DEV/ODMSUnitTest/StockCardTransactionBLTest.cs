using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.StockCardTransaction;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class StockCardTransactionBLTest
	{

		StockCardTransactionBL _StockCardTransactionBL = new StockCardTransactionBL();

		[TestMethod]
		public void StockCardTransactionBL_ListStockCardTransaction_GetAll()
		{
			
			int count = 0;
			var filter = new StockCardTransactionListModel();
			filter.PartId = 39399; 
			filter.DealerId = UserManager.UserInfo.DealerID; 
			
			 var resultGet = _StockCardTransactionBL.ListStockCardTransaction(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}


	}

}


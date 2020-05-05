using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.PurchaseOrderSuggestion;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class PurchaseOrderSuggestionBLTest
	{

		PurchaseOrderSuggestionBL _PurchaseOrderSuggestionBL = new PurchaseOrderSuggestionBL();

		[TestMethod]
		public void PurchaseOrderSuggestionBL_ListPurchaseOrderSuggestion_GetAll()
		{
			
			int count = 0;
			var filter = new PurchaseOrderSuggestionListModel();
			
			 var resultGet = _PurchaseOrderSuggestionBL.ListPurchaseOrderSuggestion(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}


	}

}


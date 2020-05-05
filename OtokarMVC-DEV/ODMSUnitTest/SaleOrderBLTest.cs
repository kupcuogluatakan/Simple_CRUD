using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using System;
using ODMSBusiness.Business;
using ODMSModel.SaleOrder;


namespace ODMSUnitTest
{

	[TestClass]
	public class SaleOrderBLTest
	{

		SaleOrderBL _SaleOrderBL = new SaleOrderBL();

		[TestMethod]
		public void SaleOrderBL_ListSaleOrderCustomers_GetAll()
		{
			 var resultGet = _SaleOrderBL.ListSaleOrderCustomers(UserManager.UserInfo, null);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void SaleOrderBL_ListPurchaseOrderTypes_GetAll()
		{
			 var resultGet = _SaleOrderBL.ListPurchaseOrderTypes(UserManager.UserInfo);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void SaleOrderBL_ListSaleOrderRemaining_GetAll()
		{
			
			int count = 0;
			var filter = new SaleOrderRemainingFilter();
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			
			 var resultGet = _SaleOrderBL.ListSaleOrderRemaining(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void SaleOrderBL_ListSelectedSaleOrderPartsStockQuants_GetAll()
		{
			 var resultGet = _SaleOrderBL.ListSelectedSaleOrderPartsStockQuants("154713");
			
			Assert.IsTrue(resultGet.Total > 0);
		}


	}

}


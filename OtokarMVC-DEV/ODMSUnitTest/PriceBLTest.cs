using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.Price;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class PriceBLTest
	{

		PriceBL _PriceBL = new PriceBL();

		[TestMethod]
		public void PriceBL_ListPrice_GetAll()
		{
			
			int count = 0;
			var filter = new PriceListModel();
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			filter.PartId = 39399; 
			filter.PriceListId = 14; 
			
			 var resultGet = _PriceBL.ListPrice(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void PriceBL_PriceListCombo_GetAll()
		{
			 var resultGet = _PriceBL.PriceListCombo(UserManager.UserInfo);
			
			Assert.IsTrue(resultGet.Total > 0);
		}


	}

}


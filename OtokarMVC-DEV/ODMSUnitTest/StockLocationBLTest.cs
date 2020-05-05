using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.StockLocation;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class StockLocationBLTest
	{

		StockLocationBL _StockLocationBL = new StockLocationBL();

		[TestMethod]
		public void StockLocationBL_ListStockLocation_GetAll()
		{
		    var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            int count = 0;
			var filter = new StockLocationListModel();
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			filter.WarehouseCode = guid; 
			filter.RackCode = guid; 
			
			 var resultGet = _StockLocationBL.ListStockLocation(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}


	}

}


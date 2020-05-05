using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using System;
using ODMSCommon.Security;
using ODMSModel.StockTypeDetail;
using ODMSModel.CycleCountResult;


namespace ODMSUnitTest
{

	[TestClass]
	public class StockTypeDetailBLTest
	{

		StockTypeDetailBL _StockTypeDetailBL = new StockTypeDetailBL();

		[TestMethod]
		public void StockTypeDetailBL_GetStockTypeDetailTotalQuantity_GetModel()
		{
			 var resultGet = _StockTypeDetailBL.GetStockTypeDetailTotalQuantity(1,1465);
			
			Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void StockTypeDetailBL_ListStockTypeDetail_GetAll()
		{
			
			int count = 0;
			var filter = new StockTypeDetailListModel();
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			filter.PartCode = "M.162127"; 
			
			 var resultGet = _StockTypeDetailBL.ListStockTypeDetail(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void StockTypeDetailBL_ListStokTypeAudit_GetAll()
		{
			
			int count = 0;
			var filter = new CycleCountResultAuditViewModel();
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			filter.PartId = 39399; 
			
			 var resultGet = _StockTypeDetailBL.ListStokTypeAudit(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Total > 0);
		}


	}

}


using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.StockCardChangePart;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class StockCardChangePartBLTest
	{

		StockCardChangePartBL _StockCardChangePartBL = new StockCardChangePartBL();

		[TestMethod]
		public void StockCardChangePartBL_ListStockCardChangePart_GetAll()
		{
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            int count = 0;
			var filter = new StockCardChangePartListModel();
			filter.PartId = 39399; 
			filter.FirstPartCode = guid; 
			filter.LastPartCode = guid; 
			
			 var resultGet = _StockCardChangePartBL.ListStockCardChangePart(filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}


	}

}


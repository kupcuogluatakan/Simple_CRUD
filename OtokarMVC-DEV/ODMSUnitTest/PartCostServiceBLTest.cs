using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using System;
using ODMSBusiness.Business;


namespace ODMSUnitTest
{

	[TestClass]
	public class PartCostServiceBLTest
	{

		PartCostServiceBL _PartCostServiceBL = new PartCostServiceBL();

		[TestMethod]
		public void PartCostServiceBL_GetPart_GetModel()
		{
			 var resultGet = _PartCostServiceBL.GetPart(10329, 1);
			
			Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
		}


	}

}


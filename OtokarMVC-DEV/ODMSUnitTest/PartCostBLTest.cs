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
	public class PartCostBLTest
	{

		PartCostBL _PartCostBL = new PartCostBL();

		[TestMethod]
		public void PartCostBL_GetGuaranteeDetPart_GetModel()
		{
		    int totalCnt = 0;
			 var resultGet = _PartCostBL.GetGuaranteeDetPart(10329,totalCnt);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.PartCode!=String.Empty && resultGet.IsSuccess);
		}


	}

}


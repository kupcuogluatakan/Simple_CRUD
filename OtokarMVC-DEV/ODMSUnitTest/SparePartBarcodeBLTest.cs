using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class SparePartBarcodeBLTest
	{

		SparePartBarcodeBL _SparePartBarcodeBL = new SparePartBarcodeBL();

		[TestMethod]
		public void SparePartBarcodeBL_List_GetAll()
		{
			 var resultGet = _SparePartBarcodeBL.List(UserManager.UserInfo, 1,true);
			
			Assert.IsTrue(resultGet.Total > 0);
		}


	}

}


using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;


namespace ODMSUnitTest
{

	[TestClass]
	public class SystemAdministrationBLTest
	{

		SystemAdministrationBL _SystemAdministrationBL = new SystemAdministrationBL();

		[TestMethod]
		public void SystemAdministrationBL_UpdateSessionData_Insert()
		{
			 var result = _SystemAdministrationBL.UpdateSessionData(UserManager.UserInfo);
			
			Assert.IsTrue(result.IsSuccess);
		}


	}

}


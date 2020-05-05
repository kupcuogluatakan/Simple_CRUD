using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class PasswordBLTest
	{

		PasswordBL _PasswordBL = new PasswordBL();

		[TestMethod]
		public void PasswordBL_GetPasswordForUser_GetModel()
		{
			 var resultGet = _PasswordBL.GetPasswordForUser(UserManager.UserInfo.UserId);
			
			Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void PasswordBL_GetLastPasswordChangeDateForUser_GetModel()
		{
			 var resultGet = _PasswordBL.GetLastPasswordChangeDateForUser(UserManager.UserInfo.UserId);
			
			Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
		}


	}

}


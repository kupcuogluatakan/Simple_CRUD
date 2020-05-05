using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.ClaimWaybillList;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class ClaimWaybillListBLTest
	{

		ClaimWaybillListBL _ClaimWaybillListBL = new ClaimWaybillListBL();

		[TestMethod]
		public void ClaimWaybillListBL_ListClaimWaybillList_GetAll()
		{
			
			int count = 0;
			var filter = new ClaimWaybillListListModel();
			
			 var resultGet = _ClaimWaybillListBL.ListClaimWaybillList(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}


	}

}


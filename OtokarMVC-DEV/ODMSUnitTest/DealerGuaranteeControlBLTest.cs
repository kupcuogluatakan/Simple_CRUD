using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.DealerGuaranteeControl;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class DealerGuaranteeControlBLTest
	{

		DealerGuaranteeControlBL _DealerGuaranteeControlBL = new DealerGuaranteeControlBL();

		[TestMethod]
		public void DealerGuaranteeControlBL_ListGuaranteeRequests_GetAll()
		{
			
			int count = 0;
			var filter = new DealerGuaranteeControlListModel();
			filter.VehicleId = 29627; 
			filter.DealerId = UserManager.UserInfo.DealerID; 
			
			 var resultGet = _DealerGuaranteeControlBL.ListGuaranteeRequests(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}


	}

}


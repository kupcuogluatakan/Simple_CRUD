using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.DealerFleetVehicle;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class DealerFleetVehicleBLTest
	{

		DealerFleetVehicleBL _DealerFleetVehicleBL = new DealerFleetVehicleBL();

		[TestMethod]
		public void DealerFleetVehicleBL_ListDealerFleetVehicle_GetAll()
		{
			
			int count = 0;
			var filter = new DealerFleetVehicleListModel();
			filter.VehicleId = 29627; 
			filter.DealerId = UserManager.UserInfo.DealerID; 
			
			 var resultGet = _DealerFleetVehicleBL.ListDealerFleetVehicle(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}


	}

}


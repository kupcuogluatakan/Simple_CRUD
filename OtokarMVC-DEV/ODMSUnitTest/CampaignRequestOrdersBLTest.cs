using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.CampaignRequestOrders;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class CampaignRequestOrdersBLTest
	{

		CampaignRequestOrdersBL _CampaignRequestOrdersBL = new CampaignRequestOrdersBL();

		[TestMethod]
		public void CampaignRequestOrdersBL_ListCampaignRequestOrders_GetAll()
		{
			
			int count = 0;
			var filter = new CampaignRequestOrdersListModel();
			filter.CampaignCode = "508"; 
			
			 var resultGet = _CampaignRequestOrdersBL.ListCampaignRequestOrders(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0); 
		}


	}

}


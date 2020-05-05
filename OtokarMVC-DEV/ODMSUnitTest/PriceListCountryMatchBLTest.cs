using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.PriceListCountryMatch;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class PriceListCountryMatchBLTest
	{

		PriceListCountryMatchBL _PriceListCountryMatchBL = new PriceListCountryMatchBL();

		[TestMethod]
		public void PriceListCountryMatchBL_Save_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PriceListCountryMatchSaveModel();
			model.PriceListId = 14; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PriceListCountryMatchBL.Save(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void PriceListCountryMatchBL_GetCountriesIncluded_GetModel()
		{
			 var resultGet = _PriceListCountryMatchBL.GetCountriesIncluded(UserManager.UserInfo, 14);
			
			Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void PriceListCountryMatchBL_GetCountriesExcluded_GetModel()
		{
			 var resultGet = _PriceListCountryMatchBL.GetCountriesExcluded(UserManager.UserInfo, 14);
			
			Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
		}


	}

}


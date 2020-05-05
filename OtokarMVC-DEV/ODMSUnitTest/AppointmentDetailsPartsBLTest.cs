using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.AppointmentDetailsParts;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class AppointmentDetailsPartsBLTest
	{

		AppointmentDetailsPartsBL _AppointmentDetailsPartsBL = new AppointmentDetailsPartsBL();

		[TestMethod]
		public void AppointmentDetailsPartsBL_DMLAppointmentDetailsParts_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new AppointmentDetailsPartsViewModel();
			model.AppointIndicId= 1; 
			model.IndicType= guid; 
			model.PartId=39399; 
			model.SelectedPartId= 1; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.Quantity= 1; 
			model.ListPrice= guid; 
			model.GroupList= guid; 
			model.CurrencyCode= guid; 
			model.MainCat= guid; 
			model.Cat= guid; 
			model.SubCat= guid; 
			model.txtPartId= guid; 
			model.ProposalSeq= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _AppointmentDetailsPartsBL.DMLAppointmentDetailsParts(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void AppointmentDetailsPartsBL_GetAppointmentDP_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new AppointmentDetailsPartsViewModel();
			model.AppointIndicId= 1; 
			model.IndicType= guid; 
			model.PartId=39399; 
			model.SelectedPartId= 1; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.Quantity= 1; 
			model.ListPrice= guid; 
			model.GroupList= guid; 
			model.CurrencyCode= guid; 
			model.MainCat= guid; 
			model.Cat= guid; 
			model.SubCat= guid; 
			model.txtPartId= guid; 
			model.ProposalSeq= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _AppointmentDetailsPartsBL.DMLAppointmentDetailsParts(UserManager.UserInfo, model);
			
			var filter = new AppointmentDetailsPartsViewModel();
			filter.Id = result.Model.Id;
			filter.PartId = 39399; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			filter.CurrencyCode = guid; 
			
			 var resultGet = _AppointmentDetailsPartsBL.GetAppointmentDP(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.Id > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void AppointmentDetailsPartsBL_GetAppIndicType_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new AppointmentDetailsPartsViewModel();
			model.AppointIndicId= 1; 
			model.IndicType= guid; 
			model.PartId=39399; 
			model.SelectedPartId= 1; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.Quantity= 1; 
			model.ListPrice= guid; 
			model.GroupList= guid; 
			model.CurrencyCode= guid; 
			model.MainCat= guid; 
			model.Cat= guid; 
			model.SubCat= guid; 
			model.txtPartId= guid; 
			model.ProposalSeq= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _AppointmentDetailsPartsBL.DMLAppointmentDetailsParts(UserManager.UserInfo, model);
			
			var filter = new AppointmentDetailsPartsViewModel();
			filter.Id = result.Model.Id;
			filter.PartId = 39399; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			filter.CurrencyCode = guid; 
			
			 var resultGet = _AppointmentDetailsPartsBL.GetAppIndicType(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.Id > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void AppointmentDetailsPartsBL_GetAppointmentDPList_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new AppointmentDetailsPartsViewModel();
			model.AppointIndicId= 1; 
			model.IndicType= guid; 
			model.PartId=39399; 
			model.SelectedPartId= 1; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.Quantity= 1; 
			model.ListPrice= guid; 
			model.GroupList= guid; 
			model.CurrencyCode= guid; 
			model.MainCat= guid; 
			model.Cat= guid; 
			model.SubCat= guid; 
			model.txtPartId= guid; 
			model.ProposalSeq= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _AppointmentDetailsPartsBL.DMLAppointmentDetailsParts(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new AppointmentDetailsPartsListModel();
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			filter.CurrencyCode = guid; 
			
			 var resultGet = _AppointmentDetailsPartsBL.GetAppointmentDPList(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
		
	}

}


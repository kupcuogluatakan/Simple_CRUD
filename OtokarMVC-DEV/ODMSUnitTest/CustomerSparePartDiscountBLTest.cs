using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.CustomerSparePartDiscount;


namespace ODMSUnitTest
{

	[TestClass]
	public class CustomerSparePartDiscountBLTest
	{

		CustomerSparePartDiscountBL _CustomerSparePartDiscountBL = new CustomerSparePartDiscountBL();

		[TestMethod]
		public void CustomerSparePartDiscountBL_DMLCustomerSparePartDiscount_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new CustomerSparePartDiscountViewModel();
			model.CustomerSparePartDiscountId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.CustomerId= 1; 
			model.PartId=39399; 
			model.DiscountRatio= 1; 
			model.DealerName= guid; 
			model.CustomerName= guid; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.OrgTypeName= guid; 
			model.SparePartClassCode= guid; 
			model.IsApplicableToWorkOrderName= guid; 
			model.IsApplicableToWorkOrder= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _CustomerSparePartDiscountBL.DMLCustomerSparePartDiscount(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void CustomerSparePartDiscountBL_GetById_GetModel()
		{
			 var resultGet = _CustomerSparePartDiscountBL.GetById(10,"TR");
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.CustomerId > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void CustomerSparePartDiscountBL_List_GetAll()
		{
		    var totalCnt = 0;
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new CustomerSparePartDiscountViewModel();
			model.CustomerSparePartDiscountId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.CustomerId= 1; 
			model.PartId=39399; 
			model.DiscountRatio= 1; 
			model.DealerName= guid; 
			model.CustomerName= guid; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.OrgTypeName= guid; 
			model.SparePartClassCode= guid; 
			model.IsApplicableToWorkOrderName= guid; 
			model.IsApplicableToWorkOrder= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _CustomerSparePartDiscountBL.DMLCustomerSparePartDiscount(UserManager.UserInfo, model);
			
			var filter = new CustomerSparePartDiscountListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.PartId = 39399; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			filter.PartCode = "M.162127"; 
			filter.SparePartClassCode = guid; 
			
			 var resultGet = _CustomerSparePartDiscountBL.List(UserManager.UserInfo, filter,out totalCnt);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void CustomerSparePartDiscountBL_ListDealersForCreate_GetAll()
		{
			 var resultGet = _CustomerSparePartDiscountBL.ListDealersForCreate(UserManager.UserInfo);
			
			Assert.IsTrue(resultGet.Total > 0);
		}


	}

}


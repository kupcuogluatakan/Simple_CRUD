using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.SparePartSaleOrder;
using System;
using ODMSBusiness.Business;


namespace ODMSUnitTest
{

	[TestClass]
	public class SparePartSaleOrderBLTest
	{

		SparePartSaleOrderBL _SparePartSaleOrderBL = new SparePartSaleOrderBL();

		[TestMethod]
		public void SparePartSaleOrderBL_DMLSparePartSaleOrder_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartSaleOrderViewModel();
			model.SparePartSaleId= 1; 
			model.SoNumber= guid; 
			model.CustomerName= guid; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.StatusName= guid; 
			model.SoTypeName= guid; 
			model.StockTypeName= guid; 
			model.PartCode = "M.162127"; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.IsProposal= true; 
			model.IsProposalName= guid; 
			model.IsFixedPrice= true; 
			model.IsFixedPriceName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SparePartSaleOrderBL.DMLSparePartSaleOrder(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void SparePartSaleOrderBL_GetSparePartSaleOrderLatestStatus_GetModel()
		{
			 var resultGet = _SparePartSaleOrderBL.GetSparePartSaleOrderLatestStatus("1");
			
			Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void SparePartSaleOrderBL_ListSparePartSaleOrders_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartSaleOrderViewModel();
			model.SparePartSaleId= 1; 
			model.SoNumber= guid; 
			model.CustomerName= guid; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.StatusName= guid; 
			model.SoTypeName= guid; 
			model.StockTypeName= guid; 
			model.PartCode = "M.162127"; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.IsProposal= true; 
			model.IsProposalName= guid; 
			model.IsFixedPrice= true; 
			model.IsFixedPriceName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SparePartSaleOrderBL.DMLSparePartSaleOrder(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new SparePartSaleOrderListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			
			 var resultGet = _SparePartSaleOrderBL.ListSparePartSaleOrders(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void SparePartSaleOrderBL_DMLSparePartSaleOrder_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartSaleOrderViewModel();
			model.SparePartSaleId= 1; 
			model.SoNumber= guid; 
			model.CustomerName= guid; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.StatusName= guid; 
			model.SoTypeName= guid; 
			model.StockTypeName= guid; 
			model.PartCode = "M.162127"; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.IsProposal= true; 
			model.IsProposalName= guid; 
			model.IsFixedPrice= true; 
			model.IsFixedPriceName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SparePartSaleOrderBL.DMLSparePartSaleOrder(UserManager.UserInfo, model);
			
			var filter = new SparePartSaleOrderListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			
			int count = 0;
			 var resultGet = _SparePartSaleOrderBL.ListSparePartSaleOrders(UserManager.UserInfo, filter, out count);
			
			var modelUpdate = new SparePartSaleOrderViewModel();
			modelUpdate.SoNumber = resultGet.Data.First().SoNumber;
			modelUpdate.SoNumber= guid; 
			modelUpdate.CustomerName= guid; 
			modelUpdate.DealerId = UserManager.UserInfo.DealerID; 
			modelUpdate.DealerName= guid; 
			modelUpdate.StatusName= guid; 
			modelUpdate.SoTypeName= guid; 
			modelUpdate.StockTypeName= guid; 
			modelUpdate.PartCode = "M.162127"; 
			modelUpdate.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			
			modelUpdate.IsProposalName= guid; 
			
			modelUpdate.IsFixedPriceName= guid; 
			
			
			
			modelUpdate.CommandType = "U";
			 var resultUpdate = _SparePartSaleOrderBL.DMLSparePartSaleOrder(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void SparePartSaleOrderBL_DMLSparePartSaleOrder_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartSaleOrderViewModel();
			model.SparePartSaleId= 1; 
			model.SoNumber= guid; 
			model.CustomerName= guid; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.StatusName= guid; 
			model.SoTypeName= guid; 
			model.StockTypeName= guid; 
			model.PartCode = "M.162127"; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.IsProposal= true; 
			model.IsProposalName= guid; 
			model.IsFixedPrice= true; 
			model.IsFixedPriceName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SparePartSaleOrderBL.DMLSparePartSaleOrder(UserManager.UserInfo, model);
			
			var filter = new SparePartSaleOrderListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			
			int count = 0;
			 var resultGet = _SparePartSaleOrderBL.ListSparePartSaleOrders(UserManager.UserInfo, filter, out count);
			
			var modelDelete = new SparePartSaleOrderViewModel();
			modelDelete.SoNumber = resultGet.Data.First().SoNumber;
			modelDelete.SoNumber= guid; 
			modelDelete.CustomerName= guid; 
			modelDelete.DealerId = UserManager.UserInfo.DealerID; 
			modelDelete.DealerName= guid; 
			modelDelete.StatusName= guid; 
			modelDelete.SoTypeName= guid; 
			modelDelete.StockTypeName= guid; 
			modelDelete.PartCode = "M.162127"; 
			modelDelete.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			modelDelete.IsProposalName= guid; 
			modelDelete.IsFixedPriceName= guid; 
			modelDelete.CommandType = "D";
			 var resultDelete = _SparePartSaleOrderBL.DMLSparePartSaleOrder(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}


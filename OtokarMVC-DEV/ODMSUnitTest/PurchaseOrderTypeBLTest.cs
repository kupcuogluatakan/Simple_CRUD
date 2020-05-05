using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.PurchaseOrderType;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class PurchaseOrderTypeBLTest
	{

		PurchaseOrderTypeBL _PurchaseOrderTypeBL = new PurchaseOrderTypeBL();

		[TestMethod]
		public void PurchaseOrderTypeBL_Insert_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PurchaseOrderTypeViewModel();
			model.PurchaseOrderTypeId= 1; 
			model.PurchaseOrderTypeName= guid; 
			model.Description= guid; 
			model.ProposalType= guid; 
			model.DeliveryPriority= 1; 
			model.SalesOrganization= guid; 
			model.OrderReason= guid; 
			model.ItemCategory= guid; 
			model.DistrChan= guid; 
			model.Division= guid; 
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.IsVehicleSelectionMust= true; 
			model.ManuelPriceAllow= true; 
			model.DealerBranchSSID= guid; 
			model.IsCurrencySelectAllow= true; 
			model.IsFirmOrderAllow= true; 
			model.IsDealerOrderAllow= true; 
			model.IsSupplierOrderAllow= true; 
			model.StockTypeName= guid; 
			model.IsSaleOrderAllow= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PurchaseOrderTypeBL.Insert(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}
        
		[TestMethod]
		public void PurchaseOrderTypeBL_Update_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PurchaseOrderTypeViewModel();
			model.PurchaseOrderTypeId= 1; 
			model.PurchaseOrderTypeName= guid; 
			model.Description= guid; 
			model.ProposalType= guid; 
			model.DeliveryPriority= 1; 
			model.SalesOrganization= guid; 
			model.OrderReason= guid; 
			model.ItemCategory= guid; 
			model.DistrChan= guid; 
			model.Division= guid; 
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.IsVehicleSelectionMust= true; 
			model.ManuelPriceAllow= true; 
			model.DealerBranchSSID= guid; 
			model.IsCurrencySelectAllow= true; 
			model.IsFirmOrderAllow= true; 
			model.IsDealerOrderAllow= true; 
			model.IsSupplierOrderAllow= true; 
			model.StockTypeName= guid; 
			model.IsSaleOrderAllow= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PurchaseOrderTypeBL.Update(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void PurchaseOrderTypeBL_Get_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PurchaseOrderTypeViewModel();
			model.PurchaseOrderTypeId= 1; 
			model.PurchaseOrderTypeName= guid; 
			model.Description= guid; 
			model.ProposalType= guid; 
			model.DeliveryPriority= 1; 
			model.SalesOrganization= guid; 
			model.OrderReason= guid; 
			model.ItemCategory= guid; 
			model.DistrChan= guid; 
			model.Division= guid; 
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.IsVehicleSelectionMust= true; 
			model.ManuelPriceAllow= true; 
			model.DealerBranchSSID= guid; 
			model.IsCurrencySelectAllow= true; 
			model.IsFirmOrderAllow= true; 
			model.IsDealerOrderAllow= true; 
			model.IsSupplierOrderAllow= true; 
			model.StockTypeName= guid; 
			model.IsSaleOrderAllow= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PurchaseOrderTypeBL.Update(UserManager.UserInfo, model);
			
			var filter = new PurchaseOrderTypeViewModel();
			filter.MultiLanguageContentAsText = "TR || TEST"; 
			
			 var resultGet = _PurchaseOrderTypeBL.Get(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
		}
        
		[TestMethod]
		public void PurchaseOrderTypeBL_Update_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PurchaseOrderTypeViewModel();
			model.PurchaseOrderTypeId= 1; 
			model.PurchaseOrderTypeName= guid; 
			model.Description= guid; 
			model.ProposalType= guid; 
			model.DeliveryPriority= 1; 
			model.SalesOrganization= guid; 
			model.OrderReason= guid; 
			model.ItemCategory= guid; 
			model.DistrChan= guid; 
			model.Division= guid; 
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.IsVehicleSelectionMust= true; 
			model.ManuelPriceAllow= true; 
			model.DealerBranchSSID= guid; 
			model.IsCurrencySelectAllow= true; 
			model.IsFirmOrderAllow= true; 
			model.IsDealerOrderAllow= true; 
			model.IsSupplierOrderAllow= true; 
			model.StockTypeName= guid; 
			model.IsSaleOrderAllow= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PurchaseOrderTypeBL.Update(UserManager.UserInfo, model);
			
			var filter = new PurchaseOrderTypeListModel();
			
			int count = 0;
			 var resultGet = _PurchaseOrderTypeBL.List(UserManager.UserInfo, filter, out count);
			
			var modelDelete = new PurchaseOrderTypeViewModel();
			modelDelete.PurchaseOrderTypeName= guid; 
			modelDelete.Description= guid; 
			modelDelete.ProposalType= guid; 
			modelDelete.SalesOrganization= guid; 
			modelDelete.OrderReason= guid; 
			modelDelete.ItemCategory= guid; 
			modelDelete.DistrChan= guid; 
			modelDelete.Division= guid; 
			modelDelete.MultiLanguageContentAsText = "TR || TEST"; 
			modelDelete.DealerBranchSSID= guid; 
			modelDelete.StockTypeName= guid; 
			modelDelete.CommandType = "D";
			 var resultDelete = _PurchaseOrderTypeBL.Update(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}

		[TestMethod]
		public void PurchaseOrderTypeBL_ListPurchaseOrderTypeAsSelectListItem_GetAll()
		{
			 var resultGet = PurchaseOrderTypeBL.ListPurchaseOrderTypeAsSelectListItem(UserManager.UserInfo, 1,true,true,true,true);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
        
		[TestMethod]
		public void PurchaseOrderTypeBL_PurchaseOrderTypeList_GetAll()
		{
			 var resultGet = _PurchaseOrderTypeBL.PurchaseOrderTypeList(UserManager.UserInfo);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
        
		[TestMethod]
		public void PurchaseOrderTypeBL_List_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PurchaseOrderTypeViewModel();
			model.PurchaseOrderTypeId= 1; 
			model.PurchaseOrderTypeName= guid; 
			model.Description= guid; 
			model.ProposalType= guid; 
			model.DeliveryPriority= 1; 
			model.SalesOrganization= guid; 
			model.OrderReason= guid; 
			model.ItemCategory= guid; 
			model.DistrChan= guid; 
			model.Division= guid; 
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.IsVehicleSelectionMust= true; 
			model.ManuelPriceAllow= true; 
			model.DealerBranchSSID= guid; 
			model.IsCurrencySelectAllow= true; 
			model.IsFirmOrderAllow= true; 
			model.IsDealerOrderAllow= true; 
			model.IsSupplierOrderAllow= true; 
			model.StockTypeName= guid; 
			model.IsSaleOrderAllow= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PurchaseOrderTypeBL.Update(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new PurchaseOrderTypeViewModel();
			filter.MultiLanguageContentAsText = "TR || TEST"; 
			
			 var resultGet = _PurchaseOrderTypeBL.List(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void PurchaseOrderTypeBL_Update_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PurchaseOrderTypeViewModel();
			model.PurchaseOrderTypeId= 1; 
			model.PurchaseOrderTypeName= guid; 
			model.Description= guid; 
			model.ProposalType= guid; 
			model.DeliveryPriority= 1; 
			model.SalesOrganization= guid; 
			model.OrderReason= guid; 
			model.ItemCategory= guid; 
			model.DistrChan= guid; 
			model.Division= guid; 
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.IsVehicleSelectionMust= true; 
			model.ManuelPriceAllow= true; 
			model.DealerBranchSSID= guid; 
			model.IsCurrencySelectAllow= true; 
			model.IsFirmOrderAllow= true; 
			model.IsDealerOrderAllow= true; 
			model.IsSupplierOrderAllow= true; 
			model.StockTypeName= guid; 
			model.IsSaleOrderAllow= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PurchaseOrderTypeBL.Update(UserManager.UserInfo, model);
			
			var filter = new PurchaseOrderTypeViewModel();
			filter.MultiLanguageContentAsText = "TR || TEST"; 
			
			int count = 0;
			 var resultGet = _PurchaseOrderTypeBL.List(UserManager.UserInfo, filter);
			
			var modelUpdate = new PurchaseOrderTypeViewModel();
			modelUpdate.PurchaseOrderTypeName= guid; 
			modelUpdate.Description= guid; 
			modelUpdate.ProposalType= guid; 
			modelUpdate.SalesOrganization= guid; 
			modelUpdate.OrderReason= guid; 
			modelUpdate.ItemCategory= guid; 
			modelUpdate.DistrChan= guid; 
			modelUpdate.Division= guid; 
			modelUpdate.MultiLanguageContentAsText = "TR || TEST"; 
			
			
			modelUpdate.DealerBranchSSID= guid; 
			
			
			
			
			modelUpdate.StockTypeName= guid; 
			
			
			
			
			modelUpdate.CommandType = "U";
			 var resultUpdate = _PurchaseOrderTypeBL.Update(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}
        

	}

}


using System;
using System.Collections.Generic;
using System.Linq;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.Customer;
using ODMSModel.CustomerAddress;
using ODMSModel.Dealer;
using ODMSModel.DealerPurchaseOrder;
using ODMSModel.DealerPurchaseOrderConfirm;
using System.Web.Mvc;
using ODMSModel.PurchaseOrder;
using ODMSModel.PurchaseOrderDetail;
using ODMSModel.SparePartSale;
using ODMSModel.SparePartSaleDetail;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class DealerPurchaseOrderConfirmController : ControllerBase
    {
        #region Member Variables

        private void SetDefaults()
        {
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            List<SelectListItem> statusList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.PurchaseOrderStatus).Data;
            statusList.RemoveAt(0);
            ViewBag.StatusList = statusList;
        }

        #endregion

        #region DealerPurchaseOrderConfirm Index

        [AuthorizationFilter(CommonValues.PermissionCodes.DealerPurchaseOrderConfirm.DealerPurchaseOrderConfirmIndex)]
        [HttpGet]
        public ActionResult DealerPurchaseOrderConfirmIndex()
        {
            SetDefaults();

            DealerPurchaseOrderConfirmListModel model = new DealerPurchaseOrderConfirmListModel();

            if (UserManager.UserInfo.GetUserDealerId() != 0)
                model.IdSupplier = UserManager.UserInfo.GetUserDealerId();

            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.DealerPurchaseOrderConfirm.DealerPurchaseOrderConfirmIndex, CommonValues.PermissionCodes.DealerPurchaseOrderConfirm.DealerPurchaseOrderConfirmIndex)]
        public ActionResult ListDealerPurchaseOrderConfirm([DataSourceRequest] DataSourceRequest request, DealerPurchaseOrderConfirmListModel model)
        {
            var purchaseOrderBo = new DealerPurchaseOrderConfirmBL();

            var v = new DealerPurchaseOrderConfirmListModel(request);

            v.IdDealer = model.IdDealer;
            v.IdSupplier = model.IdSupplier;
            v.StatusId = model.StatusId;

            var totalCnt = 0;
            var returnValue = purchaseOrderBo.ListDealerPurchaseOrderConfirm(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region DealerPurchaseOrderConfirm Reject

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerPurchaseOrderConfirm.DealerPurchaseOrderConfirmIndex, CommonValues.PermissionCodes.DealerPurchaseOrderConfirm.DealerPurchaseOrderConfirmSave)]
        public ActionResult RejectDealerPurchaseOrderConfirm(int poNumber)
        {
            DealerPurchaseOrderConfirmViewModel viewModel = new DealerPurchaseOrderConfirmViewModel();
            viewModel.PoNumber = poNumber;

            var dealerPurchaseOrderConfirmBo = new DealerPurchaseOrderConfirmBL();
            viewModel.CommandType = CommonValues.DMLType.Update;
            viewModel.StatusId = (int)CommonValues.PurchaseOrderStatus.CanceledPurhcaseOrder;
            viewModel.SupplierDealerConfirm = (int)CommonValues.SupplierDealerConfirmType.NotApprovedProposal;

            dealerPurchaseOrderConfirmBo.DMLDealerPurchaseOrderConfirm(UserManager.UserInfo, viewModel);


            // TFS No : 27807 OYA 02.01.2015
            PurchaseOrderViewModel poModel = new PurchaseOrderViewModel();
            poModel.PoNumber = poNumber;
            PurchaseOrderBL poBo = new PurchaseOrderBL();
            poBo.GetPurchaseOrder(UserManager.UserInfo, poModel);

            DealerBL dBo = new DealerBL();
            DealerViewModel dealerModel = dBo.GetDealer(UserManager.UserInfo, poModel.IdDealer.GetValue<int>()).Model;
            DealerViewModel supplierDealerModel = dBo.GetDealer(UserManager.UserInfo, poModel.SupplierIdDealer.GetValue<int>()).Model;
            string to = dealerModel.ContactEmail;
            string subject = string.Format(MessageResource.DealerPurchaseOrderConfirm_Mail_Subject, poModel.PoNumber);
            string body = string.Format(MessageResource.DealerPurchaseOrderConfirm_Mail_RejectBody, poModel.PoNumber, supplierDealerModel.Name);
            CommonBL.SendDbMail(to, subject, body);


            int totalCount = 0;
            var listModel = new PurchaseOrderDetailListModel { PurchaseOrderNumber = poNumber };
            var detBo = new PurchaseOrderDetailBL();
            var orderDetailList = detBo.ListPurchaseOrderDetails(UserManager.UserInfo, listModel, out totalCount).Data;
            // detay kayıtları iptal statüsüne çekiliyor OYA 16.07.2014 TFS No : 25668
            foreach (PurchaseOrderDetailListModel orderDetailModel in orderDetailList)
            {
                PurchaseOrderDetailViewModel detMo = new PurchaseOrderDetailViewModel();
                detMo.PurchaseOrderDetailSeqNo = orderDetailModel.PurchaseOrderDetailSeqNo;
                detBo.GetPurchaseOrderDetail(UserManager.UserInfo, detMo);
                detMo.CommandType = CommonValues.DMLType.Update;
                detMo.StatusId = (int)CommonValues.PurchaseOrderDetailStatus.Cancelled;
                detBo.DMLPurchaseOrderDetail(UserManager.UserInfo, detMo);
                if (detMo.ErrorNo > 0)
                {
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error, detMo.ErrorMessage);
                }
            }


            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
        }
        #endregion

        #region DealerPurchaseOrderConfirm Accept
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerPurchaseOrderConfirm.DealerPurchaseOrderConfirmIndex, CommonValues.PermissionCodes.DealerPurchaseOrderConfirm.DealerPurchaseOrderConfirmSave)]
        public ActionResult AcceptDealerPurchaseOrderConfirm(int poNumber)
        {
            var model = new DealerPurchaseOrderConfirmViewModel { PoNumber = poNumber };
            var bo = new DealerPurchaseOrderConfirmBL();

            var orderModel = new PurchaseOrderViewModel { PoNumber = poNumber };
            var poBo = new PurchaseOrderBL();
            poBo.GetPurchaseOrder(UserManager.UserInfo, orderModel);

            int totalCount = 0;
            var orderDetailList = new List<PurchaseOrderDetailListModel>();
            var listModel = new PurchaseOrderDetailListModel { PurchaseOrderNumber = poNumber };
            var detBo = new PurchaseOrderDetailBL();
            orderDetailList = detBo.ListPurchaseOrderDetails(UserManager.UserInfo, listModel, out totalCount).Data;

            var control = (from r in orderDetailList.AsEnumerable()
                           where r.ShipmentQuantity == 0
                           select r);
            if (control.Any() && control.Count() == orderDetailList.Count)
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.DealerPurchaseOrderPartConfirm_Warning_ShipmentQuantityEmpty);
            }
            // onaylama adımına da stok kontrolü eklendi OYA 03.04.2018
            var stockControl = orderDetailList.Any(r => r.StockQuantity == 0);
            if(stockControl)
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.Error_DB_NoStockFound);
            }


            SparePartSaleViewModel saleModel = new SparePartSaleViewModel();
            List<SparePartSaleDetailDetailModel> saleDetailList = new List<SparePartSaleDetailDetailModel>();

            SaveResults(orderModel, saleModel, orderDetailList, saleDetailList);
            DealerPurchaseOrderConfirmBL orderBl = new DealerPurchaseOrderConfirmBL();
            orderBl.DMLDealerPurchaseOrderConfirmSave(UserManager.UserInfo, saleModel, saleDetailList);

            if (saleModel.ErrorNo == 0)
            {
                model.CommandType = CommonValues.DMLType.Update;
                // ana kaydın statüsü değişmeyecek OYA 17.08.2014 TFS No : 26172
                //model.StatusId = (int)CommonValues.PurchaseOrderStatus.ClosePurchaseOrder;
                model.StatusId = orderModel.Status.GetValue<int>();

                model.SupplierDealerConfirm = (int)CommonValues.SupplierDealerConfirmType.ApprovedOrder;
                bo.DMLDealerPurchaseOrderConfirm(UserManager.UserInfo, model);

                // TFS No : 27807 OYA 02.01.2015
                PurchaseOrderViewModel poModel = new PurchaseOrderViewModel();
                poModel.PoNumber = poNumber;
                poBo.GetPurchaseOrder(UserManager.UserInfo, poModel);

                DealerBL dBo = new DealerBL();
                DealerViewModel dealerModel = dBo.GetDealer(UserManager.UserInfo, poModel.IdDealer.GetValue<int>()).Model;
                DealerViewModel supplierDealerModel = dBo.GetDealer(UserManager.UserInfo, poModel.SupplierIdDealer.GetValue<int>()).Model;
                string to = dealerModel.ContactEmail;
                string subject = string.Format(MessageResource.DealerPurchaseOrderConfirm_Mail_Subject, model.PoNumber);
                string body = string.Format(MessageResource.DealerPurchaseOrderConfirm_Mail_AcceptBody, model.PoNumber, supplierDealerModel.Name);
                CommonBL.SendDbMail(to, subject, body);


                // detay kayıtları kapalı statüsüne çekiliyor OYA 16.07.2014 TFS No : 25668
                // detay kayıtlarının statüsü değişmeyecek OYA 17.08.2014 TFS No : 26172
                // detay kayıtlarında shipment quantity 0 ise iptale çekiliyor, değilse statü değiştirilmiyor OYA 08.01.2015 TFS No : 27904
                foreach (PurchaseOrderDetailListModel orderDetailModel in orderDetailList)
                {
                    PurchaseOrderDetailViewModel detMo = new PurchaseOrderDetailViewModel();
                    detMo.PurchaseOrderDetailSeqNo = orderDetailModel.PurchaseOrderDetailSeqNo;
                    detBo.GetPurchaseOrderDetail(UserManager.UserInfo, detMo);
                    detMo.ShipmentQuantity = orderDetailModel.ShipmentQuantity;
                    detMo.CommandType = CommonValues.DMLType.Update;
                    if (detMo.ShipmentQuantity == 0)
                        detMo.StatusId = (int)CommonValues.PurchaseOrderDetailStatus.Cancelled;
                    detBo.DMLPurchaseOrderDetail(UserManager.UserInfo, detMo);
                    if (detMo.ErrorNo > 0)
                    {
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Error, detMo.ErrorMessage);
                    }
                }

                SparePartSaleController.SetTotalValues(saleModel);

                CheckErrorForMessage(model, true);
                ModelState.Clear();

                if (model.ErrorNo == 0)
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                                                          saleModel.SparePartSaleId.ToString());
                else
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error, saleModel.ErrorMessage);
            }
            else
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, saleModel.ErrorMessage);
            }
        }
        private static void SaveResults(PurchaseOrderViewModel orderModel,
            SparePartSaleViewModel saleModel,
            List<PurchaseOrderDetailListModel> orderDetailList,
            List<SparePartSaleDetailDetailModel> saleDetailList)
        {
            int totalCount = 0;

            CountryVatRatioBL countryBl = new CountryVatRatioBL();
            DealerBL dealerBl = new DealerBL();
            DealerViewModel dealerModel = dealerBl.GetDealer(UserManager.UserInfo, orderModel.IdDealer.GetValue<int>()).Model;
            
            var dealerCustomerInfo = dealerBl.GetDealerCustomerInfo(orderModel.IdDealer.GetValue<int>()).Model;

            if (dealerCustomerInfo.CustomerId != 0)
            {
                saleModel.CustomerId = dealerCustomerInfo.CustomerId;
                saleModel.CustomerTypeId = dealerCustomerInfo.CustomerTypeId;

                CustomerAddressListModel custAddModel = new CustomerAddressListModel();
                custAddModel.CustomerId = saleModel.CustomerId;
                CustomerAddressBL custAddBo = new CustomerAddressBL();
                List<CustomerAddressListModel> custAddList = custAddBo.ListCustomerAddresses(UserManager.UserInfo, custAddModel, out totalCount).Data;
                if (totalCount != 0)
                {
                    saleModel.BillingAddressId = custAddList.ElementAt(0).AddressId;
                    saleModel.ShippingAddressId = custAddList.ElementAt(0).AddressId;
                }
            }

            //saleModel.PaymentAmount = 0;
            saleModel.IsTenderSale = 0;
            saleModel.VatExclude = 0;
            saleModel.PriceListId = dealerBl.GetCountryDefaultPriceList(dealerModel.Country).Model;
            saleModel.IsReturn = false;
            saleModel.DealerId = UserManager.UserInfo.GetUserDealerId();
            saleModel.CommandType = CommonValues.DMLType.Insert;
            // kampanya talebinden gelen siparişler için normal tipte atama eklendi Oya 03.04.2018
            saleModel.SaleTypeId = (int) CommonValues.SaleType.NormalSale;
            saleModel.SaleDate = DateTime.Now;
            saleModel.SaleStatusLookVal = ((int)CommonValues.SparePartSaleStatus.NewRecord).ToString();
            saleModel.PoNumber = orderDetailList.ElementAt(0).PurchaseOrderNumber;
            // TFS NO: 27597 OYA 16.12.2014 : Başka bayinin onay vermesi sonrasında açılan spare_part_sale kaydının stok tipi
            // kampanya olarak set edilmeli. general_parameters.CAMPAIGN_STOCK_TYPE kullanılmalı. (kampanya siparişleri)
            bool isCampaignPO = orderModel.IdStockType ==
                                CommonBL.GetGeneralParameterValue(CommonValues.GeneralParameters.CampaignStockType).Model
                                        .GetValue<int>();
            saleModel.StockTypeId = isCampaignPO
                                        ? CommonBL.GetGeneralParameterValue(
                                            CommonValues.GeneralParameters.CampaignStockType).Model.GetValue<int>()
                                        : CommonBL.GetGeneralParameterValue(CommonValues.GeneralParameters.StockType).Model
                                                  .GetValue<int>();

            foreach (var result in orderDetailList)
            {
                if (result.ShipmentQuantity != 0)
                {
                    var bl = new DealerPurchaseOrderBL();
                    var model = new DealerPurchaseOrderViewModel { PartId = result.PartId.GetValue<int>(), DealerId = saleModel.DealerId };
                    bl.GetPartDetails(UserManager.UserInfo, model);

                    decimal vatRatio = countryBl.GetVatRatioByPartAndCountry(result.PartId.GetValue<int>(),
                                                                             dealerModel.Country).Model;
                    decimal listPrice = model.ListPrice.GetValue<decimal>();

                    var saleDetailModel = new SparePartSaleDetailDetailModel
                    {
                        PartSaleId = saleModel.SparePartSaleId,
                        SparePartId = result.PartId,
                        CommandType = CommonValues.DMLType.Insert,
                        CurrencyCode = dealerModel.CurrencyCode,
                        // TFS NO: 27597 OYA 16.12.2014 : kampanya siparişlerinde fiyat alanları 0 olarak set ediliyor. 
                        ListPrice = isCampaignPO ? 0 : listPrice,
                        DealerPrice = isCampaignPO ? 0 : result.DealerPrice,
                        DiscountPrice = isCampaignPO ? 0 : model.DiscountPrice,
                        DiscountRatio = model.DiscountRatio,
                        VatRatio = vatRatio,
                        PlanQuantity = result.ShipmentQuantity,
                        PoQuantity = result.ShipmentQuantity,
                        PickQuantity = 0,
                        PoOrderNo = result.PurchaseOrderDetailSeqNo.GetValue<long>(),
                        PoOrderLine = result.PurchaseOrderDetailSeqNo.GetValue<long>(),
                        StatusId = (int)CommonValues.SparePartSaleDetailStatus.NoCollectOrder
                    };
                    saleDetailList.Add(saleDetailModel);
                }
            }
        }
        #endregion
    }
}


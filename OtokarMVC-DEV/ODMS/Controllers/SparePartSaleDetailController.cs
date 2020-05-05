using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using System.Linq;
using System.Text;
using Kendo.Mvc.Extensions;
using Microsoft.Web.Mvc;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel;
using ODMSModel.Customer;
using ODMSModel.CustomerDiscount;
using ODMSModel.Dealer;
using ODMSModel.DownloadFileActionResult;
using ODMSModel.PurchaseOrder;
using ODMSModel.PurchaseOrderDetail;
using ODMSModel.SparePart;
using ODMSModel.SparePartSale;
using ODMSModel.SparePartSaleDetail;
using ODMSModel.SparePartSaleOrder;
using ODMSModel.SparePartSaleOrderDetail;
using ODMSModel.SparePartSaleWaybill;
using ODMSModel.StockCard;
using ODMSModel.StockTypeDetail;
using ODMSModel.WorkOrderPicking;
using ODMSModel.WorkOrderPickingDetail;
using ODMSBusiness.Business;
using ODMSModel.StockCardPriceListModel;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class SparePartSaleDetailController : ControllerBase
    {
        #region General Methods
        private void SetDefaults()
        {
            ViewBag.DetailStatusList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.SaleStatusDetailLookup).Data;
            ViewBag.CurrencyCodeList = CurrencyBL.ListCurrencyAsSelectList(UserManager.UserInfo).Data;
        }

        [ValidateAntiForgeryToken]
        public JsonResult GetValues(string partId, string partSaleId, string stockTypeId)
        {
            decimal shipmentQuantity = 0;
            int pickingQuantity = 0;
            int totalCount = 0;
            string currencyCode = string.Empty;
            string unitName = string.Empty;
            string alternatePart = string.Empty;
            decimal profitMarginRatio = 0;
            decimal vatRatio = 0;
            decimal listPrice = 0;
            decimal dealerPrice = 0;
            decimal discountPrice = 0;
            decimal freeQuantity = 0;
            decimal discountRatio = 0;
            decimal stockQuantity = 0;
            decimal costPrice = 0;
            int dealerId = UserManager.UserInfo.GetUserDealerId();

            if (!string.IsNullOrEmpty(partId))
            {
                SparePartIndexViewModel spModel = new SparePartIndexViewModel();
                spModel.PartId = partId.GetValue<int>();
                SparePartBL spBo = new SparePartBL();
                spBo.GetSparePart(UserManager.UserInfo, spModel);
                shipmentQuantity = spModel.ShipQuantity.GetValue<decimal>();
                alternatePart = spModel.AlternatePart;
                unitName = spModel.UnitName;

                int alternatePartId = 0;
                if (spModel.AlternatePart != "")
                {
                    SparePartIndexViewModel alternatePartModel = new SparePartIndexViewModel { PartCode = spModel.AlternatePart };
                    spBo.GetSparePart(UserManager.UserInfo, alternatePartModel);
                    alternatePartId = alternatePartModel.PartId;
                }

                StockCardPriceListBL _stockCardPriceListService = new StockCardPriceListBL();
                StockCardPriceListModel _stockCardPriceListModel = new StockCardPriceListModel();
                _stockCardPriceListModel.DealerId = UserManager.UserInfo.GetUserDealerId();
                _stockCardPriceListModel.PartId = alternatePartId;
                _stockCardPriceListService.Select(UserManager.UserInfo, _stockCardPriceListModel);
                var priceList = _stockCardPriceListModel.PriceList;
                costPrice = _stockCardPriceListService.Get(UserManager.UserInfo, _stockCardPriceListModel, CommonValues.StockCardPriceType.D).Model.CostPrice;

                if (!string.IsNullOrEmpty(stockTypeId))
                {
                    discountPrice = spBo.GetDiscountPrice(partId.GetValue<int>(), dealerId,
                                                          stockTypeId.GetValue<int>()).Model;
                    freeQuantity = spBo.GetFreeQuantity(partId.GetValue<int>(), dealerId,
                                                        stockTypeId.GetValue<int>()).Model;
                }

                StockTypeDetailListModel stdListModel = new StockTypeDetailListModel();
                stdListModel.IdPart = spModel.PartId;
                stdListModel.IdStockType = CommonBL.GetGeneralParameterValue(CommonValues.GeneralParameters.StockType).Model.GetValue<int>();
                stdListModel.IdDealer = dealerId;
                StockTypeDetailBL stdBo = new StockTypeDetailBL();
                List<StockTypeDetailListModel> stockList = stdBo.ListStockTypeDetail(UserManager.UserInfo, stdListModel, out totalCount).Data;
                if (totalCount != 0)
                {
                    stockQuantity = (from s in stockList.AsEnumerable()
                                     select s.StockQuantity).Sum()
                                                            .GetValue
                        <decimal>();
                }

                StockCardViewModel scModel = new StockCardViewModel();
                scModel.PartId = partId.GetValue<int>();
                scModel.DealerId = dealerId;
                StockCardBL scBo = new StockCardBL();
                scModel = scBo.GetStockCard(UserManager.UserInfo, scModel).Model;
                dealerPrice = scBo.GetDealerPriceByDealerAndPart(partId.GetValue<int>(), dealerId).Model;
                profitMarginRatio = scModel.ProfitMarginRatio.GetValue<decimal>();

                DealerBL dealerBo = new DealerBL();
                DealerViewModel dealerViewModel = dealerBo.GetDealer(UserManager.UserInfo, dealerId).Model;
                currencyCode = dealerViewModel.CurrencyCode;

                CountryVatRatioBL vatRatioBo = new CountryVatRatioBL();
                vatRatio = vatRatioBo.GetVatRatioByPartAndCountry(partId.GetValue<int>(), dealerViewModel.Country).Model;

                CommonBL bo = new CommonBL();
                listPrice = bo.GetPriceByDealerPartVehicleAndType(partId.GetValue<int>(), 0, dealerId, CommonValues.ListPriceLabel).Model;
                if (!string.IsNullOrEmpty(partSaleId))
                {
                    WorkOrderPickingListModel woModel = new WorkOrderPickingListModel();
                    woModel.PartSaleId = partSaleId.GetValue<int>();
                    WorkOrderPickingBL woBo = new WorkOrderPickingBL();
                    List<WorkOrderPickingListModel> woList = woBo.ListWorkOrderPicking(UserManager.UserInfo, woModel, out totalCount).Data;
                    if (totalCount != 0)
                    {
                        WorkOrderPickingDetailListModel wodModel = new WorkOrderPickingDetailListModel();
                        wodModel.WOPMstId = woList.ElementAt(0).WorkOrderPickingId;
                        WorkOrderPickingDetailBL wodBo = new WorkOrderPickingDetailBL();
                        List<WorkOrderPickingDetailListModel> detailList = wodBo.ListWorkOrderPickingDetail(UserManager.UserInfo, wodModel, out totalCount).Data;
                        if (totalCount != 0)
                        {
                            pickingQuantity = detailList.ElementAt(0).PickQuantity.GetValue<int>();
                        }
                    }

                    SparePartSaleBL spsBo = new SparePartSaleBL();
                    SparePartSaleViewModel spsModel = spsBo.GetSparePartSale(UserManager.UserInfo, partSaleId.GetValue<int>()).Model;
                    int customerId = spsModel.CustomerId;
                    CustomerDiscountListModel cdModel = new CustomerDiscountListModel();
                    cdModel.IdCustomer = customerId;
                    cdModel.IdDealer = UserManager.UserInfo.GetUserDealerId();
                    CustomerDiscountBL cdBo = new CustomerDiscountBL();
                    List<CustomerDiscountListModel> cdList = cdBo.ListCustomerDiscount(UserManager.UserInfo, cdModel, out totalCount).Data;
                    if (totalCount != 0)
                    {
                        discountRatio = cdList.ElementAt(0).PartDiscountRatio.GetValue<decimal>();
                    }
                }
            }
            else
            {
                return null;
            }
            return
                Json(
                    new
                    {
                        CurrencyCode = currencyCode,
                        UnitName = unitName,
                        PickingQuantity = pickingQuantity,
                        ShipmentQuantity = shipmentQuantity,
                        ProfitMarginRatio = profitMarginRatio,
                        VatRatio = vatRatio,
                        ListPrice = listPrice,
                        DealerPrice = dealerPrice,
                        DiscountPrice = discountPrice,
                        FreeQuantity = freeQuantity,
                        DiscountRatio = discountRatio,
                        StockQuantity = stockQuantity,
                        AlternatePart = alternatePart,
                        CostPrice = costPrice
                    });
        }

        [ValidateAntiForgeryToken]
        public JsonResult GetOtokarValues(string partId, string partSaleId, string stockTypeId)
        {
            decimal shipmentQuantity = 0;
            int pickingQuantity = 0;
            int totalCount = 0;
            string currencyCode = string.Empty;
            string unitName = string.Empty;
            decimal profitMarginRatio = 0;
            decimal vatRatio = 0;
            decimal price = 0;
            decimal listPrice = 0;
            decimal dealerPrice = 0;
            decimal discountPrice = 0;
            decimal freeQuantity = 0;
            decimal discountRatio = 0;
            decimal stockQuantity = 0;
            int dealerId = UserManager.UserInfo.GetUserDealerId();

            if (!string.IsNullOrEmpty(partId))
            {
                SparePartIndexViewModel spModel = new SparePartIndexViewModel();
                spModel.PartId = partId.GetValue<int>();
                SparePartBL spBo = new SparePartBL();
                spBo.GetSparePart(UserManager.UserInfo, spModel);
                shipmentQuantity = spModel.ShipQuantity.GetValue<decimal>();
                unitName = spModel.UnitName;
                /**************************************************************************************************************************/
                // IM103556358 talebine istinaden fiyat bilgileri stock card'ından çekildi
                // OYA 31.10.2018
                DealerBL dealerBo = new DealerBL();
                DealerViewModel dealerModel = dealerBo.GetDealer(UserManager.UserInfo, dealerId).Model;
                currencyCode = dealerModel.CurrencyCode;

                StockCardBL scBo = new StockCardBL();
                StockCardViewModel scModel = new StockCardViewModel();
                scModel.DealerId = dealerId;
                scModel.PartId = spModel.PartId;
                scBo.GetStockCard(UserManager.UserInfo, scModel);

                StockCardPriceListBL scpBo = new StockCardPriceListBL();
                StockCardPriceListModel scpListModel = new StockCardPriceListModel();
                scpListModel.StockCardId = scModel.StockCardId;
                scpListModel.PartId = spModel.PartId;
                scpListModel.DealerId = UserManager.UserInfo.GetUserDealerId();
                scpListModel.PriceId = dealerBo.GetCountryDefaultPriceList(dealerModel.Country).Model;

                decimal costPrice = scpBo.Get(UserManager.UserInfo, scpListModel, CommonValues.StockCardPriceType.D).Model.CostPrice;
                price = scModel.LastPrice.GetValue<decimal>();
                dealerPrice = scBo.GetDealerPriceByDealerAndPart(partId.GetValue<int>(), dealerId).Model;
                profitMarginRatio = scModel.ProfitMarginRatio.GetValue<decimal>();
                /**************************************************************************************************************************/

                if (!string.IsNullOrEmpty(stockTypeId))
                {
                    discountPrice = costPrice; /*spBo.GetDiscountPrice(partId.GetValue<int>(), dealerId,
                                                          stockTypeId.GetValue<int>()).Model;*/
                    freeQuantity = spBo.GetFreeQuantity(partId.GetValue<int>(), dealerId,
                                                        stockTypeId.GetValue<int>()).Model;
                }

                StockTypeDetailListModel stdListModel = new StockTypeDetailListModel();
                stdListModel.IdPart = spModel.PartId;
                stdListModel.IdStockType = CommonBL.GetGeneralParameterValue(CommonValues.GeneralParameters.StockType).Model.GetValue<int>();
                stdListModel.IdDealer = dealerId;
                StockTypeDetailBL stdBo = new StockTypeDetailBL();
                List<StockTypeDetailListModel> stockList = stdBo.ListStockTypeDetail(UserManager.UserInfo, stdListModel, out totalCount).Data;
                if (totalCount != 0)
                {
                    stockQuantity = (from s in stockList.AsEnumerable()
                                     select s.StockQuantity).Sum()
                                                            .GetValue
                        <decimal>();
                }                

                CountryVatRatioBL vatRatioBo = new CountryVatRatioBL();
                vatRatio = vatRatioBo.GetVatRatioByPartAndCountry(partId.GetValue<int>(), dealerModel.Country).Model;

                CommonBL bo = new CommonBL();
                listPrice = bo.GetPriceByDealerPartVehicleAndType(partId.GetValue<int>(), 0, dealerId, CommonValues.ListPriceLabel).Model;
                if (!string.IsNullOrEmpty(partSaleId))
                {
                    WorkOrderPickingListModel woModel = new WorkOrderPickingListModel();
                    woModel.PartSaleId = partSaleId.GetValue<int>();
                    WorkOrderPickingBL woBo = new WorkOrderPickingBL();
                    List<WorkOrderPickingListModel> woList = woBo.ListWorkOrderPicking(UserManager.UserInfo, woModel, out totalCount).Data;
                    if (totalCount != 0)
                    {
                        WorkOrderPickingDetailListModel wodModel = new WorkOrderPickingDetailListModel();
                        wodModel.WOPMstId = woList.ElementAt(0).WorkOrderPickingId;
                        WorkOrderPickingDetailBL wodBo = new WorkOrderPickingDetailBL();
                        List<WorkOrderPickingDetailListModel> detailList = wodBo.ListWorkOrderPickingDetail(UserManager.UserInfo, wodModel, out totalCount).Data;
                        if (totalCount != 0)
                        {
                            pickingQuantity = detailList.ElementAt(0).PickQuantity.GetValue<int>();
                        }
                    }

                    SparePartSaleBL spsBo = new SparePartSaleBL();
                    SparePartSaleViewModel spsModel = spsBo.GetSparePartSale(UserManager.UserInfo, partSaleId.GetValue<int>()).Model;
                    int customerId = spsModel.CustomerId;
                    CustomerDiscountListModel cdModel = new CustomerDiscountListModel();
                    cdModel.IdCustomer = customerId;
                    cdModel.IdDealer = UserManager.UserInfo.GetUserDealerId();
                    CustomerDiscountBL cdBo = new CustomerDiscountBL();
                    List<CustomerDiscountListModel> cdList = cdBo.ListCustomerDiscount(UserManager.UserInfo, cdModel, out totalCount).Data;
                    if (totalCount != 0)
                    {
                        discountRatio = cdList.ElementAt(0).PartDiscountRatio.GetValue<decimal>();
                    }
                }
            }
            else
            {
                return null;
            }
            return
                Json(
                    new
                    {
                        CurrencyCode = currencyCode,
                        UnitName = unitName,
                        PickingQuantity = pickingQuantity,
                        ShipmentQuantity = shipmentQuantity,
                        ProfitMarginRatio = profitMarginRatio,
                        VatRatio = vatRatio,
                        Price = price,
                        ListPrice = listPrice,
                        DealerPrice = dealerPrice,
                        DiscountPrice = discountPrice,
                        FreeQuantity = freeQuantity,
                        DiscountRatio = discountRatio,
                        StockQuantity = stockQuantity
                    });
        }

        [ValidateAntiForgeryToken]
        public JsonResult GetSparePartChangeInfo(string partId)
        {
            if (!string.IsNullOrEmpty(partId))
            {
                // parça değiştirilmiş mi kontrol ediliyor.
                var bo = new SparePartBL();
                bool isPartChanged = false;
                var spModel = new SparePartIndexViewModel() { PartId = partId.GetValue<int>() };
                bo.GetSparePart(UserManager.UserInfo, spModel);
                string oldPartCode = spModel.PartCode;
                int topPartId = bo.IsPartChanged(spModel.PartId, spModel.PartCode).Model;
                isPartChanged = partId.GetValue<int>() != topPartId;
                int newPartId = isPartChanged ? topPartId : partId.GetValue<int>();
                spModel = new SparePartIndexViewModel();
                spModel.PartId = newPartId;
                bo.GetSparePart(UserManager.UserInfo, spModel);
                string newPartCode = spModel.PartCode;
                // eşlenik parçalar için bölünen değişen parça kontrolü yapılmayacak
                if (spModel.IsOriginal.GetValue<bool>())
                {
                    StringBuilder newPartCodeList = new StringBuilder();
                    // parça değiştirilmişse değiştirilen parça değiştirilmemişse seçilen parça bölünmüş mü kontrol ediliyor.
                    List<SparePartSplittingViewModel> splitList = bo.ListSparePartsSplitting(spModel.PartId).Data;
                    if (isPartChanged || splitList.Count > 0)
                    {
                        foreach (SparePartSplittingViewModel sparePartSplittingViewModel in splitList)
                        {
                            var spNewModel = new SparePartIndexViewModel
                            {
                                PartId = sparePartSplittingViewModel.PartId.GetValue<int>()
                            };
                            bo.GetSparePart(UserManager.UserInfo, spNewModel);
                            newPartCodeList.Append(spNewModel.PartCode);
                            newPartCodeList.Append(",");
                        }
                    }

                    return Json(new
                    {
                        NewPartId = spModel.PartId,
                        PartName = newPartCode + CommonValues.Slash + spModel.PartNameInLanguage,
                        MessageChange =
                                isPartChanged
                                    ? string.Format(MessageResource.PurchaseOrderDetail_Warning_ChangedPart, oldPartCode, newPartCode)
                                    : string.Empty,
                        MessageSplit =
                                splitList.Count > 0
                                    ? string.Format(MessageResource.PurchaseOrderDetail_Warning_DividedPart, newPartCode, newPartCodeList)
                                    : string.Empty
                    });
                }
                else
                {
                    return Json(new
                    {
                        NewPartId = spModel.PartId,
                        PartName = spModel.PartNameInLanguage,
                        MessageChange = string.Empty,
                        MessageSplit = string.Empty
                    });
                }
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Spare Part Sale Detail Add Part
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSaleOrder.SparePartSaleOrderIndex,
            CommonValues.PermissionCodes.SparePartSaleOrder.SparePartSaleOrderDetails)]
        public ActionResult ListSparePartSaleOrder([DataSourceRequest]DataSourceRequest request, SparePartSaleOrderViewModel model)
        {
            int totalCnt;
            bool? isPartOriginal = model.IsPartOriginal;

            int sparePartSaleId = model.SparePartSaleId;
            SparePartSaleBL spsBo = new SparePartSaleBL();
            SparePartSaleViewModel spsModel = spsBo.GetSparePartSale(UserManager.UserInfo, sparePartSaleId).Model;
            CustomerBL cBo = new CustomerBL();
            CustomerIndexViewModel cModel = new CustomerIndexViewModel();
            cModel.CustomerId = spsModel.CustomerId;
            cBo.GetCustomer(UserManager.UserInfo, cModel);
            SparePartSaleOrderBL spsoBo = new SparePartSaleOrderBL();
            SparePartSaleOrderDetailBL spsodBo = new SparePartSaleOrderDetailBL();
            SparePartSaleOrderListModel spsoListModel = new SparePartSaleOrderListModel();
            spsoListModel.CustomerId = spsModel.CustomerId;
            spsoListModel.StockTypeId = spsModel.StockTypeId;
            spsoListModel.DealerId = UserManager.UserInfo.GetUserDealerId();
            spsoListModel.StatusId = (int)CommonValues.SparePartSaleOrderStatus.ApprovedOrder;
            var spsoList = spsoBo.ListSparePartSaleOrders(UserManager.UserInfo, spsoListModel, out totalCnt).Data;
            List<SparePartSaleOrderListModel> returnValue = new List<SparePartSaleOrderListModel>();
            if (isPartOriginal.HasValue && cModel.IsDealerCustomer)
            {
                foreach (SparePartSaleOrderListModel listModel in spsoList)
                {
                    SparePartSaleOrderDetailListModel spsodListModel = new SparePartSaleOrderDetailListModel();
                    spsodListModel.SoNumber = listModel.SoNumber;
                    List<SparePartSaleOrderDetailListModel> detailList = spsodBo.ListSparePartSaleOrderDetails(UserManager.UserInfo, spsodListModel, out totalCnt).Data;
                    if (detailList.Any())
                    {
                        if (detailList.Any(e => e.IsOriginalPart == isPartOriginal))
                            returnValue.Add(listModel);
                    }
                }
            }
            else
            {
                returnValue = spsoList;
            }

            return Json(new
            {
                Data = returnValue,
                Total = returnValue.Count
            });
        }
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSaleOrderDetail.SparePartSaleOrderDetailIndex,
          CommonValues.PermissionCodes.SparePartSaleOrderDetail.SparePartSaleOrderDetailDetails)]
        public ActionResult ListSparePartSaleOrderDetail([DataSourceRequest]DataSourceRequest request, SparePartSaleOrderDetailDetailModel model)
        {
            var bo = new SparePartSaleOrderDetailBL();
            var referenceModel = new SparePartSaleOrderDetailListModel(request)
            {
                SoNumber = model.SoNumber
            };
            int totalCnt;
            var returnValue = bo.ListSparePartSaleOrderDetails(UserManager.UserInfo, referenceModel, out totalCnt).Data;
            returnValue = returnValue.Where(e => e.IsOriginalPart == model.IsOriginalPart
                                                 && e.OrderQuantity > e.PlannedQuantity
                                                 && e.StatusId == (int)CommonValues.SparePartSaleOrderDetailStatus.OpenOrder).Select(e =>
                                                    new SparePartSaleOrderDetailListModel
                                                    {
                                                        SparePartSaleOrderDetailId = e.SparePartSaleOrderDetailId,
                                                        SparePartId = e.SparePartId,
                                                        SparePartCode = e.SparePartCode,
                                                        SparePartName = e.SparePartName,
                                                        OpenQuantity = e.OrderQuantity - e.PlannedQuantity,
                                                        ConfirmPrice = e.ConfirmPrice ?? e.OrderPrice,
                                                        StockQuantity = e.StockQuantity,
                                                        OrderedQuantity = e.OrderedQuantity,
                                                        StatusName = e.StatusName,
                                                        ChangedPartId = e.ChangedPartId
                                                    }).ToList();

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSaleDetail.SparePartSaleDetailCreate,
            CommonValues.PermissionCodes.SparePartSaleOrderDetail.SparePartSaleOrderDetailUpdate)]
        public ActionResult SparePartSaleDetailAddPart(string sparePartSaleId)
        {
            int totalCnt;
            SparePartSaleBL spsoBo = new SparePartSaleBL();
            SparePartSaleViewModel model = spsoBo.GetSparePartSale(UserManager.UserInfo, sparePartSaleId.GetValue<int>()).Model;

            SparePartSaleDetailBL spsdBo = new SparePartSaleDetailBL();
            SparePartSaleDetailListModel spsdListModel = new SparePartSaleDetailListModel();
            spsdListModel.PartSaleId = sparePartSaleId.GetValue<long>();
            List<SparePartSaleDetailListModel> detailList = spsdBo.ListSparePartSaleDetails(UserManager.UserInfo, spsdListModel, out totalCnt).Data;
            if (detailList.Any())
            {
                int partId = detailList.Select(f => f.SparePartId).First().GetValue<int>();
                SparePartBL spBo = new SparePartBL();
                SparePartIndexViewModel spModel = new SparePartIndexViewModel();
                spModel.PartId = partId;
                spBo.GetSparePart(UserManager.UserInfo, spModel);
                model.IsPartOriginal = spModel.IsOriginal.GetValue<bool>();
            }

            return View(model);
        }
        [HttpPost]
        public ActionResult AddParts()
        {
            int totalCount;
            CommonBL bo = new CommonBL();
            int dealerId = UserManager.UserInfo.GetUserDealerId();
            DealerBL dBo = new DealerBL();
            DealerViewModel dModel = dBo.GetDealer(UserManager.UserInfo, dealerId).Model;
            string currencyCode = dBo.GetCountryCurrencyCode(dModel.Country).Model;

            var list = ParseModelFromRequestInputStream<List<SparePartSaleOrderDetailListModel>>();

            /*
             SPARE_PART_SALE_DETAIL tablosuna seçilen satış siparişleri yazılır:
                ID_PART_SALE -> ekranı açarken geliyor
                ID_PART -> SALE_ORDER_DET.ID_PART
                CURRENCY_CODE -> DEALER.COUNTRY.CURRENCY_CODE
                IS_PRICE_FIXED -> SALE_ORDER_MST.IS_PRICE_FIXED
                LIST_PRICE -> IS_PRICE_FIXED = 1 ise: SALE_ORDER_DET.LIST_PRICE; IS_PRICE_FIXED = 0 ise FN_GET_SPARE_PART_PRICE
                DEALER_PRICE -> STOCK_CARD.AVG_DEALER_PRICE
                DISCOUNT_RATIO -> ISNULL(SALE_ORDER_DET.APPLIED_DISCOUNT_RATIO, LIST_DISCOUNT_RATIO) 
                DISCOUNT_PRICE ->IS_PRICE_FIXED = 1 ise: SALE_ORDER_DET.CONFIRM_PRICE; IS_PRICE_FIXED = 0 ise hesapladığın LIST_PRICE * ((100-DISCOUNT_RATIO)/100)
                VAT_RATIO -> FN_GET_SPARE_PART_VAT_RATIO
                STATUS_LOOKVAL ->1
                PLAN_QUANTITY -> Textboxa yazılan miktar
                PICK_PLAN_QTY -> 0
                PICKED -> 0
                RETURNED ->0
                Return_REASON ->NULL
                DELIVERY_SEQ_NO -> NULL
                SO_DET_SEQ_NO -> SALE_ORDER_DET.SO_DET_SEQ_NO
             */

            if (list.Any())
            {
                SparePartSaleOrderBL spsoBo = new SparePartSaleOrderBL();
                SparePartSaleDetailBL spsdBo = new SparePartSaleDetailBL();
                SparePartSaleOrderDetailBL spsodBo = new SparePartSaleOrderDetailBL();
                StockCardBL scBo = new StockCardBL();
                WorkOrderCardBL wocBo = new WorkOrderCardBL();
                foreach (SparePartSaleOrderDetailListModel detailModel in list)
                {
                    SparePartSaleOrderDetailDetailModel spsodModel = new SparePartSaleOrderDetailDetailModel();
                    spsodModel.SparePartSaleOrderDetailId = detailModel.SparePartSaleOrderDetailId;
                    spsodBo.GetSparePartSaleOrderDetail(UserManager.UserInfo, spsodModel);

                    int partId = spsodModel.SparePartId.GetValue<int>();
                    decimal listPrice = bo.GetPriceByDealerPartVehicleAndType(partId, 0, dealerId, CommonValues.ListPriceLabel).Model;
                    decimal dealerPrice = scBo.GetDealerPriceByDealerAndPart(partId, dealerId).Model;
                    decimal vatRatio = wocBo.GetSparePartVatRatio(UserManager.UserInfo, partId).Model;

                    SparePartSaleOrderViewModel spsModel = spsoBo.GetSparePartSaleOrder(UserManager.UserInfo, spsodModel.SoNumber);

                    SparePartSaleDetailDetailModel addedModel = new SparePartSaleDetailDetailModel();
                    addedModel.PartSaleId = detailModel.SparePartSaleId;
                    addedModel.SparePartId = spsodModel.SparePartId.GetValue<int>();
                    addedModel.CurrencyCode = currencyCode;
                    addedModel.IsPriceFixed = spsModel.IsFixedPrice;
                    addedModel.ListPrice = addedModel.IsPriceFixed ? spsodModel.ListPrice : listPrice;
                    addedModel.DealerPrice = dealerPrice;
                    addedModel.DiscountRatio = spsodModel.AppliedDiscountRatio ?? spsodModel.ListDiscountRatio;
                    addedModel.DiscountPrice = addedModel.IsPriceFixed ? spsodModel.ConfirmPrice : (spsodModel.ListPrice * ((100 - spsodModel.AppliedDiscountRatio) / 100));
                    addedModel.VatRatio = vatRatio;
                    addedModel.StatusId = (int)CommonValues.SparePartSaleDetailStatus.NoCollectOrder;
                    addedModel.PlanQuantity = detailModel.OrderedQuantity;
                    addedModel.PickedQuantity = 0;
                    addedModel.ReturnedQuantity = 0;
                    addedModel.SoDetSeqNo = spsodModel.SparePartSaleOrderDetailId.ToString();
                    addedModel.CommandType = CommonValues.DMLType.Insert;
                    spsdBo.DMLSparePartSaleDetail(UserManager.UserInfo, addedModel);
                    if (addedModel.ErrorNo > 0)
                        return Json(new { errorMessage = addedModel.ErrorMessage, errorNo = addedModel.ErrorNo });

                    /*   Insertler yapılırken, SALE_ORDER_DET.PLANNED_QUANT değerleri textboxa girilen değer kadar artırılmalı. Artırma işlemi sonrası ORDER_QUANT değerine eşit olduysa 
                        * SALE_ORDER_DET.STATUS_LOOKVAL = 1 olarak güncellenir.*/
                    spsodModel.PlannedQuantity = spsodModel.PlannedQuantity + detailModel.OrderedQuantity;
                    spsodModel.CommandType = CommonValues.DMLType.Update;
                    if (spsodModel.PlannedQuantity == spsodModel.OrderQuantity)
                    {
                        spsodModel.StatusId = (int)CommonValues.SparePartSaleOrderDetailStatus.ClosedOrder;
                    }
                    spsodBo.DMLSparePartSaleOrderDetail(UserManager.UserInfo, spsodModel);
                    if (spsodModel.ErrorNo > 0)
                        return Json(new { errorMessage = spsodModel.ErrorMessage, errorNo = spsodModel.ErrorNo });

                    /*                     
                      Eğer statü değiştirilirse, bu durumda o detayın masterın içindeki tüm detayların STATUS_LOOKVAL değeri 0dan 
                     * farklı mı diye bakılır, eğer hepsi 0dan farklıysa master kaydının STATUS_LOOKVAL değeri 5 olarak güncellenir.
                     */
                    SparePartSaleOrderDetailListModel detailListModel = new SparePartSaleOrderDetailListModel();
                    detailListModel.SoNumber = spsodModel.SoNumber;
                    List<SparePartSaleOrderDetailListModel> detailList = spsodBo.ListSparePartSaleOrderDetails(UserManager.UserInfo, detailListModel, out totalCount).Data;
                    var control = detailList.Any(e => e.StatusId != (int)CommonValues.SparePartSaleOrderDetailStatus.ClosedOrder);
                    if (!control)
                    {
                        SparePartSaleOrderViewModel spsoModel = spsoBo.GetSparePartSaleOrder(UserManager.UserInfo, spsodModel.SoNumber);
                        spsoModel.CommandType = CommonValues.DMLType.Update;
                        spsoModel.StatusId = (int)CommonValues.SparePartSaleOrderStatus.ClosedOrder;
                        spsoBo.DMLSparePartSaleOrder(UserManager.UserInfo, spsoModel);
                        if (spsoModel.ErrorNo > 0)
                            return Json(new { errorMessage = spsoModel.ErrorMessage, errorNo = spsoModel.ErrorNo });
                    }
                }

            }
            return Json(new { errorMessage = MessageResource.Global_Display_Success, MessageResource.Global_Display_Success });
        }
        #endregion

        #region Spare Part Sale Detail Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSale.SparePartSaleIndex,
            CommonValues.PermissionCodes.SparePartSaleDetail.SparePartSaleDetailUpdate)]
        public ActionResult SparePartSaleRefresh(int sparePartSaleDetailId)
        {
            /*
              LIST_PRICE FN_GET_SPARE_PART_PRICE'a parametre gönderilerek güncel değeri ile güncellenir, DISCOUNT_PRICE da LIST_PRICE'a göre tekrar hesaplanır ve o da güncellenir. 
             * PRICE_LIST_DATE alanına da günün tarihi atılır.
             */
            int count = 0;
            CommonBL bo = new CommonBL();

            SparePartSaleDetailBL spsdBo = new SparePartSaleDetailBL();
            SparePartSaleDetailDetailModel spsdModel = new SparePartSaleDetailDetailModel();
            spsdModel.SparePartSaleDetailId = sparePartSaleDetailId;
            spsdBo.GetSparePartSaleDetail(UserManager.UserInfo, spsdModel);

            SparePartSaleBL spsBo = new SparePartSaleBL();
            SparePartSaleViewModel spsModel = spsBo.GetSparePartSale(UserManager.UserInfo, spsdModel.PartSaleId).Model;

            if (spsdModel.IsPriceFixed)
            {
                int partId = spsdModel.SparePartId.GetValue<int>();
                decimal listPrice = bo.GetPriceByDealerPartVehicleAndType(partId, 0, spsModel.DealerId, CommonValues.ListPriceLabel).Model;

                spsdModel.CommandType = CommonValues.DMLType.Update;
                spsdModel.ListPrice = listPrice;
                spsdModel.DiscountPrice = listPrice * ((100 - spsdModel.DiscountRatio) / 100);
                spsdModel.PriceListDate = DateTime.Now;
                spsdBo.DMLSparePartSaleDetail(UserManager.UserInfo, spsdModel);
                if (spsdModel.ErrorNo > 0)
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error, spsdModel.ErrorMessage);
            }

            return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
        }


        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSaleDetail.SparePartSaleDetailIndex)]
        public ActionResult SparePartSaleDetailIndex(int id = 0, bool openCreatePopup = false)
        {
            int totalCount = 0;
            SparePartSaleBL bo = new SparePartSaleBL();
            SparePartSaleViewModel parentModel = bo.GetSparePartSale(UserManager.UserInfo, id).Model;

            ViewBag.PartSaleStatus = parentModel.SaleStatusLookVal;

            var model = new SparePartSaleDetailListModel
            {
                PartSaleId = id
            };
            ViewBag.OpenCreatePopup = openCreatePopup;


            int customerId = parentModel.CustomerId;
            CustomerBL cBo = new CustomerBL();
            CustomerIndexViewModel cModel = new CustomerIndexViewModel();
            cModel.CustomerId = customerId;
            cBo.GetCustomer(UserManager.UserInfo, cModel);
            string cSSID = cModel.SAPCustomerSSID;
            DealerBL dBo = new DealerBL();
            DealerViewModel dModel = dBo.GetDealerBySSID(UserManager.UserInfo, cSSID).Model;
            if (dModel.DealerId != 0)
            {
                // bayi müşterisi
                model.IsCustomerDealer = true;
            }

            return PartialView(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSaleDetail.SparePartSaleDetailIndex, CommonValues.PermissionCodes.SparePartSaleDetail.SparePartSaleDetailDetails)]
        public ActionResult ListSparePartSaleDetails([DataSourceRequest]DataSourceRequest request, SparePartSaleDetailListModel model)
        {
            DealerBL dealerBo = new DealerBL();
            DealerViewModel dealerModel = dealerBo.GetDealer(UserManager.UserInfo, UserManager.UserInfo.GetUserDealerId()).Model;
            string currencyCode = dealerBo.GetCountryCurrencyCode(dealerModel.Country).Model;

            var bo = new SparePartSaleDetailBL();
            var referenceModel = new SparePartSaleDetailListModel(request)
            {
                PartSaleId = model.PartSaleId
            };
            int totalCnt;
            var returnValue = bo.ListSparePartSaleDetails(UserManager.UserInfo, referenceModel, out totalCnt).Data;

            StockCardViewModel scModel = new StockCardViewModel();
            StockCardBL scBo = new StockCardBL();
            foreach (SparePartSaleDetailListModel sparePartSaleDetailListModel in returnValue)
            {
                scModel.PartId = sparePartSaleDetailListModel.SparePartId.GetValue<int>();
                scModel.DealerId = UserManager.UserInfo.GetUserDealerId();
                scModel = scBo.GetStockCard(UserManager.UserInfo, scModel).Model;
                sparePartSaleDetailListModel.DealerPrice = scBo.GetDealerPriceByDealerAndPart(scModel.PartId.GetValue<int>(), UserManager.UserInfo.GetUserDealerId()).Model;
                sparePartSaleDetailListModel.DealerPriceWithCurrency = sparePartSaleDetailListModel.DealerPrice + " " +
                                                                       currencyCode;
            }

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }
        #endregion

        #region Spare Part Sale Create
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSaleDetail.SparePartSaleDetailIndex, CommonValues.PermissionCodes.SparePartSaleDetail.SparePartSaleDetailCreate)]
        public ActionResult SparePartSaleDetailCreate(int id = 0)
        {
            ViewBag.IsSuccess = true;
            var model = new SparePartSaleDetailDetailModel
            {
                PartSaleId = id,
                StatusId = (int)CommonValues.SparePartSaleDetailStatus.NoCollectOrder
            };

            SparePartSaleBL bo = new SparePartSaleBL();
            SparePartSaleViewModel parentModel = bo.GetSparePartSale(UserManager.UserInfo, id).Model;
            ViewBag.PartSaleStatus = parentModel.SaleStatusLookVal;

            SetTotalValues(model, parentModel);

            SetDefaults();
            return View(model);
        }

        private static void SetTotalValues(SparePartSaleDetailDetailModel model, SparePartSaleViewModel parentModel)
        {
            DealerBL dealerBo = new DealerBL();
            DealerViewModel dealerModel = dealerBo.GetDealer(UserManager.UserInfo, UserManager.UserInfo.GetUserDealerId()).Model;
            string currencyCode = dealerBo.GetCountryCurrencyCode(dealerModel.Country).Model;
            model.DealerCurrencyCode = currencyCode;

            int totalCount = 0;
            SparePartSaleDetailListModel dModel = new SparePartSaleDetailListModel();
            dModel.PartSaleId = model.PartSaleId;
            SparePartSaleDetailBL dBo = new SparePartSaleDetailBL();
            List<SparePartSaleDetailListModel> detailList = dBo.ListSparePartSaleDetails(UserManager.UserInfo, dModel, out totalCount).Data;
            if (totalCount != 0)
            {
                decimal totalListPrice = detailList.Sum(e => e.ListPrice * e.PlanQuantity).GetValue<decimal>();
                decimal totalDiscountPrice = detailList.Sum(e => e.DiscountPrice * e.PlanQuantity).GetValue<decimal>();
                model.TotalListPrice = totalListPrice;
                model.TotalDiscountPrice = totalListPrice - totalDiscountPrice;
                model.TotalPriceWithoutVatRatio = totalDiscountPrice;
                decimal totalVatRatio = detailList.Sum(
                    e =>
                    parentModel.VatExclude == 0
                        ? (e.DiscountPrice * e.PlanQuantity * e.VatRatio) / 100
                        : 0).GetValue<decimal>();
                model.TotalPriceWithVatRatio = model.TotalPriceWithoutVatRatio + totalVatRatio;
            }
        }

        public ActionResult ExcelSample()
        {
            var bo = new SparePartSaleDetailBL();
            var ms = bo.SampleExcelFormat();
            return File(ms, CommonValues.ExcelContentType, MessageResource.SparePartSaleDetail_PageTitle_Index + CommonValues.ExcelExtOld);
        }
        private SparePartSaleDetailDetailModel InsertSingle(SparePartSaleDetailDetailModel model)
        {
            var bo = new SparePartSaleDetailBL();
            CommonBL cbo = new CommonBL();
            DealerBL dealerBo = new DealerBL();
            SparePartBL spBo = new SparePartBL();
            SetDefaults();

            if (model.SparePartId != null)
            {
                SparePartIndexViewModel spModel = new SparePartIndexViewModel();
                spModel.PartId = model.SparePartId.GetValue<int>();
                spBo.GetSparePart(UserManager.UserInfo, spModel);
                model.SparePartName = spModel.PartCode + CommonValues.Slash + spModel.AdminDesc;
                StockCardBL scBo = new StockCardBL();
                model.DealerPrice = scBo.GetDealerPriceByDealerAndPart(spModel.PartId, UserManager.UserInfo.GetUserDealerId()).Model;

                model.ListPrice = cbo.GetPriceByDealerPartVehicleAndType(spModel.PartId, 0, UserManager.UserInfo.GetUserDealerId(), CommonValues.ListPriceLabel).Model;

                DealerViewModel dealerViewModel = dealerBo.GetDealer(UserManager.UserInfo, UserManager.UserInfo.GetUserDealerId()).Model;
                CountryVatRatioBL vatRatioBo = new CountryVatRatioBL();
                model.VatRatio = vatRatioBo.GetVatRatioByPartAndCountry(spModel.PartId, dealerViewModel.Country).Model;
            }
            if (model.PlanQuantity < (model.PickQuantity + model.PickedQuantity - model.ReturnedQuantity))
            {
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.SparePartSaleDetail_Warning_QuantityPickQuantity;
                return model;
            }
            if (model.DiscountPrice > model.ListPrice)
            {
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.SparePartSaleDetail_Warning_DiscountPriceListPriceControl;
                return model;
            }

            int totalCount = 0;
            SparePartSaleDetailListModel lModel = new SparePartSaleDetailListModel();
            lModel.PartSaleId = model.PartSaleId;
            SparePartSaleDetailBL spdBo = new SparePartSaleDetailBL();
            List<SparePartSaleDetailListModel> detailList = spdBo.ListSparePartSaleDetails(UserManager.UserInfo, lModel, out totalCount).Data;
            if (totalCount != 0)
            {
                var control = (from s in detailList.AsEnumerable()
                               where s.SparePartId == model.SparePartId
                               select s);
                if (control.Any())
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.SparePartSaleDetail_Warning_SamePartExists;
                    return model;
                }
            }
            ModelState.Remove("CostPrice");
            if (ModelState.IsValid)
            {
                DealerViewModel dealerModel = dealerBo.GetDealer(UserManager.UserInfo, UserManager.UserInfo.GetUserDealerId()).Model;
                model.CurrencyCode = dealerModel.CurrencyCode;
                model.CommandType = CommonValues.DMLType.Insert;
                bo.DMLSparePartSaleDetail(UserManager.UserInfo, model);
                if (model.ErrorNo != 0)
                {
                    SetMessage(model.ErrorMessage, CommonValues.MessageSeverity.Fail);
                    return model;
                }
                ModelState.Clear();

                model = new SparePartSaleDetailDetailModel
                {
                    PartSaleId = model.PartSaleId,
                    StatusId = (int)CommonValues.SparePartSaleDetailStatus.NoCollectOrder
                };
                SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);
                return model;
            }
            else
            {
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.SparePartSaleDetail_Warning_MissingInfo;
            }
            return model;
        }
        private SparePartSaleDetailDetailModel InsertMultiple(SparePartSaleDetailDetailModel model, HttpPostedFileBase excelFile)
        {
            ModelState.Clear();
            CommonBL cbo = new CommonBL();
            DealerBL dealerBo = new DealerBL();
            StockCardBL scBo = new StockCardBL();
            var bo = new SparePartSaleDetailBL();
            Stream s = excelFile.InputStream;
            List<SparePartSaleDetailDetailModel> listModels = bo.ParseExcel(UserManager.UserInfo, model, s).Data;
            if (listModels.Count == 0)
            {
                SetMessage(MessageResource.SparePartSaleDetail_Warning_ExcelRowsDataFound, CommonValues.MessageSeverity.Fail);
            }
            else
            {
                if (listModels.Exists(q => q.ErrorNo >= 1))
                {
                    var ms = bo.SetExcelReport(listModels, model.ErrorMessage);

                    var fileViewModel = new DownloadFileViewModel
                    {
                        FileName = CommonValues.ErrorLog + DateTime.Now + CommonValues.ExcelExtOld,
                        ContentType = CommonValues.ExcelContentType,
                        MStream = ms,
                        Id = Guid.NewGuid()
                    };

                    Session[CommonValues.DownloadFileFormatCookieKey] = fileViewModel.Add(fileViewModel);
                    TempData[CommonValues.DownloadFileIdCookieKey] = fileViewModel.Id;

                    SetMessage(model.ErrorMessage, CommonValues.MessageSeverity.Fail);

                    return model;
                }
                else
                {
                    foreach (SparePartSaleDetailDetailModel sparePartSaleDetailDetailModel in listModels)
                    {
                        sparePartSaleDetailDetailModel.DealerPrice =
                            scBo.GetDealerPriceByDealerAndPart(
                                sparePartSaleDetailDetailModel.SparePartId.GetValue<int>(),
                                UserManager.UserInfo.GetUserDealerId()).Model;

                        sparePartSaleDetailDetailModel.ListPrice =
                            cbo.GetPriceByDealerPartVehicleAndType(
                                sparePartSaleDetailDetailModel.SparePartId.GetValue<int>()
                                , 0, UserManager.UserInfo.GetUserDealerId(), CommonValues.ListPriceLabel).Model;

                        DealerViewModel dealerViewModel = dealerBo.GetDealer(UserManager.UserInfo, UserManager.UserInfo.GetUserDealerId()).Model;
                        CountryVatRatioBL vatRatioBo = new CountryVatRatioBL();
                        sparePartSaleDetailDetailModel.VatRatio =
                            vatRatioBo.GetVatRatioByPartAndCountry(
                                sparePartSaleDetailDetailModel.SparePartId.GetValue<int>(), dealerViewModel.Country).Model;
                        DealerViewModel dealerModel = dealerBo.GetDealer(UserManager.UserInfo, UserManager.UserInfo.GetUserDealerId()).Model;
                        sparePartSaleDetailDetailModel.CurrencyCode = dealerModel.CurrencyCode;
                        sparePartSaleDetailDetailModel.PartSaleId = model.PartSaleId;
                        sparePartSaleDetailDetailModel.StatusId = (int)CommonValues.SparePartSaleDetailStatus.NoCollectOrder;
                        sparePartSaleDetailDetailModel.CommandType = CommonValues.DMLType.Insert;
                        bo.DMLSparePartSaleDetail(UserManager.UserInfo, sparePartSaleDetailDetailModel);
                        if (sparePartSaleDetailDetailModel.ErrorNo != 0)
                        {
                            SetMessage(sparePartSaleDetailDetailModel.ErrorMessage, CommonValues.MessageSeverity.Fail);

                            return model;
                        }
                    }
                }
                SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);
            }
            return model;
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSaleDetail.SparePartSaleDetailIndex, CommonValues.PermissionCodes.SparePartSaleDetail.SparePartSaleDetailCreate)]
        public ActionResult SparePartSaleDetailCreate(SparePartSaleDetailDetailModel model, HttpPostedFileBase excelFile)
        {
            ViewBag.IsSuccess = false;
            var spmbo = new SparePartSaleBL();
            var parentModel = spmbo.GetSparePartSale(UserManager.UserInfo, model.PartSaleId).Model;

            if (parentModel.SaleStatusLookVal == ((int)CommonValues.SparePartSaleStatus.NewRecord).ToString())
            {
                if (excelFile != null)
                {
                    ViewBag.IsExcelUpload = true;
                    model = InsertMultiple(model, excelFile);
                    ViewBag.IsSuccess = true;
                }
                else
                {
                    ViewBag.IsExcelUpload = false;
                    if (model.SparePartId != null && model.SparePartId != 0)
                    {
                        model = InsertSingle(model);
                    }

                    if (model.ErrorNo == 0)
                    {
                        ModelState.Clear();
                        ViewBag.IsSuccess = true;

                        #region Collect
                        if (Request.Params["action:SparePartSaleCollect"] != null)
                        {
                            int totalCount = 0;
                            SparePartSaleDetailListModel spsdListModel = new SparePartSaleDetailListModel()
                            {
                                PartSaleId = model.PartSaleId
                            };
                            SparePartSaleDetailBL spsdBo = new SparePartSaleDetailBL();
                            List<SparePartSaleDetailListModel> list = spsdBo.ListSparePartSaleDetails(UserManager.UserInfo, spsdListModel, out totalCount).Data;
                            if (totalCount != 0)
                            {
                                SparePartSaleController spsCont = new SparePartSaleController();
                                spsCont.SparePartSaleCollect(parentModel, list);

                                if (parentModel.ErrorNo != 0)
                                {
                                    if (model.SparePartId != null && model.SparePartId != 0)
                                    {
                                        model.SparePartId = null;
                                        SetMessage(
                                            MessageResource.SparePartSaleDetail_Warning_SuccessBut +
                                            parentModel.ErrorMessage,
                                            CommonValues.MessageSeverity.Fail);
                                    }
                                    else
                                    {
                                        SetMessage(
                                            parentModel.ErrorMessage,
                                            CommonValues.MessageSeverity.Fail);
                                    }
                                }
                                else
                                {
                                    parentModel = spmbo.GetSparePartSale(UserManager.UserInfo, model.PartSaleId).Model;
                                    parentModel.SaleStatusLookVal =
                                        ((int)CommonValues.SparePartSaleStatus.CollectOrderCreated).ToString();
                                    parentModel.CommandType = CommonValues.DMLType.Update;
                                    spmbo.DMLSparePartSale(UserManager.UserInfo, parentModel);
                                    if (parentModel.ErrorNo != 0)
                                        SetMessage(parentModel.ErrorMessage, CommonValues.MessageSeverity.Fail);
                                    else
                                    {
                                        foreach (SparePartSaleDetailListModel sparePartSaleDetailListModel in list)
                                        {
                                            SparePartSaleDetailDetailModel detailModel = new SparePartSaleDetailDetailModel
                                                ()
                                            {
                                                SparePartSaleDetailId =
                                                        sparePartSaleDetailListModel.SparePartSaleDetailId
                                            };
                                            detailModel = spsdBo.GetSparePartSaleDetail(UserManager.UserInfo, detailModel).Model;
                                            detailModel.CommandType = CommonValues.DMLType.Update;
                                            detailModel.StatusId =
                                                (int)CommonValues.SparePartSaleDetailStatus.CollectOrderCreated;
                                            spsdBo.DMLSparePartSaleDetail(UserManager.UserInfo, detailModel);
                                            if (detailModel.ErrorNo > 0)
                                            {
                                                SetMessage(detailModel.ErrorMessage, CommonValues.MessageSeverity.Fail);
                                                SetTotalValues(model, parentModel);
                                                return View(model);
                                            }
                                        }

                                        SetMessage(MessageResource.Global_Display_Success,
                                                   CommonValues.MessageSeverity.Success);
                                    }
                                }
                            }
                            else
                            {
                                SetMessage(MessageResource.SparePartSaleDetail_Warning_NoDetail,
                                           CommonValues.MessageSeverity.Fail);
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        SetMessage(model.ErrorMessage, CommonValues.MessageSeverity.Fail);
                    }
                }
            }
            else
            {
                SetMessage(MessageResource.SparePartSaleDetail_Warning_SaveCollectStatusNotValid, CommonValues.MessageSeverity.Fail);
            }
            parentModel = spmbo.GetSparePartSale(UserManager.UserInfo, model.PartSaleId).Model;
            ViewBag.PartSaleStatus = parentModel.SaleStatusLookVal;
            SetTotalValues(model, parentModel);
            if (parentModel.ErrorNo != 0)
            {
                SetMessage(parentModel.ErrorMessage, CommonValues.MessageSeverity.Fail);
            }

            int detailCount;
            SparePartBL spBo = new SparePartBL();
            SparePartIndexViewModel spModel = new SparePartIndexViewModel();
            SparePartSaleDetailListModel detailListModel = new SparePartSaleDetailListModel();
            detailListModel.PartSaleId = model.PartSaleId;
            SparePartSaleDetailBL detailBo = new SparePartSaleDetailBL();
            List<SparePartSaleDetailListModel> detailList = detailBo.ListSparePartSaleDetails(UserManager.UserInfo, detailListModel, out detailCount).Data;
            if (detailList.Any())
            {
                int partId = detailList.Select(f => f.SparePartId).First().GetValue<int>();
                spModel.PartId = partId;
                spBo.GetSparePart(UserManager.UserInfo, spModel);
                model.IsFirstPartOriginal = spModel.IsOriginal.GetValue<bool>();
            }

            return View(model);
        }
        #endregion

        #region Spare Part Sale Update
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSaleDetail.SparePartSaleDetailIndex, CommonValues.PermissionCodes.SparePartSaleDetail.SparePartSaleDetailUpdate)]
        public ActionResult SparePartSaleDetailUpdate(int sparePartsaleDetailId)
        {
            var bo = new SparePartSaleDetailBL();
            SetDefaults();

            var referenceModel = new SparePartSaleDetailDetailModel { SparePartSaleDetailId = sparePartsaleDetailId };

            if (sparePartsaleDetailId > 0 && sparePartsaleDetailId > 0)
            {
                referenceModel = bo.GetSparePartSaleDetail(UserManager.UserInfo, referenceModel).Model;
                if (referenceModel.SparePartId != null)
                {
                    DealerBL dealerBo = new DealerBL();
                    CommonBL cbo = new CommonBL();
                    SparePartBL spBo = new SparePartBL();
                    SparePartIndexViewModel spModel = new SparePartIndexViewModel();
                    spModel.PartId = referenceModel.SparePartId.GetValue<int>();
                    spBo.GetSparePart(UserManager.UserInfo, spModel);
                    referenceModel.SparePartName = spModel.PartCode + CommonValues.Slash + spModel.AdminDesc;

                    StockCardBL scBo = new StockCardBL();
                    referenceModel.DealerPrice = scBo.GetDealerPriceByDealerAndPart(spModel.PartId,
                                                                           UserManager.UserInfo.GetUserDealerId()).Model;

                    referenceModel.ListPrice = cbo.GetPriceByDealerPartVehicleAndType(spModel.PartId, 0,
                                                                             UserManager.UserInfo.GetUserDealerId(),
                                                                             CommonValues.ListPriceLabel).Model;

                    DealerViewModel dealerViewModel = dealerBo.GetDealer(UserManager.UserInfo, UserManager.UserInfo.GetUserDealerId()).Model;
                    CountryVatRatioBL vatRatioBo = new CountryVatRatioBL();
                    referenceModel.VatRatio = vatRatioBo.GetVatRatioByPartAndCountry(spModel.PartId, dealerViewModel.Country).Model;
                }

                int sparePartSaleId = referenceModel.PartSaleId;
                SparePartSaleBL spsBo = new SparePartSaleBL();
                SparePartSaleViewModel spsModel = spsBo.GetSparePartSale(UserManager.UserInfo, sparePartSaleId).Model;
                referenceModel.SparePartSaleWaybillId = spsModel.SparePartSaleWaybillId;
                SparePartSaleWaybillBL spswBo = new SparePartSaleWaybillBL();
                SparePartSaleWaybillViewModel spswModel = spswBo.GetSparePartSaleWaybill(UserManager.UserInfo, referenceModel.SparePartSaleWaybillId.GetValue<int>()).Model;
                referenceModel.SparePartSaleInvoiceId = spswModel.SparePartSaleInvoiceId;
            }
            return View(referenceModel);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSaleDetail.SparePartSaleDetailIndex,
            CommonValues.PermissionCodes.SparePartSaleDetail.SparePartSaleDetailUpdate)]
        public ActionResult SparePartSaleDetailUpdate(SparePartSaleDetailDetailModel viewModel)
        {
            DealerBL dealerBo = new DealerBL();
            var bo = new SparePartSaleDetailBL();
            SetDefaults();

            /*
             Ek bir kontrol olarak da, PLAN_QUANTITY değeri artırılıyorsa, artan miktar SO_DET_SEQ_NO doluysa, ilgili SALE_ORDER_DET.ORDER_QUANT - PLANNED_QUANT değerinden 
             * yüksek olamaz. Kullanıcıya bu durumda "Mevcut Satış Sipariş açık miktarı SALE_ORDER_DET.ORDER_QUANT - SALE_ORDER_DET.PLANNED_QUANT'dır. Bu sebeğle işleminiz 
             * gerçekleştirilemiyor" mesajı gösterilir. 
             */
            if (viewModel.SoDetSeqNo.HasValue())
            {
                SparePartSaleOrderDetailBL spsodBo = new SparePartSaleOrderDetailBL();
                SparePartSaleOrderDetailDetailModel spsodModel = new SparePartSaleOrderDetailDetailModel();
                spsodModel.SparePartSaleOrderDetailId = viewModel.SoDetSeqNo.GetValue<long>();
                spsodBo.GetSparePartSaleOrderDetail(UserManager.UserInfo, spsodModel);
                decimal controlQuantity = (spsodModel.OrderQuantity - spsodModel.PlannedQuantity).GetValue<decimal>();
                if (viewModel.PlanQuantity > controlQuantity)
                {
                    SetMessage(string.Format(MessageResource.SparePartSaleDetail_Warning_PlanQuantity, controlQuantity), CommonValues.MessageSeverity.Fail);
                    return View(viewModel);
                }
            }

            if (viewModel.PlanQuantity < (viewModel.PickQuantity + viewModel.PickedQuantity - viewModel.ReturnedQuantity))
            {
                SetMessage(MessageResource.SparePartSaleDetail_Warning_QuantityPickQuantity, CommonValues.MessageSeverity.Fail);
                return View(viewModel);
            }
            if (viewModel.DiscountPrice > viewModel.ListPrice)
            {
                SetMessage(MessageResource.SparePartSaleDetail_Warning_DiscountPriceListPriceControl, CommonValues.MessageSeverity.Fail);
                return View(viewModel);
            }

            if (ModelState.IsValid)
            {
                SparePartSaleBL masterBo = new SparePartSaleBL();
                SparePartSaleViewModel parentModel = masterBo.GetSparePartSale(UserManager.UserInfo, viewModel.PartSaleId).Model;
                DealerViewModel dealerModel = dealerBo.GetDealer(UserManager.UserInfo, UserManager.UserInfo.GetUserDealerId()).Model;
                viewModel.CurrencyCode = dealerModel.CurrencyCode;
                viewModel.CommandType = CommonValues.DMLType.Update;
                bo.DMLSparePartSaleDetail(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
                SetTotalValues(viewModel, parentModel);


                /*
                 SALE_ORDER_DET.PLANNED_QUANT aradaki fark kadar (azalsa da artsa da) SALE_ORDER_DET.PLANNED_QUANT 
                 * alanına yansıtılır. Günceleme sonrası SALE_ORDER_DET.ORDER_QUANT = PLANNED_QUANT olursa ve STATUS_LOOKVAL = 0 ise , STATUS_LOOKVAL = 1 olarak güncellenir. 
                 * SALE_ORDER_DET.ORDER_QUANT != PLANNED_QUANT ve STATUS_LOOKVAL = 1 ise, STATUS_LOOKVAL = 0 olarak güncellenir. 
                 */
                if (viewModel.SoDetSeqNo.HasValue())
                {
                    SparePartSaleOrderDetailBL spsodBo = new SparePartSaleOrderDetailBL();
                    SparePartSaleOrderDetailDetailModel spsodModel = new SparePartSaleOrderDetailDetailModel();
                    spsodModel.SparePartSaleOrderDetailId = viewModel.SoDetSeqNo.GetValue<long>();
                    spsodBo.GetSparePartSaleOrderDetail(UserManager.UserInfo, spsodModel);
                    spsodModel.CommandType = CommonValues.DMLType.Update;
                    spsodModel.PlannedQuantity = spsodModel.PlannedQuantity - viewModel.PlanQuantity;
                    if (spsodModel.OrderQuantity == spsodModel.PlannedQuantity && spsodModel.StatusId == (int)CommonValues.SparePartSaleOrderDetailStatus.OpenOrder)
                    {
                        spsodModel.StatusId = (int)CommonValues.SparePartSaleOrderDetailStatus.ClosedOrder;
                    }
                    else
                    {
                        spsodModel.StatusId = (int)CommonValues.SparePartSaleOrderDetailStatus.OpenOrder;
                    }
                    spsodBo.DMLSparePartSaleOrderDetail(UserManager.UserInfo, spsodModel);

                    /*
                     statüs güncelleme işlemi yapılırsa, ilgili kaydın
                     * (SALE_ORDER_DET'in) masterına gidilerei tüm DET kayıtlarının statüsü tamamı 1 mi veya en az 1 tane 0 statü var mı  AND MST.STATUS != 9 mu diye bakılır, eğer tamamı 1ken 
                     * yeni halinde 1 tane detay 1den farklı ise, MST.STATUS =4 olarak güncellenir, eğer tüm detayların statüsü 1 değilken son güncelleme sonrası tamamı 1'e çekildiyse 
                     * MST.STATUS = 5 olarak güncellenir. yani kısaca sipariş kapandı mı veya açıldı mı diye bakıyoruz ancak master sipariş kaydının iptal olmadığı durumda bakıyoruz ve duruma göre
                     * masterın statüsünü onaylı sipariş veya kapalı siparişe çekiyoruz.             
                     */
                    int totalCount;
                    SparePartSaleOrderDetailListModel spsdListModel = new SparePartSaleOrderDetailListModel();
                    spsdListModel.SoNumber = spsodModel.SoNumber;
                    List<SparePartSaleOrderDetailListModel> detailList = spsodBo.ListSparePartSaleOrderDetails(UserManager.UserInfo, spsdListModel, out totalCount).Data;
                    if (detailList.Any())
                    {
                        SparePartSaleOrderBL spsoBo = new SparePartSaleOrderBL();
                        SparePartSaleOrderViewModel spsoModel = spsoBo.GetSparePartSaleOrder(UserManager.UserInfo, spsodModel.SoNumber);
                        var control = detailList.Any(d => d.StatusId != (int)CommonValues.SparePartSaleOrderDetailStatus.ClosedOrder);

                        if (control)
                        {
                            spsoModel.StatusId = (int)CommonValues.SparePartSaleOrderStatus.ApprovedOrder;
                        }
                        else
                        {
                            spsoModel.StatusId = (int)CommonValues.SparePartSaleOrderStatus.ClosedOrder;
                        }
                        spsoModel.CommandType = CommonValues.DMLType.Update;
                        spsoBo.DMLSparePartSaleOrder(UserManager.UserInfo, spsoModel);
                    }
                }
            }
            return View(viewModel);
        }

        #endregion

        #region Spare Part Sale Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSaleDetail.SparePartSaleDetailIndex, CommonValues.PermissionCodes.SparePartSaleDetail.SparePartSaleDetailDelete)]
        public ActionResult SparePartSaleDetailDelete(int sparePartsaleDetailId, int sparePartSaleId)
        {
            ViewBag.HideElements = false;

            var parentBo = new SparePartSaleBL();
            var parentModel = new SparePartSaleViewModel();
            parentModel = parentBo.GetSparePartSale(UserManager.UserInfo, sparePartSaleId).Model;

            var bo = new SparePartSaleDetailBL();
            var model = new SparePartSaleDetailDetailModel
            {
                SparePartSaleDetailId = sparePartsaleDetailId
            };
            model = bo.GetSparePartSaleDetail(UserManager.UserInfo, model).Model;
            if (model.StatusId == (int)CommonValues.SparePartSaleDetailStatus.NoCollectOrder
                &&
                parentModel.SaleStatusLookVal.Equals(((int)CommonValues.SparePartSaleStatus.NewRecord).ToString()))
            {
                model.CommandType = CommonValues.DMLType.Delete;
                bo.DMLSparePartSaleDetail(UserManager.UserInfo, model);
                CheckErrorForMessage(model, true);
                ModelState.Clear();

                if (model.ErrorNo == 0)
                {
                    SetTotalValues(model, parentModel);
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                                                          MessageResource.Global_Display_Success);
                }
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
            }
            else
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.SparePartSaleDetail_Warning_StatusNotValid);
            }
        }
        #endregion

        #region Spare Part Sale Details
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSaleDetail.SparePartSaleDetailIndex, CommonValues.PermissionCodes.SparePartSaleDetail.SparePartSaleDetailDetails)]
        public ActionResult SparePartSaleDetailDetails(int sparePartsaleDetailId)
        {
            var referenceModel = new SparePartSaleDetailDetailModel { SparePartSaleDetailId = sparePartsaleDetailId };
            var bo = new SparePartSaleDetailBL();

            var model = bo.GetSparePartSaleDetail(UserManager.UserInfo, referenceModel).Model;

            DealerBL dealerBo = new DealerBL();
            DealerViewModel dealerModel = dealerBo.GetDealer(UserManager.UserInfo, UserManager.UserInfo.GetUserDealerId()).Model;
            string currencyCode = dealerBo.GetCountryCurrencyCode(dealerModel.Country).Model;
            model.DealerCurrencyCode = currencyCode;

            return View(model);
        }
        #endregion

        #region Create Purchase Order
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSaleDetail.SparePartSaleDetailOrder,
            CommonValues.PermissionCodes.SparePartSaleDetail.SparePartSaleDetailIndex)]
        public ActionResult CreatePurchaseOrder(string sparePartsaleDetailIdList)
        {
            ViewBag.HideElements = false;
            SparePartSaleBL parentBo = new SparePartSaleBL();
            SparePartSaleDetailBL bo = new SparePartSaleDetailBL();
            PurchaseOrderBL poBo = new PurchaseOrderBL();
            PurchaseOrderDetailBL podBo = new PurchaseOrderDetailBL();
            SparePartBL spBo = new SparePartBL();

            if (sparePartsaleDetailIdList.Length != 0)
            {
                List<string> idList = sparePartsaleDetailIdList.Split(',').ToList<string>();

                foreach (string idStr in idList)
                {
                    var model = new SparePartSaleDetailDetailModel();
                    model.SparePartSaleDetailId = idStr.GetValue<long>();
                    model = bo.GetSparePartSaleDetail(UserManager.UserInfo, model).Model;

                    model.CommandType = CommonValues.DMLType.Update;
                    model.PlanQuantity = model.PlanQuantity;

                    SparePartIndexViewModel spModel = new SparePartIndexViewModel() { PartId = model.SparePartId.GetValue<int>() };
                    spBo.GetSparePart(UserManager.UserInfo, spModel);
                    decimal packageQuantity = spModel.ShipQuantity.GetValue<decimal>();

                    SparePartSaleViewModel parentModel = parentBo.GetSparePartSale(UserManager.UserInfo, model.PartSaleId).Model;

                    if (parentModel.PoNumber == 0 || parentModel.PoNumber == null)
                    {
                        PurchaseOrderViewModel poModel = new PurchaseOrderViewModel();
                        poModel.IdDealer = UserManager.UserInfo.GetUserDealerId();
                        poModel.IdStockType = parentModel.StockTypeId;
                        poModel.Status = (int)CommonValues.PurchaseOrderStatus.NewRecord;
                        poModel.CommandType = CommonValues.DMLType.Insert;
                        poBo.DMLPurchaseOrder(UserManager.UserInfo, poModel);
                        if (poModel.ErrorNo != 0)
                        {
                            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, poModel.ErrorMessage);
                        }

                        parentModel.PoNumber = poModel.PoNumber;
                        parentModel.CommandType = CommonValues.DMLType.Update;
                        parentBo.DMLSparePartSale(UserManager.UserInfo, parentModel);
                        if (parentModel.ErrorNo > 0)
                        {
                            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, parentModel.ErrorMessage);
                        }
                    }

                    PurchaseOrderDetailViewModel podModel = new PurchaseOrderDetailViewModel();
                    podModel.PurchaseOrderNumber = parentModel.PoNumber.GetValue<int>();
                    podModel.PartId = model.SparePartId;
                    podModel.PackageQuantity = packageQuantity;
                    podModel.DesireQuantity = model.PlanQuantity;
                    podModel.OrderQuantity = model.PlanQuantity > packageQuantity ?
                        Math.Round((model.PlanQuantity % packageQuantity).GetValue<decimal>(), MidpointRounding.AwayFromZero) * packageQuantity
                        : packageQuantity;
                    podModel.OrderPrice = model.DealerPrice.GetValue<decimal>();
                    podModel.StatusId = (int)CommonValues.PurchaseOrderDetailStatus.NewRecord;
                    podModel.CommandType = CommonValues.DMLType.Insert;
                    podBo.DMLPurchaseOrderDetail(UserManager.UserInfo, podModel);
                    if (podModel.ErrorNo > 0)
                    {
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Error, podModel.ErrorMessage);
                    }

                    model.CommandType = CommonValues.DMLType.Update;
                    bo.DMLSparePartSaleDetail(UserManager.UserInfo, model);
                    if (model.ErrorNo > 0)
                    {
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
                    }
                }

                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                                                      MessageResource.Global_Display_Success);
            }
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                                                  MessageResource.SparePartSaleDetail_Warning_PartNotSelected);
        }
        #endregion

        #region Otokar Spare Part Sale
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSaleDetail.SparePartSaleDetailIndex)]
        public ActionResult OtokarSparePartSaleDetailIndex(int partSaleId, bool openCreatePopup = false)
        {
            SparePartSaleBL bo = new SparePartSaleBL();
            SparePartSaleViewModel parentModel = bo.GetSparePartSale(UserManager.UserInfo, partSaleId).Model;

            ViewBag.PartSaleStatus = parentModel.SaleStatusLookVal;

            var model = new OSparePartSaleDetailViewModel() { SparePartSaleId = partSaleId };
            ViewBag.OpenCreatePopup = openCreatePopup;

            return PartialView(model);
        }

        public ActionResult ListOtokarSparePartSaleDetailIndex([DataSourceRequest]DataSourceRequest request, OSparePartSaleDetailListModel hModel)
        {
            var bl = new SparePartSaleDetailBL();
            var model = new OSparePartSaleDetailListModel(request) { SparePartSaleId = hModel.SparePartSaleId };
            int totalCount = 0;

            var rValue = bl.ListOtokarSparePartSaleDetail(UserManager.UserInfo, model, out totalCount).Data;

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });
        }

        public ActionResult OtokarSparePartSaleDetailCreate(Int64 sparePartSaleId = 0)
        {
            var model = new OSparePartSaleDetailViewModel() { SparePartSaleId = sparePartSaleId };

            SparePartSaleBL spsBo = new SparePartSaleBL();
            SparePartSaleViewModel spsModel = spsBo.GetSparePartSale(UserManager.UserInfo, sparePartSaleId.GetValue<int>()).Model;
            model.StockTypeId = spsModel.StockTypeId.GetValue<int>();
            ViewBag.IsSuccessfullyAdded = true;
            return View(model);

        }

        public ActionResult ListOtokarSparePartDelivery([DataSourceRequest]DataSourceRequest request, OSparePartSaleDevlieryListModel hModel)
        {
            var bl = new SparePartSaleDetailBL();
            var model = new OSparePartSaleDevlieryListModel(request) { PartId = hModel.PartId };
            int totalCount = 0;

            var rValue = bl.ListOtokarSparePartDelivery(UserManager.UserInfo, model, out totalCount).Data;

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });
        }

        [HttpPost]
        public ActionResult OtokarSparePartSaleDetailCreate(OSparePartSaleDetailViewModel model)
        {
            ViewBag.IsSuccessfullyAdded = false;

            if (model.PartId != 0)
            {
                SparePartIndexViewModel spModel = new SparePartIndexViewModel();
                spModel.PartId = model.PartId.GetValue<int>();
                SparePartBL spBo = new SparePartBL();
                spBo.GetSparePart(UserManager.UserInfo, spModel);
                model.PartNameCode = spModel.PartCode + CommonValues.Slash + spModel.PartNameInLanguage;
            }

            SparePartSaleBL masterBo = new SparePartSaleBL();
            SparePartSaleViewModel masterModel = masterBo.GetSparePartSale(UserManager.UserInfo, model.SparePartSaleId.GetValue<int>()).Model;
            if (masterModel.SaleStatusLookVal == ((int)CommonValues.SparePartSaleStatus.NewRecord).ToString())
            {
                if (model.Price > model.DiscountPrice && model.DiscountPrice != 0)
                {
                    SetMessage(
                        string.Format(MessageResource.OtokarSparePartSaleDetail_Warning_DiscountPrice,
                                      model.DiscountPrice),
                        CommonValues.MessageSeverity.Fail);
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        var bl = new SparePartSaleDetailBL();
                        model.CommandType = CommonValues.DMLType.Insert;

                        bl.DMLOtokarSparePartSaleDetail(UserManager.UserInfo, model);

                        if (model.ErrorNo == 0)
                        {
                            model = new OSparePartSaleDetailViewModel() { SparePartSaleId = model.SparePartSaleId };

                            SparePartSaleBL spsBo = new SparePartSaleBL();
                            SparePartSaleViewModel spsModel = spsBo.GetSparePartSale(UserManager.UserInfo, model.SparePartSaleId.GetValue<int>()).Model;
                            model.StockTypeId = spsModel.StockTypeId.GetValue<int>();
                            SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);
                            ViewBag.IsSuccessfullyAdded = true;
                        }
                        else
                        {
                            SetMessage(model.ErrorMessage, CommonValues.MessageSeverity.Fail);
                        }
                    }
                }
            }
            else
            {
                SetMessage(MessageResource.OtokarSparePartSaleDetail_Warning_StateIsNotValid, CommonValues.MessageSeverity.Fail);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OtokarSparePartSaleDetailDelete(Int64 id)
        {
            var bl = new SparePartSaleDetailBL();
            var model = new OSparePartSaleDetailViewModel()
            {
                SparePartSaleDetailId = id
            };
            bl.GetOtokarSparePartSaleDetail(UserManager.UserInfo, model);

            SparePartSaleBL masterBo = new SparePartSaleBL();
            SparePartSaleViewModel masterModel = masterBo.GetSparePartSale(UserManager.UserInfo, model.SparePartSaleId.GetValue<int>()).Model;
            if (masterModel.SaleStatusLookVal == ((int)CommonValues.SparePartSaleStatus.NewRecord).ToString())
            {
                model.CommandType = CommonValues.DMLType.Delete;
                bl.DMLOtokarSparePartSaleDetail(UserManager.UserInfo, model);

                if (model.ErrorNo == 0)
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                                                          MessageResource.Global_Display_Success);
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
            }
            else
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.OtokarSparePartSaleDetail_Warning_StateIsNotValid);
            }
        }

        public ActionResult OtokarSparePartSaleDetailUpdate(Int64 id)
        {
            var bl = new SparePartSaleDetailBL();
            var model = new OSparePartSaleDetailViewModel() { SparePartSaleDetailId = id };
            bl.GetOtokarSparePartSaleDetail(UserManager.UserInfo, model);

            SparePartSaleBL spsBo = new SparePartSaleBL();
            SparePartSaleViewModel spsModel = spsBo.GetSparePartSale(UserManager.UserInfo, model.SparePartSaleId.GetValue<int>()).Model;
            model.StockTypeId = spsModel.StockTypeId.GetValue<int>();

            DealerBL dealerBo = new DealerBL();
            CountryVatRatioBL vatRatioBo = new CountryVatRatioBL();
            DealerViewModel dealerViewModel = dealerBo.GetDealer(UserManager.UserInfo, UserManager.UserInfo.GetUserDealerId()).Model;
            decimal vatRatio = vatRatioBo.GetVatRatioByPartAndCountry(model.PartId.GetValue<int>(), dealerViewModel.Country).Model;
            model.VatRatio = vatRatio;
            
            /**************************************************************************************************************************/
            // IM103556358 talebine istinaden fiyat bilgileri stock card'ından çekildi
            // OYA 31.10.2018
            StockCardBL scBo = new StockCardBL();
            StockCardViewModel scModel = new StockCardViewModel();
            scModel.DealerId = UserManager.UserInfo.GetUserDealerId();
            scModel.PartId = model.PartId;
            scBo.GetStockCard(UserManager.UserInfo, scModel);

            StockCardPriceListBL scpBo = new StockCardPriceListBL();
            StockCardPriceListModel scpListModel = new StockCardPriceListModel();
            scpListModel.StockCardId = scModel.StockCardId;
            scpListModel.PartId = model.PartId;
            scpListModel.DealerId = UserManager.UserInfo.GetUserDealerId();
            scpListModel.PriceId = dealerBo.GetCountryDefaultPriceList(dealerViewModel.Country).Model;
            model.DiscountPrice = scpBo.Get(UserManager.UserInfo, scpListModel, CommonValues.StockCardPriceType.D).Model.CostPrice;
            /**************************************************************************************************************************/

            SparePartBL spBo = new SparePartBL();
            model.Quantity = spBo.GetFreeQuantity(model.PartId.GetValue<int>(), UserManager.UserInfo.GetUserDealerId(), model.StockTypeId.GetValue<int>()).Model;

            return View(model);
        }
        [HttpPost]
        public ActionResult OtokarSparePartSaleDetailUpdate(OSparePartSaleDetailViewModel model)
        {
            if (model.PartId != 0)
            {
                SparePartIndexViewModel spModel = new SparePartIndexViewModel();
                spModel.PartId = model.PartId.GetValue<int>();
                SparePartBL spBo = new SparePartBL();
                spBo.GetSparePart(UserManager.UserInfo, spModel);
                model.PartNameCode = spModel.PartCode + CommonValues.Slash + spModel.PartNameInLanguage;
            }

            SparePartSaleBL masterBo = new SparePartSaleBL();
            SparePartSaleViewModel masterModel = masterBo.GetSparePartSale(UserManager.UserInfo, model.SparePartSaleId.GetValue<int>()).Model;
            if (masterModel.SaleStatusLookVal == ((int)CommonValues.SparePartSaleStatus.NewRecord).ToString())
            {
                if (model.Price > model.DiscountPrice && model.DiscountPrice != 0)
                {
                    SetMessage(
                         string.Format(MessageResource.OtokarSparePartSaleDetail_Warning_DiscountPrice, model.DiscountPrice), CommonValues.MessageSeverity.Fail);
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        var bl = new SparePartSaleDetailBL();
                        model.CommandType = CommonValues.DMLType.Update;

                        bl.DMLOtokarSparePartSaleDetail(UserManager.UserInfo, model);

                        if (model.ErrorNo == 0)
                        {
                            SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);
                        }
                        else
                        {
                            SetMessage(model.ErrorMessage, CommonValues.MessageSeverity.Fail);
                        }
                    }
                }
            }
            else
            {
                SetMessage(MessageResource.OtokarSparePartSaleDetail_Warning_StateIsNotValid, CommonValues.MessageSeverity.Fail);
            }
            return View(model);
        }

        #endregion
    }
}

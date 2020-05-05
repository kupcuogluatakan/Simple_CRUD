using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Transactions;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using Kendo.Mvc.Extensions;
using ODMSBusiness.Reports;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel;
using ODMSModel.Customer;
using ODMSModel.Dealer;
using ODMSModel.DealerSaleSparepart;
using ODMSModel.PurchaseOrder;
using ODMSModel.PurchaseOrderDetail;
using ODMSModel.PurchaseOrderType;
using ODMSModel.SparePartSaleOrder;
using ODMSModel.SparePartSaleOrderDetail;
using Permission = ODMSCommon.CommonValues.PermissionCodes.SparePartSaleOrder;
using ODMSBusiness.Business;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class SparePartSaleOrderController : ControllerBase
    {
        #region Private Methods
        private void FillComboBoxes()
        {
            ViewBag.YesNoList = CommonBL.ListYesNo().Data;
            ViewBag.StatusList = CommonBL.ListLookup(UserManager.UserInfo,CommonValues.LookupKeys.SaleOrderStatus).Data;
            List<SelectListItem> stockTypeList = CommonBL.ListStockTypes(UserManager.UserInfo).Data;
            ViewBag.StockTypeList = stockTypeList;
            List<SelectListItem> soTypeList = PurchaseOrderTypeBL.ListPurchaseOrderTypeAsSelectListItem(UserManager.UserInfo,UserManager.UserInfo.GetUserDealerId(), null, null, null, true).Data;
            ViewBag.SoTypeList = soTypeList;
            List<SelectListItem> firmTypeList = new List<SelectListItem>()
            {
                new SelectListItem() {Value = "1", Text = "Müşteri"},
                new SelectListItem() {Value = "2", Text = "Bayi/Servis"}
            };
            ViewBag.FirmTypeList = firmTypeList;
        }
        [ValidateAntiForgeryToken]
        public JsonResult GetStockType(string soTypeId)
        {
            if (string.IsNullOrEmpty(soTypeId))
            {
                return null;
            }
            else
            {
                PurchaseOrderTypeViewModel potModel = new PurchaseOrderTypeViewModel();
                potModel.PurchaseOrderTypeId = soTypeId.GetValue<int>();
                PurchaseOrderTypeBL potBl = new PurchaseOrderTypeBL();
                potModel = potBl.Get(UserManager.UserInfo,potModel).Model;

                return Json(new { StockTypeId = potModel.StockTypeId });
            }
        }
        #endregion

        #region Spare Part Sale OrderIndex

        [HttpGet]
        [AuthorizationFilter(Permission.SparePartSaleOrderIndex)]
        public ActionResult SparePartSaleOrderIndex(string soNumber)
        {
            FillComboBoxes();
            var model = new SparePartSaleOrderViewModel();
            model.SoNumber = soNumber;

            return View(model);
        }
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSaleOrder.SparePartSaleOrderIndex, CommonValues.PermissionCodes.SparePartSaleOrder.SparePartSaleOrderDetails)]
        public ActionResult ListSparePartSaleOrders([DataSourceRequest]DataSourceRequest request, SparePartSaleOrderViewModel model)
        {
            var bo = new SparePartSaleOrderBL();
            var referenceModel = new SparePartSaleOrderListModel(request)
            {
                SoNumber = model.SoNumber,
                CustomerId = model.CustomerId,
                DealerId = model.DealerId,
                SoTypeId = model.SoTypeId,
                StockTypeId = model.StockTypeId,
                PartCode = model.PartCode,
                PartName = model.PartName,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                StatusId = model.StatusId,
                FirmTypeId = model.FirmTypeId
            };
            int totalCnt;
            var returnValue = bo.ListSparePartSaleOrders(UserManager.UserInfo,referenceModel, out totalCnt).Model;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }
        #endregion

        #region Spare Part Sale Order Create
        [HttpGet]
        [AuthorizationFilter(Permission.SparePartSaleOrderIndex, Permission.SparePartSaleOrderCreate)]
        public ActionResult SparePartSaleOrderCreate()
        {
            ViewBag.IsCreate = true;
            FillComboBoxes();
            SparePartSaleOrderViewModel model = new SparePartSaleOrderViewModel();
            model.StatusId = (int)CommonValues.SparePartSaleOrderStatus.NewRecord;
            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.SparePartSaleOrderIndex, Permission.SparePartSaleOrderCreate)]
        public ActionResult SparePartSaleOrderCreate(SparePartSaleOrderViewModel model)
        {
            ViewBag.IsCreate = true;
            FillComboBoxes();

            model.DealerId = UserManager.UserInfo.GetUserDealerId();
            if (ModelState.IsValid)
            {
                var bo = new SparePartSaleOrderBL();

                PurchaseOrderTypeBL potBl = new PurchaseOrderTypeBL();
                PurchaseOrderTypeViewModel potModel = new PurchaseOrderTypeViewModel();
                potModel.PurchaseOrderTypeId = model.SoTypeId.GetValue<int>();
                potModel = potBl.Get(UserManager.UserInfo,potModel).Model;
                model.StockTypeId = potModel.StockTypeId;
                model.StatusId = (int)CommonValues.SparePartSaleOrderStatus.NewRecord;
                if (potModel.ManuelPriceAllow)
                {
                    model.IsFixedPrice = true;
                }
                else
                {
                    model.IsFixedPrice = model.IsProposal;
                }

                //if (model.IsProposal)
                //{
                //    model.StatusId = (int)CommonValues.SparePartSaleOrderStatus.NotApprovedProposal;
                //}
                //else
                //{
                //    model.StatusId = (int)CommonValues.SparePartSaleOrderStatus.NotApprovedOrder;
                //}
                model.OrderDate = DateTime.Now;
                model.CommandType = CommonValues.DMLType.Insert;
                bo.DMLSparePartSaleOrder(UserManager.UserInfo,model);
                CheckErrorForMessage(model, true);
                ModelState.Clear();
            }
            SparePartSaleOrderViewModel nModel = new SparePartSaleOrderViewModel();
            nModel.SoNumber = model.SoNumber;
            return View(nModel);
        }
        #endregion

        #region Spare Part Sale OrderUpdate
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSaleOrder.SparePartSaleOrderIndex, CommonValues.PermissionCodes.SparePartSaleOrder.SparePartSaleOrderUpdate)]
        public ActionResult SparePartSaleOrderUpdate(string soNumber)
        {
            ViewBag.IsCreate = false;
            FillComboBoxes();
            var model = new SparePartSaleOrderViewModel { SoNumber = soNumber };
            if (!soNumber.HasValue()) return View(model);
            var bo = new SparePartSaleOrderBL();
            model = bo.GetSparePartSaleOrder(UserManager.UserInfo,soNumber);

            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSaleOrder.SparePartSaleOrderIndex,
            CommonValues.PermissionCodes.SparePartSaleOrder.SparePartSaleOrderUpdate)]
        public ActionResult SparePartSaleOrderUpdate(SparePartSaleOrderViewModel viewModel)
        {
            ViewBag.IsCreate = false;
            FillComboBoxes();

            var sparePartbo = new SparePartSaleOrderBL();

            if (ModelState.IsValid)
            {
                PurchaseOrderTypeBL potBl = new PurchaseOrderTypeBL();
                PurchaseOrderTypeViewModel potModel = new PurchaseOrderTypeViewModel();
                potModel.PurchaseOrderTypeId = viewModel.SoTypeId.GetValue<int>();
                potModel = potBl.Get(UserManager.UserInfo,potModel).Model;
                viewModel.StockTypeId = potModel.StockTypeId;
                if (potModel.ManuelPriceAllow)
                {
                    viewModel.IsFixedPrice = true;
                }
                else
                {
                    viewModel.IsFixedPrice = viewModel.IsProposal;
                }

                if (viewModel.IsProposal)
                {
                    viewModel.StatusId = (int)CommonValues.SparePartSaleOrderStatus.NotApprovedProposal;
                }
                else
                {
                    viewModel.StatusId = (int)CommonValues.SparePartSaleOrderStatus.NotApprovedOrder;
                }

                viewModel.CommandType = CommonValues.DMLType.Update;
                sparePartbo.DMLSparePartSaleOrder(UserManager.UserInfo,viewModel);
                CheckErrorForMessage(viewModel, true);
            }
            return View(viewModel);
        }
        #endregion

        #region Spare Part Sale OrderDetails
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSaleOrder.SparePartSaleOrderIndex, CommonValues.PermissionCodes.SparePartSaleOrder.SparePartSaleOrderDetails)]
        public ActionResult SparePartSaleOrderDetails(string soNumber)
        {
            var bo = new SparePartSaleOrderBL();
            var model = bo.GetSparePartSaleOrder(UserManager.UserInfo,soNumber);
            CheckErrorForMessage(model, false);
            return View(model);
        }
        #endregion

        #region Spare Part Sale OrderCancel

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSaleOrder.SparePartSaleOrderIndex,
            CommonValues.PermissionCodes.SparePartSaleOrder.SparePartSaleOrderCancel)]
        public ActionResult SparePartSaleOrderCancel(string soNumber)
        {

            List<PurchaseOrderDetailListModel> podDetailList = new List<PurchaseOrderDetailListModel>();
            SparePartSaleOrderBL orderBo = new SparePartSaleOrderBL();
            PurchaseOrderBL poBo = new PurchaseOrderBL();
            PurchaseOrderViewModel poModel = new PurchaseOrderViewModel();
            PurchaseOrderDetailListModel podModel = new PurchaseOrderDetailListModel();
            PurchaseOrderDetailBL podBo = new PurchaseOrderDetailBL();
            var sparePartbo = new SparePartSaleOrderBL();
            SparePartSaleOrderViewModel viewModel = sparePartbo.GetSparePartSaleOrder(UserManager.UserInfo,soNumber);
            SparePartSaleOrderDetailListModel detailListModel = new SparePartSaleOrderDetailListModel();
            int detailCount = 0;
            string link = string.Empty;
            SparePartSaleOrderDetailBL detailBo = new SparePartSaleOrderDetailBL();
            DealerBL dealerBo = new DealerBL();
            DealerViewModel dealerModel = new DealerViewModel();

            /*
             İptal İşlemi; Yeni Kayıt, Kapalı Sipariş ve İptal Sipariş statüsü dışındaki (SALE_ORDER_MST.STATUS_LOOKVAL) tüm siparişlerde yapılabilir.
             * İptal işlemi yapıldığında, master kaydın statüsü 9 - İptal Sipariş  statüsüne çekilir. 
            Aynı buton İptal Sipariş statüsündeki kayıtlarda da kullanılır, butona basıldığında, SALE_ORDER_MST_STATUS_LOG tablosundan bu siparişe ait en yeni tarihli kayıt bulunur
             * ve master kaydın statüsü, bu statüye set edilir. (İptal işleminin iptali). İptal işleminin iptalinin yapılabilmesi için bağlı satınalma siparişi olmamalı (SALE_ORDER_DET.PO_DET_SEQ_NO hepsi null olmalı) 
             * yada bağlı satınalma siparişinin statüsü iptalden farklı olmalı. (PURCHASE_ORDER_MST.STATUS_LOOKVAL <> 9-İptal Sipariş). 
             * Bu durum mevcut ise, "Bağlı satınalma siparişi de iptal edildiğinden, iptal işlemi kaldırılamaz !" mesajı verilmeli ve işlem yapılmamalı.
            İptal işlemi yapılabiliyor ve bağlı satınalma siparişi var ise, satınalma siparişinin sahibi olan DEALER'a CNTCT_EMAIL adresine,
             * "PURCHASE_ORDER_MST.PO_NUMBER nolu siparişiniz, DEALER.DEALER_NAME (Satış siparişinin dealer bilgisi) tarafından iptal edilmiştir,
             * Satınalma siparişiniz takip eden gün içinde otomaik iptal edilecektir." başlığı "satınalma sipariş iptali" olacak. 
             * Altında da ilgili satınalma siparişine direk erişebileceği link olacak. 
            İptal işleminin kaldırılımasında da yukardaki mail benzeri mail atılacak, başlığı "satınalma sipariş iptalinin kaldırılması" olacak. 
             * mesaj, "PURCHASE_ORDER_MST.PO_NUMBER nolu siparişinizin, DEALER.DEALER_NAME (Satış siparişinin dealer bilgisi) iptal işlemi kaldırılmıştır" olacak. 
             * Altında da ilgili satınalma siparişine direk erişebileceği link olacak.
             */
            detailListModel.SoNumber = soNumber;
            List<SparePartSaleOrderDetailListModel> detailList = detailBo.ListSparePartSaleOrderDetails(UserManager.UserInfo,detailListModel, out detailCount).Data;
            if (detailList.Any())
            {
                podModel.SAPOfferNo = detailList.First().SoNumber;
                podDetailList = podBo.ListPurchaseOrderDetails(UserManager.UserInfo, podModel, out detailCount).Data;
                int dealerId = poModel.IdDealer.GetValue<int>();
                dealerModel = dealerBo.GetDealer(UserManager.UserInfo, dealerId).Model;

                if (podDetailList.Any())
                {
                    int poNumber = podDetailList.First().PurchaseOrderNumber;
                    poModel.PoNumber = poNumber;
                    poBo.GetPurchaseOrder(UserManager.UserInfo, poModel);
                }
                link = CommonBL.GetGeneralParameterValue("DMS_ROOT_URL").Model + "PurchaseOrder/PurchaseOrderIndex/" + poModel.PoNumber;
            }

            if (viewModel.StatusId == (int)CommonValues.SparePartSaleOrderStatus.CancelledOrder)
            {
                if (detailList.Count > 0)
                {
                    var poDetailList = detailList.Where(e => e.PurchaseOrderDetailSeqNo != null);
                    if (poDetailList.Any() || (poModel.Status == (int)CommonValues.PurchaseOrderStatus.CanceledPurhcaseOrder))
                    {
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.SparePartSaleOrder_Warning_CancelledPurchaseOrder);
                    }
                }

                string latestStatus = orderBo.GetSparePartSaleOrderLatestStatus(soNumber).Model;
                viewModel.CommandType = CommonValues.DMLType.Update;
                viewModel.StatusId = latestStatus.GetValue<int>();
                sparePartbo.DMLSparePartSaleOrder(UserManager.UserInfo, viewModel);

                if (viewModel.ErrorNo > 0)
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);

                if (podDetailList.Any())
                {
                    CommonBL.SendDbMail(dealerModel.ContactEmail, MessageResource.SparePartSaleOrder_MailSubject_RemoveCancel,
                        string.Format(MessageResource.SparePartSaleOrder_MailBody_RemoveCancel, poModel.PoNumber, dealerModel.Name) + CommonValues.NewLine + link);
                }
            }
            else
            {
                viewModel.CommandType = CommonValues.DMLType.Update;
                viewModel.StatusId = (int)CommonValues.SparePartSaleOrderStatus.CancelledOrder;
                sparePartbo.DMLSparePartSaleOrder(UserManager.UserInfo, viewModel);

                if (viewModel.ErrorNo > 0)
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);

                if (podDetailList.Any())
                {
                    CommonBL.SendDbMail(dealerModel.ContactEmail, MessageResource.SparePartSaleOrder_MailSubject_Cancel,
                        string.Format(MessageResource.SparePartSaleOrder_MailBody_Cancel, poModel.PoNumber, dealerModel.Name) + CommonValues.NewLine + link);
                }
            }

            return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
        }

        #endregion

        #region Spare Part Sale OrderDelete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSaleOrder.SparePartSaleOrderIndex,
            CommonValues.PermissionCodes.SparePartSaleOrder.SparePartSaleOrderDelete)]
        public ActionResult DeleteSparePartSaleOrder(string soNumber)
        {
            SparePartSaleOrderViewModel viewModel = new SparePartSaleOrderViewModel() { SoNumber = soNumber };
            var SparePartSaleOrderBo = new SparePartSaleOrderBL();

            viewModel.CommandType = CommonValues.DMLType.Delete;
            SparePartSaleOrderBo.DMLSparePartSaleOrder(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                                                      MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
        }

        #endregion

        #region Spare Part Sale Order Collect

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSaleOrder.SparePartSaleOrderIndex,
            CommonValues.PermissionCodes.SparePartSaleOrder.SparePartSaleOrderCollect)]
        public ActionResult SparePartSaleOrderCollect(string soNumber)
        {
            using (var ts = new TransactionScope())
            {

                int detailCount = 0;
                SparePartSaleOrderBL somBo = new SparePartSaleOrderBL();
                SparePartSaleOrderDetailBL sodBo = new SparePartSaleOrderDetailBL();
                SparePartSaleOrderViewModel somModel = somBo.GetSparePartSaleOrder(UserManager.UserInfo,soNumber);
                SparePartSaleOrderDetailListModel sodModel = new SparePartSaleOrderDetailListModel();
                sodModel.SoNumber = soNumber;
                List<SparePartSaleOrderDetailListModel> sodList = sodBo.ListSparePartSaleOrderDetails(UserManager.UserInfo,sodModel,out detailCount).Data;
                int saleDealerId = somModel.DealerId.GetValue<int>();
                DealerBL saleDealerBo = new DealerBL();
                DealerViewModel saleDealerModel = saleDealerBo.GetDealer(UserManager.UserInfo,saleDealerId).Model;

                #region  Collect Mail Table

                /*
                Sipariş içeriği;
                Gönderilecek maillerde ilgili bir PURCHASE_ORDER var ise bunun linki paylaşılacaktır, ilgili satınalma siparişi yok ise, aşağıdaki yapıda SALE_ORDER bilgileri paylaşılacaktır.
                Tablo yapısı ile gösterilecektir
                Sipariş No : SO_NUMBER (yanına Bayi Satış Sipariş No bilgisidir yazılacak)
                Sipariş Tipi : ID_SO_TYPE değeri
                Stok Tipi : ID_STOCK_TYPE değeri
                Sipariş Tarihi : ORDER_DATE

                Detay tablo;

                Parça Kodu ID_PART ile SPARE_PART.PART_CODE bulunur  ,
                Parça Adı  ID_PART ve LANG ile SPARE_PART_LANG.PART_NAME bulunur,
                Sipariş Miktarı ORDER_QUANT,
                Liste Fiyatı LIST_PRICE + Dealer Ülke para birimi,
                İndirim Oranı % ISNULL(APPLIED_DISCOUNT_RATIO,LIST_DISCOUNT_RATIO),
                Sipariş Birim Fiyatı ISNULL(CONFIRM_PRICE,ORDER_PRICE) + Dealer Ülke para birimi
             */
                StringBuilder mailTable = new StringBuilder();

                string collectInfo = string.Format(MessageResource.SparePartSaleOrder_MailBody_CollectDetail,
                    somModel.SoNumber, somModel.SoTypeName, somModel.StockTypeName
                    , somModel.OrderDate);
                mailTable.Append(collectInfo);
                mailTable.Append(CommonValues.NewLine);
                string collectHeader = MessageResource.SparePartSaleOrder_MailBody_CollectDetailHeader;
                mailTable.Append(collectHeader);
                mailTable.Append(CommonValues.NewLine);

                foreach (SparePartSaleOrderDetailListModel detailModel in sodList)
                {
                    string collectRow = string.Format(MessageResource.SparePartSaleOrder_MailBody_CollectDetailRow,
                        detailModel.SparePartCode, detailModel.SparePartName,
                        detailModel.OrderQuantity, detailModel.ListPrice + saleDealerModel.CurrencyCode,
                        detailModel.AppliedDiscountRatio ?? detailModel.ListDiscountRatio,
                        (detailModel.ConfirmPrice ?? detailModel.OrderPrice) + saleDealerModel.CurrencyCode);
                    mailTable.Append(collectRow);
                    mailTable.Append(CommonValues.NewLine);
                }

                #endregion

                var priceControl =
                    sodList.All(
                        d =>
                            (d.ConfirmPrice.GetValueOrDefault(0) > 0) &&
                            (d.AppliedDiscountRatio.GetValueOrDefault(0) > 0));
                var poControl =
                    sodList.Any(
                        d => string.IsNullOrEmpty(d.PurchaseOrderDetailSeqNo));

                PurchaseOrderBL poBo = new PurchaseOrderBL();
                PurchaseOrderDetailBL podBo = new PurchaseOrderDetailBL();
                PurchaseOrderViewModel poModel = new PurchaseOrderViewModel();
                PurchaseOrderDetailViewModel podModel = new PurchaseOrderDetailViewModel();
                podModel.PurchaseOrderDetailSeqNo =
                    sodList.Select(f => f.PurchaseOrderDetailSeqNo).First().GetValue<long>();
                podBo.GetPurchaseOrderDetail(UserManager.UserInfo,podModel);
                poModel.PoNumber = podModel.PurchaseOrderNumber;
                poBo.GetPurchaseOrder(UserManager.UserInfo,poModel);

                #region Not Approved Proposal

                /*
             SALE_ORDER_MST.STATUS_LOOKVAL = 0 - Onaysız Teklif ise, detay tüm kayıtlar için CONFIRM_PRICE ve APPLIED_DISCOUNT_RATIO alanları dolu ise master kayıt 
             * SALE_ORDER_MST.STATUS_LOOKVAL = 1 - Onaylı Teklif statüsüne çekilir, PO_DET_SEQ_NO alanları dolu ise, 
             * ilgili PURCHASE_ORDER_MST.SUPPLIER_DEALER_CONFIRM_LOOKVAL = 2 - Onaylı Teklif konumuna getirilir, 
             * PURCHASE_ORDER_DET.CONFIRM_PRICE = SALE_ORDER_DET.CONFIRM_PRICE ve PURCHASE_ORDER_DET.APPLIED_DISCOUNT_RATIO = SALE_ORDER_DET.APPLIED_DISCOUNT_RATIO 
             * yapılır.  SALE_ORDER_DET. PO_DET_SEQ_NO alanları dolu ise (aynı değere sahiptirler), PURCHASE_ORDER_MST üzerindeki ID_DEALER Servis/Bayisinin tanımlı mail adresine
             * .PO_NUMBER nolu satınalma talebiniz tekliflendirilmiştir, detay bilgilerine ulaşmak için aşağıdaki linki tıklayınız <link-ilgili purchase order'a direk erişebileceği link> e-maili gönderilir. 
             * Koşulda belirlenen alanlar tüm satırlar için dolu değil ise "Tüm teklif kalemleri için fiyat oluşturulmamıştır !" mesajı verilir.
             */
                if (somModel.StatusId == (int)CommonValues.SparePartSaleOrderStatus.NotApprovedProposal)
                {
                    //if (poControl)
                    //{
                    //    return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                    //        MessageResource.SparePartSaleOrder_Warning_NoConfirmValues);
                    //}
                    if (!priceControl)
                    {
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                            MessageResource.SparePartSaleOrder_Warning_NoPurchaseOrderDetail);
                    }

                    somModel.CommandType = CommonValues.DMLType.Update;
                    somModel.StatusId = (int)CommonValues.SparePartSaleOrderStatus.ApprovedProposal;
                    somBo.DMLSparePartSaleOrder(UserManager.UserInfo,somModel);

                    poModel.CommandType = CommonValues.DMLType.Update;
                    poModel.SupplierDealerConfirm = (int)CommonValues.SupplierDealerConfirmType.ApprovedProposal;
                    poModel.Status = (int)CommonValues.PurchaseOrderStatus.NewRecord;
                    poBo.DMLPurchaseOrder(UserManager.UserInfo,poModel);

                    PurchaseOrderDetailViewModel poDetail = new PurchaseOrderDetailViewModel();
                    foreach (SparePartSaleOrderDetailListModel soDetail in sodList.Where(c => c.PurchaseOrderDetailSeqNo.GetValue<long>() > 0))
                    {
                        poDetail.PurchaseOrderDetailSeqNo = soDetail.PurchaseOrderDetailSeqNo.GetValue<long>();
                        podBo.GetPurchaseOrderDetail(UserManager.UserInfo,poDetail);

                        poDetail.CommandType = CommonValues.DMLType.Update;
                        poDetail.ConfirmPrice = soDetail.ConfirmPrice;
                        poDetail.AppliedDiscountRatio = soDetail.AppliedDiscountRatio;
                        poDetail.SpecialExplanation = soDetail.SpecialExplanation;
                        podBo.DMLPurchaseOrderDetail(UserManager.UserInfo,poDetail);
                    }
                    if (!poControl)
                    {
                        int dealerId = poModel.IdDealer.GetValue<int>();

                        DealerBL dealerBo = new DealerBL();
                        DealerViewModel dealerModel = dealerBo.GetDealer(UserManager.UserInfo,dealerId).Model;
                        string subject = MessageResource.SparePartSaleOrder_MailSubject_NotApprovedProposalCollect;
                        string link = CommonBL.GetGeneralParameterValue("DMS_ROOT_URL").Model +
                                      "PurchaseOrder/PurchaseOrderIndex/" + poModel.PoNumber;
                        string body =
                            string.Format(MessageResource.SparePartSaleOrder_MailBody_NotApprovedProposalCollect,
                                poModel.PoNumber, dealerModel.Name) +
                            MessageResource.SparePartSaleOrder_MailBody_Link + CommonValues.NewLine + link;

                        CommonBL.SendDbMail(dealerModel.ContactEmail, subject, body);
                    }
                }

                #endregion

                #region Approved Proposal

                /*SALE_ORDER_MST.STATUS_LOOKVAL = 1 - Onaylı Teklif ise, ilgili purchase order var ise (detay kayıtlarda SALE_ORDER_DET.PO_DET_SEQ_NO var ise) buton display edilmez. 
             * Yok ise, confirm alanaları dolu ise (CONFIRM_PRICE ve APPLIED_DISCOUNT_RATIO) SALE_ORDER_MST.STATUS_LOOKVAL = 4 - Onaylı Sipariş statüsüne çekilir. 
             * Müşteri bir DEALER ise sipariş içeriği de olacak şekilde (Aşağıda tariflenecek) DEALER üzerindeki mail adresine mail atılır.*/
                if (somModel.StatusId == (int)CommonValues.SparePartSaleOrderStatus.ApprovedProposal && poControl)
                {
                    if (priceControl)
                    {
                        somModel.CommandType = CommonValues.DMLType.Update;
                        somModel.StatusId = (int)CommonValues.SparePartSaleOrderStatus.ApprovedOrder;
                        somBo.DMLSparePartSaleOrder(UserManager.UserInfo,somModel);

                        int customerId = somModel.CustomerId.GetValue<int>();
                        CustomerIndexViewModel cModel = new CustomerIndexViewModel();
                        cModel.CustomerId = customerId;
                        CustomerBL cBo = new CustomerBL();
                        cBo.GetCustomer(UserManager.UserInfo, cModel);
                        if (cModel.IsDealerCustomer)
                        {
                            int dealerId = cModel.DealerId.GetValue<int>();
                            DealerBL dealerBo = new DealerBL();
                            DealerViewModel dealerModel = dealerBo.GetDealer(UserManager.UserInfo, dealerId).Model;
                            string subject = MessageResource.SparePartSaleOrder_MailSubject_ApprovedProposalCollect;
                            string body =
                                string.Format(MessageResource.SparePartSaleOrder_MailBody_ApprovedProposalCollect) +
                                CommonValues.NewLine + mailTable;

                            CommonBL.SendDbMail(dealerModel.ContactEmail, subject, body);
                        }
                    }
                    else
                    {
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                            MessageResource.SparePartSaleOrder_Warning_NoConfirmValues);
                    }
                }

                #endregion

                #region New Record

                /*SALE_ORDER_MST.STATUS_LOOKVAL = 2 - Yeni Kayıt ise, tamamla butonu ile statü (4 - Onaylı Sipariş) yapılır. IS_PRICE_FIXED = 0 ise ilgili siparişin tüm detay kayıtları güncellenir,
             * LIST_PRICE, ORDER_PRICE  tekrar hesaplanır (ORDER_PRICE=LIST_PRICE*((100-ISNULL(APPLIED_DISCOUNT_RATIO, LIST_DISCOUNT_RATIO) /100). Müşteri DEALER ise sipariş içeriği de 
             * olacak şekilde mail gönderilir. */
                if (somModel.StatusId == (int)CommonValues.SparePartSaleOrderStatus.NewRecord)
                {
                    somModel.CommandType = CommonValues.DMLType.Update;
                    somModel.StatusId = (int)CommonValues.SparePartSaleOrderStatus.ApprovedOrder;
                    somBo.DMLSparePartSaleOrder(UserManager.UserInfo, somModel);

                    if (somModel.IsFixedPrice == false)
                    {
                        UpdateSparePartSaleOrderDetailPrice(sodList);
                    }

                    int customerId = somModel.CustomerId.GetValue<int>();
                    CustomerIndexViewModel cModel = new CustomerIndexViewModel();
                    cModel.CustomerId = customerId;
                    CustomerBL cBo = new CustomerBL();
                    cBo.GetCustomer(UserManager.UserInfo, cModel);
                    if (cModel.IsDealerCustomer)
                    {
                        int dealerId = cModel.DealerId.GetValue<int>();
                        DealerBL dealerBo = new DealerBL();
                        DealerViewModel dealerModel = dealerBo.GetDealer(UserManager.UserInfo, dealerId).Model;
                        string subject = MessageResource.SparePartSaleOrder_MailSubject_NewRecordCollect;
                        string body = string.Format(MessageResource.SparePartSaleOrder_MailBody_NewRecordCollect) +
                                      CommonValues.NewLine + mailTable;

                        CommonBL.SendDbMail(dealerModel.ContactEmail, subject, body);
                    }
                }



                #endregion

                #region NotApproved Order

                /*SALE_ORDER_MST.STATUS_LOOKVAL = 3 - Onaysız Sipariş ise, IS_PRICE_FIXED = 1 ise Confirm alanları kontrol edilir, dolu değil ise uyarı verilir. 
             * (Sipariş detayları için fiyat teyidi yapılmamıştır !), IS_PRICE_FIXED = 0 ise, detaylar bir üst case de olduğu gibi güncellenir. sipariş içeriği de olacak şekilde siparişi açan 
             * DEALER' a mail gönderilir. SALE_ORDER_MST.STATUS_LOOKVAL = 4 - Onaylı Sipariş yapılır.
            İlgili PURCHASE_ORDER_MST.STATUS_LOOKVAL = 1- Açık Sipariş SUPPLIER_DEALER_CONFIRM_LOOKVAL = 4 - Onaylı Sipariş konumuna getirilir,
             * PURCHASE_ORDER_DET.CONFIRM_PRICE = ISNULL(SALE_ORDER_DET.CONFIRM_PRICE,SALE_ORDER_DET.ORDER_PRICE) ve 
             * PURCHASE_ORDER_DET.APPLIED_DISCOUNT_RATIO = ISNULL(SALE_ORDER_DET.APPLIED_DISCOUNT_RATIO,SALE_ORDER_DET.LIST_DISCOUNT_RATIO) yapılır. 
             * PURCHASE_ORDER_MST üzerindeki ID_DEALER Servis/Bayisinin tanımlı mail adresine .PO_NUMBER nolu satınalma talebiniz onaylanmıştır,
             * detay bilgilerine ulaşmak için aşağıdaki linki tıklayınız <link-ilgili purchase order'a direk erişebileceği link> e-maili gönderilir.  */
                if (somModel.StatusId == (int)CommonValues.SparePartSaleOrderStatus.NotApprovedOrder)
                {
                    if (!priceControl)
                    {
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                            MessageResource.SparePartSaleOrder_Warning_NoConfirmValues);
                    }
                    else
                    {
                        UpdateSparePartSaleOrderDetailPrice(sodList);
                        somModel.CommandType = CommonValues.DMLType.Update;
                        somModel.StatusId = (int)CommonValues.SparePartSaleOrderStatus.ApprovedOrder;
                        somBo.DMLSparePartSaleOrder(UserManager.UserInfo, somModel);

                        if (!poControl)
                        {
                            int customerId = somModel.CustomerId.GetValue<int>();
                            CustomerIndexViewModel cModel = new CustomerIndexViewModel();
                            cModel.CustomerId = customerId;
                            CustomerBL cBo = new CustomerBL();
                            cBo.GetCustomer(UserManager.UserInfo, cModel);
                            if (cModel.IsDealerCustomer)
                            {
                                int dealerId = cModel.DealerId.GetValue<int>();
                                DealerBL dealerBo = new DealerBL();
                                DealerViewModel dealerModel = dealerBo.GetDealer(UserManager.UserInfo, dealerId).Model;
                                string subject = MessageResource.SparePartSaleOrder_MailSubject_NotApprovedOrderCollect;
                                string body = string.Format(
                                    MessageResource.SparePartSaleOrder_MailBody_NotApprovedOrderCollect, soNumber);

                                CommonBL.SendDbMail(dealerModel.ContactEmail, subject, body);
                            }
                            poModel.CommandType = CommonValues.DMLType.Update;
                            poModel.Status = (int)CommonValues.PurchaseOrderStatus.OpenPurchaseOrder;
                            poModel.SupplierDealerConfirm =
                                (int)CommonValues.SupplierDealerConfirmType.ApprovedOrder;
                            poBo.DMLPurchaseOrder(UserManager.UserInfo, poModel);

                            PurchaseOrderDetailViewModel poDetail = new PurchaseOrderDetailViewModel();
                            foreach (SparePartSaleOrderDetailListModel soDetail in sodList)
                            {
                                poDetail.PurchaseOrderDetailSeqNo = soDetail.PurchaseOrderDetailSeqNo.GetValue<long>();
                                podBo.GetPurchaseOrderDetail(UserManager.UserInfo, poDetail);

                                poDetail.CommandType = CommonValues.DMLType.Update;
                                poDetail.ConfirmPrice = soDetail.ConfirmPrice ?? soDetail.OrderPrice;
                                poDetail.AppliedDiscountRatio = soDetail.AppliedDiscountRatio ??
                                                                soDetail.ListDiscountRatio;
                                poDetail.SpecialExplanation = soDetail.SpecialExplanation;
                                podBo.DMLPurchaseOrderDetail(UserManager.UserInfo, poDetail);
                            }

                            int poDealerId = poModel.IdDealer.GetValue<int>();
                            DealerViewModel poDealerModel = saleDealerBo.GetDealer(UserManager.UserInfo, poDealerId).Model;
                            string poSubject = MessageResource.SparePartSaleOrder_MailSubject_NotApprovedOrderCollect;
                            string link = CommonBL.GetGeneralParameterValue("DMS_ROOT_URL").Model +
                                          "PurchaseOrder/PurchaseOrderIndex/" + poModel.PoNumber;
                            string poBody =
                                string.Format(MessageResource.SparePartSaleOrder_MailBody_NotApprovedOrderCollect,
                                    poModel.PoNumber, saleDealerModel.Name) +
                                MessageResource.SparePartSaleOrder_MailBody_Link + CommonValues.NewLine + link;

                            CommonBL.SendDbMail(poDealerModel.ContactEmail, poSubject, poBody);
                        }
                        else
                        {
                            int customerId = somModel.CustomerId.GetValue<int>();
                            CustomerIndexViewModel cModel = new CustomerIndexViewModel();
                            cModel.CustomerId = customerId;
                            CustomerBL cBo = new CustomerBL();
                            cBo.GetCustomer(UserManager.UserInfo, cModel);
                            if (cModel.IsDealerCustomer)
                            {
                                int dealerId = cModel.DealerId.GetValue<int>();
                                DealerBL dealerBo = new DealerBL();
                                DealerViewModel dealerModel = dealerBo.GetDealer(UserManager.UserInfo, dealerId).Model;

                                string soSubject =
                                    MessageResource.SparePartSaleOrder_MailSubject_NotApprovedOrderCollect;
                                string soBody =
                                    string.Format(MessageResource.SparePartSaleOrder_MailBody_NotApprovedOrderCollect,
                                        somModel.SoNumber, saleDealerModel.Name) +
                                    CommonValues.NewLine + mailTable;

                                CommonBL.SendDbMail(dealerModel.ContactEmail, soSubject, soBody);
                            }
                        }
                    }
                }

                #endregion
                ts.Complete();
            }
            return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
        }

        private static void UpdateSparePartSaleOrderDetailPrice(List<SparePartSaleOrderDetailListModel> sodList)
        {
            SparePartSaleOrderDetailBL sodBo = new SparePartSaleOrderDetailBL();
            foreach (SparePartSaleOrderDetailListModel detailModel in sodList)
            {
                SparePartSaleOrderDetailDetailModel detModel = new SparePartSaleOrderDetailDetailModel();
                detModel.SparePartSaleOrderDetailId = detailModel.SparePartSaleOrderDetailId;

                decimal listPrice = 0;
                CommonBL commonBo = new CommonBL();
                listPrice = commonBo.GetPriceByDealerPartVehicleAndType(detailModel.SparePartId.GetValue<int>(), null, UserManager.UserInfo.GetUserDealerId(),
                                                                        CommonValues.ListPriceLabel).Model;






                //  detModel = sodBo.GetSparePartSaleOrderDetail(detModel);
                // long partId = detailModel.SparePartId;
                //DealerSaleSparepartIndexViewModel dssModel = new DealerSaleSparepartBL().GetSparepartListPrice(partId);

                detModel.ListPrice = listPrice;
                detModel.OrderPrice = detailModel.OrderPrice;
                decimal discountRatio = (detailModel.AppliedDiscountRatio ?? detailModel.ListDiscountRatio).GetValue<decimal>();
                //detModel.OrderPrice = detailModel.ListPrice * ((100 - discountRatio) / 100);
                detModel.CommandType = CommonValues.DMLType.Update;
                sodBo.DMLSparePartSaleOrderDetail(UserManager.UserInfo, detModel);
            }
        }

        #endregion

        #region Spare Part Sale Order Create Sale Order
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSaleOrderDetail.SparePartSaleOrderDetailIndex,
            CommonValues.PermissionCodes.SparePartSaleOrderDetail.SparePartSaleOrderDetailDetails)]
        public ActionResult ListSparePartSaleOrderDetailsForSaleOrder([DataSourceRequest]DataSourceRequest request, SparePartSaleOrderViewModel model)
        {
            var bo = new SparePartSaleOrderDetailBL();
            var referenceModel = new SparePartSaleOrderDetailListModel(request)
            {
                SoNumber = model.SoNumber,
                StatusId = (int)CommonValues.SparePartSaleOrderDetailStatus.OpenOrder
            };
            int totalCnt;
            var returnValue = bo.ListSparePartSaleOrderDetails(UserManager.UserInfo, referenceModel, out totalCnt).Data;
            returnValue = returnValue.Where(e => e.OrderQuantity > e.PlannedQuantity.GetValue<int>()).ToList();

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        [HttpGet]
        [AuthorizationFilter(Permission.SparePartSaleOrderCreateSaleOrder, Permission.SparePartSaleOrderDetails)]
        public ActionResult SparePartSaleOrderCreateSaleOrder(string soNumber)
        {
            SparePartSaleOrderBL spsoBo = new SparePartSaleOrderBL();
            SparePartSaleOrderViewModel model = spsoBo.GetSparePartSaleOrder(UserManager.UserInfo, soNumber);
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateSaleOrder()
        {
            var list = ParseModelFromRequestInputStream<List<SparePartSaleOrderDetailListModel>>();
            var bus = new SparePartSaleOrderBL();

            ModelBase model = bus.CreateSaleOrder(UserManager.UserInfo,list).Model;
            if (model.ErrorNo > 0)
            {
                return Json(new { errorMessage = model.ErrorMessage, errorNo = model.ErrorNo });
            }
            return Json(new { errorMessage = MessageResource.Global_Display_Success, errorNo = model.ErrorNo });
        }
        #endregion

        #region Spare Part Sale Order - Sale Order Details
        [HttpGet]
        [AuthorizationFilter(Permission.SparePartSaleOrderCreateSaleOrder, Permission.SparePartSaleOrderDetails)]
        public ActionResult SparePartSaleOrderListSaleOrderDetails(string soNumber)
        {
            SparePartSaleOrderBL spsoBo = new SparePartSaleOrderBL();
            SparePartSaleOrderViewModel model = spsoBo.GetSparePartSaleOrder(UserManager.UserInfo, soNumber);
            return View(model);
        }
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSaleOrderDetail.SparePartSaleOrderDetailIndex,
            CommonValues.PermissionCodes.SparePartSaleOrderDetail.SparePartSaleOrderDetailDetails)]
        public ActionResult ListSparePartSaleOrderDetailsForSaleOrderDetails([DataSourceRequest]DataSourceRequest request, SparePartSaleOrderViewModel model)
        {
            var bo = new SparePartSaleOrderDetailBL();
            var referenceModel = new SparePartSaleOrderDetailListModel(request)
            {
                SoNumber = model.SoNumber
            };
            int totalCnt;
            var returnValue = bo.ListSparePartSaleOrderDetailsForSaleOrderDetails(UserManager.UserInfo, referenceModel, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }
        #endregion

        public ActionResult PrintSaleOrderPrintReport(long soNumber, int dealerId)
        {
            dealerId = UserManager.UserInfo.GetUserDealerId();
            MemoryStream ms = null;
            if (!CommonBL.CheckDealer(dealerId, soNumber, "SALE_ORDER").Model)
                return RedirectToAction("NoAuthorization", "SystemAdministration");
            try
            {

                ms = new MemoryStream(ReportManager.GetReport(ReportType.SaleOrderPrintReport, soNumber, dealerId, UserManager.LanguageCode));
            }
            catch (Exception ex)
            {
                SetMessage(ex.Message, CommonValues.MessageSeverity.Fail);
                return Redirect("~/error");
                //return GenerateAsyncOperationResponse(AsynOperationStatus.Error, ex.Message);
            }

            return File(ms, "application/pdf", "SalePrderPrintReport.pdf");
        }
    }
}

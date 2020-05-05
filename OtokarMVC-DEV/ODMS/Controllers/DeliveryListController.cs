using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMS.OtokarService;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.Dealer;
using ODMSModel.Delivery;
using ODMSModel.DeliveryList;
using ODMSModel.DeliveryListPart;
using ODMSModel.PurchaseOrder;
using ODMSModel.PurchaseOrderDetail;
using ODMSModel.SparePart;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel.Channels;
using System.Web.Configuration;
using System.Web.Mvc;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class DeliveryListController : ControllerBase
    {

        private void SetDefaults()
        {
            ViewBag.DeliveryStatusList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.DeliveryStatus).Data;
        }

        #region DeliveryList Index

        [AuthorizationFilter(CommonValues.PermissionCodes.DeliveryList.DeliveryListIndex)]
        [HttpGet]
        public ActionResult DeliveryListIndex(long id = 0)/*deliveryId*/
        {
            SetDefaults();
            DeliveryListListModel model = new DeliveryListListModel();
            if (UserManager.UserInfo.GetUserDealerId() != 0)
                model.IdDealer = UserManager.UserInfo.GetUserDealerId();
            model.IdDelivery = id;
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.DeliveryList.DeliveryListIndex)]
        public ActionResult ListDeliveryList([DataSourceRequest] DataSourceRequest request, DeliveryListListModel model)
        {
            SetDefaults();
            var deliveryListBo = new DeliveryListBL();

            var v = new DeliveryListListModel(request)
            {
                WaybillNo = model.WaybillNo,
                IdDealer = UserManager.UserInfo.GetUserDealerId(),
                DeliveryStatus = model.DeliveryStatus,
                IdDelivery = model.IdDelivery
            };

            var totalCnt = 0;
            var returnValue = deliveryListBo.ListDeliveryList(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.DeliveryList.DeliveryListIndex)]
        public ActionResult DeliveryCreate()
        {
            return View();

        }
        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.DeliveryList.DeliveryListIndex)]
        [ValidateAntiForgeryToken]
        public ActionResult DeliveryCreate(DeliveryCreateModel model)
        {
            if (string.IsNullOrEmpty(model.SapDeliveryNo))
            {
                SetMessage(MessageResource.DeliveryList_Warning_SapDeliveryNoNotEntered, CommonValues.MessageSeverity.Fail);
            }

            // OTOKAR SİPARİŞLERİNDE BURADAN DOLAYI HATA ALINIYOR. SİPARİŞ İÇERİ ALINAMIYOR. BURAYA EK KONTROL KOYMAK GEREKLİ.
            else
            {

                #region DeliveryNo Is Exist?

                var sonuc = 10;
                var deliveryListBo = new DeliveryListBL();
                var v = new DeliveryListListModel { SapDeliveryNo = model.SapDeliveryNo };
                var returnValue = deliveryListBo.IsDeliveryIdExist(UserManager.UserInfo, v, out sonuc).Model;


                switch (returnValue)
                {
                    case 1:
                        ReturnDeliveryDetails(model);
                        break;
                    case 2:
                        model.ErrorNo = 1;
                        model.ErrorMessage = MessageResource.DeliveryList_Warning_SapDeliveryNoExisted;
                        SetMessage(model.ErrorMessage, CommonValues.MessageSeverity.Fail);
                        return View(model);
                    case 5:
                        model.ErrorNo = 1;
                        model.ErrorMessage = MessageResource.DeliveryList_Warning_DeliveryInformationDoesNotBelongToYourCompany;
                        SetMessage(model.ErrorMessage, CommonValues.MessageSeverity.Fail);
                        return View(model);
                }

                #endregion

                int totalCount = 0;
                DeliveryListListModel delModel = new DeliveryListListModel();
                delModel.SapDeliveryNo = model.SapDeliveryNo;

                DeliveryListBL delBo = new DeliveryListBL();
                delBo.ListDeliveryList(UserManager.UserInfo, delModel, out totalCount);

                if (totalCount != 0)
                    SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);
                else
                {
                    ReturnDeliveryDetails(model);
                    if (model.ErrorNo > 0)
                        SetMessage(model.ErrorMessage, CommonValues.MessageSeverity.Fail);
                    else
                        SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);
                }
            }
            return View(model);
        }

        private void ReturnDeliveryDetails(DeliveryCreateModel model)
        {
            string errorMessage = string.Empty;

            DealerBL dealerBo = new DealerBL();
            DealerViewModel dealerModel = dealerBo.GetDealer(UserManager.UserInfo, UserManager.UserInfo.GetUserDealerId()).Model;

            var bl = new DeliveryListBL();
            var serviceModel = new List<DeliveryListServiceModel>();
            try
            {
                using (var pssc = GetClient())
                {
                    string psUser = WebConfigurationManager.AppSettings["PSSCUser"];
                    string psPassword = WebConfigurationManager.AppSettings["PSSCPass"];

                    DataSet rValue = pssc.ZDMS_IRSALIYE_BILGILERI(psUser, psPassword, model.SapDeliveryNo, dealerModel.SSID);
                    DataTable dt = new DataTable();
                    dt = rValue.Tables["Table1"];

                    if (dt == null || dt.Rows.Count == 0)
                    {
                        model.ErrorNo = 1;
                        model.ErrorMessage = MessageResource.DeliveryList_Warning_NoDataFound;
                        return;
                    }

                    //*******************************************************************************************
                    // test için eğerler atanıyor
                    //dt.Rows[0]["BSTKD"] = "333";
                    //dt.Rows[0]["MATNR_UK"] = "10";

                    //dt.Rows[1]["BSTKD"] = "111";
                    //dt.Rows[2]["BSTKD"] = "345";
                    //*******************************************************************************************

                    /*
                        VBELN : Teslimat numarası,
                        MATNR : Malzeme numarası
                        MATNR_UK : Teslimatın üst kalemi (sizin aşağıdaki  mailinizde sarı ile işaretlediğim yerde belirttiğiniz durum)
                                *  var ise bu üst kaleme ait malzemeyi getiriyoruz. Eğer boş ise, teslimata ait siparişte bu bilginin dolu olup 
                                *  olmadığına bakıyoruz ve siparişe ait üst kalemin malzemesini getririyoruz. Eğer her iksinde de boş ise herhangi
                                *  bir bilgi döndürmüyor.
                        LFIMG : Miktar
                        BSTKD  : SAS numarası
                        WADAT_IST : Teslimat Tarihi
                        MATBUNO : İrsaliye üstündeki matbu no bilgisi
                        NETWR : Teslimata ait siparişin KDV’siz Net fiyat değeri
                        POSNR  : Teslimata ait siparişin kalem numarası
                        UEPOS : Teslimata ait siparişin üst kalem numarası   ( Yok ise sıfır dönmektedir. )
                     */
                    serviceModel = dt.AsEnumerable().Select(row => new DeliveryListServiceModel
                    {
                        VBELN = row.Field<string>("VBELN"),
                        MATNR = row.Field<string>("MATNR"),
                        MATNR_UK = row.Field<string>("MATNR_UK") == "0" ? row.Field<string>("POSNR") : row.Field<string>("MATNR_UK"), // üst kalem numarası
                        LFIMG = row.Field<string>("LFIMG"),
                        BSTKD = row.Field<string>("BSTKD"), // parça kodu
                        WADAT_IST = row.Field<string>("WADAT_IST"),
                        MATBUNO = row.Field<string>("MATBUNO"),
                        NETWR = row.Field<string>("NETWR"),
                        POSNR = row.Field<string>("POSNR"), // kalem numarası
                        UEPOS = row.Field<string>("UEPOS") == "0" ? row.Field<string>("POSNR") : row.Field<string>("UEPOS")
                    }).ToList();

                    if (serviceModel.Any(p => string.IsNullOrEmpty(p.MATBUNO)))
                    {
                        model.ErrorNo = 1;
                        model.ErrorMessage = MessageResource.DeliveryList_Warning_MATBUNOempty;
                        return;
                    }
                    if (serviceModel.Any(p => String.IsNullOrEmpty(p.BSTKD)))
                    {
                        model.ErrorNo = 1;
                        model.ErrorMessage = MessageResource.DeliveryList_Warning_BSTKDEmpty;
                        return;
                    }
                    if (serviceModel.Any(p => String.IsNullOrEmpty(p.LFIMG)))
                    {
                        model.ErrorNo = 1;
                        model.ErrorMessage = MessageResource.DeliveryList_Warning_WaybillNotCreate;
                        return;
                    }

                    /*
                        1- Servisten dönen BSTKD kolonu ve ORIGINAL_SAP_ROW_NO değerleriyle 
                        *  PURCHASE_ORDER_DET tablosuna gidilmeli ve kayıt var mı kontrolü yapılmalı. 
                        *  Tüm dönen satırlar için kayıt olmalı, olmayan varsa onlar ekranda kullanıcıya 
                        *  "x,y,z parçalarnın satınalma siparişleri bulunamamıştır" şeklinde hata vermeli.
                        *  BSTDK = PURCHASE_ORDER_DET.SAP_OFFER_NO, 
                        *  MATNR_UK = PURCHASE_ORDER_DET.SAP_ROW_NO
                    */
                    List<DeliveryListServiceModel> groupList = (from r in serviceModel.AsEnumerable()
                                                                group r by new { r.POSNR, r.BSTKD }
                                                                    into grp
                                                                select new DeliveryListServiceModel()
                                                                {
                                                                    VBELN = grp.First().VBELN,
                                                                    MATNR = grp.First().MATNR,
                                                                    MATNR_UK = grp.First().MATNR_UK,
                                                                    LFIMG =
                                                                            grp.Sum(
                                                                                g =>
                                                                                g.LFIMG.ToCommaString()
                                                                                 .GetValue<decimal>())
                                                                               .ToString(),
                                                                    BSTKD = grp.Key.BSTKD,
                                                                    WADAT_IST = grp.First().WADAT_IST,
                                                                    MATBUNO = grp.First().MATBUNO,
                                                                    NETWR =
                                                                            (grp.First()
                                                                                .NETWR.ToCommaString()
                                                                                .GetValue<decimal>()).ToString(),
                                                                    POSNR = grp.Key.POSNR,
                                                                    UEPOS = grp.First().UEPOS
                                                                }).ToList<DeliveryListServiceModel>();
                    /*
                        SAP_OFFER_NO, SAP_ROW_NO, PART_CODE
                        SAP_OFFER_NO: Her türlü BSTKD olarak gidiyo
                        PART_CODE: MATNR_UK boşsa MATNR değeri, SAP_ROW_NO da POSNR olarak gidiyor
                        PART_CODE: MATNR_UK doluysa MATNR_UK, SAP_ROW_NO da UEPOS gidiyo

                        bu 3 değerle prosedürde PURCHASE_ORDER_DET kontrol edilecek varsa hata dönmeyecek yoksa şuanki gibi hata dönecek
                     */
                    PurchaseOrderDetailBL poDetBo = new PurchaseOrderDetailBL();
                    foreach (DeliveryListServiceModel deliveryListServiceModel in groupList)
                    {
                        string partCode = String.IsNullOrEmpty(deliveryListServiceModel.MATNR_UK)
                                              ? deliveryListServiceModel.MATNR
                                              : deliveryListServiceModel.MATNR_UK;
                        string sapRowNo = String.IsNullOrEmpty(deliveryListServiceModel.MATNR_UK)
                                              ? deliveryListServiceModel.POSNR
                                              : deliveryListServiceModel.UEPOS;
                        string sapOfferNo = deliveryListServiceModel.BSTKD;

                        PurchaseOrderDetailViewModel poDetModel = poDetBo.GetPurchaseOrderDetailsBySapInfo(sapOfferNo, sapRowNo, partCode).Model;
                        if (poDetModel.PurchaseOrderNumber == 0)
                        {
                            errorMessage += "PART_CODE : " + partCode + " SAP_ROW_NO : " + sapRowNo + " SAP_OFFER_NO : " +
                                            sapOfferNo + " , ";
                        }
                    }
                    if (errorMessage.Length != 0)
                    {
                        errorMessage += MessageResource.DeliveryList_Warning_NoPartDataFound;
                        model.ErrorNo = 1;
                        model.ErrorMessage = errorMessage;
                        return;
                    }

                    /*
                        2- DELIVERY_MST tablosuna kayıt yaratılır. 

                        ID_DEALER = Kendi bayisi, 
                        ID_SUPPLIER = NULL, 
                        SENDER_ID_DEALER = NULL,
                        STATUS = 0, 
                        SAP_DELIVERY_NO = VBELN, 
                        WAYBILL_NO = MATBUNO, 
                        WAYBILL_DATE = WADAT_IST  ,
                        IS_PLACED = 0, 
                        ACCEPTED_BY_USER = NULL
                    */
                    DeliveryCreateModel delModel = new DeliveryCreateModel()
                    {
                        SapDeliveryNo = groupList.ElementAt(0).VBELN,
                        WayBillNo = groupList.ElementAt(0).MATBUNO,
                        WayBillDate = groupList.ElementAt(0).WADAT_IST.GetValue<DateTime?>(),
                        CommandType = CommonValues.DMLType.Insert,
                        StatusId = (int)CommonValues.DeliveryStatus.NotReceived
                    };

                    /*
                        3- DELIVERY_DET tablosuna gelen her kalem için insert yapılır. 

                        ID_STOCK_TYPE = PURCHASE_ORDER_DET.ID_STOCK_TYPE (o satır için 1. maddede sorgularken bunları tutabilirsin).  
                        PO_DET_SEQ_NO = PURCHASE_ORDER_DET.SEQ_NO (o satır için 1. maddede sorgularken bunları tutabilirsin) 
                        SAP_OFFER_NO = BSTKD   , 
                        SAP_ROW_NO = POSNR, 
                        SAP_ORIGINAL_ROW_NO = MATNR_UK, 
                        SHIP_QUANTITY = LFIMG                   
                        RECIEVED_QUANT = NULL
                        INVOICE_PRICE = NETWR                 
                    */
                    List<int> poNumberList = new List<int>();
                    DeliveryListPartBL delPartBo = new DeliveryListPartBL();
                    PurchaseOrderBL poBo = new PurchaseOrderBL();
                    List<DeliveryListPartSubViewModel> detailList = new List<DeliveryListPartSubViewModel>();
                    foreach (DeliveryListServiceModel deliveryListServiceModel in groupList)
                    {
                        string partCode = String.IsNullOrEmpty(deliveryListServiceModel.MATNR_UK)
                                              ? deliveryListServiceModel.MATNR
                                              : deliveryListServiceModel.MATNR_UK;
                        string sapRowNo = String.IsNullOrEmpty(deliveryListServiceModel.MATNR_UK)
                                              ? deliveryListServiceModel.POSNR
                                              : deliveryListServiceModel.UEPOS;
                        string sapOfferNo = deliveryListServiceModel.BSTKD;

                        PurchaseOrderDetailViewModel poDetModel = poDetBo.GetPurchaseOrderDetailsBySapInfo(sapOfferNo, sapRowNo, partCode).Model;

                        PurchaseOrderViewModel poModel = new PurchaseOrderViewModel();
                        poModel.PoNumber = poDetModel.PurchaseOrderNumber;
                        poBo.GetPurchaseOrder(UserManager.UserInfo, poModel);

                        SparePartBL spBo = new SparePartBL();
                        SparePartIndexViewModel spModel = new SparePartIndexViewModel();
                        spModel.PartCode = deliveryListServiceModel.MATNR;
                        spBo.GetSparePart(UserManager.UserInfo, spModel);

                        DeliveryListPartSubViewModel delPartModel = new DeliveryListPartSubViewModel();
                        delPartModel.PartId = spModel.PartId;
                        delPartModel.StockTypeId = poModel.IdStockType.GetValue<int>();
                        delPartModel.PoDetSeqNo = poDetModel.PurchaseOrderDetailSeqNo;
                        delPartModel.SapOfferNo = sapOfferNo;
                        delPartModel.SapRowNo = deliveryListServiceModel.POSNR;
                        delPartModel.SapOriginalRowNo = sapRowNo;
                        delPartModel.ShipQnty = deliveryListServiceModel.LFIMG.GetValue<decimal>();
                        // TFS NO : 28340 OYA 03.02.2015
                        delPartModel.ReceiveQnty = delPartModel.ShipQnty;
                        delPartModel.InvoicePrice = deliveryListServiceModel.NETWR.GetValue<decimal>();
                        delPartModel.CommandType = CommonValues.DMLType.Insert;
                        detailList.Add(delPartModel);
                    }

                    DeliveryBL delBo = new DeliveryBL();
                    delBo.DMLDeliveryAndDetails(UserManager.UserInfo, delModel, detailList);
                    if (delModel.ErrorNo > 0)
                    {
                        model.ErrorNo = 1;
                        model.ErrorMessage = delModel.ErrorMessage;
                        return;
                    }

                    /*
                       4- Kayıtlar insert edildikten sonra DELIVERY_DET.PO_SEQ_NO'lar için PURCHASE_ORDER_DET tablosundaki 
                       * detay kayıtlarının SHIP_QUANT değerleri, gelen adetler eklenecek şekilde güncellenmeli 
                       * (önceden 1'ken 3 geldiyse 4 olmalı o kalem gibi). Bu kontrole ek olarak, 
                       * her detay kalemi için SHIP_QUANT >= ORDER_QUANT AND STATUS_LOOKVAL == 0 ise 
                       * detay kaydının STATUS_LOOKVAL değeri 1 olarak güncellenir. 
                       */
                    foreach (DeliveryListPartSubViewModel deliveryListPartSubViewModel in detailList)
                    {
                        PurchaseOrderDetailViewModel poDetModel = new PurchaseOrderDetailViewModel();
                        poDetModel.PurchaseOrderDetailSeqNo = deliveryListPartSubViewModel.PoDetSeqNo;
                        poDetBo.GetPurchaseOrderDetail(UserManager.UserInfo, poDetModel);

                        poDetModel.ShipmentQuantity = (poDetModel.ShipmentQuantity ?? 0) + deliveryListPartSubViewModel.ShipQnty;
                        if (poDetModel.ShipmentQuantity >= poDetModel.OrderQuantity && poDetModel.StatusId == (int)CommonValues.PurchaseOrderDetailStatus.Open)
                        {
                            poDetModel.StatusId = (int)CommonValues.PurchaseOrderDetailStatus.Closed;
                        }
                        poDetModel.CommandType = CommonValues.DMLType.Update;
                        poDetBo.DMLPurchaseOrderDetail(UserManager.UserInfo, poDetModel);
                        if (poDetModel.ErrorNo > 0)
                        {
                            model.ErrorNo = 1;
                            model.ErrorMessage = poDetModel.ErrorMessage;
                            return;
                        }

                        if (!poNumberList.Contains(poDetModel.PurchaseOrderNumber))
                        {
                            poNumberList.Add(poDetModel.PurchaseOrderNumber);
                        }
                    }

                    /*
                       5- Tüm detaylar güncellendikten sonra, 
                        * PO_SEQ_NO kayıtlarının karşılığı olan PURCHASE_ORDER_DET kayıtlarının PO_NUMBERları çekilecek. 
                        * Tüm PO_NUMBER'lar için detayların tamamının STATUS_LOOKVAL değeri 1 ise, PURCHASE_ORDER_MST tablosunun 
                        * STATUS_LOOKVAL değeri 2 olarak güncellenir.
                    */
                    foreach (int poNumber in poNumberList)
                    {
                        /*
                         * 1 tane bile açık sipariş detaylarda var ise master kapanmıyor OSMAN ve Bekir abi tarafından istendi. 24.08.2014
                         */
                        int totalCount = 0;
                        PurchaseOrderDetailListModel listModel = new PurchaseOrderDetailListModel();
                        listModel.PurchaseOrderNumber = poNumber;
                        listModel.StatusId = (int)CommonValues.PurchaseOrderDetailStatus.Open;
                        poDetBo.ListPurchaseOrderDetails(UserManager.UserInfo, listModel, out totalCount);

                        if (totalCount == 0)
                        {
                            PurchaseOrderViewModel poModel = new PurchaseOrderViewModel();
                            poModel.PoNumber = poNumber;
                            poBo.GetPurchaseOrder(UserManager.UserInfo, poModel);
                            poModel.Status = (int)CommonValues.PurchaseOrderStatus.ClosePurchaseOrder;
                            poModel.CommandType = CommonValues.DMLType.Update;
                            poBo.DMLPurchaseOrder(UserManager.UserInfo, poModel);
                            if (poModel.ErrorNo > 0)
                            {
                                model.ErrorNo = 1;
                                model.ErrorMessage = poModel.ErrorMessage;
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                model.ErrorNo = 1;
                model.ErrorMessage = ex.Message;
            }
        }
    }
}
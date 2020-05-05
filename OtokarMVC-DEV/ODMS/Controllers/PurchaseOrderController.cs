using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMS.OtokarService;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.Dealer;
using ODMSModel.PurchaseOrder;
using System;
using System.Web.Configuration;
using System.Web.Mvc;
using ODMSModel.PurchaseOrderDetail;
using ODMSModel.PurchaseOrderMatch;
using ODMSModel.PurchaseOrderType;
using ODMSModel.ServiceCallSchedule;
using ODMSModel.SparePart;
using ODMSModel.User;
using ODMSModel.Vehicle;
using System.Transactions;
using ODMSBusiness.Business;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class PurchaseOrderController : ControllerBase
    {
        #region Member Varables

        private void SetDefaults(bool isCreate = false)
        {
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.PoTypeList = PurchaseOrderTypeBL.ListPurchaseOrderTypeAsSelectListItem(UserManager.UserInfo, UserManager.UserInfo.GetUserDealerId(), null, null, true, null).Data;
            ViewBag.PoStatusList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.PurchaseOrderStatus).Data;
            ViewBag.StockTypeList = CommonBL.ListStockTypes(UserManager.UserInfo).Data;
            ViewBag.SupplierList = SupplierBL.ListSupplierComboAsSelectListItemPONotInDealer(UserManager.UserInfo, null).Data;
            List<SelectListItem> supplyTypeList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.SupplyPortLookup).Data;
            ViewBag.SupplyTypeList = supplyTypeList;
            ViewBag.DefectList = DefectBL.GetDefectComboList(null).Data;
        }

        [HttpGet]
        public JsonResult ListSupplier(string supplyTypeId)
        {
            List<SelectListItem> supplierSelectList = new List<SelectListItem>();

            if (supplyTypeId.GetValue<int>() == (int)CommonValues.SupplyPort.DealerService)
            {
                supplierSelectList = SupplierBL.ListSupplierComboAsSelectListItemPONotInDealer(UserManager.UserInfo, true).Data;
            }
            else
            {
                supplierSelectList = SupplierBL.ListSupplierComboAsSelectListItemPONotInDealer(UserManager.UserInfo, null).Data;
            }

            return Json(supplierSelectList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListDefectList(int? idVehicle)
        {
            List<SelectListItem> defectSelectList = new List<SelectListItem>();

            defectSelectList = DefectBL.GetDefectComboList(idVehicle).Data;

            return Json(defectSelectList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListPOType(string supplyTypeId)
        {
            List<SelectListItem> poTypeList = new List<SelectListItem>();

            if (supplyTypeId.GetValue<int>() == (int)CommonValues.SupplyPort.DealerService)
            {
                poTypeList = PurchaseOrderTypeBL.ListPurchaseOrderTypeAsSelectListItem(UserManager.UserInfo, UserManager.UserInfo.GetUserDealerId(), true, null, null, null).Data;
            }
            if (supplyTypeId.GetValue<int>() == (int)CommonValues.SupplyPort.Supplier)
            {
                poTypeList = PurchaseOrderTypeBL.ListPurchaseOrderTypeAsSelectListItem(UserManager.UserInfo, UserManager.UserInfo.GetUserDealerId(), null, true, null, null).Data;
            }
            if (supplyTypeId.GetValue<int>() == (int)CommonValues.SupplyPort.Otokar)
            {
                poTypeList = PurchaseOrderTypeBL.ListPurchaseOrderTypeAsSelectListItem(UserManager.UserInfo, UserManager.UserInfo.GetUserDealerId(), null, null, true, null).Data;
            }
            return Json(poTypeList, JsonRequestBehavior.AllowGet);
        }

        [ValidateAntiForgeryToken]
        public JsonResult GetStockTypeIdByGeneralParameter()
        {

            //var spModel = new PurchaseOrderTypeViewModel() { PurchaseOrderTypeId = poType.GetValue<int>() };
            var bo = new CommonBL();
            int StockTypeValue = CommonBL.GetGeneralParameterValue(CommonValues.GeneralParameters.StockType).Model.GetValue<int>();

            return Json(new
            {
                StockTypeId = StockTypeValue
            });

        }

        public JsonResult GetStockType(string poType)
        {
            dynamic stockType = new PurchaseOrderBL().GetPurchaseOrderTypeStockType(UserManager.UserInfo, poType).Model;

            return Json(stockType);

        }


        [ValidateAntiForgeryToken]
        public JsonResult GetPoTypeInfo(string poType)
        {
            if (!string.IsNullOrEmpty(poType))
            {
                var spModel = new PurchaseOrderTypeViewModel() { PurchaseOrderTypeId = poType.GetValue<int>() };
                var bo = new PurchaseOrderTypeBL();
                spModel = bo.Get(UserManager.UserInfo, spModel).Model;

                return Json(new
                {
                    ProposalType = spModel.ProposalType,
                    DeliveryPriority = spModel.DeliveryPriority,
                    SalesOrganization = spModel.SalesOrganization,
                    Division = spModel.Division,
                    DistrChan = spModel.DistrChan,
                    ItemCategory = spModel.ItemCategory,
                    OrderReason = spModel.OrderReason,
                    IsVehicleSelectionMust = spModel.IsVehicleSelectionMust
                });
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        public JsonResult ListVehicleTypes(string vehicleModelCode)
        {
            return !string.IsNullOrWhiteSpace(vehicleModelCode) ? Json(LabourDurationBL.GetVehicleTypeList(UserManager.UserInfo, vehicleModelCode, null).Data, JsonRequestBehavior.AllowGet) : Json(null, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListPurchaseOrderTypes(string supplyType)
        {
            return string.IsNullOrWhiteSpace(supplyType) ? Json(new List<SelectListItem>()) : Json(new PurchaseOrderBL().PurchaseOrderTypes(UserManager.UserInfo, supplyType).Data);
        }
        #endregion

        #region PurchaseOrder Index

        public ActionResult GetPartQuantity(string partCode)
        {
            string quantity = string.Empty;
            //if (General.IsTest)
            //    return Json(new { Quantity = quantity });

            using (var pssc = GetClient())
            {
                string psUser = WebConfigurationManager.AppSettings["PSSCUser"];
                string psPassword = WebConfigurationManager.AppSettings["PSSCPass"];

                DataSet rValue = pssc.ZYP_SD_WEB_MATERIAL_ATP2(psUser, psPassword, partCode);
                if (rValue.Tables[0].Rows.Count > 0)
                {
                    var row = rValue.Tables[0].Rows[0];
                    quantity = $"{row[1]} {row[2]}";
                }
            }

            return Json(new { Quantity = quantity });
        }
        public string GetCreditLimit()
        {
            // test ortamında servise gitmemesi için eklendi
            if (General.IsTest)
                return "0";

            string creditLimit = string.Empty;
            var dealerId = UserManager.UserInfo.GetUserDealerId();
            var dealerBo = new DealerBL();

            DealerViewModel dealerModel = dealerBo.GetDealer(UserManager.UserInfo, dealerId).Model;
            creditLimit = null;
            using (var pssc = GetClient())
            {
                string psUser = WebConfigurationManager.AppSettings["PSSCUser"];
                string psPassword = WebConfigurationManager.AppSettings["PSSCPass"];

                var rValue = pssc.ZYP_SD_WEB_CREDIT_STATU_WEB3(psUser, psPassword, dealerModel.SSID, "5100", "50", "00");
                DataTable dt = rValue.Tables["Table1"];
                if (dt == null || dt.Rows.Count == 0)
                {
                    creditLimit = null;
                }
                else
                {
                    if (dt.Columns.Contains("NOCHECK") && dt.Columns.Contains("SONUC"))
                    {
                        var noCheck = dt.Rows[0]["NOCHECK"].GetValue<string>();
                        if (!noCheck.Equals("X") && noCheck.Equals(string.Empty))
                        {
                            creditLimit = dt.Rows[0]["SONUC"].ToString().CurrencyFormat() + " " + dt.Rows[0]["CMWAE"];
                        }
                    }
                    else
                    {
                        creditLimit = MessageResource.Not_Return_NoCheck;
                    }
                }
            }
            return creditLimit;
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrder.PurchaseOrderIndex)]
        [HttpGet]
        public ActionResult PurchaseOrderIndex(Int64? poNumber, string campaignCode)
        {
            ViewBag.PoTypeList = new PurchaseOrderTypeBL().PurchaseOrderTypeList(UserManager.UserInfo).Data;
            ViewBag.PoStatusList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.PurchaseOrderStatus).Data;
            ViewBag.PoList = CommonBL.ListPo(UserManager.UserInfo).Data;
            ViewBag.StockTypeList = CommonBL.ListStockTypes(UserManager.UserInfo).Data;
            ViewBag.OrderLocationList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.SupplyTypeLookup).Data;

            PurchaseOrderListModel model = new PurchaseOrderListModel();
            if (UserManager.UserInfo.GetUserDealerId() != 0)
                model.IdDealer = UserManager.UserInfo.GetUserDealerId();

            model._PoNumber = poNumber.ToString();
            model.CreditLimit = GetCreditLimit();
            model.CampaignCode = campaignCode;

            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrder.PurchaseOrderIndex, CommonValues.PermissionCodes.PurchaseOrder.PurchaseOrderDetails)]
        public ActionResult ListPurchaseOrder([DataSourceRequest] DataSourceRequest request, PurchaseOrderListModel model)
        {
            var purchaseOrderBo = new PurchaseOrderBL();


            var v = new PurchaseOrderListModel(request)
            {
                _PoNumber = model._PoNumber.AddSingleQuote(),
                Status = model.Status,
                PoType = model.PoType,
                IdDealer = model.IdDealer,
                IdStockType = model.IdStockType,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                OrderLocation = model.OrderLocation,
                PartName = model.PartName,
                PartCode = model.PartCode,
                CampaignCode = model.CampaignCode
            };

            var totalCnt = 0;
            var returnValue = purchaseOrderBo.ListPurchaseOrder(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region PurchaseOrder Create

        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrder.PurchaseOrderIndex, CommonValues.PermissionCodes.PurchaseOrder.PurchaseOrderCreate)]
        public ActionResult PurchaseOrderCreate(int? supplierTypeId)
        {
            SetDefaults(true);
            if (supplierTypeId == (int)CommonValues.SupplyPort.DealerService)
            {
                ViewBag.SupplierList = SupplierBL.ListSupplierComboAsSelectListItemPONotInDealer(UserManager.UserInfo, true).Data;
            }

            int idDealer = UserManager.UserInfo.GetUserDealerId();

            string dealerBranchSSID = DealerBL.GetDealerBranchSSID(idDealer).Model;

            var model = new PurchaseOrderViewModel { IdDealer = UserManager.UserInfo.GetUserDealerId() };
            model.DealerBranchSSID = dealerBranchSSID;
            model.SupplyType = supplierTypeId;

            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrder.PurchaseOrderIndex, CommonValues.PermissionCodes.PurchaseOrder.PurchaseOrderCreate)]
        [HttpPost]
        public ActionResult PurchaseOrderCreate(PurchaseOrderViewModel viewModel)
        {
            var purchaseOrderBo = new PurchaseOrderBL();
            if (UserManager.UserInfo.GetUserDealerId() > 0)
            {
                viewModel.IdDealer = UserManager.UserInfo.GetUserDealerId();
            }

            SetDefaults(true);

            viewModel.Status = (int)CommonValues.PurchaseOrderStatus.NewRecord;//0 : Default Yeni Kayıt

            if (ModelState.IsValid)
            {
                var potModel = new PurchaseOrderTypeViewModel();
                if (viewModel.PoType != null && viewModel.SupplyType != (int)CommonValues.SupplyPort.Supplier)
                {
                    potModel.PurchaseOrderTypeId = viewModel.PoType.GetValue<int>();
                    var potBo = new PurchaseOrderTypeBL();
                    potModel = potBo.Get(UserManager.UserInfo, potModel).Model;
                    viewModel.SalesOrganization = potModel.SalesOrganization;
                    viewModel.ProposalType = potModel.ProposalType;
                    viewModel.DeliveryPriority = potModel.DeliveryPriority;
                    viewModel.OrderReason = potModel.OrderReason;
                    viewModel.DistrChan = potModel.DistrChan;
                    viewModel.Division = potModel.Division;
                    viewModel.ItemCategory = potModel.ItemCategory;
                    viewModel.DealerBranchSSID = potModel.DealerBranchSSID;
                }
                else
                {
                    viewModel.SalesOrganization = null;
                    viewModel.ProposalType = null;
                    viewModel.DeliveryPriority = 0;
                    viewModel.OrderReason = null;
                    viewModel.DistrChan = null;
                    viewModel.Division = null;
                    viewModel.ItemCategory = null;
                    viewModel.DealerBranchSSID = null;
                }

                if (viewModel.SupplyType == (int)CommonValues.SupplyPort.DealerService)
                {
                    int supplierIdDealer = viewModel.IdSupplier.GetValue<int>();
                    viewModel.SupplierIdDealer = supplierIdDealer;
                    viewModel.IdSupplier = null;

                    if (viewModel.IsProposal)
                    {
                        viewModel.SupplierDealerConfirm = (int)CommonValues.SupplierDealerConfirmType.NewProposal;
                        viewModel.IsPriceFixed = true;
                    }
                    else
                    {
                        viewModel.SupplierDealerConfirm = (int)CommonValues.SupplierDealerConfirmType.NotApprovedOrder;
                        if (potModel != null && potModel.ManuelPriceAllow)
                        {
                            viewModel.IsPriceFixed = true;
                        }
                    }
                }
                viewModel.CommandType = CommonValues.DMLType.Insert;
                purchaseOrderBo.DMLPurchaseOrder(UserManager.UserInfo, viewModel);

                CheckErrorForMessage(viewModel, true);
                ModelState.Clear();
                var returnModel = new PurchaseOrderViewModel { IdDealer = UserManager.UserInfo.GetUserDealerId(), PoNumber = viewModel.PoNumber };
                return View(returnModel);
            }
            return View(viewModel);
        }

        #endregion

        #region PurchaseOrder Update

        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrder.PurchaseOrderIndex, CommonValues.PermissionCodes.PurchaseOrder.PurchaseOrderUpdate)]
        [HttpGet]
        public ActionResult PurchaseOrderUpdate(Int64? poNumber)
        {
            SetDefaults(false);
            var v = new PurchaseOrderViewModel();

            int idDealer = UserManager.UserInfo.GetUserDealerId();

            string dealerBranchSSID = DealerBL.GetDealerBranchSSID(idDealer).Model;

            v.DealerBranchSSID = dealerBranchSSID;
            if (poNumber != 0)
            {
                var purchaseOrderBo = new PurchaseOrderBL();
                v.PoNumber = poNumber;

                purchaseOrderBo.GetPurchaseOrder(UserManager.UserInfo, v);

                if (v.SupplyType == (int)CommonValues.SupplyPort.DealerService)
                {
                    v.IdSupplier = v.SupplierIdDealer;
                }

                if (v.VehicleId != 0 && v.VehicleId != null)
                {
                    VehicleBL vBo = new VehicleBL();
                    VehicleIndexViewModel vModel = new VehicleIndexViewModel();
                    vModel.VehicleId = v.VehicleId.GetValue<int>();

                    vBo.GetVehicle(UserManager.UserInfo, vModel);
                    v.VehiclePlateVinNo = vModel.Plate + " " + vModel.VinNo;
                }
            }
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrder.PurchaseOrderIndex, CommonValues.PermissionCodes.PurchaseOrder.PurchaseOrderUpdate)]
        [HttpPost]
        public ActionResult PurchaseOrderUpdate(PurchaseOrderViewModel viewModel)
        {
            SetDefaults(false);
            var purchaseOrderBo = new PurchaseOrderBL();
            var potModel = new PurchaseOrderTypeViewModel();
            if (viewModel.PoType != null && viewModel.SupplyType != (int)CommonValues.SupplyPort.Supplier)
            {
                potModel.PurchaseOrderTypeId = viewModel.PoType.GetValue<int>();

                var potBo = new PurchaseOrderTypeBL();
                potModel = potBo.Get(UserManager.UserInfo, potModel).Model;

                viewModel.SalesOrganization = potModel.SalesOrganization;
                viewModel.ProposalType = potModel.ProposalType;
                viewModel.DeliveryPriority = potModel.DeliveryPriority;
                viewModel.OrderReason = potModel.OrderReason;
                viewModel.DistrChan = potModel.DistrChan;
                viewModel.Division = potModel.Division;
                viewModel.ItemCategory = potModel.ItemCategory;
                viewModel.IsVehicleSelectionMust = potModel.IsVehicleSelectionMust;
            }
            else
            {
                viewModel.SalesOrganization = null;
                viewModel.ProposalType = null;
                viewModel.DeliveryPriority = 0;
                viewModel.OrderReason = null;
                viewModel.DistrChan = null;
                viewModel.Division = null;
                viewModel.ItemCategory = null;
                viewModel.IsVehicleSelectionMust = false;
            }

            if (viewModel.SupplyType == (int)CommonValues.SupplyPort.DealerService)
            {
                if (viewModel.IsProposal)
                {
                    viewModel.SupplierDealerConfirm = (int)CommonValues.SupplierDealerConfirmType.NewProposal;
                    viewModel.IsPriceFixed = true;
                }
                else
                {
                    viewModel.SupplierDealerConfirm = (int)CommonValues.SupplierDealerConfirmType.NotApprovedOrder;
                    if (potModel != null && potModel.ManuelPriceAllow)
                    {
                        viewModel.IsPriceFixed = true;
                    }
                }
            }

            viewModel.CommandType = CommonValues.DMLType.Update;

            if (viewModel.IsVehicleSelectionMust.GetValue<bool>() && (viewModel.VehicleId == null || viewModel.VehicleId == 0))
            {
                SetMessage(MessageResource.PurchaseOrder_Error_VehicleMust, CommonValues.MessageSeverity.Fail);
                return View(viewModel);
            }

            if (ModelState.IsValid)
            {
                purchaseOrderBo.DMLPurchaseOrder(UserManager.UserInfo, viewModel);
                if (viewModel.VehicleId != 0 && viewModel.VehicleId != null)
                {
                    VehicleBL vBo = new VehicleBL();
                    VehicleIndexViewModel vModel = new VehicleIndexViewModel();
                    vModel.VehicleId = viewModel.VehicleId.GetValue<int>();

                    vBo.GetVehicle(UserManager.UserInfo, vModel);

                    viewModel.VehiclePlateVinNo = vModel.Plate + " " + vModel.VinNo;
                }
                CheckErrorForMessage(viewModel, true);
            }

            return View(viewModel);
        }

        #endregion

        #region PurchaseOrder Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrder.PurchaseOrderIndex, CommonValues.PermissionCodes.PurchaseOrder.PurchaseOrderDelete)]
        public ActionResult DeletePurchaseOrder(Int64? poNumber)
        {
            using (var ts = new TransactionScope())
            {
                PurchaseOrderViewModel viewModel = new PurchaseOrderViewModel { PoNumber = poNumber };
                var purchaseOrderBo = new PurchaseOrderBL();
                var detailBo = new PurchaseOrderDetailBL();

                purchaseOrderBo.GetPurchaseOrder(UserManager.UserInfo, viewModel);

                if (viewModel.Status != (int)CommonValues.PurchaseOrderStatus.NewRecord)
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.DB_Record_Has_Changed);

                int totalCount = 0;
                PurchaseOrderDetailListModel detailListModel = new PurchaseOrderDetailListModel();
                detailListModel.PurchaseOrderNumber = viewModel.PoNumber.GetValue<int>();

                List<PurchaseOrderDetailListModel> detailList = detailBo.ListPurchaseOrderDetails(UserManager.UserInfo, detailListModel, out totalCount).Data;

                if (totalCount != 0)
                {
                    foreach (PurchaseOrderDetailListModel poDet in detailList)
                    {
                        PurchaseOrderDetailViewModel poDetModel = new PurchaseOrderDetailViewModel();
                        poDetModel.PurchaseOrderDetailSeqNo = poDet.PurchaseOrderDetailSeqNo;
                        detailBo.GetPurchaseOrderDetail(UserManager.UserInfo, poDetModel);
                        poDetModel.CommandType = CommonValues.DMLType.Delete;

                        detailBo.DMLPurchaseOrderDetail(UserManager.UserInfo, poDetModel);

                        if (poDetModel.ErrorNo != 0)
                        {
                            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, poDetModel.ErrorMessage);
                        }
                    }
                }

                viewModel.CommandType = CommonValues.DMLType.Delete;
                purchaseOrderBo.DMLPurchaseOrder(UserManager.UserInfo, viewModel);
                ModelState.Clear();


                if (viewModel.ErrorNo == 0)
                {
                    ts.Complete();
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
                }

                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
            }
        }
        #endregion

        #region PurchaseOrder Cancel

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrder.PurchaseOrderIndex, CommonValues.PermissionCodes.PurchaseOrder.PurchaseOrderCancel)]
        public ActionResult CancelPurchaseOrder(Int64? poNumber)
        {
            int totalCount = 0;
            string orderNo = string.Empty;
            PurchaseOrderViewModel viewModel = new PurchaseOrderViewModel { PoNumber = poNumber };
            var purchaseOrderBo = new PurchaseOrderBL();
            var purchaseOrderDetailBo = new PurchaseOrderDetailBL();

            purchaseOrderBo.GetPurchaseOrder(UserManager.UserInfo, viewModel);

            if ((viewModel.SupplierIdDealer == null || viewModel.SupplierIdDealer == 0) &&
                    (viewModel.IdSupplier == null || viewModel.IdSupplier == 0))
            {
                viewModel.SupplyType = (int)CommonValues.SupplyPort.Otokar;
            }
            if (viewModel.SupplierIdDealer != null && viewModel.SupplierIdDealer != 0)
            {
                viewModel.SupplyType = (int)CommonValues.SupplyPort.DealerService;
            }
            if (viewModel.IdSupplier != null && viewModel.IdSupplier != 0)
            {
                viewModel.SupplyType = (int)CommonValues.SupplyPort.Supplier;
            }

            PurchaseOrderDetailListModel poDetListModel = new PurchaseOrderDetailListModel();
            poDetListModel.PurchaseOrderNumber = (int)poNumber;

            List<PurchaseOrderDetailListModel> poDetailList = purchaseOrderDetailBo.ListPurchaseOrderDetails(UserManager.UserInfo, poDetListModel, out totalCount).Data;
            if (totalCount != 0)
            {
                // Otokar siparişi ve açık statüsündeyse
                if (viewModel.SupplyType == (int)CommonValues.SupplyPort.Otokar
                    && viewModel.Status == (int)CommonValues.PurchaseOrderStatus.OpenPurchaseOrder)
                {
                    string sapOfferNo = poDetailList.ElementAt(0).SAPOfferNo;
                    if (!String.IsNullOrEmpty(sapOfferNo))
                    {
                        string message = OtokarWebOrderCancel(sapOfferNo);
                        if (message != string.Empty)
                        {
                            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, message);
                        }
                    }
                }

                if (poDetailList.Count != 0)
                {
                    orderNo = poDetailList.Select(d => d.SAPOfferNo).First();
                }

                foreach (PurchaseOrderDetailListModel poDet in poDetailList)
                {
                    PurchaseOrderDetailViewModel poDetModel = new PurchaseOrderDetailViewModel();
                    poDetModel.PurchaseOrderDetailSeqNo = poDet.PurchaseOrderDetailSeqNo;

                    purchaseOrderDetailBo.GetPurchaseOrderDetail(UserManager.UserInfo, poDetModel);

                    // Tedarikçi siparişi ise detaylarda kapalı statüsünde olanlar değiştirilmiyor
                    if (poDetModel.StatusId != (int)CommonValues.PurchaseOrderDetailStatus.Closed
                        || (poDetModel.StatusId != (int)CommonValues.PurchaseOrderDetailStatus.Closed
                        && viewModel.SupplyType != (int)CommonValues.SupplyPort.Supplier))
                    {
                        poDetModel.StatusId = (int)CommonValues.PurchaseOrderDetailStatus.Cancelled;
                        poDetModel.CommandType = CommonValues.DMLType.Update;
                        purchaseOrderDetailBo.DMLPurchaseOrderDetail(UserManager.UserInfo, poDetModel);
                        if (poDetModel.ErrorNo != 0)
                        {
                            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, poDetModel.ErrorMessage);
                        }
                    }
                }
            }

            viewModel.Status = (int)CommonValues.PurchaseOrderStatus.CanceledPurhcaseOrder;
            viewModel.CommandType = CommonValues.DMLType.Update;

            purchaseOrderBo.DMLPurchaseOrder(UserManager.UserInfo, viewModel);

            ModelState.Clear();

            /*TFS No : 32870 OYA Mail gönderimi sipariş kime geçildiyse, "xxx Bayi tarafından açılan 9999 nolu (PURCHASE_ORDER_DET.SAP_OFFER_NO) sipariş iptal edilmiştir." şeklinde mail gönderilecek.*/
            if (viewModel.SupplyType == (int)CommonValues.SupplyPort.DealerService && (viewModel.SupplierDealerConfirm == 2 || viewModel.SupplierDealerConfirm == 3))
            {
                DealerBL dealerBo = new DealerBL();
                DealerViewModel dealerModel = dealerBo.GetDealer(UserManager.UserInfo, viewModel.IdDealer.GetValue<int>()).Model;
                string to = dealerModel.ContactEmail;
                string subject = string.Format(MessageResource.PurchaseOrder_Display_CancelPurchaseOrderMailSubject,
                    viewModel.OrderNo);
                string body = ReturnCancelPurchaseOrderMailBody(dealerModel.Name, viewModel.PoNumber, orderNo);
                CommonBL.SendDbMail(to, subject, body);
            }

            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);

        }
        private string OtokarWebOrderCancel(string sapOfferNo)
        {
            //if (General.IsTest)
            //    return string.Empty;

            try
            {
                using (var pssc = GetClient())
                {
                    string psUser = WebConfigurationManager.AppSettings["PSSCUser"];
                    string psPassword = WebConfigurationManager.AppSettings["PSSCPass"];

                    DataSet rValue = pssc.ZYP_SD_WEB_ORDER_DELETE(psUser, psPassword, sapOfferNo);
                    DataTable dt = new DataTable();
                    dt = rValue.Tables["Table1"];
                    if (dt != null && dt.Rows.Count != 0)
                    {
                        string type = dt.Rows[0]["TYPE"].GetValue<string>();
                        string message = dt.Rows[0]["MESSAGE"].GetValue<string>();

                        if (type == "A" || type == "E" || type == "X")
                        {
                            return message;
                        }
                    }
                    else
                    {
                        return string.Format(MessageResource.Global_Warning_ServiceError, "ZYP_SD_WEB_ORDER_DELETE");
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return string.Empty;
        }

        #endregion

        #region PurchaseOrder Details
        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrder.PurchaseOrderIndex, CommonValues.PermissionCodes.PurchaseOrder.PurchaseOrderDetails)]
        [HttpGet]
        public ActionResult PurchaseOrderDetails(Int64? poNumber)
        {
            var v = new PurchaseOrderViewModel();
            var purchaseOrderBo = new PurchaseOrderBL();

            v.PoNumber = poNumber;

            SetDefaults(true);
            purchaseOrderBo.GetPurchaseOrder(UserManager.UserInfo, v);

            return View(v);
        }

        #endregion

        #region PurchaseOrder Complete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrder.PurchaseOrderIndex, CommonValues.PermissionCodes.PurchaseOrder.PurchaseOrderUpdate)]
        public ActionResult PurchaseOrderComplete(PurchaseOrderViewModel viewModel)
        {
            string msgSuc = MessageResource.Global_Display_Success;
            using (var ts = new TransactionScope(TransactionScopeOption.Suppress))
            {
                var purchaseOrderBo = new PurchaseOrderBL();
                purchaseOrderBo.GetPurchaseOrder(UserManager.UserInfo, viewModel);

                if (viewModel.Status != (int)CommonValues.PurchaseOrderStatus.NewRecord)
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.DB_Record_Has_Changed);

                msgSuc = PurchaseOrderCompleteSub(viewModel);

                if (viewModel.ErrorNo > 0)
                {
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
                }
                ts.Complete();
            }
            return GenerateAsyncOperationResponse(AsynOperationStatus.Success, msgSuc);
        }

        public string PurchaseOrderCompleteSub(PurchaseOrderViewModel viewModel)
        {
            string msgSuc = MessageResource.Global_Display_Success;

            var purchaseOrderBo = new PurchaseOrderBL();

            purchaseOrderBo.GetPurchaseOrder(UserManager.UserInfo, viewModel);

            viewModel.Status = viewModel.IsProposal
                ? (int)CommonValues.PurchaseOrderStatus.NewRecord
                : (int)CommonValues.PurchaseOrderStatus.OpenPurchaseOrder; //1
            viewModel.StatusDetail = viewModel.IsProposal
                ? (int)CommonValues.PurchaseOrderDetailStatus.NewRecord
                : (int)CommonValues.PurchaseOrderDetailStatus.Open;
            viewModel.CommandType = CommonValues.PurchaseOrderDMLType.Complete;
            purchaseOrderBo.DMLPurchaseOrder(UserManager.UserInfo, viewModel);


            if (viewModel.ErrorNo == 0)
            {
                if ((viewModel.IdSupplier == null || viewModel.IdSupplier == 0)
                  && (viewModel.SupplierIdDealer == null || viewModel.SupplierIdDealer == 0))
                {
                    OtokarWebOrderCreate(viewModel);

                    if (viewModel.ErrorNo == 0)
                    {
                        msgSuc = msgSuc + MessageResource.Global_Display_OrderNo + " : " + viewModel.OrderNo;
                    }
                }

                if ((viewModel.IdSupplier == null || viewModel.IdSupplier == 0)
                    && (viewModel.SupplierIdDealer == null || viewModel.SupplierIdDealer == 0) && viewModel.ErrorNo == 0)
                {
                    // TFS NO : 28294 OYA Acil siparişlerde mail gönderilecek. (OtokarWebCreate metodundan sonra çağırılmadı burada çağırıldı,
                    //çünkü order date ve update user'ın atanması gerekiyor)
                    if (viewModel.PoType == (int)CommonValues.PurchaseOrderType.Urgent)
                    {
                        string to = CommonBL.GetGeneralParameterValue(CommonValues.GeneralParameters.OtokarUrgentPOMailList).Model;
                        string subject = string.Format(MessageResource.PurchaseOrder_Display_UrgentMailSubject, viewModel.OrderNo);
                        string body = ReturnUrgentMailBody(viewModel.OrderNo, viewModel.PoNumber);
                        CommonBL.SendDbMail(to, subject, body);
                    }
                    else if (viewModel.PoType == (int)CommonValues.PurchaseOrderType.RedirectedUrgent)
                    {
                        string to = CommonBL.GetGeneralParameterValue(CommonValues.GeneralParameters.OtokarRedirectedUrgentPOMailList).Model;
                        string subject = string.Format(MessageResource.PurchaseOrder_Display_RedirectedUrgentMailSubject, viewModel.OrderNo);

                        string body = ReturnUrgentMailBody(viewModel.OrderNo, viewModel.PoNumber);
                        CommonBL.SendDbMail(to, subject, body);
                    }
                }
            }

            if (viewModel.ErrorNo > 0)
            {
                string errMsg = viewModel.ErrorMessage;
                //Rollback
                viewModel.CommandType = CommonValues.PurchaseOrderDMLType.RollBack;
                viewModel.Status = (int)CommonValues.PurchaseOrderStatus.NewRecord; //1
                viewModel.StatusDetail = (int)CommonValues.PurchaseOrderDetailStatus.NewRecord;

                purchaseOrderBo.DMLPurchaseOrder(UserManager.UserInfo, viewModel);
                viewModel.ErrorNo = 1;
                viewModel.ErrorMessage = errMsg;
            }

            return msgSuc;
        }

        private string ReturnCancelPurchaseOrderMailBody(string dealerName, long? poNumber, string orderNo)
        {
            int totalCount = 0;
            PurchaseOrderViewModel poModel = new PurchaseOrderViewModel();
            poModel.PoNumber = poNumber;
            PurchaseOrderBL poBo = new PurchaseOrderBL();
            poBo.GetPurchaseOrder(UserManager.UserInfo, poModel);

            UserIndexViewModel userModel = new UserIndexViewModel();
            var userId = -1;
            int.TryParse(poModel.UpdateUser, out userId);
            userModel.UserId = userId;

            UserBL userBo = new UserBL();
            userBo.GetUser(UserManager.UserInfo, userModel);

            StringBuilder body = new StringBuilder();
            string bodyMaster = string.Format(MessageResource.PurchaseOrder_Display_CancelPurchaseOrderMailBody, dealerName, poNumber, orderNo);
            body.Append(bodyMaster);

            return body.ToString();
        }

        private string ReturnUrgentMailBody(int orderNo, long? poNumber)
        {
            int totalCount = 0;
            string isUrgent = "[X]";
            PurchaseOrderViewModel poModel = new PurchaseOrderViewModel();
            poModel.PoNumber = poNumber;

            PurchaseOrderBL poBo = new PurchaseOrderBL();
            poBo.GetPurchaseOrder(UserManager.UserInfo, poModel);

            DealerBL dealerBo = new DealerBL();
            DealerViewModel dealerModel = dealerBo.GetDealer(UserManager.UserInfo, poModel.IdDealer.GetValue<int>()).Model;

            UserIndexViewModel userModel = new UserIndexViewModel();
            var userId = -1;
            int.TryParse(poModel.UpdateUser, out userId);
            userModel.UserId = userId;


            UserBL userBo = new UserBL();
            userBo.GetUser(UserManager.UserInfo, userModel);

            VehicleIndexViewModel vehicleModel = new VehicleIndexViewModel();
            vehicleModel.VehicleId = poModel.VehicleId.GetValue<int>();
            VehicleBL vehicleBo = new VehicleBL();
            vehicleBo.GetVehicle(UserManager.UserInfo, vehicleModel);

            /*
                Otokar TEklif No : PURCHASE_ORDER_DET.SAP_OFFER_NO
                SYS Sipariş No: PURCHASE_ORDER_DET.PO_NUMBER
                Tarih : PURCHASE_ORDER_MST.ORDER_DATE
                Müşteri : PURCHASE_ORDER_MST.ID_DEALER -> DEALER.DEALER_SHORT_NAME
                Siparişi Veren : PURCHASE_ORDER_MST.UPDATE_USER -> DMS_USER.CUSTOMER_NAME+ISNULL(USER_LAST_NAME,' ')
                Açıklama : SİPARİŞ
                Acil : [X]
                Şasi No : PURCHASE_ORDER_MST.ID_VEHICLE -> VEHICLE.VIN_NO
                Araç Modeli: PURCHASE_ORDER_MST.MODEL_KOD
                Araç Tipi : PURCHASE_ORDER_MST.ID_VEHICLE -> VEHICLE.V_CODE_KOD -> VEHICLE_CODE.ID_TYPE -> VEHICLE_TYPE.TYPE_NAME
                Arıza : ARIZALI
                No Parça Kodu Parça Adı Sipariş Adedi
                1   7001105C1  ENJEKTÖR-YAKIT  2
             */
            StringBuilder body = new StringBuilder();
            string bodyMaster = "<table><tbody>" + string.Format(MessageResource.PurchaseOrder_Display_UrgentMailBody,
                                        orderNo,
                                        poNumber,
                                        poModel.OrderDate,
                                        dealerModel.ShortName,
                                        userModel.UserFirstName + " " + userModel.UserMidName + " " +
                                        userModel.UserLastName,
                                        MessageResource.PurchaseOrder_Display_UrgentMailDescription,
                                        isUrgent,
                                        vehicleModel.VinNo,
                                        poModel.ModelKod,
                                        vehicleModel.VehicleType,
                                        MessageResource.PurchaseOrder_Display_UrgentMailDamage
                ) + "</tbody></table>";
            body.Append(bodyMaster);

            PurchaseOrderDetailListModel poDetailListModel = new PurchaseOrderDetailListModel();
            poDetailListModel.PurchaseOrderNumber = poNumber.GetValue<int>();

            PurchaseOrderDetailBL poDetailBo = new PurchaseOrderDetailBL();
            List<PurchaseOrderDetailListModel> detailList = poDetailBo.ListPurchaseOrderDetails(UserManager.UserInfo, poDetailListModel, out totalCount).Data;

            if (totalCount > 0)
            {
                body.Append(CommonValues.TableStart);
                body.Append("<thead><tr bgcolor=" + '\u0022' + "#c1c1a4" + '\u0022' + ">" + MessageResource.PurchaseOrder_Display_UrgentMailDetailHeader + "</tr></thead>");
                body.Append(CommonValues.NewLine);

                int rowNo = 1;
                foreach (var purchaseOrderDetailListModel in detailList)
                {
                    body.Append(CommonValues.RowStart);
                    string bodyDetail =
                        string.Format(MessageResource.PurchaseOrder_Display_UrgentMailDetailRow,
                        rowNo,
                        purchaseOrderDetailListModel.PartCode,
                        purchaseOrderDetailListModel.PartName,
                        purchaseOrderDetailListModel.OrderQuantity);
                    body.Append(bodyDetail);
                    body.Append(CommonValues.RowEnd);
                    rowNo++;
                }

                body.Append(CommonValues.TableEnd);
            }
            return body.ToString();
        }

        private void OtokarWebOrderCreate(PurchaseOrderViewModel model)
        {
            ////Canlı ortamda bu method çalışacak test ortamlarında çalışmayacak.
            //if (General.IsTest)
            //    return;

            var bl = new PurchaseOrderBL();
            var serviceModel = new PurchaseOrderServiceModel();
            var callBl = new ServiceCallScheduleBL();
            var logModel = new ServiceCallLogModel();
            try
            {
                using (var pssc = GetClient())
                {
                    string psUser = System.Web.Configuration.WebConfigurationManager.AppSettings["PSSCUser"];
                    string psPassword = System.Web.Configuration.WebConfigurationManager.AppSettings["PSSCPass"];

                    if (model.ErrorNo <= 0)
                    {
                        bl.SetDataForOtokarWebService(model);
                        //Call LOG for request
                        logModel.ReqResDic = new Dictionary<string, string>()
                            {
                                { "DealerSSID", model.DealerSSID },
                                { "OrderReason", model.OrderReason },
                                { "ProposalType", model.ProposalType },
                                { "SalesOrg", model.SalesOrganization },
                                { "DistrChan", model.DistrChan },
                                { "Division", model.Division },
                                { "ModelKod", model.ModelKod },
                                { "CreateDate", model.CreateDate.ToString("yyyyMMdd") },
                                { "ItemCategory", model.ItemCategory },
                                { "Description", model.Description },
                                { "AllDetParts", model.AllDetParts }
                            };
                        logModel.ServiceName = "WEB_ORDER_CREATE";// "RETAIL_PRICE_SERVICE";
                        logModel.IsManuel = true;
                        logModel.LogType = CommonValues.LogType.Request;

                        callBl.LogRequestService(logModel);
                        //Calling Otokar Purchase Order Create Service
                        DataSet rValue = pssc.ZYP_SD_WEB_ORDER_CREATE(psUser,
                            psPassword,
                            model.DealerSSID,//Dealer => DealerSSID*
                            !string.IsNullOrEmpty(model.BranchSSID) && model.BranchSSID.Length < 10 ? model.BranchSSID.PadLeft(10, '0') : model.BranchSSID,
                            model.OrderReason,//PURCHASE_ORDER_MST => ORDER_REASON*
                            model.ProposalType,//PURCHASE_ORDER_TYPE => PROPOSAL_TYPE*
                            model.SalesOrganization,// PURCHASE_ORDER_MST => SALES_ORGANIZATION* 5100 ez
                            model.DistrChan,//PURCHASE_ORDER_MST => DIST_CHAN*
                            model.Division,//PURCHASE_ORDER_MST => DIVISION*
                            model.ModelKod,
                            model.CreateDate.AddDays(2).ToString("yyyyMMdd"),//PURCHASE_ORDER_MST => CREATE_DATE+30*
                            model.DeliveryPriority.ToString(),//PURCHASE_ORDER_MST => DELIVERY_PRIORITY*
                            model.ItemCategory,//PURCHASE_ORDER_MST => ITEM_CATEG*
                            model.Description + " " + model.VinNo,//PURCHASE_ORDER_MST => DESCRIPTION* + " " + VEHICLE => VIN_NO*
                            model.PoNumber.ToString(),
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            model.AllDetParts//model.AllDetParts//DET 'daki tüm parça ve quantityler
                            );
                        DataTable dt = new DataTable();
                        dt = rValue.Tables["Table1"];
                        //Fill our model from datatable
                        serviceModel.ListModel = dt.AsEnumerable().Select(row => new PurchaseOrderServiceListModel
                        {
                            Type = row.Field<string>("TYPE"),
                            ID = row.Field<string>("ID"),
                            Number = row.Field<string>("NUMBER"),
                            Message = row.Field<string>("MESSAGE"),
                            MessageV1 = row.Field<string>("MESSAGE_V1"),
                            MessageV2 = row.Field<string>("MESSAGE_V2")

                        }).ToList();
                        serviceModel.AllParts = model.AllDetParts;
                        serviceModel.PoNumber = model.PoNumber;

                        bl.ServiceToDb(UserManager.UserInfo, serviceModel);
                        model.OrderNo = serviceModel.OrderNo;

                        if (serviceModel.ErrorNo > 0)
                        {
                            //Log
                            logModel.LogType = CommonValues.LogType.Response;
                            logModel.LogErrorDesc = serviceModel.ErrorMessage;
                            callBl.LogResponseService(logModel);

                            model.ErrorNo = 1;
                            model.ErrorMessage = serviceModel.ErrorMessage;
                        }
                        //Log
                        logModel.LogType = CommonValues.LogType.Response;
                        logModel.LogXml = XElement.Parse(rValue.ToXml());
                        callBl.LogResponseService(logModel);
                    }
                }
            }
            catch (Exception ex)
            {
                //Log
                logModel.LogType = CommonValues.LogType.Response;
                logModel.LogErrorDesc = ex.Message;
                callBl.LogResponseService(logModel);

                model.ErrorNo = 1;
                model.ErrorMessage = ex.Message;
            }
        }

        #endregion

        #region PurchaseOrder Query

        [HttpPost]
        public JsonResult PurchaseOrderQuery(string poNumber)
        {
            string rJson = string.Empty;
            int isError = 0;
            //if (General.IsTest)
            //    return Json(new { value = rJson, isError = isError });

            var bl = new PurchaseOrderBL();
            string offerNo = bl.GetOfferNo(poNumber).Model;

            if (!string.IsNullOrEmpty(offerNo))
            {
                try
                {
                    using (var pssc = GetClient())
                    {
                        string psUser = WebConfigurationManager.AppSettings["PSSCUser"];
                        string psPassword = WebConfigurationManager.AppSettings["PSSCPass"];

                        DataSet rValue = pssc.ZYP_SD_WEB_ORDER_STATUS(psUser, psPassword, offerNo);
                        DataTable dt = new DataTable();
                        dt = rValue.Tables["Table1"];
                        if (dt != null && dt.Rows.Count != 0)
                        {
                            rJson = CommonBL.ListLookup(UserManager.UserInfo, "ORDER_STATUS_TYPE").Data
                                .Where(p => p.Value == dt.Rows[0]["STATUSNR"].GetValue<string>())
                                .Select(p => p.Text)
                                .FirstOrDefault();

                        }
                        else
                        {
                            rJson = string.Format(MessageResource.Global_Warning_ServiceError, "ZYP_SD_WEB_ORDER_STATUS");
                            isError = 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    isError = 1;
                    rJson = ex.Message;
                }
            }
            else
                rJson = MessageResource.Global_Error_OfferNoCanNotFind;

            return Json(new { value = rJson, isError = isError });
        }

        #endregion

        #region PurchaseOrder Search

        [ValidateAntiForgeryToken]
        public JsonResult GetPartCode(string partId)
        {
            if (!string.IsNullOrEmpty(partId))
            {
                SparePartBL spBo = new SparePartBL();
                SparePartIndexViewModel spModel = new SparePartIndexViewModel();
                spModel.PartId = partId.GetValue<int>();

                spBo.GetSparePart(UserManager.UserInfo, spModel);

                return Json(new { PartCode = spModel.PartCode });
            }
            else
            {
                return null;
            }
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrder.PurchaseOrderSearchIndex)]
        [HttpGet]
        public ActionResult PurchaseOrderSearchIndex(string poNumber)
        {
            PurchaseOrderSearchListModel model = new PurchaseOrderSearchListModel();
            model.DMSOrderNo = poNumber;
            //TODO: çanlı geçisinde
            ViewBag.SLPOType = new PurchaseOrderTypeBL().PurchaseOrderTypeList(UserManager.UserInfo).Data;//PurchaseOrderTypeBL.ListPurchaseOrderTypeAsSelectListItem(UserManager.UserInfo.GetUserDealerId(),null,null,null,null);//CommonBL.ListLookup("PO_TYPE");
            ViewBag.StatusList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.ZDMSTeklifRaporu).Data;
            ViewBag.PoStatusList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.PurchaseOrderStatus).Data;
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrder.PurchaseOrderSearchIndex)]
        public ActionResult ListPurchaseOrderSearch(PurchaseOrderSearchListModel model)
        {
            List<SelectListItem> statusListTR = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.ZDMSTeklifRaporu, "TR").Data;
            List<SelectListItem> statusListEN = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.ZDMSTeklifRaporu, "EN").Data;
            bool isValid = true;
            string errorMessage = string.Empty;
            List<PurchaseOrderSearchListModel> list = new List<PurchaseOrderSearchListModel>();

            PurchaseOrderDetailBL detailBo = new PurchaseOrderDetailBL();

            DealerBL dealerBo = new DealerBL();
            DealerViewModel dealerModel = dealerBo.GetDealer(UserManager.UserInfo, UserManager.UserInfo.GetUserDealerId()).Model;
            /*
            Sorgulama kriterlerinden (başlangıç ve bitiş tarihi) veya (Otokar Teklif no veya DMS Sipariş no) girilmesi zorunludur. 
            Yoksa sorgulama yapılamaz. Diğer alanlar da bunların yanında girilebilir ama en azından bu kriterler dolu olmalıdır.
            */
            if ((model.StartDate == null || model.EndDate == null) &&
                (model.DMSOrderNo == null && model.OtokarProposeNo == null))
            {
                errorMessage = MessageResource.PurchaseOrderSearch_Warning_Input;
                isValid = false;
            }
            if (isValid)
            {
                try
                {
                    model.I_VKORG = string.Empty;
                    model.I_VTWEG = string.Empty;
                    model.I_SPART = string.Empty;
                    model.I_AUART = string.Empty;
                    model.I_LPRIO = string.Empty;
                    model.I_AUGRU = string.Empty;
                    model.I_SASBEG = string.Empty;
                    model.I_SASEND = string.Empty;
                    model.I_RFSTK = string.Empty;
                    model.I_KUNNR = string.Empty;
                    model.S_VBELN = string.Empty;
                    model.S_MATNR = string.Empty;
                    model.I_DURUM = string.Empty;
                    /*
                     Sipariş tipi seçilirse 
                        I_AUART: PURCHASE_ORDER_TYPE.PROPOSAL_TYPE
                        I_LPRIO: PURCHASE_ORDER_TYPE.DELIVERY_PRIORITY
                        I_AUGRU: PURCHASE_ORDER_TYPE.ORD_REASON
                        I_VKORG: Bu bilgi PURCHASE_ORDER_TYPE_PURCHASE_ORDER_GROUP tablosunda bulunuyor. 
                     *  Bu tabloya ekranı açan kullanıcının DEALER.ID_PO_GROUP ve  PURCHASE_ORDER_TYPE ile gidilir. 
                     *  PURCHASE_ORDER_TYPE_PURCHASE_ORDER_GROUP.SALES_ORGANIZATION değeri.
                        I_VTWEG:Bu bilgi PURCHASE_ORDER_TYPE_PURCHASE_ORDER_GROUP tablosunda bulunuyor. 
                     *  Bu tabloya ekranı açan kullanıcının DEALER.ID_PO_GROUP ve  PURCHASE_ORDER_TYPE ile gidilir. 
                     *  PURCHASE_ORDER_TYPE_PURCHASE_ORDER_GROUP.DISTR_CHAN değeri.
                        I_SPART:Bu bilgi PURCHASE_ORDER_TYPE_PURCHASE_ORDER_GROUP tablosunda bulunuyor. 
                     *  Bu tabloya ekranı açan kullanıcının DEALER.ID_PO_GROUP ve  PURCHASE_ORDER_TYPE ile gidilir. 
                     *  PURCHASE_ORDER_TYPE_PURCHASE_ORDER_GROUP.DIVISION değeri.
                     */
                    if (model.PurchaseType != null)
                    {
                        PurchaseOrderTypeViewModel poTypeModel = new PurchaseOrderTypeViewModel();
                        poTypeModel.PurchaseOrderTypeId = model.PurchaseType.GetValue<int>();

                        PurchaseOrderTypeBL poTypeBo = new PurchaseOrderTypeBL();
                        poTypeModel = poTypeBo.Get(UserManager.UserInfo, poTypeModel).Model;

                        model.I_AUART = poTypeModel.ProposalType;
                        model.I_LPRIO = poTypeModel.DeliveryPriority.GetValue<string>();
                        model.I_AUGRU = poTypeModel.OrderReason;

                        PurchaseOrderMatchViewModel poMatchModel = new PurchaseOrderMatchViewModel();
                        poMatchModel.PurhcaseOrderTypeId = poTypeModel.PurchaseOrderTypeId;
                        poMatchModel.PurhcaseOrderGroupId = dealerModel.PurchaseOrderGroupId.GetValue<int>();

                        PurchaseOrderMatchBL poMatchBo = new PurchaseOrderMatchBL();
                        poMatchModel = poMatchBo.Get(UserManager.UserInfo, poMatchModel).Model;

                        model.I_VKORG = poMatchModel.SalesOrganization;
                        model.I_VTWEG = poMatchModel.DistrChan;
                        model.I_SPART = poMatchModel.Division;
                    }
                    // TFS NO : 28120 OYA 22.01.2015 eklendi.
                    if (model.StatusId != null)
                    {
                        model.I_DURUM = (from e in statusListTR.AsEnumerable()
                                         where e.Value == model.StatusId.GetValue<string>()
                                         select e.Text).First();
                    }
                    /*
                        I_SASBEG: Ekrandan girilen başlangıç tarihi
                        I_SASEND: Ekrandan girilen bitiş tarihi
                        I_RFSTK: 0
                        I_KUNNR: Şube siparişi check edilirse DEALER.DEALER_BRANCH_SSID check etmezse DEALER.DEALER_SSID

                        Parça S_MATNR
                        DMS sipariş no yu girmiş ise
                        purchase_order_det ten SAP_OFFER_NO çekilecek
                        ve bu bilgi S_VBELN ye set edilecek (dms_sipariş no girmiş ise ve karşılığında SAP_OFFER_NO bulamıyorsak 
                           Otokar teklif no bulunamadı diyip servisi çağırmammız lazım)
                        Otokar Teklif no girmişse (DMS Sipariş no ile birlikte girilemez) S_VBELN ye set edilecek
                     */
                    if (model.StartDate != null)
                    {
                        DateTime startDate = model.StartDate.GetValue<DateTime>();
                        model.I_SASBEG = startDate.Year.ToString("D2") + startDate.Month.ToString("D2") +
                                         startDate.Day.ToString("D2");
                    }
                    if (model.EndDate != null)
                    {
                        DateTime endDate = model.EndDate.GetValue<DateTime>();
                        model.I_SASEND = endDate.Year.ToString("D2") + endDate.Month.ToString("D2") +
                                         endDate.Day.ToString("D2");
                    }

                    // TFS NO : 28120 OYA 22.01.2015 değiştirildi.
                    //model.I_RFSTK = model.IsOpenOrder ? "X" : "0";
                    model.I_KUNNR = model.IsDealerOrder ? dealerModel.BranchSSID : dealerModel.SSID;

                    if (model.PartCodeList != null && !string.IsNullOrEmpty(model.PartCodeList))
                    {
                        model.S_MATNR = model.PartCodeList.Replace(",", ";");
                    }

                    if (!String.IsNullOrEmpty(model.DMSOrderNo))
                    {
                        int detailCount = 0;
                        PurchaseOrderDetailListModel detailModel = new PurchaseOrderDetailListModel();
                        detailModel.PurchaseOrderNumber = model.DMSOrderNo.GetValue<int>();
                        List<PurchaseOrderDetailListModel> detailList = detailBo.ListPurchaseOrderDetails(UserManager.UserInfo, detailModel, out detailCount).Data;
                        if (detailCount != 0)
                        {
                            model.S_VBELN = detailList.ElementAt(0).SAPOfferNo;
                        }
                        else
                        {
                            errorMessage = MessageResource.PurchaseOrderSearch_Warning_CannotFindOtokarPurchaseNo;
                            isValid = false;
                        }
                    }

                    if (!String.IsNullOrEmpty(model.OtokarProposeNo))
                    {
                        model.S_VBELN = model.OtokarProposeNo;
                    }

                    if (isValid)
                    {
                        using (var pssc = GetClient())
                        {
                            string psUser = WebConfigurationManager.AppSettings["PSSCUser"];
                            string psPassword = WebConfigurationManager.AppSettings["PSSCPass"];

                            DataSet rValue = pssc.ZDMS_TEKLIF_RAPORU(psUser, psPassword,
                                                                 model.I_VKORG,
                                                                 model.I_VTWEG,
                                                                 model.I_SPART,
                                                                 model.I_AUART,
                                                                 model.I_LPRIO,
                                                                 model.I_AUGRU,
                                                                 model.I_SASBEG,
                                                                 model.I_SASEND,
                                                                 model.I_KUNNR,
                                                                 model.S_VBELN,
                                                                 model.S_MATNR,
                                                                 model.I_DURUM);
                            DataTable dt = new DataTable();
                            dt = rValue.Tables["Table2"];
                            if (dt == null || dt.Rows.Count == 0)
                            {
                                errorMessage = MessageResource.PurchaseOrderSearch_Warning_CannotfindAnyData;
                            }
                            else
                            {
                                list.AddRange(from DataRow r in dt.Rows
                                              select new PurchaseOrderSearchListModel
                                              {
                                                  VBELN = r["VBELN"].GetValue<string>(),
                                                  POSNR = r["POSNR"].GetValue<string>(),
                                                  ZZCHASSIS_NUM = r["ZZCHASSIS_NUM"].GetValue<string>(),
                                                  ANGDT = r["ANGDT"].GetValue<string>(),
                                                  MATNR = r["MATNR"].GetValue<string>(),
                                                  BSTKD = r["BSTKD"].GetValue<string>(),
                                                  KWMENG = r["KWMENG"].ToString().ToCommaString().GetValue<decimal>(),
                                                  SASI_TEXT = r["SASI_TEXT"].GetValue<string>(),
                                                  DPARCA = r["DPARCA"].GetValue<string>(),
                                                  BAKIYE = r["BAKIYE"].ToString().ToCommaString().GetValue<decimal>(),
                                                  TEYIT = r["TEYIT"].ToString().ToCommaString().GetValue<decimal>(),
                                                  PAKETLEME = r["PAKETLEME"].ToString().ToCommaString().GetValue<decimal>(),
                                                  MCIKISI = r["MCIKISI"].ToString().ToCommaString().GetValue<decimal>(),
                                                  IRSNO = r["IRSNO"].GetValue<string>(),
                                                  BLDAT = r["BLDAT"].GetValue<string>(),
                                                  TAHTES = r["TAHTES"].GetValue<string>(),
                                                  PSTYV = r["PSTYV"].GetValue<string>(),
                                                  DURUM = UserManager.LanguageCode == "TR" ? r["DURUM"].GetValue<string>()
                                                      :
                                                      (from f in statusListEN.AsEnumerable()
                                                       where f.Value == (from e in statusListTR.AsEnumerable()
                                                                         where e.Text == r["DURUM"].GetValue<string>()
                                                                         select e.Value).First()
                                                       select f.Text).First()
                                                  // TFS NO : 28120 OYA 22.01.2015 değiştirildi.
                                                  /*DURUM = r["DURUM"].GetValue<string>().Equals("Onaylı")
                                                              ? r["KWMENG"].GetValue<string>()
                                                                           .Trim()
                                                                           .Equals(
                                                                               r["MCIKISI"].GetValue<string>()
                                                                                           .Trim())
                                                                    ? "Onaylı - Tamamlandı"
                                                                    : r["KWMENG"].GetValue<string>()
                                                                                 .Trim()
                                                                                 .Equals(
                                                                                     r["BAKIYE"].GetValue<string>()
                                                                                                .Trim())
                                                                          ? "Onaylı - Açık"
                                                                          : r["MCIKISI"].GetValue<decimal>() <
                                                                            r["KWMENG"].GetValue<decimal>()
                                                                                ? "Onaylı - Kısmi"
                                                                                : r["DURUM"].GetValue<string>()
                                                              : r["DURUM"].GetValue<string>()*/
                                              });

                                // master statüsü de eklenecek OYA TFS No : 27520 08.12.2014
                                // Purchase Order Detail SAP Offer No sahasının başında 00 olmadığından servisten gelen değerin başındaki 
                                // 00'larda kaldırılarak arama yapılıyor. TFS No: 28013 16.01.2015
                                foreach (PurchaseOrderSearchListModel r in list)
                                {
                                    string vbeln = r.VBELN;
                                    string partCode = r.MATNR;

                                    if (!string.IsNullOrEmpty(vbeln))
                                    {
                                        if (vbeln.StartsWith("00"))
                                            vbeln = vbeln.Substring(2, 8);

                                        PurchaseOrderDetailViewModel foundModel = detailBo.GetPurchaseOrderDetailsBySapInfo(vbeln, null, null).Model;
                                        if (foundModel.PurchaseOrderNumber != 0)
                                        {
                                            PurchaseOrderViewModel poModel = new PurchaseOrderViewModel();
                                            poModel.PoNumber = foundModel.PurchaseOrderNumber;

                                            PurchaseOrderBL poBo = new PurchaseOrderBL();
                                            poBo.GetPurchaseOrder(UserManager.UserInfo, poModel);
                                            r.ParentStatus = poModel.StatusName;

                                            //SparePartIndexViewModel spModel = new SparePartIndexViewModel();
                                            //spModel.PartCode = partCode;
                                            //SparePartBL spBo = new SparePartBL();
                                            //spBo.GetSparePart(spModel);
                                            //foundModel.SAPShipIdPart = spModel.PartId;
                                            //foundModel.CommandType = CommonValues.DMLType.Update;
                                            //detailBo.DMLPurchaseOrderDetail(foundModel);
                                        }

                                    }
                                }

                                // TFS NO : 28327 OYA 03.02.2015
                                // Purchase Order Detail SAP OFFER NO , VBELN bilgisi ile eşleşmiyorsa listeden çıkarılıyor.
                                list.RemoveAll(c => c.ParentStatus == null);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                }
            }
            return Json(new
            {
                Data = list,
                Total = list.Count,
                ErrorMessage = errorMessage
            });
        }

        #endregion

    }
}
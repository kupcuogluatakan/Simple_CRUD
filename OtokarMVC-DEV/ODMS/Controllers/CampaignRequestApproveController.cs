using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Xml.Linq;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.Campaign;
using ODMSModel.CampaignRequest;
using ODMSModel.CampaignRequestApprove;
using ODMSModel.Dealer;
using ODMSModel.PurchaseOrder;
using ODMSModel.PurchaseOrderDetail;
using ODMSModel.PurchaseOrderMatch;
using ODMSModel.PurchaseOrderType;
using ODMS.OtokarService;
using ODMSModel.ServiceCallSchedule;
using ODMSCommon.Security;
using ODMSModel.CampaignVehicle;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class CampaignRequestApproveController : ControllerBase
    {
        [HttpGet]
        public JsonResult ListSupplierDealers(string campaignRequestId, string requiredQuantity)
        {
            List<SelectListItem> dealerList =
                CampaignRequestApproveBL.ListSupplierDealerAsSelectListItem(campaignRequestId.GetValue<int>(),
                requiredQuantity.GetValue<int>()).Data;
            var control = (from e in dealerList.AsEnumerable()
                           where string.IsNullOrEmpty(e.Text)
                           select e);
            if (control.Any())
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(dealerList, JsonRequestBehavior.AllowGet);
            }
        }
        private List<SelectListItem> GetSupplierDealerList(int quantity, int campaignRequestId)
        {
            List<SelectListItem> dealerList = CampaignRequestApproveBL.ListSupplierDealerAsSelectListItem(campaignRequestId.GetValue<int>(),
                quantity).Data;

            return dealerList;
        }
        private void SetDefaults()
        {
            ViewBag.CampaignList = CampaignBL.ListCampaignAsSelectListItem(UserManager.UserInfo, null).Data;
            ViewBag.ModelList = VehicleModelBL.ListVehicleModelAsSelectList(UserManager.UserInfo).Data;
            ViewBag.SupplierTypeList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.CampaignRequestSupplyTypeLookup).Data;
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            List<SelectListItem> statusList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.CampaignRequestStatusLookup).Data;
            ViewBag.RequestStatusList = (from e in statusList
                                         where e.Value.GetValue<int>() != (int)CommonValues.CampaignRequestStatus.NewRecord
                                         select e).ToList<SelectListItem>();
        }

        #region Campaign RequestApprove Index

        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignRequestApprove.CampaignRequestApproveIndex)]
        [HttpGet]
        public ActionResult CampaignRequestApproveIndex()
        {
            CampaignRequestApproveListModel model = new CampaignRequestApproveListModel();
            SetDefaults();
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignRequestApprove.CampaignRequestApproveIndex, CommonValues.PermissionCodes.CampaignRequestApprove.CampaignRequestApproveUpdate)]
        [HttpPost]
        public ActionResult GetCampaignVinNumbers(string CampaignCode)
        {

            var _bll = new CampaignVehicleBL();
            var totalCnt = 0;
            var v = new CampaignVehicleListModel();
            v.CampaignCode = CampaignCode;

            var response = _bll.ListCampaignVehiclesMain(UserManager.UserInfo, v, out totalCnt);


            return Json(new
            {
                Data = response.Data,
                Total = totalCnt
            });

        }


        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignRequestApprove.CampaignRequestApproveIndex,
            CommonValues.PermissionCodes.CampaignRequestApprove.CampaignRequestApproveDetails)]
        public ActionResult ListCampaignRequestApprove([DataSourceRequest] DataSourceRequest request,
                                                       CampaignRequestApproveListModel model)
        {
            var campaignRequestApproveBo = new CampaignRequestApproveBL();
            var v = new CampaignRequestApproveListModel(request);
            var totalCnt = 0;
            v.RequestDealerId = model.RequestDealerId;
            v.SupplierDealerId = model.SupplierDealerId;
            v.SupplierTypeId = model.SupplierTypeId;
            v.ModelKod = model.ModelKod;
            v.CampaignCode = model.CampaignCode;
            v.RequestStatusId = model.RequestStatusId;
            v.Quantity = model.Quantity;
            var returnValue = campaignRequestApproveBo.ListCampaignRequestApprove(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region Campaign RequestApprove Update

        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignRequestApprove.CampaignRequestApproveIndex,
            CommonValues.PermissionCodes.CampaignRequestApprove.CampaignRequestApproveUpdate)]
        [HttpGet]
        public ActionResult CampaignRequestApproveUpdate(int campaignRequestId)
        {
            SetDefaults();
            var v = new CampaignRequestApproveViewModel();
            if (campaignRequestId > 0)
            {
                var campaignRequestApproveBo = new CampaignRequestApproveBL();
                v.CampaignRequestId = campaignRequestId;
                campaignRequestApproveBo.GetCampaignRequestApprove(UserManager.UserInfo, v);
            }
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignRequestApprove.CampaignRequestApproveIndex,
            CommonValues.PermissionCodes.CampaignRequestApprove.CampaignRequestApproveUpdate)]
        [HttpPost]
        public ActionResult CampaignRequestApproveUpdate(CampaignRequestApproveViewModel viewModel)
        {
            SetDefaults();
            var campaignRequestApproveBo = new CampaignRequestApproveBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Update;
                viewModel.SupplierDealerId = viewModel.SupplierTypeId == 1 ? null : viewModel.SupplierDealerId;
                campaignRequestApproveBo.DMLCampaignRequestApprove(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }
            return View(viewModel);
        }

        #endregion

        #region Campaign RequestApprove Details

        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignRequestApprove.CampaignRequestApproveIndex,
            CommonValues.PermissionCodes.CampaignRequestApprove.CampaignRequestApproveDetails)]
        [HttpGet]
        public ActionResult CampaignRequestApproveDetails(int campaignRequestId)
        {
            var v = new CampaignRequestApproveViewModel();
            var campaignRequestApproveBo = new CampaignRequestApproveBL();

            v.CampaignRequestId = campaignRequestId;
            campaignRequestApproveBo.GetCampaignRequestApprove(UserManager.UserInfo, v);

            return View(v);
        }

        #endregion

        #region Campaign RequestApprove Approve

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignRequestApprove.CampaignRequestApproveIndex,
            CommonValues.PermissionCodes.CampaignRequestApprove.CampaignRequestApproveUpdate)]
        public ActionResult CampaignRequestApproveApprove(int id)
        {
            string errorMessage = string.Empty;
            CampaignRequestViewModel model = new CampaignRequestViewModel();
            model.IdCampaignRequest = id;
            CampaignRequestBL campReqBo = new CampaignRequestBL();
            campReqBo.GetCampaignRequest(UserManager.UserInfo, model);

            CampaignRequestApproveViewModel craModel = new CampaignRequestApproveViewModel();
            craModel.CampaignRequestId = id;
            CampaignRequestApproveBL craBo = new CampaignRequestApproveBL();
            craBo.GetCampaignRequestApprove(UserManager.UserInfo, craModel);

            if (craModel.RequestStatusId == (int)CommonValues.CampaignRequestStatus.Approved)
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.Error_DB_CampaignRequestApproved);
            }


            // 02.04.2018 onaylama adımında da seçilen diğer bayi için stok kontrolü yapılıyor
            List<SelectListItem> supplierDealerList = GetSupplierDealerList(model.Quantity.GetValue<int>(), id);
            var dealerHasStock = supplierDealerList.Any(e => e.Value == craModel.SupplierDealerId.ToString());
            if (!craModel.SupplierDealerId.HasValue || dealerHasStock)
            {
                // TFS No : 27394 OYA Başka bayiden seçilirse başka bayi için purchase order yaratılacak
                errorMessage = PurchaseOrderCreate(model, craModel.SupplierDealerId);


                if (!String.IsNullOrEmpty(errorMessage))
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error, errorMessage);

                //başka bayi seçilirse mail atcak


                model.RequestStatus = (int)CommonValues.CampaignRequestStatus.Approved;
                model.CommandType = "A";
                campReqBo.DMLCampaignRequest(UserManager.UserInfo, model);

                if (model.ErrorNo > 0)
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);

                // TFS No : 27807 OYA 02.01.2015

                DealerBL dBo = new DealerBL();
                DealerViewModel dealerModel = dBo.GetDealer(UserManager.UserInfo, craModel.RequestDealerId.GetValue<int>()).Model;
                DealerViewModel supplierDealerModel = dBo.GetDealer(UserManager.UserInfo, craModel.SupplierDealerId.GetValue<int>()).Model;
                string approver = craModel.SupplierTypeId == 1 ? "Otokar" : supplierDealerModel.ShortName;
                string to = dealerModel.ContactEmail;
                string subject = string.Format(MessageResource.CampaignRequestApprove_Mail_Subject, craModel.CampaignRequestId);
                string body = string.Format(MessageResource.CampaignRequestApprove_Mail_AcceptBody, craModel.CampaignRequestId, "Otokar", approver);
                CommonBL.SendDbMail(to, subject, body);

                if (craModel.SupplierDealerId.HasValue)
                {

                    string to2 = supplierDealerModel.ContactEmail;
                    string subject2 = MessageResource.DealerPurchaseOrder_Mail_Subject;
                    string body2 = string.Format(MessageResource.DealerPurchaseOrder_Mail_Body, dealerModel.Name, model.PoNumber);
                    CommonBL.SendDbMail(to2, subject2, body2);
                }

                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            }
            else
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.Error_DB_NoStockFound);
            }
        }

        private string PurchaseOrderCreate(CampaignRequestViewModel model, int? supplierDealerId)
        {
            var poModel = new PurchaseOrderViewModel();

            #region PURCHASE_ORDER_MST INSERT

            PurchaseOrderBL pobO = new PurchaseOrderBL();
            PurchaseOrderViewModel mstModel = new PurchaseOrderViewModel();
            mstModel.IdDealer = model.IdDealer;
            if (supplierDealerId != null && supplierDealerId != 0)
            {
                mstModel.SupplierIdDealer = supplierDealerId;
            }
            mstModel.PoType = CommonBL.GetGeneralParameterValue(CommonValues.GeneralParameters.CampaignPurchaseOrderType).Model.GetValue<int>();
            mstModel.IsBranchOrder = false;
            mstModel.StatusId = 0;
            mstModel.StatusDetail = 0;

            // TFS No : 26349 OYA PURCHASE_ORDER_MST tablosuna MODEL_KOD alanı eklendi. bu tabloya kayıt atılıyorken 
            //onaylanan kampanyaya ait model kod bilgisi CAMPAING tablosundan alınıp PURCHASE_ORDER_MST tablosuna yazılacak. 
            CampaignBL campBo = new CampaignBL();
            CampaignViewModel campModel = new CampaignViewModel();
            campModel.CampaignCode = model.CampaignCode;
            campBo.GetCampaign(UserManager.UserInfo, campModel);
            mstModel.ModelKod = campModel.ModelKod;

            DealerBL dealerBo = new DealerBL();
            DealerViewModel dealerModel = dealerBo.GetDealer(UserManager.UserInfo, mstModel.IdDealer.GetValue<int>()).Model;

            PurchaseOrderMatchViewModel matchModel = new PurchaseOrderMatchViewModel();
            matchModel.PurhcaseOrderGroupId = dealerModel.PurchaseOrderGroupId.GetValue<int>();
            matchModel.PurhcaseOrderTypeId = mstModel.PoType.GetValue<int>();
            PurchaseOrderMatchBL matchBo = new PurchaseOrderMatchBL();
            matchModel = matchBo.Get(UserManager.UserInfo, matchModel).Model;

            PurchaseOrderTypeViewModel poTypeModel = new PurchaseOrderTypeViewModel();
            poTypeModel.PurchaseOrderTypeId = mstModel.PoType.GetValue<int>();
            PurchaseOrderTypeBL poTypeBo = new PurchaseOrderTypeBL();
            poTypeModel = poTypeBo.Get(UserManager.UserInfo, poTypeModel).Model;
            // TFS No : 26177 OYA GENERAL_PARAMETER.CAMPAIGN_PO_TYPE ın değeri ile PURCHASE_ORDER_TYPE.ID_PO_TYPE a gidilir ve ilgili 
            // kaydın ID_STOCK_TYPE değeri alınır. 
            mstModel.IdStockType = poTypeModel.StockTypeId;
            mstModel.ProposalType = poTypeModel.ProposalType;
            mstModel.DeliveryPriority = poTypeModel.DeliveryPriority;
            mstModel.SalesOrganization = matchModel.SalesOrganization;
            mstModel.DistrChan = matchModel.DistrChan;
            mstModel.Division = matchModel.Division;
            mstModel.OrderReason = poTypeModel.OrderReason;
            mstModel.ItemCategory = poTypeModel.ItemCategory;
            mstModel.Status = (int)CommonValues.PurchaseOrderStatus.NewRecord;
            mstModel.CommandType = CommonValues.DMLType.Insert;

            if (model.VehicleId > 0) // iş kartından oluşturulmuş ise burası 0'dan büyük geliyor.
                mstModel.VehicleId = model.VehicleId;

            pobO.DMLPurchaseOrder(UserManager.UserInfo, mstModel);

            if (mstModel.ErrorNo > 0)
            {
                return mstModel.ErrorMessage;
            }

            #endregion

            #region PURCHASE_ORDER_DET INSERT

            PurchaseOrderDetailBL poDetBo = new PurchaseOrderDetailBL();
            CampaignRequestBL detailBo = new CampaignRequestBL();
            CampaignRequestDetailListModel listModel = new CampaignRequestDetailListModel();
            listModel.CampaignRequestId = model.IdCampaignRequest.GetValue<int>();

            if (supplierDealerId != null && supplierDealerId != 0)
            {
                listModel.SupplyTypeId = (int)CommonValues.SupplyPort.Supplier;
            }
            else
            {
                listModel.SupplyTypeId = (int)CommonValues.SupplyPort.Otokar;
            }

            List<CampaignRequestDetailListModel> detailList = detailBo.ListCampaignRequestDetailsAndQuantity(listModel).Data;
            if (detailList.Count != 0)
            {
                foreach (CampaignRequestDetailListModel campaignRequestDetailListModel in detailList)
                {
                    PurchaseOrderDetailViewModel poDetModel = new PurchaseOrderDetailViewModel();
                    poDetModel.PurchaseOrderNumber = mstModel.PoNumber.GetValue<int>();

                    poDetModel.OrderPrice = 0;
                    poDetModel.PartId = campaignRequestDetailListModel.PartId;
                    var desire = campaignRequestDetailListModel.DetQty;

                    var order =
                        Math.Ceiling((campaignRequestDetailListModel.DetQty) /
                                     campaignRequestDetailListModel.PackageQty) *
                        campaignRequestDetailListModel.PackageQty;

                    poDetModel.PackageQuantity = campaignRequestDetailListModel.PackageQty;
                    poDetModel.DesireQuantity = desire;
                    /*TFS NO: 27719 OYA 25.12.2014 * Kampanya onayında başka bayi seçilmiş ise satınalma kaydında paketleme adedine 
                     * göre yuvarlama olmayacak. yani kampanya siparişi ise ve başka bayi seçili ise satınalma siparişinde paket adete 
                     * göre yuvarlanmayacak. istenilen adet sipariş adet olarak set edilecek. */
                    poDetModel.OrderQuantity = listModel.SupplyTypeId == (int)CommonValues.SupplyPort.Supplier
                                                   ? campaignRequestDetailListModel.DetQty
                                                   : order;

                    poDetModel.StatusId = (int)CommonValues.PurchaseOrderDetailStatus.Open;
                    poDetModel.CommandType = CommonValues.DMLType.Insert;
                    poDetBo.DMLPurchaseOrderDetail(UserManager.UserInfo, poDetModel);

                    if (poDetModel.ErrorNo > 0)
                    {
                        return poDetModel.ErrorMessage;
                    }

                }

                var poBL = new PurchaseOrderBL();
                poModel.PoNumber = mstModel.PoNumber;
                poBL.GetPurchaseOrder(UserManager.UserInfo, poModel);

                /*TFS NO: 27719 OYA 25.12.2014 * Kampanya sipariş onay ekranında sipariş onaylanmadan önce siparişin başka bayiden 
                 * temin edileceği seçilir ve talep güncellenir. Ardından talep onaylanır. 
                 * Kampanya talebini yapan bayinin Satınalma sipariş (PO) ekranına bakıldığında ilgili siparişte SAP NO var. 
                 * Onaylama işleminde eğer başka bayi seçili ise otokar tarafına da sipariş geçilmemelidir.
                 * (supplierDealer Id doluysa create et webservis cagırma, merkezdense webservis cagıralack ) */
                if (listModel.SupplyTypeId == (int)CommonValues.SupplyPort.Otokar)
                {
                    /**Service Call*/
                    OtokarWebOrderCreate(poModel);
                }
                if (poModel.ErrorNo > 0)
                {
                    mstModel.Status = (int)CommonValues.PurchaseOrderStatus.OpenPurchaseOrder;
                    mstModel.CommandType = CommonValues.DMLType.Erase;
                    model.RequestStatus = (int)CommonValues.CampaignRequestStatus.WaitingForApproval;
                }
                else
                {
                    mstModel.Status = (int)CommonValues.PurchaseOrderStatus.OpenPurchaseOrder;
                    mstModel.CommandType = CommonValues.DMLType.Update;
                    model.RequestStatus = (int)CommonValues.CampaignRequestStatus.Approved;

                    // TFS No : 26349 PURCHASE_ORDER_MST tablosunda yaratılan kaydın PO_NUMBER değeri de 
                    // CAMPAING_REQUEST tablosunda ki PO_NUMBER alanına update edilecek.
                    model.PoNumber = mstModel.PoNumber;
                }
                detailBo.DMLCampaignRequest(UserManager.UserInfo, model);
                poBL.DMLPurchaseOrder(UserManager.UserInfo, mstModel);

                if (poModel.ErrorNo > 0)
                    return poModel.ErrorMessage;

                if (mstModel.ErrorNo > 0)
                    return mstModel.ErrorMessage;
            }

            #endregion

            return string.Empty;
        }

        private void OtokarWebOrderCreate(PurchaseOrderViewModel model)
        {
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
                    string psUser = WebConfigurationManager.AppSettings["PSSCUser"];
                    string psPassword = WebConfigurationManager.AppSettings["PSSCPass"];

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
                            string.Empty,
                            model.OrderReason,//PURCHASE_ORDER_MST => ORDER_REASON*
                            model.ProposalType,//PURCHASE_ORDER_TYPE => PROPOSAL_TYPE*
                            model.SalesOrganization,// PURCHASE_ORDER_MST => SALES_ORGANIZATION* 5100 ez
                            model.DistrChan,//PURCHASE_ORDER_MST => DIST_CHAN*
                            model.Division,//PURCHASE_ORDER_MST => DIVISION*
                            model.ModelKod,
                            model.CreateDate.AddDays(2).ToString("yyyyMMdd"),//PURCHASE_ORDER_MST => CREATE_DATE+30*
                            model.DeliveryPriority.ToString(),//PURCHASE_ORDER_MST => DELIVERY_PRIORITY*
                            model.ItemCategory,//PURCHASE_ORDER_MST => ITEM_CATEG*
                            model.Description,//PURCHASE_ORDER_MST => DESCRIPTION*
                            model.PoNumber.ToString(),
                            string.Empty,
                            string.Empty,
                            "X",
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
                logModel.LogErrorDesc = serviceModel.ErrorMessage;
                callBl.LogResponseService(logModel);

                model.ErrorNo = 1;
                model.ErrorMessage = ex.Message;
            }
        }
        #endregion

        #region Campaign RequestApprove Reject

        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignRequestApprove.CampaignRequestApproveIndex,
            CommonValues.PermissionCodes.CampaignRequestApprove.CampaignRequestApproveUpdate)]
        public ActionResult CampaignRequestApproveReject(CampaignRequestViewModel model)
        {


            return View("_CampaignRequestApproveReject", model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignRequestApprove.CampaignRequestApproveIndex,
            CommonValues.PermissionCodes.CampaignRequestApprove.CampaignRequestApproveUpdate)]
        public ActionResult CampaignRequestApproveReject(int id, string rejectionNote = "")
        {
            CampaignRequestViewModel model = new CampaignRequestViewModel();
            model.IdCampaignRequest = id;
           
            CampaignRequestBL campReqBo = new CampaignRequestBL();
            campReqBo.GetCampaignRequest(UserManager.UserInfo, model);
            model.RejectionNote = rejectionNote;

            model.RequestStatus = (int)CommonValues.CampaignRequestStatus.Cancelled;
            model.CommandType = "A";
            campReqBo.DMLCampaignRequest(UserManager.UserInfo, model);

            if (model.ErrorNo > 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);

            // TFS No : 27807 OYA 02.01.2015
            CampaignRequestApproveViewModel craModel = new CampaignRequestApproveViewModel();
            craModel.CampaignRequestId = id;
            CampaignRequestApproveBL craBo = new CampaignRequestApproveBL();
            craBo.GetCampaignRequestApprove(UserManager.UserInfo, craModel);
            DealerBL dBo = new DealerBL();
            DealerViewModel dealerModel = dBo.GetDealer(UserManager.UserInfo, craModel.RequestDealerId.GetValue<int>()).Model;
            DealerViewModel supplierDealerModel = dBo.GetDealer(UserManager.UserInfo, craModel.SupplierDealerId.GetValue<int>()).Model;
            string to = dealerModel.ContactEmail;
            string subject = string.Format(MessageResource.CampaignRequestApprove_Mail_Subject, craModel.CampaignRequestId);
            string body = string.Format(MessageResource.CampaignRequestApprove_Mail_RejectBody, craModel.CampaignRequestId, "Otokar");
            CommonBL.SendDbMail(to, subject, body);
            
            if (!string.IsNullOrEmpty(dealerModel.RegionResponsibleEmail))
                CommonBL.SendDbMail(dealerModel.RegionResponsibleEmail, subject, body);
            
            return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
        }

        #endregion

        [AuthorizationFilter(CommonValues.PermissionCodes.CampaignRequestApprove.CampaignRequestApproveIndex,
            CommonValues.PermissionCodes.CampaignRequestApprove.CampaignRequestApproveUpdate)]
        [HttpPost]
        public ActionResult ListCampaignRequestVinApprovedCounts(CampaignRequestApproveJsonModel v)
        {
            
            var _bll = new CampaignRequestApproveBL();
            var totalCnt = 0;

            var response = _bll.ListCampaignRequestVinApprovedCounts(UserManager.UserInfo, v, out totalCnt);


            return Json(new
            {
                Data = response.Data.Select(s=>new { s.VinCode, s.Count }),
                Total = totalCnt
            });

        }



    }
}
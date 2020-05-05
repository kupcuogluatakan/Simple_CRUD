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
using ODMSModel.SparePartSaleOrder;
using ODMSModel.SparePartSaleOrderDetail;
using ODMSModel.StockCard;
using ODMSModel.StockTypeDetail;
using ODMSModel.WorkOrderPicking;
using ODMSModel.WorkOrderPickingDetail;
using ODMSBusiness.Business;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class SparePartSaleOrderDetailController : ControllerBase
    {
        #region General Methods
        private void SetDefaults()
        {
            ViewBag.DetailStatusList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.SaleOrderDetailStatus).Data;
        }
        [ValidateAntiForgeryToken]
        public JsonResult GetValues(string partId, string soNumber)
        {
            decimal shipmentQuantity = 0;
            string currencyCode = string.Empty;
            string unitName = string.Empty;
            decimal listPrice = 0;
            decimal orderPrice = 0;
            decimal listDiscountRatio = 0;
            int dealerId = UserManager.UserInfo.GetUserDealerId();

            if (!string.IsNullOrEmpty(partId))
            {
                SparePartIndexViewModel spModel = new SparePartIndexViewModel();
                spModel.PartId = partId.GetValue<int>();
                SparePartBL spBo = new SparePartBL();
                spBo.GetSparePart(UserManager.UserInfo, spModel);
                shipmentQuantity = spModel.ShipQuantity.GetValue<decimal>();
                unitName = spModel.UnitName;

                DealerBL dealerBo = new DealerBL();
                DealerViewModel dealerViewModel = dealerBo.GetDealer(UserManager.UserInfo, dealerId).Model;
                currencyCode = dealerViewModel.CurrencyCode;

                CommonBL bo = new CommonBL();
                listPrice = bo.GetPriceByDealerPartVehicleAndType(partId.GetValue<int>(),0,dealerId, CommonValues.ListPriceLabel).Model;

                listDiscountRatio = spBo.GetCustomerPartDiscount(partId.GetValue<int>(), UserManager.UserInfo.GetUserDealerId(), null,"W").Model;

                orderPrice = (listPrice * ((100 - listDiscountRatio) / 100));
            }
            return
                Json(
                    new
                    {
                        CurrencyCode = currencyCode,
                        UnitName = unitName,
                        ShipmentQuantity = shipmentQuantity,
                        ListPrice = listPrice,
                        OrderPrice = orderPrice,
                        ListDiscountRatio = listDiscountRatio
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

        #region Spare Part Sale Order Detail Index
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSaleOrderDetail.SparePartSaleOrderDetailIndex)]
        public ActionResult SparePartSaleOrderDetailIndex(string soNumber, bool openCreatePopup = false)
        {
            var model = new SparePartSaleOrderDetailListModel { SoNumber = soNumber };
            ViewBag.OpenCreatePopup = openCreatePopup;

            int totalCount;
            SparePartSaleOrderDetailBL spsdBo = new SparePartSaleOrderDetailBL();
            model.SoNumber = soNumber;
            List<SparePartSaleOrderDetailListModel> detailList = spsdBo.ListSparePartSaleOrderDetails(UserManager.UserInfo, model, out totalCount).Data;

            SparePartSaleOrderBL bo = new SparePartSaleOrderBL();
            SparePartSaleOrderViewModel parentModel = bo.GetSparePartSaleOrder(UserManager.UserInfo, soNumber);
            model.MasterStatusId = parentModel.StatusId;
            model.PODetailCount = detailList.Count(e => e.PurchaseOrderDetailSeqNo.Length > 0);

            return PartialView(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSaleOrderDetail.SparePartSaleOrderDetailIndex, CommonValues.PermissionCodes.SparePartSaleOrderDetail.SparePartSaleOrderDetailDetails)]
        public ActionResult ListSparePartSaleOrderDetails([DataSourceRequest]DataSourceRequest request, SparePartSaleOrderDetailListModel model)
        {
            DealerBL dealerBo = new DealerBL();

            var bo = new SparePartSaleOrderDetailBL();
            var referenceModel = new SparePartSaleOrderDetailListModel(request)
            {
                SoNumber = model.SoNumber
            };
            int totalCnt;
            var returnValue = bo.ListSparePartSaleOrderDetails(UserManager.UserInfo, referenceModel, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        [HttpPost]
        public ActionResult UpdateDetails()
        {
            var list = ParseModelFromRequestInputStream<List<SparePartSaleOrderDetailListModel>>();
            var bus = new SparePartSaleOrderDetailBL();
            foreach (SparePartSaleOrderDetailListModel listModel in list)
            {
                SparePartSaleOrderDetailDetailModel detailModel = new SparePartSaleOrderDetailDetailModel();
                detailModel.SparePartSaleOrderDetailId = listModel.SparePartSaleOrderDetailId;
                detailModel.SoNumber = listModel.SoNumber;
                detailModel = bus.GetSparePartSaleOrderDetail(UserManager.UserInfo, detailModel).Model;
                detailModel.ConfirmPrice = listModel.ConfirmPrice;
                detailModel.AppliedDiscountRatio = listModel.AppliedDiscountRatio;
                detailModel.CommandType = CommonValues.DMLType.Update;
                bus.DMLSparePartSaleOrderDetail(UserManager.UserInfo, detailModel);
                if (detailModel.ErrorNo > 0)
                    return Json(new { errorMessage = detailModel.ErrorMessage, errorNo = detailModel.ErrorNo });
            }

            return Json(new { errorMessage = MessageResource.Global_Display_Success, errorNo = 0 });
        }
        #endregion

        #region Spare Part Sale Order Detail Create
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSaleOrderDetail.SparePartSaleOrderDetailIndex, CommonValues.PermissionCodes.SparePartSaleOrderDetail.SparePartSaleOrderDetailCreate)]
        public ActionResult SparePartSaleOrderDetailCreate(string soNumber)
        {
            ViewBag.IsSuccess = true;
            var model = new SparePartSaleOrderDetailDetailModel
            {
                SoNumber = soNumber,
                StatusId = (int)CommonValues.SparePartSaleOrderDetailStatus.OpenOrder
            };

            SparePartSaleOrderBL bo = new SparePartSaleOrderBL();
            SparePartSaleOrderViewModel parentModel = bo.GetSparePartSaleOrder(UserManager.UserInfo, soNumber);
            model.MasterStatusId = parentModel.StatusId;
            model.MasterIsPriceFixed = parentModel.IsFixedPrice;

            SetDefaults();
            return View(model);
        }

        public ActionResult ExcelSample()
        {
            var bo = new SparePartSaleOrderDetailBL();
            var ms = bo.SampleExcelFormat();
            return File(ms, CommonValues.ExcelContentType, MessageResource.SparePartSaleDetail_PageTitle_Index + CommonValues.ExcelExtOld);
        }
        private SparePartSaleOrderDetailDetailModel InsertSingle(SparePartSaleOrderDetailDetailModel model)
        {
            var bo = new SparePartSaleOrderDetailBL();
            CommonBL cbo = new CommonBL();
            SparePartBL spBo = new SparePartBL();
            SetDefaults();

            if (model.SparePartId != null)
            {
                SparePartIndexViewModel spModel = new SparePartIndexViewModel();
                spModel.PartId = model.SparePartId.GetValue<int>();
                spBo.GetSparePart(UserManager.UserInfo,spModel);
                model.SparePartName = spModel.PartCode + CommonValues.Slash + spModel.AdminDesc;
                model.ShippedQuantity = 0;//spModel.ShipQuantity.GetValue<decimal>();//bu yanlış olmu. bu değer irsaliye kesilirken set edilmeli
                model.PlannedQuantity = 0;
                model.ListPrice = cbo.GetPriceByDealerPartVehicleAndType(spModel.PartId, 0,UserManager.UserInfo.GetUserDealerId(),CommonValues.ListPriceLabel).Model;

                model.ListDiscountRatio = spBo.GetCustomerPartDiscount(model.SparePartId.GetValue<int>(), UserManager.UserInfo.GetUserDealerId(), null,
                    CommonValues.ActionType.W.ToString()).Model;
                model.ConfirmPrice = model.OrderPrice;
            }
            if (model.PlannedQuantity > model.OrderQuantity)
            {
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.SparePartSaleOrderDetail_Warning_PlannedQtyOrderQtyControl;
                return model;
            }
            if (model.OrderPrice > model.ListPrice)
            {
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.SparePartSaleOrderDetail_Warning_OrderPriceListPriceControl;
                return model;
            }

            int totalCount = 0;
            SparePartSaleOrderDetailListModel lModel = new SparePartSaleOrderDetailListModel();
            lModel.SoNumber = model.SoNumber;
            SparePartSaleOrderDetailBL spdBo = new SparePartSaleOrderDetailBL();
            List<SparePartSaleOrderDetailListModel> detailList = spdBo.ListSparePartSaleOrderDetails(UserManager.UserInfo,lModel, out totalCount).Data;
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
            if (ModelState.IsValid)
            {
                model.CommandType = CommonValues.DMLType.Insert;
                bo.DMLSparePartSaleOrderDetail(UserManager.UserInfo, model);
                if (model.ErrorNo != 0)
                {
                    SetMessage(model.ErrorMessage, CommonValues.MessageSeverity.Fail);
                    return model;
                }
                ModelState.Clear();

                model = new SparePartSaleOrderDetailDetailModel
                {
                    SoNumber = model.SoNumber,
                    StatusId = (int)CommonValues.SparePartSaleOrderDetailStatus.OpenOrder
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
        private SparePartSaleOrderDetailDetailModel InsertMultiple(SparePartSaleOrderDetailDetailModel model, HttpPostedFileBase excelFile)
        {
            ModelState.Clear();
            CommonBL cbo = new CommonBL();
            DealerBL dealerBo = new DealerBL();
            StockCardBL scBo = new StockCardBL();
            var bo = new SparePartSaleOrderDetailBL();
            Stream s = excelFile.InputStream;
            List<SparePartSaleOrderDetailDetailModel> listModels = bo.ParseExcel(UserManager.UserInfo,model, s).Data;
            
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
                    foreach (SparePartSaleOrderDetailDetailModel SparePartSaleOrderDetailDetailModel in listModels)
                    {
                        SparePartSaleOrderDetailDetailModel.ListPrice =cbo.GetPriceByDealerPartVehicleAndType(SparePartSaleOrderDetailDetailModel.SparePartId.GetValue<int>(), 0, UserManager.UserInfo.GetUserDealerId(), CommonValues.ListPriceLabel).Model;

                        SparePartSaleOrderDetailDetailModel.SoNumber = model.SoNumber;
                        SparePartSaleOrderDetailDetailModel.StatusId = (int)CommonValues.SparePartSaleOrderDetailStatus.OpenOrder;
                        SparePartSaleOrderDetailDetailModel.CommandType = CommonValues.DMLType.Insert;
                        bo.DMLSparePartSaleOrderDetail(UserManager.UserInfo, SparePartSaleOrderDetailDetailModel);
                        if (SparePartSaleOrderDetailDetailModel.ErrorNo != 0)
                        {
                            SetMessage(SparePartSaleOrderDetailDetailModel.ErrorMessage, CommonValues.MessageSeverity.Fail);

                            return model;
                        }
                    }
                }
                SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);
            }
            return model;
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSaleOrderDetail.SparePartSaleOrderDetailIndex, CommonValues.PermissionCodes.SparePartSaleOrderDetail.SparePartSaleOrderDetailCreate)]
        public ActionResult SparePartSaleOrderDetailCreate(SparePartSaleOrderDetailDetailModel model, HttpPostedFileBase excelFile)
        {
            ViewBag.IsSuccess = false;
            var spmbo = new SparePartSaleOrderBL();
            var parentModel = spmbo.GetSparePartSaleOrder(UserManager.UserInfo,model.SoNumber);

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
            }
            parentModel = spmbo.GetSparePartSaleOrder(UserManager.UserInfo,model.SoNumber);
            model.MasterStatusId = parentModel.StatusId;
            model.MasterIsPriceFixed = parentModel.IsFixedPrice;

            if (parentModel.ErrorNo != 0)
            {
                SetMessage(parentModel.ErrorMessage, CommonValues.MessageSeverity.Fail);
            }

            return View(model);
        }

        #endregion

        #region Spare Part Sale Order Detail Update
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSaleOrderDetail.SparePartSaleOrderDetailIndex, CommonValues.PermissionCodes.SparePartSaleOrderDetail.SparePartSaleOrderDetailUpdate)]
        public ActionResult SparePartSaleOrderDetailUpdate(int SparePartSaleOrderDetailId, string soNumber)
        {
            var bo = new SparePartSaleOrderDetailBL();
            SetDefaults();

            var referenceModel = new SparePartSaleOrderDetailDetailModel
            {
                SparePartSaleOrderDetailId = SparePartSaleOrderDetailId,
                SoNumber = soNumber
            };

            if (SparePartSaleOrderDetailId > 0 && SparePartSaleOrderDetailId > 0)
            {
                referenceModel = bo.GetSparePartSaleOrderDetail(UserManager.UserInfo, referenceModel).Model;
                if (referenceModel.SparePartId != null)
                {
                    DealerBL dealerBo = new DealerBL();
                    CommonBL cbo = new CommonBL();
                    SparePartBL spBo = new SparePartBL();
                    SparePartIndexViewModel spModel = new SparePartIndexViewModel();
                    spModel.PartId = referenceModel.SparePartId.GetValue<int>();
                    spBo.GetSparePart(UserManager.UserInfo, spModel);
                    referenceModel.SparePartName = spModel.PartCode + CommonValues.Slash + spModel.AdminDesc;

                    referenceModel.ListPrice = cbo.GetPriceByDealerPartVehicleAndType(spModel.PartId, 0,UserManager.UserInfo.GetUserDealerId(),CommonValues.ListPriceLabel).Model;

                    SparePartSaleOrderBL masterBo = new SparePartSaleOrderBL();
                    SparePartSaleOrderViewModel masterModel = masterBo.GetSparePartSaleOrder(UserManager.UserInfo,soNumber);
                    referenceModel.MasterStatusId = masterModel.StatusId;
                    referenceModel.MasterIsPriceFixed = masterModel.IsFixedPrice;
                }
            }
            return View(referenceModel);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSaleOrderDetail.SparePartSaleOrderDetailIndex,
            CommonValues.PermissionCodes.SparePartSaleOrderDetail.SparePartSaleOrderDetailUpdate)]
        public ActionResult SparePartSaleOrderDetailUpdate(SparePartSaleOrderDetailDetailModel viewModel)
        {
            DealerBL dealerBo = new DealerBL();
            var bo = new SparePartSaleOrderDetailBL();
            SetDefaults();

            if (viewModel.PlannedQuantity > viewModel.OrderQuantity)
            {
                viewModel.ErrorNo = 1;
                viewModel.ErrorMessage = MessageResource.SparePartSaleOrderDetail_Warning_PlannedQtyOrderQtyControl;
                return View(viewModel);
            }
            if (viewModel.OrderPrice > viewModel.ListPrice)
            {
                SetMessage(MessageResource.SparePartSaleDetail_Warning_DiscountPriceListPriceControl, CommonValues.MessageSeverity.Fail);
                return View(viewModel);
            }
            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Update;
                bo.DMLSparePartSaleOrderDetail(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }

            SparePartSaleOrderBL masterBo = new SparePartSaleOrderBL();
            SparePartSaleOrderViewModel masterModel = masterBo.GetSparePartSaleOrder(UserManager.UserInfo,viewModel.SoNumber);
            viewModel.MasterStatusId = masterModel.StatusId;
            viewModel.MasterIsPriceFixed = masterModel.IsFixedPrice;
            return View(viewModel);
        }

        #endregion

        #region Spare Part Sale Order Detail Cancel
        /*
         İptal işleminde, SALE_ORDER_MST.ID_CUSTOMER'a karşılık gelen customer bir bayi ise, bayiye mail gönderilecek. 
         * Burada kontrol yapmak lazım, Eğer SALE_ORDER_DET.PO_DET_SEQ_NO alanı doluysa farklı mail, değilse farklı mail gitmeli. Doluysa:
            "Sayın İlgili,
            Şu şu şu satın alma sipariş no'lu siparişinizin xx nolu parçayı xx adet olarak içeren kalemin bakiyesi iptal edilmiştir. 
            Detaylar için lütfen tıklayınız. (tıklayınız PurchaseOrderIndex ekranı o kayıt için açmalı)
            Bilginize,"
            Eğer SALE_ORDER_DET.PO_DET_SEQ_NO alanı boşsa:
            "Sayın İlgili,
            Adınıza açılmış xx nolu satış siparişinin xx nolu parçayı xx adet olarak içeren kalemin bakiyesi iptal edilmiştir. 
            Bilginize,"
         * İptal statüsündeki kayıtlar için SALE_ORDER_DET.PO_DET_SEQ_NO üzerinden PURCHASE_ORDER_DET.STATUS_LOOKVAL != 9 ise -> Tik gözükmeli,
         * iptal statüsündeki kayıdı SALE_ORDER_DET.STATUS_LOOKVAL = 0 olarak güncellemeli. 
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSaleOrderDetail.SparePartSaleOrderDetailIndex, CommonValues.PermissionCodes.SparePartSaleOrderDetail.SparePartSaleOrderDetailDelete)]
        public ActionResult SparePartSaleOrderDetailCancel(int SparePartSaleOrderDetailId, string soNumber)
        {
            ViewBag.HideElements = false;

            var bo = new SparePartSaleOrderDetailBL();
            var model = new SparePartSaleOrderDetailDetailModel
            {
                SparePartSaleOrderDetailId = SparePartSaleOrderDetailId,
                SoNumber = soNumber
            };
            model = bo.GetSparePartSaleOrderDetail(UserManager.UserInfo, model).Model;

            //if (model.StatusId == (int) CommonValues.SparePartSaleOrderDetailStatus.CancelledOrder)
            //{
            //    model.StatusId = (int)CommonValues.SparePartSaleOrderDetailStatus.OpenOrder;
            //}
            //else
            //{
            //    model.StatusId = (int)CommonValues.SparePartSaleOrderDetailStatus.CancelledOrder;
            //}

            model.StatusId = (int)CommonValues.SparePartSaleOrderDetailStatus.CancelledOrder;


            model.CommandType = CommonValues.DMLType.Update;
            bo.DMLSparePartSaleOrderDetail(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);
            ModelState.Clear();

            if (model.ErrorNo == 0)
            {
                SparePartSaleOrderBL masterBo = new SparePartSaleOrderBL();
                SparePartSaleOrderViewModel masterModel = masterBo.GetSparePartSaleOrder(UserManager.UserInfo,soNumber);

                CustomerIndexViewModel customerModel = new CustomerIndexViewModel();
                customerModel.CustomerId = masterModel.CustomerId.GetValue<int>();
                CustomerBL customerBo = new CustomerBL();
                customerBo.GetCustomer(UserManager.UserInfo, customerModel);
                if (customerModel.IsDealerCustomer)
                {
                    DealerBL dealerBo = new DealerBL();
                    DealerViewModel dealerModel = dealerBo.GetDealer(UserManager.UserInfo, customerModel.DealerId.GetValue<int>()).Model;

                    if (model.PoDetailSequenceNo.HasValue())
                    {
                        PurchaseOrderDetailViewModel podModel = new PurchaseOrderDetailViewModel();
                        podModel.PurchaseOrderDetailSeqNo = model.PoDetailSequenceNo.GetValue<long>();
                        PurchaseOrderDetailBL podBo = new PurchaseOrderDetailBL();
                        podBo.GetPurchaseOrderDetail(UserManager.UserInfo, podModel);

                        string link = CommonBL.GetGeneralParameterValue("DMS_ROOT_URL").Model + "PurchaseOrder/PurchaseOrderIndex/" + podModel.PurchaseOrderNumber;
                        CommonBL.SendDbMail(dealerModel.ContactEmail, MessageResource.SparePartSaleOrderDetail_MailSubject_POCancel,
                            string.Format(MessageResource.SparePartSaleOrderDetail_MailBody_POCancel, customerModel.CustomerName
                                , model.SoNumber, model.SparePartCode, model.OrderQuantity));
                    }
                    else
                    {
                        CommonBL.SendDbMail(dealerModel.ContactEmail, MessageResource.SparePartSaleOrderDetail_MailSubject_Cancel,
                            string.Format(MessageResource.SparePartSaleOrderDetail_MailBody_Cancel, customerModel.CustomerName
                                , model.SoNumber, model.SparePartCode, model.OrderQuantity));
                    }
                }

                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            }
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }
        #endregion

        #region Spare Part Sale Order Detail Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSaleOrderDetail.SparePartSaleOrderDetailIndex, CommonValues.PermissionCodes.SparePartSaleOrderDetail.SparePartSaleOrderDetailDelete)]
        public ActionResult SparePartSaleOrderDetailDelete(int SparePartSaleOrderDetailId, string soNumber)
        {
            ViewBag.HideElements = false;

            var bo = new SparePartSaleOrderDetailBL();
            var model = new SparePartSaleOrderDetailDetailModel
            {
                SparePartSaleOrderDetailId = SparePartSaleOrderDetailId,
                SoNumber = soNumber
            };
            model = bo.GetSparePartSaleOrderDetail(UserManager.UserInfo, model).Model;

            model.CommandType = CommonValues.DMLType.Delete;
            bo.DMLSparePartSaleOrderDetail(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);
            ModelState.Clear();

            if (model.ErrorNo == 0)
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            }

            SparePartSaleOrderBL masterBo = new SparePartSaleOrderBL();
            SparePartSaleOrderViewModel masterModel = masterBo.GetSparePartSaleOrder(UserManager.UserInfo,soNumber);

            CustomerIndexViewModel customerModel = new CustomerIndexViewModel();
            customerModel.CustomerId = masterModel.CustomerId.GetValue<int>();
            CustomerBL customerBo = new CustomerBL();
            customerBo.GetCustomer(UserManager.UserInfo, customerModel);
            if (customerModel.IsDealerCustomer)
            {
                /*
             Sil işleminde, SALE_ORDER_MST.ID_CUSTOMER'a karşılık gelen customer bir bayi ise, bayiye mail gönderilecek.
             * "Sayın İlgili,
                 * Şu şu şu firma tarafından, şu şu şu tarihte şu şu şu sipariş numarasıyla oluşturulan siparişiniz şu parçasını içeren sipariş kalemi silinmiştir.
                Bilginize,"
             */
                DealerBL dealerBo = new DealerBL();
                DealerViewModel dealerModel = dealerBo.GetDealer(UserManager.UserInfo, customerModel.DealerId.GetValue<int>()).Model;
                CommonBL.SendDbMail(dealerModel.ContactEmail, MessageResource.SparePartSaleOrderDetail_MailSubject_Delete,
                    string.Format(MessageResource.SparePartSaleOrderDetail_MailBody_Delete, customerModel.CustomerName
                    , masterModel.OrderDate, model.SoNumber, model.SparePartCode));

            }

            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        #endregion

        #region Spare Part Sale Order Detail Details
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSaleOrderDetail.SparePartSaleOrderDetailIndex, CommonValues.PermissionCodes.SparePartSaleOrderDetail.SparePartSaleOrderDetailDetails)]
        public ActionResult SparePartSaleOrderDetailDetails(int SparePartSaleOrderDetailId, string soNumber)
        {
            var referenceModel = new SparePartSaleOrderDetailDetailModel
            {
                SparePartSaleOrderDetailId = SparePartSaleOrderDetailId,
                SoNumber = soNumber
            };
            var bo = new SparePartSaleOrderDetailBL();

            var model = bo.GetSparePartSaleOrderDetail(UserManager.UserInfo, referenceModel).Model;

            return View(model);
        }
        #endregion
    }
}

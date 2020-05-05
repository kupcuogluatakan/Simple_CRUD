using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.Customer;
using ODMSModel.DownloadFileActionResult;
using ODMSModel.PurchaseOrder;
using ODMSModel.PurchaseOrderDetail;
using ODMSModel.PurchaseOrderType;
using ODMSModel.SparePart;
using ODMSModel.Vehicle;
using ODMSModel.Dealer;
using ODMSModel.SparePartSAPUnit;
using ODMSModel.StockCardPriceListModel;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class PurchaseOrderDetailController : ControllerBase
    {
        #region General Methods
        private void SetDefaults(int purchaseOrderNumber)
        {
            PurchaseOrderViewModel poModel = new PurchaseOrderViewModel()
            {
                PoNumber = purchaseOrderNumber.GetValue<int>()
            };

            PurchaseOrderBL poBo = new PurchaseOrderBL();
            poBo.GetPurchaseOrder(UserManager.UserInfo, poModel);

            bool isMstSupplierIdEmpty = (poModel.IdSupplier == null || poModel.IdSupplier == 0) ? true : false;
            ViewBag.IsMstSupplierIdEmpty = isMstSupplierIdEmpty;
            ViewBag.PoStatusId = poModel.Status;
            // StatusList
            List<SelectListItem> statusList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.PurchaseOrderDetailStatusLookup).Data;
            ViewBag.StatusList = statusList;
        }

        private bool CheckSupplier(int purchaseOrderNumber)
        {
            PurchaseOrderViewModel poModel = new PurchaseOrderViewModel()
            {
                PoNumber = purchaseOrderNumber.GetValue<int>()
            };

            PurchaseOrderBL poBo = new PurchaseOrderBL();
            poBo.GetPurchaseOrder(UserManager.UserInfo, poModel);

            return (poModel.SupplierIdDealer == null || poModel.SupplierIdDealer == 0) ? true : false;
        }

        public List<SelectListItem> ListCurrencyCodes(int? id)
        {
            List<SelectListItem> returnList = new List<SelectListItem>();
            if (id.HasValue)
            {
                PurchaseOrderTypeViewModel poTypeModel = new PurchaseOrderTypeViewModel();
                poTypeModel.PurchaseOrderTypeId = id.GetValue<int>();

                PurchaseOrderTypeBL poTypeBo = new PurchaseOrderTypeBL();
                poTypeModel = poTypeBo.Get(UserManager.UserInfo, poTypeModel).Model;

                if (poTypeModel.IsCurrencySelectAllow)
                {
                    returnList = CommonBL.ListCurrencies(UserManager.UserInfo).Data;
                }
                else
                {
                    int dealerId = UserManager.UserInfo.GetUserDealerId();

                    DealerBL dealerBo = new DealerBL();
                    DealerViewModel dealerModel = dealerBo.GetDealer(UserManager.UserInfo, dealerId).Model;
                    string currencyCode = dealerBo.GetCountryCurrencyCode(dealerModel.Country).Model;

                    returnList.Add(new SelectListItem() { Value = currencyCode, Text = currencyCode });
                }
            }

            return returnList;
        }

        [ValidateAntiForgeryToken]
        public JsonResult GetPackageQuantity(string partId)
        {
            if (!string.IsNullOrEmpty(partId))
            {
                SparePartIndexViewModel spModel = new SparePartIndexViewModel() { PartId = partId.GetValue<int>() };

                SparePartBL bo = new SparePartBL();
                bo.GetSparePart(UserManager.UserInfo, spModel);

                decimal packageQuantity = spModel.ShipQuantity.GetValue<decimal>();

                return Json(new { PackageQuantity = packageQuantity, UnitName = spModel.UnitName });
            }
            else
            {
                return null;
            }
        }

        private decimal GetOrderPrice(int partId)
        {
            decimal orderPrice = 0;
            SparePartIndexViewModel spModel = new SparePartIndexViewModel() { PartId = partId };

            SparePartBL bo = new SparePartBL();
            bo.GetSparePart(UserManager.UserInfo, spModel);

            if (spModel.IsOriginal.GetValue<bool>())
            {
                CommonBL commonBo = new CommonBL();
                orderPrice = commonBo.GetPriceByDealerPartVehicleAndType(partId, null, UserManager.UserInfo.GetUserDealerId(), CommonValues.DealerPriceLabel).Model;
            }
            else
            {
                StockCardBL stockCardBo = new StockCardBL();
                orderPrice = stockCardBo.GetDealerPriceByDealerAndPart(partId, UserManager.UserInfo.GetUserDealerId()).Model;
            }
            return orderPrice;
        }

        [ValidateAntiForgeryToken]
        public JsonResult GetPriceDetails(string dealerCustomerId, string supplierIdDealer, string partId)
        {
            if (!string.IsNullOrEmpty(partId))
            {
                decimal orderPrice = GetOrderPrice(partId.GetValue<int>());
                CommonBL commonBo = new CommonBL();
                decimal listPrice = commonBo.GetPriceByDealerPartVehicleAndType(partId.GetValue<int>(), null,
                                                                         UserManager.UserInfo.GetUserDealerId(),
                                                                         CommonValues.ListPriceLabel).Model;
                SparePartBL spBo = new SparePartBL();
                SparePartIndexViewModel spModel = new SparePartIndexViewModel();
                spModel.PartId = partId.GetValue<int>();
                spBo.GetSparePart(UserManager.UserInfo, spModel);
                decimal listDiscountRatio = spBo.GetCustomerPartDiscount(partId.GetValue<int>(),
                    supplierIdDealer.HasValue() ? supplierIdDealer.GetValue<int?>() : null,
                    dealerCustomerId.HasValue() ? dealerCustomerId.GetValue<int>() : UserManager.UserInfo.GetUserDealerId(),
                    CommonValues.ActionType.S.ToString()).Model;

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
                decimal costPrice = _stockCardPriceListService.Get(UserManager.UserInfo, _stockCardPriceListModel, CommonValues.StockCardPriceType.D).Model.CostPrice;


                return Json(new { OrderPrice = orderPrice, ListPrice = listPrice, ListDiscountRatio = listDiscountRatio, AlternatePart = spModel.AlternatePart, CostPrice = costPrice });
            }
            else
            {
                return null;
            }
        }

        [ValidateAntiForgeryToken]
        public JsonResult GetSparePartChangeInfo(string partId, int purchaseOrderNumber)
        {
            if (!string.IsNullOrEmpty(partId))
            {
                // parça değiştirilmiş mi kontrol ediliyor.
                var bo = new SparePartBL();
                bool isPartChanged = false;
                var spModel = new SparePartIndexViewModel() { PartId = partId.GetValue<int>() };
                var currentPart = new SparePartIndexViewModel() { PartId = partId.GetValue<int>() };

                bo.GetSparePart(UserManager.UserInfo, spModel);
                bo.GetSparePart(UserManager.UserInfo, currentPart);

                bool result = new PurchaseOrderDetailBL().CheckDealerAccessPermission(UserManager.UserInfo, partId).Model;
                if (!result)
                {
                    return Json(new
                    {
                        NewPartId = currentPart.PartId,
                        PartName = spModel.PartNameInLanguage,
                        MessageChange = string.Empty,
                        MessageSplit = string.Empty,
                        AccessPermission = MessageResource.Purchase_Order_Dealer_Part_Access_Permission
                    });
                }

                string oldPartCode = spModel.PartCode;
                int topPartId = bo.IsPartChanged(spModel.PartId, spModel.PartCode).Model;

                isPartChanged = partId.GetValue<int>() != topPartId;
                int newPartId = isPartChanged ? topPartId : partId.GetValue<int>();

                spModel = new SparePartIndexViewModel();
                spModel.PartId = newPartId;
                bo.GetSparePart(UserManager.UserInfo, spModel);

                string newPartCode = spModel.PartCode;
                // eşlenik parçalar için bölünen değişen parça kontrolü yapılmayacak
                if (spModel.IsOriginal.GetValue<bool>() && CheckSupplier(purchaseOrderNumber))
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
                                        : string.Empty,
                        AccessPermission = string.Empty

                    });
                }
                else
                {
                    if (spModel.IsOriginal.GetValue<bool>() && !CheckSupplier(purchaseOrderNumber))
                    {
                        return Json(new
                        {
                            NewPartId = currentPart.PartId,
                            PartName = currentPart.PartCode + CommonValues.Slash + spModel.PartNameInLanguage,
                            MessageChange = isPartChanged
                                        ? string.Format(MessageResource.PurchaseOrderDetail_Warning_ChangedPart, oldPartCode, newPartCode)
                                        : "",
                            MessageSplit = "",
                            AccessPermission = string.Empty

                        });

                    }
                    else
                    {
                        return Json(new
                        {
                            NewPartId = spModel.PartId,
                            PartName = spModel.PartNameInLanguage,
                            MessageChange = string.Empty,
                            MessageSplit = string.Empty,
                            AccessPermission = string.Empty
                        });

                    }
                }
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Purchase Order Detail Index

        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderDetail.PurchaseOrderDetailIndex)]
        [HttpGet]
        public ActionResult PurchaseOrderDetailIndex(int? purchaseOrderNumber, bool openCreatePopup)
        {
            PurchaseOrderViewModel poModel = new PurchaseOrderViewModel();
            poModel.PoNumber = purchaseOrderNumber.GetValue<int>();

            PurchaseOrderBL poBo = new PurchaseOrderBL();
            poBo.GetPurchaseOrder(UserManager.UserInfo, poModel);

            DealerBL dealerBo = new DealerBL();
            DealerViewModel dealerModel = dealerBo.GetDealer(UserManager.UserInfo, poModel.SupplierIdDealer.GetValue<int>()).Model;

            PurchaseOrderTypeBL potBo = new PurchaseOrderTypeBL();

            PurchaseOrderTypeViewModel potModel = new PurchaseOrderTypeViewModel();
            potModel.PurchaseOrderTypeId = poModel.PoType.GetValue<int>();
            potModel = potBo.Get(UserManager.UserInfo, potModel).Model;

            poModel.ManuelPriceAllow = potModel.ManuelPriceAllow;

            PurchaseOrderDetailListModel model = new PurchaseOrderDetailListModel();
            if (purchaseOrderNumber != null && purchaseOrderNumber != 0)
            {
                model.PurchaseOrderNumber = purchaseOrderNumber.GetValue<int>();
            }

            model.StatusMst = poModel.Status;
            model.SupplierId = poModel.IdSupplier;
            model.ManuelPriceAllow = poModel.ManuelPriceAllow;
            model.SupplyTypeMst = poModel.SupplyType;
            model.SupplierIdDealer = poModel.SupplierIdDealer;
            model.IsProposal = poModel.IsProposal;
            model.SupplierDealerConfirm = poModel.SupplierDealerConfirm;
            model.IsCampaignPO = poModel.PoType == CommonBL.GetGeneralParameterValue(CommonValues.GeneralParameters.CampaignPurchaseOrderType).Model.GetValue<int>();
            model.AcceptOrderProposal = dealerModel.AcceptOrderProposal;

            SetDefaults(purchaseOrderNumber.GetValue<int>());
            ViewBag.OpenCreatePopup = openCreatePopup;
            return PartialView(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderDetail.PurchaseOrderDetailIndex, CommonValues.PermissionCodes.PurchaseOrderDetail.PurchaseOrderDetailDetails)]
        public ActionResult ListPurchaseOrderDetail([DataSourceRequest] DataSourceRequest request, PurchaseOrderDetailListModel model)
        {
            var purchaseOrderDetailBo = new PurchaseOrderDetailBL();
            var v = new PurchaseOrderDetailListModel(request);
            var totalCnt = 0;
            v.PurchaseOrderNumber = model.PurchaseOrderNumber;

            var returnValue = purchaseOrderDetailBo.ListPurchaseOrderDetails(UserManager.UserInfo, v, out totalCnt).Data;
            return Json(new
            {
                Data = returnValue,
                Total = totalCnt,
                PoNumber = model.PurchaseOrderNumber
            });
        }

        #endregion

        #region Purchase Order Detail Create
        public ActionResult ExcelSample()
        {
            var bo = new PurchaseOrderDetailBL();
            var ms = bo.SampleExcelFormat();
            return File(ms, CommonValues.ExcelContentType, MessageResource.PurchaseOrderDetail_PageTitle_Index + CommonValues.ExcelExtOld);
        }
        private PurchaseOrderDetailViewModel InsertSingle(PurchaseOrderDetailViewModel viewModel)
        {
            SparePartBL spBo = new SparePartBL();
            var purchaseOrderDetailBo = new PurchaseOrderDetailBL();

            if (viewModel.PartId != 0 && viewModel.PartId != null)
            {
                SparePartIndexViewModel spModel = new SparePartIndexViewModel
                {
                    PartId = viewModel.PartId.GetValue<int>()
                };

                spBo.GetSparePart(UserManager.UserInfo, spModel);

                viewModel.PartName = spModel.PartNameInLanguage;

                if (!spModel.IsOrderAllowed && viewModel.SupplyTypeMst != 2)//Not Supplier
                {
                    viewModel.ErrorNo = 1;
                    SetMessage(MessageResource.PuchaseOrderDetail_Warning_PartIsNotAllowedOrder, CommonValues.MessageSeverity.Fail);
                    return viewModel;
                }
                viewModel.PartCode = spModel.PartCode;
                viewModel.PackageQuantity = spModel.ShipQuantity;
                viewModel.OrderQuantity = viewModel.PackageQuantity == 0 ? 0 : viewModel.OrderQuantity;

                SparePartSAPUnitBL spsuBl = new SparePartSAPUnitBL();
                SparePartSAPUnitViewModel spsuModel = new SparePartSAPUnitViewModel();
                spsuModel.PartId = spModel.PartId;
                spsuModel = spsuBl.Get(UserManager.UserInfo, spsuModel).Model;
                if(spsuModel.ShipQuantity != 0 && viewModel.OrderQuantity % spsuModel.ShipQuantity != 0)
                {
                    viewModel.ErrorNo = 1;
                    SetMessage(
                        string.Format(MessageResource.PuchaseOrderDetail_Warning_OrderQuantitySAPShipQuantity, spsuModel.ShipQuantity),
                        CommonValues.MessageSeverity.Fail);
                    return viewModel;
                }

                if (viewModel.OrderQuantity > Decimal.Parse("99999,99"))
                {
                    viewModel.ErrorNo = 1;
                    SetMessage(
                        string.Format(MessageResource.PuchaseOrderDetail_Warning_OrderQuantity, viewModel.OrderQuantity),
                        CommonValues.MessageSeverity.Fail);
                    return viewModel;
                }
                viewModel.OrderPrice = viewModel.OrderPrice ?? GetOrderPrice(viewModel.PartId.GetValue<int>());
            }

            ModelState.Remove("CostPrice");

            if (ModelState.IsValid)
            {
                List<SparePartSplittingViewModel> splitList = spBo.ListSparePartsSplitting(viewModel.PartId.GetValue<long>()).Data;
                // parça bölünmüş ise bölünenler insert edilecek
                if (splitList.Count > 0)
                {
                    foreach (SparePartSplittingViewModel sparePartSplittingViewModel in splitList)
                    {
                        PurchaseOrderDetailViewModel inserted = new PurchaseOrderDetailViewModel();
                        inserted.PurchaseOrderNumber = viewModel.PurchaseOrderNumber;
                        inserted.DesireDeliveryDate = viewModel.DesireDeliveryDate;
                        inserted.OrderQuantity = viewModel.OrderQuantity;
                        inserted.DesireQuantity = sparePartSplittingViewModel.Quantity;
                        inserted.PackageQuantity = viewModel.PackageQuantity;
                        inserted.OrderPrice = viewModel.OrderPrice;
                        inserted.ConfirmPrice = inserted.OrderPrice;
                        inserted.PartId = sparePartSplittingViewModel.PartId;
                        inserted.StatusId = (int)CommonValues.PurchaseOrderDetailStatus.NewRecord;
                        inserted.CurrencyCode = viewModel.CurrencyCode;
                        inserted.CommandType = CommonValues.DMLType.Insert;
                        purchaseOrderDetailBo.DMLPurchaseOrderDetail(UserManager.UserInfo, inserted);
                        if (inserted.ErrorNo > 0)
                        {
                            viewModel.ErrorNo = 1;
                            SetMessage(inserted.ErrorMessage, CommonValues.MessageSeverity.Fail);
                            ViewBag.IsSuccessfullyInserted = false;
                            return viewModel;
                        }
                    }
                }
                else
                {
                    viewModel.ConfirmPrice = viewModel.OrderPrice;
                    viewModel.StatusId = (int)CommonValues.PurchaseOrderDetailStatus.NewRecord;
                    viewModel.CommandType = CommonValues.DMLType.Insert;
                    purchaseOrderDetailBo.DMLPurchaseOrderDetail(UserManager.UserInfo, viewModel);
                    if (viewModel.ErrorNo > 0)
                    {
                        SetMessage(viewModel.ErrorMessage, CommonValues.MessageSeverity.Fail);
                        ViewBag.IsSuccessfullyInserted = false;
                        return viewModel;
                    }
                }

                ModelState.Clear();
                PurchaseOrderViewModel masterModel = new PurchaseOrderViewModel();
                masterModel.PoNumber = viewModel.PurchaseOrderNumber;

                PurchaseOrderBL masterBo = new PurchaseOrderBL();
                masterBo.GetPurchaseOrder(UserManager.UserInfo, masterModel);
                ViewBag.PoStatusId = masterModel.Status;

                VehicleIndexViewModel vehicleModel = new VehicleIndexViewModel();
                vehicleModel.VehicleId = masterModel.VehicleId.GetValue<int>();

                VehicleBL vehicleBo = new VehicleBL();
                vehicleBo.GetVehicle(UserManager.UserInfo, vehicleModel);

                PurchaseOrderDetailViewModel model = new PurchaseOrderDetailViewModel
                {
                    PurchaseOrderNumber = viewModel.PurchaseOrderNumber,
                    SupplyTypeMst = viewModel.SupplyTypeMst,
                    SupplierName = masterModel.SupplyType == (int)CommonValues.SupplyPort.DealerService ? masterModel.Location : masterModel.SupplierName,
                    PoTypeName = masterModel.PoTypeName,
                    StockTypeName = masterModel.StockTypeName,
                    VehicleName = vehicleModel.VinNo + " " + vehicleModel.VehicleCode,
                    StatusMst = masterModel.Status
                };

                SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);
                return model;
            }
            else
            {
                //if (!isUnsuitAbleForDefect)
                //{
                //    SetMessage("Parça ilk önce sözleşmeye eklenmelidir. Part Code: "+ viewModel.PartCode, CommonValues.MessageSeverity.Fail);
                //    viewModel.ErrorNo = 1;
                //    viewModel.ErrorMessage = "Parça ilk önce sözleşmeye eklenmelidir. Part Code: " + viewModel.PartCode;
                //}
                ViewBag.IsSuccessfullyInserted = false;               
            }
            return viewModel;
        }

        private PurchaseOrderDetailViewModel InsertMultiple(PurchaseOrderDetailViewModel model, HttpPostedFileBase excelFile)
        {
            ModelState.Clear();
            CommonBL cbo = new CommonBL();
            DealerBL dealerBo = new DealerBL();
            StockCardBL scBo = new StockCardBL();
            var bo = new PurchaseOrderDetailBL();

            List<PurchaseOrderDetailViewModel> listModels = new List<PurchaseOrderDetailViewModel>();
            using (Stream s = excelFile.InputStream)
            {
                listModels = bo.ParseExcel(UserManager.UserInfo, model, s, Path.GetExtension(excelFile.FileName).ToLower());
            }

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
                    foreach (PurchaseOrderDetailViewModel sparePartSaleDetailDetailModel in listModels)
                    {
                        sparePartSaleDetailDetailModel.PurchaseOrderNumber = model.PurchaseOrderNumber;
                        InsertSingle(sparePartSaleDetailDetailModel);
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

        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderDetail.PurchaseOrderDetailIndex, CommonValues.PermissionCodes.PurchaseOrderDetail.PurchaseOrderDetailCreate)]
        [HttpGet]
        public ActionResult PurchaseOrderDetailCreate(int? purchaseOrderNumber)
        {
            ViewBag.IsSuccessfullyInserted = true;
            PurchaseOrderViewModel masterModel = new PurchaseOrderViewModel();
            masterModel.PoNumber = purchaseOrderNumber;
            PurchaseOrderBL masterBo = new PurchaseOrderBL();
            masterBo.GetPurchaseOrder(UserManager.UserInfo, masterModel);

            PurchaseOrderTypeBL potBo = new PurchaseOrderTypeBL();
            PurchaseOrderTypeViewModel potModel = new PurchaseOrderTypeViewModel();
            potModel.PurchaseOrderTypeId = masterModel.PoType.GetValue<int>();

            potModel = potBo.Get(UserManager.UserInfo, potModel).Model;
            bool? manuelPriceAllow = potModel.ManuelPriceAllow;

            VehicleIndexViewModel vehicleModel = new VehicleIndexViewModel();
            vehicleModel.VehicleId = masterModel.VehicleId.GetValue<int>();
            VehicleBL vehicleBo = new VehicleBL();
            vehicleBo.GetVehicle(UserManager.UserInfo, vehicleModel);


            DealerBL dBo = new DealerBL();
            DealerCustomerInfoModel dciModel = dBo.GetDealerCustomerInfo(masterModel.IdDealer.GetValue<int>()).Model;
            int? customerIdDealer = dciModel.CustomerId;


            PurchaseOrderDetailViewModel model = new PurchaseOrderDetailViewModel
            {
                PurchaseOrderNumber = purchaseOrderNumber.GetValue<int>(),
                StatusId = (int)CommonValues.PurchaseOrderDetailStatus.NewRecord,
                ManuelPriceAllow = manuelPriceAllow,
                SupplyTypeMst = masterModel.SupplyType,
                SupplierName = masterModel.SupplyType == (int)CommonValues.SupplyPort.DealerService ? masterModel.Location : masterModel.SupplierName,
                PoTypeId = masterModel.PoType,
                PoTypeName = masterModel.PoTypeName,
                StockTypeName = masterModel.StockTypeName,
                VehicleName = vehicleModel.VinNo + " " + vehicleModel.VehicleCode,
                StatusMst = masterModel.Status,
                IsCampaignPO = masterModel.PoType == CommonBL.GetGeneralParameterValue(CommonValues.GeneralParameters.CampaignPurchaseOrderType).Model.GetValue<int>(),
                MstDealerCustomerId = customerIdDealer
            };

            if (masterModel.SupplierIdDealer.HasValue)
                model.MstSupplierIdDealer = masterModel.SupplierIdDealer.GetValue<int>();

            SetDefaults(purchaseOrderNumber.GetValue<int>());
            ViewBag.OpenCreatePopup = false;
            ViewBag.CurrencyCodeList = ListCurrencyCodes(masterModel.PoType);
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderDetail.PurchaseOrderDetailIndex, CommonValues.PermissionCodes.PurchaseOrderDetail.PurchaseOrderDetailCreate)]
        [HttpPost]
        public ActionResult PurchaseOrderDetailCreate(PurchaseOrderDetailViewModel viewModel, HttpPostedFileBase excelFile)
        {
            ViewBag.IsSuccessfullyInserted = true;
            SetDefaults(viewModel.PurchaseOrderNumber);
            viewModel.ListPrice = (!viewModel.ListPrice.HasValue || viewModel.ListPrice == 0) ? viewModel.OrderPrice : viewModel.ListPrice;
            viewModel.ConfirmPrice = (!viewModel.ConfirmPrice.HasValue || viewModel.ConfirmPrice == 0) ? viewModel.OrderPrice : viewModel.ConfirmPrice;

            // açık siparişe detay kaydı eklenemez
            PurchaseOrderViewModel masterModel = new PurchaseOrderViewModel();
            masterModel.PoNumber = viewModel.PurchaseOrderNumber;

            PurchaseOrderBL masterBo = new PurchaseOrderBL();
            masterBo.GetPurchaseOrder(UserManager.UserInfo, masterModel);

            ViewBag.PoStatusId = masterModel.Status;
            viewModel.StatusMst = masterModel.Status;
            ViewBag.CurrencyCodeList = ListCurrencyCodes(masterModel.PoType);

            if (masterModel.Status != (int)CommonValues.PurchaseOrderStatus.OpenPurchaseOrder || !masterModel.SupplierDealerConfirm.In(0, 3)) //--yeni teklif talebi||onaysız sipariş
            {
                if (excelFile != null)
                {
                    ViewBag.IsExcelUpload = true;
                    viewModel = InsertMultiple(viewModel, excelFile);
                }
                else
                {
                    ViewBag.IsExcelUpload = false;
                    viewModel = InsertSingle(viewModel);
                    if (viewModel.ErrorNo == 0)
                    {
                        if (Request.Params["action:PurchaseOrderComplete"] != null)
                        {
                            PurchaseOrderController poCont = new PurchaseOrderController();
                            string successMessage = poCont.PurchaseOrderCompleteSub(masterModel);
                            ViewBag.PoStatusId = masterModel.Status;
                            viewModel.StatusMst = masterModel.Status;
                            ModelState.Clear();
                            if (string.IsNullOrEmpty(successMessage))
                            {
                                SetMessage(masterModel.ErrorMessage, CommonValues.MessageSeverity.Fail);
                            }
                            else
                            {
                                if (masterModel.ErrorNo == 0)
                                {
                                    SetMessage(successMessage, CommonValues.MessageSeverity.Success);
                                }
                                else
                                {
                                    SetMessage(masterModel.ErrorMessage, CommonValues.MessageSeverity.Fail);
                                }
                            }
                        }
                    }
                    else
                    {
                        ViewBag.IsSuccessfullyInserted = false;
                    }
                }
            }
            else
            {
                SetMessage(MessageResource.PurchaseOrderDetail_Warning_OpenPurchaseOrder, CommonValues.MessageSeverity.Fail);
            }

            PurchaseOrderTypeBL potBo = new PurchaseOrderTypeBL();

            PurchaseOrderTypeViewModel potModel = new PurchaseOrderTypeViewModel();
            potModel.PurchaseOrderTypeId = masterModel.PoType.GetValue<int>();
            potModel = potBo.Get(UserManager.UserInfo, potModel).Model;

            bool? manuelPriceAllow = potModel.ManuelPriceAllow;
            viewModel.ManuelPriceAllow = manuelPriceAllow;


            DealerBL dBo = new DealerBL();
            DealerCustomerInfoModel dciModel = dBo.GetDealerCustomerInfo(masterModel.IdDealer.GetValue<int>()).Model;

            int? customerIdDealer = dciModel.CustomerId;
            viewModel.MstDealerCustomerId = customerIdDealer;

            if (masterModel.SupplierIdDealer.HasValue)
                viewModel.MstSupplierIdDealer = masterModel.SupplierIdDealer.GetValue<int>();

            return View(viewModel);
        }

        #endregion

        #region Purchase Order Detail Update
        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderDetail.PurchaseOrderDetailIndex, CommonValues.PermissionCodes.PurchaseOrderDetail.PurchaseOrderDetailUpdate)]
        [HttpGet]
        public ActionResult PurchaseOrderDetailUpdate(int purchaseOrderDetailSeqNo, int supplyTypeMst)
        {
            var v = new PurchaseOrderDetailViewModel();
            if (purchaseOrderDetailSeqNo > 0)
            {
                var purchaseOrderDetailBo = new PurchaseOrderDetailBL();
                v.PurchaseOrderDetailSeqNo = purchaseOrderDetailSeqNo;
                v.SupplyTypeMst = supplyTypeMst;
                purchaseOrderDetailBo.GetPurchaseOrderDetail(UserManager.UserInfo, v);

                PurchaseOrderViewModel masterModel = new PurchaseOrderViewModel();
                masterModel.PoNumber = v.PurchaseOrderNumber;
                PurchaseOrderBL poBo = new PurchaseOrderBL();
                poBo.GetPurchaseOrder(UserManager.UserInfo, masterModel);

                PurchaseOrderTypeBL potBo = new PurchaseOrderTypeBL();

                PurchaseOrderTypeViewModel potModel = new PurchaseOrderTypeViewModel();
                potModel.PurchaseOrderTypeId = masterModel.PoType.GetValue<int>();
                potModel = potBo.Get(UserManager.UserInfo, potModel).Model;

                v.ManuelPriceAllow = potModel.ManuelPriceAllow;
                v.PoTypeId = masterModel.PoType;

                int count = 0;
                CustomerBL cBo = new CustomerBL();
                CustomerListModel cListModel = new CustomerListModel();
                cListModel.DealerId = masterModel.IdDealer;

                List<CustomerListModel> customerList = cBo.GetCustomersByDealer(UserManager.UserInfo, cListModel).Data;
                int? customerIdDealer = null;
                if (customerList.Any())
                {
                    customerIdDealer = customerList.Select(f => f.CustomerId).First();
                }
                v.MstDealerCustomerId = customerIdDealer;
                v.MstSupplierIdDealer = masterModel.SupplierIdDealer.GetValue<int>();

                ViewBag.CurrencyCodeList = ListCurrencyCodes(masterModel.PoType);
            }
            SetDefaults(v.PurchaseOrderNumber);
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderDetail.PurchaseOrderDetailIndex,
            CommonValues.PermissionCodes.PurchaseOrderDetail.PurchaseOrderDetailUpdate)]
        [HttpPost]
        public ActionResult PurchaseOrderDetailUpdate(PurchaseOrderDetailViewModel viewModel,
                                                      IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults(viewModel.PurchaseOrderNumber);

            PurchaseOrderViewModel masterModel = new PurchaseOrderViewModel();
            masterModel.PoNumber = viewModel.PurchaseOrderNumber;

            PurchaseOrderBL poBo = new PurchaseOrderBL();
            poBo.GetPurchaseOrder(UserManager.UserInfo, masterModel);

            ViewBag.CurrencyCodeList = ListCurrencyCodes(masterModel.PoType);

            var purchaseOrderDetailBo = new PurchaseOrderDetailBL();

            viewModel.ListPrice = (!viewModel.ListPrice.HasValue || viewModel.ListPrice == 0) ? viewModel.OrderPrice : viewModel.ListPrice;
            viewModel.ConfirmPrice = (!viewModel.ConfirmPrice.HasValue || viewModel.ConfirmPrice == 0) ? viewModel.OrderPrice : viewModel.ConfirmPrice;

            if (viewModel.PartId != 0 && viewModel.PartId != null)
            {
                SparePartIndexViewModel spModel = new SparePartIndexViewModel
                {
                    PartId = viewModel.PartId.GetValue<int>()
                };
                SparePartBL spBo = new SparePartBL();
                spBo.GetSparePart(UserManager.UserInfo, spModel);

                viewModel.PartName = spModel.OriginalPartName.Length == 0
                                         ? spModel.AdminDesc
                                         : spModel.PartCode + " " + spModel.OriginalPartName;

                if (!spModel.IsOrderAllowed && viewModel.SupplyTypeMst != 2)//Not Supplier
                {
                    SetMessage(MessageResource.PuchaseOrderDetail_Warning_PartIsNotAllowedOrder,
                               CommonValues.MessageSeverity.Fail);
                    return View(viewModel);
                }
                viewModel.PackageQuantity = spModel.ShipQuantity;
                viewModel.OrderQuantity = viewModel.PackageQuantity == 0 ? 0 : viewModel.OrderQuantity;
                
                SparePartSAPUnitBL spsuBl = new SparePartSAPUnitBL();
                SparePartSAPUnitViewModel spsuModel = new SparePartSAPUnitViewModel();
                spsuModel.PartId = spModel.PartId;
                spsuModel = spsuBl.Get(UserManager.UserInfo, spsuModel).Model;
                if (spsuModel.ShipQuantity != 0 && viewModel.OrderQuantity % spsuModel.ShipQuantity != 0)
                {
                    viewModel.ErrorNo = 1;
                    SetMessage(
                        string.Format(MessageResource.PuchaseOrderDetail_Warning_OrderQuantitySAPShipQuantity, spsuModel.ShipQuantity),
                        CommonValues.MessageSeverity.Fail);
                    return View(viewModel);
                }

                if (viewModel.OrderQuantity > Decimal.Parse("99999,99"))
                {
                    viewModel.ErrorNo = 1;
                    SetMessage(string.Format(MessageResource.PuchaseOrderDetail_Warning_OrderQuantity, viewModel.OrderQuantity), CommonValues.MessageSeverity.Fail);
                    return View(viewModel);
                }
                if (ModelState.IsValid)
                {
                    viewModel.CommandType = CommonValues.DMLType.Update;
                    purchaseOrderDetailBo.DMLPurchaseOrderDetail(UserManager.UserInfo, viewModel);
                    CheckErrorForMessage(viewModel, true);

                    purchaseOrderDetailBo.GetPurchaseOrderDetail(UserManager.UserInfo, viewModel);
                }
            }

            return View(viewModel);
        }

        #endregion

        #region Purchase Order Detail Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderDetail.PurchaseOrderDetailIndex, CommonValues.PermissionCodes.PurchaseOrderDetail.PurchaseOrderDetailDelete)]
        public ActionResult DeletePurchaseOrderDetail(int purchaseOrderDetailSeqNo)
        {
            PurchaseOrderDetailViewModel viewModel = new PurchaseOrderDetailViewModel() { PurchaseOrderDetailSeqNo = purchaseOrderDetailSeqNo };
            var purchaseOrderDetailBo = new PurchaseOrderDetailBL();
            viewModel.CommandType = purchaseOrderDetailSeqNo > 0 ? CommonValues.DMLType.Delete : string.Empty;
            purchaseOrderDetailBo.DMLPurchaseOrderDetail(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
        }

        #endregion

        #region Purchase Order Detail Cancel

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderDetail.PurchaseOrderDetailIndex, CommonValues.PermissionCodes.PurchaseOrderDetail.PurchaseOrderDetailCancel)]
        public ActionResult CancelPurchaseOrderDetail(int purchaseOrderDetailSeqNo)
        {
            PurchaseOrderDetailViewModel viewModel = new PurchaseOrderDetailViewModel() { PurchaseOrderDetailSeqNo = purchaseOrderDetailSeqNo };

            var purchaseOrderDetailBo = new PurchaseOrderDetailBL();
            purchaseOrderDetailBo.GetPurchaseOrderDetail(UserManager.UserInfo, viewModel);

            viewModel.StatusId = (int)CommonValues.PurchaseOrderDetailStatus.Cancelled;
            viewModel.CommandType = CommonValues.DMLType.Update;

            purchaseOrderDetailBo.DMLPurchaseOrderDetail(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
        }

        #endregion

        #region Purchase Order Detail Details

        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderDetail.PurchaseOrderDetailIndex, CommonValues.PermissionCodes.PurchaseOrderDetail.PurchaseOrderDetailDetails)]
        [HttpGet]
        public ActionResult PurchaseOrderDetailDetails(int purchaseOrderDetailSeqNo)
        {
            var v = new PurchaseOrderDetailViewModel();
            var purchaseOrderDetailBo = new PurchaseOrderDetailBL();

            v.PurchaseOrderDetailSeqNo = purchaseOrderDetailSeqNo;
            purchaseOrderDetailBo.GetPurchaseOrderDetail(UserManager.UserInfo, v);

            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderDetail.PurchaseOrderDetailIndex, CommonValues.PermissionCodes.PurchaseOrderDetail.PurchaseOrderDetailDetails)]
        [HttpGet]
        public ActionResult GetPurchaseOrderSparePartDetails(int id)
        {
            return View("_DeliveryPartInfo", new PurchaseOrderDetailBL().GetPurcaOrderSparePartDetailsModel(UserManager.UserInfo,id).Data);
        }

        #endregion

        public ActionResult GetTotalPrice(string poNumber)
        {
            var bo = new PurchaseOrderDetailBL();
            var totalPrice = bo.GetTotalPrice(poNumber).Model;

            return Json(new { TotalPrice = totalPrice });

        }
    }
}
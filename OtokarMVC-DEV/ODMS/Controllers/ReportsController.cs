using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSModel.Reports;
using System.Collections.Generic;
using System.Linq;
using ODMSCommon.Security;
using ODMSCommon.Resources;
using System;
using ODMSModel.CampaignVehicle;
using System.Data;
using System.Web.UI.WebControls;
using System.Web;
using System.IO;
using ODMSModel.StockCard;
using System.Text;
using ODMSModel.DownloadFileActionResult;
using ODMS.Core;
using System.Linq;
using ODMSBusiness.Business;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class ReportsController : ControllerBase
    {
        private readonly ReportCreateBL _reportCreateBL;
        public ReportsController(ReportCreateBL reportCreateBL)
        {
            _reportCreateBL = reportCreateBL;
        }

        #region WorkOrderDetailReport

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.WorkOrderDetailReportIndex)]
        [HttpGet]
        public ActionResult WorkOrderDetailReportIndex()
        {
            var bus = new DealerBL();
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.DealerRegionList = bus.ListDealerRegions().Data;
            ViewBag.DealerRegionId = UserManager.UserInfo.DealerID > 0 ? bus.GetDealer(UserManager.UserInfo, UserManager.UserInfo.DealerID).Model.DealerRegionId : 0;
            ViewBag.YesNoList = CommonBL.ListYesNoValueInt().Data;
            ViewBag.VehicleTypeList = VehicleTypeBL.ListVehicleTypeAsSelectList(UserManager.UserInfo, "").Data;
            ViewBag.ModelList = VehicleModelBL.ListVehicleModelAsSelectList(UserManager.UserInfo).Data;
            ViewBag.WorkOrderStatusList = new WorkOrderCardBL().ListWorkOrderStatus(UserManager.UserInfo).Data;

            return View();
        }

        //[AuthorizationFilter(CommonValues.PermissionCodes.Reports.WorkOrderDetailReportIndex)]
        public ActionResult ListWorkOrderDetailReport([DataSourceRequest] DataSourceRequest request, WorkOrderDetailReportListModel model)
        {
            var reportsBo = new ReportsBL();
            var v = new WorkOrderDetailReportListModel(request)
            {
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                DealerIdList = model.DealerIdList,
                DealerRegionIdList = model.DealerRegionIdList,
                ModelKodList = model.ModelKodList,
                StatusIdList = model.StatusIdList,
                VinNo = model.VinNo,
                Plate = model.Plate,
                VehicleType = model.VehicleType,
                PartCode = model.PartCode,
                LabourCode = model.LabourCode,
                IsDayTime = model.IsDayTime,
                WorkOrderIdList = model.WorkOrderIdList, //Dikkat virgül'lü kullanımı içeriyor!!!
                WarrantyStartDate = model.WarrantyStartDate,
                WarrantyEndDate = model.WarrantyEndDate
            };

            var totalCnt = 0;
            var totalVehicle = 0;
            var returnValue = reportsBo.ListWorkOrderDetailReport(UserManager.UserInfo, v, out totalCnt, out totalVehicle).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt,
                TotalVehicle = totalVehicle


            });
        }

        #endregion

        #region WorkOrderMaintReport
        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.WorkOrderMaintReportIndex)]
        [HttpGet]
        public ActionResult WorkOrderMaintReportIndex()
        {
            var bus = new DealerBL();
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.DealerRegionList = bus.ListDealerRegions().Data;
            ViewBag.ModelList = VehicleModelBL.ListVehicleModelAsSelectList(UserManager.UserInfo).Data;

            ViewBag.GuaranteeParameters = new List<SelectListItem>
            {
                new SelectListItem {Value = "1", Text = MessageResource.Global_Display_Yes},
                new SelectListItem {Value = "2", Text = MessageResource.Global_Display_No}
            };

            ViewBag.DealerRegionId = UserManager.UserInfo.DealerID > 0
                ? bus.GetDealer(UserManager.UserInfo, UserManager.UserInfo.DealerID).Model.DealerRegionId
                : 0;

            ViewBag.PeriodicMaintList = CommonBL.ListPeriodicMaintLang(UserManager.UserInfo).Data;
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.WorkOrderMaintReportIndex)]
        public ActionResult ListWorkOrderMaintReport([DataSourceRequest] DataSourceRequest request, WorkOrderMaintReportListModel model)
        {
            var reportsBo = new ReportsBL();
            var v = new WorkOrderMaintReportListModel(request)
            {
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                InvoiceStartDate = model.InvoiceStartDate,
                InvoiceEndDate = model.InvoiceEndDate,
                DealerIdList = model.DealerIdList,
                DealerRegionIdList = model.DealerRegionIdList,
                ModelKodList = model.ModelKodList,
                InvoiceNo = model.InvoiceNo,
                PeriodicMaint = model.PeriodicMaint,
                InGuarantee = model.InGuarantee
            };

            var totalCnt = 0;
            var returnValue = reportsBo.ListWorkOrderMaintReport(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region PurchaseOrderReport

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.PurchaseOrderReportIndex)]
        [HttpGet]
        public ActionResult PurchaseOrderReportIndex()
        {
            var bus = new DealerBL();
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.DealerRegionList = bus.ListDealerRegions().Data;
            ViewBag.DealerRegionId = UserManager.UserInfo.DealerID > 0 ? bus.GetDealer(UserManager.UserInfo, UserManager.UserInfo.DealerID).Model.DealerRegionId : 0;
            ViewBag.SupplierList = SupplierBL.ListSupplierComboAsSelectListItem(UserManager.UserInfo, true).Data;
            ViewBag.WorkOrderStatusList = new WorkOrderCardBL().ListWorkOrderStatus(UserManager.UserInfo).Data;
            ViewBag.StockTypeList = CommonBL.ListStockTypes(UserManager.UserInfo).Data;
            ViewBag.PoLocationList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.POLocation).Data;
            //ViewBag.YesNoList = CommonBL.ListLookup(CommonValues.LookupKeys.YesNo);
            ViewBag.YesNoList = CommonBL.ListYesNoValueInt().Data;
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.PurchaseOrderReportIndex)]
        public ActionResult ListPurchaseOrderReport([DataSourceRequest] DataSourceRequest request, PurchaseOrderFilterRequest model)
        {
            var reportsBo = new ReportsBL();
            var v = new PurchaseOrderFilterRequest(request)
            {
                PurchaseOrderStartDate = model.PurchaseOrderStartDate,
                PurchaseOrderEndDate = model.PurchaseOrderEndDate,
                DeliveryStartDate = model.DeliveryStartDate,
                DeliveryEndDate = model.DeliveryEndDate,
                InvoiceStartDate = model.InvoiceStartDate,
                InvoiceEndDate = model.InvoiceEndDate,
                DealerIdList = model.DealerIdList,
                DealerRegionIdList = model.DealerRegionIdList,
                SupplierIdList = model.SupplierIdList,
                PartCode = model.PartCode,
                IsOriginal = model.IsOriginal,
                StockTypeId = model.StockTypeId,
                PoLocation = model.PoLocation
            };

            decimal totalPrice = 0;
            int totalCount = 0;
            var returnValue = reportsBo.GetPurchaseOrderReport(UserManager.UserInfo, v, out totalPrice, out totalCount).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCount,
                TotalPrice = totalPrice
            });
        }

        #endregion

        #region PartExchangeReport

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.PartExchangeReportIndex)]
        [HttpGet]
        public ActionResult PartExchangeReportIndex()
        {
            var bus = new DealerBL();
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.DealerRegionList = bus.ListDealerRegions().Data;
            ViewBag.DealerRegionId = UserManager.UserInfo.DealerID > 0 ? bus.GetDealer(UserManager.UserInfo, UserManager.UserInfo.DealerID).Model.DealerRegionId : 0;
            ViewBag.ModelList = VehicleModelBL.ListVehicleModelAsSelectList(UserManager.UserInfo).Data;
            ViewBag.ProcessTypeCodeList = CommonBL.ListProcessType(UserManager.UserInfo).Data;
            ViewBag.CurrencyCodeList = CommonBL.ListCurrencies(UserManager.UserInfo).Data;
            ViewBag.GifCostCenterList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.GIFCategory).Data;
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.PartExchangeReportIndex)]
        public ActionResult ListPartExchangeReport([DataSourceRequest] DataSourceRequest request, PartExchangeFilterRequest model)
        {
            var reportsBo = new ReportsBL();
            var v = new PartExchangeFilterRequest(request)
            {
                CurrencyCodeList = model.CurrencyCodeList,
                DealerIdList = model.DealerIdList,
                DealerRegionIdList = model.DealerRegionIdList,
                EndDate = model.EndDate,
                GifCostCenterList = model.GifCostCenterList,
                MaxPrice = model.MaxPrice,
                ProcessTypeList = model.ProcessTypeList,
                StartDate = model.StartDate,
                VehicleModelList = model.VehicleModelList,
                PartCode = model.PartCode
            };

            var totalCnt = 0;
            var returnValue = reportsBo.GetPartExchangeReport(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.PartExchangeReportIndex)]
        [HttpGet]
        public ActionResult PartInfo(PartExchangeFilterRequest model)
        {
            return View("_PartInfo", model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.PartExchangeReportIndex)]
        public ActionResult ListPartInfo([DataSourceRequest] DataSourceRequest request, PartInfoRequest model)
        {
            var reportsBo = new ReportsBL();
            var v = new PartInfoRequest(request)
            {
                DealerId = model.DealerId,
                DealerRegionId = model.DealerRegionId,
                ProcessType = model.ProcessType,
                Category = model.Category,
                PartId = model.PartId,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                MaxPrice = model.MaxPrice,
                Currency = model.Currency,
                VehicleModel = model.VehicleModel

            };

            int totalCount = 0;
            var returnValue = reportsBo.ListPartInfo(UserManager.UserInfo, v, out totalCount).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCount
            });
        }

        #endregion

        #region CycleCountResultReport

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.CycleCountResultReportIndex)]
        [HttpGet]
        public ActionResult CycleCountResultReportIndex()
        {
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.CycleCountStatusList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.CycleCountLookupStatus).Data;
            ViewBag.CountStockDiffList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.CycleCountDiffStatus).Data;
            ViewBag.YesNoList = CommonBL.ListYesNoValueInt().Data;
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.CycleCountResultReportIndex)]
        public ActionResult ListCycleCountResultReport([DataSourceRequest] DataSourceRequest request, CycleCountResultReportFilterRequest model)
        {
            var reportsBo = new ReportsBL();
            var v = new CycleCountResultReportFilterRequest(request)
            {
                DealerIdList = model.DealerIdList,
                CountApproveEndDate = model.CountApproveEndDate,
                CountApproveStartDate = model.CountApproveStartDate,
                CountBeginDate = model.CountBeginDate,
                CountEndDate = model.CountEndDate,
                CountStatusList = model.CountStatusList,
                CycleCountDiffIdList = model.CycleCountDiffIdList,
                IsOriginal = model.IsOriginal
            };
            int totalCount = 0;
            var returnValue = reportsBo.GetCycleCountResultReport(UserManager.UserInfo, v, out totalCount).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCount
            });
        }


        #endregion

        #region WorkOrderPartHistoryReport

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.WorkOrderPartHistoryReportIndex)]
        [HttpGet]
        public ActionResult WorkOrderPartHistoryReportIndex()
        {
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.StockTypeList = CommonBL.ListStockTypes(UserManager.UserInfo).Data;
            ViewBag.WorkOrderStatusList = new WorkOrderCardBL().ListWorkOrderStatus(UserManager.UserInfo).Data;
            ViewBag.IndicatorTypeList = new AppointmentIndicatorSubCategoryBL().ListOfIndicatorTypeCodeAsSelectListItem(UserManager.UserInfo).Data;
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.WorkOrderPartHistoryReportIndex)]
        public ActionResult ListWorkOrderPartHistoryReport([DataSourceRequest] DataSourceRequest request, WorkOrderPartHistoryReportFilterRequest model)
        {
            var reportsBo = new ReportsBL();
            var v = new WorkOrderPartHistoryReportFilterRequest(request)
            {
                DealerIdList = model.DealerIdList,
                BeginDate = model.BeginDate,
                EndDate = model.EndDate,
                PartCode = model.PartCode,
                IndicatorList = model.IndicatorList,
                StockTypeList = model.StockTypeList,
                WorkOrderCardStatList = model.WorkOrderCardStatList
            };

            int totalCount = 0;
            var returnValue = reportsBo.GetWorkOrderPartHistoryReport(UserManager.UserInfo, v, out totalCount).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCount
            });
        }


        #endregion

        #region SentPartUsageReport

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.SentPartUsageReportIndex)]
        [HttpGet]
        public ActionResult SentPartUsageReportIndex()
        {
            var bus = new DealerBL();
            ViewBag.DealerIdList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.RegionIdList = bus.ListDealerRegions().Data;
            ViewBag.DealerRegionId = UserManager.UserInfo.DealerID > 0 ? bus.GetDealer(UserManager.UserInfo, UserManager.UserInfo.DealerID).Model.DealerRegionId : 0;
            ViewBag.PurchaseOrderTypeList = new PurchaseOrderTypeBL().PurchaseOrderTypeList(UserManager.UserInfo).Data;
            ViewBag.SaleTypeList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.PickSource).Data;
            ViewBag.StockTypeList = CommonBL.ListStockTypes(UserManager.UserInfo).Data;
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.SentPartUsageReportIndex)]
        public ActionResult ListSentPartUsageReport([DataSourceRequest] DataSourceRequest request, SentPartUsageReportFilterRequest model)
        {
            var reportsBo = new ReportsBL();
            var v = new SentPartUsageReportFilterRequest(request)
            {
                DealerIdList = model.DealerIdList,
                DealerRegionIdList = model.DealerRegionIdList,
                OrderEndDate = model.OrderEndDate,
                OrderStartDate = model.OrderStartDate,
                PartId = model.PartId,
                PurchaseOrderType = model.PurchaseOrderType
            };
            int totalCount = 0;
            var returnValue = reportsBo.GetSentPartUsageReport(UserManager.UserInfo, v, out totalCount).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCount
            });
        }

        #endregion

        #region SaleReport

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.SaleReportIndex)]
        [HttpGet]
        public ActionResult SaleReportIndex()
        {
            var model = new SaleReportFilterRequest();

            var saleTypeList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.PickSource).Data;
            ViewBag.SaleTypeList = saleTypeList;

            ViewBag.SaleStatusList = new List<SelectListItem>() {
                {new SelectListItem {Text=MessageResource.SaleReport_Display_Invoiced,Value="1",Selected=true } },
                {new SelectListItem {Text=MessageResource.PurchaseOrderInquiry_Index_Cancelled,Value="0" } }
            };

            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.CustomerList = CustomerBL.ListCustomerAsSelectListItem(UserManager.UserInfo).Data;
            ViewBag.VehicleModelList = VehicleModelBL.ListVehicleModelAsSelectList(UserManager.UserInfo).Data;
            ViewBag.VehicleTypeList = VehicleTypeBL.ListVehicleTypeAsSelectList(UserManager.UserInfo, null).Data;
            ViewBag.SaleTypeLookValList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.SaleTypeLookup).Data;

            model.SaleType = null;
            return View(model);
        }

        public ActionResult SaleReportExcelSample()
        {
            var bo = new StockCardBL();
            var ms = bo.SetExcelReport(null, string.Empty);
            return File(ms, CommonValues.ExcelContentType, MessageResource.SaleReport_PageTitle_Index + CommonValues.ExcelExtOld);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.SaleReportIndex)]
        [HttpPost]
        public ActionResult SaleReportIndex(SaleReportFilterRequest model, HttpPostedFileBase excelFile)
        {
            var saleTypeList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.PickSource).Data;           
            ViewBag.SaleTypeList = saleTypeList;
            model.SaleType = null;

            ViewBag.SaleStatusList = new List<SelectListItem>() {
                {new SelectListItem {Text=MessageResource.SaleReport_Display_Invoiced,Value="1",Selected=true } },
                {new SelectListItem {Text=MessageResource.PurchaseOrderInquiry_Index_Cancelled,Value="0" } }
            };

            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.CustomerList = CustomerBL.ListCustomerAsSelectListItem(UserManager.UserInfo).Data;
            ViewBag.VehicleModelList = VehicleModelBL.ListVehicleModelAsSelectList(UserManager.UserInfo).Data;
            ViewBag.VehicleTypeList = VehicleTypeBL.ListVehicleTypeAsSelectList(UserManager.UserInfo, null).Data;
            ViewBag.SaleTypeLookValList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.SaleTypeLookup).Data;


            if (excelFile != null)
            {
                StockCardBL bo = new StockCardBL();
                Stream s = excelFile.InputStream;
                StockCardViewModel viewMo = new StockCardViewModel();
                List<StockCardViewModel> listModel = bo.ParseExcel(UserManager.UserInfo, viewMo, s).Data;

                if (viewMo.ErrorNo > 0)
                {
                    var ms = bo.SetExcelReport(listModel, viewMo.ErrorMessage);

                    var fileViewModel = new DownloadFileViewModel
                    {
                        FileName = CommonValues.ErrorLog + DateTime.Now + CommonValues.ExcelExtOld,
                        ContentType = CommonValues.ExcelContentType,
                        MStream = ms,
                        Id = Guid.NewGuid()
                    };

                    Session[CommonValues.DownloadFileFormatCookieKey] = fileViewModel.Add(fileViewModel);
                    TempData[CommonValues.DownloadFileIdCookieKey] = fileViewModel.Id;

                    SetMessage(viewMo.ErrorMessage, CommonValues.MessageSeverity.Fail);

                    return View(model);
                }
                else
                {
                    StringBuilder partCodes = new StringBuilder();
                    foreach (StockCardViewModel mo in listModel)
                    {
                        partCodes.Append(mo.PartCode);
                        partCodes.Append(",");
                    }
                    if (partCodes.Length > 0)
                    {
                        partCodes.Remove(partCodes.Length - 1, 1);
                        model.PartCodeList = partCodes.ToString();
                    }
                }
            }
            else
            {
                SetMessage(MessageResource.Global_Warning_ExcelNotSelected, CommonValues.MessageSeverity.Fail);
            }

            return View(model);
        }


        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.SaleReportIndex)]
        public ActionResult ListSaleReport([DataSourceRequest] DataSourceRequest request, SaleReportFilterRequest model)
        {
            var reportsBo = new ReportsBL();
            var v = new SaleReportFilterRequest(request)
            {
                CreateBeginDate = model.CreateBeginDate,
                CreateEndDate = model.CreateEndDate,
                CustomerId = model.CustomerId,
                InvoiceBeginDate = model.InvoiceBeginDate,
                InvoiceEndDate = model.InvoiceEndDate,
                InvoiceNo = model.InvoiceNo,
                PurchaseStatus = model.PurchaseStatus,
                SaleType = model.SaleType,
                VinNo = model.VinNo,
                PartCode = model.PartCode,
                PartCodeList = model.PartCodeList,
                DealerId = model.DealerId,
                VehicleModel = model.VehicleModel,
                VehicleType = model.VehicleType,
                SaleTypeLookVal = model.SaleTypeLookVal
            };
            var totalCnt = 0;
            var returnValue = reportsBo.ListSaleReport(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region CarServiceDurationReport

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.CarServiceDurationReportIndex)]
        [HttpGet]
        public ActionResult CarServiceDurationReportIndex()
        {
            var bus = new DealerBL();
            ViewBag.DealerIdList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.RegionIdList = bus.ListDealerRegions().Data;
            ViewBag.VehicleModelList = VehicleModelBL.ListVehicleModelAsSelectList(UserManager.UserInfo).Data;
            ViewBag.WoStatusList = new WorkOrderCardBL().ListWorkOrderStatus(UserManager.UserInfo).Data;
            ViewBag.CustTypeList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.CustomerTypeLookup).Data;
            ViewBag.CustomerIdList = CustomerBL.ListCustomerNameAndNoAsSelectListItem().Data;
            ViewBag.WorkOrderStatusList = new WorkOrderCardBL().ListWorkOrderStatus(UserManager.UserInfo).Data;
            ViewBag.GroupType = CommonBL.ListGroupTypeValueInt(false).Data;
            ViewBag.DealerRegionId = UserManager.UserInfo.DealerID > 0 ? bus.GetDealer(UserManager.UserInfo, UserManager.UserInfo.DealerID).Model.DealerRegionId : 0;

            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.CarServiceDurationReportIndex)]
        public ActionResult CarServiceDurationReport([DataSourceRequest] DataSourceRequest request, VehicleServiceDurationFilterRequest model)
        {
            var reportsBo = new ReportsBL();
            var v = new VehicleServiceDurationFilterRequest(request)
            {
                GroupType = model.GroupType,
                BeginDate = model.BeginDate,
                EndDate = model.EndDate,
                DealerIdList = model.DealerIdList,
                RegionIdList = model.RegionIdList,
                VehicleModelList = model.VehicleModelList,
                VinNo = model.VinNo,
                CustTypeList = model.CustTypeList,
                CustomerIdList = model.CustomerIdList,
                StatusIdList = model.StatusIdList,
                //Procedurde bu işlemi yapmak için IsDetail 0 seçilmesi gerekiyor ; 1 seçimi başka bir işlem gerçekleştirmekte.
                IsDetail = 0
            };

            var totalCnt = 0;
            var returnValue = reportsBo.GetCarServiceDurationReport(UserManager.UserInfo, v, out totalCnt).Data;


            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });

        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.CarServiceDurationReportIndex)]
        [HttpGet]
        public ActionResult WorkOrderInfo(WorkOrderInfoDetailParameter model/*int groupType, string groupName, string statusIds*/)
        {
            return View("_WorkOrderInfo", model);
        }


        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.CarServiceDurationReportIndex)]
        public ActionResult ListWorkOrderInfo([DataSourceRequest] DataSourceRequest request, WorkOrderInfoRequest model)
        {
            //d
            var reportsBo = new ReportsBL();
            var v = new WorkOrderInfoRequest(request)
            {
                GroupType = model.group_type,
                BeginDate = model.BeginDate,
                EndDate = model.EndDate,
                DealerIdList = model.DealerIdList,
                RegionIdList = model.RegionIdList,
                VehicleModelList = model.VehicleModelList.AddSingleQuote(),
                VinNo = model.VinNo,
                CustTypeList = model.CustTypeList.AddSingleQuote(),
                CustomerIdList = model.CustomerIdList,
                StatusIdList = model.StatusIdList,
                IsDetail = 1,
                GroupTypeVal = model.GroupTypeVal,
                VehicleId = model.VehicleId
            };

            int totalCount = 0;
            var returnValue = reportsBo.ListWorkOrderInfo(UserManager.UserInfo, v, out totalCount).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCount
            });
        }

        #endregion

        #region LaborCostPerVehicleReport

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.LaborCostPerVehicleReportIndex)]
        [HttpGet]
        public ActionResult LaborCostPerVehicleReportIndex()
        {
            var bus = new DealerBL();

            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.DealerRegionList = bus.ListDealerRegions().Data;
            ViewBag.CurrencyCodeList = CommonBL.ListCurrencies(UserManager.UserInfo).Data;
            ViewBag.CustomerList = CustomerBL.ListCustomerAsSelectListItem(UserManager.UserInfo).Data;
            ViewBag.ProcessTypeList = CommonBL.ListProcessType(UserManager.UserInfo).Data;
            ViewBag.VehicleTypeList = VehicleTypeBL.ListVehicleTypeAsSelectList(UserManager.UserInfo, null).Data;
            ViewBag.VehicleModelList = VehicleModelBL.ListVehicleModelAsSelectList(UserManager.UserInfo).Data;
            ViewBag.ProcessTypes = CommonBL.ListProcessType(UserManager.UserInfo).Data;
            ViewBag.GuaranteeParameters = CommonBL.ListYesNoValueInt().Data;
            ViewBag.GuaranteeCategories = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.GIFCategory).Data;
            ViewBag.DealerRegionId = UserManager.UserInfo.DealerID > 0 ? bus.GetDealer(UserManager.UserInfo, UserManager.UserInfo.DealerID).Model.DealerRegionId : 0;
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.LaborCostPerVehicleReportIndex)]
        public ActionResult LaborCostPerVehicleReport([DataSourceRequest] DataSourceRequest request, LaborCostPerVehicleReportFilterRequest model)
        {
            var reportsBo = new ReportsBL();

            #region Text Id List

            if (!string.IsNullOrEmpty(model.ProcessTypeIdList))
            {
                var processType_ = model.ProcessTypeIdList.Split(',');
                model.ProcessTypeIdList = "";
                for (var i = 0; i < processType_.Length; i++)
                {
                    model.ProcessTypeIdList += "'" + processType_[i] + "'" + (i + 1 < processType_.Length ? "," : "");
                }
            }
            if (!string.IsNullOrEmpty(model.VehicleModelIdList))
            {
                var vehicleModel_ = model.VehicleModelIdList.Split(',');
                model.VehicleModelIdList = "";
                for (var i = 0; i < vehicleModel_.Length; i++)
                {
                    model.VehicleModelIdList += "'" + vehicleModel_[i] + "'" + (i + 1 < vehicleModel_.Length ? "," : "");
                }
            }

            #endregion

            var v = new LaborCostPerVehicleReportFilterRequest(request)
            {
                Currency = model.Currency,
                DealerIdList = model.DealerIdList,
                RegionIdList = model.RegionIdList,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                GuaranteeCategories = model.GuaranteeCategories,
                GuaranteeConfirmDate = model.GuaranteeConfirmDate,
                CustomerIdList = model.CustomerIdList,
                ProcessTypeIdList = model.ProcessTypeIdList,
                VehicleModelIdList = model.VehicleModelIdList,
                VehicleTypeIdList = model.VehicleTypeIdList,
                VinNo = model.VinNo,
                InGuarantee = model.InGuarantee,
                MinKM = model.MinKM,
                MaxKM = model.MaxKM
            };

            var totalCnt = 0;
            var returnValue = reportsBo.GetLaborCostPerVehicleReport(UserManager.UserInfo, v, out totalCnt).Data;

            var list = new List<LaborCostPerVehicleReport>();
            var processTypeList = CommonBL.ListProcessType(UserManager.UserInfo).Data;

            foreach (var chargePerCarReport in returnValue)
            {
                if (string.IsNullOrEmpty(chargePerCarReport.ProcessTypeCode))
                    continue;

                if (list.Any(a => a.DealerName == chargePerCarReport.DealerName && a.DealerRegionName == chargePerCarReport.DealerRegionName && a.ModelName == chargePerCarReport.ModelName && a.NameSurname == chargePerCarReport.NameSurname && a.TypeName == chargePerCarReport.TypeName && a.VinNo == chargePerCarReport.VinNo && chargePerCarReport.InGuarantee == a.InGuarantee))
                {
                    var item = list.FirstOrDefault(a => a.DealerName == chargePerCarReport.DealerName && a.DealerRegionName == chargePerCarReport.DealerRegionName && a.ModelName == chargePerCarReport.ModelName && a.NameSurname == chargePerCarReport.NameSurname && a.TypeName == chargePerCarReport.TypeName && a.VinNo == chargePerCarReport.VinNo);

                    // item.WorkOrderCount += chargePerCarReport.WorkOrderCount;
                    item.WorkOrderDetailCount += chargePerCarReport.WorkOrderDetailCount;
                    var processType = item.ProcessTypes.FindIndex(f => f.Code == chargePerCarReport.ProcessTypeCode);
                    if (processType != -1)
                    {
                        item.ProcessTypes[processType].LabourPrice += chargePerCarReport.LabourPrice;
                        item.ProcessTypes[processType].PartPrice += chargePerCarReport.PartPrice;
                    }
                }
                else
                {
                    chargePerCarReport.ProcessTypes = new List<ChargePerCarProcessType>();
                    chargePerCarReport.ProcessTypes.AddRange(processTypeList.Select(s => new ChargePerCarProcessType { Name = s.Text, Code = s.Value }));

                    var processType = chargePerCarReport.ProcessTypes.FirstOrDefault(f => f.Code == chargePerCarReport.ProcessTypeCode);
                    if (processType != null)
                    {
                        processType.LabourPrice = chargePerCarReport.LabourPrice;
                        processType.PartPrice = chargePerCarReport.PartPrice;
                    }
                    chargePerCarReport.TotalAmount = chargePerCarReport.ProcessTypes.Sum(x => x.PartPrice) + chargePerCarReport.ProcessTypes.Sum(x => x.LabourPrice);
                    list.Add(chargePerCarReport);
                }
            }

            return Json(new
            {
                Data = list,
                Total = list.Count
            });

        }


        #endregion

        #region KilometerDistributionReport

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.KilometerDistributionReportIndex)]
        [HttpGet]
        public ActionResult KilometerDistributionReportIndex()
        {
            var bus = new DealerBL();
            ViewBag.DealerIdList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.RegionIdList = bus.ListDealerRegions().Data;
            ViewBag.DealerRegionId = UserManager.UserInfo.DealerID > 0 ? bus.GetDealer(UserManager.UserInfo, UserManager.UserInfo.DealerID).Model.DealerRegionId : 0;
            ViewBag.ProcessTypeCodeList = CommonBL.ListProcessType(UserManager.UserInfo).Data;
            ViewBag.VehicleModelList = VehicleModelBL.ListVehicleModelAsSelectList(UserManager.UserInfo).Data;
            ViewBag.CustTypeList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.CustomerTypeLookup).Data;
            ViewBag.CustomerIdList = CustomerBL.ListCustomerNameAndNoAsSelectListItem().Data;
            ViewBag.GroupType = CommonBL.ListGroupTypeValueInt(false).Data;
            ViewBag.CampaignCodeList = CampaignBL.ListAllCampaignAsSelectListItem().Data;

            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.KilometerDistributionReportIndex)]
        public ActionResult KilometerDistributionReport([DataSourceRequest] DataSourceRequest request, KilometerDistributionFilterRequest model)
        {
            var reportsBo = new ReportsBL();
            var v = new KilometerDistributionFilterRequest(request)
            {
                GroupType = model.GroupType,
                BeginDate = model.BeginDate,
                EndDate = model.EndDate,
                DealerIdList = model.DealerIdList,
                VehicleModelList = model.VehicleModelList,
                RegionIdList = model.RegionIdList,
                ProcessTypeList = model.ProcessTypeList,
                CustTypeList = model.CustTypeList,
                CustomerIdList = model.CustomerIdList
            };

            int totalCnt = 0;
            var returnValue = reportsBo.GetKilometerDistributionReport(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });

        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.KilometerDistributionReportIndex)]
        [HttpGet]
        public ActionResult KilometerReportInfo(int groupType, string groupCode, string VehicleModelList, string ProcessTypeList, string CustTypeList, string CustomerIdList)
        {

            WorkOrderDetailKilometerRequest w = new WorkOrderDetailKilometerRequest();
            w.GroupType = groupType;
            w.GroupCode = groupCode;
            w.VehicleModelList = VehicleModelList;
            w.ProcessTypeList = ProcessTypeList;
            w.CustTypeList = CustTypeList;
            w.CustomerIdList = CustomerIdList;

            return View("_KilometerReportInfo", w);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.KilometerDistributionReportIndex)]
        public ActionResult ListKilometerReportInfo([DataSourceRequest] DataSourceRequest request, WorkOrderDetailKilometerRequest model)
        {
            var reportsBo = new ReportsBL();
            var v = new WorkOrderDetailKilometerRequest(request)
            {
                GroupType = model.GroupType,
                GroupCode = model.GroupCode,
                CustomerId = model.CustomerId,
                VehicleModelList = model.VehicleModelList,
                ProcessTypeList = model.ProcessTypeList,
                CustTypeList = model.CustTypeList,
                CustomerIdList = model.CustomerIdList
            };

            int totalCount = 0;
            var returnValue = reportsBo.ListWorkOrderDetailKilometer(UserManager.UserInfo, v, out totalCount).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCount
            });
        }

        #endregion

        #region CampaignSummaryReport

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.CampaignSummaryReportIndex)]
        [HttpGet]
        public ActionResult CampaignSummaryReportIndex()
        {
            var bus = new DealerBL();
            ViewBag.DealerIdList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.RegionIdList = bus.ListDealerRegions().Data;
            ViewBag.DealerRegionId = UserManager.UserInfo.DealerID > 0 ? bus.GetDealer(UserManager.UserInfo, UserManager.UserInfo.DealerID).Model.DealerRegionId : 0;
            ViewBag.VehicleModelList = VehicleModelBL.ListVehicleModelAsSelectList(UserManager.UserInfo).Data;
            ViewBag.CurrencyCodeList = CommonBL.ListCurrencies(UserManager.UserInfo).Data;
            ViewBag.WarrantyStatusList = new List<SelectListItem>
            {
                new SelectListItem {Value = "1", Text = "Garantisi Var"},
                new SelectListItem {Value = "2", Text = "Garanti Dışı"}
            };

            ViewBag.CampaignCodeList = CampaignBL.ListAllCampaignAsSelectListItem().Data;
            ViewBag.GroupType = CommonBL.ListGroupTypeValueInt().Data;

            ViewBag.CampaignStatus = new List<SelectListItem>
            {
                new SelectListItem {Value = "1", Text = "Devam Eden"},
                new SelectListItem {Value = "2", Text = "Biten"}
            };

            ViewBag.IsMust = new List<SelectListItem>
            {
                new SelectListItem {Value = "1", Text = MessageResource.Global_Display_Yes},
                new SelectListItem {Value = "2", Text = MessageResource.Global_Display_No}
            };

            return View();
        }



        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.CampaignSummaryReportIndex)]
        public ActionResult CampaignSummaryReport([DataSourceRequest] DataSourceRequest request, CampaignSummaryReportFilterRequest model)
        {
            var reportsBo = new ReportsBL();
            var v = new CampaignSummaryReportFilterRequest(request)
            {
                DealerIdList = model.DealerIdList,
                RegionIdList = model.RegionIdList,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Currency = (!string.IsNullOrEmpty(model.Currency)) ? string.Format("'{0}'", model.Currency) : string.Empty,
                CampaignCode = model.CampaignCode,
                VehicleModelList = model.VehicleModelList,
                IsWarranty = model.IsWarranty,
                GroupType = model.GroupType,
                CampaignStatus = model.CampaignStatus,
                IsMust = model.IsMust,
                GuaranteeConfirmStartDate = model.GuaranteeConfirmStartDate,
                GuaranteeConfirmEndDate = model.GuaranteeConfirmEndDate
            };
            if (v.Currency == "0") { v.Currency = null; }

            int totalCnt = 0;
            var returnValue = reportsBo.GetCampaignSummaryReport(UserManager.UserInfo, v, out totalCnt).Data;

            var result = from d in returnValue.AsEnumerable()
                         group d by new
                         {
                             d.GroupCode,
                             d.GroupName,
                             d.GroupType,
                             d.CampaignCode,
                             d.Description,
                             d.StartDate,
                             d.EndDate,
                             d.CampaignUseVehicle,
                             d.Currency,
                             d.TotalCampaignInternalVehicle
                         }
                into grp
                         select new CampaignSummaryReport()
                         {
                             GroupCode = grp.Key.GroupCode,
                             GroupName = grp.Key.GroupName,
                             GroupType = grp.Key.GroupType,
                             CampaignCode = grp.Key.CampaignCode,
                             Description = grp.Key.Description,
                             StartDate = grp.Key.StartDate,
                             EndDate = grp.Key.EndDate,
                             CampaignUseVehicle = grp.Key.CampaignUseVehicle,
                             Currency = grp.Key.Currency,
                             TotalCampaignInternalVehicle = grp.Key.TotalCampaignInternalVehicle,
                             TotalCampaignUseVehicle = grp.Sum(f => f.TotalCampaignUseVehicle),
                             //Orderqty = grp.Sum(f => f.Orderqty),
                             Application = grp.Sum(f => f.Application),
                             WorkerAmount = grp.Sum(f => f.WorkerAmount),
                             BitMount = grp.Sum(f => f.BitMount),
                             ReturnedQty = grp.Sum(f => f.ReturnedQty)
                         };

            return Json(new
            {
                Data = result,
                Total = totalCnt
            });
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.CampaignSummaryReportIndex)]
        public ActionResult CampaignVehicles(string campaignCode)
        {
            ViewBag.CampaignCode = campaignCode;
            return View("_CampaignVehicles");
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.CampaignSummaryReportIndex)]
        public ActionResult CampaignVehicleList([DataSourceRequest] DataSourceRequest request, string campaignCode)
        {
            var bo = new CampaignVehicleBL();
            var total = 0;
            var list = bo.ListCampaignVehicles(UserManager.UserInfo, new CampaignVehicleListModel(request) { CampaignCode = campaignCode }, out total).Data;
            return Json(new
            {
                Data = list,
                Total = total
            });
        }


        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.CampaignSummaryReportIndex)]
        [HttpGet]
        public ActionResult CampaignSummaryInfo(string campaignCode, string regionIdList, string dealerIdList, string groupCode, int groupType, string currency, DateTime? startDate, DateTime? endDate, string vehicleModel, int guaranteeStat, int isMust, int campaignStatus, DateTime? guaranteeConfirmStartDate, DateTime? guaranteeConfirmEndDate)
        {
            CampaignSummaryInfoDetailParameter c = new CampaignSummaryInfoDetailParameter
            {
                CampaignCode = campaignCode,
                DealerIdList = dealerIdList,
                RegionIdList = regionIdList,
                GroupCode = groupCode,
                GroupType = groupType,
                Currency = currency,
                StartDate = startDate,
                EndDate = endDate,
                VehicleModel = vehicleModel,
                GuaranteeStat = guaranteeStat,
                IsMust = isMust,
                CampaignStatus = campaignStatus,
                GuaranteeConfirmStartDate = guaranteeConfirmStartDate,
                GuaranteeConfirmEndDate = guaranteeConfirmEndDate

            };
            return View("_CampaignSummaryInfo", c);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.CampaignSummaryReportIndex)]
        public ActionResult ListCampaignSummaryInfo([DataSourceRequest] DataSourceRequest request, CampaignSummaryInfoRequest model)
        {
            var reportsBo = new ReportsBL();

            var v = new CampaignSummaryInfoRequest(request)
            {
                CampaignCode = model.CampaignCode,
                DealerIdList = model.DealerIdList,
                RegionIdList = model.RegionIdList,
                GroupCode = model.GroupCode,
                GroupType = model.GroupType,
                Currency = model.Currency,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                VehicleModel = model.VehicleModel,
                GuaranteeStat = model.GuaranteeStat,
                IsMust = model.IsMust,
                CampaignStatus = model.CampaignStatus,
                GuaranteeConfirmStartDate = model.GuaranteeConfirmStartDate,
                GuaranteeConfirmEndDate = model.GuaranteeConfirmEndDate

            };
            int totalCount = 0;
            var returnValue = reportsBo.ListCampaignSummaryInfo(UserManager.UserInfo, v, out totalCount).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCount
            });
        }

        #endregion

        #region PartStockReport

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.PartStockReportIndex)]
        [HttpGet]
        public ActionResult PartStockReportIndex()
        {
            var bus = new DealerBL();
            //ViewBag.IsOriginal = new List<SelectListItem>
            //{
            //    new SelectListItem {Value = "1", Text = MessageResource.Global_Display_Yes},
            //    new SelectListItem {Value = "0", Text = MessageResource.Global_Display_No},
            //};

            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.DealerRegionList = bus.ListDealerRegions().Data;
            ViewBag.StokcTypeList = CommonBL.ListStockTypes(UserManager.UserInfo).Data;
            ViewBag.CurrencyCodeList = CommonBL.ListCurrencies(UserManager.UserInfo).Data;
            ViewBag.DealerRegionId = UserManager.UserInfo.DealerID > 0 ? bus.GetDealer(UserManager.UserInfo, UserManager.UserInfo.DealerID).Model.DealerRegionId : 0;
            ViewBag.PartClassList = new SparePartBL().ListPartClassCodes(UserManager.UserInfo).Data;
            ViewBag.YesNoList = CommonBL.ListYesNoValueIntWithAll(2).Data;
            ViewBag.VehicleGroupList = VehicleGroupBL.ListVehicleGroupAsSelectList(UserManager.UserInfo).Data;

            return View(new PartStockFilterRequest());
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.PartStockReportIndex)]
        [HttpPost]
        public ActionResult PartStockReportIndex(PartStockFilterRequest model, HttpPostedFileBase excelFile)
        {
            var bus = new DealerBL();
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.DealerRegionList = bus.ListDealerRegions().Data;
            ViewBag.StokcTypeList = CommonBL.ListStockTypes(UserManager.UserInfo).Data;
            ViewBag.CurrencyCodeList = CommonBL.ListCurrencies(UserManager.UserInfo).Data;
            ViewBag.DealerRegionId = UserManager.UserInfo.DealerID > 0 ? bus.GetDealer(UserManager.UserInfo, UserManager.UserInfo.DealerID).Model.DealerRegionId : 0;
            ViewBag.PartClassList = new SparePartBL().ListPartClassCodes(UserManager.UserInfo).Data;
            ViewBag.YesNoList = CommonBL.ListYesNoValueIntWithAll(2).Data;
            ViewBag.VehicleGroupList = VehicleGroupBL.ListVehicleGroupAsSelectList(UserManager.UserInfo).Data;

            if (excelFile != null)
            {
                StockCardBL bo = new StockCardBL();
                Stream s = excelFile.InputStream;
                StockCardViewModel viewMo = new StockCardViewModel();
                List<StockCardViewModel> listModel = bo.ParseExcel(UserManager.UserInfo, viewMo, s).Data;

                if (viewMo.ErrorNo > 0)
                {
                    var ms = bo.SetExcelReport(listModel, viewMo.ErrorMessage);

                    var fileViewModel = new DownloadFileViewModel
                    {
                        FileName = CommonValues.ErrorLog + DateTime.Now + CommonValues.ExcelExtOld,
                        ContentType = CommonValues.ExcelContentType,
                        MStream = ms,
                        Id = Guid.NewGuid()
                    };

                    Session[CommonValues.DownloadFileFormatCookieKey] = fileViewModel.Add(fileViewModel);
                    TempData[CommonValues.DownloadFileIdCookieKey] = fileViewModel.Id;

                    SetMessage(viewMo.ErrorMessage, CommonValues.MessageSeverity.Fail);

                    return View(model);
                }
                else
                {
                    StringBuilder partCodes = new StringBuilder();
                    foreach (StockCardViewModel mo in listModel)
                    {
                        partCodes.Append(mo.PartCode);
                        partCodes.Append(",");
                    }
                    if (partCodes.Length > 0)
                    {
                        partCodes.Remove(partCodes.Length - 1, 1);
                        model.PartCodeList = partCodes.ToString();
                    }
                }
            }
            else
            {
                SetMessage(MessageResource.Global_Warning_ExcelNotSelected, CommonValues.MessageSeverity.Fail);
            }
            TempData["partCodes"] = model.PartCodeList;
            model.PartCodeList = null;


            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.PartStockReportIndex)]
        public ActionResult ListPartStockReport([DataSourceRequest] DataSourceRequest request, PartStockFilterRequest model)
        {
            var reportsBo = new ReportsBL();
            var v = new PartStockFilterRequest(request)
            {
                Currency = model.Currency,
                DealerIdList = model.DealerIdList,
                RegionIdList = model.RegionIdList,
                Date = model.Date,
                IsOriginal = model.IsOriginal,
                StockTypeIdList = model.StockTypeIdList,
                PartId = model.PartId,
                PartClassCodes = model.PartClassCodes,
                PartCodeList = model.PartCodeList,
            };

            decimal totalPrice = 0;
            int totalCount = 0;
            var returnValue = reportsBo.GetPartStockReport(UserManager.UserInfo, v, out totalPrice, out totalCount).Data;
            returnValue.ForEach(f => f.TotalPrice = f.StockQuantity * f.AvgDealerPrice);
            return Json(new
            {
                Data = returnValue,
                Total = totalCount,
                TotalPrice = totalPrice
            });
        }


        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.PartStockReportIndex)]
        [HttpPost]
        public ActionResult PartStockReportForExcel([DataSourceRequest] DataSourceRequest request, PartStockFilterRequest model)
        {
            var reportsBo = new ReportsBL();
            var v = new PartStockFilterRequest(request)
            {
                Currency = model.Currency,
                DealerIdList = model.DealerIdList,
                RegionIdList = model.RegionIdList,
                Date = model.Date,
                IsOriginal = model.IsOriginal,
                StockTypeIdList = model.StockTypeIdList,
                PartId = model.PartId,
                PartClassCodes = model.PartClassCodes,
                PartCodeList = model.PartCodeList
            };

            decimal totalPrice = 0;
            int totalCount = 0;

            var returnValue = reportsBo.GetPartStockReport(UserManager.UserInfo, v, out totalPrice, out totalCount).Data;
            returnValue.ForEach(f => f.TotalPrice = f.StockQuantity * f.AvgDealerPrice);

            var dt = new DataTable();
            dt.Columns.Add(MessageResource.PartStockReport_Region, typeof(string));
            dt.Columns.Add(MessageResource.PartStockReport_Dealer, typeof(string));
            dt.Columns.Add(MessageResource.PartStockReport_Is_Original_Part, typeof(string));
            dt.Columns.Add(MessageResource.PartStockReport_Part_Section, typeof(string));
            dt.Columns.Add(MessageResource.PartStockReport_Part_Class, typeof(string));
            dt.Columns.Add(MessageResource.PartStockReport_Part_Code, typeof(string));
            dt.Columns.Add(MessageResource.PartStockReport_Part_Name, typeof(string));
            dt.Columns.Add(MessageResource.PartStockReport_StockType, typeof(string));
            dt.Columns.Add(MessageResource.PartStockReport_Usage_Stock, typeof(string));
            dt.Columns.Add(MessageResource.PartStockReport_Package_Count, typeof(string));
            dt.Columns.Add(MessageResource.PartStockReport_Critical_Stock, typeof(string));
            dt.Columns.Add(MessageResource.PartStockReport_Min_Stock_Level, typeof(string));
            dt.Columns.Add(MessageResource.PartStockReport_Max_Stock_Level, typeof(string));
            dt.Columns.Add(MessageResource.PartStockReport_Dealer_Startup_Stock_Level, typeof(string));
            dt.Columns.Add(MessageResource.PartStockReport_Cost_Avg, typeof(string));
            dt.Columns.Add(MessageResource.PartStockReport_Stock_Total_Price, typeof(string));
            dt.Columns.Add(MessageResource.PartStockReport_Currency, typeof(string));
            dt.Columns.Add(MessageResource.PartStockReport_Stock_Age, typeof(string));
            dt.Columns.Add(MessageResource.PartStockReport_TotalAmount, typeof(string));

            foreach (var item in returnValue)
            {
                var dtRow = dt.NewRow();
                dtRow[0] = item.DealerRegionName;
                dtRow[1] = item.DealerName;
                dtRow[2] = item.IsOriginalPart;
                dtRow[3] = item.PartSectionName;
                dtRow[4] = item.PartClassName;
                dtRow[5] = item.PartCode;
                dtRow[6] = item.PartName;
                dtRow[7] = item.StockTypeName;
                dtRow[8] = item.StockQuantity;
                dtRow[9] = item.PackageQuantity;
                dtRow[10] = item.CriticalStockQuantity;
                dtRow[11] = item.MinStockQuantity;
                dtRow[12] = item.MaxStockQunatity;
                dtRow[13] = item.StartupQuantity;
                dtRow[14] = item.AvgDealerPrice;
                dtRow[15] = item.TotalPrice;
                dtRow[16] = item.Currency;
                dtRow[17] = item.StockAge;
                dtRow[18] = item.TotalAmount;

                dt.Rows.Add(dtRow);
            }
            var filename = MessageResource.PartStockReport_PageTitle_Index + ".xls";
            var tw = new System.IO.StringWriter();
            var hw = new System.Web.UI.HtmlTextWriter(tw);
            var dgGrid = new DataGrid { DataSource = dt };
            dgGrid.DataBind();
            dgGrid.RenderControl(hw);

            var rows = new List<List<Tuple<object, string>>>();

            var headerList = new List<string>();
            foreach (DataColumn item in dt.Columns)
            {
                headerList.Add(item.ColumnName);
            }

            foreach (DataRow item in dt.Rows)
            {
                var row = new List<Tuple<object, string>>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(new Tuple<object, string>(item[col.ColumnName].ToString(), col.ColumnName));
                }
                rows.Add(row);
            }

            var excelBytes = new ExcelHelper().GenerateExcel(headerList, rows, new List<FilterDto>(), reportName: filename, reportObject: dt);


            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
        }
        #endregion

        #region ChargePerCarReportIndex

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.ChargePerCarReportIndex)]
        [HttpGet]
        public ActionResult ChargePerCarReportIndex()
        {
            var bus = new DealerBL();

            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.DealerRegionList = bus.ListDealerRegions().Data;
            ViewBag.CurrencyCodeList = CommonBL.ListCurrencies(UserManager.UserInfo).Data;
            ViewBag.CustomerList = CustomerBL.ListCustomerAsSelectListItem(UserManager.UserInfo).Data;
            ViewBag.ProcessTypeList = CommonBL.ListProcessType(UserManager.UserInfo).Data;
            ViewBag.VehicleTypeList = VehicleTypeBL.ListVehicleTypeAsSelectList(UserManager.UserInfo, null).Data;
            ViewBag.VehicleModelList = VehicleModelBL.ListVehicleModelAsSelectList(UserManager.UserInfo).Data;
            ViewBag.ProcessTypes = CommonBL.ListProcessType(UserManager.UserInfo).Data;
            ViewBag.GuaranteeParameters = CommonBL.ListYesNoValueInt().Data;
            ViewBag.GuaranteeCategories = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.GIFCategory).Data;
            ViewBag.DealerRegionId = UserManager.UserInfo.DealerID > 0 ? bus.GetDealer(UserManager.UserInfo, UserManager.UserInfo.DealerID).Model.DealerRegionId : 0;

            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.ChargePerCarReportIndex)]
        public ActionResult ListChargePerCarReport([DataSourceRequest] DataSourceRequest request, ChargePerCarFilterRequest model)
        {
            var reportsBo = new ReportsBL();

            #region Text Id List

            if (!string.IsNullOrEmpty(model.ProcessTypeIdList))
            {
                var processType_ = model.ProcessTypeIdList.Split(',');
                model.ProcessTypeIdList = "";
                for (var i = 0; i < processType_.Length; i++)
                {
                    model.ProcessTypeIdList += "'" + processType_[i] + "'" + (i + 1 < processType_.Length ? "," : "");
                }
            }
            if (!string.IsNullOrEmpty(model.VehicleModelIdList))
            {
                var vehicleModel_ = model.VehicleModelIdList.Split(',');
                model.VehicleModelIdList = "";
                for (var i = 0; i < vehicleModel_.Length; i++)
                {
                    model.VehicleModelIdList += "'" + vehicleModel_[i] + "'" + (i + 1 < vehicleModel_.Length ? "," : "");
                }
            }

            #endregion

            var v = new ChargePerCarFilterRequest(request)
            {
                Currency = model.Currency,
                DealerIdList = model.DealerIdList,
                RegionIdList = model.RegionIdList,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                GuaranteeCategories = model.GuaranteeCategories,
                GuaranteeConfirmDate = model.GuaranteeConfirmDate,
                CustomerIdList = model.CustomerIdList,
                ProcessTypeIdList = model.ProcessTypeIdList,
                VehicleModelIdList = model.VehicleModelIdList,
                VehicleTypeIdList = model.VehicleTypeIdList,
                VinNo = model.VinNo,
                InGuarantee = model.InGuarantee,
                MinKM = model.MinKM,
                MaxKM = model.MaxKM
            };

            var totalCnt = 0;
            var returnValue = reportsBo.GetChargePerCarReport(UserManager.UserInfo, v, out totalCnt).Data;

            var list = new List<ChargePerCarReport>();
            var processTypeList = CommonBL.ListProcessType(UserManager.UserInfo).Data;

            foreach (var chargePerCarReport in returnValue)
            {
                if (string.IsNullOrEmpty(chargePerCarReport.ProcessTypeCode))
                    continue;

                if (list.Any(a => a.DealerName == chargePerCarReport.DealerName && a.DealerRegionName == chargePerCarReport.DealerRegionName && a.ModelName == chargePerCarReport.ModelName && a.NameSurname == chargePerCarReport.NameSurname && a.TypeName == chargePerCarReport.TypeName && a.VinNo == chargePerCarReport.VinNo && chargePerCarReport.InGuarantee == a.InGuarantee && chargePerCarReport.IdVehicle == a.IdVehicle && chargePerCarReport.CategoryLookval == a.CategoryLookval))
                {
                    var item = list.FirstOrDefault(a => a.DealerName == chargePerCarReport.DealerName && a.DealerRegionName == chargePerCarReport.DealerRegionName && a.ModelName == chargePerCarReport.ModelName && a.NameSurname == chargePerCarReport.NameSurname && a.TypeName == chargePerCarReport.TypeName && a.VinNo == chargePerCarReport.VinNo && chargePerCarReport.InGuarantee == a.InGuarantee && chargePerCarReport.IdVehicle == a.IdVehicle && chargePerCarReport.CategoryLookval == a.CategoryLookval);


                    item.WorkOrderCount = returnValue.Where(x => x.VinNo == item.VinNo && x.CustomerId == item.CustomerId && x.CategoryLookval == item.CategoryLookval).GroupBy(x => x.WorkOrderId).Count();


                    item.WorkOrderDetailCount += chargePerCarReport.WorkOrderDetailCount;
                    var processType = item.ProcessTypes.FindIndex(f => f.Code == chargePerCarReport.ProcessTypeCode);
                    if (processType != -1)
                    {
                        item.ProcessTypes[processType].LabourPrice += chargePerCarReport.LabourPrice;
                        item.ProcessTypes[processType].PartPrice += chargePerCarReport.PartPrice;
                    }
                }
                else
                {
                    chargePerCarReport.ProcessTypes = new List<ChargePerCarProcessType>();
                    chargePerCarReport.ProcessTypes.AddRange(processTypeList.Select(s => new ChargePerCarProcessType { Name = s.Text, Code = s.Value }));

                    var processType = chargePerCarReport.ProcessTypes.FirstOrDefault(f => f.Code == chargePerCarReport.ProcessTypeCode);
                    if (processType != null)
                    {
                        processType.LabourPrice = chargePerCarReport.LabourPrice;
                        processType.PartPrice = chargePerCarReport.PartPrice;
                    }
                    chargePerCarReport.TotalAmount = chargePerCarReport.ProcessTypes.Sum(x => x.PartPrice) + chargePerCarReport.ProcessTypes.Sum(x => x.LabourPrice);
                    list.Add(chargePerCarReport);
                }
            }

            return Json(new
            {
                Data = list,
                Total = list.Count
            });
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.ChargePerCarReportIndex)]
        [HttpPost]
        public ActionResult ListChargePerCarReportForExcel([DataSourceRequest] DataSourceRequest request, ChargePerCarFilterRequest model)
        {
            var reportsBo = new ReportsBL();

            #region Text Id List

            if (!string.IsNullOrEmpty(model.ProcessTypeIdList))
            {
                var processType_ = model.ProcessTypeIdList.Split(',');
                model.ProcessTypeIdList = "";
                for (var i = 0; i < processType_.Length; i++)
                {
                    model.ProcessTypeIdList += "'" + processType_[i] + "'" + (i + 1 < processType_.Length ? "," : "");
                }
            }
            if (!string.IsNullOrEmpty(model.VehicleModelIdList))
            {
                var vehicleModel_ = model.VehicleModelIdList.Split(',');
                model.VehicleModelIdList = "";
                for (var i = 0; i < vehicleModel_.Length; i++)
                {
                    model.VehicleModelIdList += "'" + vehicleModel_[i] + "'" + (i + 1 < vehicleModel_.Length ? "," : "");
                }
            }

            #endregion

            var v = new ChargePerCarFilterRequest(request)
            {
                Currency = model.Currency,
                DealerIdList = model.DealerIdList,
                RegionIdList = model.RegionIdList,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                CustomerIdList = model.CustomerIdList,
                ProcessTypeIdList = model.ProcessTypeIdList,
                VehicleModelIdList = model.VehicleModelIdList,
                VehicleTypeIdList = model.VehicleTypeIdList,
                VinNo = model.VinNo,
                InGuarantee = model.InGuarantee
            };

            var totalCnt = 0;
            var returnValue = reportsBo.GetChargePerCarReport(UserManager.UserInfo, v, out totalCnt).Data;

            var list = new List<ChargePerCarReport>();
            var processTypeList = CommonBL.ListProcessType(UserManager.UserInfo).Data;

            foreach (var chargePerCarReport in returnValue)
            {
                if (string.IsNullOrEmpty(chargePerCarReport.ProcessTypeCode))
                    continue;

                if (list.Any(a => a.DealerName == chargePerCarReport.DealerName && a.DealerRegionName == chargePerCarReport.DealerRegionName && a.ModelName == chargePerCarReport.ModelName && a.NameSurname == chargePerCarReport.NameSurname && a.TypeName == chargePerCarReport.TypeName && a.VinNo == chargePerCarReport.VinNo && chargePerCarReport.InGuarantee == a.InGuarantee && chargePerCarReport.IdVehicle == a.IdVehicle && chargePerCarReport.CategoryLookval == a.CategoryLookval))
                {
                    var item = list.FirstOrDefault(a => a.DealerName == chargePerCarReport.DealerName && a.DealerRegionName == chargePerCarReport.DealerRegionName && a.ModelName == chargePerCarReport.ModelName && a.NameSurname == chargePerCarReport.NameSurname && a.TypeName == chargePerCarReport.TypeName && a.VinNo == chargePerCarReport.VinNo && chargePerCarReport.InGuarantee == a.InGuarantee && chargePerCarReport.IdVehicle == a.IdVehicle && chargePerCarReport.CategoryLookval == a.CategoryLookval);


                    item.WorkOrderCount = returnValue.Where(x => x.VinNo == item.VinNo && x.CustomerId == item.CustomerId && x.CategoryLookval == item.CategoryLookval).GroupBy(x => x.WorkOrderId).Count();


                    item.WorkOrderDetailCount += chargePerCarReport.WorkOrderDetailCount;
                    var processType = item.ProcessTypes.FindIndex(f => f.Code == chargePerCarReport.ProcessTypeCode);
                    if (processType != -1)
                    {
                        item.ProcessTypes[processType].LabourPrice += chargePerCarReport.LabourPrice;
                        item.ProcessTypes[processType].PartPrice += chargePerCarReport.PartPrice;
                    }
                }
                else
                {
                    chargePerCarReport.ProcessTypes = new List<ChargePerCarProcessType>();
                    chargePerCarReport.ProcessTypes.AddRange(processTypeList.Select(s => new ChargePerCarProcessType { Name = s.Text, Code = s.Value }));

                    var processType = chargePerCarReport.ProcessTypes.FirstOrDefault(f => f.Code == chargePerCarReport.ProcessTypeCode);
                    if (processType != null)
                    {
                        processType.LabourPrice = chargePerCarReport.LabourPrice;
                        processType.PartPrice = chargePerCarReport.PartPrice;
                    }
                    chargePerCarReport.TotalAmount = chargePerCarReport.ProcessTypes.Sum(x => x.PartPrice) + chargePerCarReport.ProcessTypes.Sum(x => x.LabourPrice);
                    list.Add(chargePerCarReport);
                }
            }

            var dt = new DataTable();
            dt.Columns.Add(MessageResource.ChargePerCarReport_Region, typeof(string));
            dt.Columns.Add(MessageResource.ChargePerCarReport_Dealer, typeof(string));
            dt.Columns.Add(MessageResource.ChargePerCarReport_Customer, typeof(string));
            dt.Columns.Add(MessageResource.ChargePerCarReport_VehicleType, typeof(string));
            dt.Columns.Add(MessageResource.ChargePerCarReport_VehicleModel, typeof(string));
            dt.Columns.Add(MessageResource.ChargePerCarReport_VinNo, typeof(string));
            dt.Columns.Add(MessageResource.ChargePerCarReport_InGuarantee, typeof(string));
            dt.Columns.Add(MessageResource.ChargePerCarReport_CategoryLookval, typeof(string));
            dt.Columns.Add(MessageResource.ChargePerCarReport_ApproveDate, typeof(string));
            dt.Columns.Add(MessageResource.ChargePerCarReport_TotalAmount, typeof(string));
            dt.Columns.Add(MessageResource.ChargePerCarReport_WorkOrderCount, typeof(string));
            dt.Columns.Add(MessageResource.ChargePerCarReport_WorkOrderDetailCount, typeof(string));
            dt.Columns.Add(MessageResource.ChargePerCarReport_CarCount, typeof(string));

            foreach (var item in processTypeList)
            {
                dt.Columns.Add(item.Text + " " + MessageResource.ChargePerCarReport_Labour, typeof(string));
                dt.Columns.Add(item.Text + " " + MessageResource.ChargePerCarReport_Part, typeof(string));
            }

            foreach (var item in list)
            {
                var dtRow = dt.NewRow();
                dtRow[0] = item.DealerRegionName;
                dtRow[1] = item.DealerName;
                dtRow[2] = item.NameSurname;
                dtRow[3] = item.TypeName;
                dtRow[4] = item.ModelName;
                dtRow[5] = item.VinNo;
                dtRow[6] = item.InGuarantee;
                dtRow[7] = item.CategoryLookval;
                dtRow[8] = item.ApproveDate;
                dtRow[9] = item.TotalAmount;
                dtRow[10] = item.WorkOrderCount;
                dtRow[11] = item.WorkOrderDetailCount;
                dtRow[12] = item.CarCount;

                for (int i = 0; i < processTypeList.Count; i++)
                {
                    dtRow[processTypeList[i].Text + " " + MessageResource.ChargePerCarReport_Labour] = item.ProcessTypes[i].LabourPrice;
                    dtRow[processTypeList[i].Text + " " + MessageResource.ChargePerCarReport_Part] = item.ProcessTypes[i].PartPrice;
                }

                dt.Rows.Add(dtRow);
            }
            var filename = MessageResource.ChargePerCarReport_PageTitle_Index + ".xls";
            var tw = new System.IO.StringWriter();
            var hw = new System.Web.UI.HtmlTextWriter(tw);
            var dgGrid = new DataGrid { DataSource = dt };
            dgGrid.DataBind();
            dgGrid.RenderControl(hw);

            var rows = new List<List<Tuple<object, string>>>();

            var headerList = new List<string>();
            foreach (DataColumn item in dt.Columns)
            {
                headerList.Add(item.ColumnName);
            }

            foreach (DataRow item in dt.Rows)
            {
                var row = new List<Tuple<object, string>>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(new Tuple<object, string>(item[col.ColumnName].ToString(), col.ColumnName));
                }
                rows.Add(row);
            }

            var excelBytes = new ExcelHelper().GenerateExcel(headerList, rows, new List<FilterDto>(), reportName: filename, reportObject: dt);


            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.ChargePerCarReportIndex)]
        [HttpGet]
        public ActionResult ChargeWorkOrderDetail(int dealerId, int dealerRegionId, int customerId, int vehicleTypeId, string modelCode, string ProcessTypeIdList, DateTime? startDate, DateTime? endDate, string currency, string vinNo, int inGuarantee, DateTime? GuaranteeConfirmDate, string GuaranteeCategories)
        {
            return View("_WorkOrderDetail", new ChargeWorkOrderDetailFilterRequest
            {
                DealerId = dealerId,
                CustomerId = customerId,
                ModelCode = modelCode,
                VehicleTypeId = vehicleTypeId,
                DealerRegionId = dealerRegionId,
                GuaranteeCategories = GuaranteeCategories,
                ProcessTypeIdList = ProcessTypeIdList,
                StartDate = startDate,
                EndDate = endDate,
                Currency = currency,
                VinNo = vinNo,
                GuaranteeConfirmDate = GuaranteeConfirmDate,
                InGuarantee = inGuarantee
            });
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.ChargePerCarReportIndex)]
        public ActionResult ListChargeWorkOrderDetail([DataSourceRequest] DataSourceRequest request, ChargeWorkOrderDetailFilterRequest model)
        {
            var reportsBo = new ReportsBL();
            var v = new ChargeWorkOrderDetailFilterRequest(request)
            {
                CustomerId = model.CustomerId,
                DealerRegionId = model.DealerRegionId,
                VehicleTypeId = model.VehicleTypeId,
                ModelCode = model.ModelCode,
                DealerId = model.DealerId,
                ProcessTypeIdList = (model.ProcessTypeIdList == null || model.ProcessTypeIdList == "null") ? null : model.ProcessTypeIdList,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Currency = model.Currency,
                VinNo = model.VinNo,
                InGuarantee = model.InGuarantee,
                GuaranteeCategories = model.GuaranteeCategories,
                GuaranteeConfirmDate = model.GuaranteeConfirmDate
            };

            int totalCount = 0;
            var returnValue = reportsBo.GetWorkOrderDetailByWorkOrderParameters(v, out totalCount).Data;

            returnValue.ForEach(f => f.Price = f.PartPrice + f.LabourPrice);
            return Json(new
            {
                Data = returnValue,
                Total = totalCount
            });
        }

        #endregion

        #region PartStockActivityReportIndex

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.PartStockActivityReportIndex)]
        [HttpGet]
        public ActionResult PartStockActivityReportIndex()
        {
            var bus = new DealerBL();
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            var yearList = new List<SelectListItem>();
            for (var i = 2015; i < DateTime.Now.Year + 1; i++)
            {
                yearList.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            ViewBag.YearList = yearList;
            ViewBag.GuaranteeParameters = new List<SelectListItem>
            {
                new SelectListItem {Value = "1", Text = MessageResource.Global_Display_Yes},
                new SelectListItem {Value = "2", Text = MessageResource.Global_Display_No}
            };
            ViewBag.DealerRegionId = UserManager.UserInfo.DealerID > 0 ? bus.GetDealer(UserManager.UserInfo, UserManager.UserInfo.DealerID).Model.DealerRegionId : 0;
            ViewBag.ProcessTypeList = GetProcessTypeList();
            return View();
        }

        private List<SelectListItem> GetProcessTypeList()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = MessageResource.PartStockActivityFilterRequest_Transaction, Value = "1" });
            list.Add(new SelectListItem { Text = MessageResource.PartStockActivityFilterRequest_Guarantee, Value = "2" });
            list.Add(new SelectListItem { Text = MessageResource.PartStockActivityFilterRequest_Customer, Value = "3" });
            list.Add(new SelectListItem { Text = MessageResource.PartStockActivityFilterRequest_Stock, Value = "4" });
            return list;
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.PartStockActivityReportIndex)]
        public ActionResult ListPartStockActivityReport([DataSourceRequest] DataSourceRequest request, PartStockActivityFilterRequest model)
        {
            var reportsBo = new ReportsBL();
            var v = new PartStockActivityFilterRequest(request)
            {
                DealerIdList = model.DealerIdList,
                InGuarantee = model.InGuarantee,
                Year = model.Year,
                PartCode = model.PartCode,
                ProcessType = model.ProcessType
            };

            int totalCount = 0;
            var returnValue = reportsBo.GetPartStockActivityReport(v, out totalCount).Data;


            foreach (var partStockActivityReport in returnValue)
            {
                switch (partStockActivityReport.ProcessType)
                {
                    case 1:
                        partStockActivityReport.ProcessTypeName = MessageResource.PartStockActivityFilterRequest_Transaction;
                        break;
                    case 2:
                        partStockActivityReport.ProcessTypeName = MessageResource.PartStockActivityFilterRequest_Guarantee;
                        break;
                    case 3:
                        partStockActivityReport.ProcessTypeName = MessageResource.PartStockActivityFilterRequest_Customer;
                        break;
                    case 4:
                        partStockActivityReport.ProcessTypeName = MessageResource.PartStockActivityFilterRequest_Stock;
                        break;
                    default:
                        break;
                }

            }
            return Json(new
            {
                Data = returnValue,
                Total = totalCount
            });
        }

        #endregion

        #region SparePartControlReportIndex

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.SparePartControlReportIndex)]
        [HttpGet]
        public ActionResult SparePartControlReportIndex()
        {
            var bus = new DealerBL();
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.DealerRegionList = bus.ListDealerRegions().Data;
            ViewBag.StokcTypeList = CommonBL.ListStockTypes(UserManager.UserInfo).Data;
            ViewBag.CurrencyCodeList = CommonBL.ListCurrencies(UserManager.UserInfo).Data;
            ViewBag.DealerRegionId = UserManager.UserInfo.DealerID > 0 ? bus.GetDealer(UserManager.UserInfo, UserManager.UserInfo.DealerID).Model.DealerRegionId : 0;
            ViewBag.YesNoList = CommonBL.ListYesNoValueIntWithAll(2).Data;
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.SparePartControlReportIndex)]
        public ActionResult ListSparePartControlReport([DataSourceRequest] DataSourceRequest request, PartStockFilterRequest model)
        {
            var reportsBo = new ReportsBL();
            var v = new PartStockFilterRequest(request)
            {
                DealerIdList = model.DealerIdList,
                RegionIdList = model.RegionIdList,
                IsOriginal = model.IsOriginal,
                PartCode = model.PartCode,
                PartName = model.PartName
            };

            var totalCnt = 0;
            var returnValue = reportsBo.GetSparePartControlReport(UserManager.UserInfo, v, out totalCnt).Data;
            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region WorkOrderProcessTypesTotalReport

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.WorkOrderProcessTypesTotalReportIndex)]
        [HttpGet]
        public ActionResult WorkOrderProcessTypesTotalReportIndex()
        {
            var bus = new DealerBL();
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.DealerRegionList = bus.ListDealerRegions().Data;
            ViewBag.StokcTypeList = CommonBL.ListStockTypes(UserManager.UserInfo).Data;
            ViewBag.CurrencyCodeList = CommonBL.ListCurrencies(UserManager.UserInfo).Data;
            ViewBag.DealerRegionId = UserManager.UserInfo.DealerID > 0 ? bus.GetDealer(UserManager.UserInfo, UserManager.UserInfo.DealerID).Model.DealerRegionId : 0;
            ViewBag.VehicleTypeList = VehicleTypeBL.ListVehicleTypeAsSelectList(UserManager.UserInfo, "").Data;
            ViewBag.VehicleModelList = VehicleModelBL.ListVehicleModelAsSelectList(UserManager.UserInfo).Data;
            ViewBag.ProcessTypes = CommonBL.ListProcessType(UserManager.UserInfo).Data;

            ViewBag.GuaranteeParameters = new List<SelectListItem>
            {
                new SelectListItem {Value = "1", Text = MessageResource.Global_Display_Yes},
                new SelectListItem {Value = "2", Text = MessageResource.Global_Display_No}
            };

            ViewBag.GroupTypeList = new List<SelectListItem>
            {
                new SelectListItem {Value = "1", Text = "Servis"},
                new SelectListItem {Value = "2", Text = "Model"},
                new SelectListItem {Value = "3", Text = "Araç Tipi"},
                new SelectListItem {Value = "4", Text = "Bölge"}
            };

            ViewBag.WorkOrderStatusList = WorkOrderBL.ListWorkOrderStatus(UserManager.UserInfo).Data;

            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.WorkOrderProcessTypesTotalReportIndex)]
        public ActionResult ListWorkOrderProcessTypesTotalReport([DataSourceRequest] DataSourceRequest request, WorkOrderProcessTypesFilterRequest model)
        {
            var reportsBo = new ReportsBL();
            var v = new WorkOrderProcessTypesFilterRequest(request)
            {
                CurrencyCode = model.CurrencyCode,
                DealerIdList = model.DealerIdList,
                RegionIdList = model.RegionIdList,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                VehicleType = model.VehicleType,
                VehicleModel = model.VehicleModel,
                GroupId = model.GroupId,
                WorkOrderStatus = model.WorkOrderStatus,
                InGuarantee = model.InGuarantee
            };

            //var totalCnt = 0;
            var returnValue = reportsBo.GetWorkOrderProcessTypesTotalReport(UserManager.UserInfo, v);
            return Json(new
            {
                Data = returnValue.Items,
                Total = returnValue.Total
            });
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.WorkOrderProcessTypesTotalReportIndex)]
        public ActionResult ListWorkOrderProcessTypesTotalReportForExcel([DataSourceRequest] DataSourceRequest request, WorkOrderProcessTypesFilterRequest model)
        {
            var reportsBo = new ReportsBL();
            var v = new WorkOrderProcessTypesFilterRequest(request)
            {
                CurrencyCode = model.CurrencyCode,
                DealerIdList = model.DealerIdList,
                RegionIdList = model.RegionIdList,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                VehicleType = model.VehicleType,
                VehicleModel = model.VehicleModel,
                GroupId = model.GroupId,
                WorkOrderStatus = model.WorkOrderStatus
            };

            //var totalCnt = 0;
            var returnValue = reportsBo.GetWorkOrderProcessTypesTotalReport(UserManager.UserInfo, v);
            var processTypeList = CommonBL.ListProcessType(UserManager.UserInfo).Data;

            var dt = new DataTable();
            dt.Columns.Add(MessageResource.WorkOrderProcessTypes_Group, typeof(string));
            dt.Columns.Add(MessageResource.WorkOrderProcessTypes_TotalCarCount, typeof(string));
            dt.Columns.Add(MessageResource.WorkOrderProcessTypes_TotalPrice, typeof(string));
            dt.Columns.Add(MessageResource.WorkOrderProcessTypes_Currency, typeof(string));
            dt.Columns.Add(MessageResource.WorkOrderProcessTypes_TotalWorkOrderCount, typeof(string));
            foreach (var item in processTypeList)
            {
                dt.Columns.Add(item.Text + " " + MessageResource.ChargePerCarReport_Price, typeof(string));
                dt.Columns.Add(item.Text + " " + MessageResource.Global_Display_Quantity, typeof(string));
                dt.Columns.Add(item.Text + " " + MessageResource.WorkOrderCard_Display_Percentage, typeof(string));
            }

            foreach (var item in returnValue.Items)
            {
                var dtRow = dt.NewRow();
                dtRow[0] = item.GroupName;
                dtRow[1] = item.TotalCarCount;
                dtRow[2] = item.TotalPrice;
                dtRow[3] = item.CurrencyCode;
                dtRow[4] = item.TotalWorkOrderCount;
                for (int i = 0; i < processTypeList.Count; i++)
                {
                    dtRow[processTypeList[i].Text + " " + MessageResource.ChargePerCarReport_Price] = item.ProcessTypes[i].Price;
                    dtRow[processTypeList[i].Text + " " + MessageResource.Global_Display_Quantity] = item.ProcessTypes[i].Count;
                    dtRow[processTypeList[i].Text + " " + MessageResource.WorkOrderCard_Display_Percentage] = item.ProcessTypes[i].Percent;
                }
                dt.Rows.Add(dtRow);
            }

            var filename = MessageResource.WorkOrderProcessTypesTotalReport_PageTitle_Index + ".xls";
            var tw = new System.IO.StringWriter();
            var hw = new System.Web.UI.HtmlTextWriter(tw);
            var dgGrid = new DataGrid { DataSource = dt };
            dgGrid.DataBind();
            dgGrid.RenderControl(hw);

            var rows = new List<List<Tuple<object, string>>>();

            var headerList = new List<string>();
            foreach (DataColumn item in dt.Columns)
            {
                headerList.Add(item.ColumnName);
            }

            foreach (DataRow item in dt.Rows)
            {
                var row = new List<Tuple<object, string>>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(new Tuple<object, string>(item[col.ColumnName].ToString(), col.ColumnName));
                }
                rows.Add(row);
            }

            var excelBytes = new ExcelHelper().GenerateExcel(headerList, rows, new List<FilterDto>(), reportName: filename, reportObject: dt);


            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
        }

        #endregion

        #region FixAssetInventoryReport

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.FixAssetInventoryReportIndex)]
        [HttpGet]
        public ActionResult FixAssetInventoryReportIndex()
        {
            var bus = new DealerBL();
            ViewBag.EquipmentTypeList = FixAssetInventoryBL.ListEquipmentTypeAsSelectList(UserManager.UserInfo).Data;
            ViewBag.VehicleGroupList = VehicleGroupBL.ListVehicleGroupAsSelectList(UserManager.UserInfo).Data;
            ViewBag.StatusList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.FixAssetStatus).Data;
            ViewBag.YesNoList = CommonBL.ListYesNo().Data;
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.DealerRegionList = bus.ListDealerRegions().Data;
            ViewBag.DealerRegionId = UserManager.UserInfo.DealerID > 0 ? bus.GetDealer(UserManager.UserInfo, UserManager.UserInfo.DealerID).Model.DealerRegionId : 0;

            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.FixAssetInventoryReportIndex)]
        public ActionResult ListFixAssetInventoryReport([DataSourceRequest] DataSourceRequest request, FixAssetInventoryReportFilterRequest model)
        {
            var reportsBo = new ReportsBL();
            var v = new FixAssetInventoryReportFilterRequest(request)
            {
                DealerIdList = model.DealerIdList,
                RegionIdList = model.RegionIdList,
                EquipmentTypeId = model.EquipmentTypeId,
                StatusId = model.StatusId,
                VehicleGroupId = model.VehicleGroupId,
                PartCode = model.PartCode,
                PartName = model.PartName
            };

            var totalCnt = 0;
            var returnValue = reportsBo.GetFixAssetInventoryReport(UserManager.UserInfo, v, out totalCnt).Data;
            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region WorkOrderPartsTotalReport

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.WorkOrderPartsTotalReportIndex)]
        [HttpGet]
        public ActionResult WorkOrderPartsTotalReportIndex()
        {
            var bus = new DealerBL();
            ViewBag.YesNoList = CommonBL.ListYesNo().Data;
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.DealerRegionList = bus.ListDealerRegions().Data;
            var x = bus.GetDealer(UserManager.UserInfo, UserManager.UserInfo.DealerID).Model;
            ViewBag.DealerRegionId = UserManager.UserInfo.DealerID > 0 ? bus.GetDealer(UserManager.UserInfo, UserManager.UserInfo.DealerID).Model.DealerRegionId : 0;

            ViewBag.GuaranteeParameters = new List<SelectListItem>
            {
                new SelectListItem {Value = "1", Text = MessageResource.Global_Display_Yes},
                new SelectListItem {Value = "2", Text = MessageResource.Global_Display_No}
            };

            ViewBag.CurrencyCodeList = CommonBL.ListCurrencies(UserManager.UserInfo).Data;
            ViewBag.IndicatorTypeList = new AppointmentIndicatorSubCategoryBL().ListOfIndicatorTypeCodeAsSelectListItem(UserManager.UserInfo).Data;
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.WorkOrderPartsTotalReportIndex)]
        public ActionResult ListWorkOrderPartsTotalReport([DataSourceRequest] DataSourceRequest request, WorkOrderPartsTotalReportFilterRequest model)
        {
            var reportsBo = new ReportsBL();
            var v = new WorkOrderPartsTotalReportFilterRequest(request)
            {
                DealerIdList = model.DealerIdList,
                RegionIdList = model.RegionIdList,
                Currency = model.Currency,
                IndicatorType = model.IndicatorType,
                InGuarantee = model.InGuarantee,
                IsOriginal = model.IsOriginal,
                IsPaid = model.IsPaid,
                Month = model.Month,
                Year = model.Year,
                PartCode = model.PartCode
            };

            var totalCnt = 0;
            var returnValue = reportsBo.GetWorkOrderPartsTotalReport(UserManager.UserInfo, v, out totalCnt).Data;
            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region InvoiceInfoReport 
        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.InvoiceInfoReportIndex)]
        [HttpGet]
        public ActionResult InvoiceInfoReportIndex()
        {
            var bus = new DealerBL();
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.DealerRegionList = bus.ListDealerRegions().Data;
            ViewBag.DealerRegionId = UserManager.UserInfo.DealerID > 0 ? bus.GetDealer(UserManager.UserInfo, UserManager.UserInfo.DealerID).Model.DealerRegionId : 0;
            ViewBag.InvoiceTypeList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.InvoiceType).Data;
            ViewBag.CustomerTypeList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.CustomerTypeLookup).Data;
            ViewBag.GovermentTypeList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.GoverntmentTypeLookup).Data;
            ViewBag.CompanyTypeList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.CompanyTypeLookup).Data;
            ViewBag.VatTypeList = VatRatioBL.ListLabelsAsSelectList().Data;
            ViewBag.LabourList = LabourBL.ListLabourAsSelectList(UserManager.UserInfo).Data;
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.InvoiceInfoReportIndex)]
        public ActionResult ListInvoiceInfoReport([DataSourceRequest] DataSourceRequest request, InvoiceInfoFilterRequest model)
        {
            var reportsBo = new ReportsBL();
            var v = new InvoiceInfoFilterRequest(request)
            {
                DealerIdList = model.DealerIdList,
                RegionIdList = model.RegionIdList,
                InvoiceCreateBeginDate = model.InvoiceCreateBeginDate,
                InvoiceCreateEndDate = model.InvoiceCreateEndDate,
                InvoiceType = model.InvoiceType,
                InvoiceNo = model.InvoiceNo,
                CustomerNameList = model.CustomerNameList,
                CustomerType = model.CustomerType,
                GovermentType = model.GovermentType,
                CompanyType = model.CompanyType,
                HasWithold = model.HasWithold,
                ShowInvoiceDetails = model.ShowInvoiceDetails,
                VatType = model.VatType,
                PartCode = model.PartCode,
                LabourIdList = model.LabourIdList
            };

            int totalCount = 0;
            var returnValue = reportsBo.ListInvoiceInfoReport(UserManager.UserInfo, v, out totalCount).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCount
            });
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.InvoiceInfoReportIndex)]
        public ActionResult ListInvoiceNo(string dealerId)
        {
            string invoiceNo = HttpContext.Request.Form["filter[filters][0][value]"];
            return string.IsNullOrEmpty(invoiceNo)
                ? null
                : Json(new ReportsBL().ListInvoiceNoForAutoComplete(invoiceNo, dealerId).Data);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.InvoiceInfoReportIndex)]
        public ActionResult ListCustomers(string dealerId)
        {
            string custName = HttpContext.Request.Form["filter[filters][0][value]"];
            if (string.IsNullOrEmpty(dealerId)) dealerId = UserManager.UserInfo.GetUserDealerId().ToString();
            return string.IsNullOrEmpty(custName)
                ? null
                : Json(new ReportsBL().ListDealerCustomersForAutoComplete(custName, dealerId).Data);
        }
        #endregion

        #region PersonnelInfoReport 
        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.PersonnelInfoReportIndex)]
        [HttpGet]
        public ActionResult PersonnelInfoReportIndex()
        {
            var bus = new DealerBL();
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.EducationCodeList = new EducationRequestBL().GetEducationList(UserManager.UserInfo).Data;
            ViewBag.TitleList = RoleBL.ListRoleTypeAsSelectListItem(UserManager.UserInfo, false).Data;
            ViewBag.VehicleModelList = VehicleModelBL.ListVehicleModelAsSelectList(UserManager.UserInfo).Data;
            ViewBag.EducationTypeList = EducationBL.ListEducationTypeAsSelectList(UserManager.UserInfo).Data;
            return View();
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.PersonnelInfoReportIndex)]
        public ActionResult ListIdentityNo()
        {
            string identityNo = HttpContext.Request.Form["filter[filters][0][value]"];
            return string.IsNullOrEmpty(identityNo)
                ? null
                : Json(new ReportsBL().ListIdentityNoForAutoComplete(identityNo).Data);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.PersonnelInfoReportIndex)]
        public ActionResult ListUserCode()
        {
            string userNo = HttpContext.Request.Form["filter[filters][0][value]"];
            return string.IsNullOrEmpty(userNo)
                ? null
                : Json(new ReportsBL().ListUserCodeForAutoComplete(userNo).Data);
        }


        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.PersonnelInfoReportIndex)]
        public ActionResult ListUserName()
        {
            string userName = HttpContext.Request.Form["filter[filters][0][value]"];
            return string.IsNullOrEmpty(userName)
                ? null
                : Json(new ReportsBL().ListUserNameForAutoComplete(userName).Data);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.PersonnelInfoReportIndex)]
        public ActionResult ListPersonnelInfoReport([DataSourceRequest] DataSourceRequest request, PersonnelInfoReportFilterRequest model)
        {
            var reportsBo = new ReportsBL();
            var v = new PersonnelInfoReportFilterRequest(request)
            {
                DealerIdList = model.DealerIdList,
                EducationBeginDate = model.EducationBeginDate,
                EducationCode = model.EducationCode,
                EducationEndDate = model.EducationEndDate,
                EducationName = model.EducationName,
                EducationType = model.EducationType,
                IsActive = model.IsActive,
                ShowEducationDetails = model.ShowEducationDetails,
                UserCodeList = model.UserCodeList,
                VehicleModel = model.VehicleModel,
                IdentityNoList = model.IdentityNoList,
                TitleList = model.TitleList,


            };

            int totalCount = 0;
            var returnValue = reportsBo.ListPersonnelInfoReport(UserManager.UserInfo, v, out totalCount).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCount
            });
        }
        #endregion

        #region SSH Report

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.SshReportIndex)]
        [HttpGet]
        public ActionResult SshReportIndex()
        {
            var bus = new DealerBL();
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.VehicleModelList = VehicleModelBL.ListVehicleModelAsSelectList(UserManager.UserInfo).Data;
            ViewBag.DefectIdList = DefectBL.GetDefectComboList(null).Data;
            ViewBag.ContractIdList = ContractBL.ListContractAsSelectListItem().Data;
            ViewBag.PoStatusList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.PurchaseOrderStatus).Data;
            return View();
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.SshReportIndex)]
        public ActionResult ListDefectNo()
        {
            string defectNo = HttpContext.Request.Form["filter[filters][0][value]"];
            return string.IsNullOrEmpty(defectNo)
                ? null
                : Json(new ReportsBL().ListDefectNoForAutoComplete(defectNo).Data);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.SshReportIndex)]
        public ActionResult ListContractName()
        {
            string contractName = HttpContext.Request.Form["filter[filters][0][value]"];
            return string.IsNullOrEmpty(contractName)
                ? null
                : Json(new ReportsBL().ListContractNameForAutoComplete(contractName).Data);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.SshReportIndex)]
        public ActionResult ListSSHReport([DataSourceRequest] DataSourceRequest request, SshReportFilterRequest model)
        {
            var reportsBo = new ReportsBL();
            var v = new SshReportFilterRequest(request)
            {
                DealerIdList = model.DealerIdList,
                ContractIdList = model.ContractIdList,
                DefectIdList = model.DefectIdList,
                ContractStartDate = model.ContractStartDate,
                ContractEndDate = model.ContractEndDate,
                IsActive = model.IsActive,
                VehicleVinNo = model.VehicleVinNo,
                PoStatus = model.PoStatus
            };

            int totalCount = 0;
            var returnValue = reportsBo.ListSshReport(UserManager.UserInfo, v, out totalCount).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCount
            });
        }
        #endregion

        #region GuaranteeReport
        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.GuaranteeReportIndex)]
        [HttpGet]
        public ActionResult GuaranteeReportIndex()
        {

            var bus = new DealerBL();
            ViewBag.DealerRegionList = bus.ListDealerRegions().Data;
            ViewBag.IndicatorTypeList = new AppointmentIndicatorSubCategoryBL().ListOfIndicatorTypeCodeAsSelectListItem(UserManager.UserInfo).Data;
            ViewBag.GifCostCenterList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.GIFCategory).Data;
            ViewBag.ProcessTypeCodeList = CommonBL.ListProcessType(UserManager.UserInfo).Data;
            ViewBag.ConfirmedUserList = CommonBL.ListConfirmedUser().Data;
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            return View();
        }
        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.GuaranteeReportIndex)]
        [HttpPost]
        public ActionResult ListGuaranteeReport([DataSourceRequest] DataSourceRequest request, GuaranteeReportFilterRequest model)
        {
            var bo = new ReportsBL();
            var v = new GuaranteeReportFilterRequest(request)
            {
                DealerIdList = model.DealerIdList,
                RegionIdList = model.RegionIdList,
                VinNo = model.VinNo,
                Year = model.Year,
                Month = model.Month,
                IndicatorType = model.IndicatorType,
                ProcessType = model.ProcessType,
                GuaranteeCategory = model.GuaranteeCategory,
                ConfirmedUser = model.ConfirmedUser
            };

            int totalCount = 0;
            var returnValue = bo.GetGuaranteeReport(v, out totalCount).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCount
            });
        }
        #endregion

        #region WorkOrderPerformanceReport
        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.WorkOrderPerformanceReportIndex)]
        [HttpGet]
        public ActionResult WorkOrderPerformanceReportIndex()
        {
            var bus = new DealerBL();
            ViewBag.DealerRegionList = bus.ListDealerRegions().Data;
            ViewBag.IndicatorTypeList = new AppointmentIndicatorSubCategoryBL().ListOfIndicatorTypeCodeAsSelectListItem(UserManager.UserInfo).Data;
            ViewBag.ProcessTypeCodeList = CommonBL.ListProcessType(UserManager.UserInfo).Data;
            ViewBag.WarrantyStatusList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.WarrantyStatus).Data;
            ViewBag.ConfirmedUserList = CommonBL.ListConfirmedUser().Data;
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;

            return View();
        }
        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.WorkOrderPerformanceReportIndex)]
        [HttpPost]
        public ActionResult ListWorkOrderPerformanceReportIndex([DataSourceRequest] DataSourceRequest request, WorkOrderPerformanceReportFilterRequest model)
        {
            var bo = new ReportsBL();
            var v = new WorkOrderPerformanceReportFilterRequest(request)
            {
                DealerIdList = model.DealerIdList,
                RegionIdList = model.RegionIdList,
                VinNo = model.VinNo,
                Year = model.Year,
                Month = model.Month,
                IndicatorType = model.IndicatorType,
                ProcessType = model.ProcessType,
                GifNo = model.GifNo,
                WorkOrderNo = model.WorkOrderNo,
                ConfirmStatus = model.ConfirmStatus,
                SendStatus = model.SendStatus,
                User = model.User,
                VehicleLeaveDate = model.VehicleLeaveDate
            };

            int totalCount = 0;
            var returnValue = bo.GetWorkOrderPreformanceReport(v, out totalCount).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCount
            });
        }

        #endregion

        #region Excel rapor çıktı alma

        /// <summary>
        /// Excel rapor çıktı alma talebi oluşturma
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddExcelRequest(int reportType, string columns)
        {
            var model = new ReportCreateModel();
            model.CommandType = "I";
            model.ReportType = reportType;
            model.Columns = columns;
            model.CreateUser = UserManager.UserInfo.UserName;
            //model.CreateUserDealerId = UserManager.UserInfo.GetUserDealerId();
            model.CreateUserDealerId = UserManager.UserInfo.DealerID;


            model.CreateUserId = UserManager.UserInfo.UserId;
            model.CreateUserLanguageCode = UserManager.UserInfo.LanguageCode;

            foreach (var item in Request.Params.AllKeys)
            {
                if (!string.IsNullOrEmpty(Request.Params[item]))
                    model.ParameterList.Add(new ReportCreateParameterModel() { Name = item, Value = Request.Params[item] });
            }
            _reportCreateBL.DMLReportCreate(model);

            return Json(new
            {
                Success = true,
                RequestId = model.Id
            });
        }

        /// <summary>
        /// Excel rapor çıktı alma talebi durumunu verir
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetExcelStatus(int requestId)
        {
            var filter = new ReportCreateModel();
            filter.Id = requestId;
            filter.IsComplete = true;
            var total = 0;
            var response = _reportCreateBL.GetAllReportCreate(filter, out total).Data;

            if (response.Count > 0)
            {
                var re = response.First();
                return Json(new
                {
                    RequestId = requestId,
                    Complete = (re.IsComplete ?? false),
                    Success = (re.IsSuccess ?? false),
                    File = re.FilePath,
                    Message = (!re.IsComplete.HasValue) ? "Excel oluşturuluyor bekleyiniz..." : re.ErrorMessage
                });
            }

            return Json(new
            {
                RequestId = 0,
                Complete = false,
                Success = false,
                File = string.Empty,
                Message = "Talep bulunamadı!"
            });
        }

        #endregion
        
        #region Filo Period Report

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.FleetPeriodReportIndex)]
        public ActionResult FleetPeriodReportIndex()
        {
            var model = new FleetPeriodReport();
            return View(model);
        }


        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.FleetPeriodReportIndex)]
        [HttpPost]
        public ActionResult ListFleetPeriodReport([DataSourceRequest] DataSourceRequest request, FleetPeriodReportFilterRequest model)
        {
            var reportsBo = new ReportsBL();
            var filter = new FleetPeriodReportFilterRequest(request)
            {
                Period = model.Period,
                FleetCode = model.FleetCode,
                FleetName = model.FleetName,
                WorkOrderCustomerName = model.WorkOrderCustomerName,
                WorkOrderCustomerVehicleVinNo = model.WorkOrderCustomerVehicleVinNo,
                WorkOrderNo = model.WorkOrderNo,
                WorkOrderOpenDate = model.WorkOrderOpenDate,
                WorkOrderCloseDate = model.WorkOrderCloseDate,                
            };
            var totalCnt = 0;
            var returnValue = reportsBo.ListFleetPeriodReport(UserManager.UserInfo, filter, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }


        #endregion

        #region Alternate Part Report

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.AlternatePartReportIndex)]
        [HttpGet]
        public ActionResult AlternatePartReportIndex()
        {
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Reports.AlternatePartReportIndex)]
        public ActionResult ListAlternatePartReport([DataSourceRequest] DataSourceRequest request, AlternatePartReportFilterRequest model)
        {
            var reportsBo = new ReportsBL();
            var v = new AlternatePartReportFilterRequest(request)
            {
                PartCode = model.PartCode,
                PartName = model.PartName,
                AlternatePartCode = model.AlternatePartCode,
                AlternatePartName = model.AlternatePartName
            };

            int totalCount = 0;
            var returnValue = reportsBo.GetAlternatePartReport(UserManager.UserInfo, v, out totalCount).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCount
            });
        }
        #endregion
    }
}

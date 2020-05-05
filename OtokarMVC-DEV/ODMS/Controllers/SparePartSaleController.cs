using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using System.Linq;
using ODMSBusiness.Reports;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.Dealer;
using ODMSModel.FleetVehicle;
using ODMSModel.PaymentType;
using ODMSModel.PurchaseOrder;
using ODMSModel.PurchaseOrderDetail;
using ODMSModel.SparePartSale;
using ODMSModel.SparePartSaleDetail;
using ODMSModel.Vehicle;
using ODMSModel.WorkOrderPicking;
using Permission = ODMSCommon.CommonValues.PermissionCodes.SparePartSale;
using ODMSModel.Customer;
using System.Transactions;
using ODMSModel.WorkOrderPickingDetail;
using ODMSBusiness.Business;
using ODMSModel.SparePartSaleWaybill;
using ODMSModel.SparePartSaleInvoice;
using ODMSModel.CustomerAddress;
using ODMSModel.SparePartSaleOrderDetail;
using Kendo.Mvc.Extensions;
using ODMSModel.Rack;
using ODMSModel.Common;
using ODMSModel.SparePartSaleOrder;
using static ODMSCommon.CommonValues.PermissionCodes;
using static ODMSCommon.CommonValues;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class SparePartSaleController : ControllerBase
    {
        #region Private Methods

        [HttpPost]
        public JsonResult GetVehicleExemption(int vehicleId)
        {
            var vehicle = new VehicleBL().GetVehicle(UserManager.UserInfo, new VehicleIndexViewModel() { VehicleId = vehicleId }).Model;
            return vehicle != null ?
                Json(vehicle.VatExcludeType, JsonRequestBehavior.AllowGet) :
                Json(null, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ListCustomerAddresses(int customerId = 0)
        {
            return
                Json(customerId == 0
                    ? new List<SelectListItem>()
                    : new SparePartSaleBL().ListCustomerAddresses(UserManager.UserInfo, customerId));
        }

        [HttpPost]
        public JsonResult ListCustomerInvoiceAddresses(int customerId = 0)
        {
            return
                Json(customerId == 0
                    ? new List<SelectListItem>()
                    : new SparePartSaleBL().ListCustomerAddresses(UserManager.UserInfo, customerId, true));
        }

        [ValidateAntiForgeryToken]
        public JsonResult GetVehicleFleet(string vehicleId, string customerId)
        {
            if (string.IsNullOrEmpty(vehicleId))
            {
                return null;
            }
            FleetVehicleBL fvBo = new FleetVehicleBL();
            bool isFleetVehicle = fvBo.IsFleetVehicle(vehicleId.GetValue<int>(), customerId.GetValue<int>()).Model;
            return Json(new { IsFleetVehicle = isFleetVehicle });
        }

        private void FillComboBoxes()
        {
            var bus = new SparePartSaleBL();
            ViewBag.CustomerTypeList = bus.ListCustomerTypes(UserManager.UserInfo).Data;
            ViewBag.PaymentTypeList = bus.ListPaymentTypes(UserManager.UserInfo).Data;
            ViewBag.BankList = bus.ListBanks().Data;
            ViewBag.YesNoList = CommonBL.ListYesNo().Data;
            ViewBag.SaleStatusList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.SaleStatusLookup).Data;
            List<SelectListItem> stockTypeList = CommonBL.ListStockTypes(UserManager.UserInfo).Data;
            ViewBag.StockTypeList = stockTypeList;
            ViewBag.SaleTypeList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.SaleTypeLookup).Data;
            ViewBag.TenderSaleStatusList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.TenderSaleStatus).Data;

            List<SelectListItem> vatExcludeTypeList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.YesNo).Data;
            ViewBag.VatExcludeTypeList = vatExcludeTypeList;

        }

        #endregion

        #region Spare Part Sale Index

        [HttpGet]
        [AuthorizationFilter(Permission.SparePartSaleIndex)]
        public ActionResult SparePartSaleIndex(int? sparePartSaleId)
        {
            FillComboBoxes();
            var model = new SparePartSaleViewModel();
            if (sparePartSaleId != null)
            {
                model.SparePartSaleId = sparePartSaleId.GetValue<int>();
            }
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSale.SparePartSaleIndex, CommonValues.PermissionCodes.SparePartSale.SparePartSaleDetails)]
        public ActionResult ListSparePartSales([DataSourceRequest] DataSourceRequest request, SparePartSaleViewModel model)
        {
            var bo = new SparePartSaleBL();

            var referenceModel = new SparePartSaleListModel(request)
            {
                CustomerId = model.CustomerId,
                InvoiceDate = model.InvoiceDate,
                WaybillDate = model.WaybillDate,
                PartCode = model.PartCode,
                PartName = model.PartName,
                SaleStatusLookVal = model.SaleStatusLookVal,
                SparePartSaleId = model.SparePartSaleId
            };

            if (model.InvoiceNo.HasValue())
            {
                referenceModel.InvoiceSerialNo = (from c in model.InvoiceNo.ToCharArray()
                                                  where Char.IsDigit(c)
                                                  select c).ToString();
                referenceModel.InvoiceNo = (from c in model.InvoiceNo.ToCharArray()
                                            where !Char.IsDigit(c)
                                            select c).ToString();
            }
            if (model.WaybillNo.HasValue())
            {
                referenceModel.WaybillSerialNo = (from c in model.WaybillNo.ToCharArray()
                                                  where Char.IsDigit(c)
                                                  select c).ToString();
                referenceModel.WaybillNo = (from c in model.WaybillNo.ToCharArray()
                                            where !Char.IsDigit(c)
                                            select c).ToString();
            }

            int totalCnt;
            var returnValue = bo.ListSparePartSales(UserManager.UserInfo, referenceModel, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region Spare Part Sale Create

        [HttpGet]
        [AuthorizationFilter(Permission.SparePartSaleIndex, Permission.SparePartSaleCreate)]
        public ActionResult SparePartSaleCreate()
        {
            ViewBag.IsCreate = true;
            FillComboBoxes();
            SparePartSaleViewModel model = new SparePartSaleViewModel();
            model.SaleStatusLookVal = ((int)CommonValues.SparePartSaleStatus.NewRecord).ToString();
            model.StockTypeId = CommonBL.GetGeneralParameterValue(CommonValues.GeneralParameters.StockType).Model.GetValue<int>();
            //model.PaymentAmount = 0;
            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.SparePartSaleIndex, Permission.SparePartSaleCreate)]
        public ActionResult SparePartSaleCreate(SparePartSaleViewModel model)
        {
            ViewBag.IsCreate = true;
            FillComboBoxes();
            if (ModelState.IsValid)
            {
                CustomerIndexViewModel customerModel = new CustomerIndexViewModel();
                customerModel.CustomerId = model.CustomerId;
                CustomerBL customerBo = new CustomerBL();
                customerBo.GetCustomer(UserManager.UserInfo, customerModel);
                model.CustomerTypeId = customerModel.CustomerTypeId;
                DealerBL dealerBo = new DealerBL();
                DealerViewModel dModel = dealerBo.GetDealer(UserManager.UserInfo, UserManager.UserInfo.GetUserDealerId()).Model;
                var bo = new SparePartSaleBL();

                if (model.VehicleId != null)
                {
                    VehicleIndexViewModel vModel = new VehicleIndexViewModel();
                    vModel.VehicleId = model.VehicleId.GetValue<int>();
                    VehicleBL vBo = new VehicleBL();
                    vBo.GetVehicle(UserManager.UserInfo, vModel);

                    //model.VatExclude = vModel.VatExcludeType; // bu bilgi artık formdan geliyor
                    model.PriceListId = vModel.IdPriceList;
                }
                else
                {
                    //model.VatExclude = 0; // bu bilgi artık formdan geliyor
                    model.PriceListId = dealerBo.GetCountryDefaultPriceList(dModel.Country).Model;
                }
                

                model.CommandType = CommonValues.DMLType.Insert;
                model.IsReturn = false;
                model.SaleStatusLookVal = ((int)CommonValues.SparePartSaleStatus.NewRecord).ToString();
                bo.DMLSparePartSale(UserManager.UserInfo, model);
                CheckErrorForMessage(model, true);
                ModelState.Clear();
            }
            SparePartSaleViewModel nModel = new SparePartSaleViewModel();
            nModel.SaleStatusLookVal = ((int)CommonValues.SparePartSaleStatus.NewRecord).ToString();
            nModel.SparePartSaleId = model.SparePartSaleId;
            return View(nModel);
        }

        #endregion

        #region Spare Part Sale Update

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSale.SparePartSaleIndex, CommonValues.PermissionCodes.SparePartSale.SparePartSaleUpdate)]
        public ActionResult SparePartSaleUpdate(int id = 0)
        {
            ViewBag.IsCreate = false;
            FillComboBoxes();
            var model = new SparePartSaleViewModel { SparePartSaleId = id };
            if (id <= 0) return View(model);
            var bo = new SparePartSaleBL();
            model = bo.GetSparePartSale(UserManager.UserInfo, id).Model;

            int customerId = model.CustomerId;
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

            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSale.SparePartSaleIndex,
            CommonValues.PermissionCodes.SparePartSale.SparePartSaleUpdate)]
        public ActionResult SparePartSaleUpdate(SparePartSaleViewModel viewModel)
        {
            ViewBag.IsCreate = false;
            FillComboBoxes();
            viewModel.IsReturn = false;

            CustomerIndexViewModel customerModel = new CustomerIndexViewModel();
            customerModel.CustomerId = viewModel.CustomerId;
            CustomerBL customerBo = new CustomerBL();
            customerBo.GetCustomer(UserManager.UserInfo, customerModel);
            viewModel.CustomerTypeId = customerModel.CustomerTypeId;

            var sparePartbo = new SparePartSaleBL();
            DealerBL dealerBo = new DealerBL();
            DealerViewModel dModel = dealerBo.GetDealer(UserManager.UserInfo, UserManager.UserInfo.GetUserDealerId()).Model;

            if (viewModel.VehicleId != null)
            {
                VehicleIndexViewModel vModel = new VehicleIndexViewModel();
                vModel.VehicleId = viewModel.VehicleId.GetValue<int>();
                VehicleBL vBo = new VehicleBL();
                vBo.GetVehicle(UserManager.UserInfo, vModel);

                viewModel.VatExclude = vModel.VatExcludeType;
                viewModel.PriceListId = vModel.IdPriceList;
            }
            else
            {
                viewModel.VatExclude = 0;
                viewModel.PriceListId = dealerBo.GetCountryDefaultPriceList(dModel.Country).Model;
            }

            ModelState.Remove("InvoiceSerialNo");
            ModelState.Remove("InvoiceNo");
            ModelState.Remove("InvoiceDate");
            ModelState.Remove("PaymentTypeId");

            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Update;
                sparePartbo.DMLSparePartSale(UserManager.UserInfo, viewModel);
                SetTotalValues(viewModel);
                CheckErrorForMessage(viewModel, true);
            }
            return View(viewModel);
        }

        public static void SetTotalValues(SparePartSaleViewModel parentModel)
        {
            int totalCount = 0;
            SparePartSaleDetailListModel dModel = new SparePartSaleDetailListModel();
            dModel.PartSaleId = parentModel.SparePartSaleId;
            SparePartSaleDetailBL dBo = new SparePartSaleDetailBL();
            List<SparePartSaleDetailListModel> detailList = dBo.ListSparePartSaleDetails(UserManager.UserInfo, dModel, out totalCount).Data;
            if (totalCount != 0)
            {
                decimal totalDiscountPrice = detailList.Sum(e => e.DiscountPrice * e.PlanQuantity).GetValue<decimal>();
                decimal totalPriceWithoutVatRatio = totalDiscountPrice;
                decimal totalVatRatio = detailList.Sum(
                    e =>
                        parentModel.VatExclude == 0
                            ? (e.DiscountPrice * e.PlanQuantity * e.VatRatio) / 100
                            : 0).GetValue<decimal>();
                decimal totalPriceWithVatRatio = totalPriceWithoutVatRatio + totalVatRatio;
                //SparePartSaleBL masterBo = new SparePartSaleBL();
                //parentModel.PaymentAmount = totalPriceWithVatRatio;
                //parentModel.CommandType = CommonValues.DMLType.Update;
                //masterBo.DMLSparePartSale(parentModel);
            }
        }

        #endregion

        #region Spare Part Sale Details

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSale.SparePartSaleIndex, CommonValues.PermissionCodes.SparePartSale.SparePartSaleDetails)]
        public ActionResult SparePartSaleDetails(int id = 0)
        {
            var bo = new SparePartSaleBL();
            var model = bo.GetSparePartSale(UserManager.UserInfo, id).Model;
            CheckErrorForMessage(model, false);
            return View(model);
        }

        #endregion

        #region Spare Part Sale Collect

        [HttpPost]
        //[ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSale.SparePartSaleIndex,
            CommonValues.PermissionCodes.SparePartSale.SparePartSaleCollect)]
        public ActionResult SparePartSaleCollect(int sparePartSaleId)
        {
            using (var ts = new TransactionScope())
            {

                SparePartSaleBL spsBo = new SparePartSaleBL();

                int totalCount = 0;
                SparePartSaleDetailListModel spsdListModel = new SparePartSaleDetailListModel()
                {
                    PartSaleId = sparePartSaleId
                };
                SparePartSaleDetailBL spsdBo = new SparePartSaleDetailBL();
                List<SparePartSaleDetailListModel> list = spsdBo.ListSparePartSaleDetails(UserManager.UserInfo, spsdListModel, out totalCount).Data;
                if (totalCount == 0)
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                        MessageResource.SparePartSale_Warning_NoDetailData);
                SparePartSaleViewModel model = spsBo.GetSparePartSale(UserManager.UserInfo, sparePartSaleId).Model;

                #region Work Order Picking

                SparePartSaleCollect(model, list);
                if (model.ErrorNo != 0)
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);

                #endregion

                #region Change Status
                model.SaleStatusLookVal = ((int)CommonValues.SparePartSaleStatus.CollectOrderCreated).ToString();
                model.CommandType = CommonValues.DMLType.Update;
                spsBo.DMLSparePartSale(UserManager.UserInfo, model);
                if (model.ErrorNo != 0)
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
                foreach (SparePartSaleDetailListModel sparePartSaleDetailListModel in list)
                {
                    SparePartSaleDetailDetailModel detailModel = new SparePartSaleDetailDetailModel()
                    {
                        SparePartSaleDetailId = sparePartSaleDetailListModel.SparePartSaleDetailId
                    };
                    detailModel = spsdBo.GetSparePartSaleDetail(UserManager.UserInfo, detailModel).Model;
                    detailModel.CommandType = CommonValues.DMLType.Update;
                    detailModel.StatusId = (int)CommonValues.SparePartSaleDetailStatus.CollectOrderCreated;
                    spsdBo.DMLSparePartSaleDetail(UserManager.UserInfo, detailModel);
                    if (detailModel.ErrorNo > 0)
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Error, detailModel.ErrorMessage);
                }
                #endregion

                ts.Complete();
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            }
        }

        #endregion

        public void SparePartSaleCollect(SparePartSaleViewModel model, List<SparePartSaleDetailListModel> list)
        {
            SparePartSaleDetailBL spsdBo = new SparePartSaleDetailBL();
            var partGroupList = (from a in list
                                 where a.PlanQuantity >= a.PickQuantity + a.PickedQuantity - a.ReturnedQuantity
                                 group a by a.SparePartId
                                         into grp
                                 select new
                                 {
                                     TotalPlanQuantity = grp.Sum(f => f.PlanQuantity),
                                     TotalPickPlannedQuantity = grp.Sum(f => f.PickQuantity),
                                     TotalPickedQuantity = grp.Sum(f => f.PickedQuantity),
                                     TotalReturnedQuantity = grp.Sum(f => f.ReturnedQuantity),
                                     PartId = grp.Key
                                 });

            if (partGroupList.Any())
            {
                WorkOrderPickingBL wopBo = new WorkOrderPickingBL();
                WorkOrderPickingViewModel wopModel = new WorkOrderPickingViewModel();
                wopModel.CommandType = CommonValues.DMLType.Insert;
                wopModel.CustomerId = model.CustomerId;
                wopModel.PartSaleId = model.SparePartSaleId;
                wopModel.IsReturn = model.IsReturn.GetValue<int>();
                wopModel.StatusId = (int)CommonValues.WorkOrderPickingStatus.NewRecord;
                wopBo.DMLWorkOrderPicking(UserManager.UserInfo, wopModel);
                if (wopModel.ErrorNo > 0)
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = wopModel.ErrorMessage;
                    return;
                }

                WorkOrderPickingDetailBL wopdBo = new WorkOrderPickingDetailBL();
                foreach (var partGroupDetail in partGroupList)
                {
                    WorkOrderPickingDetailViewModel wopdModel = new WorkOrderPickingDetailViewModel();
                    wopdModel.WorkOrderPickingMstId = wopModel.WorkOrderPickingId;
                    wopdModel.CommandType = CommonValues.DMLType.Insert;
                    wopdModel.PartId = partGroupDetail.PartId;
                    wopdModel.RequiredQuantity =
                        (partGroupDetail.TotalPlanQuantity -
                         (partGroupDetail.TotalPickPlannedQuantity + partGroupDetail.TotalPickedQuantity -
                          partGroupDetail.TotalReturnedQuantity)).GetValue<decimal>();
                    wopdModel.StockTypeId = model.StockTypeId.GetValue<int>();
                    wopdBo.DMLWorkOrderPickingDetail(UserManager.UserInfo, wopdModel);
                    if (wopdModel.ErrorNo > 0)
                    {
                        model.ErrorNo = 1;
                        model.ErrorMessage = wopdModel.ErrorMessage;
                        return;
                    }

                    long sparePartSaleDetailId =
                        list.First(e => e.SparePartId == wopdModel.PartId).SparePartSaleDetailId;
                    SparePartSaleDetailDetailModel detailModel = new SparePartSaleDetailDetailModel();
                    detailModel.SparePartSaleDetailId = sparePartSaleDetailId;
                    detailModel = spsdBo.GetSparePartSaleDetail(UserManager.UserInfo, detailModel).Model;
                    detailModel.CommandType = CommonValues.DMLType.Update;
                    detailModel.PickQuantity = wopdModel.RequiredQuantity;
                    detailModel.PickedQuantity = 0;
                    detailModel.ReturnedQuantity = 0;
                    spsdBo.DMLSparePartSaleDetail(UserManager.UserInfo, detailModel);
                    if (detailModel.ErrorNo > 0)
                    {
                        model.ErrorNo = 1;
                        model.ErrorMessage = detailModel.ErrorMessage;
                        return;
                    }

                    string to = UserManager.UserInfo.Email;
                    string subject = string.Format(MessageResource.SparePartSale_MailBody_WorkOrderPickingCreated,
                        wopModel.WorkOrderPickingId);
                    string body = string.Format(MessageResource.SparePartSale_MailBody_WorkOrderPickingCreated,
                        wopModel.WorkOrderPickingId);
                    CommonBL.SendDbMail(to, subject, body);
                }
            }
        }

        #region Spare Part Sale Create Waybill

        [HttpGet]
        [AuthorizationFilter(Permission.SparePartSaleWaybill, Permission.SparePartSaleDetails)]
        public ActionResult SparePartSaleCreateWaybill(int? sparePartSaleId)
        {
            FillComboBoxes();

            SparePartSaleBL spsBo = new SparePartSaleBL();
            SparePartSaleViewModel spsModel = spsBo.GetSparePartSale(UserManager.UserInfo, sparePartSaleId.GetValue<int>()).Model;
            SparePartSaleWaybillBL spswBo = new SparePartSaleWaybillBL();
            SparePartSaleWaybillViewModel spswModel = new SparePartSaleWaybillViewModel() { InvoiceDate = DateTime.Now };


            //if (spsModel.SparePartSaleWaybillId.GetValueOrDefault(0) > 0)
            //{
            //     spswModel = spswBo.GetSparePartSaleWaybill(spsModel.SparePartSaleWaybillId.GetValue<int>());
            //          spswModel.SparePartSaleId = sparePartSaleId.GetValueOrDefault(0);
            //    if (spswModel.InvoiceNo.HasValue() && spswModel.InvoiceNo.HasValue())
            //    {
            //        spswModel.IsWaybilled = true;
            //    }
            //    else
            //    {
            //        spswModel.IsWaybilled = false;

            if (spsModel.SparePartSaleWaybillId.GetValueOrDefault(0) > 0)
            {
                spswModel = spswBo.GetSparePartSaleWaybill(UserManager.UserInfo, spsModel.SparePartSaleWaybillId.GetValue<int>()).Model;
                spswModel.SparePartSaleId = sparePartSaleId.GetValueOrDefault(0);
                SparePartSaleInvoiceBL spsiBo = new SparePartSaleInvoiceBL();
                SparePartSaleInvoiceViewModel spsiModel = spsiBo.GetSparePartSaleInvoice(UserManager.UserInfo, spswModel.SparePartSaleInvoiceId.GetValue<int>()).Model;
                spswModel.InvoiceNo = spsiModel.InvoiceNo;
                spswModel.InvoiceSerialNo = spsiModel.InvoiceSerialNo;
                spswModel.InvoiceDate = spsiModel.InvoiceDate;
                spswModel.BillingAddressId = spsiModel.BillingAddressId;
                spswModel.PaymentTypeId = spsiModel.PaymentTypeId;
                spswModel.InstalmentNumber = spsiModel.InstalmentNumber;
                spswModel.BankId = spsiModel.BankId;
                spswModel.DueDuration = spsiModel.DueDuration;
                spswModel.SaleDate = spsModel.SaleDate;


                if (spswModel.PaymentTypeId.HasValue)
                {
                    List<PaymentTypeListModel> ptList = spsBo.ListPaymentTypes(UserManager.UserInfo).Data;
                    var control = (from e in ptList.AsEnumerable()
                                   where e.Id == spswModel.PaymentTypeId
                                   select e);
                    if (control.Any())
                    {
                        PaymentTypeListModel ptModel = control.First();
                        spswModel.BankRequired = ptModel.BankRequired;
                        spswModel.InstalmentRequired = ptModel.InstalmentRequired;
                        spswModel.DefermentRequired = ptModel.DefermentRequired;
                        spswModel.WaybillDate = DateTime.Now;
                    }
                }
                if (spsiModel.InvoiceSerialNo.HasValue())
                {
                    spswModel.IsWaybilled = true;
                }
            }
            else
            {
                // kayıt yoksa
                int count = 0;
                SparePartSaleWaybillListModel spswListModel = new SparePartSaleWaybillListModel();
                spswListModel.DealerId = spsModel.DealerId;
                List<SparePartSaleWaybillListModel> spswList = spswBo.ListSparePartSaleWaybills(UserManager.UserInfo, spswListModel, out count).Data;
                if (spswList.Any())
                {
                    SparePartSaleWaybillListModel latestWaybillInfo = spswList.First(d => d.WaybillSerialNo.HasValue());

                    if (latestWaybillInfo != null)
                    {
                        spswModel.WaybillSerialNo = latestWaybillInfo.WaybillSerialNo;
                        spswModel.WaybillNo = latestWaybillInfo.WaybillNo;
                    }
                }
                // kampanya talebinden gelen siparişlerde irsaliyeli fatura seçiminin seçili gelmesi istendi OYA 04.04.2018
                spswModel.IsWaybilled = spsModel.StockTypeId == (int) CommonValues.StockType.Kampanya;
                spswModel.CustomerId = spsModel.CustomerId;
                CustomerBL cBo = new CustomerBL();
                CustomerIndexViewModel cModel = new CustomerIndexViewModel();
                cModel.CustomerId = spswModel.CustomerId;
                cBo.GetCustomer(UserManager.UserInfo, cModel);
                spswModel.CustomerName = cModel.CustomerName;
                spswModel.CustomerLastName = cModel.CustomerLastName;
                spswModel.SparePartSaleId = spsModel.SparePartSaleId;
                spswModel.SaleDate = spsModel.SaleDate;
                spswModel.WaybillDate = DateTime.Now;

            }


            return View(spswModel);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSale.SparePartSaleIndex)]
        public ActionResult ListSparePartSaleWaybill([DataSourceRequest] DataSourceRequest request, SparePartSaleViewModel model)
        {
            var list = new SparePartSaleBL().ListWayBill(UserManager.UserInfo, model.SparePartSaleId).Data;
            return Json(new { Data = list, Total = list.Count() });
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSale.SparePartSaleWaybill)]
        public ActionResult SparePartSaleCreateWaybill(SparePartSaleWaybillViewModel model)
        {
            List<SelectListItem> spsIdList = model.SparePartSaleList;

            CustomerAddressBL caBo = new CustomerAddressBL();
            var control = model.SparePartSaleList;

            if (ModelState.IsValid)
            {
                CustomerBL cBo = new CustomerBL();
                CustomerIndexViewModel cModel = new CustomerIndexViewModel();
                cModel.CustomerId = model.CustomerId;
                cBo.GetCustomer(UserManager.UserInfo, cModel);

                SparePartSaleInvoiceViewModel spsiModel = new SparePartSaleInvoiceViewModel();

                if (model.IsWaybilled)
                {
                    CustomerAddressIndexViewModel bcaModel = new CustomerAddressIndexViewModel();
                    bcaModel.AddressId = model.BillingAddressId.GetValue<int>();
                    caBo.GetCustomerAddress(UserManager.UserInfo, bcaModel);

                    // müşteri adresi yoksa uyarı veriliyor.
                    if(string.IsNullOrEmpty(bcaModel.Address1))
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.SparePartSaleWaybill_Error_MissingAdress);

                    SparePartSaleInvoiceBL spsiBo = new SparePartSaleInvoiceBL();
                    spsiModel.BankId = model.BankId;
                    spsiModel.BillingAddressId = model.BillingAddressId.GetValue<int>();
                    spsiModel.CustomeTaxNo = cModel.TaxNo;
                    spsiModel.CustomerAddress1 = bcaModel.Address1;
                    spsiModel.CustomerAddress2 = bcaModel.Address2;
                    spsiModel.CustomerAddress3 = bcaModel.Address3;
                    spsiModel.CustomerAddressCityText = bcaModel.CityName;
                    spsiModel.CustomerAddressCountryText = bcaModel.CountryName;
                    spsiModel.CustomerAddressTownText = bcaModel.TownName;
                    spsiModel.CustomerAddressZipCode = bcaModel.ZipCode;
                    spsiModel.CustomerId = model.CustomerId;
                    spsiModel.CustomerLastName = cModel.CustomerLastName;
                    spsiModel.CustomerName = cModel.CustomerName;
                    spsiModel.CustomerPassportNo = cModel.PassportNo;
                    spsiModel.CustomerTCIdentity = cModel.TcIdentityNo;
                    spsiModel.CustomerTaxOffice = cModel.TaxOffice;
                    spsiModel.DealerId = model.DealerId;
                    spsiModel.DueDuration = model.DueDuration;
                    spsiModel.InstalmentNumber = model.InstalmentNumber;
                    spsiModel.InvoiceDate = model.InvoiceDate.GetValue<DateTime>();
                    spsiModel.InvoiceNo = model.InvoiceNo;
                    spsiModel.InvoiceSerialNo = model.InvoiceSerialNo;
                    spsiModel.IsActive = true;
                    spsiModel.PayAmount = 0;
                    spsiModel.PaymentTypeId = model.PaymentTypeId;
                    spsiModel.CommandType = CommonValues.DMLType.Insert;
                    spsiBo.DMLSparePartSaleInvoice(UserManager.UserInfo, spsiModel);

                    if (spsiModel.ErrorNo > 0)
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Error, spsiModel.ErrorMessage);

                    model.WaybillNo = model.InvoiceNo;
                    model.WaybillSerialNo = model.InvoiceSerialNo;
                    model.WaybillDate = model.InvoiceDate;
                    model.ShippingAddressId = model.BillingAddressId;
                }

                CustomerAddressIndexViewModel scaModel = new CustomerAddressIndexViewModel();
                scaModel.AddressId = model.ShippingAddressId.GetValue<int>();
                caBo.GetCustomerAddress(UserManager.UserInfo, scaModel);

                SparePartSaleWaybillBL spswBo = new SparePartSaleWaybillBL();
                model.CustomeTaxNo = cModel.TaxNo;
                model.CustomerAddress1 = scaModel.Address1;
                model.CustomerAddress2 = scaModel.Address2;
                model.CustomerAddress3 = scaModel.Address3;
                model.CustomerAddressCityText = scaModel.CityName;
                model.CustomerAddressCountryText = scaModel.CountryName;
                model.CustomerAddressTownText = scaModel.TownName;
                model.CustomerAddressZipCode = scaModel.ZipCode;
                model.CustomerId = model.CustomerId;
                model.CustomerLastName = cModel.CustomerLastName;
                model.CustomerName = cModel.CustomerName;
                model.CustomerPassportNo = cModel.PassportNo;
                model.CustomerTCIdentity = cModel.TcIdentityNo;
                model.CustomerTaxOffice = cModel.TaxOffice;
                model.DealerId = model.DealerId;
                //model.WaybillNo = model.WaybillNo;
                //model.WaybillSerialNo = model.WaybillSerialNo;
                //model.WaybillDate = model.InvoiceDate;
                //model.ShippingAddressId = model.BillingAddressId;
                model.SparePartSaleIdList = string.Join(",", spsIdList.Where(d => d != null).Select(i => i.Value).ToArray());
                model.CommandType = CommonValues.DMLType.Insert;
                if (spsiModel.SparePartSaleInvoiceId != 0)
                    model.SparePartSaleInvoiceId = spsiModel.SparePartSaleInvoiceId;
                model.IsActive = true;

                string newId = string.Empty;
                int count = 1;
                // TODO: burada liste çekmek yerine var mı kontrolü yapılmalı.
                while (count != 0)
                {
                    newId = CommonBL.GetNewID().Model;
                    SparePartSaleWaybillListModel spswListModel = new SparePartSaleWaybillListModel();
                    spswListModel.WaybillReferenceNo = newId;
                    //TODO : burada ne  yapılmak istenmiş ??? kontrol edilmeli
                    spswBo.ListSparePartSaleWaybills(UserManager.UserInfo, spswListModel, out count);
                }
                model.WaybillReferenceNo = newId;

                spswBo.DMLSparePartSaleWaybill(UserManager.UserInfo, model);
                if (model.ErrorNo > 0)
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);

                int totalCount = 0;
                SparePartSaleBL spsBo = new SparePartSaleBL();
                SparePartSaleDetailBL spsdBo = new SparePartSaleDetailBL();
                SparePartSaleOrderDetailBL spsodBo = new SparePartSaleOrderDetailBL();
                foreach (SelectListItem spsIdItem in spsIdList)
                {
                    SparePartSaleDetailListModel spsdListModel = new SparePartSaleDetailListModel();
                    spsdListModel.PartSaleId = spsIdItem.Value.GetValue<int>();
                    List<SparePartSaleDetailListModel> detailList = spsdBo.ListSparePartSaleDetails(UserManager.UserInfo, spsdListModel, out totalCount).Data;
                    foreach (SparePartSaleDetailListModel detailModel in detailList)
                    {
                        if (detailModel.SoDetSeqNo.HasValue())
                        {
                            SparePartSaleOrderDetailDetailModel spsodModel = new SparePartSaleOrderDetailDetailModel();
                            spsodModel.SparePartSaleOrderDetailId = detailModel.SoDetSeqNo.GetValue<long>();
                            spsodBo.GetSparePartSaleOrderDetail(UserManager.UserInfo, spsodModel);
                            spsodModel.CommandType = CommonValues.DMLType.Update;
                            spsodModel.ShippedQuantity = spsodModel.ShippedQuantity + (detailModel.PickedQuantity - detailModel.ReturnedQuantity);
                            spsodBo.DMLSparePartSaleOrderDetail(UserManager.UserInfo, spsodModel);
                            if (spsodModel.ErrorNo > 0)
                                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, spsodModel.ErrorMessage);
                        }
                    }
                    SparePartSaleViewModel spsModel = spsBo.GetSparePartSale(UserManager.UserInfo, spsdListModel.PartSaleId.GetValue<int>()).Model;
                    spsModel.CommandType = CommonValues.DMLType.Update;
                    spsModel.SparePartSaleWaybillId = model.SparePartSaleWaybillId;
                    spsBo.DMLSparePartSale(UserManager.UserInfo, spsModel);
                    if (spsModel.ErrorNo > 0)
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Error, spsModel.ErrorMessage);

                    spsModel = spsBo.GetSparePartSale(UserManager.UserInfo, spsdListModel.PartSaleId.GetValue<int>()).Model;
                    spsModel.CommandType = CommonValues.DMLType.Update;
                    spsModel.SaleStatusLookVal = model.IsWaybilled ? ((int)CommonValues.SparePartSaleStatus.OrderInvoiced).ToString(
                        ) : ((int)CommonValues.SparePartSaleStatus.OrderWaybilled).ToString();
                    spsBo.DMLSparePartSale(UserManager.UserInfo, spsModel);
                    if (spsModel.ErrorNo > 0)
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Error, spsModel.ErrorMessage);
                }

                var sparePartbo = new SparePartSaleBL();
                string errorMessage = sparePartbo.ExecInvoiceOpMultiple(UserManager.UserInfo, model.SparePartSaleWaybillId).Model;
                if (errorMessage != null)
                {
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error, errorMessage);
                }
            }
            else
            {
                if (model.IsWaybilled)
                {
                    if (!model.InvoiceNo.HasValue())
                    {
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.SparePartSale_Warning_InvoiceNo);
                    }
                    if (model.SparePartSaleList.Count == 0)
                    {
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.SparePartSale_Warning_SparePartList);
                    }
                    if (!model.InvoiceSerialNo.HasValue())
                    {
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.SparePartSale_Warning_InvoiceSerialNo);
                    }
                    if (model.InvoiceDate == null)
                    {
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.SparePartSale_Warning_InvoiceDate);
                    }
                    if (model.BillingAddressId == null)
                    {
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.SparePartSale_Warning_BillingAddress);
                    }
                    if (model.InstalmentRequired && model.InstalmentNumber == null)
                    {
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.SparePartSale_Warning_InstalmentNumber);
                    }
                    if (model.BankRequired && model.BankId == null)
                    {
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.SparePartSale_Warning_Bank);
                    }
                    if (model.DefermentRequired && model.DueDuration == null)
                    {
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.SparePartSale_Warning_DueDuration);
                    }
                }
                else
                {
                    if (!model.WaybillNo.HasValue())
                    {
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.SparePartSale_Warning_WaybillNo);
                    }
                    if (model.SparePartSaleList.Count == 0)
                    {
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.SparePartSale_Warning_SparePartList);
                    }
                    if (!model.WaybillSerialNo.HasValue())
                    {
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.SparePartSale_Warning_WaybillSerialNo);
                    }
                    if (model.WaybillDate == null)
                    {
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.SparePartSale_Warning_WaybillDate);
                    }
                    if (model.ShippingAddressId == null)
                    {
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.SparePartSale_Warning_ShippingAddress);
                    }
                }
            }

            return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSale.SparePartSaleIndex,
            CommonValues.PermissionCodes.SparePartSaleDetail.SparePartSaleDetailUpdate)]
        public ActionResult SparePartSaleRefresh(int sparePartSaleId)
        {
            /*
             Refresh (imaj): Seçilen SPARE_PART_SALE'ın IS_PRICE_FIXED = 0 olan SPARE_PART_SALE_DETAIL kayıtlarında update yapmalı.
            LIST_PRICE kolonu o gün için çekilip update edilmeli, DISCOUNT_PRICE= yeni_LIST_PRICE * ((100 - DISCOUNT_RATIO)/100) bu 2 alan güncellenmeli ve 
            grid refresh edilmeli. Refresh edilince checkboxlar gitmemeli, kullanıcı neleri seçtiyse aynen kalmalı ama kayıt etme işlemi de yapmamalı.
             */
            int count = 0;
            CommonBL bo = new CommonBL();
            SparePartSaleBL spsBo = new SparePartSaleBL();
            SparePartSaleViewModel model = spsBo.GetSparePartSale(UserManager.UserInfo, sparePartSaleId).Model;

            SparePartSaleDetailBL spsdBo = new SparePartSaleDetailBL();
            SparePartSaleDetailListModel spsdListModel = new SparePartSaleDetailListModel();
            spsdListModel.PartSaleId = sparePartSaleId;
            List<SparePartSaleDetailListModel> detailList = spsdBo.ListSparePartSaleDetails(UserManager.UserInfo, spsdListModel, out count).Data;
            foreach (SparePartSaleDetailListModel detailListModel in detailList)
            {
                if (detailListModel.IsPriceFixed)
                {
                    int partId = detailListModel.SparePartId.GetValue<int>();
                    decimal listPrice = bo.GetPriceByDealerPartVehicleAndType(partId, 0, model.DealerId, CommonValues.ListPriceLabel).Model;

                    SparePartSaleDetailDetailModel detailModel = new SparePartSaleDetailDetailModel();
                    detailModel.SparePartSaleDetailId = detailListModel.SparePartSaleDetailId;
                    spsdBo.GetSparePartSaleDetail(UserManager.UserInfo, detailModel);
                    detailModel.CommandType = CommonValues.DMLType.Update;
                    detailModel.ListPrice = listPrice;
                    detailModel.DiscountPrice = listPrice * ((100 - detailModel.DiscountRatio) / 100);
                    spsdBo.DMLSparePartSaleDetail(UserManager.UserInfo, detailModel);
                }
            }

            return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
        }

        #endregion

        #region Spare Part Sale Cancel Collect

        [HttpGet]
        [AuthorizationFilter(Permission.SparePartSaleCancelCollect, Permission.SparePartSaleDetails)]
        public ActionResult SparePartSaleCancelCollect(int? sparePartSaleId)
        {
            /*Gride gelecek kayıtlar: WORK_ORDER_PICKING_MST.ID_PART_SALE = SPARE_PART_SALE.ID_PART_SALE AND WORK_ORDER_PICKING_MST.STATUS_LOOKVAL IN (0,1,2)*/
            WorkOrderPickingListModel wopListModel = new WorkOrderPickingListModel();
            wopListModel.PartSaleId = sparePartSaleId;

            return View(wopListModel);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.WorkOrderPicking.WorkOrderPickingIndex)]
        public ActionResult ListSparePartSaleWorkOrderPicking([DataSourceRequest] DataSourceRequest request, WorkOrderPickingListModel model)
        {
            var bo = new WorkOrderPickingBL();

            int totalCnt;
            var returnValue = bo.ListWorkOrderPicking(UserManager.UserInfo, model, out totalCnt).Data;
            returnValue = returnValue.Where(e => e.StatusId == (int)CommonValues.WorkOrderPickingStatus.NewRecord ||
                                                 e.StatusId == (int)CommonValues.WorkOrderPickingStatus.Started ||
                                                 e.StatusId == (int)CommonValues.WorkOrderPickingStatus.Completed).ToList();

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSale.SparePartSaleIndex,
            CommonValues.PermissionCodes.SparePartSale.SparePartSaleCollect)]
        public ActionResult SparePartSaleCancelCollect(int sparePartSaleId)
        {
            using (var ts = new TransactionScope())
            {
                int detailCount = 0;
                CommonBL cBo = new CommonBL();
                WorkOrderPickingBL wopBo = new WorkOrderPickingBL();
                WorkOrderPickingDetailBL wopdBo = new WorkOrderPickingDetailBL();
                SparePartSaleDetailBL spsdBo = new SparePartSaleDetailBL();
                var workOrderPickingList = ParseModelFromRequestInputStream<List<WorkOrderPickingListModel>>();

                if (workOrderPickingList.Count == 0)
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.SparePartSaleCancelCollect_Warning_NotSelected);

                foreach (WorkOrderPickingListModel listModel in workOrderPickingList)
                {
                    WorkOrderPickingViewModel wopModel = new WorkOrderPickingViewModel();
                    wopModel.WorkOrderPickingId = listModel.WorkOrderPickingId;
                    wopBo.GetWorkOrderPicking(wopModel);

                    /*
                WORK_ORDER_PICKING_MST.STATUS_LOOKVAL= 9  ise;
                Seçilen masterlardan Herhangi birinin statüsü 9 ise, kullanıcıya "XX no'lu toplama emrinin statüsü değişmiştir." mesajı gösterilerek grid refresh edilir. TRX ROLLBACK YAPILIR.
                 */
                    if (wopModel.StatusId == (int)CommonValues.WorkOrderPickingStatus.Cancelled)
                    {
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                            string.Format(MessageResource.SparePartSale_Display_StatusChangedWorkOrderPicking,
                                wopModel.WorkOrderPickingId));
                    }

                    #region NewRecord

                    /*
                WORK_ORDER_PICKING_MST.STATUS_LOOKVAL IN (0) ise;          
                */
                    if (wopModel.StatusId == (int)CommonValues.WorkOrderPickingStatus.NewRecord ||
                        wopModel.StatusId == (int)CommonValues.WorkOrderPickingStatus.Started)
                    {
                        /*
                      * o mastera ait WORK_ORDER_PICKING_DET kayıtlarında dönülerek,  ID_PART ve REQ_QTY değerleri alınır. 
                    */
                        WorkOrderPickingDetailListModel wopdModel = new WorkOrderPickingDetailListModel();
                        wopdModel.WOPMstId = wopModel.WorkOrderPickingId;
                        List<WorkOrderPickingDetailListModel> detailList = wopdBo.ListWorkOrderPickingDetail(UserManager.UserInfo, wopdModel,
                            out detailCount).Data;

                        foreach (WorkOrderPickingDetailListModel detailModel in detailList)
                        {
                            /*
                        WORK_ORDER_PICKING_MST üzerindeki ID_PART_SALE ve 
                         * alınan ID_PART ile 
                         * SPARE_PART_SALE_DETAIL tablosuna gidilir ve tabloda dönülür (bu tabloda unique değil bu 2 kolon, o yüzden kayıtları dönmemiz gerekiyor) 
                         */
                            decimal reqQty = detailModel.RequestQuantity.GetValue<decimal>();
                            SparePartSaleDetailListModel spsodListModel = new SparePartSaleDetailListModel();
                            spsodListModel.PartSaleId = wopModel.PartSaleId.GetValue<long>();
                            spsodListModel.SparePartId = detailModel.PartId;
                            List<SparePartSaleDetailListModel> spsdList = spsdBo.ListSparePartSaleDetails(UserManager.UserInfo,
                                spsodListModel, out detailCount).Data;
                            /*
                         Bulduğumuz kayıtlarda
                         * PICK_PLANNED_QUANTITY değerini  0'a düşürerek kayıtlar arasında dönüyoruz. Örneğin REQ_QTY o parça için 10 ise ve SPARE_PART_SALE_DETAILda 2 tane kayıt varsa, 
                         * ilkinde 8 PICK_PLANNED_QTY = 8, ikincide 18 ise, ilk kayıdınki 0'a güncellenir, ikinci kayıdınki 16'ya.  Bu şekilde bütün WORK_ORDER_PICKING_DET kayıtları dönülerek
                         * SPARE_PART_SALE_DETAIL kayıtları güncellenir 
                         */
                            foreach (SparePartSaleDetailListModel spsdListModel in spsdList)
                            {
                                SparePartSaleDetailDetailModel spsdModel = new SparePartSaleDetailDetailModel();
                                spsdModel.SparePartSaleDetailId = spsdListModel.SparePartSaleDetailId;
                                spsdBo.GetSparePartSaleDetail(UserManager.UserInfo, spsdModel);

                                spsdModel.CommandType = CommonValues.DMLType.Update;
                                spsdModel.PickQuantity = 0;
                                spsdBo.DMLSparePartSaleDetail(UserManager.UserInfo, spsdModel);
                            }
                        }
                        /*sonrasında WORK_ORDER_PICKING_MST.STATUS_LOOKVAL değeri 9 olarak güncellenir.*/
                        wopModel.CommandType = CommonValues.DMLType.Update;
                        wopModel.StatusId = (int)CommonValues.WorkOrderPickingStatus.Cancelled;
                        wopBo.DMLWorkOrderPicking(UserManager.UserInfo, wopModel);
                    }

                    #endregion

                    //    #region Started

                    //    /*
                    //WORK_ORDER_PICKING_MST.STATUS_LOOKVAL IN (1) ise;          
                    //*/
                    //    if (wopModel.StatusId == (int)CommonValues.WorkOrderPickingStatus.Started)
                    //    {
                    //        /*
                    //       * o mastera ait WORK_ORDER_PICKING_DET kayıtlarında dönülerek,  ID_PART ve REQ_QTY değerleri alınır. 
                    //     */
                    //        WorkOrderPickingDetailListModel wopdModel = new WorkOrderPickingDetailListModel();
                    //        wopdModel.WOPMstId = wopModel.WorkOrderPickingId;
                    //        List<WorkOrderPickingDetailListModel> detailList = wopdBo.ListWorkOrderPickingDetail(wopdModel,
                    //            out detailCount);

                    //        foreach (WorkOrderPickingDetailListModel detailModel in detailList)
                    //        {
                    //            /*
                    //        WORK_ORDER_PICKING_MST üzerindeki ID_PART_SALE ve 
                    //         * alınan ID_PART ile 
                    //         * SPARE_PART_SALE_DETAIL tablosuna gidilir ve tabloda dönülür (bu tabloda unique değil bu 2 kolon, o yüzden kayıtları dönmemiz gerekiyor) 
                    //         */
                    //            decimal reqQty = detailModel.RequestQuantity.GetValue<decimal>();
                    //            SparePartSaleDetailListModel spsodListModel = new SparePartSaleDetailListModel();
                    //            spsodListModel.PartSaleId = wopModel.PartSaleId.GetValue<long>();
                    //            spsodListModel.SparePartId = detailModel.PartId;
                    //            List<SparePartSaleDetailListModel> spsdList = spsdBo.ListSparePartSaleDetails(
                    //                spsodListModel, out detailCount);
                    //            /*
                    //         Bulduğumuz kayıtlarda
                    //         * PICK_PLANNED_QUANTITY değerini  0'a düşürerek kayıtlar arasında dönüyoruz. Örneğin REQ_QTY o parça için 10 ise ve SPARE_PART_SALE_DETAILda 2 tane kayıt varsa, 
                    //         * ilkinde 8 PICK_PLANNED_QTY = 8, ikincide 18 ise, ilk kayıdınki 0'a güncellenir, ikinci kayıdınki 16'ya.  Bu şekilde bütün WORK_ORDER_PICKING_DET kayıtları dönülerek
                    //         * SPARE_PART_SALE_DETAIL kayıtları güncellenir 
                    //         */
                    //            foreach (SparePartSaleDetailListModel spsdListModel in spsdList)
                    //            {
                    //                SparePartSaleDetailDetailModel spsdModel = new SparePartSaleDetailDetailModel();
                    //                spsdModel.SparePartSaleDetailId = spsdListModel.SparePartSaleDetailId;
                    //                spsdBo.GetSparePartSaleDetail(spsdModel);

                    //                spsdModel.CommandType = CommonValues.DMLType.Update;
                    //                spsdModel.ReturnedQuantity = spsdModel.ReturnedQuantity.GetValueOrDefault(0) +
                    //                                                 reqQty;
                    //                if (spsdModel.PickQuantity <= reqQty)
                    //                {
                    //                    reqQty = (reqQty - spsdModel.PickQuantity.GetValue<decimal>());
                    //                    spsdModel.PickQuantity = 0;
                    //                }
                    //                else
                    //                {
                    //                    spsdModel.PickQuantity = spsdModel.PickQuantity.GetValueOrDefault(0) - reqQty;


                    //                    reqQty = 0;
                    //                }
                    //                spsdBo.DMLSparePartSaleDetail(spsdModel);
                    //            }
                    //        }
                    //        /*sonrasında WORK_ORDER_PICKING_MST.STATUS_LOOKVAL değeri 9 olarak güncellenir.*/
                    //        wopModel.CommandType = CommonValues.DMLType.Update;
                    //        wopModel.StatusId = (int)CommonValues.WorkOrderPickingStatus.Cancelled;
                    //        wopBo.DMLWorkOrderPicking(wopModel);
                    //    }

                    //    #endregion

                    #region Completed

                    /*
                 WORK_ORDER_PICKING_MST.STATUS_LOOKVAL= 2  ise;
                 * 
                */
                    if (wopModel.StatusId == (int)CommonValues.WorkOrderPickingStatus.Completed)
                    {
                        /*
                       * o mastera ait WORK_ORDER_PICKING_DET kayıtlarında dönülerek,  ID_PART ve REQ_QTY değerleri alınır. 
                     */
                        WorkOrderPickingDetailListModel wopdModel = new WorkOrderPickingDetailListModel();
                        wopdModel.WOPMstId = wopModel.WorkOrderPickingId;
                        List<WorkOrderPickingDetailListModel> detailList = wopdBo.ListWorkOrderPickingDetail(UserManager.UserInfo, wopdModel,
                            out detailCount).Data;

                        /*                     
                    WORK_ORDER_PICKING_DET_1
                    SPARE_PART_SALE_DETAIL_1 (aynı parçayı ve ID_PART_SALE'ı kapsayan 4 satış detayı kaydı varmış)
                    SPARE_PART_SALE_DETAIL_2
                    SPARE_PART_SALE_DETAIL_3
                    SPARE_PART_SALE_DETAIL_4
                    4 kayıtın RETURNED_QUANTITY değerleri güncellendikten sonra TRANSACTION yaratıp bir sonraki picking_det içinde dönmeye başlıyoruz ve böyle devam ediyoruz.
                    WORK_ORDER_PICKING_DET_2
                    WORK_ORDER_PICKING_DET_3
                     */
                        foreach (WorkOrderPickingDetailListModel detailModel in detailList)
                        {
                            /* WORK_ORDER_PICKING_MST üzerindeki ID_PART_SALE ve 
                        * alınan ID_PART ile SPARE_PART_SALE_DETAIL tablosuna gidilir ve tabloda dönülür (bu tabloda unique değil bu 2 kolon, o yüzden kayıtları dönmemiz gerekiyor)
                         * */
                            decimal pickQty = detailModel.PickQuantity.GetValue<decimal>();
                            SparePartSaleDetailListModel spsodListModel = new SparePartSaleDetailListModel();
                            spsodListModel.PartSaleId = wopModel.PartSaleId.GetValue<long>();
                            spsodListModel.SparePartId = detailModel.PartId;
                            List<SparePartSaleDetailListModel> spsdList = spsdBo.ListSparePartSaleDetails(UserManager.UserInfo,
                                spsodListModel, out detailCount).Data;

                            /*
                         * Bulduğumuz kayıtlarda RETURNED_QUANTITY değerleri PICK_QUANTITY'e eşit olacak şekilde (toplamı geçmeden) artırılır. 
                         * statüs 0,1 olduğu durumda PICK_PLANNED_QTY
                         * değerini eksilte eksilte dönüyoduk, bu sefer, RETURNED_QUANTITY'i artıra artıra dönüyoruz. 
                         */
                            foreach (SparePartSaleDetailListModel spsdListModel in spsdList)
                            {
                                SparePartSaleDetailDetailModel spsdModel = new SparePartSaleDetailDetailModel();
                                spsdModel.SparePartSaleDetailId = spsdListModel.SparePartSaleDetailId;
                                spsdBo.GetSparePartSaleDetail(UserManager.UserInfo, spsdModel);

                                spsdModel.CommandType = CommonValues.DMLType.Update;
                                spsdModel.ReturnedQuantity = spsdModel.ReturnedQuantity.GetValueOrDefault(0) +
                                                                pickQty;
                                if (spsdModel.ReturnedQuantity <= pickQty)
                                {
                                    spsdModel.ReturnedQuantity = pickQty;
                                    pickQty = (pickQty - spsdModel.ReturnedQuantity.GetValue<decimal>());
                                    //spsdModel.PickedQuantity = pickQty;
                                }
                                else
                                {
                                    pickQty = 0;
                                    spsdModel.ReturnedQuantity = spsdModel.ReturnedQuantity ?? 0 + pickQty;
                                }
                                spsdBo.DMLSparePartSaleDetail(UserManager.UserInfo, spsdModel);
                            }
                            /*
                         Döndüğümüz WORK_ORDER_PICKING_DET kaydıyla, 
                         * WORK_ORDER_PICKING_RESULT tablosuna gidilir, ID_RACK ve QTY değerleri alınır, bunlarla ve WORK_ORDER_PICKING_DET.ID_PART, ID_STOCK_TYPE değerleri de 
                         * kullanılarak aşağıda tariflenen STOCK_TRANSACTION yaratılır.

                        ID_DEALER: WORK_ORDER_PICKING_MST.ID_DEALER
                        TRX_TYPE_LOOKKEY: 1032
                        TRX_TYPE_LOOKVAL: 20
                        ID_PART:ID_PART
                        QUANTITY:QTY
                        TRANSACTION_DESC: XX nolu toplama emri iptali
                        FROM_WAREHOUSE:NULL
                        FROM_RACK:NULL
                        FROM_STOCK_TYPE:NULL
                        TO_WAREHOUSE: ID_RACK'in warehouse_id değeri
                        TO_RACK:ID_RACK
                        TO_STOCK_TYPE:ID_STOCK_TYPE (PICKING_DET'den aldığımız)  */
                            WOPDetSubListModel wopdsListModel = new WOPDetSubListModel();
                            wopdsListModel.WOPDetId = detailModel.WOPDetId;
                            List<WOPDetSubListModel> wopdsList = wopdBo.ListWorkOrderPickingDetailSub(UserManager.UserInfo, wopdsListModel, out detailCount).Data;
                            foreach (WOPDetSubListModel detailListModel in wopdsList)
                            {
                                int rackId = detailListModel.Value.GetValue<int>();
                                RackDetailModel rModel = new RackDetailModel();
                                rModel.Id = rackId;
                                RackBL rBo = new RackBL();
                                rModel = rBo.GetRack(rModel).Model;

                                StockTransactionViewModel stModel = new StockTransactionViewModel();
                                stModel.CommandType = CommonValues.DMLType.Insert;
                                stModel.DealerId = wopModel.DealerId;
                                stModel.TransactionTypeId = 20;
                                stModel.PartId = detailModel.PartId;
                                stModel.Quantity = detailListModel.Quantity;
                                stModel.TransactionDesc =
                                    string.Format(MessageResource.SparePartSale_Display_CancelWorkOrderPicking,
                                        wopModel.WorkOrderPickingId);
                                stModel.FromWarehouse = null;
                                stModel.FromRack = null;
                                stModel.FromStockType = null;
                                stModel.ToWarehouse = rModel.WarehouseId;
                                stModel.ToRack = rackId;
                                stModel.ToStockType = detailModel.StockTypeId.GetValue<int>();
                                stModel.ReserveQnty = detailListModel.Quantity;
                                cBo.DMLStockTransaction(UserManager.UserInfo, stModel);
                            }
                        }
                        /*sonrasında WORK_ORDER_PICKING_MST.STATUS_LOOKVAL değeri 9 olarak güncellenir.*/
                        wopModel.CommandType = CommonValues.DMLType.Update;
                        wopModel.StatusId = (int)CommonValues.WorkOrderPickingStatus.Cancelled;
                        wopBo.DMLWorkOrderPicking(UserManager.UserInfo, wopModel);

                        //
                    }

                    #endregion

                }
                //eğer bütün toplamalar iptal ise spare part sale yeni kayıt statüsüne çekilir.
                new SparePartSaleBL().ChangeStatusAfterPickCancel(UserManager.UserInfo, sparePartSaleId);


                ts.Complete();
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            }
        }

        #endregion

        #region Spare Part Sale Cancel

        [HttpPost]
        [AuthorizationFilter(SparePartSale.SparePartSaleIndex,
            SparePartSale.SparePartSaleCancel)]
        public ActionResult SparePartSaleCancel(int sparePartSaleId)
        {
            var sparePartbo = new SparePartSaleBL();
            SparePartSaleViewModel viewModel = sparePartbo.GetSparePartSale(UserManager.UserInfo, sparePartSaleId).Model;
            int sparePartSaleStatusId = viewModel.SaleStatusLookVal.GetValue<int>();

            #region Invoiced
            /*Faturalandı statüsündeyki kayıtlar için iptal tuşuna basıldığında, "XX,YY,ZZ Satış kayıtlarını içeren fatura iptal edilecektir, emin misiniz? İptal edilmesi durumunda 
             * işlem geri alınamaz, faturaların tüm nüshalarının elinizde bulunması gerekir. Ayrıca ilgili mali dönem içerisinde işlemin yapıldığına emin olunuz." evet derse, */
            if (sparePartSaleStatusId == (int)CommonValues.SparePartSaleStatus.OrderInvoiced)
            {
                int waybillId = viewModel.SparePartSaleWaybillId.GetValue<int>();
                SparePartSaleWaybillBL spswBo = new SparePartSaleWaybillBL();
                SparePartSaleWaybillViewModel spswModel = spswBo.GetSparePartSaleWaybill(UserManager.UserInfo, waybillId).Model;

                /*                 
                SPARE_PART_SALE_WAYBILL.WAYBILL_NO boşsa:
                SPARE_PART_SALE_WAYBILL.ID_DELIVERY doluysa işlem yapılmaz, kullanıcıya hata verilir, ROLLBACK YAPILIR.
                 */
                if (!spswModel.WaybillNo.HasValue() && spswModel.DeliveryId != null)
                {
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.SparePartSale_Warning_DeliveryNotEmpty);
                }
                else
                {
                    /*
                      * SPARE_PART_SALE_INVOICE.IS_ACTIVE = 0 AND SPARE_PART_SALE_WAYBILL_IDS kolonuna SPARE_PART_SALE'da bu satırla ilişkili WAYBILL_ID'leri virgüllü olacak şekilde koyuyoruz
                      * AND SPARE_PART_SALE_WAYBILL.ID_SPARE_PART_SALE_INVOICE = NULL (bulduğun invoice değerine eşit olanları waybillde nulllıyorum) AND 
                     */
                    int invoiceId = spswModel.SparePartSaleInvoiceId.GetValue<int>();
                    SparePartSaleInvoiceBL spsiBo = new SparePartSaleInvoiceBL();
                    SparePartSaleInvoiceViewModel spsiModel = spsiBo.GetSparePartSaleInvoice(UserManager.UserInfo, invoiceId).Model;
                    spsiModel.CommandType = CommonValues.DMLType.Update;
                    spsiModel.IsActive = false;
                    spsiModel.SparePartSaleWaybillIdList = waybillId.ToString();
                    spsiBo.DMLSparePartSaleInvoice(UserManager.UserInfo, spsiModel);

                    /*
                     SPARE_PART_SALE_WAYBILL.IS_ACTIVE = 0 AND SPARE_PART_SALE_IDS alanına SPARE_PART_SALE tablosundaki SALE_ID'lerini virgülle ayırıp atıyoruz AND 
                     */
                    spswModel.CommandType = CommonValues.DMLType.Update;
                    spswModel.SparePartSaleInvoiceId = null;
                    spswModel.IsActive = false;
                    spswModel.SparePartSaleIdList = viewModel.SparePartSaleId.ToString();
                    spswBo.DMLSparePartSaleWaybill(UserManager.UserInfo, spswModel);

                    /* SPARE_PART_SALE_DETAIL tablosunda bu faturadaki detayların SO_DET_SEQ_NO alanı dolu olanlarının 
                     * SO_DET_SEQ_NO'ları üzerinden SALE_ORDER_DET'e gidilir. SHIPPED_QUANT miktarı SPARE_PARTS_SALE_DETAIL.PICKED - RETURNED kadar azaltılır. */
                    int detailCount = 0;
                    SparePartSaleDetailBL spsdBo = new SparePartSaleDetailBL();
                    SparePartSaleDetailListModel detailModel = new SparePartSaleDetailListModel();
                    detailModel.PartSaleId = viewModel.SparePartSaleId;
                    List<SparePartSaleDetailListModel> detailList = spsdBo.ListSparePartSaleDetails(UserManager.UserInfo, detailModel, out detailCount).Data;
                    var soFullDetailList = detailList.Where(s => s.SoDetSeqNo != null);
                    SparePartSaleOrderDetailBL spsodBo = new SparePartSaleOrderDetailBL();
                    foreach (SparePartSaleDetailListModel soFullDetail in soFullDetailList)
                    {
                        SparePartSaleOrderDetailDetailModel spsodModel = new SparePartSaleOrderDetailDetailModel();
                        spsodModel.SparePartSaleOrderDetailId = soFullDetail.SoDetSeqNo.GetValue<int>();
                        spsodModel = spsodBo.GetSparePartSaleOrderDetail(UserManager.UserInfo, spsodModel).Model;
                        spsodModel.ShippedQuantity = spsodModel.ShippedQuantity - (soFullDetail.PickedQuantity - soFullDetail.ReturnedQuantity);
                        spsodModel.CommandType = CommonValues.DMLType.Update;
                        spsodBo.DMLSparePartSaleOrderDetail(UserManager.UserInfo, spsodModel);
                    }

                    /*
                      * SPARE_PART_SALE_IDS alanına yazdığın SPARE_PART_SALE'ların SPARE_PART_SALE.ID_SPARE_PART_SALE_WAYBILL = NULL AND 
                      * SPARE_PART_SALE.SALE_STATUS_LOOKVAL = 2 AND 
                     */
                    viewModel.CommandType = CommonValues.DMLType.Update;
                    viewModel.SparePartSaleWaybillId = null;
                    viewModel.SaleStatusLookVal = ((int)CommonValues.SparePartSaleStatus.OrderCollected).ToString();
                }
            }
            #endregion

            #region Waybilled
            /*
             SPARE_PART_SALE.SALE_STATUS_LOOKVAL = 2 AND SPARE_PART_SALE_WAYBILL.WAYBILL_NO = DOLU ise 
             * AND SPARE_PART_SALE_WAYBILL.ID_SPARE_PART_SALE_INVOICE = NULLsa (İrsaliyesi kesilenler oluyor bu, irsaliyeleri iptal edeceğiz bu koşulda)
             "XX,YY,ZZ Satış kayıtlarını içeren irsaliye iptal edilecektir, emin misiniz?" onayı sorulur, evet derse:
             */
            else if (sparePartSaleStatusId == (int)CommonValues.SparePartSaleStatus.OrderWaybilled)
            {
                int waybillId = viewModel.SparePartSaleWaybillId.GetValue<int>();
                SparePartSaleWaybillBL spswBo = new SparePartSaleWaybillBL();
                SparePartSaleWaybillViewModel spswModel = spswBo.GetSparePartSaleWaybill(UserManager.UserInfo, waybillId).Model;

                /*
                 SPARE_PART_SALE_WAYBILL.SPARE_PART_SALE_IDS alanına SPARE_PART_SALE tablosunda bu irsaliyenin kayıtlarının ID_PART_SALE değerleri virgülle ayrılarak yazılır.
                SPARE_PART_SALE_WAYBILL.IS_ACTIVE = 0
                 */
                spswModel.CommandType = CommonValues.DMLType.Update;
                spswModel.IsActive = false;
                spswModel.SparePartSaleIdList = viewModel.SparePartSaleId.ToString();
                spswBo.DMLSparePartSaleWaybill(UserManager.UserInfo, spswModel);

                /*
                 SPARE_PART_SALE_DETAIL tablosunda bu faturadaki detayların SO_DET_SEQ_NO alanı dolu olanlarının SO_DET_SEQ_NO'ları üzerinden SALE_ORDER_DET'e gidilir. 
                * SHIPPED_QUANT miktarı SPARE_PARTS_SALE_DETAIL.PICKED - RETURNED kadar azaltılır. 
                 */
                int detailCount = 0;
                SparePartSaleDetailBL spsdBo = new SparePartSaleDetailBL();
                SparePartSaleDetailListModel detailModel = new SparePartSaleDetailListModel();
                detailModel.PartSaleId = viewModel.SparePartSaleId;
                List<SparePartSaleDetailListModel> detailList = spsdBo.ListSparePartSaleDetails(UserManager.UserInfo, detailModel, out detailCount).Data;
                var soFullDetailList = detailList.Where(s => s.SoDetSeqNo != null);
                SparePartSaleOrderDetailBL spsodBo = new SparePartSaleOrderDetailBL();
                foreach (SparePartSaleDetailListModel soFullDetail in soFullDetailList)
                {
                    SparePartSaleOrderDetailDetailModel spsodModel = new SparePartSaleOrderDetailDetailModel();
                    spsodModel.SparePartSaleOrderDetailId = soFullDetail.SoDetSeqNo.GetValue<int>();
                    spsodModel = spsodBo.GetSparePartSaleOrderDetail(UserManager.UserInfo, spsodModel).Model;
                    spsodModel.ShippedQuantity = spsodModel.ShippedQuantity - (soFullDetail.PickedQuantity - soFullDetail.ReturnedQuantity);
                    spsodModel.CommandType = CommonValues.DMLType.Update;
                    spsodBo.DMLSparePartSaleOrderDetail(UserManager.UserInfo, spsodModel);
                }

                /*
                 SPARE_PART_SALE_WAYBILL'e karşılık gelen SPARE_PART_SALE kayıtlarının ID_SPARE_PART_SALE_WAYBILL = NULL olarak güncellenir.
                 */
                viewModel.CommandType = CommonValues.DMLType.Update;
                viewModel.SparePartSaleWaybillId = null;
                viewModel.SaleStatusLookVal = ((int)CommonValues.SparePartSaleStatus.OrderCollected).ToString();
            }
            #endregion

            #region Other Status
            else
            {
                viewModel.CommandType = CommonValues.DMLType.Update;
                viewModel.SaleStatusLookVal = ((int)CommonValues.SparePartSaleStatus.OrderCancelled).ToString();
            }
            #endregion

            sparePartbo.DMLSparePartSale(UserManager.UserInfo, viewModel);

            if (viewModel.ErrorNo > 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);

            return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
        }

        #endregion

        #region Spare Part Sale Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSale.SparePartSaleIndex,
            CommonValues.PermissionCodes.SparePartSale.SparePartSaleDelete)]
        public ActionResult DeleteSparePartSale(int sparePartSaleId)
        {
            SparePartBL spBo = new SparePartBL();
            SparePartSaleViewModel viewModel = new SparePartSaleViewModel() { SparePartSaleId = sparePartSaleId };
            var sparePartSaleBo = new SparePartSaleBL();
            SparePartSaleViewModel model = sparePartSaleBo.GetSparePartSale(UserManager.UserInfo, sparePartSaleId).Model;

            if(model.SaleStatusLookVal != ((int)CommonValues.SparePartSaleStatus.NewRecord).ToString())
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.SparePartSale_Warning_CollectStartedCannotCancel);
            }

            int detailCount = 0;
            SparePartSaleDetailListModel spsdListModel = new SparePartSaleDetailListModel();
            spsdListModel.PartSaleId = model.SparePartSaleId;
            SparePartSaleOrderBL spsoBo = new SparePartSaleOrderBL();
            SparePartSaleOrderDetailBL spsodBo = new SparePartSaleOrderDetailBL();
            SparePartSaleDetailBL spsdBo = new SparePartSaleDetailBL();
            List<SparePartSaleDetailListModel> detailList = spsdBo.ListSparePartSaleDetails(UserManager.UserInfo, spsdListModel, out detailCount).Data;
            foreach (SparePartSaleDetailListModel detailModel in detailList)
            {
                /*
                 - Öncelikle parçaları SPARE_PART_SALE_DETAIL'a eklerken rezerv koymuştuk, bunların rezervini kaldırmamız gerekiyor. 
                 * Bunun için Tüm SPARE_PART_SALE_DETAIL kayıtları ID_PART'a göre gruplanır FN_SPARE_PART_UNSERVE functionı tüm parçalar için şu parametrelerle çağırılır.
                 * (ID_PART = SPARE_PART_SALE_DETAIL.ID_PART, STOCK_TYPE = SPARE_PART_SALE.STOCK_TYPE, QUANTITY = SPARE_PART_SALE_DETAIL.PLAN_QUANTITY,
                 * DESC = "SPARE_PARTS_SALE_DETAIL.ID_PART_SALE nolu satış kaydının iptali",TYPE = 11 ) functiondan cevap olarak kaldırılabilen rezerve miktarı dönecek
                 * farklı bile olsa koyduğun miktardan devam etmelisin.
                 */
                spBo.UpdateSparePartUnserve(UserManager.UserInfo, model.DealerId, detailModel.SparePartId, model.StockTypeId.GetValue<int>(), detailModel.PlanQuantity.GetValue<decimal>(),
                    string.Format(MessageResource.SparePartSale_Display_CancelSaleOrder, detailModel.PartSaleId), 11);

                /*
                 - SPARE_PART_SALE içerisinde SO_DET_SEQ_NO dolu olan tüm detaylar için ilgili SALE_ORDER_DET kaydı bulunur, PLANNED_QUANT değeri SPARE_PART_SALE_DETAIL'daki 
                 * PICKED_QUANTITY - RETURN_QUANTITY kadar düşürülür. SALE_ORDER_DET.STATUS_LOOKVAL 1 ise, 0 olarak güncellenir. 
                 */
                if (detailModel.SoDetSeqNo.HasValue())
                {
                    SparePartSaleOrderDetailDetailModel spsodModel = new SparePartSaleOrderDetailDetailModel();
                    spsodModel.SparePartSaleOrderDetailId = detailModel.SoDetSeqNo.GetValue<int>();
                    spsodBo.GetSparePartSaleOrderDetail(UserManager.UserInfo, spsodModel);

                    spsodModel.CommandType = CommonValues.DMLType.Update;
                    spsodModel.PlannedQuantity = spsodModel.PlannedQuantity - (detailModel.PickedQuantity - detailModel.ReturnedQuantity);
                    spsodModel.StatusId = spsodModel.StatusId == (int)CommonValues.SparePartSaleOrderDetailStatus.ClosedOrder
                        ? (int)CommonValues.SparePartSaleOrderDetailStatus.OpenOrder
                        : spsodModel.StatusId;
                    spsodBo.DMLSparePartSaleOrderDetail(UserManager.UserInfo, spsodModel);

                    /*- DISTINCT(SALE_ORDER_DET.SO_NUMBER) kayıtlarının SALE_ORDER_MST.STATUS_LOOKVAL 5 ise, 4 olarak update edilir.*/
                    SparePartSaleOrderViewModel spsoModel = spsoBo.GetSparePartSaleOrder(UserManager.UserInfo, spsodModel.SoNumber);
                    spsoModel.StatusId = spsoModel.StatusId == (int)CommonValues.SparePartSaleOrderStatus.ClosedOrder
                        ? (int)CommonValues.SparePartSaleOrderStatus.ApprovedProposal
                        : spsoModel.StatusId;
                }
            }
            /*- SPARE_PART_SALE_DETAIL ve SPARE_PART_SALE kayıtları delete edilir.*/
            viewModel.CommandType = CommonValues.DMLType.Delete;
            sparePartSaleBo.DMLSparePartSale(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                                                      MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
        }

        #endregion

        #region Spare Part Sale Create Invoice

        [HttpGet]
        [AuthorizationFilter(Permission.SparePartSaleInvoice, Permission.SparePartSaleDetails)]
        public ActionResult SparePartSaleCreateInvoice(int? sparePartSaleId)
        {
            FillComboBoxes();

            SparePartSaleBL spsBo = new SparePartSaleBL();
            SparePartSaleViewModel spsModel = spsBo.GetSparePartSale(UserManager.UserInfo, sparePartSaleId.GetValue<int>()).Model;
            SparePartSaleWaybillBL spswBo = new SparePartSaleWaybillBL();
            SparePartSaleWaybillViewModel spswModel = new SparePartSaleWaybillViewModel();
            spswModel = spswBo.GetSparePartSaleWaybill(UserManager.UserInfo, spsModel.SparePartSaleWaybillId.GetValue<int>()).Model;
            SparePartSaleInvoiceViewModel spsiModel = new SparePartSaleInvoiceViewModel() { InvoiceDate = DateTime.Now };


            if (spswModel.SparePartSaleInvoiceId.HasValue)
            {
                // kayıt varsa
                SparePartSaleInvoiceBL spsiBo = new SparePartSaleInvoiceBL();
                spsiModel = spsiBo.GetSparePartSaleInvoice(UserManager.UserInfo, spswModel.SparePartSaleInvoiceId.GetValue<int>()).Model;

                if (spsiModel.PaymentTypeId.HasValue)
                {
                    List<PaymentTypeListModel> ptList = spsBo.ListPaymentTypes(UserManager.UserInfo).Data;
                    var control = (from e in ptList.AsEnumerable()
                                   where e.Id == spsiModel.PaymentTypeId
                                   select e);
                    if (control.Any())
                    {
                        PaymentTypeListModel ptModel = control.First();
                        spsiModel.BankRequired = ptModel.BankRequired;
                        spsiModel.InstalmentRequired = ptModel.InstalmentRequired;
                        spsiModel.DefermentRequired = ptModel.DefermentRequired;
                    }
                }
            }
            spsiModel.SparePartSaleId = spsModel.SparePartSaleId;
            spsiModel.SparePartSaleWaybillId = spswModel.SparePartSaleWaybillId;
            spsiModel.CustomerId = spsModel.CustomerId;
            CustomerBL cBo = new CustomerBL();
            CustomerIndexViewModel cModel = new CustomerIndexViewModel();
            cModel.CustomerId = spsiModel.CustomerId;
            cBo.GetCustomer(UserManager.UserInfo, cModel);
            spsiModel.CustomerName = cModel.CustomerName;
            spsiModel.CustomerLastName = cModel.CustomerLastName;

            return View(spsiModel);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSale.SparePartSaleWaybill)]
        public ActionResult ListSparePartSaleInvoice([DataSourceRequest] DataSourceRequest request, SparePartSaleViewModel model)
        {
            int totalCnt;
            SparePartSaleBL spsBo = new SparePartSaleBL();
            // seçilen kayda ait sps kaydı
            SparePartSaleListModel spsListModel = new SparePartSaleListModel();
            spsListModel.SparePartSaleId = model.SparePartSaleId;
            List<SparePartSaleListModel> spsList = spsBo.ListSparePartSales(UserManager.UserInfo, spsListModel, out totalCnt).Data;
            int customerId = model.CustomerId;

            // customer aynı waybill dolu waybill üzerindeki invoice null olan sps kayıtları
            SparePartSaleListModel spscListModel = new SparePartSaleListModel();
            spscListModel.CustomerId = customerId;
            List<SparePartSaleListModel> spscList = spsBo.ListSparePartSales(UserManager.UserInfo, spscListModel, out totalCnt).Data;
            spscList = spscList.Where(c => c.SparePartSaleId != model.SparePartSaleId && c.SparePartSaleWaybillId != null).ToList();

            SparePartSaleWaybillBL spswBo = new SparePartSaleWaybillBL();
            List<SparePartSaleListModel> finalList = new List<SparePartSaleListModel>();
            foreach (SparePartSaleListModel spscModel in spscList)
            {
                SparePartSaleWaybillViewModel spswModel = spswBo.GetSparePartSaleWaybill(UserManager.UserInfo, spscModel.SparePartSaleWaybillId.GetValue<int>()).Model;
                if (!spswModel.SparePartSaleInvoiceId.HasValue)
                    finalList.Add(spscModel);
            }
            spsList.AddRange(finalList);
            // sps kayıtlarının waybill bilgileri
            SparePartSaleDetailBL spsdBo = new SparePartSaleDetailBL();
            List<SparePartSaleWaybillListModel> spswList = new List<SparePartSaleWaybillListModel>();
            foreach (SparePartSaleListModel spsModel in spsList)
            {
                SparePartSaleWaybillListModel addedModel = new SparePartSaleWaybillListModel();
                int waybillId = spsModel.SparePartSaleWaybillId.GetValue<int>();
                addedModel.SparePartSaleWaybillId = waybillId;
                addedModel = spswBo.ListSparePartSaleWaybills(UserManager.UserInfo, addedModel, out totalCnt).Data.First();

                SparePartSaleDetailListModel spsdListModel = new SparePartSaleDetailListModel();
                spsdListModel.PartSaleId = spsModel.SparePartSaleId;
                List<SparePartSaleDetailListModel> spsdList = spsdBo.ListSparePartSaleDetails(UserManager.UserInfo, spsdListModel, out totalCnt).Data;

                addedModel.TotalListPriceWithoutVAT = spsdList.Sum(e => e.ListPrice * e.PlanQuantity);
                addedModel.TotalPriceWithoutVAT = spsdList.Sum(e => e.DiscountPrice * e.PlanQuantity);
                addedModel.WaybillDate = spsModel.WaybillDate ?? default(DateTime);
                spswList.Add(addedModel);
            }

            return Json(new
            {
                Data = spswList,
                Total = spswList.Count
            });
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSale.SparePartSaleInvoice)]
        public ActionResult SparePartSaleCreateInvoice(SparePartSaleInvoiceViewModel model)
        {
            List<SelectListItem> spswIdList = model.SparePartSaleWaybillList;

            CustomerAddressBL caBo = new CustomerAddressBL();

            if (ModelState.IsValid)
            {
                CustomerBL cBo = new CustomerBL();
                CustomerIndexViewModel cModel = new CustomerIndexViewModel();
                cModel.CustomerId = model.CustomerId;
                cBo.GetCustomer(UserManager.UserInfo, cModel);

                CustomerAddressIndexViewModel bcaModel = new CustomerAddressIndexViewModel();
                bcaModel.AddressId = model.BillingAddressId.GetValue<int>();
                caBo.GetCustomerAddress(UserManager.UserInfo, bcaModel);

                SparePartSaleInvoiceBL spsiBo = new SparePartSaleInvoiceBL();
                model.CustomeTaxNo = cModel.TaxNo;
                model.CustomerAddress1 = bcaModel.Address1;
                model.CustomerAddress2 = bcaModel.Address2;
                model.CustomerAddress3 = bcaModel.Address3;
                model.CustomerAddressCityText = bcaModel.CityName;
                model.CustomerAddressCountryText = bcaModel.CountryName;
                model.CustomerAddressTownText = bcaModel.TownName;
                model.CustomerAddressZipCode = bcaModel.ZipCode;
                model.CustomerId = model.CustomerId;
                model.CustomerLastName = cModel.CustomerLastName;
                model.CustomerName = cModel.CustomerName;
                model.CustomerPassportNo = cModel.PassportNo;
                model.CustomerTCIdentity = cModel.TcIdentityNo;
                model.CustomerTaxOffice = cModel.TaxOffice;
                model.IsActive = true;
                model.CommandType = CommonValues.DMLType.Insert;
                spsiBo.DMLSparePartSaleInvoice(UserManager.UserInfo, model);
                if (model.ErrorNo > 0)
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);

                SparePartSaleBL spsBo = new SparePartSaleBL();
                SparePartSaleWaybillBL spswBo = new SparePartSaleWaybillBL();
                foreach (SelectListItem spswId in spswIdList)
                {
                    SparePartSaleWaybillViewModel spswModel = spswBo.GetSparePartSaleWaybill(UserManager.UserInfo, spswId.Value.GetValue<int>()).Model;
                    spswModel.CommandType = CommonValues.DMLType.Update;
                    spswModel.SparePartSaleInvoiceId = model.SparePartSaleInvoiceId;
                    spswBo.DMLSparePartSaleWaybill(UserManager.UserInfo, spswModel);
                    if (spswModel.ErrorNo > 0)
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Error, spswModel.ErrorMessage);

                    List<int> idPartSaleList = new List<int>();
                    if (spswModel.SparePartSaleIdList != null && spswModel.SparePartSaleIdList.Length != 0)
                    {
                        string[] list = spswModel.SparePartSaleIdList.Split(',');
                        foreach (string id in list)
                            idPartSaleList.Add(id.GetValue<int>());
                    }
                    else
                    {
                        idPartSaleList.Add(model.SparePartSaleId);
                    }

                    foreach (int idPartSale in idPartSaleList)
                    {
                        SparePartSaleViewModel spsModel = spsBo.GetSparePartSale(UserManager.UserInfo, idPartSale).Model;
                        spsModel.CommandType = CommonValues.DMLType.Update;
                        spsModel.SaleStatusLookVal = ((int)CommonValues.SparePartSaleStatus.OrderInvoiced).ToString();
                        spsBo.DMLSparePartSale(UserManager.UserInfo, spsModel);
                        if (spsModel.ErrorNo > 0)
                            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, spsModel.ErrorMessage);
                    }
                }
            }
            else
            {
                if (!model.InvoiceNo.HasValue())
                {
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.SparePartSale_Warning_InvoiceNo);
                }
                if (model.SparePartSaleWaybillList.Count == 0)
                {
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.SparePartSale_Warning_SparePartWaybillList);
                }
                if (!model.InvoiceSerialNo.HasValue())
                {
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.SparePartSale_Warning_InvoiceSerialNo);
                }
                if (model.InvoiceDate == null)
                {
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.SparePartSale_Warning_InvoiceDate);
                }
                if (model.BillingAddressId == null)
                {
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.SparePartSale_Warning_BillingAddress);
                }
                if (model.InstalmentRequired && model.InstalmentNumber == null)
                {
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.SparePartSale_Warning_InstalmentNumber);
                }
                if (model.BankRequired && model.BankId == null)
                {
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.SparePartSale_Warning_Bank);
                }
                if (model.DefermentRequired && model.DueDuration == null)
                {
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.SparePartSale_Warning_DueDuration);
                }
            }

            return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSale.SparePartSaleIndex,
            CommonValues.PermissionCodes.SparePartSaleDetail.SparePartSaleDetailUpdate)]
        public ActionResult SparePartSaleWaybillRefresh(int sparePartSaleWaybillId)
        {
            /*
             Seçilen  ID_SPARE_PART_SALE_WAYBIL = SPARE_PART_SALE.ID_SPARE_PART_SALE_WAYBILL olan masterların SPARE_PART_SALE_DETAIL'ın kayıtları içinde  IS_PRICE_FIXED = 0 olan 
             * SPARE_PART_SALE_DETAIL kayıtlarında update yapmalı. 
             * LIST_PRICE kolonu o gün için çekilip update edilmeli, DISCOUNT_PRICE= yeni_LIST_PRICE * ((100 - DISCOUNT_RATIO)/100) bu 2 alan güncellenmeli ve grid refresh edilmeli.
             * Refresh edilince checkboxlar gitmemeli, kullanıcı neleri seçtiyse aynen kalmalı ama kayıt etme işlemi de yapmamalı.
             */
            using (var ts = new TransactionScope())
            {

                int count = 0;
                CommonBL bo = new CommonBL();
                SparePartSaleWaybillBL spswBo = new SparePartSaleWaybillBL();
                SparePartSaleWaybillViewModel spswModel = spswBo.GetSparePartSaleWaybill(UserManager.UserInfo, sparePartSaleWaybillId).Model;
                SparePartSaleBL spsBo = new SparePartSaleBL();
                SparePartSaleViewModel model = spsBo.GetSparePartSale(UserManager.UserInfo, spswModel.SparePartSaleId).Model;

                SparePartSaleDetailBL spsdBo = new SparePartSaleDetailBL();
                SparePartSaleDetailListModel spsdListModel = new SparePartSaleDetailListModel();
                spsdListModel.PartSaleId = spswModel.SparePartSaleId;
                List<SparePartSaleDetailListModel> detailList = spsdBo.ListSparePartSaleDetails(UserManager.UserInfo, spsdListModel, out count).Data;
                foreach (SparePartSaleDetailListModel detailListModel in detailList)
                {
                    if (detailListModel.IsPriceFixed)
                    {
                        int partId = detailListModel.SparePartId.GetValue<int>();
                        decimal listPrice = bo.GetPriceByDealerPartVehicleAndType(partId, 0, model.DealerId,
                            CommonValues.ListPriceLabel).Model;

                        SparePartSaleDetailDetailModel detailModel = new SparePartSaleDetailDetailModel();
                        detailModel.SparePartSaleDetailId = detailListModel.SparePartSaleDetailId;
                        spsdBo.GetSparePartSaleDetail(UserManager.UserInfo, detailModel);
                        detailModel.CommandType = CommonValues.DMLType.Update;
                        detailModel.ListPrice = listPrice;
                        detailModel.DiscountPrice = listPrice * ((100 - detailModel.DiscountRatio) / 100);
                        spsdBo.DMLSparePartSaleDetail(UserManager.UserInfo, detailModel);
                    }
                }
                ts.Complete();
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            }
        }

        #endregion

        #region Print Spare Part Sale

        public ActionResult PrintSparePartReportReal(int sparePartSaleId, string invoiceNo)
        {

            MemoryStream ms = null;

            try
            {
                ms = new MemoryStream(ReportManager.GetReport(ReportType.SparePartSaleRealReport, sparePartSaleId));
            }
            catch (Exception ex)
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, ex.Message);
            }

            return File(ms, "application/pdf", string.Format(MessageResource.SparePartSale_Display_InvoiceReportName, invoiceNo));
        }
        public ActionResult PrintSparePartSaleReportCopy(int sparePartSaleId, string invoiceNo)
        {

            MemoryStream ms = null;

            try
            {
                ms = new MemoryStream(ReportManager.GetReport(ReportType.SparePartSaleCopyReport, sparePartSaleId));
            }
            catch (Exception ex)
            {
                SetMessage(MessageResource.ErrorSparePartSaleReport, CommonValues.MessageSeverity.Fail);
                return SparePartSaleIndex(sparePartSaleId);
            }

            return File(ms, "application/pdf", string.Format(MessageResource.SparePartSale_Display_InvoiceCopyReportName, invoiceNo));
        }
        public ActionResult PrintSparePartSaleReportProforma(long sparePartSaleId, string invoiceNo)
        {
            MemoryStream ms = null;

            try
            {
                ms = new MemoryStream(ReportManager.GetReport(ReportType.SparePartSaleProformaReport, sparePartSaleId));
            }
            catch (Exception ex)
            {
                SetMessage(MessageResource.ErrorSparePartSaleReport, CommonValues.MessageSeverity.Fail);
                return SparePartSaleIndex(sparePartSaleId.GetValue<int>());
            }

            return File(ms, "application/pdf", string.Format(MessageResource.SparePartSale_Display_InvoiceProformaReportName, invoiceNo));
        }
        #endregion

        /*
            SparePartSaleWaybillRealReport = matbu waybill
            SparePartSaleWaybillCopyReport = önizleme waybill

            SaleOrderPrintReport = matbu invoice
            SaleOrderPrintCopyReport = önizleme invoice (rapor daha tamamlanmadı tamamlanınca eklenecek)*/

        public ActionResult PrintInvoiceReport(long sparePartSaleId, int dealerId)
        {
            dealerId = UserManager.UserInfo.GetUserDealerId();
            MemoryStream ms = null;
            try
            {
                ms = new MemoryStream(ReportManager.GetReport(ReportType.SparePartSaleRealReport, sparePartSaleId, dealerId, UserManager.LanguageCode));
            }
            catch (Exception ex)
            {
                SetMessage(MessageResource.ErrorSparePartSaleReport, CommonValues.MessageSeverity.Fail);
                return SparePartSaleIndex(sparePartSaleId.GetValue<int>());
            }

            return File(ms, "application/pdf", "SaleOrderPrintReport.pdf");
        }

        public ActionResult PrintInvoiceCopyReport(int sparePartSaleId, int dealerId)
        {

            dealerId = UserManager.UserInfo.GetUserDealerId();
            MemoryStream ms = null;
            try
            {
                ms = new MemoryStream(ReportManager.GetReport(ReportType.SparePartSaleCopyReport, sparePartSaleId, dealerId, UserManager.LanguageCode));
            }
            catch (Exception ex)
            {
                SetMessage(MessageResource.ErrorSparePartSaleReport, CommonValues.MessageSeverity.Fail);
                return SparePartSaleIndex(sparePartSaleId);
            }

            return File(ms, "application/pdf", "SaleOrderPrintCopyReport.pdf");
        }

        public ActionResult PrintWaybillReport(long sparePartSaleWaybillId, int dealerId)
        {
            dealerId = UserManager.UserInfo.GetUserDealerId();
            MemoryStream ms = null;
            try
            {
                ms = new MemoryStream(ReportManager.GetReport(ReportType.SparePartSaleWaybillRealReport, sparePartSaleWaybillId, dealerId, UserManager.LanguageCode));
            }
            catch (Exception ex)
            {
                SetMessage(ex.Message, CommonValues.MessageSeverity.Fail);
                return Redirect("~/error");
            }

            return File(ms, "application/pdf", "Satışİrsaliyesi_" + sparePartSaleWaybillId + ".pdf");
        }

        public ActionResult PrintWaybillCopyReport(long sparePartSaleWaybillId, int dealerId)
        {
            dealerId = UserManager.UserInfo.GetUserDealerId();
            MemoryStream ms = null;
            try
            {

                ms = new MemoryStream(ReportManager.GetReport(ReportType.SparePartSaleWaybillCopyReport, sparePartSaleWaybillId, dealerId, UserManager.LanguageCode));
            }
            catch (Exception ex)
            {
                SetMessage(ex.Message, CommonValues.MessageSeverity.Fail);
                return Redirect("~/error");
            }

            return File(ms, "application/pdf", "SatışİrsaliyeÖnizleme_" + sparePartSaleWaybillId + " .pdf");
        }

        #region Otokar Spare Part Sale Index

        [HttpGet]
        [AuthorizationFilter(Permission.OtokarSparePartSaleIndex)]
        public ActionResult OtokarSparePartSaleIndex()
        {
            var model = new SparePartSaleViewModel
            {
                CustomerId =
                    CommonBL.GetGeneralParameterValue(CommonValues.GeneralParameters.OtokarCustomerId).Model.GetValue<int>()
            };
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSale.OtokarSparePartSaleIndex, CommonValues.PermissionCodes.SparePartSale.OtokarSparePartSaleDetails)]
        public ActionResult ListOtokarSparePartSales([DataSourceRequest]DataSourceRequest request, SparePartSaleViewModel model)
        {
            var bo = new SparePartSaleBL();
            var referenceModel = new SparePartSaleListModel(request)
            {
                CustomerId = model.CustomerId,
                InvoiceNo = model.InvoiceNo,
                InvoiceDate = model.InvoiceDate
            };
            int totalCnt;
            var returnValue = bo.ListSparePartSales(UserManager.UserInfo, referenceModel, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }
        #endregion

        #region OtokarSpare Part Sale Create
        [HttpGet]
        [AuthorizationFilter(Permission.OtokarSparePartSaleIndex, Permission.OtokarSparePartSaleCreate)]
        public ActionResult OtokarSparePartSaleCreate()
        {
            ViewBag.IsCreate = true;
            FillComboBoxes();
            int customerId =
                CommonBL.GetGeneralParameterValue(CommonValues.GeneralParameters.OtokarCustomerId).Model.GetValue<int>();
            var custBo = new CustomerBL();
            var custMo = new CustomerIndexViewModel { CustomerId = customerId };
            custBo.GetCustomer(UserManager.UserInfo, custMo);

            UserInfo userInfo = UserManager.UserInfo;
            var model = new OtokarSparePartSaleViewModel
            {
                CustomerId = custMo.CustomerId,
                CustomerName = custMo.CustomerName,
                CustomerTypeId = custMo.CustomerTypeId.GetValue<int>(),
                CustomerType = custMo.CustomerTypeName,
                DealerId = userInfo.GetUserDealerId(),
                //IsTenderSale = 0,
                SaleDate = DateTime.Now,
                SaleResponsible = userInfo.UserName + CommonValues.EmptySpace + userInfo.LastName,
                SaleStatusLookVal = ((int)CommonValues.SparePartSaleStatus.NewRecord).ToString(),
                //PaymentAmount = 0,
                IsReturn = true
            };

            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.OtokarSparePartSaleIndex, Permission.OtokarSparePartSaleCreate)]
        public ActionResult OtokarSparePartSaleCreate(OtokarSparePartSaleViewModel model)
        {
            ViewBag.IsCreate = true;
            DealerBL dealerBo = new DealerBL();
            DealerViewModel dModel = dealerBo.GetDealer(UserManager.UserInfo, UserManager.UserInfo.GetUserDealerId()).Model;
            FillComboBoxes();

            //model.WayBilllDate = model.InvoiceDate;
            //model.WayBillNo = model.InvoiceNo;
            //model.WayBillSerialNo = model.InvoiceSerialNo;

            if (ModelState.IsValid)
            {
                using (var ts = new TransactionScope())
                {
                    model.VatExclude = 0;
                    model.PriceListId = dealerBo.GetCountryDefaultPriceList(dModel.Country).Model;

                    var bo = new SparePartSaleBL();
                    model.CommandType = CommonValues.DMLType.Insert;
                    bo.DMLSparePartSaleOtokar(UserManager.UserInfo, model);

                    CreateOtokarSparePartSaleWaybill(model);

                    CheckErrorForMessage(model, true);
                    ModelState.Clear();

                    ts.Complete();
                }
                int customerId =
                  CommonBL.GetGeneralParameterValue(CommonValues.GeneralParameters.OtokarCustomerId).Model.GetValue<int>();
                var custBo = new CustomerBL();
                var custMo = new CustomerIndexViewModel { CustomerId = customerId };
                custBo.GetCustomer(UserManager.UserInfo, custMo);
                UserInfo userInfo = UserManager.UserInfo;
                var newModel = new OtokarSparePartSaleViewModel
                {
                    CustomerId = custMo.CustomerId,
                    CustomerName = custMo.CustomerName,
                    CustomerTypeId = custMo.CustomerTypeId.GetValue<int>(),
                    CustomerType = custMo.CustomerTypeName,
                    DealerId = userInfo.GetUserDealerId(),
                    SaleDate = DateTime.Now,
                    SaleResponsible = userInfo.UserName + CommonValues.EmptySpace + userInfo.LastName,
                    SaleStatusLookVal = ((int)CommonValues.SparePartSaleStatus.NewRecord).ToString(),
                    //PaymentAmount = 0,
                    IsReturn = true,
                    SparePartSaleId = model.SparePartSaleId
                };
                return View(newModel);
            }
            return View(model);
        }

        private void CreateOtokarSparePartSaleWaybill(OtokarSparePartSaleViewModel vmodel)
        {
            var model = new SparePartSaleWaybillViewModel();
            model.IsWaybilled = true;
            model.SparePartSaleList = new List<SelectListItem>()
            { new SelectListItem { Value = vmodel.SparePartSaleId.ToString() } };

            List<SelectListItem> spsIdList = model.SparePartSaleList;

            CustomerAddressBL caBo = new CustomerAddressBL();
            var control = model.SparePartSaleList;


            CustomerBL cBo = new CustomerBL();
            CustomerIndexViewModel cModel = new CustomerIndexViewModel();
            cModel.CustomerId = vmodel.CustomerId;
            cBo.GetCustomer(UserManager.UserInfo, cModel);

            SparePartSaleInvoiceBL spsiBo = new SparePartSaleInvoiceBL();
            SparePartSaleInvoiceViewModel spsiModel = new SparePartSaleInvoiceViewModel();
            if (model.IsWaybilled)
            {
                CustomerAddressIndexViewModel bcaModel = new CustomerAddressIndexViewModel();
                bcaModel.AddressId = vmodel.BillingAddressId.GetValue<int>();
                caBo.GetCustomerAddress(UserManager.UserInfo, bcaModel);

                spsiModel.BankId = null;
                spsiModel.BillingAddressId = vmodel.BillingAddressId.GetValue<int>();
                spsiModel.CustomeTaxNo = cModel.TaxNo;
                spsiModel.CustomerAddress1 = bcaModel.Address1;
                spsiModel.CustomerAddress2 = bcaModel.Address2;
                spsiModel.CustomerAddress3 = bcaModel.Address3;
                spsiModel.CustomerAddressCityText = bcaModel.CityName;
                spsiModel.CustomerAddressCountryText = bcaModel.CountryName;
                spsiModel.CustomerAddressTownText = bcaModel.TownName;
                spsiModel.CustomerAddressZipCode = bcaModel.ZipCode;
                spsiModel.CustomerId = vmodel.CustomerId;
                spsiModel.CustomerLastName = cModel.CustomerLastName;
                spsiModel.CustomerName = cModel.CustomerName;
                spsiModel.CustomerPassportNo = cModel.PassportNo;
                spsiModel.CustomerTCIdentity = cModel.TcIdentityNo;
                spsiModel.CustomerTaxOffice = cModel.TaxOffice;
                spsiModel.DealerId = vmodel.DealerId;
                spsiModel.DueDuration = vmodel.DueDuration;
                spsiModel.InstalmentNumber = null;
                spsiModel.InvoiceDate = vmodel.InvoiceDate.GetValue<DateTime>();
                spsiModel.InvoiceNo = vmodel.InvoiceNo;
                spsiModel.InvoiceSerialNo = vmodel.InvoiceSerialNo;
                spsiModel.IsActive = true;
                spsiModel.PayAmount = 0;
                spsiModel.PaymentTypeId = vmodel.PaymentTypeId;
                spsiModel.CommandType = CommonValues.DMLType.Insert;
                spsiBo.DMLSparePartSaleInvoice(UserManager.UserInfo, spsiModel);

                if (spsiModel.ErrorNo > 0)
                {
                    vmodel.ErrorNo = 1;
                    vmodel.ErrorMessage = spsiModel.ErrorMessage;
                }


                model.WaybillNo = vmodel.InvoiceNo;
                model.WaybillSerialNo = vmodel.InvoiceSerialNo;
                model.WaybillDate = vmodel.InvoiceDate;
                model.ShippingAddressId = vmodel.BillingAddressId;
            }

            CustomerAddressIndexViewModel scaModel = new CustomerAddressIndexViewModel();
            scaModel.AddressId = vmodel.ShippingAddressId.GetValue<int>();
            caBo.GetCustomerAddress(UserManager.UserInfo, scaModel);

            SparePartSaleWaybillBL spswBo = new SparePartSaleWaybillBL();
            model.CustomeTaxNo = cModel.TaxNo;
            model.CustomerAddress1 = scaModel.Address1;
            model.CustomerAddress2 = scaModel.Address2;
            model.CustomerAddress3 = scaModel.Address3;
            model.CustomerAddressCityText = scaModel.CityName;
            model.CustomerAddressCountryText = scaModel.CountryName;
            model.CustomerAddressTownText = scaModel.TownName;
            model.CustomerAddressZipCode = scaModel.ZipCode;
            model.CustomerId = vmodel.CustomerId;
            model.CustomerLastName = cModel.CustomerLastName;
            model.CustomerName = cModel.CustomerName;
            model.CustomerPassportNo = cModel.PassportNo;
            model.CustomerTCIdentity = cModel.TcIdentityNo;
            model.CustomerTaxOffice = cModel.TaxOffice;
            model.DealerId = vmodel.DealerId;
            //model.WaybillNo = model.WaybillNo;
            //model.WaybillSerialNo = model.WaybillSerialNo;
            //model.WaybillDate = model.InvoiceDate;
            //model.ShippingAddressId = model.BillingAddressId;
            model.SparePartSaleIdList = string.Join(",", spsIdList.Where(d => d != null).Select(i => i.Value).ToArray());
            model.CommandType = CommonValues.DMLType.Insert;
            if (spsiModel.SparePartSaleInvoiceId != 0)
                model.SparePartSaleInvoiceId = spsiModel.SparePartSaleInvoiceId;
            model.IsActive = true;

            string newId = string.Empty;
            int count = 1;
            // TODO: burada liste çekmek yerine var mı kontrolü yapılmalı.
            while (count != 0)
            {
                newId = CommonBL.GetNewID().Model;
                SparePartSaleWaybillListModel spswListModel = new SparePartSaleWaybillListModel();
                spswListModel.WaybillReferenceNo = newId;
                //TODO: burada ne yapılmak istenmiş kontrol edilmeli???
                spswBo.ListSparePartSaleWaybills(UserManager.UserInfo, spswListModel, out count);
            }
            model.WaybillReferenceNo = newId;

            spswBo.DMLSparePartSaleWaybill(UserManager.UserInfo, model);
            if (model.ErrorNo > 0)
            {
                vmodel.ErrorNo = 1;
                vmodel.ErrorMessage = model.ErrorMessage;
            }
            spsiModel.CommandType = CommonValues.DMLType.Update;
            spsiModel.SparePartSaleWaybillIdList = model.SparePartSaleWaybillId.ToString();
            spsiBo.DMLSparePartSaleInvoice(UserManager.UserInfo, spsiModel);

            int totalCount = 0;
            SparePartSaleBL spsBo = new SparePartSaleBL();
            SparePartSaleDetailBL spsdBo = new SparePartSaleDetailBL();
            SparePartSaleOrderDetailBL spsodBo = new SparePartSaleOrderDetailBL();
            foreach (SelectListItem spsIdItem in spsIdList)
            {
                SparePartSaleDetailListModel spsdListModel = new SparePartSaleDetailListModel();
                spsdListModel.PartSaleId = spsIdItem.Value.GetValue<int>();
                List<SparePartSaleDetailListModel> detailList = spsdBo.ListSparePartSaleDetails(UserManager.UserInfo, spsdListModel, out totalCount).Data;
                foreach (SparePartSaleDetailListModel detailModel in detailList)
                {
                    if (detailModel.SoDetSeqNo.HasValue())
                    {
                        SparePartSaleOrderDetailDetailModel spsodModel = new SparePartSaleOrderDetailDetailModel();
                        spsodModel.SparePartSaleOrderDetailId = detailModel.SoDetSeqNo.GetValue<long>();
                        spsodBo.GetSparePartSaleOrderDetail(UserManager.UserInfo, spsodModel);
                        spsodModel.CommandType = CommonValues.DMLType.Update;
                        spsodModel.ShippedQuantity = spsodModel.ShippedQuantity + (detailModel.PickedQuantity - detailModel.ReturnedQuantity);
                        spsodBo.DMLSparePartSaleOrderDetail(UserManager.UserInfo, spsodModel);
                        if (spsodModel.ErrorNo > 0)
                            vmodel.ErrorNo = 1;
                        vmodel.ErrorMessage = spsodModel.ErrorMessage;
                    }
                }
                SparePartSaleViewModel spsModel = spsBo.GetSparePartSale(UserManager.UserInfo, spsdListModel.PartSaleId.GetValue<int>()).Model;
                spsModel.CommandType = CommonValues.DMLType.Update;
                spsModel.SparePartSaleWaybillId = model.SparePartSaleWaybillId;
                //spsModel.SaleStatusLookVal = model.IsWaybilled ? ((int)CommonValues.SparePartSaleStatus.OrderInvoiced).ToString(
                //    ) : ((int)CommonValues.SparePartSaleStatus.OrderWaybilled).ToString();
                spsBo.DMLSparePartSale(UserManager.UserInfo, spsModel);
                if (spsModel.ErrorNo > 0)
                {
                    vmodel.ErrorNo = 1;
                    vmodel.ErrorMessage = spsModel.ErrorMessage;
                }
            }

        }

        #endregion

        #region OtokarSpare Part Sale Update
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSale.OtokarSparePartSaleIndex, CommonValues.PermissionCodes.SparePartSale.OtokarSparePartSaleUpdate)]
        public ActionResult OtokarSparePartSaleUpdate(int id = 0)
        {
            ViewBag.IsCreate = false;
            FillComboBoxes();
            var model = new OtokarSparePartSaleViewModel { SparePartSaleId = id };
            if (id <= 0) return View(model);
            var bo = new SparePartSaleBL();
            model = bo.GetSparePartSaleOtokar(UserManager.UserInfo, id).Model;
            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSale.OtokarSparePartSaleIndex,
            CommonValues.PermissionCodes.SparePartSale.OtokarSparePartSaleUpdate)]
        public ActionResult OtokarSparePartSaleUpdate(OtokarSparePartSaleViewModel viewModel)
        {
            ViewBag.IsCreate = false;
            //viewModel.WayBilllDate = viewModel.InvoiceDate;
            //viewModel.WayBillNo = viewModel.InvoiceNo;
            //viewModel.WayBillSerialNo = viewModel.InvoiceSerialNo;

            FillComboBoxes();

            var sparePartbo = new SparePartSaleBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Update;
                sparePartbo.DMLSparePartSaleOtokar(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }
            return View(viewModel);
        }

        #endregion

        #region Spare Part Sale Details
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSale.OtokarSparePartSaleIndex, CommonValues.PermissionCodes.SparePartSale.OtokarSparePartSaleDetails)]
        public ActionResult OtokarSparePartSaleDetails(int id = 0)
        {
            var bo = new SparePartSaleBL();
            var model = bo.GetSparePartSale(UserManager.UserInfo, id).Model;
            CheckErrorForMessage(model, false);
            return View(model);
        }
        #endregion

        #region Spare Part Sale Invoice
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSale.SparePartSaleIndex,
            CommonValues.PermissionCodes.SparePartSale.SparePartSaleInvoice)]
        public ActionResult SparePartSaleInvoice(int id = 0)
        {
            FillComboBoxes();
            var model = new SparePartSaleViewModel { SparePartSaleId = id };
            if (id <= 0) return View(model);
            var bo = new SparePartSaleBL();
            model = bo.GetSparePartSale(UserManager.UserInfo, id).Model;
            model.StockTypeId =
                CommonBL.GetGeneralParameterValue(CommonValues.GeneralParameters.StockType).Model.GetValue<int>();
            model.TransmitNo = id.GetValue<string>();

            if (model.PaymentTypeId != 0 && model.PaymentTypeId != null)
            {
                List<PaymentTypeListModel> ptList = bo.ListPaymentTypes(UserManager.UserInfo).Data;
                var control = (from e in ptList.AsEnumerable()
                               where e.Id == model.PaymentTypeId
                               select e);
                if (control.Any())
                {
                    PaymentTypeListModel ptModel = control.First();
                    model.BankRequired = ptModel.BankRequired;
                    model.InstalmentNumberRequired = ptModel.InstalmentRequired;
                    model.DefermentNumberRequired = ptModel.DefermentRequired;
                }
            }

            ViewBag.IsUpdated = model.InvoiceNo != string.Empty;

            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSale.SparePartSaleIndex,
            CommonValues.PermissionCodes.SparePartSale.SparePartSaleInvoice)]
        public ActionResult SparePartSaleInvoice(SparePartSaleViewModel viewModel)
        {
            ViewBag.IsUpdated = false;
            FillComboBoxes();

            var sparePartbo = new SparePartSaleBL();
            SparePartSaleViewModel updated = sparePartbo.GetSparePartSale(UserManager.UserInfo, viewModel.SparePartSaleId).Model;

            if (Request.Params["action:SparePartSaleUpdate"] != null)
            {
                if (ModelState.IsValid)
                {
                    int totalCount = 0;
                    SparePartSaleListModel spsListModel = new SparePartSaleListModel();
                    spsListModel.InvoiceNo = viewModel.InvoiceNo;
                    spsListModel.InvoiceSerialNo = viewModel.InvoiceSerialNo;
                    List<SparePartSaleListModel> list = sparePartbo.ListSparePartSales(UserManager.UserInfo, spsListModel, out totalCount).Data;
                    var control = (from e in list
                                   where e.SparePartSaleId != viewModel.SparePartSaleId
                                   select e);
                    if (!control.Any())
                    {
                        updated.InvoiceDate = viewModel.InvoiceDate;
                        updated.InvoiceNo = viewModel.InvoiceNo;
                        updated.InvoiceSerialNo = viewModel.InvoiceSerialNo;
                        updated.PaymentTypeId = viewModel.PaymentTypeId;
                        updated.BankId = viewModel.BankId;
                        updated.DueDuration = viewModel.DueDuration;
                        updated.InstallmentNumber = viewModel.InstallmentNumber ?? 0;
                        updated.TransmitNo = viewModel.TransmitNo;
                        updated.WayBilllDate = viewModel.IsPrintActualDispatchDate ? (DateTime?)DateTime.Now : null;
                        updated.CommandType = CommonValues.DMLType.Update;
                        sparePartbo.DMLSparePartSale(UserManager.UserInfo, updated);
                        CheckErrorForMessage(updated, true);
                        ViewBag.IsUpdated = true;
                        SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);
                    }
                    else
                    {
                        SetMessage(MessageResource.SparePartSale_Warning_SameInvoiceNoExists, CommonValues.MessageSeverity.Fail);
                    }
                }
            }
            if (Request.Params["action:SparePartSaleInvoice"] != null)
            {
                return PrintSparePartReportReal(viewModel.SparePartSaleId, updated.InvoiceNo);
            }
            if (Request.Params["action:SparePartSaleInvoiceReport"] != null)
            {
                return PrintSparePartSaleReportCopy(viewModel.SparePartSaleId, updated.InvoiceNo);
            }
            if (Request.Params["action:SparePartSaleInvoiceProformaReport"] != null)
            {
                return PrintSparePartSaleReportProforma(viewModel.SparePartSaleId, updated.InvoiceNo);
            }

            return View(viewModel);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSale.SparePartSaleIndex,
            CommonValues.PermissionCodes.SparePartSale.SparePartSaleInvoice)]
        public ActionResult SparePartSaleInvoiceConfirm(int sparePartSaleId)
        {
            var sparePartbo = new SparePartSaleBL();
            //NOTE:Task 26153 e istinaden yapıldı => TANER
            string errorMessage = sparePartbo.ExecInvoiceOp(UserManager.UserInfo, sparePartSaleId).Model;
            if (errorMessage != null)
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, errorMessage);
            }
            /*
             * Faturalandırma işlemi yapıldıktan sonra 
             * SUPPLIER_DEALER_CONFIRM alanı 2 ye set edilecek ve quantity bakılmaksızın ilgili kayıtların  purchase_order_mst 
             * (1047 - 2 kapalı sipariş) ve purchase_order_det (1046-1 kapalı sipariş) kayıtları kapalı siparişe çekilecek.
             */
            SparePartSaleViewModel updated = sparePartbo.GetSparePartSale(UserManager.UserInfo, sparePartSaleId).Model;
            updated.CommandType = CommonValues.DMLType.Update;
            updated.SaleStatusLookVal =
                ((int)CommonValues.SparePartSaleStatus.OrderInvoiced).ToString();
            sparePartbo.DMLSparePartSale(UserManager.UserInfo, updated);
            CheckErrorForMessage(updated, true);
            if (updated.ErrorNo > 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, updated.ErrorMessage);

            PurchaseOrderBL poBo = new PurchaseOrderBL();
            PurchaseOrderViewModel poModel = new PurchaseOrderViewModel();
            poModel.PoNumber = updated.PoNumber;
            poBo.GetPurchaseOrder(UserManager.UserInfo, poModel);
            poModel.CommandType = CommonValues.DMLType.Update;
            poModel.SupplierDealerConfirm = (int)CommonValues.SupplierDealerConfirmType.ApprovedOrder;
            poModel.Status = (int)CommonValues.PurchaseOrderStatus.ClosePurchaseOrder;
            poBo.DMLPurchaseOrder(UserManager.UserInfo, poModel);

            // TFS No : 27807 OYA 02.01.2015
            DealerBL dBo = new DealerBL();
            DealerViewModel dealerModel = dBo.GetDealer(UserManager.UserInfo, poModel.IdDealer.GetValue<int>()).Model;
            string to = dealerModel.ContactEmail;
            string subject = string.Format(MessageResource.SparePartSale_Mail_Subject, poModel.PoNumber);
            DealerViewModel supplierDealerModel = dBo.GetDealer(UserManager.UserInfo, poModel.SupplierIdDealer.GetValue<int>()).Model;
            string body = string.Format(MessageResource.SparePartSale_Mail_Body, poModel.PoNumber, supplierDealerModel.Name);
            CommonBL.SendDbMail(to, subject, body);

            if (poModel.ErrorNo == 0)
            {
                int detailCount = 0;
                PurchaseOrderDetailBL poDetBo = new PurchaseOrderDetailBL();
                PurchaseOrderDetailListModel detModel = new PurchaseOrderDetailListModel();
                detModel.PurchaseOrderNumber = poModel.PoNumber.GetValue<int>();
                List<PurchaseOrderDetailListModel> detailList = poDetBo.ListPurchaseOrderDetails(UserManager.UserInfo, detModel,
                                                                                                 out detailCount).Data;
                if (detailCount > 0)
                {
                    int totalCount = 0;
                    SparePartSaleDetailListModel spsdListModel = new SparePartSaleDetailListModel();
                    spsdListModel.PartSaleId = sparePartSaleId;
                    SparePartSaleDetailBL spsdBo = new SparePartSaleDetailBL();
                    List<SparePartSaleDetailListModel> spsList = spsdBo.ListSparePartSaleDetails(UserManager.UserInfo, spsdListModel, out totalCount).Data;

                    foreach (PurchaseOrderDetailListModel purchaseOrderDetailListModel in detailList)
                    {
                        PurchaseOrderDetailViewModel detailModel = new PurchaseOrderDetailViewModel();
                        detailModel.PurchaseOrderNumber = purchaseOrderDetailListModel.PurchaseOrderNumber;
                        detailModel.PurchaseOrderDetailSeqNo = purchaseOrderDetailListModel.PurchaseOrderDetailSeqNo;
                        poDetBo.GetPurchaseOrderDetail(UserManager.UserInfo, detailModel);
                        detailModel.CommandType = CommonValues.DMLType.Update;
                        detailModel.StatusId = (int)CommonValues.PurchaseOrderDetailStatus.Closed;

                        /* TFS No : 27693 OYA 23.12.2014
                         - Faturalandır denildiğinde SPARE_PART_SALE.PO_NUMBER değeri ve ID_PART ile PURCHASE_ORDER_DET tablosuna gidilir 
                         * ve PURCHASE_ORDER_DET.SHIP_QUANT alanına SPARE_PARTS_SALE_DETAIL.PICK_QUANT değeri set edilir.
                        */
                        var control = (from e in spsList.AsEnumerable()
                                       where e.SparePartId == detailModel.PartId
                                       select e);
                        if (control.Any())
                        {
                            detailModel.ShipmentQuantity = control.ElementAt(0).PickQuantity;
                        }

                        poDetBo.DMLPurchaseOrderDetail(UserManager.UserInfo, detailModel);
                        if (detailModel.ErrorNo > 0)
                        {
                            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, detailModel.ErrorMessage);
                        }
                    }
                }
            }
            else
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, poModel.ErrorMessage);
            }

            return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
        }
        #endregion
    }
}
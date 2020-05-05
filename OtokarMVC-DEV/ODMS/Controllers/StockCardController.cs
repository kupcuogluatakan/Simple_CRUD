using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.Dealer;
using ODMSModel.DealerStartupInventoryLevel;
using ODMSModel.DownloadFileActionResult;
using ODMSModel.SparePart;
using ODMSModel.StockCard;
using System.Web.Mvc;
using System.Linq;
using System.Web.Configuration;
using ODMS.OtokarService;
using ODMSModel.StockCardPriceListModel;
using ODMSModel.ListModel;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class StockCardController : ControllerBase
    {
        #region General Methods
        private void SetDefaults(bool isDealerRemoved)
        {
            ViewBag.WarehouseList = WarehouseBL.ListWarehousesOfDealerAsSelectList(UserManager.UserInfo.GetUserDealerId()).Data;
            List<SelectListItem> dealerList = DealerBL.ListDealerAsSelectListItem().Data;

            //if (UserManager.UserInfo.GetUserDealerId() != 0 && isDealerRemoved)
            //{
            //    SelectListItem removed = (from r in dealerList.AsEnumerable()
            //                              where r.Value.GetValue<int>() == UserManager.UserInfo.GetUserDealerId()
            //                              select r).First();
            //    dealerList.Remove(removed);
            //}
            ViewBag.DealerList = dealerList;
            ViewBag.StockLocationList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.StockLocationLookup).Data;
            ViewBag.StockTypeList = CommonBL.ListStockTypes(UserManager.UserInfo).Data;

            int totalDealerRegionCount = 0;
            List<SelectListItem> dealerRegionList = new DealerRegionBL().ListDealerRegions(UserManager.UserInfo, new DealerRegionListModel(), out totalDealerRegionCount).Data.Select(s =>
            new SelectListItem
            {
                Text = s.DealerRegionName,
                Value = s.DealerRegionId.ToString()
            }).ToList();

            ViewBag.DealerRegionList = dealerRegionList;

        }
        [ValidateAntiForgeryToken]
        public JsonResult GetSparePartInfo(string partId)
        {
            if (!string.IsNullOrEmpty(partId))
            {
                SparePartIndexViewModel spModel = new SparePartIndexViewModel() { PartId = partId.GetValue<int>() };
                SparePartBL bo = new SparePartBL();
                bo.GetSparePart(UserManager.UserInfo, spModel);

                return Json(new { IsOriginalPart = spModel.IsOriginal, UnitName = spModel.UnitName });
            }
            else
            {
                return null;
            }
        }
        [HttpGet]
        public JsonResult ListRacks(int? id)
        {
            if (id.HasValue)
            {
                List<SelectListItem> rackList = CommonBL.ListRacks(UserManager.UserInfo, id.Value).Data;
                rackList.Insert(0, new SelectListItem() { Text = MessageResource.Global_Display_Choose, Value = "0" });

                return Json(rackList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region StockCardSearch Index

        public ActionResult ExcelSample()
        {
            var bo = new StockCardBL();
            var ms = bo.SetExcelReport(null, string.Empty);
            return File(ms, CommonValues.ExcelContentType, MessageResource.StockCard_PageTitle_Search + CommonValues.ExcelExtOld);
        }
        [AuthorizationFilter(CommonValues.PermissionCodes.StockCard.StockCardSearch)]
        [HttpGet]
        public ActionResult StockCardSearch()
        {
            SetDefaults(true);
            var model = new StockCardSearchListModel
            {
                StockLocationId = 1
            };
            if (UserManager.UserInfo.GetUserDealerId() != 0)
                model.DealerId = UserManager.UserInfo.GetUserDealerId();



            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.StockCard.StockCardSearch, CommonValues.PermissionCodes.StockCard.StockCardSearch)]
        [HttpPost]
        public ActionResult StockCardSearch(StockCardSearchListModel model, HttpPostedFileBase excelFile)
        {
            SetDefaults(true);
            if (model.StockLocationId == 1 && UserManager.UserInfo.GetUserDealerId() > 0)
            {
                model.DealerId = UserManager.UserInfo.GetUserDealerId();
            }

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
                        //TempData[CommonValues.StockSearchPartCodesKey] = partCodes.ToString();
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

        [AuthorizationFilter(CommonValues.PermissionCodes.StockCard.StockCardSearch, CommonValues.PermissionCodes.StockCard.StockCardSearch)]
        public ActionResult ListStockCardSearch([DataSourceRequest] DataSourceRequest request, StockCardSearchListModel model)
        {
            var stockTypeDetailBo = new StockCardBL();
            var v = new StockCardSearchListModel(request);
            if (model.StockLocationId == 1 && UserManager.UserInfo.GetUserDealerId() > 0)
            {
                v.DealerId = UserManager.UserInfo.GetUserDealerId();
            }
            else
            {
                v.DealerId = model.DealerId;
            }
            v.DealerIdsForCentral = model.DealerIdsForCentral;
            v.DealerRegionIds = model.DealerRegionIds;
            v.DealerRegionType = model.DealerRegionType;
            v.PartCode = model.PartCode;
            v.PartName = model.PartName;
            v.StockTypeId = model.StockTypeId;
            v.StockLocationId = model.StockLocationId;
            v.UnitName = model.UnitName;
            v.PartCodeList = model.PartCodeList;
            v.CurrentDealerId = ((!UserManager.UserInfo.IsDealer) ? null : (int?)UserManager.UserInfo.GetUserDealerId());
            v.IsHq = !UserManager.UserInfo.IsDealer;

            var totalCnt = 0;
            var returnValue = stockTypeDetailBo.ListStockCardSearch(UserManager.UserInfo, v, out totalCnt).Data;


            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.StockCard.StockCardSearch, CommonValues.PermissionCodes.StockCard.StockCardSearch)]
        public JsonResult GetStockCardStockValues(StockCardStockValueModel model)
        {
            var stockCardBL = new StockCardBL();

            if (model.StockLocationId == 1 && UserManager.UserInfo.GetUserDealerId() > 0)
            {
                model.DealerId = UserManager.UserInfo.GetUserDealerId();
            }


            model.CurrentDealerId = ((!UserManager.UserInfo.IsDealer) ? null : (int?)UserManager.UserInfo.GetUserDealerId());
            model.IsHq = !UserManager.UserInfo.IsDealer;

            var priceValues = stockCardBL.GetStockCardStockValues(UserManager.UserInfo, model);

            return Json(new { TotalStockValues = priceValues.Model });

        }



        #endregion




        #region StockCard Index
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.StockCard.StockCardIndex)]
        public ActionResult StockCardIndex(int? partId, int? dealerId)
        {
            SetDefaults(false);

            StockCardViewModel model = new StockCardViewModel();
            if (dealerId == null)
            {
                if (UserManager.UserInfo.GetUserDealerId() > 0)
                {
                    model.DealerId = UserManager.UserInfo.GetUserDealerId();
                }
            }
            else
            {
                if (UserManager.UserInfo.GetUserDealerId() > 0)
                {
                    model.DealerId = UserManager.UserInfo.GetUserDealerId();
                }
                else
                {
                    model.DealerId = dealerId;
                }
            }
            if (model.DealerId != null)
            {
                DealerBL dBo = new DealerBL();
                DealerViewModel dModel = dBo.GetDealer(UserManager.UserInfo, model.DealerId.GetValue<int>()).Model;
                model.DealerName = dModel.Name;

                if (partId != null)
                {
                    model.PartId = partId;
                    SparePartBL partBo = new SparePartBL();
                    SparePartIndexViewModel partModel = new SparePartIndexViewModel { PartId = partId.GetValue<int>() };
                    partBo.GetSparePart(UserManager.UserInfo, partModel);

                    model.PartName = partModel.PartNameInLanguage;
                    model.IsOriginalPart = partModel.IsOriginal.GetValue<bool>();
                    model.PartCode = partModel.PartCode;
                    model.LeadTime = partModel.LeadTime;
                    model.Volume = partModel.Volume;
                    model.Weight = partModel.Weight;
                    model.VatRatio = partModel.VatRatio.GetValue<decimal>();
                    model.UnitName = partModel.UnitName;
                    model.AlternatePart = partModel.AlternatePart;

                    int alternatePartId = 0;
                    if (model.AlternatePart != "")
                    {
                        SparePartIndexViewModel alternatePartModel = new SparePartIndexViewModel { PartCode = model.AlternatePart };
                        partBo.GetSparePart(UserManager.UserInfo, alternatePartModel);
                        alternatePartId = alternatePartModel.PartId;
                    }

                    StockCardPriceListBL _stockCardPriceListService = new StockCardPriceListBL();
                    StockCardPriceListModel _stockCardPriceListModel = new StockCardPriceListModel();
                    _stockCardPriceListModel.DealerId = UserManager.UserInfo.GetUserDealerId();
                    _stockCardPriceListModel.PartId = alternatePartId;
                    _stockCardPriceListService.Select(UserManager.UserInfo, _stockCardPriceListModel);
                    var priceList = _stockCardPriceListModel.PriceList;
                    model.CostPrice = _stockCardPriceListService.Get(UserManager.UserInfo, _stockCardPriceListModel, CommonValues.StockCardPriceType.D).Model.CostPrice;

                    int totalCountDR = 0;
                    SparePartBL spBo = new SparePartBL();
                    SparePartSupplyDiscountRatioListModel discountRatioModel =
                        new SparePartSupplyDiscountRatioListModel();
                    discountRatioModel.PartId = partId.GetValue<long>();
                    List<SparePartSupplyDiscountRatioListModel> discountRatioList = spBo.ListSparePartsSupplyDiscountRatios(UserManager.UserInfo, discountRatioModel, out totalCountDR).Data;
                    if (totalCountDR != 0)
                    {
                        var query = (from e in discountRatioList.AsEnumerable()
                                     where e.ChannelName == dModel.SaleChannelCode
                                     select e.DiscountRatio);
                        if (query.Any())
                        {
                            model.SaleChannelDiscountRatio = query.First();
                        }
                        else
                        {
                            model.SaleChannelDiscountRatio = 0;
                        }
                    }

                    int totalCount = 0;
                    DealerStartupInventoryLevelListModel dsilModel = new DealerStartupInventoryLevelListModel();
                    dsilModel.PartId = model.PartId;
                    dsilModel.DealerClassCode = dModel.DealerClassCode;
                    DealerStartupInventoryLevelBL dsilBo = new DealerStartupInventoryLevelBL();
                    List<DealerStartupInventoryLevelListModel> list = dsilBo.ListDealerStartupInventoryLevels(UserManager.UserInfo, dsilModel, out totalCount).Data;
                    if (list.Any())
                    {
                        model.StartupStockQty = list.ElementAt(0).Quantity;
                    }

                    if (model.IsOriginalPart && !General.IsTest)
                    {
                        using (var pssc = GetClient())
                        {
                            string psUser = WebConfigurationManager.AppSettings["PSSCUser"];
                            string psPassword = WebConfigurationManager.AppSettings["PSSCPass"];

                            var rValue = pssc.ZYP_SD_WEB_MATERIAL_ATP2(psUser, psPassword, model.PartCode);
                            DataTable dt = rValue.Tables["Table1"];
                            if (dt == null || dt.Rows.Count == 0)
                            {
                                model.StockServiceValue = null;
                            }
                            else
                            {
                                if (dt.Columns.Contains("MENGX"))
                                {
                                    model.StockServiceValue = dt.Rows[0]["MENGX"].GetValue<string>();
                                }
                                else
                                {
                                    model.StockServiceValue = "Stok kolonu servisten dönmemektedir";
                                }
                            }

                        }
                    }
                }

                StockCardViewModel scModel = new StockCardViewModel();
                scModel.PartId = model.PartId;
                scModel.DealerId = model.DealerId;
                StockCardBL boStockCard = new StockCardBL();
                boStockCard.GetStockCard(UserManager.UserInfo, scModel);
                if (scModel.StockCardId > 0)
                {
                    model.PartCode = scModel.PartCode + CommonValues.Slash + scModel.PartName;
                    model.StockCardId = scModel.StockCardId;
                    model.SalePrice = scModel.SalePrice;
                    model.MinStockQuantity = scModel.MinStockQuantity;
                    model.MaxStockQuantity = scModel.MaxStockQuantity;
                    model.ProfitMarginRatio = scModel.ProfitMarginRatio;
                    model.WarehouseId = scModel.WarehouseId;
                    model.RackId = scModel.RackId;
                    model.MinSaleQuantity = scModel.MinSaleQuantity;
                    model.AutoOrder = scModel.AutoOrder;
                    model.AvgDealerPrice = scModel.AvgDealerPrice;
                    model.VatRatio = scModel.VatRatio;
                    model.CriticalStockQuantity = scModel.CriticalStockQuantity;
                    model.StockQuantity = scModel.StockQuantity;
                    model.LastPrice = scModel.LastPrice;
                    model.AlternatePart = scModel.AlternatePart;
                }
            }

            return View(model);
        }


        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.StockCard.StockCardIndex,
            CommonValues.PermissionCodes.StockCard.StockCardCreate)]
        public ActionResult StockCardIndex(StockCardViewModel model)
        {
            SetDefaults(false);

            DealerBL dBo = new DealerBL();
            DealerViewModel dModel = dBo.GetDealer(UserManager.UserInfo, model.DealerId.GetValue<int>()).Model;
            var bo = new StockCardBL();

            ModelState.Remove("MinStockQuantity");
            ModelState.Remove("MaxStockQuantity");
            ModelState.Remove("MinSaleQuantity");
            ModelState.Remove("ProfitMarginRatio");
            ModelState.Remove("ProfitMarginRatio");
            ModelState.Remove("CriticalStockQuantity");
            ModelState.Remove("SaleChannelDiscountRatio");
            ModelState.Remove("StockQuantity");
            ModelState.Remove("LastPrice");
            StockCardViewModel existedModel = new StockCardViewModel
            {
                PartId = model.PartId,
                DealerId = model.DealerId
            };
            existedModel = bo.GetStockCard(UserManager.UserInfo, existedModel).Model;
            if (existedModel.StockCardId == 0)
            {
                model.CommandType = CommonValues.DMLType.Insert;
                ModelState.Remove("AvgDealerPrice");
            }
            else
            {
                model.CommandType = CommonValues.DMLType.Update;
            }

            int totalCountDR = 0;
            SparePartBL spBo = new SparePartBL();
            SparePartSupplyDiscountRatioListModel discountRatioModel =
                new SparePartSupplyDiscountRatioListModel();
            discountRatioModel.PartId = model.PartId.GetValue<long>();
            List<SparePartSupplyDiscountRatioListModel> discountRatioList = spBo.ListSparePartsSupplyDiscountRatios(UserManager.UserInfo, discountRatioModel, out totalCountDR).Data;
            if (totalCountDR != 0)
            {
                decimal discountRatio = (from e in discountRatioList.AsEnumerable()
                                         where e.ChannelName == dModel.SaleChannelCode
                                         select e.DiscountRatio).Any().GetValue<decimal>();
                model.SaleChannelDiscountRatio = discountRatio;
            }
            int totalCount = 0;
            DealerStartupInventoryLevelListModel dsilModel = new DealerStartupInventoryLevelListModel();
            dsilModel.PartId = model.PartId;
            dsilModel.DealerClassCode = dModel.DealerClassCode;
            DealerStartupInventoryLevelBL dsilBo = new DealerStartupInventoryLevelBL();
            List<DealerStartupInventoryLevelListModel> list = dsilBo.ListDealerStartupInventoryLevels(UserManager.UserInfo, dsilModel, out totalCount).Data;
            if (list.Any())
            {
                model.StartupStockQty = list.ElementAt(0).Quantity;
            }

            if (!model.CalculatedPrice)
            {
                SetMessage(MessageResource.StockCard_Warning_SalesPriceMustBeGreaterThanCalculatedPrice, CommonValues.MessageSeverity.Fail);
            }

            if (ModelState.IsValid)
            {
                bo.DMLStockCard(UserManager.UserInfo, model);
                if (model.ErrorNo == 0)
                {
                    ModelState.Clear();
                    SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);
                }
                else
                {
                    SetMessage(model.ErrorMessage, CommonValues.MessageSeverity.Fail);
                }
            }
            return RedirectToAction("StockCardIndex", new { partId = model.PartId, dealerId = model.DealerId });
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.StockCard.StockCardIndex, CommonValues.PermissionCodes.StockCard.StockCardDetails)]
        public ActionResult ListStockCard([DataSourceRequest]DataSourceRequest request, StockCardListModel model)
        {
            SetDefaults(false);
            var bo = new StockCardBL();
            var v = new StockCardListModel(request) { DealerId = model.DealerId, PartId = model.PartId };
            var totalCnt = 0;
            var returnValue = bo.ListStockCards(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }
        #endregion

        #region StockCard Create
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.StockCard.StockCardIndex, CommonValues.PermissionCodes.StockCard.StockCardCreate)]
        public ActionResult StockCardCreate()
        {
            SetDefaults(false);
            StockCardViewModel model = new StockCardViewModel();
            if (UserManager.UserInfo.GetUserDealerId() > 0)
            {
                model.DealerId = UserManager.UserInfo.GetUserDealerId();
                List<SelectListItem> dealerList = DealerBL.ListDealerAsSelectListItem().Data;
                string dealerName = (from r in dealerList.AsEnumerable()
                                     where r.Value == model.DealerId.GetValue<string>()
                                     select r.Text).First();
                model.DealerName = dealerName;
            }
            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.StockCard.StockCardIndex, CommonValues.PermissionCodes.StockCard.StockCardCreate)]
        public ActionResult StockCardCreate(StockCardViewModel model)
        {
            SetDefaults(false);

            if (model.PartId != null && model.PartId != 0)
            {
                SparePartIndexViewModel spModel = new SparePartIndexViewModel { PartId = model.PartId.GetValue<int>() };
                SparePartBL spBo = new SparePartBL();
                spBo.GetSparePart(UserManager.UserInfo, spModel);
                model.PartName = spModel.PartCode + CommonValues.Slash + spModel.PartNameInLanguage;
                model.IsOriginalPart = spModel.IsOriginal.GetValue<bool>();
            }
            if (ModelState.IsValid)
            {
                var bo = new StockCardBL();
                StockCardViewModel existedModel = new StockCardViewModel
                {
                    PartId = model.PartId,
                    DealerId = model.DealerId
                };
                existedModel = bo.GetStockCard(UserManager.UserInfo, existedModel).Model;
                if (existedModel.StockCardId == 0)
                {
                    model.CommandType = CommonValues.DMLType.Insert;
                    bo.DMLStockCard(UserManager.UserInfo, model);
                    CheckErrorForMessage(model, true);
                    ModelState.Clear();
                    model = new StockCardViewModel { DealerId = existedModel.DealerId };
                }
                else
                {
                    SetMessage(MessageResource.StockCard_Warning_DuplicateValue, CommonValues.MessageSeverity.Fail);
                }
            }
            return View(model);
        }
        #endregion

        #region StockCard Update
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.StockCard.StockCardIndex, CommonValues.PermissionCodes.StockCard.StockCardUpdate)]
        public ActionResult StockCardUpdate(int id = 0)
        {
            SetDefaults(false);

            var referenceModel = new StockCardViewModel();
            if (id > 0)
            {
                var bo = new StockCardBL();
                referenceModel.StockCardId = id;
                referenceModel = bo.GetStockCard(UserManager.UserInfo, referenceModel).Model;
            }
            return View(referenceModel);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.StockCard.StockCardIndex, CommonValues.PermissionCodes.StockCard.StockCardUpdate)]
        public ActionResult StockCardUpdate(StockCardViewModel viewModel)
        {
            SetDefaults(false);

            var bo = new StockCardBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Update;
                bo.DMLStockCard(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }
            return RedirectToAction("StockCardIndex", new { partId = viewModel.PartId, dealerId = viewModel.DealerId });
        }
        #endregion

        #region StockCard Details
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.StockCard.StockCardIndex, CommonValues.PermissionCodes.StockCard.StockCardDetails)]
        public ActionResult StockCardDetails(int id = 0)
        {
            var referenceModel = new StockCardViewModel { StockCardId = id };
            var bo = new StockCardBL();

            var model = bo.GetStockCard(UserManager.UserInfo, referenceModel).Model;

            return View(model);
        }
        #endregion

        [HttpPost]
        public JsonResult CheckStockCard(int partId, int dealerId)
        {
            var bl = new StockCardBL();
            StockCardViewModel model = new StockCardViewModel
            {
                PartId = partId,
                DealerId = dealerId
            };
            model = bl.GetStockCard(UserManager.UserInfo, model).Model;

            return Json(new { Id = model.StockCardId });
        }

    }
}
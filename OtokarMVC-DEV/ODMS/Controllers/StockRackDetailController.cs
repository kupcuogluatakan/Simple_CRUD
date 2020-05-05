using System.Collections.Generic;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.Rack;
using ODMSModel.StockRackDetail;
using System.Web.Mvc;
using System.Linq;
using ODMSModel.StockTypeDetail;
using System;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class StockRackDetailController : ControllerBase
    {
        private void SetDefaults()
        {
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
        }

        #region Empty StockRackDetail Index

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.StockRackDetail.EmptyStockRackDetailIndex)]
        [HttpGet]
        public ActionResult EmptyStockRackDetailIndex()
        {
            SetDefaults();
            StockRackDetailListModel model = new StockRackDetailListModel();
            if (UserManager.UserInfo.GetUserDealerId() != 0)
                model.IdDealer = UserManager.UserInfo.GetUserDealerId();

            return View(model);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.StockRackDetail.EmptyStockRackDetailIndex,
            ODMSCommon.CommonValues.PermissionCodes.StockRackDetail.EmptyStockRackDetailIndex)]
        public ActionResult ListEmptyStockRackDetail([DataSourceRequest] DataSourceRequest request, StockRackDetailListModel model)
        {
            var stockRackDetailBo = new StockRackDetailBL();

            var v = new StockRackDetailListModel(request)
            {
                IdDealer = model.IdDealer,
                RackName = model.RackName,
                WarehouseName = model.WarehouseName
            };

            var totalCnt = 0;
            var returnValue = stockRackDetailBo.ListEmptyStockRackDetail(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region StockRackDetail Index

        [AuthorizationFilter(CommonValues.PermissionCodes.StockRackDetail.StockRackDetailIndex)]
        [HttpGet]
        public ActionResult StockRackDetailIndex(int? dealerId, int? id)
        {
            SetDefaults();
            StockRackDetailListModel model = new StockRackDetailListModel();
            if (dealerId != null && id != null)
            {
                model.IdDealer = dealerId;
                model.PartId = id;
            }
            else
            {
                if (UserManager.UserInfo.GetUserDealerId() != 0)
                {
                    model.IdDealer = UserManager.UserInfo.GetUserDealerId();
                }
            }
            return PartialView(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.StockRackDetail.StockRackTypeDetailIndex)]
        [HttpGet]
        public ActionResult StockRackTypeDetailIndex(int? dealerId, int? id)
        {
            SetDefaults();
            StockRackTypeDetailListModel model = new StockRackTypeDetailListModel();
            if (dealerId != null && id != null)
            {
                model.IdDealer = dealerId;
                model.PartId = id;
            }
            else
            {
                if (UserManager.UserInfo.GetUserDealerId() != 0)
                {
                    model.IdDealer = UserManager.UserInfo.GetUserDealerId();
                }
            }
            return PartialView(model);
        }


        [AuthorizationFilter(CommonValues.PermissionCodes.StockRackDetail.StockRackDetailIndex,
            CommonValues.PermissionCodes.StockRackDetail.StockRackDetailIndex)]
        public ActionResult ListStockRackDetail([DataSourceRequest] DataSourceRequest request, StockRackDetailListModel model)
        {
            var stockRackDetailBo = new StockRackDetailBL();

            var v = new StockRackDetailListModel(request) { IdDealer = model.IdDealer, PartId = model.PartId };

            var totalCnt = 0;
            var returnValue = stockRackDetailBo.ListStockRackDetail(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }


        [AuthorizationFilter(CommonValues.PermissionCodes.StockRackDetail.StockRackTypeDetailIndex,
            CommonValues.PermissionCodes.StockRackDetail.StockRackTypeDetailIndex)]
        public ActionResult ListStockRackTypeDetail([DataSourceRequest] DataSourceRequest request, StockRackTypeDetailListModel model)
        {
            var stockRackDetailBo = new StockRackDetailBL();

            var v = new StockRackTypeDetailListModel(request) { IdDealer = model.IdDealer, PartId = model.PartId };

            var totalCnt = 0;
            var returnValue = stockRackDetailBo.ListStockRackTypeDetail(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }


        #endregion

        #region Stock Exchange Index

        [HttpGet]
        public JsonResult GetMaxQuantity(string partId, string stockTypeId, string fromRackId)
        {
            decimal maxQuantity = 0;

            RackDetailModel rackModel = new RackDetailModel();
            rackModel.Id = fromRackId.GetValue<int>();
            RackBL rackBo = new RackBL();
            rackModel = rackBo.GetRack(rackModel).Model;

            // Metacortext 6.5.19 artık bu ife düşecek yalnızca.
            if (!string.IsNullOrEmpty(partId) && !string.IsNullOrEmpty(stockTypeId))
            {

                StockRackDetailBL bo = new StockRackDetailBL();

                var s = bo.GetMovableQuantity(UserManager.UserInfo.GetUserDealerId(), Convert.ToInt64(partId), Convert.ToInt32(stockTypeId), Convert.ToInt32(fromRackId));
                maxQuantity = s.Model;


                //int totalCount = 0;
                //StockTypeDetailListModel listModel = new StockTypeDetailListModel();
                //listModel.IdStockType = stockTypeId.GetValue<int>();
                //listModel.IdPart = partId.GetValue<long>();
                //listModel.IdWarehouse = rackModel.WarehouseId;
                //listModel.IdDealer = UserManager.UserInfo.GetUserDealerId();

                //StockTypeDetailBL bo = new StockTypeDetailBL();
                //List<StockTypeDetailListModel> stockTypeList = bo.ListStockTypeDetail(UserManager.UserInfo, listModel, out totalCount).Data.Where(c => c.IdWarehouse == rackModel.WarehouseId).ToList();

                //if (totalCount > 0)
                //{
                //    decimal quantity = stockTypeList.ElementAt(0).TypeQuantity;
                //    decimal blockQuantity = stockTypeList.ElementAt(0).BlockQuantity;
                //    decimal reserveQuantity = stockTypeList.ElementAt(0).ReserveQuantity;

                //    maxQuantity = quantity - blockQuantity - reserveQuantity;
                //}
            }    //stcok rack dedaile gitmesi lazım
            else
            {
                // maxQuantity = new StockRackDetailBL().GetQuantity(fromRackId.GetValue<long>(), partId.GetValue<long>()).Model;
                maxQuantity = 0;
            }
            return Json(maxQuantity, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListStockTypes(string partId, string fromRackId, string toRackId)
        {
            int fromWarehouseId = 0;
            int toWarehouseId = 0;

            List<SelectListItem> stockTypeSelectList = new List<SelectListItem>();

            // ibrahim 8.5.19 / torack parametresi gereksiz.
            if (!string.IsNullOrEmpty(partId) && !string.IsNullOrEmpty(fromRackId) /*&& !string.IsNullOrEmpty(toRackId)*/)
            {
                RackBL rackBo = new RackBL();
                RackDetailModel fromRackModel = new RackDetailModel { Id = fromRackId.GetValue<int>() };
                fromRackModel = rackBo.GetRack(fromRackModel).Model;
                fromWarehouseId = fromRackModel.WarehouseId.GetValue<int>();

                RackDetailModel toRackModel = new RackDetailModel { Id = toRackId.GetValue<int>() };
                toRackModel = rackBo.GetRack(toRackModel).Model;
                toWarehouseId = toRackModel.WarehouseId.GetValue<int>();

                // if (fromRackModel.WarehouseId != toRackModel.WarehouseId)
                // {
                int totalCount = 0;
                StockTypeDetailListModel listModel = new StockTypeDetailListModel();
                listModel.IdDealer = UserManager.UserInfo.GetUserDealerId();
                listModel.IdPart = partId.GetValue<int>();
                listModel.IdWarehouse = fromRackModel.WarehouseId;
                StockTypeDetailBL bo = new StockTypeDetailBL();
                List<StockTypeDetailListModel> stockTypeList = bo.ListStockTypeDetail(UserManager.UserInfo, listModel, out totalCount).Data;

                if (totalCount > 0)
                {
                    foreach (StockTypeDetailListModel stockTypeDetailListModel in stockTypeList)
                    {
                        var control = (from r in stockTypeSelectList.AsEnumerable()
                                       where r.Value == stockTypeDetailListModel.IdStockType.GetValue<string>()
                                       select r);
                        if (!control.Any())
                        {
                            SelectListItem row = new SelectListItem()
                            {
                                Value = stockTypeDetailListModel.IdStockType.GetValue<string>(),
                                Text = stockTypeDetailListModel.AdminDesc
                            };
                            stockTypeSelectList.Add(row);
                        }
                    }
                }
                // }
            }
            return
                Json(
                    new
                    {
                        result = stockTypeSelectList,
                        fromWarehouseId = fromWarehouseId,
                        toWarehouseId = toWarehouseId
                    }
                    , JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListParts(string rackId)
        {
            List<SelectListItem> partSelectList = new List<SelectListItem>();

            if (!string.IsNullOrEmpty(rackId))
            {
                RackBL rackBo = new RackBL();
                RackDetailModel rackModel = new RackDetailModel();
                rackModel.Id = rackId.GetValue<int>();
                rackModel = rackBo.GetRack(rackModel).Model;

                int totalCount = 0;
                StockRackDetailListModel listModel = new StockRackDetailListModel();
                listModel.IdDealer = UserManager.UserInfo.GetUserDealerId();
                listModel.RackId = rackId.GetValue<int>();

                StockRackDetailBL bo = new StockRackDetailBL();
                List<StockRackDetailListModel> rackList = bo.ListStockRackDetail(UserManager.UserInfo, listModel, out totalCount).Data;
                if (totalCount > 0)
                {
                    partSelectList.AddRange(rackList.Where(x => x.Quantity > 0).Select(stockRackDetailListModel => new SelectListItem()
                    {
                        Value = stockRackDetailListModel.PartId.GetValue<string>(),
                        Text =
                                stockRackDetailListModel.PartCode + CommonValues.Minus +
                                stockRackDetailListModel.PartName
                    }));
                }
            }
            return Json(new { result = partSelectList }, JsonRequestBehavior.AllowGet);
        }

        private void SetExchangeDefaults()
        {
            StockRackDetailBL srdBo = new StockRackDetailBL();
            int totalCount = 0;
            StockRackDetailListModel srdListModel = new StockRackDetailListModel();
            srdListModel.IdDealer = UserManager.UserInfo.GetUserDealerId();
            List<StockRackDetailListModel> srdList = srdBo.ListStockRackDetail(UserManager.UserInfo, srdListModel, out totalCount).Data;
            var fromList = (from e in
                                (from f in srdList.AsEnumerable()
                                 where f.Quantity > 0
                                 select f)
                            group e by
                                new { e.RackId, RackName = e.WarehouseName + CommonValues.Minus + e.RackName }
                                into grp
                            select
                                new SelectListItem
                                {
                                    Value = grp.Key.RackId.ToString(),
                                    Text = grp.Key.RackName
                                }).ToList();
            ViewBag.FromWarehouseRackList = fromList;

            RackListModel rListModel = new RackListModel();
            rListModel.DealerId = UserManager.UserInfo.GetUserDealerId().GetValue<string>();
            rListModel.SearchIsActive = true;
            RackBL rBo = new RackBL();
            List<RackListModel> rackList = rBo.ListRacks(rListModel, out totalCount).Data;
            var toList = (from e in rackList.AsEnumerable()
                          group e by new { RackId = e.Id, RackName = e.WarehouseName + CommonValues.Minus + e.Name }
                              into grp
                          select
                              new SelectListItem
                              {
                                  Value = grp.Key.RackId.ToString(),
                                  Text = grp.Key.RackName
                              }).ToList();
            ViewBag.ToWarehouseRackList = toList;
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.StockRackDetail.StockExchangeIndex)]
        public ActionResult StockExchangeIndex()
        {
            StockExchangeViewModel model = new StockExchangeViewModel();
            SetExchangeDefaults();
            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.StockRackDetail.StockExchangeIndex)]
        public ActionResult StockExchangeIndex(StockExchangeViewModel model)
        {
            SetExchangeDefaults();

            var bo = new StockRackDetailBL();

            if (ModelState.IsValid)
            {
                RackBL rackBo = new RackBL();
                RackDetailModel toRackModel = new RackDetailModel { Id = model.ToRackId.GetValue<int>() };
                toRackModel = rackBo.GetRack(toRackModel).Model;
                model.ToWarehouseId = toRackModel.WarehouseId.GetValue<int>();

                bo.DMLStockExchange(UserManager.UserInfo, model);
                if (model.ErrorNo == 0)
                {
                    ModelState.Clear();
                    StockExchangeViewModel newModel = new StockExchangeViewModel();
                    SetMessage(string.Format(MessageResource.StockExchangeIndex_Display_Success, model.StockTransactionId), CommonValues.MessageSeverity.Success);
                    return View(newModel);
                }
                else
                {
                    SetMessage(model.ErrorMessage, CommonValues.MessageSeverity.Fail);
                }
            }
            return View(model);
        }
        #endregion
    }
}
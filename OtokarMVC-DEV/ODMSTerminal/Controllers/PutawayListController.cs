using System;
using System.Linq;
using ODMSBusiness.Terminal.StockMovement.Dtos;
using ODMSCommon;
using ODMSCommon.Exception;
using ODMSCommon.Resources;
using System.Web.Mvc;

namespace ODMSTerminal.Controllers
{
    using System.Web.Mvc;
    using ODMSBusiness.Terminal.Common;
    using ODMSCommon.Security;
    using Infrastructure.Constants;
    using Infrastructure.MvcActionFilters;
    using Models;
    using ODMSBusiness.Terminal.DeliveryGoodsPlacement.Dtos;
    using System.Collections.Generic;
    using ODMSModel.DeliveryListPart;

    [PermissionAuthorize(Permissions.PutawayList)]
    public class PutawayListController : BaseController
    {
        private readonly IMediator _mediator;

        public PutawayListController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("put-away-list/{pageId}/{sapDeliveryNo?}/{waybillNo?}")]
        public ActionResult Index(string sapDeliveryNo, string waybillNo, int pageId = 1)
        {
            var pagingInfo = new GridPagingInfo { CurrentPage = pageId };
            var request = new DeliveryListRequest(sapDeliveryNo, waybillNo, pagingInfo, UserManager.UserInfo.GetUserDealerId(), 0);
            ViewBag.DeliveryList = _mediator.Request(request);
            var model = new DeliverySearchModel { SapDeliveryNo = sapDeliveryNo, WaybillNo = waybillNo };
            return View(model);
        }

        [Route("put-away-list/waybill/{deliveryId}/{pageId}")]
        public ActionResult Waybill(long deliveryId = 0, int pageId = 1)
        {
            if (deliveryId == 0)
                return RedirectToAction("Index", new { pageId = 1 });

            var deliveryInfoRequest = new DeliveryInfoRequest(deliveryId, UserManager.UserInfo.GetUserDealerId());
            var deliveryInfo = _mediator.Request(deliveryInfoRequest);
            if (deliveryInfo == null)
                return RedirectToAction("Index", new { pageId = 1 });

            var pagingInfo = new GridPagingInfo { CurrentPage = pageId };
            var request = new DeliveryDetailListRequest(pagingInfo, UserManager.UserInfo.GetUserDealerId(), deliveryId, UserManager.LanguageCode);
            var response = _mediator.Request(request);
            var model = new DeliveryDetailModel(deliveryInfo, response);
            return View(model);
        }

        [Route("put-away-list/rack/{seqNo}")]
        public ActionResult Rack(long seqNo = 0,long deliveryId = 0)
        {
            if (seqNo == 0)
                return RedirectToAction("Index", new { pageId = 1 });

            var deliveryDetailInfoRequest = new DeliveryDetailInfoRequest(seqNo, UserManager.UserInfo.GetUserDealerId());
            var deliveryDetailInfo = _mediator.Request(deliveryDetailInfoRequest);
            if (deliveryDetailInfo == null)
                return RedirectToAction("Index", new { pageId = 1 });

            var list = _mediator.Request(new PartsPlacementListRequest(seqNo));
            var model = new DeliveryPlacementModel(deliveryDetailInfo, list.Items);

            var dealerWarehouses = _mediator.Request(new DealerWareHouseListRequest(UserManager.UserInfo.GetUserDealerId(), true));
            ViewBag.Warehouses = dealerWarehouses;
            ViewBag.DeliveryId = deliveryId;
            return View(model);
        
        }

        [HttpPost]
        public ActionResult FindRack(long deliveryId, string partCode)
        {
            var request = new FindDeliveryDetailRequest(deliveryId, partCode, UserManager.UserInfo.GetUserDealerId());
            var response = _mediator.Request(request);
            return RedirectToAction("Rack", new { seqNo = response.DeliverySequenceNo });
        }


        [HttpPost]
        public ActionResult CompleteRack(long deliveryId)
        {
            var command = new CompleteDeliveryDetailCommand(deliveryId, UserManager.UserInfo.GetUserDealerId(), UserManager.UserInfo.UserId);
            try
            {
                _mediator.Send(command);
            }
            catch (ODMSException ex)
            {
                SetMessage(ex.Message, CommonValues.MessageSeverity.Fail);
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("Waybill", new { deliveryId = deliveryId, pageId = 1 });
            }
            SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);
            return RedirectToAction("Index", new { pageId = 1 });
        }



        [HttpPost]
        public ActionResult SearchWarehouse(WarehouseSearchModel model)
        {
            if (string.IsNullOrEmpty(model.WarehouseCode))
            {
                throw new ODMSException(MessageResource.Exception_WarehouseNotFound);
            }
            if (!model.WarehouseCode.Contains("&"))
            {
                var warehouse =
                    _mediator.Request(new DealerWareHouseListRequest(UserManager.UserInfo.GetUserDealerId(), includeRacks: false))
                        .FirstOrDefault(c => c.WarehouseCode == model.WarehouseCode);
                if (warehouse == null)
                    throw new ODMSException(MessageResource.Exception_WarehouseNotFound);

                return Json(new { WarehouseId = warehouse.WarehouseId, WarehouseCode = warehouse.WarehouseCode });
            }
            else
            {
                //warehouseCode&rackCode
                var values = model.WarehouseCode.Split('&');
                var warehouse =
                    _mediator.Request(new DealerWareHouseListRequest(UserManager.UserInfo.GetUserDealerId(), includeRacks: false))
                        .FirstOrDefault(c => c.WarehouseCode == values[0]);
                if (warehouse == null)
                    throw new ODMSException(MessageResource.Exception_WarehouseNotFound);
                var rack =
                    _mediator.Request(new PutawayRackSearchRequest(values[1], warehouse.WarehouseId, model.PartId,
                        UserManager.UserInfo.GetUserDealerId()));
                if (rack == null)
                    throw new ODMSException(MessageResource.Exception_RackNotFound);
                return
                    Json(new
                    {
                        WarehouseId = warehouse.WarehouseId,
                        WarehouseCode = warehouse.WarehouseCode,
                        RackId = rack.RackId,
                        RackCode = rack.RackCode,
                        Quantity = rack.Quantity
                    });
            }
        }

        [HttpPost]
        public ActionResult SearchRack(WarehouseSearchModel model)
        {
            if (string.IsNullOrEmpty(model.RackCode))
            {
                throw new ODMSException(MessageResource.Exception_RackNotFound);
            }
            if (model.RackCode.Contains("&"))
            {
                model.RackCode = model.RackCode.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries)[1];
            }
            var rack =
                     _mediator.Request(new PutawayRackSearchRequest(model.RackCode, model.WarehouseId, model.PartId,
                         UserManager.UserInfo.GetUserDealerId()));
            return
                Json(new
                {
                    RackId = rack.RackId,
                    RackCode = rack.RackCode,
                    Quantity = rack.Quantity
                });
        }

        [HttpPost]
        [Transactional]
        public ActionResult AddPart(long deliverySeqNo, int rackId, decimal quantity, decimal leftQuantity,long deliveryId)
        {
            var command = new DeliveryPlacePartCommand(deliverySeqNo, rackId, quantity, leftQuantity, UserManager.UserInfo.UserId);
            _mediator.Send(command);
            return RedirectToAction("Waybill", new { deliveryId = deliveryId, pageId = 1 });
        }
        [HttpPost]
        [Transactional]
        public ActionResult DeletePlacement(long placementId, long deliverySeqNo,long deliveryId)
        {
            var command = new DeliveryPlacePartDeleteCommand(placementId, UserManager.UserInfo.UserId);
            _mediator.Send(command);
            return RedirectToAction("Rack", new { seqNo = deliverySeqNo ,deliveryId = deliveryId });
        }
    }
}
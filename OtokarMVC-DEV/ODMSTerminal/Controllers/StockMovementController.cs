using System;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using ODMSBusiness;
using ODMSBusiness.Terminal.Common;
using ODMSBusiness.Terminal.StockMovement.Dtos;
using ODMSCommon;
using ODMSCommon.Exception;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSTerminal.Infrastructure.Constants;
using ODMSTerminal.Infrastructure.MvcActionFilters;
using ODMSTerminal.Models;

namespace ODMSTerminal.Controllers
{
    [RoutePrefix("stock-movement")]
    [PermissionAuthorize(Permissions.StockMovement)]
    public class StockMovementController : BaseController
    {
        private readonly IMediator _mediator;

        public StockMovementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("index")]
        public ActionResult Index()
        {
            var dealerWarehouses =
                _mediator.Request(new DealerWareHouseListRequest(UserManager.UserInfo.GetUserDealerId(), true));
            if (dealerWarehouses.Count() == 1 && dealerWarehouses.First().Racks.Count() == 1)
            {
                throw new ODMSException(MessageResource.Exception_NoStockMovementDueToOneRack);
            }
            ViewBag.Warehouses = dealerWarehouses;
            return View();
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
                    _mediator.Request(new DealerWareHouseListRequest(UserManager.UserInfo.GetUserDealerId(), includeRacks:false))
                        .FirstOrDefault(c => c.WarehouseCode == model.WarehouseCode);
                if (warehouse == null)
                    throw new ODMSException(MessageResource.Exception_WarehouseNotFound);

                return Json(new {WarehouseId = warehouse.WarehouseId, WarehouseCode = warehouse.WarehouseCode});
            }
            else
            {
                //warehouseCode&rackCode
                var values = model.WarehouseCode.Split('&');
                var warehouse =
                    _mediator.Request(new DealerWareHouseListRequest(UserManager.UserInfo.GetUserDealerId(), includeRacks: true))
                        .FirstOrDefault(c => c.WarehouseCode == values[0]);
                if (warehouse == null)
                    throw new ODMSException(MessageResource.Exception_WarehouseNotFound);
                var rack = warehouse.Racks.FirstOrDefault(c => c.RackCode == values[1]);
                if(rack==null)
                    throw new ODMSException(MessageResource.Exception_RackNotFound);
                return
                    Json(
                        new
                        {
                            WarehouseId = warehouse.WarehouseId,
                            WarehouseCode = warehouse.WarehouseCode,
                            RackId = rack.RackId,
                            RackCode = rack.RackCode
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
                model.RackCode = model.RackCode.Split(new char[] {'&'}, StringSplitOptions.RemoveEmptyEntries)[1];
            }
            var rack =
                _mediator.Request(new DealerRackListRequest(UserManager.UserInfo.GetUserDealerId(), model.WarehouseId))
                    .FirstOrDefault(c => c.RackCode == model.RackCode);
            if(rack==null)
                throw new ODMSException(MessageResource.Exception_RackNotFound);
            return Json(new
            {
                RackId = rack.RackId,
                RackCode = rack.RackCode
            });
        }

        [HttpPost]
        public ActionResult SearchPart(WarehouseSearchModel model)
        {
            if (string.IsNullOrEmpty(model.PartCode))
            {
                throw new ODMSException(MessageResource.Exception_PartNotFound);
            }
            var part =
                _mediator.Request(new PartSearchRequest(UserManager.UserInfo.GetUserDealerId(), model.WarehouseId,
                    model.RackId, model.PartCode, UserManager.LanguageCode));
            if (part==null|| part.PartId==0)
            {
                throw new ODMSException(MessageResource.Exception_PartNotFound);
            }
            return Json(new
            {
                part.PartId,
                part.PartCode,
                part.Quantity
            });
        }

        [HttpGet]
        [Route("address-change")]
        public ActionResult AddressChange(WarehouseSearchModel model)
        {
            if (model == null || model.PartId == 0 || model.RackId == 0 || model.WarehouseId == 0)
                return RedirectToAction("index");
            ViewBag.StockTypes = _mediator.Request(new StockTypeListRequest(UserManager.UserInfo.GetUserDealerId(),
                model.WarehouseId,
                model.PartId,
                UserManager.LanguageCode
                ));
            var data =
                _mediator.Request(new StockRackDetailInfoRequest(UserManager.UserInfo.GetUserDealerId(),
                    model.WarehouseId, model.RackId, model.PartId, UserManager.LanguageCode));
            model.Quantity = data.Quantity;
            model.Unit = data.Unit;
            return View(model);
        }

        [HttpPost]
        public ActionResult Save(WarehouseSearchModel model)
        {
            var stockMovementCommand = new StockMovementCommand(UserManager.UserInfo.GetUserDealerId(), model.WarehouseId, model.RackId, model.PartId,
                model.TargetWarehouseId, model.TargetRackId, model.StockTypeId, model.Quantity??0, model.Description,
                model.TargetQuantity);
            _mediator.Send(stockMovementCommand);
            SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);
            return RedirectToAction("index");
        }


    }
}
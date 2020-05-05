using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using  System.Web.Mvc;
using ODMSBusiness.Terminal.Common;
using ODMSBusiness.Terminal.RackQuery.Dtos;
using ODMSBusiness.Terminal.StockMovement.Dtos;
using ODMSCommon.Exception;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSTerminal.Infrastructure.Constants;
using ODMSTerminal.Infrastructure.MvcActionFilters;
using ODMSTerminal.Models;

namespace ODMSTerminal.Controllers
{
    [PermissionAuthorize(Permissions.RackQuery)]
    [RoutePrefix("rack-query")]
    public class RackQueryController:BaseController
    {
        private readonly IMediator _mediator;

        public RackQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("index")]
        public ActionResult Index()
        {
            var dealerWarehouses =
               _mediator.Request(new DealerWareHouseListRequest(UserManager.UserInfo.GetUserDealerId(), true));
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
                    _mediator.Request(new DealerWareHouseListRequest(UserManager.UserInfo.GetUserDealerId(), includeRacks: true))
                        .FirstOrDefault(c => c.WarehouseCode == values[0]);
                if (warehouse == null)
                    throw new ODMSException(MessageResource.Exception_WarehouseNotFound);
                var rack = warehouse.Racks.FirstOrDefault(c => c.RackCode == values[1]);
                if (rack == null)
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
                model.RackCode = model.RackCode.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries)[1];
            }
            var rack =
                _mediator.Request(new DealerRackListRequest(UserManager.UserInfo.GetUserDealerId(), model.WarehouseId))
                    .FirstOrDefault(c => c.RackCode == model.RackCode);
            if (rack == null)
                throw new ODMSException(MessageResource.Exception_RackNotFound);
            return Json(new
            {
                RackId = rack.RackId,
                RackCode = rack.RackCode
            });
        }

        [HttpPost]
        [Route("results")]
        public ActionResult Results(int warehouseId, int rackId)
        {
            var list =
                _mediator.Request(new RackQueryListRequest(UserManager.UserInfo.GetUserDealerId(), warehouseId, rackId,UserManager.LanguageCode));
            if (!list.Any())
                throw new ODMSException(MessageResource.Exception_EmptyRack);
            return View(list);
        }



    }
}
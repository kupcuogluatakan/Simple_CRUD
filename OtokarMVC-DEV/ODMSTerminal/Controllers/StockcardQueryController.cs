using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ODMSBusiness.Terminal.Common;
using ODMSBusiness.Terminal.StockCardQuery.Dtos;
using ODMSCommon.Security;
using ODMSTerminal.Infrastructure.Constants;
using ODMSTerminal.Infrastructure.MvcActionFilters;
using ODMSTerminal.Models;

namespace ODMSTerminal.Controllers
{
    [PermissionAuthorize(Permissions.StockcardQuery)]
    [RoutePrefix("stockcard-query")]
    public class StockcardQueryController:BaseController
    {
        private readonly IMediator _mediator;

        public StockcardQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("index/{partCode?}")]
        public ActionResult Index(string partCode)
        {
            if (!string.IsNullOrEmpty(partCode))
                return View(GetModel(partCode));
            return View();
        }

        private StockCardInfo GetModel(string partCode)
        {
            var stockCardInfo = _mediator.Request(new StockCardInfoRequest(partCode,UserManager.UserInfo.GetUserDealerId(),UserManager.LanguageCode));

            var mainlist =
                _mediator.Request(new StockCardMainListRequest(UserManager.UserInfo.GetUserDealerId(),
                    stockCardInfo.PartId, UserManager.LanguageCode));
            stockCardInfo.MainList = mainlist;

            var detailList = _mediator.Request(new StockCardDetailListRequest(UserManager.UserInfo.GetUserDealerId(),
                stockCardInfo.PartId, UserManager.LanguageCode));

            stockCardInfo.DetailList = detailList;

            return stockCardInfo;
        }
    }
}
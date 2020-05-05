using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ODMSBusiness.Terminal.Common;
using ODMSBusiness.Terminal.OldRackQuery.Dtos;
using ODMSCommon.Security;
using ODMSData;
using ODMSTerminal.Infrastructure.Constants;
using ODMSTerminal.Infrastructure.MvcActionFilters;

namespace ODMSTerminal.Controllers
{
    [RoutePrefix("old-rack-query")]
    [PermissionAuthorize(Permissions.OldRackQuery)]
    public class OldRackQueryController : Controller
    {
        private readonly IMediator _mediator;

        public OldRackQueryController(IMediator mediator)
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

        public OldRackQueryInfo GetModel(string partCode)
        {
            var info=_mediator.Request(new OldRackQueryInfoRequest(partCode, UserManager.UserInfo.GetUserDealerId(), UserManager.LanguageCode));
            var resultList =
                _mediator.Request(new OldRackQueryListRequest(info.PartId, UserManager.UserInfo.GetUserDealerId()));
            info.ResultList = resultList;
            return info;
        }
    }
}
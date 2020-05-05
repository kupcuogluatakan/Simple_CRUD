using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ODMSBusiness.Terminal.ClaimWaybill.Dtos;
using ODMSBusiness.Terminal.Common;
using ODMSCommon.Security;
using ODMSTerminal.Infrastructure.Constants;
using ODMSTerminal.Infrastructure.MvcActionFilters;

namespace ODMSTerminal.Controllers
{
    [PermissionAuthorize(Permissions.ClaimWaybill)]
    [RoutePrefix("claim-waybill")]
    public class ClaimWaybillController:BaseController
    {
        private readonly IMediator _mediator;

        public ClaimWaybillController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("index")]
        public ActionResult Index()
        {
            var info = _mediator.Request(new ActiveClaimPeriodRequest(UserManager.UserInfo.GetUserDealerId()));
            ViewBag.ClaimWaybillList =
                _mediator.Request(new ClaimWaybillListRequest(UserManager.UserInfo.GetUserDealerId()));
            return View(info);
        }

        [HttpGet]
        [Route("new-waybill/{claimPeriodId}")]
        public ActionResult NewWaybill(long claimPeriodId)
        {
            var model = new ClaimWaybillInfo
            {
                ClaimRecallPeriodId = claimPeriodId,
                WaybillDate = DateTime.Now
            };
            return View(model);
        }
        [HttpGet]
        [Route("waybill/{claimPeriodId}/{claimWaybillId}")]
        public ActionResult Waybill(long claimPeriodId,int claimWaybillId)
        {
            var model = _mediator.Request(new ClaimWaybillInfoRequest(claimPeriodId,claimWaybillId,UserManager.UserInfo.GetUserDealerId()));
            ViewBag.Parts =
                _mediator.Request(new ClaimWaybillPartListRequest(claimWaybillId, UserManager.UserInfo.GetUserDealerId(),UserManager.LanguageCode));

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateWaybill(ClaimWaybillInfo model)
        {
            var request = new CreateWaybillRequest(
                model.ClaimRecallPeriodId, model.WaybillDate, model.WaybillNo,
                model.WaybillSerialNo,UserManager.UserInfo.GetUserDealerId(),UserManager.UserInfo.UserId
                );
            var response = _mediator.Request(request);
            return RedirectToAction("Waybill",
                new {claimPeriodId = response.ClaimPeriodId, claimWaybillId = response.ClaimWaybillId});

        }

        [HttpPost]
        public ActionResult SearchPart(string partCode, int claimWaybillId)
        {
            var request = new ClaimWaybillSearchPartRequest(claimWaybillId, partCode,
                UserManager.UserInfo.GetUserDealerId());
            var response = _mediator.Request(request);
            return Json(response);
        }

        [HttpPost]
        public ActionResult SavePart(long partId, long claimDismantledPartId, int claimWaybillId, long claimRecallPeriodId)
        {
            _mediator.Send(new AddClaimWaybillPartCommand(claimWaybillId, claimDismantledPartId,UserManager.UserInfo.UserId));
            return RedirectToAction("Waybill", new { claimPeriodId=claimRecallPeriodId, claimWaybillId});
        }

        [HttpPost]
        public ActionResult DeletePart(long dismantledPartId, int claimWaybillId, long claimRecallPeriodId)
        {
            _mediator.Send(new AddClaimWaybillPartCommand(0, dismantledPartId, UserManager.UserInfo.UserId));
            return RedirectToAction("Waybill", new { claimPeriodId = claimRecallPeriodId, claimWaybillId });
        }

    }
}
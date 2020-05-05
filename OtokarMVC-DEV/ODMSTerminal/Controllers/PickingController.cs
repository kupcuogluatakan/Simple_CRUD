using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ODMSBusiness.Terminal.Common;
using ODMSBusiness.Terminal.PickingList.Dtos;
using ODMSCommon;
using ODMSCommon.Exception;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSTerminal.Infrastructure.Constants;
using ODMSTerminal.Infrastructure.MvcActionFilters;

namespace ODMSTerminal.Controllers
{
    [PermissionAuthorize(Permissions.PickingList)]
    [RoutePrefix("picking-list")]
    public class PickingListController : BaseController
    {

        private readonly IMediator _mediator;

        public PickingListController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("index/{pageId}")]
        public ActionResult Index(int pageId)
        {
            var pagingInfo = new GridPagingInfo { CurrentPage = pageId };
            var request = new PickingListRequest(UserManager.UserInfo.GetUserDealerId(), pagingInfo);
            var response = _mediator.Request(request);
            return View(response);
        }

        [Route("parts/{pickingId}/{PartCode?}")]
        public ActionResult Parts(long pickingId, string PartCode)
        {
            var pickingInfo =
                _mediator.Request(new PickingInfoRequest(pickingId, UserManager.UserInfo.GetUserDealerId(), UserManager.LanguageCode));

            if (pickingInfo.UpdateUser != UserManager.UserInfo.UserId.ToString() && pickingInfo.StatusId==1)//Başlandı
            {
                return RedirectToAction("ChangeUser",new { pickingId });
            }

            pickingInfo.Parts = _mediator.Request(new PickingPartsRequest(pickingId, PartCode, UserManager.UserInfo.GetUserDealerId(), UserManager.LanguageCode));

            pickingInfo.PartCode = PartCode;

            return View(pickingInfo);
        }

        [Route("change-user/{pickingId}")]
        public ActionResult ChangeUser(int pickingId)
        {
            SetMessage(MessageResource.Terminal_PickingList_ChangeUserMessage,CommonValues.MessageSeverity.Fail);
            return View(pickingId);
        }
        [Transactional]
        public ActionResult ConfirmUserChange(int pickingId)
        {
            _mediator.Send(new ChangePickingUserCommand(pickingId, UserManager.UserInfo.GetUserDealerId(), UserManager.UserInfo.UserId));
            SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);
            return RedirectToAction("parts",new { pickingId});
        }

        [Route("detail/{pickingDetailId}")]
        public ActionResult Detail(long pickingDetailId)
        {
            var pickingDetailInfo =
                _mediator.Request(new PickingDetailInfoRequest(pickingDetailId, UserManager.UserInfo.GetUserDealerId()));

            

            var stockRackList =
                _mediator.Request(new StockRackListRequest(pickingDetailId, UserManager.UserInfo.GetUserDealerId(),null));


            pickingDetailInfo.StockRackList = stockRackList;

            return View(pickingDetailInfo);
        }

        [Route("save")]
        [HttpPost]
        public ActionResult Save([Bind(Exclude = "StockRackList")]PickingDetailInfo model,List<StockRackListItem> stockRackList)
        {
            if (model.LeftQuantity < stockRackList.Sum(c => c.PickQuantity))
            {
               SetMessage( MessageResource.Exception_PickingExcessiveQuantity,CommonValues.MessageSeverity.Fail);
                return View("detail", model);
            }
            if (stockRackList.Sum(c => c.PickQuantity) == 0)
            {
                SetMessage(MessageResource.Exception_NoPickQuantity, CommonValues.MessageSeverity.Fail);
                return View("detail", model);
            }

            model.StockRackList = new EnumerableResponse<StockRackListItem>(stockRackList);
            var command = new PickPartsCommand(model, UserManager.UserInfo.GetUserDealerId(), UserManager.UserInfo.UserId,UserManager.LanguageCode);
            _mediator.Send(command);
            SetMessage(MessageResource.Global_Display_Success,CommonValues.MessageSeverity.Success);
            return RedirectToAction("parts", new { pickingId = model.PickingId,PartCode=string.Empty});
        }

        [Route("complete-picking")]
        [HttpPost]
        public ActionResult Complete(long pickingId)
        {
            var command = new PickingCompleteCommand(pickingId, UserManager.UserInfo.GetUserDealerId(), UserManager.UserInfo.UserId, UserManager.LanguageCode);
            _mediator.Send(command);
            SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);
            return RedirectToAction("index", new { pageId = 1 });
        }


    }
}
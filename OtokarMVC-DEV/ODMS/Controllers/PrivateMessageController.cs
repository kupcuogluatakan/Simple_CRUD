using System.Collections.Generic;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.PrivateMessage;
using Permission = ODMSCommon.CommonValues.PermissionCodes.PrivateMessage;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class PrivateMessageController : ControllerBase
    {

        [HttpGet]
        [AuthorizationFilter(Permission.PrivateMessageIndex)]
        public ActionResult PrivateMessageIndex()
        {
            ViewBag.TypesList = GetSearchTypes();
            return View();
        }

        private List<SelectListItem> GetSearchTypes()
        {
            var list = new List<SelectListItem>
                {
                    new SelectListItem {Value = "A", Text = MessageResource.Global_Display_All},
                    new SelectListItem {Value = "R", Text = MessageResource.PrivateMessage_Display_RecievedByMe},
                    new SelectListItem {Value = "S", Text = MessageResource.PrivateMessage_Display_SentByMe}
                };


            return list;
        }

        [HttpPost]
        [AuthorizationFilter(Permission.PrivateMessageIndex)]
        public ActionResult ListMessages([DataSourceRequest]DataSourceRequest request, string type)
        {
            var total = 0;
            var model = new PrivateMessageListModel(request) { Type = type == "R" || type == "S" ? type : "A" };
            var list = new PrivateMessageBL().ListMessages(UserManager.UserInfo, model, out total).Data;
            return Json(new { Data = list, Total = total });
        }

        [HttpGet]
        [AuthorizationFilter(Permission.PrivateMessageIndex, Permission.PrivateMessageSend)]
        public ActionResult PrivateMessageSend(int id, string sender, string reciever, int recieverId, string title)
        {
            ViewBag.Title = title ?? string.Empty;
            ViewBag.MessageId = id;
            ViewBag.Sender = sender;
            ViewBag.Reciever = reciever;
            ViewBag.RecieverId = recieverId;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(Permission.PrivateMessageIndex, Permission.PrivateMessageSend)]
        public ActionResult GetMessageHistory(int messageId, int currentPage, string sender, string reciever)
        {
            int total;
            ViewBag.Sender = sender;
            ViewBag.Reciever = reciever;
            var messageHistoryList = new PrivateMessageBL().GetMessageHistory(UserManager.UserInfo, messageId, currentPage, out total).Data;
            messageHistoryList.Reverse(0, messageHistoryList.Count);
            ViewBag.Total = total;
            ViewBag.CurrentPage = currentPage;
            return PartialView("_MessageHistory", messageHistoryList);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.PrivateMessageIndex, Permission.PrivateMessageSend)]
        public ActionResult PrivateMessageSend(PrivateMessageModel model)
        {
            if (ModelState.IsValid)
            {
                new PrivateMessageBL().SendMessage(UserManager.UserInfo, model);
                CheckErrorForMessage(model, true);
            }
            else
                SetMessage(MessageResource.Err_Generic_Unexpected, CommonValues.MessageSeverity.Fail);

            return RedirectToAction("PrivateMessageSend",
                new
                {
                    id = model.MessageId,
                    sender = string.IsNullOrEmpty(model.Sender) ? UserManager.UserInfo.FullName : model.Sender,
                    reciever = model.Reciever,
                    recieverId = model.RecieverId
                });
        }

        [HttpGet]
        [AuthorizationFilter(Permission.PrivateMessageIndex, Permission.PrivateMessageSend)]
        public ActionResult SendNewMessage()
        {
            return View();
        }

        [HttpPost]
        [AuthorizationFilter(Permission.PrivateMessageIndex, Permission.PrivateMessageSend)]
        public ActionResult ListRecievers(string searchText)
        {
            return Json(new PrivateMessageBL().ListRecievers(UserManager.UserInfo, searchText).Data);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.PrivateMessageIndex, Permission.PrivateMessageSend)]
        public ActionResult SendNewMessage(PrivateMessageModel model)
        {
            if (ModelState.IsValid)
            {
                new PrivateMessageBL().SendMessage(UserManager.UserInfo, model);
                CheckErrorForMessage(model, true);
                if (model.ErrorNo == 0)
                    ModelState.Clear();
            }
            return View();

        }
    }
}

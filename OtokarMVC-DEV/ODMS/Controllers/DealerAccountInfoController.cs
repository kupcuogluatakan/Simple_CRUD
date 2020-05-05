using ODMS.Controllers;
using ODMS.Filters;
using ODMSBusiness;
using ODMSBusiness.Business;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.DealerAccountInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class DealerAccountInfoController : BankController
    {
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerAccountInfo.DealerAccountInfoIndex)]
        public ActionResult DealerAccountInfoIndex()
        {
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.DealerAccountInfo.DealerAccountInfoIndex)]
        public ActionResult ListDealerAccountInfo()
        {
            var returnModel = new DealerAccountInfoBL().List(UserManager.UserInfo, null).Data;
            return Json(new
            {
                Data = returnModel,
                Total = returnModel.Count
            });
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerAccountInfo.DealerAccountInfoCreate)]
        public ActionResult CreateDealerAccount()
        {
            var dealerAccount = new DealerAccountInfoBL().List(UserManager.UserInfo, null).Data;
            var dealerAccountCount = dealerAccount.Where(x => x.IsActive).Count();
            if (dealerAccountCount >= 4)
            {
                ViewBag.IsMessage = true;
            }
            ViewBag.BankList = ListBanks();
            return View();
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerAccountInfo.DealerAccountInfoCreate)]
        public ActionResult CreateDealerAccount(DealerAccountListModel model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.BankList = ListBanks();
                model.CommandType = CommonValues.DMLType.Insert;
                var bo = new DealerAccountInfoBL();
                bo.DealerAccountInfoDml(UserManager.UserInfo, model);
                CheckErrorForMessage(model, true);
                ModelState.Clear();

                DealerAccountListModel newModel = new DealerAccountListModel();
                newModel.IsActive = true;
                return View(newModel);
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult DealerAccountInfoUpdate(int id)
        {
            ViewBag.BankList = ListBanks();
            DealerAccountListModel returnModel = new DealerAccountListModel();
            if (id > 0)
            {
                returnModel = new DealerAccountInfoBL().List(UserManager.UserInfo, id.GetValue<int>()).Data.First();
            }
            return View(returnModel);
        }

        [HttpPost]
        public ActionResult DealerAccountInfoUpdate(DealerAccountListModel model)
        {
            var dealerAccount = new DealerAccountInfoBL().List(UserManager.UserInfo, null).Data;
            var dealerAccountCount = dealerAccount.Where(x => x.IsActive).Count();
            if (dealerAccountCount > 4)
            {
                ViewBag.IsMessage = true;
                return View(model);
            }
            if (ModelState.IsValid)
            {
                ViewBag.BankList = ListBanks();
                model.CommandType = CommonValues.DMLType.Update;
                var bo = new DealerAccountInfoBL();
                bo.DealerAccountInfoDml(UserManager.UserInfo, model);

                if (model.ErrorNo == 0)
                    SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);
                else
                    SetMessage(model.ErrorMessage, CommonValues.MessageSeverity.Fail);
            }
            return View(model);
        }



        [AuthorizationFilter(CommonValues.PermissionCodes.DealerAccountInfo.DealerAccountInfoDelete)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult DeleteDealerAccount(int id)
        {
            if (id > 0)
            {
                var model = new DealerAccountListModel
                {
                    Id = id,
                    CommandType = CommonValues.DMLType.Delete
                };
                new DealerAccountInfoBL().DealerAccountInfoDml(UserManager.UserInfo, model);
                ModelState.Clear();

                if (model.ErrorNo == 0)
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
            }
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.Err_Generic_Unexpected);
        }

        #region Helper Methods
        private List<SelectListItem> ListBanks()
        {
            return new BankBL().ListBanksAsSelectList().Data;
        }
        #endregion
    }
}
using System.Web.Mvc;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.TechnicianOperation;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class TechnicianOperationController : ControllerBase
    {
        public ActionResult TechnicianOperationIndex()
        {
            SetCombobox();
            if (Session[CommonValues.TechnicionCookieKey] == null)
                return View();


            var model = new TechnicianOperationViewModel
                {
                    DmsUserId = Session[CommonValues.TechnicionCookieKey].GetValue<int>(),
                    IsLogin = true
                };
            return View(model);
        }

        [HttpPost]
        public ActionResult TechnicianOperationIndex(TechnicianOperationViewModel model)
        {
            SetCombobox();
            if (!ModelState.IsValid)
                return View(model);
            var bl = new TechnicianOperationBL();
            model.Password = new PasswordSecurityProvider().GenerateHashedPassword(model.Password);
            bl.CheckTechnicianLogin(model);
            if (!CheckErrorForMessage(model, true))
                Session[CommonValues.TechnicionCookieKey] = model.DmsUserId;
                //SetCookie(model, 10);

            return View(model);
        }

        [HttpPost]
        public ActionResult TechnicianOperationLogout()
        {
            Session[CommonValues.TechnicionCookieKey] = null;

            var model = new TechnicianOperationViewModel();

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                model.ErrorMessage);

        }

        private void SetCombobox()
        {
            ViewBag.SLUser = CommonBL.ListUserByDealerId(UserManager.UserInfo.GetUserDealerId(), true).Data;
        }
    }
}

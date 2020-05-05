namespace ODMSTerminal.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Security;
    using ODMSBusiness;
    using ODMSCommon;
    using ODMSCommon.Resources;
    using ODMSModel;
    using ODMSModel.Dealer;
    using ODMSModel.SystemAdministration;
    using Security;
    using Infrastructure.Security;
    using Infrastructure.Security.PasswordPolicyRules;
    using ODMSCommon.Security;

    public class LoginController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(SystemAdministrationLoginModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Password) || string.IsNullOrWhiteSpace(model.UserName))
            {
                return View();
            }
            var adminBo = new SystemAdministrationBL();
            var plainPassword = model.Password;
            model.Password = new PasswordSecurityProvider().GenerateHashedPassword(model.Password);
            var userBo = adminBo.Login(model).Model;
            int dealerId = userBo.DealerID;
            if (dealerId != 0)
            {
                DealerBL dBo = new DealerBL();
                DealerViewModel dModel = dBo.GetDealer(UserManager.UserInfo, dealerId).Model;
                if (!dModel.IsActive)
                {
                    SetMessage(MessageResource.Err_Global_ActiveDealer,
                        CommonValues.MessageSeverity.Fail);
                    return View();
                }
            }
            else
            {
                SetMessage(MessageResource.Err_Terminal_DealerOnly,
                        CommonValues.MessageSeverity.Fail);
                return View();
            }

            var manager = new PasswordPolicyManager(plainPassword);

            if (userBo.IsBlocked)
            {
                System.Web.HttpContext.Current.Items.Add("UserInfo", userBo);

                var blockAccountValidator = manager.PasswordRuleValidators.First(c => c is WrongPasswordCountRule);

                if (!blockAccountValidator.Validate())
                {
                    if (manager.BlockMinutes == 0)
                    {
                        CheckErrorForMessage(
                            new ModelBase()
                            {
                                ErrorNo = 1,
                                ErrorMessage =
                                    string.Format(MessageResource.PasswordPolicy_Validation_WrongPasswordCountRule,
                                        (blockAccountValidator as WrongPasswordCountRule).AllowedWrongPasswordInputCount)
                            }, false);
                        return View();
                    }
                    else
                    {
                        var passwordBl = new PasswordBL();
                        var lastLoginFailDate = passwordBl.LastLoginFailDateForUser(userBo.UserId);
                        int elapsedMinutes = (DateTime.Now - lastLoginFailDate.GetValue<DateTime>()).Minutes;
                        if (manager.BlockMinutes > elapsedMinutes)
                        {
                            CheckErrorForMessage(
                                new ModelBase()
                                {
                                    ErrorNo = 1,
                                    ErrorMessage =
                                        string.Format(MessageResource.PasswordPolicy_Validation_AccountBlock,
                                            (manager.BlockMinutes - elapsedMinutes))
                                }, false);
                            return View();
                        }
                        else
                        {
                            //tekrar giriş yapsın
                            SetMessage(MessageResource.Err_Global_Invalid_Login, CommonValues.MessageSeverity.Fail);
                            return View();
                        }
                    }
                }
            }


            if (userBo.IsLoggedIn)
            {
                userBo.RememberMe = model.RememberMe;
                HttpContext.Items.Add("UserInfo", userBo);

                #region password policy checking
                var expireValidator = manager.PasswordRuleValidators.First(c => c is PasswordChangeDayCountRule);
                if (!expireValidator.Validate())
                {
                    userBo.IsPasswordSet = false;
                    System.Web.HttpContext.Current.Items[CommonValues.UserInfoSessionKey] = userBo;
                    CommonUtility.CreateAuthenticationCookie(userBo);
                    CheckErrorForMessage(
                        new ModelBase()
                        {
                            ErrorNo = 1,
                            ErrorMessage = MessageResource.PasswordPolicy_Validation_PasswordChangeDayCountRule
                        }, false);

                    return View();
                }

                #endregion

                if (CommonUtility.CreateAuthenticationCookie(userBo))
                {
                    var permManager = new UserPermissionManager();
                    permManager.InitializeCurrentSessionState();
                    
                    return RedirectToAction("Index","Home");
                }

                SetMessage(MessageResource.Err_Global_CookieProblem, CommonValues.MessageSeverity.Fail);
                return View();
            }
            SetMessage(MessageResource.Err_Global_Invalid_Login, CommonValues.MessageSeverity.Fail);
            return View();
        }
        [Route("logout")]
        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}
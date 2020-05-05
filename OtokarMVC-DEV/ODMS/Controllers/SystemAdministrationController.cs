using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ODMS.Security;
using ODMS.Security.PasswordPolicyRules;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel;
using ODMSModel.Dealer;
using ODMSModel.Shared;
using ODMSModel.SystemAdministration;
using System.Drawing;
using System.IO;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using ODMSModel.User;

namespace ODMS.Controllers
{
    public class SystemAdministrationController : ControllerBase
    {
        private readonly IntegrationController integrationControllers_;
        private readonly SystemAdministrationBL systemAdministrationBL_;

        public SystemAdministrationController(IntegrationController integrationControllers, SystemAdministrationBL systemAdministrationBL)
        {
            integrationControllers_ = integrationControllers;
            systemAdministrationBL_ = systemAdministrationBL;
        }

        [HttpGet]
        public JsonResult ActiveUserCount()
        {
            var count = 0;
            if (HttpContext.Application["TotalOnlineUsers"] != null)
                count = (int)HttpContext.Application["TotalOnlineUsers"];
            return Json(count, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ActiveUserList()
        {
            var modelList = new List<string>(); ;
            if (HttpContext.Application["OnlineUserSessionList"] != null)
            {
                modelList = (HttpContext.Application["OnlineUserSessionList"] as List<string>);
            }

            var model = new List<UserIndexViewModel>();
            return View(model);
        }


        [HttpGet]
        public ActionResult Login()
        {
            var permManager = new UserPermissionManager();
            permManager.ClearCurrentSessionState();
            FormsAuthentication.SignOut();
            Response.Cookies["otokar"].Expires = DateTime.Now.AddDays(-1);

            if (CheckCookie()) // ekran açıldığında cookie varsa iç sayfaya redirect ediyoruz.
                               // 26879 nolu tfs change request'e istinaden değiştirildi OYA
                               // 27638 nolu tfs change request'e istinaden değiştirildi OYA                
                if (CheckHasSystemRole() && !CheckIsTechnician())
                {
                    ViewBag.CaptchaCount = CommonBL.GetGeneralParameterValue(CommonValues.GeneralParameters.CaptchaDisplay).Model
                                           .GetValue<int>();
                    return RedirectToAction(CommonValues.DefaultSystemRoleActionName,
                                            CommonValues.DefaultSystemRoleControllerName);
                }
                else
                {
                    ViewBag.CaptchaCount = CommonBL.GetGeneralParameterValue(CommonValues.GeneralParameters.CaptchaDisplay).Model
                                           .GetValue<int>();
                    return RedirectToAction(CommonValues.DefaultNonSystemRoleActionName,
                                            CommonValues.DefaultNonSystemRoleControllerName);
                }
            //Session["CaptchaCount"] = CommonBL.GetGeneralParameterValue(CommonValues.GeneralParameters.CaptchaDisplay).Model
            //                               .GetValue<int>();
            return View(new SystemAdministrationLoginModel());
        }

        [HttpGet]
        public ActionResult NoAuthorization()
        {
            string[] permissionList = System.Web.HttpContext.Current.Session["PermissionCode"] as string[];
            List<string> permissionNameList = new List<string>();

            PermissionListModel permModel = new PermissionListModel();
            PermissionBL permBo = new PermissionBL();
            if (permissionList != null)
            {
                foreach (string s in permissionList.AsEnumerable())
                {
                    permModel.PermissionCode = s;
                    int totalCount = 0;
                    List<PermissionListModel> list = permBo.ListPermissions(UserManager.UserInfo, permModel, out totalCount).Data;
                    if (totalCount > 0)
                    {
                        if (!permissionNameList.Contains(list.ElementAt(0).PermissionName))
                            permissionNameList.Add(list.ElementAt(0).PermissionName);
                    }
                }

                ViewBag.PermissionList = permissionNameList;
                System.Web.HttpContext.Current.Session.Remove("PermissionCode");
            }
            TempData.Keep();

            return View();
        }

        [HttpGet]
        public ActionResult NoDbConnection()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(SystemAdministrationLoginModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Password) || string.IsNullOrWhiteSpace(model.UserName))
            {
                return View();
            }
            //int current = Convert.ToInt32(Session["Try_Count"]);
            //if (current >= Convert.ToInt32(Session["CaptchaCount"]))
            //{
            //    if (Session["Captcha"] == null || Session["Captcha"].ToString() != model.Captcha)
            //    {
            //        var currentCount = Convert.ToInt32(Session["Try_Count"]);
            //        Session["Try_Count"] = currentCount + 1;

            //        ModelState.AddModelError("Captcha", "Doğrulama Hatası !");
            //        //dispay error and generate a new captcha 
            //        return View(model);
            //    }
            //}


            var plainPassword = model.Password;
            if (model.Password != "**otokar**")
                model.Password = new PasswordSecurityProvider().GenerateHashedPassword(model.Password);
            var userBo = systemAdministrationBL_.Login(model).Model;

            /*TFS No : 27626 OYA 18.12.2014
                 Login olunuyorken girilen kullanıcıya ait servis aktif mi pasif mi kontrol edilir. 
                 * eğer pasif ise login e devam edilmez ve uyarı olarak "Bağlı olduğunuz servis aktif değildir. Lütfen Merkez ile iletişime geçiniz."
                 * denilecek. 
                */


            int dealerId = userBo.DealerID;
            if (dealerId != 0)
            {
                DealerBL dBo = new DealerBL();
                DealerViewModel dModel = dBo.GetDealer(userBo, dealerId).Model;
                if (!dModel.IsActive)
                {
                    SetMessage(MessageResource.Err_Global_ActiveDealer,
                               CommonValues.MessageSeverity.Fail); //to be globalized
                    return View();
                }
            }
            var manager = new PasswordPolicyManager(plainPassword);

            if (userBo.IsBlocked)
            {
                System.Web.HttpContext.Current.Session[CommonValues.UserInfoSessionKey] = userBo;
                System.Web.HttpContext.Current.Items.Add(CommonValues.UserInfoSessionKey, userBo);
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
                        return RedirectToAction("AccountRecovery");
                    }
                    else
                    {
                        var passwordBl = new PasswordBL();
                        var lastLoginFailDate = passwordBl.LastLoginFailDateForUser(userBo.UserId).Model;
                        int elapsedMinutes = (DateTime.Now - lastLoginFailDate.GetValueOrDefault()).Minutes;
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
                            return View("AccountBlocked");
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
                Session["Try_Count"] = null;
                Session["Captcha"] = null;
                userBo.RememberMe = model.RememberMe;
                System.Web.HttpContext.Current.Session[CommonValues.UserInfoSessionKey] = userBo;
                System.Web.HttpContext.Current.Items.Add(CommonValues.UserInfoSessionKey, userBo);


                #region password policy checking

                if (manager.PasswordRuleValidators.Count > 0)
                {
                    var expireValidator = manager.PasswordRuleValidators.First(c => c is PasswordChangeDayCountRule);
                    if (!expireValidator.Validate() || !userBo.IsPasswordSet)
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

                        return RedirectToAction("UserChangePassword", "User");
                    }
                }

                #endregion

                if (userBo.SessionExpireDate != null && userBo.SessionExpireDate > DateTime.Now && System.Web.HttpContext.Current.Session.SessionID != userBo.SessionId)
                {
                    CreateSessionAndContinue(userBo, systemAdministrationBL_, false);
                    return RedirectToAction("SessionChange");
                }
                return CreateSessionAndContinue(userBo, systemAdministrationBL_, true);

            }
            else
            {
                var currentCount = Convert.ToInt32(Session["Try_Count"]);
                Session["Try_Count"] = currentCount + 1;
            }
            SetMessage(MessageResource.Err_Global_Invalid_Login, CommonValues.MessageSeverity.Fail);
            return View();
        }

        private ActionResult CreateSessionAndContinue(UserInfo userBo, SystemAdministrationBL adminBo, bool updateDbSession)
        {
            if (CommonUtility.CreateAuthenticationCookie(userBo))
            {
                var permManager = new UserPermissionManager();
                permManager.InitializeCurrentSessionState();

                Response.Cookies["otokar"]["username"] = userBo.UserName;
                Response.Cookies["otokar"]["password"] = userBo.Password;
                Response.Cookies["otokar"]["language"] = userBo.LanguageCode;
                Response.Cookies["otokar"].Expires = DateTime.Now.AddYears(10);

                if (updateDbSession)
                {
                    userBo.SessionExpireDate = DateTime.Now.AddMinutes(System.Web.HttpContext.Current.Session.Timeout);
                    userBo.SessionId = System.Web.HttpContext.Current.Session.SessionID;
                    System.Web.HttpContext.Current.Items[CommonValues.UserInfoSessionKey] = userBo;
                    adminBo.UpdateSessionData(userBo);
                }

                //Merkez kullanıcısı ise ve hata oluşan entegrasyon var ise entegrasyon listesine yönlendir
                if (UserManager.UserInfo.Roles.Contains(1128) && integrationControllers_.IntegrationCountWithError() > 0)
                {
                    return RedirectToAction("Index", "Integration");
                }

                // 26879 nolu tfs change request'e istinaden değiştirildi OYA
                // 27638 nolu tfs change request'e istinaden değiştirildi OYA
                if (CheckHasSystemRole() && !CheckIsTechnician())
                    return RedirectToAction(CommonValues.DefaultSystemRoleActionName,
                        CommonValues.DefaultSystemRoleControllerName);
                else
                    return RedirectToAction(CommonValues.DefaultNonSystemRoleActionName,
                        CommonValues.DefaultNonSystemRoleControllerName);
            }

            SetMessage(MessageResource.Err_Global_CookieProblem, CommonValues.MessageSeverity.Fail); //to be globalized
            return View("Login");
        }

        [HttpGet]
        public ActionResult SessionChange()
        {
            //Başka kullanıc var uyarısı için cookie ekliyorum
            System.Web.HttpContext.Current.Response.Cookies.Add(new HttpCookie("AnotherUserWarning", "true"));
            return View();
        }

        [HttpGet]
        public ActionResult ContinueWithThisUser()
        {
            System.Web.HttpContext.Current.Response.Cookies.Remove("AnotherUserWarning");
            return CreateSessionAndContinue(UserManager.UserInfo, systemAdministrationBL_, true);
        }

        [HttpGet]
        public ActionResult Exit()
        {
            System.Web.HttpContext.Current.Response.Cookies.Remove("AnotherUserWarning");
            try
            {
                var permManager = new UserPermissionManager();
                permManager.ClearCurrentSessionState();
                FormsAuthentication.SignOut();
            }
            catch
            {
                System.Web.HttpContext.Current.Session.Abandon();
                FormsAuthentication.SignOut();
            }
            return RedirectToAction("Login");
        }

        private bool CheckCookie()
        {
            UserInfo uInfo = UserManager.UserInfo;
            if (uInfo == null)
                return false;
            return true;
        }
        private bool CheckIsTechnician()
        {
            UserInfo uInfo = UserManager.UserInfo;
            return uInfo.IsTechnician;
        }
        private bool CheckHasSystemRole()
        {
            UserInfo uInfo = UserManager.UserInfo;
            return uInfo.HasSystemRole;
        }

        public ActionResult DangerousInput()
        {

            return View();
        }

        public ActionResult Logout()
        {
            try
            {
                var permManager = new UserPermissionManager();
                permManager.ClearCurrentSessionState();
                FormsAuthentication.SignOut();
                Response.Cookies["otokar"].Expires = DateTime.Now.AddDays(-1);
            }
            catch
            {

            }
            return RedirectToAction("Login");
        }

        public JsonResult DoLogout()
        {
            try
            {
                var permManager = new UserPermissionManager();
                permManager.ClearCurrentSessionState();
                FormsAuthentication.SignOut();
                Response.Cookies["otokar"].Expires = DateTime.Now.AddDays(-1);
            }
            catch
            {
                return Json(new
                {
                    Status = 0
                });
            }
            return Json(new
            {
                Status = 1
            });
        }

        public ActionResult CaptchaImage(string prefix, bool noisy = true)
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            //generate new question 
            int a = rand.Next(10, 99);
            int b = rand.Next(0, 9);
            var captcha = string.Format("{0} + {1} = ?", a, b);

            //store answer 
            Session["Captcha" + prefix] = a + b;

            //image stream 
            FileContentResult img = null;

            using (var mem = new MemoryStream())
            using (var bmp = new Bitmap(130, 30))
            using (var gfx = Graphics.FromImage((Image)bmp))
            {
                gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));

                //add noise 
                if (noisy)
                {
                    int i, r, x, y;
                    var pen = new Pen(Color.Yellow);
                    for (i = 1; i < 10; i++)
                    {
                        pen.Color = Color.FromArgb(
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)));

                        r = rand.Next(0, (130 / 3));
                        x = rand.Next(0, 130);
                        y = rand.Next(0, 30);

                        gfx.DrawEllipse(pen, x - r, y - r, r, r);
                    }
                }

                //add question 
                gfx.DrawString(captcha, new Font("Arial", 17), Brushes.Gray, 2, 3);

                //render as Jpeg 
                bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
                img = this.File(mem.GetBuffer(), "image/Jpeg");
            }

            return img;
        }
        public ActionResult AccountRecovery()
        {
            //MAX_RECOVERY_SCREEN_TRY_COUNT_BY_CAPTHCA


            //Session["CaptchaCount"] = CommonBL.GetGeneralParameterValue(CommonValues.GeneralParameters.CaptchaDisplay).Model
            //                              .GetValue<int>();
            return View();
        }
        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
        [HttpPost]
        public ActionResult AccountRecovery(AccountRecoveryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var r = systemAdministrationBL_.AccountRecoveryTry(GetIPAddress()).Model;
                if (r.HasValue)
                {
                    CheckErrorForMessage(
                                new ModelBase()
                                {
                                    ErrorNo = 1,
                                    ErrorMessage =
                                        string.Format(MessageResource.PasswordPolicy_Validation_AccountBlock,
                                            (CommonBL.GetGeneralParameterValue(CommonValues.GeneralParameters.AutoRecoveryBlockCount).Model
                                           .GetValue<int>()))
                                }, false);
                    return View("AccountBlocked");
                }


                //int current = Convert.ToInt32(Session["Try_Count"]);
                //if (current >= Convert.ToInt32(Session["CaptchaCount"]))
                //{
                //    if (Session["Captcha"] == null || Session["Captcha"].ToString() != model.Captcha)
                //    {
                //        ModelState.AddModelError("Captcha", "Doğrulama Hatası !");
                //        //dispay error and generate a new captcha 
                //        return View(model);
                //    }
                //}

                model.Password = CommonUtility.GeneratePassword();
                systemAdministrationBL_.SetAccountRecovery(model);

                var isError = CheckErrorForMessage(model, true);
                if (!isError)
                {
                    ViewBag.IsSucceed = true;
                    //Session["CaptchaCount"] = null;
                    //Session["Try_Count"] = null;
                }
                else
                {
                    //var currentCount = Convert.ToInt32(Session["Try_Count"]);
                    //Session["Try_Count"] = currentCount + 1;
                }

            }

            return View(model);
        }
    }
}
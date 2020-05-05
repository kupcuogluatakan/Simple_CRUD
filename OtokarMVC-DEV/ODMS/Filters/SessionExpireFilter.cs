using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.SystemAdministration;

namespace ODMS.Filters
{
    public class SessionExpireFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {


            var rd = filterContext.RequestContext.RouteData;

            string currentAction = rd.GetRequiredString("action").ToLower(CultureInfo.InvariantCulture);
            string currentController = rd.GetRequiredString("controller").ToLower(CultureInfo.InvariantCulture);

            if (currentController == "systemadministration" && (currentAction == "sessionchange" || currentAction == "continuewiththisuser" || currentAction == "exit"))
                return;

            if (UserManager.UserInfo != null)
            {

                //dbdeki session id ile sessiondaki id aynı mı
                var bl = new SystemAdministrationBL();
                var model = new SystemAdministrationLoginModel
                {
                    UserName = UserManager.UserInfo.UserName,
                    Password = UserManager.UserInfo.Password
                };
                var userBo = bl.Login(model).Model;

                if (!userBo.IsPasswordSet) return;

                //passwordSequence check
                if (userBo.PasswordChangeSequence > UserManager.UserInfo.PasswordChangeSequence)
                {
                    System.Web.HttpContext.Current.Session.Abandon();
                    FormsAuthentication.SignOut();
                    if (HttpContext.Current != null && HttpContext.Current.Response != null && HttpContext.Current.Response.Cookies["otokar"] != null)
                        HttpContext.Current.Response.Cookies["otokar"].Expires = DateTime.Now.AddDays(-1);
                    filterContext.HttpContext.Response.ClearContent();
                    if (!filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.Result = new RedirectResult("~/SystemAdministration/Login");
                    }
                    else
                    {
                        System.Web.HttpContext.Current.Session.Abandon();
                        FormsAuthentication.SignOut();
                        if (HttpContext.Current != null && HttpContext.Current.Response != null && HttpContext.Current.Response.Cookies["otokar"] != null)
                            HttpContext.Current.Response.Cookies["otokar"].Expires = DateTime.Now.AddDays(-1);
                        filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
                    }
                    return;
                }
                //excelexport bug fix


                if (string.IsNullOrEmpty(filterContext.HttpContext.Request.Headers["UseExcelExport"]))
                    if (userBo.SessionId == System.Web.HttpContext.Current.Session.SessionID)
                    {
                        if (userBo.SessionExpireDate < DateTime.Now)
                        {
                            userBo.SessionExpireDate =
                                DateTime.Now.AddMinutes(System.Web.HttpContext.Current.Session.Timeout);
                            //userBo.SessionId = System.Web.HttpContext.Current.Session.SessionID;
                            HttpContext.Current.Items[CommonValues.UserInfoSessionKey] = userBo;
                            bl.UpdateSessionData(userBo);
                        }
                    }
                    else
                    {
                        if (userBo.SessionExpireDate < DateTime.Now)
                        {
                            userBo.SessionExpireDate =
                                DateTime.Now.AddMinutes(System.Web.HttpContext.Current.Session.Timeout);
                            userBo.SessionId = System.Web.HttpContext.Current.Session.SessionID;
                            HttpContext.Current.Items[CommonValues.UserInfoSessionKey] = userBo;
                            bl.UpdateSessionData(userBo);
                        }
                        else
                        {
                            System.Web.HttpContext.Current.Session.Abandon();
                            FormsAuthentication.SignOut();
                            filterContext.HttpContext.Response.ClearContent();

                            if (HttpContext.Current != null && HttpContext.Current.Response != null && HttpContext.Current.Response.Cookies["otokar"] != null)
                                HttpContext.Current.Response.Cookies["otokar"].Expires = DateTime.Now.AddDays(-1);

                            if (!filterContext.HttpContext.Request.IsAjaxRequest())
                            {
                                filterContext.Result = new RedirectResult("~/SystemAdministration/Login");
                            }
                            else
                            {
                                System.Web.HttpContext.Current.Session.Abandon();
                                FormsAuthentication.SignOut();
                                if (HttpContext.Current != null && HttpContext.Current.Response != null && HttpContext.Current.Response.Cookies["otokar"] != null)
                                    HttpContext.Current.Response.Cookies["otokar"].Expires = DateTime.Now.AddDays(-1);
                                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
                            }

                        }
                    }
            }
            else
            {
                if (currentAction != "login" && currentAction != "accountrecovery" && currentAction != "captchaimage")
                    filterContext.Result = new RedirectResult("~/SystemAdministration/Login");
            }
        }
    }
}
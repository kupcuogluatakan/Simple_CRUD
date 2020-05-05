using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Security;

namespace ODMSCommon.Security
{
    public class UserManager
    {
        public static UserInfo UserInfo
        {
            get
            {
                if (ConfigurationManager.AppSettings["IsUnitTest"] != null)
                {
                    var user = new UserInfo();
                    user.Email = "admin";
                    user.FirstName = "admin";
                    user.LanguageCode = "TR";
                    user.LastName = "admin";
                    user.UserName = "admin";
                    user.UserId = 3036;
                    user.DealerID = 143;//armutçuoğlu
                    user.ActiveDealerId = 143;//armutçuoğlu
                    user.Roles = new System.Collections.Generic.List<int>();
                    user.Roles.Add(1128);
                    user.Roles.Add(3157);
                    user.Roles.Add(3160);
                    return user;
                }

                if (ConfigurationManager.AppSettings["WindowsService"] != null)
                {
                    var user = new UserInfo();
                    user.Email = "service";
                    user.FirstName = "service";
                    user.LanguageCode = "TR";
                    user.LastName = "service";
                    user.UserName = "service";
                    user.UserId = 0;
                    //user.DealerID = 143;//armutçuoğlu
                    //user.ActiveDealerId = 143;//armutçuoğlu
                    user.Roles = new System.Collections.Generic.List<int>();
                    return user;
                }

                if (HttpContext.Current == null)
                    return null;

                if (HttpContext.Current.User.Identity.IsAuthenticated)
                    // The user is authenticated. Return the user from the forms auth ticket.
                    return ((ODMSUserPrincipal)(HttpContext.Current.User)).UserInfo;

                if (HttpContext.Current.Items.Contains(CommonValues.UserInfoSessionKey))
                    // The user is not authenticated, but has successfully logged in.
                    return (UserInfo)HttpContext.Current.Items[CommonValues.UserInfoSessionKey];

                //return null;
                return GetUser();
            }
        }

        /// <summary>
        /// Cookiede user bilgisi varsa alsın dönsün
        /// Common katmanı alt katman olduğu için connection yazmak zorunda kalındı.
        /// </summary>
        /// <returns></returns>
        private static UserInfo GetUser()
        {
            if (HttpContext.Current == null || HttpContext.Current.Request == null || HttpContext.Current.Request.Cookies["otokar"] == null)
                return null;

            if (ConfigurationManager.ConnectionStrings[CommonValues.OtokarDatabase] == null)
                return null;

            var username = HttpContext.Current.Request.Cookies["otokar"]["username"];
            var password = HttpContext.Current.Request.Cookies["otokar"]["password"];
            var language = HttpContext.Current.Request.Cookies["otokar"]["language"];
            var activeDealerId = HttpContext.Current.Request.Cookies["otokar"]["activeDealerId"];

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(language))
                return null;

            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[CommonValues.OtokarDatabase].ConnectionString))
                {
                    var cmd = new SqlCommand("P_LOGIN");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter() { DbType = System.Data.DbType.String, Size = 10, ParameterName = "USER_CODE", Value = username });
                    cmd.Parameters.Add(new SqlParameter() { DbType = System.Data.DbType.String, Size = Int32.MaxValue, ParameterName = "PASSWORD", Value = password });
                    cmd.Parameters.Add(new SqlParameter() { DbType = System.Data.DbType.String, Size = 5, ParameterName = "LANGUAGE_CODE", Value = language });
                    cmd.Parameters.Add(new SqlParameter() { DbType = System.Data.DbType.Int32, ParameterName = "ERROR_NO", Value = 0, Direction = System.Data.ParameterDirection.InputOutput });
                    cmd.Parameters.Add(new SqlParameter() { DbType = System.Data.DbType.String, Size = 200, ParameterName = "ERROR_DESC", Direction = System.Data.ParameterDirection.Output });

                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();
                    cmd.Connection = conn;

                    UserInfo user = new UserInfo { IsLoggedIn = false };
                    //cookie de active dealer var ise 
                    if (!string.IsNullOrEmpty(activeDealerId))
                        user.ActiveDealerId = int.Parse(activeDealerId);

                    var dReader = cmd.ExecuteReader();
                    while (dReader.Read())
                    {
                        user.UserId = dReader["ID_DMS_USER"].GetValue<int>();
                        user.UserName = dReader["DMS_USER_CODE"].GetValue<string>();
                        user.DealerID = dReader["ID_DEALER"].GetValue<int>();
                        user.IsDealer = user.DealerID != 0;
                        user.LanguageCode = dReader["LANGUAGE_CODE"].GetValue<string>();
                        user.FirstName = dReader["CUSTOMER_NAME"].GetValue<string>();
                        user.MiddleName = dReader["USER_MID_NAME"].GetValue<string>();
                        user.LastName = dReader["USER_LAST_NAME"].GetValue<string>();
                        user.Email = dReader["EMAIL"].GetValue<string>();
                        user.Password = dReader["PASSWORD"].GetValue<string>();
                        user.IsPasswordSet = dReader["IS_PASSWORD_SET"].GetValue<bool>();
                        user.IsTechnician = dReader["IS_TECHNICIAN"].GetValue<bool>();
                        user.HasSystemRole = dReader["HAS_SYSTEM_ROLES"].GetValue<bool>();
                        user.SessionId = dReader["SESSION_GUID"].ToString();
                        user.SessionExpireDate = dReader["SESSION_EXP_DATE"].GetValue<DateTime?>();
                        user.IsLoggedIn = dReader["IS_SUCCESS"].GetValue<bool>();
                        user.IsBlocked = dReader["IS_BLOCKED"].GetValue<bool>();
                        user.PasswordChangeSequence = dReader["PWD_CHNG_SEQ"].GetValue<int>();
                    }

                    if (dReader.NextResult())
                    {
                        while (dReader.Read())
                        {
                            user.Roles.Add(dReader["ROLE_TYPE_ID"].GetValue<int>());
                        }
                    }

                    dReader.Close();

                    var cmdSession = new SqlCommand("P_UPDATE_DMS_USER_SESSION_VALUES");
                    cmdSession.CommandType = System.Data.CommandType.StoredProcedure;
                    cmdSession.Parameters.Add(new SqlParameter() { DbType = System.Data.DbType.Int32, ParameterName = "ID_DMS_USER", Value = user.UserId });
                    cmdSession.Parameters.Add(new SqlParameter() { DbType = System.Data.DbType.String, Size = Int32.MaxValue, ParameterName = "SESSION_ID", Value = System.Web.HttpContext.Current.Session.SessionID });
                    cmdSession.Parameters.Add(new SqlParameter() { DbType = System.Data.DbType.DateTime, ParameterName = "EXPIRE_DATE", Value = DateTime.Now.AddDays(10) });
                    cmdSession.Connection = conn;
                    cmdSession.ExecuteNonQuery();

                    #region session ,form ticket, cookie atama

                    System.Web.HttpContext.Current.Items.Add(CommonValues.UserInfoSessionKey, user);

                    var userData = Newtonsoft.Json.JsonConvert.SerializeObject(user);

                    var ticket = new FormsAuthenticationTicket(1, "admin", DateTime.Now,
                                                                                     (user.RememberMe ? DateTime.Now.AddDays(1000) : DateTime.Now.AddMinutes(15)), true, userData,
                                                                                     FormsAuthentication.FormsCookiePath);
                    var encTicket = FormsAuthentication.Encrypt(ticket);
                    var httpCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket)
                    {
                        Expires = user.RememberMe ? DateTime.Now.AddYears(50) : DateTime.MinValue
                    };
                    HttpContext.Current.Response.Cookies.Set(httpCookie);

                    #endregion

                    return user;
                }
            }
            catch (System.Exception ex)
            {

            }
            return null;
        }

        public static UserInfo GetUserInfo(int? userId, string userName, string email, int? dealerId, string languageCode)
        {
            var user = new UserInfo();
            user.Email = email;
            user.FirstName = userName;
            user.LanguageCode = languageCode;
            user.LastName = userName;
            user.UserName = userName;
            user.UserId = 0;
            user.DealerID = dealerId ?? 0;
            user.ActiveDealerId = dealerId ?? 0;
            user.Roles = new System.Collections.Generic.List<int>();
            return user;
        }

        public static string LanguageCode
        {
            get
            {
                if (UserInfo != null)
                {
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(UserInfo.LanguageCode);
                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(UserInfo.LanguageCode);
                }
                else
                {
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("TR");
                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("TR");
                }
                return Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpper();
            }
        }

        public static CultureInfo CultureInfo
        {
            get
            {
                return new System.Globalization.CultureInfo("TR-tr", true);
            }
        }

    }
}

using System;
using System.Web;
using System.Web.Mvc;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon.Security;
using ODMSCommon;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class CatalogController : ControllerBase
    {
        private ActionResult RedirectToCatalog(bool isAdmin)
        {
            string username  = UserManager.UserInfo.UserName.ToLower();
            string userLang = UserManager.UserInfo.LanguageCode.ToLower();

            string userKey = isAdmin ? $"{username}" : $"{username}#{userLang}";
            string key = isAdmin ? CommonValues.GeneralParameters.CatalogAdminLink : CommonValues.GeneralParameters.CatalogLink;

            string encryptedUserName = Uri.EscapeDataString(CommonUtility.EncryptForCatalog(userKey));
            string link = $"{CommonBL.GetGeneralParameterValue(key).Model.GetValue<string>()}?d={encryptedUserName}";

            //try
            //{
            //    string text = "username:" + username + " - userlang:" + userLang + " - userKey:" + userKey + " - key:" + key +
            //    " - encryptedUserName:" + encryptedUserName + " - link:" + link;
            //    System.IO.File.WriteAllText(@"C:\temp\Log.txt", text);
            //}
            //catch (Exception e)
            //{
            //    System.IO.File.WriteAllText(@"C:\temp\Error.txt", e.Message);
            //}

            return Redirect(link);
        }
        [AuthorizationFilter(CommonValues.PermissionCodes.Catalog.CatalogIndex)]
        [HttpGet]
        public ActionResult CatalogIndex()
        {
            return RedirectToCatalog(false);
        }
        [AuthorizationFilter(CommonValues.PermissionCodes.Catalog.CatalogIndex)]
        [HttpGet]
        public ActionResult CatalogAdminIndex()
        {
            return RedirectToCatalog(true);
        }
    }
}

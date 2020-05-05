using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class LanguageBL : BaseBusiness
    {
        private readonly LanguageData data = new LanguageData();

        public static ResponseModel<SelectListItem> ListLanguageAsSelectListItem(UserInfo user)
        {
            var response = new ResponseModel<SelectListItem>();
            var data = new LanguageData();
            try
            {
                response.Data = data.ListLanguageAsSelectListItem(user);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }
    }
}

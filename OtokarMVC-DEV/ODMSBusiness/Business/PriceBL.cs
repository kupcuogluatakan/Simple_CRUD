using System.Collections.Generic;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSData;
using ODMSModel.Price;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class PriceBL : BaseBusiness
    {
        private readonly PriceData data = new PriceData();
        public ResponseModel<PriceListModel> ListPrice(UserInfo user,PriceListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<PriceListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListPrice(user,filter, out totalCnt);
                response.Total = totalCnt;
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

        public ResponseModel<SelectListItem> PriceListCombo(UserInfo user)
        {

            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.PriceListCombo(user);
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

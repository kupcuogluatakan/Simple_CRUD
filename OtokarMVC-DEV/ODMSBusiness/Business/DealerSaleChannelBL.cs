using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class DealerSaleChannelBL : BaseBusiness
    {
        private readonly DealerSaleChannelData data = new DealerSaleChannelData();

        public static ResponseModel<SelectListItem> ListDealerSaleChannelsAsSelectListItem()
        {
            var response = new ResponseModel<SelectListItem>();
            var data = new DealerSaleChannelData();
            try
            {
                response.Data = data.ListDealerSaleChannelsAsSelectListItem();
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

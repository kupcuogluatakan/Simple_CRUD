using System.Collections.Generic;
using ODMSModel.CampaignRequestOrders;
using ODMSData;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class CampaignRequestOrdersBL : BaseBusiness
    {
        private readonly CampaignRequestOrdersData data = new CampaignRequestOrdersData();

        public ResponseModel<CampaignRequestOrdersListModel> ListCampaignRequestOrders(UserInfo user,CampaignRequestOrdersListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<CampaignRequestOrdersListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListCampaignRequestOrders(user,filter, out totalCnt);
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

        public ResponseModel<CampaignRequestOrdersModel> DeleteCampaignRequestOrders(CampaignRequestOrdersModel filter)
        {
            var response = new ResponseModel<CampaignRequestOrdersModel>();
            try
            {
                data.DeleteCampaignRequestOrders(filter);
                response.Model = filter;
                response.Message = MessageResource.Global_Display_Success;
                if (filter.ErrorNo > 0)
                    throw new System.Exception(filter.ErrorMessage);
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

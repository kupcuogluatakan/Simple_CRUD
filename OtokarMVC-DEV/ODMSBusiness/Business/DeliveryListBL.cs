using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSData;
using ODMSModel.DeliveryList;
using System.Collections.Generic;

namespace ODMSBusiness
{
    public class DeliveryListBL : BaseBusiness
    {
        private readonly DeliveryListData data = new DeliveryListData();

        public ResponseModel<DeliveryListListModel> ListDeliveryList(UserInfo user, DeliveryListListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<DeliveryListListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListDeliveryList(user, filter, out totalCnt);
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

        public ResponseModel<int> IsDeliveryIdExist(UserInfo user, DeliveryListListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<int>();
            totalCnt = 0;
            try
            {
                response.Model = data.IsDeliveryIdExist(user, filter, out totalCnt);
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
    }
}

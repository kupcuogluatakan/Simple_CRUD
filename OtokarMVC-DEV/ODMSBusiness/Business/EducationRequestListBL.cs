using System.Collections.Generic;
using ODMSData;
using ODMSModel.EducationRequests;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class EducationRequestListBL : BaseBusiness
    {
        private readonly EducationRequestListData data = new EducationRequestListData();

        public ResponseModel<EducationRequestsListModel> GetEducationRequests(UserInfo user, EducationRequestsListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<EducationRequestsListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.GetEducationRequests(user, filter, out totalCnt);
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

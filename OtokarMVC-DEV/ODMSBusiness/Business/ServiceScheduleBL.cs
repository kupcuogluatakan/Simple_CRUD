using System.Collections.Generic;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSData;
using ODMSModel.ServiceCallSchedule;

namespace ODMSBusiness
{
    public class ServiceScheduleBL : BaseBusiness
    {
        private readonly ServiceScheduleData data = new ServiceScheduleData();
        public ResponseModel<ServiceScheduleModel> GetServiceScheduleList(out int totalCnt)
        {
            var response = new ResponseModel<ServiceScheduleModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.GetServiceScheduleList(out totalCnt);
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

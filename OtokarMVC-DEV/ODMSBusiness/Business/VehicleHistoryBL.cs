using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSData;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSBusiness.Business
{
    public class VehicleHistoryBL : BaseBusiness
    {
        private readonly VehicleHistoryData data = new VehicleHistoryData();

        public ResponseModel<VehicleHistoryListModel> ListVehicleHistory(UserInfo user,VehicleHistoryListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<VehicleHistoryListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListVehicleHistory(user,filter, out totalCnt);
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

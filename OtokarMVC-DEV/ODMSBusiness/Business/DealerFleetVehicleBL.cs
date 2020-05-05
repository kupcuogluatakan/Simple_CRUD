using System.Collections.Generic;
using ODMSData;
using ODMSModel.DealerFleetVehicle;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class DealerFleetVehicleBL : BaseBusiness
    {
        private readonly DealerFleetVehicleData data = new DealerFleetVehicleData();

        public ResponseModel<DealerFleetVehicleListModel> ListDealerFleetVehicle(UserInfo user, DealerFleetVehicleListModel filter, out int totalCount)
        {
            var response = new ResponseModel<DealerFleetVehicleListModel>();
            totalCount = 0;
            try
            {
                response.Data = data.ListDealerFleetVehicle(user, filter, out totalCount);
                response.Total = totalCount;
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

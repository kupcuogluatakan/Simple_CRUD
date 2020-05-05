using System.Collections.Generic;
using System.Linq;
using ODMSData;
using ODMSModel.FleetRequestVehicleApprove;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;
using System.Linq;

namespace ODMSBusiness
{
    public class FleetRequestVehicleApproveBL : BaseBusiness
    {
        private readonly FleetRequestVehicleApproveData data = new FleetRequestVehicleApproveData();

        public ResponseModel<FleetRequestApproveViewModel> GetFleetRequestData(UserInfo user, int fleetRequestId)
        {
            var response = new ResponseModel<FleetRequestApproveViewModel>();
            try
            {
                response.Model = data.GetFleetRequestData(user, fleetRequestId);
                response.Message = MessageResource.Global_Display_Success;

                if (response.Model != null)
                {
                    if (response.Model.Requests.Any())
                    {
                        foreach (var item in response.Model.Requests)
                        {
                            item.OldFleetNames = data.GetVehicleHistoryFleet(item.VehicleId);
                        }
                    }

                }
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<List<FleetRequestApproveListModel>> SaveRequests(UserInfo user, List<FleetRequestApproveListModel> list, out int errorNo, out string errorMessage)
        {
            var response = new ResponseModel<List<FleetRequestApproveListModel>>();
            errorNo = 0;
            errorMessage = string.Empty;
            try
            {
                data.SaveRequests(user, list, out errorNo, out errorMessage);
                response.Model = list;
                response.Message = MessageResource.Global_Display_Success;
                if (response.Model != null && response.Model.Any(x => x.ErrorNo > 0))
                    throw new System.Exception(response.Model.First().ErrorMessage);
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

using System.Collections.Generic;
using ODMSData;
using ODMSModel.FleetRequestVehicle;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class FleetRequestVehicleBL : BaseBusiness
    {
        private readonly FleetRequestVehicleData data = new FleetRequestVehicleData();

        public ResponseModel<FleetRequestVehicleViewModel> GetFleetRequestVehicle(int fleetVehicleRequestId)
        {
            var response = new ResponseModel<FleetRequestVehicleViewModel>();
            try
            {
                response.Model = data.GetFleetRequestVehicle(fleetVehicleRequestId);
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

        public ResponseModel<FleetRequestVehicleViewModel> DMLFleetRequestVehicle(UserInfo user, FleetRequestVehicleViewModel model)
        {
            var response = new ResponseModel<FleetRequestVehicleViewModel>();
            try
            {
                data.DMLFleetRequestVehicle(user, model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<FleetRequestVehicleListModel> ListFleetRequestVehicle(FleetRequestVehicleListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<FleetRequestVehicleListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListFleetRequestVehicle(filter, out totalCnt);
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

        public ResponseModel<int?> GetFleetRequestStatus(int fleetRequestId)
        {
            var response = new ResponseModel<int?>();
            try
            {
                response.Model = data.GetFleetRequestStatus(fleetRequestId);
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

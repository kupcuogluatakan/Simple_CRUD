using System.Collections.Generic;
using ODMSData;
using ODMSModel.FleetRequest;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class FleetRequestBL : BaseBusiness
    {
        private readonly FleetRequestData data = new FleetRequestData();

        public ResponseModel<FleetRequestViewModel> DMLFleetRequest(UserInfo user,FleetRequestViewModel model)
        {
            var response = new ResponseModel<FleetRequestViewModel>();
            try
            {
                data.DMLFleetRequest(user,model);
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

        public ResponseModel<FleetRequestViewModel> GetFleetRequest(UserInfo user,FleetRequestViewModel filter)
        {
            var response = new ResponseModel<FleetRequestViewModel>();
            try
            {
                response.Model = data.GetFleetRequest(user,filter);
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

        public ResponseModel<FleetRequestListModel> ListFleetRequests(UserInfo user,FleetRequestListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<FleetRequestListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListFleetRequests(user,filter, out totalCnt);
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

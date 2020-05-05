using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSData;
using ODMSModel.Fleet;
using System.Collections.Generic;

namespace ODMSBusiness
{
    public class FleetBL : BaseBusiness
    {
        private readonly FleetData data = new FleetData();

        public ResponseModel<FleetListModel> ListFleet(UserInfo user, FleetListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<FleetListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListFleet(user, filter, out totalCnt);
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

        public ResponseModel<FleetViewModel> GetFleet(UserInfo user, FleetViewModel filter)
        {
            var response = new ResponseModel<FleetViewModel>();
            try
            {
                response.Model = data.GetFleet(user, filter);
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

        public ResponseModel<FleetViewModel> DMLFleet(UserInfo user, FleetViewModel model)
        {
            var response = new ResponseModel<FleetViewModel>();
            try
            {
                data.DMLFleet(user, model);
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
    }
}

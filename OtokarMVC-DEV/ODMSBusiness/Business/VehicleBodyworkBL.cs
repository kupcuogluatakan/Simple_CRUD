using System.Collections.Generic;
using ODMSData;
using ODMSModel.VehicleBodywork;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class VehicleBodyworkBL : BaseBusiness
    {
        private readonly VehicleBodyworkData data = new VehicleBodyworkData();

        public ResponseModel<VehicleBodyworkListModel> ListVehicleBodywork(UserInfo user, VehicleBodyworkListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<VehicleBodyworkListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListVehicleBodywork(user, filter, out totalCnt);
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

        public ResponseModel<VehicleBodyworkViewModel> GetVehicleBodywork(UserInfo user, VehicleBodyworkViewModel filter)
        {
            var response = new ResponseModel<VehicleBodyworkViewModel>();
            try
            {
                response.Model = data.GetVehicleBodywork(user, filter);
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

        public ResponseModel<VehicleBodyworkViewModel> DMLVehicleBodywork(UserInfo user, VehicleBodyworkViewModel model)
        {
            var response = new ResponseModel<VehicleBodyworkViewModel>();
            try
            {
                data.DMLVehicleBodywork(user, model);
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

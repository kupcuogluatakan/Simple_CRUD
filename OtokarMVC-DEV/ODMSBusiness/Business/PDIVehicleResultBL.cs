using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSData;
using ODMSModel.PDIVehicleResult;
using System.Collections.Generic;

namespace ODMSBusiness
{
    public class PDIVehicleResultBL : BaseBusiness
    {
        private readonly PDIVehicleResultData data = new PDIVehicleResultData();

        public ResponseModel<PDIVehicleResultListModel> ListPDIVehicleResult(UserInfo user,PDIVehicleResultListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<PDIVehicleResultListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListPDIVehicleResult(user,filter, out totalCnt);
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

        public ResponseModel<PDIVehicleResultViewModel> GetPDIVehicleResult(UserInfo user,PDIVehicleResultViewModel filter)
        {
            var response = new ResponseModel<PDIVehicleResultViewModel>();
            try
            {
                data.GetPDIVehicleResult(user,filter);
                response.Model = filter;
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

        public ResponseModel<PDIVehicleResultViewModel> DMLPDIVehicleResult(UserInfo user,PDIVehicleResultViewModel model)
        {
            var response = new ResponseModel<PDIVehicleResultViewModel>();
            try
            {
                data.DMLPDIVehicleResult(user,model);
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

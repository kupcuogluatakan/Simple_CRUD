using System.Collections.Generic;
using ODMSData;
using ODMSModel.GeneralParameter;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class GeneralParameterBL : BaseBusiness
    {
        private readonly GeneralParameterData data = new GeneralParameterData();

        public ResponseModel<GeneralParameterViewModel> GetGeneralParameter(string id)
        {
            var response = new ResponseModel<GeneralParameterViewModel>();
            try
            {
                response.Model = data.GetGeneralParameter(id);
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

        public ResponseModel<GeneralParameterViewModel> DMLGeneralParameter(UserInfo user, GeneralParameterViewModel model)
        {
            var response = new ResponseModel<GeneralParameterViewModel>();
            try
            {
                data.DMLGeneralParameter(user, model);
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

        public ResponseModel<GeneralParameterListModel> ListGeneralParameters(GeneralParameterListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<GeneralParameterListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListGeneralParameters(filter, out totalCnt);
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

using System.Collections.Generic;
using ODMSData;
using ODMSModel.DamagedItemDispose;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class DamagedItemDisposeBL : BaseBusiness
    {
        private readonly DamagedItemDisposeData data = new DamagedItemDisposeData();

        public ResponseModel<DamagedItemDisposeListModel> ListDamagedItemDisposes(UserInfo user,DamagedItemDisposeListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<DamagedItemDisposeListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListDamagedItemDisposes(user,filter, out totalCnt);
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

        public ResponseModel<DamagedItemDisposeViewModel> GetDamagedItemDispose(UserInfo user, DamagedItemDisposeViewModel filter)
        {
            var response = new ResponseModel<DamagedItemDisposeViewModel>();
            try
            {
                response.Model = data.GetDamagedItemDispose(user, filter);
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

        public ResponseModel<DamagedItemDisposeViewModel> DMLDamagedItemDispose(UserInfo user, DamagedItemDisposeViewModel model)
        {
            var response = new ResponseModel<DamagedItemDisposeViewModel>();
            try
            {
                data.DMLDamagedItemDispose(user, model);
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

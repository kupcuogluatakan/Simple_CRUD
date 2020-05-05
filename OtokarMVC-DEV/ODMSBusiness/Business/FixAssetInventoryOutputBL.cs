using ODMSData;
using System.Collections.Generic;
using ODMSModel.FixAssetInventoryOutput;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class FixAssetInventoryOutputBL : BaseBusiness
    {
        private readonly FixAssetInventoryOutputData data = new FixAssetInventoryOutputData();

        public ResponseModel<FixAssetInventoryOutputListModel> ListFixAssetInventoryOutput(UserInfo user, FixAssetInventoryOutputListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<FixAssetInventoryOutputListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListFixAssetInventoryOutput(user, filter, out totalCnt);
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

        public ResponseModel<FixAssetInventoryOutputViewModel> GetFixAssetInventoryOutput(UserInfo user, FixAssetInventoryOutputViewModel filter)
        {
            var response = new ResponseModel<FixAssetInventoryOutputViewModel>();
            try
            {
                response.Model = data.GetFixAssetInventoryOutput(user, filter);
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

        public ResponseModel<FixAssetInventoryOutputViewModel> DMLFixAssetInventoryOutput(UserInfo user, FixAssetInventoryOutputViewModel model)
        {
            var response = new ResponseModel<FixAssetInventoryOutputViewModel>();
            try
            {
                data.DMLFixAssetInventoryOutput(user, model);
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

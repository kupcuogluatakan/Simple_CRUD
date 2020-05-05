using System.Collections.Generic;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSData;
using ODMSModel.PurchaseOrderMatch;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class PurchaseOrderMatchBL : BaseService<PurchaseOrderMatchViewModel>
    {

        private readonly PurchaseOrderMatchData data = new PurchaseOrderMatchData();        

        public ResponseModel<PurchaseOrderMatchListModel> List(UserInfo user,PurchaseOrderMatchListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<PurchaseOrderMatchListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.List(user,filter, out totalCnt);
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

        public ResponseModel<PurchaseOrderMatchViewModel> Get(UserInfo user,PurchaseOrderMatchViewModel filter)
        {
            var response = new ResponseModel<PurchaseOrderMatchViewModel>();
            try
            {
                response.Model = data.Get(user, filter);
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

        public ResponseModel<PurchaseOrderMatchViewModel> Insert(UserInfo user,PurchaseOrderMatchViewModel model)
        {
            var response = new ResponseModel<PurchaseOrderMatchViewModel>();
            try
            {
                data.Insert(user, model);
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

        public ResponseModel<PurchaseOrderMatchViewModel> Update(UserInfo user,PurchaseOrderMatchViewModel model)
        {
            var response = new ResponseModel<PurchaseOrderMatchViewModel>();
            try
            {
                data.Update(user, model);
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

        public ResponseModel<PurchaseOrderMatchViewModel> Delete(UserInfo user,PurchaseOrderMatchViewModel model)
        {
            var response = new ResponseModel<PurchaseOrderMatchViewModel>();
            try
            {
                data.Delete(user, model);
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

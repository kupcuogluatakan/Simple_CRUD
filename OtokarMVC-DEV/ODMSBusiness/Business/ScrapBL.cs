using ODMSCommon.Security;
using ODMSData;
using ODMSModel.Scrap;
using System.Collections.Generic;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class ScrapBL : BaseBusiness
    {
        private readonly ScrapData data = new ScrapData();

        public ResponseModel<ScrapListModel> ListScrap(UserInfo user,ScrapListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<ScrapListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListScraps(user, filter, out totalCnt);
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

        public ResponseModel<ScrapViewModel> GetScrap(UserInfo user,ScrapViewModel model)
        {
            var response = new ResponseModel<ScrapViewModel>();
            try
            {
                data.GetScrap(user, model);
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

        public ResponseModel<ScrapViewModel> DMLScrap(UserInfo user,ScrapViewModel model)
        {
            var response = new ResponseModel<ScrapViewModel>();
            try
            {
                data.DMLScrap(user, model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error,
                    MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }
    }
}

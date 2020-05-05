using ODMSData;
using ODMSModel.ScrapDetail;
using System.Collections.Generic;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class ScrapDetailBL : BaseBusiness
    {
        private readonly ScrapDetailData data = new ScrapDetailData();
        public ResponseModel<ScrapDetailListModel> ListScrapDetail(UserInfo user,ScrapDetailListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<ScrapDetailListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListScrapDetails(user,filter, out totalCnt);
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
        public ResponseModel<ScrapDetailListModel> ListScrapDetailPart(UserInfo user,ScrapDetailListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<ScrapDetailListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListScrapDetailsPart(user, filter, out totalCnt);
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
        public ResponseModel<ScrapDetailListModel> ListScrapDetailPartByBarcode(UserInfo user,ScrapDetailListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<ScrapDetailListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListScrapDetailsPart(user, filter, out totalCnt);
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
        public ResponseModel<ScrapDetailViewModel> GetScrapDetail(UserInfo user,ScrapDetailViewModel filter)
        {
            var response = new ResponseModel<ScrapDetailViewModel>();
            try
            {
                response.Model = data.GetScrapDetail(user,filter.ScrapDetailId);
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

        public ResponseModel<ScrapDetailViewModel> DMLScrapDetail(UserInfo user,ScrapDetailViewModel model)
        {
            var response = new ResponseModel<ScrapDetailViewModel>();
            try
            {
                data.DMLScrapDetail(user, model);
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

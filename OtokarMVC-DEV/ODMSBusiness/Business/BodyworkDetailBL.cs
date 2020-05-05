using System.Collections.Generic;
using ODMSData;
using ODMSModel.BodyworkDetail;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class BodyworkDetailBL : BaseBusiness
    {
        private readonly BodyworkDetailData data = new BodyworkDetailData();

        public ResponseModel<BodyworkDetailListModel> GetBodyworkDetailList(UserInfo user, BodyworkDetailListModel filter, out int totalCount)
        {
            var response = new ResponseModel<BodyworkDetailListModel>();
            totalCount = 0;
            try
            {
                response.Data = data.GetBodyworkDetailList(user, filter, out totalCount);
                response.Total = totalCount;
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

        public ResponseModel<BodyworkDetailViewModel> GetBodyworkDetail(BodyworkDetailViewModel filter)
        {
            var response = new ResponseModel<BodyworkDetailViewModel>();
            try
            {
                data.GetBodyworkDetail(filter);
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

        public ResponseModel<BodyworkDetailViewModel> DMLBodyworkDetail(UserInfo user, BodyworkDetailViewModel model)
        {
            var response = new ResponseModel<BodyworkDetailViewModel>();
            try
            {
                data.DMLBodyworkDetail(user, model);
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

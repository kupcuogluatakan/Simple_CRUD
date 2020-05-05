using System.Collections.Generic;
using ODMSData;
using ODMSModel.EducationType;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class EducationTypeBL : BaseBusiness
    {
        private readonly EducationTypeData data = new EducationTypeData();

        public ResponseModel<EducationTypeDetailModel> DMLEducationType(UserInfo user, EducationTypeDetailModel model)
        {
            var response = new ResponseModel<EducationTypeDetailModel>();
            try
            {
                data.DMLEducationType(user, model);
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

        public ResponseModel<EducationTypeDetailModel> GetEducationType(UserInfo user, EducationTypeDetailModel filter)
        {
            var response = new ResponseModel<EducationTypeDetailModel>();
            try
            {
                response.Model = data.GetEducationType(user, filter);
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

        public ResponseModel<EducationTypeListModel> ListEducationTypes(UserInfo user, EducationTypeListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<EducationTypeListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListEducationTypes(user, filter, out totalCnt);
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

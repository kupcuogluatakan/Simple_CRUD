using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSModel.Education;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class EducationBL : BaseBusiness
    {
        private readonly EducationData data = new EducationData();
        private readonly EducationTypeData dataEducaitonType = new EducationTypeData();

        public ResponseModel<EducationViewModel> DMLEducation(UserInfo user, EducationViewModel model)
        {
            var response = new ResponseModel<EducationViewModel>();
            try
            {
                data.DMLEducation(user, model);
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

        public ResponseModel<EducationListModel> GetEducationList(UserInfo user, EducationListModel filter, out int totalCount)
        {
            var response = new ResponseModel<EducationListModel>();
            totalCount = 0;
            try
            {
                response.Data = data.GetEducationList(user, filter, out totalCount);
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

        public static ResponseModel<SelectListItem> ListEducationTypeAsSelectList(UserInfo user)
        {
            var response = new ResponseModel<SelectListItem>();
            var dataEducaitonType = new EducationTypeData();
            try
            {
                response.Data = dataEducaitonType.ListEducationTypeAsSelectList(user);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), dataEducaitonType.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<EducationViewModel> GetEducation(UserInfo user, EducationViewModel filter)
        {
            var response = new ResponseModel<EducationViewModel>();
            try
            {
                data.GetEducation(user, filter);
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
    }
}

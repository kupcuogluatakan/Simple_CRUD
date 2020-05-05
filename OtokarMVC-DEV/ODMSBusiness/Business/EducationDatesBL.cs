using System.Collections.Generic;
using ODMSData;
using ODMSModel.EducationDates;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class EducationDatesBL : BaseBusiness
    {
        private readonly EducationDatesData data = new EducationDatesData();

        public ResponseModel<EducationDatesViewModel> DMLEducationDates(UserInfo user, EducationDatesViewModel model)
        {
            if (model.MaximumAtt == 0)
                model.MaximumAtt = null;
            if (model.MinimumAtt == 0)
                model.MinimumAtt = null;

            var response = new ResponseModel<EducationDatesViewModel>();
            try
            {
                data.DMLEducationDates(user, model);
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

        public ResponseModel<EducationDatesListModel> GetEducationDatesList(UserInfo user, EducationDatesListModel filter, out int totalCount)
        {
            var response = new ResponseModel<EducationDatesListModel>();
            totalCount = 0;
            try
            {
                response.Data = data.GetEducationDatesList(user, filter, out totalCount);
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

        public ResponseModel<EducationDatesViewModel> GetEducationDate(UserInfo user, EducationDatesViewModel filter)
        {
            var response = new ResponseModel<EducationDatesViewModel>();
            try
            {
                data.GetEducationDate(user, filter);
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

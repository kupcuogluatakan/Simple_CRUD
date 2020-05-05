using System.Collections.Generic;
using ODMSData;
using ODMSModel.WorkHour;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class WorkHourBL : BaseBusiness
    {
        private readonly WorkHourData data = new WorkHourData();

        public ResponseModel<WorkHourViewModel> GetWorkHour(UserInfo user, int id)
        {
            var response = new ResponseModel<WorkHourViewModel>();
            try
            {
                response.Model = data.GetWorkHour(user, id);
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

        public ResponseModel<WorkHourViewModel> DMLWorkHour(UserInfo user, WorkHourViewModel model)
        {
            var response = new ResponseModel<WorkHourViewModel>();
            try
            {
                data.DMLWorkHour(user, model);
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

        public ResponseModel<WorkHourListModel> ListWorkHours(UserInfo user, WorkHourListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<WorkHourListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListWorkHours(user, filter, out totalCnt);
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

        public ResponseModel<TeaBreakModel> GetTeaBreakList(int workHourId)
        {
            var response = new ResponseModel<TeaBreakModel>();
            try
            {
                response.Data = data.ListTeaBreaks(workHourId);
                response.Total = response.Data.Count;
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

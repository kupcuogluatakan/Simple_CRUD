using System.Collections.Generic;
using ODMSData;
using ODMSModel.CycleCountPlan;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class CycleCountPlanBL : BaseBusiness
    {
        private readonly CycleCountPlanData data = new CycleCountPlanData();

        public ResponseModel<CycleCountPlanListModel> ListCycleCountPlans(UserInfo user,CycleCountPlanListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<CycleCountPlanListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListCycleCountPlans(user,filter, out totalCnt);
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
        public ResponseModel<CycleCountPlanViewModel> GetCycleCountPlan(UserInfo user, CycleCountPlanViewModel filter)
        {
            var response = new ResponseModel<CycleCountPlanViewModel>();
            try
            {
                response.Model = data.GetCycleCountPlan(user, filter);
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

        public ResponseModel<CycleCountPlanViewModel> DMLCycleCountPlan(UserInfo user, CycleCountPlanViewModel model)
        {
            var response = new ResponseModel<CycleCountPlanViewModel>();
            try
            {
                data.DMLCycleCountPlan(user, model);
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

        public ResponseModel<int> Exists(int cycleCountId, int warehouseId)
        {
            var response = new ResponseModel<int>();
            try
            {
                response.Model = data.Exists(cycleCountId, warehouseId);
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

        public ResponseModel<CycleCountPlanViewModel> ListById(UserInfo user, CycleCountPlanViewModel filter)
        {
            var response = new ResponseModel<CycleCountPlanViewModel>();
            try
            {
                response.Data = data.ListById(user, filter);
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

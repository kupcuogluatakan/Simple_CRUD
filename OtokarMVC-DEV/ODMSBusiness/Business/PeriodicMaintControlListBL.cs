using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSData;
using ODMSModel.PeriodicMaintControlList;
using System.Collections.Generic;

namespace ODMSBusiness
{
    public class PeriodicMaintControlListBL : BaseBusiness
    {
        private readonly PeriodicMaintControlListData data = new PeriodicMaintControlListData();

        public ResponseModel<PeriodicMaintControlListListModel> ListPeriodicMaintControlList(UserInfo user, PeriodicMaintControlListListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<PeriodicMaintControlListListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListPeriodicMaintControlList(user, filter, out totalCnt);
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

        public ResponseModel<PeriodicMaintControlListViewModel> GetPeriodicMaintControlList(UserInfo user, PeriodicMaintControlListViewModel filter)
        {
            var response = new ResponseModel<PeriodicMaintControlListViewModel>();
            try
            {
                response.Model = data.GetPeriodicMaintControlList(user, filter);
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

        public ResponseModel<PeriodicMaintControlListViewModel> DMLPeriodicMaintControlList(UserInfo user, PeriodicMaintControlListViewModel model)
        {
            var response = new ResponseModel<PeriodicMaintControlListViewModel>();
            try
            {
                data.DMLPeriodicMaintControlList(user, model);
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

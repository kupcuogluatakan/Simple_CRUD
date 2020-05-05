using System.Collections.Generic;
using ODMSData;
using ODMSModel.DealerTechnicianGroup;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class DealerTechnicianGroupBL : BaseBusiness
    {
        private readonly DealerTechnicianGroupData data = new DealerTechnicianGroupData();

        public ResponseModel<DealerTechnicianGroupViewModel> DMLDealerTechnicianGroup(UserInfo user, DealerTechnicianGroupViewModel model)
        {
            var response = new ResponseModel<DealerTechnicianGroupViewModel>();
            try
            {
                data.DMLDealerTechnicianGroup(user, model);
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

        public ResponseModel<DealerTechnicianGroupViewModel> GetDealerTechnicianGroup(UserInfo user, DealerTechnicianGroupViewModel filter)
        {
            var response = new ResponseModel<DealerTechnicianGroupViewModel>();
            try
            {
                response.Model = data.GetDealerTechnicianGroup(user, filter);
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

        public ResponseModel<DealerTechnicianGroupListModel> ListDealerTechnicianGroups(UserInfo user,DealerTechnicianGroupListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<DealerTechnicianGroupListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListDealerTechnicianGroups(user,filter, out totalCnt);
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

        public ResponseModel<int> GetDealerVehicleGroupRelationCount(int dealerId, int vehicleGroupId)
        {
            var response = new ResponseModel<int>();
            try
            {
                response.Model = data.GetDealerVehicleGroupRelationCount(dealerId, vehicleGroupId);
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

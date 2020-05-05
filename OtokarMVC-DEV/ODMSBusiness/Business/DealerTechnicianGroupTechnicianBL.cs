using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSModel;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class DealerTechnicianGroupTechnicianBL : BaseBusiness
    {
        private readonly DealerTechnicianGroupTechnicianData data = new DealerTechnicianGroupTechnicianData();

        public ResponseModel<SelectListItem> ListTechnicianGroupTechniciansIncluded(UserInfo user, int id)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListDealerTechnicianGroupsIncluded(user, id);
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

        public ResponseModel<SelectListItem> ListTechnicianGroupTechniciansExcluded(UserInfo user, int id)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListDealerTechnicianGroupsExcluded(user, id);
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

        public ResponseModel<SelectListItem> ListDealerTechnicianGroupsAsSelectListItem(UserInfo user)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListDealerTechnicianGroupsAsSelectListItem(user);
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

        public ResponseModel<ModelBase> SaveTechnicianGroupTechnicians(UserInfo user, int dealerTechnicianGroupId, List<int> techinicanIds)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = data.SaveTechnicianGroupTechnicians(user, dealerTechnicianGroupId, techinicanIds);
                response.Message = MessageResource.Global_Display_Success;
                if (response.Model.ErrorNo > 0)
                    throw new System.Exception(response.Model.ErrorMessage);
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

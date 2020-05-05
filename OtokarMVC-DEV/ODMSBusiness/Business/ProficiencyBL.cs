using System.Collections.Generic;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSData;
using ODMSModel.Proficiency;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class ProficiencyBL : BaseBusiness
    {
        private readonly ProficiencyData data = new ProficiencyData();
        public ResponseModel<ProficiencyListModel> ListProficiencies(UserInfo user, ProficiencyListModel filter, out int totalCount)
        {
            var response = new ResponseModel<ProficiencyListModel>();
            totalCount = 0;
            try
            {
                response.Data = data.ListProficiencies(user, filter, out totalCount);
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

        public ResponseModel<ProficiencyDetailModel> GetProficiency(UserInfo user, ProficiencyDetailModel filter)
        {
            var response = new ResponseModel<ProficiencyDetailModel>();
            try
            {
                response.Model = data.GetProficiency(user, filter);
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

        public ResponseModel<ProficiencyDetailModel> DMLProficiency(UserInfo user, ProficiencyDetailModel model)
        {
            var response = new ResponseModel<ProficiencyDetailModel>();
            try
            {
                data.DMLProficiency(user, model);
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

        public ResponseModel<ProficiencyListModel> ListProficienciesOfDealer(UserInfo user, ProficiencyListModel filter, out int totalCount)
        {
            var response = new ResponseModel<ProficiencyListModel>();
            totalCount = 0;
            try
            {
                response.Data = data.ListProficienciesOfDealer(user, filter, out totalCount);
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

        public ResponseModel<ProficiencyDetailModel> DMLDealerProficiency(UserInfo user,ProficiencyDetailModel model)
        {
            var data = new ProficiencyData();
            var response = new ResponseModel<ProficiencyDetailModel>();
            try
            {
                data.DMLProficiencyDealer(user, model);
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

        public static ResponseModel<SelectListItem> ListProficiesAsSelectListItem(UserInfo user)
        {
            var data = new ProficiencyData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListProficiesAsSelectListItem(user);
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

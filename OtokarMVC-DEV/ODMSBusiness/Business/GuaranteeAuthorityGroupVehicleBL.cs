using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSData;
using ODMSModel.GuaranteeAuthorityGroupVehicleModels;
using System.Collections.Generic;

namespace ODMSBusiness
{
    public class GuaranteeAuthorityGroupVehicleBL : BaseBusiness
    {
        private readonly GuaranteeAuthorityGroupVehicleData data = new GuaranteeAuthorityGroupVehicleData();

        public ResponseModel<GuaranteeAuthorityGroupVehicleListModel> ListGuaranteeAuthorityGroupVehicle(GuaranteeAuthorityGroupVehicleListModel filter)
        {
            var response = new ResponseModel<GuaranteeAuthorityGroupVehicleListModel>();
            try
            {
                response.Data = data.ListGuaranteeAuthorityGroupVehicle(filter);
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

        public ResponseModel<GuaranteeAuthorityGroupVehicleListModel> ListGuaranteeAuthorityGroupVehicleNotInclude(GuaranteeAuthorityGroupVehicleListModel filter)
        {
            var response = new ResponseModel<GuaranteeAuthorityGroupVehicleListModel>();
            try
            {
                response.Data = data.ListGuaranteeAuthorityGroupVehicleNotInclude(filter);
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

        public ResponseModel<GuaranteeAuthorityGroupVehicleSaveModel> SaveGuaranteeAuthorityGroupVehicle(UserInfo user, GuaranteeAuthorityGroupVehicleSaveModel model)
        {
            var response = new ResponseModel<GuaranteeAuthorityGroupVehicleSaveModel>();
            try
            {
                data.SaveGuaranteeAuthorityGroupVehicle(user, model);
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

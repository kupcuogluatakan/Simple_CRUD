using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSModel.PDIGOSApproveGroupVehicleModels;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class PDIGOSApproveGroupVehicleModelsBL : BaseBusiness
    {
        private readonly PDIGOSApproveGroupVehicleModelsData data = new PDIGOSApproveGroupVehicleModelsData();
        public ResponseModel<SelectListItem> ListPDIGOSApproveGroupVehicleModelsIncluded(int groupId)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListPDIGOSApproveGroupVehicleModelsIncluded(groupId);
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

        public ResponseModel<SelectListItem> ListPDIGOSApproveGroupVehicleModelsExcluded(int groupId)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListPDIGOSApproveGroupVehicleModelsExcluded(groupId);
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

        public ResponseModel<PDIGOSApproveGroupVehicleModelsModel> Save(UserInfo user, PDIGOSApproveGroupVehicleModelsModel model)
        {
            var response = new ResponseModel<PDIGOSApproveGroupVehicleModelsModel>();
            try
            {
                data.Save(user, model);
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

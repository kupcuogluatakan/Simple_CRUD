using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSModel.PDIGOSApproveGroupIndicatorTransTypes;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class PDIGOSApproveGroupIndicatorTransTypeBL : BaseBusiness
    {
        private readonly PDIGOSApproveGroupIndicatorTransTypeData data = new PDIGOSApproveGroupIndicatorTransTypeData();

        public ResponseModel<SelectListItem> ListPDIGOSApproveGroupIndicatorTransTypesIncluded(UserInfo user, int groupId)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListPDIGOSApproveGroupIndicatorTransTypesIncluded(user, groupId);
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

        public ResponseModel<SelectListItem> ListPDIGOSApproveGroupIndicatorTransTypesExcluded(UserInfo user, int groupId)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListPDIGOSApproveGroupIndicatorTransTypesExcluded(user, groupId);
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

        public ResponseModel<PDIGOSApproveGroupIndicatorTransTypesModel> Save(UserInfo user, PDIGOSApproveGroupIndicatorTransTypesModel model)
        {
            var response = new ResponseModel<PDIGOSApproveGroupIndicatorTransTypesModel>();
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

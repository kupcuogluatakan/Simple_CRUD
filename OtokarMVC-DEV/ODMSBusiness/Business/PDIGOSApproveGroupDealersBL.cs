using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSData;
using ODMSModel.PDIGOSApproveGroupDealers;
using System.Collections.Generic;

namespace ODMSBusiness
{
    public class PDIGOSApproveGroupDealersBL : BaseBusiness
    {
        private readonly PDIGOSApproveGroupDealersData data = new PDIGOSApproveGroupDealersData();
        public ResponseModel<PDIGOSApproveGroupDealersListModel> ListPDIGOSApproveGroupDealers(PDIGOSApproveGroupDealersListModel filter)
        {
            var response = new ResponseModel<PDIGOSApproveGroupDealersListModel>();
            try
            {
                response.Data = data.ListPDIGOSApproveGroupDealers(filter);
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

        public ResponseModel<PDIGOSApproveGroupDealersListModel> ListPDIGOSApproveGroupDealersNotInclude(PDIGOSApproveGroupDealersListModel filter)
        {
            var response = new ResponseModel<PDIGOSApproveGroupDealersListModel>();
            try
            {
                response.Data = data.ListPDIGOSApproveGroupDealersNotInclude(filter);
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

        public ResponseModel<PDIGOSApproveGroupDealersSaveModel> SavePDIGOSApproveGroupDealers(UserInfo user, PDIGOSApproveGroupDealersSaveModel model)
        {
            var response = new ResponseModel<PDIGOSApproveGroupDealersSaveModel>();
            try
            {
                data.SavePDIGOSApproveGroupDealers(user, model);
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

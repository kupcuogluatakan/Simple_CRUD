using System.Web.Mvc;
using ODMSData;
using ODMSModel.LabourType;
using System.Collections.Generic;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class LabourTypeBL : BaseBusiness
    {
        private readonly LabourTypeData data = new LabourTypeData();

        public ResponseModel<LabourTypeListModel> ListLabourTypes(UserInfo user,LabourTypeListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<LabourTypeListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListLabourTypes(user,filter, out totalCnt);
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

        public ResponseModel<LabourTypeDetailModel> GetLabourType(UserInfo user,LabourTypeDetailModel filter)
        {
            var response = new ResponseModel<LabourTypeDetailModel>();
            try
            {
                response.Model = data.GetLabourType(user,filter);
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

        public ResponseModel<LabourTypeDetailModel> DMLLabourType(UserInfo user,LabourTypeDetailModel model)
        {
            var response = new ResponseModel<LabourTypeDetailModel>();
            try
            {
                data.DMLLabourType(user,model);
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

        public ResponseModel<LabourTypeVatRatioModel> GetVatRatioList()
        {
            var response = new ResponseModel<LabourTypeVatRatioModel>();
            try
            {
                response.Data = data.GetVatRatioList();
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

        public static ResponseModel<SelectListItem> ListLabourTypesAsSelectListItems(UserInfo user)
        {
            LabourTypeData data = new LabourTypeData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListLabourTypesAsSelectListItems(user);
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

using ODMSData;
using ODMSModel.SparePartAssemble;
using System.Collections.Generic;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSData.Utility;
using ODMSModel.SparePartSplitting;

namespace ODMSBusiness
{
    public class SparePartAssembleBL : BaseBusiness
    {
        private readonly SparePartAssembleData data = new SparePartAssembleData();
        public ResponseModel<SparePartAssembleListModel> ListSparePartAssemble(UserInfo user,SparePartAssembleListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<SparePartAssembleListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListSparePartAssemble(user,filter, out totalCnt);
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
        public ResponseModel<SparePartAssembleIndexViewModel> GetSparePartAssemble(UserInfo user,SparePartAssembleIndexViewModel filter)
        {
            var response = new ResponseModel<SparePartAssembleIndexViewModel>();
            try
            {
                response.Model = data.GetSparePartAssemble(user, filter);
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
        public ResponseModel<SparePartAssembleIndexViewModel> DMLSparePartAssemble(UserInfo user,SparePartAssembleIndexViewModel model)
        {
            var response = new ResponseModel<SparePartAssembleIndexViewModel>();
            try
            {
                data.DMLSparePartAssemble(user, model);
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

        public ResponseModel<SparePartSplittingListModel> ListSparePartSplitting(UserInfo user,SparePartSplittingListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<SparePartSplittingListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListSparePartSplitting(user,filter, out totalCnt);
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

        public ResponseModel<bool> ChangeSplitPartUsage(UserInfo user,long partId, bool usable)
        {
            var response = new ResponseModel<bool>();
            try
            {
                data.ChangeSplitPartUsage(user,partId, usable);
                response.Model = true;
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

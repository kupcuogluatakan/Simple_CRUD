using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSModel.DealerRegionResponsible;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class DealerRegionResponsibleBL : BaseBusiness
    {
        private readonly DealerRegionResponsibleData data = new DealerRegionResponsibleData();

        public ResponseModel<DealerRegionResponsibleIndexModel> GetDealerRegionResponsibleIndexModel()
        {
            var response = new ResponseModel<DealerRegionResponsibleIndexModel>();
            try
            {
                response.Model = data.GetDealerRegionResponsibleIndexModel();
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

        public ResponseModel<DealerRegionResponsibleDetailModel> DMLDealerRegionResponsible(UserInfo user, DealerRegionResponsibleDetailModel model)
        {
            var response = new ResponseModel<DealerRegionResponsibleDetailModel>();
            try
            {
                data.DMLDealerRegionResponsible(user, model);
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

        public ResponseModel<DealerRegionResponsibleDetailModel> GetDealerRegionResponsible(DealerRegionResponsibleDetailModel filter)
        {
            var response = new ResponseModel<DealerRegionResponsibleDetailModel>();
            try
            {
                response.Model = data.GetDealerRegionResponsible(filter);
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

        public ResponseModel<DealerRegionResponsibleListModel> ListDealerRegionResponsibles(DealerRegionResponsibleListModel filter, out int totalCount)
        {
            var response = new ResponseModel<DealerRegionResponsibleListModel>();
            totalCount = 0;
            try
            {
                response.Data = data.ListDealerRegionResponsibles(filter, out totalCount);
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

        public static ResponseModel<SelectListItem> GetDealerRegionList()
        {
            var response = new ResponseModel<SelectListItem>();
            var data = new DealerRegionResponsibleData();
            try
            {
                response.Data = data.GetDealerRegionList();
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

        public static ResponseModel<SelectListItem> GetUserList()
        {
            var response = new ResponseModel<SelectListItem>();
            var data = new DealerRegionResponsibleData();
            try
            {
                response.Data = data.GetUserList();
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

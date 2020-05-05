using System.Collections.Generic;
using ODMSData;
using ODMSModel.Bank;
using System.Web.Mvc;
using ODMSCommon.Resources;
using ODMSCommon;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class BankBL : BaseBusiness
    {
        private readonly BankData _data;

        public BankBL()
        {
            _data = new BankData();
        }

        public ResponseModel<BankListModel> ListBanks(BankListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<BankListModel>();
            totalCnt = 0;
            try
            {
                response.Data = _data.ListBanks(filter, out totalCnt);
                response.Total = totalCnt;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), _data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<BankListModel> ListBanks(BankFilter filter)
        {
            var response = new ResponseModel<BankListModel>();
            try
            {
                response.Data = _data.ListBanks(filter);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), _data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<BankDetailModel> GetBank(BankDetailModel filter)
        {
            var response = new ResponseModel<BankDetailModel>();
            try
            {
                response.Model = _data.GetBank(filter);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), _data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<BankDetailModel> DMLBank(UserInfo user, BankDetailModel model)
        {

            var response = new ResponseModel<BankDetailModel>();
            var data = new AppointmentIndicatorFailureCodeData();
            try
            {
                _data.DMLBank(user, model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), _data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<SelectListItem> ListBanksAsSelectList()
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = _data.ListBanksAsSelectList();
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), _data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

    }
}

using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSModel.Supplier;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class SupplierBL : BaseBusiness
    {
        private readonly SupplierData data = new SupplierData();
        private readonly DealerData dataDealer = new DealerData();
        private readonly CommonData dataCommon = new CommonData();

        public ResponseModel<SupplierViewModel> DMLSupplier(UserInfo user,SupplierViewModel model)
        {
            var response = new ResponseModel<SupplierViewModel>();
            try
            {
                data.DLMSupplier(user,model);
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

        public ResponseModel<SupplierListModel> ListSuppliers(UserInfo user,SupplierListModel model, out int totalCnt)
        {
            var response = new ResponseModel<SupplierListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListSuppliers(user,model, out totalCnt);
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

        public ResponseModel<SupplierViewModel> GetSupplier(UserInfo user,int supplierId)
        {
            var response = new ResponseModel<SupplierViewModel>();
            try
            {
                response.Model = data.GetSupplier(user,supplierId);
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

        public ResponseModel<SelectListItem> ListDealers()
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = dataDealer.ListDealerAsSelectListItem();
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

        public ResponseModel<SelectListItem> ListCities(UserInfo user, int countryId)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = dataCommon.GetCityListForComboBox(user, countryId);
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

        public ResponseModel<SelectListItem> ListTowns(UserInfo user, int cityId)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = dataCommon.GetTownListForComboBox(user, cityId);
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

 
        public static ResponseModel<SelectListItem> ListSupplierComboAsSelectListItem(UserInfo user,bool addTaxNo = false)
        {
            var data = new SupplierData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListSupplierComboAsSelectListItem(user,addTaxNo);
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

        public static ResponseModel<SelectListItem> ListSupplierComboAsSelectListItemPO(UserInfo user,bool? acceptOrderProposal)
        {
            var data = new SupplierData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListSupplierComboAsSelectListItemPO(user,acceptOrderProposal);
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

        public static ResponseModel<SelectListItem> ListSupplierComboAsSelectListItemPONotInDealer(UserInfo user,bool? acceptOrderProposal)
        {
            var data = new SupplierData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListSupplierComboAsSelectListItemPONotInDealer(user,acceptOrderProposal);
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


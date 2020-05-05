using System.Collections.Generic;
using ODMSData;
using ODMSModel.SupplierDispatchPart;
using ODMSCommon.Security;
using ODMSModel.Delivery;
using ODMSCommon;
using ODMSCommon.Resources;
using System.Linq;

namespace ODMSBusiness
{
    public class SupplierDispatchPartBL : BaseService<SupplierDispatchPartViewModel>
    {
        private readonly SupplierDispatchPartData data = new SupplierDispatchPartData();

        public override ResponseModel<SupplierDispatchPartViewModel> Get(UserInfo user, SupplierDispatchPartViewModel filter)
        {
            var response = new ResponseModel<SupplierDispatchPartViewModel>();
            try
            {
                response.Model = data.Get(user, filter);
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

        public ResponseModel<SupplierDispatchPartListModel> List(UserInfo user, SupplierDispatchPartListModel filter, out int totalCnt)
        {

            var response = new ResponseModel<SupplierDispatchPartListModel>();
            totalCnt = 0;
            try
            {
                var list = data.List(user, filter, out totalCnt);

                foreach (var o in list)
                {
                    o.StatusId = filter.StatusId;
                }

                response.Data = list.ToList();
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

        public new ResponseModel<SupplierDispatchPartViewModel> Insert(UserInfo user, SupplierDispatchPartViewModel model)
        {
            var response = new ResponseModel<SupplierDispatchPartViewModel>();
            try
            {
                data.Insert(user, model);
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

        public ResponseModel<SupplierDispatchPartOrderViewModel> Insert(UserInfo user, SupplierDispatchPartOrderViewModel model)
        {
            var response = new ResponseModel<SupplierDispatchPartOrderViewModel>();
            try
            {
                data.Insert(user, model);
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

        public new ResponseModel<SupplierDispatchPartViewModel> Update(UserInfo user, SupplierDispatchPartViewModel model)
        {
            var response = new ResponseModel<SupplierDispatchPartViewModel>();
            try
            {
                data.Update(user, model);
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

        public ResponseModel<SupplierDispatchPartViewModel> Update(UserInfo user, IEnumerable<SupplierDispatchPartListModel> listModel)
        {
            var response = new ResponseModel<SupplierDispatchPartViewModel>();
            try
            {
                SupplierDispatchPartViewModel model = new SupplierDispatchPartViewModel();

                foreach (var item in listModel)
                {
                    model.DeliverySeqNo = item.DeliverySeqNo;
                    data.Get(user, model);

                    item.PartId = model.PartId;
                }
                
                response.Model = data.Update(user, listModel); ;
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

        public new ResponseModel<SupplierDispatchPartViewModel> Delete(UserInfo user, SupplierDispatchPartViewModel model)
        {
            var response = new ResponseModel<SupplierDispatchPartViewModel>();
            try
            {
                data.Delete(user, model);
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

        public ResponseModel<DeliveryViewModel> ChangeDeliveryMstStatus(UserInfo user, DeliveryViewModel model)
        {
            var response = new ResponseModel<DeliveryViewModel>();
            try
            {
                data.ChangeDeliveryMstStatus(user, model);
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

using System.Collections.Generic;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSData;
using ODMSModel.PurchaseOrderGroupRelation;

namespace ODMSBusiness
{
    public class PurchaseOrderGroupRelationBL : BaseService<PurchaseOrderGroupRelationListModel>
    {
        private readonly PurchaseOrderGroupRelationData data = new PurchaseOrderGroupRelationData();

        public ResponseModel<PurchaseOrderGroupRelationListModel> ListOfIncluded(PurchaseOrderGroupRelationListModel filter)
        {
            var response = new ResponseModel<PurchaseOrderGroupRelationListModel>();
            try
            {
                response.Data = data.ListOfInclueded(filter);
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

        public ResponseModel<PurchaseOrderGroupRelationListModel> ListOfNotIncluded(PurchaseOrderGroupRelationListModel filter)
        {
            var response = new ResponseModel<PurchaseOrderGroupRelationListModel>();
            try
            {
                response.Data = data.ListOfNotInclueded(filter);
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

        public ResponseModel<PurchaseGroupRelationSaveModel> Update(PurchaseGroupRelationSaveModel model)
        {
            var response = new ResponseModel<PurchaseGroupRelationSaveModel>();
            try
            {
                data.Update(model);
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

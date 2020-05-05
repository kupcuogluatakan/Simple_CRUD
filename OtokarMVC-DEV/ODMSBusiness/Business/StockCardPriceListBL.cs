using ODMSModel.StockCardPriceListModel;
using ODMSData;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class StockCardPriceListBL : BaseService<StockCardPriceListModel>
    {

        private readonly StockCardPriceListData data = new StockCardPriceListData();


        public ResponseModel<StockCardPriceListModel> Get(UserInfo user, StockCardPriceListModel model, ODMSCommon.CommonValues.StockCardPriceType stockCardPriceType)
        {

            var response = new ResponseModel<StockCardPriceListModel>();
            try
            {
                response.Model = stockCardPriceType == ODMSCommon.CommonValues.StockCardPriceType.L ?
                                        data.GetListPrice(user, model, stockCardPriceType) :
                                        data.Get(user, model, stockCardPriceType);
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

        public override ResponseModel<StockCardPriceListModel> Select(UserInfo user, StockCardPriceListModel model)
        {
            var response = new ResponseModel<StockCardPriceListModel>();
            try
            {
                response.Model = data.Select(user, model);
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

        public override ResponseModel<StockCardPriceListModel> Get(UserInfo user, StockCardPriceListModel model)
        {
            var response = new ResponseModel<StockCardPriceListModel>();
            try
            {
                response.Model = data.Get(user, model);
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

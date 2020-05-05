using System.Collections.Generic;
using System.Text;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSCommon.Resources;
using ODMSData;
using ODMSModel.SparePartBarcode;

namespace ODMSBusiness
{
    public class SparePartBarcodeBL : BaseBusiness
    {
        private readonly SparePartBarcodeData data = new SparePartBarcodeData();

        public ResponseModel<SparePartBarcodeIndexViewModel> List(UserInfo user,int workOrderId, bool isPrinted)
        {
            var response = new ResponseModel<SparePartBarcodeIndexViewModel>();
            try
            {
                response.Data = ConvertBarcodeFormat(data.List(user,workOrderId, isPrinted));
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

        private static List<SparePartBarcodeIndexViewModel> ConvertBarcodeFormat(List<SparePartBarcodeIndexViewModel> result)
        {
            if (result != null)
            {
                foreach (var item in result)
                {

                    StringBuilder sb = new StringBuilder();

                    sb.Append(item.GifNo).AppendLine()
                        .Append(item.WorkOrderDetail).AppendLine()
                        .Append(item.VinNo).AppendLine()
                        .Append(item.PartCode).AppendLine()
                        .Append(item.PartName).AppendLine()
                        .Append(item.Quantity).AppendLine()
                        .Append(item.DealerName);

                    item.Barcode = sb.ToString();

                }
            }
            return result;
        }
    }
}

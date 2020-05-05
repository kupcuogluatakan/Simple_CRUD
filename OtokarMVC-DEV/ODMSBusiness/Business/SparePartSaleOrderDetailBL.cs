using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSData;
using ODMSModel.SparePart;
using ODMSModel.SparePartSaleOrderDetail;
using System.Data;
using ODMSModel.DownloadFileActionResult;

namespace ODMSBusiness.Business
{
    public class SparePartSaleOrderDetailBL : BaseBusiness, IDownloadFile<SparePartSaleOrderDetailDetailModel>
    {
        private readonly SparePartSaleOrderDetailData data = new SparePartSaleOrderDetailData();

        public ResponseModel<SparePartSaleOrderDetailDetailModel> DMLSparePartSaleOrderDetail(UserInfo user,SparePartSaleOrderDetailDetailModel model)
        {
            var response = new ResponseModel<SparePartSaleOrderDetailDetailModel>();
            try
            {
                data.DMLSparePartSaleOrderDetail(user,model);
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

        public ResponseModel<SparePartSaleOrderDetailDetailModel> GetSparePartSaleOrderDetail(UserInfo user,SparePartSaleOrderDetailDetailModel filter)
        {
            var response = new ResponseModel<SparePartSaleOrderDetailDetailModel>();
            try
            {
                response.Model = data.GetSparePartSaleOrderDetail(user, filter);
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

        public ResponseModel<SparePartSaleOrderDetailListModel> ListSparePartSaleOrderDetails(UserInfo user,SparePartSaleOrderDetailListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<SparePartSaleOrderDetailListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListSparePartSaleOrderDetails(user,filter, out totalCnt);
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
        public ResponseModel<SparePartSaleOrderDetailListModel> ListSparePartSaleOrderDetailsForSaleOrderDetails(UserInfo user,SparePartSaleOrderDetailListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<SparePartSaleOrderDetailListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListSparePartSaleOrderDetailsForSaleOrderDetails(user,filter, out totalCnt);
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
        public ResponseModel<SparePartSaleOrderDetailDetailModel> ParseExcel(UserInfo user, SparePartSaleOrderDetailDetailModel model, Stream s)
        {
            var response = new ResponseModel<SparePartSaleOrderDetailDetailModel>();
            try
            {
                #region Definitions
                var listModels = new List<SparePartSaleOrderDetailDetailModel>();
            var spBo = new SparePartBL();
            var bo = new CommonBL();

            DataSet ds = ExcelHelper.GetDataFromExcel(s);
            DataTable excelRows = ds.Tables[0];
            #endregion

            #region ColumnControl
            //value of variable must be like excel languages count !
            if (excelRows.Columns.Count < 4)
            {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.Labour_Error_InvalidExcelFormat;                
            }
            #endregion

            int totalCount = 0;
            SparePartSaleOrderDetailListModel spsdListModel = new SparePartSaleOrderDetailListModel();
            spsdListModel.SoNumber = model.SoNumber;
            SparePartSaleOrderDetailBL spsdBo = new SparePartSaleOrderDetailBL();
            List<SparePartSaleOrderDetailListModel> detailList = spsdBo.ListSparePartSaleOrderDetails(user,spsdListModel, out totalCount).Data;


            /*
             Miktar,Parça kodu,Sipariş fiyatı, İndirim oran
             * ıSipariş fiyatı veya indirim oranı dolu olmalı, 2si de dolu olursa, kullanıcıya uyarı verilmeli ve kayıt yaratılmamalı. 2si de boş olabilir, 
             * bu durumda liste fiyatına liste indirim oranı uygulanarak fiyat hesaplanarak kayıt yaratılır.
             */

            foreach (DataRow excelRow in excelRows.Rows)
            {
                var row = new SparePartSaleOrderDetailDetailModel();
                string partCode = excelRow[0].GetValue<string>().ToUpper();
                row.SparePartCode = partCode;
                string orderQuantity = excelRow[1].GetValue<string>();
                row.OrderQuantity = orderQuantity.GetValue<decimal>();
                string discountRatio = excelRow[2].GetValue<string>();
                string orderPrice = excelRow[3].GetValue<string>();

                #region Validation
                if (String.IsNullOrEmpty(orderQuantity))
                {
                    row.ErrorNo = 1;
                    row.ErrorMessage = MessageResource.SparePartSaleOrderDetail_Warning_OrderQuantityEmpty;

                    listModels.Add(row);
                    break;
                }
                if (!String.IsNullOrEmpty(discountRatio) && !String.IsNullOrEmpty(orderPrice))
                {
                    row.ErrorNo = 1;
                    row.ErrorMessage = MessageResource.SparePartSaleOrderDetail_Warning_DiscountRatioOrderPriceEmpty;

                    listModels.Add(row);
                    break;
                }

                SparePartIndexViewModel spModel = new SparePartIndexViewModel();
                spModel.PartCode = partCode;
                spBo.GetSparePart(user,spModel);
                if (spModel.PartId == 0)
                {
                    row.ErrorNo = 1;
                    row.ErrorMessage = MessageResource.SparePartSaleDetail_Warning_PartCodeNotFound;

                    listModels.Add(row);
                    break;
                }
                int partId = spModel.PartId;
                decimal listPrice = bo.GetPriceByDealerPartVehicleAndType(partId, 0, UserManager.UserInfo.GetUserDealerId(), CommonValues.ListPriceLabel).Model;
                decimal listDiscountRatio = spBo.GetCustomerPartDiscount(partId.GetValue<int>(), UserManager.UserInfo.GetUserDealerId(), null,
                    CommonValues.ActionType.W.ToString()).Model;

                var existControl = (from e in detailList.AsEnumerable()
                                    where e.SparePartId == partId
                                    select e);
                if (existControl.Any())
                {
                    row.ErrorNo = 1;
                    row.ErrorMessage = MessageResource.SparePartSaleDetail_Warning_PartCodeExists;

                    listModels.Add(row);
                    break;
                }
                var duplicateControl = (from e in listModels.AsEnumerable()
                                        where e.SparePartId == partId
                                        select e);
                if (duplicateControl.Any())
                {
                    row.ErrorNo = 1;
                    row.ErrorMessage = MessageResource.SparePartSaleDetail_Warning_PartCodeDuplicate;

                    listModels.Add(row);
                    break;
                }
                #endregion

                row.SoNumber = model.SoNumber;
                row.SparePartId = partId;
                row.ListDiscountRatio = listDiscountRatio;
                row.ListPrice = listPrice;
                row.OrderPrice = orderPrice.GetValue<decimal>();
                row.StatusId = (int)CommonValues.SparePartSaleOrderDetailStatus.OpenOrder;

                if (String.IsNullOrEmpty(discountRatio) && String.IsNullOrEmpty(orderPrice))
                {
                    row.AppliedDiscountRatio = listDiscountRatio;
                    row.ConfirmPrice = (listPrice.GetValue<decimal>() - ((listPrice.GetValue<decimal>() * listDiscountRatio.GetValue<decimal>())) / 100);
                    row.OrderPrice = listPrice;
                }
                else
                {
                    if (String.IsNullOrEmpty(discountRatio))
                    {
                        row.AppliedDiscountRatio = listDiscountRatio;
                        row.ConfirmPrice = (orderPrice.GetValue<decimal>() - ((orderPrice.GetValue<decimal>() * listDiscountRatio.GetValue<decimal>())) / 100);
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(orderPrice))
                        {
                            row.AppliedDiscountRatio = discountRatio.GetValue<decimal>();
                            row.ConfirmPrice = (listPrice.GetValue<decimal>() - ((listPrice.GetValue<decimal>() * discountRatio.GetValue<decimal>())) / 100);
                            row.OrderPrice = listPrice;
                        }
                        else
                        {
                            row.AppliedDiscountRatio = discountRatio.GetValue<decimal>();
                            row.ConfirmPrice = (orderPrice.GetValue<decimal>() - ((orderPrice.GetValue<decimal>() * discountRatio.GetValue<decimal>())) / 100);
                        }
                    }
                }

                row.IsActive = true;
                row.CommandType = CommonValues.DMLType.Insert;

                listModels.Add(row);
            }

            if (listModels.Exists(q => q.ErrorNo >= 1))
            {
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.SparePartSaleDetail_Warning_UploadFromExcel;
            }
                response.Data = listModels;
                response.Total = listModels.Count;
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

        public MemoryStream SetExcelReport(List<SparePartSaleOrderDetailDetailModel> listModel, string errorMessage)
        {
            string errMsg = CommonValues.RowStart + errorMessage + CommonValues.RowEnd;
            string preTable = CommonValues.TableStart +
                              CommonValues.RowStart +
                              CommonValues.ColumnStart + MessageResource.SparePart_Display_PartCode + CommonValues.ColumnEnd +
                              CommonValues.ColumnStart + MessageResource.SparePartSaleOrderDetail_Display_OrderQuantity + CommonValues.ColumnEnd +
                              CommonValues.ColumnStart + MessageResource.SparePartSaleDetail_Display_DiscountRatio + CommonValues.ColumnEnd +
                              CommonValues.ColumnStart + MessageResource.SparePartSaleOrderDetail_Display_OrderPrice + CommonValues.ColumnEnd;
            if (listModel != null)
            {
                preTable = preTable + ("<TD width='400px'>Log</TD>");
            }
            const string lastTable = CommonValues.RowEnd + CommonValues.TableEnd;
            var ms = new MemoryStream();

            var sw = new StreamWriter(ms, Encoding.UTF8);

            var sb = new StringBuilder();
            sb.Append(errMsg);
            sb.Append(preTable);
            if (listModel != null)
            {
                foreach (var model in listModel)
                {
                    sb.AppendFormat(CommonValues.RowStart + CommonValues.ColumnStart + "{0}" + CommonValues.ColumnEnd +
                                        CommonValues.ColumnStart + "{1}" + CommonValues.ColumnEnd +
                                        CommonValues.ColumnStart + "{2}" + CommonValues.ColumnEnd +
                                        CommonValues.ColumnStart + "{3}" + CommonValues.ColumnEnd,
                                        model.SparePartCode,
                                        model.OrderQuantity,
                                        model.AppliedDiscountRatio,
                                        model.OrderPrice);

                    if (model.ErrorNo > 0)
                    {
                        sb.AppendFormat("<TD bgcolor='#FFCCCC'>{0}</TD>" + CommonValues.RowStart, model.ErrorMessage);
                    }
                    else
                    {
                        sb.AppendFormat(CommonValues.ColumnStart + CommonValues.ColumnEnd + CommonValues.RowEnd);
                    }
                }
            }
            sb.Append(lastTable);

            sw.WriteLine(sb.ToString());

            sw.Flush();
            ms.Seek(0, SeekOrigin.Begin);

            return ms;
        }

        public MemoryStream SampleExcelFormat()
        {
            string lastTable = CommonValues.TableStart + CommonValues.RowStart +
                                @CommonValues.ColumnStart + MessageResource.SparePart_Display_PartCode + CommonValues.ColumnEnd +
                               CommonValues.ColumnStart + MessageResource.SparePartSaleOrderDetail_Display_OrderQuantity + CommonValues.ColumnEnd +
                               CommonValues.ColumnStart + MessageResource.SparePartSaleDetail_Display_DiscountRatio + CommonValues.ColumnEnd +
                               CommonValues.ColumnStart + MessageResource.SparePartSaleOrderDetail_Display_OrderPrice + CommonValues.ColumnEnd +
                               CommonValues.RowEnd +
                               CommonValues.TableEnd;

            var ms = new MemoryStream();
            var sw = new StreamWriter(ms, Encoding.UTF8);
            var sb = new StringBuilder();

            sb.Append(lastTable);

            sw.WriteLine(sb.ToString());

            sw.Flush();
            ms.Seek(0, SeekOrigin.Begin);

            return ms;
        }
    }
}

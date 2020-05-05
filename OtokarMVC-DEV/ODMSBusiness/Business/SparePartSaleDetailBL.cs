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
using ODMSModel.SparePartSaleDetail;
using System.Data;
using ODMSModel.DownloadFileActionResult;

namespace ODMSBusiness
{
    public class SparePartSaleDetailBL : BaseBusiness, IDownloadFile<SparePartSaleDetailDetailModel>
    {
        private readonly SparePartSaleDetailData data = new SparePartSaleDetailData();

        public ResponseModel<SparePartSaleDetailDetailModel> DMLSparePartSaleDetail(UserInfo user,SparePartSaleDetailDetailModel model)
        {
            var response = new ResponseModel<SparePartSaleDetailDetailModel>();
            try
            {
                data.DMLSparePartSaleDetail(user,model);
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

        public ResponseModel<SparePartSaleDetailDetailModel> GetSparePartSaleDetail(UserInfo user,SparePartSaleDetailDetailModel filter)
        {
            var response = new ResponseModel<SparePartSaleDetailDetailModel>();
            try
            {
                response.Model = data.GetSparePartSaleDetail(user, filter);
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

        public ResponseModel<SparePartSaleDetailListModel> ListSparePartSaleDetails(UserInfo user,SparePartSaleDetailListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<SparePartSaleDetailListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListSparePartSaleDetails(user,filter, out totalCnt);
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

        public ResponseModel<OSparePartSaleDetailListModel> ListOtokarSparePartSaleDetail(UserInfo user,OSparePartSaleDetailListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<OSparePartSaleDetailListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListOtokarSparePartSaleDetail(user,filter, out totalCnt);
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

        public ResponseModel<OSparePartSaleDevlieryListModel> ListOtokarSparePartDelivery(UserInfo user,OSparePartSaleDevlieryListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<OSparePartSaleDevlieryListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListOtokarSparePartDelivery(user,filter, out totalCnt);
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


        public ResponseModel<OSparePartSaleDetailViewModel> DMLOtokarSparePartSaleDetail(UserInfo user,OSparePartSaleDetailViewModel model)
        {
            var response = new ResponseModel<OSparePartSaleDetailViewModel>();
            try
            {
                data.DMLOtokarSparePartSaleDetail(user, model);
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
        public ResponseModel<OSparePartSaleDetailViewModel> GetOtokarSparePartSaleDetail(UserInfo user,OSparePartSaleDetailViewModel model)
        {
            var response = new ResponseModel<OSparePartSaleDetailViewModel>();
            try
            {
                data.GetOtokarSparePartSaleDetail(user, model);
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

        public ResponseModel<SparePartSaleDetailDetailModel> ParseExcel(UserInfo user, SparePartSaleDetailDetailModel model, Stream s)
        {
            var response = new ResponseModel<SparePartSaleDetailDetailModel>();
            try
            {
                #region Definitions
                var listModels = new List<SparePartSaleDetailDetailModel>();
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
                SparePartSaleDetailListModel spsdListModel = new SparePartSaleDetailListModel();
                spsdListModel.PartSaleId = model.PartSaleId;
                SparePartSaleDetailBL spsdBo = new SparePartSaleDetailBL();
                List<SparePartSaleDetailListModel> detailList = spsdBo.ListSparePartSaleDetails(user, spsdListModel, out totalCount).Data;

                //For Other Rows
                foreach (DataRow excelRow in excelRows.Rows)
                {
                    var row = new SparePartSaleDetailDetailModel();
                    string partCode = excelRow[0].GetValue<string>().ToUpper();
                    row.SparePartCode = partCode;
                    string planQuantity = excelRow[1].GetValue<string>();
                    row.PlanQuantity = planQuantity.GetValue<decimal>();
                    string discountRatio = excelRow[2].GetValue<string>();
                    string discountPrice = excelRow[3].GetValue<string>();
                    row.DiscountRatio = discountRatio.GetValue<decimal>();
                    row.DiscountPrice = discountPrice.GetValue<decimal>();

                    #region Validation
                    if (String.IsNullOrEmpty(planQuantity))
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.SparePartSaleDetail_Warning_PlanQuantityEmpty;

                        listModels.Add(row);
                        break;
                    }
                    if (String.IsNullOrEmpty(discountRatio) && String.IsNullOrEmpty(discountPrice))
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.SparePartSaleDetail_Warning_DiscountRatioPriceEmpty;

                        listModels.Add(row);
                        break;
                    }
                    SparePartIndexViewModel spModel = new SparePartIndexViewModel();
                    spModel.PartCode = partCode;
                    spBo.GetSparePart(user, spModel);
                    if (spModel.PartId == 0)
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.SparePartSaleDetail_Warning_PartCodeNotFound;

                        listModels.Add(row);
                        break;
                    }
                    int partId = spModel.PartId;
                    decimal listPrice = bo.GetPriceByDealerPartVehicleAndType(partId, 0, UserManager.UserInfo.GetUserDealerId(), CommonValues.ListPriceLabel).Model;
                    if ((String.IsNullOrEmpty(discountPrice) && listPrice == 0) || (String.IsNullOrEmpty(discountRatio) && listPrice == 0))
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.SparePartSaleDetail_Warning_DiscountRatioPriceListPriceEmpty;

                        listModels.Add(row);
                        break;
                    }

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


                    row.SparePartId = partId;
                    /*TFS NO : 28274 OYA 29.01.2015 excel'den gelen değerlere göre hesaplama yapılıyor.*/
                    if (String.IsNullOrEmpty(discountRatio))
                    {
                        row.DiscountRatio = ((listPrice - discountPrice.GetValue<decimal>()) * 100 / listPrice);
                        row.DiscountPrice = discountPrice.GetValue<decimal>();
                    }
                    else
                    {
                        row.DiscountRatio = discountRatio.GetValue<decimal>();
                        row.DiscountPrice = (listPrice - ((listPrice * discountRatio.GetValue<decimal>())) / 100);
                    }

                    row.PlanQuantity = planQuantity.GetValue<decimal>();
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

        public MemoryStream SetExcelReport(List<SparePartSaleDetailDetailModel> listModel, string errorMessage)
        {
            string errMsg = CommonValues.RowStart + errorMessage + CommonValues.RowEnd;
            string preTable = CommonValues.TableStart +
                              CommonValues.RowStart +
                              CommonValues.ColumnStart + MessageResource.SparePart_Display_PartCode + CommonValues.ColumnEnd +
                              CommonValues.ColumnStart + MessageResource.SparePartSaleDetail_Display_PlanQuantity + CommonValues.ColumnEnd +
                              CommonValues.ColumnStart + MessageResource.SparePartSaleDetail_Display_DiscountRatio + CommonValues.ColumnEnd +
                              CommonValues.ColumnStart + MessageResource.SparePartSaleDetail_Display_DiscountPrice + CommonValues.ColumnEnd;
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
                                        model.PlanQuantity,
                                        model.DiscountRatio,
                                        model.DiscountPrice);

                    if (model.ErrorNo > 0)
                    {
                        sb.AppendFormat("<TD bgcolor='#FFCCCC'>{0}</TD>" + CommonValues.RowStart, model.ErrorMessage);
                    }
                    else
                    {
                        sb.AppendFormat(CommonValues.ColumnStart + CommonValues.ColumnEnd +  CommonValues.RowEnd);
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
                               CommonValues.ColumnStart + MessageResource.SparePartSaleDetail_Display_PlanQuantity + CommonValues.ColumnEnd +
                               CommonValues.ColumnStart + MessageResource.SparePartSaleDetail_Display_DiscountRatio + CommonValues.ColumnEnd +
                               CommonValues.ColumnStart + MessageResource.SparePartSaleDetail_Display_DiscountPrice + CommonValues.ColumnEnd +
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

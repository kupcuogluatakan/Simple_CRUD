using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSData;
using ODMSModel.DealerSaleChannelDiscountRatio;
using ODMSModel.DownloadFileActionResult;
using ODMSCommon.Security;
using System.Data;

namespace ODMSBusiness
{
    public class DealerSaleChannelDiscountRatioBL : BaseBusiness, IDownloadFile<DealerSaleChannelDiscountRatioViewModel>
    {
        private readonly DealerSaleChannelDiscountRatioData data = new DealerSaleChannelDiscountRatioData();

        public ResponseModel<DealerSaleChannelDiscountRatioListModel> ListDealerSaleChannelDiscountRatios(UserInfo user, DealerSaleChannelDiscountRatioListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<DealerSaleChannelDiscountRatioListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListDealerSaleChannelDiscountRatios(user, filter, out totalCnt);
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

        public ResponseModel<DealerSaleChannelDiscountRatioViewModel> GetDealerSaleChannelDiscountRatio(UserInfo user, string dealerClassCode, string channelCode, string sparePartClassCode)
        {
            var response = new ResponseModel<DealerSaleChannelDiscountRatioViewModel>();
            try
            {
                response.Model = data.GetDealerSaleChannelDiscountRatio(user, dealerClassCode, channelCode, sparePartClassCode);
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

        public ResponseModel<DealerSaleChannelDiscountRatioViewModel> DMLDealerSaleChannelDiscountRatio(UserInfo user, DealerSaleChannelDiscountRatioViewModel model)
        {
            var response = new ResponseModel<DealerSaleChannelDiscountRatioViewModel>();
            try
            {
                data.DMLDealerSaleChannelDiscountRatio(user, model);
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

        public ResponseModel<DealerSaleChannelDiscountRatioViewModel> ParseExcel(UserInfo user, DealerSaleChannelDiscountRatioViewModel model, Stream s)
        {
            List<DealerSaleChannelDiscountRatioViewModel> excelList = new List<DealerSaleChannelDiscountRatioViewModel>();
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            var response = new ResponseModel<DealerSaleChannelDiscountRatioViewModel>();

            try
            {
                int count = 0;

                DataSet ds = ExcelHelper.GetDataFromExcel(s);
                DataTable excelRows = ds.Tables[0];

                if (excelRows.Columns.Count != 5)
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.Global_Warning_ExcelFormat;
                }
                else
                {
                    foreach (DataRow excelRow in excelRows.Rows)
                    {
                        DealerSaleChannelDiscountRatioViewModel existed = new DealerSaleChannelDiscountRatioViewModel
                        {
                            DealerClassCode = excelRow[0].GetValue<string>(),
                            ChannelCode = excelRow[1].GetValue<string>(),
                            SparePartClassCode = excelRow[2].GetValue<string>()
                        };
                        decimal tseValidRatio;
                        if (decimal.TryParse(excelRow[3].GetValue<string>(), out tseValidRatio))
                        {
                            existed.TseValidDiscountRatio = tseValidRatio;
                        }
                        else
                        {
                            existed.ErrorNo = 1;
                            existed.ErrorMessage =
                                MessageResource.DealerSaleChannelDiscountRatios_Warning_EmptyTseValidDiscountRatio;
                        }
                        decimal tseInvalidRatio;
                        if (decimal.TryParse(excelRow[4].GetValue<string>(), out tseInvalidRatio))
                        {
                            existed.TseInvalidDiscountRatio = tseInvalidRatio;
                        }
                        else
                        {
                            existed.ErrorNo = 1;
                            existed.ErrorMessage =
                                MessageResource.DealerSaleChannelDiscountRatios_Warning_EmptyTseInvalidDiscountRatio;
                        }

                        #region Format Control

                        if (existed.DealerClassCode.Length > 2)
                        {
                            existed.ErrorNo = 1;
                            existed.ErrorMessage =
                                MessageResource.DealerSaleChannelDiscountRatios_Warning_DealerClassCodeLength;
                        }
                        if (existed.DealerClassCode.Length == 0)
                        {
                            existed.ErrorNo = 1;
                            existed.ErrorMessage =
                                MessageResource.DealerSaleChannelDiscountRatios_Warning_EmptyDealerClassCode;
                        }
                        if (existed.ChannelCode.Length > 10)
                        {
                            existed.ErrorNo = 1;
                            existed.ErrorMessage = MessageResource.DealerSaleChannelDiscountRatios_Warning_ChannelCodeLength;
                        }
                        if (existed.ChannelCode.Length == 0)
                        {
                            existed.ErrorNo = 1;
                            existed.ErrorMessage = MessageResource.DealerSaleChannelDiscountRatios_Warning_EmptyChannelCode;
                        }
                        if (existed.SparePartClassCode.Length > 3)
                        {
                            existed.ErrorNo = 1;
                            existed.ErrorMessage =
                                MessageResource.DealerSaleChannelDiscountRatios_Warning_SparePartClassCodeLength;
                        }
                        if (existed.SparePartClassCode.Length == 0)
                        {
                            existed.ErrorNo = 1;
                            existed.ErrorMessage =
                                MessageResource.DealerSaleChannelDiscountRatios_Warning_EmptySparePartClassCode;
                        }
                        if (existed.TseValidDiscountRatio > 100 || existed.TseValidDiscountRatio < 0)
                        {
                            existed.ErrorNo = 1;
                            existed.ErrorMessage =
                                MessageResource.DealerSaleChannelDiscountRatios_Warning_TseValidDiscountRatioLength;
                        }
                        if (existed.TseInvalidDiscountRatio > 100 || existed.TseInvalidDiscountRatio < 0)
                        {
                            existed.ErrorNo = 1;
                            existed.ErrorMessage =
                                MessageResource.DealerSaleChannelDiscountRatios_Warning_TseInvalidDiscountRatioLength;
                        }
                        if (existed.TseValidDiscountRatio == 0)
                        {
                            existed.ErrorNo = 1;
                            existed.ErrorMessage =
                                MessageResource.DealerSaleChannelDiscountRatios_Warning_EmptyTseValidDiscountRatio;
                        }
                        if (existed.TseInvalidDiscountRatio == 0)
                        {
                            existed.ErrorNo = 1;
                            existed.ErrorMessage =
                                MessageResource.DealerSaleChannelDiscountRatios_Warning_EmptyTseInvalidDiscountRatio;
                        }

                        #endregion

                        excelList.Add(existed);

                        count++;
                    }

                    var errorCount = (from r in excelList.AsEnumerable()
                                      where r.ErrorNo > 0
                                      select r);
                    if (errorCount.Any())
                    {
                        model.ErrorNo = 1;
                        model.ErrorMessage = MessageResource.DealerSaleChannelDiscountRatio_Warning_ExcelProblem;
                    }
                    if (count == 0)
                    {
                        model.ErrorNo = 1;
                        model.ErrorMessage = MessageResource.DealerSaleChannelDiscountRatio_Warning_EmptyExcel;
                    }
                }
                response.Data = excelList;
                response.Total = excelList.Count;
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

        public MemoryStream SetExcelReport(List<DealerSaleChannelDiscountRatioViewModel> listModels, string errMsg)
        {
            string errorMessage = CommonValues.RowStart + errMsg + CommonValues.RowEnd;
            string preTable = CommonValues.TableStart + CommonValues.RowStart + CommonValues.ColumnStart + MessageResource.DealerSaleChannelDiscountRatios_Display_DealerClassCode +
            CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.DealerSaleChannelDiscountRatios_Display_ChannelCode +
            CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.DealerSaleChannelDiscountRatios_Display_SparePartClassCode +
            CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.DealerSaleChannelDiscountRatios_Display_TseValidDiscountRatio +
            CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.DealerSaleChannelDiscountRatios_Display_TseInvalidDiscountRatio + CommonValues.ColumnEnd;
            if (listModels != null)
            {
                preTable = preTable + ("<TD width='400px'>Log</TD>");
            }
            const string lastTable = CommonValues.RowEnd + CommonValues.TableEnd;
            MemoryStream ms = new MemoryStream();

            var sw = new StreamWriter(ms, Encoding.UTF8);

            StringBuilder sb = new StringBuilder();
            sb.Append(errorMessage);
            sb.Append(preTable);
            if (listModels != null)
            {
                foreach (var model in listModels)
                {
                    sb.AppendFormat(CommonValues.RowStart +
                                    CommonValues.ColumnStart + "{0}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{1}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{2}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{3}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{4}" + CommonValues.ColumnEnd,
                                    model.DealerClassCode, model.ChannelCode, model.SparePartClassCode,
                                    model.TseValidDiscountRatio, model.TseInvalidDiscountRatio);
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

    }
}

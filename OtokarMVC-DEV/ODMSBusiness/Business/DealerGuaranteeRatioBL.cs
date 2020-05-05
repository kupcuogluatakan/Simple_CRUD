using System.Globalization;
using System.Threading;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSData;
using ODMSModel.DealerGuaranteeRatio;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ODMSModel.DownloadFileActionResult;
using ODMSCommon.Security;
using System.Data;

namespace ODMSBusiness
{
    public class DealerGuaranteeRatioBL : BaseBusiness, IDownloadFile<DealerGuaranteeRatioIndexViewModel>
    {
        private readonly DealerGuaranteeRatioData data = new DealerGuaranteeRatioData();
        public ResponseModel<DealerGuaranteeRatioListModel> ListDealerGuaranteeRatio(UserInfo user,DealerGuaranteeRatioListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<DealerGuaranteeRatioListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListDealerGuaranteeRatio(user,filter, out totalCnt);
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

        public ResponseModel<DealerGuaranteeRatioIndexViewModel> DMLDealerGuaranteeRatio(UserInfo user, DealerGuaranteeRatioIndexViewModel model)
        {
            var response = new ResponseModel<DealerGuaranteeRatioIndexViewModel>();
            try
            {
                data.DMLDealerGuaranteeRatio(user, model);
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

        public ResponseModel<DealerGuaranteeRatioIndexViewModel> GetDealerGuaranteeRatio(UserInfo user, DealerGuaranteeRatioIndexViewModel filter)
        {
            var response = new ResponseModel<DealerGuaranteeRatioIndexViewModel>();
            try
            {
                response.Model = data.GetDealerGuaranteeRatio(user, filter);
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

        public ResponseModel<DealerGuaranteeRatioIndexViewModel> ParseExcel(UserInfo user, DealerGuaranteeRatioIndexViewModel model, Stream s)
        {
            List<DealerGuaranteeRatioIndexViewModel> excelList = new List<DealerGuaranteeRatioIndexViewModel>();
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            List<SelectListItem> idList = DealerBL.ListDealerSSIdAsSelectItem().Data;

            var response = new ResponseModel<DealerGuaranteeRatioIndexViewModel>();

            try
            {
                DataSet ds = ExcelHelper.GetDataFromExcel(s);
                DataTable excelRows = ds.Tables[0];

                int count = 0;

                foreach (DataRow excelRow in excelRows.Rows)
                {
                    if (excelRows.Columns.Count < 2)
                    {
                        count = 1;
                        model.ErrorNo = 1;
                        model.ErrorMessage = MessageResource.PDIControlDefinition_Warning_ColumnProblem;
                        break;
                    }

                    DealerGuaranteeRatioIndexViewModel row = new DealerGuaranteeRatioIndexViewModel
                    {
                        DealerSSID = excelRow[0].GetValue<string>()
                    };

                    if (string.IsNullOrEmpty(row.DealerSSID))
                    {
                        // exceldeki ilgili satırın yanına hata yazılır.
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.DealerStartupInventoryLevel_Warning_EmptyDealerClass;
                    }
                    // bayi tipi kontrol ediliyor.
                    var dealerClassControl = (from dc in idList.AsEnumerable()
                                              where dc.Text == row.DealerSSID
                                              select dc.Value);
                    if (!dealerClassControl.Any())
                    {
                        // exceldeki ilgili satırın yanına hata yazılır.
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.DealerStartupInventoryLevel_Warning_DealerClassNotFound;
                    }
                    else
                    {
                        row.IdDealer = dealerClassControl.ElementAt(0).GetValue<int>();
                    }

                    decimal ratio;
                    if (decimal.TryParse(excelRow[1].GetValue<string>(), out ratio))
                    {
                        row.GuaranteeRatio = ratio;
                        if (row.GuaranteeRatio == null || row.GuaranteeRatio <= 0 || row.GuaranteeRatio >= 100)
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.DealerStartupInventoryLevel_Warning_QuantityLength;
                        }
                    }
                    else
                    {
                        // exceldeki ilgili satırın yanına hata yazılır.
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.DealerStartupInventoryLevel_Warning_QuantityLength;
                    }
                    if (row.GuaranteeRatio == 0)
                    {
                        // exceldeki ilgili satırın yanına hata yazılır.
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.DealerStartupInventoryLevel_Warning_EmptyQuantity;
                    }
                    row.IsActive = true;

                    var existed = (from r in excelList.AsEnumerable()
                                   where r.DealerSSID == row.DealerSSID
                                   select r);
                    if (existed.Any())
                    {
                        // exceldeki ilgili satırın yanına hata yazılır.
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.DealerStartupInventoryLevel_Warning_DuplicateValue;
                    }

                    excelList.Add(row);
                    count++;
                }

                var errorCount = (from r in excelList.AsEnumerable()
                                  where r.ErrorNo > 0
                                  select r);
                if (errorCount.Any())
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.ClaimSupplier_Warning_FoundError;
                }
                if (count == 0)
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.DealerStartupInventoryLevel_Warning_EmptyExcel;
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

        public MemoryStream SetExcelReport(List<DealerGuaranteeRatioIndexViewModel> modelList, string errMsg)
        {
            string errorMessage = CommonValues.RowStart + errMsg + CommonValues.RowEnd;
            string preTable = CommonValues.TableStart + CommonValues.RowStart + CommonValues.ColumnStart + MessageResource.DealerGuaranteeRatio_Display_DealerSSID
                + CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.DealerGuaranteeRatio_Display_GuaranteeRatio + CommonValues.ColumnEnd;
            if (modelList != null)
            {
                preTable = preTable + ("<TD width='400px'>Log</TD>");
            }
            const string lastTable = CommonValues.RowEnd + CommonValues.TableEnd;
            MemoryStream ms = new MemoryStream();

            var sw = new StreamWriter(ms, Encoding.UTF8);

            StringBuilder sb = new StringBuilder();
            sb.Append(errorMessage);
            sb.Append(preTable);
            if (modelList != null)
            {
                foreach (var model in modelList)
                {
                    sb.AppendFormat(CommonValues.RowStart +
                                    CommonValues.ColumnStart + "{0}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{1}" + CommonValues.ColumnEnd, model.DealerSSID, model.GuaranteeRatio);
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

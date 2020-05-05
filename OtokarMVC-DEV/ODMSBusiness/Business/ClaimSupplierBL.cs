using ODMSData;
using ODMSModel.ClaimSupplier;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ODMSCommon;
using ODMSCommon.Resources;
using System.IO;
using ODMSModel.DownloadFileActionResult;
using ODMSCommon.Security;
using System.Data;

namespace ODMSBusiness
{
    public class ClaimSupplierBL : BaseBusiness, IDownloadFile<ClaimSupplierViewModel>
    {
        private readonly ClaimSupplierData data = new ClaimSupplierData();

        public ResponseModel<ClaimSupplierListModel> ListClaimSupplier(UserInfo user,ClaimSupplierListModel claimSupplierModel, out int totalCnt)
        {
            var response = new ResponseModel<ClaimSupplierListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListClaimSupplier(user,claimSupplierModel, out totalCnt);
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

        public ResponseModel<ClaimSupplierViewModel> GetClaimSupplier(UserInfo user, ClaimSupplierViewModel model)
        {
            var response = new ResponseModel<ClaimSupplierViewModel>();
            try
            {
                response.Model = data.GetClaimSupplier(user, model);
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

        public ResponseModel<ClaimSupplierViewModel> DMLClaimSupplier(UserInfo user, ClaimSupplierViewModel model)
        {
            var response = new ResponseModel<ClaimSupplierViewModel>();
            try
            {
                data.DMLClaimSupplier(user, model);
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

        public ResponseModel<ClaimSupplierViewModel> ParseExcel(UserInfo user, ClaimSupplierViewModel model, Stream s)
        {
            List<ClaimSupplierViewModel> excelList = new List<ClaimSupplierViewModel>();
            var response = new ResponseModel<ClaimSupplierViewModel>();

            try
            {
                DataSet ds = ExcelHelper.GetDataFromExcel(s);
                DataTable excelRows = ds.Tables[0];

                int count = 0;

                foreach (DataRow excelRow in excelRows.Rows)
                {
                    if (excelRows.Columns.Count < 3)
                    {
                        count = 1;
                        model.ErrorNo = 1;
                        model.ErrorMessage = MessageResource.PDIControlDefinition_Warning_ColumnProblem;
                        break;
                    }

                    ClaimSupplierViewModel row = new ClaimSupplierViewModel { SupplierCode = excelRow[0].GetValue<string>() };

                    if (string.IsNullOrEmpty(row.SupplierCode))
                    {
                        // exceldeki ilgili satırın yanına hata yazılır.
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.ClaimSupplier_Warning_EmptySupplierCode;
                    }
                    // bayi tipi kontrol ediliyor.

                    row.SupplierName = excelRow[1].GetValue<string>();
                    if (string.IsNullOrEmpty(row.SupplierName))
                    {
                        // exceldeki ilgili satırın yanına hata yazılır.
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.ClaimSupplier_Warning_EmptySupplierName;
                    }

                    row.ClaimRackCode = excelRow[2].GetValue<string>();
                    if (string.IsNullOrEmpty(row.ClaimRackCode))
                    {
                        // exceldeki ilgili satırın yanına hata yazılır.
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.ClaimSupplier_Warning_EmptyClaimRackCode;
                    }
                    row.IsActive = true;

                    var existed = (from r in excelList.AsEnumerable()
                                   where r.SupplierCode == row.SupplierCode
                                   select r);
                    if (existed.Any())
                    {
                        // exceldeki ilgili satırın yanına hata yazılır.
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.ClaimSupplier_Warning_DuplicateValue;
                    }

                    excelList.Add(row);
                    count++;
                }

                var errorCount = (from r in excelList.AsEnumerable()
                                  where r.ErrorNo == 1
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

        public MemoryStream SetExcelReport(List<ClaimSupplierViewModel> modelList, string errMsg)
        {
            string errorMessage = CommonValues.RowStart + errMsg + CommonValues.RowEnd;
            string preTable = CommonValues.TableStart + CommonValues.RowStart + CommonValues.ColumnStart +
                              MessageResource.ClaimSupplier_Display_SupplierCode
                              + CommonValues.ColumnEnd + CommonValues.ColumnStart +
                              MessageResource.ClaimSupplier_Display_SupplierName
                              + CommonValues.ColumnEnd + CommonValues.ColumnStart +
                              MessageResource.ClaimSupplier_Display_ClaimRackCode + CommonValues.ColumnEnd;
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
                                    CommonValues.ColumnStart + "{1}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{2}" + CommonValues.ColumnEnd, model.SupplierCode, model.SupplierName,
                                    model.ClaimRackCode);
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

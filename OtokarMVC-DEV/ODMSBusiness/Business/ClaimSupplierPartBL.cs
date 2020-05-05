using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSData;
using ODMSData.Utility;
using ODMSModel.ClaimSupplierPart;
using ODMSModel.DownloadFileActionResult;
using ODMSModel.SparePart;
using System.Data;

namespace ODMSBusiness
{
    public class ClaimSupplierPartBL : BaseBusiness, IDownloadFile<ClaimSupplierPartViewModel>
    {
        private readonly ClaimSupplierPartData data = new ClaimSupplierPartData();

        public static ResponseModel<SelectListItem> ListSupplierCodesAsSelectListItem(UserInfo user)
        {
            var response = new ResponseModel<SelectListItem>();
            var data = new ClaimSupplierPartData();
            try
            {
                response.Data = data.ListClaimSupplierCodesAsSelectListItem(user);
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

        public ResponseModel<ClaimSupplierPartListModel> ListClaimSupplierParts(UserInfo user,ClaimSupplierPartListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<ClaimSupplierPartListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListClaimSupplierPart(user,filter, out totalCnt);
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

        public ResponseModel<bool> DeleteAllClaimSupplierPart(string supplierCode)
        {
            var response = new ResponseModel<bool>();
            try
            {
                response.Model = data.DeleteAllClaimSupplierPart(supplierCode); ;
                response.Message = MessageResource.Global_Display_Success;
                if (!response.Model)
                    throw new System.Exception(MessageResource.Global_Display_Error);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<ClaimSupplierPartViewModel> GetClaimSupplierPart(UserInfo user, long partId, string supplierCode)
        {
            var response = new ResponseModel<ClaimSupplierPartViewModel>();
            try
            {
                response.Model = data.GetClaimSupplierPart(user, partId, supplierCode);
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

        public ResponseModel<ClaimSupplierPartViewModel> DMLClaimSupplierPart(UserInfo user, ClaimSupplierPartViewModel model)
        {
            var response = new ResponseModel<ClaimSupplierPartViewModel>();
            try
            {
                data.DMLClaimSupplierPart(user, model);
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

        public ResponseModel<ClaimSupplierPartViewModel> ParseExcel(UserInfo user, ClaimSupplierPartViewModel model, Stream s)
        {
            List<SelectListItem> claimSupplierCodeList = ListSupplierCodesAsSelectListItem(user).Data;
            List<ClaimSupplierPartViewModel> excelList = new List<ClaimSupplierPartViewModel>();

            var response = new ResponseModel<ClaimSupplierPartViewModel>();
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

                    ClaimSupplierPartViewModel row = new ClaimSupplierPartViewModel { PartCode = excelRow[0].GetValue<string>() };

                    if (string.IsNullOrEmpty(row.PartCode))
                    {
                        // exceldeki ilgili satırın yanına hata yazılır.
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.ClaimSupplierPart_Warning_EmptyPartCode;
                    }

                    SparePartBL spBo = new SparePartBL();
                    SparePartIndexViewModel spModel = new SparePartIndexViewModel { PartCode = row.PartCode };
                    spBo.GetSparePart(user, spModel);

                    if (spModel.PartId == 0)
                    {
                        // exceldeki ilgili satırın yanına hata yazılır.
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.ClaimSupplierPart_Warning_PartCodeNotFound;
                    }
                    else
                    {
                        row.PartId = spModel.PartId;
                    }

                    row.SupplierCode = excelRow[1].GetValue<string>();

                    if (string.IsNullOrEmpty(row.SupplierCode))
                    {
                        // exceldeki ilgili satırın yanına hata yazılır.
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.ClaimSupplierPart_Warning_EmptySupplierCode;
                    }

                    var supplierCodeControl = (from dc in claimSupplierCodeList.AsEnumerable()
                                               where dc.Value == row.SupplierCode
                                               select dc.Value);
                    if (!supplierCodeControl.Any())
                    {
                        // exceldeki ilgili satırın yanına hata yazılır.
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.ClaimSupplierPart_Warning_SupplierCodeNotFound;
                    }
                    else
                    {
                        row.SupplierCode = supplierCodeControl.ElementAt(0).GetValue<string>();
                    }

                    row.IsActive = true;

                    var existed = (from r in excelList.AsEnumerable()
                                   where r.PartCode == row.PartCode && r.SupplierCode == row.SupplierCode
                                   select r);
                    if (existed.Any())
                    {
                        // exceldeki ilgili satırın yanına hata yazılır.
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.ClaimSupplierPart_Warning_DuplicateValue;
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
                    model.ErrorMessage = MessageResource.ClaimSupplierPart_Warning_NotFoundError;
                }
                if (count == 0)
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.ClaimSupplierPart_Warning_EmptyExcel;
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

        public MemoryStream SetExcelReport(List<ClaimSupplierPartViewModel> modelList, string errMsg)
        {
            string errorMessage = CommonValues.RowStart + errMsg + CommonValues.RowEnd;
            string preTable = CommonValues.TableStart + CommonValues.RowStart + CommonValues.ColumnStart + MessageResource.ClaimSupplierPart_Display_PartCode
                + CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.ClaimSupplierPart_Display_SupplierCode + CommonValues.ColumnEnd;
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
                                    CommonValues.ColumnStart + "{1}" + CommonValues.ColumnEnd, model.PartCode, model.SupplierCode);
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

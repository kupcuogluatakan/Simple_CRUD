using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSData;
using ODMSData.Utility;
using ODMSModel.ClaimRecallPeriodPart;
using ODMSModel.DownloadFileActionResult;
using ODMSModel.SparePart;
using System.Data;

namespace ODMSBusiness
{
    public class ClaimRecallPeriodPartBL : BaseBusiness, IDownloadFile<ClaimRecallPeriodPartViewModel>
    {
        private readonly ClaimRecallPeriodPartData data = new ClaimRecallPeriodPartData();

        public ResponseModel<ClaimRecallPeriodPartListModel> ListClaimRecallPeriodParts(UserInfo user,ClaimRecallPeriodPartListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<ClaimRecallPeriodPartListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListClaimRecallPeriodPart(user,filter, out totalCnt);
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

        public ResponseModel<bool> DeleteAllClaimRecallPeriodPart(int claimRecallPeriodId)
        {
            var response = new ResponseModel<bool>();
            try
            {
                response.Model = data.DeleteAllClaimRecallPeriodPart(claimRecallPeriodId);
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

        public ResponseModel<ClaimRecallPeriodPartViewModel> DMLClaimRecallPeriodPart(UserInfo user, ClaimRecallPeriodPartViewModel model)
        {
            var response = new ResponseModel<ClaimRecallPeriodPartViewModel>();
            try
            {
                data.DMLClaimRecallPeriodPart(user, model);
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

        public ResponseModel<ClaimRecallPeriodPartViewModel> GetClaimRecallPeriodPart(UserInfo user, long partId)
        {
            var response = new ResponseModel<ClaimRecallPeriodPartViewModel>();
            try
            {
                response.Model = data.GetClaimRecallPeriodPart(user, partId);
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

        public ResponseModel<ClaimRecallPeriodPartViewModel> ParseExcel(UserInfo user, ClaimRecallPeriodPartViewModel model, Stream s)
        {
            List<ClaimRecallPeriodPartViewModel> excelList = new List<ClaimRecallPeriodPartViewModel>();

            var response = new ResponseModel<ClaimRecallPeriodPartViewModel>();
            try
            {
                DataSet ds = ExcelHelper.GetDataFromExcel(s);
                DataTable excelRows = ds.Tables[0];

                int count = 0;

                foreach (DataRow excelRow in excelRows.Rows)
                {
                    ClaimRecallPeriodPartViewModel row = new ClaimRecallPeriodPartViewModel
                    {
                        PartCode = excelRow[0].GetValue<string>()
                    };

                    if (string.IsNullOrEmpty(row.PartCode))
                    {
                        // exceldeki ilgili satırın yanına hata yazılır.
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.ClaimRecallPeriodPart_Warning_EmptyPartCode;
                    }

                    SparePartBL spBo = new SparePartBL();
                    SparePartIndexViewModel spModel = new SparePartIndexViewModel { PartCode = row.PartCode };
                    spBo.GetSparePart(user, spModel);

                    if (spModel.PartId == 0)
                    {
                        // exceldeki ilgili satırın yanına hata yazılır.
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.ClaimRecallPeriodPart_Warning_PartCodeNotFound;
                    }
                    else
                    {
                        row.PartId = spModel.PartId;
                    }

                    row.IsActive = true;

                    var existed = (from r in excelList.AsEnumerable()
                                   where r.PartCode == row.PartCode
                                   select r);
                    if (existed.Any())
                    {
                        // exceldeki ilgili satırın yanına hata yazılır.
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.ClaimRecallPeriodPart_Warning_DuplicateValue;
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
                    model.ErrorMessage = MessageResource.ClaimRecallPeriodPart_Warning_NotFoundError;
                }
                if (count == 0)
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.ClaimRecallPeriodPart_Warning_EmptyExcel;
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

        public MemoryStream SetExcelReport(List<ClaimRecallPeriodPartViewModel> modelList, string errMsg)
        {
            string errorMessage = CommonValues.RowStart + errMsg + CommonValues.RowEnd;
            string preTable = CommonValues.TableStart + CommonValues.RowStart + CommonValues.ColumnStart + MessageResource.ClaimRecallPeriodPart_Display_PartCode + CommonValues.ColumnEnd;
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
                    sb.AppendFormat(CommonValues.RowStart + CommonValues.ColumnStart + "{0}" + CommonValues.ColumnEnd, model.PartCode);
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

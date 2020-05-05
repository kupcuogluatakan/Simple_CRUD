using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSData;
using ODMSModel.DownloadFileActionResult;
using ODMSModel.SparePartGuaranteeAuthorityNeed;
using ODMSModel.SparePart;
using ODMSModel.SparePartGuaranteeAuthorityNeed;
using ODMSCommon.Security;
using System.Data;

namespace ODMSBusiness
{
    public class SparePartGuaranteeAuthorityNeedBL : BaseBusiness, IDownloadFile<SparePartGuaranteeAuthorityNeedViewModel>
    {
        private readonly SparePartGuaranteeAuthorityNeedData data = new SparePartGuaranteeAuthorityNeedData();
        public ResponseModel<SparePartGuaranteeAuthorityNeedListModel> ListSparePartGuaranteeAuthorityNeeds(UserInfo user, SparePartGuaranteeAuthorityNeedListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<SparePartGuaranteeAuthorityNeedListModel>();
            var data = new SparePartGuaranteeAuthorityNeedData();
            totalCnt = 0;
            try
            {
                response.Data = data.ListSparePartGuaranteeAuthorityNeeds(user, filter, out totalCnt);
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
        public ResponseModel<SparePartGuaranteeAuthorityNeedViewModel> DMLSparePartGuaranteeAuthorityNeed(UserInfo user, SparePartGuaranteeAuthorityNeedViewModel model)
        {
            var response = new ResponseModel<SparePartGuaranteeAuthorityNeedViewModel>();
            var data = new SparePartGuaranteeAuthorityNeedData();
            try
            {
                data.DMLSparePartGuaranteeAuthorityNeed(user, model);
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

        public ResponseModel<SparePartGuaranteeAuthorityNeedViewModel> ParseExcel(UserInfo user, SparePartGuaranteeAuthorityNeedViewModel model, Stream s)
        {
            List<SparePartGuaranteeAuthorityNeedViewModel> excelList = new List<SparePartGuaranteeAuthorityNeedViewModel>();
            var response = new ResponseModel<SparePartGuaranteeAuthorityNeedViewModel>();

            try
            {
                SparePartBL spBo = new SparePartBL();

                DataSet ds = ExcelHelper.GetDataFromExcel(s);
                DataTable excelRows = ds.Tables[0];

                foreach (DataRow excelRow in excelRows.Rows)
                {
                    SparePartGuaranteeAuthorityNeedViewModel row = new SparePartGuaranteeAuthorityNeedViewModel();
                    SparePartIndexViewModel spModel = new SparePartIndexViewModel();
                    string code = excelRow[0].GetValue<string>();
                    spModel.PartCode = code;
                    spBo.GetSparePart(user,spModel);

                    if (spModel.PartId != 0)
                    {
                        row.CommandType = CommonValues.DMLType.Update;
                        row.PartId = spModel.PartId;
                    }
                    else
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.SparePartGuaranteeAuthorityNeed_Warning_SparePartNotFoundError;
                    }

                    row.PartCode = code;
                    row.GuaranteeAuthorityNeed = true;
                    excelList.Add(row);
                }

                var errorCount = (from r in excelList.AsEnumerable()
                                  where r.ErrorNo > 0
                                  select r);
                if (errorCount.Any())
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.SparePartGuaranteeAuthorityNeed_Warning_NotFoundError;
                }
                if (excelRows.Rows.Count == 0)
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.SparePartGuaranteeAuthorityNeed_Warning_EmptyExcel;
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

        public MemoryStream SetExcelReport(List<SparePartGuaranteeAuthorityNeedViewModel> modelList, string errMsg)
        {
            string errorMessage = CommonValues.TableStart + errMsg + CommonValues.TableEnd;
            string preTable = CommonValues.TableStart + CommonValues.RowStart + CommonValues.ColumnStart + MessageResource.SparePart_Display_PartCode + CommonValues.ColumnEnd;
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

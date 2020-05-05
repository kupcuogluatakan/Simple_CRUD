using ODMSData;
using ODMSModel.DownloadFileActionResult;
using ODMSModel.PDIControlPartDefinition;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Resources;
using System.IO;
using ODMSCommon.Security;
using System.Data;

namespace ODMSBusiness
{
    public class PDIControlPartDefinitionBL : BaseBusiness, IDownloadFile<PDIControlPartDefinitionViewModel>
    {
        private readonly PDIControlPartDefinitionData data = new PDIControlPartDefinitionData();

        public ResponseModel<PDIControlPartDefinitionListModel> ListPDIControlPartDefinition(UserInfo user, PDIControlPartDefinitionListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<PDIControlPartDefinitionListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListPDIControlPartDefinition(user, filter, out totalCnt);
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

        public ResponseModel<PDIControlPartDefinitionViewModel> GetPDIControlPartDefinition(UserInfo user, PDIControlPartDefinitionViewModel filter)
        {
            var response = new ResponseModel<PDIControlPartDefinitionViewModel>();
            try
            {
                data.GetPDIControlPartDefinition(user, filter);
                response.Model = filter;
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

        public ResponseModel<PDIControlPartDefinitionViewModel> DMLPDIControlPartDefinition(UserInfo user, PDIControlPartDefinitionViewModel model)
        {
            var response = new ResponseModel<PDIControlPartDefinitionViewModel>();
            try
            {
                data.DMLPDIControlPartDefinition(user, model);
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

        public ResponseModel<PDIControlPartDefinitionViewModel> ParseExcel(UserInfo user, PDIControlPartDefinitionViewModel model, Stream s)
        {
            List<PDIControlPartDefinitionViewModel> excelList = new List<PDIControlPartDefinitionViewModel>();

            var response = new ResponseModel<PDIControlPartDefinitionViewModel>();
            try
            {
                List<SelectListItem> controlModelList = PDIControlDefinitionBL.ListPDIControlModelCodeAsSelectListItem().Data;
                List<SelectListItem> partList = PDIPartDefinitionBL.ListPartCodeAsSelectListItem().Data;

                DataSet ds = ExcelHelper.GetDataFromExcel(s);
                DataTable excelRows = ds.Tables[0];

                string controlCode = string.Empty;
                string modelKod = string.Empty;
                string partCode = string.Empty;


                #region ColumnControl
                if (excelRows.Columns.Count < 4)
                {
                    response.IsSuccess = false; 
                    response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, MessageResource.PDIControlDefinition_Warning_ColumnProblem);
                    return response;
                }
                #endregion

                foreach (DataRow excelRow in excelRows.Rows)
                {
                    PDIControlPartDefinitionViewModel row = new PDIControlPartDefinitionViewModel();

                    partCode = excelRow[0].GetValue<string>().Trim();
                    controlCode = excelRow[1].GetValue<string>().Trim();
                    modelKod = excelRow[2].GetValue<string>().Trim();
                    string isActive = excelRow[3].GetValue<string>().Trim();

                    row.PDIControlCode = controlCode;
                    row.PDIModelCode = modelKod;
                    row.PDIPartCode = partCode;

                    var controlModelCodeControl = (from dc in controlModelList.AsEnumerable()
                                                   where dc.Text == controlCode + CommonValues.MinusWithSpace + modelKod
                                                   select dc.Value);
                    if (controlModelCodeControl.Any())
                    {
                        row.IdPDIControlDefinition = controlModelCodeControl.ElementAt(0).GetValue<int>();

                        PDIControlPartDefinitionBL controlPartBl = new PDIControlPartDefinitionBL();
                        controlPartBl.GetPDIControlPartDefinition(user, row);

                        row.CommandType = row.IsActiveName != null ? CommonValues.DMLType.Update : CommonValues.DMLType.Insert;
                    }
                    else
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.PDIControlPartDefinition_Warning_ControlModelKodNotFound;
                    }

                    if (row.ErrorNo == 0)
                    {
                        var partCodeControl = (from m in partList.AsEnumerable()
                                               where m.Text == partCode
                                               select m.Value);
                        if (!partCodeControl.Any())
                        {
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.PDIControlPartDefinition_Warning_PartKodNotFound;
                        }

                        if (row.ErrorNo == 0)
                        {
                            int isAct;
                            if (!int.TryParse(isActive, out isAct))
                            {
                                row.ErrorNo = 1;
                                row.ErrorMessage = MessageResource.PDIControlPartDefinition_Warning_IsActiveMustBeNumeric;
                            }
                            else
                            {
                                if (isAct != 0 && isAct != 1)
                                {
                                    row.ErrorNo = 1;
                                    row.ErrorMessage = MessageResource.PDIControlPartDefinition_Warning_IsActiveMust1or0;
                                }
                                else
                                {
                                    row.IsActive = isAct.GetValue<int>() != 0;
                                }
                            }
                        }
                    }

                    var duplicateControl = (from e in excelList.AsEnumerable()
                                            where
                                                e.PDIPartCode == row.PDIPartCode &&
                                                e.IdPDIControlDefinition == row.IdPDIControlDefinition
                                            select e);
                    if (duplicateControl.Any())
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.PDIControlPartDefinition_Warning_DuplicateValues;
                    }

                    excelList.Add(row);
                }

                var errorCount = (from r in excelList.AsEnumerable()
                                  where r.ErrorNo > 0
                                  select r);
                if (errorCount.Any())
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.BreakdownDefinition_Warning_WrongData;
                }
                if (excelRows.Rows.Count == 0)
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

        public MemoryStream SetExcelReport(List<PDIControlPartDefinitionViewModel> modelList, string errMsg)
        {
            string errorMessage = CommonValues.ColumnStart + errMsg + CommonValues.ColumnEnd;
            string preTable = CommonValues.TableStart + CommonValues.RowStart
                              + CommonValues.ColumnStart + MessageResource.PDIControlPartDefinition_Display_PDIPartCode +
                              CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.PDIControlPartDefinition_Display_PDIControlCode +
                              CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.PDIControlPartDefinition_Display_PDIModelCode +
                              CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.Global_Display_IsActive
                              + CommonValues.ColumnEnd;
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
                                    CommonValues.ColumnStart + "{2}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{3}" + CommonValues.ColumnEnd,
                                    model.PDIPartCode, model.PDIControlCode, model.PDIModelCode, model.IsActive);
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

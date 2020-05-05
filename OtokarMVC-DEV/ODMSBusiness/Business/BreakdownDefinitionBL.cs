using ODMSCommon.Resources;
using ODMSData;
using ODMSModel.BreakdownDefinition;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ODMSCommon;
using ODMSModel.DownloadFileActionResult;
using ODMSCommon.Security;
using System.Data;

namespace ODMSBusiness
{
    public class BreakdownDefinitionBL : BaseBusiness, IDownloadFile<BreakdownDefinitionViewModel>
    {
        private readonly BreakdownDefinitionData data = new BreakdownDefinitionData();
        public ResponseModel<BreakdownDefinitionListModel> ListBreakdownDefinition(UserInfo user,BreakdownDefinitionListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<BreakdownDefinitionListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListBreakdownDefinition(user,filter, out totalCnt);
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

        public ResponseModel<BreakdownDefinitionViewModel> GetBreakdownDefinition(UserInfo user, BreakdownDefinitionViewModel filter)
        {
            var response = new ResponseModel<BreakdownDefinitionViewModel>();
            try
            {
                data.GetBreakdownDefinition(user, filter);
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

        public ResponseModel<BreakdownDefinitionViewModel> DMLBreakdownDefinition(UserInfo user, BreakdownDefinitionViewModel model)
        {
            var response = new ResponseModel<BreakdownDefinitionViewModel>();
            try
            {
                data.DMLBreakdownDefinition(user, model);
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

        public ResponseModel<BreakdownDefinitionViewModel> ParseExcel(UserInfo user, BreakdownDefinitionViewModel model, Stream s)
        {
            var response = new ResponseModel<BreakdownDefinitionViewModel>();
            List<BreakdownDefinitionViewModel> excelList = new List<BreakdownDefinitionViewModel>();
            try
            {
                List<SelectListItem> pdiCodeList = ListBreakdownCodeAsSelectListItem().Data;
                List<SelectListItem> languageList = LanguageBL.ListLanguageAsSelectListItem(user).Data;
                const int columnCount = 3;

                DataSet ds = ExcelHelper.GetDataFromExcel(s);
                DataTable excelRows = ds.Tables[0];

                List<string> languageCodes = new List<string>();

                string pdiCode = string.Empty;
                StringBuilder multiLanguageText = new StringBuilder();

                #region ColumnControl
                if (excelRows.Columns.Count <= columnCount)
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.PDIControlDefinition_Warning_ColumnProblem;
                }

                // kolon isimleri
                for (int i = columnCount; i < excelRows.Columns.Count; i++)
                {
                    string descLang = excelRows.Columns[i].ToString();
                    if (!string.IsNullOrEmpty(descLang) && descLang.Contains('_'))
                    {
                        List<string> langList = descLang.Split('_').ToList();
                        string langCode = langList.ElementAt(1);
                        var control = (from l in languageList.AsEnumerable()
                                       where l.Value == langCode.ToUpper()
                                       select l);
                        if (control.Any())
                        {
                            languageCodes.Add(langCode);
                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, MessageResource.BreakdownDefinition_Warning_ExcelColumns);
                            return response;
                        }
                    }
                }
                #endregion

                foreach (DataRow excelRow in excelRows.Rows)
                {
                    BreakdownDefinitionViewModel row = new BreakdownDefinitionViewModel();
                    if (languageCodes.Count == 0)
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.BreakdownDefinition_Warning_MissingLanguageCode;
                    }
                    else
                    {
                        pdiCode = excelRow[0].GetValue<string>();
                        string isActive = excelRow[1].GetValue<string>();
                        string adminDesc = excelRow[2].GetValue<string>();


                        multiLanguageText.Clear();
                        for (int i = columnCount; i < excelRows.Columns.Count; i++)
                        {
                            string multiLanguageValue = excelRow[i].GetValue<string>();
                            if (!string.IsNullOrEmpty(multiLanguageValue))
                            {
                                if (i - columnCount >= 0 && i - columnCount < languageCodes.Count)
                                {
                                    string languageCode = languageCodes.ElementAt(i - columnCount);

                                    multiLanguageText.Append(languageCode.ToUpper());
                                    multiLanguageText.Append(CommonValues.Pipe);
                                    multiLanguageText.Append(multiLanguageValue);
                                    multiLanguageText.Append(CommonValues.Pipe);

                                    if (i == columnCount)
                                        row.BreakdownName.MultiLanguageContentAsText = multiLanguageValue;
                                }
                            }
                        }
                        row.MultiLanguageContentAsText = multiLanguageText.ToString();
                        if (multiLanguageText.ToString().Length == 0)
                        {
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.BreakdownDefinition_Warning_MissingBreakdownName;
                        }


                        var codeClassControl = (from dc in pdiCodeList.AsEnumerable()
                                                where dc.Text.ToUpper() == pdiCode.ToUpper()
                                                select dc.Value);
                        if (codeClassControl.Any())
                        {
                            row.CommandType = CommonValues.DMLType.Update;
                            row.PdiBreakdownCode = codeClassControl.ElementAt(0).GetValue<string>();
                        }
                        else
                        {
                            row.CommandType = CommonValues.DMLType.Insert;
                            row.PdiBreakdownCode = pdiCode;
                        }

                        int isAct;
                        if (!int.TryParse(isActive, out isAct))
                        {
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.PDIResultDefinition_Warning_IsActiveMustBeNumeric;
                        }
                        else
                        {
                            if (isAct != 0 && isAct != 1)
                            {
                                row.ErrorNo = 1;
                                row.ErrorMessage = MessageResource.PDIResultDefinition_Warning_IsActiveMust1or0;
                            }
                            else
                            {
                                row.IsActive = isAct.GetValue<int>() != 0;
                            }
                        }

                        if (adminDesc.Length > 100)
                        {
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.PDIResultDefinition_Warning_AdminDescMustBeLength100;
                        }
                        else
                        {
                            row.AdminDesc = adminDesc;
                        }
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

        private static ResponseModel<SelectListItem> ListBreakdownCodeAsSelectListItem()
        {
            var response = new ResponseModel<SelectListItem>();
            var data = new BreakdownDefinitionData();
            try
            {
                response.Data = data.ListBreakdownCodeAsSelectListItem();
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

        public MemoryStream SetExcelReport(List<BreakdownDefinitionViewModel> modelList, string errMsg)
        {
            string errorMessage = CommonValues.RowStart + errMsg + CommonValues.RowEnd;
            string preTable = CommonValues.TableStart + CommonValues.RowStart +
                CommonValues.ColumnStart + MessageResource.BreakdownDefinition_Display_PdiBreakdownCode +
                              CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.Global_Display_IsActive +
                              CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.BreakdownDefinition_Display_AdminDesc + CommonValues.ColumnEnd +
                              CommonValues.ColumnStart + MessageResource.BreakdownDefinition_Display_BreakdownName + "_" +
                              MessageResource.User_Display_LanguageCode + CommonValues.ColumnEnd;
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
                                    CommonValues.ColumnStart + "{3}" + CommonValues.ColumnEnd, model.PdiBreakdownCode,
                                    model.IsActive, model.AdminDesc, model.BreakdownName.MultiLanguageContentAsText);
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

        public static ResponseModel<SelectListItem> ListPdiBreakdownCodeAsSelectListItem()
        {
            var response = new ResponseModel<SelectListItem>();
            var data = new BreakdownDefinitionData();
            try
            {
                response.Data = data.ListBreakdownCodeAsSelectListItem();
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
    }
}

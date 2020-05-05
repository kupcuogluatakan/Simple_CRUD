using ODMSData;
using ODMSModel.DownloadFileActionResult;
using ODMSModel.PDIResultDefinition;
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
    public class PDIResultDefinitionBL : BaseBusiness, IDownloadFile<PDIResultDefinitionViewModel>
    {
        private readonly PDIResultDefinitionData data = new PDIResultDefinitionData();

        public ResponseModel<PDIResultDefinitionListModel> ListPDIResultDefinition(UserInfo user, PDIResultDefinitionListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<PDIResultDefinitionListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListPDIResultDefinition(user, filter, out totalCnt);
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

        public ResponseModel<PDIResultDefinitionViewModel> GetPDIResultDefinition(UserInfo user, PDIResultDefinitionViewModel filter)
        {
            var response = new ResponseModel<PDIResultDefinitionViewModel>();
            try
            {
                data.GetPDIResultDefinition(user, filter);
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

        public ResponseModel<PDIResultDefinitionViewModel> DMLPDIResultDefinition(UserInfo user, PDIResultDefinitionViewModel model)
        {
            var response = new ResponseModel<PDIResultDefinitionViewModel>();
            try
            {
                data.DMLPDIResultDefinition(user, model);
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

        public static ResponseModel<SelectListItem> ListPDIResultCodeAsSelectListItem()
        {
            PDIResultDefinitionData data = new PDIResultDefinitionData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListPDIResultCodeAsSelectListItem();
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

        public ResponseModel<PDIResultDefinitionViewModel> ParseExcel(UserInfo user, PDIResultDefinitionViewModel model, Stream s)
        {
            List<PDIResultDefinitionViewModel> excelList = new List<PDIResultDefinitionViewModel>();

            var response = new ResponseModel<PDIResultDefinitionViewModel>();
            try
            {
                List<SelectListItem> codeList = ListPDIResultCodeAsSelectListItem().Data;
                List<SelectListItem> languageList = LanguageBL.ListLanguageAsSelectListItem(user).Data;


                DataSet ds = ExcelHelper.GetDataFromExcel(s);
                DataTable excelRows = ds.Tables[0];

                const int columnCount = 3;
                
                List<string> languageCodes = new List<string>();

                string resultCode = string.Empty;
                StringBuilder multiLanguageText = new StringBuilder();

                #region ColumnControl
                if (excelRows.Columns.Count < columnCount)
                {
                    model.ErrorMessage = MessageResource.BreakdownDefinition_Warning_ExcelColumns;
                    model.ErrorNo = 1;
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
                    PDIResultDefinitionViewModel row = new PDIResultDefinitionViewModel();
                    resultCode = excelRow[0].GetValue<string>();
                        string adminDesc = excelRow[2].GetValue<string>();
                        string isActive = excelRow[1].GetValue<string>();

                        if (languageCodes.Count == 0)
                        {
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.PDIResultDefinition_Warning_MissingLanguageCode;
                        }
                        else
                        {
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
                                            row.ResultNameML.txtLanguageContentAsString = multiLanguageValue;
                                    }
                                }
                            }
                            row.MultiLanguageContentAsText = multiLanguageText.ToString();
                            if (multiLanguageText.ToString().Length == 0)
                            {
                                row.ErrorNo = 1;
                                row.ErrorMessage = MessageResource.PDIResultDefinition_Warning_MissingPDIResultlName;
                            }
                        }

                        var codeClassResult = (from dc in codeList.AsEnumerable()
                                               where dc.Text == resultCode
                                               select dc.Value);
                        if (codeClassResult.Any())
                        {
                            row.CommandType = CommonValues.DMLType.Update;
                            row.PDIResultCode = codeClassResult.ElementAt(0).GetValue<string>();
                        }
                        else
                        {
                            row.CommandType = CommonValues.DMLType.Insert;
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

                        if (resultCode.Length > 3)
                        {
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.PDIResultDefinition_Warning_ResultCodeMustbe3Length;
                        }
                        else
                        {
                            row.PDIResultCode = resultCode;
                        }

                        var duplicateControl = (from e in excelList.AsEnumerable()
                                                where e.PDIResultCode == row.PDIResultCode
                                                select e);
                        if (duplicateControl.Any())
                        {
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.PDIResultDefinition_Warning_DuplicateValue;
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

        public MemoryStream SetExcelReport(List<PDIResultDefinitionViewModel> modelList, string errMsg)
        {
            string errorMessage = CommonValues.TableStart + errMsg + CommonValues.TableEnd;
            string preTable = CommonValues.TableStart + CommonValues.RowStart + CommonValues.ColumnStart + MessageResource.PDIResultDefinition_Display_PDIResultCode +
                 CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.Global_Display_IsActive +
                 CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.PDIResultDefinition_Display_AdminDesc +
                 CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.PDIResultDefinition_Display_ResultNameML + "_" + MessageResource.User_Display_LanguageCode +
                CommonValues.ColumnEnd;
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
                                    CommonValues.ColumnStart + "{2}" + CommonValues.ColumnEnd,
                                    model.PDIResultCode, model.IsActive,
                                    model.AdminDesc);
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

using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSData;
using ODMSModel.DownloadFileActionResult;
using ODMSModel.PDIPartDefinition;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ODMSBusiness
{
    public class PDIPartDefinitionBL : BaseBusiness, IDownloadFile<PDIPartDefinitionViewModel>
    {
        private readonly PDIPartDefinitionData data = new PDIPartDefinitionData();

        public ResponseModel<PDIPartDefinitionListModel> ListPDIPartDefinition(UserInfo user, PDIPartDefinitionListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<PDIPartDefinitionListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListPDIPartDefinition(user, filter, out totalCnt);
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

        public static ResponseModel<SelectListItem> ListPartCodeAsSelectListItem()
        {
            PDIPartDefinitionData data = new PDIPartDefinitionData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListPdiPartCodeAsSelectListItem();
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

        public ResponseModel<PDIPartDefinitionViewModel> GetPDIPartDefinition(UserInfo user, PDIPartDefinitionViewModel filter)
        {
            var response = new ResponseModel<PDIPartDefinitionViewModel>();
            try
            {
                data.GetPDIPartDefinition(user, filter);
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

        public ResponseModel<PDIPartDefinitionViewModel> DMLPDIPartDefinition(UserInfo user, PDIPartDefinitionViewModel model)
        {
            var response = new ResponseModel<PDIPartDefinitionViewModel>();
            try
            {
                data.DMLPDIPartDefinition(user, model);
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

        private static List<SelectListItem> ListPdiPartCodeAsSelectListItem()
        {
            PDIPartDefinitionData _pdiPartDefinitionData = new PDIPartDefinitionData();
            return _pdiPartDefinitionData.ListPdiPartCodeAsSelectListItem();
        }

        public ResponseModel<PDIPartDefinitionViewModel> ParseExcel(UserInfo user, PDIPartDefinitionViewModel model, Stream s)
        {
            var excelList = new List<PDIPartDefinitionViewModel>();
            var response = new ResponseModel<PDIPartDefinitionViewModel>();
            try
            {

                List<SelectListItem> partCodeList = ListPdiPartCodeAsSelectListItem();
                List<SelectListItem> languageList = LanguageBL.ListLanguageAsSelectListItem(user).Data;

                DataSet ds = ExcelHelper.GetDataFromExcel(s);
                DataTable excelRows = ds.Tables[0];

                const int columnCount = 3;
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
                            response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, MessageResource.PDIPartDefinition_Warning_ExcelColumns);
                            return response;
                        }
                    }
                }
                if (languageCodes.Count == 0)
                {
                    response.IsSuccess = false;
                    response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, MessageResource.PDIPartDefinition_Warning_MissingNameColumn);
                    return response;
                }
                #endregion

                foreach (DataRow excelRow in excelRows.Rows)
                {
                    PDIPartDefinitionViewModel row = new PDIPartDefinitionViewModel();
                    pdiCode = excelRow[0].GetValue<string>();
                    string isActive = excelRow[1].GetValue<string>();
                    string adminDesc = excelRow[2].GetValue<string>();

                    if (languageCodes.Count == 0)
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.PDIPartDefinition_Warning_MissingLanguageCode;
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
                                        row.MultiLanguageContentAsText = multiLanguageValue;
                                }
                            }
                        }
                        row.MultiLanguageContentAsText = multiLanguageText.ToString();
                        if (multiLanguageText.ToString().Length == 0)
                        {
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.PDIPartDefinition_Warning_MissingPDIPartName;
                        }
                        if (!multiLanguageText.ToString().Contains("TR"))
                        {
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.PDIPartDefinition_Warning_MissingPDIPartNameTR;
                        }
                    }


                    if (pdiCode.Length > 50)
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.PDIPartDefinition_Warning_PartCodeMustbe50Length;
                    }
                    else
                    {
                        if (pdiCode.Length == 0)
                        {
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.PDIPartDefinition_Warning_MissingPDICode;
                        }
                        else
                        {
                            var codeClassControl = (from dc in partCodeList.AsEnumerable()
                                                    where dc.Text == pdiCode
                                                    select dc.Value);
                            if (codeClassControl.Any())
                            {
                                row.CommandType = CommonValues.DMLType.Update;
                                row.PdiPartCode = codeClassControl.ElementAt(0).GetValue<string>();
                            }
                            else
                            {
                                row.CommandType = CommonValues.DMLType.Insert;
                                row.PdiPartCode = pdiCode;
                            }
                        }
                    }

                    int isAct;
                    if (!int.TryParse(isActive, out isAct))
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.PDIPartDefinition_Warning_IsActiveMustBeNumeric;
                    }
                    else
                    {
                        if (isAct != 0 && isAct != 1)
                        {
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.PDIPartDefinition_Warning_IsActiveMust1or0;
                        }
                        else
                        {
                            row.IsActive = isAct.GetValue<int>() != 0;
                        }
                    }

                    if (adminDesc.Length > 100)
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.PDIPartDefinition_Warning_AdminDescMustbe100Length;
                    }
                    else
                    {
                        if (adminDesc.Length == 0)
                        {
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.PDIPartDefinition_Warning_MissingAdminDesc;
                        }
                        else
                        {
                            row.AdminDesc = adminDesc;
                        }
                    }

                    var duplicateControl = (from e in excelList.AsEnumerable()
                                            where e.PdiPartCode == row.PdiPartCode
                                            select e);
                    if (duplicateControl.Any())
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.PDIPartDefinition_Warning_DuplicateValue;
                    }

                    excelList.Add(row);
                }
                

                var errorCount = (from r in excelList.AsEnumerable()
                                  where r.ErrorNo > 0
                                  select r);
                if (errorCount.Any())
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.PDIPartDefinition_Warning_WrongData;
                }
                if (excelRows.Rows.Count == 0)
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.PDIPartDefinition_Warning_EmptyExcel;
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

        public MemoryStream SetExcelReport(List<PDIPartDefinitionViewModel> modelList, string errMsg)
        {
            string errorMessage = CommonValues.TableStart + errMsg + CommonValues.TableEnd;
            string preTable = CommonValues.TableStart + CommonValues.RowStart + CommonValues.ColumnStart + MessageResource.PDIControlPartDefinition_Display_PDIPartCode
                + CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.Global_Display_IsActive
                + CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.AppointmentIndicatorCategory_Display_AdminDesc
                + CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.SparePart_Display_PartName + "_TR"
                + CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.SparePart_Display_PartName + "_" + MessageResource.User_Display_LanguageCode
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
                                    CommonValues.ColumnStart + "{3}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{4}" + CommonValues.ColumnEnd,
                                    model.PdiPartCode, model.AdminDesc,
                                    model.IsActive, model.MultiLanguageContentAsText, model.MultiLanguageContentAsText);
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

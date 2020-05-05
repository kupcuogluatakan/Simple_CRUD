using ODMSData;
using ODMSModel.AppointmentIndicatorFailureCode;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Resources;
using System.IO;
using ODMSModel.DownloadFileActionResult;
using ODMSCommon.Security;
using System.Data;

namespace ODMSBusiness
{
    public class AppointmentIndicatorFailureCodeBL : BaseBusiness, IDownloadFile<AppointmentIndicatorFailureCodeViewModel>
    {
        private readonly AppointmentIndicatorFailureCodeData data = new AppointmentIndicatorFailureCodeData();

        public ResponseModel<AppointmentIndicatorFailureCodeListModel> ListAppointmentIndicatorFailureCode(UserInfo user,AppointmentIndicatorFailureCodeListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<AppointmentIndicatorFailureCodeListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListAppointmentIndicatorFailureCode(user,filter, out totalCnt);
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

        public ResponseModel<AppointmentIndicatorFailureCodeViewModel> GetAppointmentIndicatorFailureCode(UserInfo user, AppointmentIndicatorFailureCodeViewModel filter)
        {
            var response = new ResponseModel<AppointmentIndicatorFailureCodeViewModel>();
            try
            {
                data.GetAppointmentIndicatorFailureCode(user, filter);
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

        public ResponseModel<AppointmentIndicatorFailureCodeViewModel> DMLAppointmentIndicatorFailureCode(UserInfo user, AppointmentIndicatorFailureCodeViewModel model)
        {
            var response = new ResponseModel<AppointmentIndicatorFailureCodeViewModel>();
            try
            {
                response.Model = model;
                data.DMLAppointmentIndicatorFailureCode(user, model);
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

        public static ResponseModel<SelectListItem> ListAppointmentCodeAsSelectListItem()
        {
            var response = new ResponseModel<SelectListItem>();
            var data = new AppointmentIndicatorFailureCodeData();
            try
            {
                response.Data = data.ListAppointmentCodeAsSelectListItem();
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

        public ResponseModel<AppointmentIndicatorFailureCodeViewModel> ParseExcel(UserInfo user, AppointmentIndicatorFailureCodeViewModel model, Stream s)
        {
            List<SelectListItem> codeList = ListAppointmentCodeAsSelectListItem().Data;
            List<AppointmentIndicatorFailureCodeViewModel> excelList = new List<AppointmentIndicatorFailureCodeViewModel>();
            var response = new ResponseModel<AppointmentIndicatorFailureCodeViewModel>();

            try
            {
                DataSet ds = ExcelHelper.GetDataFromExcel(s);
                DataTable excelRows = ds.Tables[0];
                
                List<string> languageCodes = new List<string>();

                string code = string.Empty;
                StringBuilder multiLanguageText = new StringBuilder();

                // kolon isimleri
                for (int i = 1; i < excelRows.Columns.Count; i++)
                {
                    string descLang = excelRows.Columns[i].ToString();
                    if (!string.IsNullOrEmpty(descLang) && descLang.Contains('_'))
                    {
                        List<string> langList = descLang.Split('_').ToList();
                        string langCode = langList.ElementAt(1);
                        languageCodes.Add(langCode);
                    }
                }


                foreach (DataRow item in excelRows.Rows)
                {
                    AppointmentIndicatorFailureCodeViewModel row = new AppointmentIndicatorFailureCodeViewModel();

                    if (languageCodes.Count == 0)
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.BreakdownDefinition_Warning_MissingLanguageCode;
                    }
                    else
                    {
                        code = item[0].GetValue<string>();
                        string adminDesc = item[1].GetValue<string>();

                        multiLanguageText.Clear();
                        for (int i = 1; i < excelRows.Columns.Count; i++)
                        {
                            string multiLanguageValue = item[i].GetValue<string>();
                            if (!string.IsNullOrEmpty(multiLanguageValue))
                            {
                                string languageCode = languageCodes.ElementAt(i - 1);

                                multiLanguageText.Append(languageCode.ToUpper());
                                multiLanguageText.Append(CommonValues.Pipe);
                                multiLanguageText.Append(multiLanguageValue);
                                multiLanguageText.Append(CommonValues.Pipe);
                            }
                        }
                        row.MultiLanguageContentAsText = multiLanguageText.ToString();
                        if (multiLanguageText.ToString().Length == 0)
                        {
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.AppointmentIndicator_Warning_MissingAdminDesc;
                        }
                        if (!multiLanguageText.ToString().Contains("TR"))
                        {
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.AppointmentIndicator_Warning_MissingAdminDescTR;
                        }
                        var codeClassControl = (from dc in codeList.AsEnumerable()
                                                where dc.Text == code
                                                select dc.Value);
                        if (codeClassControl.Any())
                        {
                            row.CommandType = CommonValues.DMLType.Update;
                            row.IdAppointmentIndicatorFailureCode = codeClassControl.ElementAt(0).GetValue<int>();
                        }
                        else
                        {
                            row.CommandType = CommonValues.DMLType.Insert;
                        }

                        row.Code = code;
                        row.AdminDesc = adminDesc;
                        row.IsActive = true;
                        row.MultiLanguageContentAsText = multiLanguageText.ToString();
                    }
                    excelList.Add(row);

                }//end main else
                

                var errorCount = (from r in excelList.AsEnumerable()
                                  where r.ErrorNo > 0
                                  select r);
                if (errorCount.Any())
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.BreakdownDefinition_Warning_ExcelColumns;
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

        public MemoryStream SetExcelReport(List<AppointmentIndicatorFailureCodeViewModel> modelList, string errMsg)
        {
            string errorMessage = CommonValues.RowStart + errMsg + CommonValues.RowEnd;
            string preTable = CommonValues.TableStart 
                + CommonValues.RowStart 
                + CommonValues.ColumnStart
                + MessageResource.AppointmentIndicatorFailureCode_Display_Code 
                + CommonValues.ColumnEnd 
                + CommonValues.ColumnStart 
                + MessageResource.AppointmentIndicatorFailureCode_Display_Description 
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
                                    CommonValues.ColumnStart + "{1}" + CommonValues.ColumnEnd, model.IdAppointmentIndicatorFailureCode,
                                    model.Description);
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

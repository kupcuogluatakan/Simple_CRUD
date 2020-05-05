using ODMSData;
using ODMSModel.DownloadFileActionResult;
using ODMSModel.PDIControlDefinition;
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
    public class PDIControlDefinitionBL : BaseBusiness, IDownloadFile<PDIControlDefinitionViewModel>
    {
        private readonly PDIControlDefinitionData data = new PDIControlDefinitionData();

        public ResponseModel<PDIControlDefinitionListModel> ListPDIControlDefinition(UserInfo user, PDIControlDefinitionListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<PDIControlDefinitionListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListPDIControlDefinition(user, filter, out totalCnt);
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

        public ResponseModel<PDIControlDefinitionViewModel> GetPDIControlDefinition(UserInfo user, PDIControlDefinitionViewModel filter)
        {
            var response = new ResponseModel<PDIControlDefinitionViewModel>();
            try
            {
                data.GetPDIControlDefinition(user, filter);
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

        public ResponseModel<PDIControlDefinitionViewModel> DMLPDIControlDefinition(UserInfo user, PDIControlDefinitionViewModel model)
        {
            var response = new ResponseModel<PDIControlDefinitionViewModel>();
            try
            {
                data.DMLPDIControlDefinition(user, model);
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

        public static ResponseModel<SelectListItem> ListPDIControlRowNoAsSelectListItem()
        {
            var data = new PDIControlDefinitionData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListPDIControlRowNoAsSelectListItem();
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

        public static ResponseModel<SelectListItem> ListPDIControlCodeAsSelectListItem()
        {
            var data = new PDIControlDefinitionData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListPDIControlCodeAsSelectListItem();
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

        public static ResponseModel<SelectListItem> ListPDIControlModelCodeAsSelectListItem()
        {
            var data = new PDIControlDefinitionData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListPDIControlModelCodeAsSelectListItem();
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

        public ResponseModel<PDIControlDefinitionViewModel> ParseExcel(UserInfo user, PDIControlDefinitionViewModel model, Stream s)
        {
            var excelList = new List<PDIControlDefinitionViewModel>();

            var response = new ResponseModel<PDIControlDefinitionViewModel>();
            try
            {
                var modelList = VehicleModelBL.ListVehicleModelAsSelectList(user);
                var languageList = LanguageBL.ListLanguageAsSelectListItem(user).Data;

                DataSet ds = ExcelHelper.GetDataFromExcel(s);
                DataTable excelRows = ds.Tables[0];

                List<string> languageCodes = new List<string>();

                string controlCode = string.Empty;
                string modelKod = string.Empty;
                StringBuilder multiLanguageText = new StringBuilder();
                PDIControlDefinitionBL pdiControlDefinitionBo = new PDIControlDefinitionBL();
                int totalCount = 0;
                PDIControlDefinitionListModel listModel = new PDIControlDefinitionListModel();

                #region ColumnControl
                if (excelRows.Columns.Count < 6)
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.PDIControlDefinition_Warning_ColumnProblem;
                }
                for (int i = 6; i < excelRows.Columns.Count; i++)
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

                    PDIControlDefinitionViewModel row = new PDIControlDefinitionViewModel();


                    controlCode = excelRow[0].GetValue<string>();
                    modelKod = excelRow[1].GetValue<string>();
                    string rowNo = excelRow[2].GetValue<string>();
                    string isActive = excelRow[3].GetValue<string>();
                    string isGroupCode = excelRow[4].GetValue<string>();

                    #region Multilanguage
                    if (languageCodes.Count == 0)
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.PDIControlDefinition_Warning_MissingLanguageCode;
                    }
                    else
                    {
                        multiLanguageText.Clear();
                        for (int i = 6; i < excelRows.Columns.Count; i++)
                        {
                            string multiLanguageValue = excelRow[i].GetValue<string>();
                            if (!string.IsNullOrEmpty(multiLanguageValue))
                            {
                                string languageCode = languageCodes.ElementAt(i - 6);

                                multiLanguageText.Append(languageCode.ToUpper());
                                multiLanguageText.Append(CommonValues.Pipe);
                                multiLanguageText.Append(multiLanguageValue);
                                multiLanguageText.Append(CommonValues.Pipe);

                                if (i == 6)
                                    row.ControlNameML.txtLanguageContentAsString = multiLanguageValue;
                            }
                        }
                        row.MultiLanguageContentAsText = multiLanguageText.ToString();
                        if (multiLanguageText.ToString().Length == 0)
                        {
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.PDIControlnDefinition_Warning_MissingPDIControlName;
                        }
                    }
                    #endregion

                    #region Model Kod
                    var modelCodeControl = (from m in modelList.Data.AsEnumerable()
                                            where m.Text == modelKod
                                            select m.Value);
                    if (!modelCodeControl.Any())
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.PDIControlDefinition_Warning_ModelKodNotFound;
                    }
                    else
                    {
                        row.ModelKod = modelKod;
                    }
                    #endregion

                    #region Control Code
                    if (controlCode.Length > 5)
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.PDIControlDefinition_Warning_ControlCodeMustbe5Length;
                    }
                    else
                    {
                        row.PDIControlCode = controlCode;
                    }
                    PDIControlDefinitionViewModel viewControlModel = new PDIControlDefinitionViewModel
                    {
                        PDIControlCode = controlCode,
                        ModelKod = modelKod
                    };

                    pdiControlDefinitionBo.GetPDIControlDefinition(user, viewControlModel);

                    if (viewControlModel.IdPDIControlDefinition != 0)
                    {
                        row.CommandType = CommonValues.DMLType.Update;
                        row.IdPDIControlDefinition = viewControlModel.IdPDIControlDefinition;
                    }
                    else
                    {
                        row.CommandType = CommonValues.DMLType.Insert;
                    }
                    #endregion

                    #region Row Number
                    int rowNumber;
                    if (!int.TryParse(rowNo, out rowNumber))
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.PDIControlDefinition_Warning_RowNoMustBeNumeric;
                    }
                    else
                    {
                        row.RowNo = rowNumber;
                    }
                    #endregion

                    #region Is Active
                    int isAct;
                    if (!int.TryParse(isActive, out isAct))
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.PDIControlDefinition_Warning_IsActiveMustBeNumeric;
                    }
                    else
                    {
                        if (isAct != 0 && isAct != 1)
                        {
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.PDIControlDefinition_Warning_IsActiveMust1or0;
                        }
                        else
                        {
                            row.IsActive = isAct.GetValue<int>() != 0;
                        }
                    }
                    #endregion

                    #region Is Group
                    int isGrp;
                    if (!int.TryParse(isGroupCode, out isGrp))
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.PDIControlDefinition_Warning_IsGroupCodeMustBeNumeric;
                    }
                    else
                    {
                        if (isGrp != 0 && isGrp != 1)
                        {
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.PDIControlDefinition_Warning_IsGroupCodeMust1or0;
                        }
                        else
                        {
                            row.IsGroupCode = isGrp.GetValue<int>() != 0;
                        }
                    }
                    #endregion

                    #region DB Duplicate
                    listModel.ModelKod = row.ModelKod;
                    var list = pdiControlDefinitionBo.ListPDIControlDefinition(user, listModel, out totalCount).Data;
                    var modelCodeControlCodeControl = (from e in list.AsEnumerable()
                                                       where
                                                           ((row.IdPDIControlDefinition != 0 &&
                                                             e.IdPDIControlDefinition != row.IdPDIControlDefinition)
                                                            || row.IdPDIControlDefinition == 0)
                                                           && e.ModelKod == row.ModelKod
                                                           && e.PDIControlCode == row.PDIControlCode
                                                       select e);
                    if (modelCodeControlCodeControl.Any())
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.PDIControlDefitinition_Warning_SameModelSameControlExists;
                    }
                    var modelCodeRowNoDuplicateControl = (from e in list.AsEnumerable()
                                                          where
                                                              ((row.IdPDIControlDefinition != 0 &&
                                                                e.IdPDIControlDefinition != row.IdPDIControlDefinition)
                                                               || row.IdPDIControlDefinition == 0)
                                                              && e.ModelKod == row.ModelKod
                                                              && e.RowNo == row.RowNo
                                                          select e);
                    if (modelCodeRowNoDuplicateControl.Any())
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.PDIControlDefinition_Warning_SameModelSameRowNoExists;
                    }
                    #endregion

                    #region Excel Duplicate
                    var duplicateControl = (from e in excelList.AsEnumerable()
                                            where (e.PDIControlCode == row.PDIControlCode && e.ModelKod == row.ModelKod)
                                                  || (e.ModelKod == row.ModelKod && e.RowNo == row.RowNo)
                                            select e);
                    if (duplicateControl.Any())
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.PDIControlDefinition_Warning_DuplicateRow;
                    }
                    #endregion

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

        public MemoryStream SetExcelReport(List<PDIControlDefinitionViewModel> modelList, string errMsg)
        {
            string errorMessage = CommonValues.RowStart + errMsg + CommonValues.RowEnd;
            string preTable = CommonValues.TableStart + CommonValues.RowStart + CommonValues.ColumnStart +
                              MessageResource.PDIControlDefinition_Display_PDIControlCode +
                              CommonValues.ColumnEnd + CommonValues.ColumnStart +
                              MessageResource.PDIControlDefinition_Display_ModelKod +
                              CommonValues.ColumnEnd + CommonValues.ColumnStart +
                              MessageResource.PDIControlDefinition_Display_RowNo +
                              CommonValues.ColumnEnd + CommonValues.ColumnStart +
                              MessageResource.Global_Display_IsActive +
                              CommonValues.ColumnEnd + CommonValues.ColumnStart +
                              MessageResource.Global_Display_IsGroupCode +
                              CommonValues.ColumnEnd + CommonValues.ColumnStart +
                              MessageResource.PDIControlDefinition_Display_ControlNameML +
                              CommonValues.ColumnEnd + CommonValues.ColumnStart +
                              MessageResource.PDIControlDefinition_Display_ControlNameML + "_TR" +
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
                                    CommonValues.ColumnStart + "{2}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{3}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{4}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{5}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{6}" + CommonValues.ColumnEnd,
                                    model.PDIControlCode, model.ModelKod, model.RowNo, model.IsActive, model.IsGroupCode,
                                    model.ControlNameML.txtLanguageContentAsString, model.ControlNameML.txtLanguageContentAsString);
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

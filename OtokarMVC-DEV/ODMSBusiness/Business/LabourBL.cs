using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSData;
using ODMSModel.Labour;
using ODMSModel.ListModel;
using ODMSBusiness.Reports.Web;
using ODMSData.Utility;
using System.Data;
using ODMSModel.DownloadFileActionResult;

namespace ODMSBusiness
{
    public class LabourBL : BaseBusiness, IDownloadFile<LabourViewModel>
    {
        private readonly LabourData data = new LabourData();

        public ResponseModel<LabourListModel> GetLabourList(UserInfo user,LabourListModel labourModel, out int totalCnt)
        {
            var response = new ResponseModel<LabourListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.GetLabourList(user,labourModel, out totalCnt);
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

        public ResponseModel<LabourAndLabourDetListModel> GetLabourListForExcel(UserInfo user)
        {
            var response = new ResponseModel<LabourAndLabourDetListModel>();
            try
            {
                response.Data = data.GetLabourListForExcel(user);
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

        public static ResponseModel<SelectListItem> ListLabourAsSelectList(UserInfo user)
        {
            LabourData data = new LabourData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListLabourAsSelectList(user);
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

        public static ResponseModel<SelectListItem> ListMainGrpAsSelectList(UserInfo user)
        {
            LabourData data = new LabourData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListMainGrpAsSelectList(user);
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

        public static ResponseModel<SelectListItem> ListSubGrpAsSelectList(UserInfo user,int? id)
        {
            LabourData data = new LabourData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListSubGrpAsSelectList(user,id);
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

        public ResponseModel<LabourViewModel> DMLLabour(UserInfo user,LabourViewModel model)
        {
            var response = new ResponseModel<LabourViewModel>();
            try
            {
                data.DMLLabour(user,model);
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

        public ResponseModel<LabourViewModel> GetLabour(UserInfo user,LabourViewModel filter)
        {
            var response = new ResponseModel<LabourViewModel>();
            try
            {
                data.GetLabour(user,filter);
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

        public ResponseModel<AutocompleteSearchListModel> ListLabourNameAsAutoCompleteSearch(UserInfo user,string strSearch, string extParam)
        {
            var response = new ResponseModel<AutocompleteSearchListModel>();
            try
            {
                response.Data = data.ListLabourNamesAsAutoCompleteSearch(user,strSearch, extParam);
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

        public ResponseModel<AutocompleteSearchListModel> ListWorkOrderLabourNameAsAutoCompleteSearch(UserInfo user,string strSearch, string extraParameter)
        {
            var response = new ResponseModel<AutocompleteSearchListModel>();
            try
            {
                response.Data = data.ListWorkOrderLabourNameAsAutoCompleteSearch(user,strSearch, extraParameter);
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

        public ResponseModel<LabourViewModel> ParseExcel(UserInfo user, LabourViewModel model, Stream s)
        {
            List<LabourViewModel> excelList = new List<LabourViewModel>();

            var response = new ResponseModel<LabourViewModel>();
            try
            {
                DataSet ds = ExcelHelper.GetDataFromExcel(s);
                DataTable excelRows = ds.Tables[0];

                List<string> languageCodes = new List<string>();
                StringBuilder multiLanguageText = new StringBuilder();

                #region ColumnControl
                if (excelRows.Columns.Count < 9)
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.Labour_Warning_ColumnProblem;
                }
                // kolon isimleri
                for (int i = 8; i < excelRows.Columns.Count; i++)
                {
                    string descLang = excelRows.Columns[i].ToString();
                    if (!string.IsNullOrEmpty(descLang) && descLang.Contains('_'))
                    {
                        List<string> langList = descLang.Split('_').ToList();
                        string langCode = langList.ElementAt(1);
                        languageCodes.Add(langCode);
                    }
                }
                #endregion

                foreach (DataRow excelRow in excelRows.Rows)
                {
                    LabourViewModel row = new LabourViewModel { };

                    if (languageCodes.Count == 0)
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.Labour_Warning_MissingLanguageCode;
                    }
                    else
                    {
                        #region MultiLanguage

                        multiLanguageText.Clear();
                        for (int i = 8; i < excelRows.Columns.Count; i++)
                        {
                            string multiLanguageValue = excelRow[i].GetValue<string>();
                            if (!string.IsNullOrEmpty(multiLanguageValue))
                            {
                                string languageCode = languageCodes.ElementAt(i - 8);

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
                            row.ErrorMessage = MessageResource.Labour_Warning_EmptyAdminDesc;
                        }
                        if (!multiLanguageText.ToString().Contains("TR"))
                        {
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.Labour_Warning_MissingAdminDescTR;
                        }

                        #endregion

                        #region Main Group Code
                        string mainGroupCode = excelRow[0].GetValue<string>();
                        if (string.IsNullOrEmpty(mainGroupCode))
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.Labour_Warning_EmptyMainGroupCode;
                        }
                        else
                        {
                            List<SelectListItem> list_MainGroup = LabourBL.ListMainGrpAsSelectList(UserManager.UserInfo).Data;
                            var mainControl = (from r in list_MainGroup.AsEnumerable()
                                               where r.Value == mainGroupCode
                                               select r.Value);
                            if (!mainControl.Any())
                            {
                                // exceldeki ilgili satırın yanına hata yazılır.
                                row.ErrorNo = 1;
                                row.ErrorMessage = MessageResource.Labour_Warning_CannotFindMainGroupCode;
                            }
                            else
                            {
                                row.LabourMainGroupId = mainControl.ElementAt(0).GetValue<int>();
                            }
                        }
                        row.LabourMainGroupName = mainGroupCode;
                        #endregion

                        #region Sub Group Code
                        string subGroupCode = excelRow[1].GetValue<string>();
                        if (string.IsNullOrEmpty(subGroupCode))
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.Labour_Warning_EmptySubGroupCode;
                        }
                        else
                        {
                            List<SelectListItem> list_SubGroup = ListSubGrpAsSelectList(user, row.LabourMainGroupId).Data;
                            var subControl = (from r in list_SubGroup.AsEnumerable()
                                              where r.Value == subGroupCode
                                              select r.Value);
                            if (!subControl.Any())
                            {
                                // exceldeki ilgili satırın yanına hata yazılır.
                                row.ErrorNo = 1;
                                row.ErrorMessage = MessageResource.Labour_Warning_CannotFindSubGroupCode;
                            }
                            else
                            {
                                row.LabourSubGroupId = subControl.ElementAt(0).GetValue<int>();
                            }
                        }
                        row.LabourSubGroupName = subGroupCode;
                        #endregion

                        #region RepairCode

                        row.RepairCode = excelRow[2].GetValue<string>();
                        if (string.IsNullOrEmpty(row.RepairCode))
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.Labour_Warning_EmptyRepairCode;
                        }
                        if (row.RepairCode.Length > 6)
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.Labour_Warning_LengthRepairCode;
                        }

                        #endregion

                        #region LabourCode

                        row.LabourCode = row.LabourMainGroupName + row.LabourSubGroupName + row.RepairCode;
                        row.LabourSSID = row.LabourCode;

                        if (string.IsNullOrEmpty(row.LabourCode))
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.Labour_Warning_EmptyLabourCode;
                        }
                        else if (row.LabourCode.Length > 30)
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.Labour_Warning_LengthLabourCode;
                        }

                        #endregion

                        #region Labour Type

                        row.LabourType = excelRow[3].GetValue<string>();
                        if (string.IsNullOrEmpty(row.LabourType))
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.Labour_Warning_EmptyLabourType;
                        }
                        else
                        {
                            List<SelectListItem> labourTypeList = LabourTypeBL.ListLabourTypesAsSelectListItems(user).Data;
                            var typeControl = (from e in labourTypeList.AsEnumerable()
                                               where e.Text == row.LabourType
                                               select e);
                            if (!typeControl.Any())
                            {
                                // exceldeki ilgili satırın yanına hata yazılır.
                                row.ErrorNo = 1;
                                row.ErrorMessage = MessageResource.Labour_Warning_CannotFindLabourType;
                            }
                            else
                            {
                                row.LabourTypeId = typeControl.ElementAt(0).Value.GetValue<int>();
                            }
                        }

                        #endregion

                        #region Admin Desc

                        row.AdminDesc = excelRow[4].GetValue<string>();
                        if (string.IsNullOrEmpty(row.AdminDesc))
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.Labour_Warning_EmptyAdminDesc;
                        }
                        else if (row.AdminDesc.Length > 250)
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.Labour_Warning_LengthAdminDesc;
                        }

                        #endregion

                        #region Is Dealer Duration

                        int isDealerDuration;
                        if (!int.TryParse(excelRow[5].GetValue<string>(), out isDealerDuration))
                        {
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.Labour_Warning_IsDealerDurationMustBeNumeric;
                        }
                        else
                        {
                            if (isDealerDuration != 0 && isDealerDuration != 1)
                            {
                                row.ErrorNo = 1;
                                row.ErrorMessage = MessageResource.Labour_Warning_IsDealerDurationMust1or0;
                            }
                            else
                            {
                                row.IsDealerDuration = isDealerDuration.GetValue<int>() != 0;
                            }
                        }

                        #endregion

                        #region Is External

                        int isExternal;
                        if (!int.TryParse(excelRow[6].GetValue<string>(), out isExternal))
                        {
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.Labour_Warning_IsExternalMustBeNumeric;
                        }
                        else
                        {
                            if (isExternal != 0 && isExternal != 1)
                            {
                                row.ErrorNo = 1;
                                row.ErrorMessage = MessageResource.Labour_Warning_IsExternalMust1or0;
                            }
                            else
                            {
                                row.IsExternal = isExternal.GetValue<int>() != 0;
                            }
                        }

                        #endregion

                        #region Is Active

                        int isAct;
                        if (!int.TryParse(excelRow[7].GetValue<string>(), out isAct))
                        {
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.Labour_Warning_IsActiveMustBeNumeric;
                        }
                        else
                        {
                            if (isAct != 0 && isAct != 1)
                            {
                                row.ErrorNo = 1;
                                row.ErrorMessage = MessageResource.Labour_Warning_IsActiveMust1or0;
                            }
                            else
                            {
                                row.IsActive = isAct.GetValue<int>() != 0;
                            }
                        }

                        #endregion

                        var existed = (from r in excelList.AsEnumerable()
                                       where r.LabourCode == row.LabourCode
                                       select r);
                        if (existed.Any())
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.Labour_Warning_DuplicateValue;
                        }
                    }
                    excelList.Add(row);
                }

                var errorCount = (from r in excelList.AsEnumerable()
                                  where r.ErrorNo == 1
                                  select r);
                if (errorCount.Any())
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.Labour_Warning_FoundError;
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

        public MemoryStream SetExcelReport(List<LabourViewModel> modelList, string errMsg)
        {
            string errorMessage = CommonValues.RowStart + errMsg + CommonValues.RowEnd;
            string preTable = CommonValues.TableStart + CommonValues.RowStart
                + CommonValues.ColumnStart + MessageResource.Labour_Display_MainGrp
                + CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.Labour_Display_SubGrp
                + CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.Labour_Display_RepairCode
                + CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.Labour_Display_Type
                + CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.Global_Display_AdminDesc
                + CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.Labour_Display_DealerDuration
                + CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.Labour_Display_IsExternal
                + CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.Global_Display_IsActive
                + CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.Labour_Display_Name + "_" + UserManager.UserInfo.LanguageCode + CommonValues.ColumnEnd;
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
                                    CommonValues.ColumnStart + "{6}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{7}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{8}" + CommonValues.ColumnEnd,
                                    model.LabourMainGroupName, model.LabourSubGroupName,
                                    model.RepairCode, model.LabourType, model.AdminDesc,
                                    model.IsDealerDuration, model.IsExternal, model.IsActive,
                                    model.MultiLanguageContentAsText);
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

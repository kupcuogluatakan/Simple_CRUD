using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSData;
using ODMSModel.AppointmentIndicatorCategory;
using ODMSModel.AppointmentIndicatorMainCategory;
using ODMSModel.AppointmentIndicatorSubCategory;
using ODMSModel.DownloadFileActionResult;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class AppointmentIndicatorMainCategoryBL : BaseBusiness, IDownloadFile<AppointmentIndicatorMainCategoryViewModel>
    {
        private readonly AppointmentIndicatorMainCategoryData data = new AppointmentIndicatorMainCategoryData();

        public static ResponseModel<SelectListItem> ListAppointmentIndicatorMainCategories(UserInfo user, bool? isActive)
        {
            var response = new ResponseModel<SelectListItem>();
            var data = new AppointmentIndicatorMainCategoryData();
            try
            {
                response.Data = data.ListAppointmentIndicatorMainCategories(user, isActive);
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

        public static ResponseModel<Dictionary<string, int>> DictioanryListAppointmentIndicatorMainCategories(UserInfo user, bool? isActive)
        {
            var response = new ResponseModel<Dictionary<string, int>>();
            var data = new AppointmentIndicatorMainCategoryData();
            try
            {
                response.Model = data.DictioanryListAppointmentIndicatorMainCategories(user, isActive);
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

        public static ResponseModel<SelectListItem> ListAppointmentIndicatorMainCategoryCodes(UserInfo user, bool? isActive)
        {
            var response = new ResponseModel<SelectListItem>();
            var data = new AppointmentIndicatorMainCategoryData();
            try
            {
                response.Data = data.ListAppointmentIndicatorMainCategoryCodes(user, isActive);
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

        public ResponseModel<AppointmentIndicatorMainCategoryListModel> GetAppointmentIndicatorMainCategoryList(UserInfo user,AppointmentIndicatorMainCategoryListModel filter, out int totalCount)
        {

            var response = new ResponseModel<AppointmentIndicatorMainCategoryListModel>();
            totalCount = 0;
            try
            {
                response.Data = data.GetAppointmentIndicatorMainCategoryList(user,filter, out totalCount);
                response.Total = totalCount;
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

        public ResponseModel<AppointmentIndicatorMainCategoryViewModel> DMLAppointmentIndicatorMainCategory(UserInfo user, AppointmentIndicatorMainCategoryViewModel model)
        {
            var response = new ResponseModel<AppointmentIndicatorMainCategoryViewModel>();
            try
            {
                response.Model = model;
                data.DMLAppointmentIndicatorMainCategory(user, model);
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

        public ResponseModel<AppointmentIndicatorMainCategoryViewModel> GetAppointmentIndicatorMainCategory(UserInfo user, AppointmentIndicatorMainCategoryViewModel filter)
        {
            var response = new ResponseModel<AppointmentIndicatorMainCategoryViewModel>();
            try
            {
                data.GetAppointmentIndicatorMainCategory(user, filter);
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
        public ResponseModel<AppointmentIndicatorMainCategoryViewModel> GetAppointmentIndicatorMainCategoryByMainCode(UserInfo user, string mainCode)
        {
            var response = new ResponseModel<AppointmentIndicatorMainCategoryViewModel>();
            try
            {
                response.Model = data.GetAppointmentIndicatorMainCategoryByMainCode(user, mainCode);
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
        public ResponseModel<AppointmentIndicatorMainCategoryViewModel> ParseExcel(UserInfo user, AppointmentIndicatorMainCategoryViewModel model, Stream s)
        {
            List<AppointmentIndicatorMainCategoryViewModel> excelList = new List<AppointmentIndicatorMainCategoryViewModel>();
            var response = new ResponseModel<AppointmentIndicatorMainCategoryViewModel>();

            try
            {
                int count = 0;
                string mainCategoryCode = string.Empty;
                string categoryCode = string.Empty;
                string subCategoryCode = string.Empty;
                string indicatorType = string.Empty;
                StringBuilder multiLanguageText = new StringBuilder();
                List<string> languageCodes = new List<string>();
                AppointmentIndicatorMainCategoryBL mainBo = new AppointmentIndicatorMainCategoryBL();
                AppointmentIndicatorCategoryBL bo = new AppointmentIndicatorCategoryBL();
                AppointmentIndicatorSubCategoryBL subBo = new AppointmentIndicatorSubCategoryBL();

                
                DataSet ds = ExcelHelper.GetDataFromExcel(s);
                DataTable excelRows = ds.Tables[0];

                #region ColumnControl
                // kolon isimleri
                for (int i = 5; i < excelRows.Columns.Count; i++)
                {
                    string descLang = excelRows.Columns[i].ToString();
                    if (!string.IsNullOrEmpty(descLang) && descLang.Contains('_'))
                    {
                        List<string> langList = descLang.Split('_').ToList();
                        string langCode = langList.ElementAt(1);
                        languageCodes.Add(langCode);
                    }
                }
                if (languageCodes.Count == 0)
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.AppointmentMainCategory_Warning_MissingLanguageCode;
                }
                #endregion

                foreach (DataRow excelRow in excelRows.Rows)
                {
                    AppointmentIndicatorMainCategoryViewModel row = new AppointmentIndicatorMainCategoryViewModel();

                    List<SelectListItem> mainCategoryList = ListAppointmentIndicatorMainCategoryCodes(user, null).Data;
                    List<SelectListItem> categoryList = AppointmentIndicatorCategoryBL.ListAppointmentIndicatorCategoryCodes(user, null).Data;
                    List<SelectListItem> subCategoryList = AppointmentIndicatorSubCategoryBL.ListAppointmentIndicatorSubCategoryCodes(user, null).Data;

                    int columnCount = excelRows.Columns.Count - 1;

                    #region Value Parse

                    mainCategoryCode = excelRow[0].GetValue<string>();
                    categoryCode = excelRow[1].GetValue<string>();
                    subCategoryCode = excelRow[2].GetValue<string>();
                    indicatorType = excelRow[3].GetValue<string>();
                    string description = excelRow[4].GetValue<string>();
                    multiLanguageText.Clear();

                    row.MainCode = mainCategoryCode;
                    row.Code = categoryCode;
                    row.SubCode = subCategoryCode;
                    row.AdminDesc = description;
                    row.IndicatorTypeCode = indicatorType;
                    #endregion


                    if (!mainCategoryCode.Equals("*") && !categoryCode.Equals("*")
                        && !subCategoryCode.Equals("*"))
                    {
                        for (int i = 5; i <= columnCount; i++)
                        {
                            string multiLanguageValue = excelRow[i].GetValue<string>();
                            if (!string.IsNullOrEmpty(multiLanguageValue))
                            {
                                string languageCode = languageCodes.ElementAt(i - 5);
                                multiLanguageText.Append(languageCode);
                                multiLanguageText.Append(CommonValues.Pipe);
                                multiLanguageText.Append(multiLanguageValue);
                                multiLanguageText.Append(CommonValues.Pipe);
                            }
                        }

                        #region Main Category

                        if (!string.IsNullOrEmpty(mainCategoryCode)
                            && !mainCategoryCode.Equals(CommonValues.Minus)
                            && categoryCode.Equals(CommonValues.Minus) &&
                            subCategoryCode.Equals(CommonValues.Minus))
                        {
                            var mainControl = (from dc in mainCategoryList.AsEnumerable()
                                               where dc.Text == mainCategoryCode
                                               select dc.Value);
                            AppointmentIndicatorMainCategoryViewModel item =
                                new AppointmentIndicatorMainCategoryViewModel();
                            if (mainControl.Count() != 0)
                            {
                                item.CommandType = CommonValues.DMLType.Update;
                                item.AppointmentIndicatorMainCategoryId =
                                    mainControl.ElementAt(0).GetValue<int>();
                                mainBo.GetAppointmentIndicatorMainCategory(user, item);
                            }
                            else
                            {
                                item.CommandType = CommonValues.DMLType.Insert;
                                item.IsActive = true;
                            }

                            item.MainCode = mainCategoryCode;
                            item.AdminDesc = description;
                            item.MultiLanguageContentAsText = multiLanguageText.ToString();

                            mainBo.DMLAppointmentIndicatorMainCategory(user, item);
                            if (item.ErrorNo > 0)
                            {
                                row.ErrorNo = 1;
                                row.ErrorMessage = item.ErrorMessage;
                                excelList.Add(row);
                                break;
                            }
                        }

                        #endregion

                        #region Category

                        if (!string.IsNullOrEmpty(mainCategoryCode) &&
                            !string.IsNullOrEmpty(categoryCode) &&
                            !categoryCode.Equals(CommonValues.Minus)
                            && subCategoryCode.Equals(CommonValues.Minus))
                        {
                            var mainControl = (from dc in mainCategoryList.AsEnumerable()
                                               where dc.Text == mainCategoryCode
                                               select dc.Value);
                            var control = (from dc in categoryList.AsEnumerable()
                                           where dc.Text == categoryCode
                                           select dc.Value);
                            AppointmentIndicatorCategoryViewModel item =
                                new AppointmentIndicatorCategoryViewModel();
                            if (control.Count() != 0)
                            {
                                item.CommandType = CommonValues.DMLType.Update;
                                item.AppointmentIndicatorCategoryId = control.ElementAt(0).GetValue<int>();
                                bo.GetAppointmentIndicatorCategory(user, item);
                            }
                            else
                            {
                                item.CommandType = CommonValues.DMLType.Insert;
                                item.IsActive = true;
                            }
                            item.AppointmentIndicatorMainCategoryId = mainControl.ElementAt(0).GetValue<int>();
                            item.Code = categoryCode;
                            item.AdminDesc = description;
                            item.MultiLanguageContentAsText = multiLanguageText.ToString();

                            bo.DMLAppointmentIndicatorCategory(user, item);
                            if (item.ErrorNo > 0)
                            {
                                row.ErrorNo = 1;
                                row.ErrorMessage = item.ErrorMessage;
                                excelList.Add(row);
                                break;
                            }
                        }

                        #endregion

                        #region Sub Category

                        if (!string.IsNullOrEmpty(mainCategoryCode) &&
                            !mainCategoryCode.Equals(CommonValues.Minus) &&
                            !string.IsNullOrEmpty(categoryCode) && !categoryCode.Equals(CommonValues.Minus) &&
                            !string.IsNullOrEmpty(subCategoryCode) &&
                            !subCategoryCode.Equals(CommonValues.Minus))
                        {
                            var subControl = (from dc in subCategoryList.AsEnumerable()
                                              where dc.Text == subCategoryCode
                                              select dc.Value);
                            var control = (from dc in categoryList.AsEnumerable()
                                           where dc.Text == categoryCode
                                           select dc.Value);
                            var mainControl = (from dc in mainCategoryList.AsEnumerable()
                                               where dc.Text == mainCategoryCode
                                               select dc.Value);
                            AppointmentIndicatorSubCategoryViewModel item =
                                new AppointmentIndicatorSubCategoryViewModel();
                            if (subControl.Count() != 0)
                            {
                                item.CommandType = CommonValues.DMLType.Update;
                                item.AppointmentIndicatorSubCategoryId = subControl.ElementAt(0).GetValue<int>();
                                subBo.GetAppointmentIndicatorSubCategory(user, item);
                            }
                            else
                            {
                                item.CommandType = CommonValues.DMLType.Insert;
                                item.IsActive = true;
                            }
                            List<string> indTypeList = subBo.ListOfIndicatorTypeCode(user).Data;
                            if (indTypeList.Contains(indicatorType))
                            {
                                item.AppointmentIndicatorCategoryId = control.ElementAt(0).GetValue<int>();
                                item.AppointmentIndicatorMainCategoryId =
                                    mainControl.ElementAt(0).GetValue<int>();
                                item.IndicatorTypeCode = indicatorType;
                                item.SubCode = subCategoryCode;
                                item.AdminDesc = description;
                                item.IsAutoCreate = false;
                                item.MultiLanguageContentAsText = multiLanguageText.ToString();

                                subBo.DMLAppointmentIndicatorSubCategory(user, item);
                                if (item.ErrorNo > 0)
                                {
                                    row.ErrorNo = 1;
                                    row.ErrorMessage = item.ErrorMessage;
                                    excelList.Add(row);
                                    break;
                                }
                            }
                            else
                            {
                                row.ErrorNo = 1;
                                row.ErrorMessage = MessageResource.AppointmentIndicatorSubCategory_Warning_IndTypCodeNotFound;
                                excelList.Add(row);
                                break;
                            }
                        }
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
                    model.ErrorMessage = MessageResource.BreakdownDefinition_Warning_ExcelColumns;
                }
                if (count == 0)
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

        public static ResponseModel<SelectListItem> ListAppointmentIndicatorMainCategories(UserInfo user, string indicatorTypeCode)
        {
            var response = new ResponseModel<SelectListItem>();
            var data = new AppointmentIndicatorMainCategoryData();
            try
            {
                response.Data = data.ListAppointmentIndicatorMainCategories(user, indicatorTypeCode);
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

        public MemoryStream SetExcelReport(List<AppointmentIndicatorMainCategoryViewModel> listModel, string errMsg)
        {
            string errorMessage = CommonValues.RowStart + errMsg + CommonValues.RowEnd;
            string preTable = CommonValues.TableStart + CommonValues.RowStart + CommonValues.ColumnStart + MessageResource.AppointmentIndicatorMainCategory_Display_MainCode +
                              CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.AppointmentIndicatorCategory_Display_Code +
                              CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.AppointmentIndicatorSubCategory_Display_SubCode +
                              CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.AppointmentIndicatorSubCategory_Title_IndicatorTypeCode +
                              CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.AppointmentIndicatorMainCategory_Display_AdminDesc +
                              CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.AppointmentIndicatorMainCategory_Display_AdminDesc + "_" +
                              MessageResource.User_Display_LanguageCode +
                              CommonValues.ColumnEnd;
            if (listModel != null)
            {
                preTable = preTable + ("<TD width='400px'>Log</TD>");
            }
            const string lastTable = CommonValues.RowEnd + CommonValues.TableEnd;
            MemoryStream ms = new MemoryStream();

            var sw = new StreamWriter(ms, Encoding.UTF8);

            StringBuilder sb = new StringBuilder();
            sb.Append(errorMessage);
            sb.Append(preTable);
            if (listModel != null)
            {
                foreach (var model in listModel)
                {
                    sb.AppendFormat(CommonValues.RowStart +
                                    CommonValues.ColumnStart + "{0}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{1}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{2}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{3}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{4}" + CommonValues.ColumnEnd, model.MainCode, model.Code,
                                    model.SubCode, model.IndicatorTypeCode, model.AdminDesc);
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

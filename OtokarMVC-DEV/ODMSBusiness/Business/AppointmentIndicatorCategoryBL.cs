using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSData;
using ODMSModel.AppointmentIndicatorCategory;
using ODMSModel.DownloadFileActionResult;
using ODMSCommon.Security;
using System.Data;

namespace ODMSBusiness
{
    public class AppointmentIndicatorCategoryBL : BaseBusiness, IDownloadFile<AppointmentIndicatorCategoryViewModel>
    {
        private readonly AppointmentIndicatorCategoryData data = new AppointmentIndicatorCategoryData();

        public static ResponseModel<SelectListItem> ListAppointmentIndicatorCategories(UserInfo user, int? mainCategoryId, bool? isActive)
        {
            var response = new ResponseModel<SelectListItem>();
            var data = new AppointmentIndicatorCategoryData();
            try
            {
                response.Data = data.ListAppointmentIndicatorCategories(user, mainCategoryId, isActive);
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

        public static ResponseModel<Dictionary<string, int>> DictionaryAppointmentIndicatorCategories(UserInfo user, int? mainCategoryId, bool? isActive)
        {
            var response = new ResponseModel<Dictionary<string, int>>();
            var data = new AppointmentIndicatorCategoryData();
            try
            {
                response.Model = data.DictionaryAppointmentIndicatorCategories(user, mainCategoryId, isActive);
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

        public static ResponseModel<SelectListItem> ListAppointmentIndicatorCategories(UserInfo user, int id, string typeCode)
        {
            var response = new ResponseModel<SelectListItem>();
            var data = new AppointmentIndicatorCategoryData();
            try
            {
                response.Data = data.ListAppointmentIndicatorCategories(user, id, typeCode);
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

        public static ResponseModel<SelectListItem> ListAppointmentIndicatorCategoryCodes(UserInfo user, bool? isActive)
        {
            var response = new ResponseModel<SelectListItem>();
            var data = new AppointmentIndicatorCategoryData();
            try
            {
                response.Data = data.ListAppointmentIndicatorCategoryCodes(user, isActive);
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

        public ResponseModel<AppointmentIndicatorCategoryListModel> GetAppointmentIndicatorCategoryList(UserInfo user,AppointmentIndicatorCategoryListModel filter, out int totalCount)
        {
            var response = new ResponseModel<AppointmentIndicatorCategoryListModel>();
            totalCount = 0;
            try
            {
                response.Data = data.GetAppointmentIndicatorCategoryList(user,filter, out totalCount);
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

        public ResponseModel<AppointmentIndicatorCategoryViewModel> GetAppointmentIndicatorCategoryList(UserInfo user,AppointmentIndicatorCategoryListModel filter, out int totalCount, bool overload)
        {
            var response = new ResponseModel<AppointmentIndicatorCategoryViewModel>();
            totalCount = 0;
            try
            {
                response.Data = data.GetAppointmentIndicatorCategoryList(user,filter, out totalCount, true);
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

        public ResponseModel<AppointmentIndicatorCategoryViewModel> DMLAppointmentIndicatorCategory(UserInfo user, AppointmentIndicatorCategoryViewModel model)
        {
            var response = new ResponseModel<AppointmentIndicatorCategoryViewModel>();
            try
            {
                response.Model = model;
                data.DMLAppointmentIndicatorCategory(user, model);
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

        public ResponseModel<AppointmentIndicatorCategoryViewModel> DMLAppointmentIndicatorCategory(UserInfo user, AppointmentIndicatorCategoryViewModel model, List<AppointmentIndicatorCategoryViewModel> filter)
        {
            var response = new ResponseModel<AppointmentIndicatorCategoryViewModel>();
            try
            {
                response.Model = model;
                data.DMLAppointmentIndicatorCategory(user, model, filter);
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

        public ResponseModel<AppointmentIndicatorCategoryViewModel> GetAppointmentIndicatorCategory(UserInfo user, AppointmentIndicatorCategoryViewModel filter)
        {
            var response = new ResponseModel<AppointmentIndicatorCategoryViewModel>();
            try
            {
                data.GetAppointmentIndicatorCategory(user, filter);
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

        public ResponseModel<AppointmentIndicatorCategoryViewModel> GetAppointmentIndicatorCategoryByCode(UserInfo user, string code)
        {
            var response = new ResponseModel<AppointmentIndicatorCategoryViewModel>();
            try
            {
                response.Model = data.GetAppointmentIndicatorCategoryByCode(user, code);
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

        public ResponseModel<AppointmentIndicatorCategoryViewModel> ParseExcel(UserInfo user, AppointmentIndicatorCategoryViewModel model, Stream s)
        {
            var listModels = new List<AppointmentIndicatorCategoryViewModel>();
            var response = new ResponseModel<AppointmentIndicatorCategoryViewModel>();
            try
            {
                #region Definitions
                var totalCnt = 0;
                var mainCategoryList = AppointmentIndicatorMainCategoryBL.DictioanryListAppointmentIndicatorMainCategories(user, true).Model;
                var listOfLabourMainGroup = GetAppointmentIndicatorCategoryList(user, new AppointmentIndicatorCategoryListModel(), out totalCnt).Data;
                var languages = new Dictionary<int, string>();
                DataSet data = ExcelHelper.GetDataFromExcel(s);
                DataTable excelRows = data.Tables[0];
                #endregion

                int columnCount = excelRows.Columns.Count;

                #region Column Control
                //value of variable must be like excel languages count !
                if (columnCount < 3)
                {
                    var m = new AppointmentIndicatorCategoryViewModel
                    {
                        ErrorNo = 1,
                        ErrorMessage = MessageResource.Labour_Error_InvalidExcelFormat
                    };

                    response.IsSuccess = false;
                    response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, MessageResource.Labour_Error_InvalidExcelFormat);

                    return response;
                }


                //value of variable must be like excel languages count !
                for (var i = 3; i < columnCount; i++)
                {
                    var lang = excelRows.Columns[i].ToString().Substring(excelRows.Columns[i].ToString().Length - 2);
                    languages.Add(i, lang.ToUpper());
                }
                #endregion

                #region Row Control
                //For Other Rows
                foreach (DataRow excelRow in excelRows.Rows)
                {
                    var itemModel = new AppointmentIndicatorCategoryViewModel
                    {
                        AppointmentIndicatorMainCategoryId = mainCategoryList.Any(p => p.Key.ToString(CultureInfo.InvariantCulture) == excelRow[0].GetValue<string>()) ?
                                                             mainCategoryList[excelRow[0].GetValue<string>()] : 0,
                        AppointmentIndicatorMainCategoryName = excelRow[0].GetValue<string>(),
                        Code = excelRow[1].GetValue<string>(),
                        AdminDesc = excelRow[2].GetValue<string>(),
                        IsActive = true,
                        CommandType = CommonValues.DMLType.Insert
                    };

                    //value of variable must be like excel languages count !
                    for (var i = 3; i < excelRows.Columns.Count; i++)
                    {
                        if (languages[i] == "TR" && string.IsNullOrEmpty(excelRow[i].GetValue<string>()))
                        {
                            itemModel.ErrorNo = 1;
                            itemModel.ErrorMessage = MessageResource.AppointmnetIndicatorSubCategory_Warning_SubCategoryName;
                        }
                        itemModel._AppointmentIndicatorCategoryName += excelRow[i].GetValue<string>() + "|";
                        itemModel.MultiLanguageContentAsText += languages[i] + CommonValues.Pipe + excelRow[i].GetValue<string>() + CommonValues.Pipe;
                    }

                    #region Validations
                    itemModel.List = listOfLabourMainGroup;
                    itemModel.Languages = languages;
                    itemModel.ExcelValidate(itemModel);
                    #endregion

                    listModels.Add(itemModel);
                }
                #endregion

                response.Data = listModels;
                response.Total = listModels.Count;
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

        public MemoryStream SetExcelReport(List<AppointmentIndicatorCategoryViewModel> listModel, string errorMessage)
        {
            string errMsg = CommonValues.RowStart + errorMessage + CommonValues.RowEnd;
            string preTable = CommonValues.TableStart +
                              CommonValues.RowStart +
                              CommonValues.ColumnStart + MessageResource.AppointmentIndicatorSubCategory_Title_Main_Category_Code + CommonValues.ColumnEnd +
                              CommonValues.ColumnStart + MessageResource.AppointmentIndicatorSubCategory_Title_Category_Code + CommonValues.ColumnEnd +
                              CommonValues.ColumnStart + MessageResource.AppointmentIndicatorCategory_Display_AdminDesc + CommonValues.ColumnEnd +
                              CommonValues.ColumnStart + MessageResource.AppointmentIndicatorSubCategory_Title_Category_Name_LangCode + CommonValues.ColumnEnd;
            if (listModel != null)
            {
                preTable = preTable + ("<TD width='400px'>Log</TD>");
            }
            const string lastTable = CommonValues.RowEnd + CommonValues.TableEnd;
            var ms = new MemoryStream();

            var sw = new StreamWriter(ms, Encoding.UTF8);

            var sb = new StringBuilder();
            sb.Append(errMsg);
            sb.Append(preTable);
            if (listModel != null)
            {
                foreach (var model in listModel)
                {
                    sb.AppendFormat(CommonValues.RowStart + CommonValues.ColumnStart + "{0}" + CommonValues.ColumnEnd +
                                        CommonValues.ColumnStart + "{1}" + CommonValues.ColumnEnd +
                                        CommonValues.ColumnStart + "{2}" + CommonValues.ColumnEnd +
                                        CommonValues.ColumnStart + "{3}" + CommonValues.ColumnEnd,
                                        model.AppointmentIndicatorMainCategoryName,
                                        model.Code,
                                        model.AdminDesc,
                                        model._AppointmentIndicatorCategoryName);

                    if (model.ErrorNo > 0)
                    {
                        sb.AppendFormat("<TD bgcolor='#FFCCCC'>{0}</TD>" + CommonValues.RowEnd, model.ErrorMessage);
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

        public MemoryStream SampleExcelFormat()
        {
            string lastTable = CommonValues.TableStart + CommonValues.RowStart + CommonValues.ColumnStart +
                               MessageResource.AppointmentIndicatorSubCategory_Title_Main_Category_Code + " </TD>" +
                               CommonValues.ColumnStart +
                               MessageResource.AppointmentIndicatorSubCategory_Title_Category_Code +
                               CommonValues.ColumnEnd +
                               CommonValues.ColumnStart + MessageResource.AppointmentIndicatorCategory_Display_AdminDesc +
                               CommonValues.ColumnEnd +
                               CommonValues.ColumnStart +
                               MessageResource.AppointmentIndicatorSubCategory_Title_Category_Name_LangCode +
                               CommonValues.ColumnEnd +
                               CommonValues.RowEnd +
                               CommonValues.TableEnd;

            var ms = new MemoryStream();
            var sw = new StreamWriter(ms, Encoding.UTF8);
            var sb = new StringBuilder();

            sb.Append(lastTable);

            sw.WriteLine(sb.ToString());

            sw.Flush();
            ms.Seek(0, SeekOrigin.Begin);

            return ms;
        }
    }
}

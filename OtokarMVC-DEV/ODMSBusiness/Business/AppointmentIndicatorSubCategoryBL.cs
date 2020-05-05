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
using ODMSModel.AppointmentIndicatorSubCategory;
using ODMSModel.DownloadFileActionResult;
using ODMSCommon.Security;
using System.Data;

namespace ODMSBusiness
{
    public class AppointmentIndicatorSubCategoryBL : BaseBusiness, IDownloadFile<AppointmentIndicatorSubCategoryViewModel>
    {
        private readonly AppointmentIndicatorSubCategoryData data = new AppointmentIndicatorSubCategoryData();

        public static ResponseModel<SelectListItem> ListAppointmentIndicatorSubCategories(UserInfo user, int? categoryId, bool? isActive)
        {
            var response = new ResponseModel<SelectListItem>();
            var data = new AppointmentIndicatorSubCategoryData();
            try
            {
                response.Data = data.ListAppointmentIndicatorSubCategories(user, categoryId, isActive);
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

        public static ResponseModel<SelectListItem> ListAppointmentIndicatorSubCategories(UserInfo user, int id, string typeCode)
        {
            var response = new ResponseModel<SelectListItem>();
            var data = new AppointmentIndicatorSubCategoryData();
            try
            {
                response.Data = data.ListAppointmentIndicatorSubCategories(user, id, typeCode);
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

        public static ResponseModel<SelectListItem> ListAppointmentIndicatorSubCategoryCodes(UserInfo user, bool? isActive)
        {
            var response = new ResponseModel<SelectListItem>();
            var data = new AppointmentIndicatorSubCategoryData();
            try
            {
                response.Data = data.ListAppointmentIndicatorSubCategoryCodes(user, isActive);
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

        public ResponseModel<AppointmentIndicatorSubCategoryListModel> GetAppointmentIndicatorSubCategoryList(UserInfo user,AppointmentIndicatorSubCategoryListModel filter, out int totalCount)
        {
            var response = new ResponseModel<AppointmentIndicatorSubCategoryListModel>();
            totalCount = 0;
            try
            {
                response.Data = data.GetAppointmentIndicatorSubCategoryList(user,filter, out totalCount);
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

        public ResponseModel<AppointmentIndicatorSubCategoryViewModel> DMLAppointmentIndicatorSubCategory(UserInfo user, AppointmentIndicatorSubCategoryViewModel model)
        {
            var response = new ResponseModel<AppointmentIndicatorSubCategoryViewModel>();
            try
            {
                data.DMLAppointmentIndicatorSubCategory(user, model);
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

        public ResponseModel<AppointmentIndicatorSubCategoryViewModel> DMLAppointmentIndicatorSubCategory(UserInfo user, AppointmentIndicatorSubCategoryViewModel model, List<AppointmentIndicatorSubCategoryViewModel> listModel)
        {
            var response = new ResponseModel<AppointmentIndicatorSubCategoryViewModel>();
            try
            {
                data.DMLAppointmentIndicatorSubCategory(user, model, listModel);
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

        public ResponseModel<AppointmentIndicatorSubCategoryViewModel> GetAppointmentIndicatorSubCategory(UserInfo user, AppointmentIndicatorSubCategoryViewModel filter)
        {
            var response = new ResponseModel<AppointmentIndicatorSubCategoryViewModel>();
            try
            {
                data.GetAppointmentIndicatorSubCategory(user, filter);
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

        public ResponseModel<AppointmentIndicatorSubCategoryViewModel> GetAppointmentIndicatorSubCategoryBySubCode(UserInfo user, string subCode)
        {
            var response = new ResponseModel<AppointmentIndicatorSubCategoryViewModel>();
            try
            {
                response.Model = data.GetAppointmentIndicatorSubCategoryBySubCode(user, subCode);
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

        public ResponseModel<AppointmentIndicatorSubCategoryViewModel> ParseExcel(UserInfo user, AppointmentIndicatorSubCategoryViewModel model, Stream s)
        {
            var listModels = new List<AppointmentIndicatorSubCategoryViewModel>();
            var response = new ResponseModel<AppointmentIndicatorSubCategoryViewModel>();
            try
            {
                #region Definitions
                var totalCnt = 0;
                var mainCategoryList = AppointmentIndicatorMainCategoryBL.DictioanryListAppointmentIndicatorMainCategories(user, true).Model;
                var categoryList = AppointmentIndicatorCategoryBL.DictionaryAppointmentIndicatorCategories(user, null, true).Model;
                var indicatorTypeCodeList = ListOfIndicatorTypeCode(user);
                var listOfLabourMainGroup = GetAppointmentIndicatorSubCategoryList(user,new AppointmentIndicatorSubCategoryListModel(), out totalCnt);
                var languages = new Dictionary<int, string>();

                DataSet ds = ExcelHelper.GetDataFromExcel(s);
                DataTable excelRows = ds.Tables[0];
                #endregion

                #region ColumnControl
                //value of variable must be like excel languages count !
                if (excelRows.Columns.Count < 6)
                    {
                        var m = new AppointmentIndicatorSubCategoryViewModel
                        {
                            ErrorNo = 1,
                            ErrorMessage = MessageResource.Labour_Error_InvalidExcelFormat
                        };

                        response.IsSuccess = false; 
                        response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, MessageResource.Labour_Error_InvalidExcelFormat);
                        return response;
                    }


                    //value of variable must be like excel languages count !
                    for (var i = 6; i < excelRows.Columns.Count; i++)
                    {
                        var lang = excelRows.Columns[i].ToString().Substring(excelRows.Columns[i].ToString().Length - 2);
                        languages.Add(i, lang.ToUpper());
                    }

                #endregion

                //For Other Rows
                foreach (DataRow excelRow in excelRows.Rows)
                {
                    var itemModel = new AppointmentIndicatorSubCategoryViewModel
                    {
                        AppointmentIndicatorMainCategoryId = mainCategoryList.Any(p => p.Key.ToString(CultureInfo.InvariantCulture) == excelRow[0].GetValue<string>()) ? mainCategoryList[excelRow[0].GetValue<string>()] : 0,
                        AppointmentIndicatorCategoryId = categoryList.Any(p => p.Key.ToString(CultureInfo.InvariantCulture) == excelRow[1].GetValue<string>()) ? categoryList[excelRow[1].GetValue<string>()] : 0,
                        AppointmentIndicatorMainCategoryName = excelRow[0].GetValue<string>(),
                        AppointmentIndicatorCategoryName = excelRow[1].GetValue<string>(),
                        SubCode = excelRow[2].GetValue<string>(),
                        AdminDesc = excelRow[3].GetValue<string>(),
                        IsAutoCreate = excelRow[4].GetValue<bool>(),
                        IndicatorTypeCode = excelRow[5].GetValue<string>().ToUpper(),
                        IsActive = true,
                        CommandType = CommonValues.DMLType.Insert
                    };

                    //value of variable must be like excel languages count !
                    for (var i = 6; i < excelRows.Columns.Count; i++)
                    {
                        if (languages[i] == "TR" && string.IsNullOrEmpty(excelRow[i].GetValue<string>()))
                        {
                            itemModel.ErrorNo = 1;
                            itemModel.ErrorMessage = MessageResource.AppointmnetIndicatorSubCategory_Warning_SubCategoryName;
                        }
                        itemModel._AppointmentIndicatorSubCategoryName += excelRow[i].GetValue<string>() + "|";
                        itemModel.MultiLanguageContentAsText += languages[i] + CommonValues.Pipe + excelRow[i].GetValue<string>() + CommonValues.Pipe;
                    }

                    #region Validations
                    itemModel.List = listOfLabourMainGroup.Data;
                    itemModel.Languages = languages;
                    itemModel.IndicatorTypeCodeList = indicatorTypeCodeList.Data;
                    itemModel.ExcelValidate(itemModel);
                    #endregion

                    listModels.Add(itemModel);
                }

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

        public MemoryStream SetExcelReport(List<AppointmentIndicatorSubCategoryViewModel> listModel, string errorMessage)
        {
            string errMsg = CommonValues.RowStart + errorMessage + CommonValues.RowEnd;
            string preTable = CommonValues.TableStart +
                              CommonValues.RowStart +
                              CommonValues.ColumnStart + MessageResource.AppointmentIndicatorSubCategory_Title_Main_Category_Code + CommonValues.ColumnEnd +
                              CommonValues.ColumnStart + MessageResource.AppointmentIndicatorSubCategory_Title_Category_Code + CommonValues.ColumnEnd +
                              CommonValues.ColumnStart + MessageResource.AppointmentIndicatorSubCategory_Title_Sub_Category_Code + CommonValues.ColumnEnd +
                              CommonValues.ColumnStart + MessageResource.AppointmentIndicatorSubCategory_Title_Sub_Category_Description + CommonValues.ColumnEnd +
                              CommonValues.ColumnStart + MessageResource.AppointmentIndicatorSubCategory_Title_Is_Auto_Create + CommonValues.ColumnEnd +
                              CommonValues.ColumnStart + MessageResource.AppointmentIndicatorSubCategory_Title_IndicatorTypeCode + CommonValues.ColumnEnd +
                              CommonValues.ColumnStart + MessageResource.AppointmentIndicatorSubCategory_Title_Sub_Category_Name_LangCode + CommonValues.ColumnEnd;
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
                                        CommonValues.ColumnStart + "{3}" + CommonValues.ColumnEnd +
                                        CommonValues.ColumnStart + "{4}" + CommonValues.ColumnEnd +
                                        CommonValues.ColumnStart + "{5}" + CommonValues.ColumnEnd +
                                        CommonValues.ColumnStart + "{6}" + CommonValues.ColumnEnd,
                                        model.AppointmentIndicatorMainCategoryName,
                                        model.AppointmentIndicatorCategoryName,
                                        model.SubCode,
                                        model.AdminDesc,
                                        Convert.ToInt32(model.IsAutoCreate),
                                        model.IndicatorTypeCode,
                                        model._AppointmentIndicatorSubCategoryName);

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

        public MemoryStream SampleExcelFormat()
        {
            string lastTable = CommonValues.TableStart + CommonValues.RowStart + CommonValues.ColumnStart +
                               MessageResource.AppointmentIndicatorSubCategory_Title_Main_Category_Code + " </TD>" +
                               CommonValues.ColumnStart +
                               MessageResource.AppointmentIndicatorSubCategory_Title_Category_Code +
                               CommonValues.ColumnEnd +
                               CommonValues.ColumnStart +
                               MessageResource.AppointmentIndicatorSubCategory_Title_Sub_Category_Code +
                               CommonValues.ColumnEnd +
                               CommonValues.ColumnStart +
                               MessageResource.AppointmentIndicatorSubCategory_Title_Sub_Category_Description +
                               CommonValues.ColumnEnd +
                               CommonValues.ColumnStart +
                               MessageResource.AppointmentIndicatorSubCategory_Title_Is_Auto_Create +
                               CommonValues.ColumnEnd +
                               CommonValues.ColumnStart +
                               MessageResource.AppointmentIndicatorSubCategory_Title_IndicatorTypeCode +
                               CommonValues.ColumnEnd +
                               CommonValues.ColumnStart +
                               MessageResource.AppointmentIndicatorSubCategory_Title_Sub_Category_Name_LangCode +
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

        public ResponseModel<string> ListOfIndicatorTypeCode(UserInfo user)
        {
            var response = new ResponseModel<string>();
            try
            {
                response.Data = data.ListOfIndicatorTypeCode(user).ToList();
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

        public ResponseModel<SelectListItem> ListOfIndicatorTypeCodeAsSelectListItem(UserInfo user)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListOfIndicatorTypeCodeAsSelectListItem(user);
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

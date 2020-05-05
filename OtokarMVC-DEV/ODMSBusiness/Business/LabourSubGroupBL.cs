using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSData;
using ODMSModel.DownloadFileActionResult;
using ODMSModel.LabourSubGroup;
using ODMSCommon.Security;
using System.Data;

namespace ODMSBusiness
{
    public class LabourSubGroupBL : BaseService<LabourSubGroupViewModel>, IDownloadFile<LabourSubGroupViewModel>
    {
        private readonly LabourSubGroupData data = new LabourSubGroupData();

        public override ResponseModel<LabourSubGroupViewModel> Get(UserInfo user, LabourSubGroupViewModel filter)
        {
            var response = new ResponseModel<LabourSubGroupViewModel>();
            try
            {
                response.Model = data.Get(user, filter);
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

        public ResponseModel<LabourSubGroupListModel> List(UserInfo user, LabourSubGroupListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<LabourSubGroupListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.List(user, filter, out totalCnt);
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

        public new ResponseModel<LabourSubGroupViewModel> Insert(UserInfo user, LabourSubGroupViewModel model)
        {
            var response = new ResponseModel<LabourSubGroupViewModel>();
            try
            {
                data.Insert(user, model);
                response.Model = model;
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

        public ResponseModel<LabourSubGroupViewModel> Insert(UserInfo user, LabourSubGroupViewModel model, List<LabourSubGroupViewModel> listModel)
        {
            var response = new ResponseModel<LabourSubGroupViewModel>();
            try
            {
                data.Insert(user, model, listModel);
                response.Model = model;
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

        public new ResponseModel<LabourSubGroupViewModel> Update(UserInfo user, LabourSubGroupViewModel model)
        {
            var response = new ResponseModel<LabourSubGroupViewModel>();
            try
            {
                data.Update(user, model);
                response.Model = model;
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

        public new ResponseModel<LabourSubGroupViewModel> Delete(UserInfo user, LabourSubGroupViewModel model)
        {
            var response = new ResponseModel<LabourSubGroupViewModel>();
            try
            {
                data.Delete(user, model);
                response.Model = model;
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

        public ResponseModel<LabourSubGroupViewModel> ParseExcel(UserInfo user, LabourSubGroupViewModel model, Stream s)
        {
            var listModels = new List<LabourSubGroupViewModel>();
            var response = new ResponseModel<LabourSubGroupViewModel>();
            try
            {

                #region Definitions
                var totalCnt = 0;
                var listOfLabourMainGroup = List(user,new LabourSubGroupListModel() { IsActive = 1 }, out totalCnt);
                var languages = new Dictionary<int, string>();

                DataSet ds = ExcelHelper.GetDataFromExcel(s);
                DataTable excelRows = ds.Tables[0];
                #endregion

                #region ColumnControl
                //value of variable must be like excel languages count !
                if (excelRows.Columns.Count < 3)
                    {
                        response.IsSuccess = false; 
                        response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, MessageResource.Labour_Error_InvalidExcelFormat);
                        return response;
                    }


                    //value of variable must be like excel languages count !
                    for (var i = 3; i < excelRows.Columns.Count; i++)
                    {
                        var lang = excelRows.Columns[i].ToString().Substring(excelRows.Columns[i].ToString().Length - 2);
                        languages.Add(i, lang.ToUpper());
                    }
                #endregion

                //For Other Rows
                foreach (DataRow excelRow in excelRows.Rows)
                {
                    var itemModel = new LabourSubGroupViewModel
                    {
                        SubGroupId = excelRow[0].GetValue<string>(),
                        MainGroupId = excelRow[1].GetValue<string>(),
                        Description = excelRow[2].GetValue<string>(),
                        IsActive = true
                    };

                    //value of variable must be like excel languages count !
                    for (var i = 3; i < excelRows.Columns.Count; i++)
                    {
                        if (languages[i] == "TR" && string.IsNullOrEmpty(excelRow[i].GetValue<string>()))
                        {
                            itemModel.ErrorNo = 1;
                            itemModel.ErrorMessage = MessageResource.LabourMainGroup_Warning_LabourMainGroupName;
                        }
                        itemModel.LabourSubGroupName += excelRow[i].GetValue<string>() + "|";
                        itemModel.MultiLanguageContentAsText += languages[i] + CommonValues.Pipe + excelRow[i].GetValue<string>() + CommonValues.Pipe;
                    }

                    #region Validations
                    itemModel.List = listOfLabourMainGroup.Data;
                    itemModel.Languages = languages;
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

        public MemoryStream SetExcelReport(List<LabourSubGroupViewModel> listModel, string errorMessage)
        {
            string errMsg = CommonValues.RowStart + errorMessage + CommonValues.RowEnd;
            string preTable = CommonValues.TableStart +
                              CommonValues.RowStart +
                              CommonValues.ColumnStart + MessageResource.LabourSubGroup_Display_SubGroupId + CommonValues.ColumnEnd +
                              CommonValues.ColumnStart + MessageResource.LabourMainGroup_Display_LabourMainGroupId + CommonValues.ColumnEnd +
                              CommonValues.ColumnStart + MessageResource.LabourSubGroup_Display_Description + CommonValues.ColumnEnd +
                              CommonValues.ColumnStart + MessageResource.LabourSubGroup_Display_LabourSubGroupNameLangCode + CommonValues.ColumnEnd;
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
                                        model.SubGroupId,
                                        model.MainGroupId,
                                        model.Description,
                                        model.LabourSubGroupName);

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
                               MessageResource.LabourSubGroup_Display_SubGroupId + CommonValues.ColumnEnd +
                               CommonValues.ColumnStart + MessageResource.LabourMainGroup_Display_LabourMainGroupId +
                               CommonValues.ColumnEnd +
                               CommonValues.ColumnStart + MessageResource.LabourSubGroup_Display_Description +
                               CommonValues.ColumnEnd +
                               CommonValues.ColumnStart +
                               MessageResource.LabourSubGroup_Display_LabourSubGroupNameLangCode +
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

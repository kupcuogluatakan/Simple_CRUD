using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSData;
using ODMSModel.DownloadFileActionResult;
using ODMSModel.EducationContributers;
using System.Data;

namespace ODMSBusiness
{
    public class EducationContributersBL : BaseBusiness, IDownloadFile<EducationContributersViewModel>
    {
        private readonly EducationContributersData data = new EducationContributersData();

        public ResponseModel<EducationContributersViewModel> DMLEducationContributers(UserInfo user, EducationContributersViewModel model)
        {
            var response = new ResponseModel<EducationContributersViewModel>();
            try
            {
                data.DMLEducationContributers(user, model);
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

        public ResponseModel<EducationContributersListModel> GetEducationContList(UserInfo user, EducationContributersListModel filter, out int totalCount)
        {
            var response = new ResponseModel<EducationContributersListModel>();
            totalCount = 0;
            try
            {
                response.Data = data.GetEducationContList(user, filter, out totalCount);
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

        private void CheckExcelRecord(EducationContributersViewModel model, string lang)
        {
            if (!CommonUtility.ValidateTcIdentityNumber(model.TCIdentity))
            {
                model.ErrorNo = 1;
                model.ErrorMessage = string.Format(MessageResource.Global_Display_NotValidFormat, MessageResource.EducationCont_Display_TCId);
            }
            if (model.WorkingCompany.Length > 50)
            {
                model.ErrorNo = 1;
                model.ErrorMessage = string.Format(MessageResource.Global_Display_NotValidFormat, MessageResource.EducationCont_Display_Company);
            }
            if (model.FullName.Length > 100)
            {
                model.ErrorNo = 1;
                model.ErrorMessage = string.Format(MessageResource.Global_Display_NotValidFormat, MessageResource.EducationCont_Display_FullName);
            }
            if (model.Grade.Length > 10)
            {
                model.ErrorNo = 1;
                model.ErrorMessage = string.Format(MessageResource.Global_Display_NotValidFormat, MessageResource.EducationCont_Display_Grade);
            }
        }

        public ResponseModel<EducationContributersViewModel> ParseExcel(UserInfo user, EducationContributersViewModel model, Stream s)
        {
            List<EducationContributersViewModel> listModels = new List<EducationContributersViewModel>();
            var response = new ResponseModel<EducationContributersViewModel>();
            try
            {
                string lang = UserManager.LanguageCode;
                EducationContributersData eduContData = new EducationContributersData();

                DataSet ds = ExcelHelper.GetDataFromExcel(s);
                DataTable excelRows = ds.Tables[0];

                foreach (DataRow excelRow in excelRows.Rows)
                {
                    EducationContributersViewModel eduContModel = new EducationContributersViewModel();
                    try
                    {
                        if (excelRow[0] != null && excelRow[1] != null && excelRow[2] != null && excelRow[3] != null)
                        {
                            eduContModel.EducationCode = model.EducationCode;
                            eduContModel.DealerId = UserManager.UserInfo.GetUserDealerId();
                            eduContModel.TCIdentity = excelRow[0].ToString();
                            eduContModel.FullName = excelRow[1].ToString();
                            eduContModel.WorkingCompany = excelRow[2].ToString();
                            eduContModel.Grade = excelRow[3].ToString();

                            CheckExcelRecord(eduContModel, lang);
                        }
                    }
                    catch (Exception ex)
                    {
                        eduContModel.ErrorNo = 1;
                        eduContModel.ErrorMessage = ex.Message;
                    }
                    finally
                    {
                        listModels.Add(eduContModel);
                    }
                }


                eduContData.IsDMLEducationContributers(user, model, listModels);

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

        public MemoryStream SetExcelReport(List<EducationContributersViewModel> listModels, string errMsg)
        {
            string errorMessage = CommonValues.RowStart + errMsg + CommonValues.RowEnd;
            string preTable = CommonValues.TableStart + CommonValues.RowStart + CommonValues.ColumnStart + MessageResource.EducationCont_Display_TCId
                + CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.EducationCont_Display_FullName + CommonValues.ColumnEnd
                + CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.EducationCont_Display_Company + CommonValues.ColumnEnd
                + CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.EducationCont_Display_Grade + CommonValues.ColumnEnd;
            if (listModels != null)
            {
                preTable = preTable + ("<TD width='400px'>Log</TD>");
            }
            const string lastTable = CommonValues.RowEnd + CommonValues.TableEnd;
            MemoryStream ms = new MemoryStream();

            var sw = new StreamWriter(ms, Encoding.UTF8);

            StringBuilder sb = new StringBuilder();
            sb.Append(errorMessage);
            sb.Append(preTable);
            if (listModels != null)
            {
                foreach (var model in listModels)
                {
                    sb.AppendFormat(CommonValues.RowStart +
                                    CommonValues.ColumnStart + "{0}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{1}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{2}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{3}" + CommonValues.ColumnEnd, model.TCIdentity,
                                    model.FullName, model.WorkingCompany, model.Grade);
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

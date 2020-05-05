using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSModel.ListModel;
using ODMSModel.MaintenanceLabour;
using ODMSModel.Maintenance;
using System.IO;
using ODMSCommon;
using ODMSCommon.Resources;
using System.Linq;
using System.Text;
using ODMSCommon.Security;
using System.Data;
using ODMSModel.DownloadFileActionResult;

namespace ODMSBusiness
{
    public class MaintenanceLabourBL : BaseBusiness, IDownloadFile<MaintenanceLabourViewModel>
    {
        private readonly MaintenanceLabourData data = new MaintenanceLabourData();
        private readonly LabourDurationData dataDuration = new LabourDurationData();


        public ResponseModel<MaintenanceLabourViewModel> DMLMaintenanceLabour(UserInfo user, MaintenanceLabourViewModel model)
        {
            var response = new ResponseModel<MaintenanceLabourViewModel>();
            try
            {
                data.DMLMaintenanceLabour(user, model);
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

        public ResponseModel<MaintenanceLabourViewModel> GetMaintenanceLabour(UserInfo user, int maintenanceId, int labourId)
        {
            var response = new ResponseModel<MaintenanceLabourViewModel>();
            try
            {
                response.Model = data.GetMaintenanceLabour(user, maintenanceId, labourId);
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

        public ResponseModel<MaintenanceLabourListModel> ListMaintenanceLabours(UserInfo user, MaintenanceLabourListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<MaintenanceLabourListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListMaintenanceLabours(user, filter, out totalCnt);
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

        public ResponseModel<MaintenanceLabourListModel> ListMaintenanceLaboursForExcel(UserInfo user, MaintenanceLabourListModel filter, MaintenanceListModel maintModel, out int totalCnt)
        {
            var response = new ResponseModel<MaintenanceLabourListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListMaintenanceLaboursForExcel(user, filter, maintModel, out totalCnt);
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

        public ResponseModel<SelectListItem> ListLabours(UserInfo user)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = dataDuration.GetLabourList(user);
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

        public ResponseModel<AutocompleteSearchListModel> ListLabours(UserInfo user, string strSearch)
        {
            var response = new ResponseModel<AutocompleteSearchListModel>();
            try
            {
                response.Data = dataDuration.GetLabourList(user, strSearch);
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

        public ResponseModel<MaintenanceLabourViewModel> ParseExcel(UserInfo user, MaintenanceLabourViewModel model, Stream s)
        {
            List<MaintenanceLabourViewModel> excelList = new List<MaintenanceLabourViewModel>();

            var response = new ResponseModel<MaintenanceLabourViewModel>();
            try
            {
                DataSet ds = ExcelHelper.GetDataFromExcel(s);
                DataTable excelRows = ds.Tables[0];

                #region ColumnControl
                if (excelRows.Columns.Count < 4)
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.Labour_Warning_ColumnProblem;
                }
                #endregion

                foreach (DataRow excelRow in excelRows.Rows)
                {
                    MaintenanceLabourViewModel row = new MaintenanceLabourViewModel();
                    #region MaintId
                    int maint;
                    if (!int.TryParse(excelRow[0].GetValue<string>(), out maint))
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.Labour_Warning_MaintIdMustBeNumeric;
                    }
                    else
                    {
                        int maintId = excelRow[0].GetValue<int>();
                        var result = new MaintenancePartData().CheckMaintId(maintId);
                        if (!result)
                        {
                            model.ErrorNo = 1;
                            model.ErrorMessage = MessageResource.Maintenance_Not_Found;
                        }
                        row.MaintenanceId = maintId;
                    }
                    #endregion
                    #region LabourNameAndCode
                    string labourCode = excelRow[1].GetValue<string>();
                    row.LabourCode = labourCode;
                    data.GetLabourByLabourCode(user, row);
                    if (row.LabourId == 0)
                    {
                        model.ErrorNo = 1;
                        model.ErrorMessage = MessageResource.Labour_Warning_CannotFindLabourName;

                    }
                    #endregion
                    #region Quantity
                    decimal isQty;
                    if (!decimal.TryParse(excelRow[2].GetValue<string>(), out isQty))
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.Labour_Warning_QuantityMustBeNumeric;
                    }
                    else
                    {
                        row.Quantity = isQty.GetValue<decimal>();
                    }
                    #endregion
                    #region IsMust
                    int isMst;
                    if (!int.TryParse(excelRow[3].GetValue<string>(), out isMst))
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.Labour_Warning_IsMustMustBeNumeric;
                    }
                    else
                    {
                        if (isMst != 0 && isMst != 1)
                        {
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.Labour_Warning_IsMust1or0;
                        }
                        else
                        {
                            row.IsMust = isMst.GetValue<int>() != 0;
                        }
                    }
                    excelList.Add(row);
                    #endregion
                    #region Is Active
                    int isAct;
                    row.IsActiveString = excelRow[4].GetValue<string>();
                    if (!int.TryParse(excelRow[4].GetValue<string>(), out isAct))
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

        public MemoryStream SetExcelReport(List<MaintenanceLabourViewModel> modelList, string errMsg)
        {
            string errorMessage = CommonValues.RowStart + errMsg + CommonValues.RowEnd;

            string preTable = CommonValues.TableStart + CommonValues.RowStart +

                               CommonValues.ColumnStart +
                               MessageResource.Maintenance_Display_ID +
                               CommonValues.ColumnEnd +
                               CommonValues.ColumnStart +
                               MessageResource.CampaignLabour_Display_LabourCode +
                               CommonValues.ColumnEnd +
                               CommonValues.ColumnStart +
                               MessageResource.AppointDetailsParts_Display_Quantity +
                               CommonValues.ColumnEnd +
                               CommonValues.ColumnStart +
                               MessageResource.MaintenancePart_Display_IsMust +
                               CommonValues.ColumnEnd +
                               CommonValues.ColumnStart +
                               MessageResource.CampaignSummaryReport_Display_IsActive +
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
                                    CommonValues.ColumnStart + "{4}" + CommonValues.ColumnEnd,
                                  model.MaintenanceId, model.LabourCode, model.Quantity.GetValue<int>(), model.IsMust.GetValue<int>(), model.IsActiveString);
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

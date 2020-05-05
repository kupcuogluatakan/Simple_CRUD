using System.Collections.Generic;
using ODMSData;
using ODMSModel.MaintenancePart;
using ODMSModel.Maintenance;
using System.IO;
using ODMSCommon;
using ODMSCommon.Resources;
using System.Text;
using ODMSModel.SparePart;
using System.Linq;
using System.Threading;
using System.Globalization;
using ODMSCommon.Security;
using System.Data;
using ODMSModel.DownloadFileActionResult;

namespace ODMSBusiness
{
    public class MaintenancePartBL : BaseBusiness, IDownloadFile<MaintenancePartViewModel>
    {
        private readonly MaintenancePartData data = new MaintenancePartData();

        public ResponseModel<MaintenancePartListModel> GetMaintenancePartList(UserInfo user, MaintenancePartListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<MaintenancePartListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.GetMaintenancePartList(user, filter, out totalCnt);
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

        public ResponseModel<MaintenancePartListModel> GetMaintenancePartListForExcel(UserInfo user, MaintenancePartListModel filter, MaintenanceListModel maintModel, out int totalCnt)
        {
            var response = new ResponseModel<MaintenancePartListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.GetMaintenancePartListForExcel(user, filter, maintModel, out totalCnt);
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

        public void GetMaintenancePart(UserInfo user, MaintenancePartViewModel filter)
        {
            MaintenancePartData data = new MaintenancePartData();

            data.GetMaintenancePart(user, filter);

        }

        public ResponseModel<MaintenancePartViewModel> DMLMaintenancePart(UserInfo user, MaintenancePartViewModel model)
        {
            var response = new ResponseModel<MaintenancePartViewModel>();
            try
            {
                data.DMLMaintenancePart(user, model);
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

        public ResponseModel<MaintenancePartViewModel> ParseExcel(UserInfo user, MaintenancePartViewModel filter, Stream s)
        {
            List<MaintenancePartViewModel> excelList = new List<MaintenancePartViewModel>();
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            var response = new ResponseModel<MaintenancePartViewModel>();
            try
            {
                DataSet ds = ExcelHelper.GetDataFromExcel(s);
                DataTable excelRows = ds.Tables[0];

                #region columnControl
                if (excelRows.Columns.Count < 7)
                {
                    filter.ErrorNo = 1;
                    filter.ErrorMessage = MessageResource.Labour_Warning_ColumnProblem;
                }
                #endregion

                foreach (DataRow excelRow in excelRows.Rows)
                {
                    MaintenancePartViewModel row = new MaintenancePartViewModel();
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
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.Maintenance_Not_Found;
                        }
                        row.MaintId = maintId;
                    }
                    #endregion
                    #region PartCode
                    string partCode = excelRow[1].GetValue<string>();
                    if (row.MaintId == 0 && string.IsNullOrEmpty(partCode))
                        continue;
                    SparePartIndexViewModel Rmodel = new SparePartIndexViewModel() { PartCode = partCode };
                    new SparePartBL().GetSparePart(user, Rmodel);
                    if (Rmodel.PartId == 0)
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.Exception_PartNotFound;
                    }
                    else
                    {
                        row.PartId = Rmodel.PartId;
                    }
                    row.PartName = partCode;

                    #endregion
                    #region IsAlternateAllow
                    int isAlternate;
                    if (!int.TryParse(excelRow[2].GetValue<string>(), out isAlternate))
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.Labour_Warning_IsAlternateMustBeNumeric;
                    }
                    else
                    {
                        if (isAlternate != 0 && isAlternate != 1)
                        {
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.Labour_Warning_IsAlternateMust1or0;
                        }
                        else
                        {
                            row.IsAlternateAllow = isAlternate.GetValue<int>() != 0;
                        }
                    }
                    #endregion
                    #region IsDifBrandAllow
                    int isDif;
                    if (!int.TryParse(excelRow[3].GetValue<string>(), out isDif))
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.Labour_Warning_IsDifMustBeNumeric;
                    }
                    else
                    {
                        if (isDif != 0 && isDif != 1)
                        {
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.Labour_Warning_IsDifMust1or0;
                        }
                        else
                        {
                            row.IsDifBrandAllow = isDif.GetValue<int>() != 0;
                        }
                    }
                    #endregion
                    #region IsMust
                    int isMst;
                    if (!int.TryParse(excelRow[4].GetValue<string>(), out isMst))
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
                    #endregion
                    #region Quantity
                    decimal isQty;
                    if (!decimal.TryParse(excelRow[5].GetValue<string>(), out isQty))
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.Labour_Warning_QuantityMustBeNumeric;
                    }
                    else
                    {
                        row.Quantity = excelRow[5].GetValue<decimal>();
                    }
                    #endregion
                    #region Is Active
                    row.IsActiveString = excelRow[6].GetValue<string>();
                    int isAct;
                    if (!int.TryParse(excelRow[6].GetValue<string>(), out isAct))
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
                    excelList.Add(row);
                }
                var errorCount = (from r in excelList.AsEnumerable()
                                  where r.ErrorNo == 1
                                  select r);
                if (errorCount.Any())
                {
                    filter.ErrorNo = 1;
                    filter.ErrorMessage = MessageResource.Labour_Warning_FoundError;
                }
                if (excelRows.Rows.Count == 0)
                {
                    filter.ErrorNo = 1;
                    filter.ErrorMessage = MessageResource.DealerStartupInventoryLevel_Warning_EmptyExcel;
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

        public MemoryStream SetExcelReport(List<MaintenancePartViewModel> modelList, string errMsg)
        {
            string errorMessage = CommonValues.RowStart + errMsg + CommonValues.RowEnd;

            string preTable = CommonValues.TableStart + CommonValues.RowStart +

                               CommonValues.ColumnStart +
                               MessageResource.Maintenance_Display_ID +
                               CommonValues.ColumnEnd +
                               CommonValues.ColumnStart +
                               MessageResource.SparePart_Display_PartCodeName +
                               CommonValues.ColumnEnd +
                               CommonValues.ColumnStart +
                               MessageResource.MaintenancePart_Display_IsAlternAllow +
                               CommonValues.ColumnEnd +
                               CommonValues.ColumnStart +
                               MessageResource.MaintenancePart_Display_IsDifBrandAllow +
                               CommonValues.ColumnEnd +
                               CommonValues.ColumnStart +
                               MessageResource.MaintenancePart_Display_IsMust +
                               CommonValues.ColumnEnd +
                               CommonValues.ColumnStart +
                               MessageResource.MaintenancePart_Display_Quantity +
                               CommonValues.ColumnEnd +
                                 CommonValues.ColumnStart +
                               MessageResource.Global_Display_IsActive +
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
                                    model.MaintId, model.PartName, model.IsAlternateAllow.GetValue<int>(),
                                    model.IsDifBrandAllow.GetValue<int>(), model.IsMust.GetValue<int>(), model.Quantity.GetValue<int>(),
                                    model.IsActiveString);
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

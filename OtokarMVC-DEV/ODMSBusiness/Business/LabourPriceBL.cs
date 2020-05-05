using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSModel.LabourPrice;
using System.IO;
using ODMSCommon;
using ODMSCommon.Resources;
using System.Linq;
using System;
using System.Text;
using System.Globalization;
using System.Threading;
using ODMSCommon.Security;
using System.Data;
using ODMSModel.DownloadFileActionResult;

namespace ODMSBusiness
{
    public class LabourPriceBL : BaseBusiness, IDownloadFile<LabourPriceViewModel>
    {
        private readonly LabourPriceData data = new LabourPriceData();
        private readonly VehicleModelData datavehicle = new VehicleModelData();
        private readonly DealerData dataDealer = new DealerData();
        private readonly CommonData dataCommon = new CommonData();


        public ResponseModel<LabourPriceListModel> ListLabourPrices(UserInfo user, LabourPriceListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<LabourPriceListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListLabourPrices(user, filter, out totalCnt);
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

        public ResponseModel<SelectListItem> ListVehicleModels(UserInfo user)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = datavehicle.ListVehicleModelAsSelectList(user);
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

        public ResponseModel<SelectListItem> ListDealerClasses()
        {
            return DealerClassBL.ListDealerClassesAsSelectListItem();
        }

        public ResponseModel<SelectListItem> ListDealerRegions()
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = dataDealer.ListDealerRegionsAsSelectListItem();
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

        public ResponseModel<SelectListItem> ListLabourTypes(UserInfo user)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListLabourPriceTypesAsSelectListItems(user);
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

        public ResponseModel<SelectListItem> ListCurrencyCodes(UserInfo user)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = dataCommon.GetCurrencyListForComboBox(user);
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

        public ResponseModel<LabourPriceViewModel> DMLLabourPrice(UserInfo user, LabourPriceViewModel model)
        {
            var response = new ResponseModel<LabourPriceViewModel>();
            try
            {
                data.DMLLabourPrice(user, model);
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

        public ResponseModel<LabourPriceViewModel> GetLabourPrice(UserInfo user, int id)
        {
            var response = new ResponseModel<LabourPriceViewModel>();
            try
            {
                response.Model = data.GetLabourPrice(user, id);
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


        public ResponseModel<LabourPriceViewModel> ParseExcel(UserInfo user, LabourPriceViewModel model, Stream s)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            List<LabourPriceViewModel> returnModel = new List<LabourPriceViewModel>();

            var response = new ResponseModel<LabourPriceViewModel>();
            try
            {
                DataSet ds = ExcelHelper.GetDataFromExcel(s);
                DataTable excelRows = ds.Tables[0];

                #region ColumnControl
                if (excelRows.Columns.Count < 11)
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.Labour_Warning_ColumnProblem;
                }
                #endregion

                foreach (DataRow excelRow in excelRows.Rows)
                {
                    LabourPriceViewModel row = new LabourPriceViewModel();
                    var lpb = new LabourPriceBL();
                    #region LabourPriceId
                    row._LabourPriceId = excelRow[0].GetValue<string>();
                    if (!string.IsNullOrEmpty(row._LabourPriceId))
                    {
                        int labourPriceId1;
                        int labourPriceId2;
                        if (!row._LabourPriceId.Contains('$'))
                        {
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.Labour_Price_Id_Format_Error;
                        }
                        else
                        {
                            string[] splitStr = row._LabourPriceId.Split('$');
                            if (!int.TryParse(splitStr[0], out labourPriceId1) || !int.TryParse(splitStr[1], out labourPriceId2))
                            {
                                row.ErrorNo = 1;
                                row.ErrorMessage = MessageResource.Labour_Warning_LabourPriceIdMustBeNumeric;
                            }
                            else if (string.IsNullOrEmpty(GetLabourPrice(user, labourPriceId1).Model.LabourPriceType))
                            {
                                row.ErrorNo = 1;
                                row.ErrorMessage = MessageResource.Cannot_Find_Labour_Price_Id;
                            }
                            else
                            {
                                row.HasTSLabourPriceId = labourPriceId1;
                                row.HasNoTSLabourPriceId = labourPriceId2;
                            }
                        }
                    }
                    #endregion
                    #region ModelCode
                    row.ModelCode = excelRow[1].GetValue<string>();
                    if (string.IsNullOrEmpty(row.ModelCode))
                    {
                        // exceldeki ilgili satırın yanına hata yazılır.
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.Labour_Warning_EmptyVehicleModel;
                    }
                    else
                    {

                        List<SelectListItem> VehicleModelList = lpb.ListVehicleModels(user).Data;
                        var typeControl = (from e in VehicleModelList.AsEnumerable()
                                           where e.Text == row.ModelCode
                                           select e);
                        if (!typeControl.Any())
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.LabourDuration_Warning_CannotFindVehicleModel;
                        }
                        else
                        {
                            row.ModelName = typeControl.ElementAt(0).Text.GetValue<string>();
                            row.ModelCode = typeControl.ElementAt(0).Value.GetValue<string>();
                        }
                    }
                    #endregion
                    #region DealerClass
                    row.DealerClassName = excelRow[2].GetValue<string>();
                    if (string.IsNullOrEmpty(row.DealerClassName))
                    {
                        // exceldeki ilgili satırın yanına hata yazılır.
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.Labour_Warning_EmptyVehicleModel;
                    }
                    else
                    {

                        List<SelectListItem> DealerClassList = lpb.ListDealerClasses().Data;
                        var typeControl = (from e in DealerClassList.AsEnumerable()
                                           where e.Text == row.DealerClassName
                                           select e);
                        if (!typeControl.Any())
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.Labour_Warning_CannotFindDealerClass;
                        }
                        else
                        {
                            row.DealerClass = typeControl.ElementAt(0).Value.GetValue<string>();
                        }
                    }
                    #endregion
                    #region DealerRegionName
                    row.DealerRegionName = excelRow[3].GetValue<string>();
                    if (string.IsNullOrEmpty(row.DealerRegionName))
                    {
                        // exceldeki ilgili satırın yanına hata yazılır.
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.Labour_Warning_EmptyDealerRegionName;
                    }
                    else
                    {

                        List<SelectListItem> DealerRegionList = lpb.ListDealerRegions().Data;
                        var typeControl = (from e in DealerRegionList.AsEnumerable()
                                           where e.Text == row.DealerRegionName
                                           select e);
                        if (!typeControl.Any())
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.Labour_Warning_CannotFindDealerRegion;
                        }
                        else
                        {
                            row.DealerRegionName = typeControl.ElementAt(0).Text.GetValue<string>();
                            row.DealerRegionId = typeControl.ElementAt(0).Value.GetValue<int>();
                        }
                    }
                    #endregion
                    #region LabourPriceType
                    int labourPriceTypeId;
                    row.LabourPriceType = excelRow[4].GetValue<string>();
                    if (string.IsNullOrEmpty(row.LabourPriceType))
                    {
                        // exceldeki ilgili satırın yanına hata yazılır.
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.Labour_Warning_EmptyLabourPriceType;
                    }
                    else if (!int.TryParse(row.LabourPriceType, out labourPriceTypeId))
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.PDIControlPartDefinition_Warning_LabourPriceTypeIdMustBeNumeric;
                    }
                    else
                    {

                        List<SelectListItem> LabourPriceTypeList = lpb.ListLabourTypes(user).Data;
                        var typeControl = (from e in LabourPriceTypeList.AsEnumerable()
                                           where e.Value == labourPriceTypeId.GetValue<string>()
                                           select e);
                        if (!typeControl.Any())
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.Labour_Warning_CannotFindLabourPriceType;
                        }
                        else
                        {
                            row.LabourPriceType = typeControl.ElementAt(0).Text.GetValue<string>();
                            row.LabourPriceTypeId = typeControl.ElementAt(0).Value.GetValue<int>();
                        }
                    }
                    #endregion
                    #region IsTsPrice
                    row._HasTSUnitPrice = excelRow[5].GetValue<string>();
                    decimal tsPrice;
                    if (!decimal.TryParse(excelRow[5].GetValue<string>(), out tsPrice))
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.Labour_Warning_IsTsPrice;
                    }
                    else
                    {
                        if (tsPrice < 0)
                        {
                            row.ErrorNo = 1;
                            row.ErrorMessage = string.Format(MessageResource.Validation_GreaterThanZero, MessageResource.LabourPrice_Display_HasTSUnitPrice);
                        }
                        else
                        {
                            row.HasTSUnitPrice = tsPrice;
                        }
                    }
                    #endregion
                    #region NoTsPrice
                    row._HasNoTSUnitPrice = excelRow[6].GetValue<string>();
                    decimal NotsPrice;
                    if (!decimal.TryParse(excelRow[6].GetValue<string>(), out NotsPrice))
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.Labour_Warning_NoTsPrice;
                    }
                    else
                    {
                        if (NotsPrice < 0)
                        {
                            row.ErrorNo = 1;
                            row.ErrorMessage = string.Format(MessageResource.Validation_GreaterThanZero, MessageResource.LabourPrice_Display_HasNoTSUnitPrice);
                        }
                        else
                        {
                            row.HasNoTSUnitPrice = NotsPrice;
                        }
                    }
                    #endregion
                    #region Currency
                    row.CurrencyCode = excelRow[7].GetValue<string>();
                    if (string.IsNullOrEmpty(row.CurrencyCode))
                    {
                        // exceldeki ilgili satırın yanına hata yazılır.
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.Labour_Warning_EmptyCurrencyName;
                    }
                    else
                    {

                        List<SelectListItem> CurrencyNameList = lpb.ListCurrencyCodes(user).Data;
                        var typeControl = (from e in CurrencyNameList.AsEnumerable()
                                           where e.Value == row.CurrencyCode
                                           select e);
                        if (!typeControl.Any())
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.LabourDuration_Warning_CannotFindCurrencyName;
                        }
                        else
                        {
                            row.CurrencyName = typeControl.ElementAt(0).Text.GetValue<string>();
                        }
                    }

                    #endregion
                    #region StartDate
                    row._ValidFromDate = excelRow[8].GetValue<string>();
                    DateTime ValidFromDate;
                    if (string.IsNullOrEmpty(row._ValidFromDate))
                    {
                        // exceldeki ilgili satırın yanına hata yazılır.
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.Labour_Warning_EmptyValidFromDate;
                    }
                    else if (!DateTime.TryParse(row._ValidFromDate, out ValidFromDate))
                    {
                        // exceldeki ilgili satırın yanına hata yazılır.
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.Labour_Warning_FormatErrorValidFromDate;
                    }
                    else
                    {
                        row.ValidFromDate = ValidFromDate;
                    }
                    #endregion
                    #region EndDate
                    row._ValidEndDate = excelRow[9].GetValue<string>();
                    DateTime ValidEndDate;
                    if (string.IsNullOrEmpty(row._ValidEndDate))
                    {
                        // exceldeki ilgili satırın yanına hata yazılır.
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.Labour_Warning_EmptyValidEndDate;
                    }
                    else if (!DateTime.TryParse(row._ValidEndDate, out ValidEndDate))
                    {
                        // exceldeki ilgili satırın yanına hata yazılır.
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.Labour_Warning_FormatErrorValidEndDate;
                    }
                    else
                    {
                        row.ValidEndDate = ValidEndDate;
                    }
                    #endregion
                    #region IsActive
                    row.IsActiveString = excelRow[10].GetValue<string>();
                    int isAct;
                    if (!int.TryParse(excelRow[10].GetValue<string>(), out isAct))
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
                            row.IsActive = isAct.GetValue<bool>();
                        }
                    }
                    #endregion

                    returnModel.Add(row);

                    //KONTROL

                    var bus = new LabourPriceBL();
                    bool isValid = true;
                    var listModel = new LabourPriceListModel();
                    var totalCnt = 0;
                    listModel.ModelKod = row.ModelCode;
                    listModel.LabourPriceTypeId = row.LabourPriceTypeId;
                    listModel.DealerRegionId = row.DealerRegionId;
                    listModel.CurrencyCode = row.CurrencyCode;
                    listModel.DealerClass = row.DealerClass;
                    listModel.ValidEndDate = row.ValidEndDate;
                    listModel.ValidFromDate = row.ValidFromDate;
                    var returnValue = bus.ListLabourPrices(UserManager.UserInfo, listModel, out totalCnt).Data.Where(c => c.LabourPriceId != string.Format("{0}${1}", row.HasTSLabourPriceId, row.HasNoTSLabourPriceId));
                    if (returnValue.Any())
                    {
                        var control = (from r in returnValue.AsEnumerable()
                                       where r.ValidFromDate == row.ValidFromDate
                                             && r.HasTsPaper == row.HasTsPaper
                                             &&
                                             (r.HasNoTSLabourPriceId != row.HasNoTSLabourPriceId &&
                                              r.HasTSLabourPriceId != row.HasTSLabourPriceId)
                                       select r);

                        var control2 = (from r in returnValue.AsEnumerable()
                                        where (((r.ValidFromDate <= row.ValidFromDate) &&
                                                (r.ValidEndDate <= row.ValidEndDate))
                                               ||
                                               ((r.ValidFromDate <= row.ValidFromDate) &&
                                                (r.ValidEndDate >= row.ValidEndDate))
                                               ||
                                               ((r.ValidFromDate >= row.ValidFromDate) &&
                                                (r.ValidEndDate <= row.ValidEndDate))
                                               ||
                                               ((r.ValidFromDate >= row.ValidFromDate) &&
                                                (r.ValidEndDate >= row.ValidEndDate)))
                                              && r.IsActive
                                              && (r.HasNoTSLabourPriceId != row.HasNoTSLabourPriceId &&
                                                  r.HasTSLabourPriceId != row.HasTSLabourPriceId)
                                        select r);
                        if (control.Any())
                        {
                            model.ErrorNo = 3;
                            model.ErrorMessage = MessageResource.LabourPrice_Warning_SameValuesExists;
                        }
                        if (control2.Any())
                        {
                            model.ErrorNo = 4;
                            model.ErrorMessage = MessageResource.LabourPrice_Warning_SamePeriodExists;
                        }
                    }
                    //
                }
                var errorCount = (from r in returnModel.AsEnumerable()
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

                response.Data = returnModel;
                response.Total = returnModel.Count;
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

        public MemoryStream SetExcelReport(List<LabourPriceViewModel> modelList, string errMsg)
        {
            string errorMessage = CommonValues.RowStart + errMsg + CommonValues.RowEnd;

            string preTable = CommonValues.TableStart + CommonValues.RowStart
              + CommonValues.ColumnStart + MessageResource.VehicleModel_Display_LabourPriceId + CommonValues.ColumnEnd
              + CommonValues.ColumnStart + MessageResource.VehicleModel_Display_Code + CommonValues.ColumnEnd
              + CommonValues.ColumnStart + MessageResource.Dealer_Display_DealerClass + CommonValues.ColumnEnd
              + CommonValues.ColumnStart + MessageResource.DealerRegion_Display_Name + CommonValues.ColumnEnd
              + CommonValues.ColumnStart + MessageResource.LabourPrice_Display_LabourPriceType + CommonValues.ColumnEnd
              + CommonValues.ColumnStart + MessageResource.LabourPrice_Display_HasTSUnitPrice + CommonValues.ColumnEnd
              + CommonValues.ColumnStart + MessageResource.LabourPrice_Display_HasNoTSUnitPrice + CommonValues.ColumnEnd
              + CommonValues.ColumnStart + MessageResource.Dealer_Display_CurrencyCode + CommonValues.ColumnEnd
              + CommonValues.ColumnStart + MessageResource.LabourPrice_Display_ValidFromDate + CommonValues.ColumnEnd
              + CommonValues.ColumnStart + MessageResource.LabourPrice_Display_ValidEndDate + CommonValues.ColumnEnd
              + CommonValues.ColumnStart + MessageResource.Global_Display_IsActive + CommonValues.ColumnEnd;

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
                                     CommonValues.ColumnStart + "{8}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{9}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{10}" + CommonValues.ColumnEnd,
                                    model._LabourPriceId, model.ModelCode, model.DealerClassName,
                                    model.DealerRegionName, model.LabourPriceType, model._HasTSUnitPrice, model._HasNoTSUnitPrice,
                                    model.CurrencyCode, model._ValidFromDate,
                                    model._ValidEndDate, model.IsActiveString);
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

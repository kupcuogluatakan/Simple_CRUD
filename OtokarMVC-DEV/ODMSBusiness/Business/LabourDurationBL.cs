using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSData;
using ODMSModel.Labour;
using ODMSModel.LabourDuration;
using ODMSModel.VehicleCode;
using ODMSModel.VehicleModel;
using ODMSModel.VehicleType;
using ODMSCommon.Security;
using System.Data;
using ODMSModel.DownloadFileActionResult;

namespace ODMSBusiness
{
    public class LabourDurationBL : BaseBusiness, IDownloadFile<LabourDurationDetailModel>
    {
        private readonly LabourDurationData data = new LabourDurationData();

        public ResponseModel<LabourDurationListModel> ListLabourDurations(UserInfo user, LabourDurationListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<LabourDurationListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListLabourDurations(user, filter, out totalCnt);
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

        public ResponseModel<LabourDurationDetailModel> GetLabourDuration(UserInfo user, LabourDurationDetailModel filter)
        {
            var response = new ResponseModel<LabourDurationDetailModel>();
            try
            {
                response.Model = data.GetLabourDuration(user, filter);
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

        public ResponseModel<LabourDurationDetailModel> DMLLabourDuration(UserInfo user, LabourDurationDetailModel model)
        {
            var response = new ResponseModel<LabourDurationDetailModel>();
            try
            {
                data.DMLLabourDuration(user, model);
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

        public ResponseModel<LabourDurationIndexModel> GetLabourDurationIndexModel(UserInfo user)
        {
            var response = new ResponseModel<LabourDurationIndexModel>();
            try
            {
                response.Model = data.GetLabourDurationIndexModel(user);
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

        public ResponseModel<SelectListItem> GetLabourList(UserInfo user)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.GetLabourList(user);
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

        public ResponseModel<SelectListItem> GetVehicleModelList(UserInfo user)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.GetVehicleModelList(user);
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

        public static ResponseModel<SelectListItem> GetVehicleTypeList(UserInfo user, string vehicleModelId, string labourId)
        {
            var response = new ResponseModel<SelectListItem>();
            var data = new LabourDurationData();
            try
            {
                response.Data = data.GetVehicleTypeList(user, vehicleModelId, labourId);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;

                CommonUtility.MakeFirstItemSelectedIfLengthIsOne(response.Data);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public static ResponseModel<SelectListItem> GetVehicleTypeEngineTypeList(UserInfo user, string vehicleModelId, string labourId)
        {
            var response = new ResponseModel<SelectListItem>();
            var data = new LabourDurationData();
            try
            {
                response.Data = data.GetVehicleTypeEngineTypeList(user, vehicleModelId, labourId);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;

                CommonUtility.MakeFirstItemSelectedIfLengthIsOne(response.Data);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public static ResponseModel<SelectListItem> GetVehicleTypeEngineTypeListSearch(UserInfo user, string labourId)
        {
            var response = new ResponseModel<SelectListItem>();
            var data = new LabourDurationData();
            try
            {
                response.Data = data.GetVehicleTypeEngineTypeListSearch(user, labourId);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;

                CommonUtility.MakeFirstItemSelectedIfLengthIsOne(response.Data);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<LabourDurationDetailModel> ParseExcel(UserInfo user, LabourDurationDetailModel model, Stream s)
        {
            List<LabourDurationDetailModel> excelList = new List<LabourDurationDetailModel>();

            var response = new ResponseModel<LabourDurationDetailModel>();
            try
            {
                DataSet ds = ExcelHelper.GetDataFromExcel(s);
                DataTable excelRows = ds.Tables[0];

                int totalCount = 0;
                LabourBL labourBo = new LabourBL();
                VehicleCodeBL vehicleCodeBo = new VehicleCodeBL();
                VehicleModelBL vehicleModelBo = new VehicleModelBL();
                VehicleTypeBL vehicleTypeBo = new VehicleTypeBL();

                #region ColumnControl
                if (excelRows.Columns.Count < 6)
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.Labour_Warning_ColumnProblem;
                }
                #endregion

                foreach (DataRow excelRow in excelRows.Rows)
                {
                        LabourDurationDetailModel row = new LabourDurationDetailModel { };

                        #region Labour Code

                        string labourcode = excelRow[0].GetValue<string>();
                        if (string.IsNullOrEmpty(labourcode))
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.Labour_Warning_EmptyLabourCode;
                        }
                        else
                        {
                            LabourViewModel labourModel = new LabourViewModel();
                            labourModel.LabourCode = labourcode;
                            labourBo.GetLabour(user,labourModel);

                            if (labourModel.LabourId == 0)
                            {
                                // exceldeki ilgili satırın yanına hata yazılır.
                                row.ErrorNo = 1;
                                row.ErrorMessage = MessageResource.Labour_Warning_CannotFindLabourCode;
                            }
                            else
                            {
                                row.LabourId = labourModel.LabourId;
                            }
                            row.LabourCode = labourcode;
                        }

                        #endregion

                        #region Vehicle Model

                        string vehicleModel = excelRow[1].GetValue<string>();
                        row.VehicleModelName = vehicleModel;
                        if (string.IsNullOrEmpty(vehicleModel))
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.LabourDuration_Warning_EmptyVehicleModel;
                        }
                        else
                        {
                            VehicleModelListModel vehicleListModel = new VehicleModelListModel();
                            vehicleListModel.VehicleModelSSID = vehicleModel;
                            List<VehicleModelListModel> modelList = vehicleModelBo.GetVehicleModelList(user,vehicleListModel, out totalCount).Data;

                            if (totalCount == 0)
                            {
                                // exceldeki ilgili satırın yanına hata yazılır.
                                row.ErrorNo = 1;
                                row.ErrorMessage = MessageResource.LabourDuration_Warning_CannotFindVehicleModel;
                            }
                            else
                            {
                                row.VehicleModelId = modelList.ElementAt(0).VehicleModelKod;
                                row.VehicleTypeName= modelList.ElementAt(0).VehicleModelKod;
                        }
                        }

                        #endregion

                        #region Vehicle Type

                        string vehicleType = excelRow[2].GetValue<string>();
                        row.VehicleTypeName = vehicleType;
                        if (string.IsNullOrEmpty(vehicleType))
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.LabourDuration_Warning_EmptyVehicleType;
                        }
                        else
                        {
                            VehicleTypeListModel vehicleTypeListModel = new VehicleTypeListModel();
                            vehicleTypeListModel.TypeSSID = vehicleType;
                            List<VehicleTypeListModel> vehicleTypeList = vehicleTypeBo.GetVehicleTypeList(user,vehicleTypeListModel, out totalCount).Data;
                            if (totalCount == 0)
                            {
                                // exceldeki ilgili satırın yanına hata yazılır.
                                row.ErrorNo = 1;
                                row.ErrorMessage = string.Format(MessageResource.LabourDuration_Warning_CannotFindVehicleType, vehicleType);
                            }
                            else
                            {
                                row.VehicleTypeId = vehicleTypeList.ElementAt(0).TypeId.GetValue<int>();
                            }
                        }

                        #endregion

                        #region Engine Type

                        string engineType = excelRow[3].GetValue<string>();
                        row.EngineType = engineType;
                        if (string.IsNullOrEmpty(engineType))
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.LabourDuration_Warning_EmptyEngineType;
                        }
                        else
                        {
                            VehicleCodeListModel vehicleCodeListModel = new VehicleCodeListModel();
                            vehicleCodeListModel.EngineType = engineType;
                            vehicleCodeListModel.VehicleTypeId = row.VehicleTypeId.GetValue<int>();
                            vehicleCodeBo.GetVehicleCodeList(user,vehicleCodeListModel, out totalCount);

                            if (totalCount == 0)
                            {
                                // exceldeki ilgili satırın yanına hata yazılır.
                                row.ErrorNo = 1;
                                row.ErrorMessage = string.Format(MessageResource.LabourDuration_Warning_CannotFindEngineType, row.VehicleTypeName);
                            }
                        }

                        #endregion

                        #region Duration

                        int duration;
                        if (!int.TryParse(excelRow[4].GetValue<string>(), out duration))
                        {
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.LabourDuration_Warning_DurationMustBeNumeric;
                        }
                        else
                        {
                            if (duration > 9999)
                            {
                                row.ErrorNo = 1;
                                row.ErrorMessage = MessageResource.LabourDuration_Warning_DurationLessThan9999;
                            }
                            else
                            {
                                row.Duration = duration.GetValue<int>();
                            }
                        }

                        #endregion

                        #region Is Active

                        int isAct;
                        if (!int.TryParse(excelRow[5].GetValue<string>(), out isAct))
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
                                       && r.VehicleTypeName == row.VehicleTypeName
                                       && r.VehicleModelName == row.VehicleModelName
                                       && r.EngineType == row.EngineType
                                       select r);
                        if (existed.Any())
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.Labour_Warning_DuplicateValue;
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

        public MemoryStream SetExcelReport(List<LabourDurationDetailModel> modelList, string errMsg)
        {
            string errorMessage = CommonValues.RowStart + errMsg + CommonValues.RowEnd;
            string preTable = CommonValues.TableStart + CommonValues.RowStart
                + CommonValues.ColumnStart + MessageResource.Labour_Display_Code
                + CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.LabourDuration_Display_VehicleModelName + "_" + MessageResource.VehicleModel_Display_SSID
                + CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.LabourDuration_Display_VehicleTypeName + "_" + MessageResource.VehicleType_Display_SSID
                + CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.Vehicle_Display_EngineType
                + CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.LabourDuration_Display_Duration
                + CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.Global_Display_IsActive + CommonValues.ColumnEnd;
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
                                    CommonValues.ColumnStart + "{5}" + CommonValues.ColumnEnd,
                                    model.LabourCode, model.VehicleModelName,
                                    model.VehicleTypeName, model.EngineType, model.Duration,
                                    model.IsActive);
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

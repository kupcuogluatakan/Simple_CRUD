using System;
using System.Collections.Generic;
using System.Data;
using System.Transactions;
using System.Web.Mvc;
using ODMSData;
using ODMSModel.ListModel;
using ODMSModel.ServiceCallSchedule;
using ODMSModel.Vehicle;
using ODMSData.Utility;
using System.IO;
using ODMSCommon;
using ODMSCommon.Resources;
using System.Text;
using ODMSModel;
using ODMSCommon.Security;
using ODMSModel.DownloadFileActionResult;

namespace ODMSBusiness
{
    public class VehicleBL : BaseBusiness, IDownloadFile<VehicleIndexViewModel>
    {
        private readonly VehicleData data = new VehicleData();

        public ResponseModel<VehicleListModel> ListVehicles(UserInfo user, VehicleListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<VehicleListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListVehicles(user, filter, out totalCnt);
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

        /// <summary>
        /// Araç şasi,model yılı,model kodu ve model adı listesini verir.
        /// </summary>
        [BusinessCache(VersionControlClass = typeof(VehicleData), VersionControlMethod = "ListVehicleLastVersion")]
        public ResponseModel<VehicleListModel> ListVehicles()
        {
            var response = new ResponseModel<VehicleListModel>();
            try
            {
                response.Data = data.ListVehicles();
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

        /// <summary>
        /// Aracın bakım paketi iş emirlerini verir.
        /// </summary>
        /// <param name="vinNo">Şasi No</param>
        /// <param name="languageCode">Dil Kodu</param>
        public ResponseModel<VehicleWorkOrderMaintModelcs> ListVehicleWorkOrderMaint(string vinNo, string languageCode)
        {
            var response = new ResponseModel<VehicleWorkOrderMaintModelcs>();
            try
            {
                response.Data = data.ListVehicleWorkOrderMaint(vinNo, languageCode);
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

        public ResponseModel<VehicleHistoryListModel> ListVehicleHistory(UserInfo user,VehicleHistoryListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<VehicleHistoryListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListVehicleHistory(user,filter, out totalCnt);
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


        public ResponseModel<VehicleHistoryDetailListModel> ListVehicleHistoryAllDetails(UserInfo user, VehicleHistoryDetailListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<VehicleHistoryDetailListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListVehicleHistoryAllDetails(user, filter, out totalCnt);
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

        public ResponseModel<VehicleHistoryDetailListModel> ListVehicleHistoryDetails(UserInfo user, VehicleHistoryDetailListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<VehicleHistoryDetailListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListVehicleHistoryDetails(user, filter, out totalCnt);
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

        public ResponseModel<VehicleHistoryListWithDetailsModel> ListVehicleHistoryListWithDetails(UserInfo user, VehicleHistoryListWithDetailsModel filter, out int totalCnt)
        {
            var response = new ResponseModel<VehicleHistoryListWithDetailsModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListVehicleHistoryListWithDetails(user, filter, out totalCnt);
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



        public ResponseModel<VehicleIndexViewModel> GetVehicle(UserInfo user, VehicleIndexViewModel filter)
        {
            var response = new ResponseModel<VehicleIndexViewModel>();
            try
            {
                response.Model = data.GetVehicle(user, filter);
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

        public ResponseModel<VehicleIndexViewModel> CheckVehicle(UserInfo user, string vinNoList)
        {
            var response = new ResponseModel<VehicleIndexViewModel>();
            try
            {
                response.Data = data.CheckVehicle(user, vinNoList);
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

        public ResponseModel<VehicleIndexViewModel> DMLVehicle(UserInfo user, VehicleIndexViewModel model)
        {
            var response = new ResponseModel<VehicleIndexViewModel>();
            try
            {
                data.DMLVehicle(user, model);
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

        public static ResponseModel<SelectListItem> ListVehicleCodeAsSelectListItem(UserInfo user, string vehicleType)
        {
            var data = new VehicleData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListVehicleCodeAsSelectListItem(user, vehicleType);
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

        public static ResponseModel<SelectListItem> ListVehicleEngineTypesAsSelectListItem(UserInfo user, int? typeId)
        {
            var data = new VehicleData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListVehicleEngineTypesAsSelectListItem(user, typeId);
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

        public ServiceCallLogModel XMLtoDBVehicle(List<VehicleXMLModel> filter)
        {
            var model = new ServiceCallLogModel();
            try
            {
                model = data.XMLtoDBVehicle(filter);
            }
            catch (Exception ex)
            {
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
            }
            return model;
        }

        public ResponseModel<ServiceCallLogModel> XMLtoDBVehicleCustomer(List<VehicleCustomerXMLModel> filter)
        {
            var response = new ResponseModel<ServiceCallLogModel>();
            try
            {
                response.Model = data.XMLtoDBVehicleCustomer(filter);

                if (response.Model.ErrorModel == null)
                    response.Model.ErrorModel = new List<ServiceCallScheduleErrorListModel>();
                //hata yok ise issucce true var ise false yapılmalı.
                response.IsSuccess = (response.Model.ErrorModel == null || response.Model.ErrorModel.Count == 0);

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

        public ResponseModel<ModelBase> UpdateNotesForVehicle(UserInfo user, List<VehicleIndexViewModel> model)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = data.UpdateNotesForVehicle(user, model);
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

        public ResponseModel<VehicleIndexViewModel> GetVin(UserInfo user, VehicleIndexViewModel filter)
        {
            var response = new ResponseModel<VehicleIndexViewModel>();
            try
            {
                response.Model = data.GetVehicleByVinNo(user, filter);
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

        public ResponseModel<VehicleContactInfoModel> GetVehicleContactInfo(int id)
        {
            var response = new ResponseModel<VehicleContactInfoModel>();
            try
            {
                response.Model = data.GetVehicleContactInfo(vehicleId: id);
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

        public ResponseModel<VehicleContactInfoModel> UpdateVehicleContactInfo(UserInfo user, VehicleContactInfoModel model)
        {
            var response = new ResponseModel<VehicleContactInfoModel>();
            try
            {
                data.UpdateVehicleContactInfo(user, model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorDesc);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<DataSet> ListVehicleHistoryForService(string vinNo)
        {
            var response = new ResponseModel<DataSet>();
            try
            {
                response.Model = data.ListVehicleHistoryForService(vinNo);
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

        public ResponseModel<VehicleHistoryTotalPriceListModel> ListVehicleHistoryTotalPrice(UserInfo user, VehicleHistoryTotalPriceListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<VehicleHistoryTotalPriceListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListVehicleHistoryTotalPrice(user, filter, out totalCnt);
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

        public ResponseModel<bool> CheckVehicleByVinNoOrPlate(string vinNo, string plate)
        {
            var response = new ResponseModel<bool>();
            try
            {
                response.Model = data.CheckVehicleByVinNoOrPlate(vinNo, plate);
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

        /// <summary>
        ///Garanti süresi biten araçların garanty durumlarını 0 a çeker açıklamasına Garanti süresi bitmiştir yazar.
        /// </summary>
        public ResponseModel<bool> UpdateWarrantyEndStatuses()
        {
            var response = new ResponseModel<bool>();
            try
            {
                data.UpdateWarrantyEndStatuses();
                response.Model = true;
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

        public ResponseModel<int> GetLastVehicleKm(int vehicleId)
        {
            var response = new ResponseModel<int>();
            try
            {
                response.Model = data.GetLastVehicleKm(vehicleId);
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

        public ResponseModel<VehicleIndexViewModel> ParseExcel(UserInfo user, VehicleIndexViewModel model, Stream s)
        {
            List<VehicleIndexViewModel> excelList = new List<VehicleIndexViewModel>();

            var response = new ResponseModel<VehicleIndexViewModel>();
            try
            {
                DataSet ds = ExcelHelper.GetDataFromExcel(s);
                DataTable excelRows = ds.Tables[0];

                if (excelRows.Columns.Count != 1)
                {
                    model.ErrorMessage = MessageResource.StockCard_Warning_ColumnError;
                    model.ErrorNo = 1;
                }
                else
                {
                    foreach (DataRow excelRow in excelRows.Rows)
                    {
                        VehicleIndexViewModel row = new VehicleIndexViewModel();
                        string vinCode = excelRow[0].GetValue<string>();
                        row.VinNo = vinCode;
                        // parça kontrol ediliyor.

                        VehicleIndexViewModel spModel = new VehicleIndexViewModel { VinNo = vinCode };
                        GetVin(user, spModel);

                        if (spModel.VehicleId == 0)
                        {
                            row.ErrorMessage = MessageResource.VinNo_Not_Found;
                            row.ErrorNo = 1;
                            model.ErrorNo = 1;
                        }
                        excelList.Add(row);
                    }
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

        public ResponseModel<VehicleIndexViewModel> ParseExcel_UpdateNotesForVehicle(UserInfo user, VehicleIndexViewModel model, Stream s)
        {
            List<VehicleIndexViewModel> excelList = new List<VehicleIndexViewModel>();

            var response = new ResponseModel<VehicleIndexViewModel>();
            try
            {
                DataSet ds = ExcelHelper.GetDataFromExcel(s);
                DataTable excelRows = ds.Tables[0];

                if (excelRows.Columns.Count != 3)
                {
                    model.ErrorMessage = MessageResource.Warning_ColumnError_Update_VehicleNote_By_Vin_No;
                    model.ErrorNo = 1;
                }
                else
                {
                    foreach (DataRow excelRow in excelRows.Rows)
                    {
                        VehicleIndexViewModel row = new VehicleIndexViewModel();
                        string vinCode = excelRow[0].GetValue<string>();
                        string specialCon = excelRow[1].GetValue<string>();
                        string warrantyDesc = excelRow[2].GetValue<string>();
                        row.VinNo = vinCode;
                        row.SpecialConditions = specialCon;
                        row.OutOfWarrantyDescription = warrantyDesc;

                        // parça kontrol ediliyor.

                        VehicleIndexViewModel spModel = new VehicleIndexViewModel { VinNo = vinCode };
                        GetVin(user, spModel);

                        if (spModel.VehicleId == 0)
                        {
                            row.ErrorMessage = MessageResource.VinNo_Not_Found;
                            row.ErrorNo = 1;
                            model.ErrorNo = 1;
                        }
                        excelList.Add(row);
                    }
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

        public MemoryStream SetExcelReport(List<VehicleIndexViewModel> modelList, string errMsg)
        {
            string errorMessage = CommonValues.TableStart + errMsg + CommonValues.TableEnd;
            string preTable = CommonValues.TableStart + CommonValues.RowStart + CommonValues.ColumnStart + MessageResource.Vehicle_VinNo_Search + CommonValues.ColumnEnd;
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
                    sb.AppendFormat(CommonValues.RowStart + CommonValues.ColumnStart + "{0}" + CommonValues.ColumnEnd, model.VinNo);
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

        public MemoryStream SetExcelReportByVehicleNotesUpdate(List<VehicleIndexViewModel> modelList, string errMsg)
        {
            string errorMessage = CommonValues.TableStart + errMsg + CommonValues.TableEnd;
            string preTable = CommonValues.TableStart + CommonValues.RowStart + CommonValues.ColumnStart + MessageResource.Vehicle_VinNo_Search + CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.GRADGif_Display_SpecialNotes + CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.GuaranteeReport_CenterNote + CommonValues.ColumnEnd;
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
                    sb.AppendFormat(CommonValues.RowStart + CommonValues.ColumnStart + "{0}" + CommonValues.ColumnEnd, model.VinNo);
                    sb.AppendFormat(CommonValues.ColumnStart + "{0}" + CommonValues.ColumnEnd, model.SpecialConditions);
                    sb.AppendFormat(CommonValues.ColumnStart + "{0}" + CommonValues.ColumnEnd, model.OutOfWarrantyDescription);
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

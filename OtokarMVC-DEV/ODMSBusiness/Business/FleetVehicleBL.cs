using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSData;
using ODMSModel.FleetVehicle;
using ODMSCommon.Security;
using System.Data;
using ODMSModel.DownloadFileActionResult;

namespace ODMSBusiness
{
    public class FleetVehicleBL : BaseBusiness, IDownloadFile<FleetVehicleViewModel>
    {
        private readonly FleetVehicleData data = new FleetVehicleData();

        public ResponseModel<FleetVehicleViewModel> GetFleetVehicle(int fleetVehicleId)
        {
            var response = new ResponseModel<FleetVehicleViewModel>();
            try
            {
                response.Model = data.GetFleetVehicle(fleetVehicleId);
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

        public ResponseModel<FleetVehicleViewModel> DMLFleetVehicle(UserInfo user, FleetVehicleViewModel model)
        {
            var response = new ResponseModel<FleetVehicleViewModel>();
            try
            {
                data.DMLFleetVehicle(user, model);
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

        public ResponseModel<FleetVehicleListModel> ListFleetVehicle(FleetVehicleListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<FleetVehicleListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListFleetVehicle(filter, out totalCnt);
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

        public ResponseModel<FleetVehicleViewModel> DMLFleetVehicleWithList(UserInfo user, FleetVehicleViewModel model, List<FleetVehicleViewModel> filter)
        {
            var response = new ResponseModel<FleetVehicleViewModel>();
            try
            {
                data.DMLFleetVehicleWithList(user, model, filter);
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

        public ResponseModel<bool> IsFleetVehicle(int vehicleId, int customerId)
        {
            var response = new ResponseModel<bool>();
            try
            {
                response.Model = data.IsFleetVehicle(vehicleId, customerId);
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

        public ResponseModel<FleetVehicleViewModel> ParseExcel(UserInfo user, FleetVehicleViewModel model, Stream s)
        {
            var response = new ResponseModel<FleetVehicleViewModel>();
            var listModel = new List<FleetVehicleViewModel>();
            try
            {

                var dal = new FleetVehicleData();

                DataSet ds = ExcelHelper.GetDataFromExcel(s);
                DataTable excelRows = ds.Tables[0];

                StringBuilder sb = new StringBuilder();


                foreach (DataRow excelRow in excelRows.Rows)
                {
                    sb.Append(",'" + excelRow[0] + "'");
                }

                if (sb.Length > 0)
                {
                    var vinList = sb.ToString().Substring(1);

                    listModel.AddRange(vinList.Split(',').Select(item => new FleetVehicleViewModel()
                    {
                        VehicleVinNo = item.Trim('\'')
                    }));

                    dal.GetVehicleCustomerList(listModel, vinList, model.FleetId);

                    foreach (var item in listModel)
                    {
                        //If vehicle exist
                        if (item.VehicleId > 0)
                        {
                            //If customer exist
                            if (item.CustomerId <= 0)
                            {
                                item.ErrorNo = 1;
                                item.ErrorMessage = MessageResource.Error_DB_CustomerNotExist;
                            }
                        }
                        else
                        {
                            item.ErrorNo = 1;
                            item.ErrorMessage = MessageResource.Error_DB_VehicleNotExist;
                        }
                    }
                }
                response.Data = listModel;
                response.Total = listModel.Count;
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

        public MemoryStream SetExcelReport(List<FleetVehicleViewModel> listModels, string errMsg)
        {
            string errorMessage = CommonValues.RowStart + errMsg + CommonValues.RowEnd;
            string name = MessageResource.Vehicle_Display_VinNo;
            string preTable = CommonValues.TableStart + CommonValues.RowStart + CommonValues.ColumnStart + name +
                              CommonValues.ColumnEnd + CommonValues.ColumnStart +
                              MessageResource.CustomerAddress_Display_CustomerName + CommonValues.ColumnEnd;
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
                    sb.AppendFormat(CommonValues.RowStart + CommonValues.ColumnStart + "{0}" + CommonValues.ColumnEnd, model.VehicleVinNo);
                    if (model.ErrorNo > 0)
                    {
                        sb.AppendFormat("<TD bgcolor='#FFCCCC'>{0}</TD>" + CommonValues.RowEnd, model.ErrorMessage);
                    }
                    else
                    {
                        sb.AppendFormat(
                            CommonValues.ColumnStart + "{0}" + CommonValues.ColumnEnd +
                            CommonValues.ColumnStart + CommonValues.ColumnEnd + CommonValues.RowEnd, model.CustomerName);
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

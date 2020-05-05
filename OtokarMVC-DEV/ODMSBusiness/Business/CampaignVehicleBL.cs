using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSData;
using ODMSModel.CampaignVehicle;
using ODMSModel.DownloadFileActionResult;
using ODMSModel.Vehicle;
using ODMSCommon.Security;
using System.Data;

namespace ODMSBusiness
{
    public class CampaignVehicleBL : BaseBusiness, IDownloadFile<CampaignVehicleViewModel>
    {
        private readonly CampaignVehicleData data = new CampaignVehicleData();

        public ResponseModel<CampaignVehicleListModel> ListCampaignVehicles(UserInfo user,CampaignVehicleListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<CampaignVehicleListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListCampaignVehicles(user,filter, out totalCnt);
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

        public ResponseModel<CampaignVehicleListModel> ListCampaignVehiclesMain(UserInfo user, CampaignVehicleListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<CampaignVehicleListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListCampaignVehiclesMain(user, filter, out totalCnt);
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


        public ResponseModel<CampaignVehicleListModel> ListCampaignVehiclesForDealer(UserInfo user, CampaignVehicleListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<CampaignVehicleListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListCampaignVehiclesForDealer(user, filter, out totalCnt);
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

        public ResponseModel<CampaignVehicleViewModel> GetCampaignVehicle(UserInfo user, CampaignVehicleViewModel filter)
        {
            var response = new ResponseModel<CampaignVehicleViewModel>();
            try
            {
                response.Model = data.GetCampaignVehicle(user, filter);
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

        public ResponseModel<CampaignVehicleViewModel> DMLCampaignVehicle(UserInfo user, CampaignVehicleViewModel model)
        {
            var response = new ResponseModel<CampaignVehicleViewModel>();
            try
            {
                data.DMLCampaignVehicle(user, model);
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

        public ResponseModel<CampaignVehicleViewModel> ParseExcel(UserInfo user, CampaignVehicleViewModel model, Stream s)
        {
            VehicleBL vehicleBL = new VehicleBL();
            List<CampaignVehicleViewModel> vehicleList = new List<CampaignVehicleViewModel>();

            var response = new ResponseModel<CampaignVehicleViewModel>();

            try
            {
                DataSet ds = ExcelHelper.GetDataFromExcel(s);
                DataTable excelRows = ds.Tables[0];

                int count = excelRows.Rows.Count;
                foreach (DataRow row in excelRows.Rows)
                {
                    string vinNo = row[0].GetValue<string>();
                    bool isActive = row[1].GetValue<bool>();
                    bool isUtilized = row[2].GetValue<bool>();

                    CampaignVehicleViewModel campaignVehicleModel = new CampaignVehicleViewModel
                    {
                        VinNo = vinNo,
                        CampaignCode = model.CampaignCode,
                        IsActive = isActive,
                        IsUtilized = isUtilized
                    };
                    vehicleList.Add(campaignVehicleModel);
                }

                foreach (CampaignVehicleViewModel viewModel in vehicleList)
                {
                    // check vehicles
                    List<VehicleIndexViewModel> existsVehicleList = vehicleBL.CheckVehicle(user,"'" + viewModel.VinNo.Trim() + "'").Data;
                    if (existsVehicleList.Count == 0)
                    {
                        viewModel.ErrorNo = 1;
                        viewModel.ErrorMessage = MessageResource.CampaignVehicle_Error_CannotFoundVinNo;
                    }
                    else
                    {
                        viewModel.VehicleId = existsVehicleList.ElementAt(0).VehicleId;
                    }
                }

                var errorCount = (from r in vehicleList.AsEnumerable()
                                  where r.ErrorNo > 0
                                  select r);
                if (errorCount.Any())
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.BreakdownDefinition_Warning_WrongData;
                }
                if (count == 0)
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.DealerStartupInventoryLevel_Warning_EmptyExcel;
                }

                response.Data = vehicleList;
                response.Total = vehicleList.Count;
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


        public MemoryStream SetExcelReport(List<CampaignVehicleViewModel> filter, string errMsg)
        {
            string errorMessage = CommonValues.RowStart + errMsg + CommonValues.RowEnd;
            string preTable = CommonValues.TableStart + CommonValues.RowStart +
                                CommonValues.ColumnStart + MessageResource.CampaignVehicle_Display_VinNo + CommonValues.ColumnEnd +
                                CommonValues.ColumnStart + MessageResource.CampaignVehicle_Display_IsActive + CommonValues.ColumnEnd +
                                CommonValues.ColumnStart + MessageResource.CampaignVehicle_Display_IsUtilized + CommonValues.ColumnEnd;
            if (filter != null)
            {
                preTable = preTable + ("<TD width='400px'>Log</TD>");
            }
            const string lastTable = CommonValues.RowEnd + CommonValues.TableEnd;
            MemoryStream ms = new MemoryStream();

            var sw = new StreamWriter(ms, Encoding.UTF8);

            StringBuilder sb = new StringBuilder();
            sb.Append(errorMessage);
            sb.Append(preTable);
            if (filter != null)
            {
                foreach (var model in filter)
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
    }
}

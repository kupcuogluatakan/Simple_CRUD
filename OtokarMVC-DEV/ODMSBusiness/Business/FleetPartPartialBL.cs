using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.DownloadFileActionResult;
using ODMSModel.FleetPartPartial;
using ODMSData;
using ODMSModel.SparePart;
using ODMSCommon.Security;
using System.Data;

namespace ODMSBusiness
{
    public class FleetPartPartialBL : BaseService<FleetPartViewModel>, IDownloadFile<FleetPartViewModel>
    {

        private readonly FleetPartPartialData data = new FleetPartPartialData();

        public ResponseModel<FleetPartPartialListModel> List(UserInfo user, FleetPartPartialListModel model, out int totalCnt)
        {
            var response = new ResponseModel<FleetPartPartialListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.List(user, model, out totalCnt).ToList();
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

        public override ResponseModel<bool> Exists(UserInfo user, FleetPartViewModel model)
        {
            var response = new ResponseModel<bool>();
            try
            {
                response.Model = data.Exists(user, model);
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

        public ResponseModel<bool> IsPartConstricted(UserInfo user, FleetPartViewModel model)
        {
            var response = new ResponseModel<bool>();
            try
            {
                response.Model = data.IsPartConstricted(user, model);
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

        public new ResponseModel<FleetPartViewModel> Insert(UserInfo user, FleetPartViewModel model)
        {
            var response = new ResponseModel<FleetPartViewModel>();
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

        public ResponseModel<FleetPartViewModel> Insert(UserInfo user, FleetPartViewModel model, List<FleetPartViewModel> filter)
        {
            var response = new ResponseModel<FleetPartViewModel>();
            try
            {
                data.Insert(user, model, filter);
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

        public new ResponseModel<FleetPartViewModel> Delete(UserInfo user, FleetPartViewModel model)
        {
            var response = new ResponseModel<FleetPartViewModel>();
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

        public ResponseModel<FleetPartViewModel> ParseExcel(UserInfo user, FleetPartViewModel model, Stream s)
        {
            var listModels = new List<FleetPartViewModel>();
            var response = new ResponseModel<FleetPartViewModel>();
            try
            {

                int totalCount = 0;
                FleetPartPartialListModel partModel = new FleetPartPartialListModel();
                partModel.FleetId = model.FleetId;
                FleetPartPartialBL bo = new FleetPartPartialBL();
                IEnumerable<FleetPartPartialListModel> partList = bo.List(user, partModel, out totalCount).Data;

                SparePartBL spBo = new SparePartBL();
                StringBuilder sb = new StringBuilder();


                DataSet ds = ExcelHelper.GetDataFromExcel(s);
                DataTable excelRows = ds.Tables[0];

                foreach (DataRow excelRow in excelRows.Rows)
                {
                    string partCode = excelRow[0].GetValue<string>();
                    SparePartIndexViewModel spModel = new SparePartIndexViewModel();
                    spModel.PartCode = partCode;
                    spBo.GetSparePart(user,spModel);

                    var fleetPartModel = new FleetPartViewModel
                    {
                        FleetId = model.FleetId,
                        PartId = spModel.PartId,
                        PartName = spModel.PartNameInLanguage
                    };

                    if (spModel.PartId == 0)
                    {
                        fleetPartModel.ErrorNo = 1;
                        fleetPartModel.ErrorMessage = string.Format(MessageResource.FleetPartPartial_Warning_InvalidPartCode, spModel.PartCode);
                    }

                    var control = (from r in partList.AsEnumerable()
                                   where r.PartId == partModel.PartId
                                   select r);
                    if (control.Any())
                    {
                        fleetPartModel.ErrorNo = 1;
                        fleetPartModel.ErrorMessage = MessageResource.FleetPartPartial_Warning_PartCodeExists;
                    }


                    listModels.Add(fleetPartModel);
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

        public MemoryStream SetExcelReport(List<FleetPartViewModel> listModels, string errMsg)
        {
            string errorMessage = CommonValues.RowStart + errMsg + CommonValues.RowEnd;
            string name = MessageResource.FleetPartViewModel_Display_PartCode;
            string preTable = CommonValues.TableStart + CommonValues.RowStart + CommonValues.ColumnStart + name + CommonValues.ColumnEnd;
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
                    sb.AppendFormat(CommonValues.RowStart + CommonValues.ColumnStart + "{0}" + CommonValues.ColumnEnd, model.PartName);
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
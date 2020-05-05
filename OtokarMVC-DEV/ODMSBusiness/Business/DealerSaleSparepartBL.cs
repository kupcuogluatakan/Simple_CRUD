using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSData;
using ODMSModel.DealerSaleSparepart;
using ODMSModel.DownloadFileActionResult;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ODMSBusiness
{
    public class DealerSaleSparepartBL : BaseBusiness, IDownloadFile<ListDealerSaleSparepartIndexViewModel>
    {
        private readonly DealerSaleSparepartData data = new DealerSaleSparepartData();

        public ResponseModel<DealerSaleSparepartListModel> ListDealerSaleSparepart(UserInfo user,DealerSaleSparepartListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<DealerSaleSparepartListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListDealerSaleSparepart(user,filter, out totalCnt);
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

        public ResponseModel<DealerSaleSparepartIndexViewModel> GetDealerSaleSparepart(UserInfo user, DealerSaleSparepartIndexViewModel filter)
        {
            var response = new ResponseModel<DealerSaleSparepartIndexViewModel>();
            try
            {
                response.Model = data.GetDealerSaleSparepart(user, filter);
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

        public ResponseModel<DealerSaleSparepartIndexViewModel> DMLDealerSaleSparepart(UserInfo user, DealerSaleSparepartIndexViewModel model)
        {
            var response = new ResponseModel<DealerSaleSparepartIndexViewModel>();
            try
            {
                data.DMLDealerSaleSparepart(user, model);
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

        public ResponseModel<ListDealerSaleSparePartReturnModel> DMLDealerSaleSparepartList(UserInfo user, ListDealerSaleSparepartIndexViewModel model)
        {
            var response = new ResponseModel<ListDealerSaleSparePartReturnModel>();
            try
            {
                response.Model = data.DMLDealerSaleSparepartList(user, model);
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

        public ResponseModel<DealerSaleSparepartIndexViewModel> GetSparepartListPrice(UserInfo user, long? idPart)
        {
            var response = new ResponseModel<DealerSaleSparepartIndexViewModel>();
            try
            {
                response.Model = data.GetSparepartListPrice(user, idPart);
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

        public MemoryStream SetExcelReportForReturnStatus(ListDealerSaleSparePartReturnModel filter, string errMsg)
        {
            string errorMessage = CommonValues.RowStart + errMsg + CommonValues.RowEnd;
            string preTable = CommonValues.TableStart + CommonValues.RowStart
                               + CommonValues.ColumnStart + MessageResource.CampaignPart_Display_PartCode
                + CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.Appointment_Display_Status;
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
                foreach (var model in filter.ListModel)
                {
                    sb.AppendFormat(CommonValues.RowStart +
                                    CommonValues.ColumnStart + "{0}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{1}" + CommonValues.ColumnEnd,
                                    model.PARTCODE, model.PROCESS_DESC);

                    sb.AppendFormat(CommonValues.ColumnStart + CommonValues.ColumnEnd + CommonValues.RowEnd);
                }
            }
            sb.Append(lastTable);

            sw.WriteLine(sb.ToString());

            sw.Flush();
            ms.Seek(0, SeekOrigin.Begin);

            return ms;
        }

        public MemoryStream SetExcelReport(List<ListDealerSaleSparepartIndexViewModel> modelList, string errMsg)
        {
            string errorMessage = CommonValues.RowStart + errMsg + CommonValues.RowEnd;
            string preTable = CommonValues.TableStart + CommonValues.RowStart
                               + CommonValues.ColumnStart + MessageResource.CampaignPart_Display_PartCode
                + CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.CustomerDiscount_Display_PartDiscountRation;
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
                foreach (var model in modelList[0].ListModel)
                {
                    sb.AppendFormat(CommonValues.RowStart +
                                    CommonValues.ColumnStart + "{0}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{1}" + CommonValues.ColumnEnd,
                                    model.PartCode, model.DiscountRatio);
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

        public ResponseModel<ListDealerSaleSparepartIndexViewModel> ParseExcel(UserInfo user, ListDealerSaleSparepartIndexViewModel model, Stream s)
        {
            List<ListDealerSaleSparepartIndexViewModel> excelList = new List<ListDealerSaleSparepartIndexViewModel>();

            var response = new ResponseModel<ListDealerSaleSparepartIndexViewModel>();

            try
            {
                DataSet ds = ExcelHelper.GetDataFromExcel(s);
                DataTable excelRows = ds.Tables[0];

                #region ColumnControl

                if (excelRows.Columns.Count < 2)
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.Labour_Warning_ColumnProblem;
                }
                else if (excelRows.Columns.Count > 2)
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.DealerSaleChannelDiscountRatio_Warning_ExcelFormat;
                }
                #endregion

                foreach (DataRow excelRow in excelRows.Rows)
                {
                    DealerSaleSparepartIndexViewModel row = new DealerSaleSparepartIndexViewModel { };

                    #region PartCode
                    string partCode = excelRow[0].GetValue<string>();
                    if (string.IsNullOrEmpty(partCode))
                    {
                        // exceldeki ilgili satırın yanına hata yazılır.
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.Warning_EmptyPartCode;
                    }
                    else
                    {
                        //AutocompleteSearch / LoadComboData
                        DealerSaleSparepartData data = new DealerSaleSparepartData();

                        var result = data.CheckPart(partCode);


                        if (result == false)
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.Warning_CannotFindCode;
                        }
                        else
                        {
                            row.PartCode = partCode;
                        }
                    }
                    row.PartCode = partCode;
                    #endregion

                    #region DiscountRatio

                    row.DiscountRatio = excelRow[1].GetValue<decimal>();
                    if (row.DiscountRatio == null)
                    {
                        // exceldeki ilgili satırın yanına hata yazılır.
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.Warning_EmptyDiscountRatio;
                    }
                    #endregion

                    if (excelList.Count != 0)
                    {
                        var existed = (from r in excelList[0].ListModel.AsEnumerable()
                                       where r.PartCode == row.PartCode
                                       select r);
                        if (existed.Any())
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.Warning_DuplicatePartCodeValue + " - " + partCode;
                        }
                    }
                    else
                    {
                        excelList.Add(new ListDealerSaleSparepartIndexViewModel() { ListModel = new List<DealerSaleSparepartIndexViewModel>() });
                    }

                    excelList[0].ListModel.Add(row);
                }
                var errorCount = (from r in excelList[0].ListModel.AsEnumerable()
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
                response.Total = excelList[0].ListModel.Count;
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

    }
}

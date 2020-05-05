using System.IO;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSData;
using ODMSModel.DownloadFileActionResult;
using ODMSModel.SparePart;
using ODMSModel.StockCard;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class StockCardBL : BaseBusiness, IDownloadFile<StockCardViewModel>
    {
        private readonly StockCardData data = new StockCardData();

        public ResponseModel<decimal> GetDealerPriceByDealerAndPart(int partId, int dealerId)
        {
            var response = new ResponseModel<decimal>();
            try
            {
                response.Model = data.GetDealerPriceByDealerAndPart(partId, dealerId);
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

        public ResponseModel<StockCardListModel> ListStockCards(UserInfo user, StockCardListModel stocCardListModel, out int totalCnt)
        {
            var response = new ResponseModel<StockCardListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListStockCards(user, stocCardListModel, out totalCnt);
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

        public ResponseModel<StockCardViewModel> GetStockCard(UserInfo user, StockCardViewModel referenceModel)
        {
            var response = new ResponseModel<StockCardViewModel>();
            try
            {
                response.Model = data.GetStockCard(user, referenceModel);
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

        public ResponseModel<StockCardViewModel> GetStockCardById(StockCardViewModel referenceModel)
        {
            var response = new ResponseModel<StockCardViewModel>();
            try
            {
                response.Model = data.GetStockCardById(referenceModel);
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
        /// STOK ARAMA EKRANI TOPLAM STOK DEĞERLERİ
        /// </summary>
        /// <param name="user"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public ResponseModel<StockCardStockValueModel> GetStockCardStockValues(UserInfo user, StockCardStockValueModel referenceModel) {

            var response = new ResponseModel<StockCardStockValueModel>();
            try
            {
                response.Model = data.GetStockCardStockValues(user,referenceModel);
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


        public ResponseModel<StockCardViewModel> DMLStockCard(UserInfo user, StockCardViewModel model)
        {
            var response = new ResponseModel<StockCardViewModel>();
            try
            {
                data.DMLStockCard(user, model);
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

        public ResponseModel<StockCardSearchListModel> ListStockCardSearch(UserInfo user, StockCardSearchListModel stockTypeDetailListModel, out int totalCnt)
        {
            var response = new ResponseModel<StockCardSearchListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListStockCardSearch(user, stockTypeDetailListModel, out totalCnt);
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

        public ResponseModel<StockCardViewModel> ParseExcel(UserInfo user, StockCardViewModel model, Stream s)
        {
            List<StockCardViewModel> excelList = new List<StockCardViewModel>();
            var response = new ResponseModel<StockCardViewModel>();

            try
            {
                DataSet result = new DataSet();
                DataSet ds = ExcelHelper.GetDataFromExcel(s);
                DataTable excelRows = ds.Tables[0];
                 
                if (excelRows.Columns.Count != 1)
                {
                    model.ErrorNo = 2;
                    model.ErrorMessage = MessageResource.StockCard_Warning_ColumnError;
                }
                else
                {
                    List<SparePartIndexViewModel> partList = new List<SparePartIndexViewModel>();

                    foreach (DataRow excelRow in excelRows.Rows)
                    {
                        string partCode = excelRow[0].GetValue<string>();

                        if (!string.IsNullOrEmpty(partCode))
                            partList.Add(new SparePartIndexViewModel() { PartCode = partCode });
                    }

                    SparePartBL spBo = new SparePartBL();
                    var rList = spBo.GetSparePartFromTable(partList).Data;

                    foreach (var item in rList)
                    {
                        StockCardViewModel row = new StockCardViewModel();
                        if (item.PartId == 0 && item.PartCode != null)
                        {
                            row.PartCode = item.PartCode;
                            row.ErrorMessage = MessageResource.StockCard_Warning_PartCodeNotFound;
                            row.ErrorNo = 1;
                            model.ErrorNo = 1;
                        }
                        else
                            row.PartCode = item.PartCode;

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

        public ResponseModel<StockCardViewModel> ParseExcelByOtokarStockSearch(UserInfo user, StockCardViewModel model, Stream s)
        {
            List<StockCardViewModel> excelList = new List<StockCardViewModel>();

            var response = new ResponseModel<StockCardViewModel>();
            try
            {
                DataSet ds = ExcelHelper.GetDataFromExcel(s);
                DataTable excelRows = ds.Tables[0];

                if (excelRows.Columns.Count != 1)
                {
                    model.ErrorMessage = MessageResource.StockCard_Warning_ColumnError;
                }
                else
                {
                    List<SparePartIndexViewModel> partList = new List<SparePartIndexViewModel>();

                    foreach (DataRow excelRow in excelRows.Rows)
                    {
                        string partCode = excelRow[0].GetValue<string>();
                        partList.Add(new SparePartIndexViewModel() { PartCode = partCode });
                    }


                    SparePartBL spBo = new SparePartBL();
                    var rList = spBo.GetSparePartFromTable(partList).Data;

                    foreach (var item in rList)
                    {
                        StockCardViewModel row = new StockCardViewModel();
                        if (item.PartId == 0 && item.PartCode != null)
                        {
                            row.PartCode = item.PartCode;
                            row.ErrorMessage = MessageResource.StockCard_Warning_PartCodeNotFound;
                            row.ErrorNo = 1;
                            model.ErrorNo = 1;
                        }
                        else
                            row.PartCode = item.PartCode;

                        if (item.OriginalPartId > 0)
                        {
                            row.ErrorMessage = MessageResource.Exception_Not_Original_Part;
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

        public MemoryStream SetExcelReport(List<StockCardViewModel> modelList, string errMsg)
        {
            string errorMessage = CommonValues.TableStart + errMsg + CommonValues.TableEnd;
            string preTable = CommonValues.TableStart + CommonValues.RowStart + CommonValues.ColumnStart + MessageResource.SparePart_Display_PartCode + CommonValues.ColumnEnd;
            if (modelList != null)
            {
                preTable = preTable + ("<TD width='400px'>Log</TD>");
            }
            const string lastTable = CommonValues.RowEnd + CommonValues.TableEnd;
            MemoryStream ms = new MemoryStream();

            var sw = new StreamWriter(ms, Encoding.UTF8);

            StringBuilder sb = new StringBuilder();
            if (modelList != null)
            {
                sb.Append(errorMessage);
            }
            sb.Append(preTable);
            if (modelList != null)
            {
                foreach (var model in modelList)
                {
                    sb.AppendFormat(CommonValues.RowStart + CommonValues.ColumnStart + "{0}" + CommonValues.ColumnEnd, model.PartCode);
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

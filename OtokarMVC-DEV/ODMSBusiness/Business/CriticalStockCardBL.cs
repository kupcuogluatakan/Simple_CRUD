using ODMSData;
using ODMSModel.CriticalStockCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ODMSCommon;
using ODMSCommon.Resources;
using System.Web.Mvc;
using System.IO;
using System.Threading;
using System.Globalization;
using ODMSModel.DownloadFileActionResult;
using ODMSModel.SparePart;
using ODMSCommon.Security;
using System.Data;

namespace ODMSBusiness
{
    public class CriticalStockCardBL : BaseBusiness, IDownloadFile<CriticalStockCardViewModel>
    {
        private readonly CriticalStockCardData data = new CriticalStockCardData();

        public ResponseModel<CriticalStockCardListModel> ListCriticalStockCard(UserInfo user,CriticalStockCardListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<CriticalStockCardListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListCriticalStockCard(user,filter, out totalCnt);
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

        public ResponseModel<CriticalStockCardViewModel> GetCriticalStockCard(UserInfo user, CriticalStockCardViewModel filter)
        {
            var response = new ResponseModel<CriticalStockCardViewModel>();
            try
            {
                response.Model = data.GetCriticalStockCard(user, filter);
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

        public ResponseModel<CriticalStockCardViewModel> DMLCriticalStockCard(UserInfo user, CriticalStockCardViewModel model)
        {
            var response = new ResponseModel<CriticalStockCardViewModel>();
            try
            {
                data.DMLCriticalStockCard(user, model);
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

        public ResponseModel<CriticalStockCardViewModel> ParseExcel(UserInfo user, CriticalStockCardViewModel model, Stream s)
        {
            List<CriticalStockCardViewModel> excelList = new List<CriticalStockCardViewModel>();
            var response = new ResponseModel<CriticalStockCardViewModel>();

            try
            {
                List<SelectListItem> SsIdDealerList = DealerBL.ListDealerSSIdAsSelectItem().Data;

                int count = 0;

                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;//decimal veri okunabilmesi için

                DataSet ds = ExcelHelper.GetDataFromExcel(s);
                DataTable excelRows = ds.Tables[0];

                if (excelRows.Columns.Count < 3)
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.Global_Warning_ExcelFormat;
                }
                else
                {
                    foreach (DataRow excelRow in excelRows.Rows)
                    {
                        CriticalStockCardViewModel row = new CriticalStockCardViewModel();

                        string SSID = excelRow[0].GetValue<string>();
                        if (SSID != String.Empty || SSID != "")
                        {
                            row.SSID = SSID;

                            if (string.IsNullOrEmpty(row.SSID))
                            {
                                // exceldeki ilgili satırın yanına hata yazılır.
                                row.ErrorNo = 1;
                                row.ErrorMessage = MessageResource.DealerStartupInventoryLevel_Warning_EmptyDealerClass;
                            }
                        }
                        else
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.DealerStartupInventoryLevel_Warning_EmptyDealerClass;
                        }
                        // bayi tipi kontrol ediliyor.

                        string partCode = excelRow[1].GetValue<string>();
                        if (partCode != String.Empty || partCode != "")
                        {
                            row.PartCode = partCode;
                            if (string.IsNullOrEmpty(row.PartCode))
                            {
                                // exceldeki ilgili satırın yanına hata yazılır.
                                row.ErrorNo = 1;
                                row.ErrorMessage = MessageResource.DealerStartupInventoryLevel_Warning_EmptyPartCode;
                            }
                        }
                        else
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.DealerStartupInventoryLevel_Warning_EmptyPartCode;
                        }

                        decimal quantity;
                        //decimal aa = er[2].GetValue<decimal>();

                        if (decimal.TryParse(excelRow[2].GetValue<string>(), out quantity))
                        {
                            row.CriticalStockQuantity = quantity;
                            if (row.CriticalStockQuantity <= 0 || row.CriticalStockQuantity >= 1000)
                            {
                                // exceldeki ilgili satırın yanına hata yazılır.
                                row.ErrorNo = 1;
                                row.ErrorMessage = MessageResource.CriticalStockCard_Warning_InvalidQuantityError;
                            }
                        }
                        else
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.DealerStartupInventoryLevel_Warning_EmptyQuantity;
                        }

                        row.IsActive = true;

                        if (!string.IsNullOrEmpty(row.SSID))
                        {
                            var dealerControl = (from r in SsIdDealerList.AsEnumerable()
                                                 where r.Text == row.SSID
                                                 select r.Value).FirstOrDefault();
                            if (dealerControl == null)//(dealerControl.Count() == 0)
                            {
                                // exceldeki ilgili satırın yanına hata yazılır.
                                row.ErrorNo = 1;
                                row.ErrorMessage = MessageResource.DealerStartupInventoryLevel_Warning_DealerClassNotFound;
                            }
                            else
                            {
                                row.IdDealer = int.Parse(dealerControl);
                            }
                        }
                        if (!string.IsNullOrEmpty(row.PartCode))
                        {
                            var spModel = new SparePartIndexViewModel { PartCode = row.PartCode };
                            var spBo = new SparePartBL();
                            spBo.GetSparePart(user, spModel);

                            if (spModel.PartId == 0)
                            {
                                // exceldeki ilgili satırın yanına hata yazılır.
                                row.ErrorNo = 1;
                                row.ErrorMessage = MessageResource.DealerStartupInventoryLevel_Warning_PartCodeNotFound;
                            }
                            else
                            {
                                row.IdPart = spModel.PartId;
                            }
                        }

                        excelList.Add(row);
                        count++;
                    }


                    var errorCount = (from r in excelList.AsEnumerable()
                                      where r.ErrorNo > 0
                                      select r);
                    if (errorCount.Any())
                    {
                        model.ErrorNo = 1;
                        model.ErrorMessage = MessageResource.ClaimSupplier_Warning_FoundError;
                    }
                    if (count == 0)
                    {
                        model.ErrorNo = 1;
                        model.ErrorMessage = MessageResource.DealerStartupInventoryLevel_Warning_EmptyExcel;
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

        public MemoryStream SetExcelReport(List<CriticalStockCardViewModel> modelList, string errMsg)
        {
            string errorMessage = CommonValues.RowStart + errMsg + CommonValues.RowEnd;
            string preTable = CommonValues.TableStart + CommonValues.RowStart + CommonValues.ColumnStart + MessageResource.Dealer_Display_SSID
                              + CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.CriticalStockCard_Display_PartCode
                              + CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.CriticalStockCard_Display_CriticalStockQuantity + CommonValues.ColumnEnd;
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
                                    CommonValues.ColumnStart + "{2}" + CommonValues.ColumnEnd, model.SSID, model.PartCode,
                                    model.CriticalStockQuantity);
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

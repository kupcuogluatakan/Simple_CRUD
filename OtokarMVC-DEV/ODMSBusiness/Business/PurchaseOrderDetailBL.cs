using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSData;
using ODMSData.Utility;
using ODMSModel.PurchaseOrder;
using ODMSModel.PurchaseOrderType;
using ODMSModel.PurchaseOrderDetail;
using ODMSModel.ServiceCallSchedule;
using ODMSModel.SparePart;
using System.Web.Mvc;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using OfficeOpenXml;
using ODMSModel.Dealer;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class PurchaseOrderDetailBL : BaseBusiness
    {
        private readonly PurchaseOrderDetailData data = new PurchaseOrderDetailData();
        private readonly SparePartBL sparePartBL = new SparePartBL();
        public ResponseModel<PurchaseOrderDetailListModel> ListPurchaseOrderDetails(UserInfo user, PurchaseOrderDetailListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<PurchaseOrderDetailListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListPurchaseOrderDetails(user, filter, out totalCnt);
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

        public ResponseModel<PurchaseOrderDetailListModel> ListPurchaseOrderDetails(UserInfo user, PurchaseOrderDetailListModel filter, out int totalCnt, out string executeSql)
        {
            var response = new ResponseModel<PurchaseOrderDetailListModel>();
            totalCnt = 0;
            executeSql = string.Empty;
            try
            {
                response.Data = data.ListPurchaseOrderDetails(user, filter, out totalCnt);
                executeSql = data.ExecuteSql;
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

        public ResponseModel<PurchaseOrderDetailViewModel> GetPurchaseOrderDetail(UserInfo user, PurchaseOrderDetailViewModel filter)
        {
            var response = new ResponseModel<PurchaseOrderDetailViewModel>();
            try
            {
                response.Model = data.GetPurchaseOrderDetail(user, filter);
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

        public ResponseModel<PurchaseOrderDetailViewModel> GetPurchaseOrderDetailList(UserInfo user, PurchaseOrderDetailViewModel filter)
        {
            var response = new ResponseModel<PurchaseOrderDetailViewModel>();
            try
            {
                response.Data = data.GetPurchaseOrderDetailList(user, filter);
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
        public ResponseModel<PurchaseOrderDetailViewModel> GetPurchaseOrderDetailsBySapInfo(string sapOfferNo, string sapRowNo, string partCode)
        {
            var response = new ResponseModel<PurchaseOrderDetailViewModel>();
            try
            {
                response.Model = data.GetPurchaseOrderDetailsBySapInfo(sapOfferNo, sapRowNo, partCode);
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
        public ResponseModel<PurchaseOrderDetailViewModel> DMLPurchaseOrderDetail(UserInfo user, PurchaseOrderDetailViewModel model)
        {
            var response = new ResponseModel<PurchaseOrderDetailViewModel>();
            try
            {
                data.DMLPurchaseOrderDetail(user, model);
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
        public ServiceCallLogModel SaveDenyReasons(DataTable dtMaster)
        {
            var logModel = new ServiceCallLogModel() { IsSuccess = true, ErrorModel = new List<ServiceCallScheduleErrorListModel>() };
            List<PurchaseOrderDetailViewModel> podList = new List<PurchaseOrderDetailViewModel>();
            PurchaseOrderDetailData poDetData = new PurchaseOrderDetailData();
            var dbhelper = new DbHelper();
            foreach (DataRow dataRow in dtMaster.Rows)
            {
                /*
                    <VBELV>0051156912</VBELV>   (SAP Teklif numarası)
                    <POSNR>000020</POSNR>        (Kalem Nuamarsı)
                    <KUNNR>0000200276</KUNNR>(Müşteri kodu)
                    <ABGRU>YM</ABGRU>              (Red Nedeni kodu)
                 */
                string vbelv = dataRow["VBELV"].GetValue<string>();
                string posnr = dataRow["POSNR"].GetValue<string>();
                string kunnr = dataRow["KUNNR"].GetValue<string>();
                string abgru = dataRow["ABGRU"].GetValue<string>();
                /*
                 * Servis dönen her satır için PURCHASE_ORDER_DET üzerinde iptal işlemi yapar.
                    VBELV değeri PURCHASE_ORDER_DET.SAP_OFFER_NO 
                    + 
                    POSNR değeri PURCHASE_ORDER_DET.SAP_ROW_NO
                    değerlerine sahip PURCHASE_ORDER_DET kaydı iptal statüsüne çekilir ve ABGRU değerine sahip lookup değerleri yeni
                    yarattığın lookkey lookval kolonlarına set edilir.
                 */

                //check if service exists in db

                var isDealerExists = new DealerData().ExistsDealerByDealerSSID(kunnr);
                if (isDealerExists)
                {
                    PurchaseOrderDetailViewModel detailModel = GetPurchaseOrderDetailsBySapInfo(vbelv, posnr, null).Model;

                    if (detailModel.PurchaseOrderDetailSeqNo != 0)
                    {
                        detailModel.CommandType = CommonValues.DMLType.Update;
                        detailModel.StatusId = (int)CommonValues.PurchaseOrderDetailStatus.Cancelled;
                        detailModel.DenyReason = abgru;
                        logModel.IsSuccess = true;
                        podList.Add(detailModel);

                    }
                    else
                    {
                        logModel.ErrorModel.Add(new ServiceCallScheduleErrorListModel()
                        {
                            Action = "SAP_OFFER_NO" + " (VBELV) : " + vbelv + " SAP_ROW_NO" + " (POSNR) : " + posnr,
                            Error =
                                string.Format(MessageResource.PurchaseOrderDetail_Warning_DetailNotFound, vbelv, posnr)
                        });
                    }
                }
            }
            if (podList.Any())
                poDetData.SaveDenyReasons(podList, logModel);
            return logModel;
        }

        public List<PurchaseOrderDetailViewModel> ParseExcel(UserInfo user, PurchaseOrderDetailViewModel model, Stream s, string extensionFile)
        {

            PurchaseOrderDetailBL podBo = new PurchaseOrderDetailBL();
            var listModels = new List<PurchaseOrderDetailViewModel>();
            var spBo = new SparePartBL();

            int detailCount = 0;
            PurchaseOrderDetailListModel listModel = new PurchaseOrderDetailListModel();
            listModel.PurchaseOrderNumber = model.PurchaseOrderNumber;
            List<PurchaseOrderDetailListModel> partList = podBo.ListPurchaseOrderDetails(user, listModel, out detailCount).Data;
            List<SelectListItem> currencyList = CommonBL.ListCurrencies(user).Data;

            using (var package = new ExcelPackage(s))
            {

                //assuming excel stream has headers so we start from 2 skipping first row
                var workSheet = package.Workbook.Worksheets.First();

                int startRow = 2;

                if (workSheet.Dimension.Columns != 5)
                {
                    var m = new PurchaseOrderDetailViewModel
                    {
                        ErrorNo = 1,
                        ErrorMessage = MessageResource.Labour_Error_InvalidExcelFormat
                    };

                    listModels.Add(m);

                    return listModels;
                }

                var culture = new CultureInfo("en-US");

                for (var rowNum = startRow; rowNum <= workSheet.Dimension.End.Row; rowNum++)
                {
                    var wsRow = workSheet.Cells[rowNum, 1, rowNum, workSheet.Dimension.End.Column];

                    var row = new PurchaseOrderDetailViewModel();

                    string partCode = wsRow[rowNum, 1].GetValue<string>().Trim(' ');
                    row.PartCode = partCode;
                    string desireQuantity = wsRow[rowNum, 2].GetValue<string>().Trim(' ');
                    row.DesireQuantity = decimal.Parse(wsRow[rowNum, 2].Text.Replace(",", "."), culture);


                    //string desireDeliveryDate = wsRow[rowNum, 3].GetValue<string>();
                    row.DesireDeliveryDate = wsRow[rowNum, 3].GetValue<DateTime?>();
                    string currencyCode = wsRow[rowNum, 4].GetValue<string>().Trim(' ');
                    row.CurrencyCode = currencyCode;
                    decimal orderPrice = string.IsNullOrEmpty(wsRow[rowNum, 5].Text) ? 0 : decimal.Parse(wsRow[rowNum, 5].Text.Replace(",", "."), culture);
                    row.OrderPrice = orderPrice;

                    #region Validation
                    if (String.IsNullOrEmpty(desireQuantity))
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.PurchaseOrderDetail_Warning_PlanQuantityEmpty;

                        if (listModels.Any(q => q.PartCode.Equals(row.PartCode) && !q.IsActive && q.DesireQuantity == row.DesireQuantity))
                            continue;
                        listModels.Add(row);
                        //
                    }
                    SparePartIndexViewModel checkModel = new SparePartIndexViewModel();
                    checkModel.PartCode = partCode;
                    spBo.GetSparePart(user, checkModel);

                    if (checkModel.PartId == 0)
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.PurchaseOrderDetail_Warning_PartCodeNotFound;
                        if (listModels.Any(q => q.PartCode.Equals(row.PartCode) && !q.IsActive && q.DesireQuantity == row.DesireQuantity))
                            continue;
                        listModels.Add(row);
                        //break;
                        continue;
                    }
                    // ekrandan kaydederken otomatik atanan bilgi burada da atanıyor. OYA 06.03.2017
                    decimal? pckgQuantity = checkModel.ShipQuantity;
                    row.OrderQuantity = pckgQuantity == 0
                                                  ? 0
                                                  : (((row.DesireQuantity -
                                                       (row.DesireQuantity % pckgQuantity)) /
                                                      pckgQuantity) +
                                                     ((row.DesireQuantity % pckgQuantity) != 0 ? 1 : 0)) *
                                                    pckgQuantity;

                    int partId = checkModel.PartId;
                    SparePartIndexViewModel spModel = new SparePartIndexViewModel();
                    spModel.PartId = partId;
                    if(partId != 0)
                        spBo.GetSparePart(user, spModel);
                    row.OrderQuantity = spModel.ShipQuantity == 0
                                                  ? 0
                                                  : (((row.DesireQuantity -
                                                       (row.DesireQuantity % spModel.ShipQuantity)) /
                                                      spModel.ShipQuantity) +
                                                     ((row.DesireQuantity % spModel.ShipQuantity) != 0 ? 1 : 0)) *
                                                    spModel.ShipQuantity;

                    PurchaseOrderViewModel poModel = new PurchaseOrderViewModel();
                    poModel.PoNumber = model.PurchaseOrderNumber;
                    PurchaseOrderBL poBo = new PurchaseOrderBL();
                    poBo.GetPurchaseOrder(user, poModel);

                    PurchaseOrderTypeBL poTypeBo = new PurchaseOrderTypeBL();
                    PurchaseOrderTypeViewModel poTypeModel = new PurchaseOrderTypeViewModel();
                    poTypeModel.PurchaseOrderTypeId = poModel.PoType.GetValue<int>();
                    poTypeModel = poTypeBo.Get(user, poTypeModel).Model;

                    var dbHelper = new DbHelper();
                    if (poTypeModel.IsCurrencySelectAllow)
                    {
                        row.CurrencyCode = currencyCode;
                        var currencyControl = (from e in currencyList
                                               where e.Value == currencyCode
                                               select e);
                        if (!currencyControl.Any())
                        {
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.PurchaseOrderDetail_Warning_CurrencyCode;
                            if (listModels.Any(q => q.PartCode.Equals(row.PartCode) && !q.IsActive && q.DesireQuantity == row.DesireQuantity))
                                continue;
                            listModels.Add(row);

                            //break;
                        }


                    }
                    else
                    {
                        int dealerId = UserManager.UserInfo.GetUserDealerId();
                        DealerBL dealerBo = new DealerBL();
                        DealerViewModel dealerModel = dealerBo.GetDealer(UserManager.UserInfo, dealerId).Model;
                        row.CurrencyCode = dealerBo.GetCountryCurrencyCode(dealerModel.Country).Model;


                    }
                    if (poTypeModel.ManuelPriceAllow)
                    {
                        row.OrderPrice = orderPrice;
                        if (orderPrice <= 0)
                        {
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.PurchaseOrderDetail_Warning_OrderPrice;
                            if (listModels.Any(q => q.PartCode.Equals(row.PartCode) && !q.IsActive && q.DesireQuantity == row.DesireQuantity))
                                continue;
                            listModels.Add(row);
                            //break;
                        }
                    }
                    else
                    {
                        row.OrderPrice = GetOrderPrice(user, partId);
                    }
                    if (poModel.SupplyType == (int)CommonValues.SupplyPort.Otokar)
                    {
                        // parça orjinal olmalı
                        if (spModel.OriginalPartId == 0 && spModel.IsOrderAllowed && spModel.IsActive)
                        {
                            row.PartId = partId;
                        }
                        else
                        {
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.PurchaseOrderDetail_Warning_Original;
                            if (listModels.Any(q => q.PartCode.Equals(row.PartCode) && !q.IsActive && q.DesireQuantity == row.DesireQuantity))
                                continue;
                            listModels.Add(row);
                            //break;
                        }
                    }
                    else
                    {
                        // parça orjinal olmamalı
                        if (spModel.OriginalPartId != 0 && spModel.IsOrderAllowed && spModel.IsActive)
                        {
                            row.PartId = partId;
                        }
                        else
                        {
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.PurchaseOrderDetail_Warning_NotOriginal;
                            if (listModels.Any(q => q.PartCode.Equals(row.PartCode) && !q.IsActive && q.DesireQuantity == row.DesireQuantity))
                                continue;
                            listModels.Add(row);
                            //break;
                        }
                    }
                    // parça daha önceden eklenmiş mi kontrolü
                    var control = (from e in partList.AsEnumerable()
                                   where e.PartId == row.PartId
                                   select e);
                    if (control.Any())
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.PurchaseOrderDetail_Warning_DuplicatePart;
                        if (listModels.Any(q => q.PartCode.Equals(row.PartCode) && !q.IsActive && q.DesireQuantity == row.DesireQuantity))
                            continue;
                        listModels.Add(row);
                        //break;
                    }

                    //son seviye parça kontolü
                    var changedPartCode = new PurchaseOrderDetailData().GetChangedPartCode(spModel.PartId);

                    if (!string.Equals(changedPartCode, spModel.PartCode))
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = string.Format(MessageResource.PurchaseOrderDetail_Warning_PartSplit, changedPartCode);
                        if (listModels.Any(q => q.PartCode.Equals(row.PartCode) && !q.IsActive && q.DesireQuantity == row.DesireQuantity))
                            continue;
                        listModels.Add(row);
                        //break;
                    }
                    #endregion

                    row.IsActive = true;
                    row.CommandType = CommonValues.DMLType.Insert;

                    listModels.Add(row);
                }


            }


            if (listModels.Exists(q => q.ErrorNo >= 1))
            {
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.SparePartSaleDetail_Warning_UploadFromExcel;
            }

            return listModels;
        }

        public ResponseModel<bool> CheckPurchaseOrderDefectPart(PurchaseOrderDetailViewModel viewModel)
        {
            var response = new ResponseModel<bool>();
            try
            {
                response.Model = data.CheckPurchaseOrderDefectPart(viewModel);
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

        public MemoryStream SetExcelReport(List<PurchaseOrderDetailViewModel> listModel, string errorMessage)
        {
            string errMsg = CommonValues.RowStart + errorMessage + CommonValues.RowEnd;
            string preTable = CommonValues.TableStart +
                              CommonValues.RowStart +
                              CommonValues.ColumnStart + MessageResource.SparePart_Display_PartCode + CommonValues.ColumnEnd +
                              CommonValues.ColumnStart + MessageResource.PurchaseOrderDetail_Display_DesireQuantity + CommonValues.ColumnEnd +
                              CommonValues.ColumnStart + MessageResource.PurchaseOrderDetail_Display_DesireDeliveryDate + CommonValues.ColumnEnd +
                              CommonValues.ColumnStart + MessageResource.PurchaseOrderDetail_Display_CurrencyCode + CommonValues.ColumnEnd;
            if (listModel != null)
            {
                preTable = preTable + ("<TD width='400px'>Log</TD>");
            }
            const string lastTable = CommonValues.RowEnd + CommonValues.TableEnd;
            var ms = new MemoryStream();

            var sw = new StreamWriter(ms, Encoding.UTF8);

            var sb = new StringBuilder();
            sb.Append(errMsg);
            sb.Append(preTable);
            if (listModel != null)
            {
                foreach (var model in listModel)
                {
                    sb.AppendFormat(CommonValues.RowStart + CommonValues.ColumnStart + "{0}" + CommonValues.ColumnEnd +
                                        CommonValues.ColumnStart + "{1}" + CommonValues.ColumnEnd +
                                        CommonValues.ColumnStart + "{2}" + CommonValues.ColumnEnd +
                                        CommonValues.ColumnStart + "{3}" + CommonValues.ColumnEnd,
                                        model.PartCode,
                                        model.DesireQuantity,
                                        model.DesireDeliveryDate,
                                        model.CurrencyCode);

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

        public MemoryStream SampleExcelFormat()
        {
            string lastTable = CommonValues.TableStart + CommonValues.RowStart + CommonValues.ColumnStart +
                               MessageResource.SparePart_Display_PartCode + CommonValues.ColumnEnd +
                               CommonValues.ColumnStart + MessageResource.PurchaseOrderDetail_Display_DesireQuantity +
                               CommonValues.ColumnEnd +
                               CommonValues.ColumnStart + MessageResource.PurchaseOrderDetail_Display_DesireDeliveryDate +
                               CommonValues.ColumnEnd +
                               CommonValues.ColumnStart + MessageResource.PurchaseOrderDetail_Display_CurrencyCode +
                               CommonValues.ColumnEnd +
                               CommonValues.ColumnStart + MessageResource.PurchaseOrderDetail_Display_OrderPrice +
                               CommonValues.ColumnEnd +
                               CommonValues.RowEnd +
                               CommonValues.TableEnd;

            var ms = new MemoryStream();
            var sw = new StreamWriter(ms, Encoding.UTF8);
            var sb = new StringBuilder();

            sb.Append(lastTable);

            sw.WriteLine(sb.ToString());

            sw.Flush();
            ms.Seek(0, SeekOrigin.Begin);

            return ms;
        }

        public ResponseModel<PurchaseOrderSparePartDetailsModel> GetPurcaOrderSparePartDetailsModel(UserInfo user, long poDetSeqNo)
        {
            var response = new ResponseModel<PurchaseOrderSparePartDetailsModel>();
            try
            {
                response.Data = data.GetPurchaseOrderSparePartDetailsModel(user, poDetSeqNo);
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

        private decimal GetOrderPrice(UserInfo user, int partId)
        {
            decimal orderPrice = 0;
            SparePartIndexViewModel spModel = new SparePartIndexViewModel() { PartId = partId };
            sparePartBL.GetSparePart(user, spModel);
            if (spModel.IsOriginal.GetValue<bool>())
            {
                CommonBL commonBo = new CommonBL();
                orderPrice = commonBo.GetPriceByDealerPartVehicleAndType(partId, null, user.GetUserDealerId(), CommonValues.DealerPriceLabel).Model;
            }
            else
            {
                StockCardBL stockCardBo = new StockCardBL();
                orderPrice = stockCardBo.GetDealerPriceByDealerAndPart(partId, user.GetUserDealerId()).Model;
            }
            return orderPrice;
        }

        public ResponseModel<decimal> GetTotalPrice(string poNumber)
        {
            var response = new ResponseModel<decimal>();
            try
            {
                response.Model = data.GetTotalPrice(poNumber);
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

        public ResponseModel<bool> CheckDealerAccessPermission(UserInfo user, string partId)
        {
            var response = new ResponseModel<bool>();
            try
            {
                response.Model = data.CheckDealerAccessPermission(user, partId);
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


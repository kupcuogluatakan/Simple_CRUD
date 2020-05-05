using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.PurchaseOrderDetail;
using ODMSModel.ServiceCallSchedule;

namespace ODMSData
{
    public class PurchaseOrderDetailData : DataAccessBase
    {
        public List<PurchaseOrderDetailListModel> ListPurchaseOrderDetails(UserInfo user,PurchaseOrderDetailListModel filter,
                                                                 out int totalCnt)
        {
            var retVal = new List<PurchaseOrderDetailListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PURCHASE_ORDER_DET");
                db.AddInParameter(cmd, "PO_NUMBER", DbType.Int32, MakeDbNull(filter.PurchaseOrderNumber));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "TotalPrice", DbType.Decimal, 0);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var purchaseOrderDetailListModel = new PurchaseOrderDetailListModel
                        {
                            DesireQuantity = reader["DESIRE_QUANTITY"].GetValue<decimal>(),
                            DesireDeliveryDate = reader["DESIRE_DELIVERY_DATE"].GetValue<DateTime?>(),
                            OrderPrice = reader["ORDER_PRICE"].GetValue<decimal>(),
                            ConfirmPrice = reader["CONFIRM_PRICE"].GetValue<decimal>(),
                            OrderQuantity = reader["ORDER_QUANTITY"].GetValue<decimal>(),
                            PackageQuantity = reader["PACKAGE_QUANTITY"].GetValue<decimal>(),
                            PartId = reader["PART_ID"].GetValue<long>(),
                            ChangedPartId = reader["CHANGED_PART_ID"].GetValue<long>(),
                            PartCode = reader["PART_CODE"].GetValue<string>(),
                            PartName = reader["PART_NAME"].GetValue<string>(),
                            PurchaseOrderDetailSeqNo = reader["PO_DET_SEQ_NO"].GetValue<int>(),
                            PurchaseOrderNumber = reader["PO_NUMBER"].GetValue<int>(),
                            StatusId = reader["STATUS_ID"].GetValue<int>(),
                            DenyReason = reader["DENY_REASON"].GetValue<string>(),
                            ReceivedQuantity = reader["RECEIVED_QUANTITY"].GetValue<decimal>(),
                            ShipmentQuantity = reader["SHIPMENT_QUANTITY"].GetValue<decimal>(),
                            // o anki stok adeti de çekiliyor Oya 03.04.2018
                            StockQuantity = reader["STOCK_QUANTITY"].GetValue<decimal>(),
                            SAPOfferNo = reader["SAP_OFFER_NO"].GetValue<string>(),
                            DealerPrice = reader["DEALER_PRICE"].GetValue<decimal>(),
                            UnitName = reader["UNIT"].GetValue<string>(),
                            CurrencyCode = reader["CURRENCY_CODE"].GetValue<string>(),
                            StatusName = reader["STATUS_NAME"].ToString(),
                            PackageQuantityString = reader["PACKAGE_QUANTITY"].GetValue<decimal>() + " " + reader["UNIT"].GetValue<string>(),
                            AppliedDiscountRatio = reader["APPLIED_DISCOUNT_RATIO"].GetValue<decimal?>(),
                            ListDiscountRatio = reader["LIST_DISCOUNT_RATIO"].GetValue<decimal?>(),
                            SpecialExplanation = reader["SPECIAL_EXPLANATION"].GetValue<string>(),
                        };
                        retVal.Add(purchaseOrderDetailListModel);
                    }

                    reader.Close();
                }

                totalCnt = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
                var totalPrice = db.GetParameterValue(cmd, "TotalPrice").GetValue<decimal>();
                retVal.ForEach(c => c.TotalPrice = totalPrice);

                retVal.ForEach(c => c.OrderPriceS = c.OrderPrice.HasValue ? c.OrderPrice.GetValueOrDefault().RoundUp(2).ToString("N2") : string.Empty);
                retVal.ForEach(c => c.ConfirmPriceS = c.ConfirmPrice.HasValue ? c.ConfirmPrice.GetValueOrDefault().RoundUp(2).ToString("N2") : string.Empty);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                CloseConnection();
            }

            return retVal;
        }

        public void DMLPurchaseOrderDetail(UserInfo user,PurchaseOrderDetailViewModel purchaseOrderDetailModel)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_PURCHASE_ORDER_DET");
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, purchaseOrderDetailModel.CommandType);
                db.AddParameter(cmd, "PO_DET_SEQ_NO", DbType.Int64, ParameterDirection.InputOutput, "PO_DET_SEQ_NO", DataRowVersion.Default, MakeDbNull(purchaseOrderDetailModel.PurchaseOrderDetailSeqNo));
                db.AddInParameter(cmd, "PO_NUMBER", DbType.Int32,
                                  MakeDbNull(purchaseOrderDetailModel.PurchaseOrderNumber));
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, MakeDbNull(purchaseOrderDetailModel.PartId));
                db.AddInParameter(cmd, "PACKAGE_QUANTITY", DbType.Decimal, purchaseOrderDetailModel.PackageQuantity);
                db.AddInParameter(cmd, "DESIRE_QUANTITY", DbType.Decimal, purchaseOrderDetailModel.DesireQuantity);
                db.AddInParameter(cmd, "ORDER_QUANTITY", DbType.Decimal, purchaseOrderDetailModel.OrderQuantity);
                db.AddInParameter(cmd, "SHIPMENT_QUANTITY", DbType.Decimal, purchaseOrderDetailModel.ShipmentQuantity);
                db.AddInParameter(cmd, "ORDER_PRICE", DbType.Decimal, purchaseOrderDetailModel.OrderPrice);
                db.AddInParameter(cmd, "DESIRE_DELIVERY_DATE", DbType.DateTime,
                                  MakeDbNull(purchaseOrderDetailModel.DesireDeliveryDate));
                db.AddInParameter(cmd, "STATUS_ID", DbType.Int32, purchaseOrderDetailModel.StatusId);
                db.AddInParameter(cmd, "DENY_REASON", DbType.String,
                                  MakeDbNull(purchaseOrderDetailModel.DenyReason));
                db.AddInParameter(cmd, "SAP_SHIP_ID_PART", DbType.String, MakeDbNull(purchaseOrderDetailModel.SAPShipIdPart));
                db.AddInParameter(cmd, "CURRENCY_CODE", DbType.String, MakeDbNull(purchaseOrderDetailModel.CurrencyCode));
                db.AddInParameter(cmd, "LIST_PRICE", DbType.Decimal, purchaseOrderDetailModel.ListPrice);
                db.AddInParameter(cmd, "LIST_DISCOUNT_RATIO", DbType.Decimal, purchaseOrderDetailModel.ListDiscountRatio);
                db.AddInParameter(cmd, "CONFIRM_PRICE", DbType.Decimal, purchaseOrderDetailModel.ConfirmPrice);
                db.AddInParameter(cmd, "APPLIED_DISCOUNT_RATIO", DbType.Decimal, purchaseOrderDetailModel.AppliedDiscountRatio);
                db.AddInParameter(cmd, "SPECIAL_EXPLANATION", DbType.String, purchaseOrderDetailModel.SpecialExplanation);


                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String,
                                  MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                purchaseOrderDetailModel.PurchaseOrderDetailSeqNo = db.GetParameterValue(cmd, "PO_DET_SEQ_NO").GetValue<long>();
                purchaseOrderDetailModel.PurchaseOrderNumber = db.GetParameterValue(cmd, "PO_NUMBER").GetValue<int>();
                purchaseOrderDetailModel.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                purchaseOrderDetailModel.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (purchaseOrderDetailModel.ErrorNo > 0)
                    purchaseOrderDetailModel.ErrorMessage =
                        ResolveDatabaseErrorXml(purchaseOrderDetailModel.ErrorMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        public PurchaseOrderDetailViewModel GetPurchaseOrderDetail(UserInfo user,PurchaseOrderDetailViewModel filter)
        {
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_PURCHASE_ORDER_DET");
                db.AddInParameter(cmd, "PO_DET_SEQ_NO", DbType.Int32, MakeDbNull(filter.PurchaseOrderDetailSeqNo));
                db.AddInParameter(cmd, "PO_NUMBER", DbType.Int32, MakeDbNull(filter.PurchaseOrderNumber));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.DesireQuantity = dReader["DESIRE_QUANTITY"].GetValue<decimal>();
                    filter.DesireDeliveryDate = dReader["DESIRE_DELIVERY_DATE"].GetValue<DateTime?>();
                    filter.OrderPrice = dReader["ORDER_PRICE"].GetValue<decimal>();
                    filter.ListPrice = dReader["LIST_PRICE"].GetValue<decimal>();
                    filter.ListDiscountRatio = dReader["LIST_DISCOUNT_RATIO"].GetValue<decimal>();
                    filter.ConfirmPrice = dReader["CONFIRM_PRICE"].GetValue<decimal>();
                    filter.AppliedDiscountRatio = dReader["APPLIED_DISCOUNT_RATIO"].GetValue<decimal>();
                    filter.OrderQuantity = dReader["ORDER_QUANTITY"].GetValue<decimal>();
                    filter.PackageQuantity = dReader["PACKAGE_QUANTITY"].GetValue<decimal>();
                    filter.ShipmentQuantity = dReader["SHIP_QUANT"].GetValue<decimal>();
                    filter.PartId = dReader["PART_ID"].GetValue<int>();
                    filter.PartName = dReader["PART_NAME"].GetValue<string>();
                    filter.PurchaseOrderDetailSeqNo = dReader["PO_DET_SEQ_NO"].GetValue<int>();
                    filter.PurchaseOrderNumber = dReader["PO_NUMBER"].GetValue<int>();
                    filter.StatusId = dReader["STATUS_ID"].GetValue<int>();
                    filter.StatusName = dReader["STATUS_NAME"].GetValue<string>();
                    filter.CurrencyCode = dReader["CURRENCY_CODE"].GetValue<string>();
                    filter.DesireDeliveryDate = dReader["DESIRE_DELIVERY_DATE"].GetValue<DateTime?>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dReader != null && !dReader.IsClosed)
                {
                    dReader.Close();
                    dReader.Dispose();
                }
                CloseConnection();
            }

            return filter;
        }

        public List<PurchaseOrderDetailViewModel> GetPurchaseOrderDetailList(UserInfo user, PurchaseOrderDetailViewModel filter)
        {
            DbDataReader dReader = null;
            List<PurchaseOrderDetailViewModel> poDetailList = new List<PurchaseOrderDetailViewModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_PURCHASE_ORDER_DET");
                db.AddInParameter(cmd, "PO_DET_SEQ_NO", DbType.Int32, MakeDbNull(filter.PurchaseOrderDetailSeqNo));
                db.AddInParameter(cmd, "PO_NUMBER", DbType.Int32, MakeDbNull(filter.PurchaseOrderNumber));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.DesireQuantity = dReader["DESIRE_QUANTITY"].GetValue<decimal>();
                    filter.DesireDeliveryDate = dReader["DESIRE_DELIVERY_DATE"].GetValue<DateTime?>();
                    filter.OrderPrice = dReader["ORDER_PRICE"].GetValue<decimal>();
                    filter.ListPrice = dReader["LIST_PRICE"].GetValue<decimal>();
                    filter.ListDiscountRatio = dReader["LIST_DISCOUNT_RATIO"].GetValue<decimal>();
                    filter.ConfirmPrice = dReader["CONFIRM_PRICE"].GetValue<decimal>();
                    filter.AppliedDiscountRatio = dReader["APPLIED_DISCOUNT_RATIO"].GetValue<decimal>();
                    filter.OrderQuantity = dReader["ORDER_QUANTITY"].GetValue<decimal>();
                    filter.PackageQuantity = dReader["PACKAGE_QUANTITY"].GetValue<decimal>();
                    filter.ShipmentQuantity = dReader["SHIP_QUANT"].GetValue<decimal>();
                    filter.PartId = dReader["PART_ID"].GetValue<int>();
                    filter.PartName = dReader["PART_NAME"].GetValue<string>();
                    filter.PurchaseOrderDetailSeqNo = dReader["PO_DET_SEQ_NO"].GetValue<int>();
                    filter.PurchaseOrderNumber = dReader["PO_NUMBER"].GetValue<int>();
                    filter.StatusId = dReader["STATUS_ID"].GetValue<int>();
                    filter.StatusName = dReader["STATUS_NAME"].GetValue<string>();
                    filter.CurrencyCode = dReader["CURRENCY_CODE"].GetValue<string>();
                    filter.DesireDeliveryDate = dReader["DESIRE_DELIVERY_DATE"].GetValue<DateTime?>();

                    poDetailList.Add(filter);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dReader != null && !dReader.IsClosed)
                {
                    dReader.Close();
                    dReader.Dispose();
                }
                CloseConnection();
            }

            return poDetailList;
        }
        public PurchaseOrderDetailViewModel GetPurchaseOrderDetailsBySapInfo(string sapOfferNo, string sapRowNo, string partCode)
        {
            PurchaseOrderDetailViewModel purchaseOrderDetailModel = new PurchaseOrderDetailViewModel();
            purchaseOrderDetailModel.SAPOfferNo = sapOfferNo;
            purchaseOrderDetailModel.SAPRowNo = sapRowNo;
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_PURCHASE_ORDER_DET_BY_SAP_INFO");
                db.AddInParameter(cmd, "SAP_OFFER_NO", DbType.String, MakeDbNull(purchaseOrderDetailModel.SAPOfferNo));
                db.AddInParameter(cmd, "SAP_ROW_NO", DbType.Int32, MakeDbNull(purchaseOrderDetailModel.SAPRowNo));
                db.AddInParameter(cmd, "PART_CODE", DbType.String, MakeDbNull(partCode));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, "TR");
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    purchaseOrderDetailModel.DesireQuantity = dReader["DESIRE_QUANTITY"].GetValue<decimal>();
                    purchaseOrderDetailModel.DesireDeliveryDate = dReader["DESIRE_DELIVERY_DATE"].GetValue<DateTime?>();
                    purchaseOrderDetailModel.OrderPrice = dReader["ORDER_PRICE"].GetValue<decimal>();
                    purchaseOrderDetailModel.OrderQuantity = dReader["ORDER_QUANTITY"].GetValue<decimal>();
                    purchaseOrderDetailModel.PackageQuantity = dReader["PACKAGE_QUANTITY"].GetValue<decimal>();
                    purchaseOrderDetailModel.PartId = dReader["PART_ID"].GetValue<int>();
                    purchaseOrderDetailModel.PartName = dReader["PART_NAME"].GetValue<string>();
                    purchaseOrderDetailModel.PurchaseOrderDetailSeqNo = dReader["PO_DET_SEQ_NO"].GetValue<int>();
                    purchaseOrderDetailModel.PurchaseOrderNumber = dReader["PO_NUMBER"].GetValue<int>();
                    purchaseOrderDetailModel.StatusId = dReader["STATUS_ID"].GetValue<int>();
                    purchaseOrderDetailModel.StatusName = dReader["STATUS_NAME"].GetValue<string>();
                    purchaseOrderDetailModel.DesireDeliveryDate = dReader["DESIRE_DELIVERY_DATE"].GetValue<DateTime?>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dReader != null && !dReader.IsClosed)
                {
                    dReader.Close();
                    dReader.Dispose();
                }
                CloseConnection();
            }
            return purchaseOrderDetailModel;
        }


        public string CompletePurchaseOrderDetail(UserInfo user,string poNumber, string rowNumber, string partCode, string orderNumber)
        {
            int errorNo = 0;
            string errorMessage = string.Empty;
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_COMPLETE_PURCHASE_ORDER_DET");
                db.AddInParameter(cmd, "PO_NUMBER", DbType.String, poNumber);
                db.AddInParameter(cmd, "ROW_NUMBER", DbType.Int32, rowNumber);
                db.AddInParameter(cmd, "PART_CODE", DbType.String, partCode);
                db.AddInParameter(cmd, "ORDER_NUMBER", DbType.Int32, orderNumber);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                errorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                errorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (errorNo > 0)
                    errorMessage =
                        ResolveDatabaseErrorXml(errorMessage);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            finally
            {
                CloseConnection();
            }
            return errorMessage;
        }


        public void SaveDenyReasons(List<PurchaseOrderDetailViewModel> listModel, ServiceCallLogModel logModel)
        {
            var listError = new List<ServiceCallScheduleErrorListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_PURCHASE_ORDER_DET");

                CreateConnection(cmd);
                foreach (var purchaseOrderDetailModel in listModel)
                {
                    cmd.Parameters.Clear();

                    db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, purchaseOrderDetailModel.CommandType);
                    db.AddParameter(cmd, "PO_DET_SEQ_NO", DbType.Int64, ParameterDirection.InputOutput, "PO_DET_SEQ_NO", DataRowVersion.Default, MakeDbNull(purchaseOrderDetailModel.PurchaseOrderDetailSeqNo));
                    db.AddInParameter(cmd, "PO_NUMBER", DbType.Int32,
                                      MakeDbNull(purchaseOrderDetailModel.PurchaseOrderNumber));
                    db.AddInParameter(cmd, "PART_ID", DbType.Int32, MakeDbNull(purchaseOrderDetailModel.PartId));
                    db.AddInParameter(cmd, "PACKAGE_QUANTITY", DbType.Decimal, purchaseOrderDetailModel.PackageQuantity);
                    db.AddInParameter(cmd, "DESIRE_QUANTITY", DbType.Decimal, purchaseOrderDetailModel.DesireQuantity);
                    db.AddInParameter(cmd, "ORDER_QUANTITY", DbType.Decimal, purchaseOrderDetailModel.OrderQuantity);
                    db.AddInParameter(cmd, "SHIPMENT_QUANTITY", DbType.Decimal, purchaseOrderDetailModel.ShipmentQuantity);
                    db.AddInParameter(cmd, "ORDER_PRICE", DbType.Decimal, purchaseOrderDetailModel.OrderPrice);
                    db.AddInParameter(cmd, "DESIRE_DELIVERY_DATE", DbType.DateTime,
                                      MakeDbNull(purchaseOrderDetailModel.DesireDeliveryDate));
                    db.AddInParameter(cmd, "STATUS_ID", DbType.Int32, purchaseOrderDetailModel.StatusId);
                    db.AddInParameter(cmd, "DENY_REASON", DbType.String,
                                      MakeDbNull(purchaseOrderDetailModel.DenyReason));
                    db.AddInParameter(cmd, "SAP_SHIP_ID_PART", DbType.String, MakeDbNull(purchaseOrderDetailModel.SAPShipIdPart));
                    db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String,
                                      MakeDbNull("TR"));
                    db.AddInParameter(cmd, "CURRENCY_CODE", DbType.String, MakeDbNull(purchaseOrderDetailModel.CurrencyCode));
                    db.AddInParameter(cmd, "LIST_PRICE", DbType.Decimal, purchaseOrderDetailModel.ListPrice);
                    db.AddInParameter(cmd, "LIST_DISCOUNT_RATIO", DbType.Decimal, purchaseOrderDetailModel.ListDiscountRatio);
                    db.AddInParameter(cmd, "CONFIRM_PRICE", DbType.Decimal, purchaseOrderDetailModel.ConfirmPrice);
                    db.AddInParameter(cmd, "APPLIED_DISCOUNT_RATIO", DbType.Decimal, purchaseOrderDetailModel.AppliedDiscountRatio);
                    db.AddInParameter(cmd, "SPECIAL_EXPLANATION", DbType.String, purchaseOrderDetailModel.SpecialExplanation);

                    db.AddInParameter(cmd, "OPERATING_USER", DbType.String, -1);
                    db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                    db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                    db.ExecuteNonQuery(cmd);

                    purchaseOrderDetailModel.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                    purchaseOrderDetailModel.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                    if (purchaseOrderDetailModel.ErrorNo > 0)
                    {
                        purchaseOrderDetailModel.ErrorMessage =
                              ResolveDatabaseErrorXml(purchaseOrderDetailModel.ErrorMessage);
                        listError.Add(new ServiceCallScheduleErrorListModel()
                        {
                            Action = MessageResource.PurchaseOrder_Display_PoNumber + " : " + purchaseOrderDetailModel.PurchaseOrderNumber + " " +
                            MessageResource.PurchaseOrderDetail_Display_SequenceNo + " : " + purchaseOrderDetailModel.PurchaseOrderDetailSeqNo,
                            Error = purchaseOrderDetailModel.ErrorMessage
                        });

                        logModel.IsSuccess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                logModel.IsSuccess = false;
                logModel.LogErrorDesc = String.Format("Messsage:{0} || TargetSite:{1}", ex.Message, ex.TargetSite);
            }
            finally
            {
                if (!logModel.IsSuccess)
                {
                    logModel.ErrorModel = listError;
                }
                CloseConnection();
            }
        }

        public bool CheckPurchaseOrderDefectPart(PurchaseOrderDetailViewModel viewModel)
        {
            CreateDatabase();
            var cmd = db.GetStoredProcCommand("P_CHECK_PO_DEFECT_PART");
            db.AddInParameter(cmd, "PO_NUMBER", DbType.Int32, MakeDbNull(viewModel.PurchaseOrderNumber));
            db.AddInParameter(cmd, "ID_PART", DbType.Int32, MakeDbNull(viewModel.PartId));
            CreateConnection(cmd);
            bool returnVal = false;
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnVal = reader["RESULT"].GetValue<bool>();
                }
                reader.Close();
            }
            return returnVal;
        }

        public decimal GetTotalPrice(string poNumber)
        {
            CreateDatabase();
            var cmd = db.GetStoredProcCommand("GET_CONFIRM_PRICE_FOR_PO_NUMBER");
            db.AddInParameter(cmd, "PO_NUMBER", DbType.Int32, MakeDbNull(Convert.ToInt32(poNumber)));
            CreateConnection(cmd);
            decimal returnVal = 0;
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnVal = reader["CONFIRM_PRICE"].GetValue<decimal>();
                }
                reader.Close();
            }
            return returnVal;
        }

        public bool CheckDealerAccessPermission(UserInfo user,string partId)
        {
            CreateDatabase();
            var cmd = db.GetStoredProcCommand("P_CHECK_DEALER_PART_ACCESS_PERMISSION");
            db.AddInParameter(cmd, "PART_ID", DbType.Int32, MakeDbNull(partId));
            db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
            CreateConnection(cmd);
            bool returnVal = false;
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnVal = reader["RESULT"].GetValue<bool>();
                }
                reader.Close();
            }
            return returnVal;
        }
        public List<PurchaseOrderSparePartDetailsModel> GetPurchaseOrderSparePartDetailsModel(UserInfo user,long poDetSeqNo)
        {
            var retVal = new List<PurchaseOrderSparePartDetailsModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_PURCHASE_ORDER_PART_DETAILS");
                db.AddInParameter(cmd, "PO_DET_SEQ_NO", DbType.Int32, MakeDbNull(poDetSeqNo));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var purchaseOrderDetailListModel = new PurchaseOrderSparePartDetailsModel
                        {
                            WayBillDate = reader["WayBillDate"].GetValue<DateTime>(),
                            SupplierId = reader["SupplierId"].GetValue<long?>(),
                            DealerId = reader["DealerId"].GetValue<int>(),
                            SenderDealerId = reader["SenderDealerId"].GetValue<int?>(),
                            ShipQuantity = reader["ShipQuantity"].GetValue<int>(),
                            PartCode = reader["PartCode"].GetValue<string>(),
                            PartName = reader["PartName"].GetValue<string>(),
                            DeliveryId = reader["DeliveryId"].GetValue<long>(),
                            DesiredPartCode = reader["DesiredPartCode"].GetValue<string>(),
                            DesiredPartName = reader["DesiredPartName"].GetValue<string>(),
                            WayBillNo = reader["WayBillNo"].GetValue<string>()
                        };
                        retVal.Add(purchaseOrderDetailListModel);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                CloseConnection();
            }

            return retVal;
        }

        public string GetChangedPartCode(int partId)
        {
            CreateDatabase();
            var cmd = db.GetStoredProcCommand("P_GET_CHANGED_PART_CODE");
            db.AddInParameter(cmd, "PART_ID", DbType.Int32, MakeDbNull(partId));
            CreateConnection(cmd);
            string changedPartCode = string.Empty;
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    changedPartCode = reader["PART_CODE"].GetValue<string>();
                }
                reader.Close();
            }
            return changedPartCode;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.Dealer;
using ODMSModel.SparePartSaleOrderDetail;
using ODMSCommon.Resources;

namespace ODMSData
{
    public class SparePartSaleOrderDetailData : DataAccessBase
    {
        public void DMLSparePartSaleOrderDetail(UserInfo user,SparePartSaleOrderDetailDetailModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_SPARE_PART_SALE_ORDER_DETAIL");
                db.AddInParameter(cmd, "SPARE_PART_SALE_ORDER_DETAIL_ID", DbType.Int32, model.SparePartSaleOrderDetailId);
                db.AddInParameter(cmd, "SO_NUMBER", DbType.String, model.SoNumber);
                db.AddInParameter(cmd, "ID_PART", DbType.String, model.SparePartId);
                db.AddInParameter(cmd, "PO_DET_SEQ_NO", DbType.String, MakeDbNull(model.PoDetailSequenceNo));
                db.AddInParameter(cmd, "ORDER_QUANTITY", DbType.Decimal, MakeDbNull(model.OrderQuantity));
                db.AddInParameter(cmd, "PLANNED_QUANTITY", DbType.Decimal, MakeDbNull(model.PlannedQuantity));
                db.AddInParameter(cmd, "SHIPPED_QUANTITY", DbType.Decimal, MakeDbNull(model.ShippedQuantity));
                db.AddInParameter(cmd, "LIST_PRICE", DbType.Decimal, MakeDbNull(model.ListPrice));
                db.AddInParameter(cmd, "ORDER_PRICE", DbType.Decimal, MakeDbNull(model.OrderPrice));
                db.AddInParameter(cmd, "CONFIRM_PRICE", DbType.Decimal, MakeDbNull(model.ConfirmPrice));
                db.AddInParameter(cmd, "LIST_DISCOUNT_RATIO", DbType.Decimal, MakeDbNull(model.ListDiscountRatio));
                db.AddInParameter(cmd, "APPLIED_DISCOUNT_RATIO", DbType.Decimal, MakeDbNull(model.AppliedDiscountRatio));
                db.AddInParameter(cmd, "PO_ORDER_LINE", DbType.Int64, MakeDbNull(model.POOrderLine));
                db.AddInParameter(cmd, "PO_QUANTITY", DbType.Decimal, MakeDbNull(model.POQuantity));
                db.AddInParameter(cmd, "PO_ORDER_NO", DbType.Int64, MakeDbNull(model.POOrderNo));
                db.AddInParameter(cmd, "SPECIAL_EXPLANATION", DbType.String, MakeDbNull(model.SpecialExplanation));
                db.AddInParameter(cmd, "STATUS_ID", DbType.Int32, model.StatusId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32,
                                  MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
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

        public SparePartSaleOrderDetailDetailModel GetSparePartSaleOrderDetail(UserInfo user, SparePartSaleOrderDetailDetailModel filter)
        {
            System.Data.Common.DbDataReader reader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_SPARE_PART_SALE_ORDER_DETAIL");
                db.AddInParameter(cmd, "SPARE_PART_SALE_ORDER_DETAIL_ID", DbType.Int32, MakeDbNull(filter.SparePartSaleOrderDetailId));
                db.AddInParameter(cmd, "SO_NUMBER", DbType.String, MakeDbNull(filter.SoNumber));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String,
                                  MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    filter.SoNumber = reader["SO_NUMBER"].GetValue<string>();
                    filter.SparePartSaleOrderDetailId = reader["SPARE_PART_SALE_ORDER_DETAIL_ID"].GetValue<int>();
                    filter.SparePartId = reader["ID_PART"].GetValue<int>();
                    filter.OrderQuantity = reader["ORDER_QUANTITY"].GetValue<decimal?>();
                    filter.PlannedQuantity = reader["PLANNED_QUANTITY"].GetValue<decimal?>();
                    filter.ShippedQuantity = reader["SHIPPED_QUANTITY"].GetValue<decimal?>();
                    filter.ListPrice = reader["LIST_PRICE"].GetValue<decimal?>();
                    filter.OrderPrice = reader["ORDER_PRICE"].GetValue<decimal?>();
                    filter.ConfirmPrice = reader["CONFIRM_PRICE"].GetValue<decimal?>();
                    filter.ListDiscountRatio = reader["LIST_DISCOUNT_RATIO"].GetValue<decimal?>();
                    filter.AppliedDiscountRatio = reader["APPLIED_DISCOUNT_RATIO"].GetValue<decimal?>();
                    filter.SparePartCode = reader["PART_CODE"].GetValue<string>();
                    filter.SparePartName = reader["PART_NAME"].GetValue<string>();
                    filter.StatusId = reader["STATUS_ID"].GetValue<int>();
                    filter.StatusName = reader["STATUS_NAME"].GetValue<string>();
                    filter.PoDetailSequenceNo = reader["PO_DET_SEQ_NO"].GetValue<string>();
                    filter.POQuantity = reader["PO_QUANTITY"].GetValue<decimal?>();
                    filter.POOrderNo = reader["PO_ORDER_NO"].GetValue<long?>();
                    filter.POOrderLine = reader["PO_ORDER_LINE"].GetValue<long?>();
                    filter.SpecialExplanation = reader["SPECIAL_EXPLANATION"].GetValue<string>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                    reader.Dispose();
                }
                CloseConnection();
            }
            return filter;
        }

        public List<SparePartSaleOrderDetailListModel> ListSparePartSaleOrderDetails(UserInfo user, SparePartSaleOrderDetailListModel filter,
                                                                           out int totalCnt)
        {
            DealerData dData = new DealerData();
            DealerViewModel dModel = dData.GetDealer(user, user.GetUserDealerId());
            string currencyCode = dData.GetCountryCurrencyCode(dModel.Country);

            var result = new List<SparePartSaleOrderDetailListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SPARE_PARTS_SALE_ORDER_DETAILS");
                db.AddInParameter(cmd, "SO_NUMBER", DbType.String, MakeDbNull(filter.SoNumber));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String,
                                  MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new SparePartSaleOrderDetailListModel
                        {
                            SoNumber = reader["SO_NUMBER"].GetValue<string>(),
                            SparePartSaleOrderDetailId = reader["SPARE_PART_SALE_ORDER_DETAIL_ID"].GetValue<int>(),
                            SparePartId = reader["ID_PART"].GetValue<long>(),
                            ChangedPartId = reader["CHANGED_PART_ID"].GetValue<long?>(),
                            OrderQuantity = reader["ORDER_QUANTITY"].GetValue<decimal?>(),
                            PlannedQuantity = reader["PLANNED_QUANTITY"].GetValue<decimal?>(),
                            ShippedQuantity = reader["SHIPPED_QUANTITY"].GetValue<decimal?>(),
                            ListPrice = reader["LIST_PRICE"].GetValue<decimal?>(),
                            OrderPrice = reader["ORDER_PRICE"].GetValue<decimal?>(),
                            ConfirmPrice = reader["CONFIRM_PRICE"].GetValue<decimal?>(),
                            ListDiscountRatio = reader["LIST_DISCOUNT_RATIO"].GetValue<decimal?>(),
                            AppliedDiscountRatio = reader["APPLIED_DISCOUNT_RATIO"].GetValue<decimal?>(),
                            SparePartCode = reader["PART_CODE"].GetValue<string>(),
                            SparePartName = reader["PART_NAME"].GetValue<string>(),
                            StatusId = reader["STATUS_ID"].GetValue<int>(),
                            StatusName = reader["STATUS_NAME"].GetValue<string>(),
                            PurchaseOrderDetailSeqNo = reader["PO_DET_SEQ_NO"].GetValue<string>(),
                            PoNumber = reader["PO_NUMBER"].GetValue<string>(),
                            CurrencyCode = reader["CURRENCY_CODE"].GetValue<string>(),
                            MasterStatusId = reader["MASTER_STATUS_ID"].GetValue<int>(),
                            IsOriginalPart = reader["IS_ORIGINAL_PART"].GetValue<bool>(),
                            StockQuantity = reader["STOCK_QUANTITY"].GetValue<decimal?>()
                        };

                        result.Add(listModel);
                    }
                    reader.Close();
                }
                totalCnt = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return result;
        }

        public List<SparePartSaleOrderDetailListModel> ListSparePartSaleOrderDetailsForSaleOrderDetails(UserInfo user, SparePartSaleOrderDetailListModel filter, out int totalCnt)
        {
            DealerData dData = new DealerData();
            DealerViewModel dModel = dData.GetDealer(user, user.GetUserDealerId());
            string currencyCode = dData.GetCountryCurrencyCode(dModel.Country);

            var result = new List<SparePartSaleOrderDetailListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SPARE_PARTS_SALE_ORDER_DETAILS_FOR_SALE_ORDER");
                db.AddInParameter(cmd, "SO_NUMBER", DbType.String, MakeDbNull(filter.SoNumber));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String,
                                  MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new SparePartSaleOrderDetailListModel
                        {
                            PartSaleId = reader["ID_PART_SALE"].GetValue<int?>(),
                            SparePartSaleWaybillId = reader["ID_SPARE_PART_SALE_WAYBILL"].GetValue<int?>(),
                            WaybillDate = reader["WAYBILL_DATE"].GetValue<DateTime?>(),
                            SparePartSaleInvoiceId = reader["ID_SPARE_PART_SALE_INVOICE"].GetValue<int?>(),
                            InvoiceDate = reader["INVOICE_DATE"].GetValue<DateTime?>(),
                            InvoiceNo = reader["INVOICE_NO"].GetValue<string>()
                        };

                        result.Add(listModel);
                    }
                    reader.Close();
                }
                totalCnt = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return result;
        }
    }
}

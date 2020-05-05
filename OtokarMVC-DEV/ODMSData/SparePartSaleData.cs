using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.Dealer;
using ODMSModel.SparePartSale;

namespace ODMSData
{
    public class SparePartSaleData : DataAccessBase
    {
        public List<SparePartSaleListModel> ListSparePartSales(UserInfo user,SparePartSaleListModel filter, out int totalCnt)
        {
            DealerData dData = new DealerData();
            DealerViewModel dModel = dData.GetDealer(user, user.GetUserDealerId());
            string currencyCode = dData.GetCountryCurrencyCode(dModel.Country);

            var retVal = new List<SparePartSaleListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SPARE_PART_SALES");
                db.AddInParameter(cmd, "ID_PART_SALE", DbType.Int32, MakeDbNull(filter.SparePartSaleId));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, user.GetUserDealerId());
                db.AddInParameter(cmd, "ID_CUSTOMER", DbType.Int32, MakeDbNull(filter.CustomerId));
                db.AddInParameter(cmd, "SALE_TYPE_ID", DbType.Int32, MakeDbNull(filter.SaleTypeId));
                db.AddInParameter(cmd, "SALE_STATUS_ID", DbType.Int32, MakeDbNull(filter.SaleStatusLookVal));
                db.AddInParameter(cmd, "INVOICE_NO", DbType.String, MakeDbNull(filter.InvoiceNo));
                db.AddInParameter(cmd, "INVOICE_SERIAL_NO", DbType.String, MakeDbNull(filter.InvoiceSerialNo));
                db.AddInParameter(cmd, "INVOICE_DATE", DbType.DateTime, MakeDbNull(filter.InvoiceDate));
                db.AddInParameter(cmd, "WAYBILL_NO", DbType.String, MakeDbNull(filter.WaybillNo));
                db.AddInParameter(cmd, "WAYBILL_SERIAL_NO", DbType.String, MakeDbNull(filter.WaybillSerialNo));
                db.AddInParameter(cmd, "WAYBILL_DATE", DbType.DateTime, MakeDbNull(filter.WaybillDate));
                db.AddInParameter(cmd, "PART_CODE", DbType.String, MakeDbNull(filter.PartCode));
                db.AddInParameter(cmd, "PART_NAME", DbType.String, MakeDbNull(filter.PartName));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
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
                        var sparePartListModel = new SparePartSaleListModel
                        {
                            CustomerName = reader["CUSTOMER_NAME"].GetValue<string>(),
                            SaleStatus = reader["SALE_STATUS_STRING"].GetValue<string>(),
                            SaleStatusLookVal = reader["SALE_STATUS_LOOKVAL"].GetValue<int>().ToString(),
                            SparePartSaleId = reader["ID_PART_SALE"].GetValue<int>(),
                            StockTypeId = reader["ID_STOCK_TYPE"].GetValue<int>(),
                            StockTypeName = reader["STOCK_TYPE_NAME"].GetValue<string>(),
                            SaleTypeId = reader["SALE_TYPE_ID"].GetValue<int?>(),
                            SaleTypeName = reader["SALE_TYPE_NAME"].GetValue<string>(),
                            DisplayCollectButton = reader["DISPLAY_COLLECT_BUTTON"].GetValue<bool>(),
                            DisplayCancelCollectButton = reader["DISPLAY_CANCEL_COLLECT_BUTTON"].GetValue<bool>(),
                            DisplayInvoiceButton = reader["DISPLAY_INVOICE_BUTTON"].GetValue<bool>(),
                            DisplayWaybillButton = reader["DISPLAY_WAYBILL_BUTTON"].GetValue<bool>(),
                            DisplayCancelButton = reader["DISPLAY_CANCEL_BUTTON"].GetValue<bool>(),
                            SparePartSaleWaybillId = reader["ID_SPARE_PART_SALE_WAYBILL"].GetValue<int?>(),
                            SaleDate = reader["SALE_DATE"].GetValue<DateTime?>(),
                            TotalListPrice = reader["TOTAL_LIST_PRICE"].GetValue<decimal?>(),
                            TotalPrice = reader["TOTAL_PRICE"].GetValue<decimal?>(),
                            WaybillDate = reader["WAYBILL_DATE"].GetValue<DateTime?>(),
                            WaybillNo = reader["WAYBILL_NO"].GetValue<string>(),
                            WaybillSerialNo = reader["WAYBILL_SERIAL_NO"].GetValue<string>(),
                            InvoiceNo = reader["INVOICE_NO"].GetValue<string>(),
                            InvoiceSerialNo = reader["INVOICE_SERIAL_NO"].GetValue<string>(),
                            InvoiceDate = reader["INVOICE_DATE"].GetValue<DateTime?>(),
                        };
                        retVal.Add(sparePartListModel);
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

            return retVal;
        }

        public void DMLSparePartSale(UserInfo user,SparePartSaleViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_SPARE_PART_SALE");
                db.AddParameter(cmd, "ID_PART_SALE", DbType.Int32, ParameterDirection.InputOutput, "ID_PART_SALE", DataRowVersion.Default, model.SparePartSaleId);
                db.AddInParameter(cmd, "CUSTOMER_TYPE_ID", DbType.Int32, model.CustomerTypeId);
                db.AddInParameter(cmd, "ID_DEALER ", DbType.Int32, user.GetUserDealerId());
                db.AddInParameter(cmd, "CUSTOMER_ID ", DbType.Int32, MakeDbNull(model.CustomerId));
                db.AddInParameter(cmd, "SALE_DATE ", DbType.DateTime, MakeDbNull(model.SaleDate));
                db.AddInParameter(cmd, "SALE_RESPONSIBLE ", DbType.String, MakeDbNull(model.SaleResponsible));
                db.AddInParameter(cmd, "SALE_TYPE_ID ", DbType.Int32, MakeDbNull(model.SaleTypeId));
                db.AddInParameter(cmd, "SALE_STATUS_ID ", DbType.Int32, MakeDbNull(model.SaleStatusLookVal));
                db.AddInParameter(cmd, "ID_VEHICLE", DbType.Int32, MakeDbNull(model.VehicleId));
                db.AddInParameter(cmd, "IS_RETURN ", DbType.Int32, MakeDbNull(model.IsReturn));
                db.AddInParameter(cmd, "PO_NUMBER ", DbType.Int64, MakeDbNull(model.PoNumber));
                db.AddInParameter(cmd, "ID_STOCK_TYPE ", DbType.Int32, MakeDbNull(model.StockTypeId));
                db.AddInParameter(cmd, "ID_PRICE_LIST ", DbType.Int32, MakeDbNull(model.PriceListId));
                db.AddInParameter(cmd, "VAT_EXCLUDE ", DbType.Int32, model.VatExclude);
                db.AddInParameter(cmd, "ID_SPARE_PART_SALE_WAYBILL ", DbType.Int32, MakeDbNull(model.SparePartSaleWaybillId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.SparePartSaleId = db.GetParameterValue(cmd, "ID_PART_SALE").GetValue<int>();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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
        public void DMLSparePartSaleOtokar(UserInfo user,OtokarSparePartSaleViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_OTOKAR_SPARE_PART_SALE");
                db.AddParameter(cmd, "ID_PART_SALE", DbType.Int32, ParameterDirection.InputOutput, "ID_PART_SALE", DataRowVersion.Default, model.SparePartSaleId);
                db.AddInParameter(cmd, "CUSTOMER_TYPE_ID", DbType.Int32, model.CustomerTypeId);
                db.AddInParameter(cmd, "ID_DEALER ", DbType.Int32, user.GetUserDealerId());
                db.AddInParameter(cmd, "CUSTOMER_ID ", DbType.Int32, MakeDbNull(model.CustomerId));
                db.AddInParameter(cmd, "SALE_DATE ", DbType.DateTime, MakeDbNull(model.SaleDate));
                db.AddInParameter(cmd, "SALE_RESPONSIBLE ", DbType.String, MakeDbNull(model.SaleResponsible));
                db.AddInParameter(cmd, "SALE_TYPE_ID ", DbType.Int32, MakeDbNull(model.SaleTypeId));
                db.AddInParameter(cmd, "SALE_STATUS_ID ", DbType.Int32, MakeDbNull(model.SaleStatusLookVal));
                db.AddInParameter(cmd, "ID_VEHICLE", DbType.Int32, MakeDbNull(model.VehicleId));
                db.AddInParameter(cmd, "IS_RETURN ", DbType.Int32, MakeDbNull(model.IsReturn));
                db.AddInParameter(cmd, "PO_NUMBER ", DbType.Int64, MakeDbNull(model.PoNumber));
                db.AddInParameter(cmd, "ID_STOCK_TYPE ", DbType.Int32, MakeDbNull(model.StockTypeId));
                db.AddInParameter(cmd, "ID_PRICE_LIST ", DbType.Int32, MakeDbNull(model.PriceListId));
                db.AddInParameter(cmd, "VAT_EXCLUDE ", DbType.Int32, model.VatExclude);
                db.AddInParameter(cmd, "ID_SPARE_PART_SALE_WAYBILL ", DbType.Int32, MakeDbNull(model.SparePartSaleWaybillId));
                db.AddInParameter(cmd, "INVOICE_DATE", DbType.DateTime, MakeDbNull(model.InvoiceDate));
                db.AddInParameter(cmd, "INVOICE_NO", DbType.String, MakeDbNull(model.InvoiceNo));
                db.AddInParameter(cmd, "INVOICE_SERIAL_NO", DbType.String, MakeDbNull(model.InvoiceSerialNo));
                db.AddInParameter(cmd, "ID_BILLING_ADDRESS", DbType.Int32, MakeDbNull(model.BillingAddressId));
                db.AddInParameter(cmd, "ID_SHIPPING_ADDRESS", DbType.String, MakeDbNull(model.ShippingAddressId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.SparePartSaleId = db.GetParameterValue(cmd, "ID_PART_SALE").GetValue<int>();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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
        public SparePartSaleViewModel GetSparePartSale(UserInfo user,int id)
        {
            DealerData dData = new DealerData();
            DealerViewModel dModel = dData.GetDealer(user, user.GetUserDealerId());
            string currencyCode = dData.GetCountryCurrencyCode(dModel.Country);

            var retVal = new SparePartSaleViewModel { SparePartSaleId = id };
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_SPARE_PART_SALE");
                db.AddInParameter(cmd, "ID_PART_SALE", DbType.Int32, MakeDbNull(id));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        retVal.CustomerTypeId = reader["CUSTOMER_TYPE_ID"].GetValue<int?>();
                        retVal.DealerId = reader["ID_DEALER"].GetValue<int>();
                        retVal.DealerName = reader["DEALER_NAME"].GetValue<string>();
                        retVal.SparePartSaleWaybillId = reader["SPARE_PART_SALE_WAYBILL_ID"].GetValue<int>();
                        retVal.CustomerId = reader["CUSTOMER_ID"].GetValue<int>();
                        retVal.CustomerName = reader["CUSTOMER_NAME"].GetValue<string>();
                        retVal.SaleDate = reader["SALE_DATE"].GetValue<DateTime?>();
                        retVal.SaleResponsible = reader["SALE_RESPONSIBLE"].GetValue<string>();
                        retVal.SaleStatusLookKey = reader["SALE_STATUS_LOOKKEY"].GetValue<int>();
                        retVal.SaleStatusLookVal = reader["SALE_STATUS_LOOKVAL"].GetValue<string>();
                        retVal.CustomerType = reader["CUSTOMER_TYPE"].GetValue<string>();
                        retVal.CurrencyCode = currencyCode;
                        retVal.SaleTypeId = reader["SALE_TYPE_ID"].GetValue<int>();
                        retVal.SaleTypeName = reader["SALE_TYPE_NAME"].GetValue<string>();
                        retVal.IsReturn = reader["IS_RETURN"].GetValue<bool?>();
                        retVal.IsReturnName = reader["IS_RETURN_NAME"].GetValue<string>();
                        retVal.VehicleId = reader["VEHICLE_ID"].GetValue<int?>();
                        retVal.VehicleName = reader["VEHICLE_NAME"].GetValue<string>();
                        retVal.PoNumber = reader["PO_NUMBER"].GetValue<long>();
                        retVal.StockTypeId = reader["ID_STOCK_TYPE"].GetValue<int?>();
                        retVal.StockTypeName = reader["STOCK_TYPE_NAME"].GetValue<string>();
                        retVal.VatExclude = reader["VAT_EXCLUDE"].GetValue<int?>();
                        retVal.PriceListId = reader["ID_PRICE_LIST"].GetValue<int?>();
                        retVal.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                        retVal.PoNumber = reader["PO_NUMBER"].GetValue<long>();
                        if (retVal.ErrorNo > 0)
                        {
                            retVal.ErrorMessage =
                                ResolveDatabaseErrorXml(db.GetParameterValue(cmd, "ERROR_DESC").ToString());
                        }
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
        public OtokarSparePartSaleViewModel GetSparePartSaleOtokar(UserInfo user,int id)
        {
            DealerData dData = new DealerData();
            DealerViewModel dModel = dData.GetDealer(user, user.GetUserDealerId());
            string currencyCode = dData.GetCountryCurrencyCode(dModel.Country);

            var retVal = new OtokarSparePartSaleViewModel { SparePartSaleId = id };
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_OTOKAR_SPARE_PART_SALE");
                db.AddInParameter(cmd, "ID_PART_SALE", DbType.Int32, MakeDbNull(id));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        retVal.CustomerTypeId = reader["CUSTOMER_TYPE_ID"].GetValue<int?>();
                        retVal.DealerId = reader["ID_DEALER"].GetValue<int>();
                        retVal.DealerName = reader["DEALER_NAME"].GetValue<string>();
                        retVal.SparePartSaleWaybillId = reader["SPARE_PART_SALE_WAYBILL_ID"].GetValue<int>();
                        retVal.CustomerId = reader["CUSTOMER_ID"].GetValue<int>();
                        retVal.CustomerName = reader["CUSTOMER_NAME"].GetValue<string>();
                        retVal.SaleDate = reader["SALE_DATE"].GetValue<DateTime?>();
                        retVal.SaleResponsible = reader["SALE_RESPONSIBLE"].GetValue<string>();
                        retVal.SaleStatusLookKey = reader["SALE_STATUS_LOOKKEY"].GetValue<int>();
                        retVal.SaleStatusLookVal = reader["SALE_STATUS_LOOKVAL"].GetValue<string>();
                        retVal.CustomerType = reader["CUSTOMER_TYPE"].GetValue<string>();
                        retVal.CurrencyCode = currencyCode;
                        retVal.SaleTypeId = reader["SALE_TYPE_ID"].GetValue<int>();
                        retVal.SaleTypeName = reader["SALE_TYPE_NAME"].GetValue<string>();
                        retVal.IsReturn = reader["IS_RETURN"].GetValue<bool?>();
                        retVal.IsReturnName = reader["IS_RETURN_NAME"].GetValue<string>();
                        retVal.VehicleId = reader["VEHICLE_ID"].GetValue<int?>();
                        retVal.VehicleName = reader["VEHICLE_NAME"].GetValue<string>();
                        retVal.PoNumber = reader["PO_NUMBER"].GetValue<long>();
                        retVal.StockTypeId = reader["ID_STOCK_TYPE"].GetValue<int?>();
                        retVal.StockTypeName = reader["STOCK_TYPE_NAME"].GetValue<string>();
                        retVal.VatExclude = reader["VAT_EXCLUDE"].GetValue<int?>();
                        retVal.PriceListId = reader["ID_PRICE_LIST"].GetValue<int?>();
                        retVal.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                        retVal.PoNumber = reader["PO_NUMBER"].GetValue<long>();
                        retVal.BillingAddressId = reader["ID_BILLING_ADDRESS"].GetValue<int?>();
                        retVal.ShippingAddressId = reader["ID_SHIPPING_ADDRESS"].GetValue<int?>();
                        retVal.InvoiceDate = reader["INVOICE_DATE"].GetValue<DateTime?>();
                        retVal.InvoiceSerialNo = reader["INVOICE_SERIAL_NO"].GetValue<string>();
                        retVal.InvoiceNo = reader["INVOICE_NO"].GetValue<string>();
                        retVal.SparePartSaleWaybillId = reader["ID_SPARE_PART_SALE_WAYBILL"].GetValue<int?>();
                        if (retVal.ErrorNo > 0)
                        {
                            retVal.ErrorMessage =
                                ResolveDatabaseErrorXml(db.GetParameterValue(cmd, "ERROR_DESC").ToString());
                        }
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
        public string ExecInvoiceOp(UserInfo user,int sparePartSaleId)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_UPDATE_SPARE_PART_SALE_INVOICE_OP_FOR_DELIVERY");
                db.AddInParameter(cmd, "ID_SPARE_PART_SALE", DbType.Int64, MakeDbNull(sparePartSaleId));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.String, MakeDbNull(user.UserId));
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                CloseConnection();
            }
            return null;
        }
        public string ExecInvoiceOpMultiple(UserInfo user, int sparePartSaleWaybillId)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_UPDATE_SPARE_PART_SALE_INVOICE_OP_FOR_DELIVERY_MULTIPLE");
                db.AddInParameter(cmd, "ID_SPARE_PART_SALE_WAYBILL", DbType.Int64, MakeDbNull(sparePartSaleWaybillId));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.String, MakeDbNull(user.UserId));
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                CloseConnection();
            }
            return null;
        }
        public string ChangeStatusAfterPickCancel(UserInfo user,int sparePartSaleId)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_CHANGE_SPARE_PART_SALE_STATUS_AFTER_PICK_CANCEL");
                db.AddInParameter(cmd, "ID_SPARE_PART_SALE", DbType.Int64, MakeDbNull(sparePartSaleId));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.String, MakeDbNull(user.UserId));
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                CloseConnection();
            }
            return null;
        }
        public List<SparePartSaleListModel> ListSparePartSaleWaybill(UserInfo user,int sparePartSaleId)
        {
            DealerData dData = new DealerData();
            var retVal = new List<SparePartSaleListModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SPARE_PART_SALE_CUSTOMER");
                db.AddInParameter(cmd, "ID_SPARE_PART_SALE", DbType.Int32, MakeDbNull(sparePartSaleId));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, user.GetUserDealerId());
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var sparePartListModel = new SparePartSaleListModel
                        {
                            IsSelected = reader["IsSelected"].GetValue<bool>(),
                            SparePartSaleId = reader["SparePartSaleId"].GetValue<int>(),
                            SaleDate = reader["SaleDate"].GetValue<DateTime?>(),
                            TotalListPrice = reader["TotalListPrice"].GetValue<decimal?>(),
                            TotalPrice = reader["TotalPrice"].GetValue<decimal?>()
                        };
                        retVal.Add(sparePartListModel);
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
    }
}

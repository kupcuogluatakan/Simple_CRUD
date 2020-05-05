using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.Dealer;
using ODMSModel.SparePartSaleWaybill;

namespace ODMSData
{
    public class SparePartSaleWaybillData : DataAccessBase
    {
        public List<SparePartSaleWaybillListModel> ListSparePartSaleWaybills(UserInfo user, SparePartSaleWaybillListModel filter, out int totalCnt)
        {
            DealerData dData = new DealerData();
            DealerViewModel dModel = dData.GetDealer(user, user.GetUserDealerId());
            string currencyCode = dData.GetCountryCurrencyCode(dModel.Country);

            var retVal = new List<SparePartSaleWaybillListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SPARE_PART_SALE_WAYBILLS");
                db.AddInParameter(cmd, "ID_SPARE_PART_SALE_WAYBILL", DbType.Int32, MakeDbNull(filter.SparePartSaleWaybillId));
                db.AddInParameter(cmd, "WAYBILL_REFERENCE_NO", DbType.String, MakeDbNull(filter.WaybillReferenceNo));
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
                        var sparePartListModel = new SparePartSaleWaybillListModel
                        {
                            SparePartSaleWaybillId = reader["ID_SPARE_PART_SALE_WAYBILL"].GetValue<int>(),
                            DealerId = reader["ID_DEALER"].GetValue<int>(),
                            CustomerId = reader["ID_CUSTOMER"].GetValue<int>(),
                            WaybillSerialNo = reader["WAYBILL_SERIAL_NO"].GetValue<string>(),
                            WaybillNo = reader["WAYBILL_NO"].GetValue<string>(),
                            WaybillReferenceNo = reader["WAYBILL_REFERENCE_NO"].GetValue<string>(),
                            ShippingAddressId = reader["ID_SHIPPING_ADDRESS"].GetValue<int>(),
                            DeliveryId = reader["ID_DELIVERY"].GetValue<int>(),
                            SparePartSaleInvoiceId = reader["ID_SPARE_PART_SALE_INVOICE"].GetValue<long?>(),
                            CustomerName = reader["CUSTOMER_NAME"].GetValue<string>(),
                            CustomerLastName = reader["CUSTOMER_LAST_NAME"].GetValue<string>(),
                            CustomerAddressCountryText = reader["CUSTOMER_ADDRESS_COUNTRY_TEXT"].GetValue<string>(),
                            CustomerAddressCityText = reader["CUSTOMER_ADDRESS_CITY_TEXT"].GetValue<string>(),
                            CustomerAddressTownText = reader["CUSTOMER_ADDRESS_TOWN_TEXT"].GetValue<string>(),
                            CustomerAddressZipCode = reader["CUSTOMER_ADDRESS_ZIP_CODE"].GetValue<string>(),
                            CustomerAddress1 = reader["CUSTOMER_ADDRESS_1"].GetValue<string>(),
                            CustomerAddress2 = reader["CUSTOMER_ADDRESS_2"].GetValue<string>(),
                            CustomerAddress3 = reader["CUSTOMER_ADDRESS_3"].GetValue<string>(),
                            CustomerTaxOffice = reader["CUSTOMER_TAX_OFFICE"].GetValue<string>(),
                            CustomeTaxNo = reader["CUSTOMER_TAX_NO"].GetValue<string>(),
                            CustomerTCIdentity = reader["CUSTOMER_TC_IDENTITY"].GetValue<string>(),
                            CustomerPassportNo = reader["CUSTOMER_PASSPORT_NO"].GetValue<string>(),
                            SparePartSaleIdList = reader["SPARE_PART_SALE_IDS"].GetValue<string>(),
                            IsActive = reader["IS_ACTIVE"].GetValue<bool>(),
                            IsActiveString = reader["IS_ACTIVE_NAME"].GetValue<string>()
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

        public void DMLSparePartSaleWaybill(UserInfo user, SparePartSaleWaybillViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_SPARE_PART_SALE_WAYBILL");
                db.AddParameter(cmd, "ID_SPARE_PART_SALE_WAYBILL",
                    DbType.Int32, ParameterDirection.InputOutput, "ID_SPARE_PART_SALE_WAYBILL",
                    DataRowVersion.Default, model.SparePartSaleWaybillId);
                db.AddInParameter(cmd, "ID_DEALER ", DbType.Int32,user.GetUserDealerId());
                db.AddInParameter(cmd, "WAYBILL_SERIAL_NO ", DbType.String, MakeDbNull(model.WaybillSerialNo));
                db.AddInParameter(cmd, "WAYBILL_NO ", DbType.String, MakeDbNull(model.WaybillNo));
                db.AddInParameter(cmd, "WAYBILL_DATE ", DbType.DateTime, MakeDbNull(model.WaybillDate));
                db.AddInParameter(cmd, "WAYBILL_REFERENCE_NO ", DbType.String, MakeDbNull(model.WaybillReferenceNo));
                db.AddInParameter(cmd, "ID_SHIPPING_ADDRESS ", DbType.Int32, MakeDbNull(model.ShippingAddressId));
                db.AddInParameter(cmd, "ID_CUSTOMER ", DbType.Int64, MakeDbNull(model.CustomerId));
                db.AddInParameter(cmd, "ID_DELIVERY ", DbType.Int64, MakeDbNull(model.DeliveryId));
                db.AddInParameter(cmd, "ID_SPARE_PART_SALE_INVOICE ", DbType.Int64, MakeDbNull(model.SparePartSaleInvoiceId));
                db.AddInParameter(cmd, "CUSTOMER_NAME ", DbType.String, model.CustomerName);
                db.AddInParameter(cmd, "CUSTOMER_LAST_NAME ", DbType.String, model.CustomerLastName);
                db.AddInParameter(cmd, "CUSTOMER_ADDRESS_COUNTRY_TEXT", DbType.String, model.CustomerAddressCountryText);
                db.AddInParameter(cmd, "CUSTOMER_ADDRESS_CITY_TEXT ", DbType.String, model.CustomerAddressCityText);
                db.AddInParameter(cmd, "CUSTOMER_ADDRESS_TOWN_TEXT ", DbType.String, model.CustomerAddressTownText);
                db.AddInParameter(cmd, "CUSTOMER_ADDRESS_ZIP_CODE ", DbType.String, model.CustomerAddressZipCode);
                db.AddInParameter(cmd, "CUSTOMER_ADDRESS_1 ", DbType.String, model.CustomerAddress1);
                db.AddInParameter(cmd, "CUSTOMER_ADDRESS_2 ", DbType.String, model.CustomerAddress2);
                db.AddInParameter(cmd, "CUSTOMER_ADDRESS_3 ", DbType.String, model.CustomerAddress3);
                db.AddInParameter(cmd, "CUSTOMER_TAX_OFFICE ", DbType.String, model.CustomerTaxOffice);
                db.AddInParameter(cmd, "CUSTOMER_TAX_NO ", DbType.String, model.CustomeTaxNo);
                db.AddInParameter(cmd, "CUSTOMER_TC_IDENTITY ", DbType.String, model.CustomerTCIdentity);
                db.AddInParameter(cmd, "CUSTOMER_PASSPORT_NO ", DbType.String, model.CustomerPassportNo);
                db.AddInParameter(cmd, "SPARE_PART_SALE_IDS ", DbType.String, model.SparePartSaleIdList);
                db.AddInParameter(cmd, "IS_ACTIVE ", DbType.Int16, model.IsActive);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.SparePartSaleWaybillId = db.GetParameterValue(cmd, "ID_SPARE_PART_SALE_WAYBILL").GetValue<int>();
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

        public SparePartSaleWaybillViewModel GetSparePartSaleWaybill(UserInfo user, int id)
        {
            DealerData dData = new DealerData();
            DealerViewModel dModel = dData.GetDealer(user, user.GetUserDealerId());
            string currencyCode = dData.GetCountryCurrencyCode(dModel.Country);

            var retVal = new SparePartSaleWaybillViewModel { SparePartSaleWaybillId = id };
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_SPARE_PART_SALE_WAYBILL");
                db.AddInParameter(cmd, "ID_SPARE_PART_SALE_WAYBILL", DbType.Int32, MakeDbNull(id));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        retVal.SparePartSaleWaybillId = reader["ID_SPARE_PART_SALE_WAYBILL"].GetValue<int>();
                        retVal.DealerId = reader["ID_DEALER"].GetValue<int>();
                        retVal.CustomerId = reader["ID_CUSTOMER"].GetValue<int>();
                        retVal.WaybillSerialNo = reader["WAYBILL_SERIAL_NO"].GetValue<string>();
                        retVal.WaybillNo = reader["WAYBILL_NO"].GetValue<string>();
                        retVal.WaybillDate = reader["WAYBILL_date"].GetValue<DateTime>();
                        retVal.WaybillReferenceNo = reader["WAYBILL_REFERENCE_NO"].GetValue<string>();
                        retVal.ShippingAddressId = reader["ID_SHIPPING_ADDRESS"].GetValue<int>();
                        retVal.DeliveryId = reader["ID_DELIVERY"].GetValue<int>();
                        retVal.SparePartSaleInvoiceId = reader["ID_SPARE_PART_SALE_INVOICE"].GetValue<long?>();
                        retVal.CustomerName = reader["CUSTOMER_NAME"].GetValue<string>();
                        retVal.CustomerLastName = reader["CUSTOMER_LAST_NAME"].GetValue<string>();
                        retVal.CustomerAddressCountryText = reader["CUSTOMER_ADDRESS_COUNTRY_TEXT"].GetValue<string>();
                        retVal.CustomerAddressCityText = reader["CUSTOMER_ADDRESS_CITY_TEXT"].GetValue<string>();
                        retVal.CustomerAddressTownText = reader["CUSTOMER_ADDRESS_TOWN_TEXT"].GetValue<string>();
                        retVal.CustomerAddressZipCode = reader["CUSTOMER_ADDRESS_ZIP_CODE"].GetValue<string>();
                        retVal.CustomerAddress1 = reader["CUSTOMER_ADDRESS_1"].GetValue<string>();
                        retVal.CustomerAddress2 = reader["CUSTOMER_ADDRESS_2"].GetValue<string>();
                        retVal.CustomerAddress3 = reader["CUSTOMER_ADDRESS_3"].GetValue<string>();
                        retVal.CustomerTaxOffice = reader["CUSTOMER_TAX_OFFICE"].GetValue<string>();
                        retVal.CustomeTaxNo = reader["CUSTOMER_TAX_NO"].GetValue<string>();
                        retVal.CustomerTCIdentity = reader["CUSTOMER_TC_IDENTITY"].GetValue<string>();
                        retVal.CustomerPassportNo = reader["CUSTOMER_PASSPORT_NO"].GetValue<string>();
                        retVal.SparePartSaleIdList = reader["SPARE_PART_SALE_IDS"].GetValue<string>();
                        retVal.IsActive = reader["IS_ACTIVE"].GetValue<bool>();
                        retVal.IsActiveString = reader["IS_ACTIVE_NAME"].GetValue<string>();
                    }
                    reader.Close();
                }

                retVal.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                retVal.ErrorMessage = ResolveDatabaseErrorXml(db.GetParameterValue(cmd, "ERROR_DESC").ToString());
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

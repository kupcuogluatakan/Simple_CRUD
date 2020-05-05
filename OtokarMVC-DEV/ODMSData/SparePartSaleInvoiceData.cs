using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.Dealer;
using ODMSModel.SparePartSaleInvoice;

namespace ODMSData
{
    public class SparePartSaleInvoiceData : DataAccessBase
    {
        public List<SparePartSaleInvoiceListModel> ListSparePartSaleInvoices(UserInfo user,SparePartSaleInvoiceListModel filter, out int totalCnt)
        {
            DealerData dData = new DealerData();
            DealerViewModel dModel = dData.GetDealer(user, user.GetUserDealerId());
            string currencyCode = dData.GetCountryCurrencyCode(dModel.Country);

            var retVal = new List<SparePartSaleInvoiceListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SPARE_PART_SALE_INVOICES");
                db.AddInParameter(cmd, "ID_SPARE_PART_SALE_INVOICE", DbType.Int32, MakeDbNull(filter.SparePartSaleInvoiceId));
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
                        var sparePartListModel = new SparePartSaleInvoiceListModel
                        {
                            SparePartSaleInvoiceId = reader["ID_PART_SALE"].GetValue<int>(),
                            DealerId = reader["ID_DEALER"].GetValue<int>(),
                            CustomerId = reader["ID_CUSTOMER"].GetValue<int>(),
                            InvoiceSerialNo = reader["INVOICE_SERIAL_NO"].GetValue<string>(),
                            InvoiceNo = reader["INVOICE_NO"].GetValue<string>(),
                            DueDuration = reader["DUE_DURATION"].GetValue<int>(),
                            PaymentTypeId = reader["ID_PAYMENT_TYPE"].GetValue<int>(),
                            BankId = reader["ID_BANK"].GetValue<int>(),
                            InstalmentNumber = reader["INSTALMENT_NUMBER"].GetValue<short>(),
                            PayAmount = reader["PAY_AMOUNT"].GetValue<decimal>(),
                            TransmitNo = reader["TRANSMIT_NO"].GetValue<string>(),
                            CustomerName = reader["CUSTOMER_NAME"].GetValue<string>(),
                            CustomerLastName = reader["CUSTOMER_LAST_NAME"].GetValue<string>(),
                            SparePartSaleWaybillIdList = reader["SPARE_PART_SALE_WAYBILL_IDS"].GetValue<string>(),
                            BillingAddressId = reader["ID_BILLING_ADDRESS"].GetValue<int>(),
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

        public void DMLSparePartSaleInvoice(UserInfo user,SparePartSaleInvoiceViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_SPARE_PART_SALE_INVOICE");
                db.AddParameter(cmd, "ID_SPARE_PART_SALE_INVOICE",
                    DbType.Int32, ParameterDirection.InputOutput, "ID_SPARE_PART_SALE_INVOICE",
                    DataRowVersion.Default, model.SparePartSaleInvoiceId);
                db.AddInParameter(cmd, "ID_DEALER ", DbType.Int32, user.GetUserDealerId());
                db.AddInParameter(cmd, "ID_CUSTOMER ", DbType.Int64, MakeDbNull(model.CustomerId));
                db.AddInParameter(cmd, "INVOICE_SERIAL_NO ", DbType.String, MakeDbNull(model.InvoiceSerialNo));
                db.AddInParameter(cmd, "INVOICE_NO ", DbType.String, MakeDbNull(model.InvoiceNo));
                db.AddInParameter(cmd, "INVOICE_DATE ", DbType.DateTime, MakeDbNull(model.InvoiceDate));
                db.AddInParameter(cmd, "DUE_DURATION ", DbType.Int32, MakeDbNull(model.DueDuration));
                db.AddInParameter(cmd, "ID_PAYMENT_TYPE ", DbType.Int32, MakeDbNull(model.PaymentTypeId));
                db.AddInParameter(cmd, "ID_BANK ", DbType.Int32, MakeDbNull(model.BankId));
                db.AddInParameter(cmd, "INSTALMENT_NUMBER ", DbType.Int16, MakeDbNull(model.InstalmentNumber));
                db.AddInParameter(cmd, "PAY_AMOUNT ", DbType.Decimal, MakeDbNull(model.PayAmount));
                db.AddInParameter(cmd, "TRANSMIT_NO ", DbType.String, MakeDbNull(model.TransmitNo));
                db.AddInParameter(cmd, "CUSTOMER_NAME ", DbType.String, MakeDbNull(model.CustomerName));
                db.AddInParameter(cmd, "CUSTOMER_LAST_NAME ", DbType.String, MakeDbNull(model.CustomerLastName));
                db.AddInParameter(cmd, "SPARE_PART_SALE_WAYBILL_IDS ", DbType.String, model.SparePartSaleWaybillIdList);
                db.AddInParameter(cmd, "ID_BILLING_ADDRESS ", DbType.Int32, MakeDbNull(model.BillingAddressId));
                db.AddInParameter(cmd, "CUSTOMER_ADDRESS_COUNTRY_TEXT", DbType.String, MakeDbNull(model.CustomerAddressCountryText));
                db.AddInParameter(cmd, "CUSTOMER_ADDRESS_CITY_TEXT ", DbType.String, MakeDbNull(model.CustomerAddressCityText));
                db.AddInParameter(cmd, "CUSTOMER_ADDRESS_TOWN_TEXT ", DbType.String, MakeDbNull(model.CustomerAddressTownText));
                db.AddInParameter(cmd, "CUSTOMER_ADDRESS_ZIP_CODE ", DbType.String, MakeDbNull(model.CustomerAddressZipCode));
                db.AddInParameter(cmd, "CUSTOMER_ADDRESS_1 ", DbType.String, MakeDbNull(model.CustomerAddress1));
                db.AddInParameter(cmd, "CUSTOMER_ADDRESS_2 ", DbType.String, model.CustomerAddress2);
                db.AddInParameter(cmd, "CUSTOMER_ADDRESS_3 ", DbType.String, model.CustomerAddress3);
                db.AddInParameter(cmd, "CUSTOMER_TAX_OFFICE ", DbType.String, model.CustomerTaxOffice);
                db.AddInParameter(cmd, "CUSTOMER_TAX_NO ", DbType.String, model.CustomeTaxNo);
                db.AddInParameter(cmd, "CUSTOMER_TC_IDENTITY ", DbType.String, model.CustomerTCIdentity);
                db.AddInParameter(cmd, "CUSTOMER_PASSPORT_NO ", DbType.String, model.CustomerPassportNo);
                db.AddInParameter(cmd, "IS_ACTIVE ", DbType.Int16, model.IsActive);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.SparePartSaleInvoiceId = db.GetParameterValue(cmd, "ID_SPARE_PART_SALE_INVOICE").GetValue<int>();
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

        public SparePartSaleInvoiceViewModel GetSparePartSaleInvoice(UserInfo user,int id)
        {
            DealerData dData = new DealerData();
            DealerViewModel dModel = dData.GetDealer(user, user.GetUserDealerId());
            string currencyCode = dData.GetCountryCurrencyCode(dModel.Country);

            var retVal = new SparePartSaleInvoiceViewModel { SparePartSaleInvoiceId = id };
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_SPARE_PART_SALE_INVOICE");
                db.AddInParameter(cmd, "ID_SPARE_PART_SALE_INVOICE", DbType.Int32, MakeDbNull(id));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        retVal.SparePartSaleInvoiceId = reader["ID_SPARE_PART_SALE_INVOICE"].GetValue<int>();
                        retVal.DealerId = reader["ID_DEALER"].GetValue<int>();
                        retVal.CustomerId = reader["ID_CUSTOMER"].GetValue<int>();
                        retVal.InvoiceSerialNo = reader["INVOICE_SERIAL_NO"].GetValue<string>();
                        retVal.InvoiceDate = reader["INVOICE_DATE"].GetValue<DateTime?>();
                        retVal.InvoiceNo = reader["INVOICE_NO"].GetValue<string>();
                        retVal.DueDuration = reader["DUE_DURATION"].GetValue<int>();
                        retVal.PaymentTypeId = reader["ID_PAYMENT_TYPE"].GetValue<int>();
                        retVal.BankId = reader["ID_BANK"].GetValue<int>();
                        retVal.InstalmentNumber = reader["INSTALMENT_NUMBER"].GetValue<short>();
                        retVal.PayAmount = reader["PAY_AMOUNT"].GetValue<decimal>();
                        retVal.TransmitNo = reader["TRANSMIT_NO"].GetValue<string>();
                        retVal.CustomerName = reader["CUSTOMER_NAME"].GetValue<string>();
                        retVal.CustomerLastName = reader["CUSTOMER_LAST_NAME"].GetValue<string>();
                        retVal.SparePartSaleWaybillIdList = reader["SPARE_PART_SALE_WAYBILL_IDS"].GetValue<string>();
                        retVal.BillingAddressId = reader["ID_BILLING_ADDRESS"].GetValue<int>();
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

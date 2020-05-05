using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;
using ODMSCommon;
using ODMSModel.Customer;
using ODMSModel.ListModel;
using ODMSCommon.Security;

namespace ODMSData
{
    public class CustomerData : DataAccessBase
    {
        public List<SelectListItem> ListCustomerAsSelectListItem(UserInfo user)
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CUSTOMERS");
                db.AddInParameter(cmd, "CUSTOMER_NAME", DbType.String, DBNull.Value);
                db.AddInParameter(cmd, "DEALER_ID", DbType.String, DBNull.Value);
                db.AddInParameter(cmd, "MOBILE_NO", DbType.String, DBNull.Value);
                db.AddInParameter(cmd, "COUNTRY_ID", DbType.Int32, DBNull.Value);
                db.AddInParameter(cmd, "CUSTOMER_TYPE_ID", DbType.Int32, DBNull.Value);
                db.AddInParameter(cmd, "WITHOLDING_STATUS", DbType.Int32, DBNull.Value);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "TC_IDENTITY_NO", DbType.String, DBNull.Value);
                db.AddInParameter(cmd, "TAX_NO", DbType.String, DBNull.Value);
                db.AddInParameter(cmd, "PASSPORT_NO", DbType.String, DBNull.Value);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, DBNull.Value);
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, DBNull.Value);
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, DBNull.Value);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, DBNull.Value);
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 400);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["CUSTOMER_ID"].GetValue<string>(),
                            Text = reader["CUSTOMER_FULL_NAME"].GetValue<string>()
                        };
                        retVal.Add(lookupItem);
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

        public List<CustomerListModel> ListCustomers(UserInfo user, CustomerListModel filter, out int totalCount)
        {
            var retVal = new List<CustomerListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CUSTOMERS");
                db.AddInParameter(cmd, "CUSTOMER_NAME", DbType.String, MakeDbNull(filter.CustomerName));
                db.AddInParameter(cmd, "DEALER_ID", DbType.String, MakeDbNull(filter.DealerId));
                db.AddInParameter(cmd, "MOBILE_NO", DbType.String, MakeDbNull(filter.MobileNo));
                db.AddInParameter(cmd, "COUNTRY_ID", DbType.Int32, MakeDbNull(filter.CountryId));
                db.AddInParameter(cmd, "CUSTOMER_TYPE_ID", DbType.Int32, MakeDbNull(filter.CustomerTypeId));
                db.AddInParameter(cmd, "WITHOLDING_STATUS", DbType.Int32, filter.WitholdingStatus);
                db.AddInParameter(cmd, "TC_IDENTITY_NO", DbType.String, MakeDbNull(filter.TcIdentityNo));
                db.AddInParameter(cmd, "TAX_NO", DbType.String, MakeDbNull(filter.TaxNo));
                db.AddInParameter(cmd, "PASSPORT_NO", DbType.String, MakeDbNull(filter.PassportNo));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var customerListModel = new CustomerListModel
                        {
                            CustomerId = reader["CUSTOMER_ID"].GetValue<int>(),
                            CustomerName = reader["CUSTOMER_FULL_NAME"].GetValue<string>(),
                            SAPCustomerSSID = reader["SAP_CUSTOMER_SSID"].GetValue<string>(),
                            BOSCustomerSSID = reader["BOS_CUSTOMER_SSID"].GetValue<string>(),
                            CompanyTypeId = reader["COMPANY_TYPE_ID"].GetValue<int?>(),
                            CompanyTypeName = reader["COMPANY_TYPE_NAME"].GetValue<string>(),
                            CountryId = reader["COUNTRY_ID"].GetValue<int?>(),
                            CountryName = reader["COUNTRY_NAME"].GetValue<string>(),
                            CustomerTypeId = reader["CUSTOMER_TYPE_ID"].GetValue<int?>(),
                            CustomerTypeName = reader["CUSTOMER_TYPE_NAME"].GetValue<string>(),
                            GovernmentTypeId = reader["GOVERNMENT_TYPE_ID"].GetValue<int?>(),
                            GovernmentTypeName = reader["GOVERNMENT_TYPE_NAME"].GetValue<string>(),
                            IsActive = reader["IS_ACTIVE"].GetValue<int?>(),
                            IsActiveName = reader["IS_ACTIVE_NAME"].GetValue<string>(),
                            DealerId = reader["DEALER_ID"].GetValue<int?>(),
                            TaxNo = reader["TAX_NO"].GetValue<string>(),
                            TaxOffice = reader["TAX_OFFICE"].GetValue<string>(),
                            TcIdentityNo = reader["TC_IDENTITY_NO"].GetValue<string>(),
                            PassportNo = reader["PASSPORT_NO"].GetValue<string>(),
                            WitholdingStatusName = reader["WITHOLDING_STATUS_NAME"].GetValue<string>()
                        };
                        retVal.Add(customerListModel);
                    }
                    reader.Close();
                }
                totalCount = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
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

        public void DMLCustomer(UserInfo user, CustomerIndexViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_CUSTOMER_MAIN");
                db.AddParameter(cmd, "CUSTOMER_ID", DbType.Int32, ParameterDirection.InputOutput, "CUSTOMER_ID", DataRowVersion.Default, model.CustomerId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "CUSTOMER_NAME", DbType.String, MakeDbNull(model.CustomerName));
                db.AddInParameter(cmd, "SAP_CUSTOMER_SSID", DbType.String, MakeDbNull(model.SAPCustomerSSID));
                db.AddInParameter(cmd, "BOS_CUSTOMER_SSID", DbType.String, MakeDbNull(model.BOSCustomerSSID));
                db.AddInParameter(cmd, "CUSTOMER_LAST_NAME", DbType.String, MakeDbNull(model.CustomerLastName));
                db.AddInParameter(cmd, "COMPANY_TYPE_ID", DbType.Int32, MakeDbNull(model.CompanyTypeId));
                db.AddInParameter(cmd, "COUNTRY_ID", DbType.Int32, MakeDbNull(model.CountryId));
                db.AddInParameter(cmd, "CUSTOMER_TYPE_ID", DbType.Int32, MakeDbNull(model.CustomerTypeId));
                db.AddInParameter(cmd, "GOVERNMENT_TYPE_ID", DbType.Int32, MakeDbNull(model.GovernmentTypeId));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, model.IsActive);
                db.AddInParameter(cmd, "MOBILE_NO", DbType.String, MakeDbNull(model.MobileNo));
                db.AddInParameter(cmd, "PASSPORT_NO", DbType.String, MakeDbNull(model.PassportNo));
                db.AddInParameter(cmd, "TAX_NO", DbType.String, MakeDbNull(model.TaxNo));
                db.AddInParameter(cmd, "TAX_OFFICE", DbType.String, MakeDbNull(model.TaxOffice));
                db.AddInParameter(cmd, "TC_IDENTITY_NO", DbType.String, MakeDbNull(model.TcIdentityNo));
                db.AddInParameter(cmd, "WITHOLDING_STATUS_ID", DbType.Int32, model.WitholdingStatus);
                db.AddInParameter(cmd, "ID_WITHOLDING", DbType.Int32, MakeDbNull(model.WitholdingId));
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(model.DealerId));
                db.AddInParameter(cmd, "IS_ELECTRONIC_INVOICE_ENABLED", DbType.Boolean, MakeDbNull(model.IsElectronicInvoiceEnabled));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.CustomerId = db.GetParameterValue(cmd, "CUSTOMER_ID").GetValue<int>();
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

        public CustomerIndexViewModel GetCustomer(UserInfo user, CustomerIndexViewModel filter)
        {
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_CUSTOMER");
                db.AddInParameter(cmd, "ID_CUSTOMER", DbType.Int32, MakeDbNull(filter.CustomerId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.CustomerId = dReader["CUSTOMER_ID"].GetValue<int>();
                    filter.CustomerName = dReader["CUSTOMER_NAME"].GetValue<string>();
                    filter.CountryId = dReader["COUNTRY_ID"].GetValue<int?>();
                    filter.CountryName = dReader["COUNTRY_NAME"].GetValue<string>();
                    filter.CompanyTypeId = dReader["COMPANY_TYPE_ID"].GetValue<int?>();
                    filter.CompanyTypeName = dReader["COMPANY_TYPE_NAME"].GetValue<string>();
                    filter.CustomerLastName = dReader["CUSTOMER_LAST_NAME"].GetValue<string>();
                    filter.SAPCustomerSSID = dReader["SAP_CUSTOMER_SSID"].GetValue<string>();
                    filter.BOSCustomerSSID = dReader["BOS_CUSTOMER_SSID"].GetValue<string>();
                    filter.CustomerTypeId = dReader["CUSTOMER_TYPE_ID"].GetValue<int?>();
                    filter.CustomerTypeName = dReader["CUSTOMER_TYPE_NAME"].GetValue<string>();
                    filter.GovernmentTypeId = dReader["GOVERNMENT_TYPE_ID"].GetValue<int?>();
                    filter.GovernmentTypeName = dReader["GOVERNMENT_TYPE_NAME"].GetValue<string>();
                    filter.IsActive = dReader["IS_ACTIVE"].GetValue<bool>();
                    filter.IsActiveName = dReader["IS_ACTIVE_NAME"].GetValue<string>();
                    filter.PassportNo = dReader["PASSPORT_NO"].GetValue<string>();
                    filter.MobileNo = dReader["MOBILE_NO"].GetValue<string>();
                    filter.TaxNo = dReader["TAX_NO"].GetValue<string>();
                    filter.TaxOffice = dReader["TAX_OFFICE"].GetValue<string>();
                    filter.TcIdentityNo = dReader["TC_IDENTITY_NO"].GetValue<string>();
                    filter.WitholdingStatus = dReader["WITHOLDING_STATUS"].GetValue<int?>();
                    filter.WitholdingStatusName = dReader["WITHOLDING_STATUS_NAME"].GetValue<string>();
                    filter.WitholdingId = dReader["ID_WITHOLDING"].GetValue<string>();
                    filter.WitholdingName = dReader["WITHOLDING_RATE"].GetValue<string>();
                    filter.IsDealerCustomer = dReader["DEALER_ID"].GetValue<int>() != 0;
                    filter.DealerId = dReader["DEALER_ID"].GetValue<int?>();
                    filter.DealerName = dReader["DEALER_NAME"].GetValue<string>();
                }

                dReader.Close();
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
        public List<CustomerListModel> GetCustomersByDealer(UserInfo user, CustomerListModel filter)
        {
            var retVal = new List<CustomerListModel>();
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_CUSTOMER_BY_DEALER");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(filter.DealerId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);


                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var customerListModel = new CustomerListModel
                        {
                            CustomerId = reader["CUSTOMER_ID"].GetValue<int>()
                        };
                        retVal.Add(customerListModel);
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
        public List<SelectListItem> GetWitholdingList(int countryId)
        {
            var retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_WITHHOLDING_COMBO");
                db.AddInParameter(cmd, "ID_COUNTRY", DbType.Int32, countryId);

                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new SelectListItem
                        {
                            Value = reader["VALUE"].GetValue<string>(),
                            Text = reader["TEXT"].GetValue<string>()
                        };
                        retVal.Add(item);
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
        public List<SelectListItem> ListCustomerNameAndNoAsSelectListItem()
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CUSTOMER_COMBO");
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["ID_CUSTOMER"].GetValue<string>(),
                            Text = reader["CUSTOMER_NAME_AND_TAX_NO"].GetValue<string>()
                        };
                        retVal.Add(lookupItem);
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
        public List<AutocompleteSearchListModel> SearchCustomer(string customerName)
        {
            var retVal = new List<AutocompleteSearchListModel>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CUSTOMER_BY_SEARCH");
                db.AddInParameter(cmd, "CUSTOMER_NAME", DbType.String, customerName);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new AutocompleteSearchListModel
                        {
                            Id = reader["ID_CUSTOMER"].GetValue<int>(),
                            Column1 = reader["CUSTOMER_NAME"].GetValue<string>()
                        };
                        retVal.Add(lookupItem);
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


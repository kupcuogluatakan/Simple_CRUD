using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;
using ODMSCommon;
using ODMSModel.CustomerAddress;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class CustomerAddressData : DataAccessBase
    {
        public List<CustomerAddressListModel> ListCustomerAddresses(UserInfo user,CustomerAddressListModel filter, out int totalCount)
        {
            var retVal = new List<CustomerAddressListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CUSTOMER_ADDRESSES");
                db.AddInParameter(cmd, "CUSTOMER_ID", DbType.Int32, filter.CustomerId);
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var customerAddressListModel = new CustomerAddressListModel
                        {
                            AddressId = reader["ADDRESS_ID"].GetValue<int>(),
                            AddressTypeId = reader["ADDRESS_TYPE_ID"].GetValue<int>(),
                            AddressTypeName = reader["ADDRESS_TYPE_NAME"].GetValue<string>(),
                            CityName = reader["CITY_NAME"].GetValue<string>(),
                            CountryName = reader["COUNTRY_NAME"].GetValue<string>(),
                            CustomerId = reader["CUSTOMER_ID"].GetValue<int>(),
                            CustomerName = reader["CUSTOMER_NAME"].GetValue<string>(),
                            TownName = reader["TOWN_NAME"].GetValue<string>(),
                            ZipCode = reader["ZIP_CODE"].GetValue<string>(),
                            IsActive = reader["IS_ACTIVE"].GetValue<int?>(),
                            IsActiveName = reader["IS_ACTIVE_NAME"].GetValue<string>()
                        };
                        retVal.Add(customerAddressListModel);
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

        public void DMLCustomerAddress(UserInfo user, CustomerAddressIndexViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_CUSTOMER_ADDRESS_MAIN");
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddParameter(cmd, "ADDRESS_ID", DbType.Int32, ParameterDirection.InputOutput, "ADDRESS_ID", DataRowVersion.Default, model.AddressId);
                db.AddInParameter(cmd, "CUSTOMER_ID", DbType.Int32, MakeDbNull(model.CustomerId));
                db.AddInParameter(cmd, "ADDRESS_TYPE_ID", DbType.Int32, MakeDbNull(model.AddressTypeId));
                db.AddInParameter(cmd, "COUNTRY_ID", DbType.Int32, MakeDbNull(model.CountryId));
                db.AddInParameter(cmd, "CITY_ID", DbType.Int32, MakeDbNull(model.CityId));
                db.AddInParameter(cmd, "TOWN_ID", DbType.Int32, MakeDbNull(model.TownId));
                db.AddInParameter(cmd, "ZIP_CODE", DbType.String, MakeDbNull(model.ZipCode));
                db.AddInParameter(cmd, "ADDRESS_1", DbType.String, MakeDbNull(model.Address1));
                db.AddInParameter(cmd, "ADDRESS_2", DbType.String, MakeDbNull(model.Address2));
                db.AddInParameter(cmd, "ADDRESS_3", DbType.String, MakeDbNull(model.Address3));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, model.IsActive);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.AddressId = db.GetParameterValue(cmd, "ADDRESS_ID").GetValue<int>();
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

        public CustomerAddressIndexViewModel GetCustomerAddress(UserInfo user, CustomerAddressIndexViewModel filter)
        {
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_CUSTOMER_ADDRESS");
                db.AddInParameter(cmd, "ID_ADDRESS", DbType.Int32, MakeDbNull(filter.AddressId));
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
                    filter.Address1 = dReader["ADDRESS_1"].GetValue<string>();
                    filter.Address2 = dReader["ADDRESS_2"].GetValue<string>();
                    filter.Address3 = dReader["ADDRESS_3"].GetValue<string>();
                    filter.AddressTypeId = dReader["ADDRESS_TYPE_ID"].GetValue<int?>();
                    filter.AddressTypeName = dReader["ADDRESS_TYPE_NAME"].GetValue<string>();
                    filter.CityId = dReader["CITY_ID"].GetValue<int?>();
                    filter.CityName = dReader["CITY_NAME"].GetValue<string>();
                    filter.TownId = dReader["TOWN_ID"].GetValue<int?>();
                    filter.TownName = dReader["TOWN_NAME"].GetValue<string>();
                    filter.ZipCode = dReader["ZIP_CODE"].GetValue<string>();
                    filter.IsActive = dReader["IS_ACTIVE"].GetValue<bool>();
                    filter.IsActiveName = dReader["IS_ACTIVE_NAME"].GetValue<string>();
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

        public List<SelectListItem> ListCustomerAddressesAsSelectListItems(UserInfo user, int customerId, bool invoiceAddressOnly = false)
        {
            var list = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CUSTOMER_ADDRESS_COMBO");
                db.AddInParameter(cmd, "CUSTOMER_ID", DbType.Int32, customerId);
                db.AddInParameter(cmd, "INVOICE_ADDRESS_ONLY", DbType.Boolean, invoiceAddressOnly);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));

                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var address = new SelectListItem
                        {
                            Value = reader["ID_ADDRESS"].GetValue<int>().ToString(),
                            Text = reader["ADDRESS_DESC"].GetValue<string>()
                        };
                        list.Add(address);
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
            return list;
        }
    }
}

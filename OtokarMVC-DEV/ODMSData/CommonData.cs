using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Logging;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.Common;
using ODMSModel.StockBlock;
using System.Globalization;
using ODMSModel.Dealer;

namespace ODMSData
{
    public class CommonData : DataAccessBase
    {
        public List<SelectListItem> ListLookup(UserInfo user, string lookupKey, string languageCode = "")
        {
            languageCode = string.IsNullOrEmpty(languageCode) ? user.LanguageCode : languageCode;

            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_LOOKUP");
                db.AddInParameter(cmd, "LOOKUP_KEY", DbType.String, lookupKey);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(languageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, DBNull.Value);
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, DBNull.Value);
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, 0);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, DBNull.Value);
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["DTL_VAL"].GetValue<string>(),
                            Text = reader["VAL_NAME"].GetValue<string>()
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
        public decimal? GetCountryVatRatio(int? countryId)
        {
            decimal? vatRatio = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_COUNTRY_VAT_RATIO");
                db.AddInParameter(cmd, "ID_COUNTRY", DbType.String, countryId);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        vatRatio = reader["PART_VAT_RATIO"].GetValue<decimal?>();
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
            return vatRatio;
        }
        public string GetNewID()
        {
            string newId = string.Empty;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("GET_NEW_ID");
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        newId = reader["NEW_ID"].GetValue<string>();
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
            return newId;
        }
        /// <summary>
        /// returns active vat ratios for combo box
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> ListVatRatio()
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_VAT_RATIOS");
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["VAT_RATIO"].GetValue<string>(),
                            Text = reader["VAT_RATIO"].GetValue<string>()
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

        /// <summary>
        /// returns active suppliers with given dealer id
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> ListDealerSuppliers(int dealerId)
        {
            var retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEALER_SUPPLIER_COMBO", dealerId);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["ID_SUPPLIER"].GetValue<string>(),
                            Text = reader["SUPPLIER_NAME"].GetValue<string>()
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


        public List<SelectListItem> GetUserListForComboBox(UserInfo user)
        {
            var list = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_USERS_SHORT");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user1 = new SelectListItem
                        {
                            Value = reader["ID_DMS_USER"].GetValue<string>(),
                            Text = reader["DMS_USER_CODE"].GetValue<string>()
                        };
                        list.Add(user1);
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
                list.Insert(0, new SelectListItem { Text = MessageResource.UserRoles_Display_SelectUser, Value = "0" });
            }
            return list;
        }

        public List<SelectListItem> GetBodyworkListForComboBox(UserInfo user)
        {
            var list = new List<SelectListItem>();
            int errorNo = 0;
            string errorMessage = string.Empty;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_BODYWORK");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var country = new SelectListItem
                        {
                            Value = reader["BODYWORK_CODE"].GetValue<string>(),
                            Text = reader["BODYWORK_NAME"].GetValue<string>()
                        };
                        list.Add(country);
                    }
                    reader.Close();
                }

                errorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                errorMessage = errorNo > 0 ? db.GetParameterValue(cmd, "ERROR_DESC").ToString() : string.Empty;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
                //logging error message
                if (errorNo > 0)
                {
                    var logger = new Loggable();
                    logger.ErrorAsync(ResolveDatabaseErrorXml(errorMessage));
                }
            }
            return list;
        }
        /// <summary>
        /// Retrive country list from database
        /// </summary>
        /// <returns>
        /// List of select list items
        /// </returns>
        public List<SelectListItem> GetCountryListForComboBox(UserInfo user)
        {
            var list = new List<SelectListItem>();
            int errorNo = 0;
            string errorMessage = string.Empty;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_COUNTRY");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var country = new SelectListItem
                        {
                            Value = reader["ID_COUNTRY"].GetValue<string>(),
                            Text = reader["COUNTRY_NAME"].GetValue<string>()
                        };
                        list.Add(country);
                    }
                    reader.Close();
                }

                errorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                errorMessage = errorNo > 0 ? db.GetParameterValue(cmd, "ERROR_DESC").ToString() : string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
                //logging error message
                if (errorNo > 0)
                {
                    var logger = new Loggable();
                    logger.ErrorAsync(ResolveDatabaseErrorXml(errorMessage));
                }
            }
            return list;
        }

        /// <summary>
        /// Retrive city list from database by given country id
        /// </summary>
        /// <returns>
        /// List of select list items
        /// </returns>
        public List<SelectListItem> GetCityListForComboBox(UserInfo user, int countryId)
        {
            var list = new List<SelectListItem>();
            int errorNo = 0;
            string errorMessage = string.Empty;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CITY_BY_COUNTRY");
                db.AddInParameter(cmd, "ID_COUNTRY", DbType.Int32, countryId);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var country = new SelectListItem
                        {
                            Value = reader["ID_CITY"].GetValue<string>(),
                            Text = reader["CITY_NAME"].GetValue<string>()
                        };
                        list.Add(country);
                    }
                    reader.Close();
                }
                errorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                errorMessage = errorNo > 0 ? db.GetParameterValue(cmd, "ERROR_DESC").ToString() : string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();

                if (errorNo > 0)
                {
                    //logging error message
                    var logger = new Loggable();
                    logger.ErrorAsync(ResolveDatabaseErrorXml(errorMessage));
                }
            }
            return list;
        }

        /// <summary>
        /// Retrive rack list from database by given warehouse id
        /// </summary>
        /// <returns>
        /// List of select list items
        /// </returns>
        public List<SelectListItem> GetRackListForComboBox(UserInfo user, int? warehouseId)
        {
            var list = new List<SelectListItem>();
            int errorNo = 0;
            string errorMessage = string.Empty;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_RACK_BY_WAREHOUSE");
                db.AddInParameter(cmd, "ID_WAREHOUSE", DbType.Int32, MakeDbNull(warehouseId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var country = new SelectListItem
                        {
                            Value = reader["ID_RACK"].GetValue<string>(),
                            Text = reader["RACK_NAME"].GetValue<string>()
                        };
                        list.Add(country);
                    }
                    reader.Close();
                }
                errorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                errorMessage = errorNo > 0 ? db.GetParameterValue(cmd, "ERROR_DESC").ToString() : string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();

                if (errorNo > 0)
                {
                    //logging error message
                    var logger = new Loggable();
                    logger.ErrorAsync(ResolveDatabaseErrorXml(errorMessage));
                }
            }
            return list;
        }
        public List<SelectListItem> ListPo(UserInfo user)
        {
            var list = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PO");
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int64, MakeDbNull((user.IsDealer) ? user.DealerID : user.GetUserDealerId()));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var country = new SelectListItem
                        {
                            Value = reader["PO_NUMBER"].GetValue<string>(),
                            Text = reader["PO_NUMBER"].GetValue<string>()
                        };
                        list.Add(country);
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
        public List<SelectListItem> GetTownListForComboBox(UserInfo user, int cityId)
        {
            var list = new List<SelectListItem>();
            int errorNo = 0;
            string errorMessage = string.Empty;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_TOWN_BY_CITY");
                db.AddInParameter(cmd, "ID_CITY", DbType.Int32, cityId);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var country = new SelectListItem
                        {
                            Value = reader["ID_TOWN"].GetValue<string>(),
                            Text = reader["TOWN_NAME"].GetValue<string>()
                        };
                        list.Add(country);
                    }
                    reader.Close();
                }
                errorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                errorMessage = errorNo > 0 ? db.GetParameterValue(cmd, "ERROR_DESC").ToString() : string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();

                if (errorNo > 0)
                {
                    //logging error message
                    var logger = new Loggable();
                    logger.ErrorAsync(ResolveDatabaseErrorXml(errorMessage));
                }
            }
            return list;
        }

        /// <summary>
        /// returns currency list
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetCurrencyListForComboBox(UserInfo user)
        {
            var list = new List<SelectListItem>();
            int errorNo = 0;
            string errorMessage = string.Empty;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CURRENCY_SHORT");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var country = new SelectListItem
                        {
                            Value = reader["CURRENCY_CODE"].GetValue<string>(),
                            Text = reader["CURRENCY_NAME"].GetValue<string>()
                        };
                        list.Add(country);
                    }
                    reader.Close();
                }
                errorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                errorMessage = errorNo > 0 ? db.GetParameterValue(cmd, "ERROR_DESC").ToString() : string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
                //logging error message
                if (errorNo > 0)
                {
                    var logger = new Loggable();
                    logger.ErrorAsync(ResolveDatabaseErrorXml(errorMessage));
                }
            }
            return list;
        }

        public List<SelectListItem> GetStockTypeListForComboBox(UserInfo user)
        {
            var list = new List<SelectListItem>();
            int errorNo = 0;
            string errorMessage = string.Empty;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_STOCK_TYPES");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var country = new SelectListItem
                        {
                            Value = reader["ID_STOCK_TYPE"].GetValue<string>(),
                            Text = reader["ADMIN_DESC"].GetValue<string>()
                        };
                        list.Add(country);
                    }
                    reader.Close();
                }

                errorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                errorMessage = errorNo > 0 ? db.GetParameterValue(cmd, "ERROR_DESC").ToString() : string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();

                if (errorNo > 0)
                {
                    //logging error message
                    var logger = new Loggable();
                    logger.ErrorAsync(ResolveDatabaseErrorXml(errorMessage));
                }
            }
            return list;
        }

        /// <summary>
        /// return value of the general parameter
        /// </summary>
        /// <param name="key">parameter key from commonvalues.generealparameters</param>
        /// <returns>value of the key</returns>
        public string GetGeneralParameterValue(string key)
        {
            var value = string.Empty;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_GENERAL_PARAMETER_VALUE");
                db.AddInParameter(cmd, "PARAMETER_ID", DbType.String, MakeDbNull(key));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        value = reader["VALUE"].GetValue<string>();

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
            return value;
        }

        public bool CheckDealer(int dealerid, long id, string type)
        {
            var value = false;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_CHECK_DEALER");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(dealerid));
                db.AddInParameter(cmd, "ID", DbType.Int64, MakeDbNull(id));
                db.AddInParameter(cmd, "TYPE", DbType.String, MakeDbNull(type));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        value = reader["RESULT"].GetValue<bool>();

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
            return value;
        }

        public decimal GetPriceByDealerPartVehicleAndType(int partId, int? vehicleId, int dealerId, string type)
        {
            var value = new decimal();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_SPARE_PART_PRICE");
                db.AddInParameter(cmd, "ID_PART", DbType.String, MakeDbNull(partId));
                db.AddInParameter(cmd, "ID_VEHICLE", DbType.String, MakeDbNull(vehicleId));
                db.AddInParameter(cmd, "ID_DEALER", DbType.String, MakeDbNull(dealerId));
                db.AddInParameter(cmd, "TYPE", DbType.String, MakeDbNull(type));
                db.AddInParameter(cmd, "PRICE_LIST_DATE", DbType.Date, MakeDbNull(0));
                db.AddInParameter(cmd, "ID_PRICE_LIST", DbType.Int32, MakeDbNull(0));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        value = reader["VALUE"].GetValue<decimal>();
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
            return value;
        }

        public List<SelectListItem> ListDealer(UserInfo user)
        {
            List<SelectListItem> listItems = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEALER_COMBO");
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new SelectListItem
                        {
                            Value = reader["ID_DEALER"].GetValue<string>(),
                            Text = reader["DEALER_NAME"].GetValue<string>()
                        };
                        listItems.Add(item);
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

            return listItems;
        }

        public List<DealerViewModel> GetDealer(UserInfo user)
        {
            List<DealerViewModel> listItems = new List<DealerViewModel>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEALER_COMBO");
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new DealerViewModel
                        {
                            DealerId = reader["ID_DEALER"].GetValue<int>(),
                            Name = reader["DEALER_NAME"].GetValue<string>(),
                            IdPoGroup = reader["ID_PO_GROUP"].GetValue<int>()
                        };
                        listItems.Add(item);
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

            return listItems;
        }

        public List<SelectListItem> ListAllDealerWihSelectListItems()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEALER_COMBO");
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, null);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new SelectListItem
                        {
                            Value = reader["ID_DEALER"].GetValue<string>(),
                            Text = reader["DEALER_NAME"].GetValue<string>()
                        };
                        listItems.Add(item);
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

            return listItems;
        }

        public List<SelectListItem> ListUserByDealerId(int? dealerId, bool? isTechnician)
        {
            List<SelectListItem> listItems = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_USERS_OF_DEALER_COMBO");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(dealerId));
                db.AddInParameter(cmd, "IS_TECHNICIAN", DbType.Int32, isTechnician);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new SelectListItem
                        {
                            Value = reader["ID_DMS_USER"].GetValue<string>(),
                            Text = reader["WORKER_NAME"].GetValue<string>()
                        };
                        listItems.Add(item);
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

            return listItems;
        }

        public List<SelectListItem> ListAppIndcFailureCode(UserInfo user)
        {
            List<SelectListItem> listItems = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_APPOINTMENT_INDICATOR_FAILURE_CODE_ACTIVE_COMBO");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));

                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new SelectListItem
                        {
                            Value = reader["ID_APPOINTMENT_INDICATOR_FAILURE_CODE"].GetValue<string>(),
                            Text =
                                    CommonValues.OpenParenthesis + reader["CODE"].GetValue<string>() + CommonValues.CloseParenthesis +
                                    reader["DESCRIPTION"].GetValue<string>()
                        };
                        listItems.Add(item);
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

            return listItems;
        }

        public List<SelectListItem> GetSelectedStockTypes(UserInfo user)
        {
            var list = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_STOCK_TYPE_SELECTED");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));

                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        bool select = (reader["ID_STOCK_TYPE"].GetValue<int>() == reader["SELECTED"].GetValue<int>());
                        var country = new SelectListItem
                        {
                            Value = reader["ID_STOCK_TYPE"].GetValue<string>(),
                            Text = reader["MAINT_NAME"].GetValue<string>(),
                            Selected = select
                        };
                        list.Add(country);
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

        public List<SelectListItem> ListStockTypes(UserInfo user, long? idPart, int? idDealer)
        {
            var list = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_STOCK_TYPE_BY_PART");
                db.AddInParameter(cmd, "ID_PART", DbType.Int64, MakeDbNull(idPart));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(idDealer));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));

                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var country = new SelectListItem
                        {
                            Value = reader["ID_STOCK_TYPE"].GetValue<string>(),
                            Text = reader["MAINT_NAME"].GetValue<string>()
                        };
                        list.Add(country);
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

        public StockBlockViewModel GetBlockQuantity(UserInfo user, long? idPart, int? idStockType, int? idDealer)
        {
            var list = new StockBlockViewModel();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_STOCK_BLOCK_QUANTITY");
                db.AddInParameter(cmd, "ID_PART", DbType.Int64, MakeDbNull(idPart));
                db.AddInParameter(cmd, "ID_STOCK_TYPE", DbType.Int32, MakeDbNull(idStockType));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(idDealer));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));

                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.BlockQty = reader["TOTAL_BLOCK_QRY"].GetValue<decimal>();
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
        public string SendDBMail(string to, string subject, string body)
        {
            string errorMessage;
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("SP_SEND_MAIL");
                db.AddInParameter(cmd, "MAIL_ADDRESSES", DbType.String, to);
                db.AddInParameter(cmd, "MAIL_CONTENT", DbType.String, body);
                db.AddInParameter(cmd, "SUBJECT", DbType.String, subject);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                errorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return errorMessage;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="to">Boşsa generalParameter ' bakıyor</param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="generalParamVal"></param>
        /// <returns></returns>
        public string SendGenericDBMail(string to, string subject, string body, string generalParamVal)
        {
            string errorMessage;
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_SEND_MAIL");
                db.AddInParameter(cmd, "MAIL_ADDRESSES", DbType.String, MakeDbNull(to));
                db.AddInParameter(cmd, "MAIL_LIST_PARAMETER_VAL", DbType.String, generalParamVal);
                db.AddInParameter(cmd, "MAIL_CONTENT", DbType.String, body);
                db.AddInParameter(cmd, "SUBJECT", DbType.String, subject);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                errorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return errorMessage;
        }

        public List<SelectListItem> ListAllLabour(UserInfo user)
        {
            var listItems = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_LABOUR_COMBO");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var item = new SelectListItem
                        {
                            Value = dr["LABOUR_CODE"].GetValue<string>(),
                            Text = dr["LABOUR_CODE"].GetValue<string>()
                        };
                        listItems.Add(item);
                    }
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

            return listItems;
        }


        public List<SelectListItem> ListConfirmedUser()
        {
            var list = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("[P_GET_USER_BY_GUARANTEE_GROUP_MEMBER]");
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new SelectListItem
                        {
                            Value = reader["ID_DMS_USER"].GetValue<string>(),
                            Text = reader["CUSTOMER_NAME"].GetValue<string>() + " " + reader["USER_MID_NAME"].GetValue<string>() + " " + reader["USER_LAST_NAME"].GetValue<string>()
                        };
                        list.Add(user);
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
                list.Insert(0, new SelectListItem { Text = MessageResource.UserRoles_Display_SelectUser, Value = "0" });
            }
            return list;
        }
        public List<SelectListItem> ListProcessType(UserInfo user)
        {
            var listItem = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_PROCESS_TYPE_COMBO");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var item = new SelectListItem()
                        {
                            Text = dr["TEXT"].GetValue<string>(),
                            Value = dr["VALUE"].GetValue<string>()
                        };
                        listItem.Add(item);
                    }
                    dr.Close();
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

            return listItem;
        }

        public List<SelectListItem> ListRacksByPartWareHouse(UserInfo user, int warehouseId, long partId)
        {
            var listItems = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_RACKS_BY_PART_WAREHOUSE_COMBO");
                db.AddInParameter(cmd, "WAREHOUSE_ID", DbType.Int32, MakeDbNull(warehouseId));
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, MakeDbNull(partId));
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var item = new SelectListItem
                        {
                            Value = dr["ID_RACK"].GetValue<string>(),
                            Text = dr["RACK_NAME"].GetValue<string>()
                        };
                        listItems.Add(item);
                    }
                    dr.Close();
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

            return listItems;
        }

        public List<SelectListItem> ListPeriodicMaintLang(UserInfo user)
        {
            var listItems = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PERIODIC_MAINT_LANG");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var item = new SelectListItem
                        {
                            Value = dr["ID_MAINT"].GetValue<string>(),
                            Text = dr["MAINT_NAME"].GetValue<string>()
                        };
                        listItems.Add(item);
                    }
                    dr.Close();
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

            return listItems;
        }

        public void DMLStockTransaction(UserInfo user, StockTransactionViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_STOCK_TRANSACTION");
                db.AddParameter(cmd, "ID_STOCK_TRANSACTION", DbType.Int64, ParameterDirection.InputOutput, "ID_STOCK_TRANSACTION", DataRowVersion.Default, model.StockTransactionId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(model.DealerId));
                db.AddInParameter(cmd, "TRANSACTION_TYPE_ID", DbType.String, MakeDbNull(model.TransactionTypeId));
                db.AddInParameter(cmd, "ID_PART", DbType.Int64, MakeDbNull(model.PartId));
                db.AddInParameter(cmd, "QUANTITY", DbType.Decimal, MakeDbNull(model.Quantity));
                db.AddInParameter(cmd, "DEALER_PRICE", DbType.Decimal, MakeDbNull(model.DealerPrice));
                db.AddInParameter(cmd, "TRANSACTION_DESC", DbType.String, MakeDbNull(model.TransactionDesc));
                db.AddInParameter(cmd, "FROM_WAREHOUSE", DbType.Int64, MakeDbNull(model.FromWarehouse));
                db.AddInParameter(cmd, "FROM_RACK", DbType.Int64, MakeDbNull(model.FromRack));
                db.AddInParameter(cmd, "FROM_STOCK_TYPE", DbType.Int64, MakeDbNull(model.FromStockType));
                db.AddInParameter(cmd, "TO_WAREHOUSE", DbType.Int64, MakeDbNull(model.ToWarehouse));
                db.AddInParameter(cmd, "TO_RACK", DbType.Int64, MakeDbNull(model.ToRack));
                db.AddInParameter(cmd, "TO_STOCK_TYPE", DbType.Int64, MakeDbNull(model.ToStockType));
                db.AddInParameter(cmd, "RESERVE_QNTY", DbType.Decimal, MakeDbNull(model.ReserveQnty));
                db.AddInParameter(cmd, "BLOCK_QNTY", DbType.Int64, MakeDbNull(model.BlockQnty));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.StockTransactionId = db.GetParameterValue(cmd, "ID_STOCK_TRANSACTION").GetValue<long>();
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
    }
}

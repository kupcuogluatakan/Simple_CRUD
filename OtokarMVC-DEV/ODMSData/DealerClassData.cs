using System.Web.Mvc;
using ODMSCommon.Security;
using ODMSModel.DealerClass;
using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSModel.Dealer;

namespace ODMSData
{
    public class DealerClassData : DataAccessBase
    {
        public List<DealerClassListModel> ListDealerClass(UserInfo user,DealerClassListModel filter, out int totalCount)
        {
            var retVal = new List<DealerClassListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEALER_CLASS");

                db.AddInParameter(cmd, "DEALER_CLASS_CODE", DbType.String, filter.DealerClassCode);
                db.AddInParameter(cmd, "SSID_DEALER_CLASS", DbType.String, filter.SSIdDealerClass);
                db.AddInParameter(cmd, "DEALER_CLASS_NAME", DbType.String, filter.DealerClassName);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.String, filter.IsActive);
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var dealerClassListModel = new DealerClassListModel
                            {
                                DealerClassCode = reader["DEALER_CLASS_CODE"].GetValue<string>(),
                                SSIdDealerClass = reader["SSID_DEALER_CLASS"].GetValue<string>(),
                                DealerClassName = reader["DEALER_CLASS_NAME"].GetValue<string>(),
                                IsActive = reader["IS_ACTIVE"].GetValue<bool>(),
                                IsActiveName = reader["IS_ACTIVE_NAME"].GetValue<string>()
                            };

                        retVal.Add(dealerClassListModel);
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

        public DealerClassViewModel GetDealerClass(UserInfo user, DealerClassViewModel filter)
        {
            System.Data.Common.DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_DEALER_CLASS");
                db.AddInParameter(cmd, "DEALER_CLASS_CODE", DbType.String, MakeDbNull(filter.DealerClassCode));

                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.SSIdDealerClass = dReader["SSID_DEALER_CLASS"].GetValue<string>();
                    filter.DealerClassName = dReader["DEALER_CLASS_NAME"].GetValue<string>();
                    filter.IsActive = dReader["IS_ACTIVE"].GetValue<bool>();
                    filter.IsActiveName = dReader["IS_ACTIVE_NAME"].GetValue<string>();
                }
                if (dReader.NextResult())
                {
                    var table = new DataTable();
                    table.Load(dReader);
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

        public void DMLDealerClass(UserInfo user, DealerClassViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_DEALER_CLASS");
                db.AddInParameter(cmd, "DEALER_CLASS_CODE", DbType.String, model.DealerClassCode);
                db.AddInParameter(cmd, "SSID_DEALER_CLASS", DbType.String, model.SSIdDealerClass);
                db.AddInParameter(cmd, "DEALER_CLASS_NAME", DbType.String, model.DealerClassName);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, model.IsActive);

                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddInParameter(cmd, "OPERATING_DATE", DbType.Date, MakeDbNull(DateTime.Now));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.DealerClassCode = db.GetParameterValue(cmd, "DEALER_CLASS_CODE").ToString();
                model.SSIdDealerClass = db.GetParameterValue(cmd, "SSID_DEALER_CLASS").ToString();
                model.DealerClassName = db.GetParameterValue(cmd, "DEALER_CLASS_NAME").ToString();
                model.IsActive = bool.Parse(db.GetParameterValue(cmd, "IS_ACTIVE").ToString());
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

        public List<SelectListItem> ListDealerClassesAsSelectListItem()
        {
            var retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEALER_CLASS_COMBO");
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listItem = new SelectListItem
                            {
                                Value = reader["DEALER_CLASS_CODE"].GetValue<string>(),
                                Text = reader["DEALER_CLASS_NAME"].GetValue<string>()
                            };
                        retVal.Add(listItem);
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

        public List<SelectListItem> ListDealerClassCodeAsSelectListItem()
        {
            var retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEALER_CLASS_CODE");
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listItem = new SelectListItem
                            {
                                Value = reader["ID_DEALER"].GetValue<string>(),
                                Text = reader["DEALER_CLASS_CODE"].GetValue<string>()
                            };
                        retVal.Add(listItem);
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

        public string GetDealerBranchSSID(int dealerId)
        {
            string dealerBranchSSID = String.Empty;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_DEALER_BRANCH_SSID");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, dealerId);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dealerBranchSSID = reader["DEALER_BRANCH_SSID"].GetValue<string>();
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

            return dealerBranchSSID;
        }

        public int GetDealerCountryId(int dealerId)
        {
            int countryId = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_DEALER_COUNTRY_ID");
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, dealerId);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        countryId = reader["ID_COUNTRY"].GetValue<int>();
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

            return countryId;
        }

        public DealerCustomerInfoModel GetDealerCustomerInfo(int dealerId)
        {
            System.Data.Common.DbDataReader dReader = null;
            DealerCustomerInfoModel model = new DealerCustomerInfoModel();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_DEALER_CUSTOMER_INFO");
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(dealerId));
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    model.CustomerId = dReader["CustomerId"].GetValue<int>();
                    model.CustomerTypeId = dReader["CustomerTypeId"].GetValue<int>();
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
            return model;
        }
    }
}

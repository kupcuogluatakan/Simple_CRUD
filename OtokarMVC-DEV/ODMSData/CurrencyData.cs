using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.Currency;
using ODMSModel.ListModel;
using ODMSModel.ViewModel;
using ODMSCommon.Resources;
using System;

namespace ODMSData
{
    public class CurrencyData : DataAccessBase
    {
        public List<SelectListItem> ListCurrencyAsSelectList(UserInfo user)
        {
            var result = new List<SelectListItem>();
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
                        var listModel = new SelectListItem
                        {
                            Value = reader["CURRENCY_CODE"].GetValue<string>(),
                            Text = reader["CURRENCY_NAME"].GetValue<string>()
                        };
                        result.Add(listModel);
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
            return result;
        }

        public List<CurrencyListModel> ListCurrency(UserInfo user,CurrencyListModel filter, out int totalCount)
        {
            var retVal = new List<CurrencyListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CURRENCY");
                db.AddInParameter(cmd, "CURRENCY_NAME", DbType.String, MakeDbNull(filter.CurrencyName));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, filter.IsActive);
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var currencyListModel = new CurrencyListModel
                        {
                            CurrencyCode = reader["CURRENCY_CODE"].GetValue<string>(),
                            DecimalPartName = reader["DECIMAL_PART_NAME"].GetValue<string>(),
                            CurrencyName = reader["CURRENCY_NAME"].GetValue<string>(),
                            AdminName = reader["ADMIN_NAME"].GetValue<string>(),
                            ListOrder = reader["LIST_ORDER"].GetValue<int>(),
                            IsActive = reader["IS_ACTIVE"].GetValue<int>(),
                            IsActiveName = reader["IS_ACTIVE_NAME"].GetValue<string>()
                        };
                        retVal.Add(currencyListModel);
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

        public CurrencyIndexViewModel GetCurrency(UserInfo user, CurrencyIndexViewModel filter)
        {
            System.Data.Common.DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_CURRENCY");
                db.AddInParameter(cmd, "CURRENCY_CODE", DbType.String, MakeDbNull(filter.CurrencyCode));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.CurrencyCode = dReader["CURRENCY_CODE"].GetValue<string>();
                    filter.DecimalPartName = dReader["DECIMAL_PART_NAME"].GetValue<string>();
                    filter.AdminName = dReader["ADMIN_NAME"].GetValue<string>();
                    filter.ListOrder = dReader["LIST_ORDER"].GetValue<int>();
                    filter.IsActive = dReader["IS_ACTIVE"].GetValue<bool>();
                    filter.IsActiveName = dReader["IS_ACTIVE_NAME"].GetValue<string>();
                }
                if (dReader.NextResult())
                {
                    var table = new DataTable();
                    table.Load(dReader);
                    filter.MultiLanguageContentAsText = GetLanguageContentFromDataSet(table, "CURRENCY_NAME");
                    filter.CurrencyName = (MultiLanguageModel)CommonUtility.DeepClone(filter.CurrencyName);
                    filter.CurrencyName.MultiLanguageContentAsText = filter.MultiLanguageContentAsText;
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

        public void DMLCurrency(UserInfo user, CurrencyIndexViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_CURRENCY_MAIN");
                db.AddParameter(cmd, "CURRENCY_CODE", DbType.String, ParameterDirection.InputOutput, "CURRENCY_CODE",
                                DataRowVersion.Default, model.CurrencyCode);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "ADMIN_NAME", DbType.String, MakeDbNull(model.AdminName));
                db.AddInParameter(cmd, "LIST_ORDER", DbType.Int32, MakeDbNull(model.ListOrder));
                db.AddInParameter(cmd, "DECIMAL_PART_NAME", DbType.String, MakeDbNull(model.DecimalPartName));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, MakeDbNull(model.IsActive));
                db.AddInParameter(cmd, "ML_CONTENT", DbType.String, MakeDbNull(model.MultiLanguageContentAsText));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.CurrencyCode = db.GetParameterValue(cmd, "CURRENCY_CODE").ToString();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo == 2)
                    model.ErrorMessage = MessageResource.Currency_Error_NullCurrencyCode;
                else if (model.ErrorNo == 1)
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

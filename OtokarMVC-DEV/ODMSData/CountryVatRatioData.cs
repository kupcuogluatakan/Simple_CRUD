using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using ODMSCommon;
using ODMSModel.CountryVatRatio;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class CountryVatRatioData : DataAccessBase
    {
        public List<CountryVatRatioListModel> ListCountryVatRatios(UserInfo user,CountryVatRatioListModel model, out int totalCnt)
        {
            var listModels = new List<CountryVatRatioListModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_COUNTRY_VAT_RATIO");
                db.AddInParameter(cmd, "ID_COUNTRY", DbType.Decimal, MakeDbNull(model.CountryId));
                db.AddInParameter(cmd, "PART_VAT_RATIO", DbType.Decimal, MakeDbNull(model.PartVatRatio));
                db.AddInParameter(cmd, "LABOUR_VAT_RATIO", DbType.Decimal, MakeDbNull(model.LabourVatRatio));
                AddPagingParametersWithLanguage(user,cmd, model);
                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var newModel = new CountryVatRatioListModel
                        {
                            CountryId = dr["ID_COUNTRY"].GetValue<int>(),
                            CountryName = dr["COUNTRY_NAME"].GetValue<string>(),
                            LabourVatRatio = dr["LABOUR_VAT_RATIO"].GetValue<decimal>(),
                            PartVatRatio = dr["PART_VAT_RATIO"].GetValue<decimal>()
                        };

                        listModels.Add(newModel);
                    }
                    dr.Close();
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

            return listModels;
        }

        public CountryVatRatioViewModel GetCountryVatRatio(int countryId)
        {
            CountryVatRatioViewModel model = new CountryVatRatioViewModel();
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
                        model.CountryId = reader["COUNTRY_ID"].GetValue<int>();
                        model.CountryName = reader["COUNTRY_NAME"].GetValue<string>();
                        model.PartVatRatio = reader["PART_VAT_RATIO"].GetValue<decimal>();
                        model.LabourVatRatio = reader["LABOUR_VAT_RATIO"].GetValue<decimal>();
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
            return model;
        }

        public void DMLCountryVatRatio(UserInfo user,CountryVatRatioViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_COUNTRY_VAT_RATIO");
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "ID_COUNTRY", DbType.Int32, MakeDbNull(model.CountryId));
                db.AddInParameter(cmd, "PART_VAT_RATIO", DbType.Decimal, MakeDbNull(model.PartVatRatio));
                db.AddInParameter(cmd, "LABOUR_VAT_RATIO", DbType.Decimal, MakeDbNull(model.LabourVatRatio));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
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

        public List<SelectListItem> GetVatRatioCountries(UserInfo user)
        {
            var list = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_SPARE_PART_VAT_RATIO_COUTRIES_COMBO");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var item = new SelectListItem
                        {
                            Value = dr["ID_COUNTRY"].GetValue<string>(),
                            Text = dr["COUNTRY_NAME"].GetValue<string>()
                        };

                        list.Add(item);
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

            return list;
        }

        public decimal GetVatRatioByPartAndCountry(int partId, int? countryId)
        {
            decimal result;
            try
            {
                SqlParameter vatRatioParameter = new SqlParameter("@VAT_RATIO", SqlDbType.Decimal, 6)
                {
                    Precision = 5,
                    Scale = 2,
                    Direction = ParameterDirection.Output
                };
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_SPARE_PART_VAT_RATIO_BY_COUNTRY");
                db.AddInParameter(cmd, "ID_PART", DbType.Int32, partId);
                db.AddInParameter(cmd, "ID_COUNTRY", DbType.Int32, countryId);
                cmd.Parameters.Add(vatRatioParameter);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                result = db.GetParameterValue(cmd, "VAT_RATIO").GetValue<decimal>();
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

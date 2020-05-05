using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSModel.GuaranteeAuthorityLimitations;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class GuaranteeAuthorityLimitationsData : DataAccessBase
    {
        public void DMLGuaranteeAuthorityLimitations(UserInfo user, GuaranteeAuthorityLimitationsViewModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_DML_GUARANTEE_AUTHORITY_LIMITATIONS");
                db.AddInParameter(cmd, "CURRENCY_CODE", DbType.String, MakeDbNull(model.CurrencyCode));
                db.AddInParameter(cmd, "MODEL_KOD", DbType.String, MakeDbNull(model.ModelKod));
                db.AddInParameter(cmd, "AMOUNT", DbType.Decimal, MakeDbNull(model.Amount));
                db.AddInParameter(cmd, "CUMULATIVE_AMOUNT", DbType.Decimal, MakeDbNull(model.CumulativeAmount));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
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

        public GuaranteeAuthorityLimitationsViewModel GetGuaranteeAuthorityLimitations(UserInfo user, string currencyCode, string modelKod)
        {
            var result = new GuaranteeAuthorityLimitationsViewModel();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_GUARANTEE_AUTHORITY_LIMITATIONS");
                db.AddInParameter(cmd, "CURRENCY_CODE", DbType.String, MakeDbNull(currencyCode));
                db.AddInParameter(cmd, "MODEL_KOD", DbType.String, MakeDbNull(modelKod));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));

                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.ModelKod = reader["MODEL_KOD"].GetValue<string>();
                        result.ModelName = reader["MODEL_NAME"].GetValue<string>();
                        result.CurrencyCode = reader["CURRENCY_CODE"].GetValue<string>();
                        result.CurrencyName = reader["CURRENCY_NAME"].GetValue<string>();
                        result.Amount = reader["AMOUNT"].GetValue<decimal>();
                        result.CumulativeAmount = reader["CUMULATIVE_AMOUNT"].GetValue<decimal>();
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

        public List<GuaranteeAuthorityLimitationsListModel> ListGuaranteeAuthorityLimitations(UserInfo user, GuaranteeAuthorityLimitationsListModel filter, out int totalCnt)
        {
            var result = new List<GuaranteeAuthorityLimitationsListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_GUARANTEE_AUTHORITY_LIMITATIONS");
                db.AddInParameter(cmd, "CURRENCY_CODE", DbType.String, MakeDbNull(filter.CurrencyCode));
                db.AddInParameter(cmd, "MODEL_KOD", DbType.String, MakeDbNull(filter.ModelKod));
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
                        var listModel = new GuaranteeAuthorityLimitationsListModel
                        {
                            ModelKod = reader["MODEL_KOD"].GetValue<string>(),
                            ModelName = reader["MODEL_NAME"].GetValue<string>(),
                            CurrencyCode = reader["CURRENCY_CODE"].GetValue<string>(),
                            CurrencyName = reader["CURRENCY_NAME"].GetValue<string>(),
                            Amount = reader["AMOUNT"].GetValue<decimal>(),
                            CumulativeAmount = reader["CUMULATIVE_AMOUNT"].GetValue<decimal>()
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

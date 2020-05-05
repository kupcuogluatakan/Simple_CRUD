using ODMSCommon.Security;
using ODMSModel.DealerGuaranteeRatio;
using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;

namespace ODMSData
{
    public class DealerGuaranteeRatioData : DataAccessBase
    {
        public void DMLDealerGuaranteeRatio(UserInfo user, DealerGuaranteeRatioIndexViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_DEALER_GUARANTEE_RATIO");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, model.IdDealer);
                db.AddInParameter(cmd, "GUARANTEE_RATIO", DbType.Decimal, model.GuaranteeRatio);

                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddInParameter(cmd, "OPERATING_DATE", DbType.Date, MakeDbNull(DateTime.Now));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                //DealerGuaranteeRatioModel.IdDealer = Int32.Parse(db.GetParameterValue(cmd, "ID_DEALER").ToString());
                model.GuaranteeRatio = decimal.Parse(db.GetParameterValue(cmd, "GUARANTEE_RATIO").ToString());
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

        public List<DealerGuaranteeRatioListModel> ListDealerGuaranteeRatio(UserInfo user,DealerGuaranteeRatioListModel filter, out int totalCount)
        {
            var retVal = new List<DealerGuaranteeRatioListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEALER_GUARANTEE_RATIO");

                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, filter.IdDealer);
                db.AddInParameter(cmd, "DEALER_NAME", DbType.String, filter.DealerName);
                db.AddInParameter(cmd, "GUARANTEE_RATIO", DbType.Decimal, filter.GuaranteeRatio);
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var DealerGuaranteeRatioListModel = new DealerGuaranteeRatioListModel
                        {
                            IdDealer = reader["ID_DEALER"].GetValue<int>(),
                            DealerName = reader["DEALER_SHRT_NAME"].GetValue<string>(),
                            DealerSSID = reader["DEALER_SSID"].GetValue<string>(),
                            GuaranteeRatio = reader["GUARANTEE_RATIO"].GetValue<decimal>()
                        };

                        retVal.Add(DealerGuaranteeRatioListModel);
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

        public DealerGuaranteeRatioIndexViewModel GetDealerGuaranteeRatio(UserInfo user, DealerGuaranteeRatioIndexViewModel filter)
        {
            System.Data.Common.DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_DEALER_GUARANTEE_RATIO");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(filter.IdDealer));

                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.IdDealer = dReader["ID_DEALER"].GetValue<int>();
                    filter.DealerName = dReader["DEALER_SHRT_NAME"].GetValue<string>();
                    filter.DealerSSID = dReader["DEALER_SSID"].GetValue<string>();
                    filter.GuaranteeRatio = dReader["GUARANTEE_RATIO"].GetValue<decimal>();

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


    }
}

using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.ClaimRecallPeriod;
using System.Data.Common;

namespace ODMSData
{
    public class ClaimRecallPeriodData : DataAccessBase
    {
        public List<ClaimRecallPeriodListModel> ListClaimRecallPeriod(UserInfo user,ClaimRecallPeriodListModel filter, out int totalCount)
        {
            var retVal = new List<ClaimRecallPeriodListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CLAIM_RECALL_PERIOD");
                db.AddInParameter(cmd, "CLAIM_RECALL_PERIOD_ID", DbType.Int32, MakeDbNull(filter.ClaimRecallPeriodId));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, filter.IsActive);
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var claimRecallPeriodListModel = new ClaimRecallPeriodListModel
                        {
                            ClaimRecallPeriodId = reader["CLAIM_RECALL_PERIOD_ID"].GetValue<int>(),
                            ValidLastDay = reader["VALID_LAST_DAY"].GetValue<DateTime>(),
                            ShipFirstDay = reader["SHIP_FIRST_DAY"].GetValue<DateTime>(),
                            ShipLastDay = reader["SHIP_LAST_DAY"].GetValue<DateTime>(),
                            IsActive = reader["IS_ACTIVE"].GetValue<int>(),
                            IsActiveName = reader["IS_ACTIVE_NAME"].GetValue<string>()
                        };
                        retVal.Add(claimRecallPeriodListModel);
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

        public ClaimRecallPeriodViewModel GetClaimRecallPeriod(UserInfo user, ClaimRecallPeriodViewModel filter)
        {
            System.Data.Common.DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_CLAIM_RECALL_PERIOD");
                db.AddInParameter(cmd, "CLAIM_RECALL_PERIOD_ID", DbType.Int32, MakeDbNull(filter.ClaimRecallPeriodId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.ClaimRecallPeriodId = dReader["CLAIM_RECALL_PERIOD_ID"].GetValue<int>();
                    filter.ValidLastDay = dReader["VALID_LAST_DAY"].GetValue<DateTime>();
                    filter.ShipFirstDay = dReader["SHIP_FIRST_DAY"].GetValue<DateTime>();
                    filter.IsActive = dReader["IS_ACTIVE"].GetValue<bool>();
                    filter.IsActiveName = dReader["IS_ACTIVE_NAME"].GetValue<string>();
                    filter.ShipLastDay = dReader["SHIP_LAST_DAY"].GetValue<DateTime>();
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

        public void DMLClaimRecallPeriod(UserInfo user, ClaimRecallPeriodViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_CLAIM_RECALL_PERIOD");
                db.AddParameter(cmd, "CLAIM_RECALL_PERIOD_ID", DbType.Int32, ParameterDirection.InputOutput, "CLAIM_RECALL_PERIOD_ID", DataRowVersion.Default, model.ClaimRecallPeriodId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "VALID_LAST_DAY", DbType.DateTime, MakeDbNull(model.ValidLastDay));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, model.IsActive);
                db.AddInParameter(cmd, "SHIP_FIRST_DAY", DbType.DateTime, MakeDbNull(model.ShipFirstDay));
                db.AddInParameter(cmd, "SHIP_LAST_DAY", DbType.DateTime, MakeDbNull(model.ShipLastDay));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ClaimRecallPeriodId = db.GetParameterValue(cmd, "CLAIM_RECALL_PERIOD_ID").GetValue<int>();
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

        public void ControlClaimPeriodLastDay()
        {
            CommonData cd = new CommonData();
            DbDataReader dReader = null;
            try
            {
                DateTime lastValidDate = new DateTime();
                var claimPeriodRemindDay = int.Parse(cd.GetGeneralParameterValue("CLAIM_PERIOD_REMIND_DAY"));
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_ACTIVE_CLAIM_RECALL_PERIOD_VALID_LAST_VALID_DATE");
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    lastValidDate = dReader["VALID_LAST_DAY"].GetValue<DateTime>();
                }

                //hiç kayıt yoksa kontrolü
                //hiç kayır bulumazsa her kod çalıştığında mail atacaktır.
                if (lastValidDate == DateTime.MinValue && DateTime.Now.AddDays(claimPeriodRemindDay) > DateTime.Now)
                {
                    db.GetStoredProcCommand("P_SEND_CLAIM_RECALL_PERIOD_WARNING_MAIL", lastValidDate);
                }
                else if (lastValidDate.AddDays(-claimPeriodRemindDay) > DateTime.Now) return;

                db.GetStoredProcCommand("P_SEND_CLAIM_RECALL_PERIOD_WARNING_MAIL", lastValidDate);
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
        }
        public void ControlClaimPeriodPartListApprove()
        {
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_REMIND_CLAIM_RECALL_PERIOD");
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();
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
        }
    }
}

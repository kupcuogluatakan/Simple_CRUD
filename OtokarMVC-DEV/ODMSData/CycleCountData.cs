using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.CycleCount;
using ODMSModel.CycleCountPlan;

namespace ODMSData
{
    public class CycleCountData : DataAccessBase
    {

        public void ApproveCycleCount(UserInfo user, CycleCountViewModel filter)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_APPROVE_CYCLE_COUNT");
                db.AddInParameter(cmd, "CYCLE_COUNT_ID", DbType.Int32, filter.CycleCountId);
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, user.GetUserDealerId());
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                filter.CycleCountId = db.GetParameterValue(cmd, "CYCLE_COUNT_ID").GetValue<string>();
                filter.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                filter.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (filter.ErrorNo == 1)
                    filter.ErrorMessage = ResolveDatabaseErrorXml(filter.ErrorMessage);
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

        public void StartCycleCount(UserInfo user, CycleCountViewModel filter)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_START_CYCLE_COUNT");
                db.AddInParameter(cmd, "CYCLE_COUNT_ID", DbType.Int32, filter.CycleCountId);
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, user.GetUserDealerId());
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                filter.CycleCountId = db.GetParameterValue(cmd, "CYCLE_COUNT_ID").GetValue<string>();
                filter.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                filter.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (filter.ErrorNo > 0)
                    filter.ErrorMessage = ResolveDatabaseErrorXml(filter.ErrorMessage);
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

        public void DMLCycleCount(UserInfo user, CycleCountViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_CYCLE_COUNT");
                db.AddParameter(cmd, "CYCLE_COUNT_ID", DbType.Int32, ParameterDirection.InputOutput, "CYCLE_COUNT_ID", DataRowVersion.Default, MakeDbNull(model.CycleCountId));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "CYCLE_COUNT_NAME", DbType.String, MakeDbNull(model.CycleCountName));
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(model.DealerId));
                db.AddInParameter(cmd, "DISPLAY_CURRENT_AMOUNT", DbType.Int32, model.DisplayCurrentAmount);
                db.AddInParameter(cmd, "CYCLE_COUNT_STATUS", DbType.Int32, model.CycleCountStatus);
                db.AddInParameter(cmd, "ID_STOCK_TYPE", DbType.Int32, model.StockTypeId);
                db.AddInParameter(cmd, "START_DATE", DbType.DateTime, MakeDbNull(model.StartDate));
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, MakeDbNull(model.EndDate));
                db.AddInParameter(cmd, "CONFIRM_DATE", DbType.DateTime, MakeDbNull(model.ConfirmDate));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.CycleCountId = db.GetParameterValue(cmd, "CYCLE_COUNT_ID").GetValue<string>();
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

        public CycleCountViewModel GetCycleCount(UserInfo user, CycleCountViewModel filter)
        {
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_CYCLE_COUNT");
                db.AddInParameter(cmd, "CYCLE_COUNT_ID", DbType.Int32, MakeDbNull(filter.CycleCountId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.CycleCountId = dReader["CYCLE_COUNT_ID"].GetValue<string>();
                    filter.CycleCountName = dReader["CYCLE_COUNT_NAME"].GetValue<string>();
                    filter.ConfirmDate = dReader["CONFIRM_DATE"].GetValue<DateTime?>();
                    filter.StockTypeId = dReader["ID_STOCK_TYPE"].GetValue<int>();
                    filter.StockTypeName = dReader["STOCK_TYPE_NAME"].GetValue<string>();
                    filter.CycleCountStatus = dReader["CYCLE_COUNT_STATUS"].GetValue<int?>();
                    filter.CycleCountStatusName = dReader["CYCLE_COUNT_STATUS_NAME"].GetValue<string>();
                    filter.DealerId = dReader["DEALER_ID"].GetValue<int>();
                    filter.DealerName = dReader["DEALER_NAME"].GetValue<string>();
                    filter.DisplayCurrentAmount = dReader["DISPLAY_CURRENT_AMOUNT"].GetValue<bool>();
                    filter.DisplayCurrentAmountName = dReader["DISPLAY_CURRENT_AMOUNT_NAME"].GetValue<string>();
                    filter.EndDate = dReader["END_DATE"].GetValue<DateTime?>();
                    filter.StartDate = dReader["START_DATE"].GetValue<DateTime?>();
                    filter.CreateUser = dReader["CREATE_USER"].GetValue<string>();
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

        public List<CycleCountViewModel> ListCycleCount(UserInfo user, CycleCountListModel filter, out int totalCnt)
        {
            var cycleCountModel = new List<CycleCountViewModel>();
            totalCnt = 0;
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CYCLE_COUNT");
                db.AddInParameter(cmd, "CYCLE_COUNT_NAME", DbType.String, MakeDbNull(filter.CycleCountName));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "CYCLE_COUNT_STATUS", DbType.Int32, filter.CycleCountStatus);
                db.AddInParameter(cmd, "CYCLE_COUNT_ID", DbType.Int32, MakeDbNull(filter.CycleCountId));
                db.AddInParameter(cmd, "ID_STOCK_TYPE", DbType.Int32, MakeDbNull(filter.StockTypeId));
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);


                using (dReader = cmd.ExecuteReader())
                {
                    while (dReader.Read())
                    {
                        var itemModel = new CycleCountViewModel
                        {
                            encryptedId =
                                CommonUtility.EncryptSymmetric(
                                    dReader["ID_CYCLE_COUNT"].GetValue<string>() + CommonValues.Minus +
                                    user.GetUserDealerId()),
                            CycleCountId = dReader["ID_CYCLE_COUNT"].GetValue<string>(),
                            CycleCountName = dReader["CYCLE_COUNT_NAME"].GetValue<string>(),
                            ConfirmDate = dReader["CONFIRM_DATE"].GetValue<DateTime?>(),
                            StockTypeId = dReader["ID_STOCK_TYPE"].GetValue<int>(),
                            StockTypeName = dReader["STOCK_TYPE_NAME"].GetValue<string>(),
                            CycleCountStatus = dReader["CYCLE_COUNT_STATUS"].GetValue<int?>(),
                            CycleCountStatusName = dReader["CYCLE_COUNT_STATUS_NAME"].GetValue<string>(),
                            EndDate = dReader["END_DATE"].GetValue<DateTime?>(),
                            StartDate = dReader["START_DATE"].GetValue<DateTime?>()
                        };

                        cycleCountModel.Add(itemModel);
                    }

                    dReader.Close();
                }
                totalCnt = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
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

            return cycleCountModel;
        }

        public void LockRack(CycleCountPlanViewModel filter)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_LOCK_CYCLE_COUNT_RACK");
                db.AddInParameter(cmd, "ID_WAREHOUSE", DbType.Int32, MakeDbNull(filter.WarehouseId));
                db.AddInParameter(cmd, "ID_RACK", DbType.Int32, MakeDbNull(filter.RackId));
                db.AddInParameter(cmd, "ID_PART", DbType.Int32, MakeDbNull(filter.PartId));
                db.AddInParameter(cmd, "PROCESS_TYPE", DbType.Int32, (int)filter.Type);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
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

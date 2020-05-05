using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.CycleCountPlan;
using System;

namespace ODMSData
{
    public class CycleCountPlanData : DataAccessBase
    {

        public List<CycleCountPlanListModel> ListCycleCountPlans(UserInfo user,CycleCountPlanListModel filter, out int totalCount)
        {
            var retVal = new List<CycleCountPlanListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CYCLE_COUNT_PLANS");
                db.AddInParameter(cmd, "CYCLE_COUNT_ID", DbType.Int32, MakeDbNull(filter.CycleCountId));
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var cycleCountPlanListModel = new CycleCountPlanListModel
                        {
                            CycleCountPlanId = reader["CYCLE_COUNT_PLAN_ID"].GetValue<int>(),
                            CycleCountId = reader["CYCLE_COUNT_ID"].GetValue<int>(),
                            WarehouseId = reader["WAREHOUSE_ID"].GetValue<int>(),
                            WarehouseName = reader["WAREHOUSE_NAME"].GetValue<string>(),
                            RackId = reader["RACK_ID"].GetValue<int?>(),
                            RackName = reader["RACK_NAME"].GetValue<string>(),
                            StockCardId = reader["STOCK_CARD_ID"].GetValue<int?>(),
                            StockCardName = reader["STOCK_CARD_NAME"].GetValue<string>()
                        };
                        retVal.Add(cycleCountPlanListModel);
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

        public void DMLCycleCountPlan(UserInfo user, CycleCountPlanViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_CYCLE_COUNT_PLAN");
                db.AddParameter(cmd, "CYCLE_COUNT_PLAN_ID", DbType.Int32, ParameterDirection.InputOutput, "CYCLE_COUNT_PLAN_ID", DataRowVersion.Default, model.CycleCountPlanId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "RACK_ID", DbType.String, MakeDbNull(model.RackId));
                db.AddInParameter(cmd, "WAREHOUSE_ID", DbType.Int32, MakeDbNull(model.WarehouseId));
                db.AddInParameter(cmd, "STOCK_CARD_ID", DbType.Int32, model.StockCardId);
                db.AddInParameter(cmd, "CYCLE_COUNT_ID", DbType.Int32, model.CycleCountId);
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, user.GetUserDealerId());
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.CycleCountPlanId = db.GetParameterValue(cmd, "CYCLE_COUNT_PLAN_ID").GetValue<int>();
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

        public CycleCountPlanViewModel GetCycleCountPlan(UserInfo user, CycleCountPlanViewModel filter)
        {
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_CYCLE_COUNT_PLAN");
                db.AddInParameter(cmd, "CYCLE_COUNT_ID", DbType.Int32, MakeDbNull(filter.CycleCountId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.CycleCountPlanId = dReader["CYCLE_COUNT_PLAN_ID"].GetValue<int>();
                    filter.CycleCountId = dReader["CYCLE_COUNT_ID"].GetValue<int>();
                    filter.RackId = dReader["RACK_ID"].GetValue<int>();
                    filter.RackName = dReader["RACK_NAME"].GetValue<string>();
                    filter.WarehouseId = dReader["WAREHOUSE_ID"].GetValue<int>();
                    filter.WarehouseName = dReader["WAREHOUSE_NAME"].GetValue<string>();
                    filter.StockCardId = dReader["STOCK_CARD_ID"].GetValue<int>();
                    filter.StockCardName = dReader["STOCK_CARD_NAME"].GetValue<string>();
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

        public int Exists(int cycleCountId, int warehouseId)
        {
            DbDataReader dReader = null;
            int result = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_EXISTS_CYCLE_COUNT_PLAN");
                db.AddInParameter(cmd, "ID_CYCLE_COUNT", DbType.Int32, cycleCountId);
                db.AddInParameter(cmd, "ID_WAREHOUSE", DbType.Int32, warehouseId);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                if (dReader.Read())
                {
                    result = dReader["Value"].GetValue<int>();
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
            return result;
        }

        public List<CycleCountPlanViewModel> ListById(UserInfo user, CycleCountPlanViewModel filter)
        {
            List<CycleCountPlanViewModel> list = new List<CycleCountPlanViewModel>();
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_CYCLE_COUNT_PLAN_BY_CYCLE_COUNT_ID");
                db.AddInParameter(cmd, "ID_CYCLE_COUNT", DbType.Int32, MakeDbNull(filter.CycleCountId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    CycleCountPlanViewModel viewModel = new CycleCountPlanViewModel();
                    viewModel.CycleCountPlanId = dReader["ID_CYCLE_COUNT_PLAN"].GetValue<int>();
                    viewModel.CycleCountId = dReader["ID_CYCLE_COUNT"].GetValue<int>();
                    viewModel.RackId = dReader["ID_RACK"].GetValue<int>();
                    viewModel.WarehouseId = dReader["ID_WAREHOUSE"].GetValue<int>();
                    viewModel.StockCardId = dReader["ID_STOCK_CARD"].GetValue<int>();
                    list.Add(viewModel);
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
            return list;
        }
    }
}

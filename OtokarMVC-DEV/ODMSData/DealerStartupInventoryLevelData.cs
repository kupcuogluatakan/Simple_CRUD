using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.DealerStartupInventoryLevel;
using ODMSCommon.Resources;
using System;

namespace ODMSData
{
    public class DealerStartupInventoryLevelData : DataAccessBase
    {
        public List<DealerStartupInventoryLevelListModel> ListDealerStartupInventoryLevels(UserInfo user,DealerStartupInventoryLevelListModel filter, out int totalCount)
        {
            var retVal = new List<DealerStartupInventoryLevelListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEALER_STARTUP_INVENTORY_LEVELS");
                db.AddInParameter(cmd, "DEALER_CLASS_CODE", DbType.String, MakeDbNull(filter.DealerClassCode));
                db.AddInParameter(cmd, "PART_CODE", DbType.String, MakeDbNull(filter.PartCode));
                db.AddInParameter(cmd, "PART_NAME", DbType.String, MakeDbNull(filter.PartName));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, filter.IsActive);
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new DealerStartupInventoryLevelListModel
                        {
                            DealerClassCode = reader["DEALER_CLASS_CODE"].ToString(),
                            DealerClassName = reader["DEALER_CLASS_NAME"].ToString(),
                            PartId = reader["ID_PART"].GetValue<int>(),
                            PartCode = reader["PART_CODE"].ToString(),
                            PartName = reader["PART_NAME"].ToString(),
                            Quantity = reader["QUANTITY"].GetValue<decimal>(),
                            PacketQuantity = reader["SHIP_QUANT"].GetValue<decimal?>(),
                            IsActive = reader["IS_ACTIVE"].GetValue<int>(),
                            IsActiveName = reader["ACTIVE_NAME"].ToString()
                        };
                        retVal.Add(listModel);
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

        public DealerStartupInventoryLevelViewModel GetDealerStartupInventoryLevel(DealerStartupInventoryLevelViewModel filter)
        {
            System.Data.Common.DbDataReader reader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_DEALER_STARTUP_INVENTORY_LEVEL");
                db.AddInParameter(cmd, "DEALER_CLASS_CODE", DbType.String, MakeDbNull(filter.DealerClassCode));
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, MakeDbNull(filter.PartId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    filter.DealerClassCode = reader["DEALER_CLASS_CODE"].ToString();
                    filter.DealerClassName = reader["DEALER_CLASS_NAME"].ToString();
                    filter.PartId = reader["ID_PART"].GetValue<int>();
                    filter.PartName = reader["PART_CODE"].ToString();
                    filter.Quantity = reader["QUANTITY"].GetValue<decimal>();
                    filter.PackageQuantity = reader["SHIP_QUANT"].GetValue<decimal?>();
                    filter.IsActive = reader["IS_ACTIVE"].GetValue<bool>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                    reader.Dispose();
                }
                CloseConnection();
            }
            return filter;
        }

        public void DMLDealerStartupInventoryLevel(UserInfo user, DealerStartupInventoryLevelViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_DEALER_STARTUP_INVENTORY_LEVEL");
                db.AddParameter(cmd, "DEALER_CLASS_CODE", DbType.String, ParameterDirection.InputOutput, "DEALER_CLASS_CODE", DataRowVersion.Default, model.DealerClassCode);
                db.AddParameter(cmd, "ID_PART", DbType.Int32, ParameterDirection.InputOutput, "ID_PART", DataRowVersion.Default, model.PartId);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, model.IsActive);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "QUANTITY", DbType.Decimal, MakeDbNull(model.Quantity));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo == 2)
                    model.ErrorMessage = MessageResource.DealerStartupInventoryLevel_Error_NullId;
                else if (model.ErrorNo > 0)
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


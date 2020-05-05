using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSData.DataContracts;
using ODMSModel.FleetPartPartial;
using ODMSCommon.Security;

namespace ODMSData
{
    public class FleetPartPartialData : DataAccessBase, IFleetPartPartial<FleetPartViewModel>
    {
        public IEnumerable<FleetPartPartialListModel> List(UserInfo user, FleetPartPartialListModel model, out int totalCnt)
        {
            var result = new List<FleetPartPartialListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_FLEETPART");
                db.AddInParameter(cmd, "FLEET_ID", DbType.Int32, MakeDbNull(model.FleetId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(model.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(model.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, model.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(model.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, totalCnt);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new FleetPartPartialListModel
                        {
                            FleetId = reader["FleetId"].GetValue<int>(),
                            PartId = reader["PartId"].GetValue<int>(),
                            PartCode = reader["PartCode"].GetValue<string>(),
                            PartName = reader["PartName"].GetValue<string>()
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

        public void Insert(UserInfo user, FleetPartViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_FLEET_PART");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "FLEET_ID", DbType.Int32, model.FleetId);
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, model.PartId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
                {
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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
        }

        public void Insert(UserInfo user, FleetPartViewModel model, List<FleetPartViewModel> listModel)
        {
            if (!listModel.Exists(q => q.ErrorNo == 1))
            {
                bool isSuccess = true;
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_FLEET_PART");
                CreateConnection(cmd);
                DbTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    foreach (var item in listModel)
                    {
                        cmd.Parameters.Clear();
                        db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                        db.AddInParameter(cmd, "FLEET_ID", DbType.Int32, model.FleetId);
                        db.AddInParameter(cmd, "PART_ID", DbType.Int32, item.PartId);
                        db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                        db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                        db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                        db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                        db.ExecuteNonQuery(cmd, transaction);
                        item.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();

                        if (item.ErrorNo > 0)
                        {
                            isSuccess = false;
                            model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                            item.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
                        }
                    }
                }
                catch (Exception Ex)
                {
                    isSuccess = false;
                    model.ErrorNo = 1;
                    model.ErrorMessage = Ex.Message;
                }
                finally
                {
                    if (isSuccess)
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                    CloseConnection();
                }
            }
        }

        public void Delete(UserInfo user, FleetPartViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_FLEET_PART");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "FLEET_ID", DbType.Int32, model.FleetId);
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, model.PartId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
                {
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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
        }

        public bool Exists(UserInfo user, FleetPartViewModel model)
        {
            var result = false;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_EXISTS_SPARE_PART_BY_PART_CODE");
                db.AddInParameter(cmd, "PART_CODE", DbType.String, model.PartName);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = reader["Count"].GetValue<bool>();
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

        public bool IsPartConstricted(UserInfo user, FleetPartViewModel model)
        {
            var result = false;
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_EXISTS_FLEET_IS_PART_CONSTRICTED_BY_FLEET_ID");
                db.AddInParameter(cmd, "ID_FLEET", DbType.Int32, model.FleetId);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = reader["IS_PART_CONSTRICTED"].GetValue<bool>();
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
    }
}

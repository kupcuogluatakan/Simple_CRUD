using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;
using ODMSCommon;
using ODMSModel.WorkshopWorker;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSData
{
    public class WorkshopWorkerData : DataAccessBase
    {
        public WorkshopWorkerIndexModel GetWorkshopWorkerIndexModel()
        {
            return new WorkshopWorkerIndexModel
            {
                WorkerList = GetWorkerList(),
                WorkshopList = GetWorkshopList()
            };
        }

        public List<WorkshopWorkerListModel> ListWorkshopWorkers(WorkshopWorkerListModel filter, out int totalCnt)
        {
            var result = new List<WorkshopWorkerListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_WORKSHOP_DMS_USER");
                db.AddInParameter(cmd, "ID_WORKSHOP", DbType.Int64, MakeDbNull(filter.WorkshopId));
                db.AddInParameter(cmd, "ID_DMS_USER", DbType.Int64, MakeDbNull(filter.WorkerId));
                db.AddInParameter(cmd, "START_DATE", DbType.DateTime, MakeDbNull(filter.StartDate));
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, MakeDbNull(filter.EndDate));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, MakeDbNull(filter.SearchIsActive));
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
                        var listModel = new WorkshopWorkerListModel
                        {
                            WorkshopId = reader["ID_WORKSHOP"].GetValue<int>(),
                            WorkerId = reader["ID_DMS_USER"].GetValue<int>(),
                            WorkshopName = reader["WORKSHOP_NAME"].GetValue<string>(),
                            WorkerName = reader["WORKER_NAME"].GetValue<string>(),
                            StartDate = reader["VALID_START_DATE"].GetValue<DateTime>(),
                            EndDate = reader["VALID_END_DATE"].GetValue<DateTime>(),
                            IsActive = reader["IS_ACTIVE"].GetValue<bool>()
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

        public WorkshopWorkerDetailModel GetWorkshopWorker(WorkshopWorkerDetailModel filter)
        {
            DbDataReader reader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_WORKSHOP_WORKER");
                db.AddInParameter(cmd, "ID_WORKSHOP", DbType.Int32, MakeDbNull(filter.WorkshopId));
                db.AddInParameter(cmd, "ID_DMS_USER", DbType.String, MakeDbNull(filter.WorkerId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    filter.WorkshopId = reader["ID_WORKSHOP"].GetValue<int>();
                    filter.WorkerId = reader["ID_DMS_USER"].GetValue<int>();
                    filter.WorkshopName = reader["WORKSHOP_NAME"].GetValue<string>();
                    filter.WorkerName = reader["WORKER_NAME"].GetValue<string>();
                    filter.StartDate = reader["VALID_START_DATE"].GetValue<DateTime>();
                    filter.EndDate = reader["VALID_END_DATE"].GetValue<DateTime>();
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

        public void DMLWorkshopWorker(UserInfo user, WorkshopWorkerDetailModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_DML_WORKSHOP_DMS_USER");
                db.AddInParameter(cmd, "ID_WORKSHOP", DbType.Int32, model.WorkshopId);
                db.AddInParameter(cmd, "ID_DMS_USER", DbType.Int32, model.WorkerId);
                db.AddInParameter(cmd, "START_DATE", DbType.DateTime, MakeDbNull(model.StartDate));
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, MakeDbNull(model.EndDate));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, MakeDbNull(model.IsActive));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo == 2)
                    model.ErrorMessage = MessageResource.WorkshopUser_Error_NullId;
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

        public List<SelectListItem> GetWorkshopList()
        {
            var result = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_WORKSHOPS_SHORT");
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new SelectListItem
                        {
                            Value = reader["ID_WORKSHOP"].GetValue<string>(),
                            Text = reader["WORKSHOP_NAME"].GetValue<string>(),
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

        public List<SelectListItem> GetWorkerList()
        {
            var result = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_USERS_SHORT_NAMESURNAME");
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(null));
                db.AddInParameter(cmd, "IS_TECHNICIAN", DbType.Int32, false);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, true);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new SelectListItem
                        {
                            Value = reader["ID_DMS_USER"].GetValue<string>(),
                            Text = reader["WORKER_NAME"].GetValue<string>(),
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
    }
}

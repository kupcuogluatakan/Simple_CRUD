using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;
using ODMSCommon;
using System.Linq;
using ODMSModel.LabourTechnician;
using ODMSCommon.Security;

namespace ODMSData
{
    public class LabourTechnicianData : DataAccessBase
    {
        public List<LabourTechnicianListModel> ListLabourTechnicians(UserInfo user, LabourTechnicianListModel filter, out int total)
        {
            var retVal = new List<LabourTechnicianListModel>();
            total = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_LABOUR_TECHNICIANS");

                db.AddInParameter(cmd, "WORK_ORDER_DETAIL_ID", DbType.Int32, MakeDbNull(filter.WorkOrderDetailId));
                db.AddInParameter(cmd, "STATUS_ID", DbType.Int32, (filter.StatusId).HasValue ? filter.StatusId : null);//***Orhan
                db.AddInParameter(cmd, "LABOUR_NAME", DbType.String, MakeDbNull(filter.LabourName));
                db.AddInParameter(cmd, "LABOUR_CODE", DbType.String, MakeDbNull(filter.LabourCode));
                db.AddInParameter(cmd, "WORK_ORDER_ID", DbType.Int32, MakeDbNull(filter.WorkOrderId));
                db.AddInParameter(cmd, "PLATE", DbType.String, MakeDbNull(filter.Plate));
                db.AddInParameter(cmd, "LABOUR_WORK_TIME_ESTIMATE", DbType.String, MakeDbNull(filter.WorkTimeEstimate));
                db.AddInParameter(cmd, "LABOUR_WORK_TIME_REAL", DbType.String, MakeDbNull(filter.WorkTimeReal));
                db.AddInParameter(cmd, "ID_DMS_USER", DbType.Int32, MakeDbNull(filter.UserID));
                db.AddInParameter(cmd, "CREATE_DATE", DbType.DateTime, MakeDbNull(filter.CreateDate));
                db.AddInParameter(cmd, "START_DATE", DbType.DateTime, MakeDbNull(filter.StartDate));
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, MakeDbNull(filter.EndDate));
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(filter.DealerId));
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
                        var item = new LabourTechnicianListModel
                        {
                            CreateDate = reader["STATUS_ID"].GetValue<int>() == 0 ? null : reader["CREATE_DATE"].GetValue<DateTime?>(),
                            EndDate = reader["END_DATE"].GetValue<DateTime?>(),
                            LabourId = reader["LABOUR_ID"].GetValue<int>(),
                            LabourCode = reader["LABOUR_CODE"].GetValue<string>(),
                            LabourName = reader["LABOUR_NAME"].GetValue<string>(),
                            LabourTechnicianId = reader["LABOUR_TECHNICIAN_ID"].GetValue<int>(),
                            Plate = reader["PLATE"].GetValue<string>(),
                            StartDate = reader["START_DATE"].GetValue<DateTime?>(),
                            StatusId = reader["STATUS_ID"].GetValue<int>(),
                            StatusName = reader["STATUS_NAME"].GetValue<string>(),
                            UserID = reader["USER_ID"].GetValue<int>(),
                            UserNameSurname = reader["USER_NAME_SURNAME"].GetValue<string>(),
                            WorkOrderDetailId = reader["WORK_ORDER_DETAIL_ID"].GetValue<int>(),
                            WorkOrderId = reader["WORK_ORDER_ID"].GetValue<int>(),
                            WorkTimeEstimate = reader["WORK_TIME_ESTIMATE"].GetValue<string>(),                          
                            LabourTimeEstimate = reader["LABOUR_TIME_ESTIMATE"].GetValue<string>(),
                            WorkTimeReal = reader["WORK_TIME_REAL"].GetValue<string>(),
                            VinNo = reader["VIN_NO"].GetValue<string>(),
                            CustomerName = reader["CUSTOMER_NAME"].GetValue<string>()
                        };
                        retVal.Add(item);
                    }
                    reader.Close();
                }
                total = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
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
        public void DMLLabourTechnician(UserInfo user, LabourTechnicianViewModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_DML_LABOUR_TECHNICIAN");
                db.AddInParameter(cmd, "LABOUR_TECHNICIAN_ID", DbType.Int32, MakeDbNull(model.LabourTechnicianId));
                db.AddInParameter(cmd, "WORK_ORDER_DETAIL_ID", DbType.Int32, MakeDbNull(model.WorkOrderDetailId));
                db.AddInParameter(cmd, "LABOUR_ID", DbType.Int32, MakeDbNull(model.LabourId));
                db.AddInParameter(cmd, "STATUS_ID", DbType.Int32, model.StatusId);
                db.AddInParameter(cmd, "WORK_TIME_ESTIMATE", DbType.String, MakeDbNull(model.WorkTimeEstimate));
                db.AddInParameter(cmd, "WORK_TIME_REAL", DbType.String, MakeDbNull(model.WorkTimeReal));
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
            if (model.ErrorNo <= 0)
            {
                DMLLabourTechnicianUser(model);
            }
        }

        private string DeleteAllTechnicianUser(int labourTechnicianId)
        {
            string errorMessage = string.Empty;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_DML_LABOUR_TECHNICIAN_USER");
                db.AddInParameter(cmd, "LABOUR_TECHNICIAN_ID", DbType.Int32, MakeDbNull(labourTechnicianId));
                db.AddInParameter(cmd, "USER_ID", DbType.String, MakeDbNull(null));
                db.AddInParameter(cmd, "WORK_TIME_REAL", DbType.String, MakeDbNull(null));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, MakeDbNull(CommonValues.DMLType.Delete));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(UserManager.UserInfo.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                int errorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                errorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (errorNo > 0)
                    errorMessage = ResolveDatabaseErrorXml(errorMessage);                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return errorMessage;
        }
        public void DMLLabourTechnicianUser(LabourTechnicianViewModel model)
        {            
            if (model.TecnicianUsers != null)
            {
                model.TecnicianUsers = model.TecnicianUsers.Where(e => e.TechnicianUserId != 0).ToList();

                List<int> listOfUsers = GetLabourTechnicianUser(model.LabourTechnicianId);
                foreach (var item in model.TecnicianUsers)
                {
                    if (listOfUsers.Contains(item.TechnicianUserId))
                    {
                        item.CommandType = CommonValues.DMLType.Update;
                    }
                    else
                    {
                        item.CommandType = CommonValues.DMLType.Insert;
                    }
                }

                var deleteList = listOfUsers.Where(e => !model.TecnicianUsers.Select(r => r.TechnicianUserId).Contains(e));
                foreach (var item in deleteList)
                {
                    LabourTecnicianUserModel deleted = new LabourTecnicianUserModel();
                    deleted.TechnicianUserId = item;
                    deleted.CommandType = CommonValues.DMLType.Delete;
                    model.TecnicianUsers.Add(deleted);
                }

                foreach (var item in model.TecnicianUsers)
                {
                    try
                    {
                        CreateDatabase();
                        var cmd = db.GetStoredProcCommand("P_DML_LABOUR_TECHNICIAN_USER");
                        db.AddInParameter(cmd, "LABOUR_TECHNICIAN_ID", DbType.Int32, MakeDbNull(model.LabourTechnicianId));
                        db.AddInParameter(cmd, "USER_ID", DbType.String, MakeDbNull(item.TechnicianUserId));
                        db.AddInParameter(cmd, "WORK_TIME_REAL", DbType.String, MakeDbNull(item.WorkTime));
                        db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, MakeDbNull(item.CommandType));
                        db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(UserManager.UserInfo.UserId));
                        db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                        db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                        CreateConnection(cmd);
                        cmd.ExecuteNonQuery();

                        model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                        model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                        if (model.ErrorNo > 0)
                        {
                            model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
                            return;
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
            }
            else
            {
                string errorMessage = DeleteAllTechnicianUser(model.LabourTechnicianId);
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = errorMessage;
                }
            }
        }

        public LabourTechnicianViewModel GetLabourTechnician(UserInfo user, LabourTechnicianViewModel filter)
        {
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_LABOUR_TECHNICIAN");
                db.AddInParameter(cmd, "LABOUR_TECHNICIAN_ID", DbType.Int32, MakeDbNull(filter.LabourTechnicianId));
                db.AddInParameter(cmd, "WORK_ORDER_DETAIL_ID", DbType.Int32, MakeDbNull(filter.WorkOrderDetailId));
                db.AddInParameter(cmd, "LABOUR_ID", DbType.Int32, MakeDbNull(filter.LabourId));
                db.AddInParameter(cmd, "STATUS_ID", DbType.Int32, MakeDbNull(filter.StatusId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.CreateDate = dReader["CREATE_DATE"].GetValue<DateTime>();
                    filter.EndDate = dReader["END_DATE"].GetValue<DateTime?>();
                    filter.LabourId = dReader["LABOUR_ID"].GetValue<int>();
                    filter.LabourCode = dReader["LABOUR_CODE"].GetValue<string>();
                    filter.LabourName = dReader["LABOUR_NAME"].GetValue<string>();
                    filter.LabourTechnicianId = dReader["LABOUR_TECHNICIAN_ID"].GetValue<int>();
                    filter.Plate = dReader["PLATE"].GetValue<string>();
                    filter.StartDate = dReader["START_DATE"].GetValue<DateTime?>();
                    filter.StatusId = dReader["STATUS_ID"].GetValue<int>();
                    filter.StatusName = dReader["STATUS_NAME"].GetValue<string>();
                    filter.UserNameSurname = dReader["USER_NAME_SURNAME"].GetValue<string>();
                    filter.WorkOrderDetailId = dReader["WORK_ORDER_DETAIL_ID"].GetValue<int>();
                    filter.WorkOrderId = dReader["WORK_ORDER_ID"].GetValue<int>();
                    filter.WorkTimeEstimate = dReader["WORK_TIME_ESTIMATE"].GetValue<decimal?>();
                    filter.LabourTimeEstimate = dReader["LABOUR_TIME_ESTIMATE"].GetValue<decimal?>();
                    filter.WorkTimeReal = dReader["WORK_TIME_REAL"].GetValue<decimal?>();
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
            filter.Users = GetLabourTechnicianUser(filter.LabourTechnicianId);
            return filter;
        }
        public List<int> GetLabourTechnicianUser(int labourTechnicianId)
        {
            DbDataReader dReader = null;
            List<int> users = new List<int>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_LABOUR_TECHNICIAN_USER");
                db.AddInParameter(cmd, "LABOUR_TECHNICIAN_ID", DbType.Int32, MakeDbNull(labourTechnicianId));
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    users.Add(dReader["ID_DMS_USER"].GetValue<int>());
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

            return users;
        }

        public List<LabourTechnicianViewModel> GetLabourTechnicianStartFinish(LabourTechnicianViewModel filter)
        {
            List<LabourTechnicianViewModel> list = new List<LabourTechnicianViewModel>();
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_LABOUR_TECHNICIAN_START_FINISH");
                db.AddInParameter(cmd, "LABOUR_TECHNICIAN_ID", DbType.Int32, MakeDbNull(filter.LabourTechnicianId));
                db.AddInParameter(cmd, "USER_ID", DbType.Int32, MakeDbNull(filter.UserID));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    LabourTechnicianViewModel model = new LabourTechnicianViewModel();
                    model.EndDate = dReader["END_DATE"].GetValue<DateTime?>();
                    model.StartDate = dReader["START_DATE"].GetValue<DateTime?>();
                    list.Add(model);
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

        public List<SelectListItem> ListTechnicianAsSelectList(int? dealerId)
        {
            var result = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_USERS_SHORT_NAMESURNAME");
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(dealerId));
                db.AddInParameter(cmd, "IS_TECHNICIAN", DbType.Int32, true);
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

        public void DMLLabourTechnicianStartFinish(LabourTechnicianViewModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_DML_LABOUR_TECHNICIAN_START_FINISH");
                db.AddInParameter(cmd, "LABOUR_TECHNICIAN_ID", DbType.Int32, MakeDbNull(model.LabourTechnicianId));
                db.AddInParameter(cmd, "USER_ID", DbType.Int32, MakeDbNull(model.UserID));
                db.AddInParameter(cmd, "START_DATE", DbType.DateTime, MakeDbNull(model.StartDate));
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, MakeDbNull(model.EndDate));
                db.AddInParameter(cmd, "LABOUR_WORK_STATUS_LOOKVAL", DbType.Int32, model.StatusId);
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

        public List<LabourTecnicianUserModel> GetLabourTecnicianInfo(int labourTechnicianId)
        {
            DbDataReader dReader = null;
            List<LabourTecnicianUserModel> users = new List<LabourTecnicianUserModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_LABOUR_TECHNICIAN_USER_INFO");
                db.AddInParameter(cmd, "LABOUR_TECHNICIAN_ID", DbType.Int32, MakeDbNull(labourTechnicianId));
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    users.Add(new LabourTecnicianUserModel()
                    {
                        TechnicianUserId = dReader["ID_DMS_USER"].GetValue<int>(),
                        TechnicianUserName = dReader["FULL_NAME"].GetValue<string>(),
                        WorkTime = dReader["LABOUR_WORK_TIME_ESTIMATE"].GetValue<decimal>(),
                        StatusName = dReader["LABOUR_STATUS"].GetValue<string>()
                    });
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
            return users;
        }
        
        public LabourTechnicianViewModel CancelLabourTecnician(UserInfo user, LabourTechnicianViewModel filter)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_CANCEL_LABOUR_TECHNICIAN");
                db.AddInParameter(cmd, "LABOUR_TECHNICIAN_ID", DbType.Int32, MakeDbNull(filter.LabourTechnicianId));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
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
            return filter;
        }

    }
}

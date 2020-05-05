﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSData.DataContracts;
using ODMSModel.LabourMainGroup;
using ODMSModel.ViewModel;

namespace ODMSData
{
    public class LabourMainGroupData : DataAccessBase, ILabourMainGroup<LabourMainGroupViewModel>
    {
        public void Delete(UserInfo user,LabourMainGroupViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_LABOUR_MAIN_GROUP");
                db.AddInParameter(cmd, "ID_MAIN_GROUP", DbType.String, MakeDbNull(model.MainGroupId));
                db.AddInParameter(cmd, "ML_CONTENT", DbType.String, model.MultiLanguageContentAsText);
                db.AddInParameter(cmd, "DESCRIPTION", DbType.String, MakeDbNull(model.Description));               
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, MakeDbNull(model.IsActive));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
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

        public LabourMainGroupViewModel Get(UserInfo user, LabourMainGroupViewModel filter)
        {
            DbDataReader reader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_LABOUR_MAIN_GROUP");
                db.AddInParameter(cmd, "ID_MAIN_GROUP", DbType.String, MakeDbNull(filter.MainGroupId));                
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    filter.MainGroupId = reader["ID_MAIN_GROUP"].GetValue<string>();
                    filter.Description = reader["ADMIN_DESC"].GetValue<string>();                  
                    filter.IsActive = reader["IS_ACTIVE"].GetValue<bool>();
                    filter.LabourGroupName = reader["LABOUR_GROUP_DESC"].GetValue<string>();                  
                }

                if (reader.NextResult())
                {
                    var table = new DataTable();
                    table.Load(reader);
                    filter.MultiLanguageContentAsText = GetLanguageContentFromDataSet(table, "LABOUR_GROUP_DESC");
                    filter.MultiLanguageName = (MultiLanguageModel)CommonUtility.DeepClone(filter.MultiLanguageName);
                    filter.MultiLanguageName.MultiLanguageContentAsText = filter.MultiLanguageContentAsText;
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

        public void Insert(UserInfo user, LabourMainGroupViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_LABOUR_MAIN_GROUP");
                db.AddInParameter(cmd, "ID_MAIN_GROUP", DbType.String, MakeDbNull(model.MainGroupId));
                db.AddInParameter(cmd, "ML_CONTENT", DbType.String, model.MultiLanguageContentAsText);
                db.AddInParameter(cmd, "DESCRIPTION", DbType.String, MakeDbNull(model.Description));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, MakeDbNull(model.IsActive));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
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

        public void Insert(UserInfo user, LabourMainGroupViewModel model, List<LabourMainGroupViewModel> listModel)
        {
            if (!listModel.Exists(q => q.ErrorNo == 1))
            {
                bool isSuccess = true;
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_LABOUR_MAIN_GROUP");
                CreateConnection(cmd);
                DbTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    foreach (var item in listModel)
                    {
                        cmd.Parameters.Clear();
                        db.AddInParameter(cmd, "ID_MAIN_GROUP", DbType.String, MakeDbNull(item.MainGroupId));
                        db.AddInParameter(cmd, "ML_CONTENT", DbType.String, item.MultiLanguageContentAsText);
                        db.AddInParameter(cmd, "DESCRIPTION", DbType.String, MakeDbNull(item.Description));
                        db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, MakeDbNull(item.IsActive));
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
                catch (Exception ex)
                {
                    isSuccess = false;
                    model.ErrorNo = 1;
                    model.ErrorMessage = ex.Message;
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

        public List<LabourMainGroupListModel> List(UserInfo user, LabourMainGroupListModel filter, out int totalCnt)
        {
            var result = new List<LabourMainGroupListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_LABOUR_MAIN_GROUP");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, filter.IsActive);
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
                        var listModel = new LabourMainGroupListModel
                        {
                            MainGroupId = reader["ID_MAIN_GROUP"].GetValue<string>(),
                            Description = reader["ADMIN_DESC"].GetValue<string>(),                           
                            IsActiveString = reader["IS_ACTIVE"].GetValue<string>(),
                            LabourGroupName = reader["LABOUR_GROUP_DESC"].GetValue<string>()
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

        public void Update(UserInfo user, LabourMainGroupViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_LABOUR_MAIN_GROUP");
                db.AddInParameter(cmd, "ID_MAIN_GROUP", DbType.String, MakeDbNull(model.MainGroupId));
                db.AddInParameter(cmd, "ML_CONTENT", DbType.String, model.MultiLanguageContentAsText);
                db.AddInParameter(cmd, "DESCRIPTION", DbType.String, MakeDbNull(model.Description));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, MakeDbNull(model.IsActive));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
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
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;
using ODMSCommon;
using ODMSModel.AppointmentIndicatorCategory;
using ODMSCommon.Security;

namespace ODMSData
{
    public class AppointmentIndicatorCategoryData : DataAccessBase
    {
        public List<SelectListItem> ListAppointmentIndicatorCategories(UserInfo user, int? mainCategoryId, bool? isActive)
        {
            var retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_APPOINTMENT_INDICATOR_CATEGORY_COMBO");
                db.AddInParameter(cmd, "APPOINTMENT_INDICATOR_MAIN_CATEGORY_ID", DbType.Int32, MakeDbNull(mainCategoryId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, MakeDbNull(isActive));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new SelectListItem
                        {
                            Value = reader["APPOINTMENT_INDICATOR_CATEGORY_ID"].GetValue<string>(),
                            Text = reader["DESCRIPTION"].GetValue<string>()
                        };
                        retVal.Add(item);
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

            return retVal;
        }

        public Dictionary<string, int> DictionaryAppointmentIndicatorCategories(UserInfo user, int? mainCategoryId, bool? isActive)
        {
            var resultList = new Dictionary<string, int>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_APPOINTMENT_INDICATOR_CATEGORY_COMBO");
                db.AddInParameter(cmd, "APPOINTMENT_INDICATOR_MAIN_CATEGORY_ID", DbType.Int32, MakeDbNull(mainCategoryId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, MakeDbNull(isActive));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        resultList.Add(reader["CODE"].GetValue<string>(), reader["APPOINTMENT_INDICATOR_CATEGORY_ID"].GetValue<int>());
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

            return resultList;
        }


        public List<SelectListItem> ListAppointmentIndicatorCategories(UserInfo user, int? mainCategoryId, string typeCode)
        {
            var retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_APPOINTMENT_INDICATOR_CATEGORY_BY_INDIC_TYPE_COMBO");
                db.AddInParameter(cmd, "APPOINTMENT_INDICATOR_MAIN_CATEGORY_ID", DbType.Int32, MakeDbNull(mainCategoryId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "INDICATOR_TYPE_CODE", DbType.String, MakeDbNull(typeCode));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new SelectListItem
                        {
                            Value = reader["APPOINTMENT_INDICATOR_CATEGORY_ID"].GetValue<string>(),
                            Text = reader["DESCRIPTION"].GetValue<string>()
                        };
                        retVal.Add(item);
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

            return retVal;
        }
        public List<SelectListItem> ListAppointmentIndicatorCategoryCodes(UserInfo user, bool? isActive)
        {
            var retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_APPOINTMENT_INDICATOR_CATEGORY_COMBO");
                db.AddInParameter(cmd, "APPOINTMENT_INDICATOR_MAIN_CATEGORY_ID", DbType.Int32, MakeDbNull(null));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, MakeDbNull(isActive));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new SelectListItem
                        {
                            Value = reader["APPOINTMENT_INDICATOR_CATEGORY_ID"].GetValue<string>(),
                            Text = reader["CODE"].GetValue<string>()
                        };
                        retVal.Add(item);
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

            return retVal;
        }
        public List<AppointmentIndicatorCategoryListModel> GetAppointmentIndicatorCategoryList(UserInfo user,AppointmentIndicatorCategoryListModel filter, out int totalCount)
        {
            List<AppointmentIndicatorCategoryListModel> list_AppointmentIndicatorCategoryM = new List<AppointmentIndicatorCategoryListModel>();

            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_APPOINTMENT_INDICATOR_CATEGORIES");
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, filter.IsActive);
                db.AddInParameter(cmd, "MAIN_CATEGORY_ID", DbType.Int32, MakeDbNull(filter.AppointmentIndicatorMainCategoryId));
                db.AddInParameter(cmd, "CATEGORY_ID", DbType.Int32, MakeDbNull(filter.AppointmentIndicatorCategoryId));
                db.AddInParameter(cmd, "CATEGORY_DESC", DbType.String, MakeDbNull(filter.AdminDesc));
                AddPagingParametersWithLanguage(user, cmd, filter);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        AppointmentIndicatorCategoryListModel model = new AppointmentIndicatorCategoryListModel
                        {
                            AppointmentIndicatorCategoryId = dr["APPOINTMENT_INDICATOR_CATEGORY_ID"].GetValue<int>(),
                            AdminDesc = dr["ADMIN_DESC"].GetValue<string>(),
                            AppointmentIndicatorCategoryName = dr["APPOINTMENT_INDICATOR_CATEGORY_NAME"].GetValue<string>(),
                            Code = dr["CODE"].GetValue<string>(),
                            IsActive = dr["IS_ACTIVE"].GetValue<int?>(),
                            IsActiveName = dr["IS_ACTIVE_NAME"].GetValue<string>(),
                            AppointmentIndicatorMainCategoryId = dr["MAIN_CATEGORY_ID"].GetValue<int>(),
                            AppointmentIndicatorMainCategoryName = dr["MAIN_CATEGORY_NAME"].GetValue<string>()
                        };

                        list_AppointmentIndicatorCategoryM.Add(model);
                    }
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

            return list_AppointmentIndicatorCategoryM;
        }

        public List<AppointmentIndicatorCategoryViewModel> GetAppointmentIndicatorCategoryList(UserInfo user,AppointmentIndicatorCategoryListModel filter, out int totalCount, bool overload)
        {
            List<AppointmentIndicatorCategoryViewModel> list_AppointmentIndicatorCategoryM = new List<AppointmentIndicatorCategoryViewModel>();

            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_APPOINTMENT_INDICATOR_CATEGORIES");
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, filter.IsActive);
                db.AddInParameter(cmd, "MAIN_CATEGORY_ID", DbType.Int32, MakeDbNull(filter.AppointmentIndicatorMainCategoryId));
                db.AddInParameter(cmd, "CATEGORY_ID", DbType.Int32, MakeDbNull(filter.AppointmentIndicatorCategoryId));
                db.AddInParameter(cmd, "CATEGORY_DESC", DbType.String, MakeDbNull(filter.AdminDesc));
                AddPagingParametersWithLanguage(user, cmd, filter);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        AppointmentIndicatorCategoryViewModel model = new AppointmentIndicatorCategoryViewModel
                        {
                            AppointmentIndicatorCategoryId = dr["APPOINTMENT_INDICATOR_CATEGORY_ID"].GetValue<int>(),
                            AdminDesc = dr["ADMIN_DESC"].GetValue<string>(),
                            _AppointmentIndicatorCategoryName = dr["APPOINTMENT_INDICATOR_CATEGORY_NAME"].GetValue<string>(),
                            Code = dr["CODE"].GetValue<string>(),
                            IsActiveName = dr["IS_ACTIVE_NAME"].GetValue<string>(),
                            AppointmentIndicatorMainCategoryId = dr["MAIN_CATEGORY_ID"].GetValue<int>(),
                            AppointmentIndicatorMainCategoryName = dr["MAIN_CATEGORY_NAME"].GetValue<string>()
                        };

                        list_AppointmentIndicatorCategoryM.Add(model);
                    }
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

            return list_AppointmentIndicatorCategoryM;
        }


        public void DMLAppointmentIndicatorCategory(UserInfo user, AppointmentIndicatorCategoryViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_APPOINTMENT_INDICATOR_CATEGORY_MAIN");

                db.AddParameter(cmd, "APPOINTMENT_INDICATOR_CATEGORY_ID", DbType.Int32, ParameterDirection.InputOutput, "APPOINTMENT_INDICATOR_CATEGORY_ID", DataRowVersion.Default, model.AppointmentIndicatorCategoryId);
                db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, MakeDbNull(model.AdminDesc));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, model.IsActive);
                db.AddInParameter(cmd, "APPOINTMENT_INDICATOR_MAIN_CATEGORY_ID", DbType.Int32, MakeDbNull(model.AppointmentIndicatorMainCategoryId));
                db.AddInParameter(cmd, "CODE", DbType.String, MakeDbNull(model.Code));
                db.AddInParameter(cmd, "ML_CONTENT", DbType.String, model.MultiLanguageContentAsText);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.AppointmentIndicatorCategoryId = db.GetParameterValue(cmd, "APPOINTMENT_INDICATOR_CATEGORY_ID").GetValue<int>();
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

        public void DMLAppointmentIndicatorCategory(UserInfo user, AppointmentIndicatorCategoryViewModel model, List<AppointmentIndicatorCategoryViewModel> filter)
        {
            if (!filter.Exists(q => q.ErrorNo == 1))
            {
                bool isSuccess = true;
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_APPOINTMENT_INDICATOR_CATEGORY_MAIN");
                CreateConnection(cmd);
                DbTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    foreach (var item in filter)
                    {
                        cmd.Parameters.Clear();
                        db.AddParameter(cmd, "APPOINTMENT_INDICATOR_CATEGORY_ID", DbType.Int32, ParameterDirection.InputOutput, "APPOINTMENT_INDICATOR_CATEGORY_ID", DataRowVersion.Default, item.AppointmentIndicatorCategoryId);
                        db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, MakeDbNull(item.AdminDesc));
                        db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, item.CommandType);
                        db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, item.IsActive);
                        db.AddInParameter(cmd, "APPOINTMENT_INDICATOR_MAIN_CATEGORY_ID", DbType.Int32, MakeDbNull(item.AppointmentIndicatorMainCategoryId));
                        db.AddInParameter(cmd, "CODE", DbType.String, MakeDbNull(item.Code));
                        db.AddInParameter(cmd, "ML_CONTENT", DbType.String, item.MultiLanguageContentAsText);
                        db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
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

        public void GetAppointmentIndicatorCategory(UserInfo user, AppointmentIndicatorCategoryViewModel filter)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_APPOINTMENT_INDICATOR_CATEGORY");

                db.AddInParameter(cmd, "APPOINTMENT_INDICATOR_CATEGORY_ID", DbType.Int32, filter.AppointmentIndicatorCategoryId);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        filter.AdminDesc = dr["ADMIN_DESC"].GetValue<string>();
                        filter.IsActive = dr["IS_ACTIVE"].GetValue<bool>();
                        filter.AppointmentIndicatorCategoryId = dr["APPOINTMENT_INDICATOR_CATEGORY_ID"].GetValue<int>();
                        filter.Code = dr["CODE"].GetValue<string>();
                        filter.IsActiveName = dr["IS_ACTIVE_NAME"].GetValue<string>();
                        filter.AppointmentIndicatorMainCategoryId = dr["MAIN_CATEGORY_ID"].GetValue<int>();
                        filter.AppointmentIndicatorMainCategoryName = dr["MAIN_CATEGORY_NAME"].GetValue<string>();
                    }
                    if (dr.NextResult())
                    {
                        var table = new DataTable();
                        table.Load(dr);
                        filter.AppointmentIndicatorCategoryName.MultiLanguageContentAsText = filter.MultiLanguageContentAsText
                                                                          = GetLanguageContentFromDataSet(table, "APPOINTMENT_INDICATOR_CATEGORY_NAME");
                    }
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

        public AppointmentIndicatorCategoryViewModel GetAppointmentIndicatorCategoryByCode(UserInfo user, string code)
        {
            AppointmentIndicatorCategoryViewModel appointmentIndicatorCategoryModel =
                new AppointmentIndicatorCategoryViewModel();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_APPOINTMENT_INDICATOR_CATEGORY_BY_CODE");

                db.AddInParameter(cmd, "CODE", DbType.String, code);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        appointmentIndicatorCategoryModel.AdminDesc = dr["ADMIN_DESC"].GetValue<string>();
                        appointmentIndicatorCategoryModel.IsActive = dr["IS_ACTIVE"].GetValue<bool>();
                        appointmentIndicatorCategoryModel.AppointmentIndicatorCategoryId = dr["APPOINTMENT_INDICATOR_CATEGORY_ID"].GetValue<int>();
                        appointmentIndicatorCategoryModel.Code = dr["CODE"].GetValue<string>();
                        appointmentIndicatorCategoryModel.IsActiveName = dr["IS_ACTIVE_NAME"].GetValue<string>();
                        appointmentIndicatorCategoryModel.AppointmentIndicatorMainCategoryId = dr["MAIN_CATEGORY_ID"].GetValue<int>();
                        appointmentIndicatorCategoryModel.AppointmentIndicatorMainCategoryName = dr["MAIN_CATEGORY_NAME"].GetValue<string>();
                    }
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

            return appointmentIndicatorCategoryModel;
        }
    }
}

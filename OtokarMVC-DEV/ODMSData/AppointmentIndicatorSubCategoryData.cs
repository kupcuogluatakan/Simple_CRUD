using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;
using ODMSCommon;
using ODMSModel.AppointmentIndicatorSubCategory;
using ODMSCommon.Security;

namespace ODMSData
{
    public class AppointmentIndicatorSubCategoryData : DataAccessBase
    {
        public List<SelectListItem> ListAppointmentIndicatorSubCategories(UserInfo user, int? categoryId, bool? isActive)
        {
            var retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_APPOINTMENT_INDICATOR_SUB_CATEGORY_COMBO");
                db.AddInParameter(cmd, "APPOINTMENT_INDICATOR_CATEGORY_ID", DbType.Int32, MakeDbNull(categoryId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, MakeDbNull(isActive));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new SelectListItem
                        {
                            Value = reader["APPOINTMENT_INDICATOR_SUB_CATEGORY_ID"].GetValue<string>(),
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
        public List<SelectListItem> ListAppointmentIndicatorSubCategories(UserInfo user, int id, string typeCode)
        {
            var retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_APPOINTMENT_INDICATOR_SUB_CATEGORY_BY_INDIC_TYPE_COMBO");
                db.AddInParameter(cmd, "APPOINTMENT_INDICATOR_CATEGORY_ID", DbType.Int32, MakeDbNull(id));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "INDICATOR_TYPE_CODE", DbType.String, MakeDbNull(typeCode));

                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new SelectListItem
                        {
                            Value = reader["APPOINTMENT_INDICATOR_SUB_CATEGORY_ID"].GetValue<string>(),
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
        public List<SelectListItem> ListAppointmentIndicatorSubCategoryCodes(UserInfo user, bool? isActive)
        {
            var retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_APPOINTMENT_INDICATOR_SUB_CATEGORY_COMBO");
                db.AddInParameter(cmd, "APPOINTMENT_INDICATOR_CATEGORY_ID", DbType.Int32, MakeDbNull(null));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, MakeDbNull(isActive));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new SelectListItem
                        {
                            Value = reader["APPOINTMENT_INDICATOR_SUB_CATEGORY_ID"].GetValue<string>(),
                            Text = reader["SUB_CODE"].GetValue<string>()
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

        public List<AppointmentIndicatorSubCategoryListModel> GetAppointmentIndicatorSubCategoryList(UserInfo user,AppointmentIndicatorSubCategoryListModel filter, out int totalCount)
        {
            List<AppointmentIndicatorSubCategoryListModel> list_AppointmentIndicatorSubCategoryM = new List<AppointmentIndicatorSubCategoryListModel>();

            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_APPOINTMENT_INDICATOR_SUB_CATEGORIES");
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, filter.IsActive);
                db.AddInParameter(cmd, "SUB_CATEGORY_ID", DbType.Int32, MakeDbNull(filter.AppointmentIndicatorSubCategoryId));
                db.AddInParameter(cmd, "CATEGORY_ID", DbType.Int32, MakeDbNull(filter.AppointmentIndicatorCategoryId));
                db.AddInParameter(cmd, "MAIN_CATEGORY_ID", DbType.Int32, MakeDbNull(filter.AppointmentIndicatorMainCategoryId));
                db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, MakeDbNull(filter.AdminDesc));
                db.AddInParameter(cmd, "IS_AUTO_CREATE", DbType.Boolean, MakeDbNull(filter.IsAutoCreate));
                AddPagingParametersWithLanguage(user, cmd, filter);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        AppointmentIndicatorSubCategoryListModel model = new AppointmentIndicatorSubCategoryListModel
                        {
                            AppointmentIndicatorSubCategoryId = dr["APPOINTMENT_INDICATOR_SUB_CATEGORY_ID"].GetValue<int>(),
                            AdminDesc = dr["ADMIN_DESC"].GetValue<string>(),
                            AppointmentIndicatorSubCategoryName = dr["APPOINTMENT_INDICATOR_SUB_CATEGORY_NAME"].GetValue<string>(),
                            SubCode = dr["SUB_CODE"].GetValue<string>(),
                            IsActive = dr["IS_ACTIVE"].GetValue<int?>(),
                            IsActiveName = dr["IS_ACTIVE_NAME"].GetValue<string>(),
                            IsAutoCreate = dr["IS_AUTO_CREATE"].GetValue<bool?>(),
                            IsAutoCreateName = dr["IS_AUTO_CREATE_NAME"].GetValue<string>(),
                            AppointmentIndicatorCategoryId = dr["CATEGORY_ID"].GetValue<int>(),
                            AppointmentIndicatorCategoryName = dr["CATEGORY_NAME"].GetValue<string>(),
                            AppointmentIndicatorMainCategoryId = dr["MAIN_CATEGORY_ID"].GetValue<int>(),
                            AppointmentIndicatorMainCategoryName = dr["MAIN_CATEGORY_NAME"].GetValue<string>()
                        };

                        list_AppointmentIndicatorSubCategoryM.Add(model);
                    }
                    dr.Close();
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

            return list_AppointmentIndicatorSubCategoryM;
        }

        public void DMLAppointmentIndicatorSubCategory(UserInfo user, AppointmentIndicatorSubCategoryViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_APPOINTMENT_INDICATOR_SUB_CATEGORY_MAIN");

                db.AddParameter(cmd, "APPOINTMENT_INDICATOR_SUB_CATEGORY_ID", DbType.Int32, ParameterDirection.InputOutput, "APPOINTMENT_INDICATOR_SUB_CATEGORY_ID", DataRowVersion.Default, model.AppointmentIndicatorSubCategoryId);
                db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, MakeDbNull(model.AdminDesc));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "APPOINTMENT_INDICATOR_CATEGORY_ID", DbType.Int32, MakeDbNull(model.AppointmentIndicatorCategoryId));
                db.AddInParameter(cmd, "INDICATOR_TYPE_CODE", DbType.String, MakeDbNull(model.IndicatorTypeCode));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, model.IsActive);
                db.AddInParameter(cmd, "IS_AUTO_CREATE", DbType.Int32, model.IsAutoCreate);
                db.AddInParameter(cmd, "SUB_CODE", DbType.String, MakeDbNull(model.SubCode));
                db.AddInParameter(cmd, "ML_CONTENT", DbType.String, MakeDbNull(model.MultiLanguageContentAsText));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.AppointmentIndicatorSubCategoryId = db.GetParameterValue(cmd, "APPOINTMENT_INDICATOR_SUB_CATEGORY_ID").GetValue<int>();
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

        public void DMLAppointmentIndicatorSubCategory(UserInfo user, AppointmentIndicatorSubCategoryViewModel model, List<AppointmentIndicatorSubCategoryViewModel> filter)
        {
            if (!filter.Exists(q => q.ErrorNo == 1))
            {
                bool isSuccess = true;
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_APPOINTMENT_INDICATOR_SUB_CATEGORY_MAIN");
                CreateConnection(cmd);
                DbTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    foreach (var item in filter)
                    {
                        cmd.Parameters.Clear();
                        db.AddParameter(cmd, "APPOINTMENT_INDICATOR_SUB_CATEGORY_ID", DbType.Int32, ParameterDirection.InputOutput, "APPOINTMENT_INDICATOR_SUB_CATEGORY_ID", DataRowVersion.Default, item.AppointmentIndicatorSubCategoryId);
                        db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, MakeDbNull(item.AdminDesc));
                        db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, item.CommandType);
                        db.AddInParameter(cmd, "APPOINTMENT_INDICATOR_CATEGORY_ID", DbType.Int32, MakeDbNull(item.AppointmentIndicatorCategoryId));
                        db.AddInParameter(cmd, "INDICATOR_TYPE_CODE", DbType.String, MakeDbNull(item.IndicatorTypeCode));
                        db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, item.IsActive);
                        db.AddInParameter(cmd, "IS_AUTO_CREATE", DbType.Int32, item.IsAutoCreate);
                        db.AddInParameter(cmd, "SUB_CODE", DbType.String, MakeDbNull(item.SubCode));
                        db.AddInParameter(cmd, "ML_CONTENT", DbType.String, MakeDbNull(item.MultiLanguageContentAsText));
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

        public void GetAppointmentIndicatorSubCategory(UserInfo user, AppointmentIndicatorSubCategoryViewModel filter)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_APPOINTMENT_INDICATOR_SUB_CATEGORY");

                db.AddInParameter(cmd, "APPOINTMENT_INDICATOR_SUB_CATEGORY_ID", DbType.Int32, filter.AppointmentIndicatorSubCategoryId);
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
                        filter.AppointmentIndicatorSubCategoryId = dr["APPOINTMENT_INDICATOR_SUB_CATEGORY_ID"].GetValue<int>();
                        filter.SubCode = dr["SUB_CODE"].GetValue<string>();
                        filter.IsActiveName = dr["IS_ACTIVE_NAME"].GetValue<string>();
                        filter.IsAutoCreate = dr["IS_AUTO_CREATE"].GetValue<bool?>();
                        filter.IsAutoCreateName = dr["IS_AUTO_CREATE_NAME"].GetValue<string>();
                        filter.AppointmentIndicatorCategoryId = dr["CATEGORY_ID"].GetValue<int>();
                        filter.AppointmentIndicatorCategoryName = dr["CATEGORY_NAME"].GetValue<string>();
                        filter.AppointmentIndicatorMainCategoryId = dr["MAIN_CATEGORY_ID"].GetValue<int>();
                        filter.AppointmentIndicatorMainCategoryName = dr["MAIN_CATEGORY_NAME"].GetValue<string>();
                        filter.IndicatorTypeCode = dr["INDICATOR_TYPE_CODE"].GetValue<string>();
                    }
                    if (dr.NextResult())
                    {
                        var table = new DataTable();
                        table.Load(dr);
                        filter.AppointmentIndicatorSubCategoryName.MultiLanguageContentAsText = filter.MultiLanguageContentAsText
                                                                          = GetLanguageContentFromDataSet(table, "APPOINTMENT_INDICATOR_SUB_CATEGORY_NAME");
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


        public AppointmentIndicatorSubCategoryViewModel GetAppointmentIndicatorSubCategoryBySubCode(UserInfo user, string subCode)
        {
            AppointmentIndicatorSubCategoryViewModel appointmentIndicatorSubCategoryModel =
                new AppointmentIndicatorSubCategoryViewModel();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_APPOINTMENT_INDICATOR_SUB_CATEGORY_BY_SUB_CODE");

                db.AddInParameter(cmd, "SUB_CODE", DbType.String, subCode);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        appointmentIndicatorSubCategoryModel.AdminDesc = dr["ADMIN_DESC"].GetValue<string>();
                        appointmentIndicatorSubCategoryModel.IsActive = dr["IS_ACTIVE"].GetValue<bool>();
                        appointmentIndicatorSubCategoryModel.AppointmentIndicatorSubCategoryId = dr["APPOINTMENT_INDICATOR_SUB_CATEGORY_ID"].GetValue<int>();
                        appointmentIndicatorSubCategoryModel.SubCode = dr["SUB_CODE"].GetValue<string>();
                        appointmentIndicatorSubCategoryModel.IsActiveName = dr["IS_ACTIVE_NAME"].GetValue<string>();
                        appointmentIndicatorSubCategoryModel.IsAutoCreate = dr["IS_AUTO_CREATE"].GetValue<bool?>();
                        appointmentIndicatorSubCategoryModel.IsAutoCreateName = dr["IS_AUTO_CREATE_NAME"].GetValue<string>();
                        appointmentIndicatorSubCategoryModel.AppointmentIndicatorCategoryId = dr["CATEGORY_ID"].GetValue<int>();
                        appointmentIndicatorSubCategoryModel.AppointmentIndicatorCategoryName = dr["CATEGORY_NAME"].GetValue<string>();
                        appointmentIndicatorSubCategoryModel.AppointmentIndicatorMainCategoryId = dr["MAIN_CATEGORY_ID"].GetValue<int>();
                        appointmentIndicatorSubCategoryModel.AppointmentIndicatorMainCategoryName = dr["MAIN_CATEGORY_NAME"].GetValue<string>();
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

            return appointmentIndicatorSubCategoryModel;
        }

        public IEnumerable<string> ListOfIndicatorTypeCode(UserInfo user)
        {
            var indicatorTypeCodeList = new List<string>();

            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_INDICATOR_TYPE_CODE");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        indicatorTypeCodeList.Add(dr["INDICATOR_TYPE_CODE"].GetValue<string>());
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

            return indicatorTypeCodeList;
        }
        public List<SelectListItem> ListOfIndicatorTypeCodeAsSelectListItem(UserInfo user)
        {
            var indicatorTypeCodeList = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_INDICATOR_TYPE_CODE");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        indicatorTypeCodeList.Add(new SelectListItem()
                        {
                            Text = dr["INDICATOR_TYPE_CODE"].GetValue<string>() + " / " + dr["INDICATOR_TYPE_NAME"].GetValue<string>(),
                            Value = dr["INDICATOR_TYPE_CODE"].GetValue<string>()
                        });
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

            return indicatorTypeCodeList;
        }

    }
}

using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;
using ODMSCommon;
using ODMSModel.AppointmentIndicatorMainCategory;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class AppointmentIndicatorMainCategoryData : DataAccessBase
    {
        public List<SelectListItem> ListAppointmentIndicatorMainCategories(UserInfo user, string indicatorTypeCode)
        {
            var retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_APPOINTMENT_INDICATOR_MAIN_CATEGORY_BY_INDIC_TYPE_COMBO");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "INDICATOR_TYPE_CODE", DbType.String, MakeDbNull(indicatorTypeCode));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new SelectListItem
                        {
                            Value = reader["APPOINTMENT_INDICATOR_MAIN_CATEGORY_ID"].GetValue<string>(),
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

        public List<SelectListItem> ListAppointmentIndicatorMainCategories(UserInfo user, bool? isActive)
        {
            var retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_APPOINTMENT_INDICATOR_MAIN_CATEGORY_COMBO");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, MakeDbNull(isActive));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new SelectListItem
                        {
                            Value = reader["APPOINTMENT_INDICATOR_MAIN_CATEGORY_ID"].GetValue<string>(),
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

        public Dictionary<string, int> DictioanryListAppointmentIndicatorMainCategories(UserInfo user, bool? isActive)
        {
            var resultList = new Dictionary<string, int>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_APPOINTMENT_INDICATOR_MAIN_CATEGORY_COMBO");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, MakeDbNull(isActive));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        resultList.Add(reader["MAIN_CODE"].GetValue<string>(), reader["APPOINTMENT_INDICATOR_MAIN_CATEGORY_ID"].GetValue<int>());
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

        public List<SelectListItem> ListAppointmentIndicatorMainCategoryCodes(UserInfo user, bool? isActive)
        {
            var retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_APPOINTMENT_INDICATOR_MAIN_CATEGORY_COMBO");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, MakeDbNull(isActive));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new SelectListItem
                        {
                            Value = reader["APPOINTMENT_INDICATOR_MAIN_CATEGORY_ID"].GetValue<string>(),
                            Text = reader["MAIN_CODE"].GetValue<string>()
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
        public List<AppointmentIndicatorMainCategoryListModel> GetAppointmentIndicatorMainCategoryList(UserInfo user,AppointmentIndicatorMainCategoryListModel filter, out int totalCount)
        {
            List<AppointmentIndicatorMainCategoryListModel> list_AppointmentIndicatorMainCategoryM = new List<AppointmentIndicatorMainCategoryListModel>();

            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_APPOINTMENT_INDICATOR_MAIN_CATEGORIES");
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, filter.IsActive);
                db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, MakeDbNull(filter.AdminDesc));
                AddPagingParametersWithLanguage(user, cmd, filter);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        AppointmentIndicatorMainCategoryListModel model = new AppointmentIndicatorMainCategoryListModel
                        {
                            AppointmentIndicatorMainCategoryId =
                                    dr["APPOINTMENT_INDICATOR_MAIN_CATEGORY_ID"].GetValue<int>(),
                            AdminDesc = dr["ADMIN_DESC"].GetValue<string>(),
                            AppointmentIndicatorMainCategoryName =
                                    dr["APPOINTMENT_INDICATOR_MAIN_CATEGORY_NAME"].GetValue<string>(),
                            MainCode = dr["MAIN_CODE"].GetValue<string>(),
                            IsActive = dr["IS_ACTIVE"].GetValue<int?>(),
                            IsActiveName = dr["IS_ACTIVE_NAME"].GetValue<string>()
                        };

                        list_AppointmentIndicatorMainCategoryM.Add(model);
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

            return list_AppointmentIndicatorMainCategoryM;
        }

        public void DMLAppointmentIndicatorMainCategory(UserInfo user, AppointmentIndicatorMainCategoryViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_APPOINTMENT_INDICATOR_MAIN_CATEGORY_MAIN");

                db.AddParameter(cmd, "APPOINTMENT_INDICATOR_MAIN_CATEGORY_ID", DbType.Int32, ParameterDirection.InputOutput, "APPOINTMENT_INDICATOR_MAIN_CATEGORY_ID", DataRowVersion.Default, model.AppointmentIndicatorMainCategoryId);
                db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, MakeDbNull(model.AdminDesc));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, MakeDbNull(model.CommandType));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, model.IsActive);
                db.AddInParameter(cmd, "CAN_BE_USED_IN_APPOINTMENT", DbType.Boolean, model.CanBeUsedInAppointment);
                db.AddInParameter(cmd, "MAIN_CODE", DbType.String, MakeDbNull(model.MainCode));
                db.AddInParameter(cmd, "ML_CONTENT", DbType.String, model.MultiLanguageContentAsText);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.AppointmentIndicatorMainCategoryId = db.GetParameterValue(cmd, "APPOINTMENT_INDICATOR_MAIN_CATEGORY_ID").GetValue<int>();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
                {
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                model.ErrorNo = 1;
                model.ErrorMessage = ex.Message;
            }
            finally
            {
                CloseConnection();
            }
        }

        public void GetAppointmentIndicatorMainCategory(UserInfo user, AppointmentIndicatorMainCategoryViewModel filter)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_APPOINTMENT_INDICATOR_MAIN_CATEGORY");

                db.AddInParameter(cmd, "APPOINTMENT_INDICATOR_MAIN_CATEGORY_ID", DbType.Int32, filter.AppointmentIndicatorMainCategoryId);
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
                        filter.CanBeUsedInAppointment = dr["CAN_BE_USED_IN_APPOINTMENT"].GetValue<bool>();
                        filter.CanBeUsedInAppointmentName = dr["CAN_BE_USED_IN_APPOINTMENT_NAME"].GetValue<string>();
                        filter.AppointmentIndicatorMainCategoryId = dr["APPOINTMENT_INDICATOR_MAIN_CATEGORY_ID"].GetValue<int>();
                        filter.MainCode = dr["MAIN_CODE"].GetValue<string>();
                        filter.IsActiveName = dr["IS_ACTIVE_NAME"].GetValue<string>();
                    }
                    if (dr.NextResult())
                    {
                        var table = new DataTable();
                        table.Load(dr);
                        filter.AppointmentIndicatorMainCategoryName.MultiLanguageContentAsText = filter.MultiLanguageContentAsText
                                                                          = GetLanguageContentFromDataSet(table, "APPOINTMENT_INDICATOR_MAIN_CATEGORY_NAME");
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
        public AppointmentIndicatorMainCategoryViewModel GetAppointmentIndicatorMainCategoryByMainCode(UserInfo user, string mainCode)
        {
            AppointmentIndicatorMainCategoryViewModel appointmentIndicatorMainCategoryModel =
                new AppointmentIndicatorMainCategoryViewModel();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_APPOINTMENT_INDICATOR_MAIN_CATEGORY_BY_MAIN_CODE");

                db.AddInParameter(cmd, "MAIN_CODE", DbType.String, mainCode);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        appointmentIndicatorMainCategoryModel.AdminDesc = dr["ADMIN_DESC"].GetValue<string>();
                        appointmentIndicatorMainCategoryModel.IsActive = dr["IS_ACTIVE"].GetValue<bool>();
                        appointmentIndicatorMainCategoryModel.AppointmentIndicatorMainCategoryId = dr["APPOINTMENT_INDICATOR_MAIN_CATEGORY_ID"].GetValue<int>();
                        appointmentIndicatorMainCategoryModel.MainCode = dr["MAIN_CODE"].GetValue<string>();
                        appointmentIndicatorMainCategoryModel.IsActiveName = dr["IS_ACTIVE_NAME"].GetValue<string>();
                    }
                    dr.Close();
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

            return appointmentIndicatorMainCategoryModel;
        }


    }
}

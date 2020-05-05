using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.ListModel;
using ODMSModel.User;

namespace ODMSData
{
    public class UserData : DataAccessBase
    {
        public List<UserListModel> ListUsers(UserInfo user, UserListModel filter, out int totalCount)
        {
            var retVal = new List<UserListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_USERS");
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(filter.DealerId));
                db.AddInParameter(cmd, "DMS_USER_CODE", DbType.String, MakeDbNull(filter.UserCode));
                db.AddInParameter(cmd, "ROLE_TYPE_ID", DbType.Int32, MakeDbNull(filter.RoleTypeId));
                db.AddInParameter(cmd, "SEX_ID", DbType.Int32, MakeDbNull(filter.SexId));
                db.AddInParameter(cmd, "MARITAL_STATUS_ID", DbType.Int32, MakeDbNull(filter.MaritalStatusId));
                db.AddInParameter(cmd, "USER_FIRST_NAME", DbType.String, MakeDbNull(filter.UserFirstName));
                db.AddInParameter(cmd, "USER_LAST_NAME", DbType.String, MakeDbNull(filter.UserLastName));
                db.AddInParameter(cmd, "IDENTITY_NO", DbType.String, MakeDbNull(filter.IdentityNo));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, MakeDbNull(filter.SearchIsActive));
                db.AddInParameter(cmd, "IS_TECHNICIAN", DbType.Boolean, filter.IsTechnician);
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
                        var userListModel = new UserListModel
                        {
                            UserId = reader["USER_ID"].GetValue<int>(),
                            UserFirstName = reader["USER_FIRST_NAME"].GetValue<string>(),
                            UserLastName = reader["USER_LAST_NAME"].GetValue<string>(),
                            UserCode = reader["USER_CODE"].GetValue<string>(),
                            TCIdentityNo = reader["TC_IDENTITY_NO"].GetValue<string>(),
                            IdentityNo = reader["IDENTITY_NO"].GetValue<string>(),
                            IsActive = reader["IS_ACTIVE"].GetValue<int?>(),
                            IsActiveName = reader["IS_ACTIVE_NAME"].GetValue<string>(),
                            EMail = reader["EMAIL"].GetValue<string>(),
                            DealerName = reader["DEALER_NAME"].GetValue<string>()
                        };
                        retVal.Add(userListModel);
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

        public List<UserListModel> ListDealerUsers(UserInfo user, UserListModel filter, out int totalCount)
        {
            var retVal = new List<UserListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEALER_USERS");
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(filter.DealerId));
                db.AddInParameter(cmd, "DMS_USER_CODE", DbType.String, MakeDbNull(filter.UserCode));
                db.AddInParameter(cmd, "USER_FIRST_NAME", DbType.String, MakeDbNull(filter.UserFirstName));
                db.AddInParameter(cmd, "USER_LAST_NAME", DbType.String, MakeDbNull(filter.UserLastName));
                db.AddInParameter(cmd, "IDENTITY_NO", DbType.String, MakeDbNull(filter.IdentityNo));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, MakeDbNull(filter.SearchIsActive));
                db.AddInParameter(cmd, "IS_TECHNICIAN", DbType.Boolean, MakeDbNull(filter.IsTechnician));
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
                        var userListModel = new UserListModel
                        {
                            UserId = reader["USER_ID"].GetValue<int>(),
                            UserFirstName = reader["USER_FIRST_NAME"].GetValue<string>(),
                            UserLastName = reader["USER_LAST_NAME"].GetValue<string>(),
                            UserCode = reader["USER_CODE"].GetValue<string>(),
                            IdentityNo = reader["IDENTITY_NO"].GetValue<string>(),
                            IsActive = reader["IS_ACTIVE"].GetValue<int?>(),
                            IsActiveName = reader["IS_ACTIVE_NAME"].GetValue<string>(),
                            EMail = reader["EMAIL"].GetValue<string>(),
                            DealerName = reader["DEALER_NAME"].GetValue<string>(),
                            RoleTypeName = reader["ROLE_TYPE_NAME"].GetValue<string>()
                        };
                        retVal.Add(userListModel);
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

        public void DMLUserPassword(UserInfo user, UserIndexViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_USER_PASSWORD");
                db.AddParameter(cmd, "USER_ID", DbType.Int32, ParameterDirection.InputOutput, "USER_ID", DataRowVersion.Default, model.UserId);
                db.AddInParameter(cmd, "PASSWORD", DbType.String, MakeDbNull(model.Password));
                db.AddInParameter(cmd, "IS_AUTO_PWD", DbType.Boolean, MakeDbNull(model.IsAutoPassword));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.UserId = db.GetParameterValue(cmd, "USER_ID").GetValue<int>();
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

        public void DMLUser(UserInfo user, UserIndexViewModel model)
        {
            try
            {
                CreateDatabase();

                string newPassword = string.Empty;
                if (model.CommandType == "I")
                {
                    newPassword = new PasswordSecurityProvider().GenerateHashedPassword(CommonUtility.GeneratePassword());
                }

                var cmd = db.GetStoredProcCommand("P_DML_USER_MAIN");
                db.AddParameter(cmd, "USER_ID", DbType.Int32, ParameterDirection.InputOutput, "USER_ID", DataRowVersion.Default, model.UserId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "USER_CODE", DbType.String, MakeDbNull(model.UserCode));
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(model.DealerId));
                db.AddInParameter(cmd, "USER_FIRST_NAME", DbType.String, MakeDbNull(model.UserFirstName));
                db.AddInParameter(cmd, "USER_MID_NAME", DbType.String, MakeDbNull(model.UserMidName));
                db.AddInParameter(cmd, "USER_LAST_NAME", DbType.String, MakeDbNull(model.UserLastName));
                db.AddInParameter(cmd, "TC_IDENTITY_NO", DbType.String, MakeDbNull(model.TCIdentityNo));
                db.AddInParameter(cmd, "PASSPORT_NO", DbType.String, MakeDbNull(model.PassportNo));
                db.AddInParameter(cmd, "BIRTH_DATE", DbType.DateTime, MakeDbNull(model.BirthDate));
                db.AddInParameter(cmd, "SEX", DbType.String, MakeDbNull(model.SexId));
                db.AddInParameter(cmd, "MARITAL_STATUS", DbType.String, MakeDbNull(model.MaritalStatusId));
                db.AddInParameter(cmd, "PHOTO_DOC_ID", DbType.Int32, MakeDbNull(model.PhotoDocId));
                db.AddInParameter(cmd, "PHONE", DbType.String, MakeDbNull(model.Phone));
                db.AddInParameter(cmd, "MOBILE", DbType.String, MakeDbNull(model.Mobile));
                db.AddInParameter(cmd, "EXTENSION", DbType.String, MakeDbNull(model.Extension));
                db.AddInParameter(cmd, "EMAIL", DbType.String, MakeDbNull(model.EMail));
                db.AddInParameter(cmd, "ADDRESS", DbType.String, MakeDbNull(model.Address));
                db.AddInParameter(cmd, "EMPLOYMENT_DATE", DbType.DateTime, MakeDbNull(model.EmploymentDate));
                db.AddInParameter(cmd, "UNEMPLOYMENT_DATE", DbType.DateTime, MakeDbNull(model.UnemploymentDate));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, model.IsActive);
                db.AddInParameter(cmd, "IS_TECHNICIAN", DbType.Boolean, model.IsTechnician);
                db.AddInParameter(cmd, "PASSWORD", DbType.String, MakeDbNull(model.Password));
                db.AddInParameter(cmd, "USER_LANGUAGE_CODE", DbType.String, MakeDbNull(model.LanguageCode));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.UserId = db.GetParameterValue(cmd, "USER_ID").GetValue<int>();
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

        public void ConvertUser(int id)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_CONVERT_USER");
                db.AddInParameter(cmd, "USER_ID", DbType.Int32, MakeDbNull(id));
                CreateConnection(cmd);
                cmd.ExecuteReader();
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
        public UserIndexViewModel GetUser(UserInfo user, UserIndexViewModel filter)
        {
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_USER");
                db.AddInParameter(cmd, "USER_ID", DbType.Int32, MakeDbNull(filter.UserId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.Address = dReader["ADDRESS"].GetValue<string>();
                    filter.BirthDate = dReader["BIRTH_DATE"].GetValue<DateTime>();
                    filter.DealerId = dReader["DEALER_ID"].GetValue<string>();
                    filter.DealerName = dReader["DEALER_NAME"].GetValue<string>();
                    filter.EMail = dReader["EMAIL"].GetValue<string>();
                    filter.EmploymentDate = dReader["EMPLOYMENT_DATE"].GetValue<DateTime?>();
                    filter.UnemploymentDate = dReader["UNEMPLOYMENT_DATE"].GetValue<DateTime?>();
                    filter.UserCode = dReader["USER_CODE"].GetValue<string>();
                    filter.UserFirstName = dReader["USER_FIRST_NAME"].GetValue<string>();
                    filter.UserLastName = dReader["USER_LAST_NAME"].GetValue<string>();
                    filter.UserMidName = dReader["USER_MID_NAME"].GetValue<string>();
                    filter.UserId = dReader["USER_ID"].GetValue<int>();
                    filter.TCIdentityNo = dReader["TC_IDENTITY_NO"].GetValue<long>();
                    filter.PassportNo = dReader["PASSPORT_NO"].GetValue<string>();
                    filter.Phone = dReader["PHONE"].GetValue<string>();
                    filter.Extension = dReader["EXTENSION"].GetValue<string>();
                    filter.Mobile = dReader["MOBILE"].GetValue<string>();
                    filter.SexId = dReader["SEX_ID"].GetValue<int>();
                    filter.Sex = dReader["SEX"].GetValue<string>();
                    filter.MaritalStatusId = dReader["MARITAL_STATUS_ID"].GetValue<int>();
                    filter.MaritalStatus = dReader["MARITAL_STATUS"].GetValue<string>();
                    filter.IsPasswordSet = dReader["IS_PASSWORD_SET"].GetValue<bool>();
                    filter.IsActive = dReader["IS_ACTIVE"].GetValue<bool>();
                    filter.IsActiveName = dReader["IS_ACTIVE_NAME"].GetValue<string>();
                    filter.IsTechnician = dReader["IS_TECHNICIAN"].GetValue<bool>();
                    filter.PhotoDocId = dReader["PHOTO_DOC_ID"].GetValue<int>();
                    filter.LanguageCode = dReader["LANGUAGE_CODE"].GetValue<string>();
                    filter.RoleTypeId = dReader["ROLE_TYPE_ID"].GetValue<int?>();
                    filter.RoleTypeName = dReader["ROLE_TYPE_NAME"].GetValue<string>();
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

        public UserIndexViewModel GetUserView(UserInfo user, UserIndexViewModel model)
        {
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_USER_VIEW");
                db.AddInParameter(cmd, "USER_ID", DbType.Int32, MakeDbNull(model.UserId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    model.Address = dReader["ADDRESS"].GetValue<string>();
                    model.BirthDate = dReader["BIRTH_DATE"].GetValue<DateTime>();
                    model.DealerId = dReader["DEALER_ID"].GetValue<string>();
                    model.DealerName = dReader["DEALER_NAME"].GetValue<string>();
                    model.EMail = dReader["EMAIL"].GetValue<string>();
                    model.EmploymentDate = dReader["EMPLOYMENT_DATE"].GetValue<DateTime?>();
                    model.UnemploymentDate = dReader["UNEMPLOYMENT_DATE"].GetValue<DateTime?>();
                    model.UserCode = dReader["USER_CODE"].GetValue<string>();
                    model.UserFirstName = dReader["USER_FIRST_NAME"].GetValue<string>();
                    model.UserLastName = dReader["USER_LAST_NAME"].GetValue<string>();
                    model.UserMidName = dReader["USER_MID_NAME"].GetValue<string>();
                    model.UserId = dReader["USER_ID"].GetValue<int>();
                    model.TCIdentityNo = dReader["TC_IDENTITY_NO"].GetValue<long>();
                    model.PassportNo = dReader["PASSPORT_NO"].GetValue<string>();
                    model.Phone = dReader["PHONE"].GetValue<string>();
                    model.Extension = dReader["EXTENSION"].GetValue<string>();
                    model.Mobile = dReader["MOBILE"].GetValue<string>();
                    model.SexId = dReader["SEX_ID"].GetValue<int>();
                    model.Sex = dReader["SEX"].GetValue<string>();
                    model.MaritalStatusId = dReader["MARITAL_STATUS_ID"].GetValue<int>();
                    model.MaritalStatus = dReader["MARITAL_STATUS"].GetValue<string>();
                    model.IsPasswordSet = dReader["IS_PASSWORD_SET"].GetValue<bool>();
                    model.IsActive = dReader["IS_ACTIVE"].GetValue<bool>();
                    model.IsActiveName = dReader["IS_ACTIVE_NAME"].GetValue<string>();
                    model.IsTechnician = dReader["IS_TECHNICIAN"].GetValue<bool>();
                    model.PhotoDocId = dReader["PHOTO_DOC_ID"].GetValue<int>();
                    model.LanguageCode = dReader["LANGUAGE_CODE"].GetValue<string>();
                    model.RoleTypeId = dReader["ROLE_TYPE_ID"].GetValue<int?>();
                    model.RoleTypeName = dReader["ROLE_TYPE_NAME"].GetValue<string>();
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
            return model;
        }

        public UserIndexViewModel GetUserByTCIdentityNo(UserInfo user, string tcIdentityNo)
        {
            UserIndexViewModel userModel = new UserIndexViewModel();
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_USER_BY_TCIDENTITYNO");
                db.AddInParameter(cmd, "TC_IDENTITY_NO", DbType.String, MakeDbNull(tcIdentityNo));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    userModel.Address = dReader["ADDRESS"].GetValue<string>();
                    userModel.BirthDate = dReader["BIRTH_DATE"].GetValue<DateTime>();
                    userModel.DealerId = dReader["DEALER_ID"].GetValue<string>();
                    userModel.DealerName = dReader["DEALER_NAME"].GetValue<string>();
                    userModel.EMail = dReader["EMAIL"].GetValue<string>();
                    userModel.EmploymentDate = dReader["EMPLOYMENT_DATE"].GetValue<DateTime>();
                    userModel.UnemploymentDate = dReader["UNEMPLOYMENT_DATE"].GetValue<DateTime>();
                    userModel.UserCode = dReader["USER_CODE"].GetValue<string>();
                    userModel.UserFirstName = dReader["USER_FIRST_NAME"].GetValue<string>();
                    userModel.UserLastName = dReader["USER_LAST_NAME"].GetValue<string>();
                    userModel.UserMidName = dReader["USER_MID_NAME"].GetValue<string>();
                    userModel.UserId = dReader["USER_ID"].GetValue<int>();
                    userModel.TCIdentityNo = dReader["TC_IDENTITY_NO"].GetValue<long>();
                    userModel.PassportNo = dReader["PASSPORT_NO"].GetValue<string>();
                    userModel.Phone = dReader["PHONE"].GetValue<string>();
                    userModel.Extension = dReader["EXTENSION"].GetValue<string>();
                    userModel.Mobile = dReader["MOBILE"].GetValue<string>();
                    userModel.SexId = dReader["SEX_ID"].GetValue<int>();
                    userModel.Sex = dReader["SEX"].GetValue<string>();
                    userModel.MaritalStatusId = dReader["MARITAL_STATUS_ID"].GetValue<int>();
                    userModel.MaritalStatus = dReader["MARITAL_STATUS"].GetValue<string>();
                    userModel.IsActive = dReader["IS_ACTIVE"].GetValue<bool>();
                    userModel.IsActiveName = dReader["IS_ACTIVE_NAME"].GetValue<string>();
                    userModel.PhotoDocId = dReader["PHOTO_DOC_ID"].GetValue<int>();
                    userModel.LanguageCode = dReader["LANGUAGE_CODE"].GetValue<string>();
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
            return userModel;
        }

        public bool GetAnyUserByTCIdentityNo(int userId, string tcIdentityNo)
        {
            int? count = 0;
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_ANY_USER_BY_IDENTITY_NO");
                db.AddInParameter(cmd, "TC_IDENTITY_NO", DbType.String, MakeDbNull(tcIdentityNo));
                db.AddInParameter(cmd, "USER_ID", DbType.Int32, MakeDbNull(userId));
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();


                while (dReader.Read())
                {
                    count = dReader["Count"].GetValue<int?>();
                }

                return (count.HasValue && count.Value > 0);
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

            return false;
        }


        public UserIndexViewModel GetUserByPassportNo(UserInfo user, string passportNo)
        {
            UserIndexViewModel userModel = new UserIndexViewModel();
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_USER_BY_PASSPORTNO");
                db.AddInParameter(cmd, "PASSPORT_NO", DbType.String, MakeDbNull(passportNo));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    userModel.Address = dReader["ADDRESS"].GetValue<string>();
                    userModel.BirthDate = dReader["BIRTH_DATE"].GetValue<DateTime>();
                    userModel.DealerId = dReader["DEALER_ID"].GetValue<string>();
                    userModel.DealerName = dReader["DEALER_NAME"].GetValue<string>();
                    userModel.EMail = dReader["EMAIL"].GetValue<string>();
                    userModel.EmploymentDate = dReader["EMPLOYMENT_DATE"].GetValue<DateTime>();
                    userModel.UnemploymentDate = dReader["UNEMPLOYMENT_DATE"].GetValue<DateTime>();
                    userModel.UserCode = dReader["USER_CODE"].GetValue<string>();
                    userModel.UserFirstName = dReader["USER_FIRST_NAME"].GetValue<string>();
                    userModel.UserLastName = dReader["USER_LAST_NAME"].GetValue<string>();
                    userModel.UserMidName = dReader["USER_MID_NAME"].GetValue<string>();
                    userModel.UserId = dReader["USER_ID"].GetValue<int>();
                    userModel.TCIdentityNo = dReader["TC_IDENTITY_NO"].GetValue<long>();
                    userModel.PassportNo = dReader["PASSPORT_NO"].GetValue<string>();
                    userModel.Phone = dReader["PHONE"].GetValue<string>();
                    userModel.Extension = dReader["EXTENSION"].GetValue<string>();
                    userModel.Mobile = dReader["MOBILE"].GetValue<string>();
                    userModel.SexId = dReader["SEX_ID"].GetValue<int>();
                    userModel.Sex = dReader["SEX"].GetValue<string>();
                    userModel.MaritalStatusId = dReader["MARITAL_STATUS_ID"].GetValue<int>();
                    userModel.MaritalStatus = dReader["MARITAL_STATUS"].GetValue<string>();
                    userModel.IsActive = dReader["IS_ACTIVE"].GetValue<bool>();
                    userModel.IsActiveName = dReader["IS_ACTIVE_NAME"].GetValue<string>();
                    userModel.PhotoDocId = dReader["PHOTO_DOC_ID"].GetValue<int>();
                    userModel.LanguageCode = dReader["LANGUAGE_CODE"].GetValue<string>();
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
            return userModel;
        }

        public bool GetAnyUserByPassportNo(int userId, string passportNo)
        {
            int? count = 0; 
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_ANY_USER_BY_PASSPORT_NO");
                db.AddInParameter(cmd, "PASSPORT_NO", DbType.String, MakeDbNull(passportNo));
                db.AddInParameter(cmd, "USER_ID", DbType.Int32, MakeDbNull(userId));
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    count = dReader["Count"].GetValue<int?>();
                }

                return (count.HasValue && count.Value > 0);
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
            return false;
        }

        public void UpdateUserLanguage(int userId, string languageCode)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_DML_USER_LANG");
                db.AddInParameter(cmd, "USER_ID", DbType.Int32, userId);
                db.AddInParameter(cmd, "USER_LANGUAGE_CODE", DbType.String, languageCode);
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

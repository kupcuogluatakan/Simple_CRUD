using System;
using System.Data;
using System.Data.Common;
using ODMSCommon.Security;
using ODMSModel.SystemAdministration;
using ODMSCommon;
using ODMSData.Utility;

namespace ODMSData
{
    public class SystemAdministrationData : DataAccessBase
    {
        public UserInfo Login(SystemAdministrationLoginModel loginModel)
        {
            DbDataReader dReader = null;
            UserInfo user = new UserInfo { IsLoggedIn = false };
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LOGIN");
                db.AddInParameter(cmd, "USER_CODE", DbType.String, MakeDbNull(loginModel.UserName));
                db.AddInParameter(cmd, "PASSWORD", DbType.String, MakeDbNull(loginModel.Password));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                dReader = cmd.ExecuteReader();
                while (dReader.Read())
                {
                    user.UserId = dReader["ID_DMS_USER"].GetValue<int>();
                    user.UserName = dReader["DMS_USER_CODE"].GetValue<string>();
                    user.DealerID = dReader["ID_DEALER"].GetValue<int>();
                    user.IsDealer = user.DealerID != 0;
                    user.LanguageCode = dReader["LANGUAGE_CODE"].GetValue<string>();
                    user.FirstName = dReader["CUSTOMER_NAME"].GetValue<string>();
                    user.MiddleName = dReader["USER_MID_NAME"].GetValue<string>();
                    user.LastName = dReader["USER_LAST_NAME"].GetValue<string>();
                    user.Email = dReader["EMAIL"].GetValue<string>();
                    user.Password = dReader["PASSWORD"].GetValue<string>();
                    user.IsPasswordSet = dReader["IS_PASSWORD_SET"].GetValue<bool>();
                    user.IsTechnician = dReader["IS_TECHNICIAN"].GetValue<bool>();
                    user.HasSystemRole = dReader["HAS_SYSTEM_ROLES"].GetValue<bool>();
                    user.SessionId = dReader["SESSION_GUID"].ToString();
                    user.SessionExpireDate = dReader["SESSION_EXP_DATE"].GetValue<DateTime?>();
                    user.IsLoggedIn = dReader["IS_SUCCESS"].GetValue<bool>();
                    user.IsBlocked = dReader["IS_BLOCKED"].GetValue<bool>();
                    user.PasswordChangeSequence = dReader["PWD_CHNG_SEQ"].GetValue<int>();
                }
                if (dReader.NextResult())
                {
                    while (dReader.Read())
                    {
                        user.Roles.Add(dReader["ROLE_TYPE_ID"].GetValue<int>());
                    }
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
            return user;
        }

        public bool IsUserActive(string userCode)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetSqlStringCommand(@"
                    SELECT COUNT(1) FROM DMS_USER WHERE DMS_USER_CODE = @DMS_USER_CODE AND IS_ACTIVE = 1
                ");
                db.AddInParameter(cmd, "DMS_USER_CODE", DbType.String, userCode);
                CreateConnection(cmd);
                var result = cmd.ExecuteScalar();
                if (result == null)
                    return false;

                return Convert.ToInt32(result) > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return false;
        }

        public void UpdateSessionData(UserInfo userBo)
        {
            new DbHelper().ExecuteNonQuery("P_UPDATE_DMS_USER_SESSION_VALUES", userBo.UserId, userBo.SessionId,
                userBo.SessionExpireDate);
        }
        public DateTime? AccountRecoveryTry(string ip)
        {
            DbDataReader dReader = null;
            DateTime? unblock_date = null;
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_ACCOUNT_RECOVERY_TRY");
                db.AddInParameter(cmd, "IP", DbType.String, ip);

                CreateConnection(cmd);
                dReader = cmd.ExecuteReader();
                while (dReader.Read())
                {
                    unblock_date = dReader["UNBLOCK_DATE"].GetValue<DateTime?>();
                }
                return unblock_date;

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
        public void SetAccountRecovery(AccountRecoveryViewModel model)
        {
            UserInfo user = new UserInfo { IsLoggedIn = false };
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_ACCOUNT_RECOVERY");
                db.AddInParameter(cmd, "USER_NAME", DbType.String, model.UserName);
                db.AddInParameter(cmd, "EMAIL", DbType.String, model.Email);
                db.AddInParameter(cmd, "IDENTITY_NO", DbType.String, model.IdentityNo);
                db.AddInParameter(cmd, "PASSWORD", DbType.String, model.Password);
                db.AddInParameter(cmd, "HASHED_PASSWORD", DbType.String, new PasswordSecurityProvider().GenerateHashedPassword(model.Password));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(db.GetParameterValue(cmd, "ERROR_DESC").GetValue<string>());
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

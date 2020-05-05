using ODMSCommon;
using ODMSData.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSData
{
    public class PasswordData : DataAccessBase
    {

        public int WrongPasswordInputCountForUser(int userId)
        {
            int result;

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_WRONG_PASSWORD_INPUT_COUNT");
                db.AddInParameter(cmd, "ID_DMS_USER", DbType.Int32, userId);
                CreateConnection(cmd);
                result = cmd.ExecuteScalar().GetValue<int>();
            }
            finally
            {
                CloseConnection();
            }

            return result;
        }

        public string GetPasswordForUser(int userId)
        {
            string result;

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_LATEST_USER_PASSWORD");
                db.AddInParameter(cmd, "ID_DMS_USER", DbType.Int32, userId);
                CreateConnection(cmd);
                result = cmd.ExecuteScalar().GetValue<string>();
            }
            finally
            {
                CloseConnection();
            }

            return result;
        }

        public DateTime? LastLoginFailDateForUser(int userId)
        {
            DateTime? loginFailDateForUser;

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_LATEST_USER_PASSWORD");
                db.AddInParameter(cmd, "ID_DMS_USER", DbType.Int32, userId);
                CreateConnection(cmd);
                loginFailDateForUser = cmd.ExecuteScalar().GetValue<DateTime?>();
            }
            finally
            {
                CloseConnection();
            }

            return loginFailDateForUser;
        }

        public DateTime? GetLastPasswordChangeDateForUser(int userId)
        {
            DateTime? passwordChangeDateForUser;

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_LAST_PASSWORD_CHANGE_DATE");
                db.AddInParameter(cmd, "ID_DMS_USER", DbType.Int32, userId);
                CreateConnection(cmd);
                passwordChangeDateForUser = cmd.ExecuteScalar().GetValue<DateTime?>();
            }
            finally
            {
                CloseConnection();
            }

            return passwordChangeDateForUser;

        }

        public bool PasswordExistsInLastNPasswords(int n, string password, int userId)
        {
            bool result;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_EXISTS_PASSWORD_TOPN_IN_PASSWORD_HISTORY", userId, password, n);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                result = cmd.ExecuteScalar().GetValue<bool>();
            }
            finally
            {
                CloseConnection();
            }

            return result;
        }

        public void UnblockUser(int userId)
        {
            new DbHelper().ExecuteNonQuery("P_UNBLOCK_ACCOUNT", userId);
        }

    }
}

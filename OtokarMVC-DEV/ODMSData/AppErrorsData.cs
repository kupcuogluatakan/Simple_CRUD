using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.Announcement;
using ODMSModel.ListModel;

namespace ODMSData
{
    public class AppErrorsData : DataAccessBase
    {
       
        public void Add(AppErrorViewModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_DML_APP_ERRORS");
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "SOURCE", DbType.String, MakeDbNull(model.Source));
                db.AddInParameter(cmd, "BUSINESS_NAME", DbType.String, MakeDbNull(model.BusinessName));
                db.AddInParameter(cmd, "METHOD_NAME", DbType.String, MakeDbNull(model.MethodName));
                db.AddInParameter(cmd, "USER_CODE", DbType.String, MakeDbNull(model.UserCode));
                db.AddInParameter(cmd, "DEBUG_PARAMETERS", DbType.String, MakeDbNull(model.DebugParameters));
                db.AddInParameter(cmd, "ERROR_TIME", DbType.DateTime, MakeDbNull(model.ErrorTime));
                db.AddInParameter(cmd, "ERROR_DESC", DbType.String, MakeDbNull(model.ErrorDesc));
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

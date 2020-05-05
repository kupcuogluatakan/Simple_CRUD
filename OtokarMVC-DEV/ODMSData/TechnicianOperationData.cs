using System.Data;
using System.Data.Common;
using ODMSCommon;
using System;

namespace ODMSData
{
    public class TechnicianOperationData : DataAccessBase
    {

        public void CheckTechnicianLogin(ODMSModel.TechnicianOperation.TechnicianOperationViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_CHECK_TECHNICIAN_LOGIN");

                db.AddInParameter(cmd,"DMS_ID",DbType.Int32,model.DmsUserId);
                db.AddInParameter(cmd,"PASS",DbType.String,model.Password);
                db.AddOutParameter(cmd,"IS_LOGIN",DbType.Boolean,1);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.IsLogin = db.GetParameterValue(cmd, "IS_LOGIN").GetValue<bool>();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(db.GetParameterValue(cmd, "ERROR_DESC").ToString());

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

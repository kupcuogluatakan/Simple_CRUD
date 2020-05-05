using System.Data;
using ODMSModel.ViewModel;
using System;

namespace ODMSData
{
    public class TestData : DataAccessBase
    {
        public void DmlTest(MultiLanguageContentViewModel infTest)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_DML_TEST_TABLE_MAIN");
                db.AddInParameter(cmd, "ML_CONTENT", DbType.String, "I|TR|MVC_Test");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, "TR");
                db.AddInParameter(cmd, "USER_NAME", DbType.String, "ozan");
                db.AddInParameter(cmd, "USER_IP", DbType.String, "1,1");
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

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


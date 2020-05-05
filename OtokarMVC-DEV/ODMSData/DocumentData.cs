using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.Common;
using System;

namespace ODMSData
{

    public class DocumentData : DataAccessBase
    {
        public int DMLDocument(UserInfo user, DocumentInfo model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_DOCUMENT_MAIN");
                db.AddParameter(cmd, "DOC_ID", DbType.Int32, ParameterDirection.InputOutput, "DOC_ID", DataRowVersion.Default, model.DocId);
                db.AddInParameter(cmd, "DOC_MIME_TYPE", DbType.String, MakeDbNull(model.DocMimeType));
                db.AddInParameter(cmd, "DOC_NAME", DbType.String, MakeDbNull(model.DocName));
                db.AddInParameter(cmd, "DOC_IMAGE", DbType.Binary, MakeDbNull(model.DocBinary));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.DocId = db.GetParameterValue(cmd, "DOC_ID").GetValue<int>();
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
            return model.DocId;
        }

        public DocumentInfo GetDocumentById(int docId)
        {
            DocumentInfo documentInfo = new DocumentInfo();
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_DOCUMENT");
                db.AddInParameter(cmd, "DOC_ID", DbType.Int32, MakeDbNull(docId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    documentInfo.DocId = dReader["DOC_ID"].GetValue<int>();
                    documentInfo.DocBinary = dReader["DOC_BINARY"].GetValue<byte[]>();
                    documentInfo.DocMimeType = dReader["DOC_MIME_TYPE"].GetValue<string>();
                    documentInfo.DocName = dReader["DOC_NAME"].GetValue<string>();
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


            return documentInfo;
        }
    }
}

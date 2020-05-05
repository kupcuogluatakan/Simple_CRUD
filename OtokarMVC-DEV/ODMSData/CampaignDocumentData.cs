using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.CampaignDocument;
using System;

namespace ODMSData
{
    public class CampaignDocumentData : DataAccessBase
    {
        public List<CampaignDocumentListModel> ListCampaignDocuments(UserInfo user,CampaignDocumentListModel filter, out int totalCount)
        {
            var retVal = new List<CampaignDocumentListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CAMPAIGN_DOCUMENT");
                db.AddInParameter(cmd, "CAMPAIGN_CODE", DbType.String, MakeDbNull(filter.CampaignCode));
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var campaignDocumentListModel = new CampaignDocumentListModel
                        {
                            CampaignCode = reader["CAMPAIGN_CODE"].GetValue<string>(),
                            DocId = reader["DOC_ID"].GetValue<int>(),
                            DocName = reader["DOC_NAME"].GetValue<string>(),
                            DocumentDesc = reader["DOCUMENT_DESC"].GetValue<string>(),
                            LanguageCode = reader["LANGUAGE_CODE"].GetValue<string>(),
                            LanguageName = reader["LANGUAGE_NAME"].GetValue<string>()
                        };

                        retVal.Add(campaignDocumentListModel);
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

        public void DMLCampaignDocument(UserInfo user, CampaignDocumentViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_CAMPAIGN_DOCUMENT");
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "CAMPAIGN_CODE", DbType.String, model.CampaignCode);
                db.AddInParameter(cmd, "DOCUMENT_DESC", DbType.String, MakeDbNull(model.DocumentDesc));
                db.AddInParameter(cmd, "DOCUMENT_ID", DbType.Int32, MakeDbNull(model.DocId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(model.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.CampaignCode = db.GetParameterValue(cmd, "CAMPAIGN_CODE").ToString();
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

        public CampaignDocumentViewModel GetCampaignDocument(UserInfo user, CampaignDocumentViewModel filter)
        {
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_CAMPAIGN_DOCUMENT");
                db.AddInParameter(cmd, "CAMPAIGN_CODE", DbType.String, MakeDbNull(filter.CampaignCode));
                db.AddInParameter(cmd, "DOCUMENT_ID", DbType.Int32, MakeDbNull(filter.DocId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(filter.LanguageCode));
                db.AddInParameter(cmd, "LANGUAGE_CODE_USER", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.CampaignCode = dReader["CAMPAIGN_CODE"].GetValue<string>();
                    filter.DocId = dReader["DOC_ID"].GetValue<int>();
                    filter.DocName = dReader["DOC_NAME"].GetValue<string>();
                    filter.DocumentDesc = dReader["DOCUMENT_DESC"].GetValue<string>();
                    filter.LanguageCode = dReader["LANGUAGE_CODE"].GetValue<string>();
                    filter.LanguageName = dReader["LANGUAGE_NAME"].GetValue<string>();
                }

                dReader.Close();
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
    }
}

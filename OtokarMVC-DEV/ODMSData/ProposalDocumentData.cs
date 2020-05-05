using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.ProposalDocuments;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSCommon.Security;

namespace ODMSData
{
    public class ProposalDocumentsData : DataAccessBase
    {
        private const string sp_getProposalDocList = "P_LIST_PROPOSAL_DOCUMENTS";
        private const string sp_dmlProposalDoc = "P_DML_PROPOSAL_DOCUMENTS";
        private const string sp_getProposalDoc = "P_GET_PROPOSAL_DOCUMENT";

        public List<ProposalDocumentsListModel> ListProposalDocuments(UserInfo user,ProposalDocumentsListModel docModel, out int totalCount)
        {
            List<ProposalDocumentsListModel> listModels = new List<ProposalDocumentsListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_getProposalDocList);
                db.AddInParameter(cmd, "PROPOSAL_ID", DbType.Int64, docModel.ProposalId);
                db.AddInParameter(cmd, "PROPOSAL_SEQ", DbType.Int32, docModel.ProposalSeq);
                db.AddInParameter(cmd, "GET_ACTIVE_ONES_ONLY", DbType.Boolean, user.IsDealer);
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(docModel.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(docModel.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, docModel.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(docModel.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ProposalDocumentsListModel model = new ProposalDocumentsListModel
                        {
                            Description = dr["TXT_NOTE"].GetValue<string>(),
                            DocId = dr["DOC_ID"].GetValue<long>(),
                            DocumentName = dr["DOC_NAME"].GetValue<string>(),
                            ProposalDocId = dr["ID_PROPOSAL_DOC"].GetValue<long>(),
                            ProposalId = dr["ID_PROPOSAL"].GetValue<long>(),
                            IsActive = dr["IS_ACTIVE"].GetValue<bool>(),
                            IsActiveName = dr["IS_ACTIVE"].GetValue<bool>()
                                                   ? MessageResource.Global_Display_Active
                                                   : string.Format(MessageResource.WorkOrderDoc_Display_Deleted,
                                                                   DateTime.Parse(dr["UPDATE_DATE"].GetValue<string>()),
                                                                   dr["UPDATE_USER"].GetValue<string>())
                        };

                        listModels.Add(model);
                    }
                    dr.Close();
                }
                totalCount = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }


            return listModels;
        }

        public void DMLProposalDocuments(UserInfo user,ProposalDocumentsViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_dmlProposalDoc);
                db.AddInParameter(cmd, "PROPOSAL_DOC_ID", DbType.Int32, model.ProposalDocId);
                db.AddInParameter(cmd, "PROPOSAL_ID", DbType.Int32, model.ProposalId);
                db.AddInParameter(cmd, "PROPOSAL_SEQ", DbType.Int32, model.ProposalSeq);
                db.AddInParameter(cmd, "TXT_NOTE", DbType.String, model.Description);
                db.AddInParameter(cmd, "DOC_NAME", DbType.String, model.DocName);
                db.AddInParameter(cmd, "DOC_MIME_TYPE", DbType.String, model.DocMimeType);
                db.AddInParameter(cmd, "DOC_BINARY", DbType.Binary, model.DocImage);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
                {
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }


        public void GetProposalDocument(ProposalDocumentsViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_getProposalDoc);
                db.AddInParameter(cmd, "DOC_ID", DbType.Int32, model.DocId);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        model.DocImage = dr["DOC_BINARY"].GetValue<byte[]>();
                        model.DocMimeType = dr["DOC_MIME_TYPE"].GetValue<string>();
                        model.DocName = dr["DOC_NAME"].GetValue<string>();
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
            //sp_getProposalDoc
        }
    }
}

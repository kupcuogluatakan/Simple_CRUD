using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.WorkOrderDocuments;
using ODMSCommon.Security;

namespace ODMSData
{
    public class WorkOrderDocumentsData : DataAccessBase
    {
        private const string sp_getWorkOrderDocList = "P_LIST_WORK_ORDER_DOCUMENTS";
        private const string sp_dmlWorkOrderDoc = "P_DML_WORK_ORDER_DOCUMENTS";
        private const string sp_getWorkOrderDoc = "P_GET_WORK_ORDER_DOCUMENT";

        public List<WorkOrderDocumentsListModel> ListWorkOrderDocuments(UserInfo user, WorkOrderDocumentsListModel filter, out int totalCount)
        {
            List<WorkOrderDocumentsListModel> listModels = new List<WorkOrderDocumentsListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_getWorkOrderDocList);
                db.AddInParameter(cmd, "WORK_ORDER_ID", DbType.Int64, filter.WorkOrderId);
                db.AddInParameter(cmd, "GET_ACTIVE_ONES_ONLY", DbType.Boolean, user.IsDealer);
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        WorkOrderDocumentsListModel model = new WorkOrderDocumentsListModel
                        {
                            Description = dr["TXT_NOTE"].GetValue<string>(),
                            DocId = dr["DOC_ID"].GetValue<long>(),
                            DocumentName = dr["DOC_NAME"].GetValue<string>(),
                            WorkOrderDocId = dr["ID_WORK_ORDER_DOC"].GetValue<long>(),
                            WorkOrderId = dr["ID_WORK_ORDER"].GetValue<long>(),
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
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return listModels;
        }

        //TODO : Id set edilmeli
        public void DMLWorkOrderDocuments(UserInfo user, WorkOrderDocumentsViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_dmlWorkOrderDoc);
                db.AddInParameter(cmd, "WORK_ORDER_DOC_ID", DbType.Int32, model.WorkOrderDocId);
                db.AddInParameter(cmd, "WORK_ORDER_ID", DbType.Int32, model.WorkOrderId);
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
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }


        public void GetWorkOrderDocument(WorkOrderDocumentsViewModel filter)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_getWorkOrderDoc);
                db.AddInParameter(cmd, "DOC_ID", DbType.Int32, filter.DocId);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        filter.DocImage = dr["DOC_BINARY"].GetValue<byte[]>();
                        filter.DocMimeType = dr["DOC_MIME_TYPE"].GetValue<string>();
                        filter.DocName = dr["DOC_NAME"].GetValue<string>();
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
    }
}

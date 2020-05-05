using ODMSCommon.Security;
using ODMSModel.PeriodicMaintControlList;
using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;

namespace ODMSData
{
    public class PeriodicMaintControlListData : DataAccessBase
    {
        public List<PeriodicMaintControlListListModel> ListPeriodicMaintControlList(UserInfo user, PeriodicMaintControlListListModel filter, out int totalCount)
        {
            var retVal = new List<PeriodicMaintControlListListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PERIODIC_MAINT_CONTROL_LIST");
                db.AddInParameter(cmd, "ID_TYPE", DbType.Int32, MakeDbNull(filter.IdType));
                db.AddInParameter(cmd, "LANGUAGE_CUSTOM", DbType.String, MakeDbNull(filter.LanguageCustom));
                db.AddInParameter(cmd, "ENGINE_TYPE", DbType.String, MakeDbNull(filter.EngineType));
                db.AddInParameter(cmd, "DOC_ID", DbType.Int64, MakeDbNull(filter.DocId));

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
                        var periodicMaintControlListListModel = new PeriodicMaintControlListListModel
                        {
                            PeriodicMaintCtrlListId = reader["ID_PERIODIC_MAINT_CTRL_LIST"].GetValue<int>(),
                            IdType = reader["ID_TYPE"].GetValue<int>(),
                            EngineType = reader["ENGINE_TYPE"].GetValue<string>(),
                            LanguageCustom = reader["LANGUAGE_CODE"].GetValue<string>(),
                            DocId = reader["DOC_ID"].GetValue<Int64>(),
                            ModelKod = reader["MODEL_KOD"].GetValue<string>(),
                            TypeName = reader["TYPE_NAME"].GetValue<string>(),
                            DocumentDesc = reader["DOCUMENT_DESC"].GetValue<string>(),
                            LanguageName = reader["LANGUAGE_NAME"].GetValue<string>(),
                            DocName = reader["DOC_NAME"].GetValue<string>()
                        };

                        retVal.Add(periodicMaintControlListListModel);
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

        public void DMLPeriodicMaintControlList(UserInfo user, PeriodicMaintControlListViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_PERIODIC_MAINT_CONTROL_LIST");

                db.AddInParameter(cmd, "ID_PERIODIC_MAINT_CTRL_LIST", DbType.Int32, model.PeriodicMaintCtrlListId);
                db.AddInParameter(cmd, "ID_TYPE", DbType.Int32, model.IdType);
                db.AddInParameter(cmd, "ENGINE_TYPE", DbType.String, MakeDbNull(model.EngineType));
                db.AddInParameter(cmd, "LANGUAGE_CUSTOM", DbType.String, MakeDbNull(model.LanguageCustom));
                db.AddInParameter(cmd, "DOC_ID", DbType.Int64, MakeDbNull(model.DocId));
                db.AddInParameter(cmd, "DOCUMENT_DESC", DbType.String, MakeDbNull(model.DocumentDesc));

                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, MakeDbNull(model.CommandType));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.IdType = db.GetParameterValue(cmd, "ID_TYPE").GetValue<int>();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo == 1)
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

        public PeriodicMaintControlListViewModel GetPeriodicMaintControlList(UserInfo user, PeriodicMaintControlListViewModel filter)
        {
            System.Data.Common.DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_PERIODIC_MAINT_CONTROL_LIST");
                db.AddInParameter(cmd, "ID_TYPE", DbType.String, MakeDbNull(filter.IdType));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.IdType = dReader["ID_TYPE"].GetValue<int>();
                    filter.EngineType = dReader["ENGINE_TYPE"].GetValue<string>();
                    filter.DocId = dReader["DOC_ID"].GetValue<Int64>();
                    filter.DocName = dReader["DOC_NAME"].GetValue<string>();
                    filter.DocumentDesc = dReader["DOCUMENT_DESC"].GetValue<string>();
                    filter.ModelKod = dReader["MODEL_KOD"].GetValue<string>();
                    filter.TypeName = dReader["TYPE_NAME"].GetValue<string>();
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

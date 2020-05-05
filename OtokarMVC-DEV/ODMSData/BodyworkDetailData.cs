using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSModel.BodyworkDetail;
using ODMSCommon.Security;

namespace ODMSData
{
    public class BodyworkDetailData : DataAccessBase
    {
        public List<BodyworkDetailListModel> GetBodyworkDetailList(UserInfo user, BodyworkDetailListModel filter, out int totalCount)
        {
            List<BodyworkDetailListModel> listModel = new List<BodyworkDetailListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_BODYWORK_DETAIL");

                db.AddInParameter(cmd, "BODYWORK_CODE", DbType.String, filter.BodyworkCode);
                db.AddInParameter(cmd, "DESCRIPTION", DbType.String, MakeDbNull(filter.Description));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, filter.SortColumn);
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, filter.SortDirection);
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, 0);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, DBNull.Value);
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var model = new BodyworkDetailListModel
                        {
                            BodyworkCode = dr["BODYWORK_CODE"].GetValue<string>(),
                            BodyworkCodeName = dr["BODYWORK_NAME"].GetValue<string>(),
                            Description = dr["DESCRIPTION"].GetValue<string>()
                        };

                        listModel.Add(model);
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
            return listModel;
        }

        public void GetBodyworkDetail(BodyworkDetailViewModel filter)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_BODYWORK_DETAIL");

                db.AddInParameter(cmd, "BODYWORK_CODE", DbType.String, filter.BodyworkCode);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        filter.Descripion = dr["DESCRIPTION"].GetValue<string>();
                    }
                    if (dr.NextResult())
                    {
                        var table = new DataTable();
                        table.Load(dr);
                        filter.BodyworkDetailName.MultiLanguageContentAsText = filter.MultiLanguageContentAsText
                                                                          = GetLanguageContentFromDataSet(table, "BODYWORK_NAME");
                    }
                    dr.Close();
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

        public void DMLBodyworkDetail(UserInfo user, BodyworkDetailViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_BODYWORK_DETAIL_MAIN");

                db.AddInParameter(cmd, "BODYWORK_CODE", DbType.String, model.BodyworkCode);
                db.AddInParameter(cmd, "DESCRIPTION", DbType.String, model.Descripion);
                db.AddInParameter(cmd, "ML_CONTENT", DbType.String, MakeDbNull(model.MultiLanguageContentAsText));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

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

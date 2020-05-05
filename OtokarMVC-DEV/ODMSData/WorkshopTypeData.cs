using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;
using ODMSCommon;
using ODMSModel.WorkshopType;
using ODMSCommon.Security;

namespace ODMSData
{
    public class WorkshopTypeData : DataAccessBase
    {
        public List<WorkshopTypeListModel> GetListWorkshopType(UserInfo user, WorkshopTypeListModel wModel, out int totalCount)
        {
            var listModel = new List<WorkshopTypeListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_WORKSHOP_TYPE");
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, wModel.IsActiveSearch);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, wModel.SortColumn);
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, wModel.SortDirection);
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
                        var model = new WorkshopTypeListModel
                        {
                            WorkshopTypeId = dr["ID_WORKSHOP_TYPE"].GetValue<int>(),
                            Description = dr["DESCRIPTION"].GetValue<string>(),
                            IsActiveString = dr["IS_ACTIVE_S"].GetValue<string>(),
                            WorkshopTypeName = dr["WORKSHOP_TYPE_NAME"].GetValue<string>()
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

        public void DMLWorkshopType(UserInfo user, WorkshopTypeViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_WORKSHOP_TYPE_MAIN");
                db.AddParameter(cmd, "WORKSHOP_TYPE_ID", DbType.Int64, ParameterDirection.InputOutput, "WORKSHOP_TYPE_ID", DataRowVersion.Default, model.WorkshopTypeId);
                db.AddInParameter(cmd, "DESCRIPTION", DbType.String, model.Descripion);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, model.IsActive);
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

        public void GetWorkshopType(UserInfo user, WorkshopTypeViewModel filter)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_WORKSHOP_TYPE");

                db.AddInParameter(cmd, "WORKSHOP_TYPE_ID", DbType.Int32, filter.WorkshopTypeId);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        filter.Descripion = dr["DESCRIPTION"].GetValue<string>();
                        filter.IsActive = dr["IS_ACTIVE"].GetValue<bool>();
                    }
                    if (dr.NextResult())
                    {
                        var table = new DataTable();
                        table.Load(dr);
                        filter.WorkshopTypelName.MultiLanguageContentAsText = filter.MultiLanguageContentAsText
                                                                          = GetLanguageContentFromDataSet(table, "WORKSHOP_TYPE_NAME");
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

        public List<SelectListItem> ListWorkshopTypeAsSelectList(UserInfo user)
        {
            List<SelectListItem> list;
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_WORKSHOP_TYPE_COMBO");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    list = new List<SelectListItem>();
                    while (dr.Read())
                    {
                        SelectListItem item = new SelectListItem
                        {
                            Value = dr["WORKSHOP_TYPE_ID"].GetValue<string>(),
                            Text = dr["WORKSHOP_TYPE_NAME"].GetValue<string>()
                        };

                        list.Add(item);

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
            return list;
        }
    }
}

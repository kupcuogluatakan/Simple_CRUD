using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.ListModel;
using ODMSModel.SparePartType;
using ODMSModel.ViewModel;
using ODMSCommon.Resources;

namespace ODMSData
{
    public class SparePartTypeData:DataAccessBase
    {
        public List<SelectListItem> ListSparePartTypeAsSelectListItem(UserInfo user)
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SPARE_PART_TYPE");
                db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, MakeDbNull(null));
                db.AddInParameter(cmd, "PART_TYPE_CODE", DbType.String, MakeDbNull(null));
                db.AddInParameter(cmd, "PART_TYPE_NAME", DbType.String, MakeDbNull(null));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, true);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, DBNull.Value);
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, DBNull.Value);
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, 0);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, DBNull.Value);
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                            {
                                Value = reader["PART_TYPE_CODE"].GetValue<string>(),
                                Text = reader["PART_TYPE_NAME"].GetValue<string>()
                            };
                        retVal.Add(lookupItem);
                    }
                    reader.Close();
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

            return retVal;
        }

        public List<SparePartTypeListModel> ListSparePartType(UserInfo user,SparePartTypeListModel referenceListModel, out int totalCount)
        {
            var retVal = new List<SparePartTypeListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SPARE_PART_TYPE");
                db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, MakeDbNull(referenceListModel.AdminDesc));
                db.AddInParameter(cmd, "PART_TYPE_CODE", DbType.String, MakeDbNull(referenceListModel.PartTypeCode));
                db.AddInParameter(cmd, "PART_TYPE_NAME", DbType.String, MakeDbNull(referenceListModel.PartTypeName));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, referenceListModel.IsActive);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(referenceListModel.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(referenceListModel.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, referenceListModel.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(referenceListModel.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var sparePartTypeListModel = new SparePartTypeListModel
                            {
                                PartTypeCode = reader["PART_TYPE_CODE"].GetValue<string>(),
                                PartTypeName = reader["PART_TYPE_NAME"].GetValue<string>(),
                                AdminDesc = reader["ADMIN_DESC"].GetValue<string>(),
                                IsActive = reader["IS_ACTIVE"].GetValue<int>(),
                                IsActiveName = reader["IS_ACTIVE_NAME"].GetValue<string>()
                            };
                        retVal.Add(sparePartTypeListModel);
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

        public SparePartTypeIndexViewModel GetSparePartType(UserInfo user,SparePartTypeIndexViewModel filter)
        {
            System.Data.Common.DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_SPARE_PART_TYPE");
                db.AddInParameter(cmd, "PART_TYPE_CODE", DbType.String, MakeDbNull(filter.PartTypeCode));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.PartTypeCode = dReader["PART_TYPE_CODE"].GetValue<string>();
                    filter.AdminDesc = dReader["ADMIN_DESC"].GetValue<string>();
                    filter.IsActive = dReader["IS_ACTIVE"].GetValue<bool>();
                    filter.IsActiveName = dReader["IS_ACTIVE_NAME"].GetValue<string>();
                }
                if (dReader.NextResult())
                {
                    var table = new DataTable();
                    table.Load(dReader);
                    filter.MultiLanguageContentAsText = GetLanguageContentFromDataSet(table, "PART_TYPE_NAME");
                    filter.PartTypeName = (MultiLanguageModel)CommonUtility.DeepClone(filter.PartTypeName);
                    filter.PartTypeName.MultiLanguageContentAsText = filter.MultiLanguageContentAsText;
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


            return filter;
        }

        public void DMLSparePartType(UserInfo user,SparePartTypeIndexViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_SPARE_PART_TYPE_MAIN");
                db.AddParameter(cmd, "PART_TYPE_CODE", DbType.String, ParameterDirection.InputOutput, "PART_TYPE_CODE", DataRowVersion.Default, model.PartTypeCode);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, MakeDbNull(model.AdminDesc));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, model.IsActive);
                db.AddInParameter(cmd, "ML_CONTENT", DbType.String, MakeDbNull(model.MultiLanguageContentAsText));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.PartTypeCode = db.GetParameterValue(cmd, "PART_TYPE_CODE").ToString();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo == 2)
                    model.ErrorMessage = MessageResource.SparePartTypeMain_Error_NullId;
                else if (model.ErrorNo == 1)
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
    }
}

using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSData.DataContracts;
using ODMSModel.Equipment;
using System.Data.Common;
using ODMSModel.ViewModel;
using ODMSCommon.Resources;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class EquipmentTypeData : DataAccessBase, IEquipmentType<EquipmentViewModel>
    {
        public EquipmentViewModel Get(UserInfo user, EquipmentViewModel filter)
        {
            DbDataReader reader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_EQUIPMENT_TYPE");
                db.AddInParameter(cmd, "EQUIPMENT_TYPE_ID", DbType.Int32, MakeDbNull(filter.EquipmentId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    filter.EquipmentId = reader["EQUIPMENT_TYPE_ID"].GetValue<int>();
                    filter.EquipmentTypeName = reader["EQUIPMENT_NAME"].GetValue<string>();
                    filter.EquipmentTypeDesc = reader["DESCRIPTION"].GetValue<string>();
                }

                if (reader.NextResult())
                {
                    var table = new DataTable();
                    table.Load(reader);
                    filter.MultiLanguageContentAsText = GetLanguageContentFromDataSet(table, "EQUIPMENT_NAME");
                    filter.MultiLanguageName = (MultiLanguageModel)CommonUtility.DeepClone(filter.MultiLanguageName);
                    filter.MultiLanguageName.MultiLanguageContentAsText = filter.MultiLanguageContentAsText;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                    reader.Dispose();
                }
                CloseConnection();
            }
            return filter;
        }

        public IEnumerable<EquipmentTypeListModel> List(UserInfo user, EquipmentTypeListModel filter, out int totalCnt)
        {
            var result = new List<EquipmentTypeListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_EQUIPMENT_TYPE");
                //db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                //db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                //db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                //db.AddInParameter(cmd, "OFFSET", DbType.Int32, MakeDbNull(filter.Offset));
                //db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                //db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                //db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                //db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                AddPagingParametersWithLanguage(user, cmd, filter);

                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new EquipmentTypeListModel
                        {
                            EquipmentId = reader["EQUIPMENT_TYPE_ID"].GetValue<int>(),
                            EquipmentTypeName = reader["EQUIPMENT_NAME"].GetValue<string>(),
                            EquipmentTypeDesc = reader["DESCRIPTION"].GetValue<string>()
                        };
                        result.Add(listModel);
                    }
                    reader.Close();
                }
                totalCnt = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return result;
        }

        //TODO : Id set edilmeli
        public void Insert(UserInfo user, EquipmentViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_EQUIPMENT_TYPE");
                db.AddInParameter(cmd, "EQUIPMENT_TYPE_ID", DbType.Int32, MakeDbNull(model.EquipmentId));
                db.AddInParameter(cmd, "ML_CONTENT", DbType.String, model.MultiLanguageContentAsText);
                db.AddInParameter(cmd, "DESCRIPTION", DbType.String, MakeDbNull(model.EquipmentTypeDesc));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo == 2)
                    model.ErrorMessage = MessageResource.EquipmentType_Error_NullId;
                else if (model.ErrorNo > 0)
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

        public void Update(UserInfo user, EquipmentViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_EQUIPMENT_TYPE");
                db.AddInParameter(cmd, "EQUIPMENT_TYPE_ID", DbType.Int32, MakeDbNull(model.EquipmentId));
                db.AddInParameter(cmd, "ML_CONTENT", DbType.String, model.MultiLanguageContentAsText);
                db.AddInParameter(cmd, "DESCRIPTION", DbType.String, MakeDbNull(model.EquipmentTypeDesc));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo == 2)
                    model.ErrorMessage = MessageResource.EquipmentType_Error_NullId;
                else if (model.ErrorNo > 0)
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

        public void Delete(UserInfo user, EquipmentViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_EQUIPMENT_TYPE");
                db.AddInParameter(cmd, "EQUIPMENT_TYPE_ID", DbType.Int32, MakeDbNull(model.EquipmentId));
                db.AddInParameter(cmd, "ML_CONTENT", DbType.String, model.MultiLanguageContentAsText);
                db.AddInParameter(cmd, "DESCRIPTION", DbType.String, MakeDbNull(model.EquipmentTypeDesc));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo == 2)
                    model.ErrorMessage = MessageResource.EquipmentType_Error_NullId;
                else if (model.ErrorNo > 0)
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

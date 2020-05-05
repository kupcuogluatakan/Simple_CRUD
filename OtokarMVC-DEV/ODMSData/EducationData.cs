using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSModel.Education;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class EducationData : DataAccessBase
    {
        private const string sp_dmlEducation = "P_DML_EDUCATION_MAIN";
        private const string sp_getEducationList = "P_LIST_EDUCATION";
        private const string sp_getEducation = "P_GET_EDUCATION";

        public void DMLEducation(UserInfo user, EducationViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_dmlEducation);

                db.AddInParameter(cmd, "EDUCATION_CODE", DbType.String, model.EducationCode);
                db.AddInParameter(cmd, "EDUCATION_TYPE_ID", DbType.Int32, model.EducationTypeId);
                db.AddInParameter(cmd, "VEHICLE_MODEL_KOD", DbType.String, model.VehicleModelCode);
                db.AddInParameter(cmd, "DURATION_DAY", DbType.Decimal, model.EducationDurationDay);
                db.AddInParameter(cmd, "DURATION_HOUR", DbType.Decimal, model.EducationDurationHour);
                db.AddInParameter(cmd, "IS_MANDATORY", DbType.Int32, model.IsMandatory);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, model.IsActive);
                db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, model.AdminDesc);
                db.AddInParameter(cmd, "ML_CONTENT", DbType.String, MakeDbNull(model.MultiLanguageContentAsText));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
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

        public List<EducationListModel> GetEducationList(UserInfo user, EducationListModel filter, out int totalCount)
        {
            List<EducationListModel> listModels = new List<EducationListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_getEducationList);

                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, filter.IsActiveSearch);
                db.AddInParameter(cmd, "IS_MANDATORY", DbType.Int32, filter.IsMandatorySearch);
                db.AddInParameter(cmd, "VEHICLE_MODEL_KOD", DbType.String, filter.VehicleModelKod);
                db.AddInParameter(cmd, "EDUCATION_TYPE_ID", DbType.Int32, filter.EducationTypeId);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
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
                        EducationListModel newModel = new EducationListModel
                        {
                            AdminDesc = dr["ADMIN_DESC"].GetValue<string>(),
                            EducationCode = dr["EDUCATION_CODE"].GetValue<string>(),
                            VehicleModelName = dr["MODEL_NAME"].GetValue<string>(),
                            EducationDurationDay = dr["DURATION_DAY"].GetValue<string>(),
                            EducationDurationHour = dr["DURATION_HOUR"].GetValue<string>(),
                            EducationName = dr["EDUCATION_NAME"].GetValue<string>(),
                            EducationTypeName = dr["EDUCATION_TYPE_NAME"].GetValue<string>(),
                            IsActiveS = dr["IS_ACTIVE_STRING"].GetValue<string>(),
                            IsMandatory = dr["IS_MANDATORY"].GetValue<string>()
                        };

                        listModels.Add(newModel);
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



        public void GetEducation(UserInfo user, EducationViewModel filter)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_getEducation);
                db.AddInParameter(cmd, "EDUCATION_CODE", DbType.String, filter.EducationCode);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        filter.AdminDesc = dr["ADMIN_DESC"].GetValue<string>();
                        filter.EducationCode = dr["EDUCATION_CODE"].GetValue<string>();
                        filter.VehicleModelName = dr["MODEL_NAME"].GetValue<string>();
                        filter.VehicleModelCode = dr["MODEL_KOD"].GetValue<string>();
                        filter.EducationDurationDay = dr["DURATION_DAY"].GetValue<int>();
                        filter.EducationDurationHour = dr["DURATION_HOUR"].GetValue<int>();
                        filter.EducationTypeName = dr["EDUCATION_TYPE_NAME"].GetValue<string>();
                        filter.EducationTypeId = dr["ID_EDUCATION_TYPE"].GetValue<int>();
                        filter.IsActive = dr["IS_ACTIVE"].GetValue<bool>();
                        filter.IsMandatory = dr["IS_MANDATORY"].GetValue<bool>();
                    }
                    if (dr.NextResult())
                    {
                        var table = new DataTable();
                        table.Load(dr);
                        filter.EducationName.MultiLanguageContentAsText = filter.MultiLanguageContentAsText = GetLanguageContentFromDataSet(table,
                                                                                                          "EDUCATION_NAME");
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

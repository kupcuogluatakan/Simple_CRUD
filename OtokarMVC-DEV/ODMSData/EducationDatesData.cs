using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSModel.EducationDates;
using ODMSCommon.Security;

namespace ODMSData
{
    public class EducationDatesData : DataAccessBase
    {
        private const string sp_dmlEducationDates = "P_DML_EDUCATION_DATES";
        private const string sp_getEducationDatesList = "P_LIST_EDUCATION_DATES";
        private const string sp_getEducationDate = "P_GET_EDUCATION_DATE";

        public void DMLEducationDates(UserInfo user, EducationDatesViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_dmlEducationDates);
                db.AddInParameter(cmd, "EDUCATION_CODE", DbType.String, model.EducationCode);
                db.AddInParameter(cmd, "ROW_NUMBER", DbType.Decimal, model.RowNumber);
                db.AddInParameter(cmd, "EDUCATION_TIME", DbType.DateTime, model.EducationTimeDT);
                db.AddInParameter(cmd, "EDUCATION_PLACE", DbType.String, model.EducationPlace);
                db.AddInParameter(cmd, "INSTRUCTOR", DbType.String, model.Instructor);
                db.AddInParameter(cmd, "NOTES", DbType.String, model.Notes);
                db.AddInParameter(cmd, "MAX_ATTENDEE", DbType.Decimal, model.MaximumAtt);
                db.AddInParameter(cmd, "MIN_ATTENDEE", DbType.Decimal, model.MinimumAtt);
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

        public List<EducationDatesListModel> GetEducationDatesList(UserInfo user, EducationDatesListModel filter, out int totalCount)
        {
            List<EducationDatesListModel> listModels = new List<EducationDatesListModel>();

            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_getEducationDatesList);
                db.AddInParameter(cmd, "EDUCATION_CODE", DbType.String, filter.EducationCode ?? "");
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
                        var model = new EducationDatesListModel
                        {
                            EducationCode = dr["EDUCATION_CODE"].GetValue<string>(),
                            RowNumber = dr["ROW_NUMBER"].GetValue<int>(),
                            EducationPlace = dr["EDUCATION_PLACE"].GetValue<string>(),
                            Instructor = dr["INSTRUCTOR"].GetValue<string>(),
                            Note = dr["NOTES"].GetValue<string>(),
                            MaximumAtt = dr["MAX_ATTENDEE"].GetValue<string>(),
                            MinimumAtt = dr["MIN_ATTENDEE"].GetValue<string>(),
                            EducationDate = dr["EDUCATION_TIME"].GetValue<DateTime>().ToString("g"),
                            EducationDateTime = dr["EDUCATION_TIME"].GetValue<DateTime>()
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

        public void GetEducationDate(UserInfo user, EducationDatesViewModel filter)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_getEducationDate);
                db.AddInParameter(cmd, "EDUCATION_CODE", DbType.String, MakeDbNull(filter.EducationCode));
                db.AddInParameter(cmd, "ROW_NUMBER", DbType.Int32, filter.RowNumber);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        filter.EducationPlace = dr["EDUCATION_PLACE"].GetValue<string>();
                        filter.Instructor = dr["INSTRUCTOR"].GetValue<string>();
                        filter.Notes = dr["NOTES"].GetValue<string>();
                        filter.MaximumAtt = dr["MAX_ATTENDEE"].GetValue<int>();
                        filter.MinimumAtt = dr["MIN_ATTENDEE"].GetValue<int>();
                        filter.EducationTimeDT = dr["EDUCATION_TIME"].GetValue<DateTime>();
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
    }
}

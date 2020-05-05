using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.EducationContributers;
using ODMSCommon.Security;

namespace ODMSData
{
    public class EducationContributersData : DataAccessBase
    {
        private const string sp_getEducationContList = "P_LIST_EDUCATION_CONTRIBUTERS";
        private const string sp_dmlEducationCont = "P_DML_EDUCATION_CONTRIBUTER";

        public List<EducationContributersListModel> GetEducationContList(UserInfo user, EducationContributersListModel filter, out int totalCount)
        {
            List<EducationContributersListModel> listModels = new List<EducationContributersListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_getEducationContList);

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
                        EducationContributersListModel model = new EducationContributersListModel
                        {
                            EducationCode = dr["EDUCATION_CODE"].GetValue<string>(),
                            DealerName = dr["DEALER_NAME"].GetValue<string>(),
                            FullName = dr["NAME_SURNAME"].GetValue<string>(),
                            Grade = dr["GRADE"].GetValue<string>(),
                            RowNumber = dr["ROW_NUMBER"].GetValue<int>(),
                            TcIdentity = dr["TC_IDENTITY_NO"].GetValue<string>(),
                            WorkingCompany = dr["WORKING_COMPANY"].GetValue<string>(),
                            EducationDate = dr["EDUCATION_TIME"].GetValue<DateTime>()
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

        public void DMLEducationContributers(UserInfo user, EducationContributersViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_dmlEducationCont);
                db.AddInParameter(cmd, "EDUCATION_CODE", DbType.String, model.EducationCode);
                db.AddInParameter(cmd, "ROW_NUMBER", DbType.Decimal, model.RowNumber);
                db.AddInParameter(cmd, "TC_IDENTITY_NO", DbType.String, model.TCIdentity);
                db.AddInParameter(cmd, "NAME_SURNAME", DbType.String, model.FullName);
                db.AddInParameter(cmd, "WORKING_COMPANY", DbType.String, model.WorkingCompany);
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, model.DealerId);
                db.AddInParameter(cmd, "GRADE", DbType.String, model.Grade);
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

        public void IsDMLEducationContributers(UserInfo user, EducationContributersViewModel filter, List<EducationContributersViewModel> filterModel)
        {
            //If list has an error then return
            if (!filterModel.Exists(q => q.ErrorNo == 1))
            {
                bool isSuccess = true;
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_dmlEducationCont);
                CreateConnection(cmd);
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    foreach (var item in filterModel)
                    {
                        cmd.Parameters.Clear();
                        db.AddInParameter(cmd, "EDUCATION_CODE", DbType.String, filter.EducationCode);
                        db.AddInParameter(cmd, "ROW_NUMBER", DbType.Decimal, filter.RowNumber);
                        db.AddInParameter(cmd, "TC_IDENTITY_NO", DbType.String, item.TCIdentity);
                        db.AddInParameter(cmd, "NAME_SURNAME", DbType.String, item.FullName);
                        db.AddInParameter(cmd, "WORKING_COMPANY", DbType.String, item.WorkingCompany);
                        db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, item.DealerId);
                        db.AddInParameter(cmd, "GRADE", DbType.String, item.Grade);
                        db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, filter.CommandType);
                        db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                        db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                        db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                        db.ExecuteNonQuery(cmd, transaction);
                        item.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();

                        if (item.ErrorNo > 0)
                        {
                            isSuccess = false;
                            item.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                        }

                    }

                }
                catch (Exception Ex)
                {
                    isSuccess = false;
                    filter.ErrorNo = 1;
                    filter.ErrorMessage = MessageResource.Err_Generic_Unexpected + "System Message : (" + Ex.Message + ")";
                }
                finally
                {
                    if (isSuccess)
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                    CloseConnection();
                }
            }

        }
    }
}

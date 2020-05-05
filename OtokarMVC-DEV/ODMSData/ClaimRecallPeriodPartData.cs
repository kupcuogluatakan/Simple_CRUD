using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.ClaimRecallPeriodPart;
using System.Data.Common;

namespace ODMSData
{
    public class ClaimRecallPeriodPartData : DataAccessBase
    {
        public List<ClaimRecallPeriodPartListModel> ListClaimRecallPeriodPart(UserInfo user,ClaimRecallPeriodPartListModel filter, out int totalCount)
        {
            var retVal = new List<ClaimRecallPeriodPartListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CLAIM_RECALL_PERIOD_PART");
                db.AddInParameter(cmd, "PART_CODE", DbType.String, MakeDbNull(filter.PartCode));
                db.AddInParameter(cmd, "PART_NAME", DbType.String, MakeDbNull(filter.PartName));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, filter.SearchIsActive);
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var claimRecallPeriodPartListModel = new ClaimRecallPeriodPartListModel
                        {
                            PartCode = reader["PART_CODE"].GetValue<string>(),
                            PartId = reader["PART_ID"].GetValue<int>(),
                            PartName = reader["PART_NAME"].GetValue<string>(),
                            IsActiveString = reader["IS_ACTIVE"].GetValue<bool>() ? MessageResource.Global_Display_Active : MessageResource.Global_Display_Passive,
                            CreateDate = reader["CREATE_DATE"].GetValue<DateTime>(),
                            UpdateDate = reader["UPDATE_DATE"].GetValue<DateTime?>(),
                            CreateUser = reader["CREATE_USER"].GetValue<string>(),
                            UpdateUser = reader["UPDATE_USER"].GetValue<string>()
                        };
                        retVal.Add(claimRecallPeriodPartListModel);
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

        public bool DeleteAllClaimRecallPeriodPart(int claimRecallPeriodId)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DELETE_ALL_CLAIM_RECALL_PERIOD_PART");
                db.AddInParameter(cmd, "CLAIM_RECALL_PERIOD_ID", DbType.Int32, claimRecallPeriodId);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                int errorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                if (errorNo > 0)
                {
                    return false;
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
            return true;
        }

        public void DMLClaimRecallPeriodPart(UserInfo user, ClaimRecallPeriodPartViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_CLAIM_RECALL_PERIOD_PART");

                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, MakeDbNull(model.PartId));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, MakeDbNull(model.IsActive));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
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
        public ClaimRecallPeriodPartViewModel GetClaimRecallPeriodPart(UserInfo user, long partId)
        {
            ClaimRecallPeriodPartViewModel viewModel = new ClaimRecallPeriodPartViewModel();

            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_CLAIM_RECALL_PERIOD_PART");
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, MakeDbNull(partId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    viewModel.PartId = dReader["PartId"].GetValue<int>();
                    viewModel.PartCode = dReader["PartCode"].GetValue<string>();
                    viewModel.PartName = dReader["PartName"].GetValue<string>();
                    viewModel.IsActive = dReader["IsActive"].GetValue<bool>();
                    viewModel.UpdateDate = dReader["UpdateDate"].GetValue<DateTime>();
                    viewModel.CreateUserName = dReader["CreateUserName"].GetValue<string>();
                    viewModel.UpdateUserName = dReader["UpdateUserName"].GetValue<string>();
                }

                return viewModel;
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
        }
    }
}

using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.FleetRequest;
using ODMSCommon.Resources;
using System;

namespace ODMSData
{
    public class FleetRequestData : DataAccessBase
    {
        //TODO: Id set edilmeli
        public void DMLFleetRequest(UserInfo user,FleetRequestViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_FLEET_REQUEST");
                db.AddInParameter(cmd, "FLEET_REQUEST_ID", DbType.Int32, MakeDbNull(model.FleetRequestId));
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, user.GetUserDealerId());
                db.AddInParameter(cmd, "STATUS_ID", DbType.String, model.StatusId);
                db.AddInParameter(cmd, "DESCRIPTION", DbType.String, MakeDbNull(model.Description));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo == 2)
                    model.ErrorMessage = MessageResource.FleetRequest_Error_NullId;
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

        public FleetRequestViewModel GetFleetRequest(UserInfo user,FleetRequestViewModel referenceModel)
        {
            DbDataReader reader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_FLEET_REQUEST");
                db.AddInParameter(cmd, "FLEET_REQUEST_ID", DbType.Int32, MakeDbNull(referenceModel.FleetRequestId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    referenceModel.FleetRequestId = reader["FLEET_REQUEST_ID"].GetValue<int>();
                    referenceModel.StatusId = reader["STATUS_ID"].GetValue<int>();
                    referenceModel.StatusName = reader["STATUS_NAME"].GetValue<string>();
                    referenceModel.Description = reader["DESCRIPTION"].GetValue<string>();
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
            return referenceModel;
        }

        public List<FleetRequestListModel> ListFleetRequests(UserInfo user,FleetRequestListModel referenceModel, out int totalCnt)
        {
            var result = new List<FleetRequestListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_FLEET_REQUEST");
                db.AddInParameter(cmd, "STATUS_ID", DbType.String, referenceModel.StatusId);
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, user.GetUserDealerId());
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(referenceModel.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(referenceModel.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, referenceModel.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(referenceModel.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new FleetRequestListModel
                        {
                            FleetRequestId = reader["FLEET_REQUEST_ID"].GetValue<int>(),
                            StatusId = reader["STATUS_ID"].GetValue<int>(),
                            StatusName = reader["STATUS_NAME"].GetValue<string>(),
                            Description = reader["DESCRIPTION"].GetValue<string>(),
                            DealerName = reader["DEALER_NAME"].GetValue<string>(),
                            RejectDescription = reader["REJECT_DESCRIPTION"].GetValue<string>(),
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
    }
}

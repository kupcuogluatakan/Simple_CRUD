using ODMSModel.FleetRequestConfirm;
using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSCommon.Security;

namespace ODMSData
{
    public class FleetRequestConfirmData : DataAccessBase
    {
        public List<FleetRequestConfirmListModel> ListFleetRequestConfirm(UserInfo user, FleetRequestConfirmListModel filter, out int totalCnt)
        {
            var result = new List<FleetRequestConfirmListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_FLEET_REQUEST_CONFIRM");
                db.AddInParameter(cmd, "STATUS_ID", DbType.String, filter.StatusId);
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
                        var listModel = new FleetRequestConfirmListModel
                        {
                            FleetRequestId = reader["FLEET_REQUEST_ID"].GetValue<int>(),
                            StatusId = reader["STATUS_ID"].GetValue<int>(),
                            StatusName = reader["STATUS_NAME"].GetValue<string>(),
                            Description = reader["DESCRIPTION"].GetValue<string>(),
                            CreateDate = reader["CREATE_DATE"].GetValue<DateTime>(),
                            DealerName = reader["DEALER_NAME"].GetValue<string>(),
                            RejectDescription = reader["REJECT_DESCRIPTION"].GetValue<string>()
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
        public void DMLFleetRequestConfirm(UserInfo user, FleetRequestConfirmViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_FLEET_REQUEST_CONFIRM");
                db.AddInParameter(cmd, "FLEET_REQUEST_ID", DbType.Int32, MakeDbNull(model.FleetRequestId));
                db.AddInParameter(cmd, "STATUS", DbType.Int32, MakeDbNull(model.StatusId));
                db.AddInParameter(cmd, "REJECT_DESC", DbType.String, MakeDbNull(model.RejectDescription));
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull((user.IsDealer) ? user.DealerID : user.GetUserDealerId()));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
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
    }
}

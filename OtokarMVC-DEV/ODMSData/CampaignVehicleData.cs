using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSModel.CampaignVehicle;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class CampaignVehicleData : DataAccessBase
    {
        public List<CampaignVehicleListModel> ListCampaignVehiclesMain(UserInfo user, CampaignVehicleListModel filter, out int totalCount)
        {
            var retVal = new List<CampaignVehicleListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CAMPAIGN_VEHICLE");
                db.AddInParameter(cmd, "CAMPAIGN_CODE", DbType.String, MakeDbNull(filter.CampaignCode));
                db.AddInParameter(cmd, "VIN_NO", DbType.String, MakeDbNull(filter.VinNo));
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var campaignVehicleListModel = new CampaignVehicleListModel
                        {
                            CampaignCode = reader["CAMPAIGN_CODE"].GetValue<string>(),
                            VehicleId = reader["VEHICLE_ID"].GetValue<int>(),
                            VinNo = reader["VIN_NO"].GetValue<string>(),
                            IsUtilized = reader["IS_UTILIZED"].GetValue<int>(),
                            IsActive = reader["IS_ACTIVE"].GetValue<int>(),
                            IsActiveName = reader["IS_ACTIVE_NAME"].GetValue<string>(),
                            IsUtilizedName = reader["IS_UTILIZED_NAME"].GetValue<string>(),
                            IsComplete = reader["IS_COMPLETE"].GetValue<int>(),
                            IsCompleteString = reader["IS_COMPLETE_NAME"].GetValue<string>()
                        };
                        retVal.Add(campaignVehicleListModel);
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


        public List<CampaignVehicleListModel> ListCampaignVehicles(UserInfo user,CampaignVehicleListModel filter, out int totalCount)
        {
            var retVal = new List<CampaignVehicleListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CAMPAIGN_VEHICLE_POPUP");
                db.AddInParameter(cmd, "CAMPAIGN_CODE", DbType.String, MakeDbNull(filter.CampaignCode));
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var campaignVehicleListModel = new CampaignVehicleListModel
                        {
                            VinNo = reader["VIN_NO"].GetValue<string>(),
                            IsUtilized = reader["IS_UTILIZED"].GetValue<int>(),
                            IsUtilizedName = reader["IS_UTILIZED_NAME"].GetValue<string>(),
                            DealerName = reader["DEALER_NAME"].GetValue<string>(),
                            WorkOrderID = reader["ID_WORK_ORDER"].GetValue<string>(),
                            WorkOrderVehicleLeaveDate = reader["VEHICLE_LEAVE_DATE"].GetValue<DateTime?>(),
                            WorkOrderKM = reader["VEHICLE_KM"].GetValue<int>(),
                            IsCanceled = reader["IS_CANCELED"].GetValue<bool>(),
                            IsCanceledName = reader["IS_CANCELED_NAME"].GetValue<string>(),
                            DenyReason = reader["CAMPAIGN_DENY_REASON"].GetValue<string>(),
                            CancelReason = reader["CANCEL_REASON"].GetValue<string>(),
                        };
                        retVal.Add(campaignVehicleListModel);
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

        public List<CampaignVehicleListModel> ListCampaignVehiclesForDealer(UserInfo user, CampaignVehicleListModel filter, out int totalCount)
        {
            var retVal = new List<CampaignVehicleListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CAMPAIGN_VEHICLE_FOR_DEALER");
                db.AddInParameter(cmd, "CAMPAIGN_CODE", DbType.String, MakeDbNull(filter.CampaignCode));
                db.AddInParameter(cmd, "VIN_NO", DbType.String, MakeDbNull(filter.VinNo));
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, (user.IsDealer) ? MakeDbNull(user.GetUserDealerId()) : null);
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var campaignVehicleListModel = new CampaignVehicleListModel
                        {
                            CampaignCode = reader["CAMPAIGN_CODE"].GetValue<string>(),
                            IsActive = reader["IS_ACTIVE"].GetValue<int>(),
                            VehicleId = reader["VEHICLE_ID"].GetValue<int>(),
                            IsActiveName = reader["IS_ACTIVE_NAME"].GetValue<string>(),
                            IsUtilized = reader["IS_UTILIZED"].GetValue<int>(),
                            IsUtilizedName = reader["IS_UTILIZED_NAME"].GetValue<string>(),
                            VinNo = reader["VIN_NO"].GetValue<string>(),
                            DealerName = reader["DEALER_SHRT_NAME"].GetValue<string>(),
                            Customer = reader["CUSTOMER"].GetValue<string>(),
                            LastWorkOrderId = reader["LAST_WORK_ORDER_ID"].GetValue<string>(),
                            IsComplete = reader["IS_COMPLETE"].GetValue<int>(),
                            IsCompleteString = reader["IS_COMPLETE_NAME"].GetValue<string>(),
                        };
                        retVal.Add(campaignVehicleListModel);
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

        public void DMLCampaignVehicle(UserInfo user, CampaignVehicleViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_CAMPAIGN_VEHICLE");
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "CAMPAIGN_CODE", DbType.String, model.CampaignCode);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, model.IsActive);
                db.AddInParameter(cmd, "IS_UTILIZED", DbType.Int32, model.IsUtilized);
                db.AddInParameter(cmd, "VEHICLE_ID", DbType.Int32, MakeDbNull(model.VehicleId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.CampaignCode = db.GetParameterValue(cmd, "CAMPAIGN_CODE").ToString();
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

        public CampaignVehicleViewModel GetCampaignVehicle(UserInfo user, CampaignVehicleViewModel filter)
        {
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_CAMPAIGN_VEHICLE");
                db.AddInParameter(cmd, "CAMPAIGN_CODE", DbType.String, MakeDbNull(filter.CampaignCode));
                db.AddInParameter(cmd, "VEHICLE_ID", DbType.Int32, MakeDbNull(filter.VehicleId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.CampaignCode = dReader["CAMPAIGN_CODE"].GetValue<string>();
                    filter.IsActive = dReader["IS_ACTIVE"].GetValue<bool>();
                    filter.VehicleId = dReader["VEHICLE_ID"].GetValue<int>();
                    filter.IsActiveName = dReader["IS_ACTIVE_NAME"].GetValue<string>();
                    filter.IsUtilized = dReader["IS_UTILIZED"].GetValue<bool>();
                    filter.IsUtilizedName = dReader["IS_UTILIZED_NAME"].GetValue<string>();
                    filter.VinNo = dReader["VIN_NO"].GetValue<string>();
                }

                dReader.Close();
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
    }
}

using System;
using ODMSModel.CampaignRequest;
using System.Collections.Generic;
using ODMSCommon;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;
using ODMSModel.ProposalCard;
using ODMSCommon.Security;

namespace ODMSData
{
    public class CampaignRequestData : DataAccessBase
    {
        public List<CampaignRequestListModel> ListCampaignRequest(UserInfo user,CampaignRequestListModel filter, out int totalCount)
        {
            var retVal = new List<CampaignRequestListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CAMPAIGN_REQUEST");
                db.AddInParameter(cmd, "ID_CAMPAIGN_REQUEST", DbType.Int32, MakeDbNull(filter.IdCampaignRequest));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(filter.IdDealer));
                db.AddInParameter(cmd, "VEHICLE_MODEL_CODE", DbType.String, MakeDbNull(filter.VerihcleModelCode));
                db.AddInParameter(cmd, "CAMPAIGN_NAME", DbType.String, MakeDbNull(filter.CampaignName));
                db.AddInParameter(cmd, "REQUEST_STATUS", DbType.Decimal, filter.RequestStatus);
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var campaignRequestListModel = new CampaignRequestListModel
                        {
                            IdCampaignRequest = reader["ID_CAMPAIGN_REQUEST"].GetValue<decimal>(),
                            VerihcleModelCode = reader["MODEL_KOD"].GetValue<string>(),
                            CampaignName = reader["CAMPAIGN_NAME"].GetValue<string>(),
                            DealerName = reader["DEALER_NAME"].GetValue<string>(),
                            WorkOrderId = reader["ID_WORK_ORDER"].GetValue<Int64>(),
                            RequestStatus = reader["REQUEST_STATUS"].GetValue<decimal>(),
                            RequestStatusName = reader["REQUEST_STATUS_NAME"].GetValue<string>(),
                            Quantity = reader["QUANTITY"].GetValue<decimal>(),
                            PreferredOrderDate = reader["PREFERRED_ORDER_DATE"].GetValue<DateTime?>(),
                            PoNumber = reader["PO_NUMBER"].GetValue<int?>()
                        };

                        retVal.Add(campaignRequestListModel);
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

        public void DMLCampaignRequest(UserInfo user, CampaignRequestViewModel model)
        {
            try
            { 
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_CAMPAIGN_REQUEST");
                db.AddParameter(cmd, "ID_CAMPAIGN_REQUEST", DbType.Int64, ParameterDirection.InputOutput, "ID_CAMPAIGN_REQUEST", DataRowVersion.Default, model.IdCampaignRequest);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "QUANTITY", DbType.Int32, MakeDbNull(model.Quantity));
                db.AddInParameter(cmd, "REQUEST_STATUS", DbType.Int32, model.RequestStatus);
                db.AddInParameter(cmd, "CAMPAIGN_CODE", DbType.String, MakeDbNull(model.CampaignCode));
                db.AddInParameter(cmd, "VIN_CODES", DbType.String, MakeDbNull(model.CampaignVinCodes));
                db.AddInParameter(cmd, "REJECTION_NOTE", DbType.String, MakeDbNull(model.RejectionNote));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(model.IdDealer));
                db.AddInParameter(cmd, "REQUEST_NOTE", DbType.String, MakeDbNull(model.RequestNote));
                db.AddInParameter(cmd, "PREFERRED_ORDER_DATE", DbType.DateTime, MakeDbNull(model.PreferredOrderDate));
                db.AddInParameter(cmd, "PO_NUMBER", DbType.Int64, MakeDbNull(model.PoNumber));

                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.IdCampaignRequest = db.GetParameterValue(cmd, "ID_CAMPAIGN_REQUEST").GetValue<decimal>();
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

        public CampaignRequestViewModel GetCampaignRequest(UserInfo user, CampaignRequestViewModel filter)
        {
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_CAMPAIGN_REQUEST");
                db.AddInParameter(cmd, "ID_CAMPAIGN_REQUEST", DbType.String, MakeDbNull(filter.IdCampaignRequest));

                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.CampaignCode = dReader["CAMPAIGN_CODE"].GetValue<string>();
                    filter.DealerName = dReader["DEALER_NAME"].GetValue<string>();
                    filter.IdDealer = dReader["ID_DEALER"].GetValue<int>();
                    filter.RequestNote = dReader["REQUEST_NOTE"].GetValue<string>();
                    filter.RequestStatus = dReader["REQUEST_STATUS"].GetValue<int>();
                    filter.Quantity = dReader["QUANTITY"].GetValue<int>();
                    filter.CampaignName = dReader["CAMPAIGN_NAME"].GetValue<string>();
                    filter.VerihcleModelCode = dReader["MODEL_KOD"].GetValue<string>();
                    filter.RequestStatusName = dReader["REQUEST_STATUS_NAME"].GetValue<string>();
                    filter.PreferredOrderDate = dReader["PREFERRED_ORDER_DATE"].GetValue<DateTime?>();
                    filter.WorkOrderId = dReader["ID_WORK_ORDER"].GetValue<Int64>();
                    filter.UpdateUser = dReader["UPDATE_USER"].GetValue<Int32>();
                    filter.UpdateUserName = dReader["UPDATE_USER_NAME"].GetValue<string>();
                    filter.CampaignVinCodes = dReader["VIN_CODES"].GetValue<string>();
                    filter.RejectionNote = dReader["REJECTION_NOTE"].GetValue<string>();
                    filter.VehicleId = dReader["VEHICLE_ID"].GetValue<int>();
                    filter.WodCampaignRequestId = dReader["WOD_CAMPAIGN_REQUEST_ID"].GetValue<int>();
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
        public ProposalCampaignRequestViewModel GetCampaignRequestProposal(UserInfo user, ProposalCampaignRequestViewModel filter)
        {
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_CAMPAIGN_REQUEST_PROPOSAL");
                db.AddInParameter(cmd, "ID_CAMPAIGN_REQUEST", DbType.String, MakeDbNull(filter.IdCampaignRequest));

                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.CampaignCode = dReader["CAMPAIGN_CODE"].GetValue<string>();
                    filter.DealerName = dReader["DEALER_NAME"].GetValue<string>();
                    filter.IdDealer = dReader["ID_DEALER"].GetValue<int>();
                    filter.RequestNote = dReader["REQUEST_NOTE"].GetValue<string>();
                    filter.RequestStatus = dReader["REQUEST_STATUS"].GetValue<int>();
                    filter.Quantity = dReader["QUANTITY"].GetValue<int>();
                    filter.CampaignName = dReader["CAMPAIGN_NAME"].GetValue<string>();
                    filter.VerihcleModelCode = dReader["MODEL_KOD"].GetValue<string>();
                    filter.RequestStatusName = dReader["REQUEST_STATUS_NAME"].GetValue<string>();
                    filter.PreferredOrderDate = dReader["PREFERRED_ORDER_DATE"].GetValue<DateTime?>();
                    filter.ProposalId = dReader["ID_PROPOSAL"].GetValue<Int64>();
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

        public List<SelectListItem> ListRequestStatusAsSelectListItem()
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CAMPAIGN_REQUEST_STATUS_LIST_COMBO");
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem { Value = reader["REQUEST_STATUS"].GetValue<string>() };
                        switch (lookupItem.Value)
                        {
                            case "0":
                                lookupItem.Text = "Taslak";
                                break;
                            case "1":
                                lookupItem.Text = "Onay Bekliyor";
                                break;
                            case "2":
                                lookupItem.Text = "Onaylı";
                                break;
                            case "3":
                                lookupItem.Text = "İptal";
                                break;
                        }
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

        public List<CampaignRequestDetailListModel> ListCampaignRequestDetails(UserInfo user,CampaignRequestDetailListModel filter, out int totalCount)
        {
            var retVal = new List<CampaignRequestDetailListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CAMPAIGN_REQUEST_DET");
                db.AddInParameter(cmd, "ID_CAMPAIGN_REQUEST", DbType.Int32, MakeDbNull(filter.CampaignRequestId));
                db.AddInParameter(cmd, "SUPPLY_TYPE", DbType.Int32, MakeDbNull(filter.SupplyTypeId));
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var campaignRequestListModel = new CampaignRequestDetailListModel
                        {
                            CampaignRequestId = reader["CAMPAIGN_REQUEST_ID"].GetValue<int>(),
                            PartId = reader["PART_ID"].GetValue<int>(),
                            PartName = reader["PART_NAME"].GetValue<string>(),
                            SupplyTypeId = reader["SUPPLY_TYPE_ID"].GetValue<int>(),
                            SupplyTypeName = reader["SUPPLY_TYPE_NAME"].GetValue<string>(),
                            Quantity = reader["QUANTITY"].GetValue<decimal>()
                        };

                        retVal.Add(campaignRequestListModel);
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

        public List<CampaignRequestDetailListModel> ListCampaignRequestDetailsAndQuantity(CampaignRequestDetailListModel filter)
        {
            var retVal = new List<CampaignRequestDetailListModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CAMPAIGN_REQUEST_DET_AND_QNTY");
                db.AddInParameter(cmd, "ID_CAMPAIGN_REQUEST", DbType.Int32, MakeDbNull(filter.CampaignRequestId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var campaignRequestListModel = new CampaignRequestDetailListModel
                        {
                            CampaignRequestId = reader["CAMPAIGN_REQUEST_ID"].GetValue<int>(),
                            PartId = reader["PART_ID"].GetValue<int>(),
                            SupplyTypeId = reader["SUPPLY_TYPE_ID"].GetValue<int>(),
                            MstQty = reader["MST_QTY"].GetValue<decimal>(),
                            DetQty = reader["DET_QTY"].GetValue<decimal>(),
                            PackageQty = reader["PACKAGE_QNTY"].GetValue<decimal>()
                        };

                        retVal.Add(campaignRequestListModel);
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
    }
}

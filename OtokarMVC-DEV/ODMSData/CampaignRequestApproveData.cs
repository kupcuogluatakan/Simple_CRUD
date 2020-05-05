using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;
using ODMSCommon;
using ODMSModel.CampaignRequestApprove;
using System;
using ODMSCommon.Security;
using ODMSCommon.Resources;

namespace ODMSData
{
    public class CampaignRequestApproveData : DataAccessBase
    {
        public List<SelectListItem> ListSupplierDealerAsSelectListItem(int campaignRequestId, int requiredQuantity)
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SUPPLIER_DEALER_COMBO");
                db.AddInParameter(cmd, "CAMPAIGN_REQUEST_ID", DbType.Int32, MakeDbNull(campaignRequestId));
                db.AddInParameter(cmd, "REQUIRED_QUANTITY", DbType.Int32, MakeDbNull(requiredQuantity));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["DEALER_ID"].GetValue<string>(),
                            Text = reader["DEALER_NAME"].GetValue<string>() + " / "+ MessageResource.CampaignRequestApprove_Display_PackageQuantity+": " + reader["Quantity"].GetValue<string>()

                            // /(Text içindeki Slash silinmesin html tarafında ayıraç olarak kullanılıyor.)
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

        public List<CampaignRequestApproveListModel> ListCampaignRequestApprove(UserInfo user,CampaignRequestApproveListModel filter, out int totalCount)
        {
            var retVal = new List<CampaignRequestApproveListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CAMPAIGN_REQUEST_APPROVE");
                db.AddInParameter(cmd, "REQUEST_DEALER_ID", DbType.Int32, MakeDbNull(filter.RequestDealerId));
                db.AddInParameter(cmd, "SUPPLIER_DEALER_ID", DbType.Int32, MakeDbNull(filter.SupplierDealerId));
                db.AddInParameter(cmd, "SUPPLIER_TYPE_ID", DbType.Int32, filter.SupplierTypeId);
                db.AddInParameter(cmd, "MODEL_KOD", DbType.String, MakeDbNull(filter.ModelKod));
                db.AddInParameter(cmd, "CAMPAIGN_CODE", DbType.String, MakeDbNull(filter.CampaignCode));
                db.AddInParameter(cmd, "REQUEST_STATUS", DbType.Int32, MakeDbNull(filter.RequestStatusId));
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var campaignRequestListModel = new CampaignRequestApproveListModel
                        {
                            CampaignCode = reader["CAMPAIGN_CODE"].GetValue<string>(),
                            CampaignName = reader["CAMPAIGN_NAME"].GetValue<string>(),
                            CampaignRequestId = reader["CAMPAIGN_REQUEST_ID"].GetValue<int>(),
                            ModelKod = reader["MODEL_KOD"].GetValue<string>(),
                            Quantity = reader["QUANTITY"].GetValue<decimal>(),
                            RequestDealerId = reader["REQUEST_DEALER_ID"].GetValue<int?>(),
                            RequestDealerName = reader["REQUEST_DEALER_NAME"].GetValue<string>(),
                            RequestNote = reader["REQUEST_NOTE"].GetValue<string>(),
                            RequestStatusId = reader["REQUEST_STATUS_ID"].GetValue<int>(),
                            RequestStatusName = reader["REQUEST_STATUS_NAME"].GetValue<string>(),
                            SupplierDealerId = reader["SUPPLIER_DEALER_ID"].GetValue<int?>(),
                            SupplierDealerName = reader["SUPPLIER_DEALER_NAME"].GetValue<string>(),
                            SupplierTypeId = reader["SUPPLIER_TYPE_ID"].GetValue<int>(),
                            SupplierTypeName = reader["SUPPLIER_TYPE_NAME"].GetValue<string>(),
                            VinCodes = reader["VIN_CODES"].GetValue<string>(),
                            ApprovedVinCodes = reader["APPROVED_VIN_CODES"].GetValue<string>(),
                            ApprovedIdCampaignRequest = reader["APPROVED_ID_CAMPAIGN_REQUEST"].GetValue<string>(),
                            RejectionNote = reader["REJECTION_NOTE"].GetValue<string>(),
                            UpdateUserName = reader["UPDATE_USER_NAME"].GetValue<string>(),
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

        public void DMLCampaignRequestApprove(UserInfo user, CampaignRequestApproveViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_CAMPAIGN_REQUEST_APPROVE");
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "CAMPAIGN_REQUEST_ID", DbType.Int32, MakeDbNull(model.CampaignRequestId));
                db.AddInParameter(cmd, "SUPPLIER_TYPE_ID", DbType.Int32, model.SupplierTypeId);
                db.AddInParameter(cmd, "SUPPLIER_DEALER_ID", DbType.Int32, MakeDbNull(model.SupplierDealerId));
                db.AddInParameter(cmd, "REQUEST_NOTE", DbType.String, MakeDbNull(model.RequestNote));
                db.AddInParameter(cmd, "REQUEST_STATUS", DbType.Int32, MakeDbNull(model.RequestStatusId));

                db.AddInParameter(cmd, "REJECTION_NOTE", DbType.String,MakeDbNull(model.RejectionNote));
                db.AddInParameter(cmd, "VIN_CODES", DbType.String, MakeDbNull(model.VinCodes));
               
                db.AddInParameter(cmd, "QUANTITY", DbType.Decimal, MakeDbNull(model.Quantity));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String,
                                  MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32,
                                  MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.CampaignRequestId = db.GetParameterValue(cmd, "CAMPAIGN_REQUEST_ID").GetValue<int>();
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

        public CampaignRequestApproveViewModel GetCampaignRequestApprove(UserInfo user,CampaignRequestApproveViewModel filter)
        {
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_CAMPAIGN_REQUEST_APPROVE");
                db.AddInParameter(cmd, "CAMPAIGN_REQUEST_ID", DbType.Int32, MakeDbNull(filter.CampaignRequestId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.CampaignCode = dReader["CAMPAIGN_CODE"].GetValue<string>();
                    filter.CampaignName = dReader["CAMPAIGN_NAME"].GetValue<string>();
                    filter.CampaignRequestId = dReader["CAMPAIGN_REQUEST_ID"].GetValue<int>();
                    filter.ModelKod = dReader["MODEL_KOD"].GetValue<string>();
                    filter.Quantity = dReader["QUANTITY"].GetValue<decimal>();
                    filter.RequestDealerId = dReader["REQUEST_DEALER_ID"].GetValue<int?>();
                    filter.RequestDealerName = dReader["REQUEST_DEALER_NAME"].GetValue<string>();
                    filter.RequestNote = dReader["REQUEST_NOTE"].GetValue<string>();
                    filter.RequestStatusId = dReader["REQUEST_STATUS_ID"].GetValue<int>();
                    filter.RequestStatusName = dReader["REQUEST_STATUS_NAME"].GetValue<string>();
                    filter.SupplierDealerId = dReader["SUPPLIER_DEALER_ID"].GetValue<int?>();
                    filter.SupplierDealerName = dReader["SUPPLIER_DEALER_NAME"].GetValue<string>();
                    filter.SupplierTypeId = dReader["SUPPLIER_TYPE_ID"].GetValue<int>();
                    filter.SupplierTypeName = dReader["SUPPLIER_TYPE_NAME"].GetValue<string>();
                    filter.IsWorkOrderRelated = dReader["IS_WORK_ORDER_RELATED"].GetValue<bool>();
                    filter.RejectionNote = dReader["REJECTION_NOTE"].GetValue<string>();
                    filter.VinCodes = dReader["VIN_CODES"].GetValue<string>();
                    filter.UpdateDate = dReader["LAST_PROCESS_DATE"].GetValue<DateTime>();
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


        public List<CampaignRequestApproveJsonModel> ListCampaignRequestVinApprovedCounts(UserInfo user, CampaignRequestApproveJsonModel filter, out int totalCount)
        {
            var retVal = new List<CampaignRequestApproveJsonModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CAMPAIGN_REQUEST_VIN_APPROVE_COUNTS");
                db.AddInParameter(cmd, "ID_CAMPAIGN_REQUEST", DbType.Int32, MakeDbNull(filter.CampaignRequestId));
                db.AddInParameter(cmd, "VIN_CODES", DbType.String, MakeDbNull(filter.VinCodes));
                
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 400);
                //AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var campaignRequestListModel = new CampaignRequestApproveJsonModel
                        {
                            CampaignRequestId = filter.CampaignRequestId,
                            VinCode = reader["VIN_CODE"].GetValue<string>(),
                            Count = reader["COUNT"].GetValue<int>()
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

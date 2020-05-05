using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.Guarantee;

namespace ODMSData
{
    public class GuaranteeData : DataAccessBase
    {
        public List<GuaranteeXMLModel> GetGuarantee(UserInfo user)
        {
            var listModel = new List<GuaranteeXMLModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_GUARANTEE_GIF_XML");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        listModel.Add(new GuaranteeXMLModel()
                        {
                            BreakdownDate = dr["BREAKDOWN_DATE"].GetValue<DateTime?>(),
                            CampaignCode = dr["CAMPAIGN_CODE"].GetValue<string>(),
                            CategoryLookval = dr["CATEGORY_LOOKVAL"].GetValue<string>(),
                            CurrencyCode = dr["CURRENCY_CODE"].GetValue<string>(),
                            CustomerAddress = dr["CRM_CUSTOMER_ADDRESS"].GetValue<string>(),
                            CustomerFax = dr["CRM_CUSTOMER_FAX"].GetValue<string>(),
                            CustomerMobile = dr["CRM_CUSTOMER_MOBILE_PHONE"].GetValue<string>(),
                            CustomerName = dr["CRM_CUSTOMER_CUSTOMER_NAME"].GetValue<string>() + " " + dr["CRM_CUSTOMER_CUSTOMER_LAST_NAME"].GetValue<string>(),
                            CustomerPhone = dr["CRM_CUSTOMER_PHONE"].GetValue<string>(),
                            DealerSSId = dr["DEALER_SSID"].GetValue<string>(),
                            FauilureCode = dr["CODE"].GetValue<string>(),
                            GuaranteeId = dr["ID_GUARANTEE"].GetValue<string>(),
                            GuaranteeSeq = dr["GUARANTEE_SEQ"].GetValue<string>(),
                            LabourTotalPrice = dr["LABOUR_TOTAL_PRICE"].GetValue<decimal>().ToDotedString(),
                            PartsTotalPrice = dr["PARTS_TOTAL_PRICE"].GetValue<decimal>().ToDotedString(),
                            PdiGif = dr["PDI_GIF"].GetValue<string>(),
                            RequestDesc = dr["REQUEST_DESC"].GetValue<string>(),
                            RequestDesc2 = dr["REQUEST2_DESC"].GetValue<string>(),
                            RequestUser = dr["REQUEST_USER"].GetValue<string>(),
                            ApproveUser = dr["APPROVE_USER"].GetValue<string>(),
                            RequestWarrantyStatus = dr["REQUEST_WARRANTY_STATUS"].GetValue<string>(),
                            VehicleEnteryDate = dr["CREATE_DATE"].GetValue<DateTime?>(),
                            VehicleKm = dr["VEHICLE_KM"].GetValue<string>(),
                            VehicleLeaveDate = dr["VEHICLE_LEAVE_DATE"].GetValue<DateTime?>(),
                            VehicleNotes = dr["VEHICLE_NOTES"].GetValue<string>(),
                            VehiclePlate = dr["VEHICLE_PLATE"].GetValue<string>(),
                            VehicleVinNo = dr["VEHICLE_VIN_NO"].GetValue<string>(),
                            WorkOrderId = dr["ID_WORK_ORDER"].GetValue<string>(),
                            ProcessTypeName = dr["PROCESS_TYPE_NAME"].GetValue<string>(),
                            IndicatorTypeName = dr["INDICATOR_TYPE_NAME"].GetValue<string>()
                        });
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
            return listModel;
        }

        public string GetGuaranteeParts(string guaranteeId, string guaranteeSeq)
        {
            string rValue = string.Empty;
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_GUARANTEE_PARTS_GIF_XML");
                db.AddInParameter(cmd, "GUARANTEE_ID", DbType.Int64, MakeDbNull(Convert.ToInt64(guaranteeId)));
                db.AddInParameter(cmd, "GUARANTEE_SEQ", DbType.String, MakeDbNull(guaranteeSeq));

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        rValue += ";" + dr["PARTS_ITEMS"].GetValue<string>();
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

            return rValue.Length > 0 ? rValue.Substring(1) : rValue;

        }
        public string GetGuaranteeLabours(string guaranteeId, string guaranteeSeq)
        {
            string rValue = string.Empty;
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_GUARANTEE_LABOURS_GIF_XML");
                db.AddInParameter(cmd, "GUARANTEE_ID", DbType.Int64, MakeDbNull(Convert.ToInt64(guaranteeId)));
                db.AddInParameter(cmd, "GUARANTEE_SEQ", DbType.String, MakeDbNull(guaranteeSeq));

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        rValue += ";" + dr["LABOUR_ITEMS"].GetValue<string>();
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

            return rValue.Length > 0 ? rValue.Substring(1) : rValue;

        }
        public string GetGuaranteeExternalLabours(string guaranteeId, string guaranteeSeq)
        {
            string rValue = string.Empty;
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_GUARANTEE_EXTERNAL_LABOURS_GIF_XML");
                db.AddInParameter(cmd, "GUARANTEE_ID", DbType.Int64, MakeDbNull(Convert.ToInt64(guaranteeId)));
                db.AddInParameter(cmd, "GUARANTEE_SEQ", DbType.String, MakeDbNull(guaranteeSeq));

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        rValue += ";" + dr["EXTERNAL_LABOUR_ITEMS"].GetValue<string>();
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

            return rValue.Length > 0 ? rValue.Substring(1) : rValue;

        }

        public void SetSuccessResultGif(string guaranteeId, string guaranteeSeq, long gifNo)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_GUARANTEE_GIF_XML");
                db.AddInParameter(cmd, "GUARANTEE_ID", DbType.Int64, MakeDbNull(Convert.ToInt64(guaranteeId)));
                db.AddInParameter(cmd, "GUARANTEE_SEQ", DbType.Int32, MakeDbNull(Convert.ToInt32(guaranteeSeq)));
                db.AddInParameter(cmd, "GUARANTEE_SSID", DbType.Int64, MakeDbNull(gifNo));

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

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

        public string GetGuaranteeCampaignSsidCsv()
        {
            string rValue = string.Empty;
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_GUARANTEE_CAMPAIGN_SSID_XML");

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        rValue += dr[0].GetValue<string>();
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

            return rValue;
        }

        public void UpdateCampaignPriceXml(DataTable model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_UPDATE_GUARANTEE_CAMPAIGN_PRICE_XML");
                cmd.Parameters.Add(new SqlParameter() { SqlDbType = SqlDbType.Structured, ParameterName = "UDT_GUARANTEE_CAMPAIGN_PRICE", Value = MakeDbNull(model) });
                //db.AddInParameter(cmd, "UDT_GUARANTEE_CAMPAIGN_PRICE", SqlDbType.Structured, MakeDbNull(model));
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
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

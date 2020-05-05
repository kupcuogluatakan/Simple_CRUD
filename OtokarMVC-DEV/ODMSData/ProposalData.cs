using Microsoft.Practices.EnterpriseLibrary.Data;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel;
using ODMSModel.Proposal;
using ODMSModel.ProposalCard;
using ODMSModel.VehicleBodyWorkProposal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ODMSData
{
    public class ProposalData : DataAccessBase
    {
        public List<ProposalListModel> ListProposal(UserInfo user,ProposalListModel filter, out int total)
        {
            var result = new List<ProposalListModel>();
            total = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PROPOSAL");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(filter.DealerId));
                db.AddInParameter(cmd, "CUSTOMER_NAME", DbType.String, MakeDbNull(filter.CustomerName));
                db.AddInParameter(cmd, "VEHICLE_PLATE", DbType.String, MakeDbNull(filter.VehiclePlate));
                db.AddInParameter(cmd, "ID_PROPOSAL_STAT", DbType.Int32, MakeDbNull(filter.ProposalStatusId));
                db.AddInParameter(cmd, "PROPOSAL_ID",DbType.Int32,MakeDbNull(filter.ProposalId));
                db.AddInParameter(cmd, "VIN_NO", DbType.String, MakeDbNull(filter.VinNo));
                db.AddInParameter(cmd, "VEHICLE_TYPE_ID", DbType.String, MakeDbNull(filter.VehicleType));
                db.AddInParameter(cmd, "START_DATE", DbType.DateTime, filter.StartDate);
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, filter.EndDate);
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new ProposalListModel
                        {
                            ProposalId = reader["ID_PROPOSAL"].GetValue<long>(),
                            ProposalSeq = reader["PROPOSAL_SEQ"].GetValue<int>(),
                            DealerName = reader["DEALER_NAME"].ToString(),
                            CustomerName = reader["CUSTOMER_NAME"].ToString(),
                            VehiclePlate = reader["PLATE"].ToString(),
                            ProposalDate = reader["PROPOSAL_DATE"].GetValue<DateTime>(),
                            ProposalStatus = reader["STATUS_DESC"].ToString(),
                            EngineNo = reader["ENGINE_NO"].ToString(),
                            VehicleId = reader["ID_VEHICLE"].GetValue<Int32>(),
                            VinNo = reader["VIN_NO"].ToString(),
                            VehicleCode = reader["V_CODE_KOD"].ToString(),
                            VehicleModel = reader["VEHICLE_MODEL"].ToString(),
                            VehicleType = reader["VEHICLE_TYPE"].ToString(),
                            WarrantyStartDate = reader["WARRANTY_START_DATE"].GetValue<DateTime>(),
                            WarrantyEndDate = reader["WARRANTY_END_DATE"].GetValue<DateTime>(),
                            WarrantyEndKilometer = reader["WARRANTY_END_KM"].ToString(),
                            DealerSSID = reader["DEALER_SSID"].ToString(),
                            IsConvert = reader["IS_CONVERT"].GetValue<bool>(),
                            IsConvertText = reader["IS_CONVERT_TEXT"].GetValue<string>(),
                            WorkOrderId = reader["ID_WORK_ORDER"].GetValue<string>(),
                            ProposalStatusId = reader["ID_PROPOSAL_STAT"].GetValue<string>(),
                            SparePartSaleId = reader["ID_SPS"].GetValue<long>()
                        };
                        result.Add(listModel);
                    }
                    reader.Close();
                }
                total = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
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

        public ProposalViewModel ConvertToWorkOrder(UserInfo user,ProposalViewModel filter)
        {
            long workOrderId;
            bool result = false;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_CONVERT_PROPOSAL_CARD_TO_WO_CARD");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.Int32, filter.ProposalId);
                //Yeni AÇkılan Telif Seq 0 olması istendiği için değişitrildi. db.AddInParameter(cmd, "PROPOSAL_SEQ", DbType.Int32, MakeDbNull(filter.ProposalSeq));
                db.AddInParameter(cmd, "PROPOSAL_SEQ", DbType.Int32, filter.ProposalSeq);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                db.AddOutParameter(cmd, "ID_WORK_ORDER", DbType.Int64, 200);
                CreateConnection(cmd);

                cmd.ExecuteNonQuery();
                filter.WorkOrderId = db.GetParameterValue(cmd, "ID_WORK_ORDER").GetValue<long>();
                filter.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                if (filter.ErrorNo > 0)
                    filter.ErrorMessage = ResolveDatabaseErrorXml(db.GetParameterValue(cmd, "ERROR_DESC").ToString());

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return filter;
        }

        public List<SelectListItem> ListProposalStats(UserInfo user)
        {
            List<SelectListItem> list;
            try
            {
                CreateDatabase();
                var rowMapper = MapBuilder<SelectListItem>.BuildAllProperties();
                list = db.ExecuteSprocAccessor("P_LIST_PROPOSAL_STAT_COMBO", rowMapper, MakeDbNull(user.LanguageCode)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return list;
        }

        public void DMLProposal(UserInfo user,ProposalViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_PROPOSAL");
                cmd.CommandTimeout = 120;
                db.AddParameter(cmd, "ID_PROPOSAL", DbType.Int32, ParameterDirection.InputOutput, "ID_PROPOSAL", DataRowVersion.Default, model.ProposalId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, MakeDbNull(model.CommandType));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(model.DealerId));
                db.AddInParameter(cmd, "ID_CUSTOMER", DbType.Int32, MakeDbNull(model.CustomerId));
                db.AddInParameter(cmd, "ID_VEHICLE", DbType.Int32, MakeDbNull(model.VehicleId));
                db.AddInParameter(cmd, "ID_APPOINTMENT", DbType.Int32, MakeDbNull(model.AppointmentId));
                db.AddInParameter(cmd, "APPOINTMENT_TYPE_ID", DbType.Int32, MakeDbNull(model.AppointmentTypeId));
                db.AddInParameter(cmd, "RESP_CONSULTANT", DbType.String, MakeDbNull(model.Stuff));
                db.AddInParameter(cmd, "TX_NOTE", DbType.String, MakeDbNull(model.Note));
                db.AddInParameter(cmd, "ID_FLEET", DbType.Int32, MakeDbNull(model.FleetId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ProposalId = db.GetParameterValue(cmd, "ID_PROPOSAL").GetValue<int>();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(db.GetParameterValue(cmd, "ERROR_DESC").ToString());
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

        public ProposalCardModel GetProposalCard(UserInfo user,long ProposalId, int seq)
        {
            var model = new ProposalCardModel();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_PROPOSAL_CARD");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.Int64, MakeDbNull(ProposalId));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int64, MakeDbNull(user.GetUserDealerId()));
                //İlk açılan teklif seq 0 olması istendiği için değiştirildi db.AddInParameter(cmd, "PROPOSAL_SEQ", DbType.Int32, MakeDbNull(seq));
                db.AddInParameter(cmd, "PROPOSAL_SEQ", DbType.Int32, seq);
                db.AddInParameter(cmd, "IS_HQ", DbType.Boolean, !user.IsDealer);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));

                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Proposal data
                        model.ProposalId = ProposalId;
                        model.ProposalDate = reader["PROPOSAL_DATE"].GetValue<DateTime>();
                        model.AppointmentId = reader["APPOINTMENT_ID"].GetValue<long>();
                        model.AppointmentDate = reader["APPOINTMENT_DATE"].GetValue<DateTime?>() == default(DateTime)
                            ? null
                            : reader["APPOINTMENT_DATE"].GetValue<DateTime?>();
                        model.AppointmentType = reader["APPOINTMENT_TYPE_NAME"].ToString();
                        model.DealerId = reader["ID_DEALER"].GetValue<int>();
                        model.ContactInfo = reader["CONTACT_INFO"].ToString();
                        model.ProposalStatId = reader["ID_PROPOSAL_STAT"].GetValue<int>();
                        model.ProposalSeq = reader["PROPOSAL_SEQ"].GetValue<int>();
                        model.ProposalStat = reader["STATUS_DESC"].ToString();
                        model.ProposalStatManualChangeAllow = reader["MANUEL_CHANGE_ALLOW"].GetValue<bool>();
                        model.IsPdiOwner = reader["IS_PDI_OWNER"].GetValue<bool>();
                        model.FleetId = reader["FLEET_ID"].GetValue<int?>();
                        model.FleetName = reader["FLEET_NAME"].ToString();
                        model.IsPartConstricted = reader["IS_PART_CONSTRICTED"].GetValue<bool?>();
                        model.OtokarPartDiscountRate = reader["OTOKAR_PART_DISCOUNT_RATE"].GetValue<decimal>();
                        model.OtokarLabourDiscountRate = reader["OTOKAR_LABOUR_DISCOUNT_RATE"].GetValue<decimal>();
                        model.DealerPartDiscountRate = reader["DEALER_PART_DISCOUNT_RATE"].GetValue<decimal>();
                        model.DealerLabourDiscountRate = reader["DEALER_LABOUR_DISCOUNT_RATE"].GetValue<decimal>();
                        model.VehicleLeaveDate = reader["VEHICLE_LEAVE_DATE"].GetValue<DateTime?>();
                        model.CustomerNote = reader["TX_NOTES"].ToString();
                        model.ApplicableFleetId = reader["APPLICABLE_FLEET_ID"].GetValue<int>();
                        model.DealerName = reader["DEALER_SHRT_NAME"].ToString();
                        model.IsCentralDealer = reader["IS_CENTRAL_DEALER"].GetValue<bool>();
                        model.IsConvert = reader["IS_CONVERT"].GetValue<bool>();
                        model.Matter1 = reader["Matter1"].GetValue<string>();
                        model.Matter2 = reader["Matter2"].GetValue<string>();
                        model.Matter3 = reader["Matter3"].GetValue<string>();
                        //model.Matter4 = reader["Matter4"].ToString();
                        model.TechnicalDesc = reader["TECHNICAL_DESC"].GetValue<string>();
                        model.ApprovedCount = reader["APPROVED_COUNT"].GetValue<int>();
                        model.WitholdingStatus = reader["WITHOLDING_STATUS"].GetValue<int?>();
                        model.WitholdingStatusName = reader["WITHOLDING_STATUS_NAME"].GetValue<string>();
                        model.WitholdingId = reader["ID_WITHOLDING"].GetValue<string>();
                        model.WitholdingName = reader["WITHOLDING_RATE"].GetValue<string>();
                        //customer data
                        var customer = new ProposalCustomerModel
                        {
                            CustomerId = reader["CUSTOMER_ID"].GetValue<int>(),
                            CustomerName = reader["CUSTOMER_NAME"].ToString(),
                            TcIdentityNo = reader["TC_IDENTITY_NO"].ToString(),
                            CustomerAddress = reader["ADDRESS_DESC"].ToString(),
                            TaxOffice = reader["TAX_OFFICE"].ToString(),
                            TaxNo = reader["TAX_NO"].ToString(),
                            Staff = reader["RESP_CONSULTANT"].ToString(),
                            CountryId = reader["ID_COUNTRY"].GetValue<int>(),

                        };
                        model.Customer = customer;
                        //vehicle data
                        var vehicle = new ProposalVehicleModel
                        {
                            VehicleId = reader["ID_VEHICLE"].GetValue<int>(),
                            VinNo = reader["VIN_NO"].ToString(),
                            EngineNo = reader["ENGINE_NO"].ToString(),
                            Plate = reader["Plate"].ToString(),
                            VehicleKilometer = reader["VEHICLE_KM"].GetValue<long?>(),
                            WarrantyStartDate = reader["WARRANTY_START_DATE"].GetValue<DateTime>(),
                            WarrantyEndDate = reader["WARRANTY_END_DATE"].GetValue<DateTime>(),
                            PaintWarrantyEndDate = reader["PAINT_WARRANTY_END_DATE"].GetValue<DateTime>(),
                            CorrosionWarrantyEndDate = reader["CORROSION_WARRANTY_END_DATE"].GetValue<DateTime>(),
                            WarrantyStatus = reader["WARRANTY_STAT"].GetValue<bool>(),
                            NoteCount = reader["VEHICLE_NOTE_COUNT"].GetValue<int>(),
                            DealerNoteCount = reader["DEALER_NOTE_COUNT"].GetValue<int>(),
                            PdiCheck = reader["PDI_CHCK"].GetValue<bool>(),
                            IsBodyWorkRequired = reader["BODYWORK_DETAIL_REQUIRED"].GetValue<bool>(),
                            IsPdiAccomplished = reader["IS_PDI_ACCOMPLISHED"].GetValue<bool>(),
                            VehicleCode = reader["VEHICLE_CODE"].ToString(),
                            VehicleModel = reader["MODEL_KOD"].ToString(),
                            VehicleType = reader["VEHICLE_TYPE"].ToString(),
                            Notes = reader["NOTES"].ToString(),
                            VehicleTypeId = reader["ID_VEHICLE_TYPE"].GetValue<int>(),
                            WarrantyEndKilometer = reader["WARRANTY_END_KM"].GetValue<long?>(),
                            VatExclude = reader["VAT_EXCLUDE"].GetValue<bool>(),
                            IsPdiApplicable = reader["IS_PDI_APPLICABLE"].GetValue<bool>(),
                            IsCampaignApplicable = reader["IS_CAMPAIGN_APPLICABLE"].GetValue<bool>(),
                            Location = reader["LOCATION"].ToString(),
                            ResponsiblePerson = reader["RESPONSIBLE_PERSON"].ToString(),
                            ResponsiblePersonPhone = reader["RESPONSIBLE_PERSON_PHONE"].ToString(),
                            IsHourMaint = reader["IS_HOUR_MAINT"].GetValue<bool>(),
                            VehicleHour = reader["VEHICLE_HOUR"].GetValue<int>(),
                            EngineType = reader["ENGINE_TYPE"].ToString(),
                            BodyWorkName = reader["BODYWORK_NAME"].ToString(),
                            ProposalCount = reader["ProposalCount"].GetValue<int>()
                        };

                        model.HasCampaign = reader["HAS_CAMPAIGN"].GetValue<bool>();
                        vehicle.SpecialConditions = reader["SPECIAL_CONDITIONS"].ToString();
                        model.DocumentCount = reader["DOCUMENT_COUNT"].GetValue<int>();
                        model.Vehicle = vehicle;
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

            return model;
        }

        public object GetProposalData(int id, string type)
        {
            dynamic obj = new { };
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_PROPOSAL_DATA");
                db.AddInParameter(cmd, "ID", DbType.Int32, id);
                db.AddInParameter(cmd, "TYPE", DbType.String, type);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        switch (type)
                        {
                            case "Appointment":
                                obj = new
                                {
                                    CustomerId = reader["CUSTOMER_ID"].ToString(),
                                    CustomerName = reader["CUSTOMER_NAME"].ToString(),
                                    CustomerSurName = reader["CUSTOMER_SURNAME"].ToString(),
                                    CustomerPhone = reader["CUSTOMER_PHONE"].ToString(),
                                    CustomerAddress = reader["CUSTOMER_ADDRESS"].ToString(),
                                    VehicleId = reader["VEHICLE_ID"].GetValue<int>(),
                                    VehicleModel = reader["MODEL_NAME"].ToString(),
                                    VehiclePlate = reader["PLATE"].ToString(),
                                    VehicleType = reader["TYPE_NAME"].ToString(),
                                    VehicleVin = reader["VIN_NO"].ToString(),
                                    Complaint = reader["COMPLAINT_DESC"].ToString(),
                                    AppointmentTypeId = reader["APPOINTMENT_TYPE_ID"].GetValue<int>()
                                };
                                break;
                            case "Customer":
                                obj = new
                                {
                                    CustomerId = reader["CUSTOMER_ID"].ToString(),
                                    CustomerName = reader["CUSTOMER_NAME"].ToString(),
                                    CustomerSurName = reader["CUSTOMER_SURNAME"].ToString(),
                                    CustomerPhone = reader["CUSTOMER_PHONE"].ToString(),
                                    CustomerAddress = reader["CUSTOMER_ADDRESS"].ToString()
                                };
                                break;
                            case "Vehicle":
                                obj = new
                                {
                                    VehicleModel = reader["MODEL_NAME"].ToString(),
                                    VehiclePlate = reader["PLATE"].ToString(),
                                    VehicleType = reader["TYPE_NAME"].ToString(),
                                    CustomerId = reader["CUSTOMER_ID"].ToString(),
                                    CustomerName = reader["CUSTOMER_NAME"].ToString(),
                                    CustomerSurName = reader["CUSTOMER_SURNAME"].ToString(),
                                    CustomerPhone = reader["CUSTOMER_PHONE"].ToString(),
                                    VehicleColor = reader["COLOR"].ToString(),
                                    VehicleVin = reader["VIN_NO"].ToString(),
                                    CustomerAddress = reader["CUSTOMER_ADDRESS"].ToString()
                                };
                                break;

                        }


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

            return obj;
        }

        public int CheckFleet(UserInfo user,int customerId, int vehicleId)
        {
            var applicableFleetId = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_PROPOSAL_APPLICABLE_FLEET_ID");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "ID_VEHICLE", DbType.Int32, MakeDbNull(vehicleId));
                db.AddInParameter(cmd, "ID_CUSTOMER", DbType.Int32, MakeDbNull(customerId));
                CreateConnection(cmd);

                cmd.ExecuteNonQuery();
                applicableFleetId = cmd.ExecuteScalar().GetValue<int>();
                cmd.Dispose();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return applicableFleetId;
        }
        public ModelBase CancelProposal(UserInfo user,long ProposalCardId)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_CANCEL_PROPOSAL_CARD",
                    ProposalCardId, MakeDbNull(user.UserId), null, null);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = int.Parse(db.GetParameterValue(cmd, "ERROR_NO").ToString());
                if (model.ErrorNo > 0)
                {
                    model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
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

            return model;
        }

        public void GetBodyworkFromVehicle(VehicleBodyworkViewModelProposal model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_BODYWORK_FROM_VEHICLE_PROPOSAL");

                db.AddInParameter(cmd, "VEHICLE_ID", DbType.Int32, MakeDbNull(model.VehicleId));

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        model.BodyworkCode = dr["BODYWORK_CODE"].GetValue<string>();
                        model.CityId = dr["ID_CITY"].GetValue<int>();
                        model.CountryId = dr["ID_COUNTRY"].GetValue<int>();
                        model.DealerId = dr["ID_DEALER"].GetValue<int>();
                        model.Manufacturer = dr["MANUFACTURER"].GetValue<string>();
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

        public ModelBase CheckForOtherCampaigns(long id)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_CHECK_PROPOSAL_CARD_CAMPAIGNS_FOR_DETAIL_ADDITION",
                    id, null, null);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = bool.Parse(db.GetParameterValue(cmd, "RESULT").ToString()) ? 1 : 0;
                if (model.ErrorNo == 0)
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = db.GetParameterValue(cmd, "REASON").ToString();
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
                }
                else
                    model.ErrorNo = 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return model;
        }
        public List<ProposalMaintenanceModel> GetMaintenance(UserInfo user,ProposalMaintenanceModel filter)
        {
            List<ProposalMaintenanceModel> returnList = new List<ProposalMaintenanceModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_PROPOSAL_PERIODIC_MAINT");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.Int64, MakeDbNull(filter.ProposalId));
                //Yeni AÇkılan Telif Seq 0 olması istendiği için değişitrildi. db.AddInParameter(cmd, "PROPOSAL_SEQ", DbType.Int32, MakeDbNull(filter.ProposalSeq));
                db.AddInParameter(cmd, "PROPOSAL_SEQ", DbType.Int32, filter.ProposalSeq);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        returnList.Add(new ProposalMaintenanceModel
                        {
                            MaintenanceKilometer = reader["MAINT_KM"].GetValue<int>(),
                            MaintenancName = reader["MAINT_NAME"].ToString(),
                            VehicleKilometer = reader["VEHICLE_KM"].GetValue<int>(),
                            MaintenanceId = reader["ID_MAINT"].GetValue<int>(),
                            ProposalId = filter.ProposalId,
                            ProposalDetailId = filter.ProposalDetailId,
                            ProposalSeq = filter.ProposalSeq
                        });

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

            return returnList;

        }

        public int GetVehicleCustomerId(int vehicleId)
        {
            var customerId = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_VEHICLE_CUSTOMER_ID");
                db.AddInParameter(cmd, "VEHICLE_ID", DbType.Int32, MakeDbNull(vehicleId));
                CreateConnection(cmd);

                cmd.ExecuteNonQuery();
                customerId = cmd.ExecuteScalar().GetValue<int>();
                cmd.Dispose();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return customerId;
        }

        public CustomerChangeModel GetCustomerChangeData(int customerId, int vehicleCustomerId)
        {
            CustomerChangeModel model = new CustomerChangeModel();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_PROPOSAL_CUSTOMER_CHANGE_DATA");
                db.AddInParameter(cmd, "CUSTOMER_ID", DbType.Int32, customerId);
                db.AddInParameter(cmd, "VEHICLE_CUSTOMER_ID", DbType.Int32, MakeDbNull(vehicleCustomerId));
                CreateConnection(cmd);

                cmd.ExecuteNonQuery();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.CustomerName = reader["CustomerName"].ToString();
                        model.VehicleCustomerName = reader["VehicleCustomerName"].ToString();

                    }

                    reader.Close();
                }
                //model.CustomerName = db.GetParameterValue(cmd, "CustomerName").GetValue<string>();
                //model.VehicleCustomerName = db.GetParameterValue(cmd, "VehicleCustomerName").GetValue<string>();
                model.CustomerId = customerId;
                model.VehicleCustomerId = vehicleCustomerId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return model;
        }
    }
}

using Microsoft.Practices.EnterpriseLibrary.Data;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel;
using ODMSModel.AppointmentDetails;
using ODMSModel.AppointmentDetailsLabours;
using ODMSModel.AppointmentDetailsParts;
using ODMSModel.Common;
using ODMSModel.ProposalCard;
using ODMSModel.WorkOrderCard;
using ODMSModel.WorkOrderCard.CampaignDetail;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ODMSData
{
    public class ProposalCardData : DataAccessBase
    {
        public ProposalCardModel GetProposalCard(UserInfo user,long ProposalId, int ProposalSeq)
        {
            var model = new ProposalCardModel();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_PROPOSAL_CARD");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.Int64, MakeDbNull(ProposalId));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int64, MakeDbNull(user.GetUserDealerId()));
                //İlk açılan teklif seq 0 olması istendiği için değiştirildi db.AddInParameter(cmd, "PROPOSAL_SEQ", DbType.Int32, MakeDbNull(ProposalSeq));
                db.AddInParameter(cmd, "PROPOSAL_SEQ", DbType.Int32, ProposalSeq);
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
                        //model.Matter4 = reader["Matter4"].ToString().Contains("müşteri")?"müşteri": reader["Matter4"].ToString().Contains("servis") ? "servis":"yarı yarıya";
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

            model.Details = GetProposalCardDetails(user,ProposalId, ProposalSeq);
            //detail list

            var detailList = model.Details.Where(c => c.Type == "INDICATOR").Select(x => new ProposalDetailList
            {
                ProposalDetailId = x.ProposalDetailId,
                IsBillable = x.IsBillable,
                InvoiceCancel = x.InvoiceCancel,
                Description = x.Description,
                TechicalDescription =
                        string.IsNullOrEmpty(x.Name)
                            ? MessageResource.WorkOrderDetails_Display_TechnicalDescription
                            : x.Name,
                DetailType = DetailType.Indicator,
                FailureCode = x.FailureCode,
                ProcessType = x.ProcessType,
                ProcessTypeCode = x.ProcessTypeCode,
                GuarantyAuthorizationNeed = x.GuarantyAuthorizationNeed,
                WarrantyStatus = x.WarrantyStatus,
                InvoiceId = x.InvoiceId,
                FailureCodeDescription = x.FailureCodeDescription,
                WarrantyStatusDesc = x.WarrantyStatusDesc,
                GosApprovalNeed = x.GosApprovalNeed,
                GuaranteeConfirmDesc = x.GuaranteeConfirmDesc,
                GosSendCheck = x.GosSendCheck,
                IndicatorType = GetIndicatorTypeFromIndicatorTypeCode(x.IndicatorTypeCode),
                AllowFailureCodeChange = x.AllowFailureCodeChange,
                GifNo = x.GifNo
            }).ToList();
            //maintenance adjustment
            detailList.ForEach(c =>
            {
                var meintenanceItem =
                    model.Details.FirstOrDefault(
                        d => d.ProposalDetailId == c.ProposalDetailId && d.MaintenanceId > 0);

                if (meintenanceItem != null)
                {
                    c.DetailType = DetailType.Meintenance;
                    c.Description = meintenanceItem.MaintenanceName;
                }
            });




            model.DetailList = detailList;
            return model;
        }
        public static IndicatorType GetIndicatorTypeFromIndicatorTypeCode(string indicatorTypeCode)
        {
            switch (indicatorTypeCode)
            {
                case "IT_A": return IndicatorType.BreakDown;
                case "IT_C": return IndicatorType.Campaign;
                case "IT_K": return IndicatorType.CouponMaint;
                case "IT_P": return IndicatorType.PeriodicMaint;
                case "IT_T": return IndicatorType.Pdi;
                case "IT_H": return IndicatorType.HourMaint;
                case "IT_O": return IndicatorType.BreakDown;

                default:
                    throw new InvalidEnumArgumentException(@"No such Indicator Type for the string indicatorType=>" +
                                                           indicatorTypeCode);
            }
        }
        public List<ProposalDetailModel> GetProposalCardDetails(UserInfo user,long ProposalId, int seq)
        {
            var model = new List<ProposalDetailModel>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PROPOSAL_CARD_DETAILS");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.Int64, MakeDbNull(ProposalId));
                //İlk açılan teklif seq 0 olması istendiği için değiştirildi db.AddInParameter(cmd, "PROPOSAL_SEQ", DbType.Int32, MakeDbNull(seq));
                db.AddInParameter(cmd, "PROPOSAL_SEQ", DbType.Int32, seq);
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "IS_HQ", DbType.Boolean, !user.IsDealer);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));

                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new ProposalDetailModel
                        {
                            Id = reader["ID"].GetValue<long>(),
                            Code = reader["CODE"].ToString(),
                            CurrencyCode = reader["CURRENCY_CODE"].ToString(),
                            Description = reader["DESCRIPTION"].ToString(),
                            DiscountRatio = reader["DISCOUNT_RATIO"].GetValue<decimal>(),
                            ProposalDetailId = reader["ID_PROPOSAL_DETAIL"].GetValue<int>(),
                            MaintenanceId = reader["ID_MAINT"].GetValue<int>(),
                            MaintenanceName = reader["MAINT_NAME"].ToString(),
                            Name = reader["NAME"].ToString(),
                            Price = reader["LIST_PRICE"].GetValue<decimal>(),
                            Quantity = reader["QUANTITY"].GetValue<decimal>(),
                            Type = reader["TYPE"].ToString(),
                            VatRatio = reader["VAT_RATIO"].GetValue<decimal>(),
                            WarrantyRatio = reader["WARRANTY_RATIO"].GetValue<decimal?>(),
                            IsCampaign = reader["IS_CAMPAIGN"].GetValue<bool>(),
                            IsBillable = reader["IS_BILLABLE"].GetValue<bool>(),
                            InvoiceCancel = reader["INVOICE_CANCEL"].GetValue<bool>(),
                            FailureCode = reader["FAILURE_CODE"].ToString(),
                            RequiredQuantity = reader["REQUIRED_QUANTITY"].GetValue<decimal>(),
                            ReservedQuantity = reader["RESERVED_QUANTITY"].GetValue<decimal>(),
                            PickedQuantity = reader["PICKED_QUANTITY"].GetValue<decimal>(),
                            ReturnedQuantity = reader["RETURNED_QUANTITY"].GetValue<decimal>(),
                            Duration = reader["DURATION"].GetValue<decimal>(),
                            HasOtokarCampaignStock = reader["HAS_OTOKAR_CAMPAIGN_STOCK"].GetValue<bool>(),
                            CampaignRequestId = reader["ID_CAMPAIGN_REQUEST"].GetValue<long?>(),
                            ProcessType = reader["PROCESS_TYPE"].ToString(),
                            ProcessTypeCode = reader["PROCESS_TYPE_CODE"].ToString(),
                            GuarantyAuthorizationNeed = reader["GUARANTY_AUTHORIZATION_NEED"].GetValue<bool>(),
                            WarrantyStatus = reader["WARRANTY_STATUS"].GetValue<int>(),
                            InvoiceId = reader["INVOICE_ID"].GetValue<long?>(),
                            DiscountPrice = reader["DISCOUNT_PRICE"].GetValue<decimal>(),
                            WarrantyPrice = reader["WARRANTY_PRICE"].GetValue<decimal>(),
                            IsFleetDiscountApplied = reader["IS_FLEET_DISCOUNT_APPLIED"].GetValue<bool>(),
                            IsExternalLabour = reader["IS_EXTERNAL_LABOUR"].GetValue<bool>(),
                            ProfitMarginRatio = reader["PROFIT_MARGIN_RATIO"].GetValue<decimal?>(),
                            DealerPrice = reader["DEALER_PRICE"].GetValue<decimal?>(),
                            FailureCodeDescription = reader["FAILURE_CODE_DESC"].ToString(),
                            IsMust = reader["IS_MUST"].GetValue<bool>(),
                            WarrantyStatusDesc = reader["WARRANTY_STATUS_DESC"].ToString(),
                            GosApprovalNeed = reader["GOS_APPROVAL_NEED"].GetValue<bool>(),
                            GuaranteeConfirmDesc = reader["GUARANTEE_CONFIRM_DESC"].ToString(),
                            GosSendCheck = reader["GOS_SEND_CHECK"].GetValue<bool>(),
                            IndicatorTypeCode = reader["INDICATOR_TYPE_CODE"].ToString(),
                            LabourWorkStatusId = reader["LABOUR_WORK_STATUS_VAL"].GetValue<sbyte>(),
                            LabourWorkStatus = reader["LABOUR_WORK_STATUS"].ToString(),
                            LabourTechnician = reader["LABOUR_TECHNICIAN"].ToString(),
                            LabourStartDate = reader["LABOUR_START_DATE"].GetValue<DateTime>(),
                            LabourFinishDate = reader["LABOUR_FINISH_DATE"].GetValue<DateTime>(),
                            StockType = reader["STOCK_TYPE"].ToString(),
                            AllowFailureCodeChange = reader["FAILURE_CODE_CHANGE"].GetValue<bool>(),
                            GifNo = reader["GIF_NO"].GetValue<long?>(),
                            WarrantyTotal = reader["WARRANTY_TOTAL"].GetValue<decimal?>(),
                    };
                        model.Add(item);

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
            //total Price   
            model.ForEach(c =>
            {
                if (c.Type != "INDICATOR")
                {
                    var quantity = c.Type == "PART" ? c.RequiredQuantity : ((c.Duration / 100) * c.Quantity);

                    //Garanti işlemi ,Yetki onaylanmış ,Garantisi onaylanmış 
                    if (c.WarrantyWillBeApproved && (c.WarrantyStatus == 2 || c.WarrantyStatus == 5))
                    {
                        //Toplam müşteri tutarı 
                        //%100 onaylandı ise garanti fiyatı baz alınacak
                        if (c.WarrantyRatio.HasValue && c.WarrantyRatio.Value == 100)
                        {
                            c.TotalCustomerPrice = quantity * (c.WarrantyPrice - (c.WarrantyPrice * (c.WarrantyRatio ?? 0) / 100));
                        }
                        //%100 onaylanmadı ise müşteriye yansıyacak tutar liste fiyatı üzerinden olacak
                        else
                        {
                            c.TotalCustomerPrice = quantity * (c.Price * (100 - (c.WarrantyRatio ?? 0)) / 100);
                        }
                    }
                    // Yeni Kayıt,Yetki Onayı Bekliyor,
                    else if (c.WarrantyWillBeApproved && (c.WarrantyStatus == 0 || c.WarrantyStatus == 1 || c.WarrantyStatus == 4))
                    {
                        //Toplam müşteri tutarı 
                        c.TotalCustomerPrice = 0;
                    }
                    //Garantisi onaylanmamış,red edilmiş yada beklemede ise
                    else
                    {
                        //Toplam müşteri tutarı 
                        c.TotalCustomerPrice = quantity * c.Price;
                    }

                    //Toplam iskonto tutarı
                    c.TotalDiscount = (c.DiscountRatio > 0 ? (c.TotalCustomerPrice * c.DiscountRatio / 100) : 0);
                    //İskontolu toplam tutar
                    c.TotalPrice = decimal.Round(c.TotalCustomerPrice - c.TotalDiscount, 2);

                    //Garanti işlemi , Yeni Kayıt,Yetki Onayı Bekliyor,Yetki Onaylandı,Onay Bekliyor,Onayladı
                    if (c.WarrantyWillBeApproved && c.WarrantyStatus == 0 || c.WarrantyStatus == 1 || c.WarrantyStatus == 2 || c.WarrantyStatus == 4 || c.WarrantyStatus == 5)
                    {
                        c.TotalWarrantyPriceWithoutDeduction = quantity * c.WarrantyPrice;
                        c.TotalWarrantyDeductionPrice = quantity * (c.WarrantyPrice - (c.WarrantyPrice * (c.WarrantyRatio ?? 100) / 100));
                        c.TotalWarrantyPrice = decimal.Round(c.TotalWarrantyPriceWithoutDeduction - c.TotalWarrantyDeductionPrice, 2);
                    }
                }
            });

            return model;
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
        public List<CampaignCheckModel> GetCampaignCheckList(UserInfo user,long proposalId)
        {
            var list = new List<CampaignCheckModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_CHECK_PROPOSAL_CARD_MANDATORY_CAMPAIGNS");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.Int64, MakeDbNull(proposalId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 400);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new CampaignCheckModel
                        {
                            CampaignCode = reader["CAMPAIGN_CODE"].ToString(),
                            Description = reader["CAMPAIGN_NAME"].ToString(),
                            IsMust = reader["RESULT"].GetValue<bool>(),
                            HasStock = reader["HAS_STOCK"].GetValue<bool>(),
                            TotalLabourDuration = reader["TOTAL_LABOUR_DURATION"].GetValue<decimal>()
                        };
                        list.Add(item);
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

            return list;
        }
        public bool CheckProposalDealer(long proposalId, int dealerId, long invoiceid = 0)
        {
            bool result;
            try
            {
                CreateDatabase();
                CreateConnection();
                result = base.ExecSqlFunction<bool>("FN_PROPOSAL_DEALER_CHECK", proposalId, 0, dealerId, invoiceid);
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
        public List<ComboBoxModel> ListFailureCodes(UserInfo user)
        {
            var list = new List<ComboBoxModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_FAILURE_CODES_FOR_PROPOSAL_CARD_COMBO");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new ComboBoxModel
                        {
                            Value = reader["ID_APPOINTMENT_INDICATOR_FAILURE_CODE"].GetValue<int>(),
                            Text = reader["CODE"].ToString() + " - " + reader["DESCRIPTION"].ToString(),
                            Description = reader["DESCRIPTION"].ToString()
                        };
                        list.Add(item);
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

            return list;
        }
        public List<SelectListItem> ListDetailProcessTypes(UserInfo user)
        {
            List<SelectListItem> list;
            try
            {
                CreateDatabase();
                var rowMapper = MapBuilder<SelectListItem>.BuildAllProperties();
                list = db.ExecuteSprocAccessor("P_LIST_PROPOSAL_INDICATOR_TYPES", rowMapper, MakeDbNull(user.LanguageCode)).ToList();

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
        public List<SelectListItem> ListDetailProcessTypes(UserInfo user,string indicatorTypeCode, long ProposalId, int seq)
        {
            List<SelectListItem> list;
            try
            {
                CreateDatabase();
                var rowMapper = MapBuilder<SelectListItem>.BuildAllProperties();
                list = db.ExecuteSprocAccessor("P_LIST_PROCESS_TYPE_BY_INDICATOR_TYPE_COMBO_PROPOSAL", rowMapper, MakeDbNull(ProposalId), MakeDbNull(indicatorTypeCode), MakeDbNull(user.LanguageCode), seq).ToList();

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
        public void AddProposalDetail(UserInfo user,AppointmentDetailsViewModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_ADD_PROPOSAL_DETAIL");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.Int64, MakeDbNull(model.AppointmentId));
                //İlk açılan teklif seq 0 olması istendiği için değiştirildi db.AddInParameter(cmd, "PROPOSAL_SEQ", DbType.Int32, MakeDbNull(model.ProposalSeq));
                db.AddInParameter(cmd, "PROPOSAL_SEQ", DbType.Int32, model.ProposalSeq);
                db.AddInParameter(cmd, "APPOINTMENT_INDICATOR_SUB_CATEGORY_ID", DbType.Int32, MakeDbNull(model.SubCategoryId));
                db.AddInParameter(cmd, "PROCESS_TYPE_CODE", DbType.String, MakeDbNull(model.ProcessTypeId));
                db.AddInParameter(cmd, "INDICATOR_TYPE_CODE", DbType.String, MakeDbNull(model.IndicatorTypeCode));
                db.AddInParameter(cmd, "ID_FAILURE_CODE", DbType.Int32, MakeDbNull(model.FailureCodeId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 400);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));

                CreateConnection(cmd);

                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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
        }
        public LabourDataModel GetLabourData(long labourId, int? vehicleId)
        {
            var model = new LabourDataModel();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_LABOUR_DATA_FOR_PROPOSAL_DETAIL");
                db.AddInParameter(cmd, "ID_LABOUR", DbType.Int64, MakeDbNull(labourId));
                db.AddInParameter(cmd, "ID_VEHICLE", DbType.Int32, MakeDbNull(vehicleId));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.Editable = reader["EDITABLE"].GetValue<bool>();
                        model.Duration = reader["DURATION"].GetValue<int>();
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
        public long GetLastLevelPartId(long partId)
        {
            long newPartId = 0;
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_GET_LAST_LEVEL_PART_ID",
                    partId);
                CreateConnection(cmd);
                newPartId = cmd.ExecuteScalar().GetValue<long>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return newPartId;
        }
        public void AddProposalPart(UserInfo user,AppointmentDetailsPartsViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_ADD_PROPOSAL_DETAIL_PART");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.Int64, MakeDbNull(model.Id));
                db.AddInParameter(cmd, "ID_PROPOSAL_DETAIL", DbType.Int32, MakeDbNull(model.AppointIndicId));
                db.AddInParameter(cmd, "ID_PART", DbType.Int64, MakeDbNull(model.PartId));
                db.AddInParameter(cmd, "QUANTITY", DbType.Decimal, MakeDbNull(model.Quantity));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 400);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));

                CreateConnection(cmd);

                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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
        }
        public bool? CheckPartLastLevel(long proposalDetailId, string csPartIds)
        {
            bool? result;
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_CHECK_PROPOSAL_CARD_PART_LAST_LEVEL",
                    proposalDetailId, csPartIds, null);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                result = db.GetParameterValue(cmd, "RESULT").GetValue<bool?>();

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
        public AppointmentDetailsViewModel GetDetailData(UserInfo user, long ProposalId, long subCategoryId)
        {
            var appointmentDetail = new AppointmentDetailsViewModel();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_PROPOSAL_CARD_DETAILS");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.Int64, MakeDbNull(ProposalId));
                db.AddInParameter(cmd, "APPOINTMENT_INDICATOR_SUB_CATEGORY_ID", DbType.Int64, MakeDbNull(subCategoryId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        //appointmentDetail.AppointmentId = reader["APPOINTMENT_INDICATOR_ID"].GetValue<int>();
                        //appointmentDetail.AppointmentIndicatorId = reader["APPOINTMENT_INDICATOR_ID"].GetValue<int>();
                        appointmentDetail.SubCategoryId = reader["APPOINTMENT_INDICATOR_SUB_CATEGORY_ID"].GetValue<int>();
                        appointmentDetail.CategoryId = reader["APPOINTMENT_INDICATOR_CATEGORY_ID"].GetValue<int>();
                        appointmentDetail.MainCategoryId = reader["APPOINTMENT_INDICATOR_MAIN_CATEGORY_ID"].GetValue<int>();
                        appointmentDetail.MainCategoryName = reader["MAIN_CATEGORY_NAME"].ToString();
                        appointmentDetail.CategoryName = reader["CATEGORY_NAME"].ToString();
                        appointmentDetail.SubCategoryName = reader["SUB_CATEGORY_NAME"].ToString();
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

            return appointmentDetail;
        }
        public void AddProposalLabour(UserInfo user, AppointmentDetailsLaboursViewModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_ADD_PROPOSAL_DETAIL_LABOUR");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.Int64, MakeDbNull(model.AppointmentId));
                db.AddInParameter(cmd, "ID_PROPOSAL_DETAIL", DbType.Int64, MakeDbNull(model.AppointmentIndicatorId));
                db.AddInParameter(cmd, "LABOUR_ID", DbType.Int32, MakeDbNull(model.LabourId));
                db.AddInParameter(cmd, "QUANTITY", DbType.Int32, MakeDbNull(model.Quantity));
                db.AddInParameter(cmd, "DURATION", DbType.Decimal, MakeDbNull(model.Duration));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 400);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));

                CreateConnection(cmd);

                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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
        }
        public void AddProposalMaint(UserInfo user, ProposalMaintenanceModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_ADD_PROPOSAL_DETAIL_MAINT");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.Int64, MakeDbNull(model.ProposalId));
                //İlk açılan teklif seq 0 olması istendiği için değiştirildi db.AddInParameter(cmd, "PROPOSAL_SEQ", DbType.Int32, MakeDbNull(model.ProposalSeq));
                db.AddInParameter(cmd, "PROPOSAL_SEQ", DbType.Int32, model.ProposalSeq);
                db.AddInParameter(cmd, "ID_PROPOSAL_DETAIL", DbType.Int64, MakeDbNull(model.ProposalDetailId));
                db.AddInParameter(cmd, "ID_MAINT", DbType.Int64, MakeDbNull(model.MaintenanceId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 400);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));

                CreateConnection(cmd);

                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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
        }
        public ProposalDiscountModel GetProposalDetailItemDataForDiscount(long ProposalId, long proposalDetailId, string type, long itemId)
        {
            var model = new ProposalDiscountModel();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_PROPOSAL_CARD_DETAIL_FOR_DISCOUNT");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.Int64, MakeDbNull(ProposalId));
                db.AddInParameter(cmd, "ID_PROPOSAL_DETAIL", DbType.Int64, MakeDbNull(proposalDetailId));
                db.AddInParameter(cmd, "TYPE", DbType.String, MakeDbNull(type));
                db.AddInParameter(cmd, "ITEM_ID", DbType.Int64, MakeDbNull(itemId));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.ProposalId = ProposalId;
                        model.ProposalDetailId = proposalDetailId;
                        model.Type = type;
                        model.ItemId = itemId;
                        model.DisableDiscount = reader["DISABLE_DISCOUNT"].GetValue<bool>();
                        model.TotalFleetDiscountRate = reader["TOTAL_FLEET_DISCOUNT_RATE"].GetValue<decimal>();
                        model.Duration = reader["DURATION"].GetValue<decimal>();
                        model.Quantity = reader["QUANTITY"].GetValue<decimal>();
                        model.ListPrice = reader["LIST_PRICE"].GetValue<decimal>();
                        model.DealerPrice = reader["DEALER_PRICE"].GetValue<decimal>();
                        model.DiscountRatio = reader["DISCOUNT_RATIO"].GetValue<decimal>();
                        model.DiscountPrice = reader["DISCOUNT_PRICE"].GetValue<decimal>();
                        model.WarrantyRatio = reader["WARRANTY_RATIO"].GetValue<decimal>();
                        model.VatRatio = reader["VAT_RATIO"].GetValue<decimal>();
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
        public ProposalQuantityDataModel GetQuantityData(UserInfo user, long ProposalId, long proposalDetailId, string type, long itemId)
        {
            var model = new ProposalQuantityDataModel();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_PROPOSAL_CARD_QUANTITY_DATA");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.Int64, MakeDbNull(ProposalId));
                db.AddInParameter(cmd, "ID_PROPOSAL_DETAIL", DbType.Int64, MakeDbNull(proposalDetailId));
                db.AddInParameter(cmd, "TYPE", DbType.String, MakeDbNull(type));
                db.AddInParameter(cmd, "ITEM_ID", DbType.Int64, MakeDbNull(itemId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.ProposalId = ProposalId;
                        model.Type = type;
                        model.ProposalDetailId = proposalDetailId;
                        model.ItemId = itemId;
                        model.Duration = reader["DURATION"].GetValue<decimal>();
                        model.Quantity = reader["QUANTITY"].GetValue<decimal>();
                        model.LabourDealerDurationCheck = reader["DEALER_DURATION_CHCK"].GetValue<bool>();
                        model.Name = reader["NAME"].ToString();
                        model.Code = reader["CODE"].ToString();
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
        public void ChangePriceList(UserInfo user, ProposalChangePriceListModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_CHANGE_PROPOSAL_CARD_DETAIL_PRICE_LIST");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.Int64, MakeDbNull(model.ProposalId));
                db.AddInParameter(cmd, "ID_PROPOSAL_DETAIL", DbType.Int64, MakeDbNull(model.ProposalDetailId));
                db.AddInParameter(cmd, "TYPE", DbType.String, MakeDbNull(model.Type));
                db.AddInParameter(cmd, "ITEM_ID", DbType.Int64, MakeDbNull(model.ItemId));
                db.AddInParameter(cmd, "PRICE_LIST_DATE", DbType.DateTime, MakeDbNull(model.PriceListDate));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 400);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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
        }
        public void UpdateQuantity(UserInfo user, ProposalQuantityDataModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_UPDATE_PROPOSAL_DETAIL_QUANTITY");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.Int64, MakeDbNull(model.ProposalId));
                db.AddInParameter(cmd, "ID_PROPOSAL_DETAIL", DbType.Int64, MakeDbNull(model.ProposalDetailId));
                db.AddInParameter(cmd, "QUANTITY", DbType.Decimal, MakeDbNull(model.Quantity));
                db.AddInParameter(cmd, "DURATION", DbType.Decimal, MakeDbNull(model.Duration));
                db.AddInParameter(cmd, "TYPE", DbType.String, MakeDbNull(model.Type));
                db.AddInParameter(cmd, "ITEM_ID", DbType.Int64, MakeDbNull(model.ItemId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 400);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));

                CreateConnection(cmd);

                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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
        }
        public void RemoveLabourOrPartFromMaintenance(UserInfo user, ProposalMaintenanceQuantityDataModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_REMOVE_PROPOSAL_CARD_MAINT_PART_LABOUR");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.Int64, MakeDbNull(model.ProposalId));
                db.AddInParameter(cmd, "ID_PROPOSAL_DETAIL", DbType.Int64, MakeDbNull(model.ProposalDetailId));
                db.AddInParameter(cmd, "ID_MAINT", DbType.Decimal, MakeDbNull(model.MaintenanceId));
                db.AddInParameter(cmd, "TYPE", DbType.String, MakeDbNull(model.Type));
                db.AddInParameter(cmd, "ITEM_ID", DbType.Int64, MakeDbNull(model.ItemId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 400);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));

                CreateConnection(cmd);

                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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
        }
        public void ChangePart(UserInfo user, ProposalMaintenanceQuantityDataModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_PROPOSAL_DETAIL_CHANGE_PART");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.Int64, MakeDbNull(model.ProposalId));
                db.AddInParameter(cmd, "ID_PROPOSAL_DETAIL", DbType.Int64, MakeDbNull(model.ProposalDetailId));
                db.AddInParameter(cmd, "ID_MAINT", DbType.Int32, MakeDbNull(model.MaintenanceId));
                db.AddInParameter(cmd, "ITEM_ID", DbType.Int64, MakeDbNull(model.ItemId));
                db.AddInParameter(cmd, "NEW_PART_ID", DbType.Int64, MakeDbNull(model.NewPartId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 400);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));

                CreateConnection(cmd);

                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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
        }

        //Emre - Teklif iptali 21.05.2019
        public void CancelProposal(UserInfo user, ProposalCancelModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_CANCEL_PROPOSAL");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.Int64, MakeDbNull(model.ProposalId));
                db.AddInParameter(cmd, "PROPOSAL_SEQ", DbType.Byte, MakeDbNull(model.ProposalSeq));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddInParameter(cmd, "CANCEL_REASON", DbType.String, MakeDbNull(model.CancelReason));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 400);

                CreateConnection(cmd);

                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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
        }

        public void CancelDetail(UserInfo user, ProposalDetailCancelModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_CANCEL_PROPOSAL_DETAIL");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.Int64, MakeDbNull(model.ProposalId));
                //İlk açılan teklif seq 0 olması istendiği için değiştirildi db.AddInParameter(cmd, "PROPOSAL_SEQ", DbType.Int32, MakeDbNull(model.ProposalSeq));
                db.AddInParameter(cmd, "PROPOSAL_SEQ", DbType.Int32, model.ProposalSeq);
                db.AddInParameter(cmd, "ID_PROPOSAL_DETAIL", DbType.Int64, MakeDbNull(model.ProposalDetailId));
                db.AddInParameter(cmd, "CANCEL_REASON", DbType.String, MakeDbNull(model.CancelReason));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 400);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));

                CreateConnection(cmd);

                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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
        }
        public void AddDiscount(UserInfo user, ProposalDiscountModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_UPDATE_PROPOSAL_DISCOUNT");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.Int64, MakeDbNull(model.ProposalId));
                db.AddInParameter(cmd, "ID_PROPOSAL_DETAIL", DbType.Int64,
                    MakeDbNull(model.ProposalDetailId));
                db.AddInParameter(cmd, "TYPE", DbType.String, MakeDbNull(model.Type));
                db.AddInParameter(cmd, "ITEM_ID", DbType.Int64, MakeDbNull(model.ItemId));
                db.AddInParameter(cmd, "DETAIL_TYPE", DbType.String, MakeDbNull(model.DiscountType.ToString().ToUpper()));
                db.AddInParameter(cmd, "DISCOUNT_PRICE", DbType.Decimal, MakeDbNull(model.DiscountPrice));
                db.AddInParameter(cmd, "DISCOUNT_RATIO", DbType.Decimal, MakeDbNull(model.DiscountRatio));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 400);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32,
                    MakeDbNull(user.UserId));

                CreateConnection(cmd);

                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            catch
            {
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.Err_Generic_Unexpected;
            }
            finally
            {

                CloseConnection();
            }
        }
        public long? UpdateVehicleKM(UserInfo user, long id, int seq, long km, bool isHourMaint, int hour, out int ErrorNo, out string ErrorMessage)
        {
            long? result;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_UPDATE_PROPOSAL_CARD_VEHICLE_KM");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.Int64, MakeDbNull(id));
                //İlk açılan teklif seq 0 olması istendiği için değiştirildi db.AddInParameter(cmd, "PROPOSAL_SEQ", DbType.Int32, MakeDbNull(seq));
                db.AddInParameter(cmd, "PROPOSAL_SEQ", DbType.Int32, seq);
                db.AddParameter(cmd, "VEHICLE_KM", DbType.Int64, ParameterDirection.InputOutput, "VEHICLE_KM", DataRowVersion.Default, MakeDbNull(km));
                db.AddParameter(cmd, "IS_HOUR_MAINT", DbType.Int32, ParameterDirection.Input, "IS_HOUR_MAINT", DataRowVersion.Default, isHourMaint);
                db.AddInParameter(cmd, "HOUR", DbType.Int32, MakeDbNull(hour));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 400);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));

                CreateConnection(cmd);

                cmd.ExecuteNonQuery();

                result = db.GetParameterValue(cmd, "VEHICLE_KM").GetValue<long?>();
                ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (ErrorNo > 0)
                    ErrorMessage = ResolveDatabaseErrorXml(ErrorMessage);
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
            return result;
        }
        public string UpdateVehiclePlate(UserInfo user, long id, string plate, out int ErrorNo, out string ErrorMessage)
        {
            string result;
            result = plate;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_UPDATE_PROPOSAL_CARD_VEHICLE_PLATE");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.Int64, MakeDbNull(id));
                db.AddInParameter(cmd, "VEHICLE_PLATE", DbType.String, MakeDbNull(plate));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 400);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));

                CreateConnection(cmd);

                cmd.ExecuteNonQuery();


                ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (ErrorNo > 0)
                    ErrorMessage = ResolveDatabaseErrorXml(ErrorMessage);
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
            return result;
        }
        public ProposalMaintenanceQuantityDataModel GetMaintenanceQuantityData(UserInfo user, long ProposalId, long proposalDetailId, string type, long itemId, int maintId)
        {
            var model = new ProposalMaintenanceQuantityDataModel();
            model.ProposalId = ProposalId;
            model.Type = type;
            model.ProposalDetailId = proposalDetailId;
            model.ItemId = itemId;
            model.MaintenanceId = maintId;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_PROPOSAL_CARD_MAINT_QUANTITY_DATA");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.Int64, MakeDbNull(ProposalId));
                db.AddInParameter(cmd, "ID_PROPOSAL_DETAIL", DbType.Int64, MakeDbNull(proposalDetailId));
                db.AddInParameter(cmd, "TYPE", DbType.String, MakeDbNull(type));
                db.AddInParameter(cmd, "ITEM_ID", DbType.Int64, MakeDbNull(itemId));
                db.AddInParameter(cmd, "ID_MAINT", DbType.Int32, MakeDbNull(maintId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.Quantity = reader["QUANTITY"].GetValue<decimal>();
                        model.LabourDealerDurationCheck = reader["DEALER_DURATION_CHCK"].GetValue<bool>();
                        model.Name = reader["NAME"].ToString();
                        model.IsMust = reader["IS_MUST"].GetValue<bool>();
                        model.Duration = reader["DURATION"].GetValue<decimal>();
                        model.AlternateAllowed = reader["ALTERNATE_ALLOW"].GetValue<bool>();
                        model.DiffrentBrandAllowed = reader["DIF_BRAND_ALLOW"].GetValue<bool>();
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
        public List<SelectListItem> ListAlternateParts(UserInfo user, long partId, int maintId, long proposalDetailId)
        {
            var list = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SPARE_PART_ALTERNATE_PARTS_COMBO_PROPOSAL");
                db.AddInParameter(cmd, "ID_PROPOSAL_DETAIL", DbType.Int64, MakeDbNull(proposalDetailId));
                db.AddInParameter(cmd, "ID_PART", DbType.Int64, MakeDbNull(partId));
                db.AddInParameter(cmd, "ID_MAINT", DbType.Int32, MakeDbNull(maintId));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new SelectListItem
                        {
                            Text = reader["PART_NAME"].ToString(),
                            Value = reader["ID_PART"].GetValue<long>().ToString(),
                            Selected = reader["IS_ORIGINAL"].GetValue<bool>()
                        };
                        list.Add(item);
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

            return list;
        }
        public ProposalCampaignModel GetCampaignData(UserInfo user, long id)
        {
            var model = new ProposalCampaignModel { ProposalId = id };
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_PROPOSAL_CARD_CAMPAIGNS");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.Int64, MakeDbNull(id));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new ProposalCampaignItem
                        {
                            CampaignCode = reader["CAMPAIGN_CODE"].ToString(),
                            ProposalDetailId = reader["APPOINTMENT_INDICATOR_SUB_CATEGORY_ID"].GetValue<int>(),
                            CanpaignName = reader["CAMPAIGN_NAME"].ToString(),
                            HasStock = reader["HAS_STOCK"].GetValue<bool>(),
                            IsMust = reader["IS_MUST"].GetValue<bool>()
                        };
                        model.Campaigns.Add(item);
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
        public void AddCampaign(UserInfo user, ProposalCampaignModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_ADD_PROPOSAL_DETAIL_CAMPAIGN");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.Int64, MakeDbNull(model.ProposalId));
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int64, MakeDbNull((user.GetUserDealerId())));
                db.AddInParameter(cmd, "CAMPAIGN_CODE", DbType.String, MakeDbNull(model.Campaigns.First().CampaignCode));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 400);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));

                CreateConnection(cmd);

                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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
        }
        public List<LabourModel> GetCampaignLabours(UserInfo user, string campaignCode, long ProposalId)
        {
            var list = new List<LabourModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PROPOSAL_CARD_CAMPAIGN_LABOURS");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.String, MakeDbNull(ProposalId));
                db.AddInParameter(cmd, "CAMPAIGN_CODE", DbType.String, MakeDbNull(campaignCode));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new LabourModel
                        {
                            LabourId = reader["ID_LABOUR"].GetValue<long>(),
                            LabourName = reader["LABOUR_NAME"].ToString(),
                            LabourCode = reader["LABOUR_CODE"].ToString(),
                            Quantity = reader["QUANTITY"].GetValue<decimal>(),
                            Duration = reader["DURATION"].GetValue<decimal>()
                        };
                        list.Add(item);
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

            return list;
        }
        public List<PartModel> GetCampaignParts(UserInfo user, string campaignCode)
        {
            var list = new List<PartModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PROPOSAL_CARD_CAMPAIGN_PARTS");
                db.AddInParameter(cmd, "CAMPAIGN_CODE", DbType.String, MakeDbNull(campaignCode));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new PartModel
                        {
                            PartId = reader["ID_PART"].GetValue<long>(),
                            PartName = reader["PART_NAME"].ToString(),
                            Quantity = reader["QUANTITY"].GetValue<decimal>(),
                            Supplier = reader["SUPPLIER"].ToString(),
                            PartCode = reader["PART_CODE"].ToString(),
                        };
                        list.Add(item);
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

            return list;
        }
        public List<DocumentModel> GetCampaignDocuments(UserInfo user, string campaignCode)
        {
            var list = new List<DocumentModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PROPOSAL_CARD_CAMPAIGN_DOCUMENTS");
                db.AddInParameter(cmd, "CAMPAIGN_CODE", DbType.String, MakeDbNull(campaignCode));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new DocumentModel
                        {
                            DocumentId = reader["DOC_ID"].GetValue<int>(),
                            DocumentName = reader["DOC_NAME"].ToString(),
                            Description = reader["DOCUMENT_DESC"].ToString()
                        };
                        list.Add(item);
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

            return list;
        }
        public VehicleNoteProposalDetailModel GetVehicleNote(UserInfo user, long id, long ProposalId)
        {
            var model = new VehicleNoteProposalDetailModel();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_PROPOSAL_CARD_VEHICLE_NOTE");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.Int64, MakeDbNull(ProposalId));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "ID_VEHICLE_NOTE_PROPOSAL", DbType.Int64, MakeDbNull(id));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.Total = reader["TOTAL"].GetValue<int>();
                        model.CreateDate = reader["CREATE_DATE"].GetValue<DateTime>();
                        model.Next = reader["NEXT_ID"].GetValue<int>();
                        model.Prev = reader["PREV_ID"].GetValue<int>();
                        model.NoteId = reader["ID_VEHICLE_NOTE_PROPOSAL"].GetValue<int>();
                        model.Note = reader["NOTE"].ToString();
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
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        public VehicleNoteDetailModel GetDealerNote(UserInfo user, long id, long proposalId, int seq)
        {
            var model = new VehicleNoteDetailModel();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_PROPOSAL_CARD_DEALER_NOTE");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.Int64, MakeDbNull(proposalId));
                //İlk açılan teklif seq 0 olması istendiği için değiştirildi db.AddInParameter(cmd, "PROPOSAL_SEQ", DbType.Int32, MakeDbNull(seq));
                db.AddInParameter(cmd, "PROPOSAL_SEQ", DbType.Int32, seq);
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "ID_DEALER_NOTE", DbType.Int64, MakeDbNull(id));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.Total = reader["TOTAL"].GetValue<int>();
                        model.CreateDate = reader["CREATE_DATE"].GetValue<DateTime>();
                        model.Next = reader["NEXT_ID"].GetValue<int>();
                        model.Prev = reader["PREV_ID"].GetValue<int>();
                        model.NoteId = reader["ID_PROPOSAL_DEALER_NOTE"].GetValue<int>();
                        model.Note = reader["NOTE"].ToString();
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

        public VehicleNoteProposalDetailModel GetVehicleNotePopup(UserInfo user, long id, long proposalId)
        {
            var model = new VehicleNoteProposalDetailModel();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_PROPOSAL_CARD_VEHICLE_NOTE_POPUP");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.Int64, MakeDbNull(proposalId));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "ID_VEHICLE_NOTE_PROPOSAL", DbType.Int64, MakeDbNull(id));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.Total = reader["TOTAL"].GetValue<int>();
                        model.CreateDate = reader["CREATE_DATE"].GetValue<DateTime>();
                        model.Next = reader["NEXT_ID"].GetValue<int>();
                        model.Prev = reader["PREV_ID"].GetValue<int>();
                        model.NoteId = reader["ID_VEHICLE_NOTE"].GetValue<int>();
                        model.Note = reader["NOTE"].ToString();
                    }

                    reader.Close();
                }
            }
            finally
            {
                CloseConnection();
            }

            return model;
        }

        public void AddDealerNote(UserInfo user, ProposalVehicleNoteModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_ADD_PROPOSAL_CARD_DEALER_NOTE");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.Int64, MakeDbNull(model.ProposalId));
                //İlk açılan teklif seq 0 olması istendiği için değiştirildi db.AddInParameter(cmd, "PROPOSAL_SEQ", DbType.Int32, MakeDbNull(model.ProposalSeq));
                db.AddInParameter(cmd, "PROPOSAL_SEQ", DbType.Int32, model.ProposalSeq);
                db.AddInParameter(cmd, "NOTE", DbType.String, MakeDbNull(model.Note));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 400);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));

                CreateConnection(cmd);

                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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
        }
        public void AddVehicleNote(UserInfo user, ProposalVehicleNoteModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_ADD_PROPOSAL_CARD_VEHICLE_NOTE");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.Int64, MakeDbNull(model.ProposalId));
                db.AddInParameter(cmd, "NOTE", DbType.String, MakeDbNull(model.Note));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 400);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));

                CreateConnection(cmd);

                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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
        }
        public void UpdateFailureCode(UserInfo user, long ProposalId, long proposalDetailId, string failureCodeId, out int errorNo,
          out string errorMessage)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_UPDATE_PROPOSAL_DETAIL_FAILURE_CODE");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.Int64, MakeDbNull(ProposalId));
                db.AddInParameter(cmd, "ID_PROPOSAL_DETAIL", DbType.Int64, MakeDbNull(proposalDetailId));
                db.AddInParameter(cmd, "ID_FAILURE_CODE", DbType.String, MakeDbNull(failureCodeId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 400);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));

                CreateConnection(cmd);

                cmd.ExecuteNonQuery();

                errorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                errorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (errorNo > 0)
                    errorMessage = ResolveDatabaseErrorXml(errorMessage);
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
        }
        public void UpdateDuration(UserInfo user, ProposalQuantityDataModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_UPDATE_PROPOSAL_DETAIL_DURATION");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.Int64, MakeDbNull(model.ProposalId));
                db.AddInParameter(cmd, "ID_PROPOSAL_DETAIL", DbType.Int64, MakeDbNull(model.ProposalDetailId));
                db.AddInParameter(cmd, "DURATION", DbType.Decimal, MakeDbNull(model.Quantity));
                db.AddInParameter(cmd, "TYPE", DbType.String, MakeDbNull(model.Type));
                db.AddInParameter(cmd, "ITEM_ID", DbType.Int64, MakeDbNull(model.ItemId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 400);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));

                CreateConnection(cmd);

                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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
        }
        public string GetTechicalDescription(long proposalDetailId)
        {
            string desc;
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_GET_PROPOSAL_DETAIL_DESCRIPTION",
                    proposalDetailId);
                CreateConnection(cmd);
                desc = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return desc;
        }
        public ModelBase UpdateTechicalDescription(UserInfo user, long proposalDetailId, string description)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_UPDATE_PROPOSAL_DETAIL_DESCRIPTION",
                    proposalDetailId, description, MakeDbNull(user.UserId), null, null);
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
        public string GetProposalContactInfo(long id)
        {
            string desc;
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_GET_PROPOSAL_CONTACT_INFO",
                    id);
                CreateConnection(cmd);
                desc = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return desc;
        }
        public ModelBase UpdateProposalContactInfo(UserInfo user, long id, string note)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_UPDATE_PROPOSAL_CONTACT_INFO",
                    id, note, MakeDbNull(user.UserId), null, null);
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
        public ModelBase UpdateProposalCampaignDenyReason(UserInfo user, long id, string denyReason)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_UPDATE_PROPOSAL_CAMPAIGN_DENY_REASON",
                    id, denyReason, MakeDbNull(user.UserId), null, null);
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
        public string GetProposalCampaignDenyReason(long id)
        {
            string desc;
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_GET_PROPOSAL_CAMPAIGN_DENY_REASON",
                    id);
                CreateConnection(cmd);
                desc = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return desc;
        }
        public List<PartReturnModel> ListDetailPartReturnItems(UserInfo user, long ProposalId)
        {
            List<PartReturnModel> list;
            try
            {
                CreateDatabase();
                var rowMapper =
                    MapBuilder<PartReturnModel>.MapAllProperties()
                        .DoNotMap(c => c.RequiredQuantity)
                        .DoNotMap(c => c.ReservedQuantity)
                        .Build();
                list = db.ExecuteSprocAccessor("P_LIST_PROPOSAL_CARD_DETAIL_PARTS_RETURN", rowMapper, ProposalId, MakeDbNull(user.LanguageCode)).ToList();

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
        public List<ModelBase> ReturnDetailParts(UserInfo user, List<PartReturnModel> detailList, long proposalDetailId)
        {
            var list = new List<ModelBase>(detailList.Count);
            CreateDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                long masterId = 0;
                try
                {

                    foreach (var partReturnModel in detailList.Where(c => c.ReturnedQuantity > 0))
                    {
                        var cmd = db.GetStoredProcCommand("P_RETURN_PROPOSAL_DETAIL_PARTS", proposalDetailId,
                            partReturnModel.PartId, partReturnModel.ReturnedQuantity, MakeDbNull(masterId),
                            MakeDbNull(user.UserId), null, null
                            );
                        db.ExecuteNonQuery(cmd, transaction);
                        var modelBase = new ModelBase();

                        modelBase.ErrorNo = int.Parse(db.GetParameterValue(cmd, "ERROR_NO").ToString());
                        if (modelBase.ErrorNo > 0)
                        {
                            modelBase.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                            modelBase.ErrorMessage = ResolveDatabaseErrorXml(modelBase.ErrorMessage);
                            list.Add(modelBase);
                        }
                        else
                              if (masterId == 0)
                            masterId = long.Parse(db.GetParameterValue(cmd, "ID_PROPOSAL_PICKING_MST").ToString());

                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }



            return list;

        }
        public ModelBase PickDetailParts(UserInfo user, long id, long proposalDetailId)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_PICK_PROPOSAL_DETAIL_PARTS",
                    id, proposalDetailId, MakeDbNull(user.UserId), MakeDbNull(user.LanguageCode), null, null);
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
        public void ChangeProposalStatus(UserInfo user, long ProposalId, int statusId, int seq, out int errorNo, out string errorMessage)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_CHANGE_PROPOSAL_STATUS");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.Int64, MakeDbNull(ProposalId));
                //İlk açılan teklif seq 0 olması istendiği için değiştirildi db.AddInParameter(cmd, "PROPOSAL_SEQ", DbType.Int32, MakeDbNull(seq));
                db.AddInParameter(cmd, "PROPOSAL_SEQ", DbType.Int32, seq);
                db.AddInParameter(cmd, "WO_CANCEL_REASON", DbType.String, string.Empty);
                db.AddInParameter(cmd, "PROPOSAL_STATUS", DbType.Int32, MakeDbNull(statusId));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);

                cmd.ExecuteNonQuery();

                errorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                errorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();

                if (errorNo > 0)
                    errorMessage = ResolveDatabaseErrorXml(errorMessage);

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
        }
        public void UpdateProposalSparePartId(long ProposalId, int sparePartSaleId, int seq)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_UPDATE_PROPOSAL_CARD_SPARE_PART_ID");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.Int64, MakeDbNull(ProposalId));
                //İlk açılan teklif seq 0 olması istendiği için değiştirildi db.AddInParameter(cmd, "PROPOSAL_SEQ", DbType.Int32, MakeDbNull(seq));
                db.AddInParameter(cmd, "PROPOSAL_SEQ", DbType.Int32, seq);
                db.AddInParameter(cmd, "SP_ID", DbType.Int32, MakeDbNull(sparePartSaleId));

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
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
        }

        public ProposalCardModel ProposalRevision(UserInfo user, long id, int seq)
        {
            var model = new ProposalCardModel();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_REVISION_PROPOSAL_CARD");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.Int64, MakeDbNull(id));
                //İlk açılan teklif seq 0 olması istendiği için değiştirildi db.AddInParameter(cmd, "PROPOSAL_SEQ", DbType.Int32, MakeDbNull(seq));
                db.AddInParameter(cmd, "PROPOSAL_SEQ", DbType.Int32, seq);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.ProposalId = reader["PROPOSAL_ID"].GetValue<long>();
                        model.ProposalSeq = reader["PROPOSAL_SEQ"].GetValue<int>();
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
        public ModelBase CreateCampaignRequest(UserInfo user, long proposalDetailId)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_CREATE_PROPOSAL_DETAIL_CAMPAIGN_REQUEST",
                    proposalDetailId, MakeDbNull(user.UserId), null, null);
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
        public List<PartModel> ListCampaignRequestDetails(UserInfo user, long proposalDetailId, ProposalCampaignRequestViewModel model)
        {
            List<PartModel> list;
            try
            {
                CreateDatabase();
                var rowMapper = MapBuilder<PartModel>.BuildAllProperties();
                list = db.ExecuteSprocAccessor("P_LIST_PROPOSAL_CARD_CAMPAIGN_REQUEST_DETAILS", rowMapper, proposalDetailId, MakeDbNull(user.LanguageCode)).ToList();
                model = new CampaignRequestData().GetCampaignRequestProposal(user, model);
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
        public PartReservationDTO GetPartReservationData(UserInfo user, long proposalDetailId)
        {
            var dto = new PartReservationDTO();
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_GET_PROPOSAL_CARD_DETAIL_PART_RESERVATION_DATA",
                    proposalDetailId,
                   null, MakeDbNull(user.LanguageCode));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dto.ProcessTypeList.Add(new SelectListItem
                        {
                            Selected = reader.GetBoolean(2),
                            Text = reader.GetString(1),
                            Value = reader.GetString(0)
                        });
                    }
                    dto.CurrentProcessType = db.GetParameterValue(cmd, "CURRENT_PROCESS_TYPE").ToString();
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

            return dto;
        }
        public ChangeProcessTypeModel GetProcessTypeData(UserInfo user, long proposalDetailId)
        {
            var model = new ChangeProcessTypeModel();
            var dto = GetPartReservationData(user,proposalDetailId);
            model.CurrentProcessType = dto.CurrentProcessType;
            model.ProcessTypeList = dto.ProcessTypeList;
            return model;
        }
        public ModelBase UpdateProcessType(UserInfo user, long proposalDetailId, string processType, bool confirmed)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_UPDATE_PROPOSAL_DETAIL_PROCESS_TYPE",
                    proposalDetailId, processType, confirmed, MakeDbNull(user.UserId), null, null, null);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = int.Parse(db.GetParameterValue(cmd, "ERROR_NO").ToString());


                if (model.ErrorNo.In(1, 2, 4))
                {
                    model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
                }
                else if (model.ErrorNo == 3)
                {
                    string desc = db.GetParameterValue(cmd, "DESC").ToString();
                    if (!string.IsNullOrEmpty(desc))
                    {
                        var items = desc.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                        var parts = new StringBuilder();
                        foreach (var item in items)
                        {
                            var arr = item.Split(new[] { "@" }, StringSplitOptions.RemoveEmptyEntries);
                            var partCode = arr[0];
                            bool hasPickingOrder = arr[1] == "1";
                            if (hasPickingOrder)
                            {
                                parts.Append(partCode).Append(",");
                            }
                        }
                        if (parts.Length > 0)
                        {
                            model.ErrorMessage =
                                string.Format(
                                    CommonUtility.GetResourceValue("ProposalCard_ProcessTypeWithPickOrders"),
                                    parts.ToString(0, parts.Length - 1));
                        }

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
            return model;
        }
        public DateTime? GetVehicleLeaveDate(long ProposalId)
        {
            DateTime? vehicleLeaveDate;
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_GET_PROPOSAL_VEHICLE_LEAVE_DATE",
                    ProposalId);
                CreateConnection(cmd);
                vehicleLeaveDate = cmd.ExecuteScalar().GetValue<DateTime?>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return vehicleLeaveDate;
        }
        public CommonValues.ProposalCardVehicleLeaveResult CheckVehicleLeaveMandatoryFields(UserInfo user, long ProposalId)
        {
            var result = 0;
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_GET_PROPOSAL_VEHICLE_LEAVE_STATUS",
                    ProposalId, MakeDbNull(user.UserId));
                CreateConnection(cmd);
                result = cmd.ExecuteScalar().GetValue<int>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            switch (result)
            {
                default:
                case 0:
                    return ODMSCommon.CommonValues.ProposalCardVehicleLeaveResult.Success;
                case 1:
                    return ODMSCommon.CommonValues.ProposalCardVehicleLeaveResult.ProcessTypeNotSet;
                case 2:
                    return ODMSCommon.CommonValues.ProposalCardVehicleLeaveResult.DismentledPartsNotSet;
                case 3:
                    return ODMSCommon.CommonValues.ProposalCardVehicleLeaveResult.IncompletedLaboursExists;
                case 4:
                    return ODMSCommon.CommonValues.ProposalCardVehicleLeaveResult.NotPickedPartsExists;
                case 5:
                    return ODMSCommon.CommonValues.ProposalCardVehicleLeaveResult.PdiResultsNotSet;
                case 6:
                    return ODMSCommon.CommonValues.ProposalCardVehicleLeaveResult.PickingIsNotFinished;
                case 7:
                    return ODMSCommon.CommonValues.ProposalCardVehicleLeaveResult.EmptyProposalDeatail;
                case 8:
                    return ODMSCommon.CommonValues.ProposalCardVehicleLeaveResult.EmptyTechDesc;
                case 9:
                    return ODMSCommon.CommonValues.ProposalCardVehicleLeaveResult.NoDetailExists;
                case 10:
                    return ODMSCommon.CommonValues.ProposalCardVehicleLeaveResult.WaitingPreApproval;


            }
        }
        public List<MandatoryRemovalPart> ListMandatoryRemovalPart(UserInfo user, long ProposalId)
        {
            List<MandatoryRemovalPart> list;
            try
            {
                CreateDatabase();
                var rowMapper = MapBuilder<MandatoryRemovalPart>.BuildAllProperties();
                list = db.ExecuteSprocAccessor("P_GET_PROPOSAL_VEHICLE_LEAVE_PARTS", rowMapper, ProposalId, MakeDbNull(user.LanguageCode)).ToList();
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
        public ProposalPartRemovalDto GetPartRemovalDto(UserInfo user,long proposalDetailId, long partId)
        {
            var dto = new ProposalPartRemovalDto();
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_GET_PROPOSAL_CARD_DETAIL_PART_REMOVAL_DATA",
                    proposalDetailId,
                    partId, MakeDbNull(user.LanguageCode));

                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dto.DismentledPartId = reader["DismentledPartId"].GetValue<long?>();
                        dto.DismentledPartName = reader["DismentledPartName"].ToString();
                        dto.DismentledPartSerialNo = reader["DismentledPartSerialNo"].ToString();
                        dto.PartId = reader["PartId"].GetValue<long>();
                        dto.PartName = reader["PartName"].ToString();
                        dto.PartCode = reader["PartCode"].ToString();
                        dto.PartSerialNo = reader["PartSerialNo"].ToString();
                    }
                }
                dto.ProposalDetailId = proposalDetailId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return dto;
        }
        public List<SelectListItem> ListRemovableParts(UserInfo user, long partId)
        {
            List<SelectListItem> list;
            try
            {
                CreateDatabase();
                var rowMapper = MapBuilder<SelectListItem>.BuildAllProperties();
                list = db.ExecuteSprocAccessor("P_LIST_REMOVABLE_PARTS_COMBO", rowMapper, partId, MakeDbNull(user.LanguageCode)).ToList();
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
        public ModelBase UpdateRemovalInfo(UserInfo user, ProposalPartRemovalDto dto)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_UPDATE_PROPOSAL_CARD_DETAIL_PART_REMOVAL_DATA",
                    dto.ProposalDetailId, dto.PartId, dto.DismentledPartId, dto.DismentledPartSerialNo, dto.PartSerialNo, MakeDbNull(user.UserId), null, null);
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
        public void AddPdiPackage(UserInfo user, ProposalAddPdiPackageModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_ADD_PROPOSAL_CARD_PDI_PACKAGE",
                    model.ProposalId,
                    model.TransmissionSerialNo,
                    model.DifferencialSerialNo,
                    MakeDbNull(model.PdiCheckNote),
                    MakeDbNull(user.UserId), MakeDbNull(user.LanguageCode), null, null);
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
        }
        public void UpdatePdiPackage(UserInfo user,ProposalAddPdiPackageModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_UPDATE_PROPOSAL_CARD_PDI_PACKAGE",
                    model.ProposalId,
                    model.TransmissionSerialNo,
                    model.DifferencialSerialNo,
                    MakeDbNull(model.PdiCheckNote),
                    MakeDbNull(user.UserId), MakeDbNull(user.LanguageCode), null, null);
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
        }


        public ProposalAddPdiPackageModel GetPdiPackageData(long id)
        {
            var model = new ProposalAddPdiPackageModel();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_PDI_VEHICLE_MST_UPDATE_DATA_PROPOSAL", id);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.PdiCheckNote = reader["PdiCheckNote"].ToString();
                        model.TransmissionSerialNo = reader["TransmissionSerialNo"].ToString();
                        model.DifferencialSerialNo = reader["DifferencialSerialNo"].ToString();
                        model.ApprovalNote = reader["ApprovalNote"].ToString();
                        model.ProposalId = reader["ProposalId"].GetValue<long>();
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
        public bool GetPdiVehicleIsControlled(long ProposalId)
        {
            bool isControlled;
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_GET_PDI_VEHICLE_RESULT_IS_CONTROLLED_PROPOSAL",
                    ProposalId);
                CreateConnection(cmd);
                isControlled = cmd.ExecuteScalar().GetValue<bool>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return isControlled;
        }
        public List<PdiResultItem> ListPdiResultItems(UserInfo user,long ProposalId)
        {
            List<PdiResultItem> list;
            try
            {
                CreateDatabase();
                var rowMapper = MapBuilder<PdiResultItem>.BuildAllProperties();
                list = db.ExecuteSprocAccessor("P_LIST_PDI_VEHICLE_RESULT_DET_PROPOSAL", rowMapper, ProposalId, MakeDbNull(user.LanguageCode)).ToList();

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
        public Tuple<string, string, List<SelectListItem>, List<SelectListItem>, List<SelectListItem>> GetPdiResultData(UserInfo user,long ProposalId, string controlCode)
        {
            string controlName = string.Empty;
            var partList = new List<SelectListItem>();
            var breakDownList = new List<SelectListItem>();
            var resultList = new List<SelectListItem>();
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_GET_PDI_VEHICLE_RESULT_DATA_PROPOSAL", ProposalId, controlCode, MakeDbNull(user.LanguageCode));

                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        controlName = reader.GetString(0);
                    }
                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            partList.Add(new SelectListItem
                            {
                                Text = reader["PART_NAME"].ToString(),
                                Value = reader["PDI_PART_CODE"].ToString()
                            });
                        }
                    }
                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            breakDownList.Add(new SelectListItem
                            {
                                Text = reader["BREAKDOWN_NAME"].ToString(),
                                Value = reader["PDI_BREAKDOWN_CODE"].ToString()
                            });
                        }
                    }
                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            resultList.Add(new SelectListItem
                            {
                                Text = reader["RESULT_NAME"].ToString(),
                                Value = reader["PDI_RESULT_CODE"].ToString()
                            });
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


            var tupple = new Tuple<string, string, List<SelectListItem>, List<SelectListItem>, List<SelectListItem>>(controlCode, controlName, partList, breakDownList, resultList);
            return tupple;
        }
        public ModelBase SavePdiResult(UserInfo user, ProposalPdiResultModel dto, string type)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_UPDATE_PDI_VEHICLE_RESULT_DET_PROPOSAL",
                    dto.ProposalId, type, dto.ControlCode, dto.PartCode, dto.BreakDownCode, dto.ResultCode,
                    MakeDbNull(user.UserId), MakeDbNull(user.LanguageCode), null, null);
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
        public ModelBase PdiSendToApproval(UserInfo user, long id)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_SEND_PDI_VEHICLE_RESULT_MST_APPROVAL_PROPOSAL",
                    id, MakeDbNull(user.UserId), null, null);
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
        public PdiPrintModel GetPdiPackageDetails(UserInfo user,long id)
        {
            var model = new PdiPrintModel();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_PDI_PACKAGE_VEHICLE_DETAILS_PROPOSAL", id, user.LanguageCode);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.Dealer = reader["DEALER_SHRT_NAME"].ToString();
                        model.Sender = reader["SENDER"].ToString();
                        model.Approver = reader["APPROVER"].ToString();
                        model.VinNo = reader["VIN_NO"].ToString();
                        model.EngineNo = reader["ENGINE_NO"].ToString();
                        model.TransmissionNo = reader["TRANSMISSION_SERIALNO"].ToString();
                        model.Status = reader["STATUS"].ToString();
                        model.DifferentialNo = reader["DIFFERENTAIL_SERIALNO"].ToString();
                        model.SenderNote = reader["PDI_CHECK_NOTE"].ToString();
                        model.ApproverNote = reader["APPROVAL_NOTE"].ToString();
                        model.PdiCreateDate = reader["CREATE_DATE"].GetValue<DateTime>();
                        model.IsControlled = reader["IS_CONTROLLED"].GetValue<bool>();

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

            model.Items = ListPdiResultItems(user,id);

            return model;
        }
        public ModelBase DeleteDetailItem(UserInfo user,long ProposalId, long proposalDetailId, string type, long itemId)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_DELETE_PROPOSAL_DETAIL_ITEM",
                    proposalDetailId, itemId, type,
                    MakeDbNull(user.UserId), null, null);
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
        public VehicleHistoryModel GetGetVehicleHistoryLastItem(int vehicleId)
        {
            VehicleHistoryModel model = null;
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_GET_PROPOSAL_CARD_LAST_VEHICLE_HISTORY_DATA", vehicleId);

                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model = new VehicleHistoryModel
                        {
                            DealerName = reader["DEALER_NAME"].ToString(),
                            HistoryDate = reader["HISTORY_DATE"].GetValue<DateTime>(),
                            VehicleKm = reader["VEHICLE_KM"].GetValue<long>()
                        };
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
            return model;
        }
        public List<SelectListItem> ListVehicleHourMaints(UserInfo user, int vehicleId)
        {
            List<SelectListItem> list;
            try
            {
                CreateDatabase();
                var rowMapper = MapBuilder<SelectListItem>.BuildAllProperties();
                list = db.ExecuteSprocAccessor("P_LIST_VEHICLE_HOUR_MAINTS_COMBO", rowMapper, MakeDbNull(vehicleId), MakeDbNull(user.LanguageCode)).ToList();
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
        public ModelBase CompletePdiVehicleResult(UserInfo user,long ProposalId)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_COMPLETE_PDI_VEHICLE_RESULT_PROPOSAL",
                    ProposalId, MakeDbNull(user.UserId), null, null);
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
        public List<PickingCancellationItem> ListPickingsForCancellation(UserInfo user,long ProposalId)
        {
            List<PickingCancellationItem> list;
            try
            {
                CreateDatabase();
                var rowMapper = MapBuilder<PickingCancellationItem>.BuildAllProperties();
                list = db.ExecuteSprocAccessor("P_LIST_PROPOSAL_PICKING_MST_FOR_CANCELLATION", rowMapper, ProposalId, MakeDbNull(user.LanguageCode)).ToList();

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
        public void CancelPicking(UserInfo user,long? ProposalId, long pickingId)
        {
            CreateDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {


                    var cmd = db.GetStoredProcCommand("P_CANCEL_PROPOSAL_PICKING_MST",
                                                      MakeDbNull(ProposalId), pickingId,
                                                      MakeDbNull(user.UserId));
                    CreateConnection(cmd);
                    db.ExecuteNonQuery(cmd, transaction);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
                finally
                {
                    CloseConnection();
                }
            }


        }

        public ModelBase CreateSparePartSale(UserInfo user, int id, int seq)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_COMPLETE_PDI_VEHICLE_RESULT_PROPOSAL",
                    id, seq, MakeDbNull(user.UserId), null, null);
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

        public void SaveGeneralInfo(UserInfo user, GeneralInfo model)
        {

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_UPDATE_PROPOSAL_GENERAL_INFO",
                    MakeDbNull(model.ProposalId), model.ProposalSeq, model.Matter1, model.Matter2, model.Matter3,model.Matter4,model.WitholdingId,
                    //MakeDbNull(model.ProposalId), model.ProposalSeq, model.Matter1, model.Matter2, model.Matter3,model.WitholdingId,
                    MakeDbNull(user.UserId));
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

        public void SaveTechnicalInfo(UserInfo user, GeneralInfo model)
        {

            try
            {
                CreateDatabase();

                var tecnicalDesc = string.Empty;
                if (model.TechnicalDesc != null && model.TechnicalDesc.Count() > 0)
                    tecnicalDesc = String.Join("/*,/", model.TechnicalDesc);



                var cmd = db.GetStoredProcCommand("P_UPDATE_PROPOSAL_TECHNICAL_INFO",
                    MakeDbNull(model.ProposalId), model.ProposalSeq, tecnicalDesc,
                    MakeDbNull(user.UserId));
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

        public void AddLabourPrice(UserInfo user, AddLabourPrice model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_CHANGE_PROPOSAL_CARD_DETAIL_LABOUR_PRICE_FOR_PROPOSAL");
                db.AddInParameter(cmd, "ID_PROPOSAL", DbType.Int64, MakeDbNull(model.WorkOrderId));
                db.AddInParameter(cmd, "ID_PROPOSAL_DETAIL", DbType.Int64, MakeDbNull(model.WorkOrderDetailId));
                db.AddInParameter(cmd, "TYPE", DbType.String, MakeDbNull(model.Type));
                db.AddInParameter(cmd, "ITEM_ID", DbType.Int64, MakeDbNull(model.ItemId));
                db.AddInParameter(cmd, "UNITPRICE", DbType.Decimal, MakeDbNull(model.UnitPrice));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 400);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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
        }

        public ModelBase CancelVehicleLeave(UserInfo user, long ProposalId, string reason)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_CANCEL_PROPOSAL_VEHICLE_LEAVE",
                    ProposalId, reason, MakeDbNull(user.UserId), null, null);
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
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSCommon.Resources;
using ODMSModel.AppointmentDetails;
using ODMSModel.Common;
using ODMSModel.WorkOrderCard;
using ODMSModel.WorkOrderCard.CampaignDetail;
using ODMSModel.WorkOrderCard.WarrantyDetail;

namespace ODMSData
{
    public class WorkOrderCardData : DataAccessBase
    {
        ///DealerId sessiondan alınır. Yetkisiz görüntülemeyi önlemek için 
        public WorkOrderCardModel GetWorkOrderCard(UserInfo user,long workOrderId)
        {
            var model = new WorkOrderCardModel();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_WORK_ORDER_CARD");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(workOrderId));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int64, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "IS_HQ", DbType.Boolean, !user.IsDealer);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));

                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //workorder data
                        model.WorkOderId = workOrderId;
                        model.ProposalId = reader["ID_PROPOSAL"].GetValue<long?>();
                        model.ProposalSeq = reader["PROPOSAL_SEQ"].GetValue<int>();
                        model.WorkOrderDate = reader["WO_DATE"].GetValue<DateTime>();
                        model.AppointmentId = reader["APPOINTMENT_ID"].GetValue<long>();
                        model.AppointmentDate = reader["APPOINTMENT_DATE"].GetValue<DateTime?>() == default(DateTime)
                            ? null
                            : reader["APPOINTMENT_DATE"].GetValue<DateTime?>();
                        model.AppointmentType = reader["APPOINTMENT_TYPE_NAME"].ToString();
                        model.DealerId = reader["ID_DEALER"].GetValue<int>();
                        model.ContactInfo = reader["CONTACT_INFO"].ToString();
                        model.WorkOrderStatId = reader["ID_WORK_ORDER_STAT"].GetValue<int>();
                        model.WorkOrderStat = reader["STATUS_DESC"].ToString();
                        model.WorkOrderStatManualChangeAllow = reader["MANUEL_CHANGE_ALLOW"].GetValue<bool>();
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
                        model.CancelReason = reader["WO_CANCEL_REASON"].ToString();
                        model.DeniedCampaignCodes = reader["DENIED_CAMPAIGN_CODES"].ToString();
                        model.DeniedCampaignServiceCodes = reader["DENIED_CAMPAIGN_SERVICE_CODES"].ToString();
                        //customer data
                        var customer = new WorkOrderCustomerModel
                        {
                            CustomerId = reader["CUSTOMER_ID"].GetValue<int>(),
                            CustomerName = reader["CUSTOMER_NAME"].ToString(),
                            TcIdentityNo = reader["TC_IDENTITY_NO"].ToString(),
                            CustomerAddress = reader["ADDRESS_DESC"].ToString(),
                            TaxOffice = reader["TAX_OFFICE"].ToString(),
                            TaxNo = reader["TAX_NO"].ToString(),
                            Staff = reader["RESP_CONSULTANT"].ToString()
                        };
                        model.Customer = customer;
                        //vehicle data
                        var vehicle = new WorkOrderVehicleModel
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
                            PopupNoteCount = reader["POPUP_VEHICLE_NOTE_COUNT"].GetValue<int>(),
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
                            WorkOrderCount = reader["WorkOrderCount"].GetValue<int>()
                        };

                        //TODO: kampanya var mı şu an 0 geliyor burdaki işleyişi konuşmak lazım
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

            model.Details = GetWorkOrderCardDetails(user,workOrderId);

            //detail list

            var detailList = model.Details.Where(c => c.Type == "INDICATOR").Select(x => new WorkOrderDetailList
            {
                WorkOrderDetailId = x.WorkOrderDetailId,
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
                GifNo = x.GifNo,
                IsCampaignCritical = x.IsCampaignCritical,
                IsFromProposal = x.IsFromProposal
            }).ToList();
            //maintenance adjustment
            detailList.ForEach(c =>
            {
                var meintenanceItem =
                    model.Details.FirstOrDefault(
                        d => d.WorkOrderDetailId == c.WorkOrderDetailId && d.MaintenanceId > 0);

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
        public List<WorkOrderDetailModel> GetWorkOrderCardDetails(UserInfo user,long workOrderId)
        {
            var model = new List<WorkOrderDetailModel>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_WORK_ORDER_CARD_DETAILS");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(workOrderId));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "IS_HQ", DbType.Boolean, !user.IsDealer);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));

                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new WorkOrderDetailModel
                        {
                            Id = reader["ID"].GetValue<long>(),
                            Code = reader["CODE"].ToString(),
                            CurrencyCode = reader["CURRENCY_CODE"].ToString(),
                            Description = reader["DESCRIPTION"].ToString(),
                            DiscountRatio = reader["DISCOUNT_RATIO"].GetValue<double>(),
                            WorkOrderDetailId = reader["ID_WORK_ORDER_DETAIL"].GetValue<int>(),
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
                            DismantledPart = reader["DISMANTLED_PART"].GetValue<string>(),
                            DismantledPartCount = reader["DISMANTLED_PART_COUNT"].GetValue<int>(),
                            WarrantyWillBeApproved = reader["WARRANTY_WILL_BE_APPROVED"].GetValue<bool>(),
                            IsFromProposal = reader["IS_FROM_PROPOSAL"].GetValue<bool>(),
                            IsCampaignCritical = reader["IS_CAMPAIGN_CRITICAL"].GetValue<bool>()
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
                    c.TotalDiscount = (c.DiscountRatio > 0 ? (c.TotalCustomerPrice * decimal.Parse(c.DiscountRatio.ToString()) / 100) : 0);
                    c.DiscountRatio = double.Parse(decimal.Round(decimal.Parse(c.DiscountRatio.ToString()), 2).ToString());

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
        public void UpdateCustomerNote(WorkOrderCustomerNoteUpdateModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_UPDATE_WORK_ORDER_CUSTOMER_NOTE");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(model.WorkOrderId));
                db.AddInParameter(cmd, "NOTE", DbType.String, MakeDbNull(model.Note));
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
        public long? UpdateVehicleKM(UserInfo user,long workOrderId, long km, bool isHourMaint, int hour, int fromUpdateBtn, out int ErrorNo, out string ErrorMessage)
        {
            long? result;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_UPDATE_WORK_ORDER_CARD_VEHICLE_KM");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(workOrderId));
                db.AddParameter(cmd, "VEHICLE_KM", DbType.Int64, ParameterDirection.InputOutput, "VEHICLE_KM", DataRowVersion.Default, MakeDbNull(km));
                db.AddParameter(cmd, "IS_HOUR_MAINT", DbType.Int32, ParameterDirection.Input, "IS_HOUR_MAINT", DataRowVersion.Default, isHourMaint);
                db.AddInParameter(cmd, "HOUR", DbType.Int32, MakeDbNull(hour));
                db.AddInParameter(cmd, "FROM_UPDATE_BTN", DbType.Int32, fromUpdateBtn);
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

        public string UpdateVehiclePlate(UserInfo user,long workOrderId, string plate, out int ErrorNo, out string ErrorMessage)
        {
            string result;
            result = plate;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_UPDATE_WORK_ORDER_CARD_VEHICLE_PLATE");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(workOrderId));
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

        public void AddWorkOrderDetail(UserInfo user,AppointmentDetailsViewModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_ADD_WORK_ORDER_DETAIL");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(model.AppointmentId));
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

        public void AddWorkOrderPart(UserInfo user,ODMSModel.AppointmentDetailsParts.AppointmentDetailsPartsViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_ADD_WORK_ORDER_DETAIL_PART");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(model.Id));
                db.AddInParameter(cmd, "ID_WORK_ORDER_DETAIL", DbType.Int32, MakeDbNull(model.AppointIndicId));
                db.AddInParameter(cmd, "ID_PART", DbType.Int64, MakeDbNull(model.PartId));
                db.AddInParameter(cmd, "QUANTITY", DbType.Decimal, MakeDbNull(model.Quantity));
                db.AddInParameter(cmd, "SELECTED_ID_PART", DbType.Decimal, MakeDbNull(model.SelectedPartId));
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

        public AppointmentDetailsViewModel GetDetailData(UserInfo user,long workOrderId, long subCategoryId)
        {
            var appointmentDetail = new AppointmentDetailsViewModel();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_WORK_ORDER_CARD_DETAILS");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(workOrderId));
                db.AddInParameter(cmd, "APPOINTMENT_INDICATOR_SUB_CATEGORY_ID", DbType.Int64, MakeDbNull(subCategoryId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
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

        public void AddWorkOrderLabour(UserInfo user,ODMSModel.AppointmentDetailsLabours.AppointmentDetailsLaboursViewModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_ADD_WORK_ORDER_DETAIL_LABOUR");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(model.AppointmentId));
                db.AddInParameter(cmd, "ID_WORK_ORDER_DETAIL", DbType.Int64, MakeDbNull(model.AppointmentIndicatorId));
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

        public LabourDataModel GetLabourData(long labourId, int? vehicleId)
        {
            var model = new LabourDataModel();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_LABOUR_DATA_FOR_WORK_ORDER_DETAIL");
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

        public WorkOrderMaintenanceModel GetMaintenance(UserInfo user,WorkOrderMaintenanceModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_WORK_ORDER_PERIODIC_MAINT");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(model.WorkOrderId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.MaintenanceKilometer = reader["MAINT_KM"].GetValue<int>();
                        model.MaintenancName = reader["MAINT_NAME"].ToString();
                        model.VehicleKilometer = reader["VEHICLE_KM"].GetValue<int>();
                        model.MaintenanceId = reader["ID_MAINT"].GetValue<int>();
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

        public void AddWorkOrderMaint(UserInfo user,WorkOrderMaintenanceModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_ADD_WORK_ORDER_DETAIL_MAINT");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(model.WorkOrderId));
                db.AddInParameter(cmd, "ID_WORK_ORDER_DETAIL", DbType.Int64, MakeDbNull(model.WorkOrderDetailId));
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

        public void CancelDetail(UserInfo user,WorkOrderDetailCancelModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_CANCEL_WORK_ORDER_DETAIL");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(model.WorkOrderId));
                db.AddInParameter(cmd, "ID_WORK_ORDER_DETAIL", DbType.Int64, MakeDbNull(model.WorkOrderDetailId));
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

        public WorkOrderDiscountModel GetWorkOrderDetailItemDataForDiscount(long workOrderId, long workOrderDetailId, string type, long itemId)
        {
            var model = new WorkOrderDiscountModel();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_WORK_ORDER_CARD_DETAIL_FOR_DISCOUNT");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(workOrderId));
                db.AddInParameter(cmd, "ID_WORK_ORDER_DETAIL", DbType.Int64, MakeDbNull(workOrderDetailId));
                db.AddInParameter(cmd, "TYPE", DbType.String, MakeDbNull(type));
                db.AddInParameter(cmd, "ITEM_ID", DbType.Int64, MakeDbNull(itemId));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.WorkOrderId = workOrderId;
                        model.WorkOrderDetailId = workOrderDetailId;
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

        public void AddDiscount(UserInfo user,WorkOrderDiscountModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_UPDATE_WORK_ORDER_DISCOUNT");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(model.WorkOrderId));
                db.AddInParameter(cmd, "ID_WORK_ORDER_DETAIL", DbType.Int64,
                    MakeDbNull(model.WorkOrderDetailId));
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

        public WorkOrderQuantityDataModel GetQuantityData(UserInfo user,long workOrderId, long workOrderDetailId, string type, long itemId)
        {
            var model = new WorkOrderQuantityDataModel();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_WORK_ORDER_CARD_QUANTITY_DATA");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(workOrderId));
                db.AddInParameter(cmd, "ID_WORK_ORDER_DETAIL", DbType.Int64, MakeDbNull(workOrderDetailId));
                db.AddInParameter(cmd, "TYPE", DbType.String, MakeDbNull(type));
                db.AddInParameter(cmd, "ITEM_ID", DbType.Int64, MakeDbNull(itemId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.WorkOrderId = workOrderId;
                        model.Type = type;
                        model.WorkOrderDetailId = workOrderDetailId;
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

        public void UpdateQuantity(UserInfo user,WorkOrderQuantityDataModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_UPDATE_WORK_ORDER_DETAIL_QUANTITY");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(model.WorkOrderId));
                db.AddInParameter(cmd, "ID_WORK_ORDER_DETAIL", DbType.Int64, MakeDbNull(model.WorkOrderDetailId));
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

        public WorkOrderMaintenanceQuantityDataModel GetMaintenanceQuantityData(UserInfo user,long workOrderId, long workOrderDetailId, string type, long itemId, int maintId)
        {
            var model = new WorkOrderMaintenanceQuantityDataModel();
            model.WorkOrderId = workOrderId;
            model.Type = type;
            model.WorkOrderDetailId = workOrderDetailId;
            model.ItemId = itemId;
            model.MaintenanceId = maintId;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_WORK_ORDER_CARD_MAINT_QUANTITY_DATA");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(workOrderId));
                db.AddInParameter(cmd, "ID_WORK_ORDER_DETAIL", DbType.Int64, MakeDbNull(workOrderDetailId));
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

        public List<SelectListItem> ListAlternateParts(UserInfo user,long partId, int maintId, long workOrderDetailId)
        {
            var list = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SPARE_PART_ALTERNATE_PARTS_COMBO");
                db.AddInParameter(cmd, "ID_WORK_ORDER_DETAIL", DbType.Int64, MakeDbNull(workOrderDetailId));
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

        public void RemoveLabourOrPartFromMaintenance(UserInfo user,WorkOrderMaintenanceQuantityDataModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_REMOVE_WORK_ORDER_CARD_MAINT_PART_LABOUR");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(model.WorkOrderId));
                db.AddInParameter(cmd, "ID_WORK_ORDER_DETAIL", DbType.Int64, MakeDbNull(model.WorkOrderDetailId));
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

        public void ChangePart(UserInfo user,WorkOrderMaintenanceQuantityDataModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_WORK_ORDER_DETAIL_CHANGE_PART");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(model.WorkOrderId));
                db.AddInParameter(cmd, "ID_WORK_ORDER_DETAIL", DbType.Int64, MakeDbNull(model.WorkOrderDetailId));
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

        public int GetVehiclePlate(string id, string plate)
        {
            var isDuplicate = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_VEHICLE_BY_PLATE");
                db.AddInParameter(cmd, "VEHICLE_PLATE", DbType.String, MakeDbNull(plate));
                db.AddInParameter(cmd, "WORK_ORDER_ID", DbType.Int64, MakeDbNull(id));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        isDuplicate = reader["NUMOFVEHICLE"].GetValue<int>();
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

            return isDuplicate;
        }
        public WorkOrderCampaignModel GetCampaignData(UserInfo user,long id)
        {
            var model = new WorkOrderCampaignModel { WorkOrderId = id };
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_WORK_ORDER_CARD_CAMPAIGNS");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(id));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new CampaignItem
                        {
                            CampaignCode = reader["CAMPAIGN_CODE"].ToString(),
                            WorkOrderDetailId = reader["APPOINTMENT_INDICATOR_SUB_CATEGORY_ID"].GetValue<int>(),
                            CanpaignName = reader["CAMPAIGN_NAME"].ToString(),
                            HasStock = reader["HAS_STOCK"].GetValue<bool>(),
                            IsMust = reader["IS_MUST"].GetValue<bool>(),
                            Denied_Campaign_Codes = reader["DENIED_CAMPAIGN_CODES"].ToString(),
                            Denied_Campaign_Service_Codes = reader["DENIED_CAMPAIGN_SERVICE_CODES"].ToString()
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

        public List<LabourModel> GetCampaignLabours(UserInfo user,string campaignCode, long workOrderId)
        {
            var list = new List<LabourModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_WORK_ORDER_CARD_CAMPAIGN_LABOURS");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.String, MakeDbNull(workOrderId));
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

        public List<PartModel> GetCampaignParts(UserInfo user,string campaignCode)
        {
            var list = new List<PartModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_WORK_ORDER_CARD_CAMPAIGN_PARTS");
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

        public List<DocumentModel> GetCampaignDocuments(UserInfo user,string campaignCode)
        {
            var list = new List<DocumentModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_WORK_ORDER_CARD_CAMPAIGN_DOCUMENTS");
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

        public void AddCampaign(UserInfo user,WorkOrderCampaignModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_ADD_WORK_ORDER_DETAIL_CAMPAIGN");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(model.WorkOrderId));
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

        public VehicleNoteDetailModel GetVehicleNote(UserInfo user,long id, long workOrderId)
        {
            var model = new VehicleNoteDetailModel();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_WOR_ORDER_CARD_VEHICLE_NOTE");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(workOrderId));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "ID_VEHICLE_NOTE", DbType.Int64, MakeDbNull(id));
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
        public VehicleNoteDetailModel GetVehicleNotePopup(UserInfo user,long id, long workOrderId)
        {
            var model = new VehicleNoteDetailModel();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_WOR_ORDER_CARD_VEHICLE_NOTE_POPUP");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(workOrderId));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "ID_VEHICLE_NOTE", DbType.Int64, MakeDbNull(id));
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
        public VehicleNoteDetailModel GetDealerNote(UserInfo user,long id, long workOrderId)
        {
            var model = new VehicleNoteDetailModel();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_WOR_ORDER_CARD_DEALER_NOTE");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(workOrderId));
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
                        model.NoteId = reader["ID_WO_DEALER_NOTE"].GetValue<int>();
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
        public void AddVehicleNote(UserInfo user,WorkOrderVehicleNoteModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_ADD_WORK_ORDER_CARD_VEHICLE_NOTE");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(model.WorkOrderId));
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
        public void AddDealerNote(UserInfo user,WorkOrderVehicleNoteModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_ADD_WORK_ORDER_CARD_DEALER_NOTE");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(model.WorkOrderId));
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

        public WorkOrderCardWarrantyModel GetWarrantyData(UserInfo user,long workOrderId, long workOrderDetailId)
        {
            var model = new WorkOrderCardWarrantyModel
            {
                WorkOrderDetailId = workOrderDetailId,
                WorkOrderId = workOrderId
            };
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_WORK_ORDER_CARD_WARRANTY_PARTS");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(workOrderId));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "ID_WORK_ORDER_DETAIL", DbType.Int64, MakeDbNull(workOrderDetailId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new WarrantyDetailItem
                        {
                            PartId = reader["PART_ID"].GetValue<int>(),
                            PartName = reader["PART_NAME"].ToString(),
                            Quantity = reader["QUANTITY"].GetValue<decimal>(),
                        };
                        model.Items.Add(item);
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
        public void AddLabourPrice(UserInfo user,AddLabourPrice model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_CHANGE_WORK_ORDER_CARD_DETAIL_LABOUR_PRICE");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(model.WorkOrderId));
                db.AddInParameter(cmd, "ID_WORK_ORDER_DETAIL", DbType.Int64, MakeDbNull(model.WorkOrderDetailId));
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
        public void ChangePriceList(UserInfo user,ChangePriceListModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_CHANGE_WORK_ORDER_CARD_DETAIL_PRICE_LIST");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(model.WorkOrderId));
                db.AddInParameter(cmd, "ID_WORK_ORDER_DETAIL", DbType.Int64, MakeDbNull(model.WorkOrderDetailId));
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
        public string GetCustomerNote(long workOrderId)
        {
            string result = "";
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_WORK_ORDER_CUSTOMER_NOTE");
                db.AddInParameter(cmd, "WORK_ORDER_ID", DbType.Int64, MakeDbNull(workOrderId));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = reader["NOTE"].ToString();
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
            return result;
        }
        public decimal GetSparePartVatRatio(UserInfo user,long partId)
        {
            decimal result;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_SPARE_PART_VAT_RATIO");
                db.AddInParameter(cmd, "ID_PART", DbType.Int64, partId);
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddOutParameter(cmd, "VAT_RATIO", DbType.Decimal, 6);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                result = db.GetParameterValue(cmd, "VAT_RATIO").GetValue<decimal>();
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
                var cmd = db.GetStoredProcCommand("P_LIST_FAILURE_CODES_FOR_WORK_ORDER_CARD_COMBO");
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
        public List<ComboBoxModel> ListFailureCodes2(UserInfo user)
        {
            var list = new List<ComboBoxModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_FAILURE_CODES_FOR_WORK_ORDER_CARD_COMBO");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new ComboBoxModel
                        {
                            Value = reader["ID_APPOINTMENT_INDICATOR_FAILURE_CODE"].GetValue<int>(),
                            Text = reader["DESCRIPTION"].ToString(),
                            Description = reader["CODE"].ToString()
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
        public void UpdateFailureCode(UserInfo user,long workOrderId, long workOrderDetailId, string failureCodeId, out int errorNo,
            out string errorMessage)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_UPDATE_WORK_ORDER_DETAIL_FAILURE_CODE");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(workOrderId));
                db.AddInParameter(cmd, "ID_WORK_ORDER_DETAIL", DbType.Int64, MakeDbNull(workOrderDetailId));
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


        public void UpdateDuration(UserInfo user,WorkOrderQuantityDataModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_UPDATE_WORK_ORDER_DETAIL_DURATION");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(model.WorkOrderId));
                db.AddInParameter(cmd, "ID_WORK_ORDER_DETAIL", DbType.Int64, MakeDbNull(model.WorkOrderDetailId));
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

        public List<CampaignCheckModel> GetCampaignCheckList(UserInfo user,long workOrderId)
        {
            var list = new List<CampaignCheckModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_CHECK_WORK_ORDER_CARD_MANDATORY_CAMPAIGNS");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(workOrderId));
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


    }
}

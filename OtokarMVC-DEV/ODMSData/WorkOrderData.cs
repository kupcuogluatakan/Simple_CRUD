using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Dynamic;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel;
using ODMSModel.VehicleBodywork;
using ODMSModel.WorkOrder;
using ODMSModel.WorkOrderCard;
using ODMSModel.GuaranteeRequestApproveDetail;

namespace ODMSData
{
    public class WorkOrderData : DataAccessBase
    {
        public List<SelectListItem> ListWorkOrderAsSelectListItem(int? vehicleId, int? dealerId)
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_WORK_ORDER_COMBO");
                db.AddInParameter(cmd, "VEHICLE_ID", DbType.Int32, MakeDbNull(vehicleId));
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(dealerId));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["ID_WORK_ORDER"].ToString(),
                            Text = reader["ID_WORK_ORDER"].ToString()
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
        /// <summary>
        /// changes status id of the give work order 
        /// </summary>
        /// <param name="workOrderId">Id of the workorder</param>
        /// <param name="statusId">status id </param>
        /// <param name="errorNo">error no</param>
        /// <param name="errorMessage">message if any error occurs</param>
        public void ChangeWorkOrderStatus(UserInfo user, long workOrderId, int statusId, out int errorNo, out string errorMessage)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_CHANGE_WORKORDER_STATUS");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(workOrderId));
                db.AddInParameter(cmd, "WO_CANCEL_REASON", DbType.String, MakeDbNull(""));
                db.AddInParameter(cmd, "WORK_ORDER_STATUS", DbType.Int32, MakeDbNull(statusId));
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

        public List<WorkOrderListModel> ListWorkOrders(UserInfo user,WorkOrderListModel model, out int total)
        {
            var result = new List<WorkOrderListModel>();
            total = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_WORK_ORDERS");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(model.DealerId));
                db.AddInParameter(cmd, "CUSTOMER_NAME", DbType.String, MakeDbNull(model.CustomerName));
                db.AddInParameter(cmd, "VEHICLE_PLATE", DbType.String, MakeDbNull(model.VehiclePlate));
                db.AddInParameter(cmd, "ID_WORK_ORDER",DbType.Int64, MakeDbNull(model.WorkOrderId));
                db.AddInParameter(cmd, "ID_WORK_ORDER_STAT", DbType.Int32, MakeDbNull(model.WorkOrderStatusId));
                db.AddInParameter(cmd, "VIN_NO", DbType.String, MakeDbNull(model.VinNo));
                db.AddInParameter(cmd, "VEHICLE_TYPE_ID", DbType.String, MakeDbNull(model.VehicleType));
                db.AddInParameter(cmd, "START_DATE", DbType.DateTime, model.StartDate);
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, model.EndDate);
                AddPagingParametersWithLanguage(user,cmd, model);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new WorkOrderListModel
                        {
                            WorkOrderId = reader["ID_WORK_ORDER"].GetValue<long>(),
                            DealerName = reader["DEALER_NAME"].ToString(),
                            CustomerName = reader["CUSTOMER_NAME"].ToString(),
                            VehiclePlate = reader["PLATE"].ToString(),
                            WorkOrderDate = reader["WO_DATE"].GetValue<DateTime>(),
                            WorkOrderStatus = reader["STATUS_DESC"].ToString(),
                            EngineNo = reader["ENGINE_NO"].ToString(),
                            VehicleId = reader["ID_VEHICLE"].GetValue<Int32>(),
                            VinNo = reader["VIN_NO"].ToString(),
                            VehicleCode = reader["V_CODE_KOD"].ToString(),
                            VehicleModel = reader["VEHICLE_MODEL"].ToString(),
                            VehicleType = reader["VEHICLE_TYPE"].ToString(),
                            WarrantyStartDate = reader["WARRANTY_START_DATE"].GetValue<DateTime>(),
                            WarrantyEndDate = reader["WARRANTY_END_DATE"].GetValue<DateTime>(),
                            WarrantyEndKilometer = reader["WARRANTY_END_KM"].ToString(),
                            DealerSSID = reader["DEALER_SSID"].ToString()
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

        public WorkOrderViewModel GetWorkOrder(UserInfo user, long workOrderId)
        {
            var model = new WorkOrderViewModel();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_WORK_ORDER");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(workOrderId));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        model.WorkOrderId = reader["ID_WORK_ORDER"].GetValue<int>();
                        model.AppointmentTypeId = reader["APPOINTMENT_TYPE_ID"].ToString();
                        model.AppointmentId = reader["APPOINTMENT_ID"].GetValue<int>();
                        model.CustomerId = reader["CUSTOMER_ID"].GetValue<int>();
                        model.Stuff = reader["RESP_CONSULTANT"].ToString();
                        model.WorkOrderDate = reader["WO_DATE"].GetValue<DateTime>();
                        model.VehicleId = reader["VEHICLE_ID"].GetValue<int>();
                        model.WorkOrderStatusId = reader["ID_WORK_ORDER_STAT"].GetValue<int>();
                        model.VehicleKM = reader["VEHICLE_KM"].GetValue<int>();
                        model.VinNo = reader["VIN_NO"].ToString();
                        model.VehiclePlate = reader["PLATE"].ToString();
                        model.DeliveryTime = reader["DELIVERY_TIME"].GetValue<DateTime>();
                        model.AppointmentDate = reader["APPOINTMENT_DATE"].GetValue<DateTime>();
                        model.Note = reader["TX_NOTES"].ToString();
                        model.CustomerName = reader["CUSTOMER_NAME"].ToString();
                        model.CustomerSurName = reader["CUSTOMER_LAST_NAME"].ToString();
                        model.CustomerPhone = reader["CUSTOMER_PHONE"].ToString();
                        model.VehicleTypeName = reader["MODEL_TYPE_NAME"].ToString();
                        model.VehicleModel = reader["MODEL_NAME"].ToString();
                        model.AppointmentType = reader["APPOINTMENT_TYPE_NAME"].ToString();
                        model.DealerId = reader["DEALER_ID"].GetValue<int>();
                        model.DealerName = reader["DEALER_NAME"].ToString();
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

        public object GetWorkOrderData(int id, string type)
        {
            dynamic obj = new { };
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_WORK_ORDER_DATA");
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
                                    CustomerFirstName = reader["CUSTOMER_FIRSTNAME"].ToString(),
                                    CustomerSurName = reader["CUSTOMER_SURNAME"].ToString(),
                                    CustomerPhone = reader["CUSTOMER_PHONE"].ToString(),
                                    CustomerAddress = reader["CUSTOMER_ADDRESS"].ToString(),
                                    VehicleId = reader["VEHICLE_ID"].GetValue<int>(),
                                    VehicleModelCode = reader["VEHICLE_MODEL_CODE"].ToString(),
                                    VehicleModel = reader["VEHICLE_MODEL_NAME"].ToString(),
                                    VehiclePlate = reader["PLATE"].ToString(),
                                    VehicleColor = reader["COLOR"].ToString(),
                                    VehicleType = reader["VEHICLE_TYPE"].ToString(),
                                    VehicleTypeName = reader["VEHICLE_TYPE_NAME"].ToString(),
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
                                    CustomerFirstName = reader["CUSTOMER_FIRSTNAME"].ToString(),
                                    CustomerPhone = reader["CUSTOMER_PHONE"].ToString(),
                                    CustomerAddress = reader["CUSTOMER_ADDRESS"].ToString()
                                };
                                break;
                            case "Vehicle":
                                obj = new
                                {
                                    VehicleModelCode = reader["VEHICLE_MODEL_CODE"].ToString(),
                                    VehicleModel = reader["VEHICLE_MODEL_NAME"].ToString(),
                                    VehiclePlate = reader["PLATE"].ToString(),
                                    VehicleType = reader["VEHICLE_TYPE"].ToString(),
                                    VehicleTypeName = reader["VEHICLE_TYPE_NAME"].ToString(),
                                    CustomerId = reader["CUSTOMER_ID"].ToString(),
                                    CustomerName = reader["CUSTOMER_NAME"].ToString(),
                                    CustomerFirstName = reader["CUSTOMER_FIRSTNAME"].ToString(),
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

        public int? GetLastWorkOrderId(int dealerId)
        {
            int? workOrderId = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_LAST_WORK_ORDER");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, dealerId);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        workOrderId = reader["ID_WORK_ORDER"].GetValue<int>();
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
            return workOrderId;
        }

        public int? GetLastWorkOrderDetailId(int workOrderId)
        {
            int? workOrderDetailId = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_LAST_WORK_ORDER_DETAIL");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int32, workOrderId);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        workOrderDetailId = reader["ID_WORK_ORDER_DETAIL"].GetValue<int>();
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
            return workOrderDetailId;
        }

        public int? GetLastWorkOrderPickingId(int dealerId)
        {
            int? id = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_LAST_WORK_ORDER_PICKING");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, dealerId);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = reader["ID_WORK_ORDER_PICKING_MST"].GetValue<int>();
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
            return id;
        }

        public int? GetLastWorkOrderPickingDetailId(int pickingId)
        {
            int? id = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_LAST_WORK_ORDER_PICKING_DETAIL");
                db.AddInParameter(cmd, "ID_WORK_ORDER_PICKING_MST", DbType.Int32, pickingId);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = reader["ID_WORK_ORDER_PICKING_DET"].GetValue<int>();
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
            return id;
        }

        public void DMLWorkOrder(UserInfo user, WorkOrderViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_WORK_ORDER");
                cmd.CommandTimeout = 120;
                db.AddParameter(cmd, "ID_WORK_ORDER", DbType.Int32, ParameterDirection.InputOutput, "ID_WORK_ORDER", DataRowVersion.Default, model.WorkOrderId);
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
                model.WorkOrderId = db.GetParameterValue(cmd, "ID_WORK_ORDER").GetValue<int>();
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
        /// <summary>
        /// returns workordercard without details
        /// </summary>
        /// <param name="workOrderId"></param>
        /// <returns></returns>
        public WorkOrderCardModel GetWorkOrderCard(UserInfo user, long workOrderId)
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
                        model.AppointmentId = reader["APPOINTMENT_ID"].GetValue<long>();
                        model.AppointmentDate = reader["APPOINTMENT_DATE"].GetValue<DateTime?>() == default(DateTime)
                            ? null
                            : reader["APPOINTMENT_DATE"].GetValue<DateTime?>();
                        model.AppointmentType = reader["APPOINTMENT_TYPE_NAME"].ToString();
                        model.WorkOrderDate = reader["WO_DATE"].GetValue<DateTime>();
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
                        model.DealerName = reader["DEALER_SHRT_NAME"].ToString();
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
                            PdiCheck = reader["PDI_CHCK"].GetValue<bool>(),
                            IsPdiAccomplished = reader["IS_PDI_ACCOMPLISHED"].GetValue<bool>(),
                            VehicleCode = reader["VEHICLE_CODE"].ToString(),
                            VehicleModel = reader["MODEL_KOD"].ToString(),
                            VehicleType = reader["VEHICLE_TYPE"].ToString(),
                            WarrantyEndKilometer = reader["WARRANTY_END_KM"].GetValue<long?>(),
                            VatExclude = reader["VAT_EXCLUDE"].GetValue<bool>(),
                            IsPdiApplicable = reader["IS_PDI_APPLICABLE"].GetValue<bool>(),
                            IsCampaignApplicable = reader["IS_CAMPAIGN_APPLICABLE"].GetValue<bool>(),
                            Location = reader["LOCATION"].ToString(),
                            ResponsiblePerson = reader["RESPONSIBLE_PERSON"].ToString(),
                            ResponsiblePersonPhone = reader["RESPONSIBLE_PERSON_PHONE"].ToString(),
                            EngineType = reader["ENGINE_TYPE"].ToString(),
                            PeriodicMaintCount = reader["PERIODIC_MAINT_COUNT"].GetValue<int>()
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


        public int CheckFleet(UserInfo user, int customerId, int vehicleId)
        {
            var applicableFleetId = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_WORK_ORDER_APPLICABLE_FLEET_ID");
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

        public ModelBase CancelWorkOrder(UserInfo user, WorkOrderCancelModel model)
        {

            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_CANCEL_WORK_ORDER_CARD", model.WorkOrderId, model.CancelReason, model.VehicleId, MakeDbNull(user.UserId), null, null);
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


        public List<SelectListItem> ListWorkOrderStatus(UserInfo user)
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_WORK_ORDER_STAT_COMBO");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["VALUE"].GetValue<string>(),
                            Text = reader["TEXT"].GetValue<string>()
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

        public void GetBodyworkFromVehicle(VehicleBodyworkViewModel filter)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_BODYWORK_FROM_VEHICLE");

                db.AddInParameter(cmd, "VEHICLE_ID", DbType.Int32, MakeDbNull(filter.VehicleId));

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        filter.BodyworkCode = dr["BODYWORK_CODE"].GetValue<string>();
                        filter.CityId = dr["ID_CITY"].GetValue<int>();
                        filter.CountryId = dr["ID_COUNTRY"].GetValue<int>();
                        filter.DealerId = dr["ID_DEALER"].GetValue<int>();
                        filter.Manufacturer = dr["MANUFACTURER"].GetValue<string>();
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

        public void DMLBodyWorkForWorkOrder(UserInfo user, VehicleBodyworkViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_BODYWORK_FOR_WORKORDER");

                db.AddInParameter(cmd, "BODYWORK_CODE", DbType.String, MakeDbNull(model.BodyworkCode));
                db.AddInParameter(cmd, "CITY_ID", DbType.Int32, MakeDbNull(model.CityId));
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(model.DealerId));
                db.AddInParameter(cmd, "MANUFACTURER", DbType.String, MakeDbNull(model.Manufacturer));
                db.AddInParameter(cmd, "VEHICLE_ID", DbType.Int32, MakeDbNull(model.VehicleId));
                db.AddInParameter(cmd, "WORK_ORDER_ID", DbType.Int64, MakeDbNull(model.WorkOrderId));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);

                cmd.ExecuteNonQuery();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").GetValue<string>();

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

        public List<PeriodicMaintHistoryModel> GetPeriodicMaintHistory(long workOrderId)
        {
            List<PeriodicMaintHistoryModel> returnModel = new List<PeriodicMaintHistoryModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_PERIODIC_MAINT_HISTORY_BY_VEHICLE");

                db.AddInParameter(cmd, "WO_ID", DbType.String, MakeDbNull(workOrderId));
                CreateConnection(cmd);

                cmd.ExecuteNonQuery();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        returnModel.Add(new PeriodicMaintHistoryModel
                        {
                            AdminDesc = reader["ADMIN_DESC"].GetValue<string>()
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
            return returnModel;
        }

        public CustomerChangeModel GetCustomerChangeData(int customerId, int vehicleCustomerId)
        {
            var model = new CustomerChangeModel();
            model.CustomerId = customerId;
            model.VehicleCustomerId = vehicleCustomerId;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_WORK_ORDER_CUSTOMER_CHANGE_DATA");
                db.AddInParameter(cmd, "CUSTOMER_ID", DbType.Int64, MakeDbNull(customerId));
                db.AddInParameter(cmd, "VEHICLE_CUSTOMER_ID", DbType.Int32, MakeDbNull(vehicleCustomerId));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        model.CustomerName = reader["CustomerName"].GetValue<string>();
                        model.VehicleCustomerName = reader["VehicleCustomerName"].GetValue<string>();
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
    }
}

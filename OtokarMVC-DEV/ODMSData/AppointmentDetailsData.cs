using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;
using ODMSCommon;
using ODMSModel.AppointmentDetails;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class AppointmentDetailsData : DataAccessBase
    {
        public void DMLAppointmentDetails(UserInfo user, AppointmentDetailsViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_APPOINTMENT_DETAIL_MAIN");
                db.AddParameter(cmd, "APPOINTMENT_INDICATOR_ID", DbType.Int32, ParameterDirection.InputOutput, "APPOINTMENT_INDICATOR_ID", DataRowVersion.Default, model.AppointmentIndicatorId);
                db.AddInParameter(cmd, "ID_APPOINTMENT", DbType.Int32, model.AppointmentId);
                db.AddInParameter(cmd, "APPOINTMENT_INDICATOR_SUB_CATEGORY_ID", DbType.Int32, model.SubCategoryId);
                db.AddInParameter(cmd, "FAILURE_CODE_ID", DbType.Int32, model.FailureCodeId);
                db.AddInParameter(cmd, "OPERATION_ID", DbType.String, CheckOperationId(model));
                db.AddInParameter(cmd, "INDIC_TYPE_CODE", DbType.String, model.IndicatorTypeCode);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.AppointmentIndicatorId = db.GetParameterValue(cmd, "APPOINTMENT_INDICATOR_ID").GetValue<int>();
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

        private string CheckOperationId(AppointmentDetailsViewModel filter)
        {
            var rValue = string.Empty;

            rValue = filter.MaintKId > 0 ? filter.MaintKId.ToString() : rValue;
            rValue = filter.MaintPId > 0 ? filter.MaintPId.ToString() : rValue;
            rValue = !string.IsNullOrEmpty(filter.CampaignCode) ? filter.CampaignCode : rValue;

            return rValue;
        }

        public AppointmentDetailsViewModel GetAppointmentDetails(UserInfo user, int appointmentIndicatorId)
        {
            var appointmentDetail = new AppointmentDetailsViewModel();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_APPOINTMENT_DETAILS");
                db.AddInParameter(cmd, "APPOINTMENT_INDICATOR_ID", DbType.Int32, MakeDbNull(appointmentIndicatorId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        appointmentDetail.AppointmentId = reader["APPOINTMENT_ID"].GetValue<int>();
                        appointmentDetail.AppointmentIndicatorId = reader["APPOINTMENT_INDICATOR_ID"].GetValue<int>();
                        appointmentDetail.SubCategoryId = reader["APPOINTMENT_INDICATOR_SUB_CATEGORY_ID"].GetValue<int>();
                        appointmentDetail.CategoryId = reader["APPOINTMENT_INDICATOR_CATEGORY_ID"].GetValue<int>();
                        appointmentDetail.MainCategoryId = reader["APPOINTMENT_INDICATOR_MAIN_CATEGORY_ID"].GetValue<int>();
                        appointmentDetail.MainCategoryName = reader["MAIN_CATEGORY_NAME"].GetValue<string>();
                        appointmentDetail.CategoryName = reader["CATEGORY_NAME"].GetValue<string>();
                        appointmentDetail.SubCategoryName = reader["SUB_CATEGORY_NAME"].GetValue<string>();
                        appointmentDetail.FailureCodeId = reader["FAILURE_CODE_ID"].GetValue<string>();
                        appointmentDetail.IndicatorTypeCode = reader["INDICATOR_TYPE_CODE"].GetValue<string>();
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

        public List<AppointmentDetailsListModel> ListAppointmentDetails(UserInfo user,AppointmentDetailsListModel filter, out int totalCount)
        {
            var retVal = new List<AppointmentDetailsListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_APPOINTMENT_DETAILS");
                db.AddInParameter(cmd, "ID_APPOINTMENT", DbType.Int32, MakeDbNull(filter.AppointmentId));
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var appointmentDetail = new AppointmentDetailsListModel
                        {
                            AppointmentId = filter.AppointmentId,
                            AppointmentIndicatorId = reader["APPOINTMENT_INDICATOR_ID"].GetValue<int>(),
                            SubCategoryId = reader["APPOINTMENT_INDICATOR_SUB_CATEGORY_ID"].GetValue<int>(),
                            CategoryId = reader["APPOINTMENT_INDICATOR_CATEGORY_ID"].GetValue<int>(),
                            MainCategoryId = reader["APPOINTMENT_INDICATOR_MAIN_CATEGORY_ID"].GetValue<int>(),
                            MainCategoryName = reader["MAIN_CATEGORY_NAME"].GetValue<string>(),
                            CategoryName = reader["CATEGORY_NAME"].GetValue<string>(),
                            SubCategoryName = reader["SUB_CATEGORY_NAME"].GetValue<string>(),
                            FailureCode = reader["FAILURE_CODE"].GetValue<string>(),
                            IndicType = reader["INDICATOR_TYPE_NAME"].GetValue<string>(),
                            IndicTypeValue = reader["INDICATOR_TYPE_CODE"].GetValue<string>()
                        };
                        retVal.Add(appointmentDetail);
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

        public double GetTotalPriceForAppointment(int appId)
        {
            double totalPrice = 0;
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_APPOINTMENT_TOTAL_PRICE");

                db.AddInParameter(cmd, "APPOINTMENT_ID", DbType.Int32, MakeDbNull(appId));

                CreateConnection(cmd);

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        totalPrice = dr["TOTAL_PRICE"].GetValue<double>();
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
            return totalPrice;
        }

        public List<SelectListItem> GetAppointmentIndicType(UserInfo user, int appointmentId, out int vehicleId)
        {
            var listItem = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_APPOINTMENT_INDICATOR_TYPE_COMBO");
                db.AddInParameter(cmd, "APPOINTMENT_ID", DbType.Int32, MakeDbNull(appointmentId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "VEHICLE_ID", DbType.Int32, 10);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var item = new SelectListItem
                        {
                            Value = dr["INDICATOR_TYPE_CODE"].GetValue<string>(),
                            Text = dr["INDICATOR_TYPE_NAME"].GetValue<string>()
                        };

                        listItem.Add(item);
                    }
                }
                vehicleId = db.GetParameterValue(cmd, "VEHICLE_ID").GetValue<int>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return listItem;
        }

        public List<SelectListItem> GetCampaignCodeByVehicleId(UserInfo user, int vehicleId)
        {
            var listItem = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_APPOINTMENT_INDICATOR_CAMPAIGN_BY_VEHICLE_ID");
                db.AddInParameter(cmd, "VEHICLE_ID", DbType.Int32, MakeDbNull(vehicleId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var item = new SelectListItem
                        {
                            Value = dr["CAMPAIGN_CODE"].GetValue<string>(),
                            Text = dr["CAMPAIGN_NAME"].GetValue<string>()
                        };

                        listItem.Add(item);
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
            return listItem;
        }

        public List<SelectListItem> GetMaintCoupon(UserInfo user)
        {
            var listItem = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_APPOINTMENT_INDICATOR_MAINT_COUPON");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var item = new SelectListItem
                        {
                            Value = dr["ID_MAINT"].GetValue<string>(),
                            Text = dr["MAINT_NAME"].GetValue<string>()
                        };

                        listItem.Add(item);
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
            return listItem;
        }

        public List<SelectListItem> GetMainByVehicle(UserInfo user, int vehicleId)
        {
            var listItems = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_WORK_ORDER_PERIODIC_MAINT");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int32, MakeDbNull(0));
                db.AddInParameter(cmd, "ID_VEHICLE", DbType.Int32, MakeDbNull(vehicleId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var item = new SelectListItem
                        {
                            Text = dr["MAINT_NAME"].GetValue<string>(),
                            Value = dr["ID_MAINT"].GetValue<string>()
                        };

                        listItems.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listItems;
        }
    }
}
